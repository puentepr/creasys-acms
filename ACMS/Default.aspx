<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="最新個人活動">
        <div class="SpaceDiv">
            <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSource1"
                EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True"
                SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="True" 
                AllowSorting="True" onrowdatabound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" SortExpression="people_type">
                    </asp:BoundField>
                    <asp:BoundField DataField="limit_count" HeaderText="活動人數" SortExpression="limit_count">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate" runat="server" Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>~<br />
                            <asp:Label ID="lblactivity_enddate" runat="server" Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="registed_count" HeaderText="已報名人數" SortExpression="registed_count">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="registable_count" HeaderText="剩餘名額" SortExpression="registable_count">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegist1" runat="server" OnClick="lbtnRegist1_Click">報名</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="NewActivityList" TypeName="ACMS.BO.SelectorBO">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="activity_type" Type="String" />
                    <asp:Parameter Name="emp_id" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" GroupingText="最新團體活動">
        <div class="SpaceDiv">
            <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSource2"
                EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True"
                SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="True" 
                onrowdatabound="GridView2_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" SortExpression="people_type">
                    </asp:BoundField>
                    <asp:BoundField DataField="limit_count" HeaderText="活動隊數" SortExpression="limit_count">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate0" runat="server" Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>~<br />
                            <asp:Label ID="lblactivity_enddate0" runat="server" Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:BoundField DataField="registed_count" HeaderText="已報名隊數" SortExpression="registed_count">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="registable_count" HeaderText="剩餘隊數" SortExpression="registable_count">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegist2" runat="server" OnClick="lbtnRegist2_Click">報名</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="NewActivityList" TypeName="ACMS.BO.SelectorBO">
                <SelectParameters>
                    <asp:Parameter DefaultValue="2" Name="activity_type" Type="String" />
                    <asp:Parameter Name="emp_id" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:Panel>
    <p>
    </p>
</asp:Content>
