using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;

public partial class CadDiagnosticoMed : System.Web.UI.Page
{//default(classes e structs) é private, por isso não coloquei
    static SqlDataReader rdr;
    static int crmMedOnline;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErro.Text = string.Empty;

        try
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["crm"].ToString()))
                    Response.Redirect("LogonMedico.aspx");
                else
                    crmMedOnline = Convert.ToInt32(Session["crm"]);
            }
        }
        catch
        {
            Response.Redirect("LogonMedico.aspx");
        }
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        lblErro.ForeColor = Color.Red;

        try
        {
            string usuario = txtUsuario.Text;
            usuario = usuario.Trim();
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário do paciente!";
                return;
            }
                        
            string diagnostico = txtDiagnostico.Text;
            if (string.IsNullOrEmpty(diagnostico))
            {
                lblErro.Text = "Digite o diagnóstico!";
                return;
            }
            string update = "cadDiagnostico_sp";

            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            SqlCommand cmd = new SqlCommand(update, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@codConsulta", ddlHorarios.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@diagnostico", diagnostico));

            int linhasAlteradas = cmd.ExecuteNonQuery();

            if (linhasAlteradas == 1)
            {
                ddlHorarios.Items.Remove(ddlHorarios.SelectedItem);

                lblErro.Text = "Sucesso ao cadastrar o diagnóstico!";

                lblErro.ForeColor = Color.Green;

                LimparTela();
            }
            else if(linhasAlteradas == 0)
            {
                lblErro.Text = "Verifique os dados e tente novamente!";

                lblErro.ForeColor = Color.Red;
            }
            else
            {
                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";

                lblErro.ForeColor = Color.Red;
            }

            Conexao.conexao.Close();
        }
        catch
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    private void LimparTela()
    {
        txtDiagnostico.Text = txtUsuario.Text = txtData.Text = string.Empty;

        ddlHorarios.SelectedIndex = 0;

        txtDiagnostico.Visible = ddlHorarios.Visible = lblDiagnostico.Visible = lblHorario.Visible = false;
    }

    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        if (UsuarioEDataFornecidos)
            LiberarSelecaoDeHorario();
    }

    protected void txtUsuario_TextChanged(object sender, EventArgs e)
    {
        if (UsuarioEDataFornecidos)
            LiberarSelecaoDeHorario();
    }

    private bool UsuarioEDataFornecidos
    {
        get { return (!string.IsNullOrEmpty(txtData.Text)) && (!string.IsNullOrEmpty(txtUsuario.Text)); }
    }

    private void LiberarSelecaoDeHorario()
    {
        try
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string query = "selectConsulta_sp";

            var cmd = new SqlCommand(query, Conexao.conexao);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@crm"    , crmMedOnline));
            cmd.Parameters.Add(new SqlParameter("@usuario", txtUsuario.Text));
            cmd.Parameters.Add(new SqlParameter("@data"   , txtData.Text));

            var leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
                ddlHorarios.Visible = lblHorario.Visible = true;
            else
                lblErro.Text = "Verifique os dados e tente novamente!";

            while (leitor.Read())
                AdicionarHorario(Convert.ToInt32(leitor["codConsulta"]), leitor["horaConsulta"].ToString(), leitor["minutoConsulta"].ToString());

            Conexao.conexao.Close();
        }
        catch
        {
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();

            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    private void AdicionarHorario(int codConsulta, string horaConsulta, string minutoConsulta)
    {
        if (minutoConsulta == "0")
            minutoConsulta += "0";

        ddlHorarios.Items.Add(horaConsulta + ":" + minutoConsulta);

        ddlHorarios.Items[ddlHorarios.Items.Count - 1].Value = codConsulta.ToString();
    }

    protected void ddlHorarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiagnostico.Visible = lblDiagnostico.Visible = ddlHorarios.SelectedIndex != 0;
    }

    protected void txtDiagnostico_TextChanged(object sender, EventArgs e)
    {
        btnCadastrar.Visible = !string.IsNullOrEmpty(txtDiagnostico.Text);
    }
}