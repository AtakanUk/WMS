using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    static SqlConnection sqlcon = new SqlConnection(@"Data Source =COBBRRA\SQLEXPRESS;Initial Catalog=WarehouseDB;Integrated Security=true");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool bLogged = false;
        using (var dbcontext = new WarehouseDBEntities1())
        {
            var loginCheck = dbcontext.Login.Where(x => x.UserName == txtUserName.Text && x.Password == txtPassword.Text).ToList().FirstOrDefault();
            if (loginCheck != null)
            {
                bLogged = true;
                Session["user"] = txtUserName.Text.Trim();
            }
            else
            {
                bLogged = false;
                lblerror.Text = "Login Failed. Incorrect Username or Password!";
            }
        }

        // Authenticate the user against the database
        if (bLogged)
        {
            // Create a new FormsAuthenticationTicket object
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, // version number
                txtUserName.Text, // user name
                DateTime.Now, // issue date
                DateTime.Now.AddMinutes(30), // expiration date
                true, // persistent cookie
                "" // user data (optional)
            );

            // Encrypt the ticket and store it in a cookie
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)
            );
            Response.Cookies.Add(authCookie);

            // Redirect the user to the requested page, or to the default page
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(txtUserName.Text, false);
                Response.Redirect("Home.aspx");
            }
        }
        else
        {
            lblerror.Text = "Invalid username or password.";
        }

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
    }
}