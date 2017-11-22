using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Paciente
{
    public Paciente(string nome, decimal quantasConsultas)
    {
        if (string.IsNullOrEmpty(nome))
            throw new Exception("Paciente: nome vazio.");

        if (quantasConsultas < 0)
            throw new Exception("Paciente: nº de consultas inválido.");

        Nome             = nome;
        QuantasConsultas = quantasConsultas;
    }
    public Paciente(string usuario, string nome)
    {
        if (string.IsNullOrEmpty(usuario))
            throw new Exception("Nome de usuário inválido!");
        if (string.IsNullOrEmpty(nome))
            throw new Exception("Nome de paciente inválido!");
        Usuario = usuario;
        Nome = nome;       
    }
    public bool Equals(Object outro)
    {
        if (outro == this)
            return true;
        if (outro == null)
            return false;
        Paciente p = (Paciente)outro;
        if (p.Nome == Nome && p.Usuario == Usuario)
            return true;
        return false;
    }
    public string Nome { get; private set; }

    public decimal QuantasConsultas { get; private set; }
    public string Usuario { get; private set; }
}