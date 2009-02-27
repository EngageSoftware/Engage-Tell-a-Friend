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
</div>