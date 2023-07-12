<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClubRep.aspx.cs" Inherits="M3.ClubRep" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
        <asp:Label ID="info" runat="server" Text="No Club Assigned"></asp:Label>
        <br />
        <br />
       
        <asp:Button ID="Button2" runat="server" Text="View Upcoming Matches" Width="312px" OnClick="viewupcomingmatches" />
        <br />
       
        <br />
        <asp:Button ID="date" runat="server" OnClick="viewavailablestad" Text="View Available Stadiums On a Date" Width="312px" />
       
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="sr" Text="Send Request" Width="312px" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="logout" Text="Log Out" />
        <br />
       
    </form>
    </body>
</html>
