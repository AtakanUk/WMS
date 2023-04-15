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
        FillGridView();
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
                lblerrormessage.Text = "Lütfen müşteri ismi giriniz.";
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
        var dataSource = productGrid.DataSource as List<Customer>;
        IEnumerable<Customer> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        // Sort the data based on the selected column and direction
        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        productGrid.DataSource = table;
        productGrid.DataBind();
    }

    private string GetSortDirection(string column)
    {
        // By default, sort the data in ascending order
        string direction = "ASC";

        // If the data is already sorted by the selected column in ascending order,
        // change the sort direction to descending order
        if (ViewState["SortExpression"] != null && ViewState["SortExpression"].ToString() == column)
        {
            if (ViewState["SortDirection"] != null && ViewState["SortDirection"].ToString() == "ASC")
            {
                direction = "DESC";
            }
        }

        // Store the selected sort expression and direction in ViewState
        ViewState["SortExpression"] = column;
        ViewState["SortDirection"] = direction;

        return direction;
    }
}