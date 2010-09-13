<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenNameList.ascx.cs"
    Inherits="WebForm_ManageActivity_OpenNameList" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;"
    Width="400" Height="350">
    <!--display: none;-->
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="報名名單列表" SkinID="title"></asp:Label>
  
    <table width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr align="center">
                        <td>
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>已報名員工</asp:ListItem>
                                <asp:ListItem>未報名員工</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Text="查詢" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="gvNameList" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    SkinID="pager" TotalRowCount="0" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="emp_id" HeaderText="工號" SortExpression="emp_id" />
                        <asp:BoundField DataField="emp_cname" HeaderText="姓名" 
                            SortExpression="emp_cname" />
                        <asp:TemplateField HeaderText="報名狀態">
                            <ItemTemplate>
                                已報名
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                    SelectCommand="SELECT * FROM [UserList]">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <div align="center">  </div>
        <asp:Button ID="btnOK" runat="server" Text="確定" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
