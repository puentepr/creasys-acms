<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenCancelRegistedList.ascx.cs"
    Inherits="WebForm_RegistActivity_OpenCancelRegistedList" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/WebForm/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="My" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <contenttemplate> 
    
<asp:Panel ID="panel1" runat="server" BackColor="white" BorderWidth="1" Style="display: none;" Width="800" Height="500" ScrollBars ="Auto" >
    <!---->
    <br /> <asp:updateprogress ID="Updateprogress1" runat="server" DisplayAfter="0">

 <ProgressTemplate>
                <my:UpdateProgress ID="myprogress1" runat="server" />
            </ProgressTemplate>

</asp:updateprogress>
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Text="取消報名清單" SkinID="title"></asp:Label>
    </div>
    <table align="center">
   
          <tr>
            <td align="right">
                <asp:Label ID="lblNATIVE_NAME" runat="server" Text="員工姓名"></asp:Label>
            </td>
         
       <td>
               <asp:TextBox ID="txtname" runat="server" Width ="70px"></asp:TextBox> 
            </td>
       
        <td>
        <asp:Button ID="btnQuery" runat="server"  OnClick ="btnQuery_Click" Text="查詢" ValidationGroup="Query" />
          <asp:Button ID="btnCancel" runat="server" Text="關閉" />
        
        </td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td align="center">
                <TServerControl:TGridView ID="GridView1" runat="server" AllowHoverEffect="True" AllowHoverSelect="True"
                    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                    EnableModelValidation="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                    SkinID="pager" TotalRowCount="0" AllowSorting="True" 
                    OnPageIndexChanged="GridView1_PageIndexChanged" Visible="false" 
                    onsorted="GridView1_Sorted" >
                    <EmptyDataTemplate>
                        <font color='Red' >查無資料</font>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="emp_id" HeaderText="姓名" SortExpression="emp_id" />
                        <asp:BoundField DataField="boss_id" HeaderText="隊長" SortExpression="boss_id" />
                        <asp:BoundField DataField="team_name" HeaderText="隊名" SortExpression="team_name" />
                        <asp:BoundField DataField="createat" HeaderText="報名時間" SortExpression="createat" />
                        <asp:BoundField DataField="regist_by" HeaderText="報名人" SortExpression="regist_by" />
                        <asp:BoundField DataField="cancel_date" HeaderText="取消時間" SortExpression="cancel_date" />
                        <asp:BoundField DataField="cancel_by" HeaderText="取消人" SortExpression="cancel_by" />
                        <asp:BoundField DataField="STATUS" HeaderText="在職狀態" SortExpression="STATUS" />
                    </Columns>
                </TServerControl:TGridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetCancelRegist" TypeName="ACMS.BO.ActivityRegistBO">
                    <SelectParameters>
                        <asp:Parameter DbType="Guid" Name="activity_id" DefaultValue=""/>
                        
                      
                        <asp:Parameter Name="name" Type="String" ConvertEmptyStringToNull="false" DefaultValue=""/>
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <div align="center">
        
      
    </div>
</asp:Panel>
<asp:Button ID="btnDummy" runat="server" SkinID="null"  style="display:none"/>
<ajaxToolkit:ModalPopupExtender ID="mpSearch" runat="server" CancelControlID="btnCancel"
    PopupControlID="panel1" PopupDragHandleControlID="panel1" TargetControlID="btnDummy" />
 </contenttemplate>
</asp:UpdatePanel>