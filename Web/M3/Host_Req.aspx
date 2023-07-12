<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Host_Req.aspx.cs" Inherits="M3.Host_Req" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Stadium Manager Name:<br />
            <asp:TextBox ID="SM" runat="server"></asp:TextBox>
            <br />
            <br />
            Date:<br />
            <asp:TextBox ID="date" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="send" Text="Send" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="back" Text="Back" />
        </div>
    </form>
</body>
</html>
