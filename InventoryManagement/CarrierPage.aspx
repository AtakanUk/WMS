<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CarrierPage.aspx.cs" Inherits="CarrierPage" %>



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
    <div class="auto-style41">
        <h3 class="auto-style52">Carrier Registration</h3>
        <table class="auto-style42">
            <tr>
                <td class="auto-style43" colspan="2">Carrier Name:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtcarriername" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Carrier Address:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtcarrieraddress" runat="server" MaxLength="99" TextMode="MultiLine" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Contact Person:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtcontactperson" runat="server" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Email:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtemail" runat="server" MaxLength="99" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style43" colspan="2">Carrier Phone:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtcarrierphone" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnsave" runat="server" Text="Save" Font-Bold="True" OnClick="btnsave_Click" Width="55px" />
                </td>

                <td class="auto-style50">
                    <asp:Button ID="btnclear" runat="server" Text="Clear" Font-Bold="True" OnClick="btnclear_Click" />
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
        <h4>
            <br />
            <span class="auto-style52">Carrier List</span><br />
            <br />
        </h4>
        <div class="auto-style51">
            <asp:GridView ID="productGrid" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowSorting="true" OnSorting="productGrid_Sorting" OnRowCommand="productGrid_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CarrierId" HeaderText="Carrier Id" SortExpression="CarrierId" />
                    <asp:BoundField DataField="CarrierName" HeaderText="Carrier Name" SortExpression="CarrierName" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("CarrierId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>

