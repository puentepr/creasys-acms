<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Team.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Team" Title="未命名頁面" %>

<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>

<%@ Register src="../OpenEmployeeSelector.ascx" tagname="OpenEmployeeSelector" tagprefix="uc2" %>
<%@ Register namespace="TServerControl" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" 
                    ActiveStepIndex="0" FinishPreviousButtonText="上一步" StartNextButtonText="下一步" 
                    StepNextButtonText="下一步" StepPreviousButtonText="上一步" 
                    >
                    <StartNavigationTemplate>
                        &nbsp;
                    </StartNavigationTemplate>
        <WizardSteps>
            <%--<asp:WizardStep ID="WizardStep2" runat="server" title="Step 2">
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
inner join dbo.V_ACSM_USER B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id"></asp:SqlDataSource>
                    <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
                </asp:Panel>
            </asp:WizardStep>--%><asp:WizardStep ID="WizardStep1" runat="server" title="Step 1">
                <uc1:RegistActivityQuery ID="RegistActivityQuery1" runat="server" OnGoSecondStep_Click ="GoSecondStep_Click"
                    TypeName="團隊報名" />
            </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
            <div align="center">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#FF0066" Text="清境農場一日遊"></asp:Label>
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Sample.jpg" />
                &nbsp;</div>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep3" runat="server" title="Step 3" 
                StepType="Finish">
                <asp:Label ID="Label2" runat="server" Text="Label" > </asp:Label>
                
                
                
                
                
                
                <asp:Panel ID="PanelRegisterInfo" runat="server" GroupingText="報名人事資料">
                
                <asp:Panel ID="PanelRegisterInfoA" runat="server">
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
                            DataSourceID="SqlDataSource1" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                            <Columns>
                                <asp:BoundField DataField="emp_cname" HeaderText="姓名" 
                                    SortExpression="emp_cname"></asp:BoundField>
                                <asp:BoundField DataField="dept_name" HeaderText="部門" 
                                    SortExpression="dept_name"></asp:BoundField>
                                                          <asp:TemplateField HeaderText="是否已填寫個人資料">
                                    <ItemTemplate>
                                       是
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">編輯個人相關欄位</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </cc1:TGridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT B.emp_cname,C.dept_name
FROM ActivityTeamMember A
inner join dbo.V_ACSM_USER B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id"></asp:SqlDataSource>
                    <uc2:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
                </asp:Panel>

                
                </asp:Panel>
                
              
                  

                    
                  
                    
                    
                    
                </asp:Panel>
                
                
                
                
                
                
                
                                <asp:Panel ID="PanelActivityInfo" runat="server" GroupingText="活動相關資訊">
                                    <asp:FormView ID="FormView2" runat="server" DataSourceID="SqlDataSource2">
                                        <EditItemTemplate>
                                            id:
                                            <asp:TextBox ID="idTextBox" runat="server" Text='<%# Bind("id") %>' />
                                            <br />
                                            activity_name:
                                            <asp:TextBox ID="activity_nameTextBox" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            people_type:
                                            <asp:TextBox ID="people_typeTextBox" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            activity_date:
                                            <asp:TextBox ID="activity_dateTextBox" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            limit_count:
                                            <asp:TextBox ID="limit_countTextBox" runat="server" 
                                                Text='<%# Bind("limit_count") %>' />
                                            <br />
                                            limit2_count:
                                            <asp:TextBox ID="limit2_countTextBox" runat="server" 
                                                Text='<%# Bind("limit2_count") %>' />
                                            <br />
                                            team_member_max:
                                            <asp:TextBox ID="team_member_maxTextBox" runat="server" 
                                                Text='<%# Bind("team_member_max") %>' />
                                            <br />
                                            team_member_min:
                                            <asp:TextBox ID="team_member_minTextBox" runat="server" 
                                                Text='<%# Bind("team_member_min") %>' />
                                            <br />
                                            regist_deadline:
                                            <asp:TextBox ID="regist_deadlineTextBox" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            cancelregist_deadline:
                                            <asp:TextBox ID="cancelregist_deadlineTextBox" runat="server" 
                                                Text='<%# Bind("cancelregist_deadline") %>' />
                                            <br />
                                            is_showfile:
                                            <asp:CheckBox ID="is_showfileCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showfile") %>' />
                                            <br />
                                            is_showprogress:
                                            <asp:CheckBox ID="is_showprogressCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showprogress") %>' />
                                            <br />
                                            is_showextpeoplecount:
                                            <asp:CheckBox ID="is_showextpeoplecountCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showextpeoplecount") %>' />
                                            <br />
                                            is_showremark:
                                            <asp:CheckBox ID="is_showremarkCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showremark") %>' />
                                            <br />
                                            grouplimit:
                                            <asp:TextBox ID="grouplimitTextBox" runat="server" 
                                                Text='<%# Bind("grouplimit") %>' />
                                            <br />
                                            acept_count:
                                            <asp:TextBox ID="acept_countTextBox" runat="server" 
                                                Text='<%# Bind("acept_count") %>' />
                                            <br />
                                            register_count:
                                            <asp:TextBox ID="register_countTextBox" runat="server" 
                                                Text='<%# Bind("register_count") %>' />
                                            <br />
                                            is_full:
                                            <asp:TextBox ID="is_fullTextBox" runat="server" Text='<%# Bind("is_full") %>' />
                                            <br />
                                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                                CommandName="Update" Text="更新" />
                                            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="取消" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            id:
                                            <asp:TextBox ID="idTextBox" runat="server" Text='<%# Bind("id") %>' />
                                            <br />
                                            activity_name:
                                            <asp:TextBox ID="activity_nameTextBox" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            people_type:
                                            <asp:TextBox ID="people_typeTextBox" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            activity_date:
                                            <asp:TextBox ID="activity_dateTextBox" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            limit_count:
                                            <asp:TextBox ID="limit_countTextBox" runat="server" 
                                                Text='<%# Bind("limit_count") %>' />
                                            <br />
                                            limit2_count:
                                            <asp:TextBox ID="limit2_countTextBox" runat="server" 
                                                Text='<%# Bind("limit2_count") %>' />
                                            <br />
                                            team_member_max:
                                            <asp:TextBox ID="team_member_maxTextBox" runat="server" 
                                                Text='<%# Bind("team_member_max") %>' />
                                            <br />
                                            team_member_min:
                                            <asp:TextBox ID="team_member_minTextBox" runat="server" 
                                                Text='<%# Bind("team_member_min") %>' />
                                            <br />
                                            regist_deadline:
                                            <asp:TextBox ID="regist_deadlineTextBox" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            cancelregist_deadline:
                                            <asp:TextBox ID="cancelregist_deadlineTextBox" runat="server" 
                                                Text='<%# Bind("cancelregist_deadline") %>' />
                                            <br />
                                            is_showfile:
                                            <asp:CheckBox ID="is_showfileCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showfile") %>' />
                                            <br />
                                            is_showprogress:
                                            <asp:CheckBox ID="is_showprogressCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showprogress") %>' />
                                            <br />
                                            is_showextpeoplecount:
                                            <asp:CheckBox ID="is_showextpeoplecountCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showextpeoplecount") %>' />
                                            <br />
                                            is_showremark:
                                            <asp:CheckBox ID="is_showremarkCheckBox" runat="server" 
                                                Checked='<%# Bind("is_showremark") %>' />
                                            <br />
                                            grouplimit:
                                            <asp:TextBox ID="grouplimitTextBox" runat="server" 
                                                Text='<%# Bind("grouplimit") %>' />
                                            <br />
                                            acept_count:
                                            <asp:TextBox ID="acept_countTextBox" runat="server" 
                                                Text='<%# Bind("acept_count") %>' />
                                            <br />
                                            register_count:
                                            <asp:TextBox ID="register_countTextBox" runat="server" 
                                                Text='<%# Bind("register_count") %>' />
                                            <br />
                                            is_full:
                                            <asp:TextBox ID="is_fullTextBox" runat="server" Text='<%# Bind("is_full") %>' />
                                            <br />
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                                CommandName="Insert" Text="插入" />
                                            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="取消" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            活動名稱:
                                            <asp:Label ID="activity_nameLabel" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            活動對象:
                                            <asp:Label ID="people_typeLabel" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            活動日期:
                                            <asp:Label ID="activity_dateLabel" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            報名截止日期:
                                            <asp:Label ID="regist_deadlineLabel" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            取消報名截止日期:
                                            <asp:Label ID="cancelregist_deadlineLabel" runat="server" 
                                                Text='<%# Bind("cancelregist_deadline") %>' />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:FormView>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:connStr %>" 
                                        SelectCommand="SELECT * FROM [Activity] WHERE ([id] = @id)">
                                        <SelectParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                </asp:Panel>
            </asp:WizardStep>
            <asp:TemplatedWizardStep runat="server" ID="WizardStep4" Title="WizardStep4">
                <CustomNavigationTemplate>
                    <asp:Button ID="Button1" runat="server" Text="完成個人資料填寫" 
                        onclick="Button1_Click" />
                </CustomNavigationTemplate>
                <ContentTemplate>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:TemplatedWizardStep>
        </WizardSteps>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="上一步" />
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                            onclick="FinishButton_Click" Text="完成" />
                    </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>

