<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RegistActivityTeam.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivityTeam" %>

<%@ Register Src="../OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelA" runat="server" GroupingText="團隊報名">
        <table align="center">
            <tr>
                <td>
                    活動名稱:
                </td>
                <td width="70%">
                    生產部聚餐
                </td>
            </tr>
            <tr>
                <td>
                    活動日期:
                </td>
                <td width="70%">
                    2010/10/10
                </td>
            </tr>
            <tr>
                <td>
                    報名團隊
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>名人隊</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    是否新增團隊
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="新增團隊" />
                </td>
            </tr>
        </table>
            </asp:Panel>
        <asp:Panel ID="PanelB" runat="server" GroupingText="新增團隊資訊" Visible="False">
            <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" DataSourceID="SqlDataSource1"
                DefaultMode="Insert" Width="100%">
                <edititemtemplate>
                       id:
                       <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                       <br />
                       activity_id:
                       <asp:TextBox ID="activity_idTextBox" runat="server" 
                           Text='<%# Bind("activity_id") %>' />
                       <br />
                       team_name:
                       <asp:TextBox ID="team_nameTextBox" runat="server" 
                           Text='<%# Bind("team_name") %>' />
                       <br />
                       remark:
                       <asp:TextBox ID="remarkTextBox" runat="server" Text='<%# Bind("remark") %>' />
                       <br />
                       <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                           CommandName="Update" Text="更新" />
                       &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                           CausesValidation="False" CommandName="Cancel" Text="取消" />
                   </edititemtemplate>
                <insertitemtemplate>
                <table align="center">

                    <tr>
                        <td>
                            團隊名稱</td>
                        <td width="70%">
                            <asp:TextBox ID="activity_idTextBox" runat="server" 
                                Text='<%# Bind("activity_id") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            附加檔案 </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="Button2" runat="server" Text="上傳" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            備註說明 </td>
                        <td>
                            <asp:TextBox ID="activity_idTextBox0" runat="server" 
                                Text='<%# Bind("activity_id") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="加入此團隊" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="Button3" runat="server" Text="新增" />
                        </td>
                    </tr>
                </table>
            </insertitemtemplate>
                <itemtemplate>
                       id:
                       <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                       <br />
                       activity_id:
                       <asp:Label ID="activity_idLabel" runat="server" 
                           Text='<%# Bind("activity_id") %>' />
                       <br />
                       team_name:
                       <asp:Label ID="team_nameLabel" runat="server" Text='<%# Bind("team_name") %>' />
                       <br />
                       remark:
                       <asp:Label ID="remarkLabel" runat="server" Text='<%# Bind("remark") %>' />
                       <br />
                   </itemtemplate>
            </asp:FormView>
            <div align="center">
                <TServerControl:TGridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="id" DataSourceID="SqlDataSource1" SkinID="pager">
                    <Columns>
                        <asp:BoundField DataField="team_name" HeaderText="團隊名稱" SortExpression="team_name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server"> 下載</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="remark" HeaderText="備註說明" SortExpression="remark" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"> 編輯隊員</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="取消報名" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox5" runat="server" Text="取消隊伍" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox4" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                SelectCommand="SELECT * FROM [ActivityTeam]"></asp:SqlDataSource>
        </asp:Panel>

    <div align="center">
        <asp:Panel ID="PanelMember" runat="server" Visible="False">
            <table align="center">
                <tr>
                    <td>
                        活動名稱:
                    </td>
                    <td width="70%">
                        生產部聚餐
                    </td>
                </tr>
                <tr>
                    <td>
                        團隊名稱
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>名人隊</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PanelMemberList" runat="server" GroupingText="團隊成員">
                <div align="right">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton1_Click1">新增團隊成員</asp:LinkButton>
                </div>
                <div align="center">
                    <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                        AutoGenerateColumns="False" DataSourceID="SqlDataSource2" ShowFooterWhenEmpty="False"
                        ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                        <Columns>
                            <asp:BoundField DataField="emp_cname" HeaderText="姓名" SortExpression="emp_cname" />
                            <asp:BoundField DataField="dept_name" HeaderText="部門" SortExpression="dept_name" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="取消報名" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox7" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox8" runat="server" Text="取消名單" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox9" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                </div>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                    SelectCommand="SELECT B.emp_cname,C.dept_name
FROM ActivityTeamMember A
inner join dbo.UserList B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id"></asp:SqlDataSource>
                <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
            </asp:Panel>
        </asp:Panel>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="上一步" 
            Visible="False" />
        <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="下一步" />
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="送出" />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="取消" />
    </div>
</asp:Content>
