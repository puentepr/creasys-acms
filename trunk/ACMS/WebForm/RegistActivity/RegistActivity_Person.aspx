<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.master" AutoEventWireup="true" CodeFile="RegistActivity_Person.aspx.cs" Inherits="WebForm_RegistActivity_RegistActivity_Person" Title="未命名頁面" %>

<%@ Register src="RegistActivityQuery.ascx" tagname="RegistActivityQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center">
        <tr>
            <td>    <asp:Panel ID="Panel1" runat="server" GroupingText="個人報名">
                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="False" 
                    ActiveStepIndex="0" FinishPreviousButtonText="上一步" StartNextButtonText="下一步" 
                    StepNextButtonText="下一步" StepPreviousButtonText="上一步">
                    <StartNavigationTemplate>
                        &nbsp;
                    </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep runat="server" title="Step 1">
                <uc1:RegistActivityQuery ID="RegistActivityQuery1" runat="server" OnGoSecondStep_Click ="GoSecondStep_Click"
                    TypeName="個人報名" />
            </asp:WizardStep>
            <asp:WizardStep runat="server" title="Step 2">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"> </asp:Label>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            姓名 </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            攜伴人數 </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            費用 </td>
                        <td>
                            500 </td>
                    </tr>
                    <tr>
                        <td>
                            備註說明 </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"> </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                            CommandName="MovePrevious" Text="上一步" />
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                            onclick="FinishButton_Click" Text="完成" />
                    </FinishNavigationTemplate>
    </asp:Wizard></asp:Panel></td>
        </tr>
    </table>


</asp:Content>

