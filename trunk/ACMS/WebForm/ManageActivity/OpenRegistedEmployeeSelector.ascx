<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistedEmployeeSelector.ascx.cs" Inherits="WebForm_OpenRegistedEmployeeSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="800" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消個人報名" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup0" runat="server" Text="員工編號"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup1" runat="server" Text="員工姓名"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            <asp:Button ID="Button1" runat="server" Text="查詢" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" ShowFooterWhenEmpty="False"
                    ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="ID"
                    SkinID="pager" DataSourceID="SqlDataSource2" 
                    EnableModelValidation="True" onsorted="GridView1_Sorted">
                    <EmptyDataTemplate>
                        <font color='Red' >查無資料</font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" 
                            SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                            SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" ItemStyle-Width="400px"
                            SortExpression="C_DEPT_NAME" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connStr %>" 
                    SelectCommand="SELECT * FROM [V_ACSM_USER2]"></asp:SqlDataSource>
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