<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity" %>

<%@ Register src="OpenUnRegistEmployeeSelector.ascx" tagname="OpenUnRegistEmployeeSelector" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label2" runat="server" Text="活動名稱:生產部聚餐"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="活動日期:2010/10/10"></asp:Label>
    <br />
    <asp:Panel ID="Panel4" runat="server" GroupingText="個人報名資料">
        <table align="center">
            <tr>
                <td>
                    姓名</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    附加檔案</td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button1" runat="server" Text="上傳" />
                </td>
            </tr>
            <tr>
                <td>
                    攜伴人數</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    費用</td>
                <td>
                    500</td>
            </tr>
            <tr>
                <td>
                    備註說明</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server" GroupingText="代理報名列表">
    <div align="right"> <asp:LinkButton ID="LinkButton1" runat="server" 
            onclick="LinkButton1_Click">新增代報人員</asp:LinkButton></div>
       
        <TServerControl:TGridView ID="GridView2" runat="server" 
            AllowHoverEffect="True" AllowHoverSelect="True" AutoGenerateColumns="False" 
            DataKeyNames="activity_id" DataSourceID="SqlDataSource1" 
            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" 
            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%">
            <Columns>
                <asp:BoundField DataField="emp_cname" HeaderText="員工姓名" 
                    SortExpression="emp_cname" />
                <asp:BoundField DataField="ext_people" HeaderText="攜伴人數" 
                    SortExpression="ext_people" />
                <asp:BoundField DataField="remark" HeaderText="備註說明" SortExpression="remark" />
                <asp:BoundField DataField="dept_name" HeaderText="部門" 
                    SortExpression="dept_name" />
                <asp:BoundField DataField="price_money" HeaderText="費用" 
                    SortExpression="price_money" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="取消報名" />
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="取消名單" />
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.*,B.emp_cname,C.dept_name,D.price_money
FROM dbo.ActivityRegist A
inner join dbo.UserList B on A.emp_id=B.emp_id 
inner join dbo.DeptList C on B.dept_id=C.dept_id
inner join dbo.ActivityPrice D on A.activity_id=D.activity_id"></asp:SqlDataSource>
        <uc1:OpenUnRegistEmployeeSelector ID="OpenUnRegistEmployeeSelector1" 
            runat="server" />
    </asp:Panel>
</asp:Content>

