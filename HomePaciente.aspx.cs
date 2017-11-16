using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePaciente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["paciente"].ToString()))
                    Response.Redirect("LogonPac.aspx");
            }
            catch
            {
                Response.Redirect("LogonPac.aspx");
            }
        }
    }
}