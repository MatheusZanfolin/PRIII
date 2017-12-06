using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

public partial class MandarSMSPacSA : System.Web.UI.Page
{
    SqlDataReader rdr = null;
    //List<string> usuarios = new List<string>();
    string celular = "";
    string email = "";

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
        try
        {

            string mensagem = txtMensagem.Text;
            mensagem = mensagem.Trim();
            if (string.IsNullOrEmpty(mensagem))
            {
                lblErro.Text = "Digite a mensagem!";
                return;
            }

            
            string usuario = txtPaciente.Text;
            if (string.IsNullOrEmpty(usuario))
            {
                lblErro.Text = "Digite o usuário!";
                return;
            }
            
            if (Conexao.conexao.State != System.Data.ConnectionState.Open)
                Conexao.conexao.Open();

            string query = "";
            string email = "";
            //stored procedure mandar sms paciente
            query = "select email from Paciente where usuario=@usuario";
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
           try
            {
                MailMessage objMensagem = new MailMessage();
                objMensagem.From = new MailAddress("caratecam@gmail.com");
                objMensagem.To.Add(email);
                objMensagem.Subject = "Sistema Consultório Médico";
                objMensagem.Body = txtMensagem.Text;
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                sc.Port = 25;
                sc.Credentials = new NetworkCredential("caratecam@gmail.com", "lucasdoi108");
                sc.EnableSsl = true;
                sc.Send(objMensagem);
                lblErro.Text= "Email enviado com sucesso!";
                //lblErro.ForeColor = System.Drawing.Color.Green;
            }
            catch 
            {
                lblErro.Text = "O email do usuario, não é valido";
            }



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