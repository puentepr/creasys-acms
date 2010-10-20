﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenListItem.ascx.cs"
    Inherits="WebForm_ManageActivity_OpenListItem" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;display: none"
    Width="300" Height="350">
    <!---->
    <div align="center">
        <table width="100%" cellpadding="10" cellspacing="10">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server" SkinID="title" Text="編輯選項"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="選項名稱"></asp:Label>
                                <asp:RequiredFieldValidator ID="chk_txtfield_item_name" runat="server" 
                                    ControlToValidate="txtfield_item_name" ErrorMessage="選項名稱必填" 
                                    ValidationGroup="CustomFieldItemAdd">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfield_item_name" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblValue" runat="server" Text="選項值" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfield_item_text" runat="server" Visible="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" 
                                    ValidationGroup="CustomFieldItemAdd" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                         <TServerControl:TGridView ID="GridView_CustomFieldItem" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="field_item_id" DataSourceID="ObjectDataSource_CustomFieldItem" 
                        SkinID="pager">
                        <Columns>
                            <asp:BoundField DataField="field_item_name" HeaderText="選項名稱" SortExpression="field_item_name" />
                            <asp:BoundField DataField="field_item_text" HeaderText="選項值" SortExpression="field_item_text"
                                Visible="False" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDel_CustomFieldItem" runat="server" CausesValidation="False"
                                        CommandName="Delete" OnClick="lbtnDel_CustomFieldItem_Click" Text="刪除" 
                                        onclientclick="return confirm('確定要刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource_CustomFieldItem" runat="server" DeleteMethod="DELETE"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByField_id"
                        TypeName="ACMS.BO.CustomFieldItemBO">
                        <DeleteParameters>
                            <asp:Parameter Name="field_item_id" Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:Parameter Name="field_id" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <div align="center">
        </div>
        <asp:Button ID="btnOK" runat="server" Text="確定" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
