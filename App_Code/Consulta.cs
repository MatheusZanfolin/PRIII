using System;

public partial class Consulta{
private int codConsulta;
public int CodConsulta { 
get;
private set;
}
private DateTime dataHoraConsulta;
public DateTime DataHoraConsulta{
get;
private set;
}
private bool meiaHora;
public  bool MeiaHora{
get;
private set;
}
private int codStatus;
public int CodStatus{
get;
private set;
}
private string diagnostico;
public string Diagnostico{
get;
private set;
}
private int codAvaliacao;
public int CodAvaliacao{
get;
private set;
}
private int crm;
public int Crm{
get;
private set;
}
private string usuario;
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
        this.codConsulta = codConsulta;
        this.dataHoraConsulta = dataHoraConsulta;
        this.crm = crm;
        this.usuario = usuario;

    }
}