/// <reference path="jquery-1.6.1.debug-vsdoc.js"/>
/// <reference path="jquery.simplemodal.debug.js"/>

/*global jQuery */

(function ($) {
    "use strict";
    $.fn.engageTellAFriend = function () {
        var $tafWrap = this;
    
        $(function () {
            $tafWrap.find('.taf-anchor a').click(function (event) {
                event.preventDefault();
                $(this).closest('.taf-wrap').find('.taf-form-wrap').modal({ persist: true, appendTo: '#Form' });
            });
        });
    };
} (jQuery));
