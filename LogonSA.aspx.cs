using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class SA : System.Web.UI.Page
{//default(classes e structs) é private, por isso não coloquei
    SqlDataReader leitor;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["usuario"] != null)
                {
                    lblErro.Text = "Logout feito com sucesso!";

                    lblErro.ForeColor = System.Drawing.Color.Green;
                }                    

                Session.RemoveAll();
            }
            catch
            {
                Session.RemoveAll();
            }
        }

    }

    protected void btnLogonSA_Click(object sender, EventArgs e)
    {
        try
        {
        if (!Conexao.conexao.State.Equals(System.Data.ConnectionState.Open))
            Conexao.conexao.Open();
            string senhaAchada = "";
            string usuario = txtUsuario.Text;
            usuario = usuario.Trim();//não armazenar espaços em branco
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário!";
                return;
            }

            string senha = txtSenha.Text;
            senha = senha.Trim();
            if (string.IsNullOrEmpty(senha))
            {
                lblErro.Text = "Digite a senha!";
                return;
            }

            string query = "logon_sp";

            var comandoQuery = new SqlCommand(query, Conexao.conexao);
            comandoQuery.CommandType = System.Data.CommandType.StoredProcedure;
            comandoQuery.Parameters.AddWithValue("@usuario", usuario);
            leitor = comandoQuery.ExecuteReader();
            if (leitor.HasRows)
            {
                if (leitor.Read())
                {
                    senhaAchada = leitor.GetValue(0).ToString();
                }
            }
            else
            {
                lblErro.Text = "Logon ou senha inválidos !";

                lblErro.ForeColor = System.Drawing.Color.Red;
            }

            leitor.Close();
            Conexao.conexao.Close();
            leitor = null;

            if (senhaAchada == senha)
            {
                Session["usuario"] = usuario;
                Response.Redirect("SA.aspx");//Redireciona para a página inicial do SA
            }
            else
            {
                lblErro.Text = "Logon ou senha inválidos !";

                lblErro.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch
        {
            if (leitor != null)
                leitor.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            leitor = null;
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

            lblErro.ForeColor = System.Drawing.Color.Red;
        }
    }
}