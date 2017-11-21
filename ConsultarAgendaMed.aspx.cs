using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultarAgendaMed : System.Web.UI.Page
{//default(classes e structs)é private, por isso não coloquei
    static string nomeMedOnline;
    static int crmMedOnline;
    static SqlDataReader rdr;
    static int numeroDeConsultas = 0;

    static string[] horarios = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["crm"].ToString()))
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

    
    private void InsereNaMatriz(DateTime horario, bool meiaHora, int codConsulta, string nomePac)
    {
        int indiceColuna = -1;
        int indiceLinha  = -1;

        int qtosDiasDeDiferenca = horario.Subtract(DateTime.Now).Days;

        for (int i = 0; i < horarios.Length; i++)
            if (horarios[i] == horario.ToString("HH:mm"))
                indiceLinha = i + 1;

        indiceColuna = qtosDiasDeDiferenca + 1;

        tabDados.Rows[indiceLinha].Cells[indiceColuna].Text = codConsulta + " - " + nomePac;

        if (!meiaHora)
            tabDados.Rows[indiceLinha + 1].Cells[indiceColuna].Text = codConsulta + " - " + nomePac;
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
            lblConsulta.Text = "0";

            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            numeroDeConsultas = 0;
            int numero = Convert.ToInt32(txtNumero.Text);
            if (numero <= 0)
            {
                lblErro.Text = "Digite um número de dias positivo!";
                return;
            }
            string query = "select * from agendaMed_view where crm = @crm and dataHoraConsulta >= getdate() and dataHoraConsulta <= DATEADD(DAY, @dias, getdate())";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@dias", numero));
            cmd.Parameters.Add(new SqlParameter("@crm", crmMedOnline));
            
            rdr = cmd.ExecuteReader();
            
            if (rdr.HasRows)
            {
                /**
                for (int dia = 0; dia < 7; dia++)
                {
                    if (dia == 0)
                        tabDados.Rows.Add(new TableRow());
                    tabDados.Rows[0].Cells.Add(new TableCell());
                
                    int hora = 18;
                    for (int i = 1; i < 13; i++)
                    {
                        if (dia == 0)
                            tabDados.Rows.Add(new TableRow());
                        tabDados.Rows[i].Cells.Add(new TableCell());
                        tabDados.Rows[i].Cells[2 * dia].Text = hora / 2 + "h";
                        if (hora % 2 != 0)
                        {
                            tabDados.Rows[i].Cells[2 * dia].Text += "30 - " + (hora + 1) / 2 + "h00";
                        }
                        else
                        {
                            tabDados.Rows[i].Cells[2 * dia].Text += "00 - " + (hora / 2) + "h30";
                        }
                        hora++;
                        if (hora == 24)
                            hora = 28;
                    }
                
                    for (int i = 0; i < 13; i++)
                    {
                        tabDados.Rows[i].Cells.Add(new TableCell());
                        if (i == 0)
                            tabDados.Rows[i].Cells[2 * dia + 1].Text = "Médico: " + nomeMedOnline + " CRM: " + crmMedOnline;
                        if (i == 1)
                            tabDados.Rows[i].Cells[2 * dia + 1].Text = "Código da Consulta / Nome do Paciente: ";
                    }
                }//fim do for(int dia=0;dia<7;dia++)
                **/

                GerarTabela(numero);

                numeroDeConsultas = 0;
                while (rdr.Read())
                {


                    InsereNaMatriz(DateTime.Parse(rdr["dataHoraConsulta"].ToString()), (bool)rdr["meiaHora"], Convert.ToInt32(rdr["codConsulta"]),
                                    rdr["usuario"].ToString());
                    numeroDeConsultas++;

                }

                lblConsulta.Text = numeroDeConsultas.ToString();
            }
            else
            {
                lblConsulta.Text = string.Empty;

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

    private void GerarTabela(int quantosDias)
    {
        tabDados.Rows.Clear();

        for (int i = 0; i <= horarios.Count(); i++)
        {
            tabDados.Rows.Add(new TableRow());

            for (int j = 0; j <= quantosDias; j++)
            {
                var novaCelula = new TableCell();

                novaCelula.HorizontalAlign = HorizontalAlign.Center;
                novaCelula.VerticalAlign   = VerticalAlign.Middle;

                tabDados.Rows[i].Cells.Add(novaCelula);
            }                
        }

        for (int i = 1; i <= horarios.Count(); i++)
            tabDados.Rows[i].Cells[0].Text = horarios[i - 1];

        for (int i = 1; i <= quantosDias; i++)
            tabDados.Rows[0].Cells[i].Text = DateTime.Now.AddDays(i - 1).ToString("dd/MM/yyyy");
    }

    protected void btnRedefinir_Click(object sender, EventArgs e)
    {
        int numLinhas = tabDados.Rows.Count;
        for (int i = numLinhas - 1; i >= 0; i--)
            tabDados.Rows.RemoveAt(i);
        btnGeraRelatorio_Click(null, null);
    }
}