<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyPager.ascx.cs" Inherits="MyPager" %>
<table width="100%">
    <tr align="left">
        <td>
            <asp:Button ID="btnFirst" runat="server" CommandName="Page" CommandArgument="First"
                Text="First" />
            <asp:Button ID="btnPrev" runat="server" CommandName="Page" CommandArgument="Prev"
                Text="Prev" />
            <asp:Dropdownlist id="ddlCurrentPage" runat="server" autopostback="true" 
                onselectedindexchanged="ddlCurrentPage_SelectedIndexChanged">
                    </asp:Dropdownlist>
            <asp:Button ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
                Text="Next" />
            <asp:Button ID="btnLast" runat="server" CommandName="Page" CommandArgument="Last"
                Text="Last" />
        </td>
        <td align="right">
            <asp:Label ID="lblRowCountInfo" runat="server"></asp:Label>
        </td>
    </tr>
</table>
