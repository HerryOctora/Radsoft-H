$(document).ready(function () {   
    $("#UserID").focus();

    var RTCPeerConnection = /*window.RTCPeerConnection ||*/ window.webkitRTCPeerConnection || window.mozRTCPeerConnection;
    var _IpAddress = "";
    

    WinChangePassword = $("#WinChangePassword").kendoWindow({
        height: 200,
        title: "Reset Password",
        visible: false,
        width: 500,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        //close: onPopUpClose
    }).data("kendoWindow");


    if (RTCPeerConnection) (function () {
        var rtc = new RTCPeerConnection({ iceServers: [] });
        if (1 || window.mozRTCPeerConnection) {
            rtc.createDataChannel('', { reliable: false });
        };
        rtc.onicecandidate = function (evt) {
            if (evt.candidate) grepSDP("a=" + evt.candidate.candidate);
        };
        rtc.createOffer(function (offerDesc) {
            grepSDP(offerDesc.sdp);
            rtc.setLocalDescription(offerDesc);
        }, function (e) { console.warn("offer failed", e); });

        var addrs = Object.create(null);
        addrs["0.0.0.0"] = false;
        function updateDisplay(newAddr) {
            if (newAddr in addrs) return;
            else addrs[newAddr] = true;
           // var displayAddrs = Object.keys(addrs).filter(function (k) { return addrs[k]; });
            _IpAddress = newAddr;            
        }

        function grepSDP(sdp) {
            var hosts = [];
            sdp.split('\r\n').forEach(function (line) {
                if (~line.indexOf("a=candidate")) {
                    var parts = line.split(' '),
                        addr = parts[4],
                        type = parts[7];
                    if (type === 'host') updateDisplay(addr);
                } else if (~line.indexOf("c=")) {
                    var parts = line.split(' '),
                        addr = parts[2];
                    updateDisplay(addr);
                }
            });
        }
    })(); else {
        _IpAddress = "<code>ifconfig | grep inet | grep -v inet6 | cut -d\" \" -f2 | tail -n1</code>";
    }
    
    $(document).bind('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            LoginClick();
        }
    });    
    
    $("#btnLogin").click(function () {
        LoginClick();
    });

    function LoginClick_Old12052017() {

        if (_IpAddress == null || _IpAddress == '') {
            _IpAddress = "No Network";
        }

        if ($("#UserID").val() == "" || $("#UserID").val() == null) {
            alertify.alert("UserID must be filled");
            $("#UserID").focus();
        }
        else if ($("#Password").val() == "" || $("#Password").val() == null) {
            alertify.alert("Password must be filled");
        }
        else {
            _IpAddress = "No Network";
            var _id = $("#UserID").val();
            var _userID = _id.toLowerCase();
            var _password = $("#Password").val();
            var _sessionID = "RAD-ID" + chance.guid();

            $.blockUI({});
            $.ajax({
                url: window.location.origin + '/Radsoft/Quest/LoginCheck/' + _userID + '/' + _password + '/' + _sessionID + '/' + _IpAddress,
                type: 'GET',
                cache: 'false',
                contenType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        localStorage.removeItem("SessionID");
                        localStorage.removeItem("User");
                        localStorage.removeItem("expireMonitor");
                        localStorage.setItem("SessionID", _sessionID);
                        localStorage.setItem("expireMonitor", new Date().getTime() + 60 * 60000);
                        localStorage.setItem("User", _userID);
                        sessionStorage.id = _sessionID;
                        sessionStorage.user = _userID;
                        window.location = window.location.origin + "/WEB/Home.html";
                    }
                    $.unblockUI();
                },
                error: function (data) {
                    alertify.success(data.responseText);
                    $.unblockUI();
                }
            });
        }
    }

    function LoginClick() {

        if (_IpAddress == null || _IpAddress == '') {
            _IpAddress = "No Network";
        }

        if ($("#UserID").val() == "" || $("#UserID").val() == null) {
            alertify.alert("UserID must be filled");
            $("#UserID").focus();
        }
        else if ($("#Password").val() == "" || $("#Password").val() == null) {
            alertify.alert("Password must be filled");
        }
        else {
            _IpAddress = "No Network";
            var _userID = $("#UserID").val();
            var _password = $("#Password").val();
            var _sessionID = "RAD-ID" + chance.guid();

            var Quest = {
                UsersID: _userID,
                Password: _password,
                SessionID: _sessionID,
                IpAddress: _IpAddress
            };

            $.blockUI({});
            $.ajax({
                url: window.location.origin + "/Radsoft/Quest/LoginCheck/",
                type: 'POST',
                data: JSON.stringify(Quest),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        localStorage.removeItem("SessionID");
                        localStorage.removeItem("User");
                        localStorage.removeItem("expireMonitor");
                        localStorage.setItem("SessionID", _sessionID);
                        localStorage.setItem("expireMonitor", new Date().getTime() + 60 * 60000);
                        localStorage.setItem("User", _userID);
                        sessionStorage.id = _sessionID;
                        sessionStorage.user = _userID;
                        window.location = window.location.origin + "/WEB/Home.html";
                    }
                    $.unblockUI();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });
        }
    }


    $("#btnResetPassword").click(function () {
        
        alertify.confirm("Are you sure want to Reset Password ?", function (e) {
            if (e) {
                $("#btnResetNow").kendoButton({
                    imageUrl: "../../Images/Icon/IcBtnRefresh.png"
                });

                WinChangePassword.center();
                WinChangePassword.open();
            }
        });
    });


    $("#btnResetNow").click(function () {
        var _id = $("#UserID").val();
        var _userID = _id.toLowerCase();
        var _password = $("#Password").val();
        var _email = $("#Email").val();

        if ($("#Email").val() != "")
        {
            var SendMail1 = {
                        Email: $("#Email").val(),
                        UsersID: _userID
                    };
            $.ajax({
                url: window.location.origin + "/Radsoft/Login/CheckEmail",
                type: 'POST',
                data: JSON.stringify(SendMail1),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == "true") {
                        alertify.confirm("Are you sure want to Reset Password ?", function (e) {
                            if (e) {
                                sendemail(_email, _userID);
                            }
                        });
                    }
                    else
                    {
                        alertify.alert("Email Not Register");
                    }
                }
            });
            
        }
        else
        {
            alertify.alert("Please Fill Email!");
        }
        
    });

    function sendemail(_email, _userID) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Login/CheckID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _email,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == "true") {
                    var SendMail = {
                        Email: _email,
                        UsersID: _userID
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Login/SendMailByInput",
                        type: 'POST',
                        data: JSON.stringify(SendMail),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            $("#Email").val("");
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                            $("#Email").val("");
                        }
                    });
                }
                else {
                    alertify.alert("ID Not Register");
                }
            }
        });

    }

});
