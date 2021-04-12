<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="LoginSystemASP.NET.User1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User</title>
    <link href="style-main.css" rel="stylesheet" />
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
                    <asp:Button ID="logOut" runat="server" Text="Log out" OnClick="logOut_Click"  CssClass="link logout"/>
                </div>
            </header>
            <div class="profile">
                <asp:Image ID="Image1" runat="server" CssClass="profile-image" />
             <asp:Label ID="lblUsername" runat="server" Text="" CssClass="username-logged"></asp:Label>
            
            </div>

            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
               <asp:Label ID="lblBiography" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnOpenImage" runat="server" Text="Open"  OnClick="btnOpenImage_Click"/>
            <asp:Button ID="btnSaveImage" runat="server" Text="Save"  OnClick="btnSaveImage_Click"/>
            <asp:Button ID="btnLoadImage" runat="server" Text="Load" />
            <asp:FileUpload ID="FileUpload1" runat="server"  Text="Upload"/>

        </div>
    </form>
</body>
<script src="JavaScript.js"></script>
</html>
