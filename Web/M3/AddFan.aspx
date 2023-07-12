<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFan.aspx.cs" Inherits="M3.AddFan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        Name<br />
        <asp:TextBox ID="Fname" runat="server"></asp:TextBox>
        <br />
        <br />
        Username<br />
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <br />
        Password<br />
        <asp:TextBox ID="password" runat="server"></asp:TextBox>
        <br />
        <br />
        National ID<br />
        <asp:TextBox ID="ID" runat="server"></asp:TextBox>
        <br />
        <br />
        Phone Number<br />
        <asp:TextBox ID="phone" runat="server" TextMode="Number"></asp:TextBox>
        <br />
        <br />
        Birthdate<br />
        <asp:TextBox ID="birth" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <br />
        Address<br />
        <asp:TextBox ID="address" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="AddF" />
        
    </form>
</body>
</html>
