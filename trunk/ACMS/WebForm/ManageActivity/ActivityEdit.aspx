<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityEdit.aspx.cs" Inherits="WebForm_ManageActivity_ActivityEdit" %>

<%@ Register Src="../OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table align="center" width="100%">
        <tr>
            <td>
            <asp:Panel ID="Panel1" runat="server" GroupingText="新增活動">
                <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" DataSourceID="SqlDataSource1"
                    EnableModelValidation="True" Width="100%">
                    <ItemTemplate>
                        <br />
                        <table align="center">
                            <tr>
                                <td>
                                    活動名稱
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("activity_name") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動對象
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("people_type") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtactivity_date" runat="server" Text='<%# Bind("activity_date") %>'></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtactivity_date">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動人數上限
                                </td>
                                <td>
                                    <asp:TextBox ID="peoplelimit_countTextBox" runat="server" Text='<%# Bind("peoplelimit_count") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動備取人數
                                </td>
                                <td>
                                    <asp:TextBox ID="peoplelimit2_countTextBox" runat="server" Text='<%# Bind("peoplelimit2_count") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    報名截止日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtregist_deadline" runat="server" Text='<%# Bind("regist_deadline") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtregist_deadline">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    取消報名截止日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcancelregist_deadline" runat="server" Text='<%# Bind("cancelregist_deadline") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtcancelregist_deadline">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否顯示附加檔案
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("is_showfile") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    附加檔案</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                    <asp:Button ID="Button6" runat="server" Text="上傳" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否顯示活動進度
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("is_showprogress") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    族群限定
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("grouplimit") %>'>
                                        <asp:ListItem Value="N">否</asp:ListItem>
                                        <asp:ListItem Value="Y">是</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    報名者自填欄位</td>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4">
                                        <asp:ListItem>攜伴人數</asp:ListItem>
                                        <asp:ListItem>姓名</asp:ListItem>
                                        <asp:ListItem>性別</asp:ListItem>
                                        <asp:ListItem>身分證字號</asp:ListItem>
                                        <asp:ListItem>備註</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動費用設定
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td nowrap="nowrap">
                                                <asp:CheckBox ID="CheckBox5" runat="server" Text="報名費" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                元
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CheckBox6" runat="server" Text="材料費" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                元
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CheckBox7" runat="server" Text="其他" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                元
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="族群限定名單">
                                        檔案上傳<asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="Button1" runat="server" Text="上傳" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button3" runat="server" Text="新增族群" OnClick="Button3_Click" />
                                        <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                                            AutoGenerateColumns="False" DataKeyNames="emp_id" DataSourceID="SqlDataSource1"
                                            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                            SkinID="pager" TotalRowCount="0" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="emp_id" HeaderText="員工編號" ReadOnly="True" SortExpression="emp_id" />
                                                <asp:BoundField DataField="emp_cname" HeaderText="姓名" SortExpression="emp_cname" />
                                                <asp:BoundField DataField="dept_name" HeaderText="部門" SortExpression="dept_name" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server">刪除</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </TServerControl:TGridView>
                                        &nbsp;&nbsp;
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                                            SelectCommand="SELECT B.emp_id,B.emp_cname,C.dept_name
FROM ActivityGroupLimit A
inner join UserList B on A.emp_id=B.emp_id
inner join DeptList C on B.dept_id=C.dept_id
WHERE A.activity_id=1"></asp:SqlDataSource>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="存檔" />
                                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="取消" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                    SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
                <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
          </asp:Panel>  </td>
        </tr>
    </table>
</asp:Content>
