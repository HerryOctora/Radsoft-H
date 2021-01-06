$(document).ready(function () {
    //Global Variabel
    var win;

    //1
    initButton();
    //2
    initWindow(null);

    function initButton() {
        $("#BtnSubmit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproved.png"
        });

        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
        });
    }

    
    

    function initWindow(e) {
        win = $("#WinHelp").kendoWindow({
            height: "300px",
            title: "Users Help",
            visible: false,
            width: "550px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 0 })
            }
        }).data("kendoWindow").center().open();
    }

    $("#BtnSubmit").click(function () {
        
        alertify.confirm("Are you sure want to submit your request?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/SubmitRequest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + sessionStorage.getItem("ip"),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        win.close();
                        alertify.alert("Submit request success");
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
            else {
                win.close();
                alertify.alert("Request canceled");
            }
        });
    });

    $("#BtnCancel").click(function () {
        
        win.close();
        alertify.alert("Request canceled.");
    });

});