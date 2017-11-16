using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeMedico : System.Web.UI.Page
{
    private const string VAR_ARMAZENAMENTO_CRM  = "crm";

    private Medico medicoLogado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["crm"] == null)
                    Response.Redirect("LogonMedico.aspx");
            }
            catch {
                Response.Redirect("LogonMedico.aspx");
            }

        }
    }

    
}