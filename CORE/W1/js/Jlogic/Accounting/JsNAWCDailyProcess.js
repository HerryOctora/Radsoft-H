$(document).ready(function () {
    init();

    

    function init()
    {
        $("#ValueDate").kendoDatePicker({
            value: new Date()
        });

      //  $("#msg").html(" asdasd <br/> asdasdas <br/> adsasd");
    }

    $("#BtnGenerate").click(function () {
        
        alertify.confirm("Are you sure want to Generate NAWC?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/NAWCDailyProcess/GenerateNAWCProcess/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#msg").html(data.ErrMsg + data.Msg);
                        alertify.confirm("Generate Done, Please Check Log");
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
});