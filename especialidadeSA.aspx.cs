using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class especialidadeSA: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["usuario"] == null)
                    Response.Redirect("logonSA.aspx");
            }
            catch
            {
                Response.Redirect("logonSA.aspx");
            }
            
         }
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            Conexao.conexao.Open();
            string espec = txtEspecialidade.Text;
            espec = espec.Trim();//não armazenar espaços em branco
            if (string.IsNullOrEmpty(espec))
            {
                lblErro.Text = "Digite o nome da especialidade!!";
                return;
            }
            string sql = "CadastrarE_sp";//cadastrar especialidade
            SqlCommand cmd = new SqlCommand(sql, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nomeEspecialidade", espec));
            if (cmd.ExecuteNonQuery() == 1)
            {
                lblErro.Text = "Sucesso ao cadastrar " + espec + "!";

                lblErro.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblErro.Text = "Verifique os dados e tente novamente!";
            }
            Conexao.conexao.Close();
        }
        catch
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            lblErro.Text="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}