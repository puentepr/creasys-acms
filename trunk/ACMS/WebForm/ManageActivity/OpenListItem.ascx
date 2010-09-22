﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenListItem.ascx.cs"
    Inherits="WebForm_ManageActivity_OpenListItem" %>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;"
    Width="300" Height="350">
    <!--display: none-->
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="編輯選項" SkinID="title"></asp:Label>
  
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="選項名稱"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblValue" runat="server" Text="選項值" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="新增" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="gvCustomField" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="field_item_id"
                    DataSourceID="SqlDataSource1" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    SkinID="pager" TotalRowCount="0" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="field_item_name" HeaderText="選項名稱" SortExpression="field_item_name" />
                         <asp:BoundField DataField="field_item_text" HeaderText="選項值" SortExpression="field_item_text" Visible="False" />
                        <asp:CommandField ShowDeleteButton="True" />
                                 
                    </Columns>
                </TServerControl:TGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                    
                    SelectCommand="SELECT [field_id], [field_item_id], [field_item_name], [field_item_text] FROM [CustomFieldItem] WHERE ([field_id] = @field_id)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="" Name="field_id" Type="Int32" />
                    </SelectParameters>
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
