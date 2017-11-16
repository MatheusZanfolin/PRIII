using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Descrição resumida de Conexao
/// </summary>
public class Conexao
{
    private static SqlConnection con = new SqlConnection (stringDeConexao);
    public static SqlConnection conexao
    {
        get {
            return con;
        }

        set
        {
            if (value == null)
                throw new Exception("Conexao: atribuição de conexão nula.");

            con = value;
        }
    }

    public static string stringDeConexao
    {
        get { return WebConfigurationManager.ConnectionStrings["PRII16191ConnectionString"].ConnectionString; }
    }
}