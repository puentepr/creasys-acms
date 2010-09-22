<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistActivityQuery.ascx.cs" Inherits="WebForm_RegistActivity_RegistActivityQuery" %>
<%@ Register src="OpenRegistedEmployeeSelector.ascx" tagname="OpenRegistedEmployeeSelector" tagprefix="uc1" %>
<%@ Register src="OpenRegistedTeamSelector.ascx" tagname="OpenRegistedTeamSelector" tagprefix="uc2" %>

    <div class="SpaceDiv">
        <table align="center">
            <tr>
                <td>
                    活動名稱</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    活動日期</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender
                        ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy/MM/dd">
                    </ajaxToolkit:CalendarExtender>
                    ~<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                        TargetControlID="txtEndDate" Format="yyyy/MM/dd">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>列出已報名活動</asp:ListItem>
                        <asp:ListItem>列出未報名活動</asp:ListItem>
                        <asp:ListItem>列出已代理報名活動</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查詢" />
                </td>
            </tr>
        </table>
        <br />
            <TServerControl:TGridView ID="gv_Activity" runat="server" AllowHoverEffect="True" 
                AllowHoverSelect="True" AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="SqlDataSource1" EnableModelValidation="True" 
                onrowdatabound="gv_Activity_RowDataBound" PageSize="2" 
                ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" SkinID="pager" 
                TotalRowCount="0" Width="100%">
                <Columns>
                    <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                        SortExpression="activity_name" />
                    <asp:BoundField DataField="people_type" HeaderText="活動對象" 
                        SortExpression="people_type">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="活動日期" SortExpression="activity_startdate">
                        <ItemTemplate>
                            <asp:Label ID="lblactivity_startdate" runat="server" 
                                Text='<%# Bind("activity_startdate", "{0:g}") %>'></asp:Label>
                            ~<br />
                            <asp:Label ID="lblactivity_enddate" runat="server" 
                                Text='<%# Bind("activity_enddate", "{0:g}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" 
                                Text='<%# Bind("activity_startdate") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                       <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日" 
                        DataFormatString="{0:d}" />
                <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日" 
                        DataFormatString="{0:d}" />
                    <asp:TemplateField HeaderText="是否額滿" SortExpression="is_full">
                        <ItemTemplate>
                            <asp:Label ID="lblisfull" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                                   <asp:TemplateField>
                    <ItemTemplate>
                    <asp:LinkButton ID="lbtnRegist" runat="server" onclick="lbtnRegist_Click">報名</asp:LinkButton>
                    <br />
                        <asp:LinkButton ID="lbtnRegistEdit" runat="server" 
                            onclick="lbtnRegistEdit_Click">報名資料修改</asp:LinkButton>
                        <br />
                        <asp:LinkButton ID="lbtnCancelRegist" runat="server" onclick="lbtnCancelRegist_Click">取消報名</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </TServerControl:TGridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:connStr %>" SelectCommand="SELECT A.id,A.activity_name,A.people_type,A.activity_startdate,A.activity_enddate,A.limit_count
,(SELECT COUNT(*) FROM [ActivityRegist] WHERE activity_id=A.id) as RegistCount,A.regist_deadline,A.cancelregist_deadline,B.emp_id
FROM [Activity] A
left join [ActivityRegist] B on A.id=activity_id and B.emp_id='1111'
WHERE A.activity_type=1"></asp:SqlDataSource>
        <uc1:OpenRegistedEmployeeSelector ID="OpenRegistedEmployeeSelector1" 
            runat="server" Visible="False" />
        <uc2:OpenRegistedTeamSelector ID="OpenRegistedTeamSelector1" runat="server" 
            Visible="False" />
    </div>
