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

public partial class Outbound : System.Web.UI.Page
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

        Dictionary<int, int> basket = Session["Basket"] as Dictionary<int, int>;
        if (basket == null)
        {
            basket = new Dictionary<int, int>();
        }
        DataTable basketTable = new DataTable();
        basketTable.Columns.Add("ProductId", typeof(string));
        basketTable.Columns.Add("ProductCount", typeof(int));
        foreach (var item in basket)
        {
            basketTable.Rows.Add(item.Key, item.Value);
        }

        basketGrid.DataSource = basketTable;
        basketGrid.DataBind();
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.clear();
    }

    public void clear()
    {
        Session.Remove("Basket");
        Dictionary<int, int> basket = new Dictionary<int, int>();
        DataTable basketTable = new DataTable();
        basketTable.Columns.Add("ProductId", typeof(string));
        basketTable.Columns.Add("ProductCount", typeof(int));
        foreach (var item in basket)
        {
            basketTable.Rows.Add(item.Key, item.Value);
        }

        basketGrid.DataSource = basketTable;
        basketGrid.DataBind();

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
                    ProductCount = 0,
                };
                dbContext.Products.AddOrUpdate(item);
            }
            else
            {

                dbContext.Products.AddOrUpdate(product);
            }

            dbContext.SaveChanges();
            FillGridView();
        }
    }

    protected void SendList_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            Dictionary<int, int> basket = Session["Basket"] as Dictionary<int, int>;
            if (basket == null)
            {
                basket = new Dictionary<int, int>();
            }
            DataTable basketTable = new DataTable();
            basketTable.Columns.Add("ProductId", typeof(string));
            basketTable.Columns.Add("ProductCount", typeof(int));
            foreach (var item in basket)
            {
                basketTable.Rows.Add(item.Key, item.Value);
            }
            var orderNumber = dbContext.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault();
            int newOrderNumber = 0;
            if (orderNumber != null)
            {
                newOrderNumber = orderNumber.OrderId + 1;
            }
            foreach (DataRow row in basketTable.Rows)
            {

                string productId = row["ProductId"].ToString();
                string productCount = row["ProductCount"].ToString();
                var productIdAsInt = int.Parse(productId);
                var productCountAsInt = int.Parse(productCount);
                var product = dbContext.Products.Where(p => p.ProductId == productIdAsInt).FirstOrDefault();          
                var newOrder = new Orders()
                {
                    OrderId = newOrderNumber,
                    OrderProductAmount = productCountAsInt,
                    OrderProductId = productIdAsInt,
                    OrderStatus = false
                };

                dbContext.Orders.Add(newOrder);
                dbContext.SaveChanges();

            }


        }
    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Products.ToList();
            basketGrid.DataSource = items;
            basketGrid.DataBind();
        }
    }

    protected void AddToShipmentButton_Click(object sender, EventArgs e)
    {
        bool productIdcheck = int.TryParse(txtproid.Text,out int productId);
        bool productCountCheck = int.TryParse(txtitemcount.Text,out int productCount);
        if(productCountCheck && productIdcheck)
        {
            Dictionary<int, int> basket = Session["Basket"] as Dictionary<int, int>;
            if (basket == null)
            {
                basket = new Dictionary<int, int>();
            }


            if (basket.ContainsKey(productId))
            {
                basket[productId] = productCount;
            }
            else
            {
                basket.Add(productId, productCount);
            }

            Session["Basket"] = basket;

            DataTable basketTable = new DataTable();
            basketTable.Columns.Add("ProductId", typeof(string));
            basketTable.Columns.Add("ProductCount", typeof(int));
            foreach (var item in basket)
            {
                basketTable.Rows.Add(item.Key, item.Value);
            }
            basketGrid.DataSource = basketTable;
            basketGrid.DataBind();
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
            btnsave.Text = "Update";
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var id = int.Parse(hfProductId.Value);
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

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        var dataSource = basketGrid.DataSource as List<Products>;
        IEnumerable<Products> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        // Sort the data based on the selected column and direction
        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        basketGrid.DataSource = table;
        basketGrid.DataBind();
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