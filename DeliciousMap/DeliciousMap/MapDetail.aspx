<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MapDetail.aspx.cs" Inherits="DeliciousMap.MapDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0">
        <tr>
            <td>CreateDate</td>
            <td>
                <asp:Literal ID="ltlcreateDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>Title</td>
            <td>
                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>Longitude  Latitude</td>
            <td>
                <asp:Literal ID="ltlLongitude" runat="server"></asp:Literal>
                <asp:Literal ID="ltlLatitude" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>CoverImage</td>
            <td>
               <a runat="server" id="aLinkPic" title="點我觀看原圖 (另開新視窗)" target="_blank">
                    <img runat="server" id="imgCoverImage" width="450" height="300" /></a>
            </td>
        </tr>
        <tr>
            <td>Body</td>
            <td>
                <asp:Literal ID="ltlBody" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>

    <a href="MapList.aspx"> 回前頁 </a>
</asp:Content>
