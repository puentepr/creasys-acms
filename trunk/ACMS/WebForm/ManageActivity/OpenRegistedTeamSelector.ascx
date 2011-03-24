<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistedTeamSelector.ascx.cs"
    Inherits="WebForm_OpenRegistedTeamSelector" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <contenttemplate> 
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="500"><!--display: none;-->
    <br /><asp:updateprogress ID="Updateprogress1" runat="server" DisplayAfter="0">

 <ProgressTemplate>
                <my:UpdateProgress ID="myprogress1" runat="server" />
            </ProgressTemplate>

</asp:updateprogress>
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消團隊報名" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td align="center">
            <asp:Panel ID="Panel3" runat="server" GroupingText="團員列表">
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" ShowFooterWhenEmpty="False"
                    ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="ID"
                    SkinID="pager" DataSourceID="SqlDataSource2" 
                    EnableModelValidation="True" onrowdatabound="GridView1_RowDataBound" 
                    onsorted="GridView1_Sorted">
                    <EmptyDataTemplate>
                        <font color='Red' >查無資料</font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" 
                            SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                            SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門"   ItemStyle-Width="200px"
                            SortExpression="C_DEPT_NAME" />
                        <asp:TemplateField HeaderText="隊長">
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="取消" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connStr %>" 
                    SelectCommand="SELECT * FROM [V_ACSM_USER2]"></asp:SqlDataSource>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="確定" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
    
     </contenttemplate>
</asp:UpdatePanel>