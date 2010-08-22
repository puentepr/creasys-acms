<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="公佈欄">
    <div class="MyDiv">
        <TServerControl:TGridView ID="TGridView1" runat="server" 
            AllowHoverEffect="True" AllowHoverSelect="True" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" 
            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" 
            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%">
            <Columns>
                <asp:BoundField DataField="event_date" DataFormatString="{0:d}" HeaderText="日期" 
                    SortExpression="event_date" />
                <asp:BoundField DataField="title" HeaderText="內容" SortExpression="title" />
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            SelectCommand="SELECT * FROM [BulletinBoard]"></asp:SqlDataSource>
               </div> </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" GroupingText="最新活動">
     <div class="MyDiv">
        <TServerControl:TGridView ID="TGridView2" runat="server" 
            AllowHoverEffect="True" AllowHoverSelect="True" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource2" 
            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" 
            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%">
            <Columns>
                <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                    SortExpression="activity_name" />
                <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                    SortExpression="people_type" />
                <asp:BoundField DataField="acept_count" HeaderText="錄取人數" 
                    SortExpression="acept_count" />
                <asp:BoundField DataField="register_count" HeaderText="報名人數" 
                    SortExpression="register_count" />
                <asp:BoundField DataField="activity_date" HeaderText="活動日期" 
                    SortExpression="activity_date" />
                <asp:BoundField DataField="is_full" HeaderText="是否額滿" 
                    SortExpression="is_full" />
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            SelectCommand="SELECT * FROM [Activity]"></asp:SqlDataSource>
                </div></asp:Panel>
    <p>
    </p>
</asp:Content>

