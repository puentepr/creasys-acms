<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenTeamPersonInfo.ascx.cs" Inherits="WebForm_RegistActivity_OpenTeamPersonInfo" %>
<script src="<%=this.ResolveUrl("~/js/JScript.js") %>" type="text/javascript"></script>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="cursor: move;
    " Width="400" Height="100"><!--display: none;-->
    <table width="95%">
        <tr>
            <td>
               <table align="center" cellpadding="5" cellspacing="5">
                            <tr ID="tr_idno" runat="server">
                                <td width="150" align="right"><asp:RadioButtonList ID="rblidno_type" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>身分證字號</asp:ListItem>
                                        <asp:ListItem>護照號碼</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtidno" runat="server" Text='<%# Bind("idno") %>' 
                                        Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtidno" runat="server" 
                                        ControlToValidate="txtidno" Display="Dynamic" ErrorMessage="身分證字號必填" 
                                        ValidationGroup="person_info"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr ID="tr_remark" runat="server">
                                <td width="150" align="right">
                                    &nbsp;<asp:Label ID="lblRemark" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtremark" runat="server" 
                                        Text='<%# Bind("ext_people") %>' Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="chk_txtremark" runat="server" 
                                        ControlToValidate="txtremark" Display="Dynamic" 
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