using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstatisticaCancelamentosConsulta : System.Web.UI.Page
{
    private const int ANO_CRIACAO_CLINICA = 2017;

    private const string MESES_DO_ANO = "Janeiro, Fevereiro, Março, Abril, Maio, Junho, Julho, Agosto, Setembro, Outubro, Novembro, Dezembro";

    private const int QTDE_LINHAS_GRAF_CONSULTAS = 5;

    private const string COR_BARRA_GRAFICO_CANCELAMENTOS = "#900000";

    protected void Page_Load(object sender, EventArgs e)
    {        
        PreencherDDLAnoCancelamento();
        PrepararGraficosDoGrafico();
        PreencherCancelamentosConsulta();
    }

    private void PrepararGraficosDoGrafico()
    {
        grafCancelamentosConsulta.Width = new Unit(100, UnitType.Percentage);
        grafCancelamentosConsulta.ChartWidth = "1050";

        grafCancelamentosConsulta.Height = new Unit(500, UnitType.Pixel);
        grafCancelamentosConsulta.ChartHeight = "500";
    }

    private void PreencherCancelamentosConsulta()
    {
        grafCancelamentosConsulta.Series.Clear();

        grafCancelamentosConsulta.CategoriesAxis = MESES_DO_ANO;
        grafCancelamentosConsulta.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS;

        BarChartSeries valores = new BarChartSeries();

        valores.Name = "Nº de consultas canceladas";
        valores.Data = CancelamentosNoAno(DateTime.Now.Year);

        valores.BarColor = COR_BARRA_GRAFICO_CANCELAMENTOS;

        grafCancelamentosConsulta.Series.Add(valores);

        grafCancelamentosConsulta.Series[0].BarColor = Color.AliceBlue.ToString();
    }

    private decimal[] CancelamentosNoAno(int ano)
    {
        decimal[] cancelamentos = new decimal[12];

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand query = null;
        for (int mes = 1; mes <= 12; mes++)
        {
            decimal cancelamentosDoMes = 0;

            query = new SqlCommand("consultasCanceladasNoMes_sp", Conexao.conexao);
            query.CommandType = CommandType.StoredProcedure;


            query.Parameters.AddWithValue("@mes", mes);
            query.Parameters.AddWithValue("@ano", ano);

            var leitor = query.ExecuteReader();

            if (!leitor.Read())
                throw new Exception("MedicaoEstatistica: Erro de BD");

            cancelamentosDoMes = leitor.GetInt32(0);

            leitor.Close();

            cancelamentos[mes - 1] = cancelamentosDoMes;
        }

        Conexao.conexao.Close();

        return cancelamentos;
    }

    private void PreencherDDLAnoCancelamento()
    {
        if (!IsPostBack)
        {
            for (int ano = ANO_CRIACAO_CLINICA; ano <= DateTime.Now.Year; ano++)
                ddlAnoCancelamento.Items.Add(ano.ToString());

            if (ddlAnoCancelamento.Items.Count > 1)
                ddlAnoCancelamento.SelectedIndex = 1;
        }            
    }

    protected void ddlAnoCancelamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnoCancelamento.SelectedIndex < 1)
            return;

        PreencherCancelamentosConsulta();
    }
}