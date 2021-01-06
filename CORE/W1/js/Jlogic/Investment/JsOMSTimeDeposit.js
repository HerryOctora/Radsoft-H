$(document).ready(function () {
    var win;
    var winFundPositionIndex0;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var UphtmlInstrumentPK;
    var UphtmlInstrumentID;
    var UphtmlInstrumentName;

    var checkedInvestmentBuy = {};
    var checkedInvestmentSell = {};
    var gridFundPositionGlob;
    var gridFundExposureGlob;

    var _AmountValue;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var globUpdate = 0;

    var GlobValidator = $("#WinInvestmentInstruction").kendoValidator().data("kendoValidator");
    var GlobValidatorUpdate = $("#WinUpdateInvestment").kendoValidator().data("kendoValidator");
    var _d = new Date();
    var _fy = _d.getFullYear();
    var _defaultPeriodPK;


    if (_GlobClientCode == "03") {
        $("#LblGenerateOmsDeposito").show();
    }
    else {
        $("#LblGenerateOmsDeposito").hide();
    }

    if (_GlobClientCode == "01") {
        $("#lblSignature").show();
        $("#lblPosition").show();
    }
    else {
        $("#lblSignature").hide();
        $("#lblPosition").hide();
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

    
    //BIT ROLL OVER INTEREST
    $("#BitRollOverInterest").click(function () {
        if ($(this).is(':checked')) {
            _AmountValue = $("#Amount").val();
            recallRollOverInterest();
        } else {
            $("#Amount").data("kendoNumericTextBox").value(_AmountValue);
        }
    });

    function recallRollOverInterest() {
        $("#OldMaturityDate").hide();
        var _MaturityDate, _ValueDate, _AcqDate, _InstrumentPK, _Amount, _InterestPercent, _InterestDaysType, _InterestPaymentType, _PaymentModeOnMaturity;

        if ($("#InstructionDate").val() == undefined || $("#InstructionDate").val() == "" || $("#InstructionDate").val() == null)
            _ValueDate = '01/01/1900';
        else
            _ValueDate = kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");

        if ($("#AcqDate").val() == undefined || $("#AcqDate").val() == "" || $("#AcqDate").val() == null)
            _AcqDate = '01/01/1900';
        else
            _AcqDate = kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM-dd-yy");

        if ($("#OldMaturityDate").val() == undefined || $("#OldMaturityDate").val() == "" || $("#OldMaturityDate").val() == null)
            _MaturityDate = '01/01/1900';
        else
            _MaturityDate = kendo.toString($("#OldMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy");

        if ($("#InstrumentPK").val() == undefined)
            _InstrumentPK = 0;
        else
            _InstrumentPK = $('#InstrumentPK').val();

        if ($("#Amount").val() == undefined)
            _Amount = 0;
        else
            _Amount = $('#Amount').val();

        if ($("#OldInterestPercent").val() == undefined)
            _InterestPercent = 0;
        else
            _InterestPercent = $('#OldInterestPercent').val();

        if ($("#OldInterestDaysType").val() == undefined)
            _InterestDaysType = 0;
        else
            _InterestDaysType = $('#OldInterestDaysType').val();

        if ($("#OldInterestPaymentType").val() == undefined)
            _InterestPaymentType = 0;
        else
            _InterestPaymentType = $('#OldInterestPaymentType').val();

        if ($("#OldPaymentModeOnMaturity").val() == undefined)
            _PaymentModeOnMaturity = 0;
        else
            _PaymentModeOnMaturity = $('#OldPaymentModeOnMaturity').val();

        var InvestmentInstruction = {
            ValueDate: _ValueDate,
            InstrumentPK: _InstrumentPK,
            Amount: _Amount,
            AcqDate: _AcqDate,
            MaturityDate: _MaturityDate,
            InterestPercent: _InterestPercent,
            InterestDaysType: _InterestDaysType,
            InterestPaymentType: _InterestPaymentType,
            PaymentModeOnMaturity: _PaymentModeOnMaturity,
            FundPK: $("#FundPK").val()
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/OMSTimeDeposit/GetRollOverInterestByDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(InvestmentInstruction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == undefined || data == "" || data == null || data == 0)
                    $("#Amount").data("kendoNumericTextBox").value("0");
                else
                    $("#Amount").data("kendoNumericTextBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
            }
        });
    }

    $("#UpOldMaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#UpOldMaturityDate").hide();

    //END BIT ROLL OVER INTEREST

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

    $("#DateFrom").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        change: onChangeDateFrom,
        value: new Date()
    });

    $("#UpAcqDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#UpMaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#InstructionDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        value: new Date()
    });
    $("#UpInstructionDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    function onChangeDateFrom() {
        $("#InstructionDate").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
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
                change: onChangeFilterFundID,
                index: 0,
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }


    });
    function onChangeFilterFundID() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        } else {
            refresh();
            
        }
     
    }


    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FilterExposureType",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#FilterExposureType").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                change: onChangeFilterExposureType,
                dataSource: data,
                index: 0,
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function onChangeFilterExposureType() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        refresh();
    }

    if (_GlobClientCode == "17") {
        $("#LblPendingCash").show();
        RefreshPendingCash();
    }
    else {
        $("#LblPendingCash").hide();
    }

    RefreshAUMYesterday();
    RefreshAvailableCash();
    ResetSelected();

    function validateData() {
        
        if ($("#InterestPaymentType").val() != null)
        {
            if (($("#SpecificDate").data("kendoDatePicker").value() == "" || $("#SpecificDate").data("kendoDatePicker").value() == null) &&
                ($("#InterestPaymentType").val() == 4 || $("#InterestPaymentType").val() == 5 || $("#InterestPaymentType").val() == 6)) {
                alertify.alert("Specific date must be filled");
                return 0;
            }
        }
        if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
            alertify.alert("Amount must be filled");
            return 0;
        }
        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }



    function validateDataUpdate() {

        if ($("#UpInterestPaymentType").val() != null) {
            if (($("#UpSpecificDate").data("kendoDatePicker").value() == "" || $("#UpSpecificDate").data("kendoDatePicker").value() == null) &&
                ($("#UpInterestPaymentType").val() == 4 || $("#UpInterestPaymentType").val() == 5 || $("#UpInterestPaymentType").val() == 6)) {
                alertify.alert("Specific date must be filled");
                return 0;
            }
        }
        if ($("#UpAmount").data("kendoNumericTextBox").value() == 0) {
            alertify.alert("Amount must be filled");
            return 0;
        }
        if (GlobValidatorUpdate.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
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

    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Period",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#FilterPeriod").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                change: onChangeFilterPeriod,
                dataSource: data,
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function onChangeFilterPeriod() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    WinOMSTimeDepositListing = $("#WinOMSTimeDepositListing").kendoWindow({
        height: 600,
        title: "* Listing OMS Time Deposit",
        visible: false,
        width: 900,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
        close: onWinOMSTimeDepositListingClose
    }).data("kendoWindow");


    win = $("#WinInvestmentInstruction").kendoWindow({
        height: 750,
        title: "Investment Ticket Detail",
        visible: false,
        width: 800,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 280, left: 300 })
        },
        close: onPopUpClose
    }).data("kendoWindow");

    winFundPositionIndex0 = $("#WinDetailFundPositionIndex0").kendoWindow({
        height: 400,
        title: "Detail",
        visible: false,
        width: 800,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 320, left: 300 })
        },
    }).data("kendoWindow");

    WinUpdateInvestment = $("#WinUpdateInvestment").kendoWindow({
        height: 750,
        title: "Investment Ticket Update",
        visible: false,
        width: 1000,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 320, left: 300 })
        },
        close: _clearUpAttributes
    }).data("kendoWindow");

    $("#AcqDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
    });

    $("#MaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#OldMaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#OldMaturityDate").hide();

    $("#SettledDate").kendoDatePicker({
        format: "dd/MMM/yyyy"
    });

    $("#SpecificDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
    });

    $("#UpSpecificDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
    });



    function onPopUpClose() {
        $("#InstrumentPK").val('');
        $("#BankPK").val('');
        $("#BankBranchPK").val('');
        $("#InvestmentNotes").val('');
        $("#InstrumentID").val('');
        $("#AcqDate").data("kendoDatePicker").value(null);
        $("#MaturityDate").data("kendoDatePicker").value(null);
        $("#InterestPercent").data("kendoNumericTextBox").value(null);
        $("#TextAmount").val('');
        $("#Amount").data("kendoNumericTextBox").value(null);
        $("#AmountToTransfer").data("kendoNumericTextBox").value(null);
        $("#TrxType").data("kendoComboBox").value(null);
        $("#FundCashRefPK").data("kendoComboBox").value(null);
        GlobValidator.hideMessages();
    }



    function onCloseUpdateInvestment() {
        $("#UpInstrumentPK").val('');
        $("#UpFundPK").val('');
        $("#UpOrderPrice").data("kendoNumericTextBox").value(null);
        $("#UpAmount").data("kendoNumericTextBox").value(null);
        $("#UpAmountToTransfer").data("kendoNumericTextBox").value(null);
        $("#UpLot").data("kendoNumericTextBox").value(null);
        $("#UpVolume").data("kendoNumericTextBox").value(null);
        $("#UpFundCashRefPK").data("kendoComboBox").value(null);
        GlobValidator.hideMessages(); 
    }


    //$("#NetCashAvailable").on("click", function () {
    //    if ($("#FilterFundID").val() == "") {
    //        _fundID = "0";
    //    }
    //    else {
    //        _fundID = $("#FilterFundID").val();
    //    }

    //    var _url = window.location.origin + "/Radsoft/OMSEquity/OMSEquityGetNetAvailableCashDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
    //    var dsListAvailableCash = getDataSourceListAvailableCash(_url);

    //    $("#gridListAvailableCashDetail").kendoGrid({
    //        dataSource: dsListAvailableCash,
    //        height: "90%",
    //        scrollable: {
    //            virtual: true
    //        },
    //        reorderable: true,
    //        sortable: true,
    //        resizable: true,
    //        groupable: true,
    //        filterable: {
    //            extra: false,
    //            operators: {
    //                string: {
    //                    contains: "Contain",
    //                    eq: "Is equal to",
    //                    neq: "Is not equal to"
    //                }
    //            }
    //        },
    //        pageable: true,
    //        pageable: {
    //            input: true,
    //            numeric: false
    //        },
    //        columns: [
    //           { field: "FSource", title: "Source", width: 400 },
    //           { field: "Amount", title: "Amount", format: "{0:n0}", width: 200 }
    //        ]
    //    });

    //    WinListAvailableCashDetail.open();
    //});

    var WinListAvailableCashDetail;

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


    initButton();
    InitgridFundPosition();


    if (_GlobClientCode != "09") {
        InitgridFundExposure();
    }

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

        $("#BtnSaveDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCancelDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
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

        $("#BtnExport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOMSTimeDepositListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRefreshFilterMaturity").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnImportOmsDeposito").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnGenerateOmsDeposito").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
    }




    var splitter = $("#splitter").kendoSplitter({
        orientation: "vertical",
        panes: [
             { collapsible: true, size: "1000px" },
             { collapsible: true, size: "450px" },
             { collapsible: true, size: "550px" }
        ],
    }).data("kendoSplitter");

    $("#tabstrip").kendoTabStrip();

    $("#BtnToInvestment").click(function () {
        splitter.toggle("#PaneInvestment");
    });

    $("#BtnToExposure").click(function () {
        splitter.toggle("#PaneExposure");
    });

    $("#BtnToFundPosition").click(function () {
        splitter.toggle("#PaneFundPosition");
    });

    $("#BtnToShow").click(function () {
        splitter.expand("#PaneInvestment");
        //splitter.expand("#PaneByInstrument");
        splitter.expand("#PaneExposure");
        splitter.expand("#PaneFundPosition");
    });

    $("#BtnToHide").click(function () {
        splitter.collapse("#PaneInvestment");
        //splitter.collapse("#PaneByInstrument");
        splitter.collapse("#PaneExposure");
        splitter.collapse("#PaneFundPosition");
    });

    $("#BtnRefresh").click(function () {

        refresh();

        alertify.set('notifier','position', 'top-center'); alertify.success("Refresh Done");
    });

    function refresh() {
        if (_GlobClientCode == "17") {
            RefreshPendingCash();
        }
        RefreshAUMYesterday();
        RefreshAvailableCash();
        //RefreshNetOMSTimeDeposit();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        if ($("#FilterFundID").data("kendoComboBox").value() == null || $("#FilterFundID").data("kendoComboBox").value() == "") {
            alertify.alert("Please Fill Fund");
            return;
        }
        if ($("#FilterFundID").val() == "" || $("#FilterFundID").val() == "0") {
            _fundID = "0";
            _type = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
            _type = $("#FilterExposureType").val();
        }
        if ($("#FilterPeriod").val() == "") {
            _filterPeriod = "0";
        }
        else {
            _filterPeriod = $("#FilterPeriod").val();
        }
        if ($("#FilterValue").val() == "") {
            _filterValue = "0";
        }
        else {
            _filterValue = $("#FilterValue").val();
        }
        //var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        //$("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);
        //saiki
        var newDS = getDataSourceInvestmentByMaturity(window.location.origin + "/Radsoft/OMSTimeDeposit/GetDataInvestmentTimeDepositByMaturity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _filterPeriod + "/" + _filterValue);
        $("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 4 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

        //// Refresh Grid Bank Position Per Fund ( atas Layar )
        var newDS = getDataSourceA(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_BankPosition_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridFundPosition").data("kendoGrid").setDataSource(newDS);

        // 
        if (_GlobClientCode != "09") {
            var newDS = getDataSourceC(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSExposureDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _type);
            $("#gridFundExposure").data("kendoGrid").setDataSource(newDS);
        }


        //var newDS = getDataSourceOMSTimeDepositByInstrument(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        //$("#gridOMSTimeDepositByInstrument").data("kendoGrid").setDataSource(newDS);

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
        RefreshAvailableCash();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshSellOnly() {
        RefreshAvailableCash();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 4 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshBuySell() {
        RefreshAvailableCash();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID);
        $("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);

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
                { field: "AfterTTwoNAVPercent", aggregate: "sum" }],
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
                             StatusDesc: { type: "string" }
                         }
                     }
                 }
             });
    }

    function InitgridFundPosition() {
        var AccountApprovedURL = window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_BankPosition_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceA(AccountApprovedURL);

        gridFundPositionGlob = $("#gridFundPosition").kendoGrid({
            dataSource: dataSourceApproved,
            editable: false,
            height: "730px",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            selectable: "cell",
            resizable: true,
            toolbar: kendo.template($("#gridFundPositionTemplate").html()),
            excel: {
                fileName: "DepositoFundPosition.xlsx"
            },
            columns: [{
                field: "Name",
                title: "Bank", headerAttributes: {
                    style: "text-align: center"
                },
                width: 175
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
                        field: "CurrBalance", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "CurrNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 45
                    }]
                }, {
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "Movement", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterBalance", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 45
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
                        field: "MovementTOne", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTOne", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterTOneNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 45
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
                        field: "MovementTTwo", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTTwo", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterTTwoNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 45
                    }]
                }]
            }]
        }).data("kendoGrid");
    }

    $(gridFundPositionGlob.tbody).on("click", "td", function (e) {
        
        var row = $(this).closest("tr");
        var rowIdx = $("tr", gridFundPositionGlob.tbody).index(row);
        var colIdx = $("td", row).index(this);
        var colName = $('#gridFundPosition').find('th').eq(colIdx).text();
        var item = gridFundPositionGlob.dataItem(row);
        $("#gridDetailFundPositionIndex0").empty();
        if (colIdx == 0 || colIdx == 1) {
            dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_InstrumentDetailPerFundPerBankT0/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Name);

            $("#gridDetailFundPositionIndex0").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                reorderable: true,
                sortable: true,
                height: "300px",
                toolbar: ["excel"],
                excel: {
                    fileName: "DetailFundPositionT0.xlsx"
                },
                columns: [
                    {
                        field: "InstrumentID", title: "ID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InterestPercent", title: "Interest %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Balance", title: "T0 Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    }
                ]
            });
            winFundPositionIndex0.open();
        }
        else if (colIdx == 3) {
            dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_InstrumentDetailPerFundPerBankT0Movement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Name);

            $("#gridDetailFundPositionIndex0").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                reorderable: true,
                sortable: true,
                height: "300px",
                toolbar: ["excel"],
                excel: {
                    fileName: "DetailFundPositionT0.xlsx"
                },
                columns: [
                    {
                        field: "InstrumentID", title: "ID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InterestPercent", title: "Interest %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Balance", title: "T0 Movement", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    }
                ]
            });
            winFundPositionIndex0.open();
        }
        else if (colIdx == 6) {
            dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_InstrumentDetailPerFundPerBankT1Movement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Name);

            $("#gridDetailFundPositionIndex0").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                reorderable: true,
                sortable: true,
                height: "300px",
                toolbar: ["excel"],
                excel: {
                    fileName: "DetailFundPositionT0.xlsx"
                },
                columns: [
                    {
                        field: "InstrumentID", title: "ID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InterestPercent", title: "Interest %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Balance", title: "T1 Movement", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    }
                ]
            });
            winFundPositionIndex0.open();
        }
        else if (colIdx == 9) {
            dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_InstrumentDetailPerFundPerBankT2Movement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Name);

            $("#gridDetailFundPositionIndex0").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                reorderable: true,
                sortable: true,
                height: "300px",
                toolbar: ["excel"],
                excel: {
                    fileName: "DetailFundPositionT0.xlsx"
                },
                columns: [
                    {
                        field: "InstrumentID", title: "ID", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InterestPercent", title: "Interest %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Balance", title: "T2 Movement", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    }
                ]
            });
            winFundPositionIndex0.open();
        }


    });

    function getDataSourceFundPositionIndex0(_url) {
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
                             InterestPercent: { type: "number" },
                             MaturityDate: { type: "date" },
                             Balance: { type: "number" },
                         }
                     }
                 }
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
                            StatusInvestment: { type: "number" },
                            StatusDealing: { type: "number" },
                            StatusSettlement: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            InstrumentPK: { type: "number" },
                            InstrumentID: { type: "string" },
                            InstrumentName: { type: "string" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            TrxType: { type: "number" },
                            TrxTypeID: { type: "string" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            Category: { type: "string" },
                            Volume: { type: "number" },
                            Amount: { type: "number" },
                            InterestPercent: { type: "number" },
                            MaturityDate: { type: "date" },
                            TrxBuy: { type: "number" },
                            BitBreakable: { type: "boolean" },
                            BankBranchPK: { type: "number" },
                            BankBranchID: { type: "string" },
                        }
                    }
                },
                group:
                    [
                        {
                            field: "OrderStatusDesc", aggregates: [
                                { field: "Volume", aggregate: "sum" },
                                { field: "Amount", aggregate: "sum" },
                                { field: "FundID", aggregate: "count" },
                            ]
                        }
                    ],
                aggregate: [{ field: "Volume", aggregate: "sum" },
                { field: "FundID", aggregate: "count" },
                ]
            });
    }

    function getDataSourceInvestmentByMaturity(_url) {
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
                             InstrumentName: { type: "string" },
                             Category: { type: "string" },
                             Balance: { type: "number" },
                             InterestPercent: { type: "number" },
                             AcqDate: { type: "date" },
                             MaturityDate: { type: "date" },
                         }
                     }
                 },
                 aggregate: [{ field: "Balance", aggregate: "sum" }]
             });
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }

        if (_GlobClientCode == "05") {
            var gridTimeDepositBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSTimeDepositInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    {
                        hidden: false, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: false, field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "TrxType", title: "TrxType", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Bank", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },

                    {
                        field: "Volume", title: "Nominal", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "InterestPercent", title: "Int %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 30,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                ]
            }).data("kendoGrid");
        }
        else {
            var gridTimeDepositBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateBuyOnly").html()),
                excel: {
                    fileName: "OMSTimeDepositInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    {
                        hidden: false, field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                    },
                    {
                        hidden: true, field: "TrxType", title: "TrxType", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "InstrumentID", title: "Bank", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "MaturityDate", title: "Maturity Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },

                    {
                        field: "Volume", title: "Nominal", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "InterestPercent", title: "Int %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 30,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                ]
            }).data("kendoGrid");
        }


        gridTimeDepositBuyOnly.table.on("click", ".checkboxBuy", selectRowBuy);
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

        $("#SelectedAllApprovedTimeDepositBuy").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataTimeDepositBuyOnly(_checked, "Approved");

        });

        //gridTimeDepositBuyOnly.table.on("click", ".cSelectedDetailApprovedTimeDepositBuy", selectDataApprovedTimeDepositBuy);

        function selectDataApprovedTimeDepositBuy(e) {


            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositBuyOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataTimeDepositBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Investment",
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
    function SelectDeselectAllDataTimeDepositBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshBuySell();
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 4 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 5 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }

        if (_GlobClientCode == "05") {
            var gridTimeDepositSellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSTimeDepositInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        hidden: false, field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        hidden: true, field: "TrxType", title: "TrxType", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "SettledDate", title: "Placement Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "InstrumentID", title: "Bank", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Volume", title: "Nominal", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MaturityDate", title: "M.Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "InterestPercent", title: "Int %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 30,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "TrxBuy", title: "TrxBuy", hidden: true, width: 50 },
                ]
            }).data("kendoGrid");
        }
        else {
            var gridTimeDepositSellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridInvestmentInstructionSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridInvestmentInstructionTemplateSellOnly").html()),
                excel: {
                    fileName: "OMSTimeDepositInvestmentInstruction.xlsx"
                },
                columns: [
                    //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                    { command: { text: "Show", click: showDetailsSell }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                        template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedInvestment",
                    //    width: 30,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "FundID", title: "Fund", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                    },
                    {
                        hidden: true, field: "TrxType", title: "TrxType", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "SettledDate", title: "Placement Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "InstrumentID", title: "Bank", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50
                    },
                    {
                        field: "Volume", title: "Nominal", headerAttributes: {
                            style: "text-align: center"
                        }, width: 70, format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "MaturityDate", title: "M.Date", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "InterestPercent", title: "Int %", headerAttributes: {
                            style: "text-align: center"
                        }, width: 30,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", hidden: true, width: 50 },
                    { hidden: true, field: "TrxBuy", title: "TrxBuy", hidden: true, width: 50 },
                ]
            }).data("kendoGrid");
        }


        gridTimeDepositSellOnly.table.on("click", ".checkboxSell", selectRowSell);
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

        $("#SelectedAllApprovedTimeDepositSell").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataTimeDepositSellOnly(_checked, "Approved");

        });

        gridTimeDepositSellOnly.table.on("click", ".cSelectedDetailApprovedTimeDepositSell", selectDataApprovedTimeDepositSell);

        function selectDataApprovedTimeDepositSell(e) {


            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositSellOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataTimeDepositSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Investment",
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
    function SelectDeselectAllDataTimeDepositSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Investment",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshBuySell();
                refreshSellOnly();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }

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

            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/OMSTimeDeposit/GetDataInvestmentTimeDepositByMaturity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/2/2",
            dataSourceApproved = getDataSourceInvestmentByMaturity(InvestmentInstructionApprovedURL);
        }


        gridInvestmentInstructionGlob = $("#gridInvestmentInstruction").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "480px",
            selectable: "cell",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridInvestmentInstructionDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridInvestmentInstructionTemplate").html()),
            excel: {
                fileName: "OMSTimeDeposit.xlsx"
            },
            columns: [
                {
                    field: "InstrumentID", title: "Bank", headerAttributes: {
                        style: "text-align: center"
                    }, width: 40
                },
                {
                    field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Category", title: "Category", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Balance", title: "Balance", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>"
                    , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" }
                },
                {
                    field: "AcqDate", title: "Acq Date", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#", attributes: { style: "text-align:center;" }
                },
                {
                    field: "MaturityDate", title: "Maturity Date", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#", attributes: { style: "text-align:center;" }
                },
                {
                    field: "InterestPercent", title: "Int %", headerAttributes: {
                        style: "text-align: center"
                    }, width: 30,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
            ]
        }).data("kendoGrid");
    }

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
                { field: "T7", title: "T-7", format: "{0:n2}", width: 150, groupFooterTemplate: "#=kendo.toString(sum,'n4')#", format: "{0:n4}", attributes: { style: "text-align:right;" } },]
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

    function getDataSourceC(_url) {
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
                 pageSize: 10,
                 aggregate: [{ field: "CurrentValue", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             FundID: { type: "string" },
                             ExposureType: { type: "string" },
                             Parameter: { type: "string" },
                             CurrentValue: { type: "number" },
                             PotentialValue: { type: "number" },
                             MaxValue: { type: "number" },
                             DifferenceValue: { type: "number" },
                             CurrentPercentage: { type: "number" },
                             PotentialPercentage: { type: "number" },
                             MaxPercentage: { type: "number" },
                             DifferencePercentage: { type: "number" },
                         }
                     }
                 }
             });
    }

    if (_GlobClientCode != "09") {
        $(gridFundExposureGlob.tbody).on("click", "td", function (e) {

            var row = $(this).closest("tr");
            var rowIdx = $("tr", gridFundExposureGlob.tbody).index(row);
            var colIdx = $("td", row).index(this);
            var colName = $('#gridFundExposure').find('th').eq(colIdx).text();
            var item = gridFundExposureGlob.dataItem(row);
            $("#gridDetailFundPositionIndex0").empty();
            if (colIdx == 3 || colIdx == 2) {
                dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_ExposureDetailCurrentBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Parameter + "/" + item.FundID + "/" + item.ExposureType);

                $("#gridDetailFundPositionIndex0").kendoGrid({
                    dataSource: dataSourceApproved,
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    height: "300px",
                    toolbar: ["excel"],
                    excel: {
                        fileName: "DetailFundExposureCurrentBalance.xlsx"
                    },
                    columns: [
                        {
                            field: "InstrumentID", title: "ID", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "InterestPercent", title: "Interest %", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "Balance", title: "Current Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        }
                    ]
                });
                winFundPositionIndex0.open();
            } else if (colIdx == 4) {
                dataSourceApproved = getDataSourceFundPositionIndex0(window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_ExposureDetailPotential/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + item.Parameter + "/" + item.FundID + "/" + item.ExposureType);

                $("#gridDetailFundPositionIndex0").kendoGrid({
                    dataSource: dataSourceApproved,
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    height: "300px",
                    toolbar: ["excel"],
                    excel: {
                        fileName: "DetailFundExposureCurrentBalance.xlsx"
                    },
                    columns: [
                        {
                            field: "InstrumentID", title: "ID", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "InterestPercent", title: "Interest %", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#", headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        },
                        {
                            field: "Balance", title: "Current Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                                style: "text-align: center"
                            }, width: 50
                        }
                    ]
                });
                winFundPositionIndex0.open();
            }

        });
    }

    function gridFundExposureOnDataBound(e) {
        var grid = $("#gridFundExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.MaxValue > 0) {
                if (row.DifferenceValue < 0) {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
                } else {
                    $('tr[data-uid="' + row.uid + '"] ').removeClass("approvedRowPending");
                }
            }
            if (row.MaxPercentage > 0) {
                if (row.DifferencePercentage < 0) {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
                } else {
                    $('tr[data-uid="' + row.uid + '"] ').removeClass("approvedRowPending");
                }
            }
        });
    }

    function InitgridFundExposure() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
            _type = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
            _type = $("#FilterExposureType").val();
        }
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FundApprovedURL = window.location.origin + "/Radsoft/OMSTimeDeposit/OMSExposureDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _type,
             dataSourceApproved = getDataSourceC(FundApprovedURL);
        }


        gridFundExposureGlob = $("#gridFundExposure").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            selectable: "cell",
            dataBound: gridFundExposureOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "730px",
            toolbar: kendo.template($("#gridFundExposureTemplate").html()),
            excel: {
                fileName: "OMSTimeDepositExposure.xlsx"
            },
            columns: [
                {
                    field: "FundID", title: "Fund", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "ExposureType", title: "ExposureType", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "Parameter", title: "Parameter", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "CurrentValue", title: "Current Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                },
                {
                    field: "PotentialValue", title: "Potential Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MaxValue", title: "Max Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "DifferenceValue", title: "Difference Value", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "CurrentPercentage", title: "Current (%)", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "PotentialPercentage", title: "Potential (%)", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MaxPercentage", title: "Max (%)", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "DifferencePercentage", title: "Difference (%)", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                 {
                     field: "DifferenceAmount", title: "Amount", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                         style: "text-align: center"
                     }, width: 50
                 },
            ]
        }).data("kendoGrid");
    }

    function gridInvestmentInstructionDataBound() {
        var grid = $("#gridInvestmentInstruction").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.OrderStatusDesc == "4.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentYellow");
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

    function gridInvestmentInstructionBuyOnlyDataBound() {
        var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedInvestmentBuy[row.InvestmentPK] = null;
                checkedInvestmentBuy[0] = null;
            }

            if (checkedInvestmentBuy[row.InvestmentPK] && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3) && (row.OrderStatusDesc != "6.REJECT" && row.StatusDesc != "VOID")) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxBuy")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "4.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentYellow");
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

            

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedInvestmentSell[row.InvestmentPK] = null;
                checkedInvestmentSell[0] = null;
            }
            

            if (checkedInvestmentSell[row.InvestmentPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc != "VOID") && row.OrderStatusDesc != "" && row.InvestmentPK != 0 && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxSell")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "4.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentYellow");
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


    $("#BtnAdd").on("click", function () {
        BtnAddClick(1);
    });

    $('#BtnRefreshFilterMaturity').on("click", function () {
        refresh();

        alertify.set('notifier','position', 'top-center'); alertify.success("Refresh Done");
    });

    function BtnAddClick(_bs) {
        globUpdate = 0;
        $("#trSpecificDate").hide();
        $("#LblStatutoryType").show();
        //BIT ROLL OVER INTEREST
        $("#BitRollOverInterest").prop('checked', false);
        $("#LblBitRollOverInterest").hide();
        //END BIT ROLL OVER INTEREST
        if (_bs == 2) {
            $("#BitBreakable").prop("disabled", true);
            $("#BreakInterestPercent").attr("required", true);
        }
        else {
            $("#BitBreakable").prop("disabled", false);
            $("#BitBreakable").prop('checked', true);
            $("#BreakInterestPercent").attr("required", false);
        }
        $("#Category").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Normal", value: "Deposit Normal" },
                { text: "On Call", value: "Deposit On Call" },
                // { text: "Negotiabale Certificate Deposit", value: "Negotiabale Certificate Deposit" }
            ],
            filter: "contains",
            suggest: true,
            index: 0
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDCurrencyPK,
                    index: 0
                });

                if (_bs == 1) {
                    $("#CurrencyPK").data("kendoComboBox").enable(true);
                } else {
                    $("#CurrencyPK").data("kendoComboBox").enable(false);
                }


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#StatutoryType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: 1 },
                { text: "No", value: 0 }
            ],
            filter: "contains",
            value: setCmbStatutoryType()
        });

        function setCmbStatutoryType() {
            return false;
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankPK,
                });
                if (_bs == 1) {
                    $("#BankPK").data("kendoComboBox").enable(true);
                } else {
                    $("#BankPK").data("kendoComboBox").enable(false);
                }

                $.ajax({
                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        $("#BankBranchPK").kendoComboBox({
                            dataValueField: "BankBranchPK",
                            dataTextField: "ID",
                            dataSource: data,
                            filter: "contains",
                            suggest: true

                        });
                        $("#BankBranchPK").data("kendoComboBox").enable(false);


                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        function onChangeBankPK() {
            $("#BankBranchPK").data("kendoComboBox").value("");
            $("#FundCashRefPK").data("kendoComboBox").value("");
            $("#CurrencyPK").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/ValidateCheckBankPTPCode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/GetBankCurrencyByPTPCode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $("#CurrencyPK").kendoComboBox({
                                        dataValueField: "CurrencyPK",
                                        dataTextField: "ID",
                                        dataSource: data,
                                        filter: "contains",
                                        suggest: true,
                                        index: 0
                                    });
                                    $("#CurrencyPK").data("kendoComboBox").enable(true);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                            if (_GlobClientCode == "99") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#BankBranchPK").kendoComboBox({
                                            dataValueField: "BankBranchPK",
                                            dataTextField: "ID",
                                            dataSource: data,
                                            filter: "contains",
                                            suggest: true,
                                            //index: 0,
                                            change: onChangeBankBranchPK
                                        });
                                        $("#BankBranchPK").data("kendoComboBox").enable(true);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#BankBranchPK").kendoComboBox({
                                            dataValueField: "BankBranchPK",
                                            dataTextField: "ID",
                                            dataSource: data,
                                            filter: "contains",
                                            suggest: true,
                                            change: onChangeBankBranchPK
                                        });
                                        $("#BankBranchPK").data("kendoComboBox").enable(true);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/GetBankInformation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    //if (data.InterestDaysType == 0) {
                                    //    $("#InterestDaysType").data("kendoComboBox").value("");
                                    //}
                                    //else {
                                    //    $("#InterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                                    //}
                                    if (data.InterestPaymentType == 0) {
                                        $("#InterestPaymentType").data("kendoComboBox").value("");
                                    }
                                    else {
                                        $("#InterestPaymentType").data("kendoComboBox").value(data.InterestPaymentType);
                                    }
                                    if (data.PaymentModeOnMaturity == 0) {
                                        $("#PaymentModeOnMaturity").data("kendoComboBox").value("");
                                    }
                                    else {
                                        $("#PaymentModeOnMaturity").data("kendoComboBox").value(data.PaymentModeOnMaturity);
                                    }

                                    if (data.InterestPaymentType == 4 || data.InterestPaymentType == 5 || data.InterestPaymentType == 6) {
                                        $("#trSpecificDate").show();
                                    } else {
                                        $("#trSpecificDate").hide();
                                    }

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        }
                        else {
                            alertify.alert("This bank is not available for placement Deposito, please check PTP code on master Bank");
                            return;
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }

        }

        var _urlTrxType;
        if (_bs == 1) {
            _urlTrxType = window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 5;
        }
        else {
            _urlTrxType = window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 99;
        }

        $.ajax({
            url: _urlTrxType,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType
                });
                if (_bs == 1) {
                    $("#TrxType").data("kendoComboBox").enable(false);
                    $("#TrxType").data("kendoComboBox").select(1);
                } else {
                    $("#TrxType").data("kendoComboBox").enable(true);
                    $("#TrxType").data("kendoComboBox").value("");
                }

                if (_GlobClientCode == "25") {
                    if ($("#TrxType").val() == 1) {
                        $("#lblAmountToTransfer").show();
                    }
                    else {
                        $("#lblAmountToTransfer").hide();
                    }
                }
                else if (_GlobClientCode == "17") {
                    console.log($("#TrxType").val());
                    if ($("#TrxType").val() == 1) {
                        $("#lblAmountToTransfer").show();
                    }
                    else if ($("#TrxType").val() == 2) {
                        $("#lblAmountToTransfer").show();
                    }
                    else if ($("#TrxType").val() == 3) {
                        $("#lblAmountToTransfer").show();
                    }
                    else {
                        $("#lblAmountToTransfer").hide();
                    }
                }
                else {
                    $("#lblAmountToTransfer").hide();
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

            if (_GlobClientCode == "03") {
                //console.log($("#TrxType").val());
                if ($("#TrxType").val() == 3) {
                    $("#InterestDaysType").data("kendoComboBox").enable(true);
                    $("#InterestPaymentType").data("kendoComboBox").enable(true);
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(true);
                }
                else {
                    $("#InterestDaysType").data("kendoComboBox").enable(false);
                    $("#InterestPaymentType").data("kendoComboBox").enable(false);
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(false);
                }

            }


            if ($("#TrxType").val() == 3) {
                $("#MaturityDate").data("kendoDatePicker").enable(true);
                $("#Amount").data("kendoNumericTextBox").enable(true);
                $("#InterestPercent").data("kendoNumericTextBox").enable(true);
                $("#LblBreakInterestPercent").hide();
                $("#LblStatutoryType").show();
                $("#BreakInterestPercent").attr("required", false);
                //BIT ROLL OVER INTEREST
                $("#LblBitRollOverInterest").show();
                $("#LblBitRollOverInterestBuy").show();
                //END BIT ROLL OVER INTEREST
                if (_GlobClientCode == "17") {
                    $("#lblAmountToTransfer").show();
                }
                else {
                    $("#lblAmountToTransfer").hide();
                }

                $("#PaymentModeOnMaturity").data("kendoComboBox").enable(true);


            } else if ($("#TrxType").val() == 2) {
                $("#MaturityDate").data("kendoDatePicker").enable(false);
                $("#LblBreakInterestPercent").show();
                $("#LblStatutoryType").show();
                $("#BreakInterestPercent").attr("required", true);
                //BIT ROLL OVER INTEREST
                $("#LblBitRollOverInterest").hide();
                $("#LblBitRollOverInterestBuy").hide();
                //END BIT ROLL OVER INTEREST

                if (_GlobClientCode == "17") {
                    $("#lblAmountToTransfer").show();
                }
                else {
                    $("#lblAmountToTransfer").hide();
                }
            }

            else {
                $("#MaturityDate").data("kendoDatePicker").enable(false);
                $("#LblBreakInterestPercent").hide();
                $("#LblStatutoryType").show();
                $("#BreakInterestPercent").attr("required", false);
                //BIT ROLL OVER INTEREST
                $("#LblBitRollOverInterest").hide();
                $("#LblBitRollOverInterestBuy").hide();
                //END BIT ROLL OVER INTEREST
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
            _clearData();
        }




        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/INV",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#FundCashRefPK").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundCashRefPK,
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeFundCashRefPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        $("#AmountToTransfer").kendoNumericTextBox({
            format: "n0"
        });

        $("#Amount").kendoNumericTextBox({
            format: "n0",
            change: onChangeAmount,
        });

        function onChangeAmount() {
            var _Amount;
            if ($("#Amount").val() == null || $("#Amount").val() == undefined || $("#Amount").val() == "")
                _Amount = 0;
            else
                _Amount = $("#Amount").val();

            $("#AmountToTransfer").data("kendoNumericTextBox").value(_Amount);
        }



        $("#InterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
        });

        $("#OldInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
        });

        $("#BreakInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
        });



        $("#TextAmount").kendoMaskedTextBox({
            change: onchangeTextAmount,
        });


        function onchangeTextAmount() {
            var str = $("#TextAmount").data("kendoMaskedTextBox").value();
            if (str.includes("m") || str.includes("M")) {
                var strs = parseFloat(str);
                var strnum = strs * 1000000;
                var num = 0;

                num = Number(strnum);
                $("#Amount").data("kendoNumericTextBox").value(num);
                $("#AmountToTransfer").data("kendoNumericTextBox").value(num);
            }
            else if (str.includes("b") || str.includes("B")) {
                var strs = parseFloat(str);
                var strnum = strs * 1000000000;
                var num = 0;

                num = Number(strnum);
                $("#Amount").data("kendoNumericTextBox").value(num);
                $("#AmountToTransfer").data("kendoNumericTextBox").value(num);
            }
            else {
                alertify.alert("Text Must be 'm'/'M'/'b'/'B'");
            }

        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboIntDaysTypeDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestDaysType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestDaysType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeInterestDaysType,
                    enable: false,
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        function onChangeInterestDaysType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestPaymentType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestPaymentType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeInterestPaymentType
                });

                if (_bs == 1) {
                    $("#InterestPaymentType").data("kendoComboBox").enable(true);
                } else {
                    $("#InterestPaymentType").data("kendoComboBox").enable(false);
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        function onChangeInterestPaymentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 4 || this.value() == 5 || this.value() == 6) {
                $("#trSpecificDate").show();
            } else {
                $("#trSpecificDate").hide();
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PaymentModeOnMaturity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PaymentModeOnMaturity").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    enable: false,
                    dataSource: data,
                    change: onChangePaymentModeOnMaturity
                });

                if (_bs == 1) {
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(true);
                } else {
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(false);
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });






        function onChangePaymentModeOnMaturity() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }




        $("#SettledDate").data("kendoDatePicker").value($("#InstructionDate").data("kendoDatePicker").value());


        if (_bs == 1) {
            _clearAttributes();
            $("#LblAcqDate").hide();
            $("#LblInstrumentPK").hide();
            $("#MaturityDate").data("kendoDatePicker").enable(true);
            $("#LblBreakInterestPercent").hide();
            $("#BreakInterestPercent").attr("required", false);
            $("#LblRollOverInterest").hide();
            $("#LblStatutoryType").show();

            if (_GlobClientCode == "17") {
                $("#lblAmountToTransfer").show();
            }
            else {
                $("#lblAmountToTransfer").hide();
            }
        }
        else if ($("#TrxType").val() == 3) {
            _clearAttributes();
            $("#LblAcqDate").show();
            $("#LblInstrumentPK").show();
            $("#AcqDate").data("kendoDatePicker").enable(false);
            $("#MaturityDate").data("kendoDatePicker").enable(true);
            $("#BreakInterestPercent").attr("required", false);
            $("#LblBreakInterestPercent").hide();
            $("#LblRollOverInterest").show();
            $("#LblStatutoryType").show();
            //$("#RowBankBranch").hide();
            //$("#BankBranchPK").attr("required", false);
            if (_GlobClientCode == "17") {
                $("#lblAmountToTransfer").show();
            }
            else {
                $("#lblAmountToTransfer").hide();
            }
        }
        else if ($("#TrxType").val() == 2) {
            _clearAttributes();
            $("#LblAcqDate").show();
            $("#LblInstrumentPK").show();
            $("#LblBreakInterestPercent").show();
            $("#LblRollOverInterest").hide();
            $("#LblStatutoryType").show();
            $("#BreakInterestPercent").attr("required", true);
            if (_GlobClientCode == "17") {
                $("#lblAmountToTransfer").show();
            }
            else {
                $("#lblAmountToTransfer").hide();
            }
        } else {
            _clearAttributes();
            $("#LblAcqDate").show();
            $("#LblInstrumentPK").show();
            $("#LblBreakInterestPercent").show();
            $("#LblRollOverInterest").hide();
            $("#LblStatutoryType").show();
            $("#BreakInterestPercent").attr("required", true);
            //$("#RowBankBranch").hide();
            //$("#BankBranchPK").attr("required", false);
        }


        if (_bs == 1) {

            $("#InterestPercent").data("kendoNumericTextBox").enable(true);
            $("#MaturityDate").data("kendoDatePicker").enable(true);
            $("#AcqDate").data("kendoDatePicker").enable(true);
            $("#Category").data("kendoComboBox").enable(true);

        }
        else if (_bs == 3) {

            $("#InterestPercent").data("kendoNumericTextBox").enable(true);
            $("#Amount").data("kendoNumericTextBox").enable(true);
        }
        else {
            $("#Amount").data("kendoNumericTextBox").enable(true);
            $("#InterestPercent").data("kendoNumericTextBox").enable(false);
            $("#MaturityDate").data("kendoDatePicker").enable(false);
            $("#AcqDate").data("kendoDatePicker").enable(false);
            $("#Category").data("kendoComboBox").enable(false);
        }

        win.center();
        win.open();

    }

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
                                 BitBreakable: { type: "boolean" },
                                 ID: { type: "string" },
                                 BankID: { type: "string" },
                                 InterestPercent: { type: "number" },
                                 MaturityDate: { type: "date" },
                                 AcqDate: { type: "date" },
                                 Balance: { type: "number" },
                                 CurrencyID: { type: "string" },
                                 Category: { type: "string" },
                             }
                         }
                     }
                 });
    }

    function initListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").data("kendoComboBox").value() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
        var dsListInstrument = getDataSourceListInstrument(_url);

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
            dataBound: gridInstrumentDataBound,
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
               { field: "InstrumentPK", title: "", hidden: true, width: 100 },
               { field: "BitBreakable", title: "Breakable", width: 120, template: "#= BitBreakable ? 'Yes' : 'No' #" },
               { field: "ID", title: "ID", width: 200 },
               { field: "BankID", title: "Bank", width: 200 },
               { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
               { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
               { field: "InterestPercent", title: "Interest Percent", width: 200 },
               { field: "CurrencyID", title: "Currency ID", width: 200 },
               { field: "Category", title: "Category", width: 200 },
               { field: "NoFP", title: "NoFP",hidden:true, width: 200 },
               

            ]
        });

    }

    function gridInstrumentDataBound() {
        var grid = $("#gridListInstrument").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Flag == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApprove");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function ListInstrumentSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridListInstrument").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            //Validasi Breakable
            if (dataItemX.BitBreakable == false && $("#TrxType").val() == 2) {
                alertify.alert("Instrument is Not Breakable !");
                return;
            }
            else {
                _AmountValue = 0;
                $("#NoFP").val(dataItemX.NoFP);
                $("#InstrumentID").val(dataItemX.ID);
                $("#InstrumentPK").val(dataItemX.InstrumentPK);
                $("#MaturityDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((dataItemX.MaturityDate), 'dd/MMM/yyyy')));
                $("#OldMaturityDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((dataItemX.MaturityDate), 'dd/MMM/yyyy')));
                $("#OldMaturityDate").hide();

                $("#AcqDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((dataItemX.AcqDate), 'dd/MMM/yyyy')));
                $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
                $("#OldInterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
                $("#BankPK").data("kendoComboBox").value(dataItemX.BankPK);
                $("#BankBranchPK").data("kendoComboBox").value(dataItemX.BankBranchPK);
                $("#PaymentModeOnMaturity").data("kendoComboBox").value(dataItemX.PaymentModeOnMaturity);
                $("#OldPaymentModeOnMaturity").val(dataItemX.PaymentModeOnMaturity);
                $("#InterestPaymentType").data("kendoComboBox").value(dataItemX.InterestPaymentType);
                $("#OldInterestPaymentType").val(dataItemX.InterestPaymentType);
                $("#InterestDaysType").data("kendoComboBox").value(dataItemX.InterestDaysType);
                $("#OldInterestDaysType").val(dataItemX.InterestDaysType);
                $("#CurrencyPK").data("kendoComboBox").value(dataItemX.CurrencyPK);
                $("#Category").data("kendoComboBox").value(dataItemX.Category);
                $("#Amount").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#AmountToTransfer").data("kendoNumericTextBox").value(dataItemX.AmountToTransfer);
                $("#BitBreakable").prop('checked', dataItemX.BitBreakable);

                if ($("#BitRollOverInterest").is(':checked')) {
                    recallRollOverInterest();
                }

                $.ajax({
                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.BankPK,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        $("#BankBranchPK").kendoComboBox({
                            dataValueField: "BankBranchPK",
                            dataTextField: "ID",
                            dataSource: data,
                            filter: "contains",
                            change: onChangeBankBranchPK,
                            suggest: true
                        });
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
                WinListInstrument.close();
            }

        }
        e.handled = true;
    }

    function onChangeBankBranchPK() {
        console.log("Test")
        if (_GlobClientCode == "99") {
            $.ajax({
                url: window.location.origin + "/Radsoft/BankBranch/GetInterestDaysType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankBranchPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data.InterestDaysType != 0)
                        $("#InterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                    else
                        $("#InterestDaysType").data("kendoComboBox").value("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        else {

            $.ajax({
                url: window.location.origin + "/Radsoft/BankBranch/GetInterestDaysType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankBranchPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data.InterestDaysType != 0)
                        $("#InterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                    else
                        $("#InterestDaysType").data("kendoComboBox").value("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    }

    $("#btnClearListInstrument").click(function () {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#AcqDate").data("kendoDatePicker").value("");
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#InterestPercent").data("kendoNumericTextBox").value("");
        $("#BankPK").data("kendoComboBox").value("");
        $("#BankBranchPK").data("kendoComboBox").value("");

    });

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    var globInvestmentPK = 0;
    var globDealingPK = 0;
    var globHistoryPK = 0;
    var globStatusInvestment = '';
    var globPeriodPK = 0;
    var globValueDate = '';
    var globReference = '';
    var globInstructionDate = '';
    var globNotes = '';
    var globInstrumentType = 0;

    var element;
    var posY;

    $("#BtnSave").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        element = $("#BtnSave");
        posY = element.offset();

        if (kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") == kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy")) {
            alertify.alert("Maturity Date is same with Instruction Date, Save cancel").moveTo(posY.left, posY.top);
            return;
        }
        if ($('#Category').val() == "Negotiabale Certificate Deposit") {
            globInstrumentType = 10;
        }
        else {
            globInstrumentType = 5;
        }

        var _urlForReference;
        globNotes = $("#InvestmentNotes").val();
        var val = validateData();
        if (val == 1) {
            $.blockUI();
            if (_ParamFundScheme == 'TRUE') {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolioWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val();
            }
            else {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
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

                                    if ($("#InvestmentNotes").val() != '') {
                                        globNotes = $("#InvestmentNotes").val();
                                    }

                                    var _instrumentPK;
                                    var _reference;
                                    if ($('#TrxType').val() == 1 || $('#TrxType').val() == 3) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Amount').val() + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val() + "/" + $('#ParamDays').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == true) {
                                                    $.unblockUI();
                                                    alertify.confirm("Cash Not Available, </br> Are You Sure To Continue ?", function (e) {
                                                        if (e) {
                                                            $.blockUI();
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BankPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val() + "/" + $('#CurrencyPK').val(),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    _instrumentPK = data;
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/3",
                                                                        type: 'GET',
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (datacheck) {
                                                                            if (datacheck.AlertExposure == 10 && $('#TrxType').val() == 1) {
                                                                                $.unblockUI();
                                                                                alertify.alert("Cannot Save Data, Please Check Exposure Per Fund Per Bank !")
                                                                                return;
                                                                            }

                                                                            else if (datacheck.AlertExposure == 1 || datacheck.AlertExposure == 2 || datacheck.AlertExposure == 5 || datacheck.AlertExposure == 6) {
                                                                                $.unblockUI();
                                                                                InitGridInformationExposure(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);

                                                                            }


                                                                            else {
                                                                                if (_GlobClientCode == "20")
                                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TD/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                                else
                                                                                    _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                                                $.ajax({
                                                                                    url: _urlForReference,
                                                                                    type: 'GET',
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (reference) {
                                                                                        _reference = reference;
                                                                                        AddInvestmentTicket(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);


                                                                                        //$.ajax({
                                                                                        //    url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BankPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val() + "/" + $('#CurrencyPK').val(),
                                                                                        //    type: 'GET',
                                                                                        //    contentType: "application/json;charset=utf-8",
                                                                                        //    success: function (data) {
                                                                                        //        _instrumentPK = data;
                                                                                        //        AddInvestmentTicket(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);
                                                                                        //        $.unblockUI();

                                                                                        //    },
                                                                                        //    error: function (data) {
                                                                                        //        $.unblockUI();
                                                                                        //        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                                        //    }
                                                                                        //});
                                                                                    },
                                                                                    error: function (data) {
                                                                                        $.unblockUI();
                                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                                    }
                                                                                });
                                                                            }
                                                                        }
                                                                    });
                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                }
                                                            });


                                                        }
                                                    });
                                                }
                                                else {
                                                    if (_GlobClientCode == "20")
                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TD/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
                                                    else
                                                        _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                                    $.ajax({
                                                        url: _urlForReference,
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (reference) {
                                                            _reference = reference;

                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BankPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val() + "/" + $('#CurrencyPK').val(),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    _instrumentPK = data;
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _instrumentPK + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/3",
                                                                        type: 'GET',
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (datacheck) {
                                                                            if (datacheck.AlertExposure == 10 && $('#TrxType').val() == 1) {
                                                                                $.unblockUI();
                                                                                alertify.alert("Cannot Save Data, Please Check Exposure Per Fund Per Bank !")
                                                                                return;
                                                                            }

                                                                            else if (datacheck.AlertExposure == 1 || datacheck.AlertExposure == 2 || datacheck.AlertExposure == 5 || datacheck.AlertExposure == 6) {
                                                                                $.unblockUI();
                                                                                InitGridInformationExposure();

                                                                            }
                                                                            else {
                                                                                AddInvestmentTicket(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);

                                                                            }
                                                                        }
                                                                    });
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
                                    else if ($('#TrxType').val() == 2) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == 1) {
                                                    $.unblockUI();
                                                    alertify.alert("Instrument Not Avaliable").moveTo(posY.left, posY.top - 200);
                                                    return;
                                                }
                                                else if (data == 2) {
                                                    $.unblockUI();
                                                    alertify.alert("Short Sell").moveTo(posY.left, posY.top - 200);
                                                    return;
                                                }
                                                else {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/3",
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (datacheck) {
                                                            if (datacheck.AlertExposure == 3 || datacheck.AlertExposure == 4 || datacheck.AlertExposure == 7 || datacheck.AlertExposure == 8) {
                                                                $.unblockUI();
                                                                InitGridInformationExposure();

                                                            }
                                                            else {
                                                                if (_GlobClientCode == "20")
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TD/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
                                                                else
                                                                    _urlForReference = window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";

                                                                $.ajax({
                                                                    url: _urlForReference,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (reference) {
                                                                        _reference = reference;

                                                                        if ($('#InstrumentPK').val() == null || $('#InstrumentPK').val() == 0) {
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BankPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val() + "/" + $('#CurrencyPK').val(),
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    _instrumentPK = data;
                                                                                    AddInvestmentTicket(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);

                                                                                },
                                                                                error: function (data) {
                                                                                    $.unblockUI();
                                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                                                }
                                                                            });
                                                                        } else {
                                                                            _instrumentPK = $('#InstrumentPK').val();
                                                                            AddInvestmentTicket(_instrumentPK, _reference, $('#TrxType').val(), globInstrumentType);

                                                                        }
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
                                            },
                                            error: function (data) {
                                                $.unblockUI();
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    }
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
                    // $.unblockUI();
                }

            });
        }
    });

    function AddInvestmentTicket(_instrumentPK, _reference, _trxType, globInstrumentType) {
        $.blockUI();
        if (_trxType == 1) {

            if (_GlobClientCode == "17") {
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    AmountToTransfer: $('#AmountToTransfer').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    StatutoryType: $('#StatutoryType').val(),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user")

                };
            }
            if (_GlobClientCode == "03") {
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    StatutoryType: $('#StatutoryType').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user")

                };
            }
            else {
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    AmountToTransfer: $('#AmountToTransfer').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    StatutoryType: $('#StatutoryType').val(),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user")

                };
            }


        }
        else {
            var _acqdate = "";
            var _notes = "";
            if (_trxType == 2) {
                _acqdate = kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM-dd-yy");
                _notes = "LIQUIDATE";
            }
            else {
                _acqdate = kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy");
                _notes = "ROLLOVER";
            }
            if (_GlobClientCode == "03") {
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: _acqdate,
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    StatutoryType: $('#StatutoryType').val(),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user"),
                    TrxBuy: $('#NoFP').val(),

                };
            }

            else if (_GlobClientCode == "17") {
                console.log("testCok")
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    AmountToTransfer: $('#AmountToTransfer').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: _acqdate,
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    StatutoryType: $('#StatutoryType').val(),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user"),
                    TrxBuy: $('#NoFP').val(),
                }


            }
            else {
                var InvestmentInstruction = {
                    PeriodPK: _defaultPeriodPK,
                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    Reference: _reference,
                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InstrumentPK: _instrumentPK,
                    InstrumentTypePK: globInstrumentType,
                    OrderPrice: 1,
                    RangePrice: 0,
                    AcqPrice: 1,
                    Lot: 0,
                    CurrencyPK: $('#CurrencyPK').val(),
                    LotInShare: 0,
                    Volume: $('#Amount').val(),
                    Amount: $('#Amount').val(),
                    BankBranchPK: $('#BankBranchPK').val(),
                    FundCashRefPK: 1,
                    BankPK: $('#BankPK').val(),
                    TrxType: $('#TrxType').val(),
                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                    CounterpartPK: 0,
                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    AcqDate: _acqdate,
                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    InterestPercent: $('#InterestPercent').val(),
                    FundPK: $('#FundPK').val(),
                    MarketPK: 1,
                    InvestmentNotes: $('#InvestmentNotes').val(),
                    Category: $("#Category").data("kendoComboBox").value(),
                    InterestDaysType: $('#InterestDaysType').val(),
                    InterestPaymentType: $('#InterestPaymentType').val(),
                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                    BitBreakable: $('#BitBreakable').is(":checked"),
                    StatutoryType: $('#StatutoryType').val(),
                    //BIT ROLL OVER INTEREST
                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                    //END BIT ROLL OVER INTEREST
                    EntryUsersID: sessionStorage.getItem("user"),
                    TrxBuy: $('#NoFP').val(),
                }


            };
        }

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
            },
            error: function (data) {
                $.unblockUI();
                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
            }
        });
    }

    $("#BtnAddBuy").on("click", function () {
        BtnAddClick(1);
    });

    $("#BtnAddSell").on("click", function () {
        BtnAddClick(2);
    });

    $("#BtnExport").click(function (e) {
        
        alertify.confirm("Are you sure want to Export Data to Excel ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/ExportOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnApproveBySelectedTimeDepositBuy").click(function (e) {

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
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateApproveBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/ApproveOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Approved Investment Success");
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

    $("#BtnRejectBySelectedTimeDepositBuy").click(function (e) {

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
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateRejectBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/RejectOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    $("#BtnApproveBySelectedTimeDepositSell").click(function (e) {

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
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateApproveBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/ApproveOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Approved Investment Success");

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

    $("#BtnRejectBySelectedTimeDepositSell").click(function (e) {

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
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateRejectBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/RejectOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    $("#BtnApproveBySelectedTimeDepositBuySell").click(function (e) {

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

            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateInvestment = {
                        InstrumentTypePK: 5,
                        TrxType: 4,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateApproveBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 4,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/ApproveOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Approved Investment Success");

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

    $("#BtnRejectBySelectedTimeDepositBuySell").click(function (e) {

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

            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateInvestment = {
                        InstrumentTypePK: 5,
                        TrxType: 4,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/ValidateRejectBySelectedDataOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 5,
                                    TrxType: 4,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/RejectOMSTimeDepositBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    function _clearAttributes() {
        $("#MaturityDate").attr("required", true);
        $("#BankBranchPK").attr("required", true);
        $("#RowBankBranch").show();
        $("#MaturityDate").data("kendoDatePicker").enable(true);
        $("#InterestPercent").data("kendoNumericTextBox").enable(true);
        $("#Amount").data("kendoNumericTextBox").enable(true);
        $("#BitBreakable").val("");
        //$("#BitBreakable").prop('checked', false);
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


    function showDetailsBuy(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var grid;
        _resetButtonUpdateBuySell();
        $("#BtnUpdateInvestmentBuy").show();
        $("#BtnUpdateInvestmentSell").hide();

        grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        _showdetails(dataItemX,1);

    }

    function showDetailsSell(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var grid;

        _resetButtonUpdateBuySell();
        $("#BtnUpdateInvestmentSell").show();
        $("#BtnUpdateInvestmentBuy").hide();

        grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        if (dataItemX.StatusDesc != "VIEW ONLY") {
            _showdetails(dataItemX,2);
        }
        else {
            alertify.alert("Can't Show View Only");
            return;
        }


    }

    function _showdetails(dataItemX, _BuySell) {
        $("#BtnUpdateInvestmentBuy").hide();
        $("#BtnUpdateInvestmentSell").hide();
        if (dataItemX.OrderStatusDesc == "1.PENDING" || dataItemX.OrderStatusDesc == "2.APPROVED" || dataItemX.OrderStatusDesc == "5.PARTIAL" || dataItemX.OrderStatusDesc == "3.OPEN") {
            if (_BuySell == 1) {
                $("#BtnUpdateInvestmentBuy").show();
            }
            else {
                $("#BtnUpdateInvestmentSell").show();
            }

        }
        $("#UptrSpecificDate").hide();
        $("#trSpecificDate").hide();

        //if (dataItemX.TrxTypeID != "LIQUIDATE" && dataItemX.OrderStatusDesc == "1.PENDING" || dataItemX.OrderStatusDesc == "2.APPROVED" || dataItemX.OrderStatusDesc == "5.PARTIAL" || dataItemX.OrderStatusDesc == "3.OPEN") {
        //    $("#BtnUpdateInvestmentBuy").hide();
        //    $("#BtnUpdateInvestmentSell").show();
        //}
        //else
        //{
        //    $("#BtnUpdateInvestmentBuy").show();
        //    $("#BtnUpdateInvestmentSell").hide();

        //}
        $("#UpInvestmentPK").val(dataItemX.InvestmentPK);
        $("#UpNoFP").val(dataItemX.TrxBuy);
        $("#UpDealingPK").val(dataItemX.DealingPK);
        $("#UpReference").val(dataItemX.Reference);
        $("#UpHistoryPK").val(dataItemX.HistoryPK);
        $("#UpStatusInvestment").val(dataItemX.StatusInvestment);
        if (dataItemX.TrxTypeID == "PLACEMENT") {
            $("#UpTrxType").val(1);
            $("#LblStatutoryType").show();
        }
        else if (dataItemX.TrxTypeID == "LIQUIDATE") {
            $("#UpTrxType").val(2);
            $("#LblStatutoryType").show();
        }
        else {
            $("#UpTrxType").val(3);
            $("#LblStatutoryType").show();
            //BIT ROLL OVER INTEREST
            $("#LblBitRollOverInterest").show();
            $("#LblBitRollOverInterestBuy").show();
            //END BIT ROLL OVER INTEREST
        }

        $("#UpBitBreakable").prop('checked', dataItemX.BitBreakable);
        //BIT ROLL OVER INTEREST
        $("#BitRollOverInterest").prop('checked', dataItemX.BitRollOverInterest);
        $("#BitRollOverInterestBuy").prop('checked', dataItemX.BitRollOverInterest);
        //END BIT ROLL OVER INTEREST
        $("#UpTrxTypeID").val(dataItemX.TrxTypeID);
        $("#UpInvestmentNotes").val(dataItemX.InvestmentNotes);
        $("#UpInstructionDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate(dataItemX.InstructionDate), 'dd/MMM/yyyy'));
        $("#EntryUsersID").val(dataItemX.EntryUsersID);
        $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
        $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
        $("#VoidUsersID").val(dataItemX.VoidUsersID);
        $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));


        if (dataItemX.TrxType == 2) {
            $("#UpBitBreakable").prop("disabled", true);
        }
        else {
            $("#UpBitBreakable").prop("disabled", false);
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestPaymentType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpInterestPaymentType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeUpInterestPaymentType,
                    value: setUpInterestPaymentType()
                });

                if (dataItemX.TrxType == 1) {
                    $("#UpInterestPaymentType").data("kendoComboBox").enable(true);
                } else {
                    $("#UpInterestPaymentType").data("kendoComboBox").enable(false);
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        function onChangeUpInterestPaymentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            if (this.value() == 4 || this.value() == 5 || this.value() == 6) {
                $("#trSpecificDate").show();
            } else {
                $("#trSpecificDate").hide();
            }
        }


        function setUpInterestPaymentType() {
            if (dataItemX.InterestPaymentType == null) {
                return "";
            } else {
                if (dataItemX.InterestPaymentType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPaymentType;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestDaysType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpInterestDaysType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeUpInterestDaysType,
                    value: setUpInterestDaysType()
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });





        function onChangeUpInterestDaysType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        function setUpInterestDaysType() {
            if (dataItemX.InterestDaysType == null) {
                return "";
            } else {
                if (dataItemX.InterestDaysType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestDaysType;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PaymentModeOnMaturity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpPaymentModeOnMaturity").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeUpPaymentModeOnMaturity,
                    value: setUpPaymentModeOnMaturity()
                });
                if (dataItemX.TrxType == 1) {
                    $("#UpPaymentModeOnMaturity").data("kendoComboBox").enable(true);
                } else {
                    $("#UpPaymentModeOnMaturity").data("kendoComboBox").enable(false);
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        function onChangeUpPaymentModeOnMaturity() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        function setUpPaymentModeOnMaturity() {
            if (dataItemX.PaymentModeOnMaturity == null) {
                return "";
            } else {
                if (dataItemX.PaymentModeOnMaturity == 0) {
                    return "";
                } else {
                    return dataItemX.PaymentModeOnMaturity;
                }
            }
        }

        $("#UpCategory").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Normal", value: "Deposit Normal" },
                { text: "On Call", value: "Deposit On Call" }
            ],
            filter: "contains",
            enable: false,
            suggest: true,
            value: setUpCategory(),
        });

        $("#UpStatutoryType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: 1 },
                { text: "No", value: 0 }
            ],
            filter: "contains",
            value: setUpCmbStatutoryType()
        });

        function setUpCmbStatutoryType() {
            if (dataItemX.StatutoryType == null) {
                return false;
            } else {
                return dataItemX.StatutoryType;
            }
        }



        $("#UpAcqDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: onChangeDateFrom,
            value: kendo.toString(kendo.parseDate(dataItemX.AcqDate), 'dd/MMM/yyyy HH:mm:ss')
        });


        //$("#UpInstructionDate").kendoDatePicker({
        //    format: "dd/MMM/yyyy",
        //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        //    change: onChangeDateFrom,
        //    value: kendo.toString(kendo.parseDate(dataItemX.InstructionDate), 'dd/MMM/yyyy HH:mm:ss')
        //});

        $("#UpSpecificDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: onChangeDateFrom,
            value: kendo.toString(kendo.parseDate(dataItemX.PaymentInterestSpecificDate), 'dd/MMM/yyyy')
        });

        function setUpCategory() {
            if (dataItemX.Category == null) {
                return "";
            } else {
                if (dataItemX.Category == 0) {
                    return "";
                } else {
                    return dataItemX.Category;
                }
            }
        }

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
                    change: onChangeUpFundPK,
                    value: setUpFundPK(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeUpFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            _clearUpData();
        }

        function setUpFundPK() {
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCurrencyByPTPCode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.BankPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });
                if (dataItemX.TrxType == 1) {
                    $("#UpCurrencyPK").data("kendoComboBox").enable(true);
                } else {
                    $("#UpCurrencyPK").data("kendoComboBox").enable(false);
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#UpCurrencyPK").kendoComboBox({
        //            dataValueField: "CurrencyPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: OnChangeUpCurrencyPK,
        //            value: setUpCurrencyPK(),

        //        });
        //        if (dataItemX.TrxType == 1) {
        //            $("#UpCurrencyPK").data("kendoComboBox").enable(true);
        //        } else {
        //            $("#UpCurrencyPK").data("kendoComboBox").enable(false);
        //        }


        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        function setUpCurrencyPK() {
            if (dataItemX.CurrencyPK == null) {
                return "";
            } else {
                if (dataItemX.CurrencyPK == 0) {
                    return 1;
                } else {
                    return dataItemX.CurrencyPK;
                }
            }
        }
        function OnChangeUpCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //var _urlTrxType;
        //if(dataItemX.TrxType == 1)
        //{
        //   _urlTrxType = window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 5
        //}else
        //{
        //   _urlTrxType = window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 99
        //}



        //$.ajax({
        //    url: _urlTrxType,
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#UpTrxType").kendoComboBox({
        //            dataValueField: "Code",
        //            dataTextField: "DescOne",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: onChangeTrxType,
        //            //value: setUpTrxType(),
        //        });

        //        if (dataItemX.TrxType == 1) {
        //            $("#UpTrxType").data("kendoComboBox").enable(false);
        //        } else {
        //            $("#UpTrxType").data("kendoComboBox").enable(true);
        //        }

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //function setUpTrxType() {
        //    if (dataItemX.TrxType == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.TrxType == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.TrxType;
        //        }
        //    }
        //}


        //function onChangeTrxType() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //    if ($("#UpTrxType").val() == 1) {
        //        _clearAttributes();
        //        $("#UpMaturityDate").data("kendoDatePicker").enable(true);
        //    }
        //    else if ($("#TrxType").val() == 3) {
        //        _clearAttributes();
        //        $("#UpAcqDate").data("kendoDatePicker").enable(false);
        //        $("#UpMaturityDate").data("kendoDatePicker").enable(true);
        //        //$("#UpRowBankBranch").hide();
        //        //$("#UpBankBranchPK").attr("required", false);
        //    }
        //    else {
        //        _clearAttributes();
        //        //$("#UpRowBankBranch").hide();
        //        //$("#UpBankBranchPK").attr("required", false);

        //    }

        //}

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
                    change: onChangeUpFundPK,
                    value: setUpFundPK(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeUpFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            _clearUpData();
        }
        function setUpFundPK() {
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



        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpBankPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpBankPK,
                    value: setUpBankPK(),

                });
                if (dataItemX.TrxType == 1) {
                    $("#UpBankPK").data("kendoComboBox").enable(true);
                } else {
                    $("#UpBankPK").data("kendoComboBox").enable(false);
                }


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function setUpBankPK() {
            if (dataItemX.BankPK == null) {
                return "";
            } else {
                if (dataItemX.BankPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankPK;
                }
            }
        }

        function onChangeUpBankPK() {
            if (dataItemX.BankPK != null) {
                if (this.value() == dataItemX.BankPK) {
                    return;
                }
            }
            $("#UpCurrencyPK").data("kendoComboBox").value("");
            $("#UpBankBranchPK").data("kendoComboBox").value("");
            $("#UpFundCashRefPK").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Bank/ValidateCheckBankPTPCode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/GetBankCurrencyByPTPCode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankPK").data("kendoComboBox").value(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $("#UpCurrencyPK").kendoComboBox({
                                        dataValueField: "CurrencyPK",
                                        dataTextField: "ID",
                                        dataSource: data,
                                        filter: "contains",
                                        suggest: true,
                                        index: 0
                                    });
                                    $("#UpCurrencyPK").data("kendoComboBox").enable(true);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                            if (_GlobClientCode == "99") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankPK").data("kendoComboBox").value(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {

                                        $("#UpBankBranchPK").kendoComboBox({
                                            dataValueField: "BankBranchPK",
                                            dataTextField: "ID",
                                            dataSource: data,
                                            filter: "contains",
                                            suggest: true,
                                            change: onChangeUpBankBranchPK,
                                            //index: 0
                                        });
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankPK").data("kendoComboBox").value(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {

                                        $("#UpBankBranchPK").kendoComboBox({
                                            dataValueField: "BankBranchPK",
                                            dataTextField: "ID",
                                            dataSource: data,
                                            filter: "contains",
                                            suggest: true,
                                            change: onChangeUpBankBranchPK,
                                        });
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }


                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/GetBankInformation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankPK").data("kendoComboBox").value(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    //$("#UpInterestDaysType ").data("kendoComboBox").value(data.InterestDaysType);
                                    $("#UpInterestPaymentType ").data("kendoComboBox").value(data.InterestPaymentType);
                                    $("#UpPaymentModeOnMaturity ").data("kendoComboBox").value(data.PaymentModeOnMaturity);

                                    if (data.InterestPaymentType == 4 || data.InterestPaymentType == 5 || data.InterestPaymentType == 6) {
                                        $("#UptrSpecificDate").show();
                                    } else {
                                        $("#UptrSpecificDate").hide();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        }
                        else {
                            alertify.alert("This bank is not available for placement Deposito, please check PTP code on master Bank").moveTo(posY.left, posY.top - 200);
                            return;
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                    }
                });

            }

        }


        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.BankPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpBankBranchPK").kendoComboBox({
                    dataValueField: "BankBranchPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpBankBranchPK,
                    value: setUpBankBranchPK(),
                });

                if (dataItemX.TrxType == 1) {
                    $("#UpBankBranchPK").data("kendoComboBox").enable(true);
                } else {
                    $("#UpBankBranchPK").data("kendoComboBox").enable(false);
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function setUpBankBranchPK() {
            if (dataItemX.BankBranchPK == null) {
                return "";
                if (_GlobClientCode == "99") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/BankBranch/GetInterestDaysType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankBranchPK").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data.InterestDaysType != 0)
                                $("#InterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                            else
                                $("#InterestDaysType").data("kendoComboBox").value("");
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            } else {
                if (dataItemX.BankBranchPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankBranchPK;
                }
            }
        }

        function onChangeUpBankBranchPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if (_GlobClientCode == "99") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/BankBranch/GetInterestDaysType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankBranchPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data.InterestDaysType != 0)
                            $("#InterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                        else
                            $("#InterestDaysType").data("kendoComboBox").value("");
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/BankBranch/GetInterestDaysType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpBankBranchPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data.InterestDaysType != 0)
                            $("#UpInterestDaysType").data("kendoComboBox").value(data.InterestDaysType);
                        else
                            $("#UpInterestDaysType").data("kendoComboBox").value("");
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/INV",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#UpFundCashRefPK").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpFundCashRefPK,
                    value: setUpFundCashRefPK(),
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setUpFundCashRefPK() {
            if (dataItemX.FundCashRefPK == null) {
                return "";
            } else {
                if (dataItemX.FundCashRefPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundCashRefPK;
                }
            }
        }
        function onChangeUpFundCashRefPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#UpAmountToTransfer").kendoNumericTextBox({
            format: "n0",
            value: setUpUpAmountToTransfer(),
        });
        function setUpUpAmountToTransfer() {
            console.log(dataItemX.AmountToTransfer);

            if (dataItemX.AmountToTransfer == null) {
                return "";
            } else {
                if (dataItemX.AmountToTransfer == 0) {
                    return "";
                } else {
                    return dataItemX.AmountToTransfer;
                }
            }
        }

        $("#UpAmount").kendoNumericTextBox({
            format: "n0",
            value: setUpAmount(),
            change: onChangeUpAmount
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

        function onChangeUpAmount() {
            var _Amount;
            if ($("#UpAmount").val() == null || $("#UpAmount").val() == undefined || $("#UpAmount").val() == "")
                _Amount = 0;
            else
                _Amount = $("#UpAmount").val();

            $("#UpAmountToTransfer").data("kendoNumericTextBox").value(_Amount);
        }



        $("#UpInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
        });


        $("#UpBreakInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
        });

        if (dataItemX.TrxTypeID == "PLACEMENT") {
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            $("#UpMaturityDate").data("kendoDatePicker").enable(true);
            $("#UpAcqDate").data("kendoDatePicker").enable(true);
            $("#LblUpBreakInterestPercent").hide();
            $("#LblStatutoryType").show();
            if (_GlobClientCode == "17") {
                $("#lblAmountToTransfer").show();
            }
            else {
                $("#lblAmountToTransfer").hide();
            }

        } else if (dataItemX.TrxTypeID == "LIQUIDATE") {
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(false);
            $("#UpMaturityDate").data("kendoDatePicker").enable(false);
            $("#UpAcqDate").data("kendoDatePicker").enable(false);
            $("#LblUpBreakInterestPercent").hide();
            $("#LblStatutoryType").show();
            if (_GlobClientCode == "17") {
                $("#lblUpAmountToTransfer").show();
            }
            else {
                $("#lblUpAmountToTransfer").hide();
            }
        }
        else if (dataItemX.TrxTypeID == "ROLLOVER") {
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            $("#UpMaturityDate").data("kendoDatePicker").enable(false);
            $("#UpAcqDate").data("kendoDatePicker").enable(false);
            $("#LblUpBreakInterestPercent").hide();
            $("#LblStatutoryType").show();
            //BIT ROLL OVER INTEREST
            $("#LblBitRollOverInterest").show();
            $("#LblBitRollOverInterestBuy").show();
            //END BIT ROLL OVER INTEREST
            if (_GlobClientCode == "17") {
                $("#lblUpAmountToTransfer").show();
            }
            else {
                $("#lblUpAmountToTransfer").hide();
            }
        }

        else {
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            $("#UpMaturityDate").data("kendoDatePicker").enable(false);
            $("#UpAcqDate").data("kendoDatePicker").enable(false);
            $("#LblUpBreakInterestPercent").hide();
            $("#LblStatutoryType").show();
            //BIT ROLL OVER INTEREST
            $("#LblBitRollOverInterest").show();
            $("#LblBitRollOverInterestBuy").show();
            //END BIT ROLL OVER INTEREST
        }



        $("#UpInterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
        $("#UpBreakInterestPercent").data("kendoNumericTextBox").value(dataItemX.BreakInterestPercent);
        $("#UpMaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);

        $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
        $("#UpInstrumentID").val(dataItemX.InstrumentID + ' - ' + dataItemX.InstrumentName);


        if (dataItemX.TrxTypeID == "PLACEMENT") {
            _clearUpAttributes();
            $("#UpMaturityDate").data("kendoDatePicker").enable(true);
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            console.log(dataItemX.TrxTypeID)
            if (_GlobClientCode == "25") {
                if (dataItemX.TrxTypeID == "PLACEMENT") {
                    $("#lblUpAmountToTransfer").show();
                }
                else {
                    $("#lblUpAmountToTransfer").hide();
                }
            }
            else if (_GlobClientCode == "17") {
                if (dataItemX.TrxTypeID == "PLACEMENT") {
                    $("#lblUpAmountToTransfer").show();
                }
                else {
                    $("#lblUpAmountToTransfer").hide();
                }
            }
            else {
                $("#lblUpAmountToTransfer").hide();
            }
        }
        else if (dataItemX.TrxTypeID == "LIQUIDATE") {
            _clearUpAttributes();
            $("#UpMaturityDate").data("kendoDatePicker").enable(false);
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(false);
            $("#UpAmount").data("kendoNumericTextBox").enable(true);
            if (_GlobClientCode == "17") {
                if (dataItemX.TrxTypeID == "LIQUIDATE") {
                    $("#lblUpAmountToTransfer").show();
                }
                else {
                    $("#lblUpAmountToTransfer").hide();
                }
            }

            else {
                $("#lblUpAmountToTransfer").hide();
            }
            //$("#UpRowBankBranch").hide();
            //$("#UpBankBranchPK").attr("required", false);
        }
        else if (dataItemX.TrxTypeID == "ROLLOVER") {
            _clearUpAttributes();
            $("#UpAcqDate").data("kendoDatePicker").enable(false);
            $("#UpMaturityDate").data("kendoDatePicker").enable(true);
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            $("#UpAmount").data("kendoNumericTextBox").enable(true);

            if (_GlobClientCode == "17") {
                if (dataItemX.TrxTypeID == "ROLLOVER") {
                    $("#lblUpAmountToTransfer").show();
                }
                else {
                    $("#lblUpAmountToTransfer").hide();
                }
            }

            else {
                $("#lblUpAmountToTransfer").hide();
            }
            //$("#UpRowBankBranch").hide();
            //$("#UpBankBranchPK").attr("required", false);
        }

        else {
            _clearUpAttributes();
            $("#UpAcqDate").data("kendoDatePicker").enable(false);
            $("#UpMaturityDate").data("kendoDatePicker").enable(true);
            $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
            $("#UpAmount").data("kendoNumericTextBox").enable(true);
            //$("#UpRowBankBranch").hide();
            //$("#UpBankBranchPK").attr("required", false);

        }

        WinUpdateInvestment.center();
        WinUpdateInvestment.open();
    }


    function initUpListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpTrxType").data("kendoComboBox").value() + "/" + $("#UpFundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy");
        var dsUpListInstrument = getDataSourceListInstrument(_url);

        $("#gridUpListInstrument").kendoGrid({
            dataSource: dsUpListInstrument,
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
               { field: "ID", title: "ID", width: 200 },
               { field: "BankID", title: "Bank", width: 200 },
               { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
               { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
               { field: "InterestPercent", title: "Interest Percent", width: 200 },
               { field: "CurrencyID", title: "Currency ID", width: 200 },
               { field: "Category", title: "Category", width: 200 },

            ]
        });

    }

    function UpListInstrumentSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridUpListInstrument").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            $("#UpInstrumentID").val(dataItemX.ID);
            $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
            $("#UpMaturityDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((dataItemX.MaturityDate), 'dd/MMM/yyyy')));
            $("#UpAcqDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((dataItemX.AcqDate), 'dd/MMM/yyyy')));
            $("#UpInterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
            $("#UpBankPK").data("kendoComboBox").value(dataItemX.BankPK);
            $("#UpCurrencyPK").val(dataItemX.CurrencyPK);
            $("#UpCategory").val(dataItemX.Category);
            $("#UpAmount").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#UpBitBreakable").prop('checked', dataItemX.BitBreakable);
            

            //$("#MaturityDate").val(dataItemX.MaturityDate);
            //$("#AcqDate").val(dataItemX.AcqDate);
            //$("#InterestPercent").val(dataItemX.InterestPercent);
            //$("#BankPK").val(dataItemX.BankPK);
            //$("#Category").val(dataItemX.Category);
            //$("#Amount").val(dataItemX.Balance);
            //WinUpListInstrument.close();
            //$.ajax({
            //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentInformationByInstrumentPKForOMSDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.InstrumentPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            //    type: 'GET',
            //    contentType: "application/json;charset=utf-8",
            //    success: function (data) {

            //        //$("#MaturityDate").data("kendoDatePicker").value(data.MaturityDate);
            //        $("#MaturityDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((data.MaturityDate), 'dd/MMM/yyyy')));
            //        $("#AcqDate").data("kendoDatePicker").value(kendo.toString(kendo.parseDate((data.AcqDate), 'dd/MM/yyyy')));
            //        $("#InterestPercent").data("kendoNumericTextBox").value(data.InterestPercent);
            //        $("#BankPK").data("kendoComboBox").value(data.BankPK);
            //        //$("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
            //        $("#Category").data("kendoComboBox").value(data.Category);
            //        $("#Amount").data("kendoNumericTextBox").value(dataItemX.Balance);

            $.ajax({
                url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboBankOnlyByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.BankPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    $("#UpBankBranchPK").kendoComboBox({
                        dataValueField: "BankBranchPK",
                        dataTextField: "ID",
                        dataSource: data,
                        filter: "contains",
                        suggest: true
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            WinUpListInstrument.close();
            //        $.ajax({
            //            url: window.location.origin + "/Radsoft/OMSTimeDeposit/RHBBankBranch/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
            //            type: 'GET',
            //            contentType: "application/json;charset=utf-8",
            //            success: function (data) {

            //                $("#BankBranchPK").kendoComboBox({
            //                    dataValueField: "PK",
            //                    dataTextField: "BankBranchCode",
            //                    dataSource: data,
            //                    filter: "contains",
            //                    suggest: true
            //                });
            //            },
            //            error: function (data) {
            //                alertify.alert(data.responseText);
            //            }
            //        });
            //        WinListInstrument.close();
            //    },
            //    error: function (data) {
            //        alertify.alert(data.responseText);
            //    }
            //});
        }
        e.handled = true;
    }

    $("#btnUpClearListInstrument").click(function () {
        $("#UpInstrumentPK").val("");
        $("#UpInstrumentID").val("");
        $("#UpAcqDate").data("kendoDatePicker").value("");
        $("#UpMaturityDate").data("kendoDatePicker").value("");
        $("#UpInterestPercent").data("kendoNumericTextBox").value("");
        $("#UpBankPK").data("kendoComboBox").value("");
        $("#UpBankBranchPK").data("kendoComboBox").value("");

    });

    
    $("#BtnUpdateInvestmentBuy").click(function () {
        var element = $("#BtnUpdateInvestmentBuy");
        var posY = element.offset();
        if (kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") == kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy")) {
            alertify.alert("Maturity Date is same with Instruction Date, Save cancel").moveTo(posY.left, posY.top);
            return;
        }
        var val = validateDataUpdate();
        if (val == 1) {
            alertify.confirm("Are you sure want to Update ?", function (e) {

                $.blockUI({});
                var Investment = {
                    InvestmentPK: $('#UpInvestmentPK').val(),
                    DealingPK: $('#UpDealingPK').val(),
                    HistoryPK: $('#UpHistoryPK').val(),
                    StatusInvestment: $('#UpStatusInvestment').val(),
                    OrderStatusDesc: 'OPEN',

                    TrxType: $('#UpTrxType').val(),
                    MarketPK: $('#UpMarketPK').val(),
                    InstrumentPK: $('#UpInstrumentPK').val(),
                    FundCashRefPK: 1,
                    OrderPrice: $('#UpOrderPrice').val(),
                    Lot: $('#UpLot').val(),
                    LotInShare: $('#UpLotInShare').val(),
                    Volume: $('#UpVolume').val(),
                    Amount: $('#UpAmount').val(),
                    InvestmentNotes: $('#UpInvestmentNotes').val(),
                    BitBreakable: $('#UpBitBreakable').is(":checked"),
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
                                                url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpBankPK').val() + "/" + $('#UpInterestPercent').val() + "/" + $('#UpCategory').val() + "/" + $('#UpCurrencyPK').val(),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    _instrumentPK = data;
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/3",
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (dataCheckExposure) {

                                                            if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                                $.unblockUI();
                                                                InitGridUpInformationExposure();

                                                            } else {
                                                                if ($('#UpCategory').val() == "Negotiabale Certificate Deposit") {
                                                                    globInstrumentType = 10;
                                                                }
                                                                else {
                                                                    globInstrumentType = 5;
                                                                }
                                                                // ADD DISINI
                                                                if ($('#UpTrxType').val() == 1) {

                                                                    var InvestmentInstruction = {
                                                                        PeriodPK: _defaultPeriodPK,
                                                                        ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        Reference: $('#UpReference').val(),
                                                                        InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InstrumentPK: _instrumentPK,
                                                                        InstrumentTypePK: globInstrumentType,
                                                                        OrderPrice: 1,
                                                                        RangePrice: 0,
                                                                        AcqPrice: 1,
                                                                        Lot: 0,
                                                                        CurrencyPK: $('#UpCurrencyPK').val(),
                                                                        LotInShare: 0,
                                                                        Volume: $('#UpAmount').val(),
                                                                        Amount: $('#UpAmount').val(),
                                                                        AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                        BankBranchPK: $('#UpBankBranchPK').val(),
                                                                        FundCashRefPK: 1,
                                                                        BankPK: $('#UpBankPK').val(),
                                                                        TrxType: 1,
                                                                        TrxTypeID: 'PLACEMENT',
                                                                        CounterpartPK: 0,
                                                                        SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InterestPercent: $('#UpInterestPercent').val(),
                                                                        FundPK: $('#UpFundPK').val(),
                                                                        MarketPK: 1,
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                        InterestDaysType: $('#UpInterestDaysType').val(),
                                                                        InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                        PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                        BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                    };
                                                                }
                                                                else if (($('#UpTrxType').val() == 2)) {
                                                                    var InvestmentInstruction = {
                                                                        PeriodPK: _defaultPeriodPK,
                                                                        ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        Reference: $('#UpReference').val(),
                                                                        InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InstrumentPK: _instrumentPK,
                                                                        InstrumentTypePK: globInstrumentType,
                                                                        OrderPrice: 1,
                                                                        RangePrice: 0,
                                                                        AcqPrice: 1,
                                                                        Lot: 0,
                                                                        CurrencyPK: $('#UpCurrencyPK').val(),
                                                                        LotInShare: 0,
                                                                        Volume: $('#UpAmount').val(),
                                                                        Amount: $('#UpAmount').val(),
                                                                        AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                        BankBranchPK: $('#UpBankBranchPK').val(),
                                                                        FundCashRefPK: 1,
                                                                        BankPK: $('#UpBankPK').val(),
                                                                        TrxType: 2,
                                                                        TrxTypeID: 'LIQUIDATE',
                                                                        CounterpartPK: 0,
                                                                        SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InterestPercent: $('#UpInterestPercent').val(),
                                                                        BreakInterestPercent: $('#UpBreakInterestPercent').val(),
                                                                        FundPK: $('#UpFundPK').val(),
                                                                        MarketPK: 1,
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                        InterestDaysType: $('#UpInterestDaysType').val(),
                                                                        InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                        PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                        BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                    };
                                                                }
                                                                else if (($('#UpTrxType').val() == 3)) {
                                                                    var InvestmentInstruction = {
                                                                        TrxBuy: $('#UpNoFP').val(),
                                                                        PeriodPK: _defaultPeriodPK,
                                                                        ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        Reference: $('#UpReference').val(),
                                                                        InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InstrumentPK: $('#UpInstrumentPK').val(),
                                                                        InstrumentTypePK: globInstrumentType,
                                                                        OrderPrice: 1,
                                                                        RangePrice: 0,
                                                                        AcqPrice: 1,
                                                                        Lot: 0,
                                                                        CurrencyPK: $('#UpCurrencyPK').val(),
                                                                        LotInShare: 0,
                                                                        Volume: $('#UpAmount').val(),
                                                                        Amount: $('#UpAmount').val(),
                                                                        AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                        BankBranchPK: $('#UpBankBranchPK').val(),
                                                                        FundCashRefPK: 1,
                                                                        BankPK: $('#UpBankPK').val(),
                                                                        TrxType: 3,
                                                                        TrxTypeID: 'ROLLOVER',
                                                                        CounterpartPK: 0,
                                                                        SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InterestPercent: $('#UpInterestPercent').val(),
                                                                        FundPK: $('#UpFundPK').val(),
                                                                        MarketPK: 1,
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                        InterestDaysType: $('#UpInterestDaysType').val(),
                                                                        InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                        PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                        BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                        //BIT ROLL OVER INTEREST
                                                                        BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                                                        //END BIT ROLL OVER INTEREST
                                                                        EntryUsersID: sessionStorage.getItem("user")
                                                                    }
                                                                }

                                                                else {
                                                                    var InvestmentInstruction = {
                                                                        TrxBuy: $('#UpNoFP').val(),
                                                                        PeriodPK: _defaultPeriodPK,
                                                                        ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        Reference: $('#UpReference').val(),
                                                                        InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InstrumentPK: $('#UpInstrumentPK').val(),
                                                                        InstrumentTypePK: globInstrumentType,
                                                                        OrderPrice: 1,
                                                                        RangePrice: 0,
                                                                        AcqPrice: 1,
                                                                        Lot: 0,
                                                                        CurrencyPK: $('#UpCurrencyPK').val(),
                                                                        LotInShare: 0,
                                                                        Volume: $('#UpAmount').val(),
                                                                        Amount: $('#UpAmount').val(),
                                                                        BankBranchPK: $('#UpBankBranchPK').val(),
                                                                        FundCashRefPK: 1,
                                                                        BankPK: $('#UpBankPK').val(),
                                                                        TrxType: 3,
                                                                        TrxTypeID: 'ROLLOVER',
                                                                        CounterpartPK: 0,
                                                                        SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                        InterestPercent: $('#UpInterestPercent').val(),
                                                                        FundPK: $('#UpFundPK').val(),
                                                                        MarketPK: 1,
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                        InterestDaysType: $('#UpInterestDaysType').val(),
                                                                        InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                        PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                        BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                        //BIT ROLL OVER INTEREST
                                                                        BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                                                        //END BIT ROLL OVER INTEREST
                                                                        EntryUsersID: sessionStorage.getItem("user")
                                                                    }
                                                                }

                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                    type: 'POST',
                                                                    data: JSON.stringify(InvestmentInstruction),
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        $.unblockUI();
                                                                        alertify.alert("Amend Success").moveTo(posY.left, posY.top);
                                                                        WinUpdateInvestment.close();
                                                                        refresh();
                                                                    },
                                                                    error: function (data) {
                                                                        $.unblockUI();
                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                                    }
                                                                });
                                                                $.unblockUI();
                                                            }
                                                        }
                                                    });

                                                },
                                                error: function (data) {
                                                    $.unblockUI();
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                }
                                            });
                                        }
                                    });

                                }
                                else {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpBankPK').val() + "/" + $('#UpInterestPercent').val() + "/" + $('#UpCategory').val() + "/" + $('#UpCurrencyPK').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            _instrumentPK = data;
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/3",
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (dataCheckExposure) {

                                                    if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                        $.unblockUI();
                                                        InitGridUpInformationExposure();

                                                    } else {
                                                        if ($('#UpCategory').val() == "Negotiabale Certificate Deposit") {
                                                            globInstrumentType = 10;
                                                        }
                                                        else {
                                                            globInstrumentType = 5;
                                                        }
                                                        // ADD DISINI
                                                        if ($('#UpTrxType').val() == 1) {

                                                            var InvestmentInstruction = {
                                                                PeriodPK: _defaultPeriodPK,
                                                                ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                Reference: $('#UpReference').val(),
                                                                InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InstrumentPK: _instrumentPK,
                                                                InstrumentTypePK: globInstrumentType,
                                                                OrderPrice: 1,
                                                                RangePrice: 0,
                                                                AcqPrice: 1,
                                                                Lot: 0,
                                                                CurrencyPK: $('#UpCurrencyPK').val(),
                                                                LotInShare: 0,
                                                                Volume: $('#UpAmount').val(),
                                                                Amount: $('#UpAmount').val(),
                                                                AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                BankBranchPK: $('#UpBankBranchPK').val(),
                                                                FundCashRefPK: 1,
                                                                BankPK: $('#UpBankPK').val(),
                                                                TrxType: 1,
                                                                TrxTypeID: 'PLACEMENT',
                                                                CounterpartPK: 0,
                                                                SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InterestPercent: $('#UpInterestPercent').val(),
                                                                FundPK: $('#UpFundPK').val(),
                                                                MarketPK: 1,
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                InterestDaysType: $('#UpInterestDaysType').val(),
                                                                InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                EntryUsersID: sessionStorage.getItem("user")

                                                            };
                                                        }
                                                        else if (($('#UpTrxType').val() == 2)) {
                                                            var InvestmentInstruction = {
                                                                PeriodPK: _defaultPeriodPK,
                                                                ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                Reference: $('#UpReference').val(),
                                                                InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InstrumentPK: _instrumentPK,
                                                                InstrumentTypePK: globInstrumentType,
                                                                OrderPrice: 1,
                                                                RangePrice: 0,
                                                                AcqPrice: 1,
                                                                Lot: 0,
                                                                CurrencyPK: $('#UpCurrencyPK').val(),
                                                                LotInShare: 0,
                                                                Volume: $('#UpAmount').val(),
                                                                Amount: $('#UpAmount').val(),
                                                                AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                BankBranchPK: $('#UpBankBranchPK').val(),
                                                                FundCashRefPK: 1,
                                                                BankPK: $('#UpBankPK').val(),
                                                                TrxType: 2,
                                                                TrxTypeID: 'LIQUIDATE',
                                                                CounterpartPK: 0,
                                                                SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InterestPercent: $('#UpInterestPercent').val(),
                                                                BreakInterestPercent: $('#UpBreakInterestPercent').val(),
                                                                FundPK: $('#UpFundPK').val(),
                                                                MarketPK: 1,
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                InterestDaysType: $('#UpInterestDaysType').val(),
                                                                InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                EntryUsersID: sessionStorage.getItem("user")

                                                            };
                                                        }
                                                        else if (($('#UpTrxType').val() == 3)) {
                                                            var InvestmentInstruction = {
                                                                TrxBuy: $('#UpNoFP').val(),
                                                                PeriodPK: _defaultPeriodPK,
                                                                ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                Reference: $('#UpReference').val(),
                                                                InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InstrumentPK: $('#UpInstrumentPK').val(),
                                                                InstrumentTypePK: globInstrumentType,
                                                                OrderPrice: 1,
                                                                RangePrice: 0,
                                                                AcqPrice: 1,
                                                                Lot: 0,
                                                                CurrencyPK: $('#UpCurrencyPK').val(),
                                                                LotInShare: 0,
                                                                Volume: $('#UpAmount').val(),
                                                                Amount: $('#UpAmount').val(),
                                                                AmountToTransfer: $('#UpAmountToTransfer').val(),
                                                                BankBranchPK: $('#UpBankBranchPK').val(),
                                                                FundCashRefPK: 1,
                                                                BankPK: $('#UpBankPK').val(),
                                                                TrxType: 3,
                                                                TrxTypeID: 'ROLLOVER',
                                                                CounterpartPK: 0,
                                                                SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InterestPercent: $('#UpInterestPercent').val(),
                                                                FundPK: $('#UpFundPK').val(),
                                                                MarketPK: 1,
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                InterestDaysType: $('#UpInterestDaysType').val(),
                                                                InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                //BIT ROLL OVER INTEREST
                                                                BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                                                //END BIT ROLL OVER INTEREST
                                                                EntryUsersID: sessionStorage.getItem("user")
                                                            }
                                                        }

                                                        else {
                                                            var InvestmentInstruction = {
                                                                TrxBuy: $('#UpNoFP').val(),
                                                                PeriodPK: _defaultPeriodPK,
                                                                ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                Reference: $('#UpReference').val(),
                                                                InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InstrumentPK: $('#UpInstrumentPK').val(),
                                                                InstrumentTypePK: globInstrumentType,
                                                                OrderPrice: 1,
                                                                RangePrice: 0,
                                                                AcqPrice: 1,
                                                                Lot: 0,
                                                                CurrencyPK: $('#UpCurrencyPK').val(),
                                                                LotInShare: 0,
                                                                Volume: $('#UpAmount').val(),
                                                                Amount: $('#UpAmount').val(),
                                                                BankBranchPK: $('#UpBankBranchPK').val(),
                                                                FundCashRefPK: 1,
                                                                BankPK: $('#UpBankPK').val(),
                                                                TrxType: 3,
                                                                TrxTypeID: 'ROLLOVER',
                                                                CounterpartPK: 0,
                                                                SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                InterestPercent: $('#UpInterestPercent').val(),
                                                                FundPK: $('#UpFundPK').val(),
                                                                MarketPK: 1,
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                Category: $("#UpCategory").data("kendoComboBox").value(),
                                                                InterestDaysType: $('#UpInterestDaysType').val(),
                                                                InterestPaymentType: $('#UpInterestPaymentType').val(),
                                                                PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                                                PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                                                BitBreakable: $('#UpBitBreakable').is(":checked"),
                                                                //BIT ROLL OVER INTEREST
                                                                BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                                                //END BIT ROLL OVER INTEREST
                                                                EntryUsersID: sessionStorage.getItem("user")
                                                            }
                                                        }

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                            type: 'POST',
                                                            data: JSON.stringify(InvestmentInstruction),
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                $.unblockUI();
                                                                alertify.alert("Amend Success").moveTo(posY.left, posY.top);
                                                                WinUpdateInvestment.close();
                                                                refresh();
                                                            },
                                                            error: function (data) {
                                                                $.unblockUI();
                                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                            }
                                                        });
                                                        $.unblockUI();
                                                    }
                                                }
                                            });

                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                        }
                                    });
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


    $("#BtnUpdateInvestmentSell").click(function () {
        var element = $("#BtnUpdateInvestmentSell");
        var posY = element.offset();
        if (kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") == kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy")) {
            alertify.alert("Maturity Date is same with Instruction Date, Save cancel").moveTo(posY.left, posY.top);
            return;
        }
        var val = validateDataUpdate();
        if (val == 1) {

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
                    InstrumentPK: $('#UpInstrumentPK').val(),
                    FundCashRefPK: 1,
                    OrderPrice: $('#UpOrderPrice').val(),
                    Lot: $('#UpLot').val(),
                    LotInShare: $('#UpLotInShare').val(),
                    Volume: $('#UpVolume').val(),
                    Amount: $('#UpAmount').val(),
                    AmountToTransfer: $('#UpAmountToTransfer').val(),
                    InvestmentNotes: $('#UpInvestmentNotes').val(),
                    BitBreakable: $('#UpBitBreakable').is(":checked"),
                    UpdateUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                    type: 'POST',
                    data: JSON.stringify(Investment),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/3",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (dataCheckExposure) {

                                if (dataCheckExposure.AlertExposure == 3 || dataCheckExposure.AlertExposure == 4 || dataCheckExposure.AlertExposure == 7 || dataCheckExposure.AlertExposure == 8) {
                                    $.unblockUI();
                                    InitGridUpInformationExposure();

                                } else {

                                    if ($('#UpCategory').val() == "Negotiabale Certificate Deposit") {
                                        globInstrumentType = 10;
                                    }
                                    else {
                                        globInstrumentType = 5;
                                    }

                                    var InvestmentInstruction = {
                                        PeriodPK: _defaultPeriodPK,
                                        ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        Reference: $('#UpReference').val(),
                                        InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InstrumentPK: $('#UpInstrumentPK').val(),
                                        InstrumentTypePK: globInstrumentType,
                                        OrderPrice: 1,
                                        RangePrice: 0,
                                        AcqPrice: 1,
                                        Lot: 0,
                                        CurrencyPK: $('#UpCurrencyPK').val(),
                                        LotInShare: 0,
                                        Volume: $('#UpAmount').val(),
                                        Amount: $('#UpAmount').val(),
                                        AmountToTransfer: $('#UpAmountToTransfer').val(),
                                        BankBranchPK: $('#UpBankBranchPK').val(),
                                        FundCashRefPK: 1,
                                        BankPK: $('#UpBankPK').val(),
                                        TrxType: 2,
                                        TrxTypeID: 'LIQUIDATE',
                                        CounterpartPK: 0,
                                        SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        AcqDate: kendo.toString($("#UpAcqDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InterestPercent: $('#UpInterestPercent').val(),
                                        BreakInterestPercent: $('#UpBreakInterestPercent').val(),
                                        FundPK: $('#UpFundPK').val(),
                                        MarketPK: 1,
                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                        Category: $("#UpCategory").data("kendoComboBox").value(),
                                        InterestDaysType: $('#UpInterestDaysType').val(),
                                        InterestPaymentType: $('#UpInterestPaymentType').val(),
                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                        PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                                        PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                                        TrxBuy: $('#UpNoFP').val(),
                                        BitBreakable: $('#UpBitBreakable').is(":checked"),
                                        //BIT ROLL OVER INTEREST
                                        BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                        //END BIT ROLL OVER INTEREST
                                        EntryUsersID: sessionStorage.getItem("user")

                                    };


                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                        type: 'POST',
                                        data: JSON.stringify(InvestmentInstruction),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            $.unblockUI();
                                            alertify.alert("Amend Success").moveTo(posY.left, posY.top);
                                            WinUpdateInvestment.close();
                                            refresh();
                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                        }
                                    });
                                    $.unblockUI();
                                }
                            }
                        });
                    },

                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                    }
                });


            });

        }

    });

    $("#BtnCancelUpdateInvestment").click(function () {
        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                WinUpdateInvestment.close();
                alertify.alert("Close Update");
            }
        });
    });


    function _resetButtonUpdateBuySell() {
        $("#BtnUpdateInvestmentBuy").hide();
        $("#BtnUpdateInvestmentSell").hide();
    }

    function _clearUpAttributes() {
        $("#UpMaturityDate").attr("required", true);
        $("#UpBankBranchPK").attr("required", true);
        $("#UpRowBankBranch").show();
        $("#UpMaturityDate").data("kendoDatePicker").enable(true);
        $("#UpInterestPercent").data("kendoNumericTextBox").enable(true);
        $("#UpAmount").data("kendoNumericTextBox").enable(true);
    }


    $("#BtnOMSTimeDepositListing").click(function () {
        showOMSTimeDepositListing();
    });


    // Untuk Form Listing

    function showOMSTimeDepositListing(e) {

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



        WinOMSTimeDepositListing.center();
        WinOMSTimeDepositListing.open();

    }

    $("#BtnOkOMSTimeDepositListing").click(function () {

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

        ////console.log(stringInvestmentFrom);


        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var OMSTimeDepositListing = {
                    Message: $('#Message').val(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    ParamFundID: $("#FilterFundID").data("kendoComboBox").value(),
                    ParamListDate: $('#DateFrom').val(),
                    BitIsMature: $('#BitIsMature').is(":checked"),
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    stringInvestmentFrom: stringInvestmentFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDepositListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(OMSTimeDepositListing),
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

    });

    $("#BtnCancelOMSTimeDepositListing").click(function () {

        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinOMSTimeDepositListing.close();
                alertify.success("Cancel Listing");
            }
        });
    });


    function onWinOMSTimeDepositListingClose() {
        $("#Message").val("")
        $('#BitIsMature').prop('checked', false);
        $("#detailMature").hide();
        $("#gridMature").empty();
    }


    $("#BitIsMature").click(function () {
        if ($("#BitIsMature").prop('checked') == true) {
            $("#detailMature").show();
            $("#gridMature").empty();
            initMature();
        }
        else {
            $("#detailMature").hide();
            $("#gridMature").empty();
        }


    });

    function initMature() {
        var dsListMature = InitDataSourceMature();
        var gridMature = $("#gridMature").kendoGrid({
            dataSource: dsListMature,
            height: 300,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [
                {
                    field: "Selected",
                    width: 30,
                    template: "<input class='cSelectedDetailMature' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedDetailMature' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
               { field: "Reference", title: "Reference", width: 150 },
               { field: "AcqDate", title: "Acq Date", width: 150, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
               { field: "FundID", title: "Fund", width: 120 },
               { field: "InstrumentID", title: "Bank", width: 120 },
               { field: "Volume", title: "Nominal", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
               { field: "InterestPercent", title: "Interest (%)", format: "{0:n4}", template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }, width: 120 },
            ]
        }).data("kendoGrid");

        $("#SelectedDetailMature").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataMature(_checked);

        });

        gridMature.table.on("click", ".cSelectedDetailMature", selectDataMature);

        function selectDataMature(e) {


            var grid = $("#gridMature").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _reference = dataItemX.Reference;
            var _checked = this.checked;
            SelectDeselectDataMature(_checked, _reference);

        }



    }

    function SelectDeselectDataMature(_a, _b) {
        var _s = _b.replace(/\//g, '-');
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectDataMature/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _s,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier', 'position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
    function SelectDeselectAllDataMature(_a) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectAllDataByDateMature/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail").prop('checked', _a);
                refreshMature();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function refreshMature() {

        var newDS = getDataSourceMature(window.location.origin + "/Radsoft/Investment/GetDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
        $("#gridMature").data("kendoGrid").setDataSource(newDS);


    }


    // Untuk List MATURE

    function InitDataSourceMature() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/Investment/InitDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/0",
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 25,
                      schema: {
                          model: {
                              fields: {
                                  Selected: { type: "boolean" },
                                  Reference: { type: "string" },
                                  FundID: { type: "string" },
                                  InstrumentID: { type: "string" },
                                  Volume: { type: "number" },
                                  AcqDate: { type: "date" },
                                  InterestPercent: { type: "number" }

                              }
                          }
                      }
                  });
    }


    function getDataSourceMature() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/Investment/GetDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 25,
                      schema: {
                          model: {
                              fields: {
                                  Selected: { type: "boolean" },
                                  Reference: { type: "string" },
                                  FundID: { type: "string" },
                                  InstrumentID: { type: "string" },
                                  Volume: { type: "number" },
                                  AcqDate: { type: "date" },
                                  InterestPercent: { type: "number" }

                              }
                          }
                      }
                  });
    }



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



    function InitGridInformationExposure(_instrumentPK, _reference, _trxType, globInstrumentType) {
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

    function onWinValidateFundExposureClose() {
        $.unblockUI({});
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {
            $.blockUI({});
            $.ajax({
                url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (reference) {
                    _reference = reference;

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Instrument/GetLastInstrumentByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/5",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            _instrumentPK = data;

                            if ($('#TrxType').val() == 1) {

                                if (_GlobClientCode == "17") {
                                    var InvestmentInstruction = {
                                        PeriodPK: _defaultPeriodPK,
                                        ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        Reference: _reference,
                                        InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InstrumentPK: _instrumentPK,
                                        InstrumentTypePK: globInstrumentType,
                                        OrderPrice: 1,
                                        RangePrice: 0,
                                        AcqPrice: 1,
                                        Lot: 0,
                                        CurrencyPK: $('#CurrencyPK').val(),
                                        LotInShare: 0,
                                        Volume: $('#Amount').val(),
                                        Amount: $('#Amount').val(),
                                        AmountToTransfer: $('#AmountToTransfer').val(),
                                        BankBranchPK: $('#BankBranchPK').val(),
                                        FundCashRefPK: 1,
                                        BankPK: $('#BankPK').val(),
                                        TrxType: $('#TrxType').val(),
                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                        CounterpartPK: 0,
                                        SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        AcqDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InterestPercent: $('#InterestPercent').val(),
                                        FundPK: $('#FundPK').val(),
                                        MarketPK: 1,
                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                        Category: $("#Category").data("kendoComboBox").value(),
                                        InterestDaysType: $('#InterestDaysType').val(),
                                        InterestPaymentType: $('#InterestPaymentType').val(),
                                        PaymentInterestSpecificDate: $('#SpecificDate').val(),
                                        PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                        BreakInterestPercent: $('#BreakInterestPercent').val(),
                                        BitBreakable: $('#BitBreakable').is(":checked"),
                                        EntryUsersID: sessionStorage.getItem("user")

                                    };
                                }
                                else {
                                    var InvestmentInstruction = {
                                        PeriodPK: _defaultPeriodPK,
                                        ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        Reference: _reference,
                                        InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InstrumentPK: _instrumentPK,
                                        InstrumentTypePK: globInstrumentType,
                                        OrderPrice: 1,
                                        RangePrice: 0,
                                        AcqPrice: 1,
                                        Lot: 0,
                                        CurrencyPK: $('#CurrencyPK').val(),
                                        LotInShare: 0,
                                        Volume: $('#Amount').val(),
                                        Amount: $('#Amount').val(),
                                        AmountToTransfer: $('#AmountToTransfer').val(),
                                        BankBranchPK: $('#BankBranchPK').val(),
                                        FundCashRefPK: 1,
                                        BankPK: $('#BankPK').val(),
                                        TrxType: $('#TrxType').val(),
                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                        CounterpartPK: 0,
                                        SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        AcqDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        InterestPercent: $('#InterestPercent').val(),
                                        FundPK: $('#FundPK').val(),
                                        MarketPK: 1,
                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                        Category: $("#Category").data("kendoComboBox").value(),
                                        InterestDaysType: $('#InterestDaysType').val(),
                                        InterestPaymentType: $('#InterestPaymentType').val(),
                                        PaymentInterestSpecificDate: $('#SpecificDate').val(),
                                        PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                        BreakInterestPercent: $('#BreakInterestPercent').val(),
                                        BitBreakable: $('#BitBreakable').is(":checked"),
                                        //BIT ROLL OVER INTEREST
                                        BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                        //END BIT ROLL OVER INTEREST
                                        EntryUsersID: sessionStorage.getItem("user")

                                    };
                                }


                            }
                            else {
                                var _acqdate = "";
                                var _notes = "";
                                if ($('#TrxType').val() == 2) {
                                    _acqdate = kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM-dd-yy");
                                    _notes = "LIQUIDATE";
                                    _instrumentPK = $('#InstrumentPK').val()
                                }
                                else {
                                    _acqdate = kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy");
                                    _notes = "ROLLOVER";
                                }
                                var InvestmentInstruction = {
                                    PeriodPK: _defaultPeriodPK,
                                    ValueDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    Reference: _reference,
                                    InstructionDate: kendo.toString($("#InstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    InstrumentPK: _instrumentPK,
                                    InstrumentTypePK: globInstrumentType,
                                    OrderPrice: 1,
                                    RangePrice: 0,
                                    AcqPrice: 1,
                                    Lot: 0,
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    LotInShare: 0,
                                    Volume: $('#Amount').val(),
                                    Amount: $('#Amount').val(),
                                    AmountToTransfer: $('#AmountToTransfer').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    FundCashRefPK: 1,
                                    BankPK: $('#BankPK').val(),
                                    TrxType: $('#TrxType').val(),
                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                    CounterpartPK: 0,
                                    SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    AcqDate: _acqdate,
                                    MaturityDate: kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    InterestPercent: $('#InterestPercent').val(),
                                    FundPK: $('#FundPK').val(),
                                    MarketPK: 1,
                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                    Category: $("#Category").data("kendoComboBox").value(),
                                    InterestDaysType: $('#InterestDaysType').val(),
                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                                    BitBreakable: $('#BitBreakable').is(":checked"),
                                    //BIT ROLL OVER INTEREST
                                    BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                                    //END BIT ROLL OVER INTEREST
                                    EntryUsersID: sessionStorage.getItem("user"),
                                    TrxBuy: $('#NoFP').val(),

                                };
                            }
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
                                                    alertify.alert("Insert Investment Success !");
                                                    win.close();
                                                    $.unblockUI({});
                                                    refresh();


                                                },
                                                error: function (data) {
                                                    $.unblockUI({});
                                                    alertify.alert(data.responseText);
                                                }
                                            });

                                        },
                                        error: function (data) {
                                            $.unblockUI({});
                                            alertify.alert(data.responseText);
                                        }


                                    });

                                },
                                error: function (data) {
                                    $.unblockUI({});
                                    alertify.alert(data.responseText);
                                }


                            });
                        },
                        error: function (data) {
                            $.unblockUI({});
                            alertify.alert(data.responseText);
                        }
                    });
                },
                error: function (data) {
                    $.unblockUI({});
                    alertify.alert(data.responseText);
                }
            });

        });

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
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {

            if ($('#UpCategory').val() == "Negotiabale Certificate Deposit") {
                globInstrumentType = 10;
            }
            else {
                globInstrumentType = 5;
            }

            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetLastInstrumentByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/5",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    _instrumentPK = data;
                    // ADD DISINI
                    if ($('#UpTrxType').val() == 1) {

                        var InvestmentInstruction = {
                            PeriodPK: _defaultPeriodPK,
                            ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            Reference: $('#UpReference').val(),
                            InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InstrumentPK: _instrumentPK,
                            InstrumentTypePK: globInstrumentType,
                            OrderPrice: 1,
                            RangePrice: 0,
                            AcqPrice: 1,
                            Lot: 0,
                            CurrencyPK: $('#UpCurrencyPK').val(),
                            LotInShare: 0,
                            Volume: $('#UpAmount').val(),
                            Amount: $('#UpAmount').val(),
                            BankBranchPK: $('#UpBankBranchPK').val(),
                            FundCashRefPK: 1,
                            BankPK: $('#UpBankPK').val(),
                            TrxType: 1,
                            TrxTypeID: 'PLACEMENT',
                            CounterpartPK: 0,
                            SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InterestPercent: $('#UpInterestPercent').val(),
                            FundPK: $('#UpFundPK').val(),
                            MarketPK: 1,
                            InvestmentNotes: $('#InvestmentNotes').val(),
                            Category: $("#UpCategory").data("kendoComboBox").value(),
                            InterestDaysType: $('#UpInterestDaysType').val(),
                            InterestPaymentType: $('#UpInterestPaymentType').val(),
                            PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                            PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                            BitBreakable: $('#UpBitBreakable').is(":checked"),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                    }
                    else if (($('#UpTrxType').val() == 2)) {
                        var InvestmentInstruction = {
                            PeriodPK: _defaultPeriodPK,
                            ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            Reference: $('#UpReference').val(),
                            InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InstrumentPK: $('#UpInstrumentPK').val(),
                            InstrumentTypePK: globInstrumentType,
                            OrderPrice: 1,
                            RangePrice: 0,
                            AcqPrice: 1,
                            Lot: 0,
                            CurrencyPK: $('#UpCurrencyPK').val(),
                            LotInShare: 0,
                            Volume: $('#UpAmount').val(),
                            Amount: $('#UpAmount').val(),
                            BankBranchPK: $('#UpBankBranchPK').val(),
                            FundCashRefPK: 1,
                            BankPK: $('#UpBankPK').val(),
                            TrxType: 2,
                            TrxTypeID: 'LIQUIDATE',
                            CounterpartPK: 0,
                            SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InterestPercent: $('#UpInterestPercent').val(),
                            BreakInterestPercent: $('#UpBreakInterestPercent').val(),
                            FundPK: $('#UpFundPK').val(),
                            MarketPK: 1,
                            InvestmentNotes: $('#InvestmentNotes').val(),
                            Category: $("#UpCategory").data("kendoComboBox").value(),
                            InterestDaysType: $('#UpInterestDaysType').val(),
                            InterestPaymentType: $('#UpInterestPaymentType').val(),
                            PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                            PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                            BitBreakable: $('#UpBitBreakable').is(":checked"),
                            //BIT ROLL OVER INTEREST
                            BitRollOverInterest: $('#BitRollOverInterest').is(":checked"),
                            //END BIT ROLL OVER INTEREST
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                    } else {
                        var InvestmentInstruction = {
                            TrxBuy: $('#UpNoFP').val(),
                            PeriodPK: _defaultPeriodPK,
                            ValueDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            Reference: $('#UpReference').val(),
                            InstructionDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InstrumentPK: $('#UpInstrumentPK').val(),
                            InstrumentTypePK: globInstrumentType,
                            OrderPrice: 1,
                            RangePrice: 0,
                            AcqPrice: 1,
                            Lot: 0,
                            CurrencyPK: $('#UpCurrencyPK').val(),
                            LotInShare: 0,
                            Volume: $('#UpAmount').val(),
                            Amount: $('#UpAmount').val(),
                            BankBranchPK: $('#UpBankBranchPK').val(),
                            FundCashRefPK: 1,
                            BankPK: $('#UpBankPK').val(),
                            TrxType: 3,
                            TrxTypeID: 'ROLLOVER',
                            CounterpartPK: 0,
                            SettledDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            AcqDate: kendo.toString($("#UpInstructionDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            MaturityDate: kendo.toString($("#UpMaturityDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            InterestPercent: $('#UpInterestPercent').val(),
                            FundPK: $('#UpFundPK').val(),
                            MarketPK: 1,
                            InvestmentNotes: $('#InvestmentNotes').val(),
                            Category: $("#UpCategory").data("kendoComboBox").value(),
                            InterestDaysType: $('#UpInterestDaysType').val(),
                            InterestPaymentType: $('#UpInterestPaymentType').val(),
                            PaymentModeOnMaturity: $('#UpPaymentModeOnMaturity').val(),
                            PaymentInterestSpecificDate: $('#UpSpecificDate').val(),
                            BitBreakable: $('#UpBitBreakable').is(":checked"),
                            EntryUsersID: sessionStorage.getItem("user")

                        }
                    }

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
                                            $.unblockUI();
                                            alertify.alert("Amend Success")
                                            WinUpdateInvestment.close();
                                            refresh();
                                        },
                                        error: function (data) {
                                            $.unblockUI();
                                            alertify.alert(data.responseText)
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


            //return;
        }, function () {
            var Investment = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                OrderStatusDesc: '1.PENDING',

                TrxType: $('#UpTrxType').val(),
                MarketPK: $('#UpMarketPK').val(),
                InstrumentPK: $('#UpInstrumentPK').val(),
                FundCashRefPK: 1,
                OrderPrice: $('#UpOrderPrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitBreakable: $('#UpBitBreakable').is(":checked"),
                UpdateUsersID: sessionStorage.getItem("user")
            };
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

    function _clearData() {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#AcqDate").data("kendoDatePicker").value("");
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#InterestPercent").data("kendoNumericTextBox").value("");
        $("#BankPK").data("kendoComboBox").value("");
        $("#BankBranchPK").data("kendoComboBox").value("");
        $("#Category").data("kendoComboBox").value("Deposit Normal");
        $("#CurrencyPK").data("kendoComboBox").value("");
        $("#Amount").data("kendoNumericTextBox").value("");
        $("#AmountToTransfer").data("kendoNumericTextBox").value("");
        //BIT ROLL OVER INTEREST
        $("#BitRollOverInterest").prop('checked', false);
        //END BIT ROLL OVER INTEREST
        $("#InterestDaysType").data("kendoComboBox").value("");
        $("#InterestPaymentType").data("kendoComboBox").value("");
        $("#PaymentModeOnMaturity").data("kendoComboBox").value("");
    }

    function _clearUpData() {
        $("#UpInstrumentPK").val("");
        $("#UpInstrumentID").val("");
        $("#UpAcqDate").data("kendoDatePicker").value("");
        $("#UpMaturityDate").data("kendoDatePicker").value("");
        $("#UpInterestPercent").data("kendoNumericTextBox").value("");
        $("#UpBankPK").data("kendoComboBox").value("");
        $("#UpBankBranchPK").data("kendoComboBox").value("");
        $("#UpCategory").data("kendoComboBox").value("");
        $("#UpCurrencyPK").data("kendoComboBox").value("");
        $("#UpAmount").data("kendoNumericTextBox").value("");
        $("#UpAmountToTransfer").data("kendoNumericTextBox").value("");
        //END BIT ROLL OVER INTEREST
        $("#BitRollOverInterest").prop('checked', false);
        //BIT ROLL OVER INTEREST
        $("#UpInterestDaysType").data("kendoComboBox").value("");
        $("#UpInterestPaymentType").data("kendoComboBox").value("");
        $("#UpPaymentModeOnMaturity").data("kendoComboBox").value("");
    }

    $("#BtnImportOmsDeposito").click(function () {
        document.getElementById("FileImportOmsDeposito").click();
    });

    $("#FileImportOmsDeposito").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportOmsDeposito").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ImportOmsDepositoTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ImportOmsDeposito_Import/ " + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {

                    console.log(data);
                    if (data == "Import OMS Time Deposit Success") {
                        //cek ada exposure atau gak
                        $.ajax({
                            url: window.location.origin + "/Radsoft/OMSTimeDeposit/CheckExposureFromImport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 1) {
                                    $.unblockUI();
                                    console.log("ada Exposure");
                                    $("#FileImportOmsDeposito").val("");
                                    InitGridExposureImport();
                                }
                                else {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSTimeDeposit/InsertIntoInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $.unblockUI();
                                            $("#FileImportOmsDeposito").val("");
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
                        $("#FileImportOmsDeposito").val("");
                    }


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOmsDeposito").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOmsDeposito").val("");
        }
    });

    function InitGridExposureImport() {
        WinFundExposureFromImport.center();
        WinFundExposureFromImport.open();


        $("#gridFundExposureFromImport").empty();

        var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureFromImportOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                url: window.location.origin + "/Radsoft/OMSTimeDeposit/InsertIntoInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_ExposureFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (_ComplianceEmail == 'TRUE') {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailFundExposureFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $.unblockUI();
                                        alertify.alert("Import OMS Success !");
                                        refresh();
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


    $("#BtnGenerateOmsDeposito").click(function () {

        //var All = 0;
        //All = [];
        //for (var i in checkedEquityBuy) {
        //    if (checkedEquityBuy[i]) {
        //        All.push(i);
        //    }
        //}

        //for (var i in checkedEquitySell) {
        //    if (checkedEquitySell[i]) {
        //        All.push(i);
        //    }
        //}

        //var ArrayInvestmentFrom = All;
        //var stringInvestmentFrom = '';

        //for (var i in ArrayInvestmentFrom) {
        //    stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        //}
        //stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)


        //if (stringInvestmentFrom == "") {
        //    alertify.alert("There's No Selected Data");
        //}
        //else {

        alertify.confirm("Are you sure want to Download OMS Deposito data ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSTimeDeposit/GenerateOMSDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI({});
                        window.location = data;

                    },
                    error: function (data) {
                        $.unblockUI({});
                        alertify.alert(data.responseText);
                    }

                });
            }
        });
        //}

    });


});

