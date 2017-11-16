using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class UltimasConsultasMed : System.Web.UI.Page
{//default(classes e structs) é private, por isso não coloquei
    SqlDataReader rdr;
    int crmMedOnline;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["crm"].ToString()))
                    Response.Redirect("LogonMedico.aspx");
            }
            catch
            {
                Response.Redirect("LogonMedico.aspx");
            }
        }
    }

    protected void btnGeraRelatorio_Click(object sender, EventArgs e)
    {
        try
        {
            int numero;
            try
            {
               numero = Convert.ToInt32(txtNumero.Text.Trim());
            }
            catch
            {
                lblErro.Text = "Digite um número inteiro !!";
                return;
            }
            if (numero <= 0)
            {
                lblErro.Text = "O número deve ser maior que 0!";
                return;
            }
            string usuario = txtUsuario.Text;
            usuario = usuario.Trim();
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário!";
                return;
            }
            Conexao.conexao.Open();
            string query = "SELECT TOP(@numero)* FROM ultimasConsultas_view" +
                "WHERE crm=@crm AND usuario=@usuario ORDER BY dataHoraConsuta DESC";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@numero", numero));
            cmd.Parameters.Add(new SqlParameter("@crm", crmMedOnline));
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
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
            }

            else
            {
                lblErro.Text = "Não há nenhuma consulta no histórico!";
            }
            rdr.Close();
            Conexao.conexao.Close();
            rdr = null;
        }
        catch
        {
          if (rdr != null)
                rdr.Close();
          if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
          rdr = null;
          lblErro.Text =  "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

        }
    }
    private void insereNaMatriz(int codConsulta, DateTime dataHora, string diagnostico)
    {
        int indLinha = tabDados.Rows.Count;
        tabDados.Rows.Add(new TableRow());
        for (int i = 0; i < 4; i++)
            tabDados.Rows[indLinha].Cells.Add(new TableCell());
        tabDados.Rows[indLinha].Cells[0].Text = codConsulta.ToString();
        tabDados.Rows[indLinha].Cells[1].Text = dataHora.Day + "/" + dataHora.Month + "/" + dataHora.Year;
        tabDados.Rows[indLinha].Cells[2].Text = dataHora.Hour + ":" + dataHora.Minute;
        tabDados.Rows[indLinha].Cells[3].Text = diagnostico;
    }

    protected void btnRedefinir_Click(object sender, EventArgs e)
    {
        int numLinhas = tabDados.Rows.Count;
        for (int i = numLinhas - 1; i >= 0; i++)
            tabDados.Rows.RemoveAt(i);
        btnGeraRelatorio_Click(null, null);
    }
}