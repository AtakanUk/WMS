<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Shipment.aspx.cs" Inherits="Shipment" %>



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
        <h3 class="auto-style52">Orders That Are Ready To Ship</h3>
        <div class="auto-style51">
            <asp:GridView ID="orderGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="orderGrid_RowCommand" DataKeyNames="OrderId">
                <Columns>
                    <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                    <asp:TemplateField HeaderText="Order Status">
                        <ItemTemplate>
                            <%# Eval("OrderStatus").ToString().Equals("False") ? "Not Completed" : "Completed" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Button" Text="Ship" CommandName="GetOrderID" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>

