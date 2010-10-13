<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Team.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Team" Title="���R�W����" %>

<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>

<%@ Register src="../OpenEmployeeSelector.ascx" tagname="OpenEmployeeSelector" tagprefix="uc2" %>
<%@ Register src="../OpenSmallEmployeeSelector.ascx" tagname="OpenSmallEmployeeSelector" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" 
                    ActiveStepIndex="0" FinishPreviousButtonText="�W�@�B" StartNextButtonText="�U�@�B" 
                    StepNextButtonText="�U�@�B" StepPreviousButtonText="�W�@�B" 
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
                            �ζ��W��</td>
                        <td width="70%">
                            <asp:TextBox ID="activity_idTextBox" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �Ƶ����� </td>
                        <td>
                            <asp:TextBox ID="activity_idTextBox0" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelMemberList" runat="server" GroupingText="�ζ�����">
                    <div align="right">
                        <asp:LinkButton ID="lbtnAddPerson" runat="server" OnClick="lbtnAddPerson_Click">�s�W�ζ�����</asp:LinkButton>
                    </div>
                    <div align="center">
                        <cc1:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                            AllowHoverSelect="True" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource2" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                            <Columns>
                                <asp:BoundField DataField="emp_cname" HeaderText="�m�W" 
                                    SortExpression="emp_cname"></asp:BoundField>
                                <asp:BoundField DataField="dept_name" HeaderText="����" 
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
                    TypeName="�ζ����W" />
            </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
            <div align="center">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#FF0066" Text="�M�ҹA���@��C"></asp:Label>
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Sample.jpg" />
                &nbsp;</div>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep3" runat="server" title="Step 3">
                <asp:Label ID="Label2" runat="server" Text="Label" > </asp:Label>
                
                
                
                
                
                
                
                                <asp:Panel ID="PanelActivityInfo0" runat="server" 
                    GroupingText="���ʬ�����T">
                                    <asp:FormView ID="FormView3" runat="server" DataSourceID="SqlDataSource2" 
                                        Width="700px">
                                        <EditItemTemplate>
                                            id:
                                            <asp:TextBox ID="idTextBox0" runat="server" Text='<%# Bind("id") %>' />
                                            <br />
                                            activity_name:
                                            <asp:TextBox ID="activity_nameTextBox0" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            people_type:
                                            <asp:TextBox ID="people_typeTextBox0" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            activity_date:
                                            <asp:TextBox ID="activity_dateTextBox0" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            limit_count:
                                            <asp:TextBox ID="limit_countTextBox0" runat="server" 
                                                Text='<%# Bind("limit_count") %>' />
                                            <br />
                                            limit2_count:
                                            <asp:TextBox ID="limit2_countTextBox0" runat="server" 
                                                Text='<%# Bind("limit2_count") %>' />
                                            <br />
                                            team_member_max:
                                            <asp:TextBox ID="team_member_maxTextBox0" runat="server" 
                                                Text='<%# Bind("team_member_max") %>' />
                                            <br />
                                            team_member_min:
                                            <asp:TextBox ID="team_member_minTextBox0" runat="server" 
                                                Text='<%# Bind("team_member_min") %>' />
                                            <br />
                                            regist_deadline:
                                            <asp:TextBox ID="regist_deadlineTextBox0" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            cancelregist_deadline:
                                            <asp:TextBox ID="cancelregist_deadlineTextBox0" runat="server" 
                                                Text='<%# Bind("cancelregist_deadline") %>' />
                                            <br />
                                            is_showfile:
                                            <asp:CheckBox ID="is_showfileCheckBox0" runat="server" 
                                                Checked='<%# Bind("is_showfile") %>' />
                                            <br />
                                            is_showprogress:
                                            <asp:CheckBox ID="is_showprogressCheckBox0" runat="server" 
                                                Checked='<%# Bind("is_showprogress") %>' />
                                            <br />
                                            is_showextpeoplecount:
                                            <asp:CheckBox ID="is_showextpeoplecountCheckBox0" runat="server" 
                                                Checked='<%# Bind("is_showextpeoplecount") %>' />
                                            <br />
                                            is_showremark:
                                            <asp:CheckBox ID="is_showremarkCheckBox0" runat="server" 
                                                Checked='<%# Bind("is_showremark") %>' />
                                            <br />
                                            grouplimit:
                                            <asp:TextBox ID="grouplimitTextBox0" runat="server" 
                                                Text='<%# Bind("grouplimit") %>' />
                                            <br />
                                            acept_count:
                                            <asp:TextBox ID="acept_countTextBox0" runat="server" 
                                                Text='<%# Bind("acept_count") %>' />
                                            <br />
                                            register_count:
                                            <asp:TextBox ID="register_countTextBox0" runat="server" 
                                                Text='<%# Bind("register_count") %>' />
                                            <br />
                                            is_full:
                                            <asp:TextBox ID="is_fullTextBox0" runat="server" 
                                                Text='<%# Bind("is_full") %>' />
                                            <br />
                                            <asp:LinkButton ID="UpdateButton0" runat="server" CausesValidation="True" 
                                                CommandName="Update" Text="��s" />
                                            &nbsp;<asp:LinkButton ID="UpdateCancelButton0" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="����" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            id:
                                            <asp:TextBox ID="idTextBox1" runat="server" Text='<%# Bind("id") %>' />
                                            <br />
                                            activity_name:
                                            <asp:TextBox ID="activity_nameTextBox1" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            people_type:
                                            <asp:TextBox ID="people_typeTextBox1" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            activity_date:
                                            <asp:TextBox ID="activity_dateTextBox1" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            limit_count:
                                            <asp:TextBox ID="limit_countTextBox1" runat="server" 
                                                Text='<%# Bind("limit_count") %>' />
                                            <br />
                                            limit2_count:
                                            <asp:TextBox ID="limit2_countTextBox1" runat="server" 
                                                Text='<%# Bind("limit2_count") %>' />
                                            <br />
                                            team_member_max:
                                            <asp:TextBox ID="team_member_maxTextBox1" runat="server" 
                                                Text='<%# Bind("team_member_max") %>' />
                                            <br />
                                            team_member_min:
                                            <asp:TextBox ID="team_member_minTextBox1" runat="server" 
                                                Text='<%# Bind("team_member_min") %>' />
                                            <br />
                                            regist_deadline:
                                            <asp:TextBox ID="regist_deadlineTextBox1" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            cancelregist_deadline:
                                            <asp:TextBox ID="cancelregist_deadlineTextBox1" runat="server" 
                                                Text='<%# Bind("cancelregist_deadline") %>' />
                                            <br />
                                            is_showfile:
                                            <asp:CheckBox ID="is_showfileCheckBox1" runat="server" 
                                                Checked='<%# Bind("is_showfile") %>' />
                                            <br />
                                            is_showprogress:
                                            <asp:CheckBox ID="is_showprogressCheckBox1" runat="server" 
                                                Checked='<%# Bind("is_showprogress") %>' />
                                            <br />
                                            is_showextpeoplecount:
                                            <asp:CheckBox ID="is_showextpeoplecountCheckBox1" runat="server" 
                                                Checked='<%# Bind("is_showextpeoplecount") %>' />
                                            <br />
                                            is_showremark:
                                            <asp:CheckBox ID="is_showremarkCheckBox1" runat="server" 
                                                Checked='<%# Bind("is_showremark") %>' />
                                            <br />
                                            grouplimit:
                                            <asp:TextBox ID="grouplimitTextBox1" runat="server" 
                                                Text='<%# Bind("grouplimit") %>' />
                                            <br />
                                            acept_count:
                                            <asp:TextBox ID="acept_countTextBox1" runat="server" 
                                                Text='<%# Bind("acept_count") %>' />
                                            <br />
                                            register_count:
                                            <asp:TextBox ID="register_countTextBox1" runat="server" 
                                                Text='<%# Bind("register_count") %>' />
                                            <br />
                                            is_full:
                                            <asp:TextBox ID="is_fullTextBox1" runat="server" 
                                                Text='<%# Bind("is_full") %>' />
                                            <br />
                                            <asp:LinkButton ID="InsertButton0" runat="server" CausesValidation="True" 
                                                CommandName="Insert" Text="���J" />
                                            &nbsp;<asp:LinkButton ID="InsertCancelButton0" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="����" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            ���ʦW��:
                                            <asp:Label ID="activity_nameLabel0" runat="server" 
                                                Text='<%# Bind("activity_name") %>' />
                                            <br />
                                            ���ʹ�H:
                                            <asp:Label ID="people_typeLabel0" runat="server" 
                                                Text='<%# Bind("people_type") %>' />
                                            <br />
                                            ���ʤ��:
                                            <asp:Label ID="activity_dateLabel0" runat="server" 
                                                Text='<%# Bind("activity_date") %>' />
                                            <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                            ���W�I���:
                                            <asp:Label ID="regist_deadlineLabel0" runat="server" 
                                                Text='<%# Bind("regist_deadline") %>' />
                                            <br />
                                            �������W�I���:
                                            <asp:Label ID="cancelregist_deadlineLabel0" runat="server" 
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
                
                
                
                
                
                
                <asp:Panel ID="PanelRegisterInfo" runat="server" GroupingText="�ζ��T�w���">
                
                <asp:Panel ID="PanelRegisterInfoA" runat="server">
                                  <table align="center">
                    <tr>
                        <td>
                            �ζ��W��</td>
                        <td width="70%">
                            <asp:TextBox ID="activity_idTextBox" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �Ƶ����� </td>
                        <td>
                            <asp:TextBox ID="activity_idTextBox0" runat="server" 
                                Text='<%# Bind("activity_id") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelMemberList" runat="server" GroupingText="�ζ�����">
                    <div align="right">
                        <asp:LinkButton ID="lbtnAddPerson" runat="server" OnClick="lbtnAddPerson_Click">�s�W�ζ�����</asp:LinkButton>
                    </div>
                    <div align="center">
                        <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                            AllowHoverSelect="True" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource1" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                            <Columns>
                                <asp:BoundField DataField="NATIVE_NAME" HeaderText="�m�W" 
                                    SortExpression="NATIVE_NAME"></asp:BoundField>
                                <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="����" 
                                    SortExpression="C_DEPT_ABBR"></asp:BoundField>
                                                          <asp:TemplateField HeaderText="�O�_�w��g�ӤH���">
                                    <ItemTemplate>
                                       �O
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">�s��ӤH�������</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </TServerControl:TGridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT B.NATIVE_NAME,B.C_DEPT_ABBR
FROM ActivityTeamMember A
inner join dbo.V_ACSM_USER B on A.emp_id=B.ID
"></asp:SqlDataSource>
                    <uc3:OpenSmallEmployeeSelector ID="OpenSmallEmployeeSelector1" runat="server" />
                </asp:Panel>

                
                </asp:Panel>
                
              
                  

                    
                  
                    
                    
                    
                </asp:Panel>
                
                
                
                
                
                
                
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 4">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 5" StepType="Finish">
                �`�N�ƶ�<br />
                <br />
                �Ц۳Ƨŧů���
            </asp:WizardStep>
            <asp:TemplatedWizardStep runat="server" ID="WizardStep4" Title="WizardStep4">
                <CustomNavigationTemplate>
                    <asp:Button ID="Button1" runat="server" Text="�����ӤH��ƶ�g" 
                        onclick="Button1_Click" />
                </CustomNavigationTemplate>
                <ContentTemplate>
                    �����Ҧr��<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <br />
                    �Ƶ�<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    &nbsp;
                </ContentTemplate>
            </asp:TemplatedWizardStep>
        </WizardSteps>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="�W�@�B" />
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                            onclick="FinishButton_Click" Text="����" />
                    </FinishNavigationTemplate>
                    <StepNavigationTemplate>
                        <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="�W�@�B" />
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" 
                            Text="�U�@�B" />
                    </StepNavigationTemplate>
    </asp:Wizard>
</asp:Content>

