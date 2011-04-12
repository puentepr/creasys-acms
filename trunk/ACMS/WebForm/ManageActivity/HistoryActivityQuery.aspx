<%@ Page Title="���v��Ƭd��" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="HistoryActivityQuery.aspx.cs" Inherits="HistoryWebForm_ActivityQuery" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<%@ Register src="OpenNameList.ascx" tagname="OpenNameList" tagprefix="uc1" %>--%>
<%@ Register Src="../RegistActivity/OpenRegistedByMeEmpSelector.ascx" TagName="OpenRegistedByMeEmpSelector"
    TagPrefix="uc2" %>
<%@ Register Src="../RegistActivity/OpenRegisedTeammemberSelector.ascx" TagName="OpenRegisedTeammemberSelector"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center">
        <tr>
            <td align ="right">
                ���ʤ��
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                �~<asp:DropDownList ID="ddlMonth" runat="server">
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
            <td align ="right">
                ���ʥD����
            </td>
            <td>
                <asp:DropDownList ID="ddlUnit" runat="server" DataSourceID="ObjectDataSource_Unit"
                    DataTextField="name" DataValueField="id">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectUnit" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="�d��" OnClick="btnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td align ="right">
                ��������
            </td>
            <td>
                <asp:RadioButtonList ID="rblActivity_type" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="True" OnSelectedIndexChanged="rblActivity_type_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">�ӤH����</asp:ListItem>
                    <asp:ListItem Value="2">�ζ�����</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Label ID="lblGrideView1" runat="server" ForeColor="Red" Text="�d�L�ŦX���󪺸��... "
        Visible="False"></asp:Label>
    <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
        AutoGenerateColumns="False" DataKeyNames="id,activity_type" DataSourceID="ObjectDataSource1"
        EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
        SkinID="pager" TotalRowCount="0" Width="1000px" OnRowDataBound="GridView1_RowDataBound"
        OnDataBound="GridView1_DataBound">
        <Columns>
            <asp:BoundField DataField="activity_name" HeaderText="���ʦW��" SortExpression="activity_name"
                ItemStyle-Width="150px" />
            <asp:TemplateField HeaderText="���ʹ�H" SortExpression="people_type" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���ʤ���}�l" SortExpression="activity_startdate">
                <ItemTemplate>
                    <asp:Label ID="lblactivity_startdate" runat="server" Text='<%# Bind("activity_startdate") %>'
                        Width="120px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����" SortExpression="activity_enddate">
                <ItemTemplate>
                    <asp:Label ID="lblactivity_enddate" runat="server" Text='<%# Bind("activity_enddate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���W�I���" SortExpression="regist_deadline">
                <ItemTemplate>
                    <asp:Label ID="lblregist_deadline" runat="server" Text='<%# Bind("regist_deadline") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�������W�I���" SortExpression="cancelregist_deadline">
                <ItemTemplate>
                    <asp:Label ID="lblcancelregist_deadline" runat="server" Text='<%# Bind("cancelregist_deadline") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewActivity" runat="server" OnClick="lbtnViewActivity_Click">�˵����e</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click">�ץX�W��</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelRegist" runat="server" OnClick="lbtnCancelRegist_Click"
                        CommandArgument='<%# Eval("regist_deadline","{0:d}")%>' CommandName='<%# Eval("cancelregist_deadline","{0:d}")%>'
                        Visible="False">�������W</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="ActivityQuery" TypeName="ACMS.BO.SelectorBO">
        <SelectParameters>
            <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="org_id" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="querytype" Type="String" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter ControlID="rblActivity_type" ConvertEmptyStringToNull="False"
                Name="activity_type" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:OpenRegistedByMeEmpSelector ID="OpenRegistedByMeEmpSelector1" runat="server" />
    <uc3:OpenRegisedTeammemberSelector ID="OpenRegisedTeammemberSelector1" runat="server" />
</asp:Content>
