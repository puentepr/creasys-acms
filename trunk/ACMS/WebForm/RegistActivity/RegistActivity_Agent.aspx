<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Agent.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Agent" Title="未命名頁面" %>
<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>
<%@ Register src="OpenUnRegistEmployeeSelector.ascx" tagname="openunregistemployeeselector" tagprefix="uc1" %>
<%@ Register namespace="TServerControl" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" 
                    ActiveStepIndex="0" FinishPreviousButtonText="上一步" StartNextButtonText="下一步" 
                    StepNextButtonText="下一步" StepPreviousButtonText="上一步">
                    <StartNavigationTemplate>
                        &nbsp;
                    </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" title="Step 1">
                <uc1:RegistActivityQuery ID="RegistActivityQuery1" runat="server" OnGoSecondStep_Click ="GoSecondStep_Click"
                    TypeName="代理報名" />
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" title="Step 2">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"> </asp:Label>
    
                
                    <div align="right">
                        <asp:LinkButton ID="lbtnAddPerson" runat="server" OnClick="lbtnAddPerson_Click">新增代報人員</asp:LinkButton>
                    </div>
                    <cc1:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                        AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="activity_id" 
                        DataSourceID="SqlDataSource1" EnableModelValidation="True" PageSize="2" 
                        ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                        TotalRowCount="0" Width="100%">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="emp_cname" HeaderText="員工姓名" 
                                SortExpression="emp_cname"></asp:BoundField>
                            <asp:BoundField DataField="ext_people" HeaderText="攜伴人數" 
                                SortExpression="ext_people"></asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註說明" SortExpression="remark">
                            </asp:BoundField>
                            <asp:BoundField DataField="dept_name" HeaderText="部門" 
                                SortExpression="dept_name"></asp:BoundField>
                            <asp:BoundField DataField="price_money" HeaderText="費用" 
                                SortExpression="price_money"></asp:BoundField>
                           
                            <asp:CommandField ShowDeleteButton="True" />
                           
                        </Columns>
                    </cc1:TGridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.*,B.emp_cname,C.dept_name,D.price_money
FROM dbo.ActivityRegist A
inner join dbo.UserList B on A.emp_id=B.emp_id 
inner join dbo.DeptList C on B.dept_id=C.dept_id
inner join dbo.ActivityPrice D on A.activity_id=D.activity_id"></asp:SqlDataSource>
                    <uc1:OpenUnRegistEmployeeSelector ID="OpenUnRegistEmployeeSelector1" 
                        runat="server" />
             
            </asp:WizardStep>
        </WizardSteps>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="上一步" />
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                            onclick="FinishButton_Click" Text="完成" />
                    </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>

