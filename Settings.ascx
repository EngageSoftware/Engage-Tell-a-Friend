<%@ Control Language="C#" AutoEventWireup="false" Inherits="Engage.Dnn.TellAFriend.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagName="help" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js"></script>--%>

<%--<script type="text/javascript">

    // wire up function to check box click event
    $(document).ready(function(){
        $("input[@id=<%=UseSiteUrlCheckBox.ClientID %>]").click(function(){
            if ($(this).is(":checked"))
            {
                // show the text box so they can enter the URL
                jQuery(".SiteUrl").slideDown("slow");
                
                // show and enable the client side validator that requires a value in the textbox
                jQuery("#<%= SiteUrlRequired.ClientID %>").show();
                ValidatorEnable(jQuery("#<%= SiteUrlRequired.ClientID %>")[0], true);
                
                // ValidatorEnable(document.getElementById("dnn_ctr401_ModuleSettings_Settings_SiteUrlRequired"), false);
                // ValidatorValidate(Page_Validators[0],true);
            }
            else
            {
                // remove the value of the textbox
                jQuery("#<%= SiteUrlTextBox.ClientID %>").val("");
                        
                // hide the text box
                jQuery(".SiteUrl").slideUp("slow");
                
                // hide and disable the client side validator that requires a value in the textbox
                jQuery("#<%= SiteUrlRequired.ClientID %>").hide();
                ValidatorEnable(jQuery("#<%= SiteUrlRequired.ClientID %>")[0], true);
                
                // ValidatorEnable(document.getElementById("dnn_ctr401_ModuleSettings_Settings_SiteUrlRequired"), false);
                // ValidatorValidate(Page_Validators[0],false);
            }
        });
    });

</script>--%>

<div class="TellAFriendSettings">

    <div class="UseSiteUrl">
        <dnn:help runat="server" ControlName="UseSiteUrlCheckBox" CssClass="Normal" ResourceKey="UseSiteUrlLabel" />
        <asp:CheckBox runat="server" ID="UseSiteUrlCheckBox" CssClass="NormalCheckBox" />
    </div>
    
    <div class="SiteUrl" runat="server" id="SiteUrlDiv">
        <dnn:help runat="server" ControlName="SiteUrlTextBox" CssClass="Normal" ResourceKey="SiteUrlLabel" />
        <asp:TextBox id="SiteUrlTextBox" runat="server" CssClass="NormalTextBox" Width="50%"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="SiteUrlRequired" ControlToValidate="SiteUrlTextBox" ResourceKey="SiteUrlRequired" EnableClientScript="true"></asp:RequiredFieldValidator>
    </div>
    
</div>