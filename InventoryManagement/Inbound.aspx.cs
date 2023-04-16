using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inbound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        FillGridView();
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtproid.Text = string.Empty;
        txtproname.Text = string.Empty;
        Stock.Text = string.Empty;
    }

    protected void searchForExistingItem(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtproid.Text.Trim()))
        {
            using (var db = new WarehouseDBEntities1())
            {
                int productId = int.Parse(txtproid.Text.Trim());
                var product = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();

                if (product != null)
                {
                    txtproid.Text = product.ProductId.ToString();
                    txtproname.Text = product.ProductName;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product is not registered in Inventory. Please register the product first.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product is not registered in Inventory. Please register the product first.')", true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (string.IsNullOrEmpty(txtproid.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product is not registered in Inventory. Please register the product first.')", true);
            }
            else
            {
                int productId = int.Parse(txtproid.Text.Trim());
                var product = dbContext.Products.Where(p => p.ProductId == productId).FirstOrDefault();

                if (product == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product is not registered in Inventory. Please register the product first.')", true);
                }
                else
                {
                    // Update existing product
                    var inbound = int.Parse(Stock.Text.Trim());
                    var updatedStock = product.ProductCount + inbound;
                    product.ProductCount = updatedStock;
                    dbContext.Products.AddOrUpdate(product);
                    dbContext.SaveChanges();
                    txtproid.Text = product.ProductId.ToString();
                    txtproname.Text = product.ProductName;
                }
            }

            FillGridView();
        }
    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Products.ToList();
            productGrid.DataSource = items;
            productGrid.DataBind();
        }
    }

    protected void lnk_onClick(object sender, EventArgs e)
    {

        using (var dbContext = new WarehouseDBEntities1())
        {
            int ProductId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            var items = dbContext.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            hfProductId.Value = items.ProductId.ToString();
            txtproid.Text = items.ProductId.ToString();
            txtproname.Text = items.ProductName.ToString();
            btnsave.Text = "Update";
        }
    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        var dataSource = productGrid.DataSource as List<Products>;
        IEnumerable<Products> data = dataSource;
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