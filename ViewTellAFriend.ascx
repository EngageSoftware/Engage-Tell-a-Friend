<%@ Control language="C#" Inherits="Engage.Dnn.TellAFriend.ViewTellAFriend" AutoEventWireup="false" Codebehind="ViewTellAFriend.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>
<%@ Import Namespace="Engage.Dnn.TellAFriend"%>
<%@ Import Namespace="DotNetNuke.Entities.Modules"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<div class="tafOuterWrap">
    <div id="ModalAnchorDiv" class="ModalAnchorDiv" runat="server">
        <a href="#" class="tafLink CommandButton"><%= Localization.GetString("MainLinkText", LocalResourceFile) %></a>
    </div>
    <div id="FormWrapDiv" runat="server" class="tafWrap Normal">
        <div class="tafIntroduction"><%= Localization.GetString("Introduction", LocalResourceFile) %></div>
        <div class="tafForm">
	        <div class="tafLast row">
	            <p><%= Localization.GetString("FriendName", LocalResourceFile) %></p>
                <asp:TextBox ID="FriendNameTextBox" runat="server" CssClass="FriendNameTextBox NormalTextBox"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FriendNameTextBox" Display="Dynamic" ResourceKey="FriendNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
            </div>
            <div class="row">
                <p><%= Localization.GetString("RecipientEmailAddress", LocalResourceFile) %></p>
                <asp:TextBox ID="FriendsEmailTextBox" runat="server" CssClass="FriendsEmailTextBox NormalTextBox"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-email NormalRed"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-msg-rt NormalRed" ValidationExpression="^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Mm][Ii][Ll]|[Mm][Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])$"/>
            </div>
            <div class="tafFirst row">
                <p><%= Localization.GetString("SenderName", LocalResourceFile) %></p>
	            <asp:TextBox ID="SenderNameTextBox" runat="server" CssClass="SenderNameTextBox NormalTextBox"/>
	            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SenderNameTextBox" Display="Dynamic" ResourceKey="SenderNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
	        </div>
            <div class="row">
                <p><%= Localization.GetString("EmailAddress", LocalResourceFile) %></p>
	            <asp:TextBox ID="SenderEmailTextBox" runat="server" CssClass="SenderEmailTextBox NormalTextBox"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-msg-last NormalRed" ValidationExpression="^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Mm][Ii][Ll]|[Mm][Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])$"/>
            </div>
            <div class="row" runat="server" id="MessageRow">
                <p><%= Localization.GetString("Message", LocalResourceFile) %></p>
                <asp:TextBox runat="server" TextMode="MultiLine" Rows="6" CssClass="MessageTextBox NormalTextBox" />
            </div>
            <div>
                <a href="#" class="tafSubmitLink CommandButton"><%= Localization.GetString("SubmitButton.Text", LocalResourceFile) %></a>
                <span class="tafAjaxLoader"></span>
            </div>
        </div>
        <div class="tafErrorModuleMessage" style="display:none;">
	        <engage:ModuleMessage runat="server" MessageType="Error" TextResourceKey="EmailError" CssClass="EmailErrorMessage" />
        </div>
        <div class="tafSuccessModuleMessage" style="display:none;">
            <engage:ModuleMessage runat="server" MessageType="Success" TextResourceKey="EmailSuccess" CssClass="EmailSuccessMessage" />
        </div>
    </div>
</div>
<script type="text/javascript">
    jQuery(function() {
        jQuery(".FriendsEmailTextBox").focus(function() {
            jQuery(".SuccessModuleMessage").slideUp("slow");
        });

        jQuery('.tafSubmitLink').click(function(event) {
            event.preventDefault();
            if (typeof (Page_ClientValidate) == 'function') {
                var validationResult = Page_ClientValidate('EngageTellAFriend');
                if (validationResult) {
                    jQuery(".tafAjaxLoader").show();
                    jQuery.ajax({
                        type: "POST",
                        url: CurrentContextInfo.WebMethodUrl,
                        data: '{"localResourceFile":"' + CurrentContextInfo.LocalResourceFile + '","siteUrl":"' + CurrentContextInfo.SiteUrl + '","portalName":"' + CurrentContextInfo.PortalName + '","senderEmail":"' + jQuery('.SenderEmailTextBox').val() + '","friendsEmail":"' + jQuery('.FriendsEmailTextBox').val() + '","senderName":"' + jQuery('.SenderNameTextBox').val() + '","friendName":"' + jQuery('.FriendNameTextBox').val() + '","message":"' + ((jQuery('.MessageTextBox').length > 0) ? jQuery('.MessageTextBox').val() : '') + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(msg) {
                            if (msg === "") {
                                displaySuccess();
                            }
                            else {
                                displayError();
                            }
                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            displayError();
                        }
                    });
                }
            }
        });

        jQuery('.tafOuterWrap').click(function(event) {
            if (jQuery(event.target).is('.tafLink')) {
                event.preventDefault;
                jQuery(this).find('.tafWrap').modal();
            }
        });
    });
    
    function displayError() {
        jQuery(".tafForm").hide();
        jQuery(".tafAjaxLoader").hide();
        jQuery(".tafErrorModuleMessage").show();
        jQuery(".tafSuccessModuleMessage").hide();
    }
    function displaySuccess() {
        jQuery(".tafForm").hide();
        jQuery(".tafAjaxLoader").hide();
        jQuery(".tafSuccessModuleMessage").show();
        jQuery(".tafErrorModuleMessage").hide();
    }   
</script>