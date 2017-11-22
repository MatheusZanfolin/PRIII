using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Medico
/// </summary>
public class Medico
{
   
    public int CRM{
        get;
        private set;
	}
    
    public string Nome{
        get;
        private set;
    }
    
    public DateTime DataNascimento{
        get;
        private set;
    }
   
    public string Email{
        get;
        private set;
    }
   
    public string Celular{
        get;
        private set;
    }
   
    public string Telefone{
        get;
        private set;
    }
   
    public string Senha{
        get;
        private set;
    }
   
    public int CodEspecialidade{
        get;
        private set;
    }
  
    public string Foto{
        get;
        private set;
    }
	
    public Medico(int c, string n, int e)
    {
        if (c <= 0)
            throw new Exception("Código de Médico inválido!");
        if(n==null || n=="")
            throw new Exception("Nome de Médico inválido!");
        if(e<=0)
            throw new Exception("Código de Especialidade de Médico inválido!");
        this.CRM = c;
        this.Nome = n;
        this.CodEspecialidade = e;
    }
    public Medico(int c, string n) {
        if (c <= 0)
            throw new Exception("Código de Médico inválido!");
        if (n == null || n == "")
            throw new Exception("Nome de Médico inválido!");
        this.CRM = c;
        this.Nome = n;
    }
    public bool Equals(Object outro)
    {
        if (this == outro)
            return true;
        if (outro == null)
            return false;
        Medico m = (Medico)outro;
        if (m.CRM == CRM && m.Nome == Nome)
            return true;
        return false;
    }
}