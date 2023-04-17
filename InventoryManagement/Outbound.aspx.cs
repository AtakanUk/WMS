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
using static Outbound;
using System.Runtime.Remoting.Messaging;

public partial class Outbound : System.Web.UI.Page
{
    public class OutboundOrder
    {
        public int? OrderProductAmount { get; set; }
        public string CustomerName { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            using (var dbcontext = new WarehouseDBEntities1())
            {
                var query = dbcontext.Products.ToList();
                if (query != null)
                {
                    foreach (var itemId in query)
                    {

                        var itemValue = itemId.ProductId.ToString() + " --- " + itemId.ProductName;
                        ddlItemId.Items.Add(new ListItem(itemValue));
                    }
                }
                
                ddlCustomers.DataSource = dbcontext.Customer.ToList();
                ddlCustomers.DataTextField = "CustomerName";
                ddlCustomers.DataValueField = "CustomerId";
                ddlCustomers.DataBind();
            }

        }

        Dictionary<int, OutboundOrder> basket = Session["Basket"] as Dictionary<int, OutboundOrder>;
        if (basket == null)
        {
            basket = new Dictionary<int, OutboundOrder>();
        }
        DataTable basketTable = new DataTable();
        basketTable.Columns.Add("ProductId", typeof(string));
        basketTable.Columns.Add("OrderProductAmount", typeof(int));
        basketTable.Columns.Add("CustomerName", typeof(string));
        foreach (var item in basket)
        {
            basketTable.Rows.Add(item.Key, item.Value.OrderProductAmount, item.Value.CustomerName);
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
        Dictionary<int, OutboundOrder> basket = new Dictionary<int, OutboundOrder>();
        DataTable basketTable = new DataTable();


        basketTable.Columns.Add("ProductId", typeof(string));
        basketTable.Columns.Add("OrderProductAmount", typeof(int));
        basketTable.Columns.Add("CustomerName", typeof(string));
        foreach (var item in basket)
        {
            basketTable.Rows.Add(item.Key, item.Value.OrderProductAmount, item.Value.CustomerName);
        }
        basketGrid.DataSource = basketTable;
        basketGrid.DataBind();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            string inputString = ddlItemId.SelectedValue;
            string separator = " --- ";
            string result = inputString.Substring(0, inputString.IndexOf(separator)).Trim();
            int productId = int.Parse(result);
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
            Dictionary<int, OutboundOrder> basket = Session["Basket"] as Dictionary<int, OutboundOrder>;
            if (basket == null)
            {
                basket = new Dictionary<int, OutboundOrder>();
            }
            DataTable basketTable = new DataTable();
            basketTable.Columns.Add("ProductId", typeof(string));
            basketTable.Columns.Add("OrderProductAmount", typeof(int));
            basketTable.Columns.Add("CustomerName", typeof(string));
            foreach (var item in basket)
            {
                basketTable.Rows.Add(item.Key, item.Value.OrderProductAmount, item.Value.CustomerName);
            }
            var orderNumber = dbContext.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault();
            int newOrderNumber = 0;
            if (orderNumber != null)
            {
                newOrderNumber = orderNumber.OrderId + 1;
            }
            foreach (DataRow row in basketTable.Rows)
            {
                string inputString = ddlCustomers.SelectedValue;
                var check = int.TryParse(inputString, out int cusNumber);

                string productId = row["ProductId"].ToString();
                string productCount = row["OrderProductAmount"].ToString();
                var productIdAsInt = int.Parse(productId);
                var productCountAsInt = int.Parse(productCount);
                var product = dbContext.Products.Where(p => p.ProductId == productIdAsInt).FirstOrDefault();
                var newOrder = new Orders()
                {
                    OrderId = newOrderNumber,
                    OrderProductAmount = productCountAsInt,
                    OrderProductId = productIdAsInt,
                    OrderStatus = false,
                    CustomerId = cusNumber
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
        string inputString = ddlItemId.SelectedValue;
        string separator = " --- ";
        string result = inputString.Substring(0, inputString.IndexOf(separator)).Trim();

        string resultForCus = ddlCustomers.SelectedItem.Text;






        bool productIdcheck = int.TryParse(result, out int productId);
        bool productCountCheck = int.TryParse(txtitemcount.Text, out int productCount);
        if (productCountCheck && productIdcheck)
        {
            Dictionary<int, OutboundOrder> basket = Session["Basket"] as Dictionary<int, OutboundOrder>;
            if (basket == null)
            {
                basket = new Dictionary<int, OutboundOrder>();
            }


            if (basket.ContainsKey(productId))
            {
                basket[productId] = new OutboundOrder 
                {
                 CustomerName = resultForCus,
                 OrderProductAmount = productCount
                };
            }
            else
            {
                var order = new OutboundOrder
                {

                    CustomerName = resultForCus,
                    OrderProductAmount = productCount
                };
                basket.Add(productId, order);
            }

            Session["Basket"] = basket;

            DataTable basketTable = new DataTable();
            basketTable.Columns.Add("ProductId", typeof(string));
            basketTable.Columns.Add("OrderProductAmount", typeof(int));
            basketTable.Columns.Add("CustomerName", typeof(string));
            foreach (var item in basket)
            {
                basketTable.Rows.Add(item.Key, item.Value.OrderProductAmount,item.Value.CustomerName);
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
            ddlItemId.SelectedValue = items.ProductId.ToString();
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