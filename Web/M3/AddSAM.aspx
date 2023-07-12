<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSAM.aspx.cs" Inherits="M3.AddSAM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    Name<br />
        <asp:TextBox ID="SAMname" runat="server"></asp:TextBox>
        <br />
        <br />
        Username<br />
        <asp:TextBox ID="SAMusername" runat="server"></asp:TextBox>
        <br />
        <br />
        Password<br />
        <asp:TextBox ID="SAMpassword" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="AddAssocManager" Text="Add" />
    </form>
</body>
</html>
