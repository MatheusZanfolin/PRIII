using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AvaliacaoConsulta : System.Web.UI.Page
{
    private string usuario;

    private SqlDataReader leitor;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["paciente"].ToString()))
                    Response.Redirect("LogonPac.aspx");
                else
                    usuario = Session["paciente"].ToString();
            }
            catch
            {
                Response.Redirect("LogonPac.aspx");
            }
            
            try
            {
                AtualizarConsultas();

            }
            catch
            {
                lblMensagem.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde...";
            }
        }
    }

    private void AtualizarConsultas()
    {
        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        ddlConsulta.Items.Clear();

        var comando = new SqlCommand("SELECT * FROM consultasPendentes_vw WHERE usuario = @usuario", Conexao.conexao);

        comando.Parameters.AddWithValue("@usuario", Session["paciente"].ToString());

        leitor = comando.ExecuteReader();

        while (leitor.Read())
            AdicionarConsulta(leitor["codConsulta"].ToString(), DateTime.Parse(leitor["dataHoraConsulta"].ToString()), (bool)leitor["meiaHora"], leitor["nomeMedico"].ToString(), leitor["nomeEspecialidade"].ToString());

        leitor.Close();
        Conexao.conexao.Close();

        leitor = null;
     }

    private void AdicionarConsulta(string codConsulta, DateTime dataHoraConsulta, bool meiaHora, string nomeMedico, string nomeEspecialidade)
    {
        string novoItem = nomeMedico + " (" + nomeEspecialidade + ") " + dataHoraConsulta.ToString("dd/MM/yyyy HH:mm") + " -> "; //A consulta deve ser formatada para melhos visualização

        novoItem += meiaHora ? dataHoraConsulta.AddMinutes(30).ToString("HH:mm") : dataHoraConsulta.AddHours(1).ToString("HH:mm"); //O término da consulta é calculado baseado em se esta foi cadastrada como "meiaHora" ou não

        ddlConsulta.Items.Add(novoItem);

        ddlConsulta.Items[ddlConsulta.Items.Count - 1].Value = codConsulta;
    }

    protected void btnPostar_Click(object sender, EventArgs e)
    {
        try
        {
            int nota = Convert.ToInt32(txtNota.Text);
            if (nota < 0 || nota > 10)
            {
                lblMensagem.Text = "Digite uma nota válida!";
                return;
            }
            string comentario = txtComentario.Text;

            if (Conexao.conexao.State != ConnectionState.Open)
                Conexao.conexao.Open();

            var comando = new SqlCommand("avaliacaoConsultaPaciente_sp", Conexao.conexao);

            comando.CommandType = CommandType.StoredProcedure;                                 

            comando.Parameters.AddWithValue("@codConsulta", ddlConsulta.SelectedValue);
            comando.Parameters.AddWithValue("@nivelSatisfacao", nota);
            comando.Parameters.AddWithValue("@descricao", comentario);
            
            int linhasAlteradas = comando.ExecuteNonQuery();

            Conexao.conexao.Close();

            if (linhasAlteradas != 2) // Uma na tabela de avaliações e outra na tabela de consultas (que foi atualizada)
            {
                throw new Exception("AvaliacaoConsulta: Erro de BD");
            }
            else
            {
                lblMensagem.Text = "Avaliação cadastrada com sucesso!";
                lblMensagem.ForeColor = System.Drawing.Color.Green;
            }

            AtualizarConsultas();
        }
        catch
        {
            if (Conexao.conexao!=null && Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();

            lblMensagem.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Tente novamente mais tarde...";

            lblMensagem.ForeColor = System.Drawing.Color.Red;
        }
    }
}