/// <reference path="json2.debug.js"/>
/// <reference path="jquery-1.4.1.debug-vsdoc.js"/>
/// <reference path="jquery.simplemodal.debug.js"/>

(function ($) {
    function taf_displayMessage($tafFormWrap, successful) {
        $tafFormWrap.find(".taf-progress-icon").hide();
        if (successful) {
            $tafFormWrap.find(".taf-success").show();
            $tafFormWrap.find(".taf-error").hide();
        }
        else {
            $tafFormWrap.find(".taf-error").show();
            $tafFormWrap.find(".taf-success").hide(); 
        }
    }
    $(function() {
        $('.taf-submit a').click(function(event) {
            var $tafFormWrap = $(this).closest('.taf-form-wrap'),
                validationResult,
                tafData,
                $tafForm;
            
            event.preventDefault();
            
            if ($.isFunction(Page_ClientValidate)) {
                validationResult = Page_ClientValidate('EngageTellAFriend');
                if (validationResult) {
                    $tafFormWrap.find(".taf-progress-icon").show();
                    
                    $tafForm = $tafFormWrap.find('.taf-form');
                    tafData = {
                        localResourceFile: CurrentContextInfo.LocalResourceFile,
                        siteUrl: CurrentContextInfo.SiteUrl,
                        portalName: CurrentContextInfo.PortalName,
                        friendName: $tafForm.find('input:eq(0)').val(),
                        friendsEmail: $tafForm.find('input:eq(1)').val(),
                        senderName: $tafForm.find('input:eq(2)').val(),
                        senderEmail: $tafForm.find('input:eq(3)').val(),
                        message: $tafForm.find('textarea').val() || '',
                        portalEmail: CurrentContextInfo.PortalEmail,
                        currentCulture: CurrentContextInfo.CurrentCulture
                    };

                    $.ajax({
                        type: "POST",
                        url: CurrentContextInfo.WebMethodUrl,
                        data: JSON.stringify(tafData),
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function(data) {
                            var msg = eval('(' + data + ')');
                            if (msg.hasOwnProperty('d')) {
                                return msg.d;
                            }
                            else {
                                return msg;
                            }
                        },
                        success: function(msg) { taf_displayMessage($tafFormWrap, msg === ''); },
                        error: function(XMLHttpRequest, textStatus, errorThrown) { taf_displayMessage($tafFormWrap, false); }
                    });
                }
            }
        });
        
        $('.taf-anchor a').click(function(event) {
            event.preventDefault();
            $(this).closest('.taf-wrap').find('.taf-form-wrap').modal({ persist: true });
        });
    });
} (jQuery))