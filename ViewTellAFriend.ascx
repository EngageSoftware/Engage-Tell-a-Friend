<%@ Control language="C#" Inherits="Engage.Dnn.TellAFriend.ViewTellAFriend" AutoEventWireup="false" Codebehind="ViewTellAFriend.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>
<%@ Import Namespace="Engage.Dnn.TellAFriend"%>
<%@ Import Namespace="DotNetNuke.Entities.Modules"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>

<div id="ModalAnchorDiv" class="ModalAnchorDiv" runat="server">
    <a href="#" onclick="ShowForm(); return false;" class="tafLink"><%= Localization.GetString("MainLinkText", LocalResourceFile) %></a>
</div>

<div id="FormWrapDiv" runat="server" class="tafWrap">

    <div class="tafIntroduction">
        <%= Localization.GetString("Introduction", LocalResourceFile) %>
    </div>
    
    <div class="tafForm">
           
	    <div class="tafLast row"><%= Localization.GetString("FriendName", LocalResourceFile) %><br />
            <asp:TextBox ID="FriendNameTextBox" runat="server" Width="80%" CssClass="FriendNameTextBox"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="FriendNameTextBox" Display="Dynamic" ResourceKey="FriendNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-mgs-rt"></asp:RequiredFieldValidator>
        </div>
        
        <div class="row"><%= Localization.GetString("RecipientEmailAddress", LocalResourceFile) %><br />
            <asp:TextBox ID="FriendsEmailTextBox" runat="server" Width="65%" CssClass="FriendsEmailTextBox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailRequired" ValidationGroup="EngageTellAFriend"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-mgs-rt" ValidationExpression="^[+_a-zA-Z0-9-]+(\.[+_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"></asp:RegularExpressionValidator>
        </div>
        
        <div class="tafFirst row"><%= Localization.GetString("SenderName", LocalResourceFile) %><br />
	        <asp:TextBox ID="SenderNameTextBox" runat="server" Width="80%" CssClass="SenderNameTextBox"></asp:TextBox>
	        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SenderNameTextBox" Display="Dynamic" ResourceKey="SenderNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-mgs-rt"></asp:RequiredFieldValidator>
	    </div>
        
        <div class="row"><%= Localization.GetString("EmailAddress", LocalResourceFile) %><br />
	        <asp:TextBox ID="SenderEmailTextBox" runat="server" Width="65%" CssClass="SenderEmailTextBox"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailRequired" ValidationGroup="EngageTellAFriend" CssClass="error-mgs-rt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-mgs-rt" ValidationExpression="^[+_a-zA-Z0-9-]+(\.[+_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"></asp:RegularExpressionValidator>
        </div>

        <div class="row" runat="server" id="MessageRow"><%= Localization.GetString("Message", LocalResourceFile) %> <br />
            <asp:TextBox runat="server" TextMode="MultiLine" Width="65%"  Rows="6" CssClass="MessageTextBox" />
        </div>
        
        <div>
            <a href="#" onclick="SubmitTAF(); return false;" class="tafSubmitLink"><%= Localization.GetString("SubmitButton.Text", LocalResourceFile) %></a>
            <p class="tafAjaxLoader" />
        </div>
        
    </div>
   
    <div class="tafErrorModuleMessage" style="display: none;">
	    <engage:ModuleMessage runat="server" MessageType="Error" TextResourceKey="EmailError" CssClass="EmailErrorMessage" />
    </div>

    <div class="tafSuccessModuleMessage" style="display: none;">
        <engage:ModuleMessage runat="server" MessageType="Success" TextResourceKey="EmailSuccess" CssClass="EmailSuccessMessage" />
    </div>
 
</div>

<script type="text/javascript" src='<%= ResolveUrl("JavaScript/jquery.simplemodal.js") %>'></script>

<script type="text/javascript">
 
    jQuery(function(){
        jQuery(".FriendsEmailTextBox").focus(function (){
            jQuery(".SuccessModuleMessage").slideUp("slow");
        });
    });

    function ShowForm() {
        jQuery('#<%= FormWrapDiv.ClientID %>').modal();
    }

    function SubmitTAF() {
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
                        if (msg == "") {
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
    }
    
    function displayError() {
        jQuery("#<%= FormWrapDiv.ClientID %>").hide();
        jQuery(".tafAjaxLoader").hide();
        jQuery(".tafErrorModuleMessage").show();
        jQuery(".tafSuccessModuleMessage").hide();
    }

    function displaySuccess() {
        jQuery("#<%= FormWrapDiv.ClientID %>").hide();
        jQuery(".tafAjaxLoader").hide();
        jQuery(".tafSuccessModuleMessage").show();
        jQuery(".tafErrorModuleMessage").hide();
    }
        
</script>