<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="首頁" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" GroupingText="最新個人活動">
               <asp:Label ID="lblGrideView1" runat="server" ForeColor="Red" Text="目前沒有個人活動可以報名... "
                        Visible="False"></asp:Label>
                         <div class="SpaceDiv">
                    
                    <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                        AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSource1"
                        EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True"
                        SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="True" AllowSorting="True"
                        OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                        <EmptyDataTemplate>
                            目前沒有個人活動可以報名...
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                            <asp:TemplateField HeaderText="活動對象" SortExpression="people_type">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnRegist2Agent" runat="server" OnClick="lbtnRegist2Agent_Click">代理報名</asp:LinkButton>
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
                <asp:Label ID="lblGrideView2" runat="server" ForeColor="Red" Text="目前沒有團隊活動可以報名... "
                    Visible="False"></asp:Label>
                <div class="SpaceDiv">
                    <TServerControl:TGridView ID="GridView2" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                        AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSource2"
                        EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True"
                        SkinID="pager" TotalRowCount="0" Width="100%" AllowPaging="True" OnRowDataBound="GridView2_RowDataBound"
                        OnDataBound="GridView2_DataBound">
                        <EmptyDataTemplate>
                            目前沒有團隊活動可以報名...
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                            <asp:TemplateField HeaderText="活動對象" SortExpression="people_type">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
