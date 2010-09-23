<%@ Page Title="" Language="C#"  MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="ActivityEdit.aspx.cs" Inherits="WebForm_ManageActivity_ActivityEdit" %>

<%@ Register Src="../OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc1" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="OpenListItem.ascx" TagName="OpenListItem" TagPrefix="uc2" %>
<%@ Register namespace="TServerControl" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" DisplaySideBar="False"
        FinishPreviousButtonText="上一步" StartNextButtonText="下一步" StepNextButtonText="下一步"
        StepPreviousButtonText="上一步">
        <WizardSteps>
            <asp:WizardStep runat="server" Title="Step 1">
                     <FTB:FreeTextBox ID="FreeTextBox1" runat="server" AllowHtmlMode="False" 
                            AssemblyResourceHandlerPath="" AutoConfigure="" 
                            AutoGenerateToolbarsFromString="True" AutoHideToolbar="True" 
                            AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl="" 
                            BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif" 
                            ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource" 
                            ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21" 
                            ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False" 
                            DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False" 
                            DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" 
                            DownLevelRows="10" EditorBorderColorDark="128, 128, 128" 
                            EditorBorderColorLight="128, 128, 128" EnableHtmlMode="True" EnableSsl="False" 
                            EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True" 
                            GutterBackColor="129, 169, 226" GutterBorderColorDark="128, 128, 128" 
                            GutterBorderColorLight="255, 255, 255" Height="350px" HelperFilesParameters="" 
                            HelperFilesPath="" HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" 
                            ImageGalleryPath="../../FTBimages/" 
                            ImageGalleryUrl="../../ftb.imagegallery.aspx?rif={0}&amp;cif={0}" 
                            InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource" 
                            Language="zh-TW" PasteMode="Default" ReadOnly="False" 
                            RemoveScriptNameFromBookmarks="True" RemoveServerNameFromUrls="True" 
                            RenderMode="NotSet" ScriptMode="External" ShowTagPath="False" SslUrl="/." 
                            StartMode="DesignMode" StripAllScripting="False" 
                            SupportFolder="../../Edit/" TabIndex="-1" 
                            TabMode="InsertSpaces" Text="" TextDirection="LeftToRight" 
                            ToolbarBackColor="Transparent" ToolbarBackgroundImage="True" 
                            ToolbarImagesLocation="InternalResource" 
                            ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage,InsertImageFromGallery,InsertRule|Cut,Copy,Paste;Undo,Redo,Print" 
                            ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" 
                            UseToolbarBackGroundImage="True" Width="100%">
                        </FTB:FreeTextBox>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 2">
                <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" DataSourceID="SqlDataSource2"
                    EnableModelValidation="True">
                    <ItemTemplate>
                        <table align="center">
                            <tr>
                                <td>
                                    活動主辦單位
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList5" runat="server">
                                        <asp:ListItem>人事室</asp:ListItem>
                                        <asp:ListItem>會計部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動名稱 
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("activity_name") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動對象
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("people_type") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動日期(起)</td>
                                <td>
                                    <asp:TextBox ID="txtactivity_date" runat="server" Text='<%# Bind("activity_date") %>'></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtactivity_date" Format="yyyy/MM/dd">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="時"></asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem>分</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    活動日期(迄)</td>
                                <td>
                                    <asp:TextBox ID="txtactivity_date0" runat="server" 
                                        Text='<%# Bind("activity_date") %>'></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                        Format="yyyy/MM/dd" TargetControlID="txtactivity_date">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem Value="時"></asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList4" runat="server">
                                        <asp:ListItem>分</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbllimit_count" runat="server" Text="活動人數上限 " Visible="False"></asp:Label>
                                    <asp:Label ID="lbllimit_count_team" runat="server" Text="活動隊數上限 " Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="peoplelimit_countTextBox" runat="server" Text='<%# Bind("limit_count") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbllimit2_count" runat="server" Text="活動備取人數 " Visible="False"></asp:Label>
                                    <asp:Label ID="lbllimit2_count_team" runat="server" Text="活動備取隊數" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="peoplelimit2_countTextBox" runat="server" Text='<%# Bind("limit2_count") %>' />
                                </td>
                            </tr>
                            <tr id="trteam_member_max" runat="server">
                                <td>
                                    每隊人數上限
                                </td>
                                <td>
                                    <asp:TextBox ID="txtteam_member_max" runat="server" Text='<%# Bind("team_member_max") %>' />
                                </td>
                            </tr>
                            <tr id="trteam_member_min" runat="server">
                                <td>
                                    每隊人數下限
                                </td>
                                <td>
                                    <asp:TextBox ID="txtteam_member_min" runat="server" Text='<%# Bind("team_member_min") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    報名截止日
                                </td>
                                <td>
                                    <asp:TextBox ID="txtregist_deadline" runat="server" Text='<%# Bind("regist_deadline") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                        TargetControlID="txtregist_deadline" Format="yyyy/MM/dd">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    取消報名截止日
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcancelregist_deadline" runat="server" Text='<%# Bind("cancelregist_deadline") %>' />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtcancelregist_deadline" Format="yyyy/MM/dd">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否顯示附加檔案
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("is_showfile") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否顯示活動進度 
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" 
                                        Checked='<%# Bind("is_showprogress") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    附加檔案
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                    <asp:Button ID="Button6" runat="server" Text="上傳" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <TServerControl:TGridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="id" DataSourceID="SqlDataSource_File" 
                                        AllowHoverEffect="True" AllowHoverSelect="True" ShowFooterWhenEmpty="False" 
                                        ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="file_desc" HeaderText="檔案說明" 
                                                SortExpression="file_desc" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server">下載</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server">刪除</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </TServerControl:TGridView>
                                    <asp:SqlDataSource ID="SqlDataSource_File" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:connStr %>" 
                                        SelectCommand="SELECT * FROM [ActivatyFiles]"></asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                    SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 3">
                <asp:Panel ID="PanelCustomFieldA1" runat="server" GroupingText="個人固定欄位">
                   
                        <div>
                            <asp:CheckBox ID="cbPersonID" runat="server" Text="身分證字號" />
                            <br>
                            <asp:CheckBox ID="cbExtPersonLimit" runat="server" Text="攜伴人數限制" />
                            <asp:TextBox ID="txtExtPersonLimit" runat="server" Width="50px"></asp:TextBox>
                            ~<asp:TextBox ID="txtExtPersonLimit0" runat="server" Width="50px"></asp:TextBox>
                            人 </br>
                        </div>
   
                </asp:Panel>
                 <asp:Panel ID="PanelCustomFieldA2" runat="server" GroupingText="個人固定欄位">
                     <div>
                         <asp:CheckBox ID="cbPersonID0" runat="server" Text="身分證字號" />
                         <br />
                         <asp:CheckBox ID="cbPersonID2" runat="server" Text="備註" />
                         <br></br>
                     </div>
                </asp:Panel>
                 <asp:Panel ID="PanelCustomFieldB2" runat="server" GroupingText="團隊固定欄位">
                 
                 
                 
                 
                     <asp:CheckBox ID="cbPersonID1" runat="server" Text="隊名" />
                     <br>
                     <asp:CheckBox ID="cbExtPersonLimit0" runat="server" Text="攜伴人數限制" />
                     <asp:TextBox ID="txtExtPersonLimit1" runat="server" Width="50px"></asp:TextBox>
                     ~<asp:TextBox ID="txtExtPersonLimit2" runat="server" Width="50px"></asp:TextBox>
                     人 </br>
                 
                 
                 
                 
                 </asp:Panel>
                <asp:Panel ID="PanelCustomFieldC" runat="server" GroupingText="自訂欄位">
                    <table>
                        <tr>
                            <td>
                                欄位名稱
                            </td>
                            <td>
                                <asp:TextBox ID="txtfield_name" runat="server"></asp:TextBox>
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
                                <asp:Button ID="btnAddCustomField" runat="server" Text="新增" />
                            </td>
                        </tr>
                    </table>
                    <TServerControl:TGridView ID="gvCustomField" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="field_id" DataSourceID="SqlDataSource1" SkinID="pager">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
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
                                    <asp:LinkButton ID="lbtnEditItem" runat="server" Visible='<%# IsShowEdit( Eval("IsShowEdit") ) %>'
                                        OnClick="lbtnEditItem_Click" CommandArgument='<%# Eval("field_id") %>'>編輯選項</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                        SelectCommand="SELECT activity_id, field_id, field_name, field_control,CASE field_control WHEN 'textbox' THEN 0 ELSE 1 END as IsShowEdit
FROM CustomField WHERE (activity_id = @activity_id)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="activity_id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <uc2:OpenListItem ID="OpenListItem1" runat="server" />
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 4">
                <asp:RadioButtonList ID="rblgrouplimit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="Y">使用族群限定名單</asp:ListItem>
                    <asp:ListItem Value="N">不使用族群限定名單</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Panel ID="Panel1" runat="server" GroupingText="族群限定名單">
                    檔案上傳<asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" Text="上傳" />
                    <asp:Button ID="btnAddGroupLimit" runat="server" OnClick="btnAddGroupLimit_Click"
                        Text="新增族群" /><asp:Button ID="btnExportExcel" runat="server" 
                        Text="匯出族群名單" />
                    <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True"
                        AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource3"
                        EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                        SkinID="pager" TotalRowCount="0" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                            <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                            <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" SortExpression="C_DEPT_ABBR" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server">刪除</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    &nbsp;<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                        SelectCommand="SELECT B.ID,B.WORK_ID,B.NATIVE_NAME,B.C_DEPT_ABBR
FROM ActivityGroupLimit A
inner join V_ACSM_USER B on A.emp_id=B.ID
WHERE A.activity_id=1"></asp:SqlDataSource>
                    <uc1:OpenEmployeeSelector ID="OpenEmployeeSelector1" runat="server" />
                </asp:Panel>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Step 5">
                注意事項<br />
                <asp:TextBox ID="TextBox3" runat="server" Height="294px" TextMode="MultiLine" 
                    Width="400px"></asp:TextBox>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
