$(document).ready(function () {
    var win;
    

    var GlobValidator = $("#WinReval").kendoValidator().data("kendoValidator");
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
    function initWindow() {
        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });
        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateTo
        });

        function OnChangeValueDateFrom() {

            var _date = Date.parse($("#ValueDateFrom").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());


        }
        function OnChangeValueDateTo() {
            var _date = Date.parse($("#ValueDateTo").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }

        }


        win = $("#WinReval").kendoWindow({
            height: 200,
            title: "* Portfolio Revaluation",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }

    //$("#BtnGenerate").click(function () {
    //    
    //        alertify.confirm("Are you sure want to Generate Portfolio Revaluation?", function (e) {
    //            if (e) {
    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/Journal/ValidateCheckJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/PortfolioRevaluation",
    //                    type: 'GET',
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {
    //                        if (data == "") {
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/Journal/GeneratePortfolioRevaluation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
    //                                type: 'GET',
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (data) {
    //                                    
    //                                    alertify.alert(data)
    //                                },
    //                                error: function (data) {
    //                                    alertify.alert(data.responseText);
    //                                }
    //                            });
    //                        } else {
    //                            alertify.alert(data);
    //                        }
    //                    },
    //                    error: function (data) {
    //                        alertify.alert(data.responseText);
    //                    }
    //                });
    //            }
    //        });

    //});

    $("#BtnGenerate").click(function () {
        

        alertify.confirm("Are you sure want to Generate Portfolio Revaluation ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/ValidateCheckJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/PortfolioRevaluation",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Journal/GeneratePortfolioRevaluation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    
                                    alertify.alert(data)

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        } else {
                            alertify.alert(data);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
});