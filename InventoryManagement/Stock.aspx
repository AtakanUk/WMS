<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Stock" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style41 {
            text-align: center;
        }

        .auto-style42 {
            width: 100%;
        }

        .auto-style43 {
            text-align: right;
            width: 608px;
        }

        .auto-style44 {
            text-align: left;
        }

        .auto-style46 {
            text-align: right;
            width: 571px;
        }

        .auto-style49 {
            text-align: center;
            width: 8px;
        }

        .auto-style50 {
            text-align: center;
            width: 73px;
        }

        .auto-style51 {
            text-align: center;
        }

        .auto-style52 {
            color: #003399;
        }

        .table-borderless {
            border-collapse: separate;
            border-spacing: 5px 5px;
        }

        .table {
            border-collapse: collapse;
            width: 100%;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-left: auto; margin-right: auto; width: 600px;">
        <h3 class="auto-style52">Item List</h3>
        <asp:HiddenField ID="hfProductId" runat="server" />
        <div class="auto-style51">
            <asp:GridView ID="productGrid" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting">
                <Columns>
                    <asp:BoundField DataField="ProductId" HeaderText="Item Id" SortExpression="ProductId" />
                    <asp:BoundField DataField="ProductName" HeaderText="Item Name" SortExpression="ProductName" />
                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" SortExpression="ProductDescription" />
                    <asp:BoundField DataField="ProductCount" HeaderText="Item Count" SortExpression="ProductCount" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>

