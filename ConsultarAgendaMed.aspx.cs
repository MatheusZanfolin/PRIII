using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultarAgendaMed : System.Web.UI.Page
{//default(classes e structs)é private, por isso não coloquei
    string nomeMedOnline;
    int crmMedOnline;
    SqlDataReader rdr;
    int numeroDeConsultas = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["crm"] == null)
                    Response.Redirect("LogonMedico.aspx");
                else
                {
                    crmMedOnline = Convert.ToInt32(Session["crm"]);
                }
            }
            catch
            {
                Response.Redirect("LogonMedico.aspx");
            }

            try
            {
                if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                    Conexao.conexao.Open();

                string query = "select crm, nome from selectMed_view where crm=@crm";
                SqlCommand cmdQuery = new SqlCommand(query, Conexao.conexao);

                cmdQuery.Parameters.Add(new SqlParameter("@crm", crmMedOnline));

                rdr = cmdQuery.ExecuteReader();

                if (rdr.HasRows)
                {
                    if (rdr.Read())
                        nomeMedOnline = rdr.GetValue(0).ToString();
                }
                else//crm não está no BD
                {
                    Response.Redirect("LogonMedico.aspx");
                }
            }
            catch
            {
                if (rdr != null)
                    rdr.Close();
                if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                    Conexao.conexao.Close();
                rdr = null;

                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde!";
            }
        }
    }

    
    private void insereNaMatriz(DateTime horario, bool meiaHora, int codConsulta, string nomePac)
    {
        int indCol = 0;
        int hora = horario.Hour;
        int min = horario.Minute;
        int indLinha = pegarIndLinha(hora, min);
        int dia = horario.Day;
        int diaAtual = DateTime.Now.Day;
        indCol = 2 * (dia - diaAtual) + 1;

        tabDados.Rows[indLinha].Cells[indCol].Text = codConsulta + " - " + nomePac;
        if (!meiaHora)
            tabDados.Rows[indLinha + 1].Cells[indCol].Text = codConsulta + " - " + nomePac;
    }
    private int pegarIndLinha(int hora, int min)
    {
       
        hora *= 2;
        if (min == 30)
            hora++;
        if (hora >= 28)//volta ao trabalho às 14h
            hora -= 4;//horário de almoço: dura 2h
        hora -= 16;
        return hora;
    }

   

    protected void btnGeraRelatorio_Click(object sender, EventArgs e)
    {
        try
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            numeroDeConsultas = 0;
            int numero = Convert.ToInt32(txtNumero.Text);
            if (numero <= 0)
            {
                lblErro.Text = "Digite um número de dias positivo!";
                return;
            }
            string query = "select * from agendaMed_sp where" +
            "crm=@crm and " +
            "(dataHoraConsulta between getdate() and"+
            "DATEADD(DAY, @dias - 1, getdate()))";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@dias", numero));
            cmd.Parameters.Add(new SqlParameter("@crm", crmMedOnline));
            
            rdr = cmd.ExecuteReader();
            
            if (rdr.HasRows)
            {

                for (int dia = 0; dia < 7; dia++)
                {
                    if (dia == 0)
                        tabDados.Rows.Add(new TableRow());
                    tabDados.Rows[0].Cells.Add(new TableCell());

                    tabDados.Rows[0].Cells[2 * dia].Text = "Data: " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    if (dia == 0)
                        tabDados.Rows.Add(new TableRow());
                    tabDados.Rows[1].Cells.Add(new TableCell());
                    tabDados.Rows[1].Cells[2 * dia].Text = "Horário: ";
                    int hora = 18;
                    for (int i = 2; i < 14; i++)
                    {
                        if (dia == 0)
                            tabDados.Rows.Add(new TableRow());
                        tabDados.Rows[i].Cells.Add(new TableCell());
                        tabDados.Rows[i].Cells[2 * dia].Text = hora / 2 + "h";
                        if (hora % 2 != 0)
                        {
                            tabDados.Rows[i].Cells[2 * dia].Text += "30 - " + (hora + 1) / 2 + "h";
                        }
                        else
                        {
                            tabDados.Rows[i].Cells[2 * dia].Text += " - " + (hora / 2) + "h30";
                        }
                        hora++;
                        if (hora == 24)
                            hora = 28;
                    }
                    for (int i = 0; i < 14; i++)
                    {
                        tabDados.Rows[i].Cells.Add(new TableCell());
                        if (i == 0)
                            tabDados.Rows[i].Cells[2 * dia + 1].Text = "Médico: " + nomeMedOnline + " CRM: " + crmMedOnline;
                        if (i == 1)
                            tabDados.Rows[i].Cells[2 * dia + 1].Text = "Código da Consulta / Nome do Paciente: ";

                    }
                }//fim do for(int dia=0;dia<7;dia++)
                int numeroDeConsultas = 0;
                while (rdr.Read())
                {


                    insereNaMatriz((DateTime)rdr.GetValue(0), (bool)rdr.GetValue(1), Convert.ToInt32(rdr.GetValue(2)),
                                    (rdr.GetValue(3)).ToString());
                    numeroDeConsultas++;

                }

            }
            else
            {
                lblErro.Text = "Não há nenhuma consulta marcada nesta semana!";
            }
            rdr.Close();
            rdr = null;
            lblConsulta.Text = numeroDeConsultas.ToString();
            Conexao.conexao.Close();
        }
        catch
        {
            if (rdr != null)
                rdr.Close();
            if(Conexao.conexao.State != System.Data.ConnectionState.Closed)
            Conexao.conexao.Close();
            rdr = null;
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

        }
    }

    protected void btnRedefinir_Click(object sender, EventArgs e)
    {
        int numLinhas = tabDados.Rows.Count;
        for (int i = numLinhas - 1; i >= 0; i--)
            tabDados.Rows.RemoveAt(i);
        btnGeraRelatorio_Click(null, null);
    }
}