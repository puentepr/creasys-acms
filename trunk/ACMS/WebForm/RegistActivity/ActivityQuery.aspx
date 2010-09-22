<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="ActivityQuery.aspx.cs" Inherits="WebForm_RegistActivity_ActivityQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table>
            <tr>
                <td>
                    ���ʦW��</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    ���ʤ��</td>
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
                    <asp:Button ID="Button1" runat="server" Text="�d��" />
                </td>
            </tr>
        </table>
        <br />
        <TServerControl:TGridView ID="GridView1" runat="server" 
            AllowHoverEffect="True" AllowHoverSelect="True" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" 
            EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" 
            ShowHeaderWhenEmpty="False" SkinID="pager" TotalRowCount="0" Width="100%" 
            onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="activity_name" HeaderText="���ʦW��" 
                    SortExpression="activity_name" />
                <asp:BoundField DataField="people_type" HeaderText="���ʹ�H" 
                    SortExpression="people_type" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="limit_count" HeaderText="���ʤH��" 
                    SortExpression="limit_count" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RegistCount" HeaderText="�w���W�H��" 
                    SortExpression="RegistCount" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="���ʤ��" SortExpression="activity_startdate">
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
                <asp:TemplateField HeaderText="�O�_�B��" SortExpression="is_full">
                    <ItemTemplate>
                        <asp:Label ID="lblisfull" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                            <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegistEdit" runat="server">�s��</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRegistCancel" runat="server">�������W</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </TServerControl:TGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connStr %>" 
            SelectCommand="SELECT A.id,A.activity_name,A.people_type,A.activity_startdate,A.activity_enddate,A.limit_count
,(SELECT COUNT(*) FROM [ActivityRegist] WHERE activity_id=A.id) as RegistCount,B.emp_id
FROM [Activity] A
inner join [ActivityRegist] B on A.id=activity_id and B.emp_id='1111'
"></asp:SqlDataSource>


</asp:Content>

