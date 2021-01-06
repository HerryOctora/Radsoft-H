$(document).ready(function () {

    var WinListInstrument;
    var WinListInstrumentByCrossFund;
    var WinListAvailableCashDetail;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var checkedInvestmentBuy = {};
    var checkedInvestmentSell = {};
    var _statusExposureRangePrice;
    var _globinstrumentPK, _globinstrumentTypePK, _globorderPrice, _globvolume, _globamount, _globcrossFundFromPK, _globsettledDate, _globlastCouponDate, _globnextCouponDate, _globmaturityDate,
        _globinterestPercent, _globaccruedInterest, _globtenor, _globinvestmentNotes, _globincomeTaxInterestAmount, _globincomeTaxGainAmount, _globamount, _globtotalAmount, _globinvestmentTrType,
        _globpriceMode, _globbitIsAmortized, _globyieldPercent, _globfundPK, _globbitIsRounding, _globbitForeignTrx, _globbitHTM, _globbIRate, _globinvestmentStrategy, _globinvestmentStyle,
        _globinvestmentObjective, _globrevision, _globotherInvestmentObjective, _globotherInvestmentStyle, _globotherRevision;

    var _globUpinvestmentPK, _globUphistoryPK, _globUpdealingPK, _globUpreference, _globUpinstrumentPK, _globUpinstrumentTypePK, _globUporderPrice, _globUpvolume, _globUpamount, _globUpcrossFundFromPK, _globUpsettledDate, _globUplastCouponDate, _globUpnextCouponDate, _globUpmaturityDate,
        _globUpinterestPercent, _globUpaccruedInterest, _globUptenor, _globUpinvestmentNotes, _globUpincomeTaxInterestAmount, _globUpincomeTaxGainAmount, _globUpamount, _globUptotalAmount, _globUpinvestmentTrType,
        _globUppriceMode, _globUpbitIsAmortized, _globUpyieldPercent, _globUpfundPK, _globUpbitIsRounding, _globUpbitForeignTrx, _globUpbitHTM, _globUpbIRate, _globUpinvestmentStrategy, _globUpinvestmentStyle,
        _globUpinvestmentObjective, _globUprevision, _globUpotherInvestmentObjective, _globUpotherInvestmentStyle, _globUpotherRevision, _globUporderStatus, _globUpstatusInvestment, _globUpacqDate, _globUpacqPrice, _globUpacqVolume, _globUptrxBuy, _globUptrxBuyType;

    var GlobValidator = $("#WinInvestmentInstruction").kendoValidator().data("kendoValidator");

    var GlobValidatorUpdate = $("#WinUpdateInvestment").kendoValidator().data("kendoValidator");
    var _d = new Date();
    var _fy = _d.getFullYear();
    var _defaultPeriodPK;

    var globUpdate = 0;
    var globPaymentType = 0;

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

        $("#UpInvestmentStyle").attr("required", true);
        $("#UpOtherInvestmentStyle").attr("required", true);
        $("#UpInvestmentObjective").attr("required", true);
        $("#UpOtherInvestmentObjective").attr("required", true);
        $("#UpRevision").attr("required", true);
        $("#UpOtherRevision").attr("required", true);
        $("#UpInvestmentStrategy").attr("required", true);
    }
    else {
        $("#InvestmentStyle").attr("required", false);
        $("#OtherInvestmentStyle").attr("required", false);
        $("#InvestmentObjective").attr("required", false);
        $("#OtherInvestmentObjective").attr("required", false);
        $("#Revision").attr("required", false);
        $("#OtherRevision").attr("required", false);
        $("#InvestmentStrategy").attr("required", false);

        $("#UpInvestmentStyle").attr("required", false);
        $("#UpOtherInvestmentStyle").attr("required", false);
        $("#UpInvestmentObjective").attr("required", false);
        $("#UpOtherInvestmentObjective").attr("required", false);
        $("#UpRevision").attr("required", false);
        $("#UpOtherRevision").attr("required", false);
        $("#UpInvestmentStrategy").attr("required", false);

    }




    ResetSelected();

    WinValidateCrossFundBuyExposure = $("#WinValidateCrossFundBuyExposure").kendoWindow({
        height: 500,
        title: "* Fund Exposure",
        visible: false,
        width: 1400,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
        close: onWinValidateCrossFundBuyExposureClose
    }).data("kendoWindow");


    WinUpValidateCrossFundBuyExposure = $("#WinUpValidateCrossFundBuyExposure").kendoWindow({
        height: 500,
        title: "* Fund Exposure",
        visible: false,
        width: 1400,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
        close: onWinUpValidateCrossFundBuyExposureClose
    }).data("kendoWindow");


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

    $("#DateFrom").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        change: onChangeDateFrom,
        value: new Date()
    });

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

    if (_GlobClientCode == "05") {
        $("#LblBitHTM").hide();
        $("#LblUpBitHTM").hide();
    }
    else {
        $("#LblBitHTM").show();
        $("#LblUpBitHTM").show();
    }


    $("#LastCouponDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    });
    $("#LastCouponDate").data("kendoDatePicker").enable(false);

    $("#NextCouponDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    });
    $("#NextCouponDate").data("kendoDatePicker").enable(false);
    $("#MaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#MaturityDate").data("kendoDatePicker").enable(false);

    $("#SettledDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        change: OnChangeSettledDate,
        value: _setSettledDate()
    });

    $("#AcqDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    });
    //$("#AcqDate").data("kendoDatePicker").enable(false);

    //$("#AcqDate").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate1").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate2").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate3").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate4").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate5").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate6").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate7").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate8").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});
    //$("#AcqDate9").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    //    change: OnChangeAcqDate
    //});

    function _setSettledDate() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SettledDate").data("kendoDatePicker").value(new Date(data));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }



    //function OnChangeLastCouponDate() {
    //        if ($("#LastCouponDate").data("kendoDatePicker").value() != null
    //                  && $("#LastCouponDate").data("kendoDatePicker").value() != ''
    //                   && $("#SettledDate").data("kendoDatePicker").value() != ''
    //                   && $("#SettledDate").data("kendoDatePicker").value() != null
    //                  && $('#InstrumentPK').val() != ''
    //                  && $('#InstrumentPK').val() != null
    //                  && $("#OrderPrice").data("kendoNumericTextBox").value() != ''
    //                  && $("#OrderPrice").data("kendoNumericTextBox").value() != null
    //                  && $("#Volume").data("kendoNumericTextBox").value() != ''
    //                  && $("#Volume").data("kendoNumericTextBox").value() != null
    //                  ) {
    //            GetBondInterest();

    //        }
    //        GetNextCouponDate();


    //}


    //function OnChangeAcqDate() {
    //    RecalNetAmount();
    //}

    function OnChangeSettledDate() {

        //if ($("#SettledDate").val() < $("#DateFrom").val()) {
        //    alertify.alert("Please Check Settle Date < Value Date !");
        //    return 0;
        //}

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                return;
            },
            error: function (data) {
                alertify.alert("Please Fill Value Date or Instrument");
                $("#InstrumentPK").val("");
                $("#InstrumentID").val("");
            }
        });

        recalYield();
        RecalNetAmount();

    }


    //-----------------------------------------------------------

    $("#UpLastCouponDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    });

    $("#UpNextCouponDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    });
    $("#UpNextCouponDate").data("kendoDatePicker").enable(false);
    $("#UpMaturityDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    $("#UpMaturityDate").data("kendoDatePicker").enable(false);

    $("#UpSettledDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        change: OnChangeUpSettledDate,
        value: _setUpSettledDate()
    });

    $("#UpAcqDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
    });

    //$("#UpAcqDate").data("kendoDatePicker").enable(false);

    //$("#UpAcqDate").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate1").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate2").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate3").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate4").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate5").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate6").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate7").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate8").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    //$("#UpAcqDate9").kendoDatePicker({
    //    format: "MM/dd/yyyy",
    //    change: OnChangeUpAcqDate,
    //});

    function _setUpSettledDate() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpSettledDate").data("kendoDatePicker").value(new Date(data));
                //$("#UpAcqDate").data("kendoDatePicker").value(new Date(data));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }





    //---------------------------------------------------

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

    $.ajax({
        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FilterTypeMaturity",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#FilterTypeMaturity").kendoComboBox({
                dataValueField: "Code",
                dataTextField: "DescOne",
                filter: "contains",
                suggest: true,
                change: onChangeFilterTypeMaturity,
                dataSource: data,
            });
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    function onChangeFilterTypeMaturity() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    $("#FilterValue").kendoNumericTextBox({
        format: "n0",
        decimals: 0,
        change: OnChangeFilterValue,
    });

    function OnChangeFilterValue() {

        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
    }

    if (_GlobClientCode == "17") {
        $("#LblPendingCash").show();
        $("#BtnImportOMSBond").hide();
        RefreshPendingCash();
    }
    else if (_GlobClientCode == "03") {
        $("#BtnImportOMSBond").show();
    }
    else {
        $("#LblPendingCash").hide();
        $("#BtnImportOMSBond").hide();
    }

    RefreshAUMYesterday();
    RefreshAvailableCash();
    RefreshNetBuySell();


    function validateData() {

        //if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
        //    alertify.alert("Amount must be filled");
        //    return 0;
        //}
        ////if ($("#AcqPrice").val() == 0 || $("#AcqVolume").val() == 0 || $("#AcqDate").val() == "") {
        ////    alertify.alert("Validation not Pass, Please Check AcqPrice, AcqVolume and AcqDate");
        ////    return 0;
        ////}
        //if ($("#FundPK").val() == 0 || $("#TrxType").val() == 0 || $("#OrderPrice").val() == 0 || $("#Volume").val() == 0 || $("#SettledDate").val() == "") {
        //    alertify.alert("Validation not Pass, Price Must be filled");
        //    return 0;
        //}
        if (GlobValidator.validate()) {
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

    function UpValidateData() {

        //if ($("#UpAmount").data("kendoNumericTextBox").value() == 0) {
        //    alertify.alert("Amount must be filled");
        //    return 0;
        //}
        ////if ($("#UpAcqPrice").val() == 0 || $("#UpAcqVolume").val() == 0 || $("#UpAcqDate").val() == "") {
        ////    alertify.alert("Validation not Pass, Please Check AcqPrice, AcqVolume and AcqDate");
        ////    return 0;
        ////}
        //if ($("#UpFundPK").val() == 0 || $("#UpTrxType").val() == 0 || $("#UpOrderPrice").val() == 0 || $("#UpVolume").val() == 0 || $("#UpSettledDate").val() == "") {
        //    alertify.alert("Validation not Pass");
        //    return 0;
        //}
        if (GlobValidatorUpdate.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function RefreshNetBuySell() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/OMSBond_GetNetBuySell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
            url: window.location.origin + "/Radsoft/OMSBond/OMSBond_GetAUMYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    WinOMSBondListing = $("#WinOMSBondListing").kendoWindow({
        height: 300,
        title: "* Listing OMS Bond",
        visible: false,
        width: 600,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        modal: true,
        close: onWinOMSBondListingClose
    }).data("kendoWindow");

    win = $("#WinInvestmentInstruction").kendoWindow({
        height: "900px",
        title: "Investment Ticket Detail",
        visible: false,
        width: "750px",
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        close: onPopUpClose
    }).data("kendoWindow");

    WinUpdateInvestment = $("#WinUpdateInvestment").kendoWindow({
        height: "900px",
        title: "Investment Ticket Update",
        visible: false,
        width: "750px",
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 50 })
        },
        close: onCloseUpdateInvestment
    }).data("kendoWindow");

    //WinInvestmentListing = $("#WinInvestmentListing").kendoWindow({
    //    height: 400,
    //    title: "* Listing Investment",
    //    visible: false,
    //    width: 900,
    //    open: function (e) {
    //        this.wrapper.css({ top: 80 })
    //    },
    //    modal: true,
    //    close: onWinInvestmentListingClose
    //}).data("kendoWindow");

    function onPopUpClose() {
        $("#BitIsAmortized").prop('checked', false);
        $("#BitIsRounding").prop('checked', false);
        $("#PriceMode").val(1);
        $("#InvestmentNotes").val('');
        $("#InstrumentPK").val('');
        $("#InstrumentID").val('');
        $("#OrderPrice").val("")
        $("#Amount").val("0");
        $("#Volume").val("");
        $("#AccruedInterest").val("0");
        $("#DoneAmount").val("0");
        $("#IncomeTaxGainAmount").val("0");
        $("#IncomeTaxInterestAmount").val("0");
        $("#TotalAmount").val("0");
        $("#TrxType").val("");
        $("#TrxTypeDesc").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#SettledDate").data("kendoDatePicker").value("");
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#LastCouponDate").data("kendoDatePicker").value("");
        $("#NextCouponDate").data("kendoDatePicker").value("");
        $("#InterestPercent").val("0");
        $("#YieldPercent").val("0");
        $("#AcqDate").data("kendoDatePicker").value("");
        $("#AcqPrice").val("0");
        $("#AcqVolume").val("0");
        $("#InvestmentTrType").val('');
        //$("#AcqPrice").val("");
        //$("#AcqDate").data("kendoDatePicker").value("");
        //$("#AcqVolume").val("");
        //$("#AcqPrice1").val("");
        //$("#AcqDate1").data("kendoDatePicker").value("");
        //$("#AcqVolume1").val("");
        //$("#AcqPrice2").val("");
        //$("#AcqDate2").data("kendoDatePicker").value("");
        //$("#AcqVolume2").val("");
        //$("#AcqPrice3").val("");
        //$("#AcqDate3").data("kendoDatePicker").value("");
        //$("#AcqVolume3").val("");
        //$("#AcqPrice4").val("");
        //$("#AcqDate4").data("kendoDatePicker").value("");
        //$("#AcqVolume4").val("");
        //$("#AcqPrice5").val("");
        //$("#AcqDate5").data("kendoDatePicker").value("");
        //$("#AcqVolume5").val("");
        //$("#AcqPrice6").val("");
        //$("#AcqDate6").data("kendoDatePicker").value("");
        //$("#AcqVolume6").val("");
        //$("#AcqPrice7").val("");
        //$("#AcqDate7").data("kendoDatePicker").value("");
        //$("#AcqVolume7").val("");
        //$("#AcqPrice8").val("");
        //$("#AcqDate8").data("kendoDatePicker").value("");
        //$("#AcqVolume8").val("");
        //$("#AcqPrice9").val("");
        //$("#AcqDate9").data("kendoDatePicker").value("");
        //$("#AcqVolume9").val("");
        $("#CrossFundFromPK").val('');
        $("#CrossFundFromID").val('');
        $("#BitForeignTrx").prop('checked', false);
        $("#BitHTM").prop('checked', false);
        $("#BIRate").val('');
        $("#InvestmentStrategy").val('');
        $("#InvestmentStyle").val('');
        $("#InvestmentObjective").val('');
        $("#Revision").val('');
        $("#OtherInvestmentStyle").val('');
        $("#OtherInvestmentObjective").val('');
        $("#OtherRevision").val('');
        $("#FundPK").val("");
    }

    function onCloseUpdateInvestment() {
        $("#UpBitIsAmortized").prop('checked', false);
        $("#UpBitIsRounding").prop('checked', false);
        $("#UpPriceMode").val(1);
        $("#UpInstrumentPK").val('');
        $("#UpOrderPrice").val("")
        $("#UpAmount").val("0");
        $("#UpVolume").val("");
        $("#UpAccruedInterest").val("0");
        $("#UpDoneAmount").val("0");
        $("#UpInvestmentTrType").val('');
        //$("#UpIncomeTaxGainAmount").val("0");
        //$("#UpIncomeTaxInterestAmount").val("0");
        //$("#UpTotalAmount").val("0");
        $("#UpTrxType").val("");
        $("#UpTrxTypeDesc").val("");
        $("#UpCounterpartPK").val("");
        $("#UpCounterpartID").val("");
        $("#UpCounterpartName").val("");
        $("#UpSettledDate").data("kendoDatePicker").value("");
        $("#UpMaturityDate").data("kendoDatePicker").value("");
        $("#UpLastCouponDate").data("kendoDatePicker").value("");
        $("#UpNextCouponDate").data("kendoDatePicker").value("");
        $("#UpInterestPercent").val("0");
        $("#UpYieldPercent").val("0");
        $("#UpAcqDate").data("kendoDatePicker").value("");
        $("#UpAcqPrice").val("0");
        $("#UpAcqVolume").val("0");
        $("#UpBitForeignTrx").prop('checked', false);
        $("#UpBitHTM").prop('checked', false);
        //$("#UpAcqPrice").val("");
        //$("#UpAcqDate").data("kendoDatePicker").value("");
        //$("#UpAcqVolume").val("");
        //$("#UpAcqPrice1").val("");
        //$("#UpAcqDate1").data("kendoDatePicker").value("");
        //$("#UpAcqVolume1").val("");
        //$("#UpAcqPrice2").val("");
        //$("#UpAcqDate2").data("kendoDatePicker").value("");
        //$("#UpAcqVolume2").val("");
        //$("#UpAcqPrice3").val("");
        //$("#UpAcqDate3").data("kendoDatePicker").value("");
        //$("#UpAcqVolume3").val("");
        //$("#UpAcqPrice4").val("");
        //$("#UpAcqDate4").data("kendoDatePicker").value("");
        //$("#UpAcqVolume4").val("");
        //$("#UpAcqPrice5").val("");
        //$("#UpAcqDate5").data("kendoDatePicker").value("");
        //$("#UpAcqVolume5").val("");
        //$("#UpAcqPrice6").val("");
        //$("#UpAcqDate6").data("kendoDatePicker").value("");
        //$("#UpAcqVolume6").val("");
        //$("#UpAcqPrice7").val("");
        //$("#UpAcqDate7").data("kendoDatePicker").value("");
        //$("#UpAcqVolume7").val("");
        //$("#UpAcqPrice8").val("");
        //$("#UpAcqDate8").data("kendoDatePicker").value("");
        //$("#UpAcqVolume8").val("");
        //$("#UpAcqPrice9").val("");
        //$("#UpAcqDate9").data("kendoDatePicker").value("");
        //$("#UpAcqVolume9").val("");

        $("#BIRate").val('');
        $("#InvestmentStrategy").val('');
        $("#InvestmentStyle").val('');
        $("#InvestmentObjective").val('');
        $("#Revision").val('');
        $("#UpOtherInvestmentStyle").val('');
        $("#UpOtherInvestmentObjective").val('');
        $("#UpOtherRevision").val('');

        $("#UpFundPK").val("");
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

    WinListInstrumentByCrossFund = $("#WinListInstrumentByCrossFund").kendoWindow({
        height: 500,
        title: "Instrument List By Cross Fund",
        visible: false,
        width: 1200,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 80 })
        },
        close: onWinListInstrumentByCrossFundClose
    }).data("kendoWindow");

    function onWinListInstrumentByCrossFundClose() {
        $("#gridListInstrumentByCrossFund").empty();
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


    initButton();

    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnRefreshFilterMaturity").kendoButton({
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

        $("#BtnUpCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdateInvestmentBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnUpdateInvestmentSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOMSBondListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnImportOMSBond").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

    }






    $("#FilterInstrumentType").kendoComboBox({
        dataValueField: "value",
        dataTextField: "text",
        filter: "contains",
        suggest: true,
        dataSource: [
                { text: "Bond", value: "1" },
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
            { collapsible: true, size: "850px" },
            { collapsible: true, size: "610px" },
            { collapsible: true, size: "610px" },
            //{ collapsible: true, size: "610px" },
            //{ collapsible: true, size: "610px" },
            { collapsible: true, size: "430px" }
        ],
    }).data("kendoSplitter");

    $("#BtnToInvestment").click(function () {
        splitter.toggle("#PaneInvestment");
    });

    //$("#BtnToBySector").click(function () {
    //    splitter.toggle("#PaneBySector");
    //});

    $("#BtnToByMaturity").click(function () {
        splitter.toggle("#PaneByMaturity");
    });

    //$("#BtnToByInstrument").click(function () {
    //    splitter.toggle("#PaneByInstrument");
    //});

    $("#BtnToExposure").click(function () {
        splitter.toggle("#PaneExposure");
    });

    $("#BtnToFundPosition").click(function () {
        splitter.toggle("#PaneFundPosition");
    });

    $("#BtnToShow").click(function () {
        splitter.expand("#PaneInvestment");
        //splitter.expand("#PaneBySector");
        splitter.expand("#PaneByMaturity");
        //splitter.expand("#PaneByInstrument");
        splitter.expand("#PaneExposure");
        splitter.expand("#PaneFundPosition");
    });

    $("#BtnToHide").click(function () {
        splitter.collapse("#PaneInvestment");
        //splitter.collapse("#PaneBySector");
        splitter.collapse("#PaneByMaturity");
        //splitter.collapse("#PaneByInstrument");
        splitter.collapse("#PaneExposure");
        splitter.collapse("#PaneFundPosition");
    });

    //function GetPriceFromYahooFinance() {
    //    
    //    $.blockUI({});
    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/OMSBond/OMSBond_ListStockForYahooFinance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            var _query = "";
    //            _query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22" + data.toString() + "%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

    //            $.getJSON(_query
    //                ).done(function (data) {
    //                    if (data.query.results != null) {
    //                        var _listInstrumentMarketInfo = [];
    //                        for (var i = 0, l = data.query.results.quote.length; i < l; i++) {
    //                            var _instrumentMarketInfo = {
    //                                Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
    //                                InstrumentID: data.query.results.quote[i].Symbol,
    //                                Price: data.query.results.quote[i].Ask
    //                            };
    //                            _listInstrumentMarketInfo.push(_instrumentMarketInfo);
    //                        }
    //                        $.ajax({
    //                            url: window.location.origin + "/Radsoft/InstrumentMarketInfo/InstrumentMarketInfo_ReNewData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                            type: 'POST',
    //                            data: JSON.stringify(_listInstrumentMarketInfo),
    //                            contentType: "application/json;charset=utf-8",
    //                            success: function (data) {
    //                                alertify.alert(data);
    //                                refresh();
    //                                $.unblockUI();
    //                            },
    //                            error: function (data) {
    //                                alertify.alert(data.responseText);
    //                            }
    //                        });
    //                    } else {
    //                        $.unblockUI();
    //                        alertify.alert("No Data");
    //                    }
    //                }
    //                ).fail(function (jqxhr, textStatus, error) {
    //                    var err = textStatus + ", " + error;
    //                    alertify.alert(err);
    //                    $.unblockUI();
    //                })
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //            $.unblockUI();
    //        }
    //    });
    //}

    $("#BtnRefresh").click(function () {

        refresh();

        alertify.set('notifier', 'position', 'top-center'); alertify.success("Refresh Done");
    });


    //function HideLabel() {
    //    $("#LblAcqPrice").hide();
    //    $("#LblAcqDate").hide();
    //    $("#LblAcqVolume").hide();
    //    $("#LblAcqPrice1").hide();
    //    $("#LblAcqDate1").hide();
    //    $("#LblAcqVolume1").hide();
    //    $("#LblAcqPrice2").hide();
    //    $("#LblAcqDate2").hide();
    //    $("#LblAcqVolume2").hide();
    //    $("#LblAcqPrice3").hide();
    //    $("#LblAcqDate3").hide();
    //    $("#LblAcqVolume3").hide();
    //    $("#LblAcqPrice4").hide();
    //    $("#LblAcqDate4").hide();
    //    $("#LblAcqVolume4").hide();
    //    $("#LblAcqPrice5").hide();
    //    $("#LblAcqDate5").hide();
    //    $("#LblAcqVolume5").hide();
    //    $("#LblAcqPrice6").hide();
    //    $("#LblAcqDate6").hide();
    //    $("#LblAcqVolume6").hide();
    //    $("#LblAcqPrice7").hide();
    //    $("#LblAcqDate7").hide();
    //    $("#LblAcqVolume7").hide();
    //    $("#LblAcqPrice8").hide();
    //    $("#LblAcqDate8").hide();
    //    $("#LblAcqVolume8").hide();
    //    $("#LblAcqPrice9").hide();
    //    $("#LblAcqDate9").hide();
    //    $("#LblAcqVolume9").hide();

    //}

    //function HideUpLabel() {
    //    $("#LblUpAcqPrice").hide();
    //    $("#LblUpAcqDate").hide();
    //    $("#LblUpAcqVolume").hide();
    //    $("#LblUpAcqPrice1").hide();
    //    $("#LblUpAcqDate1").hide();
    //    $("#LblUpAcqVolume1").hide();
    //    $("#LblUpAcqPrice2").hide();
    //    $("#LblUpAcqDate2").hide();
    //    $("#LblUpAcqVolume2").hide();
    //    $("#LblUpAcqPrice3").hide();
    //    $("#LblUpAcqDate3").hide();
    //    $("#LblUpAcqVolume3").hide();
    //    $("#LblUpAcqPrice4").hide();
    //    $("#LblUpAcqDate4").hide();
    //    $("#LblUpAcqVolume4").hide();
    //    $("#LblUpAcqPrice5").hide();
    //    $("#LblUpAcqDate5").hide();
    //    $("#LblUpAcqVolume5").hide();
    //    $("#LblUpAcqPrice6").hide();
    //    $("#LblUpAcqDate6").hide();
    //    $("#LblUpAcqVolume6").hide();
    //    $("#LblUpAcqPrice7").hide();
    //    $("#LblUpAcqDate7").hide();
    //    $("#LblUpAcqVolume7").hide();
    //    $("#LblUpAcqPrice8").hide();
    //    $("#LblUpAcqDate8").hide();
    //    $("#LblUpAcqVolume8").hide();
    //    $("#LblUpAcqPrice9").hide();
    //    $("#LblUpAcqDate9").hide();
    //    $("#LblUpAcqVolume9").hide();

    //}


    function ClearRequiredAttribute() {
        $("#OrderPrice").removeAttr("required");
        //$("#AcqPrice").removeAttr("required");
        $("#SettledDate").removeAttr("required");
        $("#MaturityDate").removeAttr("required");
        $("#LastCouponDate").removeAttr("required");
        $("#NextCouponDate").removeAttr("required");
        $("#InterestPercent").removeAttr("required");

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
        $("#BtnUpdatInvestmentSell").hide();

        grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        _showdetails(dataItemX, 1);

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
        $("#BtnUpdatInvestmentBuy").hide();

        grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        _showdetails(dataItemX, 2);

    }

    function hideInstrumentType()
    {
        if (_GlobClientCode == "05") {
            $("#lblUpInstrument").show();
            $("#lblInstrument").show();
        }
        else {
            $("#lblUpInstrument").show();
            $("#lblInstrument").show();
        }
    }


    function _showdetails(dataItemX, _bs) {
        $("#BtnUpdateInvestmentBuy").hide();
        $("#BtnUpdateInvestmentSell").hide();
        hideInstrumentType();
        if (dataItemX.OrderStatusDesc == "1.PENDING" || dataItemX.OrderStatusDesc == "2.APPROVED" || dataItemX.OrderStatusDesc == "5.PARTIAL" || dataItemX.OrderStatusDesc == "3.OPEN") {
            if (_bs == 1) {
                $("#BtnUpdateInvestmentBuy").show();
                $("#BtnSaveBuy").hide();
            }
            else {
                $("#BtnUpdateInvestmentSell").show();
                $("#BtnSaveSell").hide();
            }

        }

        if (_GlobClientCode == "03" && _bs == 2 && dataItemX.OrderStatusDesc == "1.PENDING") {
            $("#BtnUpdateInvestmentSell").hide();
        }


        //HideUpLabel();
        ClearRequiredAttribute();
        $("#UpReference").val(dataItemX.Reference);
        $("#UpInvestmentPK").val(dataItemX.InvestmentPK);
        $("#UpDealingPK").val(dataItemX.DealingPK);
        $("#UpHistoryPK").val(dataItemX.HistoryPK);
        $("#UpStatusInvestment").val(dataItemX.StatusInvestment);
        $("#UpInvestmentNotes").val(dataItemX.InvestmentNotes);
        $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
        $("#UpInstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
        $("#UpTrxType").val(dataItemX.TrxType);
        $("#UpTrxBuy").val(dataItemX.TrxBuy);
        $("#UpTrxBuyType").val(dataItemX.TrxBuyType);
        $("#OrderStatus").val(dataItemX.OrderStatusDesc);
        $("#UpLastCouponDate").data("kendoDatePicker").value(dataItemX.LastCouponDate);
        $("#UpNextCouponDate").data("kendoDatePicker").value(dataItemX.NextCouponDate);
        $("#UpMaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
        $("#UpSettledDate").data("kendoDatePicker").value(dataItemX.SettledDate);
        $("#UpBitIsAmortized").prop('checked', dataItemX.BitIsAmortized);
        $("#UpBitIsRounding").prop('checked', dataItemX.BitIsRounding);
        $("#UpAcqDate").data("kendoDatePicker").value(dataItemX.AcqDate);
        $("#UpAcqPrice").val(dataItemX.AcqPrice);
        $("#UpAcqVolume").val(dataItemX.AcqVolume);
        $("#UpBitForeignTrx").prop('checked', dataItemX.BitForeignTrx);
        $("#UpBitHTM").prop('checked', dataItemX.BitHTM);
        $("#UpInvestmentTrType").val(dataItemX.InvestmentTrType);

        $("#UpBIRate").val(dataItemX.BIRate);
        $("#UpInvestmentStrategy").val(dataItemX.InvestmentStrategy);
        $("#UpInvestmentStyle").val(dataItemX.InvestmentStyle);
        $("#UpInvestmentObjective").val(dataItemX.InvestmentObjective);
        $("#UpRevision").val(dataItemX.Revision);

        $("#UpOtherInvestmentStyle").val(dataItemX.OtherInvestmentStyle);
        $("#UpOtherInvestmentObjective").val(dataItemX.OtherInvestmentObjective);
        $("#UpOtherRevision").val(dataItemX.OtherRevision);

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
                    change: onChangeUpBIRate,
                    enabled: false,
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
                $("#lblUpOtherRevision").show();
                $("#UpOtherRevision").attr("required", true);
            } else {
                $("#lblUpOtherRevision").hide();
                $("#UpOtherRevision").attr("required", false);
            }
        }

        function setCmbUpRevision() {
            if (dataItemX.Revision == 5) {
                $("#lblUpOtherRevision").show();
                $("#UpOtherRevision").attr("required", true);
            } else {
                $("#lblUpOtherRevision").hide();
                $("#UpOtherRevision").attr("required", false);
            }
            if (dataItemX.Revision == null) {
                return "";
            } else {
                if (dataItemX.Revision == 0) {
                    return "";
                }
                else if (dataItemX.Revision == 5) {
                    $("#lblUpOtherRevision").show();
                }
                else {
                    return dataItemX.Revision;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BondPrice",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (_bs == 2) {
                    $("#UpPriceMode").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        enable: false,
                        change: onChangePriceMode,
                        value: setUpPriceMode(),
                    });
                } else {
                    $("#UpPriceMode").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        change: onChangePriceMode,
                        value: setUpPriceMode(),
                    });
                }



            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setUpPriceMode() {
            if (dataItemX.PriceMode == null) {
                return 1;
            } else {
                if (dataItemX.PriceMode == 0) {
                    return 1;
                } else {
                    return dataItemX.PriceMode;
                }
            }
        }

        function onChangePriceMode() {
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
                    index: 0,
                    suggest: true,
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
            //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpMarketPK").val(),
            //    type: 'GET',
            //    contentType: "application/json;charset=utf-8",
            //    success: function (data) {
            //        $("#UpInstrumentPK").kendoComboBox({
            //            dataValueField: "InstrumentPK",
            //            dataTextField: "ID",
            //            dataSource: data,
            //            filter: "contains",
            //            suggest: true,
            //            change: onChangeUpInstrumentPK,
            //        });
            //    },
            //    error: function (data) {
            //        alertify.alert(data.responseText);
            //    }
            //});


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


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpCrossFundFromPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpCrossFundFromPK,
                    value: setUpCrossFundFromPK(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeUpCrossFundFromPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setUpCrossFundFromPK() {
            if (dataItemX.CrossFundFromPK == null) {
                return "";
            } else {
                if (dataItemX.CrossFundFromPK == 0) {
                    return "";
                } else {
                    return dataItemX.CrossFundFromPK;
                }
            }
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 2,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpTrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpTrxType,
                    value: setUpTrxType(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeUpTrxType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setUpTrxType() {
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

        // InstrumentTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpInstrumentTypePK").kendoComboBox({
                    dataValueField: "InstrumentTypePK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeUpInstrumentTypePK,
                    enabled: false,
                    value: setCmbUpInstrumentTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeUpInstrumentTypePK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbUpInstrumentTypePK() {
            if (dataItemX.InstrumentTypePK == null) {
                return "";
            } else {
                if (dataItemX.InstrumentTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentTypePK;
                }
            }
        }

        $("#UpVolume").kendoNumericTextBox({
            format: "n0",
            change: OnChangeUpVolume,
            value: setUpVolume(),
        });

        function setUpVolume() {
            if (dataItemX.Volume == null) {
                return "";
            } else {
                if (dataItemX.Volume == 0) {
                    return "";
                } else {
                    return dataItemX.Volume;
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



        $("#UpAmount").kendoNumericTextBox({
            format: "n0",
            //change: OnChangeAmount
            value: setUpAmount(),
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
        $("#UpAmount").data("kendoNumericTextBox").enable(false);

        $("#UpInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setUpInterestPercent(),
        });
        function setUpInterestPercent() {
            if (dataItemX.InterestPercent == null) {
                return "";
            } else {
                if (dataItemX.InterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPercent;
                }
            }
        }

        $("#UpInterestPercent").data("kendoNumericTextBox").enable(false);

        $("#UpDoneAmount").kendoNumericTextBox({
            format: "n0",
            value: setUpDoneAmount(),
        });
        function setUpDoneAmount() {
            if (dataItemX.DoneAmount == null) {
                return "";
            } else {
                if (dataItemX.DoneAmount == 0) {
                    return "";
                } else {
                    return dataItemX.DoneAmount;
                }
            }
        }
        $("#UpDoneAmount").data("kendoNumericTextBox").enable(false);

        $("#UpAccruedInterest").kendoNumericTextBox({
            format: "n0",
            value: setUpAccruedInterest(),
        });
        function setUpAccruedInterest() {
            if (dataItemX.AccruedInterest == null) {
                return "";
            } else {
                if (dataItemX.AccruedInterest == 0) {
                    return "";
                } else {
                    return dataItemX.AccruedInterest;
                }
            }
        }
        $("#UpAccruedInterest").data("kendoNumericTextBox").enable(false);

        $("#UpAcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setUpAcqPrice(),
        });
        function setUpAcqPrice() {
            if (dataItemX.AcqPrice == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice;
                }
            }
        }
        //$("#UpAcqPrice").data("kendoNumericTextBox").enable(false);

        $("#UpAcqVolume").kendoNumericTextBox({
            format: "n0",
            value: setUpAcqVolume(),
        });
        function setUpAcqVolume() {
            if (dataItemX.AcqVolume == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume;
                }
            }
        }
        //$("#UpAcqVolume").data("kendoNumericTextBox").enable(false);

        $("#UpYieldPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setUpYieldPercent(),
        });
        function setUpYieldPercent() {
            if (dataItemX.YieldPercent == null) {
                return "";
            } else {
                if (dataItemX.YieldPercent == 0) {
                    return "";
                } else {
                    return dataItemX.YieldPercent;
                }
            }
        }

        showhideUpLabel(_bs);
        WinUpdateInvestment.center();
        WinUpdateInvestment.open();
    }


    function showhideUpLabel(_bs) {

        if (_bs == 1) {
            $("#LblUpAcqDate").hide();
            $("#LblUpAcqPrice").hide();
            $("#LblUpAcqVolume").hide();
            $("#LblBitIsAmortized").show();
            $("#LblBitIsRounding").show();
            $("#LblInvestmentTrType").show();


        }
        else {
            if (_GlobClientCode == "20" || _GlobClientCode == "03" || _GlobClientCode == "21" || _GlobClientCode == "22") {
                $("#LblUpAcqDate").hide();
                $("#LblUpAcqPrice").hide();
                $("#LblUpAcqVolume").hide();
                $("#LblInvestmentTrType").show();
            }
            else {
                $("#LblUpAcqDate").show();
                $("#LblUpAcqPrice").show();
                $("#LblUpAcqVolume").show();
                $("#LblBitIsAmortized").hide();
                $("#LblBitIsRounding").hide();
                $("#LblInvestmentTrType").show();
            }
        }
        if (_GlobClientCode == "20") {
            $("#trUpBIRate").show();
            $("#trUpInvestmentStrategy").show();
            $("#trUpInvestmentStyle").show();
            $("#trUpInvestmentObjective").show();
            $("#trUpRevision").show();
            $("#LblInvestmentTrType").hide();
        }
    }


    $("#BtnUpdateInvestmentBuy").click(function () {
        if ($('#UpTrxType').val() == 1 && ($('#UpCrossFundFromPK').val() != 0 || $('#UpCrossFundFromPK').val() != "")) {
            alertify.alert("Please Update Cross Fund From Trx Sell !");
            return;
        }


        var val = UpValidateData();
        var _setInstrumentType;
        if (val == 1) {
            alertify.confirm("Are you sure want to Update ?", function () {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpInstrumentPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        if (_GlobClientCode == "05") {
                            _setInstrumentType = $('#UpInstrumentTypePK').val();
                        }
                        else {
                            _setInstrumentType = data;
                        }


                        var InvestmentInstruction = {
                            InvestmentPK: $('#UpInvestmentPK').val(),
                            HistoryPK: $('#UpHistoryPK').val(),
                            DealingPK: $('#UpDealingPK').val(),
                            PeriodPK: _defaultPeriodPK,
                            ValueDate: $('#DateFrom').val(),
                            Reference: $("#UpReference").val(),
                            StatusInvestment: $('#UpStatusInvestment').val(),
                            InstructionDate: $('#DateFrom').val(),
                            InstrumentPK: $('#UpInstrumentPK').val(),
                            InstrumentTypePK: _setInstrumentType,
                            OrderPrice: $('#UpOrderPrice').val(),
                            RangePrice: 0,
                            OrderStatusDesc: $('#OrderStatus').val(),
                            Lot: 0,
                            LotInShare: 100,
                            Volume: $('#UpVolume').val(),
                            Amount: $('#UpAmount').val(),
                            TrxType: $('#UpTrxType').val(),
                            TrxTypeID: $("#UpTrxType").data("kendoComboBox").text(),
                            CounterpartPK: 0,
                            FundPK: $('#UpFundPK').val(),
                            SettledDate: $('#UpSettledDate').val(),
                            LastCouponDate: $('#UpLastCouponDate').val(),
                            NextCouponDate: $('#UpNextCouponDate').val(),
                            MaturityDate: $('#UpMaturityDate').val(),
                            InterestPercent: $('#UpInterestPercent').val(),
                            DoneAccruedInterest: $('#UpAccruedInterest').val(),
                            AccruedInterest: $('#UpAccruedInterest').val(),
                            Tenor: $('#UpTenor').val(),
                            InvestmentNotes: $('#InvestmentNotes').val(),
                            DoneAmount: $('#UpAmount').val(),
                            PriceMode: $('#UpPriceMode').val(),
                            BitIsAmortized: $('#UpBitIsAmortized').is(":checked"),
                            YieldPercent: $('#UpYieldPercent').val(),
                            BitIsRounding: $('#UpBitIsRounding').is(":checked"),
                            EntryUsersID: sessionStorage.getItem("user"),
                            MarketPK: 1,
                            CrossFundFromPK: $('#UpCrossFundFromPK').val(),
                            InvestmentNotes: $('#UpInvestmentNotes').val(),
                            BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                            BitHTM: $('#UpBitHTM').is(":checked"),
                            UpdateUsersID: sessionStorage.getItem("user"),
                            InvestmentTrType: $('#UpInvestmentTrType').val(),
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
                            url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                            type: 'POST',
                            data: JSON.stringify(InvestmentInstruction),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                //cek range price
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpOrderPrice').val(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        _statusExposureRangePrice = data.Validate;

                                        if (data.Validate == 1 && _GlobClientCode == "21") {
                                            $.unblockUI();
                                            alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                if (e) {
                                                    $.blockUI();
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
                                                                        url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _InvPKForEmail,
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
                                                                                        alertify.alert(_msgSuccess);
                                                                                        WinUpdateInvestment.close();
                                                                                        refresh();
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
                                                            alertify.alert(data.responseText);
                                                        }
                                                    });

                                                }
                                                else
                                                    return;
                                            })
                                        }
                                        else {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/2",
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (dataCheckExposure) {

                                                    if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                        $.unblockUI();
                                                        InitGridUpInformationExposure();

                                                    }

                                                    else {

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
                                                                            url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _InvPKForEmail,
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
                                                                                            alertify.alert(_msgSuccess);
                                                                                            WinUpdateInvestment.close();
                                                                                            refresh();
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
                                                                alertify.alert(data.responseText);
                                                            }
                                                        });
                                                    }
                                                }
                                            });
                                        }
                                    }

                                })
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            }
                        });
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }).moveTo(posY.left, posY.top);
        }
    });


    $("#BtnUpdateInvestmentSell").click(function () {
        _globUpinvestmentPK = $('#UpInvestmentPK').val(),
            _globUphistoryPK = $('#UpHistoryPK').val(),
            _globUpdealingPK = $('#UpDealingPK').val(),
            _globUpreference = $('#UpReference').val(),
            _globUpinstrumentPK = $('#UpInstrumentPK').val();
        _globUpinstrumentTypePK = $('#UpInstrumentTypePK').val();
        _globUporderPrice = $('#UpOrderPrice').val();
        _globUpvolume = $('#UpVolume').val();
        _globUpamount = $('#UpAmount').val();
        _globUpcrossFundFromPK = $('#UpCrossFundFromPK').val();
        _globUpsettledDate = $('#UpSettledDate').val();
        _globUplastCouponDate = $('#UpLastCouponDate').val();
        _globUpnextCouponDate = $('#UpNextCouponDate').val();
        _globUpmaturityDate = $('#UpMaturityDate').val();
        _globUpinterestPercent = $('#UpInterestPercent').val();
        _globUpaccruedInterest = $('#UpAccruedInterest').val();
        _globUptenor = $('#UpTenor').val();
        _globUpinvestmentNotes = $('#UpInvestmentNotes').val();
        _globUpincomeTaxInterestAmount = $('#UpIncomeTaxInterestAmount').val();
        _globUpincomeTaxGainAmount = $('#UpIncomeTaxGainAmount').val();
        _globUpamount = $('#UpAmount').val();
        _globUptotalAmount = $('#UpTotalAmount').val();
        _globUpinvestmentTrType = $('#UpInvestmentTrType').val();
        _globUppriceMode = $('#UpPriceMode').val();
        _globUpbitIsAmortized = $('#UpBitIsAmortized').is(":checked");
        _globUpyieldPercent = $('#UpYieldPercent').val();
        _globUpfundPK = $('#UpFundPK').val();
        _globUpbitIsRounding = $('#UpBitIsRounding').is(":checked");
        _globUpbitForeignTrx = $('#UpBitForeignTrx').is(":checked");
        _globUpbitHTM = $('#UpBitHTM').is(":checked");
        _globUpbIRate = $('#UpBIRate').val();
        _globUpinvestmentStrategy = $('#UpInvestmentStrategy').val();
        _globUpinvestmentStyle = $('#UpInvestmentStyle').val();
        _globUpinvestmentObjective = $('#UpInvestmentObjective').val();
        _globUprevision = $('#UpRevision').val();
        _globUpotherInvestmentObjective = $('#UpOtherInvestmentObjective').val();
        _globUpotherInvestmentStyle = $('#UpOtherInvestmentStyle').val();
        _globUpotherRevision = $('#UpOtherRevision').val();
        _globUporderStatus = $('#UpOrderStatus').val();
        _globUpstatusInvestment = $('#UpStatusInvestment').val();
        _globUpacqDate = $('#UpAcqDate').val();
        _globUpacqPrice = $('#UpAcqPrice').val();
        _globUpacqVolume = $('#UpAcqVolume').val();
        _globUptrxBuy = $('#UpTrxBuy').val();
        _globUptrxBuyType = $('#UpTrxBuyType').val();


        var element = $("#BtnUpdateInvestmentSell");
        var posY = element.offset() - 50;
        alertify.confirm("Are you sure want to Update ?", function (e) {

            $.blockUI({});

            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpInstrumentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (_GlobClientCode == "05") {
                        _setInstrumentType = $("#UpInstrumentTypePK").val();
                    }
                    else {
                        _setInstrumentType = data;
                    }

                    var InvestmentInstruction = {
                        InvestmentPK: $('#UpInvestmentPK').val(),
                        HistoryPK: $('#UpHistoryPK').val(),
                        DealingPK: $('#UpDealingPK').val(),
                        PeriodPK: _defaultPeriodPK,
                        ValueDate: $('#DateFrom').val(),
                        Reference: $("#UpReference").val(),
                        StatusInvestment: $('#UpStatusInvestment').val(),
                        InstructionDate: $('#DateFrom').val(),
                        InstrumentPK: $('#UpInstrumentPK').val(),
                        InstrumentTypePK: _setInstrumentType,
                        OrderPrice: $('#UpOrderPrice').val(),
                        OrderStatusDesc: $('#OrderStatus').val(),
                        RangePrice: 0,
                        Lot: 0,
                        LotInShare: 100,
                        Volume: $('#UpVolume').val(),
                        Amount: $('#UpAmount').val(),
                        TrxType: $('#UpTrxType').val(),
                        TrxTypeID: $("#UpTrxType").data("kendoComboBox").text(),
                        CounterpartPK: 0,
                        FundPK: $('#UpFundPK').val(),
                        SettledDate: $('#UpSettledDate').val(),
                        LastCouponDate: $('#UpLastCouponDate').val(),
                        NextCouponDate: $('#UpNextCouponDate').val(),
                        MaturityDate: $('#UpMaturityDate').val(),
                        InterestPercent: $('#UpInterestPercent').val(),
                        DoneAccruedInterest: $('#UpAccruedInterest').val(),
                        AccruedInterest: $('#UpAccruedInterest').val(),
                        Tenor: $('#UpTenor').val(),
                        InvestmentNotes: $('#InvestmentNotes').val(),
                        DoneAmount: $('#UpAmount').val(),
                        PriceMode: $('#UpPriceMode').val(),
                        BitIsAmortized: $('#UpBitIsAmortized').is(":checked"),
                        AcqDate: $('#UpAcqDate').val(),
                        AcqPrice: $('#UpAcqPrice').val(),
                        AcqVolume: $('#UpAcqVolume').val(),
                        YieldPercent: $('#UpYieldPercent').val(),
                        BitIsRounding: $('#UpBitIsRounding').is(":checked"),
                        EntryUsersID: sessionStorage.getItem("user"),
                        MarketPK: 1,
                        CrossFundFromPK: $('#UpCrossFundFromPK').val(),
                        TrxBuy: $('#UpTrxBuy').val(),
                        TrxBuyType: $('#UpTrxBuyType').val(),
                        InvestmentNotes: $('#UpInvestmentNotes').val(),
                        BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                        BitHTM: $('#UpBitHTM').is(":checked"),
                        UpdateUsersID: sessionStorage.getItem("user"),
                        InvestmentTrType: $('#UpInvestmentTrType').val(),

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
                        url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                        type: 'POST',
                        data: JSON.stringify(InvestmentInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            var Validate = {
                                ValueDate: $('#DateFrom').val(),
                                FundPK: $('#UpFundPK').val(),
                                InstrumentPK: $('#UpInstrumentPK').val(),
                                Volume: $('#UpVolume').val(),
                                TrxBuy: $('#UpTrxBuy').val(),
                                TrxBuyType: $('#UpTrxBuyType').val(),
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
                                            data: JSON.stringify(InvestmentInstruction),
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
                                            url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpOrderPrice').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                _statusExposureRangePrice = data.Validate;

                                                if (data.Validate == 1 && _GlobClientCode == "21") {
                                                    $.unblockUI();
                                                    alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                        if (e) {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/2",
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (dataCheckExposure) {

                                                                    if (dataCheckExposure.AlertExposure == 3 || dataCheckExposure.AlertExposure == 4 || dataCheckExposure.AlertExposure == 7 || dataCheckExposure.AlertExposure == 8) {
                                                                        $.unblockUI();
                                                                        InitGridUpInformationExposure();

                                                                    } else {


                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                            type: 'POST',
                                                                            data: JSON.stringify(InvestmentInstruction),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {

                                                                                if ($('#UpCrossFundFromPK').val() != "") {
                                                                                    UpdateCrossFundInvestmentBuy();
                                                                                }
                                                                                _msgSuccess = data;
                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                    type: 'GET',
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        _InvPKForEmail = data;
                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _InvPKForEmail,
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
                                                                                                            alertify.alert(_msgSuccess);
                                                                                                            WinUpdateInvestment.close();
                                                                                                            refresh();
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
                                                                                alertify.alert(data.responseText);
                                                                            }
                                                                        });
                                                                    }
                                                                }
                                                            });
                                                        }
                                                        else
                                                            return;
                                                    });
                                                }
                                                else {
                                                    //cek range price
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpOrderPrice').val(),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            _statusExposureRangePrice = data.Validate;
                                                            alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                if (e) {
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val() + "/" + $('#UpTrxType').val() + "/2",
                                                                        type: 'GET',
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (dataCheckExposure) {

                                                                            if (dataCheckExposure.AlertExposure == 3 || dataCheckExposure.AlertExposure == 4 || dataCheckExposure.AlertExposure == 7 || dataCheckExposure.AlertExposure == 8) {
                                                                                $.unblockUI();
                                                                                InitGridUpInformationExposure();

                                                                            } else {


                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                    type: 'POST',
                                                                                    data: JSON.stringify(InvestmentInstruction),
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        if ($('#UpCrossFundFromPK').val() != "") {
                                                                                            UpdateCrossFundInvestmentBuy();
                                                                                        }

                                                                                        _msgSuccess = data;
                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                            type: 'GET',
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                _InvPKForEmail = data;
                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _InvPKForEmail,
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
                                                                                                                    alertify.alert(_msgSuccess);
                                                                                                                    WinUpdateInvestment.close();
                                                                                                                    refresh();
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
                                                                                        alertify.alert(data.responseText);
                                                                                    }
                                                                                });
                                                                            }
                                                                        }
                                                                    });
                                                                }
                                                                else
                                                                    return;
                                                            });
                                                        }
                                                    })
                                                }
                                            }

                                        });
                                    }
                                    $.unblockUI();
                                }
                            });
                        }
                    });
                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                }
            });
            $.unblockUI();

        }).moveTo(posY.left, posY.top - 200);

    });


    $("#BtnUpCancel").click(function () {

        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                WinUpdateInvestment.close();
                alertify.alert("Close Update");
            }
        });
    });


    function refresh() {
        if (_GlobClientCode == "17") {
            RefreshPendingCash();
        }
        RefreshAUMYesterday();
        RefreshAvailableCash();
        RefreshNetBuySell();
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

        if ($("#FilterTypeMaturity").val() == "") {
            _filterTypeMaturity = "0";
        }
        else {
            _filterTypeMaturity = $("#FilterTypeMaturity").val();
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

        //var newDS = getDataSourceInvesmentInstruction(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1);
        //$("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

        // Refresh Grid Bank Position Per Fund ( atas Layar )
        var newDS = getDataSourceA(window.location.origin + "/Radsoft/OMSBond/OMSBond_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridFundPosition").data("kendoGrid").setDataSource(newDS);

        // Re
        var newDS = getDataSourceFundExposurePerFund(window.location.origin + "/Radsoft/OMSBond/OMSBond_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/2");
        $("#gridFundExposure").data("kendoGrid").setDataSource(newDS);

        //var newDS = getDataSourceOMSBondBySectorID(window.location.origin + "/Radsoft/OMSBond/OMSBond_BySectorID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        //$("#gridOMSBondBySectorID").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSBondByMaturity(window.location.origin + "/Radsoft/OMSBond/OMSBond_ByMaturity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _filterTypeMaturity + "/" + _filterPeriod + "/" + _filterValue);
        $("#gridOMSBondByMaturity").data("kendoGrid").setDataSource(newDS);

        //var newDS = getDataSourceOMSBondByInstrument(window.location.origin + "/Radsoft/OMSBond/OMSBond_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        //$("#gridOMSBondByInstrument").data("kendoGrid").setDataSource(newDS);

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
        RefreshNetBuySell();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshSellOnly() {
        RefreshAvailableCash();
        RefreshNetBuySell();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    InitgridFundPosition();

    function InitgridFundPosition() {
        var AccountApprovedURL = window.location.origin + "/Radsoft/OMSBond/OMSBond_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                fileName: "OMSBondFundPosition.xlsx"
            },
            columns: [{
                field: "Name",
                title: "InstrumentID", headerAttributes: {
                    style: "text-align: center"
                },
                width: 125
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
                        field: "CurrBalance", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "CurrNAVPercent", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                        format: "{0:n4}", attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 55
                    }]
                }, {
                    title: "Movement", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "Movement", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterBalance", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterNAVPercent", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 55
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
                        field: "MovementTOne", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTOne", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterTOneNAVPercent", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 55
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
                        field: "MovementTTwo", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTTwo", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterTTwoNAVPercent", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 55
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
                        field: "MovementTThree", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }]
                }, {
                    title: "After", headerAttributes: {
                        style: "text-align: center"
                    },
                    columns: [{
                        field: "AfterTThree", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                        attributes: { style: "text-align:right;" }, title: "Value", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 75
                    }, {
                        field: "AfterTThreeNAVPercent", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                        attributes: { style: "text-align:right;" }, title: "% Nav", headerAttributes: {
                            style: "text-align: center"
                        },
                        width: 55
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
                         }
                     }
                 }
             });
    }

    function InitgridFundExposure() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSBond/OMSBond_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/2",
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
                fileName: "OMSBondExposure.xlsx"
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
                    field: "MaxExposurePercent", title: "MaxExposure %", format: "{0:n4}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MinExposurePercent", title: "MinExposure %", format: "{0:n4}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "ExposurePercent", title: "Exposure %", format: "{0:n4}", attributes: { style: "text-align:right;" }, headerAttributes: {
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

    InitgridOMSBondByMaturity();

    function gridOMSBondByMaturityOnDataBound(e) {
        var grid = $("#gridOMSBondByMaturity").data("kendoGrid");
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

    function getDataSourceOMSBondByMaturity(_url) {
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
                             InstrumentTypeID: { type: "string" },
                             SectorID: { type: "string" },
                             CurrentExposure: { type: "number" },
                             Volume: { type: "number" },
                             AvgPrice: { type: "number" },
                             Cost: { type: "number" },
                             ClosePrice: { type: "number" },
                             LastPrice: { type: "number" },
                             PriceDifference: { type: "number" },
                             MarketValue: { type: "number" },
                             UnRealized: { type: "number" },
                             GainLoss: { type: "number" },
                             NextCouponDate: { type: "date" },
                             MaturityDate: { type: "date" },
                             Status: { type: "number" }
                         }
                     }
                 },
                 group: {
                     field: "InstrumentTypeID", aggregates: [
                     { field: "Volume", aggregate: "sum" },
                     { field: "CurrentExposure", aggregate: "sum" },
                     { field: "Cost", aggregate: "sum" },
                     { field: "UnRealized", aggregate: "sum" },
                     { field: "GainLoss", aggregate: "max" }
                     ]
                 },
                 aggregate: [{ field: "Volume", aggregate: "sum" },
                     { field: "CurrentExposure", aggregate: "sum" },
                     { field: "Cost", aggregate: "sum" },
                     { field: "UnRealized", aggregate: "sum" },
                     { field: "GainLoss", aggregate: "max" }
                 ]
             });
    }

    function InitgridOMSBondByMaturity() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSBond/OMSBond_ByMaturity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/0/0/0",
         dataSourceApproved = getDataSourceOMSBondByMaturity(FundApprovedURL);

        var gridOMSBondByMaturity = $("#gridOMSBondByMaturity").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSBondByMaturityOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSBondByMaturityTemplate").html()),
            excel: {
                fileName: "OMSBondByMaturity.xlsx"
            },
            columns: [
                {
                    hidden: true, field: "InstrumentTypeID", title: "InstrumentTypeID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                 {
                     field: "InstrumentID", title: "InstrumentID", headerAttributes: {
                         style: "text-align: center"
                     }, width: 100
                 },
                {
                    field: "InterestPercent", title: "Interest %", format: "{0:n4}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                {
                    hidden: true, field: "IndexID", title: "Index", headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                { hidden: true, field: "LastCouponDate", title: "Last Coupon Date", width: 70, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                { field: "MaturityDate", title: "Maturity Date", width: 70, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                {
                    field: "Volume", title: "Volume", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                {
                    field: "LastPrice", title: "LastPrice", format: "{0:n8}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                {
                    field: "MarketValue", title: "MarketValue", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 90
                },
                {
                    field: "CurrentExposure", title: "Curr Exposure %", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                    attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                { field: "NextCouponDate", title: "Next Coupon Date", width: 70, template: "#= kendo.toString(kendo.parseDate(NextCouponDate), 'dd/MMM/yyyy')#" },

                {
                    field: "AvgPrice", title: "AvgPrice", format: "{0:n8}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                {
                    hidden: true, field: "Cost", title: "Cost", format: "{0:n0}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },

                {
                    hidden: true, field: "ClosePrice", title: "ClosePrice", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },

                {
                    hidden: true, field: "PriceDifference", title: "PriceDiff %", format: "{0:n4}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },

                {
                    field: "UnRealized", title: "UnRealized", format: "{0:n0}", attributes: { style: "text-align:right;" }, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
                {
                    hidden: true, field: "GainLoss", title: "GainLoss %", format: "{0:n4}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 70
                },
               ]
        }).data("kendoGrid");
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }


        var gridBondBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
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
                fileName: "OMSBondInvestmentInstruction.xlsx"
            },
            columns: [
                ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                { command: { text: "S", click: showDetailsBuy }, title: " ", width: 40 },
                {
                    headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                    template: "<input type='checkbox' class='checkboxBuy'/>", width: 45
                },
                //{
                //    field: "SelectedInvestment",
                //    width: 30,
                //    template: "<input class='cSelectedDetailApprovedBondBuy' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                //    headerTemplate: "<input id='SelectedAllApprovedBondBuy' type='checkbox'  />",
                //    filterable: true,
                //    sortable: false,
                //    columnMenu: false
                //},
                { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                {
                    field: "OrderStatusDesc", title: "O.Status", headerAttributes: {
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
                    }, width: 70, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                },
                {
                    field: "InstrumentID", title: "Stock", headerAttributes: {
                        style: "text-align: center"
                    }, width: 80
                },
                {
                    hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                {
                    field: "DonePrice", title: "Price", headerAttributes: {
                        style: "text-align: center"
                    }, width: 70, format: "{0:n6}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "DoneVolume", title: "Nominal", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", headerAttributes: {
                        style: "text-align: center"
                    }, width: 80, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "DoneAmount", title: "Gross Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "InvestmentNotes", title: "Remark", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", width: 50 },
                //{ hidden: true, field: "AcqPrice", title: "AcqPrice", width: 50 },
                { hidden: true, field: "InterestPercent", title: "InterestPercent", width: 50 },
                { hidden: true, field: "LastCouponDate", title: "LastCouponDate", width: 50 },
                { hidden: true, field: "NextCouponDate", title: "NextCouponDate", width: 50 },
                { hidden: true, field: "MaturityDate", title: "MaturityDate", width: 50 },
                { hidden: true, field: "SettledDate", title: "SettledDate", width: 50 },
                { hidden: true, field: "Tenor", title: "Tenor", width: 50 },
                { hidden: true, field: "AccruedInterest", title: "AccruedInterest", width: 50 },
                { hidden: true, field: "IncomeTaxInterestAmount", title: "IncomeTaxInterestAmount", width: 50 },
                { hidden: true, field: "IncomeTaxGainAmount", title: "IncomeTaxGainAmount", width: 50 },
                { hidden: true, field: "TotalAmount", title: "TotalAmount", width: 50 },
                { hidden: true, field: "BitForeignTrx", title: "BitForeignTrx", width: 50 },
                { hidden: true, field: "BitHTM", title: "BitHTM", width: 50 },
                {
                    hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
            ]
        }).data("kendoGrid");

        gridBondBuyOnly.table.on("click", ".checkboxBuy", selectRowBuy);
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


        $("#SelectedAllApprovedBondBuy").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            SelectDeselectAllDataBondBuyOnly(_checked, "Approved");

        });

        //gridBondBuyOnly.table.on("click", ".cSelectedDetailApprovedBondBuy", selectDataApprovedBondBuy);

        function selectDataApprovedBondBuy(e) {


            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataBondBuyOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataBondBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Investment",
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
    function SelectDeselectAllDataBondBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Investment",
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/" + _fundID,
                dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }


        var gridBondSellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
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
                fileName: "OMSBondInvestmentInstruction.xlsx"
            },
            columns: [
                //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                { command: { text: "S", click: showDetailsSell }, title: " ", width: 40 },
                {
                    headerTemplate: "<input type='checkbox' id='chbS' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbS'></label>",
                    template: "<input type='checkbox' class='checkboxSell'/>", width: 45
                },
                //{
                //    field: "SelectedInvestment",
                //    width: 30,
                //    template: "<input class='cSelectedDetailApprovedBondSell' type='checkbox'   #= SelectedInvestment ? 'checked=checked' : '' # />",
                //    headerTemplate: "<input id='SelectedAllApprovedBondSell' type='checkbox'  />",
                //    filterable: true,
                //    sortable: false,
                //    columnMenu: false
                //},
                { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                { field: "StatusInvestment", title: "Status.", filterable: false, hidden: true, width: 100 },
                {
                    field: "OrderStatusDesc", title: "O.Status", headerAttributes: {
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
                    }, width: 70, aggregates: ["count"], footerTemplate: "Total Count: #=count#", groupFooterTemplate: "Count: #=count#"
                },
                {
                    field: "InstrumentID", title: "Stock", headerAttributes: {
                        style: "text-align: center"
                    }, width: 80
                },
                {
                    hidden: true, field: "InstrumentName", title: "Name", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },

                {
                    field: "DonePrice", title: "Price", headerAttributes: {
                        style: "text-align: center"
                    }, width: 70, format: "{0:n6}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "DoneVolume", title: "Nominal", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", headerAttributes: {
                        style: "text-align: center"
                    }, width: 80, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    hidden: true, field: "DoneVolume", title: "Volume", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "DoneAmount", title: "Gross Amount", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100, format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                {
                    field: "InvestmentNotes", title: "Remark", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                { hidden: true, field: "InstrumentTypePK", title: "Instrument Type", width: 50 },
                //{ hidden: true, field: "AcqPrice", title: "AcqPrice", width: 50 },
                { hidden: true, field: "InterestPercent", title: "InterestPercent", width: 50 },
                { hidden: true, field: "LastCouponDate", title: "LastCouponDate", width: 50 },
                { hidden: true, field: "NextCouponDate", title: "NextCouponDate", width: 50 },
                { hidden: true, field: "MaturityDate", title: "MaturityDate", width: 50 },
                { hidden: true, field: "SettledDate", title: "SettledDate", width: 50 },
                { hidden: true, field: "Tenor", title: "Tenor", width: 50 },
                { hidden: true, field: "AccruedInterest", title: "AccruedInterest", width: 50 },
                { hidden: true, field: "IncomeTaxInterestAmount", title: "IncomeTaxInterestAmount", width: 50 },
                { hidden: true, field: "IncomeTaxGainAmount", title: "IncomeTaxGainAmount", width: 50 },
                { hidden: true, field: "TotalAmount", title: "TotalAmount", width: 50 },
                { hidden: true, field: "AcqDate", title: "AcqDate", width: 50 },
                {
                    hidden: true,
                    field: "AcqPrice", title: "Price", width: 70, format: "{0:n6}", attributes: { style: "text-align:right;" }
                },
                {
                    hidden: true,
                    field: "AcqVolume", title: "Volume", width: 70, format: "{0:n6}", attributes: { style: "text-align:right;" }
                },
                {
                    hidden: true, field: "MarketID", title: "MarketID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 50
                },
                { hidden: true, field: "TrxBuy", title: "TrxBuy", width: 50 },
                { hidden: true, field: "TrxBuyType", title: "TrxBuyType", width: 50 },
                { hidden: true, field: "BitForeignTrx", title: "BitForeignTrx", width: 50 },
                { hidden: true, field: "BitHTM", title: "BitHTM", width: 50 },
            ]
        }).data("kendoGrid");


        gridBondSellOnly.table.on("click", ".checkboxSell", selectRowSell);
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

        $("#SelectedAllApprovedBondSell").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataBondSellOnly(_checked, "Approved");

        });

        //gridBondSellOnly.table.on("click", ".cSelectedDetailApprovedBondSell", selectDataApprovedBondSell);

        function selectDataApprovedBondSell(e) {


            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataBondSellOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataBondSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Investment",
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
    function SelectDeselectAllDataBondSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Investment",
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
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             OrderPrice: { type: "number" },
                             Lot: { type: "number" },
                             Volume: { type: "number" },
                             Amount: { type: "number" },

                         }
                     }
                 },
                 group:
                     [
                     {
                         field: "FundID", aggregates: [
                         { field: "Lot", aggregate: "sum" },
                         { field: "Amount", aggregate: "sum" }
                         ]
                     },
                     {
                         field: "TrxTypeID", aggregates: [
                         { field: "Lot", aggregate: "sum" },
                         { field: "Amount", aggregate: "sum" }
                         ]
                     }

                     ],
                 aggregate: [{ field: "Lot", aggregate: "sum" },
                         { field: "Amount", aggregate: "sum" }
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
                            OrderPrice: { type: "number" },
                            Lot: { type: "number" },
                            Volume: { type: "number" },
                            Amount: { type: "number" },
                            InstrumentTypePK: { type: "number" },
                            AcqPrice: { type: "number" },
                            AcqVolume: { type: "number" },
                            AcqDate: { type: "date" },
                            InterestPercent: { type: "number" },
                            LastCouponDate: { type: "date" },
                            NextCouponDate: { type: "date" },
                            MaturityDate: { type: "date" },
                            SettledDate: { type: "date" },
                            Tenor: { type: "number" },
                            AccruedInterest: { type: "number" },
                            IncomeTaxInterestAmount: { type: "number" },
                            IncomeTaxGainAmount: { type: "number" },
                            TotalAmount: { type: "number" },
                            MarketPK: { type: "number" },
                            MarketID: { type: "string" },
                            TrxBuy: { type: "number" },
                            TrxBuyType: { type: "string" },
                            BitForeignTrx: { type: "boolean" },
                            BitHTM: { type: "boolean" },
                        }
                    }
                },
                group:
                    [
                        {
                            field: "OrderStatusDesc", aggregates: [
                                { field: "DoneVolume", aggregate: "sum" },
                                { field: "DoneAmount", aggregate: "sum" },
                                { field: "FundID", aggregate: "count" },
                            ]
                        }
                    ],
                aggregate: [{ field: "DoneVolume", aggregate: "sum" },
                { field: "DoneAmount", aggregate: "sum" },
                { field: "FundID", aggregate: "count" },
                ]
            });
    }

    function gridInvestmentInstructionDataBound() {
        var grid = $("#gridInvestmentInstruction").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusInvestment == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            }
            else if (row.StatusInvestment == 2) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
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

    $('#BtnRefreshFilterMaturity').on("click", function () {
        refresh();

        alertify.set('notifier', 'position', 'top-center'); alertify.success("Refresh Done");
    });


    function recalYield() {

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetDataInstrumentForYield/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var Instrument = {
                    SettledDate: $('#SettledDate').val(),
                    MaturityDate: $('#MaturityDate').val(),
                    InterestRate: $('#InterestPercent').val(),
                    CostPrice: $('#OrderPrice').val(),
                    PaymentPeriod: data.PaymentPeriod,
                    InterestDaysType: data.InterestDaysType,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSBond/GetYield/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(Instrument),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#YieldPercent").data("kendoNumericTextBox").value(data * 100);

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
    }

    function BtnAddClick(_bs) {

        //HideLabel();
        ClearRequiredAttribute();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        //$("#LblAcqPrice").show();
        //$("#LblAcqDate").show();
        //$("#LblAcqVolume").show();
        //$("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        //$("#LblIncomeTaxGainAmount").show();
        //$("#LblIncomeTaxInterestAmount").show();
        //$("#LblNetAmount").show();

        $("#AcqPrice").val(0);
        $("#AcqVolume").val(0);

        $("#OrderPrice").attr("required", true);
        $("#Volume").attr("required", true);
        //$("#AcqPrice").attr("required", true);
        //$("#AcqDate").attr("required", true);
        //$("#AcqVolume").attr("required", true);
        $("#SettledDate").attr("required", true);
        $("#LastCouponDate").attr("required", true);
        $("#NextCouponDate").attr("required", true);
        $("#InterestPercent").attr("required", true);
        //if (_GlobClientCode == "20")
        //{
        //    $("#LblSettlementDate").show();
        //} else { }

        hideInstrumentType();
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
                    change: onChangeBIRate,
                    enabled: false,
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


        //InvestmentStyle  
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
                $("#lblOtherInvestmentStyle").show();
                $("#OtherInvestmentStyle").attr("required", true);
            } else {
                $("#lblOtherInvestmentStyle").hide();
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
                $("#lblOtherInvestmentObjective").show();
                $("#OtherInvestmentObjective").attr("required", true);
            } else {
                $("#lblOtherInvestmentObjective").hide();
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
                $("#lblOtherRevision").show();
                $("#OtherRevision").attr("required", true);
            } else {
                $("#lblOtherRevision").hide();
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
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BondPrice",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                if (_bs == 2) {
                    $("#PriceMode").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        enable: false,
                        change: onChangePriceMode,
                        index: 0
                    });
                } else {
                    $("#PriceMode").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        change: onChangePriceMode,
                        index: 0
                    });
                }


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePriceMode() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SettledDate").data("kendoDatePicker").value(new Date(data));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 2,
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
                    index: 0,
                    suggest: true,
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
            //    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val(),
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


        }

        function onChangeInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $("#AccruedInterest").val(0);
                $("#DoneAmount").val(0);
                $("#IncomeTaxGainAmount").val(0);
                $("#IncomeTaxInterestAmount").val(0);
                $("#TotalAmount").val(0);

                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                        $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));
                        return;
                    },
                    error: function (data) {
                        alertify.alert("Please Fill Value Date or Instrument");
                        $("#InstrumentPK").val("");
                        $("#InstrumentID").val("");
                    }
                });

                var Investment = {
                    FundPK: $('#FundPK').val(),
                    InstrumentPK: $('#InstrumentPK').val(),
                    InstrumentTypePK: 2,
                    TrxType: $("#TrxType").data("kendoComboBox").value(),
                    ValueDate: $('#DateFrom').val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/GetInformationByInstrumentPKByTrxType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(Investment),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Volume").data("kendoNumericTextBox").value(0);
                        $("#Amount").data("kendoNumericTextBox").value(0);
                        $("#MaturityDate").data("kendoDatePicker").value(new Date(data.MaturityDate));
                        //$("#MaturityDate").data("kendoDatePicker").value(data.MaturityDate);
                        $("#InterestPercent").data("kendoNumericTextBox").value(data.InterestPercent);
                        //$("#AcqVolume").data("kendoNumericTextBox").value(0);
                        //$("#AcqPrice").data("kendoNumericTextBox").value(data.AvgPrice);
                        //if ($('#TrxType').val() == 2)
                        //{
                        //    $("#AcqDate").data("kendoDatePicker").value(new Date(data.AcqDate));
                        //}



                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CrossFundFromPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCrossFundFromPK,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCrossFundFromPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        // InstrumentTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentTypePK").kendoComboBox({
                    dataValueField: "InstrumentTypePK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeInstrumentTypePK,
                    enabled: false,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeInstrumentTypePK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#Volume").kendoNumericTextBox({
            format: "n0",
            change: OnChangeVolume
        });

        function OnChangeVolume() {
            price = $("#OrderPrice").data("kendoNumericTextBox").value() / 100
            $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * price);
            $("#AcqVolume").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value());
            recalYield();
            RecalNetAmount();

        }

        $("#OrderPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeOrderPrice
        });


        $("#Amount").kendoNumericTextBox({
            format: "n0",
            //change: OnChangeAmount
        });
        $("#Amount").data("kendoNumericTextBox").enable(false);

        $("#InterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,

        });
        $("#InterestPercent").data("kendoNumericTextBox").enable(false);


        $("#AcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });
        //$("#AcqPrice").data("kendoNumericTextBox").enable(false);


        $("#AcqVolume").kendoNumericTextBox({
            format: "n0",
        });
        //$("#AcqVolume").data("kendoNumericTextBox").enable(false);

        $("#YieldPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,

        });
        $("#YieldPercent").data("kendoNumericTextBox").enable(false);

        $("#AccruedInterest").kendoNumericTextBox({
            format: "n0",

        });
        $("#AccruedInterest").data("kendoNumericTextBox").enable(false);

        $("#DoneAmount").kendoNumericTextBox({
            format: "n0",

        });
        $("#DoneAmount").data("kendoNumericTextBox").enable(false);

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

        showhideLabel(_bs);

        win.center();
        win.open();

    }

    function showhideLabel(_bs) {
        if (_bs == 1) {
            $("#LblAcqDate").hide();
            $("#LblAcqPrice").hide();
            $("#LblAcqVolume").hide();
            $("#LblBitIsAmortized").show();
            $("#LblBitIsRounding").show();
            $("#LblInvestmentTrType").show();
        }
        else {
            if (_GlobClientCode == "20" || _GlobClientCode == "03" || _GlobClientCode == "21" || _GlobClientCode == "22") {
                $("#LblAcqDate").hide();
                $("#LblAcqPrice").hide();
                $("#LblAcqVolume").hide();
                $("#LblInvestmentTrType").show();
            }
            else {
                $("#LblAcqDate").show();
                $("#LblAcqPrice").show();
                $("#LblAcqVolume").show();
                $("#LblBitIsAmortized").hide();
                $("#LblBitIsRounding").hide();
                $("#LblInvestmentTrType").show();
            }

        }
        if (_GlobClientCode == "20") {
            $("#LblBIRate").show();
            $("#LblInvestmentStrategy").show();
            $("#LblInvestmentStyle").show();
            $("#LblInvestmentObjective").show();
            $("#LblRevision").show();
            $("#LblInvestmentTrType").hide();
        }
    }


    function _resetButtonBuySell() {
        $("#BtnSaveBuy").hide();
        $("#BtnSaveSell").hide();
    }

    function _resetButtonUpdateBuySell() {
        $("#BtnUpdateInvestmentBuy").hide();
        $("#BtnUpdateInvestmentSell").hide();
    }


    function OnChangeOrderPrice() {
        $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * $("#OrderPrice").data("kendoNumericTextBox").value() / 100);
        recalYield();
        RecalNetAmount();

    }

    function getTotalAmount() {
        $("#TotalAmount").data("kendoNumericTextBox").value($("#Amount").data("kendoNumericTextBox").value() + $("#AccruedInterest").data("kendoNumericTextBox").value() - $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value() - $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value())
    }

    $("#BtnSaveBuy").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;

        var val = validateData();
        var _urlForReference;
        var _msgSuccess;

        var _setInstrumentType = 0;
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
                        alertify.alert("Save Cancel, Already Generate End Day Trails FundPortfolio");
                        return;
                    }
                    else {
                        if ($("#TotalAmount").val() == 0) {
                            $.unblockUI();
                            alertify.alert("Please Fill All Data First!");
                            return;
                        } else {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/ValidateCheckIssueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Instrument/ValidateCheckFirstCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#InstrumentPK').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == false) {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Amount').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val(),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data == true) {
                                                                $.unblockUI();
                                                                alertify.confirm("Cash Not Available, </br> Are You Sure To Continue ?", function (e) {
                                                                    if (e) {
                                                                        $.blockUI();
                                                                        //cek range price
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#OrderPrice').val(),
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                _statusExposureRangePrice = data.Validate;
                                                                                //cam only
                                                                                if (data.Validate == 1 && _GlobClientCode == "21") {
                                                                                    $.unblockUI();
                                                                                    alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                                        if (e) {
                                                                                            $.blockUI();

                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                                                type: 'GET',
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                                        $.unblockUI();
                                                                                                        InitGridInformationExposure();

                                                                                                    }

                                                                                                    else {

                                                                                                        $.ajax({
                                                                                                            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                                            type: 'GET',
                                                                                                            contentType: "application/json;charset=utf-8",
                                                                                                            success: function (data) {
                                                                                                                _setInstrumentType = data;
                                                                                                            },
                                                                                                            error: function (data) {
                                                                                                                $.unblockUI();
                                                                                                                alertify.alert(data.responseText);
                                                                                                            }
                                                                                                        });

                                                                                                        if (_GlobClientCode == "20")
                                                                                                            _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                                    InstrumentPK: $('#InstrumentPK').val(),
                                                                                                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                                                    OrderPrice: $('#OrderPrice').val(),
                                                                                                                    RangePrice: 0,
                                                                                                                    Lot: 0,
                                                                                                                    LotInShare: 100,
                                                                                                                    Volume: $('#Volume').val(),
                                                                                                                    Amount: $('#Amount').val(),
                                                                                                                    TrxType: $('#TrxType').val(),
                                                                                                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                                    CounterpartPK: 0,
                                                                                                                    MarketPK: 1,
                                                                                                                    FundPK: $('#FundPK').val(),
                                                                                                                    SettledDate: $('#SettledDate').val(),
                                                                                                                    LastCouponDate: $('#LastCouponDate').val(),
                                                                                                                    NextCouponDate: $('#NextCouponDate').val(),
                                                                                                                    MaturityDate: $('#MaturityDate').val(),
                                                                                                                    InterestPercent: $('#InterestPercent').val(),
                                                                                                                    AccruedInterest: $('#AccruedInterest').val(),
                                                                                                                    DoneAccruedInterest: $('#AccruedInterest').val(),
                                                                                                                    Tenor: $('#Tenor').val(),
                                                                                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                                    IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                                                    IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                                                    DoneAmount: $('#Amount').val(),
                                                                                                                    TotalAmount: $('#TotalAmount').val(),
                                                                                                                    InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                                    PriceMode: $('#PriceMode').val(),
                                                                                                                    BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                                                    YieldPercent: $('#YieldPercent').val(),
                                                                                                                    CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                                                    BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                                                                                    BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                                    BitHTM: $('#BitHTM').is(":checked"),
                                                                                                                    BIRate: $('#BIRate').val(),
                                                                                                                    InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                                    InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                                    InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                                    Revision: $('#Revision').val(),

                                                                                                                    OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                                    OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                                    OtherRevision: $('#OtherRevision').val(),

                                                                                                                    EntryUsersID: sessionStorage.getItem("user")
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
                                                                                                                                    url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
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
                                                                                                                                                    alertify.alert(_msgSuccess);
                                                                                                                                                    win.close();
                                                                                                                                                    refresh();
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
                                                                                                                        alertify.alert(data.responseText);
                                                                                                                    }
                                                                                                                });
                                                                                                            },
                                                                                                            error: function (data) {
                                                                                                                $.unblockUI();
                                                                                                                alertify.alert(data.responseText);
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
                                                                                //normal
                                                                                else {


                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                                $.unblockUI();
                                                                                                InitGridInformationExposure();

                                                                                            }

                                                                                            else {

                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                                    type: 'GET',
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {
                                                                                                        _setInstrumentType = data;
                                                                                                    },
                                                                                                    error: function (data) {
                                                                                                        $.unblockUI();
                                                                                                        alertify.alert(data.responseText);
                                                                                                    }
                                                                                                });

                                                                                                if (_GlobClientCode == "20")
                                                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                            InstrumentPK: $('#InstrumentPK').val(),
                                                                                                            InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                                            OrderPrice: $('#OrderPrice').val(),
                                                                                                            RangePrice: 0,
                                                                                                            Lot: 0,
                                                                                                            LotInShare: 100,
                                                                                                            Volume: $('#Volume').val(),
                                                                                                            Amount: $('#Amount').val(),
                                                                                                            TrxType: $('#TrxType').val(),
                                                                                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                            CounterpartPK: 0,
                                                                                                            MarketPK: 1,
                                                                                                            FundPK: $('#FundPK').val(),
                                                                                                            SettledDate: $('#SettledDate').val(),
                                                                                                            LastCouponDate: $('#LastCouponDate').val(),
                                                                                                            NextCouponDate: $('#NextCouponDate').val(),
                                                                                                            MaturityDate: $('#MaturityDate').val(),
                                                                                                            InterestPercent: $('#InterestPercent').val(),
                                                                                                            AccruedInterest: $('#AccruedInterest').val(),
                                                                                                            DoneAccruedInterest: $('#AccruedInterest').val(),
                                                                                                            Tenor: $('#Tenor').val(),
                                                                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                            IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                                            IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                                            DoneAmount: $('#Amount').val(),
                                                                                                            TotalAmount: $('#TotalAmount').val(),
                                                                                                            InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                            PriceMode: $('#PriceMode').val(),
                                                                                                            BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                                            YieldPercent: $('#YieldPercent').val(),
                                                                                                            CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                                            BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                                                                            BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                            BitHTM: $('#BitHTM').is(":checked"),
                                                                                                            BIRate: $('#BIRate').val(),
                                                                                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                            Revision: $('#Revision').val(),

                                                                                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                            OtherRevision: $('#OtherRevision').val(),

                                                                                                            EntryUsersID: sessionStorage.getItem("user")
                                                                                                        };
                                                                                                        $.ajax({
                                                                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                            type: 'POST',
                                                                                                            data: JSON.stringify(InvestmentInstruction),
                                                                                                            contentType: "application/json;charset=utf-8",
                                                                                                            success: function (data) {
                                                                                                                alertify.alert(data);
                                                                                                                win.close();
                                                                                                                refresh();
                                                                                                                $.unblockUI();
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

                                                                                            }
                                                                                        }
                                                                                    });

                                                                                }
                                                                            }
                                                                        })
                                                                        //

                                                                    }
                                                                });
                                                            }
                                                            else {

                                                                //cek range price
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#OrderPrice').val(),
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        _statusExposureRangePrice = data.Validate;
                                                                        //cam only
                                                                        if (data.Validate == 1 && _GlobClientCode == "21") {
                                                                            $.unblockUI();
                                                                            alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                                if (e) {
                                                                                    $.blockUI();
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                                $.unblockUI();
                                                                                                InitGridInformationExposure();

                                                                                            }

                                                                                            else {

                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                                    type: 'GET',
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {
                                                                                                        _setInstrumentType = data;
                                                                                                    },
                                                                                                    error: function (data) {
                                                                                                        $.unblockUI();
                                                                                                        alertify.alert(data.responseText);
                                                                                                    }
                                                                                                });

                                                                                                if (_GlobClientCode == "20")
                                                                                                    _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                            InstrumentPK: $('#InstrumentPK').val(),
                                                                                                            InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                                            OrderPrice: $('#OrderPrice').val(),
                                                                                                            RangePrice: 0,
                                                                                                            Lot: 0,
                                                                                                            LotInShare: 100,
                                                                                                            Volume: $('#Volume').val(),
                                                                                                            Amount: $('#Amount').val(),
                                                                                                            TrxType: $('#TrxType').val(),
                                                                                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                            CounterpartPK: 0,
                                                                                                            MarketPK: 1,
                                                                                                            FundPK: $('#FundPK').val(),
                                                                                                            SettledDate: $('#SettledDate').val(),
                                                                                                            LastCouponDate: $('#LastCouponDate').val(),
                                                                                                            NextCouponDate: $('#NextCouponDate').val(),
                                                                                                            MaturityDate: $('#MaturityDate').val(),
                                                                                                            InterestPercent: $('#InterestPercent').val(),
                                                                                                            AccruedInterest: $('#AccruedInterest').val(),
                                                                                                            DoneAccruedInterest: $('#AccruedInterest').val(),
                                                                                                            Tenor: $('#Tenor').val(),
                                                                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                            IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                                            IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                                            DoneAmount: $('#Amount').val(),
                                                                                                            TotalAmount: $('#TotalAmount').val(),
                                                                                                            InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                            PriceMode: $('#PriceMode').val(),
                                                                                                            BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                                            YieldPercent: $('#YieldPercent').val(),
                                                                                                            CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                                            BitIsRounding: $('#BitIsRounding').is(":checked"),

                                                                                                            BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                            BitHTM: $('#BitHTM').is(":checked"),
                                                                                                            BIRate: $('#BIRate').val(),
                                                                                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                            Revision: $('#Revision').val(),

                                                                                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                            OtherRevision: $('#OtherRevision').val(),
                                                                                                            EntryUsersID: sessionStorage.getItem("user")
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
                                                                                                                            url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
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

                                                                                                                                            alertify.alert(_msgSuccess);
                                                                                                                                            win.close();
                                                                                                                                            refresh();
                                                                                                                                            $.unblockUI();
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
                                                                                                                alertify.alert(data.responseText);
                                                                                                            }
                                                                                                        });
                                                                                                    },
                                                                                                    error: function (data) {
                                                                                                        $.unblockUI();
                                                                                                        alertify.alert(data.responseText);
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
                                                                        //normal
                                                                        else {
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                        $.unblockUI();
                                                                                        InitGridInformationExposure();

                                                                                    }

                                                                                    else {

                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                            type: 'GET',
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                _setInstrumentType = data;
                                                                                            },
                                                                                            error: function (data) {
                                                                                                $.unblockUI();
                                                                                                alertify.alert(data.responseText);
                                                                                            }
                                                                                        });

                                                                                        if (_GlobClientCode == "20")
                                                                                            _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                    InstrumentPK: $('#InstrumentPK').val(),
                                                                                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                                    OrderPrice: $('#OrderPrice').val(),
                                                                                                    RangePrice: 0,
                                                                                                    Lot: 0,
                                                                                                    LotInShare: 100,
                                                                                                    Volume: $('#Volume').val(),
                                                                                                    Amount: $('#Amount').val(),
                                                                                                    TrxType: $('#TrxType').val(),
                                                                                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                    CounterpartPK: 0,
                                                                                                    MarketPK: 1,
                                                                                                    FundPK: $('#FundPK').val(),
                                                                                                    SettledDate: $('#SettledDate').val(),
                                                                                                    LastCouponDate: $('#LastCouponDate').val(),
                                                                                                    NextCouponDate: $('#NextCouponDate').val(),
                                                                                                    MaturityDate: $('#MaturityDate').val(),
                                                                                                    InterestPercent: $('#InterestPercent').val(),
                                                                                                    AccruedInterest: $('#AccruedInterest').val(),
                                                                                                    DoneAccruedInterest: $('#AccruedInterest').val(),
                                                                                                    Tenor: $('#Tenor').val(),
                                                                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                    IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                                    IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                                    DoneAmount: $('#Amount').val(),
                                                                                                    TotalAmount: $('#TotalAmount').val(),
                                                                                                    InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                    PriceMode: $('#PriceMode').val(),
                                                                                                    BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                                    YieldPercent: $('#YieldPercent').val(),
                                                                                                    CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                                    BitIsRounding: $('#BitIsRounding').is(":checked"),

                                                                                                    BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                    BitHTM: $('#BitHTM').is(":checked"),
                                                                                                    BIRate: $('#BIRate').val(),
                                                                                                    InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                    InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                    InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                    Revision: $('#Revision').val(),

                                                                                                    OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                    OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                    OtherRevision: $('#OtherRevision').val(),
                                                                                                    EntryUsersID: sessionStorage.getItem("user")
                                                                                                };
                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                    type: 'POST',
                                                                                                    data: JSON.stringify(InvestmentInstruction),
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {
                                                                                                        alertify.alert(data);
                                                                                                        win.close();
                                                                                                        refresh();
                                                                                                        $.unblockUI();
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

                                                                                    }
                                                                                }
                                                                            });

                                                                        }
                                                                    }
                                                                })
                                                                //


                                                            }


                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText);
                                                        }
                                                    });
                                                }
                                                else {
                                                    $.unblockUI();
                                                    alertify.alert("Can't save data, please check First Coupon Date ! ");
                                                }
                                            },
                                            error: function (data) {
                                                $.unblockUI();
                                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
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
                    }

                }

            });
        }
    });


    $("#BtnSaveSell").click(function (e) {
        _globinstrumentPK = $('#InstrumentPK').val();
        _globinstrumentTypePK = $('#InstrumentTypePK').val();
        _globorderPrice = $('#OrderPrice').val();
        _globvolume = $('#Volume').val();
        _globamount = $('#Amount').val();
        _globcrossFundFromPK = $('#CrossFundFromPK').val();
        _globsettledDate = $('#SettledDate').val();
        _globlastCouponDate = $('#LastCouponDate').val();
        _globnextCouponDate = $('#NextCouponDate').val();
        _globmaturityDate = $('#MaturityDate').val();
        _globinterestPercent = $('#InterestPercent').val();
        _globaccruedInterest = $('#AccruedInterest').val();
        _globtenor = $('#Tenor').val();
        _globinvestmentNotes = $('#InvestmentNotes').val();
        _globincomeTaxInterestAmount = $('#IncomeTaxInterestAmount').val();
        _globincomeTaxGainAmount = $('#IncomeTaxGainAmount').val();
        _globamount = $('#Amount').val();
        _globtotalAmount = $('#TotalAmount').val();
        _globinvestmentTrType = $('#InvestmentTrType').val();
        _globpriceMode = $('#PriceMode').val();
        _globbitIsAmortized = $('#BitIsAmortized').is(":checked");
        _globyieldPercent = $('#YieldPercent').val();
        _globfundPK = $('#FundPK').val();
        _globbitIsRounding = $('#BitIsRounding').is(":checked");
        _globbitForeignTrx = $('#BitForeignTrx').is(":checked");
        _globbitHTM = $('#BitHTM').is(":checked");
        _globbIRate = $('#BIRate').val();
        _globinvestmentStrategy = $('#InvestmentStrategy').val();
        _globinvestmentStyle = $('#InvestmentStyle').val();
        _globinvestmentObjective = $('#InvestmentObjective').val();
        _globrevision = $('#Revision').val();
        _globotherInvestmentObjective = $('#OtherInvestmentObjective').val();
        _globotherInvestmentStyle = $('#OtherInvestmentStyle').val();
        _globotherRevision = $('#OtherRevision').val();



        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var val = validateData();
        var _setInstrumentType = 0;
        var _urlForReference;
        e.handled = true;
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
                        alertify.alert("Save Cancel, Already Generate End Day Trails FundPortfolio");
                        return;
                    }
                    else {
                        if ($("#TotalAmount").val() == 0) {
                            $.unblockUI();
                            alertify.alert("Please Fill All Data First!");
                            return;
                        } else {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Instrument/ValidateCheckFirstCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#InstrumentPK').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {

                                        var Validate = {
                                            ValueDate: $('#DateFrom').val(),
                                            FundPK: $('#FundPK').val(),
                                            InstrumentPK: $('#InstrumentPK').val(),
                                            Volume: $('#Volume').val(),
                                            TrxBuy: $('#TrxBuy').val(),
                                            TrxBuyType: $('#TrxBuyType').val(),
                                            BitHTM: $('#BitHTM').is(":checked"),
                                            BIRate: $('#BIRate').val(),
                                            InvestmentStrategy: $('#InvestmentStrategy').val(),
                                            InvestmentStyle: $('#InvestmentStyle').val(),
                                            InvestmentObjective: $('#InvestmentObjective').val(),
                                            Revision: $('#Revision').val(),
                                            OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                            OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                            OtherRevision: $('#OtherRevision').val(),

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'POST',
                                            data: JSON.stringify(Validate),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == 1) {
                                                    $.unblockUI();
                                                    alertify.alert("Short Sell")
                                                    return;
                                                }
                                                else {

                                                    //cek range price
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#OrderPrice').val(),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            _statusExposureRangePrice = data.Validate;
                                                            //cam only
                                                            if (data.Validate == 1 && _GlobClientCode == "21") {
                                                                $.unblockUI();
                                                                alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                    if (e) {
                                                                        $.blockUI();
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data.AlertExposure == 3 || data.AlertExposure == 4 || data.AlertExposure == 7 || data.AlertExposure == 8) {
                                                                                    $.unblockUI();
                                                                                    InitGridInformationExposure();

                                                                                }

                                                                                else {
                                                                                    // DISINI EXPOSURE MINIMUM
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            _setInstrumentType = data;
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText);
                                                                                        }
                                                                                    });

                                                                                    if (_GlobClientCode == "20")
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                                                InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                                OrderPrice: $('#OrderPrice').val(),
                                                                                                RangePrice: 0,
                                                                                                Lot: 0,
                                                                                                LotInShare: 100,
                                                                                                Volume: $('#Volume').val(),
                                                                                                Amount: $('#Amount').val(),
                                                                                                TrxType: $('#TrxType').val(),
                                                                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                                CounterpartPK: 0,
                                                                                                MarketPK: 1,
                                                                                                FundPK: $('#FundPK').val(),
                                                                                                SettledDate: $('#SettledDate').val(),
                                                                                                LastCouponDate: $('#LastCouponDate').val(),
                                                                                                NextCouponDate: $('#NextCouponDate').val(),
                                                                                                MaturityDate: $('#MaturityDate').val(),
                                                                                                InterestPercent: $('#InterestPercent').val(),
                                                                                                AccruedInterest: $('#AccruedInterest').val(),
                                                                                                Tenor: $('#Tenor').val(),
                                                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                                IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                                IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                                DoneAmount: $('#Amount').val(),
                                                                                                TotalAmount: $('#TotalAmount').val(),
                                                                                                PriceMode: $('#PriceMode').val(),
                                                                                                BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                                YieldPercent: $('#YieldPercent').val(),
                                                                                                BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                                                                TrxBuy: $('#TrxBuy').val(),
                                                                                                TrxBuyType: $('#TrxBuyType').val(),
                                                                                                InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                                AcqDate: $('#AcqDate').val(),
                                                                                                AcqPrice: $('#AcqPrice').val(),
                                                                                                AcqVolume: $('#AcqVolume').val(),
                                                                                                CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                                BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                                BitHTM: $('#BitHTM').is(":checked"),
                                                                                                BIRate: $('#BIRate').val(),
                                                                                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                                InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                                InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                                Revision: $('#Revision').val(),
                                                                                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                                OtherRevision: $('#OtherRevision').val(),
                                                                                                EntryUsersID: sessionStorage.getItem("user")
                                                                                            };
                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                type: 'POST',
                                                                                                data: JSON.stringify(InvestmentInstruction),
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    if ($('#CrossFundFromPK').val() != "") {
                                                                                                        InsertCrossFundInvestmentBuy();
                                                                                                    }
                                                                                                    _msgSuccess = data;
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                        type: 'GET',
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            _InvPKForEmail = data;
                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
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



                                                                                                                                alertify.alert(_msgSuccess);
                                                                                                                                win.close();
                                                                                                                                $.unblockUI();
                                                                                                                                refresh();
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
                                                                                                    alertify.alert(data.responseText);
                                                                                                }
                                                                                            });
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText);
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
                                                            //normal
                                                            else {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val() + "/" + $('#TrxType').val() + "/2",
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data.AlertExposure == 3 || data.AlertExposure == 4 || data.AlertExposure == 7 || data.AlertExposure == 8) {
                                                                            $.unblockUI();
                                                                            InitGridInformationExposure();

                                                                        }

                                                                        else {
                                                                            // DISINI EXPOSURE MINIMUM
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    _setInstrumentType = data;
                                                                                },
                                                                                error: function (data) {
                                                                                    $.unblockUI();
                                                                                    alertify.alert(data.responseText);
                                                                                }
                                                                            });

                                                                            if (_GlobClientCode == "20")
                                                                                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                        InstrumentPK: $('#InstrumentPK').val(),
                                                                                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                                        OrderPrice: $('#OrderPrice').val(),
                                                                                        RangePrice: 0,
                                                                                        Lot: 0,
                                                                                        LotInShare: 100,
                                                                                        Volume: $('#Volume').val(),
                                                                                        Amount: $('#Amount').val(),
                                                                                        TrxType: $('#TrxType').val(),
                                                                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                                        CounterpartPK: 0,
                                                                                        MarketPK: 1,
                                                                                        FundPK: $('#FundPK').val(),
                                                                                        SettledDate: $('#SettledDate').val(),
                                                                                        LastCouponDate: $('#LastCouponDate').val(),
                                                                                        NextCouponDate: $('#NextCouponDate').val(),
                                                                                        MaturityDate: $('#MaturityDate').val(),
                                                                                        InterestPercent: $('#InterestPercent').val(),
                                                                                        AccruedInterest: $('#AccruedInterest').val(),
                                                                                        Tenor: $('#Tenor').val(),
                                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                                        IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                                        IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                                        DoneAmount: $('#Amount').val(),
                                                                                        TotalAmount: $('#TotalAmount').val(),
                                                                                        PriceMode: $('#PriceMode').val(),
                                                                                        BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                                                        YieldPercent: $('#YieldPercent').val(),
                                                                                        BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                                                        TrxBuy: $('#TrxBuy').val(),
                                                                                        TrxBuyType: $('#TrxBuyType').val(),
                                                                                        InvestmentTrType: $('#InvestmentTrType').val(),
                                                                                        AcqDate: $('#AcqDate').val(),
                                                                                        AcqPrice: $('#AcqPrice').val(),
                                                                                        AcqVolume: $('#AcqVolume').val(),
                                                                                        CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                                                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                                                        BitHTM: $('#BitHTM').is(":checked"),
                                                                                        BIRate: $('#BIRate').val(),
                                                                                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                                                        InvestmentStyle: $('#InvestmentStyle').val(),
                                                                                        InvestmentObjective: $('#InvestmentObjective').val(),
                                                                                        Revision: $('#Revision').val(),
                                                                                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                                                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                                                        OtherRevision: $('#OtherRevision').val(),
                                                                                        EntryUsersID: sessionStorage.getItem("user")
                                                                                    };
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                        type: 'POST',
                                                                                        data: JSON.stringify(InvestmentInstruction),
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {

                                                                                            if ($('#CrossFundFromPK').val() != "") {
                                                                                                InsertCrossFundInvestmentBuy();
                                                                                            }
                                                                                            _msgSuccess = data;
                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Investment/GetLastInvestmentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                type: 'GET',
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    _InvPKForEmail = data;
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _InvPKForEmail,
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


                                                                                                                        alertify.alert(_msgSuccess);
                                                                                                                        win.close();
                                                                                                                        $.unblockUI();
                                                                                                                        refresh();
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
                                                                                            alertify.alert(data.responseText);
                                                                                        }
                                                                                    });
                                                                                },
                                                                                error: function (data) {
                                                                                    $.unblockUI();
                                                                                    alertify.alert(data.responseText);
                                                                                }


                                                                            });



                                                                        }


                                                                    }
                                                                });

                                                            }
                                                        }
                                                    })
                                                    //

                                                }

                                            }

                                        });
                                    }
                                    else {
                                        $.unblockUI();
                                        alertify.alert("Can't save data, please check First Coupon Date ! ");
                                    }
                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);;
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

        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
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



    function gridInvestmentInstructionBuyOnlyDataBound(e) {


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

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.OrderStatusDesc == "4.MATCH") {
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

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.OrderStatusDesc == "4.MATCH") {
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

    $("#BtnApproveBySelectedBondBuy").click(function (e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
        }
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
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        HighRiskType: 1,
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
                                    url: window.location.origin + "/Radsoft/OMSBond/ValidateApproveBySelectedDataOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                InstrumentTypePK: 2,
                                                TrxType: 1,
                                                FundID: $("#FilterFundID").val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateFrom').val(),
                                                ApprovedUsersID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/OMSBond/ApproveOMSBondBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    $("#BtnRejectBySelectedBondBuy").click(function (e) {

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
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSBond/ValidateRejectBySelectedDataOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSBond/RejectOMSBondBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    $("#BtnApproveBySelectedBondSell").click(function (e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
        }

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
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    var ValidateInvestment = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        HighRiskType: 1,
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
                                    url: window.location.origin + "/Radsoft/OMSBond/ValidateApproveBySelectedDataOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                InstrumentTypePK: 2,
                                                TrxType: 2,
                                                FundID: $("#FilterFundID").val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateFrom').val(),
                                                ApprovedUsersID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/OMSBond/ApproveOMSBondBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    $("#BtnRejectBySelectedBondSell").click(function (e) {

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
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSBond/ValidateRejectBySelectedDataOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSBond/RejectOMSBondBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    function GetNextCouponDate() {
        if ($("#LastCouponDate").data("kendoDatePicker").value() != null) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#NextCouponDate").data("kendoDatePicker").value(new Date(data));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }
    }

    function GetUpNextCouponDate() {
        if ($("#UpLastCouponDate").data("kendoDatePicker").value() != null) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#UpLastCouponDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpInstrumentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#UpNextCouponDate").data("kendoDatePicker").value(new Date(data));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }
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

    function RecalNetAmount() {
        if ($('#InstrumentPK').val() == null || $('#InstrumentPK').val() == "" || $('#OrderPrice').val() == null || $('#OrderPrice').val() == 0 || $('#Volume').val() == null || $('#Volume').val() == 0) {
            return;
        }
        var Bond = {
            InstrumentPK: $('#InstrumentPK').val(),
            InstrumentTypePK: 2,
            ValueDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            NextCouponDate: kendo.toString($("#NextCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            LastCouponDate: kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            Price: $('#OrderPrice').val(),
            Volume: $('#Volume').val(),
            //AcqPrice: $('#AcqPrice').val(),
            //AcqDate: kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            //AcqVolume: $('#AcqVolume').val(),
            //AcqPrice1: $('#AcqPrice1').val(),
            //AcqDate1: $("#AcqDate1").val(),
            //AcqVolume1: $('#AcqVolume1').val(),
            //AcqPrice2: $('#AcqPrice2').val(),
            //AcqDate2: $('#AcqDate2').val(),
            //AcqVolume2: $('#AcqVolume2').val(),
            //AcqPrice3: $('#AcqPrice3').val(),
            //AcqDate3: $('#AcqDate3').val(),
            //AcqVolume3: $('#AcqVolume3').val(),
            //AcqPrice4: $('#AcqPrice4').val(),
            //AcqDate4: $('#AcqDate4').val(),
            //AcqVolume4: $('#AcqVolume4').val(),
            //AcqPrice5: $('#AcqPrice5').val(),
            //AcqDate5: $('#AcqDate5').val(),
            //AcqVolume5: $('#AcqVolume5').val(),
            //AcqPrice6: $('#AcqPrice6').val(),
            //AcqDate6: $('#AcqDate6').val(),
            //AcqVolume6: $('#AcqVolume6').val(),
            //AcqPrice7: $('#AcqPrice7').val(),
            //AcqDate7: $('#AcqDate7').val(),
            //AcqVolume7: $('#AcqVolume7').val(),
            //AcqPrice8: $('#AcqPrice8').val(),
            //AcqDate8: $('#AcqDate8').val(),
            //AcqVolume8: $('#AcqVolume8').val(),
            //AcqPrice9: $('#AcqPrice9').val(),
            //AcqDate9: $('#AcqDate9').val(),
            //AcqVolume9: $('#AcqVolume9').val(),

        };

        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/GetGrossAmount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(Bond),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                //$("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                //$("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                $("#DoneAmount").data("kendoNumericTextBox").value(data.GrossAmount);
                //$("#TotalAmount").data("kendoNumericTextBox").value(data.NetAmount);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    //------------------------------------

    function OnChangeUpVolume() {
        price = $("#UpOrderPrice").data("kendoNumericTextBox").value() / 100
        $("#UpAmount").data("kendoNumericTextBox").value($("#UpVolume").data("kendoNumericTextBox").value() * price);
        //$("#UpAcqVolume").data("kendoNumericTextBox").value($("#UpVolume").data("kendoNumericTextBox").value());
        UprecalYield();
        UpRecalNetAmount();
    }

    function OnChangeUpSettledDate() {

        //if ($("#UpSettledDate").val() < $("#DateFrom").val()) {
        //    alertify.alert("Please Check Settle Date < Value Date !");
        //    return 0;
        //}
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#UpSettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpInstrumentPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpLastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                $("#UpNextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                return;
            },
            error: function (data) {
                alertify.alert("Please Fill Value Date or Instrument");
                $("#UpInstrumentPK").val("");
                $("#UpInstrumentID").val("");
            }
        });

        UprecalYield();
        UpRecalNetAmount();
    }

    function OnChangeUpOrderPrice() {
        price = $("#UpOrderPrice").data("kendoNumericTextBox").value() / 100
        $("#UpAmount").data("kendoNumericTextBox").value($("#UpVolume").data("kendoNumericTextBox").value() * price);
        UprecalYield();
        UpRecalNetAmount();
    }

    function UpRecalNetAmount() {

        var Bond = {
            InstrumentPK: $('#UpInstrumentPK').val(),
            InstrumentTypePK: 2,
            ValueDate: kendo.toString($("#UpSettledDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            SettledDate: kendo.toString($("#UpSettledDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            NextCouponDate: kendo.toString($("#UpNextCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            LastCouponDate: kendo.toString($("#UpLastCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
            Price: $('#UpOrderPrice').val(),
            Volume: $('#UpVolume').val(),


            //AcqPrice: $('#UpAcqPrice').val(),
            //AcqDate: $("#UpAcqDate").val(),
            //AcqVolume: $('#UpAcqVolume').val(),
            //AcqPrice1: $('#UpAcqPrice1').val(),
            //AcqDate1: $("#UpAcqDate1").val(),
            //AcqVolume1: $('#UpAcqVolume1').val(),
            //AcqPrice2: $('#UpAcqPrice2').val(),
            //AcqDate2: $('#UpAcqDate2').val(),
            //AcqVolume2: $('#UpAcqVolume2').val(),
            //AcqPrice3: $('#UpAcqPrice3').val(),
            //AcqDate3: $('#UpAcqDate3').val(),
            //AcqVolume3: $('#UpAcqVolume3').val(),
            //AcqPrice4: $('#UpAcqPrice4').val(),
            //AcqDate4: $('#UpAcqDate4').val(),
            //AcqVolume4: $('#UpAcqVolume4').val(),
            //AcqPrice5: $('#UpAcqPrice5').val(),
            //AcqDate5: $('#UpAcqDate5').val(),
            //AcqVolume5: $('#UpAcqVolume5').val(),
            //AcqPrice6: $('#UpAcqPrice6').val(),
            //AcqDate6: $('#UpAcqDate6').val(),
            //AcqVolume6: $('#UpAcqVolume6').val(),
            //AcqPrice7: $('#UpAcqPrice7').val(),
            //AcqDate7: $('#UpAcqDate7').val(),
            //AcqVolume7: $('#UpAcqVolume7').val(),
            //AcqPrice8: $('#UpAcqPrice8').val(),
            //AcqDate8: $('#UpAcqDate8').val(),
            //AcqVolume8: $('#UpAcqVolume8').val(),
            //AcqPrice9: $('#UpAcqPrice9').val(),
            //AcqDate9: $('#UpAcqDate9').val(),
            //AcqVolume9: $('#UpAcqVolume9').val(),

        };

        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/GetGrossAmount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(Bond),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                //$("#UpIncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                //$("#UpIncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                $("#UpDoneAmount").data("kendoNumericTextBox").value(data.GrossAmount);
                //$("#UpTotalAmount").data("kendoNumericTextBox").value(data.NetAmount);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    $("#BtnOMSBondListing").click(function () {
        showOMSBondListing();
    });

    function showOMSBondListing(e) {

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

        WinOMSBondListing.center();
        WinOMSBondListing.open();

    }

    $("#BtnOkOMSBondListing").click(function () {
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
                    var OMSBondListing = {
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
                        url: window.location.origin + "/Radsoft/OMSBond/OMSBondListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(OMSBondListing),
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

    $("#BtnCancelOMSBondListing").click(function () {

        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinOMSBondListing.close();
                alertify.success("Cancel Listing");
            }
        });
    });

    function onWinOMSBondListingClose() {
        $("#Message").val("")
    }

    //-- INSTRUMENT

    $("#btnListInstrumentPK").click(function () {
        if ($("#TrxType").val() == 1)
        {
            if ($("#CrossFundFromPK").val() == "" || $("#CrossFundFromPK").val() == 0) {
                initListInstrumentPK();

                WinListInstrument.center();
                WinListInstrument.open();
            }
            else {

                initListInstrumentPKByCrossFund();

                WinListInstrumentByCrossFund.center();
                WinListInstrumentByCrossFund.open();
            }
        }
        else
        {
            initListInstrumentPK();

            WinListInstrument.center();
            WinListInstrument.open();
        }
        
  

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
                                 BegBalance: { type: "number" },
                                 MovBalance: { type: "number" },
                                 Balance: { type: "number" },
                                 AcqPrice: { type: "number" },
                                 TrxBuy: { type: "number" },
                                 TrxBuyType: { type: "string" },
                                 PaymentType: { type: "number" },
                             }
                         }

                     },
                     group:
                     [
                     {
                         field: "TrxBuyType"
                     },
                     ],
                 });
    }

    function initListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").val() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val();
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
                    { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                ]
            });
        }
        else {
            if (_GlobClientCode == "03" || _GlobClientCode == "20" || _GlobClientCode == "21" || _GlobClientCode == "22") {
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
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "MovBalance", title: "Mov Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "AvgPrice", title: "Acq Price", hidden: true, width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                        { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },

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
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "MovBalance", title: "Mov Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "AvgPrice", title: "Acq Price", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "AcqDate", title: "Acq Date", width: 150, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                        { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    ]
                });
            }
        }


    }

    function ListInstrumentSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridListInstrument").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.ID);
            $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#Amount").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
            $("#TrxBuy").val(dataItemX.TrxBuy);
            $("#TrxBuyType").val(dataItemX.TrxBuyType);
            $("#AcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
            $("#AcqPrice").data("kendoNumericTextBox").value(dataItemX.AvgPrice);
            $("#AcqVolume").data("kendoNumericTextBox").value(dataItemX.BegBalance);
            globPaymentType = dataItemX.PaymentType;
            //console.log(dataItemX.PaymentType);
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.InstrumentPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                    $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                    return;
                },
                error: function (data) {
                    alertify.alert("Please Fill Value Date or Instrument");
                    $("#InstrumentPK").val("");
                    $("#InstrumentID").val("");
                }
            });


            //if (_GlobClientCode == "05")
            //{
                getInstrumentTypeByInstrumentPK(dataItemX.InstrumentPK);
            //}
            
            WinListInstrument.close();

        }
        e.handled = true;
    }

    $("#btnClearListInstrument").click(function () {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#Volume").data("kendoNumericTextBox").value("");
        $("#Amount").data("kendoNumericTextBox").value(0);
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#InterestPercent").data("kendoNumericTextBox").value(0);

        $("#OrderPrice").data("kendoNumericTextBox").value("");
        $("#LastCouponDate").data("kendoDatePicker").value("");
        $("#NextCouponDate").data("kendoDatePicker").value("");
        $("#AccruedInterest").data("kendoNumericTextBox").value(0);
        $("#DoneAmount").data("kendoNumericTextBox").value(0);
        $("#TrxBuy").val("");
        $("#TrxBuyType").val("");
        $("#AcqDate").data("kendoDatePicker").value("");
        $("#AcqPrice").data("kendoNumericTextBox").value(0);
        $("#AcqVolume").data("kendoNumericTextBox").value(0);
    });

    function initUpListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpTrxType").data("kendoComboBox").value() + "/" + $("#UpFundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpMarketPK").val();
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
                    { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                ]
            });
        }
        else {
            if (_GlobClientCode == "03" || _GlobClientCode == "20" || _GlobClientCode == "21" || _GlobClientCode == "22" ) {
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "MovBalance", title: "Mov Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "AvgPrice", title: "Acq Price", hidden: true, width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                        { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "MovBalance", title: "Mov Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                        { field: "AvgPrice", title: "Acq Price", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "AcqDate", title: "Acq Date", width: 150, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                        { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                        { field: "TrxBuyType", title: "Type", hidden: true, width: 150 },
                        { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    ]
                });

            }
        }


    }

    function UpListInstrumentSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridUpListInstrument").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
            $("#UpInstrumentID").val(dataItemX.ID);
            $("#UpVolume").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#UpAmount").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#UpMaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            $("#UpInterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
            $("#UpTrxBuy").val(dataItemX.TrxBuy);
            $("#UpTrxBuyType").val(dataItemX.TrxBuyType);
            $("#UpAcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
            $("#UpAcqPrice").data("kendoNumericTextBox").value(dataItemX.AvgPrice);
            $("#UpAcqVolume").data("kendoNumericTextBox").value(dataItemX.BegBalance);
            globPaymentType = dataItemX.PaymentType;
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#UpSettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.InstrumentPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#UpLastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                    $("#UpNextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                    return;
                },
                error: function (data) {
                    alertify.alert("Please Fill Value Date or Instrument");
                    $("#UpInstrumentPK").val("");
                    $("#UpInstrumentID").val("");
                }
            });





            //if (_GlobClientCode == "05") {
                getUpInstrumentTypeByInstrumentPK(dataItemX.InstrumentPK);
            //}

            
            WinUpListInstrument.close();

        }
        e.handled = true;
    }

    $("#btnClearUpListInstrument").click(function () {
        $("#UpInstrumentPK").val("");
        $("#UpInstrumentID").val("");
        $("#UpVolume").data("kendoNumericTextBox").value("");
        $("#UpAmount").data("kendoNumericTextBox").value(0);
        $("#UpMaturityDate").data("kendoDatePicker").value("");
        $("#UpInterestPercent").data("kendoNumericTextBox").value(0);
        $("#UpTrxBuy").val("");
        $("#UpTrxBuyType").val("");

        $("#UpOrderPrice").data("kendoNumericTextBox").value("");
        $("#UpLastCouponDate").data("kendoDatePicker").value("");
        $("#UpNextCouponDate").data("kendoDatePicker").value("");
        $("#UpAccruedInterest").data("kendoNumericTextBox").value(0);
        $("#UpDoneAmount").data("kendoNumericTextBox").value(0);
        $("#UpAcqDate").data("kendoDatePicker").value("");
        $("#UpAcqPrice").data("kendoNumericTextBox").value(0);
        $("#UpAcqVolume").data("kendoNumericTextBox").value(0);
    });



    function UprecalYield() {

        if ($("#UpSettledDate").data("kendoDatePicker").value != null || $("#UpMaturityDate").data("kendoDatePicker").value != null
            || $("#UpAmount").data("kendoNumericTextBox").value != 0 || $("#UpAmount").data("kendoNumericTextBox").value != null
            || $("#UpVolume").data("kendoNumericTextBox").value != 0 || $("#UpVolume").data("kendoNumericTextBox").value != null
            ) {


            var SettledDate = new Date($("#UpSettledDate").val());
            var MaturityDate = new Date($("#UpMaturityDate").val());
            var sday = parseInt(SettledDate.getDate());
            var smonth = parseInt(SettledDate.getMonth() + 1);
            var syear = parseInt(SettledDate.getFullYear());
            var mday = parseInt(MaturityDate.getDate());
            var mmonth = parseInt(MaturityDate.getMonth() + 1);
            var myear = parseInt(MaturityDate.getFullYear());
            var interest = parseFloat($("#UpInterestPercent").data("kendoNumericTextBox").value() / 100.0);

            var price = parseFloat($("#UpAmount").data("kendoNumericTextBox").value());
            var face = parseFloat($("#UpVolume").data("kendoNumericTextBox").value());

            var n = pay_periods(globPaymentType, sday, smonth, syear, mday, mmonth, myear);

            var ytm;
            ytm = 100 * bond_ytm(price / face, interest, globPaymentType, n);
            $("#UpYieldPercent").data("kendoNumericTextBox").value(ytm.toFixed(3));
            //console.log(n);
            //console.log(ytm.toFixed(3));
        }

    }

    function getDataSourceListInstrumentByCrossFund(_url) {
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
                         WinListInstrumentByCrossFund.close();
                         alertify.alert("No Data For Cross Fund");
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
                                 BegBalance: { type: "number" },
                                 MovBalance: { type: "number" },
                                 Balance: { type: "number" },
                                 AcqPrice: { type: "number" },
                                 TrxBuy: { type: "number" },
                                 TrxBuyType: { type: "string" },
                                 PaymentType: { type: "number" },
                             }
                         }

                     },
                     group:
                     [
                     {
                         field: "TrxBuyType"
                     },
                     ],
                 });
    }

    function initListInstrumentPKByCrossFund() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPKByCrossFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").val() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val() + "/" + $("#CrossFundFromPK").data("kendoComboBox").value();
        var dsListInstrumentByCrossFund = getDataSourceListInstrumentByCrossFund(_url);
        $("#gridListInstrumentByCrossFund").kendoGrid({
            dataSource: dsListInstrumentByCrossFund,
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
                   { command: { text: "Select", click: ListInstrumentByCrossFundSelect }, title: " ", width: 100 },
                   { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                   { field: "Price", title: "", hidden: true, width: 100 },
                   { field: "ID", title: "ID", width: 300 },
                   { field: "Balance", title: "Balance", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                   { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                ]
            });
        }
      


    function ListInstrumentByCrossFundSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridListInstrumentByCrossFund").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.ID);
            $("#Price").val(dataItemX.Price);
            $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#Amount").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
            $("#TrxBuy").val(dataItemX.TrxBuy);
            $("#TrxBuyType").val(dataItemX.TrxBuyType);
            $("#AcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
            $("#AcqPrice").data("kendoNumericTextBox").value(dataItemX.AvgPrice);
            $("#AcqVolume").data("kendoNumericTextBox").value(dataItemX.BegBalance);
            globPaymentType = dataItemX.PaymentType;
            $("#InstrumentTypePK").data("kendoComboBox").value(dataItemX.InstrumentTypePK);
            //console.log(dataItemX.PaymentType);
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.InstrumentPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                    $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                    return;
                },
                error: function (data) {
                    alertify.alert("Please Fill Value Date or Instrument");
                    $("#InstrumentPK").val("");
                    $("#InstrumentID").val("");
                }
            });




            WinListInstrumentByCrossFund.close();

        }
        e.handled = true;
    }




    function getDataSourceUpListInstrumentByCrossFund(_url) {
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
                         WinUpListInstrumentByCrossFund.close();
                         alertify.alert("No Data For Cross Fund");
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
                                 BegBalance: { type: "number" },
                                 MovBalance: { type: "number" },
                                 Balance: { type: "number" },
                                 AcqPrice: { type: "number" },
                                 TrxBuy: { type: "number" },
                                 TrxBuyType: { type: "string" },
                                 PaymentType: { type: "number" },
                             }
                         }

                     },
                     group:
                     [
                     {
                         field: "TrxBuyType"
                     },
                     ],
                 });
    }

    function initUpListInstrumentPKByCrossFund() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSBondByMarketPKByCrossFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").val() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val() + "/" + $("#CrossFundFromPK").data("kendoComboBox").value();
        var dsUpListInstrumentByCrossFund = getDataSourceUpListInstrumentByCrossFund(_url);
        $("#gridUpListInstrumentByCrossFund").kendoGrid({
            dataSource: dsUpListInstrumentByCrossFund,
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
               { command: { text: "Select", click: UpListInstrumentByCrossFundSelect }, title: " ", width: 100 },
               { field: "InstrumentPK", title: "", hidden: true, width: 100 },
               { field: "Price", title: "", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 300 },
               { field: "Balance", title: "Balance", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
               { field: "InterestPercent", title: "Interest Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
               { field: "PaymentType", title: "PaymentType", hidden: true, width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },

            ]
        });
    }



    function UpListInstrumentByCrossFundSelect(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridUpListInstrumentByCrossFund").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            $("#UpInstrumentPK").val(dataItemX.InstrumentPK);
            $("#UpInstrumentID").val(dataItemX.ID);
            $("#UpPrice").val(dataItemX.Price);
            $("#UpVolume").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#UpAmount").data("kendoNumericTextBox").value(dataItemX.Balance);
            $("#UpMaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            $("#UpInterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
            $("#UpTrxBuy").val(dataItemX.TrxBuy);
            $("#UpTrxBuyType").val(dataItemX.TrxBuyType);
            $("#UpAcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
            $("#UpAcqPrice").data("kendoNumericTextBox").value(dataItemX.AvgPrice);
            $("#UpAcqVolume").data("kendoNumericTextBox").value(dataItemX.BegBalance);
            globPaymentType = dataItemX.PaymentType;
            //console.log(dataItemX.PaymentType);
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.InstrumentPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#UpLastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                    $("#UpNextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));


                    return;
                },
                error: function (data) {
                    alertify.alert("Please Fill Value Date or Instrument");
                    $("#UpInstrumentPK").val("");
                    $("#UpInstrumentID").val("");
                }
            });




            WinUpListInstrumentByCrossFund.close();

        }
        e.handled = true;
    }

    function getInstrumentTypeByInstrumentPK(_instrumentPK) {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _instrumentPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentTypePK").data("kendoComboBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
    function getUpInstrumentTypeByInstrumentPK(_instrumentPK) {

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _instrumentPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpInstrumentTypePK").data("kendoComboBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
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



    function InitGridInformationExposure() {
        WinValidateFundExposure.center();
        WinValidateFundExposure.open();


        $("#gridInformationExposure").empty();



        if (_GlobClientCode == "20") {

            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureForCustom20/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
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
                    { field: "FundID", title: "Fund ID", width: 200 },
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
        else {
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



    }

    


    function onWinValidateFundExposureClose() {
        var _urlForReference;

        alertify.confirm("Are You Sure To Continue Save Data ?", function () {

            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    _setInstrumentType = data;
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            if (_GlobClientCode == "20")
                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                        InstrumentPK: $('#InstrumentPK').val(),
                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                        OrderPrice: $('#OrderPrice').val(),
                        RangePrice: 0,
                        Lot: 0,
                        LotInShare: 100,
                        Volume: $('#Volume').val(),
                        Amount: $('#Amount').val(),
                        TrxType: $('#TrxType').val(),
                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                        CounterpartPK: 0,
                        MarketPK: 1,
                        FundPK: $('#FundPK').val(),
                        SettledDate: $('#SettledDate').val(),
                        LastCouponDate: $('#LastCouponDate').val(),
                        NextCouponDate: $('#NextCouponDate').val(),
                        MaturityDate: $('#MaturityDate').val(),
                        InterestPercent: $('#InterestPercent').val(),
                        AccruedInterest: $('#AccruedInterest').val(),
                        Tenor: $('#Tenor').val(),
                        InvestmentNotes: $('#InvestmentNotes').val(),
                        IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                        IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                        DoneAmount: $('#Amount').val(),
                        TotalAmount: $('#TotalAmount').val(),
                        PriceMode: $('#PriceMode').val(),
                        BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                        YieldPercent: $('#YieldPercent').val(),
                        BitIsRounding: $('#BitIsRounding').is(":checked"),
                        TrxBuy: $('#TrxBuy').val(),
                        TrxBuyType: $('#TrxBuyType').val(),
                        InvestmentTrType: $('#InvestmentTrType').val(),
                        AcqDate: $('#AcqDate').val(),
                        AcqPrice: $('#AcqPrice').val(),
                        AcqVolume: $('#AcqVolume').val(),
                        CrossFundFromPK: $('#CrossFundFromPK').val(),
                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                        BitHTM: $('#BitHTM').is(":checked"),
                        BIRate: $('#BIRate').val(),
                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                        InvestmentStyle: $('#InvestmentStyle').val(),
                        InvestmentObjective: $('#InvestmentObjective').val(),
                        Revision: $('#Revision').val(),
                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                        OtherRevision: $('#OtherRevision').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                        type: 'POST',
                        data: JSON.stringify(InvestmentInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($('#CrossFundFromPK').val() != "") {
                                InsertCrossFundInvestmentBuy();
                            }
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

                                                        if ((_statusExposureRangePrice == 1 && _GlobClientCode == "21") || (_statusExposureRangePrice == 1 && _GlobClientCode != "21")) {

                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val() + "/" + _investmentPK,
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {


                                                                    if (_ComplianceEmail == 'TRUE') {
                                                                        var HighRisk = {
                                                                            Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                            InvestmentPK: _investmentPK,
                                                                            HighRiskType: 2,
                                                                        };

                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                            type: 'POST',
                                                                            data: JSON.stringify(HighRisk),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {

                                                                                alertify.alert("Insert Investment Success !");
                                                                                win.close();
                                                                                refresh();
                                                                            },
                                                                            error: function (data) {
                                                                                alertify.alert(data.responseText);
                                                                            }
                                                                        });
                                                                    }
                                                                    else {
                                                                        alertify.alert("Insert Investment Success !");
                                                                        win.close();
                                                                        refresh();
                                                                    }

                                                                }
                                                            });
                                                        }
                                                        else {
                                                            alertify.alert("Insert Investment Success !");
                                                            win.close();
                                                            refresh();
                                                        }

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
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                },
                                error: function (data) {
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



    function InitGridUpInformationExposure() {
        WinUpValidateFundExposure.center();
        WinUpValidateFundExposure.open();


        $("#gridUpInformationExposure").empty();

        if (_GlobClientCode == "20") {
            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureForCustom20/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UpFundPK').val(),
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
                    { field: "FundID", title: "Fund ID", width: 200 },
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
        else {
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
    }

    function onWinUpValidateFundExposureClose() {
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {


            var InvestmentInstruction = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                PeriodPK: _defaultPeriodPK,
                ValueDate: $('#DateFrom').val(),
                Reference: $("#UpReference").val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                InstructionDate: $('#DateFrom').val(),
                InstrumentPK: $('#UpInstrumentPK').val(),
                InstrumentTypePK: _setInstrumentType,
                OrderPrice: $('#UpOrderPrice').val(),
                RangePrice: 0,
                OrderStatusDesc: $('#OrderStatus').val(),
                Lot: 0,
                LotInShare: 100,
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                TrxType: $('#UpTrxType').val(),
                TrxTypeID: $("#UpTrxType").data("kendoComboBox").text(),
                CounterpartPK: 0,
                FundPK: $('#UpFundPK').val(),
                SettledDate: $('#UpSettledDate').val(),
                LastCouponDate: $('#UpLastCouponDate').val(),
                NextCouponDate: $('#UpNextCouponDate').val(),
                MaturityDate: $('#UpMaturityDate').val(),
                InterestPercent: $('#UpInterestPercent').val(),
                DoneAccruedInterest: $('#UpAccruedInterest').val(),
                AccruedInterest: $('#UpAccruedInterest').val(),
                Tenor: $('#UpTenor').val(),
                InvestmentNotes: $('#InvestmentNotes').val(),
                DoneAmount: $('#UpAmount').val(),
                PriceMode: $('#UpPriceMode').val(),
                BitIsAmortized: $('#UpBitIsAmortized').is(":checked"),
                YieldPercent: $('#UpYieldPercent').val(),
                BitIsRounding: $('#UpBitIsRounding').is(":checked"),
                EntryUsersID: sessionStorage.getItem("user"),
                MarketPK: 1,
                CrossFundFromPK: $('#UpCrossFundFromPK').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                BitHTM: $('#UpBitHTM').is(":checked"),
                BIRate: $('#UpBIRate').val(),
                InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                InvestmentStyle: $('#UpInvestmentStyle').val(),
                InvestmentObjective: $('#UpInvestmentObjective').val(),
                Revision: $('#UpRevision').val(),
                OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                OtherRevision: $('#UpOtherRevision').val(),
                UpdateUsersID: sessionStorage.getItem("user"),
                InvestmentTrType: $('#UpInvestmentTrType').val(),
            };




            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                type: 'POST',
                data: JSON.stringify(InvestmentInstruction),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if ($('#UpCrossFundFromPK').val() != "") {
                        UpdateCrossFundInvestmentBuy();
                    }


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
                                                console.log("done email fund exposure");

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _investmentPK,
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
                                                                    console.log("done email price exposure");
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
                                        alertify.alert('Update Success');
                                        WinUpdateInvestment.close();
                                        refresh();
                                    }

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
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });


        }, function () {
            var InvestmentInstruction = {
                InvestmentPK: $('#UpInvestmentPK').val(),
                HistoryPK: $('#UpHistoryPK').val(),
                DealingPK: $('#UpDealingPK').val(),
                PeriodPK: _defaultPeriodPK,
                ValueDate: $('#DateFrom').val(),
                Reference: $("#UpReference").val(),
                StatusInvestment: $('#UpStatusInvestment').val(),
                InstructionDate: $('#DateFrom').val(),
                InstrumentPK: $('#UpInstrumentPK').val(),
                InstrumentTypePK: _setInstrumentType,
                OrderPrice: $('#UpOrderPrice').val(),
                RangePrice: 0,
                OrderStatusDesc: $('#OrderStatus').val(),
                Lot: 0,
                LotInShare: 100,
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                TrxType: $('#UpTrxType').val(),
                TrxTypeID: $("#UpTrxType").data("kendoComboBox").text(),
                CounterpartPK: 0,
                FundPK: $('#UpFundPK').val(),
                SettledDate: $('#UpSettledDate').val(),
                LastCouponDate: $('#UpLastCouponDate').val(),
                NextCouponDate: $('#UpNextCouponDate').val(),
                MaturityDate: $('#UpMaturityDate').val(),
                InterestPercent: $('#UpInterestPercent').val(),
                DoneAccruedInterest: $('#UpAccruedInterest').val(),
                AccruedInterest: $('#UpAccruedInterest').val(),
                Tenor: $('#UpTenor').val(),
                InvestmentNotes: $('#InvestmentNotes').val(),
                DoneAmount: $('#UpAmount').val(),
                PriceMode: $('#UpPriceMode').val(),
                BitIsAmortized: $('#UpBitIsAmortized').is(":checked"),
                YieldPercent: $('#UpYieldPercent').val(),
                BitIsRounding: $('#UpBitIsRounding').is(":checked"),
                EntryUsersID: sessionStorage.getItem("user"),
                MarketPK: 1,
                CrossFundFromPK: $('#UpCrossFundFromPK').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                BitForeignTrx: $('#UpBitForeignTrx').is(":checked"),
                BitHTM: $('#UpBitHTM').is(":checked"),
                BIRate: $('#UpBIRate').val(),
                InvestmentStrategy: $('#UpInvestmentStrategy').val(),
                InvestmentStyle: $('#UpInvestmentStyle').val(),
                InvestmentObjective: $('#UpInvestmentObjective').val(),
                Revision: $('#UpRevision').val(),
                OtherInvestmentObjective: $('#UpOtherInvestmentObjective').val(),
                OtherInvestmentStyle: $('#UpOtherInvestmentStyle').val(),
                OtherRevision: $('#UpOtherRevision').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };


            // cancel
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(InvestmentInstruction),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $.unblockUI();
                    refresh();
                    alertify.alert('Update Cancel');

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

    $("#BtnImportOMSBond").click(function () {
        document.getElementById("FileImportOMSBond").click();
    });

    $("#FileImportOMSBond").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportOMSBond").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        if (files.length > 0) {
            data.append("OMSBond", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "OMSBond_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportOMSBond").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOMSBond").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOMSBond").val("");
        }
    });

    function InsertCrossFundInvestmentBuy() {


        var val = validateData();
        var _urlForReference;
        var _msgSuccess;

        var _setInstrumentType = 0;
        if (val == 1) {
            $.blockUI({});
            if (_ParamFundScheme == 'TRUE') {
                _urlRef = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/CheckOMS_EndDayTrailsFundPortfolioWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globcrossFundFromPK;
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
                        alertify.alert("Save Cancel, Already Generate End Day Trails FundPortfolio");
                        return;
                    }
                    else {
                        if (_globtotalAmount == 0) {
                            $.unblockUI();
                            alertify.alert("Please Fill All Data First!");
                            return;
                        } else {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/ValidateCheckIssueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globcrossFundFromPK,
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globamount + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globcrossFundFromPK,
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == true) {
                                                    $.unblockUI();
                                                    alertify.confirm("Cash Not Available, </br> Are You Sure To Continue ?", function (e) {
                                                        if (e) {
                                                            $.blockUI();
                                                            //cek range price
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globorderPrice,
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    _statusExposureRangePrice = data.Validate;
                                                                    //cam only
                                                                    if (data.Validate == 1 && _GlobClientCode == "21") {
                                                                        $.unblockUI();
                                                                        alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                            if (e) {
                                                                                $.blockUI();

                                                                                $.ajax({
                                                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globamount + "/" + 1 + "/2",
                                                                                    type: 'GET',
                                                                                    contentType: "application/json;charset=utf-8",
                                                                                    success: function (data) {
                                                                                        if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                            $.unblockUI();
                                                                                            InitGridCrossFundBuyInformationExposure();

                                                                                        }

                                                                                        else {

                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globinstrumentPK,
                                                                                                type: 'GET',
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    _setInstrumentType = data;
                                                                                                },
                                                                                                error: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data.responseText);
                                                                                                }
                                                                                            });

                                                                                            if (_GlobClientCode == "20")
                                                                                                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                        InstrumentPK: _globinstrumentPK,
                                                                                                        InstrumentTypePK: _globinstrumentTypePK,
                                                                                                        OrderPrice: _globorderPrice,
                                                                                                        RangePrice: 0,
                                                                                                        Lot: 0,
                                                                                                        LotInShare: 100,
                                                                                                        Volume: _globvolume,
                                                                                                        Amount: _globamount,
                                                                                                        TrxType: 1,
                                                                                                        TrxTypeID: "BUY",
                                                                                                        CounterpartPK: 0,
                                                                                                        MarketPK: 1,
                                                                                                        FundPK: _globcrossFundFromPK,
                                                                                                        SettledDate: _globsettledDate,
                                                                                                        LastCouponDate: _globlastCouponDate,
                                                                                                        NextCouponDate: _globnextCouponDate,
                                                                                                        MaturityDate: _globmaturityDate,
                                                                                                        InterestPercent: _globinterestPercent,
                                                                                                        AccruedInterest: _globaccruedInterest,
                                                                                                        DoneAccruedInterest: 0,
                                                                                                        Tenor: _globtenor,
                                                                                                        InvestmentNotes: _globinvestmentNotes,
                                                                                                        IncomeTaxInterestAmount: _globincomeTaxInterestAmount,
                                                                                                        IncomeTaxGainAmount: _globincomeTaxGainAmount,
                                                                                                        DoneAmount: _globamount,
                                                                                                        TotalAmount: _globtotalAmount,
                                                                                                        InvestmentTrType: _globinvestmentTrType,
                                                                                                        PriceMode: _globpriceMode,
                                                                                                        BitIsAmortized: _globbitIsAmortized,
                                                                                                        YieldPercent: _globyieldPercent,
                                                                                                        CrossFundFromPK: _globfundPK,
                                                                                                        BitIsRounding: _globbitIsRounding,
                                                                                                        BitForeignTrx: _globbitForeignTrx,
                                                                                                        BitHTM: _globbitHTM,
                                                                                                        BIRate: _globbIRate,
                                                                                                        InvestmentStrategy: _globinvestmentStrategy,
                                                                                                        InvestmentStyle: _globinvestmentStyle,
                                                                                                        InvestmentObjective: _globinvestmentObjective,
                                                                                                        Revision: _globrevision,

                                                                                                        OtherInvestmentObjective: _globotherInvestmentObjective,
                                                                                                        OtherInvestmentStyle: _globotherInvestmentStyle,
                                                                                                        OtherRevision: _globotherRevision,

                                                                                                        EntryUsersID: sessionStorage.getItem("user")
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
                                                                                                                        url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globorderPrice + "/" + _InvPKForEmail,
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
                                                                                                                                        alertify.alert(_msgSuccess);
                                                                                                                                        win.close();
                                                                                                                                        refresh();
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
                                                                                                            alertify.alert(data.responseText);
                                                                                                        }
                                                                                                    });
                                                                                                },
                                                                                                error: function (data) {
                                                                                                    $.unblockUI();
                                                                                                    alertify.alert(data.responseText);
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
                                                                    //normal
                                                                    else {


                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globamount + "/" + 1 + "/2",
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                    $.unblockUI();
                                                                                    InitGridCrossFundBuyInformationExposure();

                                                                                }

                                                                                else {

                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globinstrumentPK,
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            _setInstrumentType = data;
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText);
                                                                                        }
                                                                                    });

                                                                                    if (_GlobClientCode == "20")
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                InstrumentPK: _globinstrumentPK,
                                                                                                InstrumentTypePK: _globinstrumentTypePK,
                                                                                                OrderPrice: _globorderPrice,
                                                                                                RangePrice: 0,
                                                                                                Lot: 0,
                                                                                                LotInShare: 100,
                                                                                                Volume: _globvolume,
                                                                                                Amount: _globamount,
                                                                                                TrxType: 1,
                                                                                                TrxTypeID: "BUY",
                                                                                                CounterpartPK: 0,
                                                                                                MarketPK: 1,
                                                                                                FundPK: _globcrossFundFromPK,
                                                                                                SettledDate: _globsettledDate,
                                                                                                LastCouponDate: _globlastCouponDate,
                                                                                                NextCouponDate: _globnextCouponDate,
                                                                                                MaturityDate: _globmaturityDate,
                                                                                                InterestPercent: _globinterestPercent,
                                                                                                AccruedInterest: _globaccruedInterest,
                                                                                                DoneAccruedInterest: 0,
                                                                                                Tenor: _globtenor,
                                                                                                InvestmentNotes: _globinvestmentNotes,
                                                                                                IncomeTaxInterestAmount: _globincomeTaxInterestAmount,
                                                                                                IncomeTaxGainAmount: _globincomeTaxGainAmount,
                                                                                                DoneAmount: _globamount,
                                                                                                TotalAmount: _globtotalAmount,
                                                                                                InvestmentTrType: _globinvestmentTrType,
                                                                                                PriceMode: _globpriceMode,
                                                                                                BitIsAmortized: _globbitIsAmortized,
                                                                                                YieldPercent: _globyieldPercent,
                                                                                                CrossFundFromPK: _globfundPK,
                                                                                                BitIsRounding: _globbitIsRounding,
                                                                                                BitForeignTrx: _globbitForeignTrx,
                                                                                                BitHTM: _globbitHTM,
                                                                                                BIRate: _globbIRate,
                                                                                                InvestmentStrategy: _globinvestmentStrategy,
                                                                                                InvestmentStyle: _globinvestmentStyle,
                                                                                                InvestmentObjective: _globinvestmentObjective,
                                                                                                Revision: _globrevision,

                                                                                                OtherInvestmentObjective: _globotherInvestmentObjective,
                                                                                                OtherInvestmentStyle: _globotherInvestmentStyle,
                                                                                                OtherRevision: _globotherRevision,

                                                                                                EntryUsersID: sessionStorage.getItem("user")
                                                                                            };
                                                                                            $.ajax({
                                                                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                                type: 'POST',
                                                                                                data: JSON.stringify(InvestmentInstruction),
                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                success: function (data) {
                                                                                                    alertify.alert(data);
                                                                                                    win.close();
                                                                                                    refresh();
                                                                                                    $.unblockUI();
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

                                                                                }
                                                                            }
                                                                        });

                                                                    }
                                                                }
                                                            })
                                                            //

                                                        }
                                                    });
                                                }
                                                else {

                                                    //cek range price
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globorderPrice,
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            _statusExposureRangePrice = data.Validate;
                                                            //cam only
                                                            if (data.Validate == 1 && _GlobClientCode == "21") {
                                                                $.unblockUI();
                                                                alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                                    if (e) {
                                                                        $.blockUI();
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globamount + "/" + 1 + "/2",
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                                    $.unblockUI();
                                                                                    InitGridCrossFundBuyInformationExposure();

                                                                                }

                                                                                else {

                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globinstrumentPK,
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            _setInstrumentType = data;
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText);
                                                                                        }
                                                                                    });

                                                                                    if (_GlobClientCode == "20")
                                                                                        _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                                InstrumentPK: _globinstrumentPK,
                                                                                                InstrumentTypePK: _globinstrumentTypePK,
                                                                                                OrderPrice: _globorderPrice,
                                                                                                RangePrice: 0,
                                                                                                Lot: 0,
                                                                                                LotInShare: 100,
                                                                                                Volume: _globvolume,
                                                                                                Amount: _globamount,
                                                                                                TrxType: 1,
                                                                                                TrxTypeID: "BUY",
                                                                                                CounterpartPK: 0,
                                                                                                MarketPK: 1,
                                                                                                FundPK: _globcrossFundFromPK,
                                                                                                SettledDate: _globsettledDate,
                                                                                                LastCouponDate: _globlastCouponDate,
                                                                                                NextCouponDate: _globnextCouponDate,
                                                                                                MaturityDate: _globmaturityDate,
                                                                                                InterestPercent: _globinterestPercent,
                                                                                                AccruedInterest: _globaccruedInterest,
                                                                                                DoneAccruedInterest: 0,
                                                                                                Tenor: _globtenor,
                                                                                                InvestmentNotes: _globinvestmentNotes,
                                                                                                IncomeTaxInterestAmount: _globincomeTaxInterestAmount,
                                                                                                IncomeTaxGainAmount: _globincomeTaxGainAmount,
                                                                                                DoneAmount: _globamount,
                                                                                                TotalAmount: _globtotalAmount,
                                                                                                InvestmentTrType: _globinvestmentTrType,
                                                                                                PriceMode: _globpriceMode,
                                                                                                BitIsAmortized: _globbitIsAmortized,
                                                                                                YieldPercent: _globyieldPercent,
                                                                                                CrossFundFromPK: _globfundPK,
                                                                                                BitIsRounding: _globbitIsRounding,
                                                                                                BitForeignTrx: _globbitForeignTrx,
                                                                                                BitHTM: _globbitHTM,
                                                                                                BIRate: _globbIRate,
                                                                                                InvestmentStrategy: _globinvestmentStrategy,
                                                                                                InvestmentStyle: _globinvestmentStyle,
                                                                                                InvestmentObjective: _globinvestmentObjective,
                                                                                                Revision: _globrevision,

                                                                                                OtherInvestmentObjective: _globotherInvestmentObjective,
                                                                                                OtherInvestmentStyle: _globotherInvestmentStyle,
                                                                                                OtherRevision: _globotherRevision,
                                                                                                EntryUsersID: sessionStorage.getItem("user")
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
                                                                                                                url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globorderPrice + "/" + _InvPKForEmail,
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

                                                                                                                                alertify.alert(_msgSuccess);
                                                                                                                                win.close();
                                                                                                                                refresh();
                                                                                                                                $.unblockUI();
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
                                                                                                    alertify.alert(data.responseText);
                                                                                                }
                                                                                            });
                                                                                        },
                                                                                        error: function (data) {
                                                                                            $.unblockUI();
                                                                                            alertify.alert(data.responseText);
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
                                                            //normal
                                                            else {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _globcrossFundFromPK + "/" + _globamount + "/" + 1 + "/2",
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data.AlertExposure == 1 || data.AlertExposure == 2 || data.AlertExposure == 5 || data.AlertExposure == 6) {
                                                                            $.unblockUI();
                                                                            InitGridCrossFundBuyInformationExposure();

                                                                        }

                                                                        else {

                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globinstrumentPK,
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    _setInstrumentType = data;
                                                                                },
                                                                                error: function (data) {
                                                                                    $.unblockUI();
                                                                                    alertify.alert(data.responseText);
                                                                                }
                                                                            });

                                                                            if (_GlobClientCode == "20")
                                                                                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                                                                                        InstrumentPK: _globinstrumentPK,
                                                                                        InstrumentTypePK: _globinstrumentTypePK,
                                                                                        OrderPrice: _globorderPrice,
                                                                                        RangePrice: 0,
                                                                                        Lot: 0,
                                                                                        LotInShare: 100,
                                                                                        Volume: _globvolume,
                                                                                        Amount: _globamount,
                                                                                        TrxType: 1,
                                                                                        TrxTypeID: "BUY",
                                                                                        CounterpartPK: 0,
                                                                                        MarketPK: 1,
                                                                                        FundPK: _globcrossFundFromPK,
                                                                                        SettledDate: _globsettledDate,
                                                                                        LastCouponDate: _globlastCouponDate,
                                                                                        NextCouponDate: _globnextCouponDate,
                                                                                        MaturityDate: _globmaturityDate,
                                                                                        InterestPercent: _globinterestPercent,
                                                                                        AccruedInterest: _globaccruedInterest,
                                                                                        DoneAccruedInterest: 0,
                                                                                        Tenor: _globtenor,
                                                                                        InvestmentNotes: _globinvestmentNotes,
                                                                                        IncomeTaxInterestAmount: _globincomeTaxInterestAmount,
                                                                                        IncomeTaxGainAmount: _globincomeTaxGainAmount,
                                                                                        DoneAmount: _globamount,
                                                                                        TotalAmount: _globtotalAmount,
                                                                                        InvestmentTrType: _globinvestmentTrType,
                                                                                        PriceMode: _globpriceMode,
                                                                                        BitIsAmortized: _globbitIsAmortized,
                                                                                        YieldPercent: _globyieldPercent,
                                                                                        CrossFundFromPK: _globfundPK,
                                                                                        BitIsRounding: _globbitIsRounding,
                                                                                        BitForeignTrx: _globbitForeignTrx,
                                                                                        BitHTM: _globbitHTM,
                                                                                        BIRate: _globbIRate,
                                                                                        InvestmentStrategy: _globinvestmentStrategy,
                                                                                        InvestmentStyle: _globinvestmentStyle,
                                                                                        InvestmentObjective: _globinvestmentObjective,
                                                                                        Revision: _globrevision,

                                                                                        OtherInvestmentObjective: _globotherInvestmentObjective,
                                                                                        OtherInvestmentStyle: _globotherInvestmentStyle,
                                                                                        OtherRevision: _globotherRevision,
                                                                                        EntryUsersID: sessionStorage.getItem("user")
                                                                                    };
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                                                        type: 'POST',
                                                                                        data: JSON.stringify(InvestmentInstruction),
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            alertify.alert(data);
                                                                                            win.close();
                                                                                            refresh();
                                                                                            $.unblockUI();
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

                                                                        }
                                                                    }
                                                                });

                                                            }
                                                        }
                                                    })
                                                    //


                                                }


                                            },
                                            error: function (data) {
                                                $.unblockUI();
                                                alertify.alert(data.responseText);
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
                    }

                }

            });
        }

    }



    function UpdateCrossFundInvestmentBuy() {

        var val = UpValidateData();
        var _setInstrumentType;
        if (val == 1) {
            alertify.confirm("Are you sure want to Update ?", function () {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globUpinstrumentPK,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        if (_GlobClientCode == "05") {
                            _setInstrumentType = _globUpinstrumentTypePK;
                        }
                        else {
                            _setInstrumentType = data;
                        }

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/GetInvestmentPKFromCrossFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globUpinvestmentPK + "/" + _globUphistoryPK,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                var InvestmentInstruction = {
                                    InvestmentPK: data.InvestmentPK, // ???
                                    HistoryPK: data.HistoryPK, // ??
                                    DealingPK: data.DealingPK, // ??
                                    PeriodPK: _defaultPeriodPK,
                                    ValueDate: $('#DateFrom').val(),
                                    Reference: data.Reference,
                                    StatusInvestment: data.StatusInvestment, // ??


                                    InstructionDate: $('#DateFrom').val(),
                                    InstrumentPK: _globUpinstrumentPK,
                                    InstrumentTypePK: _globUpinstrumentTypePK,
                                    OrderPrice: _globUporderPrice,
                                    RangePrice: 0,
                                    Lot: 0,
                                    LotInShare: 100,
                                    Volume: _globUpvolume,
                                    Amount: _globUpamount,
                                    TrxType: 1,
                                    TrxTypeID: "BUY",
                                    CounterpartPK: 0,
                                    MarketPK: 1,
                                    FundPK: data.FundPK,
                                    SettledDate: _globUpsettledDate,
                                    LastCouponDate: _globUplastCouponDate,
                                    NextCouponDate: _globUpnextCouponDate,
                                    MaturityDate: _globUpmaturityDate,
                                    InterestPercent: _globUpinterestPercent,
                                    AccruedInterest: _globUpaccruedInterest,
                                    DoneAccruedInterest: _globUpaccruedInterest,
                                    Tenor: _globUptenor,
                                    InvestmentNotes: _globUpinvestmentNotes,
                                    IncomeTaxInterestAmount: _globUpincomeTaxInterestAmount,
                                    IncomeTaxGainAmount: _globUpincomeTaxGainAmount,
                                    DoneAmount: _globUpamount,
                                    TotalAmount: _globUptotalAmount,
                                    InvestmentTrType: _globUpinvestmentTrType,
                                    PriceMode: _globUppriceMode,
                                    BitIsAmortized: _globUpbitIsAmortized,
                                    YieldPercent: _globUpyieldPercent,
                                    CrossFundFromPK: data.CrossFundFromPK,
                                    BitIsRounding: _globUpbitIsRounding,
                                    BitForeignTrx: _globUpbitForeignTrx,
                                    BitHTM: _globUpbitHTM,
                                    BIRate: _globUpbIRate,
                                    InvestmentStrategy: _globUpinvestmentStrategy,
                                    InvestmentStyle: _globUpinvestmentStyle,
                                    InvestmentObjective: _globUpinvestmentObjective,
                                    Revision: _globUprevision,

                                    OtherInvestmentObjective: _globUpotherInvestmentObjective,
                                    OtherInvestmentStyle: _globUpotherInvestmentStyle,
                                    OtherRevision: _globUpotherRevision,
                                    UpdateUsersID: sessionStorage.getItem("user"),
                                    EntryUsersID: sessionStorage.getItem("user")




                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                    type: 'POST',
                                    data: JSON.stringify(InvestmentInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        //cek range price
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSBond/ValidateCheckRangePriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globUpinstrumentPK + "/" + _globUporderPrice,
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                _statusExposureRangePrice = data.Validate;

                                                if (data.Validate == 1 && _GlobClientCode == "21") {
                                                    $.unblockUI();
                                                    alertify.confirm(data.Result + " Are You Sure Want To Continue Save Data ?", function (e) {
                                                        if (e) {
                                                            $.blockUI();

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
                                                                                url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globUpinstrumentPK + "/" + _globUpfundPK + "/" + _globUporderPrice + "/" + _InvPKForEmail,
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
                                                                                                alertify.alert(_msgSuccess);
                                                                                                WinUpdateInvestment.close();
                                                                                                refresh();
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
                                                                    alertify.alert(data.responseText);
                                                                }
                                                            });

                                                        }
                                                        else
                                                            return;
                                                    })
                                                }
                                                else {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundExposure/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globUpinstrumentPK + "/" + _globUpcrossFundFromPK + "/" + _globUpamount + "/" + 1 + "/2",
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (dataCheckExposure) {

                                                            if (dataCheckExposure.AlertExposure == 1 || dataCheckExposure.AlertExposure == 2 || dataCheckExposure.AlertExposure == 5 || dataCheckExposure.AlertExposure == 6) {
                                                                $.unblockUI();
                                                                InitGridUpCrossFundBuyInformationExposure();

                                                            }

                                                            else {

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
                                                                                    url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globUpinstrumentPK + "/" + _globUpcrossFundFromPK + "/" + _globUporderPrice + "/" + _InvPKForEmail,
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
                                                                                                    alertify.alert(_msgSuccess);
                                                                                                    WinUpdateInvestment.close();
                                                                                                    refresh();
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
                                                                        alertify.alert(data.responseText);
                                                                    }
                                                                });
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        });

                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            }
                        });
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }).moveTo(posY.left, posY.top);
        }
    }









    function InitGridCrossFundBuyInformationExposure() {

        WinValidateCrossFundBuyExposure.center();
        WinValidateCrossFundBuyExposure.open();


        $("#gridCrossFundBuyInformationExposure").empty();



        if (_GlobClientCode == "20") {

            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureForCustom20/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globcrossFundFromPK,
                dataSourceInformationExposure = getDataSourceInformationExposure(Info);

            gridInformationExposure = $("#gridCrossFundBuyInformationExposure").kendoGrid({
                dataSource: dataSourceInformationExposure,
                reorderable: true,
                sortable: true,
                resizable: true,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                dataBound: gridInformationCrossFundBuyExposureOnDataBound,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                height: "300px",
                excel: {
                    fileName: "Information_FundExposure.xlsx"
                },
                columns: [
                    { field: "FundID", title: "Fund ID", width: 200 },
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
        else {
            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                dataSourceInformationExposure = getDataSourceInformationExposure(Info);

            gridInformationExposure = $("#gridCrossFundBuyInformationExposure").kendoGrid({
                dataSource: dataSourceInformationExposure,
                reorderable: true,
                sortable: true,
                resizable: true,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                dataBound: gridInformationCrossFundBuyExposureOnDataBound,
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



    }




    function onWinValidateCrossFundBuyExposureClose() {
        var _urlForReference;


        alertify.confirm("Are You Sure To Continue Save Data ?", function () {


            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTypeByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globinstrumentPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    _setInstrumentType = data;
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            if (_GlobClientCode == "20")
                _urlForReference = window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BOND/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
                        InstrumentPK: _globinstrumentPK,
                        InstrumentTypePK: _globinstrumentTypePK,
                        OrderPrice: _globorderPrice,
                        RangePrice: 0,
                        Lot: 0,
                        LotInShare: 100,
                        Volume: _globvolume,
                        Amount: _globamount,
                        TrxType: 1,
                        TrxTypeID: "BUY",
                        CounterpartPK: 0,
                        MarketPK: 1,
                        FundPK: _globcrossFundFromPK,
                        SettledDate: _globsettledDate,
                        LastCouponDate: _globlastCouponDate,
                        NextCouponDate: _globnextCouponDate,
                        MaturityDate: _globmaturityDate,
                        InterestPercent: _globinterestPercent,
                        AccruedInterest: _globaccruedInterest,
                        DoneAccruedInterest: 0,
                        Tenor: _globtenor,
                        InvestmentNotes: _globinvestmentNotes,
                        IncomeTaxInterestAmount: _globincomeTaxInterestAmount,
                        IncomeTaxGainAmount: _globincomeTaxGainAmount,
                        DoneAmount: _globamount,
                        TotalAmount: _globtotalAmount,
                        InvestmentTrType: _globinvestmentTrType,
                        PriceMode: _globpriceMode,
                        BitIsAmortized: _globbitIsAmortized,
                        YieldPercent: _globyieldPercent,
                        CrossFundFromPK: _globfundPK,
                        BitIsRounding: _globbitIsRounding,
                        BitForeignTrx: _globbitForeignTrx,
                        BitHTM: _globbitHTM,
                        BIRate: _globbIRate,
                        InvestmentStrategy: _globinvestmentStrategy,
                        InvestmentStyle: _globinvestmentStyle,
                        InvestmentObjective: _globinvestmentObjective,
                        Revision: _globrevision,

                        OtherInvestmentObjective: _globotherInvestmentObjective,
                        OtherInvestmentStyle: _globotherInvestmentStyle,
                        OtherRevision: _globotherRevision,

                        EntryUsersID: sessionStorage.getItem("user")
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

                                            if (_ComplianceEmail == 'TRUE') {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(HighRisk),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {

                                                        if ((_statusExposureRangePrice == 1 && _GlobClientCode == "21") || (_statusExposureRangePrice == 1 && _GlobClientCode != "21")) {

                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/OMSBond/InsertExposureRangePriceFromOMSBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _globinstrumentPK + "/" + _crossFundFromPK + "/" + _globorderPrice + "/" + _investmentPK,
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {


                                                                    if (_ComplianceEmail == 'TRUE') {
                                                                        var HighRisk = {
                                                                            Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                            InvestmentPK: _investmentPK,
                                                                            HighRiskType: 2,
                                                                        };

                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/EmailExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                            type: 'POST',
                                                                            data: JSON.stringify(HighRisk),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {

                                                                                alertify.alert("Insert Investment Success !");
                                                                                win.close();
                                                                                refresh();
                                                                            },
                                                                            error: function (data) {
                                                                                alertify.alert(data.responseText);
                                                                            }
                                                                        });
                                                                    }
                                                                    else {
                                                                        alertify.alert("Insert Investment Success !");
                                                                        win.close();
                                                                        refresh();
                                                                    }

                                                                }
                                                            });
                                                        }
                                                        else {
                                                            alertify.alert("Insert Investment Success !");
                                                            win.close();
                                                            refresh();
                                                        }

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
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                },
                                error: function (data) {
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






    function gridInformationCrossFundBuyExposureOnDataBound(e) {
        var grid = $("#gridCrossFundBuyInformationExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertExposure == 1 || row.AlertExposure == 3 || row.AlertExposure == 5 || row.AlertExposure == 7) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
        });
    }



    function InitGridUpCrossFundBuyInformationExposure() {
        WinUpValidateCrossFundBuyExposure.center();
        WinUpValidateCrossFundBuyExposure.open();


        $("#gridUpCrossFundBuyInformationExposure").empty();

        if (_GlobClientCode == "20") {
            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposureForCustom20/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _globUpcrossFundFromPK,
                dataSourceUpInformationExposure = getDataSourceUpInformationExposure(Info);


            gridUpInformationExposure = $("#gridUpCrossFundBuyInformationExposure").kendoGrid({
                dataSource: dataSourceUpInformationExposure,
                reorderable: true,
                sortable: true,
                resizable: true,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                dataBound: gridUpCrossFundBuyInformationExposureOnDataBound,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                height: "300px",
                excel: {
                    fileName: "Information_FundExposure.xlsx"
                },
                columns: [
                    { field: "FundID", title: "Fund ID", width: 200 },
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
        else {
            var Info = window.location.origin + "/Radsoft/FundExposure/GetDataInformationFundExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                dataSourceUpInformationExposure = getDataSourceUpInformationExposure(Info);


            gridUpInformationExposure = $("#gridUpCrossFundBuyInformationExposure").kendoGrid({
                dataSource: dataSourceUpInformationExposure,
                reorderable: true,
                sortable: true,
                resizable: true,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                dataBound: gridUpCrossFundBuyInformationExposureOnDataBound,
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


    }

    function onWinUpValidateCrossFundBuyExposureClose() {
        alertify.confirm("Are You Sure To Continue Save Data ?", function () {


            var InvestmentInstruction = {
                InvestmentPK: _globUpinvestmentPK,
                HistoryPK: _globUphistoryPK,
                DealingPK: _globUpdealingPK,
                PeriodPK: _defaultPeriodPK,
                ValueDate: $('#DateFrom').val(),
                Reference: _globUpreference,
                StatusInvestment: _globUpstatusInvestment,
                InstructionDate: $('#DateFrom').val(),
                InstrumentPK: _globUpinstrumentPK,
                InstrumentTypePK: _setInstrumentType,
                OrderPrice: _globUporderPrice,
                RangePrice: 0,
                OrderStatusDesc: _globUporderStatus,
                Lot: 0,
                LotInShare: 100,
                Volume: _globUpvolume,
                Amount: _globUpamount,
                TrxType: 1,
                TrxTypeID: "BUY",
                CounterpartPK: 0,
                FundPK: _globUpcrossFundFromPK,
                SettledDate: _globUpsettledDate,
                LastCouponDate: _globUplastCouponDate,
                NextCouponDate: _globUpnextCouponDate,
                MaturityDate: _globUpmaturityDate,
                InterestPercent: _globUpinterestPercent,
                DoneAccruedInterest: _globUpaccruedInterest,
                AccruedInterest: _globUpaccruedInterest,
                Tenor: _globUptenor,
                InvestmentNotes: _globUpinvestmentNotes,
                DoneAmount: _globUpamount,
                PriceMode: _globUppriceMode,
                BitIsAmortized: _globUpbitIsAmortized,
                YieldPercent: _globUpyieldPercent,
                BitIsRounding: _globUpbitIsRounding,
                EntryUsersID: sessionStorage.getItem("user"),
                MarketPK: 1,
                CrossFundFromPK: _globUpfundPK,
                BitForeignTrx: _globUpbitForeignTrx,
                BitHTM: _globUpbitHTM,
                BIRate: _globUpbIRate,
                InvestmentStrategy: _globUpinvestmentStrategy,
                InvestmentStyle: _globUpinvestmentStyle,
                InvestmentObjective: _globUpinvestmentObjective,
                Revision: _globUprevision,
                OtherInvestmentObjective: _globUpotherInvestmentObjective,
                OtherInvestmentStyle: _globUpotherInvestmentStyle,
                OtherRevision: _globUpotherRevision,
                UpdateUsersID: sessionStorage.getItem("user"),
                InvestmentTrType: _globUpinvestmentTrType,
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
                                                console.log("done email fund exposure");

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/OMSEquity/InsertExposureRangePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val() + "/" + _investmentPK,
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
                                                                    console.log("done email price exposure");
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
                                        alertify.alert('Update Success');
                                        WinUpdateInvestment.close();
                                        refresh();
                                    }

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
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });


        }, function () {
            var InvestmentInstruction = {
                InvestmentPK: _globUpinvestmentPK,
                HistoryPK: _globUphistoryPK,
                DealingPK: _globUpdealingPK,
                PeriodPK: _defaultPeriodPK,
                ValueDate: $('#DateFrom').val(),
                Reference: _globUpreference,
                StatusInvestment: _globUpstatusInvestment,
                InstructionDate: $('#DateFrom').val(),
                InstrumentPK: _globUpinstrumentPK,
                InstrumentTypePK: _setInstrumentType,
                OrderPrice: _globUporderPrice,
                RangePrice: 0,
                OrderStatusDesc: _globUporderStatus,
                Lot: 0,
                LotInShare: 100,
                Volume: _globUpvolume,
                Amount: _globUpamount,
                TrxType: 1,
                TrxTypeID: "BUY",
                CounterpartPK: 0,
                FundPK: _globUpfundPK,
                SettledDate: _globUpsettledDate,
                LastCouponDate: _globUplastCouponDate,
                NextCouponDate: _globUpnextCouponDate,
                MaturityDate: _globUpmaturityDate,
                InterestPercent: _globUpinterestPercent,
                DoneAccruedInterest: _globUpaccruedInterest,
                AccruedInterest: _globUpaccruedInterest,
                Tenor: _globUptenor,
                InvestmentNotes: _globUpinvestmentNotes,
                DoneAmount: _globUpamount,
                PriceMode: _globUppriceMode,
                BitIsAmortized: _globUpbitIsAmortized,
                YieldPercent: _globUpyieldPercent,
                BitIsRounding: _globUpbitIsRounding,
                EntryUsersID: sessionStorage.getItem("user"),
                MarketPK: 1,
                CrossFundFromPK: _globUpcrossFundFromPK,
                BitForeignTrx: _globUpbitForeignTrx,
                BitHTM: _globUpbitHTM,
                BIRate: _globUpbIRate,
                InvestmentStrategy: _globUpinvestmentStrategy,
                InvestmentStyle: _globUpinvestmentStyle,
                InvestmentObjective: _globUpinvestmentObjective,
                Revision: _globUprevision,
                OtherInvestmentObjective: _globUpotherInvestmentObjective,
                OtherInvestmentStyle: _globUpotherInvestmentStyle,
                OtherRevision: _globUpotherRevision,
                UpdateUsersID: sessionStorage.getItem("user"),
                InvestmentTrType: _globUpinvestmentTrType,
            };


            // cancel
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(InvestmentInstruction),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $.unblockUI();
                    refresh();
                    alertify.alert('Update Cancel');

                },
                error: function (data) {
                    $.unblockUI();
                    alertify.alert(data.responseText);
                }
            });
        });

    }


    function gridUpCrossFundBuyInformationExposureOnDataBound(e) {
        var grid = $("#gridUpCrossFundBuyInformationExposure").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertExposure == 1 || row.AlertExposure == 3 || row.AlertExposure == 5 || row.AlertExposure == 7) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
        });
    }



});

