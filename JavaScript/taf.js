jQuery(function() {
    jQuery('.taf-submit a').click(function(event) {
        event.preventDefault();
        if (typeof (Page_ClientValidate) == 'function') {
            var validationResult = Page_ClientValidate('EngageTellAFriend');
            if (validationResult) {
                jQuery(".taf-progress-icon").show();
                jQuery.ajax({
                    type: "POST",
                    url: CurrentContextInfo.WebMethodUrl,
                    data: '{"localResourceFile":"' + CurrentContextInfo.LocalResourceFile + '","siteUrl":"' + CurrentContextInfo.SiteUrl + '","portalName":"' + CurrentContextInfo.PortalName + '","senderEmail":"' + jQuery('.taf-form input:eq(3)').val() + '","friendsEmail":"' + jQuery('.taf-form input:eq(1)').val() + '","senderName":"' + jQuery('.taf-form input:eq(2)').val() + '","friendName":"' + jQuery('.taf-form input:eq(0)').val() + '","message":"' + ((jQuery('.taf-form input:eq(4)').length > 0) ? jQuery('.taf-form input:eq(4)').val() : '') + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) { jQuery(".taf-progress-icon").hide(); if (msg === "") { jQuery(".taf-success").show(); } else { jQuery(".taf-error").show(); } },
                    error: function(XMLHttpRequest, textStatus, errorThrown) { displayError(); }
                });
            }
        }
    });
    jQuery('.taf-anchor').click(function(event) {
        if (jQuery(event.target).is('.taf-anchor a')) {
            jQuery('.taf-form-wrap').modal();
            return false;
        }
    });
});