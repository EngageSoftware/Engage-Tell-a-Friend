<%@ Control Language="C#" AutoEventWireup="false" Inherits="Engage.Dnn.TellAFriend.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagName="help" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>
<div class="TellAFriendSettings">
    <div class="SiteUrl" runat="server" id="SiteUrlDiv">
        <dnn:help runat="server" ControlName="SiteUrlTextBox" CssClass="Normal" ResourceKey="SiteUrlLabel" />
        <asp:TextBox id="SiteUrlTextBox" runat="server" CssClass="NormalTextBox" Width="50%"></asp:TextBox>
    </div>
</div>