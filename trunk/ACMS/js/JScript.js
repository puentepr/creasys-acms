function IsCheckBox(chk) {
    if (chk.type == 'checkbox')
        return true;
    else
        return false;
}

function Check2(parentChk, GridViewId, ChildId) {
    var oElements = document.getElementsByTagName("INPUT");
    var bIsChecked = parentChk.checked;
    for (i = 0; i < oElements.length; i++) {

        if (IsCheckBox(oElements[i]) && IsMatch2(oElements[i].id, GridViewId, ChildId)) {
            if (oElements[i].disabled == false) { 
                oElements[i].checked = bIsChecked;
            }
            
        }
    }
}
function IsMatch2(id, GridViewId, ChildId) {
    var sPattern = '.' + GridViewId + '.*' + ChildId + '$';
    var oRegExp = new RegExp(sPattern);
    if (oRegExp.exec(id))
        return true;
    else
        return false;
}