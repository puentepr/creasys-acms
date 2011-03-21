<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="RegistActivity_Person.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Person"
    Title="個人報名" %>

<%@ Register Src="RegistActivity_Query.ascx" TagName="RegistActivity_Query" TagPrefix="uc1" %>
<%@ Register Src="OpenAgentSelector.ascx" TagName="OpenAgentSelector" TagPrefix="uc4" %>
<%@ Register Src="../DatetimePicker.ascx" TagName="DatetimePicker" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
            <asp:HiddenField ID="hiMode1" runat="server" />
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     
    
    <uc1:RegistActivity_Query ID="RegistActivity_Query1" runat="server" OnGoSecondStep_Click="GoSecondStep_Click"
        ActivityType="1" />
    <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" ActiveStepIndex="0"
        FinishPreviousButtonText="上一步" StartNextButtonText="下一步" StepNextButtonText="下一步"
        StepPreviousButtonText="上一步" onnextbuttonclick="Wizard1_NextButtonClick">
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
                <asp:Panel ID="PanelActivityInfo" runat="server" Width ="800px" GroupingText="活動相關資訊">
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
                                        活動人數上限
                                    </td>
                                    <td>
                                        <asp:Label ID="limit_countLabel" runat="server" Text='<%# Bind("limit_count") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        活動備取人數
                                    </td>
                                    <td>
                                        <asp:Label ID="limit2_countLabel" runat="server" Text='<%# Bind("limit2_count") %>' />
                                    </td>
                                </tr>
                                <%--                                <tr id="trteam_member_max" runat="server">
                                    <td>
                                        每隊人數限制
                                    </td>
                                    <td>
                                        <asp:Label ID="team_member_minLabel" runat="server" Text='<%# Bind("team_member_min") %>' />
                                        ~<asp:Label ID="team_member_maxLabel" runat="server" Text='<%# Bind("team_member_max") %>' />
                                    </td>
                                </tr>--%>
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
                <asp:Panel ID="PanelRegisterInfo" Width ="800px" runat="server" GroupingText="報名人事資料">
                    <asp:Panel ID="PanelRegisterInfoA" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:FormView ID="FormView_RegisterPersonInfo" runat="server" DataSourceID="ObjectDataSource_RegisterPersonInfo"
                                                EnableModelValidation="True">
                                                <ItemTemplate>
                                                    部門:<asp:Label ID="dept_nameLabel" runat="server" Text='<%# Bind("C_DEPT_NAME") %>' />
                                                    &nbsp; 工號:
                                                    <asp:Label ID="emp_idLabel" runat="server" Text='<%# Bind("WORK_ID") %>' />
                                                    &nbsp;姓名:
                                                    <asp:Label ID="emp_cnameLabel" runat="server" Text='<%# Bind("NATIVE_NAME") %>' />
                                                </ItemTemplate>
                                            </asp:FormView>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAgent" runat="server" OnClick="btnAgent_Click" Text="代理報名" Visible="False"/>
                                            <uc4:OpenAgentSelector ID="OpenAgentSelector1" runat="server" OnGetSmallEmployeesClick="GetSmallEmployees_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:ObjectDataSource ID="ObjectDataSource_RegisterPersonInfo" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="RegisterPersonInfo" TypeName="ACMS.BO.SelectorBO">
                                    <SelectParameters>
                                        <asp:Parameter Name="emp_id" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Panel ID="PanelRegisterInfoB" runat="server" Width ="800px" >
                        <TServerControl:TGridView ID="GridView_RegisterPeoplinfo" runat="server" AllowHoverEffect="True"
                            AllowHoverSelect="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_RegisterPeoplenfo"
                            EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                            SkinID="pager" TotalRowCount="0" Width="100%" DataKeyNames="ID" OnRowDataBound="GridView_RegisterPeoplinfo_RowDataBound"
                            OnDataBound="GridView_RegisterPeoplinfo_DataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="WORK_ID" HeaderText="工號" />
                                <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" />
                                <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" />
                            </Columns>
                        </TServerControl:TGridView>
                        <asp:ObjectDataSource ID="ObjectDataSource_RegisterPeoplenfo" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="RegisterPeopleInfo" TypeName="ACMS.BO.SelectorBO">
                            <SelectParameters>
                                <asp:Parameter Name="activity_id" Type="String" />
                                <asp:Parameter Name="emp_id" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 3">
                <asp:Panel ID="PanelCustomFieldA1" runat="server" GroupingText="報名相關資料" Width ="800px">
                    <table align="center" >
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel_CustomField" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:FormView ID="FormView_fixA" runat="server" DataSourceID="ObjectDataSource_fixA"
                                            OnDataBound="FormView_fixA_DataBound">
                                            <ItemTemplate>
                                                <table >
                                                    <tr>
                                                        <td colspan="2">
                                                            <font color="blue">您所提供的身份證字號將僅做為此次活動之使用!!</font>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_person_fix1" runat="server">
                                                        <td colspan="2">
                                                            <table >
                                                                <tr>
                                                                    <td width="200" style ="text-align:right ">
                                                                        <asp:RadioButtonList ID="rblidno_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblidno_type_SelectedIndexChanged"
                                                                            RepeatDirection="Vertical" SelectedIndex='<%# Bind("idno_type") %>'>
                                                                            <asp:ListItem Selected="True">身分證字號</asp:ListItem>
                                                                            <asp:ListItem>護照號碼</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtperson_fix1" runat="server" Text='<%# Bind("idno") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="chk_txtperson_fix1" runat="server" ControlToValidate="txtperson_fix1"
                                                                            Display="Dynamic" ErrorMessage="身分證字號必填" ValidationGroup="WizardNext" Text="*"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_person_fix2" runat="server">
                                                        <td width="200" style ="text-align:right ">
                                                            攜伴人數
                                                        </td>
                                                        <td width="200" >
                                                            <asp:TextBox ID="txtperson_fix2" runat="server" Text='<%# Bind("ext_people") %>'
                                                                Width="50px"></asp:TextBox>
                                                            (<asp:Label ID="lblAf2Start" runat="server" Text="Label"></asp:Label>~<asp:Label ID="lblAf2End" runat="server" Text="Label"></asp:Label>)<asp:RequiredFieldValidator ID="chk_txtperson_fix2" runat="server" ControlToValidate="txtperson_fix2"
                                                                Display="Dynamic" ErrorMessage="攜伴人數限制必填" ValidationGroup="WizardNext" Text="*"></asp:RequiredFieldValidator><asp:CompareValidator ID="chk_txtperson_fix2_2" runat="server" ControlToValidate="txtperson_fix2"
                                                                Display="Dynamic" ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer"
                                                                ValidationGroup="WizardNext" Text="*"></asp:CompareValidator><asp:RangeValidator ID="chk_txtperson_fix2_3" runat="server" ControlToValidate="txtperson_fix2"
                                                                Display="Dynamic" ErrorMessage="人數必在限制範圍內" Type="Integer" ValidationGroup="WizardNext"
                                                                Text="*"></asp:RangeValidator></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:FormView>
                                        <asp:ObjectDataSource ID="ObjectDataSource_fixA" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="SelectActivityRegistByPK" TypeName="ACMS.BO.ActivityRegistBO">
                                            <SelectParameters>
                                                <asp:Parameter DbType="Guid" Name="activity_id" />
                                                <asp:Parameter Name="emp_id" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
</asp:Content>
