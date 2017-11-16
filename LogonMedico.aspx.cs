using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogonMedico : System.Web.UI.Page
{
    private const string VAR_ARMAZENAMENTO_CRM = "crm";
    private SqlDataReader leitor;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            try
            {
                if (!string.IsNullOrEmpty(Session["crm"].ToString()))
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

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (SenhaCorreta())
                LogarMedico();
            else
            {
                lblErro.Text = "Usuário e/ou senha inválidos!";

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
            lblErro.Text = "Usuário e/ou senha inválidos!";

            lblErro.ForeColor = System.Drawing.Color.Red;
        }
    }

    private bool SenhaCorreta()
    {
        bool senhaCorreta = false;
        Conexao.conexao.Open();
        string query = "logonMedico_sp";
        var comando = new SqlCommand(query, Conexao.conexao);

        comando.CommandType = System.Data.CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@crm", txtCrm.Text);

        leitor = comando.ExecuteReader();

        if (leitor.Read())
        {
            var senha = leitor["senha"].ToString();

            senhaCorreta = txtSenha.Text == senha;
        }                

        leitor.Close();
        
        Conexao.conexao.Close();
        leitor = null;
        return senhaCorreta;
    }

    private void LogarMedico()
    {
        ArmazenarMedico(txtCrm.Text);

        Response.Redirect("HomeMedico.aspx");
    }

    private void ArmazenarMedico(string crm)
    {
        Session[VAR_ARMAZENAMENTO_CRM] = crm;
    }
}