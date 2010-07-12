<%@ Control Language="C#" AutoEventWireup="false" Inherits="Engage.Dnn.TellAFriend.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagName="help" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>

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
    <div class="taf-subject">
        <dnn:help runat="server" ControlName="SubjectTextBox" CssClass="Normal" ResourceKey="SubjectLabel" />
        <asp:TextBox ID="SubjectTextBox" runat="server" CssClass="NormalTextBox" Width="50%"/>
        <asp:RequiredFieldValidator ID="SubjectRequiredValidator" runat="server" ControlToValidate="SubjectTextBox" Display="Dynamic" ResourceKey="SubjectRequired" CssClass="NormalRed error-msg-rt"/>
    </div>
    <div class="taf-body">
        <dnn:help runat="server" ControlName="BodyTextBox" CssClass="Normal" ResourceKey="BodyLabel" />
        <asp:TextBox ID="BodyTextBox" runat="server" CssClass="NormalTextBox" TextMode="MultiLine" Rows="8" Columns="80"/>
        <asp:RequiredFieldValidator ID="BodyRequiredValidator" runat="server" ControlToValidate="BodyTextBox" Display="Dynamic" ResourceKey="BodyRequired" CssClass="NormalRed error-msg-rt"/>
    </div>
    <div class="taf-restore">
        <asp:Button ID="RestoreButton" runat="server" ResourceKey="RestoreButton" CausesValidation="false"/>
    </div>
    <br />
    <div class="taf-help">
        <a id="TokenLink" href="#" class="CommandButton"><%= Localization.GetString("TokenReferenceLink.Text", LocalResourceFile) %></a>
        <div id="TokenList" class="taf-tokens" style="display: none; border: 1px solid #000;">
            <%= Localization.GetString("TokenReference.Text", LocalResourceFile) %>
        </div>
    </div>
</div>

<script type="text/javascript">
    (function ($) {
        $(function () {
            $('#TokenLink').click(function (event) {
                event.preventDefault();
                $('#TokenList').slideToggle();
            });
        });
    } (jQuery))
</script>