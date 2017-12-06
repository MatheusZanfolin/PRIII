using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["usuario"].ToString()))
                    Response.Redirect("logonSA.aspx");
            }
            catch
            {
                Response.Redirect("logonSA.aspx");
            }
        }
    }

    protected void MenuSA_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

    protected void MenuSA_MenuItemClick1(object sender, MenuEventArgs e)
    {
        if (e.Item.Text.ToUpper() == "LOGOUT")
            Request.QueryString["logout"] = "true";
    }
}