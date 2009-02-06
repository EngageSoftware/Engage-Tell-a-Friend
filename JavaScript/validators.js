/*
Contains functions that override default asp.net validator functions for enhanced UI experience.
*/

ValidatorUpdateIsValid = function() { 
	Page_IsValid = AllValidatorsValid(Page_Validators); 
	ClearValidatorCallouts(); 
	SetValidatorCallouts(); 
} 

ValidatorValidate = function(val, validationGroup, event) {
    val.isvalid = true;
    if ((typeof(val.enabled) == 'undefined' || val.enabled != false) && IsValidationGroupMatch(val, validationGroup)) {
        if (typeof(val.evaluationfunction) == 'function') {
            val.isvalid = val.evaluationfunction(val);
            if (!val.isvalid && Page_InvalidControlToBeFocused == null &&
                typeof(val.focusOnError) == 'string' && val.focusOnError == 't') {
                ValidatorSetFocus(val, event);
            }
        }
    }
    
    ClearValidatorCallouts();
    SetValidatorCallouts(); 

    ValidatorUpdateDisplay(val);
}

SetValidatorCallouts = function()
{
    var i;
    var pageValid = true;                    
    for (i = 0; i < Page_Validators.length; i++) {         
        var inputControl = document.getElementById(Page_Validators[i].controltovalidate);               
        if (!Page_Validators[i].isvalid) {                                                        
            if(pageValid)
                inputControl.focus();
            WebForm_AppendToClassName(inputControl, 'error');
            pageValid = false;                                                     
        }                        
    }                    
    return pageValid;
}

ClearValidatorCallouts = function()
{
    var i;                    
    var invalidConrols = [];
    for (i = 0; i < Page_Validators.length; i++) {         
        var inputControl = document.getElementById(Page_Validators[i].controltovalidate);               
        if (inputControl != null) {
            WebForm_RemoveClassName(inputControl, 'error');
        }
    }                                        
}   

ValidatorOnChange = function (event) {
    if (!event) {
        event = window.event;
    }
    Page_InvalidControlToBeFocused = null;
    var targetedControl;
    if ((typeof(event.srcElement) != "undefined") && (event.srcElement != null)) {
        targetedControl = event.srcElement;
    }
    else {
        targetedControl = event.target;
    }
    var vals;    
    
    if (typeof(targetedControl.Validators) != "undefined") {
        vals = targetedControl.Validators;
        // alert('here - 1');
    }
    else {
        if (targetedControl.tagName.toLowerCase() == "label") {
            targetedControl = document.getElementById(targetedControl.htmlFor);
            vals = targetedControl.Validators;	                    
        }
    }
    var i;

	//if date picker caused it in IE...which can't find the textbox to get the list of validators for the control, so revalidate whole page
    if(vals == null)   
		vals = Page_Validators;
    	
	for (i = 0; i < vals.length; i++) {
		ValidatorValidate(vals[i], null, event);
	}	   
	
	ValidatorUpdateIsValid(); 
}  

ValidatorUpdateDisplay = function (val) 
{
	if (typeof(val.display) == "string") 
	{
		if (val.display == "None") 
		{
			return;
		}
		if (val.display == "Dynamic") 
		{
			//changed this block to not set display to inline, but to remove style attribute entirely
			if(val.isvalid)
				jQuery(val).hide("fast");
			else
				jQuery(val).show("fast");
			return;
		}
	}
	if ((navigator.userAgent.indexOf("Mac") > -1) && (navigator.userAgent.indexOf("MSIE") > -1)) 
	{
		val.style.display = "inline";
	}
	val.style.visibility = val.isvalid ? "hidden" : "visible";
}
