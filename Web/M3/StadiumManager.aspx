<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StadiumManager.aspx.cs" Inherits="M3.StadiumManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="info" runat="server" Text="No Stadium Assigned"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="viewreqs" Text="View Requests" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="logout" Text="Log Out" />
    </form>
</body>
</html>
