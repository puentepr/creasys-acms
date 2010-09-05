<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="BulletinBoardEdit.aspx.cs" Inherits="WebForm_ManageBulletinBoard_BulletinBoardEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="SqlDataSource1" onmodechanged="FormView1_ModeChanged">
        <EditItemTemplate>
            id:
            <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
            <br />
            title:
            <asp:TextBox ID="titleTextBox" runat="server" Text='<%# Bind("title") %>' />
            <br />
            forwho:
            <asp:TextBox ID="forwhoTextBox" runat="server" Text='<%# Bind("forwho") %>' />
            <br />
            detail:
            <asp:TextBox ID="detailTextBox" runat="server" Text='<%# Bind("detail") %>' />
            <br />
            poststart_date:
            <asp:TextBox ID="poststart_dateTextBox" runat="server" 
                Text='<%# Bind("poststart_date") %>' />
            <br />
            postend_date:
            <asp:TextBox ID="postend_dateTextBox" runat="server" 
                Text='<%# Bind("postend_date") %>' />
            <br />
            pintop:
            <asp:TextBox ID="pintopTextBox" runat="server" Text='<%# Bind("pintop") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="更新" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="取消" />
        </EditItemTemplate>
        <InsertItemTemplate>
            id:
            <asp:TextBox ID="idTextBox" runat="server" Text='<%# Bind("id") %>' />
            <br />
            title:
            <asp:TextBox ID="titleTextBox" runat="server" Text='<%# Bind("title") %>' />
            <br />
            forwho:
            <asp:TextBox ID="forwhoTextBox" runat="server" Text='<%# Bind("forwho") %>' />
            <br />
            detail:
            <asp:TextBox ID="detailTextBox" runat="server" Text='<%# Bind("detail") %>' />
            <br />
            poststart_date:
            <asp:TextBox ID="poststart_dateTextBox" runat="server" 
                Text='<%# Bind("poststart_date") %>' />
            <br />
            postend_date:
            <asp:TextBox ID="postend_dateTextBox" runat="server" 
                Text='<%# Bind("postend_date") %>' />
            <br />
            pintop:
            <asp:TextBox ID="pintopTextBox" runat="server" Text='<%# Bind("pintop") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="插入" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="取消" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table align="center">
                <tr>
                    <td>
                        標題 
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>' 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        詳細內容</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" Height="140px" 
                            Text='<%# Bind("detail") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        發布對象 
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("forwho") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        發佈日期 
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" 
                            Text='<%# Bind("poststart_date") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                            Format="yyyy/MM/dd" TargetControlID="txtStartDate">
                        </ajaxToolkit:CalendarExtender>
                        ~<asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("postend_date") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                            Format="yyyy/MM/dd" TargetControlID="txtEndDate">
                        </ajaxToolkit:CalendarExtender>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        置頂</td>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("pintop") %>' 
                            Text="是" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="存檔" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" onclientclick="reset();" Text="取消" />
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:connStr %>" 
        SelectCommand="SELECT * FROM [BulletinBoard] WHERE ([id] = @id)">
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    
    
</asp:Content>

