using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class CadSA : System.Web.UI.Page
{
    private int linhasAlteradas;
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
        }
    }

    protected void btnCadSA_Click(object sender, EventArgs e)
    {
        try
        {
            string usuario = txtUsuario.Text;
            usuario = usuario.Trim();
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = ("Digite o usuário!");
            }
            string senha = txtSenha.Text;
            senha = senha.Trim();
            if (string.IsNullOrEmpty(senha))
            {
                lblErro.Text = "Digite a senha !";
            }

            Conexao.conexao.Open();

            string query = "CadastrarS_sp";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@senha", senha));
            linhasAlteradas = cmd.ExecuteNonQuery();

            Conexao.conexao.Close();

            if (linhasAlteradas == 1)
            {
                lblErro.Text = "Cadastro de " + usuario + " completado com sucesso!";

                lblErro.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblErro.Text = "Verifique os dados e tente novamente!";
            }
        }
        catch
        {
            if (Conexao.conexao != null && Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();

            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}
