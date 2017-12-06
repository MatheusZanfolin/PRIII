using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data.SqlTypes;
using System.Web.Configuration;
using System.Drawing;

public partial class CadMed : System.Web.UI.Page
{
    private const string NOME_PASTA_UPLOADS = "Uploads Médicos";
    private SqlDataReader rdr;
    private string[] formatosValidos = {".png", ".jpg", ".bmp"};
    List<Especialidade> especs = new List<Especialidade>();//lista de especialidades

    private Color TXT_VAZIO = Color.Gray;

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

            try
            {                
                if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                    Conexao.conexao.Open();

                string query = "SELECT * FROM especialidades_vw";//seleciona todas as especialidades cadastradas
                SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {//populando a lista especs
                        especs.Add(new Especialidade(Convert.ToInt32(rdr.GetValue(0)), rdr.GetValue(1).ToString()));
                        lsbEspec.Items.Add(especs.Last().NomeEspecialidade);

                        lsbEspec.Items[lsbEspec.Items.Count - 1].Value = especs.Last().CodEspecialidade.ToString();
                    }//populando o listbox com os nomes das especialidades
                }
                else
                {
                    lblErro.Text = "Não há nenhuma especialidade cadastrada!";

                    TravarTela();
                }

                rdr.Close();
                Conexao.conexao.Close();

                rdr = null;
            }
            catch (Exception ex)
            {
                if (rdr != null)
                    rdr.Close();
                if (!Conexao.conexao.State.Equals(System.Data.ConnectionState.Closed))
                    Conexao.conexao.Close();
                rdr = null;                
                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
            }
        }
    }

    private void LimparTela()
    {
        txtCelular.Text = txtCRM.Text = txtDataNasc.Text = txtEmail.Text = txtNome.Text = txtSenha.Text = txtTelefone.Text = string.Empty;
    }

    private void TravarTela()
    {
        txtCelular.Enabled = txtCRM.Enabled = txtDataNasc.Enabled = txtEmail.Enabled = txtNome.Enabled = txtSenha.Enabled = txtTelefone.Enabled = lsbEspec.Enabled = fupFoto.Enabled = false;
        txtCelular.BackColor = txtCRM.BackColor = txtDataNasc.BackColor = txtEmail.BackColor = txtNome.BackColor = txtSenha.BackColor = txtTelefone.BackColor = lsbEspec.BackColor = TXT_VAZIO;

        TravarTela();
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {//cadastro
        try
        {
           int crm = Convert.ToInt32(txtCRM.Text);
           string nome = txtNome.Text;
           string dataNasc = txtDataNasc.Text;
           string email = txtEmail.Text;
           string celular = txtCelular.Text;
           string telefone = txtTelefone.Text;
           string senha = txtSenha.Text;
           if(!validarDados(crm, nome, dataNasc, email, celular, telefone, senha))
            return;

            int codEspecSel = Convert.ToInt32(lsbEspec.SelectedValue);

            /*pegar o upload*/
            string caminho = "";
            try
            {
                caminho = SalvarFoto();
            }
            catch
            {
                lblErro.Text = "Envie um arquivo com as seguintes extensões: .png , .jpg ou .bmp !";
                return;
            }
            
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string query = "";
            //stored procedure de cadastrar médico
            query = "CadastrarM_sp";
            SqlCommand cmdInsert = new SqlCommand(query, Conexao.conexao);
            cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
            cmdInsert.Parameters.AddWithValue("@crm", crm);
            cmdInsert.Parameters.AddWithValue("@nome", nome);
            cmdInsert.Parameters.AddWithValue("@dataNascimento", new SqlDateTime(DateTime.Parse(txtDataNasc.Text)));
            cmdInsert.Parameters.AddWithValue("@email", email);
            cmdInsert.Parameters.AddWithValue("@celular", celular);
            cmdInsert.Parameters.AddWithValue("@telefone", telefone);
            cmdInsert.Parameters.AddWithValue("@foto", caminho);
            cmdInsert.Parameters.AddWithValue("@senha", senha);
            cmdInsert.Parameters.AddWithValue("@codEspecialidade", codEspecSel);
            int linhasMod = cmdInsert.ExecuteNonQuery();//linhas modificadas no BD
            if (linhasMod < 0)
                lblErro.Text = ("Verifique os dados e tente novamente!");
            else
            {
                lblErro.Text = "Sucesso no cadastro do médico " + nome + "!";

                lblErro.ForeColor = System.Drawing.Color.Green;

                LimparTela();
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
    private bool validarDados(int crm, string nome, string dataNasc, string email,
        string celular, string telefone, string senha){
            nome = nome.Trim();
            if (string.IsNullOrEmpty(nome))
            {
                lblErro.Text = "Digite o nome do médico !";
                return false;
            }
            dataNasc = dataNasc.Trim();
            if (string.IsNullOrEmpty(dataNasc))
            {
                lblErro.Text = "Digite a Data de Nascimento do médico!";
                return false;
            }
            email = email.Trim();
            if (string.IsNullOrEmpty(email))
            {
                lblErro.Text = "Digite o email do médico !!";
                return false;
            }
            celular = celular.Trim();
            if (string.IsNullOrEmpty(celular))
            {
                lblErro.Text = "Digite o celular do médico!";
                return false;
            }
            telefone = telefone.Trim();
            if (string.IsNullOrEmpty(telefone))
            {
                lblErro.Text = "Digite o telefone do médico!";
                return false;
            }
            senha = senha.Trim();
            if (string.IsNullOrEmpty(senha))
            {
                lblErro.Text = "Digite a senha do médico!";
                return false;
            }
        return true;
    }
    private string SalvarFoto()
    {
        

            string caminhoCompleto = Directory.CreateDirectory(Server.MapPath(NOME_PASTA_UPLOADS) + Path.DirectorySeparatorChar).FullName;

            string caminho = "";
            if (FormatoValido())
            {
                string nomeArquivo = txtCRM.Text + Path.GetExtension(fupFoto.PostedFile.FileName);

                caminhoCompleto += nomeArquivo;

                fupFoto.PostedFile.SaveAs(caminhoCompleto);

                caminho = caminhoCompleto.Substring(caminhoCompleto.IndexOf(NOME_PASTA_UPLOADS + Path.DirectorySeparatorChar + nomeArquivo));
            }

            return caminho;
        
    }
    private bool FormatoValido()
    {
        foreach (string extensao in formatosValidos)
            if (Path.GetExtension(fupFoto.FileName).ToLower() == extensao)
                return true;

        return false;
    }
}