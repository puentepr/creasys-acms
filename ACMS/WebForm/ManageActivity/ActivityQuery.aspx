<%@ Page Title="報名狀況查詢" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityQuery.aspx.cs" Inherits="WebForm_ActivityQuery" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../RegistActivity/OpenRegisedTeammemberSelector.ascx" TagName="OpenRegisedTeammemberSelector"
    TagPrefix="uc1" %>
<%@ Register Src="OpenRegistedList.ascx" TagName="OpenRegistedList" TagPrefix="uc4" %>
<%@ Register src="OpenCancelRegistedList.ascx" tagname="OpenCancelRegistedList" tagprefix="uc5" %>
<%--<%@ Register src="OpenNameList.ascx" tagname="OpenNameList" tagprefix="uc1" %>--%>
<%@ Register Src="OpenRegistedByMeEmpSelector.ascx" TagName="OpenRegistedByMeEmpSelector"
    TagPrefix="uc2" %>
<%@ Register Src="OpenRegistedTeammemberSelector.ascx" TagName="OpenRegistedTeammemberSelector"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <table align="center">
        <tr>
            <td align ="right">
                活動日期
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                年<asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                月
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align ="right">
                活動主辦單位
            </td>
            <td>
                <asp:DropDownList ID="ddlUnit" runat="server" DataSourceID="ObjectDataSource_Unit"
                    DataTextField="name" DataValueField="id">
                </asp:DropDownList>
             
                <asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectUnit" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td align ="right">
                活動類型
                </td>
            <td>
                 <asp:RadioButtonList ID="rblActivity_type" runat="server" 
                     RepeatDirection="Horizontal" AutoPostBack="True" 
                     onselectedindexchanged="rblActivity_type_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">個人活動</asp:ListItem>
                                <asp:ListItem Value="2">團隊活動</asp:ListItem>
                            </asp:RadioButtonList></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:Label ID="lblGrideView1" runat="server" ForeColor="Red" Text="查無符合條件的資料... "
        Visible="False"></asp:Label>
    <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
        AutoGenerateColumns="False" DataKeyNames="id,activity_type" DataSourceID="ObjectDataSource1"
        EnableModelValidation="True" PageSize="2" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
        SkinID="pager" TotalRowCount="0" Width="100%" OnRowDataBound="GridView1_RowDataBound"
        OnDataBound="GridView1_DataBound">
        <Columns>
            <asp:BoundField DataField="activity_name" HeaderText="活動名稱" 
                SortExpression="activity_name"  ItemStyle-Width ="150px">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="活動對象" SortExpression="people_type" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("people_type") %>' 
                        Width="150px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="活動日期開始" SortExpression="activity_startdate">
                <ItemTemplate>
                    <asp:Label ID="lblactivity_startdate" runat="server" Text='<%# Bind("activity_startdate","{0:s}") %>'
                        Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="活動日期結束" SortExpression="activity_enddate">
                <ItemTemplate>
                    <asp:Label ID="lblactivity_enddate" runat="server" Text='<%# Bind("activity_enddate","{0:s}") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="regist_deadline" HeaderText="報名截止日" DataFormatString="{0:d}" />
            <asp:BoundField DataField="cancelregist_deadline" HeaderText="取消報名截止日" DataFormatString="{0:d}" />
            <asp:BoundField DataField="RegisterCount" HeaderText="報名人數" 
                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewActivity" runat="server" OnClick="lbtnViewActivity_Click">檢視內容</asp:LinkButton></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewActivityList" runat="server" OnClick="lbtnViewActivityList_Click">檢視名單</asp:LinkButton></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelRegist" runat="server" OnClick="lbtnCancelRegist_Click"
                        CommandArgument='<%# Eval("regist_deadline","{0:d}")%>' CommandName='<%# Eval("cancelregist_deadline","{0:d}")%>'>取消報名</asp:LinkButton></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click">匯出名單</asp:LinkButton></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCancelList" runat="server" 
                        onclick="lbtnCancelList_Click" >取消查詢</asp:LinkButton></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <RowStyle Wrap="True" />
    </TServerControl:TGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="ActivityQuery" TypeName="ACMS.BO.SelectorBO">
        <SelectParameters>
            <asp:Parameter Name="activity_startdate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="activity_enddate" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="org_id" Type="String" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="querytype" Type="String" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter ControlID="rblActivity_type" 
                ConvertEmptyStringToNull="False" Name="activity_type" 
                PropertyName="SelectedValue" Type="String" />
            
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:OpenRegistedByMeEmpSelector ID="OpenRegistedByMeEmpSelector1" runat="server" />
    <uc3:OpenRegistedTeammemberSelector ID="OpenRegistedTeammemberSelector1" runat="server" />
    <uc1:OpenRegisedTeammemberSelector ID="OpenRegisedTeammemberSelector1" runat="server" 
                Visible="False" />
    <uc4:OpenRegistedList ID="OpenRegistedList1" runat="server" />    
   <uc5:OpenCancelRegistedList ID="OpenCancelRegistedList1" runat="server" />
   
     
</asp:Content>
