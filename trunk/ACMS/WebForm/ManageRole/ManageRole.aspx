<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ManageRole.aspx.cs" Inherits="WebForm_ManageRole_ManageRole" %>

<%--<%@ Register Src="OpenManageRoleProgramMapping.ascx" TagName="OpenManageRoleProgramMapping"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="<%$ Resources:AddNewRole %>">
    </asp:Panel>
    <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True"
        AllowHoverSelect="True" DataSourceID="ObjectDataSource_ManageRole" ShowFooterWhenEmpty="False"
        ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="role_id,ID"
        SkinID="pager"  OnRowDataBound="GridView1_RowDataBound"
        EnableModelValidation="True" 
        AllowPaging="True">
      <HeaderStyle  BackColor="#293955" Wrap="False" ForeColor="White" Font-Size="Small" Font-Bold="False" />
        <Columns>
            <asp:BoundField DataField="role_name" HeaderText="角色名稱" />
            <asp:BoundField DataField="role_description" HeaderText="角色說明" />
            <asp:BoundField DataField="NATIVE_NAME" HeaderText="使用者名稱" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="ibtnAdd" runat="server" ImageUrl="~/images/addperson.png" 
                        ToolTip="新增人員" />
                    <asp:ImageButton ID="ibtnDel" runat="server" ImageUrl="~/images/del.png" 
                        ToolTip="刪除人員" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView>
    <asp:ObjectDataSource ID="ObjectDataSource_ManageRole" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="BLL_Select" 
        TypeName="BLL_ManageRole" DeleteMethod="BLL_Delete" 
        >
        <deleteparameters>
                    <asp:Parameter Name="original_role_id" Type="Int32" />
                    <asp:Parameter Name="original_ID" Type="String" />
                </deleteparameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="Panel2" runat="server">
    </asp:Panel>
    <%--<%@ Register Src="OpenManageRoleProgramMapping.ascx" TagName="OpenManageRoleProgramMapping"
    TagPrefix="uc1" %>--%>
    <asp:ValidationSummary ID="sum_Insert" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Insert" />
    <asp:ValidationSummary ID="sum_Update" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Update" />
</asp:Content>
