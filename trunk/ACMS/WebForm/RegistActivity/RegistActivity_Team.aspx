<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Team.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Team" Title="未命名頁面" %>
<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>
<%@ Register src="../OpenEmployeeSelector.ascx" tagname="openemployeeselector" tagprefix="uc1" %>
<%@ Register namespace="TServerControl" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center">
        <tr>
            <td>    <asp:Panel ID="Panel1" runat="server" GroupingText="團隊報名">
                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" 
                    ActiveStepIndex="0" FinishPreviousButtonText="上一步" StartNextButtonText="下一步" 
                    StepNextButtonText="下一步" StepPreviousButtonText="上一步">
                    <StartNavigationTemplate>
                        &nbsp;
                    </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" title="Step 1">
                <uc1:RegistActivityQuery ID="RegistActivityQuery1" runat="server" OnGoSecondStep_Click ="GoSecondStep_Click"
                    TypeName="團隊報名" />
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" title="Step 2">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"> </asp:Label>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            團隊名稱</td>
                        <td width="70%">
                            <asp:TextBox ID="activity_idTextBox" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            備註說明 </td>
                        <td>
                            <asp:TextBox ID="activity_idTextBox0" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelMemberList" runat="server" GroupingText="團隊成員">
                    <div align="right">
                        <asp:LinkButton ID="lbtnAddPerson" runat="server" OnClick="lbtnAddPerson_Click">新增團隊成員</asp:LinkButton>
                    </div>
                    <div align="center">
                        <cc1:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                            AllowHoverSelect="True" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource2" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                            <Columns>
                                <asp:BoundField DataField="emp_cname" HeaderText="姓名" 
                                    SortExpression="emp_cname"></asp:BoundField>
                                <asp:BoundField DataField="dept_name" HeaderText="部門" 
                                    SortExpression="dept_name"></asp:BoundField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </cc1:TGridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT B.emp_cname,C.dept_name
FROM ActivityTeamMember A
inner join dbo.UserList B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id"></asp:SqlDataSource>
                    <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
                </asp:Panel>
            </asp:WizardStep>
        </WizardSteps>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="上一步" />
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                            onclick="FinishButton_Click" Text="完成" />
                    </FinishNavigationTemplate>
    </asp:Wizard></asp:Panel></td>
        </tr>
    </table>
</asp:Content>

