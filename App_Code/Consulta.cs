using System;

public partial class Consulta{

public int CodConsulta { 
get;
private set;
}

public DateTime DataHoraConsulta{
get;
private set;
}

public  bool MeiaHora{
get;
private set;
}

public int CodStatus
{
get;
private set;
}
public string Diagnostico{
get;
private set;
}

public int CodAvaliacao{
get;
private set;
}

public int Crm{
get;
private set;
}

public string Usuario{
get;
private set;
}
    public Consulta(int codConsulta, DateTime dataHoraConsulta,
     int crm, string usuario)
    {
        if (codConsulta <= 0)
            throw new Exception("C�digo de consulta inv�lido!");
        if (dataHoraConsulta == null)
            throw new Exception("Data da consulta inv�lida!");
        if (crm <= 0)
            throw new Exception("CRM inv�lido!");
        if (string.IsNullOrEmpty(usuario))
            throw new Exception("Nome de usu�rio inv�lido!");
        this.CodConsulta = codConsulta;
        this.DataHoraConsulta = dataHoraConsulta;
        this.Crm = crm;
        this.Usuario = usuario;

    }
    public bool Equals(Object outro)
    {
        if (this == outro)
            return true;
        if (outro == null)
            return false;
        Consulta c = (Consulta)outro;
        if (c.CodConsulta == this.CodConsulta && c.DataHoraConsulta.Equals(DataHoraConsulta) && c.Crm == Crm && c.Usuario == Usuario)
            return true;
        return false;
    }
}