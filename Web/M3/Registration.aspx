<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="M3.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="AddSAM" Text="Association Manager" Width="184px" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="AddCR" Text="Club Representative" Width="184px" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="AddSM" Text="Stadium Manager" Width="184px" />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="AddFan" Text="Fan" Width="184px" />
    </form>
</body>
</html>
