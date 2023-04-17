using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
            FillGridView();
        }
        
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        txtaddress.Text = string.Empty;
        txtcustomername.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtphone.Text = string.Empty;
        lblerrormessage.Text = lblsuccessmassage.Text = "";
        btnsave.Text = "Save";

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (string.IsNullOrEmpty(txtcustomername.Text))
            {
                lblerrormessage.Text = "Please enter a valid Customer Name.";
                FillGridView();
                return;
            }
            var customer = new Customer
            {
                Address = string.IsNullOrEmpty(txtaddress.Text) ? string.Empty : txtaddress.Text,
                CustomerName = string.IsNullOrEmpty(txtcustomername.Text) ? string.Empty : txtcustomername.Text,
                Email = string.IsNullOrEmpty(txtemail.Text) ? string.Empty : txtemail.Text,
                Phone = string.IsNullOrEmpty(txtphone.Text) ? string.Empty : txtphone.Text
            };

            dbContext.Customer.AddOrUpdate(customer);
            dbContext.SaveChanges();


            FillGridView();
        }
    }
    protected void productGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {
            Button btn = (Button)sender;
            string customerId = btn.CommandArgument;
            var check = int.TryParse(customerId, out int customerNumber);
            if(check)
            {
               var itemToRemove = dbcontext.Customer.Where(x => x.CustomerId == customerNumber).FirstOrDefault();
                dbcontext.Customer.Remove(itemToRemove);
                dbcontext.SaveChanges();
                FillGridView();
            }

        }

    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Customer.ToList();
            productGrid.DataSource = items;
            productGrid.DataBind();
        }
    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        FillGridView();
        var dataSource = productGrid.DataSource as List<Customer>;
        IEnumerable<Customer> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        productGrid.DataSource = table;
        productGrid.DataBind();
    }

    private string GetSortDirection(string column)
    {
        string direction = "ASC";

        if (ViewState["SortExpression"] != null && ViewState["SortExpression"].ToString() == column)
        {
            if (ViewState["SortDirection"] != null && ViewState["SortDirection"].ToString() == "ASC")
            {
                direction = "DESC";
            }
        }

        ViewState["SortExpression"] = column;
        ViewState["SortDirection"] = direction;

        return direction;
    }
}