using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for MedicaoEstatistica
/// </summary>
public class MedicaoEstatistica
{
    private static string stringDeConexao = WebConfigurationManager.ConnectionStrings["PRII16191ConnectionString"].ConnectionString;

    public static int QuantasConsultasDoMedico(string crmMedico, int mes)
    {
        if (!MesValido(mes))
            throw new Exception("MedicaoEstatistica: pesquisa por consulta em mês inválido");

        int quantasConsultas = -1;

        var conexao = new SqlConnection(stringDeConexao);

        conexao.Open();

        var storedProcedure = "exec consultasNoMes_sp @mes, @crm";

        var comando = new SqlCommand(storedProcedure, conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@mes", mes);
        comando.Parameters.AddWithValue("@crm", crmMedico);

        SqlDataReader leitor = comando.ExecuteReader();
        if (!leitor.Read())
            throw new Exception("MedicaoEstatistica: erro de leitura do BD");

        quantasConsultas = leitor.GetInt32(0);

        leitor.Close();

        conexao.Close();
        
        return quantasConsultas;
    }

    public static int QuantosAtendimentosDaEspecialidade(int codEspecialidade, SqlDateTime dia)
    {
        if (!ChavePrimariaValida(codEspecialidade))
            throw new Exception("MedicaoEstatistica: busca por atendimentos com código de especialidade inválido");

        if (CompararData(dia) > 0)
            throw new Exception("MedicaoEstatistica: busca por atendimentos em data no futuro");

        int quantosAtendimentos = -1;

        var conexao = new SqlConnection(stringDeConexao);

        conexao.Open();

        var storedProcedure = "exec atendimentoPorEspecialidade_sp @especialidade, @dia";

        var comando = new SqlCommand(storedProcedure, conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@especialidade", codEspecialidade);
        comando.Parameters.AddWithValue("@dia", dia);

        SqlDataReader leitor = comando.ExecuteReader();
        if (!leitor.Read())
            throw new Exception("MedicaoEstatistica: erro de leitura do BD");

        quantosAtendimentos = leitor.GetInt32(0);

        leitor.Close();

        conexao.Close();

        return quantosAtendimentos;
    }

    public static int QuantasConsultasDoPaciente(int codPaciente)
    {
        if (!ChavePrimariaValida(codPaciente))
            throw new Exception("MedicaoEstatistica: busca por consultas de paciente com chave inválida");

        int quantasConsultas = -1;

        var conexao = new SqlConnection(stringDeConexao);

        conexao.Open();

        var storedProcedure = "exec consultasDoPaciente_sp @paciente";

        var comando = new SqlCommand(storedProcedure, conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@paciente", codPaciente);

        SqlDataReader leitor = comando.ExecuteReader();
        if (!leitor.Read())
            throw new Exception("MedicaoEstatistica: erro de leitura do BD");

        quantasConsultas = leitor.GetInt32(0);

        leitor.Close();

        conexao.Close();

        return quantasConsultas;
    }

    public static int QuantasConsultasCanceladasNoMes(int mes, int ano)
    {
        if (!DataValida(mes, ano))
            throw new Exception();

        int quantasConsultasCanceladas = -1;

        var conexao = new SqlConnection(stringDeConexao);

        conexao.Open();

        var storedProcedure = "exec consultasCanceladasNoMes_sp @mes, @ano";

        var comando = new SqlCommand(storedProcedure, conexao);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@mes", mes);
        comando.Parameters.AddWithValue("@ano", ano);

        SqlDataReader leitor = comando.ExecuteReader();
        if (!leitor.Read())
            throw new Exception("MedicaoEstatistica: erro de leitura do BD");

        quantasConsultasCanceladas = leitor.GetInt32(0);

        leitor.Close();

        conexao.Close();

        return quantasConsultasCanceladas;
    }

    private static bool DataValida(int mes, int ano)
    {
        if (!MesValido(mes))
            return false;

        return CompararAno(ano) <= 0;
    }

    private static bool ChavePrimariaValida(int chave)
    {
        return chave > 0;
    }

    private static bool MesValido(int mes)
    {
        return (mes > 0) && (mes < 13);
    }

    private static int CompararAno(int ano)
    {
        return ano - DateTime.Now.Year;
    }

    private static int CompararData(SqlDateTime data)
    {
        return data.CompareTo(new SqlDateTime(DateTime.Now));
    }
}