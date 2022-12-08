<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="MapList.aspx.cs" Inherits="DeliciousMap.BackAdmin.MapList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Button runat="server" ID="btnCreate" Text="新增" OnClick="btnCreate_Click" />
    <asp:Button runat="server" ID="btnDelete" Text="刪除" OnClick="btnDelete_Click" /><br />
    <asp:TextBox ID="txtkeyword" runat="server" placeHolder="請輸入搜尋文字"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" /><br />

    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <asp:CheckBox ID="ckbDel" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />

            <p><a href="MapDetail.aspx?ID=<%# Eval("ID") %>" title="前往編輯:<%# Eval("Title") %>"><%# Eval("Title")%></a> </p>

            <asp:PlaceHolder runat="server" Visible='<%#!string.IsNullOrWhiteSpace(Eval("CoverImage")as string)%>'>
                <a href="MapDetail.aspx?ID=<%# Eval("ID") %>" title="前往編輯:<%# Eval("Title") %>"><img src="<%# Eval("CoverImage")%>" width="150" height="150" /></a>
            </asp:PlaceHolder>
        </ItemTemplate>
    </asp:Repeater>
    <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
        <p>尚未有資料</p>
    </asp:PlaceHolder>
</asp:Content>
