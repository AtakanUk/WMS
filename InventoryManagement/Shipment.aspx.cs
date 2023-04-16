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
using System.Configuration;
using NUnit.Framework.Interfaces;

public partial class Shipment : System.Web.UI.Page
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

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Orders
                    .Select(o => new { o.OrderId, o.OrderStatus, o.CarrierId, o.CarrierName}).Where(x=>x.OrderStatus == true)
                    .ToList();
            var distinctOrderTypes = items.GroupBy(o => o.OrderId)
                                      .Select(g => g.First())
                                      .ToList();
            orderGrid.DataSource = distinctOrderTypes;
            orderGrid.DataBind();
        }
    }

    protected void orderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlItems = (DropDownList)e.Row.FindControl("ddlItems");
            PopulateItems(ddlItems);
        }
    }

    //protected void orderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddlItems = (DropDownList)e.Row.FindControl("ddlItems");
    //        int orderId = Convert.ToInt32(orderGrid.DataKeys[e.Row.RowIndex].Value);

    //        using (var dbContext = new WarehouseDBEntities1())
    //        {
    //            var items = from i in dbContext.Carrier
    //                        select new { i.CarrierName};

    //            ddlItems.DataSource = items;
    //            ddlItems.DataBind();

    //            var order = dbContext.Orders.SingleOrDefault(o => o.OrderId == orderId);
    //            if (order != null && order.CarrierId.HasValue)
    //            {
    //                ddlItems.SelectedValue = order.CarrierId.ToString();
    //            }
    //        }
    //    }
    //}

    protected void orderGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            if (e.CommandName == "GetOrderID")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = orderGrid.Rows[index];
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DropDownList ddlItems = (DropDownList)row.FindControl("ddlItems");

                string selectedValue = ((DropDownList)row.FindControl("ddlItems")).SelectedValue;

                int orderId = Convert.ToInt32(orderGrid.DataKeys[rowIndex].Value);

                var carrier = dbContext.Carrier.Where(x => x.CarrierName == selectedValue).FirstOrDefault();

                var checkList = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
                foreach (var item in checkList)
                {
                    var inventoryCheck = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                    if (inventoryCheck.ProductCount >= item.OrderProductAmount)
                    {
                        var order = dbContext.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
                        order.CarrierName = carrier.CarrierName;
                        order.CarrierId = carrier.CarrierId;
                        dbContext.Orders.AddOrUpdate(order);
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

    protected void PopulateItems(DropDownList ddlItems)
    {
        using (var dbcontext = new WarehouseDBEntities1())
        {

            //var carrierList = from carriers in dbcontext.Carrier
            //             select carriers.CarrierName;

            var carrierList = dbcontext.Carrier.ToList();
            if (carrierList != null)
            {
                ddlItems.DataSource = carrierList;
                ddlItems.DataBind();
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