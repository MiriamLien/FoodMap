<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="DeliciousMap.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload><asp:Label ID="lblMasg" runat="server" Text="lblMsg"></asp:Label><br />
        <asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload><asp:Label ID="lblMsg2" runat="server" Text="lblMsg"></asp:Label><br />
        <asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload><asp:Label ID="lblMsg3" runat="server" Text="lblMsg"></asp:Label><br />
        <asp:FileUpload ID="FileUpload4" runat="server"></asp:FileUpload><asp:Label ID="lblMsg4" runat="server" Text="lblMsg"></asp:Label><br />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </form>
</body>
</html>
