/*
    //初始化物件
    initObj = {
      txtSerarchID: "控制項 id"
}
*/

//function checkInput() {
//    var txt = document.getElementById(initObj.btnSearchID);

//    if (txt) {
//        if (txt.value.length == 0) {
//            alert("尚未輸入值");
//            return false;
//        }
//    }
//    return true;
//}

$(document).ready(function () {
    $("#" + initObj.btnSearchID).click(function () {
        var val = $("#" + initObj.txtSearchID).val();

        if (val.length == 0) {
            alert('尚未輸入值');
            return false;
        }
        else
            return true;
    })
});