$(document).ready(function () {
    IdleTimeMinutes = 0;
    GetIdleTimeMinutes();

    
      
    function GetIdleTimeMinutes() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetIdleTimeMinutes/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                IdleTimeMinutes = data;
                CheckIdle();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function CheckIdle() {
        if (IdleTimeMinutes > 0) {
            TimeVal = 0;
            var TimeInterval = setInterval(CallTimerInc, 60000);
            $(document).bind('mousemove click mouseup mousedown keydown keypress keyup submit change mouseenter scroll resize dblclick', function (event) {
                TimeVal = 0;
            });
        }
    }



    function CallTimerInc() {
        TimeVal++;
        if (TimeVal >= IdleTimeMinutes) {
            
            TimeVal = 0;
            sessionStorage.clear();
            window.parent.location = window.location.origin + "/WEB/Login.html";
        }
    }
});