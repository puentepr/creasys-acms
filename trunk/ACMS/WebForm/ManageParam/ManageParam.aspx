<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="ManageParam.aspx.cs" Inherits="WebForm_ManageParam_ManageParam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <table align="center">
                    <tr>
                        <td>
                <asp:FormView ID="FormView1" runat="server" DataKeyNames="param_type,param_key" 
                    DataSourceID="SqlDataSource1" DefaultMode="Insert">                    
                    <InsertItemTemplate>
                        <table align="center">
                            <tr>
                                <td>
                                    參數類別</td>
                                <td>
                                    <asp:TextBox ID="lblparam_type" runat="server" 
                                        Text='<%# Bind("param_type") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    參數名稱</td>
                                <td>
                                    <asp:TextBox ID="lblparam_key" runat="server" Text='<%# Bind("param_key") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    參數值</td>
                                <td>
                                    <asp:TextBox ID="lblparam_value" runat="server" 
                                        Text='<%# Bind("param_value") %>' />
                                    <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="新增" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>                    
                </asp:FormView>
                        </td>
                    </tr>
                </table>

    <TServerControl:TGridView  ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" 
        SkinID="pager" AllowHoverEffect="True" AllowHoverSelect="True" 
        ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False" TotalRowCount="0">
        <Columns>
            <asp:TemplateField HeaderText="參數類別" SortExpression="param_type">
                <EditItemTemplate>
                    <asp:TextBox ID="txtparam_type" runat="server" Text='<%# Bind("param_type") %>' 
                        Width="98%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblparam_type" runat="server" Text='<%# Bind("param_type") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="參數名稱" SortExpression="param_key">
                <EditItemTemplate>
                    <asp:TextBox ID="txtparam_key" runat="server" Text='<%# Bind("param_key") %>' 
                        Width="98%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblparam_key" runat="server" Text='<%# Bind("param_key") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="參數值" SortExpression="param_value">
                <EditItemTemplate>
                    <asp:TextBox ID="txtparam_value" runat="server" 
                        Text='<%# Bind("param_value") %>' Width="98%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblparam_value" runat="server" Text='<%# Bind("param_value") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="更新"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="取消"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="編輯"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Delete" Text="刪除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </TServerControl:TGridView >
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:connStr %>" 
        
                    SelectCommand="SELECT id, param_type, param_key, param_value FROM ParamSetting ORDER BY param_type" 
                    DeleteCommand="DELETE FROM [ParamSetting] WHERE [id] = @id" 
                    InsertCommand="INSERT INTO [ParamSetting] ([param_type], [param_key], [param_value]) VALUES (@param_type, @param_key, @param_value)" UpdateCommand="UPDATE [ParamSetting] SET [param_value] = @param_value ,[param_type] = @param_type, [param_key] = @param_key 
WHERE  [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="param_value" />
            <asp:Parameter Name="param_type" />
            <asp:Parameter Name="param_key" />
            <asp:Parameter Name="id" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="param_type" Type="String" />
            <asp:Parameter Name="param_key" Type="String" />
            <asp:Parameter Name="param_value" Type="String" />
        </InsertParameters>
                </asp:SqlDataSource>

</asp:Content>

