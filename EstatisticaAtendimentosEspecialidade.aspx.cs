using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstatisticaAtendimentosEspecialidade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PrepararGraficosDoGrafico();

        if (!IsPostBack)
            grafAtendimentosEspecialidade.Visible = false;
    }

    private void PrepararGraficosDoGrafico()
    {
        grafAtendimentosEspecialidade.Width = new Unit(100, UnitType.Percentage);
        grafAtendimentosEspecialidade.ChartWidth = "1050";

        grafAtendimentosEspecialidade.Height = new Unit(500, UnitType.Pixel);
        grafAtendimentosEspecialidade.ChartHeight = "500";
    }

    protected void txtDiaConsultas_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDiaConsultas.Text))
            return;

        PreencherAtendimentosEspecialidade();
    }

    private void PreencherAtendimentosEspecialidade()
    {
        Dictionary<int, string> especialidades = TodasAsEspecialidades();

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand procedimento = null;
        for (int i = 0; i < especialidades.Count; i++)
        {
            PieChartValue novoValor = new PieChartValue();

            procedimento = new SqlCommand("atendimentoPorEspecialidade_sp", Conexao.conexao);
            procedimento.CommandType = CommandType.StoredProcedure;

            procedimento.Parameters.AddWithValue("@codEspecialidade", especialidades.Keys.ElementAt(i));
            procedimento.Parameters.AddWithValue("@dia", new SqlDateTime(DateTime.Parse(txtDiaConsultas.Text)));

            var leitor = procedimento.ExecuteReader();

            if (!leitor.Read())
                throw new Exception("MedicaoEstatistica: erro de BD");

            novoValor.Data = leitor.GetInt32(0);
            novoValor.Category = especialidades.Values.ElementAt(i);

            grafAtendimentosEspecialidade.PieChartValues.Add(novoValor);

            leitor.Close();
        }

        if (grafAtendimentosEspecialidade.PieChartValues.Count == 1) //O gráfico não funcionará corretamente com apenas um elemento
        {
            var valorDeSuporte = new PieChartValue();

            valorDeSuporte.Category = ".";
            valorDeSuporte.Data     = 0.1M;

            grafAtendimentosEspecialidade.PieChartValues.Add(valorDeSuporte);
        }

        Conexao.conexao.Close();

        grafAtendimentosEspecialidade.Visible = true;
    }

    private Dictionary<int, string> TodasAsEspecialidades()
    {
        Dictionary<int, string> especialidades = new Dictionary<int, string>();

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand comando = new SqlCommand("SELECT codEspecialidade, nomeEspecialidade FROM Especialidade", Conexao.conexao);

        var leitor = comando.ExecuteReader();

        if (!leitor.HasRows)
            throw new Exception("MedicaoEstatistica: não existem especialidades");

        while (leitor.Read())
            especialidades.Add(Convert.ToInt32(leitor["codEspecialidade"]), leitor["nomeEspecialidade"].ToString());

        leitor.Close();
        Conexao.conexao.Close();

        return especialidades;
    }
}