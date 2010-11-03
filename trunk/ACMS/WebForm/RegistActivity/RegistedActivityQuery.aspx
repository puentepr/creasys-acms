<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="RegistedActivityQuery.aspx.cs" Inherits="WebForm_RegistActivity_RegistedActivityQuery" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="OpenRegistedByMeEmpSelector.ascx" TagName="OpenRegistedByMeEmpSelector"
    TagPrefix="uc1" %>
<%@ Register Src="OpenRegisedTeammemberSelector.ascx" TagName="OpenRegisedTeammemberSelector"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="SpaceDiv">
                <table align="center">
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            活動類型
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblFinish" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="N">執行中活動</asp:ListItem>
                                <asp:ListItem Value="Y">歷史資料查詢</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查詢" />
                        </td>
                    </tr>
                </table>
                <br />
                <TServerControl:TGridView ID="GridView1" runat="server" DataKeyNames="id,activity_type"
                    DataSourceID="ObjectDataSource1" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="True"
                    SkinID="pager" Width="100%" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    AllowHoverEffect="True" AllowHoverSelect="True" TotalRowCount="0" OnRowDataBound="GridView1_RowDataBound">
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
                                <asp:LinkButton ID="lbtnRegistEdit" runat="server" OnClick="lbtnRegistEdit_Click">編輯</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnRegistCancel" runat="server" OnClick="lbtnRegistCancel_Click"
                                    CommandArgument='<%# Eval("regist_deadline","{0:d}")%>' CommandName='<%# Eval("cancelregist_deadline","{0:d}")%>'>取消報名</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="RegistedActivityQuery" TypeName="ACMS.BO.SelectorBO">
                    <SelectParameters>
                        <asp:Parameter Name="activity_name" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="activity_enddate_finish" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="emp_id" Type="String" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <uc1:OpenRegistedByMeEmpSelector ID="OpenRegistedByMeEmpSelector1" runat="server" />
                               <uc2:OpenRegisedTeammemberSelector ID="OpenRegisedTeammemberSelector1" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
