<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Item.aspx.cs" Inherits="Item" %>



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
        <h3 class="auto-style52">Item Registration</h3>
        <asp:HiddenField ID="hfProductId" runat="server" />
        <table class="auto-style42 table-borderless">
            <tr>
                <td class="auto-style43" colspan="2">Item ID:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtproid" CssClass="form-control" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Item Name:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtproname" CssClass="form-control" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Item Description:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtprodes" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Item Weight:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtproweight" CssClass="form-control" runat="server" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Item Size:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtprosize" CssClass="form-control" runat="server" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Item Origin:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtproorigin" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" Font-Bold="True" OnClick="btnsave_Click" Width="55px" />
                </td>
                <td class="auto-style50">
                    <asp:Button ID="btnclear" class="btn btn-primary btn-sm" CssClass="form-control" runat="server" Text="Clear" Font-Bold="True" OnClick="btnclear_Click" />

                </td>
                <td class="auto-style50">
                    <asp:Button ID="Button2" class="btn btn-primary btn-sm" CssClass="form-control" runat="server" Text="Check" Font-Bold="True" OnClick="searchForExistingItem" />
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
                <span class="auto-style52">Item List</span><br />
                <br />
            </h4>

            <asp:GridView ID="productGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting" OnRowCommand="productGrid_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ProductId" HeaderText="Item Id" SortExpression="ProductId" />
                    <asp:BoundField DataField="ProductName" HeaderText="Item Name" SortExpression="ProductName" />
                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" SortExpression="ProductDescription" />
                    <asp:BoundField DataField="ProductCount" HeaderText="Item Count" SortExpression="ProductCount" />
                    <asp:BoundField DataField="ProductWeight" HeaderText="Item Weight" />
                    <asp:BoundField DataField="ProductSize" HeaderText="Item Size" />
                    <asp:BoundField DataField="ProductOrigin" HeaderText="Item Origin" SortExpression="ProductOrigin" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="table-header" />
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>

