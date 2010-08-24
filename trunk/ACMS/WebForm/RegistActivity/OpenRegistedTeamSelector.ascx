<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenRegistedTeamSelector.ascx.cs" Inherits="WebForm_OpenRegistedTeamSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消報名" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
             <td align="center">
                <asp:Panel ID="Panel2" runat="server" GroupingText="已新增團隊列表">
                    <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                        AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="id" 
                        DataSourceID="SqlDataSource1" EnableModelValidation="True" 
                        ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                        TotalRowCount="0">
                        <Columns>
                            <asp:BoundField DataField="team_name" HeaderText="團隊名稱" 
                                SortExpression="team_name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server">取消報名</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connStr %>" 
                        SelectCommand="SELECT * FROM [ActivityTeam]"></asp:SqlDataSource>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
            <asp:Panel ID="Panel3" runat="server" GroupingText="團員列表">
                <asp:Label ID="Label1" runat="server" Text="團隊名稱"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>天生好手隊</asp:ListItem>
                </asp:DropDownList>
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" ShowFooterWhenEmpty="False"
                    ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="emp_id"
                    SkinID="pager" DataSourceID="SqlDataSource2" 
                    EnableModelValidation="True">
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
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="取消報名" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connStr %>" 
                    SelectCommand="SELECT * FROM [UserList]"></asp:SqlDataSource> </asp:Panel>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="取消報名" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />