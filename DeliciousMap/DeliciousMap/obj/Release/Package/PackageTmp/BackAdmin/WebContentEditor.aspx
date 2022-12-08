<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebContentEditor.aspx.cs" Inherits="DeliciousMap.BackAdmin.WebContentEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <span>經度的值為 -180~180，可至小數 6 位</span> 
        <span>緯度的值為 -90~90，可至小數 6 位</span>
        <asp:HiddenField ID="hfID" runat="server" />
        <table>
            <tr>
                <th>標題</th>
                <td> <asp:TextBox runat="server" ID="txttitle" /></td>
            </tr>
            <tr>
                <th>內文</th>
                <td> <asp:TextBox runat="server" ID="txtbody" TextMode="MultiLine" /></td>
            </tr>
           <tr>
                <th>緯度</th>
                <td><asp:TextBox runat="server" ID="txtLatitude" TextMode="Number" /> </td>
            </tr>
           <tr>
                <th>經度</th>
                <td><asp:TextBox runat="server" ID="txtLongitude"/></td>
            </tr>
        </table>
        <asp:Button runat="server" ID="BtnSave" Text="儲存" Onclick="btnSave_Click" />
        <asp:Literal runat="server" ID="ltlMessage" />
    </form>
</body>
</html>
