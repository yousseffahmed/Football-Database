<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewRequests.aspx.cs" Inherits="M3.viewRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Host:<br />
        <asp:TextBox ID="hostname" runat="server"></asp:TextBox>
        <br />
        <br />
        Guest:<br />
        <asp:TextBox ID="guestname" runat="server"></asp:TextBox>
        <br />
        <br />
        Date:<br />
        <asp:TextBox ID="date" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="accept" Text="Accept Request" Width="140px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="reject" Text="Reject Request" Width="140px" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Back" OnClick="back"/>
        <br />
        <br />
    </form>
</body>
</html>
