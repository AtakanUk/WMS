using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Entity.Migrations;
using System.Runtime.Remoting.Contexts;
using FastMember;

public partial class Item : System.Web.UI.Page
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
        this.clear();
    }

    protected void productGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {
            if (e.CommandName == "DeleteProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                var itemForDelete = dbcontext.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                dbcontext.Products.Remove(itemForDelete);
                dbcontext.SaveChanges();
                FillGridView();
            }
        }

    }
    public void clear()
    {
        hfProductId.Value = string.Empty;
        txtproname.Text = string.Empty;
        lblerrormessage.Text = lblsuccessmassage.Text = string.Empty;
        txtprodes.Text = string.Empty;
        txtproid.Text = string.Empty;
        txtproorigin.Text = string.Empty;
        txtprosize.Text = string.Empty;
        txtproweight.Text = string.Empty;
        btnsave.Text = "Save";

    }

    protected void searchForExistingItem(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtproid.Text.Trim()))
        {
            using (var db = new WarehouseDBEntities1())
            {
                bool check = int.TryParse(txtproid.Text.Trim(), out int productId);
                if (check)
                {
                    var product = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();

                    if (product != null)
                    {
                        txtproid.Text = product.ProductId.ToString();
                        txtproname.Text = product.ProductName;
                        txtprodes.Text = product.ProductDescription;
                        txtproorigin.Text = product.ProductOrigin;
                        txtprosize.Text = product.ProductSize;
                        txtproweight.Text = product.ProductWeight;
                    }
                }
            }
        }
        else
        {
            clear();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            int productId = int.Parse(txtproid.Text.Trim());
            var product = dbContext.Products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (product == null)
            {
                var item = new Products
                {
                    ProductId = productId,
                    ProductName = txtproname.Text,
                    ProductDescription = txtprodes.Text,
                    ProductCount = 0,
                    ProductOrigin = txtproorigin.Text,
                    ProductSize = txtprosize.Text,
                    ProductWeight = txtproweight.Text
                };
                dbContext.Products.AddOrUpdate(item);
            }
            else
            {
                product.ProductName = txtproname.Text.Trim();
                product.ProductDescription = txtprodes.Text.Trim();
                product.ProductOrigin = txtproorigin.Text.Trim();
                product.ProductSize = txtprosize.Text.Trim();
                product.ProductWeight = txtproweight.Text.Trim();
                dbContext.Products.AddOrUpdate(product);
            }

            dbContext.SaveChanges();
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
            txtprodes.Text = items.ProductDescription.ToString();
            btnsave.Text = "Update";
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (!string.IsNullOrEmpty(txtproid.Text))
            {
                var check = int.TryParse(txtproid.Text,out int id);
                if(check)
                {
                    var productToDelete = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
                    if (productToDelete != null)
                    {
                        dbContext.Products.Remove(productToDelete);
                        dbContext.SaveChanges();
                    }
                    clear();
                    FillGridView();
                    lblsuccessmassage.Text = "Deleted Successfully";
                }
            }
        }
    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        FillGridView();
        var dataSource = productGrid.DataSource as List<Products>;
        IEnumerable<Products> data = dataSource;
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