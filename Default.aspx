<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LoginSystemASP.NET.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Account</title>
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
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Index.aspx" CssClass="link">Home</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx" CssClass="link">Log in</asp:HyperLink>

                </div>
            </header>
            <section class="section" id="section">
                <div class="black-wrapper" id="wrapper-img"></div>
                <div class="black-wrapper-black" id="wrapper-img-2"></div>
                <div class="form">
                    <h1 class="headline">Create account</h1>
                    <div class="block">
                        <asp:Label ID="Label1" runat="server" Text="Username" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="text-box"></asp:TextBox>
                    </div>
                    <div class="block">
                        <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="text-box"></asp:TextBox>
                    </div>
                    <div class="block">
                        <asp:Label ID="Label3" runat="server" Text="Biography" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtBiography" runat="server" CssClass="text-box"></asp:TextBox>
                    </div>
                     <div class="block">
                        <asp:Label ID="Label6" runat="server" Text="E-mail" CssClass="label-name"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="text-box"></asp:TextBox>
                    </div>
                    <div class="check">
                        <div class="check-gender">
                            <asp:CheckBox ID="chcMale" runat="server" CssClass="checbox" />
                            <asp:Label ID="Label4" runat="server" Text="Male" CssClass="txt-check"></asp:Label>
                        </div>
                        <div class="check-gender">
                            <asp:CheckBox ID="chcFemale" runat="server" CssClass="checbox" />
                            <asp:Label ID="Label5" runat="server" Text="Female" CssClass="txt-check"></asp:Label>
                        </div>
                    </div>
                   <asp:Button ID="btnCreateAccount" runat="server" OnClick="btnCreateAccount_Click" Text="Create" CssClass="button-submit" />
                    <asp:Label ID="lblProgress" runat="server" Text="" CssClass="error-label"></asp:Label>
                </div>
            </section>

        </div>
    </form>
</body>
<script src="JavaScript1.js"></script>
</html>
