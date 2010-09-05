<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ManageRole.aspx.cs" Inherits="WebForm_ManageRole_ManageRole" %>

<%--<%@ Register Src="OpenManageRoleProgramMapping.ascx" TagName="OpenManageRoleProgramMapping"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="<%$ Resources:AddNewRole %>">
        <asp:FormView ID="FormView1" runat="server" DefaultMode="Insert" Width="100%">
            <insertitemtemplate>
                        <table cellpadding="5" cellspacing="5">
                            <tr>
                                <td>
                                    <asp:Label ID="lblParent" runat="server" Text="<%$ Resources:Parent %>"></asp:Label>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="ddlParent" runat="server" DataSourceID="ObjectDataSource_SelectAllPapas"
                                DataTextField="role_name" DataValueField="role_id" AutoPostBack="True" 
                                        onselectedindexchanged="ddlRolePapas_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource_SelectAllPapas" runat="server" 
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="BLL_SelectAllRoles" 
                                        TypeName="BLL_ManageRole"></asp:ObjectDataSource>
                                </td>
                                <td>
                                    <asp:Label ID="lblRoleName" runat="server" Text="<%$ Resources:role_name %>"></asp:Label>
                                </td>
                                <td width="23%">
                                    <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" Width="95%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtRoleName" runat="server" 
                                        ControlToValidate="txtRoleName" Display="None" 
                                        ErrorMessage="<%$ Resources:chk_txtRoleName %>" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblRoleDescription" runat="server" 
                                Text="<%$ Resources:role_description %>"></asp:Label>
                                </td>
                                <td width="23%" style="margin-left: 40px">
                                    <asp:TextBox ID="txtRoleDescription" runat="server" MaxLength="50" 
                                Width="95%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtRoleDescription" runat="server" 
                                        ControlToValidate="txtRoleDescription" Display="None" 
                                        ErrorMessage="<%$ Resources:chk_txtRoleDescription %>" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" 
                                Text="<%$ Resources:Add %>" ValidationGroup="Insert" />
                                </td>
                            </tr>
                        </table>
                    </insertitemtemplate>
        </asp:FormView>
    </asp:Panel>
    <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True"
        AllowHoverSelect="True" DataSourceID="ObjectDataSource_ManageRole" ShowFooterWhenEmpty="False"
        ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="role_id"
        SkinID="pager" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound"
        OnRowUpdated="GridView1_RowUpdated" EnableModelValidation="True" 
        AllowPaging="True" PageSize="2">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtnUpdate" runat="server" Text="<%$ Resources:Update %>" ValidationGroup="Update"
                        CommandName="Update"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel" runat="server" Text="<%$ Resources:Cancel %>" CommandName="Cancel"
                        CausesValidation="False"></asp:LinkButton>
                    &nbsp;
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="<%$ Resources:Edit %>" CommandName="Edit"
                        CausesValidation="False" Visible='<%# BooleanConverter(Eval("editable")) %>'></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        OnClientClick="<%$ Resources:DeleteConfirm %>" Text="<%$ Resources:Delete %>"
                        Visible='<%# BooleanConverter(Eval("editable")) %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="ParentName" HeaderText="<%$ Resources:Parent %>" SortExpression="ParentName"
                ReadOnly="True">
                <ItemStyle Width="22%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="<%$ Resources:role_name %>" SortExpression="role_name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtRoleName0" runat="server" Text='<%# Bind("role_name") %>' MaxLength="50"
                        Width="90%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="chk_txtRoleName0" runat="server" ControlToValidate="txtRoleName0"
                        Display="None" ErrorMessage="<%$ Resources:chk_txtRoleName %>" ValidationGroup="Update"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("role_name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="22%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:role_description %>" SortExpression="role_description">
                <EditItemTemplate>
                    <asp:TextBox ID="txtRoleDescription0" runat="server" Text='<%# Bind("role_description") %>'
                        MaxLength="50" Width="90%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="chk_txtRoleDescription0" runat="server" ControlToValidate="txtRoleDescription0"
                        Display="None" ErrorMessage="<%$ Resources:chk_txtRoleDescription %>" ValidationGroup="Update"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("role_name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="22%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:active %>" SortExpression="active">
                <EditItemTemplate>
                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# BooleanConverter(Eval("active")) %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkActive0" runat="server" Checked='<%# BooleanConverter(Eval("active")) %>'
                        Enabled="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:SetupFormAuth %>">
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnSetup" runat="server" OnClick="lbtnSetup_Click" Text="<%$ Resources:Setup %>"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
      <HeaderStyle  BackColor="#293955" Wrap="False" ForeColor="White" Font-Size="Small" Font-Bold="False" />
    </TServerControl:TGridView>
    <asp:ObjectDataSource ID="ObjectDataSource_ManageRole" runat="server" InsertMethod="BLL_Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="BLL_Select" TypeName="BLL_ManageRole"
        UpdateMethod="BLL_Update" DeleteMethod="BLL_Delete" 
        onupdated="ObjectDataSource_Role_Updated">
        <deleteparameters>
                    <asp:Parameter Name="original_role_id" Type="Int32" />
                </deleteparameters>
        <updateparameters>
                    <asp:Parameter Name="original_role_id" Type="Int32" />
                    <asp:Parameter Name="role_name" Type="String" />
                    <asp:Parameter Name="role_description" Type="String" />
                    <asp:Parameter Name="Active" Type="String" />
                </updateparameters>
        <selectparameters>
                    <asp:Parameter Name="role_id" Type="Int32" />
                </selectparameters>
        <insertparameters>
                    <asp:Parameter Name="Parent" Type="Int32" />
                    <asp:Parameter Name="role_name" Type="String" />
                    <asp:Parameter Name="role_description" Type="String" />
                </insertparameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="Panel2" runat="server">
    </asp:Panel>
   <%-- <uc1:OpenManageRoleProgramMapping ID="OpenManageRoleProgramMapping1" runat="server" />--%>
    <asp:ValidationSummary ID="sum_Insert" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Insert" />
    <asp:ValidationSummary ID="sum_Update" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Update" />
</asp:Content>
