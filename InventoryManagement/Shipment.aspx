<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Shipment.aspx.cs" Inherits="Shipment" %>



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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-left: auto; margin-right: auto; width: 600px;">
        <div class="auto-style41">
            <h3 class="auto-style52">Orders That Are Ready To Ship</h3>
            <div class="auto-style51">
                <asp:GridView ID="orderGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="orderGrid_RowDataBound" OnRowCommand="orderGrid_RowCommand" DataKeyNames="OrderId" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                        <asp:TemplateField HeaderText="Order Status">
                            <ItemTemplate>
                                <%# Eval("OrderStatus").ToString().Equals("False") ? "Not Completed" : "Completed" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Items">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlItems" CssClass="form-control" runat="server" DataTextField="CarrierName" DataValueField="CarrierName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" Text="Ship" CommandName="GetOrderID">
                            <ControlStyle CssClass="btn btn-primary" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </div>
    </div>
</asp:Content>

