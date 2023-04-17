using System;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if (userName == "" || password == "")
            {
                Response.Write("<script>alert('Please enter an user name and password.');</script>");
                return;
            }

            var checkForUser = dbcontext.UserInformations.Where(x => x.UserName == userName).FirstOrDefault();
            if (checkForUser != null)
            {
                Response.Write("<script>alert('This username is already registered.');</script>");
                return;
            }
            var registerUser = new UserInformations()
            {
                UserName = userName,
                Password = password
            };
            dbcontext.UserInformations.AddOrUpdate(registerUser);
            dbcontext.SaveChanges();
            Response.Write("<script>alert('Successfully registered.');</script>");
            Response.Redirect("~/Login.aspx");
            return;
        }


    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
    }
}