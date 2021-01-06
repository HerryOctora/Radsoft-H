$(document).ready(function () {
    var win;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var _defaultPeriodPK;
    
    // Get Default Period
    $.ajax({
        url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            _defaultPeriodPK = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
            return;
        }
    });

    var GlobValidator = $("#WinFundFactSheetRpt").kendoValidator().data("kendoValidator");
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
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        // ComboBox Value Date
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
        }

        // ComboBox Period
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: true,
                    change: OnChangePeriodPK,
                    value: setCmbPeriodPK()
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePeriodPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPeriodPK() {
            return _defaultPeriodPK;
        }

        // ComboBox Fund
        $.ajax({
            //url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                //$("#FundPK").kendoMultiSelect({
                //    dataValueField: "FundPK",
                //    dataTextField: "ID",
                //    filter: "contains",
                //    suggest: true,
                //    dataSource: data
                //});
                //$("#FundPK").data("kendoMultiSelect").value("0");

                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
                return;
            }
        });
        function onChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFundPK() {
            return "";
        }

        win = $("#WinFundFactSheetRpt").kendoWindow({
            height: 350,
            title: "Fund Fact Sheet Report",
            visible: false,
            width: 700,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }

    $("#BtnDownload").click(function () {
        var val = validateData();
        if (val == 1) {
            //var ArrayFund = $("#FundPK").data("kendoMultiSelect").value();
            //var strFund = '';
            //for (var i in ArrayFund) {
            //    strFund = strFund + ArrayFund[i] + ',';
            //}
            //strFund = strFund.substring(0, strFund.length - 1);

            alertify.confirm("Are you sure want to download?", function (e) {
                if (e) {
                    var FundFactSheetRpt = {
                        ValueDate: $('#ValueDate').val(),
                        PeriodPK: $('#PeriodPK').val(),
                        FundPK: $('#FundPK').val(),
                        //FundPK: strFund,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/FundFactSheetReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFactSheetRpt_Download/",
                        type: 'POST',
                        data: JSON.stringify(FundFactSheetRpt),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            return;
                        }
                    });
                }
            });
        }
    });

    $("#BtnCancel").click(function () {        
        alertify.confirm("Are you sure want to close?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Closed");
            }
        });
    });

});