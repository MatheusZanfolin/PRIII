using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstatisticaConsultasPaciente : System.Web.UI.Page
{
    private const string COR_BARRA_GRAF_PACIENTE    = "Blue";
    private const int    QTDE_LINHAS_GRAF_CONSULTAS = 5;

    protected void Page_Load(object sender, EventArgs e)
    {
        PrepararGraficosDoGrafico();
        PreencherConsultasPaciente();
    }

    private void PrepararGraficosDoGrafico()
    {
        grafConsultasPaciente.ChartType  = BarChartType.Bar;
        grafConsultasPaciente.Width      = new Unit(1100, UnitType.Pixel);
        grafConsultasPaciente.ChartWidth = "1050";

        double altura = 100;

        grafConsultasPaciente.ChartHeight = altura.ToString();
        grafConsultasPaciente.Height      = new Unit(altura, UnitType.Pixel);
    }

    private void PreencherConsultasPaciente()
    {
        grafConsultasPaciente.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS;

        ListaDePacientes pacientes = TodosOsPacientes();

        BarChartSeries novaSerie = new BarChartSeries();

        novaSerie.Name     = "Nº de consultas do paciente";
        novaSerie.BarColor = COR_BARRA_GRAF_PACIENTE;

        int quantosPacientes = pacientes.QuantosPacientes;

        decimal[] numConsultasPaciente = new decimal[quantosPacientes];

        for (int i = 0; i < quantosPacientes; i++)
        {
            var paciente = pacientes.ProximoPaciente();

            if (!string.IsNullOrEmpty(grafConsultasPaciente.CategoriesAxis))
                grafConsultasPaciente.CategoriesAxis += ", ";

            grafConsultasPaciente.CategoriesAxis += paciente.Nome;

            numConsultasPaciente[i] = paciente.QuantasConsultas;

            var altura = Convert.ToDouble(grafConsultasPaciente.Height.Value.ToString());

            altura += 50;

            grafConsultasPaciente.Height = new Unit(altura, UnitType.Pixel);
            grafConsultasPaciente.ChartHeight = altura.ToString();
        }

        novaSerie.Data = numConsultasPaciente;

        grafConsultasPaciente.Series.Add(novaSerie);
        

        //while (pacientes.HaPaciente)
        //    AdicionarPaciente(pacientes.ProximoPaciente());
    }

    private void AdicionarPaciente(Paciente p)
    {
        

        var novaBarra = new BarChartSeries();

        novaBarra.Name    = "Nº de consultas";
        novaBarra.Data    = new decimal[1];
        novaBarra.Data[0] = p.QuantasConsultas;

        novaBarra.BarColor = COR_BARRA_GRAF_PACIENTE;

        grafConsultasPaciente.Series.Add(novaBarra);
    }

    private ListaDePacientes TodosOsPacientes()
    {
        ListaDePacientes lista = new ListaDePacientes();

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand query = new SqlCommand("consultasDosPacientes_sp", Conexao.conexao);

        query.CommandType = CommandType.StoredProcedure;

        var leitor = query.ExecuteReader();

        while (leitor.Read())
        {
            string nome = leitor["nomePaciente"].ToString();
            decimal quantasConsultas = Convert.ToDecimal(leitor["qtasConsultas"]);

            var novoPaciente = new Paciente(nome, quantasConsultas);

            lista.Adicionar(novoPaciente);
        }

        leitor.Close();
        Conexao.conexao.Close();

        return lista;
    }
}