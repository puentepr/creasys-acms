<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Person.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Person" Title="未命名頁面" %>

<%@ Register Assembly="TServerControl.Web" Namespace="TServerControl.Web" TagPrefix="TServerControl" %>

<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>

<%@ Register src="../OpenEmployeeSelector.ascx" tagname="OpenEmployeeSelector" tagprefix="uc2" %>

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
            <asp:WizardStep runat="server" title="Step 1">
                <uc1:RegistActivityQuery ID="RegistActivityQuery1" runat="server" OnGoSecondStep_Click ="GoSecondStep_Click" OnGoThirdStep_Click ="GoThirdStep_Click" 
                    TypeName="個人報名" />
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 2">
                活對資訊(含圖文附加檔案)
            </asp:WizardStep>
            <asp:WizardStep runat="server" title="Step 3">
                <asp:Label ID="Label2" runat="server" Text="Label" > </asp:Label>
                
                
                
                
                
                
                <asp:Panel ID="PanelRegisterInfo" runat="server" GroupingText="報名人事資料">
                
                <asp:Panel ID="PanelRegisterInfoA" runat="server">
                                    <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            工號:
                            <asp:Label ID="emp_idLabel" runat="server" Text='<%# Bind("emp_id") %>' />
                            &nbsp;姓名:
                            <asp:Label ID="emp_cnameLabel" runat="server" Text='<%# Bind("emp_cname") %>' />
                            &nbsp;部門:
                            <asp:Label ID="dept_nameLabel" runat="server" Text='<%# Bind("dept_name") %>' />
                            &nbsp;<asp:Button ID="btnAgent" runat="server" onclick="btnAgent_Click" 
                                Text="代理報名" />
                            <br />
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.[emp_id], A.[emp_cname],B.dept_name
FROM [UserList] A
inner join dbo.DeptList B on A.dept_id=B.dept_id
WHERE A.emp_id=@emp_id">
                        <SelectParameters>
                            <asp:Parameter Name="emp_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <uc2:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
                
                </asp:Panel>
                
                  <asp:Panel ID="PanelRegisterInfoB" runat="server">  
                      <cc1:TGridView ID="gv_Activity" runat="server" AllowHoverEffect="True" 
                          AllowHoverSelect="True" AutoGenerateColumns="False" 
                          DataSourceID="SqlDataSource3" EnableModelValidation="True" PageSize="2" 
                          ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                          TotalRowCount="0" Width="100%">
                          <Columns>
                              <asp:TemplateField>
                                  <ItemTemplate>
                                      <asp:RadioButton ID="RadioButton1" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="emp_id" HeaderText="工號" />
                              <asp:BoundField DataField="emp_cname" HeaderText="姓名" />
                              <asp:BoundField DataField="dept_name" HeaderText="部門" />
                          </Columns>
                      </cc1:TGridView>
                      <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.[emp_id], B.[emp_cname],C.dept_name
FROM [ActivityRegist] A
inner join dbo.UserList B on A.emp_id=B.emp_id
inner join dbo.DeptList C on B.dept_id=C.dept_id
WHERE (A.[activity_id] = @activity_id)
">
                          <SelectParameters>
                              <asp:Parameter Name="activity_id" Type="Int32" />
                          </SelectParameters>
                      </asp:SqlDataSource>
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
            <asp:WizardStep runat="server" Title="Step 4">
              

                
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
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

