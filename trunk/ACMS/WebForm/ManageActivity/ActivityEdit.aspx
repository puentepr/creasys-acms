<%@ Page Title="活動編輯" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="ActivityEdit.aspx.cs" Inherits="WebForm_ManageActivity_ActivityEdit" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="OpenListItem.ascx" TagName="OpenListItem" TagPrefix="uc2" %>
<%@ Register Src="../DatetimePicker.ascx" TagName="DatetimePicker" TagPrefix="uc3" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
 <!--
        function CheckAll() {
            //            for (c in document.all.getElementsByTagName("input"))
            //                document.all[c].checked = event.srcElement.checked;
            alert('add');
        }


    
        
       -->
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <My:UpdateProgress ID="myprogress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="1" DisplaySideBar="False"
        FinishPreviousButtonText="上一步" StartNextButtonText="下一步" StepNextButtonText="下一步"
        StepPreviousButtonText="上一步" OnFinishButtonClick="Wizard1_FinishButtonClick"
        OnNextButtonClick="Wizard1_NextButtonClick" OnActiveStepChanged="Wizard1_ActiveStepChanged">
        <WizardSteps>
            <asp:WizardStep runat="server" Title="Step 1">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <font color="blue">此頁面您可編輯或上傳此活動公告或訊息(文字與圖檔)，提供報名者了解活動內容與規劃</font><br />
                        <asp:Panel ID="plFCKEditor" runat="server">
                            <asp:Literal ID="liactivity_info" runat="server" Visible="False"></asp:Literal>
                            <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Width="800px" Height="400px"
                                BasePath="~/FCKeditor/">
                            </FCKeditorV2:FCKeditor>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 2">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <font color="blue">請填寫本次活動的基本資訊，同時亦可上傳活動相關檔案 </font></font><br />
                        <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSource_Activaty"
                            EnableModelValidation="True" OnPreRender="FormView1_PreRender" OnPageIndexChanging="FormView1_PageIndexChanging">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    活動主辦單位
                                                </td>
                                                <td>
                                                    <TServerControl:TDropDownList ID="ddlorg_id" runat="server" DataSourceID="ObjectDataSource_Unit"
                                                        DataTextField="name" DataValueField="id" SelectedValue='<%# Bind("org_id") %>'>
                                                    </TServerControl:TDropDownList>
                                                    <asp:RequiredFieldValidator ID="chk_ddlorg_id" runat="server" ControlToValidate="ddlorg_id"
                                                        Display="Dynamic" ErrorMessage="活動主辦單位必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                    <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="SelectUnit" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    活動名稱&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtactivity_name" runat="server" Text='<%# Bind("activity_name") %>'
                                                        Width="350px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="chk_txtactivity_name" runat="server" ControlToValidate="txtactivity_name"
                                                        Display="Dynamic" ErrorMessage="活動名稱必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    活動對象
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpeople_type" runat="server" Text='<%# Bind("people_type") %>'
                                                        Height="47px" TextMode="MultiLine" Width="350px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="chk_txtpeople_type" runat="server" ControlToValidate="txtpeople_type"
                                                        Display="Dynamic" ErrorMessage="活動對象必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    活動日期(起)
                                                </td>
                                                <td>
                                                    <uc3:DatetimePicker ID="txtactivity_startdate" DateTimeValue='<%# Bind("activity_startdate") %>'
                                                        runat="server" EnableTheming="False" EnableViewState="True" RequiredErrorMessage="活動日期(起)必填"
                                                        FormatErrorMessage="活動日期(起)格式不正確" ValidationGroup="WizardNext" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    活動日期(迄)
                                                </td>
                                                <td>
                                                    <uc3:DatetimePicker ID="txtactivity_enddate" runat="server" DateTimeValue='<%# Bind("activity_enddate") %>'
                                                        EnableTheming="True" RequiredErrorMessage="活動日期(迄)必填" FormatErrorMessage="活動日期(迄)格式不正確"
                                                        ValidationGroup="WizardNext" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="lbllimit_count" runat="server" Visible="False">活動人數上限</asp:Literal>
                                                    <asp:Literal ID="lbllimit_count_team" runat="server" Visible="False">活動隊數上限</asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlimit_count" runat="server" Text='<%# Bind("limit_count") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="lbllimit2_count" runat="server" Visible="False">活動備取人數</asp:Literal>
                                                    <asp:Literal ID="lbllimit2_count_team" runat="server" Visible="False">活動備取隊數</asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlimit2_count" runat="server" Text='<%# Bind("limit2_count") %>' />
                                                </td>
                                            </tr>
                                            <tr id="trteam_member_max" runat="server">
                                                <td>
                                                    每隊人數上限
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtteam_member_max" runat="server" Text='<%# Bind("team_member_max") %>' />
                                                    <asp:RequiredFieldValidator ID="chk_txtteam_member_max" runat="server" Display="Dynamic"
                                                        ErrorMessage="每隊人數上限必填" ValidationGroup="WizardNext" ControlToValidate="txtteam_member_max"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="chk_txtteam_member_max2" runat="server" ControlToValidate="txtteam_member_max"
                                                        ErrorMessage="每隊人數上限必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                        Display="Dynamic"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr id="trteam_member_min" runat="server">
                                                <td>
                                                    每隊人數下限
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtteam_member_min" runat="server" Text='<%# Bind("team_member_min") %>' />
                                                    <asp:RequiredFieldValidator ID="chk_txtteam_member_min" runat="server" Display="Dynamic"
                                                        ErrorMessage="每隊人數下限必填" ValidationGroup="WizardNext" ControlToValidate="txtteam_member_min"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="chk_txtteam_member_min2" runat="server" ControlToValidate="txtteam_member_min"
                                                        ErrorMessage="每隊人數下限必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                        Display="Dynamic"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    報名開始日
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtregist_startdate" runat="server" Text='<%# Bind("regist_startdate","{0:d}") %>' />
                                                    <ajaxToolkit:CalendarExtender ID="txtregist_startdate_CalendarExtender" runat="server"
                                                        Format="yyyy/MM/dd" TargetControlID="txtregist_startdate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="chk_txtregist_startdate" runat="server" ControlToValidate="txtregist_startdate"
                                                        Display="Dynamic" ErrorMessage="報名開始日必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="chk_txtregist_startdate2" runat="server" ControlToValidate="txtregist_startdate"
                                                        Display="Dynamic" ErrorMessage="報名開始日格式不正確" Operator="DataTypeCheck" Type="Date"
                                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    報名截止日
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtregist_deadline" runat="server" Text='<%# Bind("regist_deadline","{0:d}") %>' />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy/MM/dd"
                                                        TargetControlID="txtregist_deadline">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="chk_txtregist_deadline" runat="server" ControlToValidate="txtregist_deadline"
                                                        Display="Dynamic" ErrorMessage="報名截止日必填" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="chk_txtregist_deadline2" runat="server" ControlToValidate="txtregist_deadline"
                                                        Display="Dynamic" ErrorMessage="報名截止日格式不正確" Operator="DataTypeCheck" Type="Date"
                                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="chk_txtregist_deadline3" runat="server" ControlToCompare="txtregist_startdate"
                                                        ControlToValidate="txtregist_deadline" Display="Dynamic" ErrorMessage="報名開始日不可晚於報名截止日"
                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="WizardNext"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    取消報名截止日
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcancelregist_deadline" runat="server" Text='<%# Bind("cancelregist_deadline","{0:d}") %>' />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtcancelregist_deadline"
                                                        Format="yyyy/MM/dd">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="chk_txtcancelregist_deadline" runat="server" ControlToValidate="txtcancelregist_deadline"
                                                        ErrorMessage="取消報名截止日必填" Display="Dynamic" ValidationGroup="WizardNext"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="chk_txtcancelregist_deadline2" runat="server" ControlToValidate="txtcancelregist_deadline"
                                                        ErrorMessage="取消報名截止日格式不正確" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                        ValidationGroup="WizardNext"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="chk_txtcancelregist_deadline3" runat="server" ControlToCompare="txtregist_deadline"
                                                        ControlToValidate="txtcancelregist_deadline" Display="Dynamic" ErrorMessage="報名截止日不可晚於取消報名截止日"
                                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="WizardNext"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    活動進度顯示
                                                </td>
                                                <td>
                                                    <TServerControl:TCheckBoxYN ID="chkis_showprogres" runat="server" YesNo='<%# Eval("is_showprogress") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    附加檔案
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    <asp:Button ID="btnUpload" runat="server" Text="上傳" OnClick="btnUpload_Click" OnInit="btnUpload_Init" />
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" CssClass="pldisVisible" />
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td colspan="2">
                                                    <asp:CheckBox ID="cbSend3DayMail" runat="server" 
                                                        Checked="<%# Bind('Send3DayMail') %>" Text="發送活動前三天提醒通知信件" />
                                                </td>
                                            </tr>
                                           </tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="cbSend1DayMail" runat="server" 
                                                        Checked="<%# Bind('Send1DayMail') %>"   Text ="發送活動前一天提醒通知信件"/>
                                                </td>
                                            </tr>
                                           </tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="cbSendUnregist" runat="server" 
                                                        Checked="<%# Bind('SendUnregist') %>"   Text ="於前一個工作天發送未報名提醒通知信件"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <TServerControl:TGridView ID="GridView_UpFiles" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="path" DataSourceID="ObjectDataSource_UpFiles" AllowHoverEffect="True"
                                                        AllowHoverSelect="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                                        SkinID="pager" TotalRowCount="0" Width="100%" AllowSorting="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="name" HeaderText="活動資料下載" SortExpression="name" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnFileDownload" runat="server" CommandArgument='<%# Eval("path") %>'
                                                                        OnClick="lbtnFileDownload_Click">下載</asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnFileDelete" runat="server" CommandArgument='<%# Eval("path") %>'
                                                                        OnClientClick="return confirm('確定要刪除嗎?')" OnClick="lbtnFileDelete_Click">刪除</asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </TServerControl:TGridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                        <font color="blue">請勾選或自行設定此次報名者務必填寫的欄位內容，無須設定，請按下一步</font></font><br />
                        <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource_Activaty2"
                            DataKeyNames="id" OnDataBound="FormView2_DataBound">
                            <ItemTemplate>
                                <asp:Panel ID="PanelCustomFieldA1" runat="server" GroupingText="勾選/身分ID，攜伴人數">
                                    <div>
                                        <TServerControl:TCheckBoxYN ID="chkis_showperson_fix1" runat="server" Text="身份證字號或護照號碼"
                                            YesNo='<%# Bind("is_showperson_fix1") %>' />
                                        <asp:UpdatePanel ID="UpdatePanelA" runat="server">
                                            <ContentTemplate>
                                                <TServerControl:TCheckBoxYN ID="chkis_showperson_fix2" runat="server" Text="攜伴人數限制"
                                                    YesNo='<%# Bind("is_showperson_fix2") %>' AutoPostBack="True" OnCheckedChanged="chkis_showperson_fix2_CheckedChanged" />
                                                <asp:TextBox ID="txtpersonextcount_min" runat="server" Text='<%# Bind("personextcount_min") %>'
                                                    Width="50px" Visible="False"></asp:TextBox>
                                                ~
                                                <asp:TextBox ID="txtpersonextcount_max" runat="server" Text='<%# Bind("personextcount_max") %>'
                                                    Width="50px" Visible="False"></asp:TextBox>人
                                                <asp:RequiredFieldValidator ID="chk_txtpersonextcount_min" runat="server" ControlToValidate="txtpersonextcount_min"
                                                    Display="Dynamic" ErrorMessage="攜伴人數限制必填" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="chk_txtpersonextcount_min2" runat="server" ControlToValidate="txtpersonextcount_min"
                                                    ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                    Visible="False" Display="Dynamic"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="chk_txtpersonextcount_max" runat="server" ControlToValidate="txtpersonextcount_max"
                                                    Display="Dynamic" ErrorMessage="攜伴人數限制必填" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="chk_txtpersonextcount_max2" runat="server" ControlToValidate="txtpersonextcount_max"
                                                    ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                    Visible="False" Display="Dynamic"></asp:CompareValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCustomFieldB1" runat="server" GroupingText="隊員 / 身份ID、資料填寫設定"
                                    Width="500">
                                    <TServerControl:TCheckBoxYN ID="chkis_showidno" runat="server" Text="身分證字號" YesNo='<%# Bind("is_showidno") %>' />
                                    <asp:UpdatePanel ID="UpdatePanelB" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <TServerControl:TCheckBoxYN ID="chkis_showremark" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkis_showremark_CheckedChanged" Text="備註" YesNo='<%# Bind("is_showremark") %>' />
                                                    </td>
                                                    <td>
                                                        &nbsp; 備註顯示名稱<asp:TextBox ID="txtremark_name" runat="server" Text='<%# Bind("remark_name") %>'
                                                            Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="chk_txtremark_name" runat="server"
                                                                ControlToValidate="txtremark_name" Display="Dynamic" ErrorMessage="備註顯示名稱必填"
                                                                ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="PanelCustomFieldB2" runat="server" GroupingText="團隊/隊名" Width="500">
                                    <TServerControl:TCheckBoxYN ID="chkis_showteam_fix1" runat="server" Text="隊名" YesNo='<%# Bind("is_showteam_fix1") %>' />
                                    <asp:UpdatePanel ID="UpdatePanelC" runat="server" UpdateMode="Conditional" Visible="False">
                                        <ContentTemplate>
                                            <TServerControl:TCheckBoxYN ID="chkis_showteam_fix2" runat="server" Text="攜伴人數限制"
                                                YesNo='<%# Bind("is_showteam_fix2") %>' AutoPostBack="True" OnCheckedChanged="chkis_showteam_fix2_CheckedChanged" />
                                            <asp:TextBox ID="txtteamextcount_min" runat="server" Text='<%# Bind("teamextcount_min") %>'
                                                Width="50px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtteamextcount_max" runat="server" Text='<%# Bind("teamextcount_max") %>'
                                                Width="50px" Visible="False"></asp:TextBox>
                                            &nbsp;<asp:RequiredFieldValidator ID="chk_txtteamextcount_min" runat="server" ControlToValidate="txtteamextcount_min"
                                                Display="Dynamic" ErrorMessage="最少攜伴人數限制必填" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="chk_txtteamextcount_min2" runat="server" ControlToValidate="txtteamextcount_min"
                                                    ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
                                                    Visible="False" Display="Dynamic"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="chk_txtteamextcount_max" runat="server" ControlToValidate="txtteamextcount_max"
                                                        Display="Dynamic" ErrorMessage="最多攜伴人數限制必填" ValidationGroup="WizardNext" Visible="False"></asp:RequiredFieldValidator><asp:CompareValidator
                                                            ID="chk_txtteamextcount_max2" runat="server" ControlToValidate="txtteamextcount_max"
                                                            ErrorMessage="攜伴人數限制必填數字" Operator="DataTypeCheck" Type="Integer" ValidationGroup="WizardNext"
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
                        <asp:Panel ID="PanelCustomFieldC" runat="server" GroupingText="自行設定/資料填寫、單選、複選、費用加總選項">
                            <table>
                                <tr>
                                    <td>
                                        欄位名稱
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfield_name" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="chk_txtfield_name" runat="server" ControlToValidate="txtfield_name"
                                            ErrorMessage="欄位名稱必填" ValidationGroup="CustomFieldAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        欄位類型
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlfield_control" runat="server">
                                            <asp:ListItem Value="textbox">文字輸入</asp:ListItem>
                                            <asp:ListItem Value="textboxlist">費用項目</asp:ListItem>
                                            <asp:ListItem Value="checkboxlist">複選項目</asp:ListItem>
                                            <asp:ListItem Value="radiobuttonlist">單選項目</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddCustomField" runat="server" Text="新增" OnClick="btnAddCustomField_Click"
                                            ValidationGroup="CustomFieldAdd" />
                                    </td>
                                </tr>
                            </table>
                            <TServerControl:TGridView ID="GridView_CustomField" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="field_id" DataSourceID="ObjectDataSource_CustomField" SkinID="pager"
                                AllowSorting="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="欄位名稱" SortExpression="field_name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("field_name") %>' Width="90%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("field_name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="欄位類型" SortExpression="field_control" HeaderStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlfield_control" runat="server" Enabled="False" SelectedValue='<%# Bind("field_control") %>'>
                                                <asp:ListItem Value="textbox">文字輸入</asp:ListItem>
                                                <asp:ListItem Value="textboxlist">費用項目</asp:ListItem>
                                                <asp:ListItem Value="checkboxlist">複選項目</asp:ListItem>
                                                <asp:ListItem Value="radiobuttonlist">單選項目</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlfield_control" runat="server" Enabled="False" SelectedValue='<%# Bind("field_control") %>'>
                                                <asp:ListItem Value="textbox">文字輸入</asp:ListItem>
                                                <asp:ListItem Value="textboxlist">費用項目</asp:ListItem>
                                                <asp:ListItem Value="checkboxlist">複選項目</asp:ListItem>
                                                <asp:ListItem Value="radiobuttonlist">單選項目</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEditItem" runat="server" Visible='<%# Eval("IsShowEdit")%>'
                                                OnClick="lbtnEditItem_Click" CommandArgument='<%# Eval("field_id") %>'>編輯選項</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDeleteCustomField" runat="server" CausesValidation="False"
                                                CommandName="Delete" OnClick="lbtnDeleteCustomField_Click" OnClientClick="return confirm ('確定要刪除嗎?');"
                                                Text="刪除"></asp:LinkButton>
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
                            <uc2:OpenListItem ID="OpenListItem1" runat="server" Visible="false" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 4">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <font color="blue">請設定此次活動報名人員，無須設定，請按下一步</font><br />
                        <asp:RadioButtonList ID="rblgrouplimit" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblgrouplimit_Change" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="Y">限定報名人員</asp:ListItem>
                            <asp:ListItem Value="N">不限定報名人員(含ASUS、UTC、ASTP、ASTP-TW)</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Panel ID="plCoompanyLimit" runat="server" GroupingText="公司別設定" Visible="False">
                            公司別：<asp:DropDownList ID="ddlC_NAME" runat="server" DataSourceID="ObjectDataSource_CNAME"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                            <asp:Button ID="btnAddLimitCompany" runat="server" OnClick="btnAddLimitCompany_Click"
                                Text="新增" />
                            <asp:ObjectDataSource ID="ObjectDataSource_CNAME" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="CNAMESelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                            <asp:HiddenField ID="hiActivity_ID" runat="server" />
                            <TServerControl:TGridView ID="gvLimitCompany" runat="server" AllowHoverEffect="True"
                                AllowHoverSelect="True" AutoGenerateColumns="False" EnableModelValidation="True"
                                ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0"
                                AllowSorting="True" AutoGenerateDeleteButton="True" DataKeyNames="id" DataSourceID="dbLimintCompany">
                                <EmptyDataTemplate>
                                    <font color='Red'>查無資料</font>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="COMPANY_CODE" HeaderText="公司代號" SortExpression="COMPANY_CODE" />
                                    <asp:BoundField DataField="C_NAME" HeaderText="公司名稱" SortExpression="C_NAME" />
                                </Columns>
                            </TServerControl:TGridView>
                            <asp:ObjectDataSource ID="dbLimintCompany" runat="server" DeleteMethod="DeleteLimitCompany"
                                SelectMethod="GetLimitCompany" TypeName="ACMS.DAO.ActivityGroupLimitDAO">
                                <DeleteParameters>
                                    <asp:Parameter Name="id" Type="Int32" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hiActivity_ID" DbType="Guid" Name="activity_id"
                                        PropertyName="Value" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                        <asp:Panel ID="Panel_GroupLimit" runat="server" GroupingText="名單設定" Visible="False">
                            檔案上傳<asp:FileUpload ID="FileUpload_GroupLimit" runat="server" />
                            <asp:Button ID="btnUpload_GroupLimit" runat="server" Text="excel名冊上傳" OnClick="btnUpload_GroupLimit_Click" />
                            &nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Public/NameListSample.xls">下載範例檔</asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd_GroupLimit" runat="server" OnClick="btnAddGroupLimit_Click"
                                Text="系統名單選取" />
                            <asp:Button ID="btnExport_GroupLimit" runat="server" Text="匯出excel名單" OnClick="btnExport_GroupLimit_Click" />
                            <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" OnGetEmployeesClick="GetEmployees_Click"
                                runat="server" Visible="false" />
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <TServerControl:TGridView ID="GridView_GroupLimit" runat="server" AllowHoverEffect="True"
                                    AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="keyID" DataSourceID="ObjectDataSource_GroupLimit"
                                    EnableModelValidation="True" PageSize="50" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                                    SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="False" AllowSorting="False">
                                    <EmptyDataTemplate>
                                        <font color="Red">無資料</font>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" SortExpression="C_DEPT_NAME" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDel_GroupLimit" runat="server" OnClick="lbtnDel_GroupLimit_Click"
                                                    OnClientClick="return confirm('確定要刪除嗎?');" Visible="False">刪除</asp:LinkButton>
                                                <asp:CheckBox ID="chk1" runat="server" Text=" " />
                                                <asp:HiddenField ID="hiID" runat="server" EnableViewState="False" Value='<%# Bind("ID") %>' />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged"
                                                    Text="全選" />
                                                <asp:LinkButton ID="lbtnDel_GroupLimit" runat="server" ForeColor="White" OnClick="lbtnDel_GroupLimit_Click"
                                                    OnClientClick="return confirm('確定要刪除所選的資料?');">刪除</asp:LinkButton>
                                            </HeaderTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 5">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <font color="blue">請註明同仁於報名本次活動時，所需留意的事項或要點</font><br />
                        <asp:RequiredFieldValidator ID="chk_txtnotice" runat="server" ControlToValidate="txtnotice"
                            ErrorMessage="注意事項必填" ValidationGroup="WizardNext">*</asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox ID="txtnotice" runat="server" Height="300px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:WizardStep>
        </WizardSteps>
        <StepNavigationTemplate>
            <asp:Button ID="btnPrevious" runat="server" CssClass="WizardControlButton" Text="上一步"
                CommandName="MovePrevious" />
            <asp:Button ID="btnNext" runat="server" CssClass="WizardControlButton" CommandName="MoveNext"
                Text="下一步" CausesValidation="true" ValidationGroup="WizardNext" />
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
