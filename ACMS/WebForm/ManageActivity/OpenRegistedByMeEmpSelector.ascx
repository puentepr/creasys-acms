<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistedByMeEmpSelector.ascx.cs"
    Inherits="WebForm_RegistActivity_OpenRegistedByMeEmpSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    display: none;" Width="800" Height="600">
    <!---->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消個人報名" SkinID="title"></asp:Label>
    </div>
    <table align="center">
        <tr>
            <td align="right">
                <asp:Label ID="lblDEPT_ID" runat="server" Text="部門"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDEPT_ID" runat="server" DataSourceID="ObjectDataSource_Dept"
                    DataTextField="Text" DataValueField="Value">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="DeptSelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td align="right">
                <asp:Label ID="lblJOB_CNAME" runat="server" Text="職稱"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlJOB_GRADE_GROUP" runat="server" DataSourceID="ObjectDataSource_JOB_GRADE_GROUP"
                    DataTextField="Text" DataValueField="Value">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_JOB_GRADE_GROUP" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="JOB_GRADE_GROUPSelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblWORK_ID" runat="server" Text="員工編號"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWORK_ID" runat="server"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblNATIVE_NAME" runat="server" Text="員工姓名"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNATIVE_NAME" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblSEX" runat="server" Text="性別"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rblSEX" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="女" Value="0"></asp:ListItem>
                    <asp:ListItem Text="男" Value="1"></asp:ListItem>
                    <asp:ListItem Text="不拘" Value="" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
       
        <tr>
            <td align="right">
                <asp:Label ID="lblEXPERIENCE_START_DATE" runat="server" Text="年資起始日"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEXPERIENCE_START_DATE" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy/MM/dd"
                    TargetControlID="txtEXPERIENCE_START_DATE">
                </ajaxToolkit:CalendarExtender>
                <asp:CompareValidator ID="chk_txtEXPERIENCE_START_DATE" runat="server" ControlToValidate="txtEXPERIENCE_START_DATE"
                    Display="None" ErrorMessage="年資起始日必填日期格式" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="Query"></asp:CompareValidator>
            </td>
            <td align="right">
                <asp:Label ID="lblC_NAME" runat="server" Text="公司別"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlC_NAME" runat="server" DataSourceID="ObjectDataSource_CNAME"
                    DataTextField="Text" DataValueField="Value">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_CNAME" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="CNAMESelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server"  OnClick ="btnQuery_Click" Text="查詢" ValidationGroup="Query" />
            </td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource1"
                    EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    SkinID="pager" TotalRowCount="0" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GridView1_PageIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" SortExpression="C_DEPT_ABBR" />
                        <asp:TemplateField  >
                            <ItemTemplate >
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="RegistedByMeEmpSelectorByManage" TypeName="ACMS.BO.SelectorBO">
                    <SelectParameters>
                        <asp:Parameter DbType="Guid" Name="activity_id" />
                        <asp:Parameter Name="regist_by" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="DEPT_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="JOB_GRADE_GROUP" Type="Int16"  DefaultValue="999" />
                        <asp:Parameter Name="WINDOWS_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="NATIVE_NAME" Type="String" ConvertEmptyStringToNull="false" />
                         <asp:Parameter Name="SEX" Type="String" ConvertEmptyStringToNull="false" />
                          <asp:Parameter Name="EXPERIENCE_START_DATE" Type="DateTime" ConvertEmptyStringToNull="false" DefaultValue="1900/1/1" />
                          <asp:Parameter Name="C_NAME" Type="String" ConvertEmptyStringToNull="false" />
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="確定" OnClick="btnOK_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null"  style="display:none"/>
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
