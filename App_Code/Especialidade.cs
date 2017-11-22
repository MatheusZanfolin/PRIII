using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Especialidade
/// </summary>
public class Especialidade
{
    private string nomeEspecialidade;
    public string NomeEspecialidade
    {
        get
        {
            return nomeEspecialidade;
        }
    }
    private int codEspecialidade;
    public int CodEspecialidade
    {
        get
        {
            return codEspecialidade;
        }
    }
    public Especialidade(int cod, string nome)
    {
        if (cod <= 0)
            throw new Exception("Código de Especialidade inválido");
        if(nome == null || nome =="")
            throw new Exception("Nome de Especialidade inválido");
        this.codEspecialidade = cod;
        this.nomeEspecialidade = nome;
        //
        // TODO: Add constructor logic here
        //
    }
}