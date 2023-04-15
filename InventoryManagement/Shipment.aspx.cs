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

public partial class Shipment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        FillGridView();
    }

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Orders
                    .Select(o => new { o.OrderId, o.OrderStatus }).Where(x=>x.OrderStatus == true)
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
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int orderId = Convert.ToInt32(orderGrid.DataKeys[rowIndex].Value);

                var checkList = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
                foreach (var item in checkList)
                {
                    var inventoryCheck = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                    if (inventoryCheck.ProductCount >= item.OrderProductAmount)
                    {
                        inventoryCheck.ProductCount = inventoryCheck.ProductCount - (int)item.OrderProductAmount;
                        dbContext.Products.AddOrUpdate(inventoryCheck);
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
        }
    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        var dataSource = orderGrid.DataSource as List<Orders>;
        IEnumerable<Orders> data = dataSource;
        DataTable table = new DataTable();
        using (var reader = ObjectReader.Create(data))
        {
            table.Load(reader);
        }

        // Sort the data based on the selected column and direction
        table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        orderGrid.DataSource = table;
        orderGrid.DataBind();
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