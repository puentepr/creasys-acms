<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="最新個人活動">
        <div class="SpaceDiv">
            <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" 
                AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="SqlDataSource1" EnableModelValidation="True" 
                onrowdatabound="GridView1_RowDataBound" PageSize="2" 
                ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                TotalRowCount="0" Width="100%">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                        SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                        SortExpression="people_type">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="limit_count" HeaderText="活動人數" 
                        SortExpression="limit_count">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RegistCount" HeaderText="已報名人數" 
                        SortExpression="RegistCount">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate" runat="server" 
                                Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>
                            ~<asp:Label ID="lblactivity_enddate" runat="server" 
                                Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" 
                                Text='<%# Bind("activity_startdate") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否額滿" SortExpression="is_full">
                        <ItemTemplate>
                            <asp:Label ID="lblisfull" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegistEdit" runat="server">編輯</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.id,A.activity_name,A.people_type,A.activity_startdate,A.activity_enddate,A.limit_count
,(SELECT COUNT(*) FROM [ActivityRegist] WHERE activity_id=A.id) as RegistCount,B.emp_id
FROM [Activity] A
left join [ActivityRegist] B on A.id=activity_id and B.emp_id='1111'
WHERE A.activity_type=1"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" GroupingText="最新團體活動">
        <div class="SpaceDiv">
            <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" 
                AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="SqlDataSource2" EnableModelValidation="True" 
                onrowdatabound="GridView2_RowDataBound" PageSize="2" 
                ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                TotalRowCount="0" Width="100%">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                        SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                        SortExpression="people_type">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="limit_count" HeaderText="活動人數" 
                        SortExpression="limit_count">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RegistCount" HeaderText="已報名人數" 
                        SortExpression="RegistCount">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate0" runat="server" 
                                Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>
                            ~<asp:Label ID="lblactivity_enddate0" runat="server" 
                                Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" 
                                Text='<%# Bind("activity_startdate") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否額滿" SortExpression="is_full">
                        <ItemTemplate>
                            <asp:Label ID="lblisfull0" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegistEdit0" runat="server">編輯</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.id,A.activity_name,A.people_type,A.activity_startdate,A.activity_enddate,A.limit_count
,(SELECT COUNT(*) FROM [ActivityRegist] WHERE activity_id=A.id) as RegistCount,B.emp_id
FROM [Activity] A
left join [ActivityRegist] B on A.id=activity_id and B.emp_id='1111'
WHERE A.activity_type=2 "></asp:SqlDataSource>
        </div>
    </asp:Panel>
    <p>
    </p>
</asp:Content>
