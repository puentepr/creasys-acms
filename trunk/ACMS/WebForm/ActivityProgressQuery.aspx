<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="ActivityProgressQuery.aspx.cs" Inherits="WebForm_ActivityProgressQuery" Title="未命名頁面" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <table align="center">
            <tr>
                <td>
                          活動名稱:<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>員工體檢</asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" 
            RepeatColumns="3" BorderStyle="Outset" CellPadding="4" ForeColor="#333333" 
            GridLines="Both">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <ItemTemplate>
                <asp:Label ID="emp_idLabel" runat="server" Text='<%# Eval("emp_id") %>' />
                &nbsp;&nbsp;&nbsp; <asp:Label ID="check_statusLabel" runat="server" 
                    Text='<%# Eval("check_status") %>' />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            
            SelectCommand="SELECT emp_id,CASE check_status WHEN 1 THEN '已報到'  WHEN 2 THEN '已取消' WHEN 3 THEN '已完成' WHEN 4 THEN '已離職' END as check_status  FROM [ActivityRegist]"></asp:SqlDataSource></td>
            </tr>
          
        </table>

</asp:Content>

