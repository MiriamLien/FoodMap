<%@ Page Title="美食地圖-MapList" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MapList.aspx.cs" Inherits="DeliciousMap.MapList" %>

<%@ Register Src="~/ShareControls/ucPage.ascx" TagPrefix="uc1" TagName="ucPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Pager > a {
            margin: 50px;
        }

        .SearchPanel {
            border: dashed 1px black;
        }

            .SearchPanel > label {
                font-size: larger;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="SearchPanel">
        <label for="<% = this.txtkeyword.ClientID %>">關鍵字</label><%--第一種解法--%>
        <asp:TextBox ID="txtkeyword" runat="server" /><br />
        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
        <%--        //第二種解法
         <label>
            關鍵字<asp:TextBox ID="TextBox1" runat="server" /><br />
        </label>--%>
        <%--        //第三種解法
        <asp:Label runat="server" Text="關鍵字" AssociatedControlID="txtkeyword"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" />--%>
    </div>

    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <div class="CardList">
                <p>
                    <a href="MapDetail.aspx?ID=<%# Eval("ID") %>" title="前往內容:<%# Eval("Title") %>"><%# Eval("Title")%></a>
                </p>

                <asp:PlaceHolder runat="server" Visible='<%#!string.IsNullOrWhiteSpace(Eval("CoverImage")as string)%>'>
                    <p>
                        <a href="MapDetail.aspx?ID=<%# Eval("ID") %>" title="前往內容:<%# Eval("Title") %>">
                            <img src="<%# Eval("CoverImage")%>" width="150" height="150" /></a>
                    </p>
                </asp:PlaceHolder>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
        <p>尚未有資料</p>
    </asp:PlaceHolder>

    <uc1:ucPage runat="server" ID="ucPage" />

    <script>
        var initObj = {
            txtSearchID = "<% = this.txtkeyword.ClientID %>";
            btnSearchID = "<% = this.txtkeyword.ClientID %>";
        };
    </script>
    <script src="\JavaScript\Models\MapList.js"></script>
</asp:Content>
