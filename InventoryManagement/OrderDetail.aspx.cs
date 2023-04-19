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
        public string CarrierName { get; set; }
        public int? CarrierId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string IsShipped { get; set; }
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
                // Replace this with your database code to retrieve the order IDs
                var items = dbcontext.Orders
                    .Select(o => new { o.OrderId, o.OrderStatus })
                    .ToList();
                var distinctOrderTypes = items.GroupBy(o => o.OrderId)
                                          .Select(g => g.First())
                                          .ToList();

                // Create a DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("OrderId", typeof(int));

                // Add each item from the list as a new row in the DataTable
                foreach (var order in distinctOrderTypes)
                {
                    DataRow row = dt.NewRow();
                    row["OrderId"] = order.OrderId;
                    dt.Rows.Add(row);
                }


                // Bind the DataTable to the DropDownList
                ddlorderid.DataSource = distinctOrderTypes;
                ddlorderid.DataTextField = "OrderID"; // Replace with your actual column name
                ddlorderid.DataValueField = "OrderID"; // Replace with your actual column name
                ddlorderid.DataBind();

                // Add a default option
                ddlorderid.Items.Insert(0, new ListItem("Select an order ID", ""));
            }




            searchForOrder(sender, e);
        }

    }

    protected void searchForOrder(object sender, EventArgs e)
    {
        var check = int.TryParse(ddlorderid.SelectedValue, out int orderId);
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
            var items = dbContext.Orders.Where(x => x.OrderId == orderId).ToList();
            foreach (var item in items)
            {
                OrderProductType orderToAdd = new OrderProductType();
                var product = dbContext.Products.Where(x => x.ProductId == item.OrderProductId).FirstOrDefault();
                var customer = dbContext.Customer.Where(x => x.CustomerId == item.CustomerId).FirstOrDefault();
                if(customer == null)
                {
                    customer = new Customer();
                }
                if (product == null)
                {
                    return;
                }
                orderToAdd.ProductName = string.IsNullOrEmpty(product.ProductName) ? string.Empty : product.ProductName;
                orderToAdd.ProductId = product.ProductId;
                orderToAdd.OrderStatus = item.OrderStatus;
                orderToAdd.OrderProductAmount = item.OrderProductAmount;
                orderToAdd.CarrierName = string.IsNullOrEmpty(item.CarrierName) ? string.Empty : item.CarrierName;
                orderToAdd.CarrierId = item.CarrierId;
                orderToAdd.CustomerName = string.IsNullOrEmpty(customer.CustomerName) ? string.Empty : customer.CustomerName;
                orderToAdd.CustomerId = customer.CustomerId;
                orderToAdd.IsShipped = (item.CarrierId != null) ? "Sent" : "Not Sent";
                orderList.Add(orderToAdd);
            }
            productGrid.DataSource = orderList;
            productGrid.DataBind();
        }

    }

    protected void productGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        var check = int.TryParse(ddlorderid.SelectedValue, out int orderId);
        if (check)
        {
            FillGridView(orderId);
            var dataSource = productGrid.DataSource as List<OrderProductType>;
            IEnumerable<OrderProductType> data = dataSource;
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data))
            {
                table.Load(reader);
            }

            table.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            productGrid.DataSource = table;
            productGrid.DataBind();
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