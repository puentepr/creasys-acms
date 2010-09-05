<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="ActivityHistoryQuery.aspx.cs" Inherits="WebForm_ActivityHistoryQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="活動歷史資料查詢">
    <div class="SpaceDiv">
        <table>
            <tr>
                <td>
                    活動日期</td>
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
                    月</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    活動主辦單位</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查詢" />
                    <asp:Button ID="Button2" runat="server" Text="匯出" onclick="Button2_Click" />
                </td>
            </tr>
        </table>
        <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
            AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="id" 
            DataSourceID="SqlDataSource2" EnableModelValidation="True" PageSize="2" 
            ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
            TotalRowCount="0" Width="100%">
            <Columns>
                <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                    SortExpression="activity_name" />
                <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                    SortExpression="people_type" />
                <asp:BoundField DataField="activity_date" HeaderText="活動日期" 
                    SortExpression="activity_date" />
                <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日期" />
                <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日期" />
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
    </div>
</asp:Panel>
</asp:Content>

