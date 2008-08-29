<%@ Control language="C#" Inherits="Engage.Dnn.TellAFriend.ViewTellAFriend" AutoEventWireup="false" Codebehind="ViewTellAFriend.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>
<%@ Import Namespace="Engage.Dnn.TellAFriend"%>
<%@ Import Namespace="DotNetNuke.Entities.Modules"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>

<%= Localization.GetString("Introduction", LocalResourceFile) %>

<div id="dnnGalleryTellAFriend" class="content">
    
    <div class="tafName">

	    <p class="tafFirst row"><%= Localization.GetString("FirstName", LocalResourceFile) %><br />
	    <asp:TextBox ID="FirstNameTextBox" runat="server" Width="80%" CssClass="FirstNameTextBox"></asp:TextBox>
	    <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstNameTextBox" Display="Dynamic" ResourceKey="RequiredFieldValidator" ValidationGroup="EngageTellAFriend"></asp:RequiredFieldValidator>
	    </p>
    	
	    <p class="tafLast row"><%= Localization.GetString("LastName", LocalResourceFile) %><br />
        <asp:TextBox ID="LastNameTextBox" runat="server" Width="80%" CssClass="LastNameTextBox"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="LastNameTextBox" Display="Dynamic" ResourceKey="RequiredFieldValidator" ValidationGroup="EngageTellAFriend"></asp:RequiredFieldValidator>
        </p>
        
    </div>

    <p class="row"><%= Localization.GetString("EmailAddress", LocalResourceFile) %><br />
	    <asp:TextBox ID="SenderEmailTextBox" runat="server" Width="65%" CssClass="SenderEmailTextBox"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="RequiredFieldValidator" ValidationGroup="EngageTellAFriend"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="InvalidEmailAddress" ValidationGroup="EngageTellAFriend" ValidationExpression="^[+_a-zA-Z0-9-]+(\.[+_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"></asp:RegularExpressionValidator>
    </p>

    <p class="row"><%= Localization.GetString("RecipientEmailAddress", LocalResourceFile) %><br />
        <asp:TextBox ID="FriendsEmailTextBox" runat="server" Width="65%" CssClass="FriendsEmailTextBox"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="RequiredFieldValidator" ValidationGroup="EngageTellAFriend"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="InvalidEmailAddress" ValidationGroup="EngageTellAFriend" ValidationExpression="^[+_a-zA-Z0-9-]+(\.[+_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"></asp:RegularExpressionValidator>
    </p>

    <p class="row"><%= Localization.GetString("Message", LocalResourceFile) %> <br />
        <asp:TextBox runat="server" TextMode="MultiLine" Width="65%"  Rows="6" CssClass="MessageTextBox" />
    </p>

    <div class="ValidationErrorModuleMessage" style="display: none;">
	    <engage:ModuleMessage runat="server" MessageType="Error" TextResourceKey="ValidationError" CssClass="EmailErrorMessage" />
    </div>
    
    <div class="ErrorModuleMessage" style="display: none;">
	    <engage:ModuleMessage runat="server" MessageType="Error" TextResourceKey="EmailError" CssClass="EmailErrorMessage" />
    </div>

    <div class="SuccessModuleMessage" style="display: none;">
        <engage:ModuleMessage runat="server" MessageType="Success" TextResourceKey="EmailSuccess" CssClass="EmailSuccessMessage" />
    </div>

    <div>
        <input type="button" onclick="ExpressValidate();" causesvalidation="false" class="SubmitButton" value="<%= Localization.GetString("SubmitButton.Text", LocalResourceFile) %>" />
        <p class="AjaxLoader" />
    </div>
    
</div>
    
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js"></script>
<script type="text/javascript" src='<%= ResolveUrl("JavaScript/jquery.watermarkinput.js") %>'></script>

<script type="text/javascript">
   
    jQuery(function(){
        jQuery(".FriendsEmailTextBox").focus(function (){
            jQuery(".SuccessModuleMessage").slideUp("slow");
        });
    });

    function ExpressValidate()
    {
        if (typeof(Page_ClientValidate) == 'function'){
            var validationResult = Page_ClientValidate('EngageTellAFriend'); 
            if (validationResult){
                jQuery(".AjaxLoader").show();  
                jQuery.ajax ({
                    type: "POST",
                    url: CurrentContextInfo.WebMethodUrl,
                    data: '{"localResourceFile":"' + CurrentContextInfo.LocalResourceFile + '","useSiteUrl":"' + CurrentContextInfo.UseSiteUrl + '","siteUrl":"' + CurrentContextInfo.SiteUrl + '","tabId":"' + CurrentContextInfo.TabId + '","portalName":"' + CurrentContextInfo.PortalName + '","senderEmail":"'+ jQuery('.SenderEmailTextBox').val() + '","friendsEmail":"'+ jQuery('.FriendsEmailTextBox').val() + '","firstName":"'+ jQuery('.FirstNameTextBox').val() + '","lastName":"'+ jQuery('.LastNameTextBox').val() + '","message":"' + jQuery('.MessageTextBox').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg){
                        if(msg == "")
                        {
                            displaySuccess();
                        }
                        else
                        {
                            displayError();
                        }
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown){
                        displayError();
                    }
                    });
            }
            else {
                displayValidationError();
            }
        }
    }
    
    function displayValidationError(){
        jQuery(".ValidationErrorModuleMessage").slideDown("slow");
        jQuery(".SuccessModuleMessage").slideUp("slow");
        jQuery(".ErrorModuleMessage").slideUp("slow");
    }
    
    function displayError(){
        jQuery(".AjaxLoader").hide();
        jQuery(".ErrorModuleMessage").slideDown("slow");
        jQuery(".SuccessModuleMessage").slideUp("slow");
        jQuery(".ValidationErrorModuleMessage").slideUp("slow");
    }
    
    function displaySuccess(){
        jQuery(".AjaxLoader").hide();
        jQuery(".FriendsEmailTextBox").val("").Watermark('<%= Localization.GetString("SendAnother", LocalResourceFile) %>');
        jQuery(".SuccessModuleMessage").slideDown("slow");
        jQuery(".ValidationErrorModuleMessage").slideUp("slow"); 
        jQuery(".ErrorModuleMessage").slideUp("slow"); 
    }
    
    jQuery(document).ready(function(){
        jQuery.noConflict();
        jQuery('.content input, .content textarea, .content select').focus(function(){
            jQuery(this).parents('.row').addClass("over");
        }).blur(function(){
            jQuery(this).parents('.row').removeClass("over");
        });
    });
</script>