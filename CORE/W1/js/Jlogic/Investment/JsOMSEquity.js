$(document).ready(function () {
    var chat = $.connection.chatHub;
 
    function SendNotification()
    {
        $.connection.hub.start().done(function () {
            chat.server.send('', '');
        });
    }
   
        var WinListInstrument;
        var WinListAvailableCashDetail;
        var htmlInstrumentPK;
        var htmlInstrumentID;
        var htmlInstrumentName;
        var checkedInvestmentBuy = {};
        var checkedInvestmentSell = {};

        var UpGlobValidator = $("#WinUpdateInvestment").kendoValidator().data("kendoValidator");
        var GlobValidator = $("#WinInvestmentInstruction").kendoValidator().data("kendoValidator");
        var _d = new Date();
        var _fy = _d.getFullYear();
        var _defaultPeriodPK;
        var _globMode;
    var _instrumentTypePK;

    if (_GlobClientCode == "01" || _GlobClientCode == "20") {
        $("#lblSignature").show();
        $("#lblPosition").show();
    }
    else {
        $("#lblSignature").hide();
        $("#lblPosition").hide();
    }

    if (_GlobClientCode == "20") {
        $("#InvestmentStyle").attr("required", true);
        $("#OtherInvestmentStyle").attr("required", true);
        $("#InvestmentObjective").attr("required", true);
        $("#OtherInvestmentObjective").attr("required", true);
        $("#Revision").attr("required", true);
        $("#OtherRevision").attr("required", true);
        $("#InvestmentStrategy").attr("required", true);
        $("#BIRate").attr("required", true);

        $("#UpInvestmentStyle").attr("required", true);
        $("#UpOtherInvestmentStyle").attr("required", true);
        $("#UpInvestmentObjective").attr("required", true);
        $("#UpOtherInvestmentObjective").attr("required", true);
        $("#UpRevision").attr("required", true);
        $("#UpOtherRevision").attr("required", true);
        $("#UpInvestmentStrategy").attr("required", true);
        $("#UpBIRate").attr("required", true);
    }
    else {
        $("#InvestmentStyle").attr("required", false);
        $("#OtherInvestmentStyle").attr("required", false);
        $("#InvestmentObjective").attr("required", false);
        $("#OtherInvestmentObjective").attr("required", false);
        $("#Revision").attr("required", false);
        $("#OtherRevision").attr("required", false);
        $("#InvestmentStrategy").attr("required", false);
        $("#BIRate").attr("required", false);

        $("#UpInvestmentStyle").attr("required", false);
        $("#UpOtherInvestmentStyle").attr("required", false);
        $("#UpInvestmentObjective").attr("required", false);
        $("#UpOtherInvestmentObjective").attr("required", false);
        $("#UpRevision").attr("required", false);
        $("#UpOtherRevision").attr("required", false);
        $("#UpInvestmentStrategy").attr("required", false);
        $("#UpBIRate").attr("required", false);
    }


        WinValidateFundExposure = $("#WinValidateFundExposure").kendoWindow({
            height: 500,
            title: "* Fund Exposure",
            visible: false,
            width: 1400,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinValidateFundExposureClose
        }).data("kendoWindow");

        WinUpValidateFundExposure = $("#WinUpValidateFundExposure").kendoWindow({
            height: 500,
            title: "* Fund Exposure",
            visible: false,
            width: 1400,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinUpValidateFundExposureClose
        }).data("kendoWindow");

    WinFundExposureFromImport = $("#WinFundExposureFromImport").kendoWindow({
        height: 500,
        title: "* Fund Exposure",
        visible: false,
        width: 1400,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
        close: onWinFundExposureFromEmailClose
    }).data("kendoWindow");


        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: onChangeDateFrom,
            value: new Date()
        });

        function onChangeDateFrom() {
            refresh();
        }


    if (_GlobClientCode == '20') {
        $("#ParamDays").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "0", value: 0 },
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                { text: "3", value: 3 },
                { text: "4", value: 4 },
                { text: "5", value: 5 },
                { text: "6", value: 6 },
                { text: "7", value: 7 }
            ],
            filter: "contains",
            suggest: true,
            index: 7,
            change: OnChangeParamDays,
        });
    }
    else {
        $("#ParamDays").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "0", value: 0 },
                { text: "1", value: 1 },
                { text: "2", value: 2 },
                { text: "3", value: 3 },
                { text: "4", value: 4 },
                { text: "5", value: 5 },
                { text: "6", value: 6 },
                { text: "7", value: 7 }
            ],
            filter: "contains",
            suggest: true,
            index: 0,
            change: OnChangeParamDays,
        });
    }

        function OnChangeParamDays() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                refresh();
            }
        }

    //Signature 1
    $.ajax({
        url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#Signature1").kendoComboBox({
                dataValueField: "SignaturePK",
                dataTextField: "Name",
                dataSource: data,
                change: OnChangeSignature1,
                filter: "contains",
                suggest: true
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function OnChangeSignature1() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        if ($("#Signature1").val() == 0 || $("#Signature1").val() == "") {
            $("#Position1").val("");
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position1").val(data.Position);
                }
            });
        }

    }


    //Signature 2
    $.ajax({
        url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#Signature2").kendoComboBox({
                dataValueField: "SignaturePK",
                dataTextField: "Name",
                dataSource: data,
                change: OnChangeSignature2,
                filter: "contains",
                suggest: true
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function OnChangeSignature2() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        if ($("#Signature2").val() == 0 || $("#Signature2").val() == "") {
            $("#Position2").val("");
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position2").val(data.Position);
                }
            });
        }
    }

    //Signature 3
    $.ajax({
        url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#Signature3").kendoComboBox({
                dataValueField: "SignaturePK",
                dataTextField: "Name",
                dataSource: data,
                change: OnChangeSignature3,
                filter: "contains",
                suggest: true
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function OnChangeSignature3() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if ($("#Signature3").val() == 0 || $("#Signature3").val() == "") {
            $("#Position3").val("");
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position3").val(data.Position);
                }
            });
        }
    }

    //Signature 4
    $.ajax({
        url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#Signature4").kendoComboBox({
                dataValueField: "SignaturePK",
                dataTextField: "Name",
                dataSource: data,
                change: OnChangeSignature4,
                filter: "contains",
                suggest: true
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function OnChangeSignature4() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if ($("#Signature4").val() == 0 || $("#Signature4").val() == "") {
            $("#Position4").val("");
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position4").val(data.Position);
                }
            });
        }
    }

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                refresh();
                
            }
        }

        if (_GlobClientCode == "09") {
            $("#LblNavProjection").show();
        }
        else {
            $("#LblNavProjection").hide();
        }

        if (_GlobClientCode == "17") {
            $("#LblPendingCash").show();
            RefreshPendingCash();
        }
        else {
            $("#LblPendingCash").hide();
        }

        if (_GlobClientCode == "05") {
            $("#LblBitHTM").hide();
            $("#LblUpBitHTM").hide();
        }
        else {
            $("#LblBitHTM").show();
            $("#LblUpBitHTM").show();
        }
            
        RefreshAUMYesterday();
        RefreshAvailableCash();
        RefreshNetBuySell();


    function validateData() {

        //if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
        //    alertify.alert("Amount <> 0").moveTo(300, 500);
        //    return 0;
        //}
        //if ($("#FundPK").data("kendoComboBox").value() == "" || $("#FundPK").data("kendoComboBox").value() == 0) {
        //    alertify.alert("Fund must be filled").moveTo(300, 500);
        //    return 0;
        //}
        //if ($("#TrxType").data("kendoComboBox").value() == "" || $("#TrxType").data("kendoComboBox").value() == 0) {
        //    alertify.alert("TrxType must be filled").moveTo(300, 500);
        //    return 0;
        //}
        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass").moveTo(300, 500);
            return 0;
        }
    }

    function UpvalidateData() {

        //if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
        //    alertify.alert("Amount <> 0").moveTo(300, 500);
        //    return 0;
        //}
        //if ($("#FundPK").data("kendoComboBox").value() == "" || $("#FundPK").data("kendoComboBox").value() == 0) {
        //    alertify.alert("Fund must be filled").moveTo(300, 500);
        //    return 0;
        //}
        //if ($("#TrxType").data("kendoComboBox").value() == "" || $("#TrxType").data("kendoComboBox").value() == 0) {
        //    alertify.alert("TrxType must be filled").moveTo(300, 500);
        //    return 0;
        //}
        if (UpGlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass").moveTo(300, 500);
            return 0;
        }
    }

    function RefreshPendingCash() {
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetPendingCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == null || data == 0) {
                        $('#NetPendingCash').val(0);
                        $('#NetPendingCash').text(kendo.toString(data, "n"));
                    }
                    else {
                        $('#NetPendingCash').val(data);
                        $('#NetPendingCash').text(kendo.toString(data, "n"));
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    function RefreshNetBuySell() {
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetNetBuySell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $('#NetBuySell').val(data);
                    $('#NetBuySell').text(kendo.toString(data, "n"));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    function RefreshAvailableCash() {
            $('#NetCashAvailable').text("LOADING . . . . .");
            //$.blockUI({});
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            if ($("#ParamDays").val() == "") {
                _paramDays = "0";
            }
            else {
                _paramDays = $("#ParamDays").val();
            }
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramDays,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == null || data == 0) {
                        //$.unblockUI();
                   
                        $('#NetCashAvailable').val(0);
                        $('#NetCashAvailable').text(kendo.toString(data, "n"));
                    }
                    else {
                        //$.unblockUI();
                        $('#NetCashAvailable').val(data);
                        $('#NetCashAvailable').text(kendo.toString(data, "n"));
                    }

                },
                error: function (data) {
                    //$.unblockUI();
                    alertify.alert(data.responseText);
                }
            });
        }

    function RefreshAUMYesterday() {
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetAUMYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $('#AumYesterday').val(data);
                    $('#AumYesterday').text(kendo.toString(data, "n"));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                _defaultPeriodPK = data;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: 500,
            title: "Instrument List",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

    function onWinListInstrumentClose() {
            $("#gridListInstrument").empty();
        }


    WinOMSEquityListing = $("#WinOMSEquityListing").kendoWindow({
            height: 300,
            title: "* Listing OMS Equity",
            visible: false,
            width: 600,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinOMSEquityListingClose
        }).data("kendoWindow");


    $("#BtnImportOmsEquity").kendoButton({
        imageUrl: "../../Images/Icon/IcImport.png"
    });

    $("#BtnOMSEquityListing").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnPosting.png"
    });

    WinUpListInstrument = $("#WinUpListInstrument").kendoWindow({
        height: 500,
        title: "Instrument List",
        visible: false,
        width: 1200,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        close: onWinUpListInstrumentClose
    }).data("kendoWindow");

    function onWinUpListInstrumentClose() {
        $("#gridUpListInstrument").empty();
    }

    win = $("#WinInvestmentInstruction").kendoWindow({
        height: 700,
        title: "Order Slip Detail",
        visible: false,
        width: 900,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 120, left: 300 })
        },
        close: onPopUpClose
    }).data("kendoWindow");

    WinUpdateInvestment = $("#WinUpdateInvestment").kendoWindow({
        height: 700,
        title: "Order Slip Ammend",
        visible: false,
        width: 900,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 120, left: 300 })
        },
        close: onCloseUpdateInvestment
    }).data("kendoWindow");

    function onPopUpClose() {
        $("#InvestmentNotes").val('');
        if (_GlobClientCode == "09") {
            $("#CmbInstrumentPK").val('');

        }
        else {
            $("#InstrumentPK").val('');
            $("#InstrumentID").val('');
        }
        $("#FindInstrument").val('');
        $("#MarketPK").val('');
        $("#RangePrice").val('');
        $("#OrderPrice").data("kendoNumericTextBox").value(null);
        $("#Amount").data("kendoNumericTextBox").value(null);
        $("#Lot").data("kendoNumericTextBox").value(null);
        $("#Volume").data("kendoNumericTextBox").value(null);
        $("#TrxType").data("kendoComboBox").value(null);
        $("#BitForeignTrx").prop('checked', false);
        $("#BitHTM").prop('checked', false);
        $("#InvestmentTrType").val('');
        $("#BIRate").val('');
        $("#InvestmentStrategy").val('');
        $("#InvestmentStyle").val('');
        $("#InvestmentObjective").val('');
        $("#Revision").val('');
        $("#OtherInvestmentStyle").val('');
        $("#OtherInvestmentObjective").val('');
        $("#OtherRevision").val('');
        $("#FundPK").val('');


        $("#LblPrice").show();
        $("#LblLot").show();
        $("#LblLotInShare").show();
        $("#LblVolume").show();
        $("#MethodType").data("kendoComboBox").value(1);
        $("#Lot").attr("required", true);
        $("#OrderPrice").attr("required", true);
        //onChangeMethodType();
        GlobValidator.hideMessages();
    }

    function onCloseUpdateInvestment() {
        $("#UpInvestmentNotes").val('');
        if (_GlobClientCode == "09") {
            $("#CmbUpInstrumentPK").val('');

        }
        else {
            $("#UpInstrumentPK").val('');
        }
        $("#UpFundPK").val('');
        $("#UpMarketPK").val('');
        $("#UpRangePrice").val('');
        $("#UpOrderPrice").data("kendoNumericTextBox").value(null);
        $("#UpAmount").data("kendoNumericTextBox").value(null);
        $("#UpLot").data("kendoNumericTextBox").value(null);
        $("#UpVolume").data("kendoNumericTextBox").value(null);
        $("#UpBitForeignTrx").prop('checked', false);
        $("#UpBitHTM").prop('checked', false);
        $("#UpBIRate").val('');
        $("#UpInvestmentStrategy").val('');
        $("#UpInvestmentStyle").val('');
        $("#UpInvestmentObjective").val('');
        $("#UpRevision").val('');
        $("#UpOtherInvestmentStyle").val('');
        $("#UpOtherInvestmentObjective").val('');
        $("#UpOtherRevision").val('');
        $("#UpFundPK").val('');
        GlobValidator.hideMessages();
    }

    $("#BtnOMSEquityListing").click(function () {
            showOMSEquityListing();
        });

    // Untuk Form Listing

    function showOMSEquityListing(e) {

            $("#DownloadMode").kendoComboBox({
                dataValueField: "text",
                dataTextField: "text",
                dataSource: [
                   { text: "Excel" },
                   { text: "PDF" },
                ],
                filter: "contains",
                suggest: true,
                change: OnChangeDownloadMode,
                index: 0
            });
            function OnChangeDownloadMode() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }

            WinOMSEquityListing.center();
            WinOMSEquityListing.open();

        }

    $("#BtnOkOMSEquityListing").click(function () {

        var All = 0;
        All = [];
        for (var i in checkedInvestmentSell) {
            if (checkedInvestmentSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedInvestmentBuy) {
            if (checkedInvestmentBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
                if (e) {
                    var OMSEquityListing = {
                        Message: $('#Message').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamListDate: $('#DateFrom').val(),
                        Signature1: $("#Signature1").data("kendoComboBox").value(),
                        Signature2: $("#Signature2").data("kendoComboBox").value(),
                        Signature3: $("#Signature3").data("kendoComboBox").value(),
                        Signature4: $("#Signature4").data("kendoComboBox").value(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSEquity/OMSEquityListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(OMSEquityListing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.error(data.responseText);
                        }

                    });
                }
            });
        }

    });

    $("#BtnCancelOMSEquityListing").click(function () {

            alertify.confirm("Are you sure want to cancel listing?", function (e) {
                if (e) {
                    WinOMSEquityListing.close();
                    alertify.success("Cancel Listing");
                }
            });
        });

    function onWinOMSEquityListingClose() {
            $("#Message").val("")
        }

    WinListAvailableCashDetail = $("#WinListAvailableCashDetail").kendoWindow({
            height: 500,
            title: "AvailableCashDetail",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListAvailableCashDetailClose
        }).data("kendoWindow");

    function onWinListAvailableCashDetailClose() {
        $("#gridListAvailableCashDetail").empty();
    }

    $("#SettledDate").kendoDatePicker({
        format: "MM/dd/yyyy"
    });

    initButton();
    ResetSelected();

    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnRenewMarket").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnSaveBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnSaveSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCancelUpdateInvestment").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdateInvestmentBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnUpdateInvestmentSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnNavProjection").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });


    }

    WinNavProjection = $("#WinNavProjection").kendoWindow({
        height: 550,
        title: "* Nav Projection",
        visible: false,
        width: 1100,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
    }).data("kendoWindow");

    $("#FilterInstrumentType").kendoComboBox({
        dataValueField: "value",
        dataTextField: "text",
        filter: "contains",
        suggest: true,
        dataSource: [
                { text: "EQUITY", value: "1" },
                { text: "BOND", value: "2" },
                { text: "DEPOSITO", value: "3" },
        ],
        change: onChangeFilterInstrumentType,
    });

    function onChangeFilterInstrumentType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    var splitter = $("#splitter").kendoSplitter({
        orientation: "vertical",
        panes: [
            { collapsible: true, size: "1350px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "430px" }
        ],
    }).data("kendoSplitter");

    $("#BtnToInvestment").click(function () {
        splitter.toggle("#PaneInvestment");
    });

    $("#BtnToBySector").click(function () {
        splitter.toggle("#PaneBySector");
    });

    $("#BtnToByIndex").click(function () {
        splitter.toggle("#PaneByIndex");
    });

    $("#BtnToByInstrument").click(function () {
        splitter.toggle("#PaneByInstrument");
    });

    $("#BtnToExposure").click(function () {
        splitter.toggle("#PaneExposure");
    });

    $("#BtnToFundPosition").click(function () {
        splitter.toggle("#PaneFundPosition");
    });

    $("#BtnToShow").click(function () {
        splitter.expand("#PaneInvestment");
        splitter.expand("#PaneBySector");
        splitter.expand("#PaneByIndex");
        splitter.expand("#PaneByInstrument");
        splitter.expand("#PaneExposure");
        splitter.expand("#PaneFundPosition");
    });

    $("#BtnToHide").click(function () {
        splitter.collapse("#PaneInvestment");
        splitter.collapse("#PaneBySector");
        splitter.collapse("#PaneByIndex");
        splitter.collapse("#PaneByInstrument");
        splitter.collapse("#PaneExposure");
        splitter.collapse("#PaneFundPosition");
    });

    function GetPriceFromYahooFinance() {
            

        $.blockUI({});
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_ListStockForYahooFinance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _query = "";
                _query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22" + data.toString() + "%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

                $.getJSON(_query
                    ).done(function (data) {
                        if (data.query.results != null) {
                            var _listInstrumentMarketInfo = [];
                            for (var i = 0, l = data.query.results.quote.length; i < l; i++) {
                                var _instrumentMarketInfo = {
                                    Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    InstrumentID: data.query.results.quote[i].Symbol,
                                    Price: data.query.results.quote[i].Ask
                                };
                                _listInstrumentMarketInfo.push(_instrumentMarketInfo);
                            }
                            $.ajax({
                                url: window.location.origin + "/Radsoft/InstrumentMarketInfo/InstrumentMarketInfo_ReNewData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(_listInstrumentMarketInfo),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                    $.unblockUI();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            $.unblockUI();
                            alertify.alert("No Data");
                        }
                    }
                    ).fail(function (jqxhr, textStatus, error) {
                        var err = textStatus + ", " + error;
                        alertify.alert(err);
                        $.unblockUI();
                    })
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

    $("#BtnRefresh").click(function () {

        refresh();

        //RefreshAvailableCash();
        //RefreshNetBuySell();
            alertify.set('notifier','position', 'top-center'); alertify.success("Refresh Done");
    });

    $("#BtnRenewMarket").click(function () {
        GetPriceFromYahooFinance();
        //  GenerateNavProjection();
    });

    function showDetailsBuy(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var grid;
        _resetButtonUpdateBuySell();
        $("#BtnUpdateInvestmentBuy").show();
        $("#BtnUpdatInvestmentSell").hide();

        grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        _showdetails(dataItemX, 1);
    }

    $("#BtnUpdateInvestmentBuy").click(function () {
    var element = $("#BtnUpdateInvestmentBuy");
    var posY = element.offset() - 50;
    if (_GlobClientCode == "09") {
        _UpinstrumentPK = $('#CmbUpInstrumentPK').val();

    }
    else {
        _UpinstrumentPK = $('#UpInstrumentPK').val();
        var Upval = UpvalidateData();
    }
    if (Upval == 1) {
        alertify.confirm("Are you sure want to Update ?", function () {
            $.blockUI({});

            var Investment = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                OrderStatusDesc: $('#OrderStatus').val(),

                TrxType: $('#UpTrxType').val(),
                MarketPK: $('#UpMarketPK').val(),
                InstrumentPK: _UpinstrumentPK,
                InvestmentTrType: $('#UpInvestmentTrType').val(),
                OrderPrice: $('#UpOrderPrice').val(),
                RangePrice: $('#UpRangePrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                BitHTM: $('#UpBitHTM').is(":checked"),
                BIRate: $('#UpBIRate').val(),
                InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                InvestmentStyle: $('#UpInvestmentStyle').val(),
                InvestmentObjective: $('#UpInvestmentObjective').val(),
                Revision: $('#UpRevision').val(),
                OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                OtherRevision: $('#UpOtherRevision').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(Investment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UpAmount').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpFundPK').val() + "/" + $('#ParamDays').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                $.unblockUI();
                                alertify.confirm("Cash Not Available, </br> Are You Sure To Continue ?", function (e) {
                                    if (e) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/1",
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (dataCheckExposure) {

                                                if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                    $.unblockUI();
                                                    InitGridUpInformationExposure();

                                                } else {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                        type: 'POST',
                                                        data: JSON.stringify(Investment),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            $.unblockUI();
                                                            refresh();
                                                            alertify.alert('Amend Success').moveTo(posY.left, posY.top);
                                                            WinUpdateInvestment.close();
                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                        }
                                                    });
                                                }
                                            }
                                        });
                                    }
                                });
                            }
                            //Avaiable Cash
                            else {
                                if ($('#UpOrderPrice').val() > 0) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataCheckPrice) {

                                            if (dataCheckPrice.Validate == 0) {

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/1",
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (dataCheckExposure) {

                                                        if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                            $.unblockUI();
                                                            InitGridUpInformationExposure();

                                                        } else {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                type: 'POST',
                                                                data: JSON.stringify(Investment),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    refresh();
                                                                    alertify.alert('Amend Success').moveTo(posY.left, posY.top);
                                                                    WinUpdateInvestment.close();
                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                                }
                                                            });
                                                        }
                                                    }
                                                });
                                            }
                                            else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert("Amend Cancel, Can't Process This Data </br> Min Price  : " + dataCheckPrice.MinPrice + " </br> and Max Price : " + dataCheckPrice.MaxPrice).moveTo(posY.left, posY.top);
                                                        refresh();

                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                    }
                                                });
                                                return;
                                            }
                                        }
                                    });
                                } else {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/1",
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataCheckExposure) {
                                            if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                $.unblockUI();
                                                InitGridUpInformationExposure();

                                            } else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        refresh();
                                                        alertify.alert('Amend Success').moveTo(posY.left, posY.top);
                                                        WinUpdateInvestment.close();
                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                    }
                                                });
                                            }
                                        }
                                    });
                                }
                            }
                            $.unblockUI();
                        }
                    });



                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                }
            });


        }).moveTo(posY.left, posY.top);
    }
});

    function showDetailsSell(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var grid;


        grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        _showdetails(dataItemX, 2);

    }

    function _showdetails(dataItemX, _bs) {

    $("#BtnUpdateInvestmentBuy").hide();
    $("#BtnUpdateInvestmentSell").hide();
    if (dataItemX.OrderStatusDesc == "1.PENDING" || dataItemX.OrderStatusDesc == "2.APPROVED" || dataItemX.OrderStatusDesc == "5.PARTIAL" || dataItemX.OrderStatusDesc == "3.OPEN") {
        if (_bs == 1) {
            $("#BtnUpdateInvestmentBuy").show();
        }
        else {
            $("#BtnUpdateInvestmentSell").show();
        }

    }

    $("#UpBIRate").val(dataItemX.BIRate);
    $("#UpInvestmentStrategy").val(dataItemX.InvestmentStrategy);
    $("#UpInvestmentStyle").val(dataItemX.InvestmentStyle);
    $("#UpInvestmentObjective").val(dataItemX.InvestmentObjective);
    $("#UpRevision").val(dataItemX.Revision);
    $("#UpOtherInvestmentStyle").val(dataItemX.OtherInvestmentStyle);
    $("#UpOtherInvestmentObjective").val(dataItemX.OtherInvestmentObjective);
    $("#UpOtherRevision").val(dataItemX.OtherRevision);
    $("#UpInvestmentTrType").val(dataItemX.InvestmentTrType);
    $("#UpInvestmentPK").val(dataItemX.InvestmentPK);
    $("#UpDealingPK").val(dataItemX.DealingPK);
    $("#UpHistoryPK").val(dataItemX.HistoryPK);
    $("#UpStatusInvestment").val(dataItemX.StatusInvestment);
    $("#UpInvestmentNotes").val(dataItemX.InvestmentNotes);
    $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
    $("#UpInstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
    $("#UpLotInShare").val(dataItemX.LotInShare);
    $("#OrderStatus").val(dataItemX.OrderStatusDesc);
    $("#UpRangePrice").val(dataItemX.RangePrice);
    $("#UpBitForeignTrx").prop('checked', dataItemX.BitForeignTrx);
    $("#UpBitHTM").prop('checked', dataItemX.BitHTM);
    $("#EntryUsersID").val(dataItemX.EntryUsersID);
    $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
    $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
    $("#VoidUsersID").val(dataItemX.VoidUsersID);
    $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
    $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
    $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
    $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
    $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));


    //UpBIRate
    $.ajax({
        url: window.location.origin + "/Radsoft/BenchmarkIndex/GetBIRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpBIRate").kendoComboBox({
                dataValueField: "BenchmarkIndexPK",
                dataTextField: "CloseInd",
                dataSource: data,
                filter: "contains",
                suggest: true,
                index: 0,
                change: onChangeUpBIRate

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeUpBIRate() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }


    //InvestmentStrategy  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentStrategy",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpInvestmentStrategy").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeInvestmentStrategy

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeInvestmentStrategy() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }


    //combo box InvestmentStyle   
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentStyle",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpInvestmentStyle").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeUpInvestmentStyle,
                value: setCmbUpInvestmentStyle()
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function onChangeUpInvestmentStyle() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 6) {
            $("#lblUpOtherInvestmentStyle").show();
            $("#UpOtherInvestmentStyle").attr("required", true);
        } else {
            $("#lblUpOtherInvestmentStyle").hide();
            $("#UpOtherInvestmentStyle").attr("required", false);
        }
    }


    function setCmbUpInvestmentStyle() {
        if (dataItemX.InvestmentStyle == 6) {
            $("#lblUpOtherInvestmentStyle").show();
            $("#UpOtherInvestmentStyle").attr("required", true);
        } else {
            $("#lblUpOtherInvestmentStyle").hide();
            $("#UpOtherInvestmentStyle").attr("required", false);
        }
        if (dataItemX.InvestmentStyle == null) {
            return "";
        } else {
            if (dataItemX.InvestmentStyle == 0) {
                return "";
            }
            else if (dataItemX.InvestmentStyle == 6) {
                $("#lblUpOtherInvestmentStyle").show();
            }
            else {
                return dataItemX.InvestmentStyle;
            }
        }
    }



    //InvestmentObjective  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentObjective",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpInvestmentObjective").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeUpInvestmentObjective,
                value: setCmbUpInvestmentObjective()

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeUpInvestmentObjective() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 5) {
            $("#lblUpInvestmentObjective").show();
            $("#UpOtherInvestmentObjective").attr("required", true);
        } else {
            $("#lblUpInvestmentObjective").hide();
            $("#UpOtherInvestmentObjective").attr("required", false);
        }

    }

    function setCmbUpInvestmentObjective() {
        if (dataItemX.InvestmentObjective == 5) {
            $("#lblUpInvestmentObjective").show();
            $("#UpOtherInvestmentObjective").attr("required", true);
        } else {
            $("#lblUpInvestmentObjective").hide();
            $("#UpOtherInvestmentObjective").attr("required", false);
        }
        if (dataItemX.InvestmentObjective == null) {
            return "";
        } else {
            if (dataItemX.InvestmentObjective == 0) {
                return "";
            }
            else if (dataItemX.InvestmentObjective == 5) {
                $("#lblUpInvestmentObjective").show();
            }
            else {
                return dataItemX.InvestmentObjective;
            }
        }
    }



    //UpRevision  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Revision",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpRevision").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeUpRevision,
                value: setCmbUpRevision()

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeUpRevision() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 5) {
            $("#lblUpRevision").show();
            $("#UpOtherRevision").attr("required", true);
        } else {
            $("#lblUpRevision").hide();
            $("#UpOtherRevision").attr("required", false);
        }
    }

    function setCmbUpRevision() {
        if (dataItemX.Revision == 5) {
            $("#lblUpRevision").show();
            $("#UpOtherRevision").attr("required", true);
        } else {
            $("#lblUpRevision").hide();
            $("#UpOtherRevision").attr("required", false);
        }
        if (dataItemX.Revision == null) {
            return "";
        } else {
            if (dataItemX.Revision == 0) {
                return "";
            }
            else if (dataItemX.Revision == 5) {
                $("#lblUpRevision").show();
            }
            else {
                return dataItemX.Revision;
            }
        }
    }


    if (_GlobClientCode == "09") {

        //Combo Box Instrument 
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentComboByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/1",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpCmbInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpInstrumentPK,
                    value: setCmbUpInstrumentPK()
                });
            },
            error: function (data) {
                alertify.error(data.responseText);
            }
        });
        function onChangeUpInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbUpInstrumentPK() {
            if (dataItemX.InstrumentPK == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
                }
            }
        }

    }

    //Combo Box Market 
    $.ajax({
        url: window.location.origin + "/Radsoft/Market/GetMarketCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpMarketPK").kendoComboBox({
                dataValueField: "MarketPK",
                dataTextField: "ID",
                dataSource: data,
                filter: "contains",
                suggest: true,
                index: 0,
                change: onChangeUpMarketPK,
                value: setCmbMarketPK()
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    function onChangeUpMarketPK() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        //$("#UpInstrumentPK").val("");

        ////Combo Box Instrument 
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquityByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpMarketPK").val(),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#UpInstrumentPK").kendoComboBox({
        //            dataValueField: "InstrumentPK",
        //            dataTextField: "ID",
        //            dataSource: data,
        //            filter: "contains",
        //            suggest: true,
        //            change: onChangeInstrumentPK,
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //function onChangeInstrumentPK() {

        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }


        //}

    }

    function setCmbMarketPK() {
        if (dataItemX.MarketPK == null) {
            return "";
        } else {
            if (dataItemX.MarketPK == 0) {
                return "";
            } else {
                return dataItemX.MarketPK;
            }
        }
    }



    ////Combo Box Instrument 
    //$.ajax({
    //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquityByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.MarketPK,
    //    type: 'GET',
    //    contentType: "application/json;charset=utf-8",
    //    success: function (data) {
    //        $("#UpInstrumentPK").kendoComboBox({
    //            dataValueField: "InstrumentPK",
    //            dataTextField: "ID",
    //            dataSource: data,
    //            filter: "contains",
    //            suggest: true,
    //            change: onChangeInstrumentPK,
    //            value: setCmbInstrumentPK()
    //        });
    //    },
    //    error: function (data) {
    //        alertify.alert(data.responseText);
    //    }
    //});
    //function onChangeInstrumentPK() {

    //    if (this.value() && this.selectedIndex == -1) {
    //        var dt = this.dataSource._data[0];
    //        this.text('');
    //    }
    //}

    //function setCmbInstrumentPK() {
    //    if (dataItemX.InstrumentPK == null) {
    //        return "";
    //    } else {
    //        if (dataItemX.InstrumentPK == 0) {
    //            return "";
    //        } else {
    //            return dataItemX.InstrumentPK;
    //        }
    //    }
    //}


    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 1,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            if (_bs == 0) {
                $("#UpTrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    //index: 0,
                    value: setCmbTrxType()
                });
            } else if (_bs == 1) // 1 = buy
            {
                $("#UpTrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    //index: 0,
                    value: setCmbTrxType()
                });
            } else {
                $("#UpTrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    //index: 1,
                    value: setCmbTrxType()

                });
            }


        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    function onChangeTrxType() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    function setCmbTrxType() {
        if (dataItemX.TrxType == null) {
            return "";
        } else {
            if (dataItemX.TrxType == 0) {
                return "";
            } else {
                return dataItemX.TrxType;
            }
        }
    }

    //Combo Box Fund 
    $.ajax({
        url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpFundPK").kendoComboBox({
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
        }
    });
    function onChangeFundPK() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    function setCmbFundPK() {
        if (dataItemX.FundPK == null) {
            return "";
        } else {
            if (dataItemX.FundPK == 0) {
                return "";
            } else {
                return dataItemX.FundPK;
            }
        }
    }


    $("#UpAmount").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        value: setUpAmount(),
        //change: OnChangeAmount
    });
    function setUpAmount() {
        if (dataItemX.Amount == null) {
            return "";
        } else {
            if (dataItemX.Amount == 0) {
                return "";
            } else {
                return dataItemX.Amount;
            }
        }
    }
    //$("#UpAmount").data("kendoNumericTextBox").enable(false);

    $("#UpMethodType").kendoComboBox({
        dataValueField: "value",
        dataTextField: "text",
        filter: "contains",
        suggest: true,
        dataSource: [
            { text: "Share", value: "1" },
            { text: "Amount", value: "2" },
        ],
        change: onChangeUpMethodType,
        value: setCmbUpMethodType()
    });

    function setCmbUpMethodType() {
        if (dataItemX.OrderPrice == null) {
            return 2;
        } else {
            if (dataItemX.OrderPrice == "" || dataItemX.OrderPrice == 0) {
                $("#LblUpPrice").hide();
                $("#LblUpLot").hide();
                $("#LblUpLotInShare").hide();
                $("#LblUpVolume").hide();
                $("#UpAmount").attr("required", false);
                $("#UpAmount").data("kendoNumericTextBox").enable(false);
                $("#UpLot").attr("required", false);
                $("#UpOrderPrice").attr("required", false);

                $("#UpAmount").data("kendoNumericTextBox").enable(true);
                $("#UpAmount").attr("required", true);

                return 2;
            } else {
                $("#LblUpPrice").hide();
                $("#LblUpLot").hide();
                $("#LblUpLotInShare").hide();
                $("#LblUpVolume").hide();
                $("#UpAmount").attr("required", false);
                $("#UpAmount").data("kendoNumericTextBox").enable(false);
                $("#UpLot").attr("required", false);
                $("#UpOrderPrice").attr("required", false);

                $("#LblUpPrice").show();
                $("#LblUpLot").show();
                $("#LblUpLotInShare").show();
                $("#LblUpVolume").show();

                $("#UpLot").attr("required", true);
                $("#UpOrderPrice").attr("required", true);

                return 1;
            }
        }
    }

    function onChangeUpMethodType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }



        if ($("#UpMethodType").val() == 1) {
            ResetLblUpMethodType();
            $("#LblUpPrice").show();
            $("#LblUpLot").show();
            $("#LblUpLotInShare").show();
            $("#LblUpVolume").show();

            $("#UpLot").attr("required", true);
            $("#UpOrderPrice").attr("required", true);
        }
        else {
            ResetLblUpMethodType();
            $("#UpAmount").data("kendoNumericTextBox").value("");
            $("#UpAmount").data("kendoNumericTextBox").enable(true);
            $("#UpAmount").attr("required", true);
        }
    }


    function ResetLblUpMethodType() {
        $("#LblUpPrice").hide();
        $("#LblUpLot").hide();
        $("#LblUpLotInShare").hide();
        $("#LblUpVolume").hide();
        $("#UpAmount").attr("required", false);
        $("#UpAmount").data("kendoNumericTextBox").enable(false);

        $("#UpLot").attr("required", false);
        $("#UpOrderPrice").attr("required", false);

    }



    $("#UpLot").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        change: OnChangeUpLot,
        value: setUpLot(),
    });

    function setUpLot() {
        if (dataItemX.DoneLot == null) {
            return "";
        } else {
            if (dataItemX.DoneLot == 0) {
                return "";
            } else {
                return dataItemX.DoneLot;
            }
        }
    }



    $("#UpVolume").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        change: OnChangeUpVolume,
        value: setUpVolume(),
    });

    function setUpVolume() {
        if (dataItemX.Lot == null) {
            return "";
        } else {
            if (dataItemX.DoneVolume == 0) {
                return "";
            } else {
                return dataItemX.DoneVolume;
            }
        }
    }

    $("#UpOrderPrice").kendoNumericTextBox({
        format: "n6",
        decimals: 6,
        change: OnChangeUpOrderPrice,
        value: setUpOrderPrice(),
    });

    function setUpOrderPrice() {
        if (dataItemX.OrderPrice == null) {
            return "";
        } else {
            if (dataItemX.OrderPrice == 0) {
                return "";
            } else {
                return dataItemX.OrderPrice;
            }
        }
    }



    if (_GlobClientCode == "09") {

        $("#trUpMethodType").hide();
        $("#trUpMarket").hide();
        $("#trUpInstrumentList").hide();
        $("#trUpForeignTax").hide();
        $("#LblUpRangePrice").hide();
        $("#trUpInstrumentCombo").show();
        $("#trUpInvestmentTrType").hide();
        $("#trUpBIRate").hide();
        $("#trUpInvestmentStrategy").hide();
        $("#trUpInvestmentStyle").hide();
        $("#trUpInvestmentObjective").hide();
        $("#trUpRevision").hide();

    }
    else if (_GlobClientCode == "26") {
        $("#trUpMethodType").show();
        $("#trUpMarket").show();
        $("#trUpInstrumentList").show();
        $("#trUpForeignTax").show();
        $("#LblUpRangePrice").show();
        $("#trUpInstrumentCombo").hide();
        $("#trUpInvestmentTrType").show();
        $("#trUpBIRate").hide();
        $("#trUpInvestmentStrategy").hide();
        $("#trUpInvestmentStyle").hide();
        $("#trUpInvestmentObjective").hide();
        $("#trUpRevision").hide();
    }
    else if (_GlobClientCode == "20") {
        $("#trUpMethodType").hide();
        $("#trUpMarket").show();
        $("#trUpInstrumentList").show();
        $("#trUpForeignTax").show();
        $("#LblUpRangePrice").show();
        $("#trUpInstrumentCombo").hide();
        $("#trUpInvestmentTrType").hide();
        $("#trUpBIRate").show();
        $("#trUpInvestmentStrategy").show();
        $("#trUpInvestmentStyle").show();
        $("#trUpInvestmentObjective").show();
        $("#trUpRevision").show();
    }

    else {
        $("#trUpMethodType").show();
        $("#trUpMarket").show();
        $("#trUpInstrumentList").show();
        $("#trUpForeignTax").show();
        $("#LblUpRangePrice").show();
        $("#trUpInstrumentCombo").hide();
        $("#trUpInvestmentTrType").hide();
        $("#trUpBIRate").hide();
        $("#trUpInvestmentStrategy").hide();
        $("#trUpInvestmentStyle").hide();
        $("#trUpInvestmentObjective").hide();
        $("#trUpRevision").hide();
    }

    WinUpdateInvestment.center();
    WinUpdateInvestment.open();
}

    function OnChangeUpLot() {
        $("#UpVolume").data("kendoNumericTextBox").value($("#UpLot").data("kendoNumericTextBox").value() * 100);
        RecalculateUpPriceAndVolume();
    }

    function OnChangeUpOrderPrice() {
        RecalculateUpPriceAndVolume();
    }

    function RecalculateUpPriceAndVolume() {
        if ($("#UpVolume").data("kendoNumericTextBox").value() != "" && $("#UpVolume").data("kendoNumericTextBox").value() != null && $("#UpVolume").data("kendoNumericTextBox").value() != 0 && $("#UpOrderPrice").data("kendoNumericTextBox").value() != 0) {
            $("#UpAmount").data("kendoNumericTextBox").value($("#UpOrderPrice").data("kendoNumericTextBox").value() * $("#UpVolume").data("kendoNumericTextBox").value())
        }
    }

    function OnChangeUpVolume() {
        $("#UpLot").data("kendoNumericTextBox").value($("#UpVolume").data("kendoNumericTextBox").value() / 100);
        RecalculateUpPriceAndVolume();
    }

    $("#BtnUpdateInvestmentSell").click(function () {
    var element = $("#BtnUpdateInvestmentSell");
    var posY = element.offset() - 50;
    if (_GlobClientCode == "09") {
        _UpinstrumentPK = $('#CmbUpInstrumentPK').val();

    }
    else {
        _UpinstrumentPK = $('#UpInstrumentPK').val();
        var Upval = UpvalidateData();
    }
    if (Upval == 1) {
        alertify.confirm("Are you sure want to Update ?", function (e) {

            $.blockUI({});

            var Investment = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                OrderStatusDesc: $('#OrderStatus').val(),

                TrxType: $('#UpTrxType').val(),
                MarketPK: $('#UpMarketPK').val(),
                InstrumentPK: _UpinstrumentPK,
                InvestmentTrType: $('#UpInvestmentTrType').val(),
                RangePrice: $('#UpRangePrice').val(),
                OrderPrice: $('#UpOrderPrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                BitHTM: $('#UpBitHTM').is(":checked"),
                BIRate: $('#UpBIRate').val(),
                InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                InvestmentStyle: $('#UpInvestmentStyle').val(),
                InvestmentObjective: $('#UpInvestmentObjective').val(),
                Revision: $('#UpRevision').val(),
                OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                OtherRevision: $('#UpOtherRevision').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(Investment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    if ($('#UpOrderPrice').val() > 0) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _UpinstrumentPK + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (dataCheckPrice) {
                                if (dataCheckPrice.Validate == 0) {
                                    var Validate = {
                                        ValueDate: $('#DateFrom').val(),
                                        FundPK: $('#UpFundPK').val(),
                                        InstrumentPK: _UpinstrumentPK,
                                        Volume: $('#UpVolume').val(),
                                        TrxBuy: $('#UpTrxBuy').val(),
                                        TrxBuyType: $('#UpTrxBuyType').val(),
                                        Amount: $('#UpAmount').val(),
                                        MethodType: $('#UpMethodType').val(),
                                        BitHTM: $('#UpBitHTM').is(":checked"),
                                        BIRate: $('#UpBIRate').val(),
                                        InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                                        InvestmentStyle: $('#UpInvestmentStyle').val(),
                                        InvestmentObjective: $('#UpInvestmentObjective').val(),
                                        Revision: $('#UpRevision').val(),
                                        OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                                        OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                                        OtherRevision: $('#UpOtherRevision').val(),
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(Validate),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == true) {

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        refresh();
                                                        alertify.alert('Short Sell, Amend Cancel').moveTo(posY.left, posY.top - 200);

                                                        return;
                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                    }
                                                });



                                            }
                                            else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/1",
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (dataCheckExposure) {

                                                        if (dataCheckExposure.AlertExposure == 3 || dataCheckExposure.AlertExposure == 4 || dataCheckExposure.AlertExposure == 7 || dataCheckExposure.AlertExposure == 8) {
                                                            $.unblockUI();
                                                            InitGridUpInformationExposure();

                                                        } else {


                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                type: 'POST',
                                                                data: JSON.stringify(Investment),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    refresh();
                                                                    alertify.alert('Amend Success').moveTo(posY.left, posY.top - 200);
                                                                    WinUpdateInvestment.close();
                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                }
                                                            });
                                                        }
                                                    }
                                                });
                                            }
                                            $.unblockUI();
                                        }
                                    });
                                } else {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                        type: 'POST',
                                        data: JSON.stringify(Investment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            $.unblockUI();
                                            refresh();
                                            alertify.alert('Amend Cancel, Check Price please').moveTo(posY.left, posY.top - 200);

                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                        }
                                    });
                                    return;

                                }
                            }
                        });
                    }
                    else {
                        var Validate = {
                            ValueDate: $('#DateFrom').val(),
                            FundPK: $('#UpFundPK').val(),
                            InstrumentPK: _UpinstrumentPK,
                            Volume: $('#UpVolume').val(),
                            TrxBuy: $('#UpTrxBuy').val(),
                            TrxBuyType: $('#UpTrxBuyType').val(),
                            Amount: $('#UpAmount').val(),
                            MethodType: $('#UpMethodType').val(),
                            BitHTM: $('#UpBitHTM').is(":checked"),
                            BIRate: $('#UpBIRate').val(),
                            InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                            InvestmentStyle: $('#UpInvestmentStyle').val(),
                            InvestmentObjective: $('#UpInvestmentObjective').val(),
                            Revision: $('#UpRevision').val(),
                            OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                            OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                            OtherRevision: $('#UpOtherRevision').val(),

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(Validate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == true) {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                        type: 'POST',
                                        data: JSON.stringify(Investment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            $.unblockUI();
                                            refresh();
                                            alertify.alert('Short Sell, Amend Cancel').moveTo(posY.left, posY.top - 200);

                                            return;
                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                        }
                                    });
                                }
                                else {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/1",
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataCheckExposure) {

                                            if (dataCheckExposure.AlertExposure == 3 || dataCheckExposure.AlertExposure == 4 || dataCheckExposure.AlertExposure == 7 || dataCheckExposure.AlertExposure == 8) {
                                                $.unblockUI();
                                                InitGridUpInformationExposure();

                                            } else {


                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        refresh();
                                                        alertify.alert('Amend Success').moveTo(posY.left, posY.top - 200);
                                                        WinUpdateInvestment.close();
                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                    }
                                                });
                                            }
                                        }
                                    });
                                }
                                $.unblockUI();
                            }
                        });
                    }
                }
            });
            $.unblockUI();

        }).moveTo(posY.left, posY.top - 200);
    }
});

    $("#BtnCancelUpdateInvestment").click(function () {
        var element = $("#BtnCancelUpdateInvestment");
        var posY = element.offset();
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                WinUpdateInvestment.close();
                alertify.alert("Close Update").moveTo(posY.left, posY.top - 200);
            }
        }).moveTo(posY.left, posY.top - 200);
    });

    function refresh() {
        if (_GlobClientCode == "17") {
            RefreshPendingCash();
        }
        RefreshAUMYesterday();
        RefreshAvailableCash();
        RefreshNetBuySell();
        // GenerateNavProjection();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        if ($("#FilterFundID").data("kendoComboBox").value() == null || $("#FilterFundID").data("kendoComboBox").value() == "") {
            alertify.alert("Please Fill Fund");
            return;
        }
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }

        var newDS = getDataSourceInvesmentInstruction(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID);
        $("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

        // Refresh Grid Bank Position Per Fund ( atas Layar )
        var newDS = getDataSourceA(window.location.origin + "/Radsoft/OMSEquity/OMSEquity_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridFundPosition").data("kendoGrid").setDataSource(newDS);

        // Re
        var newDS = getDataSourceFundExposurePerFund(window.location.origin + "/Radsoft/OMSEquity/OMSEquity_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/1");
        $("#gridFundExposure").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSEquityBySectorID(window.location.origin + "/Radsoft/OMSEquity/OMSEquity_BySectorID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSEquityBySectorID").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSEquityByIndex(window.location.origin + "/Radsoft/OMSEquity/OMSEquity_ByIndex/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSEquityByIndex").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSEquityByInstrument(window.location.origin + "/Radsoft/OMSEquity/OMSEquity_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSEquityByInstrument").data("kendoGrid").setDataSource(newDS);

        for (var i in checkedInvestmentBuy) {
            checkedInvestmentBuy[i] = null
        }
        for (var i in checkedInvestmentSell) {
            checkedInvestmentSell[i] = null
        }

        if ($('#chbB').is(":checked")) {
            $("#chbB").click();
        }
        if ($('#chbS').is(":checked")) {
            $("#chbS").click();
        }
    }

    function refreshBuyOnly() {
        if (_GlobClientCode == "17") {
            RefreshPendingCash();
        }
        RefreshAvailableCash();
        RefreshNetBuySell();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshSellOnly() {
        if (_GlobClientCode == "17") {
            RefreshPendingCash();
        }
        RefreshAvailableCash();
        RefreshNetBuySell();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    InitgridFundPosition();

    function InitgridFundPosition() {
        var AccountApprovedURL = window.location.origin + "/Radsoft/OMSEquity/OMSEquity_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceA(AccountApprovedURL);

        gridFundPositionGlob = $("#gridFundPosition").kendoGrid({
            dataSource: dataSourceApproved,
            editable: false,
            height: "420px",
            scrollable: {
                virtual: true
            },
            pageable: false,
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            selectable: "cell",
            resizable: true,
            toolbar: kendo.template($("#gridFundPositionTemplate").html()),
            excel: {
                fileName: "OMSEquityFundPosition.xlsx"
            },
            columns: [{
                field: "Name",
                title: "InstrumentID", headerAttributes: {
                    style: "text-align: center"
                },
                width: 100
            },
            {
                title: "T0", headerAttributes: {
                    style: "text-align: center"
                },
                columns: [{
                    title: "Current", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "CurrBalance", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }, {
                        field: "CurrNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        format: "{0:n2}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 60
                    }]
                }, {
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "Movement", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterBalance", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }, {
                        field: "AfterNAVPercent", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 60
                    }]
                }]
            },
            {
                title: "T + 1", headerAttributes: {
                    style: "text-align: center"
                },
                columns: [{
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "MovementTOne", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTOne", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }, {
                        field: "AfterTOneNAVPercent", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 60
                    }]
                }]
            },
            {
                title: "T + 2", headerAttributes: {
                    style: "text-align: center"
                },
                columns: [{
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "MovementTTwo", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTTwo", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }, {
                        field: "AfterTTwoNAVPercent", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 60
                    }]
                }]
            },
            {
                title: "T + 3", headerAttributes: {
                    style: "text-align: center"
                },
                columns: [{
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "MovementTThree", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTThree", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 80
                    }, {
                        field: "AfterTThreeNAVPercent", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 60
                    }]
                }]
            }]
        }).data("kendoGrid");
    }

    function getDataSourceA(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                Name: { type: "string" },
                                CurrBalance: { type: "number" },
                                CurrNAVPercent: { type: "number" },
                                Movement: { type: "number" },
                                AfterBalance: { type: "number" },
                                AfterNAVPercent: { type: "number" },
                                MovementTOne: { type: "number" },
                                AfterTOne: { type: "number" },
                                AfterTOneNAVPercent: { type: "number" },
                                MovementTTwo: { type: "number" },
                                AfterTTwo: { type: "number" },
                                AfterTTwoNAVPercent: { type: "number" },
                                MovementTThree: { type: "number" },
                                AfterTThree: { type: "number" },
                                AfterTThreeNAVPercent: { type: "number" },
                                StatusDesc: { type: "string" }
                            }
                        }
                    },
                    aggregate: [{ field: "CurrBalance", aggregate: "sum" },
                    { field: "CurrNAVPercent", aggregate: "sum" },
                    { field: "Movement", aggregate: "sum" },
                    { field: "AfterBalance", aggregate: "sum" },
                    { field: "AfterNAVPercent", aggregate: "sum" },

                    { field: "MovementTOne", aggregate: "sum" },
                    { field: "AfterTOne", aggregate: "sum" },
                    { field: "AfterTOneNAVPercent", aggregate: "sum" },

                    { field: "MovementTTwo", aggregate: "sum" },
                    { field: "AfterTTwo", aggregate: "sum" },
                    { field: "AfterTTwoNAVPercent", aggregate: "sum" },

                    { field: "MovementTThree", aggregate: "sum" },
                    { field: "AfterTThree", aggregate: "sum" },
                    { field: "AfterTThreeNAVPercent", aggregate: "sum" }
                    ]
                });
    }

    InitgridFundExposure();

    function gridFundExposureOnDataBound(e) {
        var grid = $("#gridFundExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertMaxExposure == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            } else if (row.AlertWarningMaxExposure == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            }
        });
    }

    function getDataSourceFundExposurePerFund(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                ExposureType: { type: "string" },
                                ExposureID: { type: "string" },
                                MaxExposurePercent: { type: "number" },
                                MinExposurePercent: { type: "number" },
                                MarketValue: { type: "number" },
                                ExposurePercent: { type: "number" },
                                AlertMinExposure: { type: "boolean" },
                                AlertMaxExposure: { type: "boolean" },
                                AlertWarningMaxExposure: { type: "boolean" },
                                AlertWarningMinExposure: { type: "boolean" }
                            },


                        }
                    },

                    group: {
                        field: "ExposureType", aggregates: [
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "ExposurePercent", aggregate: "sum" },
                        ]
                    },
                    aggregate: [
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "ExposurePercent", aggregate: "sum" },
                    ]
                });
    }

    function InitgridFundExposure() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSEquity/OMSEquity_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/1",
            dataSourceApproved = getDataSourceFundExposurePerFund(FundApprovedURL);

        gridFundExposureGlob = $("#gridFundExposure").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            groupable: true,
            reorderable: true,
            resizable: true,
            sortable: true,
            selectable: "cell",
            dataBound: gridFundExposureOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "1000px",
            toolbar: kendo.template($("#gridFundExposureTemplate").html()),
            excel: {
                fileName: "OMSEquityExposure.xlsx"
            },
            columns: [
                {
                    field: "ExposureType", title: "ExposureType", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "ExposureID", title: "ExposureID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MaxExposurePercent", title: "MaxExposure %", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MinExposurePercent", title: "MinExposure %", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "ExposurePercent", title: "Exposure %", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "AlertMinExposure", title: "Min Exp", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= AlertMinExposure ? 'Yes' : 'No' #"
                },
                {
                    field: "AlertMaxExposure", title: "Max Exp", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= AlertMaxExposure ? 'Yes' : 'No' #"
                },
                {
                    field: "AlertWarningMinExposure", title: "Warning Min", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= AlertWarningMinExposure ? 'Yes' : 'No' #"
                },
                {
                    field: "AlertWarningMaxExposure", title: "Warning Max", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= AlertWarningMaxExposure ? 'Yes' : 'No' #"
                },

            ]
        }).data("kendoGrid");
    }

    InitgridOMSEquityBySectorID();

    function gridOMSEquityBySectorIDOnDataBound(e) {
        var grid = $("#gridOMSEquityBySectorID").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSEquityBySectorID(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                InstrumentID: { type: "string" },
                                SectorID: { type: "string" },
                                CurrentExposure: { type: "number" },
                                Lot: { type: "number" },
                                AvgPrice: { type: "number" },
                                Cost: { type: "number" },
                                ClosePrice: { type: "number" },
                                LastPrice: { type: "number" },
                                PriceDifference: { type: "number" },
                                MarketValue: { type: "number" },
                                UnRealized: { type: "number" },
                                GainLoss: { type: "number" }
                            }
                        }
                    },
                    group: {
                        field: "SectorID", aggregates: [
                        { field: "Lot", aggregate: "sum" },
                        { field: "CurrentExposure", aggregate: "sum" },
                        { field: "Cost", aggregate: "sum" },
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "UnRealized", aggregate: "sum" },
                        { field: "GainLoss", aggregate: "max" }
                        ]
                    },
                    aggregate: [{ field: "Lot", aggregate: "sum" },
                        { field: "CurrentExposure", aggregate: "sum" },
                        { field: "Cost", aggregate: "sum" },
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "UnRealized", aggregate: "sum" },
                        { field: "GainLoss", aggregate: "max" }
                    ]
                });
    }

    function InitgridOMSEquityBySectorID() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSEquity/OMSEquity_BySectorID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceOMSEquityBySectorID(FundApprovedURL);

        gridOMSEquityBySectorID = $("#gridOMSEquityBySectorID").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSEquityBySectorIDOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSEquityBySectorIDTemplate").html()),
            excel: {
                fileName: "OMSEquityBySector.xlsx"
            },
            columns: [
                    {
                        field: "InstrumentID", title: "InstrumentID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                //{
                //    field: "SectorID", title: "SectorID", headerAttributes: {
                //        style: "text-align: center"
                //    }, width: 50
                //},
                {
                    field: "CurrentExposure", title: "Curr Exposure %", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Lot", title: "Lot", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Cost", title: "Cost", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },

                {
                    field: "ClosePrice", title: "ClosePrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "LastPrice", title: "LastPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "PriceDifference", title: "PriceDiff %", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                },
                {
                    field: "UnRealized", title: "UnRealized", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "GainLoss", title: "GainLoss %", format: "{0:n2}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
            ]
        }).data("kendoGrid");
    }

    InitgridOMSEquityByIndex();

    function gridOMSEquityByIndexOnDataBound(e) {
        var grid = $("#gridOMSEquityByIndex").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSEquityByIndex(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                InstrumentID: { type: "string" },
                                IndexID: { type: "string" },
                                CurrentExposure: { type: "number" },
                                Lot: { type: "number" },
                                AvgPrice: { type: "number" },
                                Cost: { type: "number" },
                                ClosePrice: { type: "number" },
                                LastPrice: { type: "number" },
                                PriceDifference: { type: "number" },
                                MarketValue: { type: "number" },
                                UnRealized: { type: "number" },
                                GainLoss: { type: "number" }
                            }
                        }
                    },
                    group: {
                        field: "IndexID", aggregates: [
                        { field: "Lot", aggregate: "sum" },
                        { field: "CurrentExposure", aggregate: "sum" },
                        { field: "Cost", aggregate: "sum" },
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "UnRealized", aggregate: "sum" },
                        { field: "GainLoss", aggregate: "max" }
                        ]
                    },
                    aggregate: [{ field: "Lot", aggregate: "sum" },
                        { field: "CurrentExposure", aggregate: "sum" },
                        { field: "Cost", aggregate: "sum" },
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "UnRealized", aggregate: "sum" },
                        { field: "GainLoss", aggregate: "max" }
                    ]
                });
    }

    function InitgridOMSEquityByIndex() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSEquity/OMSEquity_ByIndex/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceOMSEquityByIndex(FundApprovedURL);

        gridOMSEquityByIndex = $("#gridOMSEquityByIndex").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSEquityByIndexOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSEquityByIndexTemplate").html()),
            excel: {
                fileName: "OMSEquityByIndex.xlsx"
            },
            columns: [
                    {
                        field: "InstrumentID", title: "InstrumentID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                //{
                //    field: "IndexID", title: "Index", headerAttributes: {
                //        style: "text-align: center"
                //    }, width: 50
                //},
                    {
                        field: "CurrentExposure", title: "Curr Exposure %", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                {
                    field: "Lot", title: "Lot", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Cost", title: "Cost", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },

                {
                    field: "ClosePrice", title: "ClosePrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "LastPrice", title: "LastPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "PriceDifference", title: "PriceDiff %", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                },
                {
                    field: "UnRealized", title: "UnRealized", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "GainLoss", title: "GainLoss %", format: "{0:n2}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
            ]
        }).data("kendoGrid");
    }

    InitgridOMSEquityByInstrument();

    function gridOMSEquityByInstrumentOnDataBound(e) {
        var grid = $("#gridOMSEquityByInstrument").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSEquityByInstrument(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                InstrumentID: { type: "string" },
                                SectorID: { type: "string" },
                                CurrentExposure: { type: "number" },
                                Lot: { type: "number" },
                                AvgPrice: { type: "number" },
                                Cost: { type: "number" },
                                ClosePrice: { type: "number" },
                                LastPrice: { type: "number" },
                                PriceDifference: { type: "number" },
                                MarketValue: { type: "number" },
                                UnRealized: { type: "number" },
                                GainLoss: { type: "number" },
                                Status: { type: "number" }
                            }
                        }
                    },
                    //group: {
                    //    field: "IndexID", aggregates: [
                    //    { field: "Lot", aggregate: "sum" },
                    //    { field: "CurrentExposure", aggregate: "sum" },
                    //    { field: "Cost", aggregate: "sum" },
                    //    { field: "UnRealized", aggregate: "sum" },
                    //    { field: "GainLoss", aggregate: "max" }
                    //    ]
                    //},
                    aggregate: [{ field: "Lot", aggregate: "sum" },
                        { field: "CurrentExposure", aggregate: "sum" },
                        { field: "Cost", aggregate: "sum" },
                        { field: "MarketValue", aggregate: "sum" },
                        { field: "UnRealized", aggregate: "sum" },
                        { field: "GainLoss", aggregate: "max" }
                    ]
                });
    }

    function InitgridOMSEquityByInstrument() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSEquity/OMSEquity_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceOMSEquityByInstrument(FundApprovedURL);

        gridOMSEquityByInstrument = $("#gridOMSEquityByInstrument").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSEquityByInstrumentOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSEquityByInstrumentTemplate").html()),
            excel: {
                fileName: "OMSEquityByInstrument.xlsx"
            },
            columns: [
                    {
                        field: "InstrumentID", title: "Instrument ID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "SectorID", title: "Sub Sector", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "CurrentExposure", title: "Curr Exposure %", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                        attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                {
                    field: "Lot", title: "Lot", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Cost", title: "Cost", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },

                {
                    field: "ClosePrice", title: "ClosePrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "LastPrice", title: "LastPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "PriceDifference", title: "PriceDiff %", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                },
                {
                    field: "UnRealized", title: "UnRealized", format: "{0:n2}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "GainLoss", title: "GainLoss %", format: "{0:n2}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    hidden: true, field: "Status", title: "Status", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
            ]
        }).data("kendoGrid");
    }

    if (_GlobClientCode == "05") {
        InitgridInvestmentInstruction();
        function InitgridInvestmentInstruction() {
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
                alertify.alert("Please Fill Date");
            }
            else {
                var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvesmentInstruction(InvestmentInstructionApprovedURL);
            }


            $("#gridInvestmentInstruction").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "700px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplate").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                        { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                        {
                            field: "OrderStatusDesc", title: "O.Status", headerAttributes: {
                                style: "text-align: center"
                            }, width: 30, attributes: { style: "text-align:center;" }
                        },
                        {
                            field: "FundID", title: "Fund", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "InstrumentID", title: "InstrumentID", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                    {
                        hidden: true, field: "TrxTypeID", title: "(B/S)", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "OrderPrice", title: "O.Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DonePrice", title: "D.Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DoneLot", title: "Lot", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { field: "ApprovedDealingTime", title: "Update Time Dealer", width: 50, format: "{0:dd/MMM/yyyy HH:mm:ss}" },
                    {
                        field: "InvestmentNotes", title: "Remark", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                ]
            });
        }

    }
    else
    {
        InitgridInvestmentInstruction();
        function InitgridInvestmentInstruction() {
            if ($("#FilterFundID").val() == "") {
                _fundID = "0";
            }
            else {
                _fundID = $("#FilterFundID").val();
            }
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
                alertify.alert("Please Fill Date");
            }
            else {
                var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvesmentInstruction(InvestmentInstructionApprovedURL);
            }


            $("#gridInvestmentInstruction").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "700px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplate").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                        { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                        {
                            field: "OrderStatusDesc", title: "O.Status", headerAttributes: {
                                style: "text-align: center"
                            }, width: 30, attributes: { style: "text-align:center;" }
                        },
                        {
                            field: "FundID", title: "Fund", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "InstrumentID", title: "InstrumentID", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                    {
                        hidden: true, field: "TrxTypeID", title: "(B/S)", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "DonePrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DoneLot", title: "Lot", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { field: "ApprovedDealingTime", title: "Update Time Dealer", width: 50, format: "{0:dd/MMM/yyyy HH:mm:ss}" },
                    {
                        field: "InvestmentNotes", title: "Remark", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                ]
            });
        }
    }

    InitgridInvestmentInstructionBuyOnly();

    function InitgridInvestmentInstructionBuyOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }

        if (_GlobClientCode == "05") {
            var gridEquityBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },

                columns: [
                    ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "DealingPK", title: "", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DonePrice", title: "Done Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    //{
                    //    field: "DoneLot", title: "Lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                    { hidden: true, field: "BitForeignTrx", title: "BitForeignTrx", width: 50 },
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "09") {
            var gridEquityBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },

                columns: [
                    ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 40 },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "DealingPK", title: "", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "DonePrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    //{
                    //    field: "DoneLot", title: "Lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },

                ]
            }).data("kendoGrid");


        }
        else if (_GlobClientCode == "28") {
            var gridEquityBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "DealingPK", title: "", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    //{
                    //    field: "DonePrice", title: "Price", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    field: "DoneLot", title: "Lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },

                ]
            }).data("kendoGrid");

        }
        else {
            var gridEquityBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "DealingPK", title: "", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" },
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    //{
                    //    field: "DonePrice", title: "Price", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    field: "DoneLot", title: "Lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },

                ]
            }).data("kendoGrid");
        }

        gridEquityBuyOnly.table.on("click", ".checkboxBuy", selectRowBuy);
        var oldPageSizeApproved = 0;


        $('#chbB').change(function (ev) {

            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxBuy').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            grid.dataSource.pageSize(oldPageSizeApproved);

            var checked = [];
            for (var i in checkedInvestmentBuy) {
                if (checkedInvestmentBuy[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });


        function selectRowBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedInvestmentBuy[dataItemZ.InvestmentPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }

        }


        $("#SelectedAllApprovedEquityBuy").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataEquityBuyOnly(_checked, "Approved");

        });

        //gridEquityBuyOnly.table.on("click", ".cSelectedDetailApprovedEquityBuy", selectDataApprovedEquityBuy);

        function selectDataApprovedEquityBuy(e) {


            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataEquityBuyOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataEquityBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
}

    function SelectDeselectAllDataEquityBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshBuyOnly();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    InitgridInvestmentInstructionSellOnly();

    function InitgridInvestmentInstructionSellOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }

        if (_GlobClientCode == "05") {
            var gridEquitySellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "FundPK", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DonePrice", title: "Done Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    //{
                    //    field: "DoneLot", title: "lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                    { hidden: true, field: "BitForeignTrx", title: "BitForeignTrx", width: 50 },
                ]
            }).data("kendoGrid");
        }

        else if (_GlobClientCode == "09") {
            var gridEquitySellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "FundPK", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                    {
                        field: "DonePrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "DoneLot", title: "lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                ]
            }).data("kendoGrid");

        }
        else if (_GlobClientCode == "28") {
            var gridEquitySellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "FundPK", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                    //{
                    //    field: "DonePrice", title: "Price", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    field: "DoneLot", title: "lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                ]
            }).data("kendoGrid");
        }
        else {
            var gridEquitySellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "800px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSEquityInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 40 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "OrderStatusDesc", hidden: true, title: "O.Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: true, field: "FundPK", title: "FundPK", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                    },
                    {
                        field: "InstrumentID", title: "Stock", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "OrderPrice", title: "Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "Volume", title: "Volume", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },

                    //{
                    //    field: "DonePrice", title: "Price", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    field: "DoneLot", title: "lot", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 60, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                    //        style: "text-align: center"
                    //    }, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    //},
                    {
                        field: "DoneAmount", title: "Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "RangePrice", title: "Range Price", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "InvestmentNotes", title: "Notes", width: 50 },
                    { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                ]
            }).data("kendoGrid");
        }


        gridEquitySellOnly.table.on("click", ".checkboxSell", selectRowSell);
        var oldPageSizeApproved = 0;


        $('#chbS').change(function (ev) {

            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxSell').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            grid.dataSource.pageSize(oldPageSizeApproved);

            var checked = [];
            for (var i in checkedInvestmentSell) {
                if (checkedInvestmentSell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);

        });

        function selectRowSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedInvestmentSell[dataItemZ.InvestmentPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }
        }

        $("#SelectedAllApprovedEquitySell").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataEquitySellOnly(_checked, "Approved");

        });

        //gridEquitySellOnly.table.on("click", ".cSelectedDetailApprovedEquitySell", selectDataApprovedEquitySell);

        function selectDataApprovedEquitySell(e) {


            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataEquitySellOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataEquitySellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
}

    function SelectDeselectAllDataEquitySellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshSellOnly();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }

    function getDataSourceInvesmentInstruction(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 500,
                    schema: {
                        model: {
                            fields: {
                                InvestmentPK: { type: "number" },
                                DealingPK: { type: "number" },
                                HistoryPK: { type: "number" },
                                TrxTypeID: { type: "string" },
                                FundID: { type: "string" },
                                StatusInvestment: { type: "number" },
                                StatusDealing: { type: "number" },
                                StatusSettlement: { type: "number" },
                                OrderStatusDesc: { type: "string" },
                                Notes: { type: "string" },
                                InstrumentPK: { type: "number" },
                                InstrumentID: { type: "string" },
                                InstrumentName: { type: "string" },
                                RangePrice: { type: "string" },
                                OrderPrice: { type: "number" },
                                DonePrice: { type: "number" },
                                DoneLot: { type: "number" },
                                Volume: { type: "number" },
                                DoneAmount: { type: "number" },
                                ApprovedDealingTime: { type: "date" },
                                InvestmentNotes: { type: "string" },

                            }
                        }
                    },
                    group:
                        [
                        {
                            field: "TrxTypeID", aggregates: [
                            { field: "DoneLot", aggregate: "sum" },
                            { field: "DoneAmount", aggregate: "sum" }
                            ]
                        }

                        ],
                    aggregate: [{ field: "DoneLot", aggregate: "sum" },
                            { field: "DoneAmount", aggregate: "sum" }
                    ]
                });
    }

    function getDataSourceInvestmentInstructionBuySell(_url) {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        url: _url,
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 500,
                schema: {
                    model: {
                        fields: {
                            InvestmentPK: { type: "number" },
                            DealingPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            SelectedInvestment: { type: "boolean" },
                            TrxTypeID: { type: "string" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            StatusInvestment: { type: "number" },
                            StatusDealing: { type: "number" },
                            StatusSettlement: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            InstrumentPK: { type: "number" },
                            InstrumentID: { type: "string" },
                            InstrumentName: { type: "string" },
                            RangePrice: { type: "string" },
                            OrderPrice: { type: "number" },
                            Lot: { type: "number" },
                            Volume: { type: "number" },
                            Amount: { type: "number" },
                            InvestmentNotes: { type: "string" },
                            MarketPK: { type: "number" },
                            MarketID: { type: "string" },
                            TrxBuy: { type: "number" },
                            TrxBuyType: { type: "string" },
                            BitForeignTrx: { type: "boolean" },

                        }
                    }
                },
                group:
                    [
                        {
                            field: "OrderStatusDesc", aggregates: [
                                { field: "DoneLot", aggregate: "sum" },
                                { field: "DoneAmount", aggregate: "sum" },
                                { field: "FundID", aggregate: "count" },
                            ]
                        }
                    ],
                aggregate: [{ field: "DoneLot", aggregate: "sum" },
                { field: "DoneAmount", aggregate: "sum" },
                { field: "FundID", aggregate: "count" },
                ]
            });
    }

    function gridInvestmentInstructionDataBound() {
        var grid = $("#gridInvestmentInstruction").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.OrderStatusDesc == "4.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else if (row.OrderStatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApprove");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    $('#BtnAdd').on("click", function () {

        BtnAddClick(0);
    });

    $('#BtnAddSell').on("click", function () {

        BtnAddClick(2);
    });

    $('#BtnAddBuy').on("click", function () {

        BtnAddClick(1);
    });
        
    function BtnAddClick(_bs) {

    $("#LotInShare").val(100);


    if (_GlobClientCode == "09") {

        //Combo Box Instrument 
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CmbInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeInstrumentPK,
                });
            },
            error: function (data) {
                alertify.error(data.responseText);
            }
        });
        function onChangeInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

    }

    //BIRate  
    $.ajax({
        url: window.location.origin + "/Radsoft/BenchmarkIndex/GetBIRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#BIRate").kendoComboBox({
                dataValueField: "BenchmarkIndexPK",
                dataTextField: "CloseInd",
                dataSource: data,
                filter: "contains",
                suggest: true,

                index: 0,
                change: onChangeBIRate

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeBIRate() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }




    //InvestmentStrategy  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentStrategy",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#InvestmentStrategy").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeInvestmentStrategy

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeInvestmentStrategy() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }




    //combo box InvestmentStyle 
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentStyle",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#InvestmentStyle").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeInvestmentStyle
                //value: setCmbInvestmentStyle()
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function onChangeInvestmentStyle() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 6) {
            $("#lblInvestmentStyle").show();
            $("#OtherInvestmentStyle").attr("required", true);
        } else {
            $("#lblInvestmentStyle").hide();
            $("#OtherInvestmentStyle").attr("required", false);
        }
    }




    //InvestmentObjective  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentObjective",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#InvestmentObjective").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeInvestmentObjective
                //value: setCmbInvestmentObjective()

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeInvestmentObjective() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 5) {
            $("#lblInvestmentObjective").show();
            $("#OtherInvestmentObjective").attr("required", true);
        } else {
            $("#lblInvestmentObjective").hide();
            $("#OtherInvestmentObjective").attr("required", false);
        }

    }





    //Revision   
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Revision",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#Revision").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeRevision
                //value: setCmbRevision()

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeRevision() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        if (this.value() == 5) {
            $("#lblRevision").show();
            $("#OtherRevision").attr("required", true);
        } else {
            $("#lblRevision").hide();
            $("#OtherRevision").attr("required", false);
        }

    }

    //InvestmentTrType  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentTrType",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#InvestmentTrType").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                change: onChangeInvestmentTrType

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeInvestmentTrType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    //UpInvestmentTrType  
    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentTrType",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#UpInvestmentTrType").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                dataSource: data,
                index: 0,
                change: onChangeUpInvestmentTrType

            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


    function onChangeUpInvestmentTrType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 1,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            if (_bs == 0) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    index: 0
                });
            } else if (_bs == 1) // 1 = buy
            {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    index: 0
                });
            } else {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    index: 1

                });
            }


        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    function onChangeTrxType() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    $.ajax({
        url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#FundPK").kendoComboBox({
                dataValueField: "FundPK",
                dataTextField: "ID",
                dataSource: data,
                filter: "contains",
                suggest: true,
                change: onChangeFundPK,
                value: $("#FilterFundID").data("kendoComboBox").value()
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    function onChangeFundPK() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    //Combo Box Market 
    $.ajax({
        url: window.location.origin + "/Radsoft/Market/GetMarketCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#MarketPK").kendoComboBox({
                dataValueField: "MarketPK",
                dataTextField: "ID",
                dataSource: data,
                filter: "contains",
                suggest: true,
                index: 0,
                change: onChangeMarketPK,
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    function onChangeMarketPK() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

        //$("#InstrumentPK").val("");

        ////Combo Box Instrument 
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquityByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val(),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#InstrumentPK").kendoComboBox({
        //            dataValueField: "InstrumentPK",
        //            dataTextField: "ID",
        //            dataSource: data,
        //            filter: "contains",
        //            suggest: true,
        //            change: onChangeInstrumentPK,
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //function onChangeInstrumentPK() {

        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }


        //}

    }



    $("#MethodType").kendoComboBox({
        dataValueField: "value",
        dataTextField: "text",
        filter: "contains",
        suggest: true,
        index: 0,
        dataSource: [
            { text: "Share", value: "1" },
            { text: "Amount", value: "2" },
        ],
        change: onChangeMethodType,
    });

    function onChangeMethodType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }



        if ($("#MethodType").val() == 1) {
            ResetLblMethodType();
            $("#LblPrice").show();
            $("#LblLot").show();
            $("#LblLotInShare").show();
            $("#LblVolume").show();

            $("#Lot").attr("required", true);
            $("#OrderPrice").attr("required", true);

            $("#Lot").data("kendoNumericTextBox").value(null);
            $("#OrderPrice").data("kendoNumericTextBox").value(null);
        }
        else {
            ResetLblMethodType();
            $("#Amount").data("kendoNumericTextBox").value("");
            $("#Amount").data("kendoNumericTextBox").enable(true);
            $("#Amount").attr("required", true);
        }
    }


    function ResetLblMethodType() {
        $("#LblPrice").hide();
        $("#LblLot").hide();
        $("#LblLotInShare").hide();
        $("#LblVolume").hide();
        $("#Volume").data("kendoNumericTextBox").value(0);
        $("#Amount").attr("required", false);
        $("#Amount").data("kendoNumericTextBox").enable(false);

        $("#Lot").attr("required", false);
        $("#OrderPrice").attr("required", false);

    }





    $("#Lot").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        change: OnChangeLot
    });




    $("#Volume").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        change: OnChangeVolume
    });

    $("#OrderPrice").kendoNumericTextBox({
        format: "n6",
        decimals: 6,
        change: OnChangeOrderPrice

    });

    $("#Amount").kendoNumericTextBox({
        format: "n2",
        decimals: 2,
        value: 0,
    });

    $("#Amount").data("kendoNumericTextBox").enable(false);

    if (_bs == 1) {
        _resetButtonBuySell();
        $("#BtnSaveBuy").show();
        $("#BtnSaveSell").hide();

    }
    else {
        _resetButtonBuySell();
        $("#BtnSaveSell").show();
        $("#BtnSaveBuy").hide();
    }


    if (_GlobClientCode == "09") {

        $("#trMethodType").hide();
        $("#trMarket").hide();
        $("#trInstrumentList").hide();
        $("#trForeignTax").hide();
        $("#LblRangePrice").hide();
        $("#trInstrumentCombo").show();
        $("#trInvestmentTrType").hide();
        $("#trBIRate").hide();
        $("#trInvestmentStrategy").hide();
        $("#trInvestmentStyle").hide();
        $("#trInvestmentObjective").hide();
        $("#trRevision").hide();

    }
    else if (_GlobClientCode == "26") {

        $("#trMethodType").hide();
        $("#trMarket").hide();
        $("#trInstrumentList").hide();
        $("#trForeignTax").hide();
        $("#LblRangePrice").hide();
        $("#trInstrumentCombo").show();
        $("#trInvestmentTrType").show();
        $("#trBIRate").hide();
        $("#trInvestmentStrategy").hide();
        $("#trInvestmentStyle").hide();
        $("#trInvestmentObjective").hide();
        $("#trRevision").hide();

    }

    else if (_GlobClientCode == "20") {
        $("#trMethodType").hide();
        $("#trMarket").show();
        $("#trInstrumentList").show();
        $("#trForeignTax").show();
        $("#LblRangePrice").show();
        $("#trInstrumentCombo").hide();
        $("#trInvestmentTrType").hide();
        $("#trBIRate").show();
        $("#trInvestmentStrategy").show();
        $("#trInvestmentStyle").show();
        $("#trInvestmentObjective").show();
        $("#trRevision").show();
    }

    else {
        $("#trMethodType").show();
        $("#trMarket").show();
        $("#trInstrumentList").show();
        $("#trForeignTax").show();
        $("#LblRangePrice").show();
        $("#trInstrumentCombo").hide();
        $("#trInvestmentTrType").hide();
        $("#trBIRate").hide();
        $("#trInvestmentStrategy").hide();
        $("#trInvestmentStyle").hide();
        $("#trInvestmentObjective").hide();
        $("#trRevision").hide();
    }

    $('#FindInstrument').change(function () {
        if ($("#TrxType").val() == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/FindInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FindInstrument').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    //$("#InstrumentPK").data("TextBox").value(data.InstrumentPK);
                    $("#InstrumentPK").val(data.InstrumentPK);
                    $("#InstrumentID").val(data.ID);
                    $("#InstrumentTypePK").val(data.InstrumentTypePK);
                }
            });
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/FindInstrumentTypeSell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FindInstrument').val() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    //$("#InstrumentPK").data("TextBox").value(data.InstrumentPK);
                    $("#InstrumentPK").val(data.InstrumentPK);
                    $("#InstrumentID").val(data.ID);
                    $("#InstrumentTypePK").val(data.InstrumentTypePK);
                }
            });
        }
    });


    win.open();

}

    function _resetButtonBuySell() {
        $("#BtnSaveBuy").hide();
        $("#BtnSaveSell").hide();
    }

    function _resetButtonUpdateBuySell() {
        $("#BtnUpdateInvestmentBuy").hide();
        $("#BtnUpdateInvestmentSell").hide();
    }

    function OnChangeLot() {
        $("#Volume").data("kendoNumericTextBox").value($("#Lot").data("kendoNumericTextBox").value() * $("#LotInShare").val());
        RecalculatePriceAndVolume();
    }

    function OnChangeOrderPrice() {
        RecalculatePriceAndVolume();
    }

    function RecalculatePriceAndVolume() {
        if ($("#Volume").data("kendoNumericTextBox").value() != "" && $("#Volume").data("kendoNumericTextBox").value() != null && $("#Volume").data("kendoNumericTextBox").value() != 0 && $("#OrderPrice").data("kendoNumericTextBox").value() != 0) {
            $("#Amount").data("kendoNumericTextBox").value($("#OrderPrice").data("kendoNumericTextBox").value() * $("#Volume").data("kendoNumericTextBox").value())
        }
    }

    function OnChangeVolume() {
        $("#Lot").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() / $("#LotInShare").val());
        RecalculatePriceAndVolume();
    }

    $("#BtnSaveBuy").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;

        var _urlForReference;
        var _InvPKForEmail;
        var _msgSuccess;
        var element = $("#BtnSaveBuy");
        var posY = element.offset();
        var _instrumentPK;
        //$.blockUI({});
        if (_GlobClientCode == "09") {
            _instrumentPK = $('#CmbInstrumentPK').val();
            _instrumentTypePK = 1;
            var val = 1;
        }
        else {
            _instrumentPK = $('#InstrumentPK').val();
            _instrumentTypePK = $('#InstrumentTypePK').val();
            var val = validateData();
        }


        if (val == 1) {
            $.blockUI({});
            if (_ParamFundScheme == 'TRUE') {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolioWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val();
            }
            else {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
            }
            $.ajax({
                url: _urlRef,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        $.unblockUI();
                        alertify.alert("Save Cancel, Already Generate End Day Trails FundPortfolio").moveTo(posY.left, posY.top - 200);
                        return;
                    }
                    else {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Fund/ValidateCheckIssueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Amount').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val() + "/" + $('#ParamDays').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == true) {
                                                $.unblockUI();
                                                alertify.confirm("Cash Not Available, </br> Are You Sure To Continue ?", function (e) {
                                                    if (e) {
                                                        $.blockUI();
                                                        if ($('#OrderPrice').val() > 0) {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val(),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data.Validate == 1) {
                                                                        $.unblockUI();
                                                                        alertify.confirm("Price should be between </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice + "</br></br> Are You Sure Want To Continue Save Data ?", function (e) {
                                                                            if (e) {
                                                                                $.blockUI();
                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                                    type: 'GET',
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                            $.unblockUI();
                                                                                            InitGridInformationExposure();

                                                                                        }


                                                                                        else {
                                                                                            if (_GlobClientCode == "20")
                                                                                                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                                            else
                                                                                                _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                                            $.ajax({
                                                                                                url: _urlForReference,
                                                                                                type: 'GET',
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    var InvestmentInstruction = {
                                                                                                        PeriodPK: _defaultPeriodPK,
                                                                                                        ValueDate: $('#DateFrom').val(),
                                                                                                        Reference: data,
                                                                                                        InstructionDate: $('#DateFrom').val(),
                                                                                                        //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                                        InstrumentPK: _instrumentPK,
                                                                                                        InstrumentTypePK: _instrumentTypePK,
                                                                                                        OrderPrice: $('#OrderPrice').val(),
                                                                                                        RangePrice: $('#RangePrice').val(),
                                                                                                        AcqPrice: 0,
                                                                                                        Lot: $('#Lot').val(),
                                                                                                        LotInShare: $('#LotInShare').val(),
                                                                                                        Volume: $('#Volume').val(),
                                                                                                        Amount: $('#Amount').val(),
                                                                                                        TrxType: $('#TrxType').val(),
                                                                                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                        CounterpartPK: 0,
                                                                                                        FundPK: $('#FundPK').val(),
                                                                                                        MarketPK: $('#MarketPK').val(),
                                                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                        BitHTM: $('#BitHTM').is(":checked"),
                                                                                                        BIRate: $('#BIRate').val(),
                                                                                                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                        InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                        InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                        Revision: $('#Revision').val(),

                                                                                                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                        OtherRevision: $('#OtherRevision').val(),

                                                                                                        EntryUsersID: sessionStorage.getItem("user"),
                                                                                                        InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                    };
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                        type: 'POST',
                                                                                                        data: JSON.stringify(InvestmentInstruction),
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            _msgSuccess = data;
                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                                type: 'GET',
                                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                                success: function (data) {
                                                                                                                    _InvPKForEmail = data;
                                                                                                                    $.ajax({
                                                                                                                        url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
                                                                                                                        type: 'GET',
                                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                                        success: function (data) {


                                                                                                                            if (_ComplianceEmail == 'TRUE') {
                                                                                                                                var HighRisk = {
                                                                                                                                    Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                                                                                    InvestmentPK: _InvPKForEmail,
                                                                                                                                    HighRiskType: 2,
                                                                                                                                };

                                                                                                                                $.ajax({
                                                                                                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                                                                                    type: 'POST',
                                                                                                                                    data: JSON.stringify(HighRisk),
                                                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                                                    success: function (data) {

                                                                                                                                        $.unblockUI();
                                                                                                                                        alertify.alert(_msgSuccess).moveTo(posY.left, posY.top - 200);
                                                                                                                                        win.close();
                                                                                                                                        refresh();
                                                                                                                                        SendNotification();
                                                                                                                                    },
                                                                                                                                    error: function (data) {
                                                                                                                                        $.unblockUI();
                                                                                                                                        alertify.alert(data.responseText);
                                                                                                                                    }
                                                                                                                                });
                                                                                                                            }

                                                                                                                        }
                                                                                                                    });
                                                                                                                },
                                                                                                                error: function (data) {
                                                                                                                    $.unblockUI();
                                                                                                                    alertify.alert(data.responseText);
                                                                                                                }
                                                                                                            });



                                                                                                        },
                                                                                                        error: function (data) {
                                                                                                            $.unblockUI();
                                                                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                                        }
                                                                                                    });
                                                                                                },
                                                                                                error: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                                }


                                                                                            });
                                                                                        }

                                                                                    }
                                                                                });
                                                                            }
                                                                            else
                                                                                return;
                                                                        })
                                                                    }
                                                                    else if (data.Validate == 0) {
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                    $.unblockUI();
                                                                                    InitGridInformationExposure();

                                                                                }


                                                                                else {
                                                                                    if (_GlobClientCode == "20")
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                                    else
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                                    $.ajax({
                                                                                        url: _urlForReference,
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            var InvestmentInstruction = {
                                                                                                PeriodPK: _defaultPeriodPK,
                                                                                                ValueDate: $('#DateFrom').val(),
                                                                                                Reference: data,
                                                                                                InstructionDate: $('#DateFrom').val(),
                                                                                                //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                                InstrumentPK: _instrumentPK,
                                                                                                InstrumentTypePK: _instrumentTypePK,
                                                                                                OrderPrice: $('#OrderPrice').val(),
                                                                                                RangePrice: $('#RangePrice').val(),
                                                                                                AcqPrice: 0,
                                                                                                Lot: $('#Lot').val(),
                                                                                                LotInShare: $('#LotInShare').val(),
                                                                                                Volume: $('#Volume').val(),
                                                                                                Amount: $('#Amount').val(),
                                                                                                TrxType: $('#TrxType').val(),
                                                                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                CounterpartPK: 0,
                                                                                                FundPK: $('#FundPK').val(),
                                                                                                MarketPK: $('#MarketPK').val(),
                                                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                BitHTM: $('#BitHTM').is(":checked"),
                                                                                                BIRate: $('#BIRate').val(),
                                                                                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                Revision: $('#Revision').val(),

                                                                                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                OtherRevision: $('#OtherRevision').val(),

                                                                                                EntryUsersID: sessionStorage.getItem("user"),
                                                                                                InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                            };
                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                type: 'POST',
                                                                                                data: JSON.stringify(InvestmentInstruction),
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                                    win.close();
                                                                                                    refresh();
                                                                                                    SendNotification();
                                                                                                },
                                                                                                error: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                                }
                                                                                            });
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                        }


                                                                                    });
                                                                                }

                                                                            }
                                                                        });
                                                                    }
                                                                    else {
                                                                        $.unblockUI();
                                                                        alertify.alert("Can't Process This Data </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice).moveTo(posY.left, posY.top - 200);
                                                                        return;
                                                                    }



                                                                }
                                                            });
                                                        }

                                                        else {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                        $.unblockUI();
                                                                        InitGridInformationExposure();

                                                                    }
                                                                    else {
                                                                        if (_GlobClientCode == "20")
                                                                            _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                        else
                                                                            _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                        $.ajax({
                                                                            url: _urlForReference,
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                var InvestmentInstruction = {
                                                                                    PeriodPK: _defaultPeriodPK,
                                                                                    ValueDate: $('#DateFrom').val(),
                                                                                    Reference: data,
                                                                                    InstructionDate: $('#DateFrom').val(),
                                                                                    //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                    InstrumentPK: _instrumentPK,
                                                                                    InstrumentTypePK: _instrumentTypePK,
                                                                                    OrderPrice: $('#OrderPrice').val(),
                                                                                    RangePrice: $('#RangePrice').val(),
                                                                                    AcqPrice: 0,
                                                                                    Lot: $('#Lot').val(),
                                                                                    LotInShare: $('#LotInShare').val(),
                                                                                    Volume: $('#Volume').val(),
                                                                                    Amount: $('#Amount').val(),
                                                                                    TrxType: $('#TrxType').val(),
                                                                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                    CounterpartPK: 0,
                                                                                    FundPK: $('#FundPK').val(),
                                                                                    MarketPK: $('#MarketPK').val(),
                                                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                    BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                    BitHTM: $('#BitHTM').is(":checked"),
                                                                                    BIRate: $('#BIRate').val(),
                                                                                    InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                    InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                    InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                    Revision: $('#Revision').val(),

                                                                                    OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                    OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                    OtherRevision: $('#OtherRevision').val(),

                                                                                    EntryUsersID: sessionStorage.getItem("user"),
                                                                                    InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                };
                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                    type: 'POST',
                                                                                    data: JSON.stringify(InvestmentInstruction),
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        $.unblockUI();
                                                                                        alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                        win.close();
                                                                                        refresh();
                                                                                        SendNotification();
                                                                                    },
                                                                                    error: function (data) {
                                                                                        $.unblockUI();
                                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                                    }
                                                                                });
                                                                            },
                                                                            error: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                            }

                                                                        });
                                                                    }

                                                                }
                                                            });
                                                        }

                                                    }
                                                });

                                            } // AVAILABLE CASH
                                            else {
                                                if ($('#OrderPrice').val() > 0) {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val(),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data.Validate == 1) {
                                                                $.unblockUI();
                                                                alertify.confirm("Price should be between </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice + "</br></br> Are You Sure Want To Continue Save Data ?", function (e) {
                                                                    if (e) {
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                    $.unblockUI();
                                                                                    InitGridInformationExposure();

                                                                                }

                                                                                else {
                                                                                    if (_GlobClientCode == "20")
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                                    else
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                                    $.ajax({
                                                                                        url: _urlForReference,
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            var InvestmentInstruction = {
                                                                                                PeriodPK: _defaultPeriodPK,
                                                                                                ValueDate: $('#DateFrom').val(),
                                                                                                Reference: data,
                                                                                                InstructionDate: $('#DateFrom').val(),
                                                                                                //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                                InstrumentPK: _instrumentPK,
                                                                                                InstrumentTypePK: _instrumentTypePK,
                                                                                                OrderPrice: $('#OrderPrice').val(),
                                                                                                RangePrice: $('#RangePrice').val(),
                                                                                                AcqPrice: 0,
                                                                                                Lot: $('#Lot').val(),
                                                                                                LotInShare: $('#LotInShare').val(),
                                                                                                Volume: $('#Volume').val(),
                                                                                                Amount: $('#Amount').val(),
                                                                                                TrxType: $('#TrxType').val(),
                                                                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                CounterpartPK: 0,
                                                                                                FundPK: $('#FundPK').val(),
                                                                                                MarketPK: $('#MarketPK').val(),
                                                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                BitHTM: $('#BitHTM').is(":checked"),
                                                                                                BIRate: $('#BIRate').val(),
                                                                                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                Revision: $('#Revision').val(),

                                                                                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                OtherRevision: $('#OtherRevision').val(),

                                                                                                EntryUsersID: sessionStorage.getItem("user"),
                                                                                                InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                            };
                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                type: 'POST',
                                                                                                data: JSON.stringify(InvestmentInstruction),
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    _msgSuccess = data;
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                        type: 'GET',
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            _InvPKForEmail = data;
                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
                                                                                                                type: 'GET',
                                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                                success: function (data) {

                                                                                                                    if (_ComplianceEmail == 'TRUE') {
                                                                                                                        var HighRisk = {
                                                                                                                            Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                                                                            InvestmentPK: _InvPKForEmail,
                                                                                                                            HighRiskType: 2,
                                                                                                                        };

                                                                                                                        $.ajax({
                                                                                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                                                                            type: 'POST',
                                                                                                                            data: JSON.stringify(HighRisk),
                                                                                                                            contentType: "application/json;charset=utf-8",
                                                                                                                            success: function (data) {
                                                                                                                                $.unblockUI();
                                                                                                                                alertify.alert(_msgSuccess).moveTo(posY.left, posY.top - 200);
                                                                                                                                win.close();
                                                                                                                                refresh();
                                                                                                                                SendNotification();
                                                                                                                            },
                                                                                                                            error: function (data) {
                                                                                                                                $.unblockUI();
                                                                                                                                alertify.alert(data.responseText);
                                                                                                                            }
                                                                                                                        });
                                                                                                                    }



                                                                                                                }
                                                                                                            });
                                                                                                        },
                                                                                                        error: function (data) {
                                                                                                            $.unblockUI();
                                                                                                            alertify.alert(data.responseText);
                                                                                                        }
                                                                                                    });

                                                                                                },
                                                                                                error: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                                }
                                                                                            });
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                        }


                                                                                    });
                                                                                }

                                                                            }
                                                                        });
                                                                    }
                                                                    else
                                                                        return;
                                                                })
                                                            }
                                                            else if (data.Validate == 0) {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                            $.unblockUI();
                                                                            InitGridInformationExposure();

                                                                        }

                                                                        else {
                                                                            if (_GlobClientCode == "20")
                                                                                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                            else
                                                                                _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                            $.ajax({
                                                                                url: _urlForReference,
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    var InvestmentInstruction = {
                                                                                        PeriodPK: _defaultPeriodPK,
                                                                                        ValueDate: $('#DateFrom').val(),
                                                                                        Reference: data,
                                                                                        InstructionDate: $('#DateFrom').val(),
                                                                                        //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                        InstrumentPK: _instrumentPK,
                                                                                        InstrumentTypePK: _instrumentTypePK,
                                                                                        OrderPrice: $('#OrderPrice').val(),
                                                                                        RangePrice: $('#RangePrice').val(),
                                                                                        AcqPrice: 0,
                                                                                        Lot: $('#Lot').val(),
                                                                                        LotInShare: $('#LotInShare').val(),
                                                                                        Volume: $('#Volume').val(),
                                                                                        Amount: $('#Amount').val(),
                                                                                        TrxType: $('#TrxType').val(),
                                                                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                        CounterpartPK: 0,
                                                                                        FundPK: $('#FundPK').val(),
                                                                                        MarketPK: $('#MarketPK').val(),
                                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                        BitHTM: $('#BitHTM').is(":checked"),
                                                                                        BIRate: $('#BIRate').val(),
                                                                                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                        InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                        InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                        Revision: $('#Revision').val(),

                                                                                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                        OtherRevision: $('#OtherRevision').val(),

                                                                                        EntryUsersID: sessionStorage.getItem("user"),
                                                                                        InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                    };
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                        type: 'POST',
                                                                                        data: JSON.stringify(InvestmentInstruction),
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                            win.close();
                                                                                            refresh();
                                                                                            SendNotification();
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                        }
                                                                                    });
                                                                                },
                                                                                error: function (data) {
                                                                                    $.unblockUI();
                                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                                                                                }


                                                                            });
                                                                        }

                                                                    }
                                                                });
                                                            }
                                                            else {
                                                                $.unblockUI();
                                                                alertify.alert("Can't Process This Data </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice).moveTo(posY.left, posY.top - 200);
                                                                return;
                                                            }


                                                        }
                                                    });
                                                }
                                                else {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                $.unblockUI();
                                                                InitGridInformationExposure();

                                                            }

                                                            else {
                                                                if (_GlobClientCode == "20")
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                else
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                $.ajax({
                                                                    url: _urlForReference,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        var InvestmentInstruction = {
                                                                            PeriodPK: _defaultPeriodPK,
                                                                            ValueDate: $('#DateFrom').val(),
                                                                            Reference: data,
                                                                            InstructionDate: $('#DateFrom').val(),
                                                                            //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                            InstrumentPK: _instrumentPK,
                                                                            InstrumentTypePK: _instrumentTypePK,
                                                                            OrderPrice: $('#OrderPrice').val(),
                                                                            RangePrice: $('#RangePrice').val(),
                                                                            AcqPrice: 0,
                                                                            Lot: $('#Lot').val(),
                                                                            LotInShare: $('#LotInShare').val(),
                                                                            Volume: $('#Volume').val(),
                                                                            Amount: $('#Amount').val(),
                                                                            TrxType: $('#TrxType').val(),
                                                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                            CounterpartPK: 0,
                                                                            FundPK: $('#FundPK').val(),
                                                                            MarketPK: $('#MarketPK').val(),
                                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                                            BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                            BitHTM: $('#BitHTM').is(":checked"),
                                                                            BIRate: $('#BIRate').val(),
                                                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                                                            Revision: $('#Revision').val(),

                                                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                            OtherRevision: $('#OtherRevision').val(),

                                                                            EntryUsersID: sessionStorage.getItem("user"),
                                                                            InvestmentTrType: $('#InvestmentTrType').val(),
                                                                        };
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                            type: 'POST',
                                                                            data: JSON.stringify(InvestmentInstruction),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                win.close();
                                                                                refresh();
                                                                                SendNotification();
                                                                            },
                                                                            error: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                            }
                                                                        });
                                                                    },
                                                                    error: function (data) {
                                                                        $.unblockUI();
                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                    }

                                                                });
                                                            }

                                                        }
                                                    });
                                                }

                                            }
                                            $.unblockUI();
                                        }

                                    });

                                }
                                else {
                                    $.unblockUI();
                                    alertify.alert("can't save data, please check Issueance Date");
                                }
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
                            }


                        });
                    }
                    //$.unblockUI();
                }

            });
        }
    });

    $("#BtnSaveSell").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var element = $("#BtnSaveSell");
        var posY = element.offset();
        var _OrderPriceForRangePriceExposure;
        var _FundPKForRangePriceExposure;
        _OrderPriceForRangePriceExposure = $('#OrderPrice').val();
        _FundPKForRangePriceExposure = $('#FundPK').val();

        var _urlForReference;
        var _InvPKForEmail;
        var _msgSuccess;

        if (_GlobClientCode == "09") {
            _instrumentPK = $('#CmbInstrumentPK').val();
            _instrumentTypePK = 1;
            var val = 1;
        }
        else {
            _instrumentPK = $('#InstrumentPK').val();
            _instrumentTypePK = $('#InstrumentTypePK').val();
            var val = validateData();
        }

        if (val == 1) {
            $.blockUI({});
            if (_ParamFundScheme == 'TRUE') {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolioWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val();
            } else {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
            }
            $.ajax({
                url: _urlRef,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        $.unblockUI();
                        alertify.alert("Save Cancel, Already Generate End Day Trails FundPortfolio").moveTo(posY.left, posY.top - 200);
                        return;
                    }
                    else {
                        if ($('#OrderPrice').val() > 0) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data.Validate == 1) {
                                        $.unblockUI();
                                        alertify.confirm("Price should be between </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice + "</br></br> Are You Sure Want To Continue Save Data ?", function (e) {
                                            if (e) {

                                                var Validate = {
                                                    ValueDate: $('#DateFrom').val(),
                                                    FundPK: $('#FundPK').val(),
                                                    InstrumentPK: _instrumentPK,
                                                    Volume: $('#Volume').val(),
                                                    TrxBuy: $('#TrxBuy').val(),
                                                    TrxBuyType: $('#TrxBuyType').val(),
                                                    Amount: $('#Amount').val(),
                                                    MethodType: $('#MethodType').val(),
                                                    BitHTM: $('#BitHTM').is(":checked"),
                                                    BIRate: $('#BIRate').val(),
                                                    InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                    InvestmentStyle: $('#InvestmentStyle').val(),
                                                    InvestmentObjective: $('#InvestmentObjective').val(),
                                                    Revision: $('#Revision').val(),

                                                    OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                    OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                    OtherRevision: $('#OtherRevision').val(),

                                                };
                                                $.blockUI({});
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(Validate),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if (data == true) {
                                                            $.unblockUI();
                                                            alertify.alert("Short Sell").moveTo(posY.left, posY.top - 200);
                                                            return;
                                                        }
                                                        else {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data.AlertExposure == 3 || data.AlertExposure == 4 || data.AlertExposure == 7 || data.AlertExposure == 8) {
                                                                        $.unblockUI();
                                                                        InitGridInformationExposure();

                                                                    }

                                                                    else {
                                                                        if (_GlobClientCode == "20")
                                                                            _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                        else
                                                                            _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

                                                                        $.ajax({
                                                                            url: _urlForReference,
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                var InvestmentInstruction = {
                                                                                    PeriodPK: _defaultPeriodPK,
                                                                                    ValueDate: $('#DateFrom').val(),
                                                                                    Reference: data,
                                                                                    InstructionDate: $('#DateFrom').val(),
                                                                                    //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                                    InstrumentPK: _instrumentPK,
                                                                                    InstrumentTypePK: _instrumentTypePK,
                                                                                    OrderPrice: $('#OrderPrice').val(),
                                                                                    RangePrice: $('#RangePrice').val(),
                                                                                    AcqPrice: 0,
                                                                                    Lot: $('#Lot').val(),
                                                                                    LotInShare: $('#LotInShare').val(),
                                                                                    Volume: $('#Volume').val(),
                                                                                    Amount: $('#Amount').val(),
                                                                                    TrxType: $('#TrxType').val(),
                                                                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                    CounterpartPK: 0,
                                                                                    FundPK: $('#FundPK').val(),
                                                                                    MarketPK: $('#MarketPK').val(),
                                                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                    BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                    BitHTM: $('#BitHTM').is(":checked"),
                                                                                    BIRate: $('#BIRate').val(),
                                                                                    InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                    InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                    InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                    Revision: $('#Revision').val(),

                                                                                    OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                    OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                    OtherRevision: $('#OtherRevision').val(),
                                                                                    EntryUsersID: sessionStorage.getItem("user"),
                                                                                    InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                };
                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                    type: 'POST',
                                                                                    data: JSON.stringify(InvestmentInstruction),
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        _msgSuccess = data;
                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                            type: 'GET',
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                _InvPKForEmail = data;

                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + _FundPKForRangePriceExposure + "/" + _OrderPriceForRangePriceExposure + "/" + _InvPKForEmail,
                                                                                                    type: 'GET',
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {

                                                                                                        if (_ComplianceEmail == 'TRUE') {
                                                                                                            var HighRisk = {
                                                                                                                Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                                                                InvestmentPK: _InvPKForEmail,
                                                                                                                HighRiskType: 2,
                                                                                                            };

                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                                                                type: 'POST',
                                                                                                                data: JSON.stringify(HighRisk),
                                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                                success: function (data) {
                                                                                                                    $.unblockUI();
                                                                                                                    alertify.alert(_msgSuccess).moveTo(posY.left, posY.top - 200);
                                                                                                                    win.close();
                                                                                                                    refresh();
                                                                                                                    SendNotification();
                                                                                                                },
                                                                                                                error: function (data) {
                                                                                                                    $.unblockUI();
                                                                                                                    alertify.alert(data.responseText);
                                                                                                                }
                                                                                                            });
                                                                                                        }


                                                                                                    }
                                                                                                });
                                                                                            },
                                                                                            error: function (data) {
                                                                                                $.unblockUI();
                                                                                                alertify.alert(data.responseText);
                                                                                            }
                                                                                        });
                                                                                        $.unblockUI();
                                                                                        alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                        win.close();
                                                                                        refresh();
                                                                                        SendNotification();
                                                                                    },
                                                                                    error: function (data) {
                                                                                        $.unblockUI();
                                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                                    }
                                                                                });
                                                                            },
                                                                            error: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                            }

                                                                        });

                                                                    }
                                                                    $.unblockUI();

                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                }

                                                            });
                                                        }
                                                    }
                                                });
                                            }
                                            else
                                                return;
                                        })
                                    }
                                    else if (data.Validate == 0) {
                                        var Validate = {
                                            ValueDate: $('#DateFrom').val(),
                                            FundPK: $('#FundPK').val(),
                                            InstrumentPK: _instrumentPK,
                                            Volume: $('#Volume').val(),
                                            TrxBuy: $('#TrxBuy').val(),
                                            TrxBuyType: $('#TrxBuyType').val(),
                                            Amount: $('#Amount').val(),
                                            MethodType: $('#MethodType').val(),
                                            BitHTM: $('#BitHTM').is(":checked"),
                                            BIRate: $('#BIRate').val(),
                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                            Revision: $('#Revision').val(),

                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                            OtherRevision: $('#OtherRevision').val(),

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'POST',
                                            data: JSON.stringify(Validate),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == true) {
                                                    $.unblockUI();
                                                    alertify.alert("Short Sell").moveTo(posY.left, posY.top - 200);
                                                    return;
                                                }
                                                else {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data.AlertExposure == 3 || data.AlertExposure == 4 || data.AlertExposure == 7 || data.AlertExposure == 8) {
                                                                $.unblockUI();
                                                                InitGridInformationExposure();

                                                            }

                                                            else {
                                                                if (_GlobClientCode == "20")
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                else
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

                                                                $.ajax({
                                                                    url: _urlForReference,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        var InvestmentInstruction = {
                                                                            PeriodPK: _defaultPeriodPK,
                                                                            ValueDate: $('#DateFrom').val(),
                                                                            Reference: data,
                                                                            InstructionDate: $('#DateFrom').val(),
                                                                            //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                            InstrumentPK: _instrumentPK,
                                                                            InstrumentTypePK: _instrumentTypePK,
                                                                            OrderPrice: $('#OrderPrice').val(),
                                                                            RangePrice: $('#RangePrice').val(),
                                                                            AcqPrice: 0,
                                                                            Lot: $('#Lot').val(),
                                                                            LotInShare: $('#LotInShare').val(),
                                                                            Volume: $('#Volume').val(),
                                                                            Amount: $('#Amount').val(),
                                                                            TrxType: $('#TrxType').val(),
                                                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                            CounterpartPK: 0,
                                                                            FundPK: $('#FundPK').val(),
                                                                            MarketPK: $('#MarketPK').val(),
                                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                                            BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                            BitHTM: $('#BitHTM').is(":checked"),
                                                                            BIRate: $('#BIRate').val(),
                                                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                                                            Revision: $('#Revision').val(),

                                                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                            OtherRevision: $('#OtherRevision').val(),
                                                                            EntryUsersID: sessionStorage.getItem("user"),
                                                                            InvestmentTrType: $('#InvestmentTrType').val(),
                                                                        };
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                            type: 'POST',
                                                                            data: JSON.stringify(InvestmentInstruction),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {

                                                                                $.unblockUI();
                                                                                alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                                win.close();
                                                                                refresh();
                                                                                SendNotification();
                                                                            },
                                                                            error: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                            }
                                                                        });
                                                                    },
                                                                    error: function (data) {
                                                                        $.unblockUI();
                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                    }

                                                                });

                                                            }
                                                            $.unblockUI();

                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                        }

                                                    });
                                                }
                                            }
                                        });
                                    }
                                    else {
                                        $.unblockUI();
                                        alertify.alert("Can't Process This Data </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice).moveTo(posY.left, posY.top - 200);
                                        return;
                                    }

                                }
                            });
                        }
                        else {
                            var Validate = {
                                ValueDate: $('#DateFrom').val(),
                                FundPK: $('#FundPK').val(),
                                InstrumentPK: _instrumentPK,
                                Volume: $('#Volume').val(),
                                TrxBuy: $('#TrxBuy').val(),
                                TrxBuyType: $('#TrxBuyType').val(),
                                Amount: $('#Amount').val(),
                                MethodType: $('#MethodType').val(),
                                BitHTM: $('#BitHTM').is(":checked"),
                                BIRate: $('#BIRate').val(),
                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                InvestmentStyle: $('#InvestmentStyle').val(),
                                InvestmentObjective: $('#InvestmentObjective').val(),
                                Revision: $('#Revision').val(),

                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                OtherRevision: $('#OtherRevision').val(),

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(Validate),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == true) {
                                        $.unblockUI();
                                        alertify.alert("Short Sell").moveTo(posY.left, posY.top - 200);
                                        return;
                                    }
                                    else {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/1",
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data.AlertExposure == 3 || data.AlertExposure == 4 || data.AlertExposure == 7 || data.AlertExposure == 8) {
                                                    $.unblockUI();
                                                    InitGridInformationExposure();

                                                }

                                                else {
                                                    if (_GlobClientCode == "20")
                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
                                                    else
                                                        _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

                                                    $.ajax({
                                                        url: _urlForReference,
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            var InvestmentInstruction = {
                                                                PeriodPK: _defaultPeriodPK,
                                                                ValueDate: $('#DateFrom').val(),
                                                                Reference: data,
                                                                InstructionDate: $('#DateFrom').val(),
                                                                //InstrumentPK: $(htmlInstrumentPK).val(),
                                                                InstrumentPK: _instrumentPK,
                                                                InstrumentTypePK: _instrumentTypePK,
                                                                OrderPrice: $('#OrderPrice').val(),
                                                                RangePrice: $('#RangePrice').val(),
                                                                AcqPrice: 0,
                                                                Lot: $('#Lot').val(),
                                                                LotInShare: $('#LotInShare').val(),
                                                                Volume: $('#Volume').val(),
                                                                Amount: $('#Amount').val(),
                                                                TrxType: $('#TrxType').val(),
                                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                CounterpartPK: 0,
                                                                FundPK: $('#FundPK').val(),
                                                                MarketPK: $('#MarketPK').val(),
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                BitHTM: $('#BitHTM').is(":checked"),
                                                                BIRate: $('#BIRate').val(),
                                                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                InvestmentStyle: $('#InvestmentStyle').val(),
                                                                InvestmentObjective: $('#InvestmentObjective').val(),
                                                                Revision: $('#Revision').val(),

                                                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                OtherRevision: $('#OtherRevision').val(),
                                                                EntryUsersID: sessionStorage.getItem("user"),
                                                                InvestmentTrType: $('#InvestmentTrType').val(),
                                                            };
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                type: 'POST',
                                                                data: JSON.stringify(InvestmentInstruction),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data).moveTo(posY.left, posY.top - 200);
                                                                    win.close();
                                                                    refresh();
                                                                    SendNotification();
                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                }
                                                            });
                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                        }

                                                    });
                                                }
                                            },
                                            error: function (data) {
                                                $.unblockUI();
                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                            }

                                        });

                                    }
                                    $.unblockUI();

                                }
                            });
                        }
                    }
                    //$.unblockUI();
                }

            });
        }
    });

    $("#BtnCancel").click(function () {
            var element = $("#BtnCancel");
            var posY = element.offset();
            alertify.confirm("Are you sure want to close detail?", function (e) {
                if (e) {
                    win.close();
                    alertify.alert("Close Detail").moveTo(posY.left, posY.top - 200);
                }
            }).moveTo(posY.left, posY.top - 200);
        });

    function getDataSourceListAvailableCash(_url) {
            return new kendo.data.DataSource(
                        {
                            transport:
                                    {

                                        read:
                                            {

                                                url: _url,
                                                dataType: "json"
                                            }
                                    },
                            batch: true,
                            cache: false,
                            error: function (e) {
                                WinListAvailableCashDetail.close();
                                alertify.alert("No Data");
                            },
                            pageSize: 100,
                            schema: {
                                model: {
                                    fields: {
                                        FSource: { type: "string" },
                                        T0: { type: "number" },
                                        T1: { type: "number" },
                                        T2: { type: "number" },
                                        T3: { type: "number" },
                                        T4: { type: "number" },
                                        T5: { type: "number" },
                                        T6: { type: "number" },
                                        T7: { type: "number" },
                                    }
                                }
                            },
                            group: {
                                field: "Flag", aggregates: [
                                    { field: "T0", aggregate: "sum" },
                                    { field: "T1", aggregate: "sum" },
                                    { field: "T2", aggregate: "sum" },
                                    { field: "T3", aggregate: "sum" },
                                    { field: "T4", aggregate: "sum" },
                                    { field: "T5", aggregate: "sum" },
                                    { field: "T6", aggregate: "sum" },
                                    { field: "T7", aggregate: "sum" },
                                ]
                            },
                            aggregate: [
                            { field: "T0", aggregate: "sum" },
                            { field: "T1", aggregate: "sum" },
                            { field: "T2", aggregate: "sum" },
                            { field: "T3", aggregate: "sum" },
                            { field: "T4", aggregate: "sum" },
                            { field: "T5", aggregate: "sum" },
                            { field: "T6", aggregate: "sum" },
                            { field: "T7", aggregate: "sum" },
                            ],
                        });
        }

    $("#NetCashAvailable").on("click", function () {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }

        if ($("#ParamDays").val() == "") {
            _paramDays = "0";
        }
        else {
            _paramDays = $("#ParamDays").val();
        }

        var _url = window.location.origin + "/Radsoft/OMSEquity/OMSEquityGetNetAvailableCashDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramDays;
        var dsListAvailableCash = getDataSourceListAvailableCash(_url);

        $("#gridListAvailableCashDetail").kendoGrid({
            dataSource: dsListAvailableCash,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            toolbar: ["excel"],
            dataBound: gridListAvailableCashDetailDataBound,
            columns: [
                { field: "FSource", title: "Source", width: 200 },
                { field: "T0", title: "T-0", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T1", title: "T-1", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T2", title: "T-2", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T3", title: "T-3", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T4", title: "T-4", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T5", title: "T-5", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T6", title: "T-6", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "T7", title: "T-7", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },
            ]
        });





        WinListAvailableCashDetail.open();
    });


    function gridListAvailableCashDetailDataBound() {
            var grid = $("#gridListAvailableCashDetail").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {

                if (row.Flag == "T6") {
                    grid.hideColumn(8);
                }
                else if (row.Flag == "T5") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                }
                else if (row.Flag == "T4") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                    grid.hideColumn(6);
                }
                else if (row.Flag == "T3") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                    grid.hideColumn(6);
                    grid.hideColumn(5);
                }
                else if (row.Flag == "T2") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                    grid.hideColumn(6);
                    grid.hideColumn(5);
                    grid.hideColumn(4);
                }
                else if (row.Flag == "T1") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                    grid.hideColumn(6);
                    grid.hideColumn(5);
                    grid.hideColumn(4);
                    grid.hideColumn(3);
                }
                else if (row.Flag == "T0") {
                    grid.hideColumn(8);
                    grid.hideColumn(7);
                    grid.hideColumn(6);
                    grid.hideColumn(5);
                    grid.hideColumn(4);
                    grid.hideColumn(3);
                    grid.hideColumn(2);
                }
            });
        }


    function gridInvestmentInstructionBuyOnlyDataBound() {
            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {


                checkedInvestmentBuy[0] = null;

                if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                    checkedInvestmentBuy[row.InvestmentPK] = null;
                }

                if (checkedInvestmentBuy[row.InvestmentPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                    $('tr[data-uid="' + row.uid + '"] ')
                        .addClass("k-state-selected")
                        .find(".checkboxBuy")
                        .attr("checked", "checked");
                }
                

                if (row.OrderStatusDesc == "4.MATCH") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
                } else if (row.OrderStatusDesc == "3.OPEN") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
                } else if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
                } else if (row.OrderStatusDesc == "5.PARTIAL") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
                } else if (row.OrderStatusDesc == "2.APPROVED") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApprove");
                } else {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
                }
            });
        }

    function gridInvestmentInstructionSellOnlyDataBound() {
            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {

                checkedInvestmentSell[0] = null;

                if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                    checkedInvestmentSell[row.InvestmentPK] = null;
                }

                if (checkedInvestmentSell[row.InvestmentPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                    $('tr[data-uid="' + row.uid + '"] ')
                        .addClass("k-state-selected")
                        .find(".checkboxSell")
                        .attr("checked", "checked");
                }

                if (row.OrderStatusDesc == "4.MATCH") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
                } else if (row.OrderStatusDesc == "3.OPEN") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
                } else if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
                } else if (row.OrderStatusDesc == "5.PARTIAL") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
                } else if (row.OrderStatusDesc == "2.APPROVED") {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApprove");
                } else {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
                }
            });
        }

    $("#BtnApproveBySelectedEquityBuy").click(function (e) {

            var All = 0;
            All = [];
            for (var i in checkedInvestmentBuy) {
                if (checkedInvestmentBuy[i]) {
                    All.push(i);
                }
            }

            var ArrayInvestmentFrom = All;
            var stringInvestmentFrom = '';

            for (var i in ArrayInvestmentFrom) {
                stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

            }
            stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

            //console.log(stringInvestmentFrom);

            if (stringInvestmentFrom == "") {
                alertify.alert("There's No Selected Data");
            }
            else {
                alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                    if (e) {

                        var ValidateInvestment = {
                            InstrumentTypePK: 1,
                            TrxType: 1,
                            FundID: $("#FilterFundID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateFrom').val(),
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/ValidateCheckExposurePreTrade/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateInvestment),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == "") {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateApproveBySelectedDataOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(ValidateInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == 2) {
                                                alertify.alert("Data Already Approved");
                                                $.unblockUI();
                                                return;
                                            }
                                            else if (data == 3) {
                                                alertify.alert("Data Already Reject");
                                                $.unblockUI();
                                                return;
                                            }
                                            else if (data == 4) {
                                                alertify.alert("Data Already OPEN, Please Contact Dealer");
                                                $.unblockUI();
                                                return;
                                            }
                                            else if (data == 5) {
                                                alertify.alert("Data Already MATCH, Please Contact Dealer");
                                                $.unblockUI();
                                                return;
                                            }
                                            else if (data == 6) {
                                                alertify.alert("Data Already PARTIAL, Please Contact Dealer");
                                                $.unblockUI();
                                                return;
                                            }
                                            else {
                                                var Investment = {
                                                    InstrumentTypePK: 1,
                                                    TrxType: 1,
                                                    FundID: $("#FilterFundID").val(),
                                                    DateFrom: $('#DateFrom').val(),
                                                    DateTo: $('#DateFrom').val(),
                                                    ApprovedUsersID: sessionStorage.getItem("user"),
                                                    stringInvestmentFrom: stringInvestmentFrom,

                                                };

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/OMSEquity/ApproveOMSEquityBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        refresh();
                                                        if (data == "") {
                                                            alertify.alert("Approved Investment Success");
                                                        }
                                                        else {
                                                            alertify.alert(data);
                                                        }

                                                        SendNotification();
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }

                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });

                                }
                                else {
                                    alertify.alert(data);

                                }


                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }
                        });
                    }
                });
            }


        });

    $("#BtnRejectBySelectedEquityBuy").click(function (e) {

            var All = 0;
            All = [];
            for (var i in checkedInvestmentBuy) {
                if (checkedInvestmentBuy[i]) {
                    All.push(i);
                }
            }

            var ArrayInvestmentFrom = All;
            var stringInvestmentFrom = '';

            for (var i in ArrayInvestmentFrom) {
                stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

            }
            stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

            //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateInvestment = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateRejectBySelectedDataOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateInvestment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Already OPEN, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Already MATCH, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Investment = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSEquity/RejectOMSEquityBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Reject Investment Success");

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
            });
        }
            
            
        });

    $("#BtnApproveBySelectedEquitySell").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedInvestmentSell) {
            if (checkedInvestmentSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateInvestment = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/ValidateCheckExposurePreTrade/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateInvestment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSEquity/ValidateApproveBySelectedDataOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(ValidateInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == 2) {
                                            alertify.alert("Data Already Approved");
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 3) {
                                            alertify.alert("Data Already Reject");
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 4) {
                                            alertify.alert("Data Already OPEN, Please Contact Dealer");
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 5) {
                                            alertify.alert("Data Already MATCH, Please Contact Dealer");
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 6) {
                                            alertify.alert("Data Already PARTIAL, Please Contact Dealer");
                                            $.unblockUI();
                                            return;
                                        }
                                        else {
                                            var Investment = {
                                                InstrumentTypePK: 1,
                                                TrxType: 2,
                                                FundID: $("#FilterFundID").val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateFrom').val(),
                                                ApprovedUsersID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/OMSEquity/ApproveOMSEquityBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(Investment),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    refresh();
                                                    if (data == "") {
                                                        alertify.alert("Approved Investment Success");
                                                    }
                                                    else {
                                                        alertify.alert(data);
                                                    }
                                                    SendNotification();

                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                }
                                            });
                                        }

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });
                            }
                            else {
                                alertify.alert(data);

                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });


                }
            });
        }


    });

    $("#BtnRejectBySelectedEquitySell").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedInvestmentSell) {
            if (checkedInvestmentSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        //console.log(stringInvestmentFrom);
        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateInvestment = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateRejectBySelectedDataOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateInvestment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Already OPEN, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Already MATCH, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL, Please Contact Dealer");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Investment = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSEquity/RejectOMSEquityBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Reject Investment Success");
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
            });
        }
            
        });

    function GenerateNavProjection() {

            
        var _date = kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                //if (data == false) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSEquity/GenerateNavProjection/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date + "/" + _fundID,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data != null) {
                            if (data.Nav != 0 || data.Nav != null) {
                                $('#NavProjection').val(data.Nav);
                                $('#NavProjection').text(kendo.toString(data.Nav, "n"));
                                $('#ComparePercent').val(data.Compare);
                                $('#ComparePercent').text(kendo.toString(data.Compare / 100, "p"));
                            }
                            else {
                                $('#NavProjection').val(0);
                                $('#NavProjection').text(kendo.toString(0, "n"));
                                $('#ComparePercent').val(0);
                                $('#ComparePercent').text(kendo.toString(0, "p"));
                            }
                        }

                    }
                });


                //} else {
                //    alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                //}
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    }

    function ResetSelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/ResetSelectedInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }

    if (_GlobClientCode != "09") {

        $("#btnListInstrumentPK").click(function () {
            initListInstrumentPK();

            WinListInstrument.center();
            WinListInstrument.open();
            htmlInstrumentPK = "#InstrumentPK";
            htmlInstrumentID = "#InstrumentID";


        });

        $("#btnUpListInstrumentPK").click(function () {
            initUpListInstrumentPK();

            WinUpListInstrument.center();
            WinUpListInstrument.open();
            UphtmlInstrumentPK = "#InstrumentPK";
            UphtmlInstrumentID = "#InstrumentID";


        });

        function getDataSourceListInstrument(_url) {
            return new kendo.data.DataSource(
                        {
                            transport:
                                    {

                                        read:
                                            {

                                                url: _url,
                                                dataType: "json"
                                            }
                                    },
                            batch: true,
                            cache: false,
                            error: function (e) {
                                WinListInstrument.close();
                                alertify.alert("No Data");
                            },
                            pageSize: 100,
                            schema: {
                                model: {
                                    fields: {
                                        InstrumentPK: { type: "Number" },
                                        ID: { type: "string" },
                                        InterestPercent: { type: "number" },
                                        MaturityDate: { type: "date" },
                                        AcqDate: { type: "date" },
                                        Balance: { type: "number" },
                                        TrxBuy: { type: "number" },
                                        TrxBuyType: { type: "string" },
                                    }
                                }

                            },
                        });
        }

        function initListInstrumentPK() {

            var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquityByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").data("kendoComboBox").value() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val();
            var dsListInstrument = getDataSourceListInstrument(_url);
            if ($("#TrxType").data("kendoComboBox").value() == 1) {
                $("#gridListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },

                    ]
                });
            }
            else {
                $("#gridListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },
                        { field: "Balance", title: "Volume", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },

                    ]
                });
            }


        }

        function ListInstrumentSelect(e) {
            if (e.handled !== true) // This will prevent event triggering more then once
            {
                var grid = $("#gridListInstrument").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                $("#InstrumentPK").val(dataItemX.InstrumentPK);
                $("#InstrumentTypePK").val(dataItemX.InstrumentTypePK);
                $("#InstrumentID").val(dataItemX.ID);
                $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#Lot").data("kendoNumericTextBox").value(dataItemX.Balance / 100);
                $("#TrxBuy").val(dataItemX.TrxBuy);
                $("#TrxBuyType").val(dataItemX.TrxBuyType);

                WinListInstrument.close();

            }
            e.handled = true;
        }

        $("#btnClearListInstrument").click(function () {
            $("#InstrumentPK").val("");
            $("#InstrumentTypePK").val("");
            $("#InstrumentID").val("");
            $("#Lot").data("kendoNumericTextBox").value(0);
            $("#Volume").data("kendoNumericTextBox").value(0);
            $("#Amount").data("kendoNumericTextBox").value(0);
            $("#OrderPrice").data("kendoNumericTextBox").value(0);
            $("#TrxBuy").val("");
            $("#TrxBuyType").val("");
        });




        function initUpListInstrumentPK() {

            var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSEquityByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpTrxType").data("kendoComboBox").value() + "/" + $("#UpFundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpMarketPK").val();
            var dsListInstrument = getDataSourceListInstrument(_url);
            if ($("#UpTrxType").data("kendoComboBox").value() == 1) {
                $("#gridUpListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                        { command: { text: "Select", click: UpListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },

                    ]
                });
            }
            else {
                $("#gridUpListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                        { command: { text: "Select", click: UpListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },
                        { field: "Balance", title: "Volume", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },

                    ]
                });
            }


        }

        function UpListInstrumentSelect(e) {
            if (e.handled !== true) // This will prevent event triggering more then once
            {
                var grid = $("#gridUpListInstrument").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
                $("#UpInstrumentTypePK").val(dataItemX.InstrumentTypePK);
                $("#UpInstrumentID").val(dataItemX.ID);
                $("#UpVolume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#UpLot").data("kendoNumericTextBox").value(dataItemX.Balance / 100);
                $("#UpTrxBuy").val(dataItemX.TrxBuy);
                $("#UpTrxBuyType").val(dataItemX.TrxBuyType);

                WinUpListInstrument.close();

            }
            e.handled = true;
        }

        $("#btnClearUpListInstrument").click(function () {
            $("#UpInstrumentPK").val("");
            $("#UpInstrumentTypePK").val("");
            $("#UpInstrumentID").val("");
            $("#UpLot").data("kendoNumericTextBox").value(0);
            $("#UpVolume").data("kendoNumericTextBox").value(0);
            $("#UpAmount").data("kendoNumericTextBox").value(0);
            $("#UpTrxBuy").val("");
            $("#UpTrxBuyType").val("");
            $("#UpOrderPrice").data("kendoNumericTextBox").value(0);
        });
    }

    $("#BtnImportOmsEquity").click(function () {
        document.getElementById("FileImportOmsEquity").click();
    });

    $("#FileImportOmsEquity").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportOmsEquity").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ImportOmsEquityTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ImportOmsEquity_Import/ " + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {

                    console.log(data);
                    if (data == "Import OMS Equity Success") {
                        //cek ada exposure atau gak
                        $.ajax({
                            url: window.location.origin + "/Radsoft/OMSEquity/CheckExposureFromImport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 1) {
                                    $.unblockUI();
                                    console.log("ada Exposure");
                                    InitGridExposureImport();
                                }
                                else {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/InsertIntoInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $.unblockUI();
                                            $("#FileImportOmsEquity").val("");
                                            refresh();
                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                    else {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportOmsEquity").val("");
                    }


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOmsEquity").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOmsEquity").val("");
        }
    });

    function getDataSourceInformationExposure(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 100,
                    schema: {
                        model: {
                            fields: {
                                Exposure: { type: "number" },
                                ExposureID: { type: "string" },
                                Parameter: { type: "number" },
                                ParameterDesc: { type: "string" },
                                AlertExposure: { type: "number" },
                                ExPosurePercent: { type: "number" },
                                WarningMaxExposure: { type: "number" },
                                MaxExposurePercent: { type: "number" },
                                WarningMinExposure: { type: "number" },
                                MinExposurePercent: { type: "number" },
                                MarketValue: { type: "number" },
                                WarningMaxValue: { type: "number" },
                                MaxValue: { type: "number" },
                                WarningMinValue: { type: "number" },
                                MinValue: { type: "number" },

                            }
                        }
                    }
                });
    }

    function InitGridInformationExposure() {
        WinValidateFundExposure.center();
        WinValidateFundExposure.open();


        $("#gridInformationExposure").empty();
            
        var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            dataSourceInformationExposure = getDataSourceInformationExposure(Info);


        gridInformationExposure = $("#gridInformationExposure").kendoGrid({
            dataSource: dataSourceInformationExposure,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridInformationExposureOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "300px",
            excel: {
                fileName: "Information_FundExposure.xlsx"
            },
            columns: [
                { field: "Exposure", title: "Exposure", hidden : true, width: 200 }, 
                { field: "ExposureID", title: "Name", width: 150 },
                { field: "Parameter", title: "Parameter", hidden: true, width: 200 },
                { field: "ParameterDesc", title: "Parameter", width: 150 },
                { field: "AlertExposure", title: "AlertExposure", hidden: true, width: 200 },
                {
                    field: "ExposurePercent", title: "Exp %", width: 75, format: "{0:n4}",
                    template: "#: ExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMaxExposure", title: "Warn Max %", width: 75, format: "{0:n4}",
                    template: "#: WarningMaxExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MaxExposurePercent", title: "Max %", width: 75, format: "{0:n4}",
                    template: "#: MaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMinExposure", title: "Warn Min %", width: 75, format: "{0:n4}",
                    template: "#: WarningMinExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MinExposurePercent", title: "Min %", width: 75, format: "{0:n4}",
                    template: "#: MinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "MarketValue", title: "Value", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMaxValue", title: "Warn Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MaxValue", title: "Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMinValue", title: "Warn Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MinValue", title: "Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },





            ]
        }).data("kendoGrid");
    }

    function gridInformationExposureOnDataBound(e) {
        var grid = $("#gridInformationExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertExposure == 1 || row.AlertExposure == 3 || row.AlertExposure == 5 || row.AlertExposure == 7) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
        });
    }


// update

    function getDataSourceUpInformationExposure(_url) {
        return new kendo.data.DataSource(
                {
                    transport:
                            {
                                read:
                                    {
                                        url: _url,
                                        dataType: "json"
                                    }
                            },
                    batch: true,
                    cache: false,
                    error: function (e) {
                        alert(e.errorThrown + " - " + e.xhr.responseText);
                        this.cancelChanges();
                    },
                    pageSize: 100,
                    schema: {
                        model: {
                            fields: {
                                Exposure: { type: "number" },
                                ExposureID: { type: "string" },
                                Parameter: { type: "number" },
                                ParameterDesc: { type: "string" },
                                AlertExposure: { type: "number" },
                                ExPosurePercent: { type: "number" },
                                WarningMaxExposure: { type: "number" },
                                MaxExposurePercent: { type: "number" },
                                WarningMinExposure: { type: "number" },
                                MinExposurePercent: { type: "number" },
                                MarketValue: { type: "number" },
                                WarningMaxValue: { type: "number" },
                                MaxValue: { type: "number" },
                                WarningMinValue: { type: "number" },
                                MinValue: { type: "number" },

                            }
                        }
                    }
                });
    }

    function InitGridUpInformationExposure(_globMode) {
        WinUpValidateFundExposure.center();
        WinUpValidateFundExposure.open();


        $("#gridUpInformationExposure").empty();

        var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            dataSourceUpInformationExposure = getDataSourceUpInformationExposure(Info);


        gridUpInformationExposure = $("#gridUpInformationExposure").kendoGrid({
            dataSource: dataSourceUpInformationExposure,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridUpInformationExposureOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "300px",
            excel: {
                fileName: "Information_FundExposure.xlsx"
            },
            columns: [
                { field: "Exposure", title: "Exposure", hidden: true, width: 200 },
                { field: "ExposureID", title: "Name", width: 150 },
                { field: "Parameter", title: "Parameter", hidden: true, width: 200 },
                { field: "ParameterDesc", title: "Parameter", width: 150 },
                { field: "AlertExposure", title: "AlertExposure", hidden: true, width: 200 },
                {
                    field: "ExposurePercent", title: "Exp %", width: 75, format: "{0:n4}",
                    template: "#: ExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMaxExposure", title: "Warn Max %", width: 75, format: "{0:n4}",
                    template: "#: WarningMaxExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MaxExposurePercent", title: "Max %", width: 75, format: "{0:n4}",
                    template: "#: MaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMinExposure", title: "Warn Min %", width: 75, format: "{0:n4}",
                    template: "#: WarningMinExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MinExposurePercent", title: "Min %", width: 75, format: "{0:n4}",
                    template: "#: MinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "MarketValue", title: "Value", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMaxValue", title: "Warn Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MaxValue", title: "Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMinValue", title: "Warn Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MinValue", title: "Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },





            ]
        }).data("kendoGrid");
    }

    function onWinUpValidateFundExposureClose() {
        var _urlForReference;
        if (_GlobClientCode == "09") {
            _instrumentPK = $('#UpCmbInstrumentPK').val();
            _instrumentTypePK = 1;
        }
        else {
            _instrumentPK = $('#UpInstrumentPK').val();
            _instrumentTypePK = $('#UpInstrumentTypePK').val();
        }
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {
            if (_GlobClientCode == "20")
                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
            else
                _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

            $.ajax({
                url: _urlForReference,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {



                    var Investment = {
                        InvestmentPK: $('#UpInvestmentPK').val(),
                        DealingPK: $('#UpDealingPK').val(),
                        HistoryPK: $('#UpHistoryPK').val(),
                        StatusInvestment: $('#UpStatusInvestment').val(),
                        OrderStatusDesc: $('#OrderStatus').val(),

                        TrxType: $('#UpTrxType').val(),
                        MarketPK: $('#UpMarketPK').val(),
                        InstrumentPK: $('#UpInstrumentPK').val(),
                        InvestmentTrType: $('#UpInvestmentTrType').val(),
                        OrderPrice: $('#UpOrderPrice').val(),
                        RangePrice: $('#UpRangePrice').val(),
                        Lot: $('#UpLot').val(),
                        LotInShare: $('#UpLotInShare').val(),
                        Volume: $('#UpVolume').val(),
                        Amount: $('#UpAmount').val(),
                        InvestmentNotes: $('#UpInvestmentNotes').val(),
                        BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                        BitHTM: $('#UpBitHTM').is(":checked"),
                        BIRate: $('#BIRate').val(),
                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                        InvestmentStyle: $('#InvestmentStyle').val(),
                        InvestmentObjective: $('#InvestmentObjective').val(),
                        Revision: $('#Revision').val(),

                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                        OtherRevision: $('#OtherRevision').val(),
                        UpdateUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                        type: 'POST',
                        data: JSON.stringify(Investment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    _investmentPK = data;

                                    var HighRisk = {
                                        Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InvestmentPK: _investmentPK,
                                        HighRiskType: 2,

                                    };

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_ExposurePreTrade/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(HighRisk),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (_ComplianceEmail == 'TRUE') {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(HighRisk),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {


                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _investmentPK,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {


                                                                if (_ComplianceEmail == 'TRUE') {
                                                                    var HighRisk1 = {
                                                                        Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InvestmentPK: _investmentPK,
                                                                        HighRiskType: 2,
                                                                    };

                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                        type: 'POST',
                                                                        data: JSON.stringify(HighRisk1),
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (data) {
                                                                            $.unblockUI();
                                                                            refresh();
                                                                            alertify.alert('Amend Success');
                                                                            WinUpdateInvestment.close();
                                                                        },
                                                                        error: function (data) {
                                                                            $.unblockUI();
                                                                            alertify.alert(data.responseText);
                                                                        }
                                                                    });
                                                                }

                                                            }
                                                        });


                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                $.unblockUI();
                                                refresh();
                                                alertify.alert('Amend Success');
                                                WinUpdateInvestment.close();
                                            }


                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText);
                                }
                            });
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }
            });
        }, function () {
            var Investment = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                OrderStatusDesc: $('#OrderStatus').val(),

                TrxType: $('#UpTrxType').val(),
                MarketPK: $('#UpMarketPK').val(),
                InstrumentPK: $('#UpInstrumentPK').val(),
                InvestmentTrType: $('#UpInvestmentTrType').val(),
                OrderPrice: $('#UpOrderPrice').val(),
                RangePrice: $('#UpRangePrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                BitHTM: $('#UpBitHTM').is(":checked"),
                BIRate: $('#BIRate').val(),
                InvestmentStrategy: $('#InvestmentStrategy').val(),
                InvestmentStyle: $('#InvestmentStyle').val(),
                InvestmentObjective: $('#InvestmentObjective').val(),
                Revision: $('#Revision').val(),

                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                OtherRevision: $('#OtherRevision').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };
            // ga jadi continue , ammend cancel
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(Investment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $.unblockUI();
                    refresh();
                    alertify.alert('Amend Cancel');

                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }
            });


        });


    }

    function gridUpInformationExposureOnDataBound(e) {
        var grid = $("#gridUpInformationExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertExposure == 1 || row.AlertExposure == 3 || row.AlertExposure == 5 || row.AlertExposure == 7) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
        });
}

    function InitGridExposureImport() {
        WinFundExposureFromImport.center();
        WinFundExposureFromImport.open();


        $("#gridFundExposureFromImport").empty();

        var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureFromImportOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            dataSourceInformationExposure = getDataSourceInformationExposure(Info);


        gridInformationExposure = $("#gridFundExposureFromImport").kendoGrid({
            dataSource: dataSourceInformationExposure,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridInformationExposureFromImportOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "300px",
            excel: {
                fileName: "Information_FundExposure.xlsx"
            },
            columns: [
                { field: "Exposure", title: "Exposure", hidden: true, width: 200 },
                { field: "ExposureID", title: "Name", width: 150 },
                { field: "Parameter", title: "Parameter", hidden: true, width: 200 },
                { field: "ParameterDesc", title: "Parameter", width: 150 },
                { field: "AlertExposure", title: "AlertExposure", hidden: true, width: 200 },
                {
                    field: "ExposurePercent", title: "Exp %", width: 75, format: "{0:n4}",
                    template: "#: ExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMaxExposure", title: "Warn Max %", width: 75, format: "{0:n4}",
                    template: "#: WarningMaxExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MaxExposurePercent", title: "Max %", width: 75, format: "{0:n4}",
                    template: "#: MaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMinExposure", title: "Warn Min %", width: 75, format: "{0:n4}",
                    template: "#: WarningMinExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MinExposurePercent", title: "Min %", width: 75, format: "{0:n4}",
                    template: "#: MinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "MarketValue", title: "Value", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMaxValue", title: "Warn Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MaxValue", title: "Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMinValue", title: "Warn Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MinValue", title: "Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },





            ]
        }).data("kendoGrid");
    }

    function gridInformationExposureFromImportOnDataBound(e) {
        var grid = $("#gridFundExposureFromImport").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertExposure == 1 || row.AlertExposure == 3 || row.AlertExposure == 5 || row.AlertExposure == 7) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
        });
    }

    function onWinFundExposureFromEmailClose() {


        alertify.confirm("Are You Sure To Continue Save Data ?", function () {
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/InsertIntoInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_ExposureFromOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (_ComplianceEmail == 'TRUE') {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailFundExposureFromOMSEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $.unblockUI();
                                        alertify.alert("Import OMS Success !");
                                        refresh();
                                        SendNotification();
                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                            else {
                                $.unblockUI();
                                alertify.alert("Import OMS Success !");
                                refresh();
                                SendNotification();
                            }
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });


                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }
            });

        });

}

    function onWinValidateFundExposureClose() {
        var _urlForReference;
        if (_GlobClientCode == "09") {
            _instrumentPK = $('#CmbInstrumentPK').val();
            _instrumentTypePK = 1;
            var val = 1;
        }
        else {
            _instrumentPK = $('#InstrumentPK').val();
            _instrumentTypePK = $('#InstrumentTypePK').val();
            var val = validateData();
        }
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {
            if (_GlobClientCode == "20")
                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EQ/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
            else
                _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

            $.ajax({
                url: _urlForReference,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var InvestmentInstruction = {
                        PeriodPK: _defaultPeriodPK,
                        ValueDate: $('#DateFrom').val(),
                        Reference: data,
                        InstructionDate: $('#DateFrom').val(),
                        //InstrumentPK: $(htmlInstrumentPK).val(),
                        InstrumentPK: _instrumentPK,
                        InstrumentTypePK: _instrumentTypePK,
                        OrderPrice: $('#OrderPrice').val(),
                        RangePrice: $('#RangePrice').val(),
                        AcqPrice: 0,
                        Lot: $('#Lot').val(),
                        LotInShare: $('#LotInShare').val(),
                        Volume: $('#Volume').val(),
                        Amount: $('#Amount').val(),
                        TrxType: $('#TrxType').val(),
                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                        CounterpartPK: 0,
                        FundPK: $('#FundPK').val(),
                        MarketPK: $('#MarketPK').val(),
                        InvestmentNotes: $('#InvestmentNotes').val(),
                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                        BitHTM: $('#BitHTM').is(":checked"),
                        EntryUsersID: sessionStorage.getItem("user"),
                        InvestmentTrType: $('#InvestmentTrType').val(),
                        BIRate: $('#BIRate').val(),
                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                        InvestmentStyle: $('#InvestmentStyle').val(),
                        InvestmentObjective: $('#InvestmentObjective').val(),
                        Revision: $('#Revision').val(),
                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                        OtherRevision: $('#OtherRevision').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                        type: 'POST',
                        data: JSON.stringify(InvestmentInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    _investmentPK = data;


                                    var HighRisk = {
                                        Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InvestmentPK: _investmentPK,
                                        HighRiskType: 2,

                                    };

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_ExposurePreTrade/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(HighRisk),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {

                                            if (_ComplianceEmail == 'TRUE') {

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(HighRisk),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _investmentPK,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {


                                                                if (_ComplianceEmail == 'TRUE') {
                                                                    var HighRisk1 = {
                                                                        Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InvestmentPK: _investmentPK,
                                                                        HighRiskType: 2,
                                                                    };

                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                        type: 'POST',
                                                                        data: JSON.stringify(HighRisk1),
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (data) {
                                                                            console.log("done email");
                                                                            $.unblockUI();
                                                                            alertify.alert("Insert Investment Success !");
                                                                            win.close();
                                                                            refresh();
                                                                            SendNotification();
                                                                        },
                                                                        error: function (data) {
                                                                            $.unblockUI();
                                                                            alertify.alert(data.responseText);
                                                                        }
                                                                    });
                                                                }

                                                            }
                                                        });

                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                $.unblockUI();
                                                alertify.alert("Insert Investment Success !");
                                                win.close();
                                                refresh();
                                                SendNotification();
                                            }


                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText);
                                }
                            });
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }

            });

        });
    }


    
});

