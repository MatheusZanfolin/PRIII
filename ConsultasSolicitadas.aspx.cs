using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class ConsultasSolicitadas : System.Web.UI.Page
{//default(classes) é private, por isso não coloquei
    SqlDataReader rdr;
    List<Consulta> listaCons = new List<Consulta>();
    List<Medico> listaMed = new List<Medico>();
    List<Paciente> listaPac = new List<Paciente>();
    List<DateTime> horarios = new List<DateTime>();//lista de horários
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["usuario"].ToString()))
                    Response.Redirect("logonSA.aspx");
            }
            catch
            {
                Response.Redirect("logonSA.aspx");
            }
            try
            {
                string query = "select * from VerConsultaSolicitada_view";
                Conexao.conexao.Open();
                SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    Consulta consAtual = null;
                    Medico medAtual = null;
                    Paciente pacAtual = null;
                    while (rdr.Read())
                    {
                        consAtual = new Consulta(Convert.ToInt32(rdr.GetValue(0)), (DateTime)(rdr.GetValue(1)), Convert.ToInt32(rdr.GetValue(3)), rdr.GetValue(5).ToString());
                        listaCons.Add(consAtual);
                        medAtual = new Medico(Convert.ToInt32(rdr.GetValue(3)), rdr.GetValue(2).ToString());
                        listaMed.Add(medAtual);
                        pacAtual = new Paciente(rdr.GetValue(5).ToString(), rdr.GetValue(4).ToString());
                    }
                    
                    int qtdCons = listaCons.Count;//qtde de consultas
                    for (int i = 0; i < qtdCons; i++)
                    {
                        lsbConsulta.Items.Add(listaCons[i].CodConsulta.ToString());
                    }
                    for (int i = 0; i < qtdCons; i++)
                    {
                        lsbMedico.Items.Add(listaMed[i].Nome.ToString());
                    }
                    alterarCampos(listaCons[0].CodConsulta);
                }
                else
                {
                    lblErro.Text = "Não há nenhuma consulta solicitada!";
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
    private void alterarCampos(int codConsulta)
    {
        Consulta consAtual = listaCons.Find(x => x.CodConsulta == codConsulta);
        int qtdConsultas = listaCons.Count;
        int i;
        for (i = 0; i < qtdConsultas; i++)
        {
            if(lsbConsulta.Items[i].Text == codConsulta.ToString())
                break ;
        }
        if (i == qtdConsultas)// /*
            throw new Exception("Erro na lista de Consultas!");// */
        lsbConsulta.Items[i].Selected = true;
        int crmAtual = consAtual.Crm;
        Medico medAtual = listaMed.Find(x => x.CRM == crmAtual);
        int j;
        int qtdMedicos = listaMed.Count;
        for (j = 0; j < qtdMedicos; j++)
        {
            if (lsbMedico.Items[j].Text == medAtual.Nome)
                break;
        }
        if (j == qtdMedicos)// /*
            throw new Exception("Erro na lista de Médicos!");// */
        lsbMedico.Items[j].Selected = true;
        txtPaciente.Text = listaPac.Find(x => x.Usuario == consAtual.Usuario).Nome;
        int qtdHorarios = lsbHorarios.Rows;
        for(int k = qtdHorarios - 1; k >= 0; k--)
        {
            lsbHorarios.Items.RemoveAt(k);
        }
        DateTime dataAtual = consAtual.DataHoraConsulta;
        txtData.Text = dataAtual.Day.ToString() + '/'+ dataAtual.Month.ToString() + '/' + dataAtual.Year.ToString();
        rbHora.Checked = false;
        for ( i = 9; (i < 12 || i >= 14) && i < 18; i++)
        {// médico trabalha das 9 às 17 e das 12h às 14h tem horário de almoço
            horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 0, 0));
            horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 30, 0));
        }
        string query = "select * from HorarioDispMed_view " +
        "where crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)"
        +"and MONTH(dataHoraConsulta)= MONTH(@data) "
        +"and DAY(dataHoraConsulta)= DAY(@data)";
        DateTime horarioAtual;
        Conexao.conexao.Open();
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@crm", crmAtual));
        cmd.Parameters.Add(new SqlParameter("@data", dataAtual));
        rdr = cmd.ExecuteReader();
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
            string hora, min, seg;
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
            int indHorario;
            for (indHorario = 0; indHorario < lsbHorarios.Rows; indHorario++)
            {
                if (lsbHorarios.Items[i].Text == dataAtual.Hour + ":" + dataAtual.Minute + ":" + dataAtual.Second)
                    break;
            }
            lsbHorarios.Items[indHorario].Selected = true;
        }
        else
        {
            lblErro.Text = "Não há horários disponíveis para o médico " + medAtual.Nome + " no dia "+ dataAtual.Day +"/"+dataAtual.Month+"/"+dataAtual.Year;
        }
        rdr.Close();
        Conexao.conexao.Close();
        rdr = null;
    }
    protected void btnAgendar_Click(object sender, EventArgs e)
    {
        try
        {   int codConsulta = Convert.ToInt32(lsbConsulta.Items[lsbConsulta.SelectedIndex].Text);
            int crm = listaMed.Find(x => x.Nome == lsbMedico.Items[lsbMedico.SelectedIndex].Text).CRM;
            string horarioSel = lsbHorarios.Items[lsbHorarios.SelectedIndex].Value;
            DateTime dataSel = horarios.Find(x =>
            (x.Hour == Convert.ToInt32(horarioSel.Substring(0, 2))) &&
            (x.Minute == Convert.ToInt32(horarioSel.Substring(3, 2))) &&
            (x.Second == Convert.ToInt32(horarioSel.Substring(6, 2))));
            if (dataValida(dataSel))
            {
                bool meiaHora = !rbHora.Checked;
                Conexao.conexao.Open();
                string update = "AlterarConsultaSolicitar_sp";
                SqlCommand cmd = new SqlCommand(update, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add( new SqlParameter ("@codConsulta",codConsulta));
                cmd.Parameters.Add( new SqlParameter ("@dataHoraConsulta",dataSel));
                cmd.Parameters.Add( new SqlParameter ("@meiaHora",meiaHora));
                cmd.Parameters.Add( new SqlParameter ("@crm",crm));
                int linhasAlteradas = cmd.ExecuteNonQuery();

                if(linhasAlteradas == 1)
                    lblErro.Text = "Alteração da consulta de código "+codConsulta+" feita com sucesso!";
                if (linhasAlteradas < 1)
                    lblErro.Text = "Verifique os dados e tente novamente!";
                if (linhasAlteradas > 1)
                    lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema !";
                Conexao.conexao.Close();
            }
            else
            {
                lblErro.Text = "Data inválida!";
            }
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbConsulta_SelectedIndexChanged(object sender, EventArgs e)
    {
        alterarCampos(Convert.ToInt32(lsbConsulta.Items[lsbConsulta.SelectedIndex].Text));
        
    }

    protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            Medico medAtual = listaMed.Find(x => x.Nome == lsbMedico.Items[lsbMedico.SelectedIndex].Text);
            string query = "select * from HorarioDispMed_view where " +
            "crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)" +
            "and MONTH(dataHoraConsulta)= month(@data) " +
            "and DAY(dataHoraConsulta)= day(@data)";
            Conexao.conexao.Open();
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@crm", medAtual.CRM));
            cmd.Parameters.Add(new SqlParameter("@data", DateTime.Parse(txtData.Text)));
            rdr = cmd.ExecuteReader();
            DateTime dataAtual = DateTime.Parse(txtData.Text);
            DateTime horarioAtual;
            int indiceHorarioAtual;
            if (rdr.HasRows)
            {
                bool meiaHora;
                for (int i = horarios.Count - 1; i >= 0; i--)
                    horarios.RemoveAt(i);
                for (int i = 9; (i < 12 || i >= 14) && i < 18; i++)
                {// médico trabalha das 9 às 17 e das 12h às 14h tem horário de almoço
                    horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 0, 0));
                    horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 30, 0));
                }
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
                Conexao.conexao.Close();
                string hora, min, seg;
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
            else {
                lblErro.Text = "Não há horários disponíveis para o médico " + medAtual.Nome + " no dia " + txtData.Text;
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
        try
        {
           DateTime data = DateTime.Parse(txtData.Text);//Descobrir dia mostrado no calendário

            if (dataValida(data))
            {
                for (int i = horarios.Count - 1; i >= 0; i--)
                    horarios.RemoveAt(i);
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

    private void descobrirHorariosDisponiveis(DateTime data)
    {
        int codMedSel = listaMed.Find(x => x.Nome == lsbMedico.Items[lsbMedico.SelectedIndex].Text).CRM;
        
        string query = "select * from HorarioDispMed_sp" +
        "where crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)" +
        "and MONTH(dataHoraConsulta)= MONTH(@data) and DAY(dataHoraConsulta)= DAY(@data)";
        Conexao.conexao.Open();
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
        cmd.CommandType = System.Data.CommandType.Text;
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
            Conexao.conexao.Close();
            string hora, min, seg;
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
        if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
            Conexao.conexao.Close();
        rdr = null;
    }

    private bool podeAgendar()
    {
        int codMedSel = listaMed.Find(x => x.Nome == lsbMedico.Items[lsbMedico.SelectedIndex].Text).CRM;
        DateTime data = DateTime.Parse(txtData.Text);
        string query = "select * from MarcouMsmDia_view where " +
        "crm=@crm and" +
        "YEAR(dataHoraConsulta)=YEAR(@data)"+
        "and MONTH(dataHoraConsulta)= MONTH(@data) " +
        "and DAY(dataHoraConsulta)= DAY(@data)";//seleciona os pacientes marcados com
        Conexao.conexao.Open();
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
                if (usuarioAchado == txtPaciente.Text)
                {//se algum dos pacientes já marcados for o 
                    rdr.Close();
                    rdr = null;
                    Conexao.conexao.Close();
                    return false;
                }//paciente que deseja marcar consulta agora
            }
        }

        rdr.Close();
        rdr = null;
        Conexao.conexao.Close();
        return true;
    }
}