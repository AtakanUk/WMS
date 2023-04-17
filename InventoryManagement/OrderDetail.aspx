﻿<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <table class="auto-style42">
            <tr>
                <td class="auto-style43" colspan="2">Order ID:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtorderid" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="Check Order" Font-Bold="True" OnClick="searchForOrder" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblsuccessmassage" runat="server" Text="" ForeColor="Green"></asp:Label>
                    <asp:Label ID="lblerrormessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    <div class="auto-style41">
        <h3 class="auto-style52">Product List</h3>
        <asp:HiddenField ID="hfProductId" runat="server" />
        <div class="auto-style51">
            <asp:GridView ID="productGrid" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                    <asp:BoundField DataField="ProductId" HeaderText="Product Id" SortExpression="ProductId" />
                    <asp:BoundField DataField="OrderProductAmount" HeaderText="Order Product Amount" SortExpression="OrderProductAmount" />
                    <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" SortExpression="OrderStatus" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>
