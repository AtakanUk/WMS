<%@ Page Title="Product" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Outbound.aspx.cs" Inherits="Outbound" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function addProduct() {
            // Get the product details from the form
            var productId = document.getElementById("productId").value;
            var productName = document.getElementById("productName").value;
            var productPrice = document.getElementById("productPrice").value;

            // Create a new row for the product
            var newRow = "<tr><td>" + productId + "</td><td>" + productName + "</td><td>" + productPrice + "</td></tr>";

            // Append the new row to the table
            var tableBody = document.getElementById("productTable").getElementsByTagName("tbody")[0];
            tableBody.innerHTML += newRow;

            // Clear the form
            document.getElementById("productId").value = "";
            document.getElementById("productName").value = "";
            document.getElementById("productPrice").value = "";
        }
        function saveProducts() {
            // Get all the product rows from the table
            var rows = document.getElementById("productTable").getElementsByTagName("tbody")[0].getElementsByTagName("tr");

            // Create an array to hold the product data
            var products = [];

            // Loop through the rows and add each product to the array
            for (var i = 0; i < rows.length; i++) {
                var cells = rows[i].getElementsByTagName("td");
                var productId = cells[0].innerHTML;
                var productName = cells[1].innerHTML;
                var productPrice = cells[2].innerHTML;
                var product = { productId: productId, productName: productName, productPrice: productPrice };
                products.push(product);
            }

            // Send the product data to the server to save to the database
            // ...
        }
        document.getElementById("addProductButton").addEventListener("click", addProduct);
        document.getElementById("saveButton").addEventListener("click", saveProducts);
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
        <h3 class="auto-style52">Item Registration</h3>
        <asp:HiddenField ID="hfProductId" runat="server" />
        <table class="auto-style42">
            <tr>
                <td class="auto-style43" colspan="2">Item ID:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtproid" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                </td>
                <td class="auto-style43" colspan="2">Item Count:</td>
                <td class="auto-style44" colspan="2">
                    <asp:TextBox ID="txtitemcount" runat="server" MaxLength="99" onkeypress="return isNumberKey(event)" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnsave" runat="server" Text="Add To Basket" Font-Bold="True" OnClick="AddToBasketButton_Click" Width="100px" />
                </td>
                <td class="auto-style49">
                    <asp:Button ID="btncheckout" runat="server" Text="Delete" OnClick="CheckoutButton_Click" Width="56px" Font-Bold="True" />
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
            <span class="auto-style52">Outbound List</span><br />
            <br />
        </h4>
        <div class="auto-style51">
            <asp:GridView ID="basketGrid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ProductId" HeaderText="Product ID" />
                    <asp:BoundField DataField="ProductCount" HeaderText="Product Count" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>

