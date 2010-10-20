<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistByMeEmpSelector.ascx.cs" Inherits="WebForm_RegistActivity_OpenRegistByMeEmpSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消報名" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                    <TServerControl:TGridView ID="GridView1" runat="server" 
                        AllowHoverEffect="True" AllowHoverSelect="True" AutoGenerateColumns="False" 
                        DataKeyNames="ID" DataSourceID="ObjectDataSource1" 
                        EnableModelValidation="True" ShowFooterWhenEmpty="False" 
                        ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" 
                        AllowPaging="True" AllowSorting="True" 
                        onpageindexchanged="GridView1_PageIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" 
                                SortExpression="WORK_ID" />
                            <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                                SortExpression="NATIVE_NAME" />
                            <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" 
                                SortExpression="C_DEPT_ABBR" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="RegistByMeEmpSelector" TypeName="ACMS.BO.SelectorBO">
                        <SelectParameters>
                            <asp:Parameter DbType="Guid" Name="activity_id" />
                            <asp:Parameter Name="regist_by" Type="String" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />