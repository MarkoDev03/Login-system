<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LoginSystemASP.NET.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log in</title>
    <link href="style4.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <header class="index-header">
                <div class="logo">
                    <asp:Image ID="logo" runat="server" ImageUrl="~/Images/MarkoLogo2.png" />
                    <p>Marko Perović</p>
                </div>
                <div>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx" CssClass="link">Home</asp:HyperLink>            
                </div>
            </header>
            <section class="section" id="section">
                <div class="black-wrapper" id="wrapper-img"></div>
                <div class="black-wrapper-black" id="wrapper-img-2"></div>
                <div class="form">
                    <h1 class="headline">Log in</h1>
                    <div class="block">
                        <asp:Label ID="Label1" runat="server" Text="Username" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="text-box"></asp:TextBox>
                    </div>
                    <div class="block">
                        <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="text-box"></asp:TextBox>
                    </div>
                    <asp:Button ID="Button1" runat="server" OnClick="btnCreateAccount_Click" Text="Log in" CssClass="button-submit" />
                    <asp:Button ID="Button2" runat="server" Text="Reset password" onclick="Button2_Click" CssClass="forgot-password"/>
                    <asp:Label ID="lblProgress" runat="server" Text="" CssClass="a" ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </section>
        </div>
    </form>
</body>
<script src="JavaScript1.js"></script>
</html>
