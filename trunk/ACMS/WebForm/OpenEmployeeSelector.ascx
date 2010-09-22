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
                        <td align="right">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="部門"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" 
                                AutoPostBack="True" DataSourceID="ObjectDataSource1" DataTextField="dept_name" 
                                DataValueField="dept_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="BLL_Department_Select" TypeName="BLL_Pubic">
                            </asp:ObjectDataSource>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblJOB_CNAME" runat="server" Text="職稱"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJOB_CNAME" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtNATIVE_NAME" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblSEX" runat="server" Text="性別"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblSEX" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="女" Value="0"></asp:ListItem>
                                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                <asp:ListItem Text="不拘" Value="" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblBIRTHDAY" runat="server" Text="生日"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBIRTHDAY_start" runat="server"></asp:TextBox>
                            ~<asp:TextBox ID="txtBIRTHDAY_end" runat="server"></asp:TextBox>
                        </td>
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
                        </td>
                        <td align="right">
                            <asp:Label ID="lblC_NAME" runat="server" Text="公司別"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtC_NAME" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" />
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
                    SkinID="pager" DataSourceID="SqlDataSource1" 
                    EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" 
                            SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                            SortExpression="NATIVE_NAME" />
                        <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" SortExpression="C_DEPT_ABBR" />
                          <asp:BoundField DataField="C_NAME" HeaderText="公司別" SortExpression="C_NAME" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="全選" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    TypeName="BLL_OpenEmployeeSelector" 
                    SelectMethod="BLL_OpenEmployeeSelector_Select" 
                    onselecting="ObjectDataSource2_Selecting">
                    <SelectParameters>
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="DEPT_ID" Type="String" />
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="JOB_CNAME" Type="String" />
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="WORK_ID" Type="String" />
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="NATIVE_NAME" Type="String" />
                        <asp:Parameter ConvertEmptyStringToNull="false" Name="SEX" Type="String" />
                        <asp:Parameter Name="AGE" Type="Int32" />
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="EXPERIENCE_START_DATE" Type="String" />
                        <asp:Parameter ConvertEmptyStringToNull="false"  Name="C_NAME" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
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