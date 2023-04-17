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

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Register.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool bLogged = false;
        using (var dbcontext = new WarehouseDBEntities1())
        {
            var loginCheck = dbcontext.UserInformations.Where(x => x.UserName == txtUserName.Text && x.Password == txtPassword.Text).ToList().FirstOrDefault();
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

        if (bLogged)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, 
                txtUserName.Text, 
                DateTime.Now, 
                DateTime.Now.AddMinutes(30), 
                true, 
                "" 
            );

            
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)
            );
            Response.Cookies.Add(authCookie);

            
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