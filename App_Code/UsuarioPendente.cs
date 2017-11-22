using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Usuario
/// </summary>
public class UsuarioPendente
{
    public UsuarioPendente(string endereco, DateTime dataNascimento, string email, string celular, string telefone, string foto)
    {
        if (string.IsNullOrEmpty(endereco))
            throw new Exception("UsuarioPendente: endereço nulo.");

        if (dataNascimento == null)
            throw new Exception("UsuarioPendente: data de nascimento nula.");

        if (string.IsNullOrEmpty(email))
            throw new Exception("UsuarioPendente: endereço nulo.");

        if (string.IsNullOrEmpty(celular))
            throw new Exception("UsuarioPendente: celular nulo.");

        if (string.IsNullOrEmpty(telefone))
            throw new Exception("UsuarioPendente: telefone.");

        if (string.IsNullOrEmpty(foto))
            throw new Exception("UsuarioPendente: telefone.");

        Endereco       = endereco;
        DataNascimento = dataNascimento;
        Email          = email;
        Celular        = celular;
        Telefone       = telefone;
        Foto           = foto;
    }   

    public UsuarioPendente(string endereco, DateTime dataNascimento, string email, string telefone, string foto)
    {
        if (string.IsNullOrEmpty(endereco))
            throw new Exception("UsuarioPendente: endereço nulo.");

        if (dataNascimento == null)
            throw new Exception("UsuarioPendente: data de nascimento nula.");

        if (string.IsNullOrEmpty(email))
            throw new Exception("UsuarioPendente: endereço nulo.");

        if (string.IsNullOrEmpty(telefone))
            throw new Exception("UsuarioPendente: telefone.");

        Endereco       = endereco;
        DataNascimento = dataNascimento;
        Email          = email;
        Celular        = null;
        Telefone       = telefone;
        Foto           = foto;
    }

    public string Endereco
    {
        get;

        private set;
    }

    public DateTime DataNascimento
    {
        get;

        private set;
    }

    public string Email
    {
        get;

        private set;
    }

    public string Celular
    {
        get;

        private set;
    }

    public string Telefone
    {
        get;

        private set;
    }

    public string Foto
    {
        get;

        private set;
    }
}