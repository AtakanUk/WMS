using System;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    static SqlConnection sqlcon = new SqlConnection(@"Data Source =COBBRRA\SQLEXPRESS;Initial Catalog=WarehouseDB;Integrated Security=true");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(@"Data Source =COBBRRA\SQLEXPRESS;Initial Catalog=WarehouseDB;Integrated Security=true"))
        {
            sqlcon.Close();
            sqlcon.Open();
            string checkquery = "Select count(1) from Login where Username='" + txtUserName.Text + "' and Password='" + txtPassword.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(checkquery, sqlcon);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 1)
            {
                //lblerror.Text = "login Successful!";

                Session["user"] = txtUserName.Text.Trim();
                Response.Redirect("Home.aspx");

            }
            else
            {
                lblerror.Text = "Login Failed. Incorrect Username or Password!";
            }
            sqlcon.Close();
            // Perform database operations here
        }

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
    }
}