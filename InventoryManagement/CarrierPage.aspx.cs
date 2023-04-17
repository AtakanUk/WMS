using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CarrierPage : System.Web.UI.Page
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
        txtcarrierphone.Text = string.Empty;
        txtcarrieraddress.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtcarrierphone.Text = string.Empty;
        txtcarriername.Text = string.Empty;
        lblerrormessage.Text = lblsuccessmassage.Text = "";
        btnsave.Text = "Save";

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (string.IsNullOrEmpty(txtcarriername.Text))
            {
                lblerrormessage.Text = "Please enter a valid Carrier Name.";
                FillGridView();
                return;
            }
            var carrier = new Carrier
            {
                Address = string.IsNullOrEmpty(txtcarrieraddress.Text) ? string.Empty : txtcarrieraddress.Text,
                CarrierName = string.IsNullOrEmpty(txtcarriername.Text) ? string.Empty : txtcarriername.Text,
                Email = string.IsNullOrEmpty(txtemail.Text) ? string.Empty : txtemail.Text,
                Phone = string.IsNullOrEmpty(txtcarrierphone.Text) ? string.Empty : txtcarrierphone.Text,
                ContactPerson = string.IsNullOrEmpty(txtcontactperson.Text) ? string.Empty : txtcontactperson.Text
            };

            dbContext.Carrier.AddOrUpdate(carrier);
            dbContext.SaveChanges();


            FillGridView();
        }
    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Carrier.ToList();
            productGrid.DataSource = items;
            productGrid.DataBind();
        }
    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        FillGridView();
        var dataSource = productGrid.DataSource as List<Carrier>;
        IEnumerable<Carrier> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        productGrid.DataSource = table;
        productGrid.DataBind();
    }

    protected void productGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {
            if (e.CommandName == "DeleteRow")
            {
                int carrierId = Convert.ToInt32(e.CommandArgument);
                var itemToDelete = dbcontext.Carrier.Find(carrierId);
                dbcontext.Carrier.Remove(itemToDelete);
                dbcontext.SaveChanges();
                FillGridView();
            }
        }

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