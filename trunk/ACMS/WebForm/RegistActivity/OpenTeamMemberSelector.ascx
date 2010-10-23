<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenTeamMemberSelector.ascx.cs" Inherits="WebForm_RegistActivity_OpenTeamMemberSelector" %>
<script src="<%=this.ResolveUrl("~/js/JScript.js") %>" type="text/javascript"></script>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="500"><!--display: none;-->
    <br />
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup2" runat="server" Text="部門"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" 
                                DataSourceID="ObjectDataSource_Dept" DataTextField="Text" 
                                DataValueField="Value">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="DeptSelector" 
                                TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup0" runat="server" Text="員工編號"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWORK_ID" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup1" runat="server" Text="員工姓名"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNATIVE_NAME" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td align="right">
                            <asp:Button ID="btnQuery" runat="server" onclick="btnQuery_Click" Text="查詢" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
<%--                    <TServerControl:TGridView ID="GridView_Employee" runat="server" 
                        AutoGenerateColumns="False" 
                        DataKeyNames="ID" DataSourceID="ObjectDataSource_Employee" 
                        ShowFooterWhenEmpty="False" 
                        ShowHeaderWhenEmpty="True" SkinID="pager" TotalRowCount="0" Width="90%">
                        <Columns>

                        </Columns>
                    </TServerControl:TGridView>--%>
                    
<TServerControl:TGridView ID="GridView_Employee" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" ShowFooterWhenEmpty="False"
                    ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="ID"
                    SkinID="pager" 
                    EnableModelValidation="True" DataSourceID="ObjectDataSource_Employee" 
                    AllowPaging="True" AllowSorting="True" 
                    onpageindexchanged="GridView_Employee_PageIndexChanged">
                    <Columns>
                
                                                    <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" 
                                SortExpression="WORK_ID" ReadOnly="True" />
                            <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" 
                                SortExpression="NATIVE_NAME" />
                            <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" 
                                SortExpression="C_DEPT_ABBR" />
                                                    <asp:BoundField DataField="C_NAME" HeaderText="公司別" SortExpression="C_NAME" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <TServerControl:TCheckBoxYN ID="CheckBox1" runat="server" 
                                                                Enabled='<%#Eval("keyValue") %>' />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <input ID="cbCheckAll" runat="server" 
                                                                onclick="Check2(this,'GridView_Employee','CheckBox1');" type="checkbox" /><asp:Literal 
                                                                ID="Literal1" runat="server">全選</asp:Literal>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                        
                    </Columns>
                </TServerControl:TGridView>                    
                    
                    
                    <asp:ObjectDataSource ID="ObjectDataSource_Employee" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="RegistableTeamMember" 
                        TypeName="ACMS.BO.SelectorBO">
                        <SelectParameters>
                             <asp:Parameter Name="DEPT_ID" Type="String" ConvertEmptyStringToNull="false" />
                             <asp:Parameter Name="WORK_ID" Type="String" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="NATIVE_NAME" Type="String" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="activity_id" Type="String" ConvertEmptyStringToNull="false" />                      
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="確定" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />