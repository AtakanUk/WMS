using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    //public bool isloggedin { get; set; }
    //public string username { get { return lblusername.innertext; } set { lblusername.innertext = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            lblusername.Text = Session["user"].ToString();
        }
        else
        {
            btnLogOut.Visible = false;
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        // Clear the authentication cookie
        FormsAuthentication.SignOut();

        // Clear session variables
        Session.Abandon();

        // Redirect to the login page
        Response.Redirect("Login.aspx");
    }
}