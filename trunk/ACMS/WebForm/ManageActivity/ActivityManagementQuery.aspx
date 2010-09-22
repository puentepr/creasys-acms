<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityManagementQuery.aspx.cs" Inherits="WebForm_ActivityManagementQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="新增活動">
        <div class="SpaceDiv">
            <asp:Button ID="btnAddActivity" runat="server" OnClick="btnAddActivity_Click" Text="新增個人活動" />
            <asp:Button ID="btnAddActivityTeam" runat="server" OnClick="btnAddActivityTeam_Click"
                Text="新增團隊活動" />
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" GroupingText="查詢活動">
        <div class="SpaceDiv">
            <table>
                <tr>
                    <td>
                        活動名稱
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        活動日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy/MM/dd">
                        </ajaxToolkit:CalendarExtender>
                        ~<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="yyyy/MM/dd">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="查詢" />
                    </td>
                </tr>
            </table>
            <br />
            <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource2" EnableModelValidation="True"
                PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager"
                TotalRowCount="0" Width="100%">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" SortExpression="people_type" />
                    <asp:BoundField DataField="acept_count" HeaderText="錄取人數" SortExpression="acept_count" />
                    <asp:BoundField DataField="register_count" HeaderText="報名人數" SortExpression="register_count" />
                    <asp:BoundField DataField="activity_date" HeaderText="活動日期" SortExpression="activity_date" />
                    <asp:BoundField DataField="is_full" HeaderText="是否額滿" SortExpression="is_full" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEditActivaty" runat="server" OnClick="lbtnEditActivaty_Click">編輯</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return confirm('族群名單將清空!確定要繼續嗎?')">取消族群設定</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connStr %>"
                SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
        </div>
    </asp:Panel>
</asp:Content>
