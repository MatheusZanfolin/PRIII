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

public partial class EstatisticaConsultasMedico : System.Web.UI.Page
{
    private const string MESES_DO_ANO = "Janeiro, Fevereiro, Março, Abril, Maio, Junho, Julho, Agosto, Setembro, Outubro, Novembro, Dezembro";

    private const int QTDE_LINHAS_GRAF_CONSULTAS = 5;

    protected void Page_Load(object sender, EventArgs e)
    {
        PrepararGraficosDoGrafico();
    }

    private void PrepararGraficosDoGrafico()
    {
        grafConsultasMedico.Width      = new Unit(100, UnitType.Percentage);
        grafConsultasMedico.ChartWidth = "1050";

        grafConsultasMedico.Height = new Unit(500, UnitType.Pixel);
        grafConsultasMedico.ChartHeight = "500";
    }

    private void PreencherConsultasMedico()
    {
        grafConsultasMedico.CategoriesAxis = MESES_DO_ANO; //Cada mês tem uma barra no gráfico
        grafConsultasMedico.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS;

        BarChartSeries valores = new BarChartSeries();

        int crm = Convert.ToInt32(ddlMedico.SelectedValue);

        valores.Name = "Nº de consultas";
        valores.Data = ConsultasNoAno(DateTime.Now.Year, crm); //Populamento da gráfico

        grafConsultasMedico.Series.Add(valores);

        grafConsultasMedico.Series[0].BarColor = Color.AliceBlue.ToString();
    }

    protected void ddlMedico_DataBound(object sender, EventArgs e)
    {
        if (ddlMedico.Items.Count > 0)
        {
            ddlMedico.SelectedIndex = 0;

            PreencherConsultasMedico();
        }
    }

    private decimal[] ConsultasNoAno(int ano, int crm)
    {
        decimal[] consultas = new decimal[12];

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand query = null;
        for (int mes = 1; mes <= 12; mes++)
        {
            decimal consultasDoMes = 0;

            query = new SqlCommand("consultasNoMes_sp", Conexao.conexao);
            query.CommandType = CommandType.StoredProcedure;

            query.Parameters.AddWithValue("@ano", ano);
            query.Parameters.AddWithValue("@mes", mes);
            query.Parameters.AddWithValue("@crm", crm);

            var leitor = query.ExecuteReader();

            if (!leitor.Read())
                throw new Exception("MedicaoEstatistica: Erro de BD");

            consultasDoMes = leitor.GetInt32(0);

            leitor.Close();

            consultas[mes - 1] = consultasDoMes;
        }

        Conexao.conexao.Close();

        return consultas;
    }

    protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencherConsultasMedico();
    }

    protected void ddlMedico_DataBinding(object sender, EventArgs e)
    {
        if (ddlMedico.Items.Count == 0)
            ddlMedico.Items.Add("Escolha o médico");
    }
}