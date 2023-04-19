<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        .auto-style43 {
        }

        .auto-style44 {
            text-align: center;
        }


        .auto-style46 {
            text-align: right;
        }

        .secclmn {
        }

        .auto-style44 {
            color: blue;
        }

        .table-borderless {
            border-collapse: separate;
            border-spacing: 5px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-left: auto; margin-right: auto; width: 20%;">
        <table class="auto-style43 table-borderless">
            <tr>
                <td class="auto-style44" colspan="2">
                    <h3><strong>Login</strong></h3>
                </td>
            </tr>
            <tr>
                <td class="auto-style46">Username:&nbsp;</td>
                <td class="secclmn">
                    <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style46">Password:&nbsp;</td>
                <td class="secclmn">
                    <asp:TextBox ID="txtPassword" CssClass="form-control" Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style46">
                    <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
                </td>
                <td class="auto-style46">
                    <asp:Button ID="btnRefresh" runat="server" class="btn btn-primary btn-sm" CssClass="form-control" Text="Clear" OnClick="btnRefresh_Click" />
                <asp:Label ID="lblerror" runat="server" ForeColor="#CC0000"></asp:Label>
                </td>
                 <td class="auto-style46">
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Register" OnClick="btnRegister_Click" />
                </td>
            </tr>

        </table>
    </div>
</asp:Content>

