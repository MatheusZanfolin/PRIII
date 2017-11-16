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
            throw new Exception("Código de consulta inválido!");
        if (dataHoraConsulta == null)
            throw new Exception("Data da consulta inválida!");
        if (crm <= 0)
            throw new Exception("CRM inválido!");
        if (string.IsNullOrEmpty(usuario))
            throw new Exception("Nome de usuário inválido!");
        this.codConsulta = codConsulta;
        this.dataHoraConsulta = dataHoraConsulta;
        this.crm = crm;
        this.usuario = usuario;

    }
}