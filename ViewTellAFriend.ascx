<%@ Control language="C#" Inherits="Engage.Dnn.TellAFriend.ViewTellAFriend" AutoEventWireup="false" Codebehind="ViewTellAFriend.ascx.cs" %>
<%@ Import Namespace="Engage.Dnn.TellAFriend"%>
<%@ Import Namespace="DotNetNuke.Entities.Modules"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<asp:Panel ID="ModuleWrap" runat="server" class="taf-wrap">
    <asp:Panel ID="ModalAnchorPanel" CssClass="taf-anchor" runat="server">
        <a href="#" class="CommandButton"><%= Localization.GetString("MainLinkText", LocalResourceFile) %></a>
    </asp:Panel>
    <asp:Panel ID="FormWrapPanel" runat="server" CssClass="Normal taf-form-wrap">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="taf-intro"><%= Localization.GetString("Introduction", LocalResourceFile) %></div>
            <div class="taf-form">
	            <div>
	                <p><%= Localization.GetString("FriendName", LocalResourceFile) %></p>
                    <asp:TextBox ID="FriendNameTextBox" runat="server" CssClass="NormalTextBox"/>
                    <asp:RequiredFieldValidator ID="FriendNameRequiredValidator" runat="server" ControlToValidate="FriendNameTextBox" Display="Dynamic" ResourceKey="FriendNameRequired" CssClass="NormalRed error-msg-rt"/>
                </div>
                <div>
                    <p><%= Localization.GetString("RecipientEmailAddress", LocalResourceFile) %></p>
                    <asp:TextBox ID="FriendsEmailTextBox" runat="server" CssClass="NormalTextBox"/>
                    <asp:RequiredFieldValidator ID="FriendEmailRequiredValidator" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailRequired" CssClass="NormalRed error-msg-email" />
                    <asp:RegularExpressionValidator ID="FriendEmailPatternValidator" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailInvalid" CssClass="NormalRed error-msg-rt" />
                </div>
                <div>
                    <p><%= Localization.GetString("SenderName", LocalResourceFile) %></p>
	                <asp:TextBox ID="SenderNameTextBox" runat="server" CssClass="NormalTextBox"/>
	                <asp:RequiredFieldValidator ID="SenderNameRequiredValidator" runat="server" ControlToValidate="SenderNameTextBox" Display="Dynamic" ResourceKey="SenderNameRequired" CssClass="NormalRed error-msg-rt"/>
	            </div>
                <div>
                    <p><%= Localization.GetString("EmailAddress", LocalResourceFile) %></p>
	                <asp:TextBox ID="SenderEmailTextBox" runat="server" CssClass="NormalTextBox"/>
                    <asp:RequiredFieldValidator ID="SenderEmailRequiredValidator" runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailRequired" CssClass="NormalRed error-msg-rt"/>
                    <asp:RegularExpressionValidator ID="SenderEmailPatternValidator" runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailInvalid" CssClass="NormalRed error-msg-last" />
                </div>
                <div runat="server" id="MessageRow">
                    <p><%= Localization.GetString("Message", LocalResourceFile) %></p>
                    <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" Rows="6" CssClass="NormalTextBox" />
                </div>
                <div>
                    <dnn:DnnCaptcha runat="server" ID="InvisibleCaptcha" ProtectionMode="InvisibleTextBox" Display="Dynamic" />
                    <dnn:DnnCaptcha runat="server" ID="TimedCaptcha" ProtectionMode="MinimumTimeout" Display="Dynamic" />
                    <dnn:DnnCaptcha runat="server" ID="StandardCaptcha" Display="Dynamic" EnableRefreshImage="True" />
                </div>
            
                <div class="taf-submit">
                    <asp:Button ID="SubmitButton" runat="server" CssClass="CommandButton" ResourceKey="SubmitButton" />
                </div>
                <span class="taf-progress-icon"></span>
            </div>
            <asp:Panel runat="server" ID="ErrorPanel" CssClass="NormalRed taf-error" Visible="False">
	            <%= Localization.GetString("EmailError.Text", LocalResourceFile)%>
            </asp:Panel>
            <asp:Panel runat="server" ID="SuccessPanel" CssClass="Normal taf-success" Visible="false">
                <%= Localization.GetString("EmailSuccess.Text", LocalResourceFile)%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Panel>

<script type="text/javascript">
    (function ($) {
        $(function () {
            $('#<%=this.ModuleWrap.ClientID %>').engageTellAFriend();
        });
    }(jQuery))
</script>
