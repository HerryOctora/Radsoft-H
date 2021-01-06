$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinCurrencyReval").kendoValidator().data("kendoValidator");
    var checkedIds = {};

    function validateData() {

        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    initWindow();



    function HideParameter() {
        $("#LblDateFrom").hide();
    }
    function initWindow() {


        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //change: OnChangeValueDateFrom
        });



        //function OnChangeValueDateFrom() {
        //    if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
        //        $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
        //    }
        //}


        win = $("#WinCurrencyReval").kendoWindow({
            height: 500,
            title: "Currency Reval",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");



        win.center();
        win.open();
    }

    if (_GlobClientCode == '21') {
        $("#LblDateTo").show();
    }
    else
    {
        $("#LblDateTo").hide();
    }

    $("#BtnGenerate").click(function () {


            alertify.confirm("Are you sure want to Generate data ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var CurrencyReval = {

                        ValueDate: $('#ValueDate').val(),
                        UsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CurrencyReval/GenerateCurrencyReval/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(CurrencyReval),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
  
         

    });


    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Report");
            }
        });
    });


    $("#CloseGrid").click(function () {

        WinFundClient.close();
    });

    //function ClearAttribute() {
    //    $("#LblValueDate").hide();
    //    $("#LblValueDateFrom").hide();
    //    $("#LblDateTo").hide();
    //}

    $("#BtnVoid").click(function () {
        alertify.confirm("Are you sure want to Void Currency Reval ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/CurrencyReval/VoidCurrencyReval/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });
});