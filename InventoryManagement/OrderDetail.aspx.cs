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

public partial class OrderDetail : System.Web.UI.Page
{
    public class OrderProductType
    {
        public int? OrderProductAmount { get; set; }
        public bool? OrderStatus { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

    }

    protected void searchForOrder(object sender, EventArgs e)
    {
        var check = int.TryParse(txtorderid.Text, out int orderId);
        if (check)
        {
            FillGridView(orderId);
        }

    }


    void FillGridView(int orderId)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            List<OrderProductType> orderList = new List<OrderProductType>();
            var items = dbContext.Orders.Where(x=>x.OrderId == orderId).ToList();
            foreach (var item in items)
            {
                OrderProductType orderToAdd = new OrderProductType();
                var product = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                if (product == null)
                {
                    return;
                }
                orderToAdd.ProductName = product.ProductName;
                orderToAdd.ProductId = product.ProductId;
                orderToAdd.OrderStatus = item.OrderStatus;
                orderToAdd.OrderProductAmount = item.OrderProductAmount;

                orderList.Add(orderToAdd);
            }
            productGrid.DataSource = orderList;
            productGrid.DataBind();
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