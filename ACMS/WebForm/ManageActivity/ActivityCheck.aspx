<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityCheck.aspx.cs" Inherits="WebForm_ActivityCheck" Title="活動進度登錄" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="<%=this.ResolveUrl("~/js/JScript.js") %>" type="text/javascript"></script>

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
                        DataTextField="activity_name" DataValueField="id" Width="350px">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource_Activity" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAllActivity" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                </td>
                <td>
                    部門
                </td>
                <td>
                    <asp:DropDownList ID="ddlDEPT_ID" runat="server" DataSourceID="ObjectDataSource_Dept"
                        DataTextField="Text" DataValueField="Value">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="DeptSelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    員工工號
                </td>
                <td>
                    <asp:TextBox ID="txtemp_id" runat="server"></asp:TextBox>
                </td>
                <td>
                    員工姓名
                </td>
                <td>
                    <asp:TextBox ID="txtemp_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" GroupingText="登錄狀態人員列表">
                    <div align="center">
                        登錄狀態:<asp:DropDownList ID="ddlcheck_status" runat="server">
                            <asp:ListItem Value="0">未報到</asp:ListItem>
                            <asp:ListItem Value="1">已報到</asp:ListItem>
                            <asp:ListItem Value="2">已完成</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click" /></div>
                    <TServerControl:TGridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        DataSourceID="ObjectDataSource1" SkinID="pager" Width="100%" AllowHoverEffect="True"
                        AllowHoverSelect="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                        TotalRowCount="0" DataKeyNames="emp_id,activity_type" AllowPaging="True" 
                        AllowSorting="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input id="cbCheckAll" onclick="Check2(this,'GridView1','CheckBox1');" runat="server"
                                        type="checkbox" /><asp:Literal ID="Literal1" runat="server">全選</asp:Literal>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" SortExpression="C_DEPT_NAME" />
                            <asp:BoundField DataField="WORK_ID" HeaderText="工號" SortExpression="WORK_ID" />
                            <asp:BoundField DataField="NATIVE_NAME" HeaderText="員工姓名" SortExpression="NATIVE_NAME" />
                            <asp:BoundField DataField="createat" HeaderText="報名時間" 
                                SortExpression="createat" />
                            <asp:BoundField DataField="check_status" HeaderText="登錄狀態" ReadOnly="True" SortExpression="check_status" />
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="ActivityCheckQuery" TypeName="ACMS.BO.SelectorBO">
                        <SelectParameters>
                            <asp:Parameter Name="activity_id" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="DEPT_ID" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="emp_id" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="emp_name" Type="String" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
