using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class AgendarConsulta : System.Web.UI.Page
{//default(classes e structs) é private, por isso não coloquei
    static SqlDataReader rdr;//leitor de querys BD
    static List<Especialidade> especs = new List<Especialidade>();//lista de especialidades
    static List<Medico> medicos = new List<Medico>();//lista de médicos
    static List<DateTime> horarios = new List<DateTime>();//lista de horários
    static int codMedSel =0;//código do médico selecionado 
    static DateTime data;//data selecionada
    static string usuario;//nome de usuário do paciente selecionado
    static string hora, min, seg;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["usuario"].ToString()))//se o usuário sa não está 
                    Response.Redirect("LogonSA.aspx");//definido, significa que a pessoa não está logada
            }
            catch {
                    Response.Redirect("LogonSA.aspx");
            }
            try { 
                if (Conexao.conexao.State != System.Data.ConnectionState.Open)
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
                        lsbEspec.Items[lsbEspec.Items.Count - 1].Value = especs.Last().CodEspecialidade.ToString();
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
            catch (Exception ex)
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
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string nomeEspecSel = lsbEspec.SelectedItem.Text;//nome de especialidade selecionado

            if (lsbEspec.SelectedIndex <= 0)
            {
                lblErro.Text = "Selecione uma especialidade!";
                return;
            }

            string codEspecSel = lsbEspec.SelectedValue;//descobrir codEspecialidade selecionado
            string query = "select crm, nome, codEspecialidade from MedicoEspecialista_view where codEspecialidade=@codEspec";//selecionar os médicos com determinada especialização
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@codEspec", codEspecSel));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                medicos.Clear();

                while (rdr.Read())
                {
                    int crm = Convert.ToInt32(rdr["crm"]);
                    medicos.Add(new Medico(crm, rdr["nome"]+"", Convert.ToInt32(rdr["codEspecialidade"])));//populando a lista medicos	
                    lsbMedico.Items.Add(medicos.Last().Nome);//adicionando os nomes dos medicos no listbox 

                    lsbMedico.Items[lsbMedico.Items.Count - 1].Value = medicos.Last().CRM.ToString();
                }
                lsbMedico.Visible = true;
            }
            else
            {
                lblErro.Text = "Não há nenhum especialista em " + nomeEspecSel + " cadastrado!";
            }

            rdr.Close();
            Conexao.conexao.Close();
            rdr = null;
        }
        catch(Exception)
        {
            if (rdr != null)
                rdr.Close();

            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();

            rdr = null;
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
    {//evento do nome do médico for selecionado
        string nomeMedSel = lsbMedico.SelectedItem.Text;

        if (lsbMedico.SelectedIndex <= 0)
        {
            lblErro.Text = "Selecione um médico!";

            return;
        }

        codMedSel = medicos.Find(x => x.Nome == nomeMedSel).CRM;//codigo do medico selecionado
        txtData.Visible = true;
        txtPaciente.Visible = true;
    }
    private void descobrirHorariosDisponiveis(DateTime data){        
          string query = "select * from HorarioDispMed_view " +
            "where crm=@crm and YEAR(dataHoraConsulta)=YEAR(@data)"+
            "and MONTH(dataHoraConsulta)= MONTH(@data) " +
            "and DAY(dataHoraConsulta)= DAY(@data)";
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();
                    SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@crm", codMedSel);
                    cmd.Parameters.AddWithValue("@data", data);

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
                            if (horarios.Exists(x => x.Equals(horarioAtual)))//se em um dos horários 
                            {                                                //já está marcada outra consulta
                                indiceHorarioAtual = horarios.FindIndex(x => x.Equals(horarioAtual));
                                horarios.RemoveAt(indiceHorarioAtual);    //removerá da lista de  
                                if (!meiaHora)                            //horários aqueles nos quais já foi marcado consulta
                                    horarios.RemoveAt(indiceHorarioAtual);//se durar 1 hora,  
                            }                                             //removerá o próximo horário também
                        }                        
                    }//fim do if(rdr.HasRows)

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

        rdr.Close();
                    rdr = null;
        Conexao.conexao.Close();
    }

    private bool podeAgendar()
    {
        if (Conexao.conexao.State != System.Data.ConnectionState.Open)
            Conexao.conexao.Open();

        string query = "select * from MarcouMsmDia_view " +
        "where crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)"+
        "and MONTH(dataHoraConsulta)= MONTH(@data) and " +
        "DAY(dataHoraConsulta)= DAY(@data)";//seleciona os pacientes marcados com
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);//determinado médico em determinada data
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@crm", codMedSel));
        cmd.Parameters.Add(new SqlParameter("@data", data));
        rdr = cmd.ExecuteReader();
        string usuarioAchado = "";
        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                usuarioAchado = rdr.GetValue(0).ToString();
                if (usuarioAchado == usuario)
                {//se algum dos pacientes já marcados for o 
                    rdr.Close();
                    rdr = null;
                    Conexao.conexao.Close();
                    return false;
                }//paciente que deseja marcar consulta agora,
            }//ele não poderá marcar, já que não pode marcar
        }//com o mesmo médico no mesmo dia 2 consultas com o mesmo paciente

        rdr.Close();
        rdr = null;
        Conexao.conexao.Close();
        return true;
    }

    protected void btnAgendar_Click(object sender, EventArgs e)
    {
        try {
            string horarioSel = lsbHorarios.SelectedItem.Text;

            if (lsbHorarios.SelectedIndex <= 0)
            {
                lblErro.Text = "Selecione um horário!";

                return;
            }

            string usuario = txtPaciente.Text;
            usuario = usuario.Trim();
            if (!string.IsNullOrEmpty(usuario)) {
                var diaSelecionado  = DateTime.Parse(txtData.Text);
                var horaSelecionada = horarios[lsbHorarios.SelectedIndex - 1];

                DateTime dataSel = diaSelecionado.AddHours(horaSelecionada.Hour).AddMinutes(horaSelecionada.Minute);

                bool meiaHora = !rbHora.Checked;
                string insert = "AgendarConsulta_sp";
                Conexao.conexao.Open();
                SqlCommand cmdInsert = new SqlCommand(insert, Conexao.conexao);//insere na tabela consulta
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.Parameters.Add(new SqlParameter("@dataHoraConsulta", dataSel));
                cmdInsert.Parameters.Add(new SqlParameter("@meiaHora", meiaHora));
                cmdInsert.Parameters.Add(new SqlParameter("@crm", codMedSel));
                cmdInsert.Parameters.Add(new SqlParameter("@usuario", usuario));
                int linhasMod = cmdInsert.ExecuteNonQuery();//linhas da tabela do BD modificadas 

                if (linhasMod < 1)
                    lblErro.Text = "Verifique os dados e tente novamente!";

                if (linhasMod == 1)
                {
                    lblErro.Text = "Sucesso ao agendar!";

                    lblErro.ForeColor = System.Drawing.Color.Green;
                }
                    

                if (linhasMod > 1)
                    lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema !";

                Conexao.conexao.Close();
            }
            else {
                lblErro.Text = "Digite o nome de usuário do paciente que deseja marcar consulta !!";
            }
        }
        catch
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
    
    private bool dataValida(DateTime data){// A data só é válida se não for em um dia passado
        return data.CompareTo(DateTime.Now) >= 0;
}


    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        //data foi selecionada
        try
        {
            lsbHorarios.Visible = true;

            data = DateTime.Parse(txtData.Text);//Descobrir dia mostrado no calendário

            if (dataValida(data))
            {
                horarios.Clear();

                for (int i = 9; (i < 12 || i >= 14) && i < 17; i++)
                {// médico trabalha das 9 às 17 e das 12h às 14h tem horário de almoço
                    horarios.Add(new DateTime(data.Year, data.Month, data.Day, i, 0, 0));

                    if (i != 17)
                        horarios.Add(new DateTime(data.Year, data.Month, data.Day, i, 30, 0));

                    if (i == 11)
                        i = 13;
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
            if (rdr != null)
                rdr.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            rdr = null;
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbHorarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        var podeMarcarConsultaLonga = EhPossivelMarcarUmaHora();

        if (!podeMarcarConsultaLonga)
            rbHora.Checked = false;

        rbHora.Visible = podeMarcarConsultaLonga;
    }

    private bool EhPossivelMarcarUmaHora()
    {
        if (lsbHorarios.SelectedIndex <= 0)
            return false;

        if (lsbHorarios.SelectedIndex >= lsbHorarios.Items.Count - 1)
            return false;

        if (HorariosConsecutivosLivres())
            return true;

        return false;
    }

    private bool HorariosConsecutivosLivres()
    {
        var horarioAtual   = DateTime.Parse(lsbHorarios.SelectedItem.ToString());
        var proximaHorario = DateTime.Parse(lsbHorarios.Items[lsbHorarios.SelectedIndex + 1].ToString());

        return proximaHorario.Equals(horarioAtual.AddMinutes(30));
    }
}