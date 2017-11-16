using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class logonPac : System.Web.UI.Page
{
    private SqlDataReader rdr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["paciente"].ToString()))
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

    protected void btnLogonPac_Click(object sender, EventArgs e)
    {
        try
        {
            string usuario = txtUsuario.Text;
            usuario = usuario.Trim();
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário !";
                return;
            }
            string senha = txtSenha.Text;
            senha = senha.Trim();
            if (string.IsNullOrEmpty(senha))
            {
                lblErro.Text = "Digite a senha!";
            }
            string senhaAchada = "";
            

            Conexao.conexao.Open();

            string query = "";
            query = "logonPac_sp";//stored procedure que devolve a senha achada
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usuario", usuario);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                if (rdr.Read())
                {
                    senhaAchada = rdr.GetValue(0).ToString();
                    rdr.Close();
                    rdr = null;
                    Conexao.conexao.Close();
                    if (senha == senhaAchada)
                    {
                        Session["paciente"] = usuario;
                        Response.Redirect("HomePaciente.aspx");
                    }
                    else
                    {
                        lblErro.Text = "Usuário e/ou senha inválidos!";

                        lblErro.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                lblErro.Text = "Usuário e/ou senha inválidos!";

                lblErro.ForeColor = System.Drawing.Color.Red;
            }

            if (rdr != null)
                rdr.Close();

            if(Conexao.conexao.State!=System.Data.ConnectionState.Closed)
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

            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}