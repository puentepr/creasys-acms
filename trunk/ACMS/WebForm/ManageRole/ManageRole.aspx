<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ManageRole.aspx.cs" Inherits="WebForm_ManageRole_ManageRole" %>

<%@ Register Src="../ManageActivity/OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector"
    TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="OpenEmployeeSelector.ascx" TagName="OpenEmployeeSelector" TagPrefix="uc2" %>
<%--<%@ Register Src="OpenManageRoleProgramMapping.ascx" TagName="OpenManageRoleProgramMapping"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                角色名稱<asp:DropDownList ID="ddlRole" runat="server" DataSourceID="ObjectDataSource_RoleList"
                    DataTextField="role_name" DataValueField="id" AppendDataBoundItems="True" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="chk_ddlRole" runat="server" ControlToValidate="ddlRole"
                    ErrorMessage="角色名稱必填" ValidationGroup="add" Display="None"></asp:RequiredFieldValidator>
                <asp:ObjectDataSource ID="ObjectDataSource_RoleList" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectRoleList" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                主辦單位<asp:DropDownList ID="ddlUnit" runat="server" DataSourceID="ObjectDataSource_Unit"
                    DataTextField="name" DataValueField="id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="chk_ddlUnit" runat="server" ControlToValidate="ddlUnit"
                    ErrorMessage="新增[後台管理者]或[活動管理者時]主辦單位必填" ValidationGroup="add" Display="None"
                    Visible="False"></asp:RequiredFieldValidator>
                <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectUnit" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource></asp:PlaceHolder>
                人員<asp:TextBox ID="txtEmployee" runat="server"></asp:TextBox><asp:Button ID="btnQueryPerson"
                    runat="server" Text="..." Height="21px" OnClick="btnQueryPerson_Click" />
                <uc2:OpenEmployeeSelector ID="OpenEmployeeSelector1" OnGetEmployeesClick="GetEmployees_Click"
                    runat="server" />
                <asp:Button ID="btnInsert" runat="server" Text="新增" ValidationGroup="add" OnClick="btnInsert_Click" />
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                    ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" TotalRowCount="0" AutoGenerateColumns="False"
                    DataKeyNames="id" SkinID="pager" EnableModelValidation="True" AllowPaging="True"
                    Style="margin-right: 0px" AllowSorting="True" DataSourceID="ObjectDataSource1"
                    Width="100%">
                    <HeaderStyle BackColor="#293955" Wrap="False" ForeColor="White" Font-Size="Small"
                        Font-Bold="False" />
                    <Columns>
                        <asp:BoundField DataField="role_name" HeaderText="角色名稱" SortExpression="role_name" />
                        <asp:BoundField DataField="unit_name" HeaderText="主辦單位" SortExpression="unit_name" />
                        <asp:TemplateField HeaderText="人員" SortExpression="">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NATIVE_NAME") %>'></asp:Label>
                                (<asp:Label ID="Label2" runat="server" Text='<%# Bind("WORK_ID") %>'></asp:Label>)
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("C_DEPT_ABBR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" OnClick="lbtnDelete_Click"
                                    OnClientClick="return confirm('確定要刪除嗎?');" Text="刪除"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="ACMS.VO.RoleUserMappingVO"
                    DeleteMethod="DeleteRoleUserMapping" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectRoleUserMapping" TypeName="ACMS.BO.RoleUserMappingBO"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ValidationSummary ID="sum_add" runat="server" DisplayMode="List" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="add" />
    </div>
</asp:Content>
