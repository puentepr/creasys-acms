<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenEmployeeSelector.ascx.cs" Inherits="WebForm_ManageActivity_OpenEmployeeSelector" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
     <%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<script src="<%=this.ResolveUrl("~/js/JScript.js") %>" type="text/javascript"></script>
 <script type="text/javascript" >
 <!--
 function CheckAll() {
            for (c in document.all.getElementsByTagName("input"))
                if (c.indexOf("chkRJRA") > 0)
                document.all[c].checked = event.srcElement.checked;
        }
       -->
    </script>
<asp:Panel ID="panel1" runat="server" Height="550px" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="900px" ScrollBars ="Auto" ><!--display: none;-->
    
    <asp:updateprogress ID="Updateprogress1" runat="server" DisplayAfter="0">

 <ProgressTemplate>
                <my:UpdateProgress ID="myprogress1" runat="server" />
            </ProgressTemplate>

</asp:updateprogress>
    
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="人員選取" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblC_NAME" runat="server" Text="公司別"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlC_NAME" runat="server" AutoPostBack="True" 
                                DataSourceID="ObjectDataSource_CNAME" DataTextField="Text" 
                                DataValueField="Value" onselectedindexchanged="ddlC_NAME_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource_CNAME" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="CNAMESelector" 
                                TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="部門"></asp:Label>
                        </td>
                        <td>
                            
                            <asp:CheckBox ID="cbUnderDept" runat="server" Text="含所屬單位" Checked="True" />
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" 
                                DataSourceID="ObjectDataSource_Dept" DataTextField="Text" 
                                DataValueField="Value">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="DeptSelectorByCompanyCode" TypeName="ACMS.BO.SelectorBO">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlC_NAME" Name="COMPANYCODE" 
                                        PropertyName="SelectedValue"  ConvertEmptyStringToNull ="False" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            &nbsp;</td>
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
                            <asp:TextBox ID="txtNATIVE_NAME" runat="server" ></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblJOB_CNAME" runat="server" Text="職稱"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlJOB_GRADE_GROUP" runat="server" 
                                DataSourceID="ObjectDataSource_JOB_GRADE_GROUP" DataTextField="Text" 
                                DataValueField="Value">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSEX" runat="server" Text="性別"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblSEX" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="女" Value="0"></asp:ListItem>
                                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="不拘" Value=""></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblEXPERIENCE_START_DATE" runat="server" Text="年資起始日"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEXPERIENCE_START_DATE" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Format="yyyy/MM/dd" TargetControlID="txtEXPERIENCE_START_DATE">
                            </ajaxToolkit:CalendarExtender>
                            <asp:CompareValidator ID="chk_txtEXPERIENCE_START_DATE" runat="server" 
                                ControlToValidate="txtEXPERIENCE_START_DATE" Display="None" 
                                ErrorMessage="年資起始日必填日期格式" Operator="DataTypeCheck" Type="Date" 
                                ValidationGroup="Query"></asp:CompareValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblBIRTHDAY" runat="server" Text="生日"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBIRTHDAY_start_year" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlBIRTHDAY_start_month" runat="server">
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                            ~<asp:DropDownList ID="ddlBIRTHDAY_end_year" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlBIRTHDAY_end_month" runat="server">
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem Selected="True">12</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" onclick="btnQuery_Click" Text="查詢" 
                    ValidationGroup="Query" />
                <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="確定" />
                <asp:Button ID="btnCancel" runat="server" Text="關閉" />
                <asp:ObjectDataSource ID="ObjectDataSource_JOB_GRADE_GROUP" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="JOB_GRADE_GROUPSelector" TypeName="ACMS.BO.SelectorBO">
                </asp:ObjectDataSource>
                <TServerControl:TGridView ID="GridView_Employee" runat="server" 
                    AllowHoverEffect="True" AllowHoverSelect="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="ObjectDataSource_Employee" EnableModelValidation="True" 
                    onpageindexchanged="GridView_Employee_PageIndexChanged" PageSize="5" 
                    ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                    TotalRowCount="0" Visible="False" onsorted="GridView_Employee_Sorted">
                    <EmptyDataTemplate>
                        <font color="Red">查詢不到符合條件的資料... </font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" 
                            SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                            SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門"  ItemStyle-Width="200px"
                            SortExpression="C_DEPT_NAME" />
                        <asp:BoundField DataField="C_NAME" HeaderText="公司別" SortExpression="C_NAME" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <TServerControl:TCheckBoxYN ID="chkRJRA" runat="server" Checked='<%# Eval("keyValue") %>'
                                    Enabled='<%# Eval("keyValue") %>' />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="全選" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource_Employee" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    TypeName="ACMS.BO.SelectorBO" 
                    SelectMethod="EmployeeSelector" 
                    >
                    <SelectParameters>
                        <asp:Parameter  Name="DEPT_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter  Name="JOB_GRADE_GROUP" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter   Name="WORK_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter   Name="NATIVE_NAME" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="SEX" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="BIRTHDAY_S" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="BIRTHDAY_E" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter   Name="EXPERIENCE_START_DATE" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter  Name="C_NAME" Type="String" ConvertEmptyStringToNull="false" />
                         <asp:Parameter  Name="UnderDept" Type="Boolean"  ConvertEmptyStringToNull="false"  />
                        <asp:Parameter DbType="Guid" Name="activity_id" />
                        <asp:Parameter  Name="COMPANY_CODE" Type="String" ConvertEmptyStringToNull="false" />
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />