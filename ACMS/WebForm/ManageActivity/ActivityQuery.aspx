<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityQuery.aspx.cs" Inherits="WebForm_ActivityQuery" %>

<%--<%@ Register src="OpenNameList.ascx" tagname="OpenNameList" tagprefix="uc1" %>--%>

<%@ Register src="../RegistActivity/OpenRegistedByMeEmpSelector.ascx" tagname="OpenRegistedByMeEmpSelector" tagprefix="uc2" %>
<%@ Register src="../RegistActivity/OpenRegisedTeammemberSelector.ascx" tagname="OpenRegisedTeammemberSelector" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center">
        <tr>
            <td>
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
            <td>
                ���ʥD����
            </td>
            <td>
                <asp:DropDownList ID="ddlUnit" runat="server" 
                    DataSourceID="ObjectDataSource_Unit" DataTextField="Text" 
                    DataValueField="Value">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="UnitSelector" 
                    TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="�d��" onclick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <TServerControl:TGridView ID="GridView1" runat="server" 
        AllowHoverEffect="True" AllowHoverSelect="True"
        AutoGenerateColumns="False" DataKeyNames="id,activity_type" 
        DataSourceID="ObjectDataSource1" EnableModelValidation="True"
        PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager"
        TotalRowCount="0" Width="100%">
        <Columns>
            <asp:BoundField DataField="activity_name" HeaderText="���ʦW��" SortExpression="activity_name" />
            <asp:BoundField DataField="people_type" HeaderText="���ʹ�H" SortExpression="people_type" />
       <asp:TemplateField HeaderText="���ʤ��" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate" runat="server" 
                                Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>~<br/>
                            <asp:Label ID="lblactivity_enddate" runat="server" 
                                Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
            <asp:BoundField DataField="regist_deadline" HeaderText="���W�I���" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="cancelregist_deadline" HeaderText="�������W�I���" 
                DataFormatString="{0:d}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewActivity" runat="server" 
                        onclick="lbtnViewActivity_Click">�˵����e</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnExport" runat="server" onclick="lbtnExport_Click">�ץX�W��</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelRegist" runat="server" 
                        onclick="lbtnCancelRegist_Click" CommandArgument='<%# Eval("regist_deadline","{0:d}")%>' CommandName='<%# Eval("cancelregist_deadline","{0:d}")%>'>�������W</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
       SelectMethod="ActivityQuery" 
        TypeName="ACMS.BO.SelectorBO">
        <SelectParameters>
            <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="org_id" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="querytype" Type="String" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:OpenRegistedByMeEmpSelector ID="OpenRegistedByMeEmpSelector1" runat="server" />
        <uc3:OpenRegisedTeammemberSelector ID="OpenRegisedTeammemberSelector1" runat="server" />
</asp:Content>