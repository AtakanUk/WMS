<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Outbound.aspx.cs" Inherits="Outbound" %>



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
    <div class="auto-style41">
        <h3 class="auto-style52">Outbound Order</h3>
        <asp:HiddenField ID="hfProductId" runat="server" />
        <table class="auto-style42 table-borderless">
            <tr>
                <td class="auto-style43">Item ID:</td>
                <td class="auto-style44">
                    <asp:DropDownList ID="ddlItemId" runat="server" Width="128px" CssClass="form-control" MaxLength="99"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style43">Item Count:</td>
                <td class="auto-style44">
                    <asp:TextBox ID="txtitemcount" runat="server" MaxLength="99" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43">Customer Name:</td>
                <td class="auto-style44">
                    <asp:DropDownList ID="ddlCustomers" CssClass="form-control" runat="server" Width="128px"></asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnsave" class="btn btn-primary" runat="server" Text="Add To Order List" Font-Bold="True" OnClick="AddToShipmentButton_Click" Width="150px" />
                </td>
                <td class="auto-style49">
                    <asp:Button ID="btncheckout" class="btn btn-primary btn-sm" CssClass="form-control" runat="server" Text="Send List" OnClick="SendList_Click" Width="150px" Font-Bold="True" />
                </td>
                <td class="auto-style50">
                    <asp:Button ID="btnclear" runat="server" class="btn btn-primary btn-sm" CssClass="form-control" Text="Clear" Font-Bold="True" OnClick="btnclear_Click" />
                </td>
                <td class="auto-style44">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblsuccessmassage" runat="server" Text="" ForeColor="Green"></asp:Label>
                    <asp:Label ID="lblerrormessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="margin-left: auto; margin-right: auto; width: 600px;">
            <h4>
                <br />
                <br />
            </h4>
            <h4 class="auto-style52">Selected Items</h4>
            <div class="auto-style51">
                <asp:GridView ID="basketGrid" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="False" HeaderText="Selected Items">
                    <Columns>
                        <asp:BoundField DataField="ProductId" HeaderText="Item Id" />
                        <asp:BoundField DataField="OrderProductAmount" HeaderText="Product Count" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </div>
    </div>
</asp:Content>

