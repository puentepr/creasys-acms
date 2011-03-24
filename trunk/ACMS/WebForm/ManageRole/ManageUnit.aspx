<%@ Page Title="主辦單位設定" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ManageUnit.aspx.cs" Inherits="WebForm_ManageRole_ManageUnit" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        <table>
                            <tr align="center">
                                <td>
                                    主辦單位名稱<asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="chk_txtname" runat="server" ControlToValidate="txtname"
                                        Display="None" ErrorMessage="主辦單位名稱必填" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btnInsert" runat="server" Text="新增" ValidationGroup="add" OnClick="btnInsert_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowFooterWhenEmpty="False"
                            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" 
                            AllowPaging="True" DataKeyNames="id" onrowdeleting="GridView1_RowDeleting" 
                            onrowupdating="GridView1_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="單位名稱" SortExpression="name">
                                    
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtname_edit" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="chk_txtname_edit" runat="server" ControlToValidate="txtname_edit"
                                            Display="None" ErrorMessage="主辦單位名稱必填" ValidationGroup="edit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <span disabled="disabled">
                                        <input id="GridView1_ctl02_cbactive" disabled="disabled" 
                                            name="GridView1$ctl02$cbactive" type="checkbox" /></span>
                                    </FooterTemplate>
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="啟用">
                                    <EditItemTemplate>
                                        <TServerControl:TCheckBoxYN ID="cbactive_edit" runat="server" YesNo='<%# Bind("active") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <TServerControl:TCheckBoxYN ID="cbactive" runat="server" Enabled="False" YesNo='<%# Eval("active") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="更新" ValidationGroup="edit"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="取消"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="編輯"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" 
                                            CommandName="Delete" onclientclick="return confirm('確定要刪除資料?');" Text="刪除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                        </TServerControl:TGridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectUnit" TypeName="ACMS.BO.UnitBO" DataObjectTypeName="ACMS.VO.UnitVO"
        UpdateMethod="UpdateUnit" DeleteMethod="Delete">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ValidationSummary ID="sum_add" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="add" />
    <asp:ValidationSummary ID="sum_edit" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="edit" />
</asp:Content>
