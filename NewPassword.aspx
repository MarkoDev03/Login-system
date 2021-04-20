﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" Inherits="LoginSystemASP.NET.NewPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Password</title>
    <link href="style4.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <header class="index-header">
                <div class="logo">
                    <asp:Image ID="logo" runat="server" ImageUrl="~/Images/MarkoLogo2.png" />
                    <p>Marko Perović</p>
                </div>
                <div>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Index.aspx" CssClass="link">Home</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx" CssClass="link">Log in</asp:HyperLink>

                </div>
            </header>
            <section class="section" id="section">
                <div class="black-wrapper" id="wrapper-img"></div>
                <div class="black-wrapper-black" id="wrapper-img-2"></div>
                <div class="form">
                    <h1 class="headline">New password</h1>

                    <div class="block">
                        <asp:Label ID="Label6" runat="server" Text="Password" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="text-box"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnNewPassword" runat="server" Text="Apply" CssClass="button-submit" OnClick="btnNewPassword_Click"/>
                    <asp:Label ID="lblProgress" runat="server" Text="" CssClass="error-label"></asp:Label>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
