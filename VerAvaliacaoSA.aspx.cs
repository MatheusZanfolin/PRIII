using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class VerAvaliacaoSA : System.Web.UI.Page
{
    private SqlDataReader rdr = null;
    private static List<int> codEspecialidades = new List<int>();//lista com os códigos das especialidades
    private static List<string> usuarios = new List<string> ();//lista com os nomes de usuários dos pacientes
    private static List<int> codMedicos = new List<int> ();//lista com os crm's 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try {
                if (Session["usuario"] == null)
                    Response.Redirect("logonSA.aspx");
            }
            catch
            {
                Response.Redirect("logonSA.aspx");
            }
           
        }
    }

    protected void lsbOpcao_SelectedIndexChanged(object sender, EventArgs e)
    {//quando o secretário selecionar uma das opções do listbox
      try
      {
        lblErro.Text = "";

        if (Conexao.conexao.State!=System.Data.ConnectionState.Open)
            Conexao.conexao.Open();
            switch (lsbOpcao.SelectedIndex)
            {
                case 0:
                    popularTodasAvaliacoes();
                    break;
                case 1:
                    //stored procedure de selecionar especialidade                    

                  popularLista("Especialidade");
                    break;
                case 2:
                    //stored procedure de selecionar medico
                   popularLista("Medico");
                    break;
                case 3:
                    //stored procedure de selecionar paciemte
                  popularLista("Paciente");
                    break;
            }
            rdr.Close();

            Conexao.conexao.Close();
            rdr = null;
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
    private void popularLista(string opcao){
        string query = "";
          SqlCommand cmd = null;
       switch (opcao){
        case "Especialidade":
            query = "select * from Especialidades_view";
        break;
        case "Medico":
            query = "select * from selectMed_view";
        break;
        case "Paciente":
            query = "select * from selectPac_view";
        break;
       }
                    cmd = new SqlCommand(query, Conexao.conexao);
                    cmd.CommandType = System.Data.CommandType.Text;
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {//populando a lista de códigos de especialidades
                            switch(opcao){
                                case "Especialidade":
                                    codEspecialidades.Add(Convert.ToInt32(rdr.GetValue(0)));
                                    lsbEspecialidade.Items.Add(rdr.GetValue(1).ToString());
                                break;
                                case "Medico":
                                    codMedicos.Add(Convert.ToInt32(rdr.GetValue(0)));
                                    lsbMedico.Items.Add(rdr.GetValue(1).ToString());
                                break;
                                case "Paciente":
                                    usuarios.Add(rdr.GetValue(0).ToString());
                                    lsbPaciente.Items.Add(rdr.GetValue(1).ToString());
                        break;
                            }
                        }//colocando no listbox os nomes das especialidades
                    }
                    else
                    {
                        switch(opcao){
                        case "Especialidade":
                            lblErro.Text = "Não há nenhuma especialidade cadastrada!";
                        break;
                        case "Medico":
                            lblErro.Text = "Não há nenhum médico cadastrado!";
                        break;
                        case "Paciente":
                            lblErro.Text = "Não há nenhum paciente cadastrado!";
                    break;
                        }
                    }
                    lsbMedico.Visible = lsbEspecialidade.Visible = lsbPaciente.Visible = false;
                    switch(opcao){
                    case "Especialidade":
                    lsbEspecialidade.Enabled = lsbEspecialidade.Visible=true;
                
                    break;
                    case "Medico":
                    lsbMedico.Enabled = lsbMedico.Visible=true;
                    break;
                    case "Paciente":
                    lsbPaciente.Enabled = lsbPaciente.Visible= true;
                break;
                }
    }
    private void popularTodasAvaliacoes()
    {
        lblErro.Text = "";
        //stored procedure de selecionar todas avaliações
        string query = "select * from todasAvaliacoes_view order by dataHoraConsulta desc";
       SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
        cmd.CommandType = System.Data.CommandType.Text;
        rdr = cmd.ExecuteReader();
        if (rdr.HasRows)
        {
            int somatoriaNotas = 0;
            int qtdeNotas = 1;
            while (rdr.Read())
            {
                tabDados.Rows.Add(new TableRow());
                if (qtdeNotas == 1)
                {
                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[0].Text = "Data da Consulta";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[1].Text = "Nome do Médico";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[2].Text = "Especialidade";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[3].Text = "Nome do Paciente";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[4].Text = "Avaliação";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[5].Text = "Descrição";
                    tabDados.Rows.Add(new TableRow());
                }
                for (int i = 0; i < 6; i++)
                {//populando a tabela do formulário
                    tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                    tabDados.Rows[qtdeNotas].Cells[i].Text = rdr.GetValue(i).ToString();
                    //pegando os valores do BD
                }
                somatoriaNotas += Convert.ToInt32(tabDados.Rows[qtdeNotas].Cells[4].Text);
                qtdeNotas++;

            }
             
            tabDados.Rows.Add(new TableRow());
            tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
            tabDados.Rows[qtdeNotas].Cells[0].Text = qtdeNotas-1 + " Avaliações !";
            //Calculando nota na avaliação pelo usuário média
            tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
            tabDados.Rows[qtdeNotas].Cells[1].Text = "MÉDIA :";
            tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
            tabDados.Rows[qtdeNotas].Cells[2].Text = somatoriaNotas / (qtdeNotas-1) + "";
            tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
            tabDados.Rows[qtdeNotas].Cells[3].Text = "       ";
            /*tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
            tabDados.Rows[qtdeNotas].Cells[0].Text = "       ";*/
        }
        else
        {
            lblErro.Text = "Ainda não há nenhuma avaliação cadastrada!";
        }
    }

    protected void lsbEspecialidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
         {
        lblErro.Text = "";
        if (Conexao.conexao.State!=System.Data.ConnectionState.Open)
            Conexao.conexao.Open();

            int codEspecSel = lsbEspecialidade.SelectedIndex;
            codEspecSel = codEspecialidades[codEspecSel];
            string query = "";
            //stored procedure para selecionar as avaliações de determinada especialidade
            query = "select * from avaliacoesEspecialidade_view where codEspecialidade = @codEspecialidade order by dataHoraConsulta desc";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@codEspecialidade", codEspecSel));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                int somatoriaNotas = 0;
                int qtdeNotas = 1;
            while (rdr.Read())
            {
                tabDados.Rows.Add(new TableRow());
                if (qtdeNotas == 1) {
                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[0].Text = "Data da Consulta";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[1].Text = "Nome do Médico";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[2].Text = "Especialidade";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[3].Text = "Nome do Paciente";

                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[4].Text = "Avaliação ";
                    tabDados.Rows[0].Cells.Add(new TableCell());
                    tabDados.Rows[0].Cells[5].Text = "Descrição";
                    tabDados.Rows.Add(new TableRow());
                }
                for (int i = 0; i < 6; i++)
                    {// populando a tabela do formulário
                        tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                        tabDados.Rows[qtdeNotas].Cells[i].Text = rdr.GetValue(i).ToString();
                        //pegando os valores do BD
                    }
                    somatoriaNotas += Convert.ToInt32(tabDados.Rows[qtdeNotas].Cells[4].Text);
                    qtdeNotas++;

                }
                 
                tabDados.Rows.Add(new TableRow());
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[0].Text = qtdeNotas -1 + " Avaliações !";
                //Calculando nota na avaliação pelo usuário média
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[1].Text ="MÉDIA :";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[2].Text = somatoriaNotas / (qtdeNotas-1) + "";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[3].Text = "       ";
                /*tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[0].Text = "       ";*/
            }
            else
            {
                lblErro.Text = "Não há nenhuma avaliação com estas características cadastrada!";
            }
            rdr.Close();
            Conexao.conexao.Close();

            rdr = null;
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbMedico_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErro.Text = "";
        try
        {
            if(Conexao.conexao.State!=System.Data.ConnectionState.Open)
            Conexao.conexao.Open();

            int crm = lsbMedico.SelectedIndex;
            crm = codMedicos[crm];
            string query = "";
            //stored procedure para selecionar as avaliações de determinada especialidade
            query = "select * from avaliacoesMedico_view where crm=@crm order by dataHoraConsulta desc";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@crm", crm));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                int somatoriaNotas = 0;
                int qtdeNotas = 1;
                while (rdr.Read())
                {
                    tabDados.Rows.Add(new TableRow());
                    if (qtdeNotas == 1)
                    {
                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[0].Text = "Data da Consulta";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[1].Text = "Nome do Médico";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[2].Text = "Especialidade";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[3].Text = "Nome do Paciente";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[4].Text = "Avaliação ";
                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[5].Text = "Descrição";
                        tabDados.Rows.Add(new TableRow());
                    }
                    for (int i = 0; i < 6; i++)
                    {// populando a tabela do formulário

                        tabDados.Rows[qtdeNotas ].Cells.Add(new TableCell());
                        tabDados.Rows[qtdeNotas].Cells[i].Text = rdr.GetValue(i).ToString();
                        //pegando os valores do BD
                    }
                    somatoriaNotas += Convert.ToInt32(tabDados.Rows[qtdeNotas].Cells[4].Text);
                    qtdeNotas++;

                }
                 
                tabDados.Rows.Add(new TableRow());
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[0].Text = qtdeNotas-1 + " Avaliações !";
                //Calculando nota na avaliação pelo usuário média
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[1].Text ="MÉDIA :";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[2].Text = somatoriaNotas /( qtdeNotas -1)+ "";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[3].Text = "       ";
                //tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                //tabDados.Rows[qtdeNotas].Cells[0].Text = "       ";
            }
            else
            {
                lblErro.Text = "Não há nenhuma avaliação com estas características cadastrada!";
            }
            rdr.Close();
            Conexao.conexao.Close();
            rdr = null;
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }

    protected void lsbPaciente_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErro.Text = "";
        try
        {
            if(Conexao.conexao.State!=System.Data.ConnectionState.Open)
            Conexao.conexao.Open();

            int posicao = lsbPaciente.SelectedIndex;
            string usuario = usuarios[posicao];
            string query = "";
            //stored procedure para selecionar as avaliações de determinada especialidade
            query = "select * from avaliacoesPaciente_view where usuario=@usuario order by dataHoraConsulta desc";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                int somatoriaNotas = 0;
                int qtdeNotas = 1;
                while (rdr.Read())
                {
                    tabDados.Rows.Add(new TableRow());
                    if (qtdeNotas == 1)
                    {
                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[0].Text = "Data da Consulta";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[1].Text = "Nome do Médico";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[2].Text = "Especialidade";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[3].Text = "Nome do Paciente";

                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[4].Text = "Avaliação ";
                        tabDados.Rows[0].Cells.Add(new TableCell());
                        tabDados.Rows[0].Cells[5].Text = "Descrição";
                        tabDados.Rows.Add(new TableRow());
                    }
                    for (int i = 0; i < 6; i++)
                    {// populando a tabela do formulário

                        tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                        tabDados.Rows[qtdeNotas ].Cells[i].Text = rdr.GetValue(i).ToString();
                        //pegando os valores do BD
                    }
                    somatoriaNotas += Convert.ToInt32(tabDados.Rows[qtdeNotas].Cells[4].Text);
                    qtdeNotas++;

                }
                 
                tabDados.Rows.Add(new TableRow());
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[0].Text = qtdeNotas -1 + " Avaliações !";
                //Calculando nota na avaliação pelo usuário média
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[1].Text ="MÉDIA :";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[2].Text = somatoriaNotas / (qtdeNotas -1)+ "";
                tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[3].Text = "       ";
                /*tabDados.Rows[qtdeNotas].Cells.Add(new TableCell());
                tabDados.Rows[qtdeNotas].Cells[0].Text = "       ";*/
            }
            else
            {
                lblErro.Text = "Não há nenhuma avaliação com estas características cadastrada!";
            }
            rdr.Close();
            Conexao.conexao.Close();
            rdr = null;
        }
        catch
        {
            lblErro.Text = "Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}