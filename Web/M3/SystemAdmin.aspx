<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemAdmin.aspx.cs" Inherits="M3.SystemAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Add Club:<br />
        <br />
        Name
        <br />
        <asp:TextBox ID="CNA" runat="server"></asp:TextBox>
        <br />
        Location<br />
        <asp:TextBox ID="CLA" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="AddClub" Text="Add" />
        <br />
        <br />
        <br />
        Delete Club:<br />
        <br />
        Name<br />
        <asp:TextBox ID="CND" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="DeleteClub" Text="Delete" />
        <br />
        <br />
        <br />
        Add Stadium:<br />
        <br />
        Name<br />
        <asp:TextBox ID="SNA" runat="server"></asp:TextBox>
        <br />
        Location<br />
        <asp:TextBox ID="SLA" runat="server"></asp:TextBox>
        <br />
        Capacity<br />
        <asp:TextBox ID="SCA" runat="server" TextMode="Number"></asp:TextBox>
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="AddStadium" Text="Add" />
        <br />
        <br />
        <br />
        Delete Stadium:<br />
        <br />
        Name<br />
        <asp:TextBox ID="SND" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="DeleteStadium" Text="Delete" />
        <br />
        <br />
        <br />
        Block Fan:<br />
        <br />
        National ID<br />
        <asp:TextBox ID="FNB" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button5" runat="server" OnClick="BlockFan" Text="Block" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button6" runat="server" OnClick="logout" Text="Log Out" />
    </form>
</body>
</html>
