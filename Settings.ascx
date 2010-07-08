<%@ Control Language="C#" AutoEventWireup="false" Inherits="Engage.Dnn.TellAFriend.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagName="help" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<div class="taf-settings">
    <div class="taf-siteurl">
        <dnn:help runat="server" ControlName="SiteUrlTextBox" CssClass="Normal" ResourceKey="SiteUrlLabel" />
        <asp:TextBox id="SiteUrlTextBox" runat="server" CssClass="NormalTextBox" Width="50%" />
    </div>
    <div class="taf-message">
        <dnn:help runat="server" ControlName="ShowMessageCheckBox" CssClass="Normal" ResourceKey="ShowMessageLabel" />
        <asp:CheckBox id="ShowMessageCheckBox" runat="server" />
    </div>
    <div class="taf-modal">
        <dnn:help runat="server" ControlName="ShowModalCheckBox" CssClass="Normal" ResourceKey="ShowModalLabel" />
        <asp:CheckBox ID="ShowModalCheckBox" runat="server" />
    </div>
    <div class="taf-from">
        <dnn:help runat="server" ControlName="FromTextBox" CssClass="Normal" ResourceKey="FromLabel" />
        <asp:TextBox ID="FromTextBox" runat="server" CssClass="NormalTextBox" Width="50%" />
        <asp:RegularExpressionValidator ID="FromValidator" runat="server" ControlToValidate="FromTextBox" Display="Dynamic" ResourceKey="FromInvalid" CssClass="NormalRed error-msg-last" />
    </div>
    <div class="taf-cc">
        <dnn:help runat="server" ControlName="CarbonCopyTextBox" CssClass="Normal" ResourceKey="CarbonCopyLabel" />
        <asp:TextBox ID="CarbonCopyTextBox" runat="server" CssClass="NormalTextBox" Width="50%" />
        <asp:RegularExpressionValidator ID="CarbonCopyValidator" runat="server" ControlToValidate="CarbonCopyTextBox" Display="Dynamic" ResourceKey="CarbonCopyInvalid" CssClass="NormalRed error-msg-last" />
    </div>
    <div class="taf-bcc">
        <dnn:help runat="server" ControlName="BlindCarbonCopyTextBox" CssClass="Normal" ResourceKey="BlindCarbonCopyLabel" />
        <asp:TextBox ID="BlindCarbonCopyTextBox" runat="server" CssClass="NormalTextBox" Width="50%" />
        <asp:RegularExpressionValidator ID="BlindCarbonCopyValidator" runat="server" ControlToValidate="BlindCarbonCopyTextBox" Display="Dynamic" ResourceKey="BlindCarbonCopyInvalid" CssClass="NormalRed error-msg-last" />
    </div>
</div>