<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="ActivityCheck.aspx.cs" Inherits="WebForm_ActivityCheck" Title="未命名頁面" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        

<table>
            <tr>
                <td>
                    活動名稱</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>生產部聚餐</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    活動日期</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                        TargetControlID="txtStartDate">
                    </ajaxToolkit:CalendarExtender>
                    ~<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                        TargetControlID="txtEndDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    員工工號</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    員工姓名</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="查詢" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="匯出" />
                </td>
            </tr>
        </table>
                     <asp:Panel ID="Panel1" runat="server" GroupingText="登錄狀態人員列表">
        <div align="right">
       
                        登錄狀態:<asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>已完成</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="Button3" runat="server" Text="批次更新" /></div>
         
                        <TServerControl:TGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource1" SkinID="pager" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="emp_cname" HeaderText="員工姓名" 
                                    SortExpression="emp_cname" />
                                <asp:BoundField DataField="dept_name" HeaderText="部門" 
                                    SortExpression="dept_name" />
                                <asp:BoundField DataField="check_status" HeaderText="登錄狀態" ReadOnly="True" 
                                    SortExpression="check_status" />
                            </Columns>
                        </TServerControl:TGridView>
         
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT B.emp_cname,C.dept_name,CASE A.check_status WHEN 1 THEN '已報到'  WHEN 2 THEN '已取消' WHEN 3 THEN '已完成' WHEN 4 THEN '已離職' END as check_status
FROM ActivityRegist A
inner join dbo.UserList B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id"></asp:SqlDataSource>
         
                    </asp:Panel>


</asp:Content>

