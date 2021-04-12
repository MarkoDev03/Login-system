<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LoginSystemASP.NET.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log in</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
          <header class="index-header">
             <div class="logo">
                    <asp:Image ID="logo" runat="server" ImageUrl="~/Images/MarkoLogo2.png" />
                 <p>Marko Perović</p>
             </div>
<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Index.aspx" CssClass="link">Create account</asp:HyperLink>
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
            <asp:Label ID="Label2" runat="server" Text="Password"  CssClass="label-name"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  CssClass="text-box"></asp:TextBox>
            </div>
    
            
            <asp:Button ID="Button1" runat="server"  OnClick="btnCreateAccount_Click" Text="Create" CssClass="button-submit" />
            <asp:Label ID="lblProgress" runat="server" Text="" ForeColor="White"></asp:Label>
       </div>
</section>
             </div>
    </form>
</body>
<script src="JavaScript.js"></script>
</html>
