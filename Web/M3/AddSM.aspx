<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSM.aspx.cs" Inherits="M3.AddSM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    Name<br />
        <asp:TextBox ID="SMname" runat="server"></asp:TextBox>
        <br />
        <br />
        Username<br />
        <asp:TextBox ID="SMusername" runat="server"></asp:TextBox>
        <br />
        <br />
        Password<br />
        <asp:TextBox ID="SMpassword" runat="server"></asp:TextBox>
        <br />
        <br />
        Stadium Name<br />
        <asp:TextBox ID="stadium" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="AddStadiumManager" Text="Add" />
    </form>
</body>
</html>
