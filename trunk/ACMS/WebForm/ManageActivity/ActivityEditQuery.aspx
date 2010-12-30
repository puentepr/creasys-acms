﻿<%@ Page Title="活動管理" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityEditQuery.aspx.cs" Inherits="WebForm_ActivityEditQuery" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" GroupingText="查詢活動">
                <div class="SpaceDiv">
                    <table>
                        <tr>
                            <td>
                                活動名稱
                            </td>
                            <td>
                                <asp:TextBox ID="txtactivity_name" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                活動日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtactivity_startdate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtactivity_startdate"
                                    Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>
                                ~<asp:TextBox ID="txtactivity_enddate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtactivity_enddate"
                                    Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <TServerControl:TGridView ID="GridView1" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSource1"
                        ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True" SkinID="pager" Width="100%"
                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                        OnRowDataBound="GridView1_RowDataBound" AllowHoverEffect="True" 
                        AllowHoverSelect="True" ondatabound="GridView1_DataBound" TotalRowCount="0">
                        <Columns>
                            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                            <asp:TemplateField HeaderText="活動對象" SortExpression="people_type">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="limit_count" HeaderText="活動人數" SortExpression="limit_count">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="registed_count" HeaderText="已報名人數" SortExpression="registed_count">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                                <ItemTemplate>
                                    <asp:Label ID="lblactivity_startdate0" runat="server" Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>~<br />
                                    <asp:Label ID="lblactivity_enddate0" runat="server" Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="registable_count" HeaderText="剩餘名額" SortExpression="registable_count">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEditActivaty" runat="server" OnClick="lbtnEditActivaty_Click">編輯</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelActivaty" runat="server" OnClientClick="return confirm('所有相關資料將一併刪除!確定要繼續嗎?')"
                                        OnClick="lbtnDelActivaty_Click">刪除</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="ActivityEditQuery" TypeName="ACMS.BO.SelectorBO">
                        <SelectParameters>
                            <asp:Parameter Name="activity_name" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
