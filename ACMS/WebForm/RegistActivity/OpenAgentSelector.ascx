﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenAgentSelector.ascx.cs"
    Inherits="WebForm_RegistActivity_OpenAgentSelector" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>

 <asp:updateprogress ID="Updateprogress1" runat="server" DisplayAfter="0">

 <ProgressTemplate>
                <my:UpdateProgress ID="myprogress1" runat="server" />
            </ProgressTemplate>

</asp:updateprogress>
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" SkinID="DragPanel"  Style="display: none;"
    ScrollBars="Auto" Width="750px" Height="500px">
    
    <br />
   
    <div align="center" style="cursor:move;">
        <asp:Label ID="lblTitle" runat="server" SkinID="title"></asp:Label>
    </div>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblC_NAME" runat="server" Text="公司別"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlC_NAME" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource_CNAME"
                                DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="ddlC_NAME_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource_CNAME" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="CNAMESelector" TypeName="ACMS.BO.SelectorBO"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup2" runat="server" Text="部門"></asp:Label>
                        </td>
                        <td colspan="3">
                         <asp:CheckBox ID="cbUnderDept" runat="server" Text="含所屬單位" Checked="True" />
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" DataSourceID="ObjectDataSource_Dept"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource_Dept" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="DeptSelectorByCompanyCode" TypeName="ACMS.BO.SelectorBO">
                                <SelectParameters>
                            <asp:ControlParameter ControlID="ddlC_NAME" Name="COMPANYCODE" PropertyName="SelectedValue"
                                ConvertEmptyStringToNull="False" />
                        </SelectParameters>
                                
                                </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup0" runat="server" Text="員工編號"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWORK_ID" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblProgramGroup1" runat="server" Text="員工姓名"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtNATIVE_NAME" runat="server" Width="120px"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查詢" />
                            <asp:Button ID="btnCancel" runat="server" Text="關閉" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <%--                    <TServerControl:TGridView ID="GridView_Employee" runat="server" 
                        AutoGenerateColumns="False" 
                        DataKeyNames="ID" DataSourceID="ObjectDataSource_Employee" 
                        ShowFooterWhenEmpty="False" 
                        ShowHeaderWhenEmpty="True" SkinID="pager" TotalRowCount="0" Width="90%">
                        <Columns>

                        </Columns>
                    </TServerControl:TGridView>--%>
                <TServerControl:TGridView ID="GridView_Employee" runat="server" AllowHoverEffect="True"
                    AllowHoverSelect="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    TotalRowCount="0" AutoGenerateColumns="False" DataKeyNames="ID" SkinID="pager"
                    EnableModelValidation="True" DataSourceID="ObjectDataSource_Employee" 
                    AllowPaging="True" OnPageIndexChanged="GridView_Employee_PageIndexChanged"  OnDataBound ="GridView_Employee_DataBound">
                    <EmptyDataTemplate>
                        <font color="red">查詢結果無資料</font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="C_DEPT_NAME" HeaderText="部門" SortExpression="C_DEPT_NAME" />
                        <asp:BoundField DataField="WORK_ID" HeaderText="員工編號" ReadOnly="True" SortExpression="WORK_ID" />
                        <asp:BoundField DataField="NATIVE_NAME" HeaderText="姓名" SortExpression="NATIVE_NAME" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSelect" Enabled='<%#Eval("keyValue") %>' runat="server" OnClick="lbtnSelect_Click">選擇</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource_Employee" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="RegistableMember" TypeName="ACMS.BO.SelectorBO">
                    <SelectParameters>
                        <asp:Parameter Name="DEPT_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="WORK_ID" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="NATIVE_NAME" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter Name="activity_id" Type="String" ConvertEmptyStringToNull="false" />
                        <asp:Parameter DefaultValue="1" Name="activity_type" Type="String" />
                        <asp:Parameter  Name="UnderDept" Type="Boolean" ConvertEmptyStringToNull="false" />
                        <asp:Parameter DefaultValue="" Name="Company_ID" Type="String"  ConvertEmptyStringToNull="false" />
                        
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel" 
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
