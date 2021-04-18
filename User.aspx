<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="LoginSystemASP.NET.User1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User</title>
    <link href="style3.css" rel="stylesheet" />
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
                    <asp:Button ID="logOut" runat="server" Text="Log out" OnClick="logOut_Click" CssClass="link logout" />
                </div>
            </header>
            <section class="user-section" id="user-sectio-x">
            </section>
            <div class="profile-picture-options">
                <div class="profile">
                    <asp:Image ID="Image1" runat="server" CssClass="profile-image" />
                    <asp:Label ID="lblUsername" runat="server" Text="" CssClass="username-logged"></asp:Label>
                </div>

                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>


                <asp:Label ID="lblBiography" runat="server" Text="" CssClass="user-biography"></asp:Label>
                <asp:Button ID="btnOpenImage" runat="server" Text="Post" OnClick="btnOpenImage_Click" CssClass="button-option" />
              
                <label class="button-option label-style">
                    <asp:FileUpload ID="FileUpload1" runat="server" Text="Upload" CssClass="upload-file" />
                    <asp:Label ID="Label2" runat="server" Text="Chose image" CssClass="upload-text"></asp:Label>
                </label>
                  <asp:Button ID="btnDeleteAccpount" runat="server" Text="Delete"  CssClass="button-option" ForeColor="Red"  OnClick="btnDeleteAccpount_Click"/>

            </div>
        </div>
    </form>
</body>

<script src="UserJS.js"></script>
</html>
