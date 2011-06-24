<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistedByMeEmpSelector.ascx.cs"
    Inherits="WebForm_RegistActivity_OpenRegistedByMeEmpSelectorYY" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
            display: none;" Width="800" Height="500" ScrollBars="Auto">
            <!---->
            <asp:UpdateProgress ID="Updateprogress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <My:UpdateProgress ID="myprogress1" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div align="center">
                <asp:Label ID="lblTitle" runat="server" Text="取消個人報名" SkinID="title"></asp:Label>
            </div>
            <table align="center">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblC_NAME" runat="server" Text="公司別"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlC_NAME" runat="server" DataSourceID="ObjectDataSource_CNAME"
                            DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="ddlC_NAME_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource_CNAME" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="CNAMESelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDEPT_ID" runat="server" Text="部門"></asp:Label>
                    </td>
                    <td colspan ="3">
                        <asp:CheckBox ID="cbUnderDept" runat="server" Text="含所屬單位" Checked="True" />
                        <asp:DropDownList ID="ddlDEPT_ID" runat="server" DataSourceID="ObjectDataSource_Dept"
                            DataTextField="Text" DataValueField="Value">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="DeptSelectorByCompanyCode" TypeName="ACMS.BO.SelectorBO">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlC_NAME" Name="COMPANYCODE" PropertyName="SelectedValue"
                                    ConvertEmptyStringToNull="False" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td align="right">
                        
                    </td>
                    <td>
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
                    <td>
                       
                    </td>
                    <td align="center">
                     <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查詢" ValidationGroup="Query" />
                        <asp:Button ID="btnOK" runat="server" Text="確定" OnClick="btnOK_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="關閉" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center">
                        <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                            AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource1"
                            EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                            SkinID="pager" TotalRowCount="0" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GridView1_PageIndexChanged"
                            OnSorted="GridView1_Sorted">
                            <EmptyDataTemplate>
                                <font color="Red">查詢不到符合條件的資料... </font>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                                <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                                <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" SortExpression="C_DEPT_NAME" ItemStyle-Width="400px"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
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
                                <asp:Parameter Name="JOB_GRADE_GROUP" Type="Int16" DefaultValue="999" />
                                <asp:Parameter Name="WINDOWS_ID" Type="String" ConvertEmptyStringToNull="false" />
                                <asp:Parameter Name="NATIVE_NAME" Type="String" ConvertEmptyStringToNull="false" />
                                <asp:Parameter Name="SEX" Type="String" ConvertEmptyStringToNull="false" />
                                <asp:Parameter Name="EXPERIENCE_START_DATE" Type="DateTime" ConvertEmptyStringToNull="false"
                                    DefaultValue="1900/1/1" />
                                <asp:Parameter Name="C_NAME" Type="String" ConvertEmptyStringToNull="false" />
                                <asp:Parameter Name="UnderDept" Type="Boolean" ConvertEmptyStringToNull="false" />
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
    </ContentTemplate>
</asp:UpdatePanel>
