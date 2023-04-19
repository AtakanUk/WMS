<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerPage.aspx.cs" Inherits="CustomerPage" %>



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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="auto-style41">
        <h3 class="auto-style52">Customer Registration</h3>
        <table class="auto-style42 table-borderless">
            <tr>
                <td class="auto-style43" colspan="2">Customer Name:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtcustomername" CssClass="form-control" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Address:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtaddress" CssClass="form-control" runat="server" MaxLength="99" TextMode="MultiLine" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Email:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" MaxLength="99" TextMode="MultiLine" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Phone:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtphone" CssClass="form-control" runat="server" MaxLength="99" TextMode="MultiLine" Width="128px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" Font-Bold="True" OnClick="btnsave_Click" Width="55px" />
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
            <span class="auto-style52">Customer List</span><br />
            <br />
        </h4>
        <div class="auto-style51">
            <asp:GridView ID="productGrid" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting" CssClass="table table-bordered table-striped" OnRowDeleting="productGrid_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="CustomerId" HeaderText="Customer Id" SortExpression="CustomerId" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("CustomerId") %>' OnClick="btnDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
        </div>
    </div>
</asp:Content>

