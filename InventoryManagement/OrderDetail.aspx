<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>



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
            width: 60%;
        }


        .auto-style43 {
            text-align: right;
            width: 800px;
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
    <table class="auto-style42 table-borderless">
        <tr>
            <td class="auto-style43" colspan="2">Order ID:</td>
            <td class="auto-style44" colspan="2">
                <asp:DropDownList ID="ddlorderid" CssClass="form-control" runat="server" Width="180px"></asp:DropDownList>


            </td>
            <td class="auto-style44" colspan="2">
                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Check Order" Font-Bold="True" OnClick="searchForOrder" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblsuccessmassage" runat="server" Text="" ForeColor="Green"></asp:Label>
                <asp:Label ID="lblerrormessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="margin-left: auto; margin-right: auto; width: 600px;">
        <div class="auto-style41">
            <h3 class="auto-style52">Product List</h3>
            <asp:HiddenField ID="hfProductId" runat="server" />
            <div class="auto-style51">
                <asp:GridView ID="productGrid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                        <asp:BoundField DataField="ProductId" HeaderText="Product Id" SortExpression="ProductId" />
                        <asp:BoundField DataField="OrderProductAmount" HeaderText="Order Product Amount" SortExpression="OrderProductAmount" />
                        <asp:BoundField DataField="CarrierId" HeaderText="Carrier Id" SortExpression="CarrierId" />
                        <asp:BoundField DataField="CarrierName" HeaderText="Carrier Name" SortExpression="CarrierName" />
                        <asp:BoundField DataField="CustomerId" HeaderText="Customer Id" SortExpression="CustomerId" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                        <asp:TemplateField HeaderText="Order Status" SortExpression="OrderStatus">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("OrderStatus")) ? "Ready To Send" : "Not Ready" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="IsShipped" HeaderText="Send Status" SortExpression="IsShipped" />
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </div>
    </div>
</asp:Content>

