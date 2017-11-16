using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class SolicitarConsulta : System.Web.UI.Page
{//default(classes e structs) é private, por isso nõa coloquei
    SqlDataReader rdr;//leitor de querys BD
    List<Especialidade> especs = new List<Especialidade>();//lista de especialidades
    List<Medico> medicos = new List<Medico>();//lista de médicos
    List<DateTime> horarios = new List<DateTime>();//lista de horários
    int codMedSel = 0;//código do médico selecionado 
    DateTime data;//data selecionada
    string usuarioOnline;//nome de usuário do paciente selecionado
    string hora, min, seg;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["paciente"].ToString()))
                    Response.Redirect("LogonPac.aspx");
            }
            catch
            {
                Response.Redirect("LogonPac.aspx");
            }
            try
            {
                Conexao.conexao.Open();
                lsbEspec.Visible = true;
                string query = "select * from Especialidades_view";//seleciona todas as especialidades
                SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {

                    while (rdr.Read())
                    {
                        especs.Add(new Especialidade((int)rdr.GetValue(0), rdr.GetValue(1).ToString()));    //populando a lista especs
                        lsbEspec.Items.Add(especs.Last().NomeEspecialidade);//adicionando os nomes das especs no listbox 

                    }
                }
                else
                {
                    lblErro.Text = "Não há nenhuma especialidade cadastrada!";

                }
                rdr.Close();
                rdr = null;
                Conexao.conexao.Close();
            }
            catch 
            {
                if (rdr != null)
                    rdr.Close();
                if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                    Conexao.conexao.Close();
                rdr = null;
                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
            }
        }
    }
    protected void lsbEspec_SelectedIndexChanged(object sender, EventArgs e)
    { //evento de o secretário seleiconar uma especialidade
        try
        {
            Conexao.conexao.Open();
            string nomeEspecSel = lsbEspec.Items[lsbEspec.SelectedIndex].Value;//nome de especialidade selecionado
            int codEspecSel = especs.Find(x => x.NomeEspecialidade == nomeEspecSel).CodEspecialidade;//descobri codEspecialidade selecionado
            string query = "MedicoEspecialista_sp ";//selecionar os médicos com determinada especialização
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codEspec", codEspecSel));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    medicos.Add(new Medico((int)rdr.GetValue(0), rdr.GetValue(1).ToString(), (int)rdr.GetValue(2)));//populando a lista medicos	
                    lsbMedico.Items.Add(medicos.Last().Nome);//adicionando os nomes dos medicos no listbox 
                }
                lsbMedico.Visible = true;
            }
            else
            {
                lblErro.Text = "Não há nenhum médico " + nomeEspecSel + " cadastrado!";
            }
            rdr.Close();
            rdr = null;
        }
        catch 
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
    {//evento do nome do médico for selecionado
        string nomeMedSel = lsbMedico.Items[lsbMedico.SelectedIndex].Value;
        codMedSel = medicos.Find(x => x.Nome == nomeMedSel).CRM;//codigo do medico selecionado
        txtData.Visible = true;
    }
    private void descobrirHorariosDisponiveis(DateTime data)
    {
        string query = "HorarioDispMed_sp";
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@crm", codMedSel));
        cmd.Parameters.Add(new SqlParameter("@data", data));
        rdr = cmd.ExecuteReader();
        DateTime horarioAtual;

        bool meiaHora;
        if (rdr.HasRows)
        {
            int indiceHorarioAtual;
            while (rdr.Read())
            {
                horarioAtual = (DateTime)rdr.GetValue(0);
                meiaHora = (bool)rdr.GetValue(1);
                if (horarios.Exists(x => x.Equals(horarioAtual)))
                {
                    indiceHorarioAtual = horarios.FindIndex(x => x.Equals(horarioAtual));
                    horarios.RemoveAt(indiceHorarioAtual);
                    if (!meiaHora)
                        horarios.RemoveAt(indiceHorarioAtual);
                }
            }
            rdr.Close();
            rdr = null;
            foreach (DateTime horario in horarios)
            {
                hora = horario.Hour.ToString();
                min = horario.Minute.ToString();
                seg = horario.Second.ToString();
                while (hora.Length < 2)
                    hora = "0" + hora;
                while (min.Length < 2)
                    min = "0" + min;
                while (seg.Length < 2)
                    seg = "0" + seg;
                lsbHorarios.Items.Add(hora + ":" + min + ":" + seg);
            }
            lsbHorarios.Visible = true;
        }
        if (rdr != null)
            rdr.Close();
        rdr = null;
    }

    private bool podeAgendar()
    {

        string query = "MarcouMsmDia_sp ";//seleciona os pacientes marcados com
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);//determinado médico em determinada data
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@crm", codMedSel));
        cmd.Parameters.Add(new SqlParameter("@data", data));
        rdr = cmd.ExecuteReader();
        string usuarioAchado = "";
        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                usuarioAchado = rdr.GetValue(0).ToString();
                if (usuarioAchado == usuarioOnline)
                {//se algum dos pacientes já marcados for o 
                    rdr.Close();
                    rdr = null;
                    return false;
                }//paciente que deseja marcar consulta agora
            }
        }

        rdr.Close();
        rdr = null;
        return true;
    }

    protected void btnAgendar_Click(object sender, EventArgs e)
    {
        try
        {
            string horarioSel = lsbHorarios.Items[lsbHorarios.SelectedIndex].Value;
            
                DateTime dataSel = horarios.Find(x =>
                (x.Hour == Convert.ToInt32(horarioSel.Substring(0, 2))) &&
                (x.Minute == Convert.ToInt32(horarioSel.Substring(3, 2))) &&
                (x.Second == Convert.ToInt32(horarioSel.Substring(6, 2))));
               // bool meiaHora = !rbHora.Checked;
                string insert = "AgendarConsultaPac_sp ";
                SqlCommand cmdInsert = new SqlCommand(insert, Conexao.conexao);//insere na tabela consulta
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.Parameters.Add(new SqlParameter("@dataHoraConsulta", dataSel));
                //cmdInsert.Parameters.Add(new SqlParameter("@meiaHora", meiaHora));
                cmdInsert.Parameters.Add(new SqlParameter("@crm", codMedSel));
                cmdInsert.Parameters.Add(new SqlParameter("@usuario", usuarioOnline));
                int linhasMod = cmdInsert.ExecuteNonQuery();//linhas da tabela do BD modificadas 
                if (linhasMod < 1)
                    lblErro.Text = "Verifique os dados e tente novamente!";
                if (linhasMod == 1)
                    lblErro.Text = "Sucesso ao agendar!";
                if (linhasMod > 1)
                    lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema !";

            }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    private bool dataValida(DateTime data)
    {// A DATA SÓ É VÁLIDA SE NÃO FOR NUM DIA PASSADO
        if (data.Year < DateTime.Now.Year)
            return false;
        if (data.Month < DateTime.Now.Month)
            return false;
        if (data.Day < DateTime.Now.Day)
            return false;
        return true;
    }


    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        //data foi selecionada
        try
        {
            data = DateTime.Parse(txtData.Text);//Descobrir dia mostrado no calendário

            if (dataValida(data))
            {
                for (int i = 9; (i < 12 || i >= 14) && i < 18; i++)
                {// médico trabalha das 9 às 17 e das 12h às 14h tem horário de almoço
                    horarios.Add(new DateTime(data.Year, data.Month, data.Day, i, 0, 0));
                    horarios.Add(new DateTime(data.Year, data.Month, data.Day, i, 30, 0));
                }

                if (podeAgendar())
                {

                    descobrirHorariosDisponiveis(data);
                    //ver os horários disponíveis do médico naquela data
                }//fim do if(podeAgendar())
                else
                {
                    lblErro.Text = "Erro! Este Paciente já marcou consulta com este médico nesta data!";
                }
            }//fim do if(dataValida())
            else
            {
                lblErro.Text = ("Selecione uma data futura!");

            }
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }

    }
}