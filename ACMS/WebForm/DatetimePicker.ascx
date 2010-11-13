<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatetimePicker.ascx.cs"
    Inherits="WebForm_DatetimePicker" %>
<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
<asp:DropDownList ID="ddlHour" runat="server">
    <asp:ListItem>0</asp:ListItem>
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
    <asp:ListItem>13</asp:ListItem>
    <asp:ListItem>14</asp:ListItem>
    <asp:ListItem>15</asp:ListItem>
    <asp:ListItem>16</asp:ListItem>
    <asp:ListItem>17</asp:ListItem>
    <asp:ListItem>18</asp:ListItem>
    <asp:ListItem>19</asp:ListItem>
    <asp:ListItem>20</asp:ListItem>
    <asp:ListItem>21</asp:ListItem>
    <asp:ListItem>22</asp:ListItem>
    <asp:ListItem>23</asp:ListItem>
</asp:DropDownList>時
<asp:DropDownList ID="ddlMinute" runat="server">
    <asp:ListItem>0</asp:ListItem>
    <asp:ListItem>30</asp:ListItem> 
</asp:DropDownList>分
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>
<asp:RequiredFieldValidator ID="chk_txtDate" runat="server" ErrorMessage="" 
    ControlToValidate="txtDate" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="chk_txtDate2" runat="server" 
    ControlToValidate="txtDate" Operator="DataTypeCheck" Type="Date" 
    Display="Dynamic"></asp:CompareValidator>
