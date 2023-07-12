<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCR.aspx.cs" Inherits="M3.AddCR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    Name<br />
        <asp:TextBox ID="CRname" runat="server"></asp:TextBox>
        <br />
        <br />
        Username<br />
        <asp:TextBox ID="CRusername" runat="server"></asp:TextBox>
        <br />
        <br />
        Password<br />
        <asp:TextBox ID="CRpassword" runat="server"></asp:TextBox>
        <br />
        <br />
        Club Name<br />
        <asp:TextBox ID="club" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="AddClubRep" Text="Add" />
    </form>
</body>
</html>
