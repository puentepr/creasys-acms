<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegisedTeammemberSelector.ascx.cs"
    Inherits="WebForm_RegistActivity_OpenRegisedTeammemberSelectorX" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1"  Width="800" Height="500" ScrollBars="Auto">
    <!---->
    <br />
    <br />
    
   
    <asp:UpdateProgress ID="Updateprogress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <My:UpdateProgress ID="myprogress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消團隊報名" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource1"
                    EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    SkinID="pager" TotalRowCount="0" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GridView1_PageIndexChanged"
                    OnRowDataBound="GridView1_RowDataBound" OnSorted="GridView1_Sorted">
                    <EmptyDataTemplate>
                        <font color='Red'>查無資料</font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" SortExpression="C_DEPT_NAME" ItemStyle-Width="400px" />
                        <asp:TemplateField HeaderText="隊長">
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="取消報名">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="RegistedMyTeamMemberSelector" TypeName="ACMS.BO.SelectorBO">
                    <SelectParameters>
                        <asp:Parameter DbType="Guid" Name="activity_id" />
                        <asp:Parameter Name="emp_id" Type="String" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red" 
                    Text="若您取消報名則團隊人數將低於下限，因此系統將取消整個團隊的報名資格，若確定要取消報名，請點選「確定取消報名」按鈕!" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="確定" OnClick="btnOK_Click" />
          <asp:Button ID="btnCancelAll" runat="server" Text="全隊取消" OnClick="btnCancelAll_Click" />
        <asp:Button ID="btnOK0" runat="server" OnClick="btnOK0_Click" Text="確定取消報名" 
            Visible="False" />
        <asp:Button ID="btnCancel" runat="server" Text="關閉" />
      
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
