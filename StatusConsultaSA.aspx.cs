using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlTypes;

public partial class StatusConsultaSA : System.Web.UI.Page
{//default(classes e structs) é private, por isso não coloquei
    SqlDataReader rdr;
    int numeroDeConsultas=0;
    
    List<Medico> medicos = new List<Medico>();//Lista de médicos
    int crmSel;//INFORMAÇÕES DA CONSULTA SELECIONADA
    //string usuario;
    string statusAt = "";//status atual*/
    DateTime data;//data da consulta selecionada
    protected void Page_Load(object sender, EventArgs e)
    {
        /* try
         {
             lblErro.Text = "Só clique no botão alterar status se deu a hora da consulta e ele chegou ou faltou, ou então se ele não comparecerá à consulta !";
             SqlConnection Conexao.conexao;
             string query = "selectMed_sp";//seleciona todos os médicos cadastrados
             SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
             cmd.CommandType = System.Data.CommandType.StoredProcedure;
             rdr = cmd.ExecuteReader();
             if (rdr.HasRows)
             {
                 while (rdr.Read())
                 {//popula a lista de médicos
                     medicos.Add(new Medico(Convert.ToInt32(rdr.GetValue(0)), rdr.GetValue(1).ToString()));
                     lsbMedico.Items.Add(medicos.Last().Nome);
                 }//popula o listbox com os nomes dos médicos
             }
             else
             {
                 lblErro.Text = "Não há médicos cadastrados!";
             }
             rdr.Close();
             rdr = null;
         }
         catch
         {
             lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
         }*/
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

    /* protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
     {//quando o secretário seleciona um médico
         string nomeMedSel = lsbMedico.Items[lsbMedico.SelectedIndex].Value;
         crmSel = medicos.Find(x -> x.Nome == nomeMedSel).CRM;
         txtPaciente.Enabled = true;
         cldConsulta.Enabled = true;
}*/


    /*protected void cldConsulta_SelectionChanged(object sender, EventArgs e)
    {//quando a pessoa selecionar uma data do calendário
            
            //data = cldData.SelectedDate;
        
        }*/
    private void insereNaMatriz(DateTime horario, bool meiaHora, int crm, int codConsulta, string nomeMed, string usuario, string nomePac)
    {
        int indCol = 0;
        int hora = horario.Hour;
        int min = horario.Minute;
        int indLinha = pegarIndLinha(hora, min);
        int qtdCol = (tabDados.Rows[0].Cells.Count) ;//número de colunas
        bool medJaTabela = JaFoiIncluido(crm);//nome do médico já está na tabela

        if (!medJaTabela)
        {
            tabDados.Rows[0].Cells.Add(new TableCell());
            tabDados.Rows[0].Cells[qtdCol].Text = "Médico: " + nomeMed + " - CRM: " + crm;
            tabDados.Rows[1].Cells.Add(new TableCell());
            tabDados.Rows[1].Cells[qtdCol].Text = "Código da Consulta:/Nome do Paciente:";
            for (int i = 2; i < 14; i++)
                tabDados.Rows[i].Cells.Add(new TableCell());

            indCol = qtdCol;

        }
        else
            indCol = IndiceMedico(crm);

        tabDados.Rows[indLinha].Cells[indCol].Text = codConsulta + " - " + nomePac;
        tabDados.Rows[indLinha].Cells[indCol].HorizontalAlign = HorizontalAlign.Center;


        if (!meiaHora)
        {
            tabDados.Rows[indLinha + 1].Cells[indCol].Text = codConsulta + " - " + nomePac;
            tabDados.Rows[indLinha + 1].Cells[indCol].HorizontalAlign = HorizontalAlign.Center;
        }            
    }

    private int IndiceMedico(int crm)
    {
        if (tabDados.Rows.Count <= 0)
            throw new Exception("StatusConsultaSA: tabela de consultas vazia.");

        char[] espacoEmBranco = { ' ' };

        for (int coluna = 1; coluna < tabDados.Rows[0].Cells.Count; coluna++)
            foreach (string palavra in tabDados.Rows[0].Cells[coluna].Text.Split(espacoEmBranco))
                if (palavra == crm.ToString())
                    return coluna;

        throw new Exception("StatusConsultaSA: O médico não está na tabela.");
    }

    private bool JaFoiIncluido(int crm)
    {
        if (tabDados.Rows.Count <= 0)
            return false;

        char[] espacoEmBranco = { ' ' };

        for (int coluna = 1; coluna < tabDados.Rows[0].Cells.Count; coluna++)
            foreach (string palavra in tabDados.Rows[0].Cells[coluna].Text.Split(espacoEmBranco))
                if (palavra == crm.ToString())
                    return true;

        return false;
    }

    private int pegarIndLinha(int hora, int min)
    {
        int coluna = 2;
        hora    *= 2;
        if (min == 30)
            hora++;
        if (hora >= 28)//volta ao trabalho às 14h
            hora -=4;//horário de almoço: dura 2h
        hora -= 16;
        return hora;
    }
   // protected void btnAgendar_Click(object sender, EventArgs e)
       /*{
        try
        {
            bool compareceu = rbPresenca.Checked;
            string novoStatus;
            string update;
            SqlCommand cmd;
            if (txtStatusAt.Text == "AGENDADA")//se a consulta está marcada
            {
                update = "alterarStatus_sp  ";
                cmd = new SqlCommand(update, Conexao.conexao);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (compareceu)
                {
                    novoStatus = "PENDENTE";//se o paciente compareceu(ainda não há diagnóstico nem avaliação)
                }
                else
                {
                    novoStatus = "CANCELADA";//paciente faltou/cancelou
                }
                cmd.Parameters.Add(new SqlParameter("@novoStatus", novoStatus));
                cmd.Parameters.Add(new SqlParameter("@crm", crmSel));
                cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@data", data));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    lblErro.Text = "Sucesso ao alterar status para " + novoStatus + " !!";
                }
                else
                {
                    lblErro.Text = "Verifique os dados e tente novamente!";
                }
            }
            else
            {
                lblErro.Text = "Impossível alterar o status, já que a presença do paciente já foi marcada!";
            }
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }*/
   // }

    protected void btnRedefinir_Click(object sender, EventArgs e)
    {
        int numCol = tabDados.Rows[0].Cells.Count;
        int numLin = tabDados.Rows.Count;
        for(int i = numLin - 1; i >= 0; i--)
        {
            for(int j = numCol - 1; j >= 0; j--)
                tabDados.Rows[i].Cells.RemoveAt(j);


            tabDados.Rows.RemoveAt(i);
        }
        data = DateTime.Parse(txtData.Text);

        btnGerarRel_Click(null, null);//força a reinicialização
    }

    protected void btnGerarRel_Click(object sender, EventArgs e)
    {
        try
        {
            lblErro.Text = string.Empty;
            lblNum.Visible = lblConsulta.Visible = true;

            data = DateTime.Parse(txtData.Text);

            if (Conexao.conexao.State != ConnectionState.Open)
                Conexao.conexao.Open();

            numeroDeConsultas = 0;
            string query = "select * from agendaConsultaDia_view " +
                "where CAST(dataHoraConsulta AS DATE) = CAST(@data AS DATE)";

            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@data", new SqlDateTime(data)));
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                tabDados.Rows.Add(new TableRow());
                tabDados.Rows[0].Cells.Add(new TableCell());
                tabDados.Rows[0].Cells[0].Text = "Data: " + data.Day + "/" + data.Month + "/" + data.Year;
                tabDados.Rows.Add(new TableRow());
                tabDados.Rows[1].Cells.Add(new TableCell());
                tabDados.Rows[1].Cells[0].Text = "Horário: ";
                int hora = 18;
                for (int i = 2; i < 14; i++)
                {
		            tabDados.Rows.Add(new TableRow());
                    tabDados.Rows[i].Cells.Add(new TableCell());
                    tabDados.Rows[i].Cells[0].Text = hora / 2 + "h";
                    if (hora % 2 != 0)
                    {
                        tabDados.Rows[i].Cells[0].Text += "30 - " + (hora + 1) / 2 + "h00";
                    }
                    else
                    {
                        tabDados.Rows[i].Cells[0].Text += "00 - " + (hora / 2) + "h30";
                    }
                    hora++;
                    if (hora == 24)
                        hora = 28;
                }
                numeroDeConsultas = 0;
                while (rdr.Read())
                {
                    numeroDeConsultas++;
                    insereNaMatriz(DateTime.Parse(rdr.GetValue(0).ToString()), rdr.GetBoolean(1), Convert.ToInt32(rdr.GetValue(2)),
                        Convert.ToInt32(rdr.GetValue(3)), rdr.GetValue(4).ToString(), rdr.GetValue(5).ToString(), rdr.GetValue(6).ToString());
                }
            }
            else
            {
                numeroDeConsultas = 0;
                lblErro.Text = "Não há nenhuma consulta marcada neste dia!";
                lblNum.Visible = lblConsulta.Visible = false;
            }

            rdr.Close();
            rdr = null;
            lblConsulta.Text = numeroDeConsultas.ToString();
            Conexao.conexao.Close();
        }
        catch(Exception ex)
        {
            if (rdr != null)
                rdr.Close();

            if (Conexao.conexao.State != ConnectionState.Closed)
                Conexao.conexao.Close();

            rdr = null;

            lblErro.Text ="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void btnAlterarConsulta_Click(object sender, EventArgs e)
    {
        try {
            if (string.IsNullOrEmpty(txtAlterarConsulta.Text))
            {
                lblSemCodConsulta.Visible = true;

                return;
            }

            int codConsultaAlterar = Convert.ToInt32(txtAlterarConsulta.Text);

            if (Conexao.conexao.State != ConnectionState.Open)
                Conexao.conexao.Open();

            string update = "alterarStatus_sp";
            SqlCommand cmd = new SqlCommand(update, Conexao.conexao);
            cmd.CommandType =CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@novoStatus", "CANCELADA"));
            cmd.Parameters.Add(new SqlParameter("@codConsulta", codConsultaAlterar));
            int linhasAlteradas = cmd.ExecuteNonQuery();
            if (linhasAlteradas > 1)
            {
                lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
                Conexao.conexao.Close();
                return;
            }
            if(linhasAlteradas == 1)
            {
                lblErro.Text = "Sucesso ao cancelar a consulta de código "+codConsultaAlterar+" !";

                lblErro.ForeColor = System.Drawing.Color.Green;

                lblConsulta.Text = (Convert.ToInt32(lblConsulta.Text) - 1).ToString();
            }
            else
            {
                lblErro.Text = "Verifique os dados e tente novamente!";
            }
            Conexao.conexao.Close();
        }
        catch
        {
            if (Conexao.conexao.State != ConnectionState.Closed)
                Conexao.conexao.Close();
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}