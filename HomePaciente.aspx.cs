using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePaciente : System.Web.UI.Page
{
    enum StatusSolicitacao
    {
        Indeferida,
        Deferida,
        Pendente,
        Indefinida
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["paciente"].ToString()))
                    Response.Redirect("LogonPac.aspx");
            }
            catch
            {
                Response.Redirect("LogonPac.aspx");
            }

            var status = StatusDaSoclicitacao();

            switch (status)
            {
                case StatusSolicitacao.Deferida:
                    ExibirTelaPrincipal();
                    break;

                case StatusSolicitacao.Indeferida:
                    ExibirTelaSecundaria();

                    pnlNovaSolicitacao.Visible = true;
                    break;

                case StatusSolicitacao.Pendente:
                    ExibirTelaSecundaria();

                    pnlSolicitacaoPendente.Visible = true;
                    break;
            }
        }
    }

    private void ExibirTelaPrincipal()
    {
        pnlPaciente.Visible = true;

        pnlUsuario.Visible = false;
    }

    private void ExibirTelaSecundaria()
    {
        pnlPaciente.Visible = false;

        pnlUsuario.Visible = true;
    }

    private StatusSolicitacao StatusDaSoclicitacao()
    {
        try 
        {            
            var usuario = Session["paciente"];

            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string query = "SELECT * FROM requisicaoUsuario_view WHERE usuario = @usuario";

            var comando = new SqlCommand(query, Conexao.conexao);

            comando.CommandType = System.Data.CommandType.Text;

            comando.Parameters.AddWithValue("@usuario", usuario);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                if (leitor.IsDBNull(1))
                {
                    Conexao.conexao.Close();

                    return StatusSolicitacao.Pendente;
                }                    
                else
                {                    
                    var deferida = Convert.ToBoolean(leitor["aceita"]);

                    if (!deferida)
                        txtComentarios.Text = leitor["comentarios"].ToString();

                    Conexao.conexao.Close();

                    return deferida ? StatusSolicitacao.Deferida : StatusSolicitacao.Indeferida;
                }
            }
            else
                throw new Exception("HomePaciente: erro de BD");            
        }
        catch
        {
            if (Conexao.conexao != null && Conexao.conexao.State == System.Data.ConnectionState.Open)
                Conexao.conexao.Close();

            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde!";

            return StatusSolicitacao.Indefinida;
        }
    }

    protected void MenuPac_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (e.Item.Text.ToUpper() == "LOGOUT")
            Request.QueryString["logout"] = "true";
    }

    protected void btnNovaSolicitacao_Click(object sender, EventArgs e)
    {
        DeletarSolicitacaoAntiga();

        Response.Redirect("~/CadPaciente.aspx");
    }

    private void DeletarSolicitacaoAntiga()
    {
        try
        {
            var usuario = Session["paciente"];

            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string query = "deletarUsuario_sp";

            var comando = new SqlCommand(query, Conexao.conexao);

            comando.CommandType = System.Data.CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@usuario", usuario);

            var resultado = comando.ExecuteNonQuery();

            if (resultado <= 0 || resultado > 1)
                throw new Exception("HomePaciente: erro de BD");
        }
        catch
        {
            if (Conexao.conexao != null && Conexao.conexao.State == System.Data.ConnectionState.Open)
                Conexao.conexao.Close();

            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde!";
        }
    }
}