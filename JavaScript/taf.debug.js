/// <reference path="json2.debug.js"/>
/// <reference path="jquery-1.4.1.debug-vsdoc.js"/>
/// <reference path="jquery.simplemodal.debug.js"/>

/*global jQuery */

(function ($, window) {
    "use strict";
    $.fn.engageTellAFriend = function (options) {
        var opts = $.extend({}, $.fn.engageTellAFriend.defaults, options),
            $tafWrap = $(this);
    
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
        
        $(function () {
            $tafWrap.find('.taf-submit a').click(function (event) {
                var $tafFormWrap = $(this).closest('.taf-form-wrap'),
                    validationResult,
                    tafData,
                    $tafForm,
                    page_ClientValidate = window.Page_ClientValidate;
                
                event.preventDefault();
                
                if ($.isFunction(page_ClientValidate)) {
                    validationResult = page_ClientValidate(opts.validationGroup);
                    if (validationResult) {
                        $tafFormWrap.find(".taf-progress-icon").show();
                        
                        $tafForm = $tafFormWrap.find('.taf-form');
                        tafData = {
                            localResourceFile: opts.localResourceFile,
                            siteUrl: opts.siteUrl,
                            portalName: opts.portalName,
                            friendName: $tafForm.find('input:eq(0)').val(),
                            friendsEmail: $tafForm.find('input:eq(1)').val(),
                            senderName: $tafForm.find('input:eq(2)').val(),
                            senderEmail: $tafForm.find('input:eq(3)').val(),
                            message: $tafForm.find('textarea').val() || '',
                            portalEmail: opts.portalEmail,
                            currentCulture: opts.currentCulture,
                            tabId: opts.tabId,
                            moduleId: opts.moduleId
                        };

                        $.ajax({
                            type: "POST",
                            url: opts.webMethodUrl,
                            data: JSON.stringify(tafData),
                            dataType: "text",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) {
                                var msg = JSON.parse(data);
                                if (msg.hasOwnProperty('d')) {
                                    return msg.d;
                                }
                                else {
                                    return msg;
                                }
                            },
                            success: function (msg) {
                                taf_displayMessage($tafFormWrap, msg === '');
                            },
                            error: function (/*XMLHttpRequest, textStatus, errorThrown*/) {
                                taf_displayMessage($tafFormWrap, false);
                            }
                        });
                    }
                }
            });
            
            $tafWrap.find('.taf-anchor a').click(function (event) {
                event.preventDefault();
                $(this).closest('.taf-wrap').find('.taf-form-wrap').modal({ persist: true });
            });
        });
    };
    
    $.fn.engageTellAFriend.defaults = {
        siteUrl: location.href,
        localResourceFile: '/DesktopModules/EngageTellAFriend/App_LocalResources/ViewTellAFriend.ascx.resx',
        portalId: 0,
        portalName: '',
        portalEmail: '',
        webMethodUrl: '/DesktopModules/EngageTellAFriend/WebMethods.asmx/SendEmail',
        currentCulture: 'en-US',
        validationGroup: 'EngageTellAFriend'
    };
} (jQuery, this));