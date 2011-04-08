<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistActivity_Query.ascx.cs"
    Inherits="WebForm_RegistActivity_RegistActivity_Query" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>

    
            <asp:Panel ID="Panel2" runat="server" GroupingText="查詢活動">
               
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
                                ~<asp:TextBox ID="txtactivity_enddate" runat="server"></asp:TextBox><ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtactivity_enddate"
                                    Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="lblGrideView1" runat="server" ForeColor="Red" Text="查無符合條件的資料... "
                        Visible="False"></asp:Label>
                   
                            <TServerControl:TGridView ID="GridView1" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSource1"
                                ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True" SkinID="pager" Width="100%"
                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowHoverEffect="True"
                                AllowHoverSelect="True" TotalRowCount="0" EnableModelValidation="True" OnRowDataBound="GridView1_RowDataBound"
                                OnDataBound="GridView1_DataBound" >
                                <Columns>
                                 
                                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" SortExpression="activity_name"
                                        ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="活動對象" SortExpression="people_type" ItemStyle-Width="150px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:HiddenField ID="hiID" runat="server"   Value='<%# Bind("id") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="活動日期開始" SortExpression="activity_startdate">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblactivity_startdate" runat="server" Text='<%# Bind("activity_startdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="活動日期結束" SortExpression="activity_enddate">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblactivity_enddate" runat="server" Text='<%# Bind("activity_enddate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日" SortExpression="regist_deadline"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cancelregist_deadline"  HeaderText="取消報名截止日" SortExpression="cancelregist_deadline"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
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
                   
               
            </asp:Panel>
      
