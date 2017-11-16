using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
public partial class MandarSMSPacSA : System.Web.UI.Page
{
    SqlDataReader rdr = null;
    //List<string> usuarios = new List<string>();
    string celular = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        /* string query = "";
         //stored procedure para selecionar todos os pacientes
         SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
         rdr = cmd.ExecuteReader();
         if (rdr.HasRows)
         {
             while (rdr.Read())
             {

             }
         }
         */


       

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["usuario"].ToString() ))
                    Response.Redirect("logonSA.aspx");
            }
            catch
            {
                Response.Redirect("logonSA.aspx");
            }
        }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        // instancia objeto que permite enviar e-mail
        MailMessage objMail = new MailMessage();
        try
        {

            string mensagem = txtMensagem.Text;
            mensagem = mensagem.Trim();
            if (string.IsNullOrEmpty(mensagem))
            {
                lblErro.Text = "Digite a mensagem!";
                return;
            }
            //limite de tamanho da mensagem?
            string usuario = txtPaciente.Text;
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário!";
                return;
            }
            
            Conexao.conexao.Open();

            string query = "";
            string email = "";
            //stored procedure mandar sms paciente
            query = "select * from MandarSMS_view where usuario=@usuario";
            SqlCommand cmd = new SqlCommand(query, Conexao.conexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));

            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                if (rdr.Read())
                    email = rdr.GetValue(0).ToString();
            }
            else
            {
                lblErro.Text = "Este nome de usuário não está cadastrado!";
            }

            rdr.Close();
            Conexao.conexao.Close();

            rdr = null;
            //enviar SMS
            objMail.From = new MailAddress("lucasdoiryu@hotmail.com");
            objMail.Sender = new MailAddress("lucasdoiryu@hotmail.com");
            objMail.To.Add(email);
            objMail.Subject = txtMensagem.Text;
            SmtpClient cliente = new SmtpClient();
            cliente.Send(objMail);
        }
        catch
        {
            if (rdr != null)
                rdr.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            rdr = null;
            lblErro.Text ="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }
    }
}