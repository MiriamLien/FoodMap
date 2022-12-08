<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="MemberDetails.aspx.cs" Inherits="DeliciousMap.BackAdmin.MemberDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="1">
        <tr>
            <td>會員代碼:</td>
            <td>
                <asp:Literal ID="ltlID" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>帳號:</td>
            <td>
                <asp:Literal ID="ltlAccount" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>密碼:</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label><br />
    <asp:Button ID="btnsave" runat="server" Text="儲存" onclick="btnsave_Click"/>
    <asp:Button ID="btnCancel" runat="server" Text="取消" onclick="btnCancel_Click"/>
</asp:Content>
