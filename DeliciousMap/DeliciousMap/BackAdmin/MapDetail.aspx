<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="MapDetail.aspx.cs" Inherits="DeliciousMap.BackAdmin.MapDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>有顯示 (*) 為必填欄位</p>
    <table>
        <tr>
            <td>標題 (*)</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>內文 (*)</td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" textMode="MultiLine"/>
            </td>
        </tr>
        <tr>
            <td>經度 (*)</td>
            <td>
                <asp:TextBox ID="txtLongitude" runat="server" />
            </td>
        </tr>
        <tr>
            <td>緯度 (*)</td>
            <td>
                <asp:TextBox ID="txtLatitude" runat="server" />
            </td>
        </tr>
        <tr>
            <td>封面圖 (*)</td>
            <td>
                <asp:FileUpload ID="flCoverimage" runat="server" />
                <asp:Image ID="image" runat="server" />
            </td>
        </tr>
        <tr>
            <td>是否顯示 (*)</td>
            <td>
                <asp:CheckBox ID="chkIsEnable" runat="server"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="lblMsg" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnsave" runat="server" Text="儲存" OnClick="btnsave_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
</asp:Content>
