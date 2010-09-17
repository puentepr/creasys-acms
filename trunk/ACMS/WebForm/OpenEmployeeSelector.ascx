<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenEmployeeSelector.ascx.cs" Inherits="WebForm_OpenEmployeeSelector" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="500" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="人員選取" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup" runat="server" Text="部門"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProgramGroup" runat="server" 
                                AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="dept_name" 
                                DataValueField="dept_id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:connStr %>" 
                                SelectCommand="SELECT * FROM [DeptList]"></asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:Label ID="lblProgramGroup2" runat="server" Text="職稱"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup0" runat="server" Text="員工編號"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblProgramGroup1" runat="server" Text="員工姓名"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup3" runat="server" Text="性別"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>女</asp:ListItem>
                                <asp:ListItem>男</asp:ListItem>
                                <asp:ListItem Selected="True">不拘</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Label ID="lblProgramGroup4" runat="server" Text="年齡"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramGroup5" runat="server" Text="年資起始日"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblProgramGroup6" runat="server" Text="公司別"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
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
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connStr %>" 
                    SelectCommand="SELECT * FROM [V_ACSM_USER]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" Text="確定" />
        <asp:Button ID="btnCancel" runat="server"  Text="取消" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />