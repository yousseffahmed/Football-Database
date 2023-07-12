<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewAvailableStadiums.aspx.cs" Inherits="M3.viewavstad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Date:<br />
            <asp:TextBox ID="date" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="viewavailablestads" Text="View" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Back" OnClick="back" />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
