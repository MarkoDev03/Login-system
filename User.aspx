<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="LoginSystemASP.NET.User1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblBiography" runat="server" Text=""></asp:Label>

            <asp:Button ID="logOut" runat="server" Text="Log out"  OnClick="logOut_Click"/>
        </div>
    </form>
</body>
</html>
