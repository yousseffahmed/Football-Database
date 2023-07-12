<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewMatchestoAttend.aspx.cs" Inherits="M3.viewMatchestoAttend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Date:<br />
        <asp:TextBox ID="date" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="viewmatches" Text="View" />
        <br />
        <br />
        <br />
        Purchase Ticket:<br />
        National ID:<br />
        <asp:TextBox ID="natid" runat="server"></asp:TextBox>
        <br />
        <br />
        Host Club:<br />
        <asp:TextBox ID="hostname" runat="server"></asp:TextBox>
        <br />
        <br />
        Guest Club:<br />
        <asp:TextBox ID="guestname" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Purchase" OnClick="purchase" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Back" OnClick="back" />
        <br />
        <br />
    </form>
</body>
</html>
