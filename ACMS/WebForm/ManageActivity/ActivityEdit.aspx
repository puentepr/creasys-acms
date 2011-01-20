<%@ Page Title="���ʽs��" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="ActivityEdit.aspx.cs" Inherits="WebForm_ManageActivity_ActivityEdit" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="OpenListItem.ascx" TagName="OpenListItem" TagPrefix="uc2" %>
<%@ Register Src="../DatetimePicker.ascx" TagName="DatetimePicker" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="1" DisplaySideBar="False"
        FinishPreviousButtonText="�W�@�B" StartNextButtonText="�U�@�B" StepNextButtonText="�U�@�B"
        StepPreviousButtonText="�W�@�B" OnFinishButtonClick="Wizard1_FinishButtonClick"
        OnNextButtonClick="Wizard1_NextButtonClick" OnActiveStepChanged="Wizard1_ActiveStepChanged">
        <WizardSteps>
            <asp:WizardStep runat="server" Title="Step 1">
                <font color="blue">�гz�L�����W����r�s�边�A�s�W/�ק惡�����ʪ����e</font><br />
                <asp:Panel ID="plFCKEditor" runat="server">
                    <asp:Literal ID="liactivity_info" runat="server" Visible="False"></asp:Literal>
                    <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Width="800px" Height="400px">
                    </FCKeditorV2:FCKeditor>
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 2">
                <font color="blue">�ж�g�������ʪ��򥻸�T�A�P�ɥ�i�W�Ǭ��ʬ����ɮ� </font></font><br />
                <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSource_Activaty"
                    EnableModelValidation="True" OnPreRender="FormView1_PreRender">
                    <ItemTemplate>
                        <table align="center">
                            <tr>
                                <td>
                                    ���ʥD����
                                </td>
                                <td>
                                    <TServerControl:TDropDownList ID="ddlorg_id" runat="server" DataSourceID="ObjectDataSource_Unit"
                                        DataTextField="name" DataValueField="id" SelectedValue='<%# Bind("org_id") %>'>
                                    </TServerControl:TDropDownList>
                                    <asp:RequiredFieldValidator ID="chk_ddlorg_id" runat="server" ControlToValidate="ddlorg_id"
                                        Display="Dynamic" ErrorMessage="���ʥD���쥲��" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                    <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="SelectUnit" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���ʦW��&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtactivity_name" runat="server" Text='<%# Bind("activity_name") %>'
                                        Width="350px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtactivity_name" runat="server" ControlToValidate="txtactivity_name"
                                        Display="Dynamic" ErrorMessage="���ʦW�٥���" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���ʹ�H
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpeople_type" runat="server" Text='<%# Bind("people_type") %>'
                                        Height="47px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtpeople_type" runat="server" ControlToValidate="txtpeople_type"
                                        Display="Dynamic" ErrorMessage="���ʹ�H����" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���ʤ��(�_)
                                </td>
                                <td>
                                    <uc3:DatetimePicker ID="txtactivity_startdate" DateTimeValue='<%# Bind("activity_startdate") %>'
                                        runat="server" EnableTheming="False" EnableViewState="True" RequiredErrorMessage="���ʤ��(�_)����"
                                        FormatErrorMessage="���ʤ��(�_)�榡�����T" ValidationGroup="WizardNext" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���ʤ��(��)
                                </td>
                                <td>
                                    <uc3:DatetimePicker ID="txtactivity_enddate" runat="server" DateTimeValue='<%# Bind("activity_enddate") %>'
                                        EnableTheming="True" RequiredErrorMessage="���ʤ��(��)����" FormatErrorMessage="���ʤ��(��)�榡�����T"
                                        ValidationGroup="WizardNext" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="lbllimit_count" runat="server" Visible="False">���ʤH�ƤW��</asp:Literal>
                                    <asp:Literal ID="lbllimit_count_team" runat="server" Visible="False">���ʶ��ƤW��</asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlimit_count" runat="server" Text='<%# Bind("limit_count") %>' />
                                    
                                    <asp:CompareValidator ID="chk_txtlimit_count2" runat="server" ControlToValidate="txtlimit_count"
                                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="lbllimit2_count" runat="server" Visible="False">���ʳƨ��H��</asp:Literal>
                                    <asp:Literal ID="lbllimit2_count_team" runat="server" Visible="False">���ʳƨ�����</asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlimit2_count" runat="server" Text='<%# Bind("limit2_count") %>' />
                                   
                                    <asp:CompareValidator ID="chk_txtlimit2_count2" runat="server" ControlToValidate="txtlimit2_count"
                                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr id="trteam_member_max" runat="server">
                                <td>
                                    �C���H�ƤW��
                                </td>
                                <td>
                                    <asp:TextBox ID="txtteam_member_max" runat="server" Text='<%# Bind("team_member_max") %>' />
                                    <asp:RequiredFieldValidator ID="chk_txtteam_member_max" runat="server" Display="Dynamic"
                                        ErrorMessage="�C���H�ƤW������" ValidationGroup="WizardNext" ControlToValidate="txtteam_member_max"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chk_txtteam_member_max2" runat="server" ControlToValidate="txtteam_member_max"
                                        ErrorMessage="�C���H�ƤW������Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr id="trteam_member_min" runat="server">
                                <td>
                                    �C���H�ƤU��
                                </td>
                                <td>
                                    <asp:TextBox ID="txtteam_member_min" runat="server" Text='<%# Bind("team_member_min") %>' />
                                    <asp:RequiredFieldValidator ID="chk_txtteam_member_min" runat="server" Display="Dynamic"
                                        ErrorMessage="�C���H�ƤU������" ValidationGroup="WizardNext" ControlToValidate="txtteam_member_min"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chk_txtteam_member_min2" runat="server" ControlToValidate="txtteam_member_min"
                                        ErrorMessage="�C���H�ƤU������Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���W�}�l��
                                </td>
                                <td>
                                    <asp:TextBox ID="txtregist_startdate" runat="server" Text='<%# Bind("regist_startdate","{0:d}") %>' />
                                    <ajaxToolkit:CalendarExtender ID="txtregist_startdate_CalendarExtender" runat="server"
                                        Format="yyyy/MM/dd" TargetControlID="txtregist_startdate">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="chk_txtregist_startdate" runat="server" ControlToValidate="txtregist_startdate"
                                        Display="Dynamic" ErrorMessage="���W�}�l�饲��" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chk_txtregist_startdate2" runat="server" ControlToValidate="txtregist_startdate"
                                        Display="Dynamic" ErrorMessage="���W�}�l��榡�����T" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���W�I���
                                </td>
                                <td>
                                    <asp:TextBox ID="txtregist_deadline" runat="server" Text='<%# Bind("regist_deadline","{0:d}") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy/MM/dd"
                                        TargetControlID="txtregist_deadline">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="chk_txtregist_deadline" runat="server" ControlToValidate="txtregist_deadline"
                                        Display="Dynamic" ErrorMessage="���W�I��饲��" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chk_txtregist_deadline2" runat="server" ControlToValidate="txtregist_deadline"
                                        Display="Dynamic" ErrorMessage="���W�I���榡�����T" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                    <asp:CompareValidator ID="chk_txtregist_deadline3" runat="server" ControlToCompare="txtregist_startdate"
                                        ControlToValidate="txtregist_deadline" Display="Dynamic" ErrorMessage="���W�}�l�餣�i�ߩ���W�I���"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="WizardNext"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �������W�I���
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcancelregist_deadline" runat="server" Text='<%# Bind("cancelregist_deadline","{0:d}") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtcancelregist_deadline"
                                        Format="yyyy/MM/dd">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="chk_txtcancelregist_deadline" runat="server" ControlToValidate="txtcancelregist_deadline"
                                        ErrorMessage="�������W�I��饲��" Display="Dynamic" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chk_txtcancelregist_deadline2" runat="server" ControlToValidate="txtcancelregist_deadline"
                                        ErrorMessage="�������W�I���榡�����T" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                    <asp:CompareValidator ID="chk_txtcancelregist_deadline3" runat="server" ControlToCompare="txtregist_deadline"
                                        ControlToValidate="txtcancelregist_deadline" Display="Dynamic" ErrorMessage="���W�I��餣�i�ߩ�������W�I���"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="WizardNext"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �O�_��ܪ��[�ɮ�
                                </td>
                                <td>
                                    <TServerControl:TCheckBoxYN ID="chkis_showfile" runat="server" YesNo='<%# Eval("is_showfile") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �O�_��ܬ��ʶi��
                                </td>
                                <td>
                                    <TServerControl:TCheckBoxYN ID="chkis_showprogres" runat="server" YesNo='<%# Eval("is_showprogress") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���[�ɮ�
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnUpload" runat="server" Text="�W��" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <TServerControl:TGridView ID="GridView_UpFiles" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="path" DataSourceID="ObjectDataSource_UpFiles" AllowHoverEffect="True"
                                        AllowHoverSelect="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                        SkinID="pager" TotalRowCount="0" Width="100%" AllowSorting="False">
                                        <Columns>
                                            <asp:BoundField DataField="name" HeaderText="�ɮצW��" SortExpression="name" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFileDownload" runat="server" CommandArgument='<%# Eval("path") %>'
                                                        OnClick="lbtnFileDownload_Click">�U��</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFileDelete" runat="server" CommandArgument='<%# Eval("path") %>'
                                                        OnClientClick="return confirm('�T�w�n�R����?')" OnClick="lbtnFileDelete_Click">�R��</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </TServerControl:TGridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="ObjectDataSource_Activaty" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectActivatyByActivatyID" TypeName="ACMS.BO.ActivatyBO">
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
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <font color="blue">�ШM�w�P�����W�������ʮɡA�ҥ�����g����줺�e</font></font><br />
                        <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource_Activaty2"
                            DataKeyNames="id">
                            <ItemTemplate>
                                <asp:Panel ID="PanelCustomFieldA1" runat="server" GroupingText="�ӤH�T�w���">
                                    <div>
                                        <TServerControl:TCheckBoxYN ID="chkis_showperson_fix1" runat="server" Text="�����Ҧr�����@�Ӹ��X"
                                            YesNo='<%# Bind("is_showperson_fix1") %>' />
                                        <asp:UpdatePanel ID="UpdatePanelA" runat="server">
                                            <ContentTemplate>
                                                <TServerControl:TCheckBoxYN ID="chkis_showperson_fix2" runat="server" Text="���H�ƭ���"
                                                    YesNo='<%# Bind("is_showperson_fix2") %>' AutoPostBack="True" OnCheckedChanged="chkis_showperson_fix2_CheckedChanged" />
                                                <asp:TextBox ID="txtpersonextcount_min" runat="server" Text='<%# Bind("personextcount_min") %>'
                                                    Width="50px"></asp:TextBox>
                                                ~
                                                <asp:TextBox ID="txtpersonextcount_max" runat="server" Text='<%# Bind("personextcount_max") %>'
                                                    Width="50px"></asp:TextBox>�H
                                                <asp:RequiredFieldValidator ID="chk_txtpersonextcount_min" runat="server" ControlToValidate="txtpersonextcount_min"
                                                    Display="Dynamic" ErrorMessage="���H�ƭ����" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="chk_txtpersonextcount_min2" runat="server" ControlToValidate="txtpersonextcount_min"
                                                    ErrorMessage="���H�ƭ����Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                    Visible="False" Display="Dynamic"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="chk_txtpersonextcount_max" runat="server" ControlToValidate="txtpersonextcount_max"
                                                    Display="Dynamic" ErrorMessage="���H�ƭ����" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="chk_txtpersonextcount_max2" runat="server" ControlToValidate="txtpersonextcount_max"
                                                    ErrorMessage="���H�ƭ����Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                    Visible="False" Display="Dynamic"></asp:CompareValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCustomFieldB1" runat="server" GroupingText="�ӤH�T�w���" Width="500">
                                    <TServerControl:TCheckBoxYN ID="chkis_showidno" runat="server" Text="�����Ҧr��" YesNo='<%# Bind("is_showidno") %>' />
                                    <asp:UpdatePanel ID="UpdatePanelB" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <TServerControl:TCheckBoxYN ID="chkis_showremark" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkis_showremark_CheckedChanged" Text="�Ƶ�" YesNo='<%# Bind("is_showremark") %>' />
                                                    </td>
                                                    <td>
                                                        &nbsp; �Ƶ���ܦW��<asp:TextBox ID="txtremark_name" runat="server" Text='<%# Bind("remark_name") %>'
                                                            Width="50px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="chk_txtremark_name" runat="server" ControlToValidate="txtremark_name"
                                                            Display="Dynamic" ErrorMessage="�Ƶ���ܦW�٥���" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="PanelCustomFieldB2" runat="server" GroupingText="�ζ��T�w���" Width="500">
                                    <TServerControl:TCheckBoxYN ID="chkis_showteam_fix1" runat="server" Text="���W" YesNo='<%# Bind("is_showteam_fix1") %>' />
                                    <asp:UpdatePanel ID="UpdatePanelC" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <TServerControl:TCheckBoxYN ID="chkis_showteam_fix2" runat="server" Text="���H�ƭ���"
                                                YesNo='<%# Bind("is_showteam_fix2") %>' AutoPostBack="True" OnCheckedChanged="chkis_showteam_fix2_CheckedChanged" />
                                            <asp:TextBox ID="txtteamextcount_min" runat="server" Text='<%# Bind("teamextcount_min") %>'
                                                Width="50px"></asp:TextBox>~<asp:TextBox ID="txtteamextcount_max" runat="server"
                                                    Text='<%# Bind("teamextcount_max") %>' Width="50px"></asp:TextBox>
                                            �H
                                            <asp:RequiredFieldValidator ID="chk_txtteamextcount_min" runat="server" ControlToValidate="txtteamextcount_min"
                                                Display="Dynamic" ErrorMessage="�̤����H�ƭ����" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="chk_txtteamextcount_min2" runat="server" ControlToValidate="txtteamextcount_min"
                                                ErrorMessage="���H�ƭ����Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                Visible="False" Display="Dynamic"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="chk_txtteamextcount_max" runat="server" ControlToValidate="txtteamextcount_max"
                                                Display="Dynamic" ErrorMessage="�̦h���H�ƭ����" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="chk_txtteamextcount_max2" runat="server" ControlToValidate="txtteamextcount_max"
                                                ErrorMessage="���H�ƭ����Ʀr" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                Visible="False" Display="Dynamic"></asp:CompareValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="ObjectDataSource_Activaty2" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="SelectActivatyByActivatyID" TypeName="ACMS.BO.ActivatyBO">
                            <SelectParameters>
                                <asp:Parameter DbType="Guid" Name="id" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:Panel ID="PanelCustomFieldC" runat="server" GroupingText="�ۭq���">
                            <table>
                                <tr>
                                    <td>
                                        ���W��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfield_name" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="chk_txtfield_name" runat="server" ControlToValidate="txtfield_name"
                                            ErrorMessage="���W�٥���" ValidationGroup="CustomFieldAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        �������
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlfield_control" runat="server">
                                            <asp:ListItem Value="textbox">��r��J</asp:ListItem>
                                            <asp:ListItem Value="textboxlist">�O�ζ���</asp:ListItem>
                                            <asp:ListItem Value="checkboxlist">�ƿﶵ��</asp:ListItem>
                                            <asp:ListItem Value="radiobuttonlist">��ﶵ��</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddCustomField" runat="server" Text="�s�W" OnClick="btnAddCustomField_Click"
                                            ValidationGroup="CustomFieldAdd" />
                                    </td>
                                </tr>
                            </table>
                            <TServerControl:TGridView ID="GridView_CustomField" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="field_id" DataSourceID="ObjectDataSource_CustomField" SkinID="pager" AllowSorting="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="���W��" SortExpression="field_name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("field_name") %>' Width="90%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("field_name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������" SortExpression="field_control" HeaderStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlfield_control" runat="server" Enabled="False" SelectedValue='<%# Bind("field_control") %>'>
                                                <asp:ListItem Value="textbox">��r��J</asp:ListItem>
                                                <asp:ListItem Value="textboxlist">�O�ζ���</asp:ListItem>
                                                <asp:ListItem Value="checkboxlist">�ƿﶵ��</asp:ListItem>
                                                <asp:ListItem Value="radiobuttonlist">��ﶵ��</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlfield_control" runat="server" Enabled="False" SelectedValue='<%# Bind("field_control") %>'>
                                                <asp:ListItem Value="textbox">��r��J</asp:ListItem>
                                                <asp:ListItem Value="textboxlist">�O�ζ���</asp:ListItem>
                                                <asp:ListItem Value="checkboxlist">�ƿﶵ��</asp:ListItem>
                                                <asp:ListItem Value="radiobuttonlist">��ﶵ��</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEditItem" runat="server" Visible='<%# Eval("IsShowEdit")%>'
                                                OnClick="lbtnEditItem_Click" CommandArgument='<%# Eval("field_id") %>'>�s��ﶵ</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDeleteCustomField" runat="server" CausesValidation="False"
                                                CommandName="Delete" OnClick="lbtnDeleteCustomField_Click" OnClientClick="return confirm ('�T�w�n�R����?');"
                                                Text="�R��"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </TServerControl:TGridView>
                            <asp:ObjectDataSource ID="ObjectDataSource_CustomField" runat="server" DataObjectTypeName="ACMS.VO.CustomFieldVO"
                                DeleteMethod="DELETE" InsertMethod="INSERT" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="SelectByActivity_id" TypeName="ACMS.BO.CustomFieldBO">
                                <DeleteParameters>
                                    <asp:Parameter Name="field_id" Type="Int32" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:Parameter DbType="Guid" Name="activity_id" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <uc2:OpenListItem ID="OpenListItem1" runat="server" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 4">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <font color="blue">�ШM�w�O�_�ݭ��w�i���W�������ʪ��P���W��]��Y�ڸs�W��^</font><br />
                        <asp:RadioButtonList ID="rblgrouplimit" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblgrouplimit_Change" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="Y">�ϥαڸs���w�W��</asp:ListItem>
                            <asp:ListItem Value="N">���ϥαڸs���w�W��</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Panel ID="Panel_GroupLimit" runat="server" GroupingText="�ڸs���w�W��">
                            �ɮפW��<asp:FileUpload ID="FileUpload_GroupLimit" runat="server" />
                            <asp:Button ID="btnUpload_GroupLimit" runat="server" Text="�W��" OnClick="btnUpload_GroupLimit_Click" />
                            &nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Public/NameListSample.xls">�U���d����</asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd_GroupLimit" runat="server" OnClick="btnAddGroupLimit_Click"
                                Text="�s�W�ڸs" />
                            <asp:Button ID="btnExport_GroupLimit" runat="server" Text="�ץX�ڸs�W��" OnClick="btnExport_GroupLimit_Click" />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <TServerControl:TGridView ID="GridView_GroupLimit" runat="server" AllowHoverEffect="True"
                                        AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="keyID" DataSourceID="ObjectDataSource_GroupLimit"
                                        EnableModelValidation="True" PageSize="50" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                        SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="False" AllowSorting="False">
                                        <Columns>
                                            <asp:BoundField DataField="WORK_ID" HeaderText="���u�s��" ReadOnly="True" SortExpression="WORK_ID" />
                                            <asp:BoundField DataField="NATIVE_NAME" HeaderText="�m�W" SortExpression="NATIVE_NAME" />
                                            <asp:BoundField DataField="C_DEPT_NAME" HeaderText="����" SortExpression="C_DEPT_NAME" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDel_GroupLimit" runat="server" OnClick="lbtnDel_GroupLimit_Click"
                                                        OnClientClick="return confirm('�T�w�n�R����?');">�R��</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </TServerControl:TGridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:ObjectDataSource ID="ObjectDataSource_GroupLimit" runat="server" DeleteMethod="DELETE"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByActivity_id"
                                TypeName="ACMS.BO.ActivityGroupLimitBO">
                                <DeleteParameters>
                                    <asp:Parameter Name="id" Type="Int32" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:Parameter DbType="Guid" Name="activity_id" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" OnGetEmployeesClick="GetEmployees_Click"
                                runat="server" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 5">
                <font color="blue">�е����P������W�������ʮɡA�һݯd�N���ƶ��έn�I</font><br />
                <asp:RequiredFieldValidator ID="chk_txtnotice" runat="server" ControlToValidate="txtnotice"
                    ErrorMessage="�`�N�ƶ�����" ValidationGroup="WizardNext">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtnotice" runat="server" Height="300px" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </asp:WizardStep>
        </WizardSteps>
        <StepNavigationTemplate>
            <asp:Button ID="btnPrevious" runat="server" CssClass="WizardControlButton" Text="�W�@�B"
                CommandName="MovePrevious" />
            <asp:Button ID="btnNext" runat="server" CssClass="WizardControlButton" CommandName="MoveNext"
                Text="�U�@�B" CausesValidation="true" ValidationGroup="WizardNext" />
        </StepNavigationTemplate>
    </asp:Wizard>
    <asp:ValidationSummary ID="sum_WizardNext" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="WizardNext" />
    <asp:ValidationSummary ID="sum_CustomFieldAdd" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="CustomFieldAdd" />
    <asp:ValidationSummary ID="sum_CustomFieldItemAdd" runat="server" ValidationGroup="CustomFieldItemAdd"
        ShowMessageBox="True" ShowSummary="False" />
    <asp:ValidationSummary ID="sum_Query" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Query" />
</asp:Content>
