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

public partial class PickPage : System.Web.UI.Page
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
            FillGridView();
        }
        
    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Orders
                    .Select(o => new { o.OrderId, o.OrderStatus })
                    .ToList();
            var distinctOrderTypes = items.GroupBy(o => o.OrderId)
                                      .Select(g => g.First())
                                      .ToList();
            orderGrid.DataSource = distinctOrderTypes;
            orderGrid.DataBind();
        }
    }

    protected void orderGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (e.CommandName == "GetOrderID")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);

                var checkList = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
                foreach (var item in checkList)
                {
                    var inventoryCheck = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                    if (inventoryCheck.ProductCount >= item.OrderProductAmount)
                    {
                        item.OrderStatus = true;
                        dbContext.Orders.AddOrUpdate(item);
                        dbContext.SaveChanges();
                        FillGridView();
                    }
                    else
                    {
                        string errorMessage = "Insufficient stock amount. Check stocks please.";
                        Response.Write("<script>alert('" + errorMessage + "');</script>");
                        FillGridView();
                        return;
                    }
                }
            }
            else if (e.CommandName == "CheckOrder")
            {
                FillGridView();
                int orderId = Convert.ToInt32(e.CommandArgument);
                getOrder(orderId);
            }
            else if(e.CommandName == "DeleteProduct")
            {
                FillGridView();
                int orderId = Convert.ToInt32(e.CommandArgument);
                var itemForDelete = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
                foreach (var item in itemForDelete)
                {
                    dbContext.Orders.Remove(item);
                    dbContext.SaveChanges();
                    FillGridView();
                }
            }
        }
    }

    void getOrder(int orderId)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            List<OrderProductType> orderList = new List<OrderProductType>();
            var items = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
            foreach (var item in items)
            {
                OrderProductType orderToAdd = new OrderProductType();
                var product = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                if(product == null)
                {
                    return;
                }
                orderToAdd.ProductName = string.IsNullOrEmpty(product.ProductName) ? string.Empty : product.ProductName;
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
        FillGridView();
        var dataSource = orderGrid.DataSource as List<Orders>;
        IEnumerable<Orders> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        orderGrid.DataSource = table;
        orderGrid.DataBind();
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

    protected void productGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {
           
        }

    }
}