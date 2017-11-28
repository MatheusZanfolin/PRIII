using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
public partial class MandarEmailPacSA : System.Web.UI.Page
{
    SqlDataReader rdr = null;
    
    string email = "";

    protected void Page_Load(object sender, EventArgs e)
    {
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
        
       
       /* try
        {*/

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
            // MailMessage mailMessage = new MailMessage ("caratecam@gmail.com", email);
            MailMessage objEmail = new MailMessage();
            objEmail.From =  new MailAddress("caratecam@gmail.com");
            objEmail.Sender = new MailAddress("caratecam@gmail.com");
            objEmail.To.Add(email);
            objEmail.Subject = "Consultório Médico: Área de Serviço ao Paciente";
            objEmail.Body = mensagem;
            

        //mailMessage.Subject = txtMensagem.Text;


        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Send(objEmail);
        //smtpClient.Send(mailMessage);

        /*}
        catch
        {
            if (rdr != null)
                rdr.Close();
            if (Conexao.conexao.State != System.Data.ConnectionState.Closed)
                Conexao.conexao.Close();
            rdr = null;
            lblErro.Text ="Ocorreu um erro inesperado! Estamos trabalhando continuamente para resolver o problema! Por favor, tente novamente mais tarde!";
        }*/

    }

}