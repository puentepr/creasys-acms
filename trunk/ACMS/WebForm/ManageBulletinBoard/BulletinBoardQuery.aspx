<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="BulletinBoardQuery.aspx.cs" Inherits="WebForm_ManageBulletinBoard_BulletinBoardQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table align="center">
        <tr>
            <td>
                ���D
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �o����H
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �o�G���
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy/MM/dd">
                </ajaxToolkit:CalendarExtender>
                ~<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="yyyy/MM/dd">
                </ajaxToolkit:CalendarExtender>
                <asp:Button ID="Button2" runat="server" Text="�d��" />
            &nbsp;
                <asp:LinkButton ID="lbtnNew" runat="server" onclick="lbtnNew_Click">�s�W���G�涵��</asp:LinkButton>
            </td>
        </tr>
    </table>
        <TServerControl:TGridView  ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" SkinID="pager" 
        AllowHoverEffect="True" AllowHoverSelect="True" ShowFooterWhenEmpty="False" 
        ShowHeaderWhenEmpty="False" TotalRowCount="0">
            <Columns>
                <asp:BoundField DataField="title" HeaderText="���D" SortExpression="title" >
                <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="forwho" HeaderText="��H" SortExpression="forwho" >
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="pintop" HeaderText="�m��" SortExpression="pintop" />
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="��s"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="����"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" 
                            CommandName="Edit" onclick="lbtnEdit_Click" Text="�s��"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
    </TServerControl:TGridView >
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:connStr %>" 
        SelectCommand="SELECT * FROM [BulletinBoard]"></asp:SqlDataSource>
</asp:Content>
