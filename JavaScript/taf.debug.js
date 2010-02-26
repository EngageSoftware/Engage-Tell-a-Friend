/// <reference path="json2.debug.js"/>
/// <reference path="jquery-1.4.1.debug-vsdoc.js"/>
/// <reference path="jquery.simplemodal.debug.js"/>
function taf_displayMessage(successful) {
    jQuery(".taf-progress-icon").hide();
    if (successful) {
        jQuery(".taf-success").show();
        jQuery(".taf-error").hide();
    }
    else {
        jQuery(".taf-error").show();
        jQuery(".taf-success").hide(); 
    }
}
jQuery(function() {
    jQuery('.taf-submit a').click(function(event) {
        event.preventDefault();
        if (typeof (Page_ClientValidate) === 'function') {
            var validationResult = Page_ClientValidate('EngageTellAFriend');
            if (validationResult) {
                var tafData = {
                    localResourceFile: CurrentContextInfo.LocalResourceFile,
                    siteUrl: CurrentContextInfo.SiteUrl,
                    portalName: CurrentContextInfo.PortalName,
                    senderEmail: jQuery('.taf-form input:eq(3)').val(),
                    friendsEmail: jQuery('.taf-form input:eq(1)').val(),
                    senderName: jQuery('.taf-form input:eq(2)').val(),
                    friendName: jQuery('.taf-form input:eq(0)').val(),
                    message: jQuery('.taf-form textarea').val() || '',
                    portalEmail: CurrentContextInfo.PortalEmail,
                    currentCulture: CurrentContextInfo.CurrentCulture
                };

                jQuery(".taf-progress-icon").show();
                jQuery.ajax({
                    type: "POST",
                    url: CurrentContextInfo.WebMethodUrl,
                    data: JSON.stringify(tafData),
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) {
                        var msg = eval('(' + data + ')');
                        if (msg.hasOwnProperty('d'))
                            return msg.d;
                        else
                            return msg;
                    },
                    success: function(msg) { taf_displayMessage(msg === ''); },
                    error: function(XMLHttpRequest, textStatus, errorThrown) { taf_displayMessage(false); }
                });
            }
        }
    });
    jQuery('.taf-anchor').click(function(event) {
        //todo: add a real click event and traverse up to the form wrap more generically.
        if (jQuery(event.target).is('.taf-anchor a')) {
            event.preventDefault();
            jQuery(this).siblings('.taf-form-wrap').modal({ persist: true });
        }
    });
});