<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DeliciousMap.BackAdmin.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        登入的帳號: <asp:Literal ID="ltlAccount" runat="server"/>
        <p>
        <asp:Button ID="btnLogout" runat="server" Text="登出" onclick="btnLogout_Click"/>
        </p>
    </form>
</body>
</html>
