using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CadPaciente : System.Web.UI.Page
{
    private string[] formatosValidosDeFoto = { ".png", ".jpg", ".bmp" };

    private const string NOME_PASTA_UPLOADS = "Uploads Pacientes";

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnTerminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (EntradaValida() && DataValida(DateTime.Parse(txtNascimento.Text)))
            {
                RegistrarPacienteNoBanco();

                lblErro.Text = "Cadastro realizado com sucesso!";

                lblErro.ForeColor = System.Drawing.Color.Green;
            }                
            else
                lblErro.Text = "Verifique os dados e tente novamente!";
        }
        catch
        {
            if (Conexao.conexao.State != ConnectionState.Closed)
                Conexao.conexao.Close();
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde!";
        } 
    }
    private bool DataValida(DateTime data)
    {
        DateTime dataAtual = DateTime.Now;

        if (! (data.CompareTo(dataAtual) < 0))
            return false;

        return true;
    }
    private void RegistrarPacienteNoBanco()
    {
        Conexao.conexao.Open();

        string caminho = SalvarFoto();

        string procedimento = "CadastrarPaciente_sp";

        SqlCommand comando = new SqlCommand(procedimento, Conexao.conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@usuario"       , txtUsuario.Text);
        comando.Parameters.AddWithValue("@nome"          , txtNome.Text);
        comando.Parameters.AddWithValue("@endereco"      , txtEndereco.Text);
        comando.Parameters.AddWithValue("@dataNascimento", new SqlDateTime(DateTime.Parse(txtNascimento.Text)));
        comando.Parameters.AddWithValue("@email"         , txtEmail.Text);
        comando.Parameters.AddWithValue("@celular"       , txtCelular.Text);
        comando.Parameters.AddWithValue("@telefone"      , txtTelefone.Text);
        comando.Parameters.AddWithValue("@senha"         , txtSenha.Text);        
        comando.Parameters.AddWithValue("@foto"          , caminho);
        
        if (comando.ExecuteNonQuery() < 1)
            throw new Exception("CadPaciente: erro ao inserir paciente");

        Conexao.conexao.Close();
    }

    private string SalvarFoto()
    {
        string caminhoCompleto = Directory.CreateDirectory(Server.MapPath(NOME_PASTA_UPLOADS) + Path.DirectorySeparatorChar).FullName;

        string caminho = "";
        if (FormatoValido())
        {
            string nomeArquivo = txtUsuario.Text + Path.GetExtension(fupFoto.PostedFile.FileName);

            caminhoCompleto += nomeArquivo;

            fupFoto.PostedFile.SaveAs(caminhoCompleto);

            caminho = caminhoCompleto.Substring(caminhoCompleto.IndexOf(NOME_PASTA_UPLOADS + Path.DirectorySeparatorChar + nomeArquivo));
        }

        return caminho;
    }

    private bool FormatoValido()
    {
        foreach (string extensao in formatosValidosDeFoto)
            if (Path.GetExtension(fupFoto.FileName).ToLower() == extensao)
                return true;

        return false;
    }

    private bool EntradaValida()
    {
        if (!vldUsuario.IsValid)
            return false;

        if (!vldSenha.IsValid)
            return false;

        if (!vldNome.IsValid)
            return false;

        if (!vldEndereco.IsValid)
            return false;

        if (!vldNascimento.IsValid)
            return false;

        if (!vldEmail.IsValid)
            return false;

        if (!vldTelefone.IsValid)
            return false;

        return true;
    }
    
}