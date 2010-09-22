<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityQuery.aspx.cs" Inherits="WebForm_ActivityQuery" %>

<%@ Register src="OpenNameList.ascx" tagname="OpenNameList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                ���ʤ��
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>2010</asp:ListItem>
                    <asp:ListItem>2011</asp:ListItem>
                </asp:DropDownList>
                �~<asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                ��
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                ���ʥD����
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="�d��" />
            </td>
        </tr>
    </table>
    <TServerControl:TGridView ID="gvActivityList" runat="server" 
        AllowHoverEffect="True" AllowHoverSelect="True"
        AutoGenerateColumns="False" DataKeyNames="id" 
        DataSourceID="SqlDataSource2" EnableModelValidation="True"
        PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager"
        TotalRowCount="0" Width="100%">
        <Columns>
            <asp:BoundField DataField="activity_name" HeaderText="���ʦW��" SortExpression="activity_name" />
            <asp:BoundField DataField="people_type" HeaderText="���ʹ�H" SortExpression="people_type" />
            <asp:BoundField DataField="activity_date" HeaderText="���ʤ��" SortExpression="activity_date" />
            <asp:BoundField DataField="regist_deadline" HeaderText="���W�I���" />
            <asp:BoundField DataField="cancelregist_deadline" HeaderText="�������W�I���" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnShowPerson" runat="server" 
                        onclick="lbtnShowPerson_Click">�˵����e</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnExport" runat="server">�ץX�W��</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelRegist" runat="server">�������W</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
        SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
    <uc1:OpenNameList ID="OpenNameList1" runat="server" />
</asp:Content>
