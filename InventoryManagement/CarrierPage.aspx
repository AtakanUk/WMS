<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CarrierPage.aspx.cs" Inherits="CarrierPage" %>



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
            text-align: center;
            margin-left: auto;
            margin-right: auto;
            color: #003399;
        }

        .auto-style43 {
            text-align: right;
            margin-left: auto;
            margin-right: auto;
            width: 608px;
        }

        .auto-style44 {
            text-align: left;
            margin-left: auto;
            margin-right: auto;
        }

        .auto-style46 {
            text-align: right;
            margin-left: auto;
            margin-right: auto;
            width: 571px;
        }

        .auto-style49 {
            text-align: center;
            margin-left: auto;
            margin-right: auto;
            width: 8px;
        }

        .auto-style50 {
            text-align: center;
            margin-left: auto;
            margin-right: auto;
            width: 73px;
        }

        .auto-style51 {
            text-align: center;
            margin-left: auto;
            margin-right: auto;
        }

        .auto-style52 {
            color: #003399;
        }

        .container {
            margin: auto;
            text-align: center;
            margin-left: auto;
            margin-right: auto;
        }

        .table-borderless {
            border-collapse: separate;
            border-spacing: 5px 5px;
        }
 
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="auto-style41">
    <h3 class="mb-4 text-center auto-style52">Carrier Registration</h3>

    <table class="auto-style42 table-borderless">
        <tr>
            <td class="auto-style43" colspan="2">Carrier Name:</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtcarriername" CssClass="form-control" runat="server" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Carrier Address:</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtcarrieraddress" CssClass="form-control" runat="server" MaxLength="99" TextMode="MultiLine" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Contact Person:</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtcontactperson" CssClass="form-control" runat="server" MaxLength="99" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Email:</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="99" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43 border" colspan="2">Carrier Phone:</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtcarrierphone" CssClass="form-control" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style46">
                <asp:Button ID="btnsave" class="btn btn-primary" runat="server" Text="Save" Font-Bold="True" OnClick="btnsave_Click" Width="55px" />
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
                <span class="auto-style52">Carrier List</span><br />
                <br />
            </h4>

            <asp:GridView ID="productGrid" AutoGenerateColumns="false" OnRowCommand="productGrid_RowCommand" runat="server" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="CarrierId" HeaderText="Carrier Id" SortExpression="CarrierId" />
                    <asp:BoundField DataField="CarrierName" HeaderText="Carrier Name" SortExpression="CarrierName" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("CarrierId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />

    </div>
        </div>
</asp:Content>

