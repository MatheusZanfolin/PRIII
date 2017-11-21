using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MedicaoEstatisticas : System.Web.UI.Page
{
    
    private const int ANO_CRIACAO_CLINICA = 2017;

    private const int QTDE_LINHAS_GRAF_CONSULTAS = 5;

    private const string MESES_DO_ANO = "Janeiro, Fevereiro, Março, Abril, Maio, Junho, Julho, Agosto, Setembro, Outubro, Novembro, Dezembro";

    private const string COR_BARRA_GRAF_PACIENTE = "Blue";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           /*try
            {
                if ((!string.IsNullOrEmpty(Session["crm"].ToString()))
                    || (!string.IsNullOrEmpty(Session["usuario"].ToString())))
                    Response.Redirect("Principal.aspx");
            }
            catch
            {
                Response.Redirect("Principal.aspx");
            }*/
            
            Inicializar();
        }       
    }

    private void Inicializar()
    {
        PreencherDDLAnoCancelamento();
        PrepararGraficosDosGraficos();
    }    

    private void PreencherGraficos()
    {
        PreencherConsultasMedico();        
        PreencherAtendimentosEspecialidade();
        PreencherConsultasPaciente();
        PreencherCancelamentosConsulta();
    }    

    private void PreencherConsultasMedico()
    {
        grafConsultasMedico.CategoriesAxis = MESES_DO_ANO; //Cada mês tem uma barra no gráfico
        grafConsultasMedico.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS; 

        BarChartSeries valores = new BarChartSeries();

        int crm = Convert.ToInt32(ddlMedico.SelectedValue);

        valores.Data = ConsultasNoAno(DateTime.Now.Year, crm); //Populamento da gráfico

        grafConsultasMedico.Series.Add(valores);

        grafConsultasMedico.Series[0].BarColor = Color.AliceBlue.ToString();
    }    

    private void PreencherAtendimentosEspecialidade()
    {
        grafAtendimentosEspecialidade.ChartWidth  = "500";
        grafAtendimentosEspecialidade.ChartHeight = "300";

        grafAtendimentosEspecialidade.Height = new Unit(310);

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        Dictionary<int, string> especialidades = TodasAsEspecialidades();

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

            novoValor.Data     = leitor.GetInt32(0);
            novoValor.Category = especialidades.Values.ElementAt(i);

            grafAtendimentosEspecialidade.PieChartValues.Add(novoValor);

            leitor.Close();
        }

        Conexao.conexao.Close();
    }        

    private void PreencherConsultasPaciente()
    {
        grafConsultasPaciente.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS;

        ListaDePacientes pacientes = TodosOsPacientes();

        while (pacientes.HaPaciente)
            AdicionarPaciente(pacientes.ProximoPaciente());
    }    

    private void PreencherCancelamentosConsulta()
    {
        grafConsultasMedico.CategoriesAxis = MESES_DO_ANO;
        grafConsultasMedico.ValueAxisLines = QTDE_LINHAS_GRAF_CONSULTAS;

        BarChartSeries valores = new BarChartSeries();

        valores.Data = CancelamentosNoAno(DateTime.Now.Year);

        grafConsultasMedico.Series.Add(valores);

        grafConsultasMedico.Series[0].BarColor = Color.AliceBlue.ToString();
    }

    private decimal[] ConsultasNoAno(int ano, int crm)
    {
        decimal[] consultas = new decimal[12];

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand query = null;
        for (int mes = 0; mes < 12; mes++)
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

            consultas[mes] = consultasDoMes;
        }            

        Conexao.conexao.Close();

        return consultas;
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

        leitor .Close();
        Conexao.conexao.Close();

        return especialidades;
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
            string  nome             = leitor["nomePaciente"].ToString();
            decimal quantasConsultas = Convert.ToDecimal(leitor["qtasConsultas"]);

            var novoPaciente = new Paciente(nome, quantasConsultas);

            new Paciente("", 10);

            lista.Adicionar(novoPaciente);
        }

        leitor .Close();
        Conexao.conexao.Close();

        return lista;
    }

    private void AdicionarPaciente(Paciente p)
    {
        if (!string.IsNullOrEmpty(grafConsultasPaciente.CategoriesAxis))
            grafConsultasPaciente.CategoriesAxis += ", ";

        grafConsultasPaciente.CategoriesAxis += p.Nome;

        var novaBarra = new BarChartSeries();

        novaBarra.Data[0] = p.QuantasConsultas;

        novaBarra.BarColor = COR_BARRA_GRAF_PACIENTE;

        grafConsultasPaciente.Series.Add(novaBarra);
    }

    private decimal[] CancelamentosNoAno(int ano)
    {
        decimal[] cancelamentos = new decimal[12];

        if (Conexao.conexao.State != ConnectionState.Open)
            Conexao.conexao.Open();

        SqlCommand query = null;
        for (int mes = 0; mes < 12; mes++)
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

            cancelamentos[mes] = cancelamentosDoMes;
        }

        Conexao.conexao.Close();

        return cancelamentos;
    }

    private void PrepararGraficosDosGraficos()
    {
        const string ALTURA_GRAF_PADRAO = "200";

        grafConsultasMedico.ChartHeight = "300";
        grafAtendimentosEspecialidade.ChartHeight = ALTURA_GRAF_PADRAO;
        grafCancelamentosConsulta.ChartHeight = ALTURA_GRAF_PADRAO;
        grafConsultasPaciente.ChartHeight = ALTURA_GRAF_PADRAO;

        grafConsultasMedico.ChartWidth = "1100";

        const string ALTURA_PADRAO = "250";

        grafConsultasMedico.Height = new Unit("310");
        grafAtendimentosEspecialidade.Height = new Unit(ALTURA_PADRAO);
        grafCancelamentosConsulta.Height = new Unit(ALTURA_PADRAO);
        grafConsultasPaciente.Height = new Unit(ALTURA_PADRAO);
    }

    private void PreencherDDLAnoCancelamento()
    {
        for (int ano = ANO_CRIACAO_CLINICA; ano <= DateTime.Now.Year; ano++)
            ddlAnoCancelamento.Items.Add(ano.ToString());
    }

    protected void ddlMedico_Load(object sender, EventArgs e)
    {
   
    }


    protected void ddlMedico_Init(object sender, EventArgs e)
    {

    }

    protected void ddlMedico_DataBound(object sender, EventArgs e)
    {
        PreencherGraficos();
    }
}