<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityQuery.aspx.cs" Inherits="WebForm_ActivityQuery" %>

<%@ Register src="OpenNameList.ascx" tagname="OpenNameList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                活動日期
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>2010</asp:ListItem>
                    <asp:ListItem>2011</asp:ListItem>
                </asp:DropDownList>
                年<asp:DropDownList ID="DropDownList2" runat="server">
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
                月
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                活動主辦單位
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="查詢" />
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
            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
            <asp:BoundField DataField="people_type" HeaderText="活動對象" SortExpression="people_type" />
            <asp:BoundField DataField="activity_date" HeaderText="活動日期" SortExpression="activity_date" />
            <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日" />
            <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnShowPerson" runat="server" 
                        onclick="lbtnShowPerson_Click">檢視內容</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnExport" runat="server">匯出名單</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelRegist" runat="server">取消報名</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
        SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
    <uc1:OpenNameList ID="OpenNameList1" runat="server" />
</asp:Content>
