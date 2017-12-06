using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;

public partial class ConsultasSolicitadas : System.Web.UI.Page
{//default(classes) é private, por isso não coloquei
    SqlDataReader rdr;
    static List<Consulta> listaCons = new List<Consulta>();
    static List<Medico> listaMed = new List<Medico>();
    static List<Paciente> listaPac = new List<Paciente>();
    static List<DateTime> horarios = new List<DateTime>();//lista de horários
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErro.Text = string.Empty;

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
            //cldData.SelectionMode = CalendarSelectionMode.Day;
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();
            string query = "select * from selectMed_view";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                Medico medAtual = null;
                while (rdr.Read())
                {
                    medAtual = new Medico(Convert.ToInt32(rdr.GetValue(0)), rdr.GetValue(1).ToString());
                    listaMed.Add(medAtual);
                }
            }
            int qtdMed = listaMed.Count;
            for (int i = 0; i < qtdMed; i++)
            {
                lsbMedico.Items.Add(listaMed[i].Nome.ToString());
            }
             query = "select * from VerConsultaSolicitada_view";
            if(Conexao.conexao.State!=System.Data.ConnectionState.Open)
                Conexao.conexao.Open();
                cmd = new SqlCommand(query, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    Consulta consAtual = null;
                   
                    Paciente pacAtual = null;
                    while (rdr.Read())
                    {
                        consAtual = new Consulta(Convert.ToInt32(rdr.GetValue(0)), (DateTime)(rdr.GetValue(1)), Convert.ToInt32(rdr.GetValue(3)), rdr.GetValue(5).ToString());
                        if(!listaCons.Exists(x => x.Equals(consAtual)))
                            listaCons.Add(consAtual);
                    /*    medAtual = new Medico(Convert.ToInt32(rdr.GetValue(3)), rdr.GetValue(2).ToString());
                        if (!listaMed.Exists(x => x.Equals(medAtual)))
                            listaMed.Add(medAtual);*/
                        pacAtual = new Paciente(rdr.GetValue(5).ToString(), rdr.GetValue(4).ToString());
                        if (!listaPac.Exists(x => x.Equals(pacAtual)))
                            listaPac.Add(pacAtual);
                }
                    
                    int qtdCons = listaCons.Count;//qtde de consultas
                    //int qtdMed = listaMed.Count;
                    for (int i = 0; i < qtdCons; i++)
                    {
                        lsbConsulta.Items.Add(listaCons[i].CodConsulta.ToString());
                    }
                   
                    AlterarCampos(listaCons[0].CodConsulta);
                lsbConsulta.Items[0].Selected = true;
                }
                else
                {
                    lblErro.Text = "Não há nenhuma consulta solicitada!";

                    lblErro.ForeColor = Color.Red;
                }
                if(rdr!=null)
                    rdr.Close();
                rdr = null;
            if(Conexao.conexao.State!=System.Data.ConnectionState.Closed)
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
    private void AlterarCampos(int codConsulta)
    {
        Consulta consAtual = listaCons.Find(x => x.CodConsulta== codConsulta);
        int qtdConsultas = listaCons.Count;
        int i;
        for (i = 0; i < qtdConsultas; i++)
        {
            if(lsbConsulta.Items[i].Text == codConsulta.ToString())
                break ;
        }
        if (i == qtdConsultas)// /*
            throw new Exception("Erro na lista de Consultas!");// */
       // lsbConsulta.Items[i].Selected = true;
        int crmAtual = consAtual.Crm;
        Medico medAtual = listaMed.Find(x => x.CRM == crmAtual);
        int j;
        int qtdMedicos = listaMed.Count;
        for (j = 0; j < qtdMedicos; j++)
            lsbMedico.Items[j].Selected = false;
        for (j = 0; j < qtdMedicos; j++)
        {
            
            if (lsbMedico.Items[j].Text == medAtual.Nome)
                break;
        }
        if (j == qtdMedicos)// /*
            throw new Exception("Erro na lista de Médicos!");// */
        //lsbMedico.Items[j].Selected = true;
        txtPaciente.Text = listaPac.Find(x => x.Usuario == consAtual.Usuario).Nome;
        int qtdHorarios = lsbHorarios.Rows;
        lsbHorarios.ClearSelection();
        lsbHorarios.Items.Clear();
        
        DateTime dataAtual = consAtual.DataHoraConsulta;
        //cldData.SelectedDate = dataAtual;
        txtData.Text = dataAtual.Day.ToString() + '/'+ dataAtual.Month.ToString() + '/' + dataAtual.Year.ToString();
        //       rbHora.Checked = false;
        horarios.Clear();
        for ( i = 9; (i < 12 || i >= 14) && i < 17; i++)
        {// médico trabalha das 9 às 17 e das 12h às 14h tem horário de almoço
            horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 0, 0));

            if (i != 17)
                horarios.Add(new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, i, 30, 0));

            if (i == 11)
                i = 13;
        }
        string query = "select * from HorarioDispMed_view " +
        "where crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)"
        +"and MONTH(dataHoraConsulta)= MONTH(@data) "
        +"and DAY(dataHoraConsulta)= DAY(@data) and not(meiaHora is null)";
        DateTime horarioAtual;
        if(Conexao.conexao.State!=System.Data.ConnectionState.Open)
        Conexao.conexao.Open();
        SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@crm", crmAtual));
        cmd.Parameters.Add(new SqlParameter("@data", dataAtual));
        rdr = cmd.ExecuteReader();
        bool meiaHora = true;
        
        if (rdr.HasRows)
        {
            int indiceHorarioAtual;
            while (rdr.Read())
            {
                horarioAtual = (DateTime)rdr.GetValue(0);

                try
                {    meiaHora = Convert.ToBoolean(rdr.GetValue(1));
                
                }
                catch
                {
                } 
                    
                            
                if (horarios.Exists(x => x.Equals(horarioAtual)))
                {
                    indiceHorarioAtual = horarios.FindIndex(x => x.Equals(horarioAtual));
                    horarios.RemoveAt(indiceHorarioAtual);
                    if (!meiaHora)
                        horarios.RemoveAt(indiceHorarioAtual);
                }
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
        for (indHorario = 0; indHorario < lsbHorarios.Items.Count; indHorario++)
        {
            if (lsbHorarios.Items[indHorario].Text == dataAtual.Hour + ":" + dataAtual.Minute + ":" + dataAtual.Second)
                break;
        }
        lsbHorarios.Items[DescobrirCodHorario(dataAtual)].Selected = true;
        rbHora.Checked = false;
        rbHora.Visible = true;
        lsbHorarios.Visible = true;
        lsbMedico.Items[listaMed.FindIndex(x => x.CRM == crmAtual)].Selected = true;
        lsbMedico.Visible = true;
        rdr.Close();
        Conexao.conexao.Close();
        rdr = null;
    }
    private int DescobrirCodHorario(DateTime data)
    {
        string hora, min, seg;
        hora = data.Hour.ToString();
        min = data.Minute.ToString();
        seg = data.Second.ToString();
        while (hora.Length < 2)
            hora = "0" + hora;
        while (min.Length < 2)
            min = "0" + min;
        while (seg.Length < 2)
            seg = "0" + seg;
        for(int i = 0; i < lsbHorarios.Items.Count; i++)
        {
            if (lsbHorarios.Items[i].Text == (hora + ":" + min + ":" + seg))
                return i;
        }
        throw new Exception("Erro na compatibilidade com os horários e seus códigos na lista") ;
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
            if (DataValida(dataSel))
            {
                bool meiaHora = !rbHora.Checked;

                if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                    Conexao.conexao.Open();

                string update = "AlterarConsultaSolicitar_sp";
                SqlCommand cmd = new SqlCommand(update, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add( new SqlParameter ("@codConsulta",codConsulta));
                cmd.Parameters.Add( new SqlParameter ("@dataHoraConsulta",dataSel));
                cmd.Parameters.Add( new SqlParameter ("@meiaHora",meiaHora));
                cmd.Parameters.Add( new SqlParameter ("@crm",crm));
                int linhasAlteradas = cmd.ExecuteNonQuery();

                lblErro.ForeColor = Color.Red;

                if(linhasAlteradas == 1)
                {
                    lblErro.Text = "Alteração da consulta de código " + codConsulta + " feita com sucesso!";

                    lblErro.ForeColor = Color.Green;

                    lsbConsulta.Items.RemoveAt(lsbConsulta.SelectedIndex);

                    if (lsbConsulta.Items.Count > 0)
                    {
                        lsbConsulta.SelectedIndex = 0;

                        AlterarCampos(Convert.ToInt32(lsbConsulta.SelectedItem.Text));
                    }
                    else
                        LimparTela();
                }                    

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

    private void LimparTela()
    {
        lsbConsulta.Items.Clear();
        lsbHorarios.Items.Clear();
        lsbMedico  .Items.Clear();

        txtData.Text = txtPaciente.Text = string.Empty;

        rbHora.Visible = false;
    }

    protected void lsbConsulta_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlterarCampos(Convert.ToInt32(lsbConsulta.Items[lsbConsulta.SelectedIndex].Text));      
    }

    protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            Medico medAtual = listaMed.Find(x => x.Nome == lsbMedico.Items[lsbMedico.SelectedIndex].Text);
            string query = "select * from HorarioDispMed_view where " +
            "crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)" +
            "and MONTH(dataHoraConsulta)= month(@data) " +
            "and DAY(dataHoraConsulta)= day(@data) and not(meiaHora is null)";
            Conexao.conexao.Open();
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@crm", medAtual.CRM));
            cmd.Parameters.Add(new SqlParameter("@data", DateTime.Parse(txtData.Text)));
            //cmd.Parameters.Add(new SqlParameter("@data", DatecldData.SelectedDate));
            rdr = cmd.ExecuteReader();
            DateTime dataAtual = DateTime.Parse(txtData.Text);
            //DateTime dataAtual = cldData.SelectedDate;
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
        catch
        {
            if (rdr != null)
                rdr.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            rdr = null;
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

            lblErro.ForeColor = Color.Red;
        }
    }
    private bool DataValida(DateTime data)
    {// A DATA SÓ É VÁLIDA SE NÃO FOR NUM DIA PASSADO
      
        return data.CompareTo(DateTime.Now) >= 0;
    }


    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        try
        {
           DateTime data = DateTime.Parse(txtData.Text);//Descobrir dia mostrado no calendário

            if (DataValida(data))
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

                    lblErro.ForeColor = Color.Red;
                }
            }//fim do if(dataValida())
            else
            {
                lblErro.Text = "Selecione uma data futura!";

                lblErro.ForeColor = Color.Red;
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
        
        string query = "select * from HorarioDispMed_view" +
        "where crm = @crm and YEAR(dataHoraConsulta)=YEAR(@data)" +
        "and MONTH(dataHoraConsulta)= MONTH(@data) and DAY(dataHoraConsulta)= DAY(@data) and not(meiaHora is null)";
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
        //DateTime data = cldData.SelectedDate;
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

   /* protected void cldData_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime data = cldData.SelectedDate;//Descobrir dia mostrado no calendário

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
    }*/
}