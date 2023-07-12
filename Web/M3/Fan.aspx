<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fan.aspx.cs" Inherits="M3.Fan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="viewavailablematches" Text="View Available Matches To Attend" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="logout" Text="Log Out" />
    </form>
</body>
</html>
