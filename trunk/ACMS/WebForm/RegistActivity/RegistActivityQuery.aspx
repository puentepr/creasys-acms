﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegistActivityQuery.aspx.cs" Inherits="WebForm_RegistActivityQuery" %>
<%@ Register src="OpenRegistedEmployeeSelector.ascx" tagname="OpenRegistedEmployeeSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="活動報名">
    <div class="MyDiv">
        <table>
            <tr>
                <td>
                    活動名稱</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    活動日期</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender
                        ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate">
                    </ajaxToolkit:CalendarExtender>
                    ~<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                        TargetControlID="txtEndDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>列出已報名活動</asp:ListItem>
                        <asp:ListItem>列出未報名活動</asp:ListItem>
                        <asp:ListItem>列出已代理報名活動</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查詢" />
                </td>
            </tr>
        </table>
        <br />
        <TServerControl:TGridView ID="TGridView2" runat="server" 
            AllowHoverEffect="True" AllowHoverSelect="True" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource2" 
            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" 
            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%">
            <Columns>
                <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                    SortExpression="activity_name" />
                <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                    SortExpression="people_type" />
                <asp:BoundField DataField="activity_date" HeaderText="活動日期" 
                    SortExpression="activity_date" />
                <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日期" />
                <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日期" />
                <asp:BoundField DataField="is_full" HeaderText="是否額滿" 
                    SortExpression="is_full" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click1">參加</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton1_Click">取消報名</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
        <uc1:OpenRegistedEmployeeSelector ID="OpenRegistedEmployeeSelector1" 
            runat="server" />
    </div>
</asp:Panel>
</asp:Content>

