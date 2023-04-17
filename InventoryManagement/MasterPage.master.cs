using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            string username = HttpContext.Current.User.Identity.Name;
            lblusername.Text = username;
            
        }
        else
        {
            btnLogOut.Visible = false;
        }

    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Session.Abandon();

        Response.Redirect("Login.aspx");
    }
}