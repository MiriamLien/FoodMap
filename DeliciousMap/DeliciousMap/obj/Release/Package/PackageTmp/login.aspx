<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DeliciousMap.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>美食地圖-登入</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1 align="center">會員登入</h1>
        <asp:PlaceHolder ID="plLogin" runat="server">
           Account: <asp:TextBox ID="txtAccount" runat="server"/><br /><br />
           Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/><br />
           <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /><br />
           <asp:Literal ID="ltlMessage" runat="server"/>
        </asp:PlaceHolder>
        <br />
        <asp:PlaceHolder ID="plUserInfo" runat="server">
            登入會員: <asp:Literal ID="ltlAccount" runat="server"/><br />
            請前往<a href="/Backadmin/Index.aspx">後台</a>
        </asp:PlaceHolder>
    </form>
</body>
</html>
