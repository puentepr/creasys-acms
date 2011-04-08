<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityProgressQuery.aspx.cs" Inherits="WebForm_RegistActivity_ActivityProgressQuery"
    Title="活動進度查詢" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="center">
                        活動名稱:<asp:DropDownList ID="ddlActivity" runat="server" DataSourceID="ObjectDataSource_Activity"
                            DataTextField="activity_name" DataValueField="id" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource_Activity" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetAllMyActivity" TypeName="ACMS.BO.SelectorBO">
                            <SelectParameters>
                                <asp:Parameter Name="emp_id" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <br />
                        <asp:Label ID="lblPS" ForeColor ="Red" runat="server" Text="顏色說明:未報到-灰底；已報到-綠底；已完成-黃底；本人-紅色字體"></asp:Label>
                        &nbsp;<asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource_ActivityProcessQuery"
                            RepeatColumns="3" CellPadding="4" ForeColor="#333333" GridLines="Both" OnItemDataBound="DataList1_ItemDataBound"
                            ShowFooter="False" ShowHeader="False" DataKeyField="emp_id" Width="1000px" 
                            RepeatDirection="Horizontal">
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Width="200px" />
                            <ItemTemplate>
                              <asp:Label ID="lblSEQNO" runat="server" Text='<%# Eval("SEQNO") %>' />
                                <asp:Label ID="NATIVE_NAMELabel" runat="server" Text='<%# Eval("NATIVE_NAME") %>' /><asp:Label
                                    ID="emp_idLabel" runat="server" Text='<%# Eval("WORK_ID") %>' />
                                    <br />
                                <asp:Label ID="check_statusLabel" runat="server" Text='<%# Eval("check_status") %>' />
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:ObjectDataSource ID="ObjectDataSource_ActivityProcessQuery" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="ActivityProcessQuery" TypeName="ACMS.BO.SelectorBO">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlActivity" Name="activity_id" PropertyName="SelectedValue"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
