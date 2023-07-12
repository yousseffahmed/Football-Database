<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssocManager.aspx.cs" Inherits="M3.AssocManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Add New Match:<br />
        <br />
        Host Name<br />
        <asp:TextBox ID="HCA" runat="server"></asp:TextBox>
        <br />
        <br />
        Guest Name<br />
        <asp:TextBox ID="GCA" runat="server"></asp:TextBox>
        <br />
        <br />
        Start Time<br />
        <asp:TextBox ID="STA" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
        <br />
        <br />
        End Time<br />
        <asp:TextBox ID="ETA" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="AddMatch" Text="Add" />
        <br />
        <br />
        <br />
        Delete Match:<br />
        <br />
        Host Name<br />
        <asp:TextBox ID="HCD" runat="server"></asp:TextBox>
        <br />
        <br />
        Guest Name<br />
        <asp:TextBox ID="GCD" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="DeleteMatch" Text="Delete" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="ViewUpcomingMatches" Text="View Upcoming Matches" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" OnClick="ViewPlayedMatches" Text="View Played Matches" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button5" runat="server" OnClick="ViewClubsNeverMatched" Text="View Clubs Never Matched" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button6" runat="server" OnClick="logout" Text="Log Out" />
        <br />
    </form>
</body>
</html>
