<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapContentList.aspx.cs" Inherits="DeliciousMap.MapContentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>美食地圖-列表</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server">

        </asp:GridView>
        <asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
            <div>無資料</div>
        </asp:PlaceHolder>
    </form>
</body>
</html>
