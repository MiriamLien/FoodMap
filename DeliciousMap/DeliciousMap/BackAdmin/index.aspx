<%@ Page Title="美食地圖-首頁" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DeliciousMap.BackAdmin.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    登入的帳號：
    <asp:Literal ID="ltlAccount" runat="server" />&emsp;
    <asp:Button ID="btnLogout" runat="server" Text="登出" OnClick="btnLogout_Click" />
</asp:Content>
