<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenTeamPersonInfo.ascx.cs" Inherits="WebForm_RegistActivity_OpenTeamPersonInfo" %>
<script src="<%=this.ResolveUrl("~/js/JScript.js") %>" type="text/javascript"></script>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="300" Height="200"><!--display: none;-->
    <table width="100%">
        <tr>
            <td align="center">
               <table>
                            <tr ID="tr_person_fix1" runat="server">
                                <td width="150">
                                    身分證字號/護照號碼 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtperson_fix1" runat="server" Text='<%# Bind("idno") %>' 
                                        Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtperson_fix1" runat="server" 
                                        ControlToValidate="txtperson_fix1" Display="Dynamic" ErrorMessage="身分證字號必填" 
                                        ValidationGroup="person_info"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr ID="tr_person_fix2" runat="server">
                                <td width="150">
                                    &nbsp;<asp:Label ID="lblRemark" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtperson_fix2" runat="server" 
                                        Text='<%# Bind("ext_people") %>' Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtperson_fix2" runat="server" 
                                        ControlToValidate="txtperson_fix2" Display="Dynamic" 
                                        ValidationGroup="person_info"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="確定" 
            ValidationGroup="person_info" />
        <asp:Button ID="btnCancel" runat="server"  Text="關閉" />
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />