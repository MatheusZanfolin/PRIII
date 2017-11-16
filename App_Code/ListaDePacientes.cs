using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ListaDePacientes
/// </summary>
public class ListaDePacientes
{
    private Queue<Paciente> filaDePacientes;

    public ListaDePacientes()
    {
        filaDePacientes = new Queue<Paciente>();
    }

    public void Adicionar(Paciente p)
    {
        if (p == null)
            throw new Exception("ListaDePacientes: inclusão de paciente nulo.");

        filaDePacientes.Enqueue(p);
    }

    public Paciente ProximoPaciente()
    {
        return filaDePacientes.Dequeue();
    }

    public bool HaPaciente
    {
        get { return filaDePacientes.Count > 0; }
    }
}