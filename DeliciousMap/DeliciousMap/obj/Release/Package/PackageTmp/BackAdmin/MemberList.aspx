<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="DeliciousMap.BackAdmin.MemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnCreate" runat="server" Text="新增" onclick="btnCreate_Click"/>&nbsp
    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" /><br />

    <asp:TextBox ID="txtkeyword" runat="server" placeHolder="請輸入搜尋文字"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click"/><br />

    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkdel" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID")%>'/>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="代碼" />
            <asp:BoundField DataField="Account" HeaderText="帳號" />
           <%-- <asp:BoundField DataField="CreateDate" HeaderText="建立日期" />--%>

            <asp:TemplateField>
                <ItemTemplate>
                    <a href="MemberDetails.aspx?ID=<%# Eval("ID") %>">編輯</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:PlaceHolder ID="plcEmpty" runat="server">
        <p>查無資料</p>
    </asp:PlaceHolder>
</asp:Content>
