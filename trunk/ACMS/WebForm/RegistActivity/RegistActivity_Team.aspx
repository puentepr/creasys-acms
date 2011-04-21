<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="RegistActivity_Team.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Team"
    Title="團體報名" %>

<%@ Register Src="RegistActivity_Query.ascx" TagName="RegistActivity_Query" TagPrefix="uc1" %>
<%@ Register Src="OpenAgentSelector.ascx" TagName="OpenAgentSelector" TagPrefix="uc4" %>
<%@ Register Src="../DatetimePicker.ascx" TagName="DatetimePicker" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="OpenTeamMemberSelector.ascx" TagName="OpenTeamMemberSelector" TagPrefix="uc2" %>
<%@ Register Src="OpenTeamPersonInfo.ascx" TagName="OpenTeamPersonInfo" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:HiddenField ID="hiMode1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc1:RegistActivity_Query ID="RegistActivity_Query1" runat="server" OnGoSecondStep_Click="GoSecondStep_Click"
                ActivityType="2" />
            <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" ActiveStepIndex="1"
                FinishPreviousButtonText="上一步" StartNextButtonText="下一步" StepNextButtonText="下一步"
                StepPreviousButtonText="上一步" 
                onactivestepchanged="Wizard1_ActiveStepChanged" 
                onnextbuttonclick="Wizard1_NextButtonClick">
                <StartNavigationTemplate>
                    <asp:Button ID="btnStart" runat="server" CausesValidation="true" CommandName="MoveNext"
                        CssClass="WizardControlButton" Text="下一步" />
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <asp:Button ID="btnPrevious" runat="server" CommandName="MovePrevious" CssClass="WizardControlButton"
                        Text="上一步" />
                    <asp:Button ID="btnNext" runat="server" CausesValidation="true" CommandName="MoveNext"
                        CssClass="WizardControlButton" Text="下一步" ValidationGroup="WizardNext" OnClick="btnNext_Click" />
                </StepNavigationTemplate>
                <WizardSteps>
                    <asp:WizardStep runat="server" Title="Step 1" StepType="Start">
                        <div align="center">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Step 2">
                        <asp:Panel ID="PanelActivityInfo" runat="server" GroupingText="活動相關資訊" Width="800px">
                            <asp:FormView ID="FormView_ActivatyDetails" runat="server" DataSourceID="ObjectDataSource_ActivatyDetails"
                                Width="700px" OnDataBound="FormView_ActivatyDetails_DataBound">
                                <ItemTemplate>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                活動主辦單位
                                            </td>
                                            <td>
                                                <asp:Label ID="org_idLabel" runat="server" Text='<%# Bind("UnitName") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                活動名稱&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="activity_nameLabel" runat="server" Text='<%# Bind("activity_name") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                活動對象
                                            </td>
                                            <td>
                                                <asp:Label ID="people_typeLabel" runat="server" Text='<%# Bind("people_type") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                活動日期
                                            </td>
                                            <td>
                                                <asp:Label ID="activity_startdateLabel" runat="server" Text='<%# Bind("activity_startdate","{0:s}") %>' />
                                                ~<asp:Label ID="activity_enddateLabel" runat="server" Text='<%# Bind("activity_enddate","{0:s}") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                活動隊數上限
                                            </td>
                                            <td>
                                                <asp:Label ID="limit_countLabel" runat="server" Text='<%# Bind("limit_count") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                活動備取隊數
                                            </td>
                                            <td>
                                                <asp:Label ID="limit2_countLabel" runat="server" Text='<%# Bind("limit2_count") %>' />
                                            </td>
                                        </tr>
                                        <tr id="trteam_member_max" runat="server">
                                            <td>
                                                每隊人數限制
                                            </td>
                                            <td>
                                                <asp:Label ID="team_member_minLabel" runat="server" Text='<%# Bind("team_member_min") %>' />
                                                ~<asp:Label ID="team_member_maxLabel" runat="server" Text='<%# Bind("team_member_max") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 23px">
                                                報名日期
                                            </td>
                                            <td style="height: 23px">
                                                <asp:Label ID="regist_startdateLabel" runat="server" Text='<%# Bind("regist_startdate","{0:d}") %>' />
                                                ~<asp:Label ID="regist_deadlineLabel" runat="server" Text='<%# Bind("regist_deadline","{0:d}") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                取消報名截止日
                                            </td>
                                            <td>
                                                <asp:Label ID="cancelregist_deadlineLabel" runat="server" Text='<%# Bind("cancelregist_deadline","{0:d}") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <TServerControl:TGridView ID="GridView_UpFiles" runat="server" AllowHoverEffect="True"
                                                    AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="path" DataSourceID="ObjectDataSource_UpFiles"
                                                    EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                                    SkinID="pager" TotalRowCount="0" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="name" HeaderText="活動資料下載" SortExpression="name" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnFileDownload" runat="server" CommandArgument='<%# Eval("path") %>'
                                                                    OnClick="lbtnFileDownload_Click">下載</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </TServerControl:TGridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </ItemTemplate>
                            </asp:FormView>
                            <asp:ObjectDataSource ID="ObjectDataSource_ActivatyDetails" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="SelectActivatyDTByID" TypeName="ACMS.BO.ActivatyBO">
                                <SelectParameters>
                                    <asp:Parameter DbType="Guid" Name="id" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSource_UpFiles" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="SELECT" TypeName="ACMS.BO.UpFileBO">
                                <SelectParameters>
                                    <asp:Parameter Name="dirName" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                        <asp:Panel ID="Panel_TeamFix" runat="server" GroupingText="團隊/隊名" Width="800px">
                            <table align="center">
                                <tr id="tr_showteam_fix1" runat="server">
                                    <td>
                                        團隊名稱
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox ID="txtteam_name" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="chk_txtteam_name" runat="server" ControlToValidate="txtteam_name"
                                            Display="Dynamic" ErrorMessage="團隊名稱必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="tr_showteam_fix2" runat="server">
                                    <td>
                                        攜伴人數
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtext_people" runat="server" MaxLength="50" Width="50px"></asp:TextBox>
                                        (<asp:Label ID="lbltext_peopleStart" runat="server" Text="Label"></asp:Label>
                                        ~<asp:Label ID="lbltext_peopleEnd" runat="server" Text="Label"></asp:Label>
                                        )<asp:RequiredFieldValidator ID="chk_text_people" runat="server" ControlToValidate="txtext_people"
                                            Display="Dynamic" ErrorMessage="攜伴人數限制必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="chk_text_people2" runat="server" ControlToValidate="txtext_people"
                                            Display="Dynamic" ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer"
                                            ValidationGroup="WizardNext"></asp:CompareValidator>
                                        <asp:RangeValidator ID="chk_text_people3" runat="server" ControlToValidate="txtext_people"
                                            Display="Dynamic" ErrorMessage="攜伴人數必在限制範圍內" Type="Integer" ValidationGroup="WizardNext"></asp:RangeValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="PanelTeamMember" runat="server" GroupingText="團隊成員" Width="800px">
                                    <asp:Panel ID="PanelTeamMenberInfoA" runat="server" Width="800px">
                                        <div align="right">
                                            <asp:Button ID="btnAddTeamMember" runat="server" OnClick="btnAgent_Click" Text="新增團隊成員" />
                                            <uc2:OpenTeamMemberSelector ID="OpenTeamMemberSelector1" OnGetEmployeesClick="GetEmployees_Click"
                                                runat="server" Visible="False" />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelTeamMenberInfoB" runat="server" Width="800px">
                                        <TServerControl:TGridView ID="GridView_TemMember" runat="server" AllowHoverEffect="True"
                                            AllowHoverSelect="True" AutoGenerateColumns="False" EnableModelValidation="True"
                                            ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0"
                                            Width="100%" DataKeyNames="emp_id" OnRowDataBound="GridView_TemMember_RowDataBound"
                                            OnDataBound="GridView_TemMember_DataBound">
                                            <Columns>
                                                <asp:BoundField DataField="WORK_ID" HeaderText="工號" />
                                                <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" />
                                                <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" />
                                                <asp:BoundField DataField="WritePersonInfo" HeaderText="是否已填寫個人資料">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnVOedit" runat="server" OnClick="lbtnVOedit_Click">編輯個人報名資料</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnVOdelete" runat="server" CausesValidation="False" OnClick="lbtnVOdelete_Click"
                                                            Text="刪除"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </TServerControl:TGridView>
                                        <uc5:OpenTeamPersonInfo ID="OpenTeamPersonInfo1" runat="server" 
                                            OnGetTeamPersonInfoClick="GetTeamPersonInfo_Click" Visible="False" />
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Step 3">
                        <asp:UpdatePanel ID="UpdatePanel_CustomField" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelCustomFieldA1" runat="server" GroupingText="報名相關資料">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Step 4">
                        注意事項:<br />
                        <asp:Literal ID="Literal_notice" runat="server"></asp:Literal>
                    </asp:WizardStep>
                </WizardSteps>
                <FinishNavigationTemplate>
                    <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                        Text="上一步" />
                    <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" OnClick="FinishButton_Click"
                        Text="完成" />
                </FinishNavigationTemplate>
            </asp:Wizard>
            <asp:ValidationSummary ID="sum_WizardNext" runat="server" ShowMessageBox="True" ShowSummary="False"
                ValidationGroup="WizardNext" />
            <asp:ValidationSummary ID="sum_person_info" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="person_info" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
