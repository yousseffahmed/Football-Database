<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Please Sign In<br />
        Username<p>
            <asp:TextBox ID="username" runat="server" style="margin-top: 0px"></asp:TextBox>
        </p>
        <p>
            Password</p>
        <p>
            <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="sigin" runat="server" OnClick="login" Text="Log In" />
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="AddUser" Text="Register" />
        </p>
    </form>
</body>
</html>
