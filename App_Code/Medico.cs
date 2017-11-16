using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Medico
/// </summary>
public class Medico
{
    private int crm;
    public int CRM{
	get{
	return crm;

	}
	}
    private string nome;
    public string Nome{
	get{
	return nome;

	}
	}
    private DateTime dataNascimento;
    public DateTime DataNascimento{
	get{
            return dataNascimento;

	}
	}
    private string email;
    public string Email{
	get{
	return email;

	}
	}
    private string celular;
    public string Celular{
	get{
	return celular;

	}
	}
    private string telefone;
    public string Telefone{
	get{
	return telefone;

	}
	}
    private string senha;
    public string Senha{
	get{
	return senha;

	}
	}
    private int codEspecialidade;
    public int CodEspecialidade{
	get{
	return codEspecialidade;

	}
	}
    private string foto;
    public string Foto{
	get{
	return foto;

	}
	}
    public Medico(int c, string n, int e)
    {
        if (c <= 0)
            throw new Exception("Código de Médico inválido!");
        if(n==null || n=="")
            throw new Exception("Nome de Médico inválido!");
        if(e<=0)
            throw new Exception("Código de Especialidade de Médico inválido!");
        this.crm = c;
        this.nome = n;
        this.codEspecialidade = e;
    }
    public Medico(int c, string n) {
        if (c <= 0)
            throw new Exception("Código de Médico inválido!");
        if (n == null || n == "")
            throw new Exception("Nome de Médico inválido!");
        this.codEspecialidade = c;
        this.nome = n;
    }
}