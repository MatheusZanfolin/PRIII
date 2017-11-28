using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class UltimasConsultasPac : System.Web.UI.Page
{
    private SqlDataReader rdr;
    private static string usuarioPacOnline ;
    private static List<int> crms = new List<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            crms.Clear();
            try
            {
                if (string.IsNullOrEmpty(Session["paciente"].ToString()))
                    Response.Redirect("LogonPac.aspx");
                else
                    usuarioPacOnline = Session["paciente"].ToString();
            }
            catch
            {
                Response.Redirect("LogonPac.aspx");
            }
            try
            {
            if(Conexao.conexao.State!=System.Data.ConnectionState.Open)
                Conexao.conexao.Open();
                string query = "select * from medicosRelPac_view where usuario=@usuario";
                SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@usuario", usuarioPacOnline));
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                    if (crms.Count==0 || !crms.Exists(x => x == Convert.ToInt32(rdr.GetValue(0))))
                    {
                        crms.Add(Convert.ToInt32(rdr.GetValue(0)));

                        lsbMedico.Items.Add(rdr.GetValue(1).ToString());
                    }
                    }
                }
                else
                {
                    lblErro.Text = ("Não há nenhuma consulta sua cadastrada no histórico !");

                }
                rdr.Close();
                Conexao.conexao.Close();
                rdr = null;
           }
            catch
            {
                if(rdr!=null)
                rdr.Close();
                if(Conexao.conexao.State == System.Data.ConnectionState.Open)
                Conexao.conexao.Close();
                rdr = null;
                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

            }
        }
    }

    protected void btnGeraRelatorio_Click(object sender, EventArgs e)
    {
        try
        {

            int numero = Convert.ToInt32(txtNumero.Text);
            string nomeMedSel = lsbMedico.Items[lsbMedico.SelectedIndex].Text;
            int crmSel = crms[lsbMedico.SelectedIndex];

            string query = "SELECT TOP(@numero) * FROM ultimasConsultas_view " +
                "WHERE crm  = @crm AND usuario = @usuario  ORDER BY dataHoraConsulta DESC";
            Conexao.conexao.Open();
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@numero", numero));
            cmd.Parameters.Add(new SqlParameter("@crm", crmSel));
            cmd.Parameters.Add(new SqlParameter("@usuario", usuarioPacOnline));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                tabDados.Rows.Add(new TableRow());
                for (int i = 0; i < 4; i++)
                {
                    tabDados.Rows[0].Cells.Add(new TableCell());
                }
                tabDados.Rows[0].Cells[0].Text = "Código da Consulta:";
                tabDados.Rows[0].Cells[1].Text = "Data:";
                tabDados.Rows[0].Cells[2].Text = "Horário:";
                tabDados.Rows[0].Cells[3].Text = "Diagnóstico:";
                while (rdr.Read())
                {
                    insereNaMatriz(Convert.ToInt32(rdr.GetValue(0)), (DateTime)rdr.GetValue(1), rdr.GetValue(2).ToString());
                }
                rdr.Close();
                Conexao.conexao.Close();
                rdr = null;
            }
            else
            {
                lblErro.Text ="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
                rdr.Close();
                Conexao.conexao.Close();
                rdr = null;
                return;
            }
        }
        catch
        {
            if (rdr != null)
                rdr.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            rdr = null;
            lblErro.Text ="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
    private void insereNaMatriz(int codConsulta, DateTime dataHora, string diagnostico)
    {
        int indLinha = tabDados.Rows.Count;
        string hora, minuto;
        hora = dataHora.Hour.ToString();
        minuto = dataHora.Minute.ToString();
        while (hora.Length < 2)
            hora = "0" + hora;
        while (minuto.Length < 2)
            minuto = "0" + minuto;
        tabDados.Rows.Add(new TableRow());
        for (int i = 0; i < 4; i++)
            tabDados.Rows[indLinha].Cells.Add(new TableCell());
        tabDados.Rows[indLinha].Cells[0].Text = codConsulta.ToString();
        tabDados.Rows[indLinha].Cells[1].Text = dataHora.Day + "/" + dataHora.Month + "/" + dataHora.Year;
        tabDados.Rows[indLinha].Cells[2].Text =  hora + ":" + minuto;
        tabDados.Rows[indLinha].Cells[3].Text = diagnostico;
    }

    protected void btnRedefinir_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("UltimasConsultasPac.aspx");
    }
}