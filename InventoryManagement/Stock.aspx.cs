﻿using System;
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

public partial class Stock : System.Web.UI.Page
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

    void FillGridView()
    {
        using (var dbContext = new WarehouseDBEntities1())
        {
            var items = dbContext.Products.ToList();
            productGrid.DataSource = items;
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