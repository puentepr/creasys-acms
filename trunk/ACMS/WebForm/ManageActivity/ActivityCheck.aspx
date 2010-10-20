<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityCheck.aspx.cs" Inherits="WebForm_ActivityCheck" Title="未命名頁面" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <div class="SpaceDiv">
    <table align="center">
        <tr>
            <td>
                活動名稱
            </td>
            <td>
                <asp:DropDownList ID="ddlActivity" runat="server" DataSourceID="ObjectDataSource_Activity"
                    DataTextField="activity_name" DataValueField="id">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource_Activity" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetAllActivity" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                員工工號
            </td>
            <td>
                <asp:TextBox ID="txtemp_id" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                員工姓名
            </td>
            <td>
                <asp:TextBox ID="txtemp_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="匯出" onclick="btnExport_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" GroupingText="登錄狀態人員列表" Width="400px">
        <div align="center">
            登錄狀態:<asp:DropDownList ID="ddlcheck_status" runat="server">
                <asp:ListItem Value="0">未報到</asp:ListItem>
                <asp:ListItem Value="1">已報到</asp:ListItem>
                <asp:ListItem Value="2">已完成</asp:ListItem>
                <asp:ListItem Value="4">已離職</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnUpdate" runat="server" Text="批次更新" 
                onclick="btnUpdate_Click" /></div>
        <TServerControl:TGridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataSourceID="ObjectDataSource1" SkinID="pager" Width="100%" AllowHoverEffect="True"
            AllowHoverSelect="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
            TotalRowCount="0" DataKeyNames="emp_id">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NATIVE_NAME" HeaderText="員工姓名" SortExpression="NATIVE_NAME" />
                <asp:BoundField DataField="C_DEPT_ABBR" HeaderText="部門" SortExpression="C_DEPT_ABBR" />
                <asp:BoundField DataField="check_status" HeaderText="登錄狀態" ReadOnly="True" SortExpression="check_status" />
            </Columns>
        </TServerControl:TGridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ActivityCheckQuery" TypeName="ACMS.BO.SelectorBO">
            <SelectParameters>
                <asp:Parameter ConvertEmptyStringToNull="False" Name="activity_id" 
                    Type="String" />
                <asp:Parameter ConvertEmptyStringToNull="False" Name="emp_id" Type="String" />
                <asp:Parameter ConvertEmptyStringToNull="False" Name="emp_name" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel></div>
</asp:Content>
