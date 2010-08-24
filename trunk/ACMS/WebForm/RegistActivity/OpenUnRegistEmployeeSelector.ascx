<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenUnRegistEmployeeSelector.ascx.cs" Inherits="WebForm_OpenUnRegistEmployeeSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="代理報名" SkinID="title"></asp:Label>
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
                        <td align="left">
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
                <asp:Panel ID="Panel2" runat="server" GroupingText="報名資訊">
                    <table align="center">
                        <tr>
                            <td align="left">
                                攜伴人數</td>
                            <td align="left">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                費用</td>
                            <td align="left">
                                500</td>
                        </tr>
                        <tr>
                            <td align="left">
                                備註說明</td>
                            <td align="left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <TServerControl:TGridView ID="GridView1" runat="server" 
                        AllowHoverEffect="True" AllowHoverSelect="True" AutoGenerateColumns="False" 
                        DataKeyNames="emp_id" DataSourceID="SqlDataSource2" 
                        EnableModelValidation="True" ShowFooterWhenEmpty="False" 
                        ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0">
                        <Columns>
                            <asp:BoundField DataField="emp_id" HeaderText="員工編號" ReadOnly="True" 
                                SortExpression="emp_id" />
                            <asp:BoundField DataField="emp_cname" HeaderText="姓名" 
                                SortExpression="emp_cname" />
                            <asp:BoundField DataField="dept_id" HeaderText="部門" SortExpression="dept_id" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                </asp:Panel>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connStr %>" 
                    SelectCommand="SELECT * FROM [UserList]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="報名" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />