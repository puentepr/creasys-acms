<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistActivity_Query.ascx.cs"
    Inherits="WebForm_RegistActivity_RegistActivity_Query" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<div class="SpaceDiv">
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
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtactivity_startdate"
                                    Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>
                                ~<asp:TextBox ID="txtactivity_enddate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtactivity_enddate"
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
                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowHoverEffect="True"
                        AllowHoverSelect="True" TotalRowCount="0" EnableModelValidation="True" 
                        onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name" />
                            <asp:TemplateField HeaderText="活動對象" SortExpression="people_type">
                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                                <ItemTemplate>
                                    <asp:Label ID="lblactivity_startdate" runat="server" Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>
                                    ~<br />
                                    <asp:Label ID="lblactivity_enddate" runat="server" Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日"  SortExpression="regist_deadline" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日"  SortExpression="cancelregist_deadline" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="registable_count" HeaderText="剩餘名額" SortExpression="registable_count">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnRegist" runat="server" OnClick="lbtnRegist_Click">報名</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnRegistAgent" runat="server" OnClick="lbtnRegistAgent_Click">代理報名</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </TServerControl:TGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="RegistActivity_Query" TypeName="ACMS.BO.SelectorBO">
                        <SelectParameters>
                            <asp:Parameter Name="activity_name" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
                            <asp:Parameter Name="activity_type" Type="String" />
                            <asp:Parameter Name="emp_id" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
