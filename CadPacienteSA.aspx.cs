using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CadPacienteSA : System.Web.UI.Page
{
    private static UsuarioPendente[] usuariosPendentes;

    private Color TXT_VAZIO = Color.Gray;   

    private SqlDataReader leitor;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["usuario"].ToString()))
                    Response.Redirect("logonSA.aspx");
            }
        }
        catch
        {
            Response.Redirect("logonSA.aspx");
        }

        try
        {
            int quantasPendencias = QuantasPendencias();

            if (quantasPendencias <= 0)
                TravarTela();
            else if (!IsPostBack)
                LerUsuariosPendentes(quantasPendencias);
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde!";
        }
    }

    private bool LerUsuariosPendentes(int quantasPendencias) //Do BD
    {
        if (quantasPendencias <= 0)
        {
            usuariosPendentes = null;

            return false;
        }            

        usuariosPendentes = new UsuarioPendente[quantasPendencias];

        if(Conexao.conexao==null)
            Conexao.conexao = new SqlConnection(Conexao.stringDeConexao);

        Conexao.conexao.Open();

        var query = "SELECT * FROM dadosPendentes_view";

        var comando = new SqlCommand(query, Conexao.conexao);

        leitor = comando.ExecuteReader();

        int indiceDeInclusao = 0;
        while (leitor.Read())
        {
            if (!string.IsNullOrEmpty(leitor["celular"].ToString()))
                usuariosPendentes[indiceDeInclusao] = new UsuarioPendente(leitor["endereco"].ToString(), DateTime.Parse(leitor["dataNascimento"].ToString()), leitor["email"].ToString(), leitor["celular"].ToString(), leitor["telefone"].ToString(), leitor["foto"].ToString());
            else
                usuariosPendentes[indiceDeInclusao] = new UsuarioPendente(leitor["endereco"].ToString(), DateTime.Parse(leitor["dataNascimento"].ToString()), leitor["email"].ToString(), leitor["telefone"].ToString(), leitor["foto"].ToString());

            indiceDeInclusao++;
        }

        leitor.Close();

        Conexao.conexao.Close();

        return true;
    }

    private int QuantasPendencias()
    {
        int quantasPendencias = 0;
        if(Conexao.conexao==null)
            Conexao.conexao = new SqlConnection(Conexao.stringDeConexao);

        Conexao.conexao.Open();

        var query = "SELECT COUNT(*) FROM dadosPendentes_view";

        var comando = new SqlCommand(query, Conexao.conexao);

        var leitor = comando.ExecuteReader();

        var leu = leitor.Read();

        quantasPendencias = leitor.GetInt32(0);

        leitor.Close();

        Conexao.conexao.Close();

        if (!leu)
            throw new Exception("CadPacienteSA: erro de BD");

        return quantasPendencias;
    }

    protected void ddlPendentes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (usuariosPendentes.Length > 0)
            ExibirDadosDe(usuariosPendentes[ddlPendentes.SelectedIndex]);
    }

    private void ExibirDadosDe(UsuarioPendente usuario)
    {
        //Dados que, com certeza, não são nulos
        txtEndereco.Text   = usuario.Endereco;                              
        txtNascimento.Text = usuario.DataNascimento.ToString("dd/mm/yyyy");
        txtEmail.Text      = usuario.Email;
        txtTelefone.Text   = usuario.Telefone;
        imgFoto.ImageUrl   = usuario.Foto;
        txtCelular.Text = usuario.Celular;
    }

    private void RemoverPendencia(int indice)
    {
        for (int i = indice; i < usuariosPendentes.Length - 1; i++)
            usuariosPendentes[i] = usuariosPendentes[i + 1];

        UsuarioPendente[] novoVetor = null;

        if (usuariosPendentes.Length > 1)
        {
            novoVetor = new UsuarioPendente[usuariosPendentes.Length - 1];

            for (int i = 0; i < novoVetor.Length; i++)
                novoVetor[i] = usuariosPendentes[i];
        }            

        usuariosPendentes = novoVetor;
    }

    protected void btnDeferir_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtComentarios.Text))
            return;

        DefinirPendenciaComo(true);
    }

    protected void btnIndeferir_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtComentarios.Text))
            return;

        DefinirPendenciaComo(false);
    }

    private void DefinirPendenciaComo(bool aceita)
    {
        if(Conexao.conexao==null)
            Conexao.conexao = new SqlConnection(Conexao.stringDeConexao);

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        string update = "definirPendenciaComo_sp";

        var comando = new SqlCommand(update, Conexao.conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@aceita" , aceita);
        comando.Parameters.AddWithValue("@usuario", ddlPendentes.SelectedValue);

        if (aceita)
            comando.Parameters.AddWithValue("@comentarios", string.Empty);
        else
            comando.Parameters.AddWithValue("@comentarios", txtComentarios.Text);

        if (comando.ExecuteNonQuery() < 1)
        {
            Conexao.conexao.Close();

            throw new Exception("CadPacienteSA: Erro de atualização do BD");
        }            

        Conexao.conexao.Close();

        RemoverPendencia(ddlPendentes.SelectedIndex);

        ddlPendentes.Items.Remove(ddlPendentes.SelectedItem);

        if (ddlPendentes.Items.Count <= 0)
            TravarTela();
        else
            ExibirDadosDe(usuariosPendentes[0]);
    }

    protected void ddlPendentes_Load(object sender, EventArgs e)
    {
        if (usuariosPendentes != null)
            if (usuariosPendentes.Length > 0)
                ExibirDadosDe(usuariosPendentes[0]);
    }

    private void TravarTela()
    {
        txtEndereco.BackColor   = TXT_VAZIO;
        txtEmail.BackColor      = TXT_VAZIO;
        txtNascimento.BackColor = TXT_VAZIO;
        txtTelefone.BackColor   = TXT_VAZIO;
        txtCelular.BackColor    = TXT_VAZIO;

        txtEndereco.Text    = string.Empty;
        txtEmail.Text       = string.Empty;
        txtNascimento.Text  = string.Empty;
        txtTelefone.Text    = string.Empty;
        txtCelular.Text     = string.Empty;
        imgFoto.ImageUrl    = string.Empty;
        txtComentarios.Text = string.Empty;

        btnDeferir.Enabled = btnIndeferir.Enabled = false;
    }

    protected void txtComentarios_TextChanged(object sender, EventArgs e)
    {
        //Uma requisição só pode ser indeferida caso o SA comente algo a respeito dela (apontando onde está o erro)

        if (string.IsNullOrEmpty(txtComentarios.Text))
        {
            btnIndeferir.Enabled = false;

            btnDeferir.Enabled = true;
        }            
        else
        {
            btnIndeferir.Enabled = true;

            btnDeferir.Enabled = false;
        }            
    }

    protected void txtCelular_TextChanged(object sender, EventArgs e)
    {

    }
}