﻿$(document).ready(function () {
    document.title = 'FORM DEALING INSTRUCTION';
    //Global Variabel
    var win;
    var winOldData;
    var winSplit;
    var WinCounterpartExposure;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinInvestmentListing;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var htmlCountepartExposureID;
    var htmlCountepartExposureName;
    var htmlTotalPerBrokerFee;
    var htmlAllBrokerFee;
    var htmlExposure;

    var WinListCounterpart;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobInstrumentType;
    var GlobTrxType;
    var GlobStatusSettlement;
    var GlobParamBoardType;
    var checkedBondBuy = {};
    var checkedBondSell = {};
    var checkedEquityBuy = {};
    var checkedEquitySell = {};
    var checkedDepositoBuy = {};
    var checkedDepositoSell = {};
    var checkedReksadanaBuy = {};
    var checkedReksadanaSell = {};
    //1
    initButton();
    //2
    initWindow();
    //3
    if (_GlobClientCode == "20") {

        $("#BtnAllKertasKerja").show();
    }
    else {
        $("#BtnAllKertasKerja").hide();
    }

    if (_GlobClientCode == "11") {
        $("#lblSignature").hide();
        $("#lblPosition").hide();
    }
    if (_GlobClientCode == "13") {
        $("#divOMSGridHeaderBuyEq").attr("class", "OMSGridHeaderSell");
        $("#divOMSGridHeaderSellEq").attr("class", "OMSGridHeaderBuy");
        $("#divOMSGridHeaderBuyBond").attr("class", "OMSGridHeaderSell");
        $("#divOMSGridHeaderSellBond").attr("class", "OMSGridHeaderBuy");
        $("#divOMSGridHeaderBuyTD").attr("class", "OMSGridHeaderSell");
        $("#divOMSGridHeaderSellTD").attr("class", "OMSGridHeaderBuy");
        $("#divOMSGridHeaderBuyRD").attr("class", "OMSGridHeaderSell");
        $("#divOMSGridHeaderSellRD").attr("class", "OMSGridHeaderBuy");
    }
    else {
        $("#lblSignature").show();
        $("#lblPosition").show();
        $("#divOMSGridHeaderBuy").attr("class", "OMSGridHeaderBuy");
        $("#divOMSGridHeaderSell").attr("class", "OMSGridHeaderSell");
    }

    if (_GlobClientCode == "05") {
        $("#BtnGetAvgByTrx").hide();
        $("#BtnGetPerInstrument").hide();
        $("#lblSignature").hide();
        $("#lblPosition").hide();

    }
    else {
        $("#BtnPreview").hide();
    }


    ResetSelected();


    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnCancelSplit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOldData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnOldData.png"
        });

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnApproved").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });

        $("#BtnReject").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnSplit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnSplitOk").kendoButton({

        });
        $("#BtnSplitClose").kendoButton({

        });

        $("#BtnListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnGetPerInstrument").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnShow").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnUpdateInvestmentAcq").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnUpdateCounterpartOkEquityBuy").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseEquityBuy").kendoButton({

        });

        $("#BtnUpdateCounterpartOkEquitySell").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseEquitySell").kendoButton({

        });

        $("#BtnApproveBySelectedEquityBuy").kendoButton({

        });
        $("#BtnApproveBySelectedEquitySell").kendoButton({

        });

        $("#BtnRejectBySelectedEquityBuy").kendoButton({

        });
        $("#BtnRejectBySelectedEquitySell").kendoButton({

        });
        $("#BtnUnApproveBySelectedEquityBuy").kendoButton({

        });
        $("#BtnUnApproveBySelectedEquitySell").kendoButton({

        });

        $("#BtnUpdateCounterpartOkBondBuy").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseBondBuy").kendoButton({

        });

        $("#BtnUpdateCounterpartOkBondSell").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseBondSell").kendoButton({

        });

        $("#BtnApproveBySelectedBondBuy").kendoButton({

        });
        $("#BtnApproveBySelectedBondSell").kendoButton({

        });

        $("#BtnRejectBySelectedBondBuy").kendoButton({

        });
        $("#BtnRejectBySelectedBondSell").kendoButton({

        });
        $("#BtnUnApproveBySelectedBondBuy").kendoButton({

        });
        $("#BtnUnApproveBySelectedBondSell").kendoButton({

        });

        $("#BtnUpdateCounterpartOkTimeDepositBuy").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseTimeDepositBuy").kendoButton({

        });

        $("#BtnUpdateCounterpartOkTimeDepositSell").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseTimeDepositSell").kendoButton({

        });

        $("#BtnUpdateCounterpartOkReksadanaSell").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseReksadanaSell").kendoButton({

        });
        $("#BtnApproveBySelectedTimeDepositBuy").kendoButton({

        });
        $("#BtnApproveBySelectedTimeDepositSell").kendoButton({

        });

        $("#BtnRejectBySelectedTimeDepositBuy").kendoButton({

        });
        $("#BtnRejectBySelectedTimeDepositSell").kendoButton({

        });
        $("#BtnUnApproveBySelectedTimeDepositBuy").kendoButton({

        });
        $("#BtnUnApproveBySelectedTimeDepositSell").kendoButton({

        });

        $("#BtnCheckDataInvestment").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnBrokerFeeRpt").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnDealingListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnCheckCounterpartExposure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnGetAvgByTrx").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnUpdateCounterpartOkReksadanaBuy").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseReksadanaBuy").kendoButton({

        });

        $("#BtnUpdateCounterpartOkReksadanaSell").kendoButton({

        });
        $("#BtnUpdateCounterpartCloseReksadanaSell").kendoButton({

        });

        $("#BtnApproveBySelectedReksadanaBuy").kendoButton({

        });
        $("#BtnApproveBySelectedReksadanaSell").kendoButton({

        });

        $("#BtnRejectBySelectedReksadanaBuy").kendoButton({

        });
        $("#BtnRejectBySelectedReksadanaSell").kendoButton({

        });
        $("#BtnUnApproveBySelectedReksadanaBuy").kendoButton({

        });
        $("#BtnUnApproveBySelectedReksadanaSell").kendoButton({

        });

        $("#BtnPreview").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnAllKertasKerja").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOkAllKertasKerja").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelAllKertasKerja").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
    }

    

    var splitter = $("#splitter").kendoSplitter({
        orientation: "vertical",
        panes: [
            { collapsible: true, size: "850px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "430px" },
            { collapsible: true, size: "430px" }
        ],
    }).data("kendoSplitter");
   

    $("#BtnToEquity").click(function () {
        splitter.toggle("#PaneEquity");
    });

    $("#BtnToBond").click(function () {
        splitter.toggle("#PaneBond");
    });

    $("#BtnToTimeDeposit").click(function () {
        splitter.toggle("#PaneTimeDeposit");
    });

    $("#BtnToReksadana").click(function () {
        splitter.toggle("#PaneReksadana");
    });

    $("#BtnToShow").click(function () {
        splitter.expand("#PaneEquity");
        splitter.expand("#PaneBond");
        splitter.expand("#PaneTimeDeposit");
        splitter.expand("#PaneReksadana");
    });

    $("#BtnToHide").click(function () {
        splitter.collapse("#PaneEquity");
        splitter.collapse("#PaneBond");
        splitter.collapse("#PaneTimeDeposit");
        splitter.collapse("#PaneReksadana");
    });

    function RefreshNetBuySellDealingEquity() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetDealingNetBuySellEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellDealingEquity').val(data);
                $('#NetBuySellDealingEquity').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellDealingBond() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/OMSBond_GetDealingNetBuySellBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellDealingBond').val(data);
                $('#NetBuySellDealingBond').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellDealingTimeDeposit() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_GetDealingNetBuySellTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellDealingTimeDeposit').val(data);
                $('#NetBuySellDealingTimeDeposit').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellDealingTimeDeposit() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_GetDealingNetBuySellTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellDealingTimeDeposit').val(data);
                $('#NetBuySellDealingTimeDeposit').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellDealingReksadana() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_GetDealingNetBuySellReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellDealingReksadana').val(data);
                $('#NetBuySellDealingReksadana').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function initWindow() {

        $("#SplitValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });
        $("#SplitLastCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });

        $("#SplitNextCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });

        $("#SplitMaturityDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });

        $("#SplitAcqDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#SplitAcqDate1").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#SplitAcqDate2").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#SplitAcqDate3").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#SplitAcqDate4").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#SplitAcqDate5").kendoDatePicker({
            format: "MM/dd/yyyy",

        });

        $("#SplitSettledDate").kendoDatePicker({
            change: OnChangeSettledDate
        });

        $("#SplitInstructionDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });



        //$("#DateFrom").kendoDatePicker({
        //    format: "MM/dd/yyyy",
        //    change: onChangeDateFrom,
        //    value: new Date()
        //});

        //function onChangeDateFrom() {
        //    refresh();
        //}
        //$("#DateTo").kendoDatePicker({
        //    format: "MM/dd/yyyy",
        //    change: onChangeDateTo,
        //    value: new Date()
        //});

        //function onChangeDateTo() {
        //    refresh();
        //}
        $("#DateFrom").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateTo,
            value: new Date(),
        });
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeValueDate,
        });

        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            refresh();
        }
        function OnChangeDateTo() {
            var _DateTo = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateTo) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
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
                alert("Please Fill Fund");
                var dt = this.dataSource._data[0];
                this.text('');

            } else {
                refresh();
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterCounterpartID").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCounterpartPK,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                alert("Please Fill Counterpart");
                var dt = this.dataSource._data[0];
                this.text('');

            } else {
                refresh();

            }
        }




        InitgridDealingInstructionEquityBuyOnly();
        InitgridDealingInstructionEquitySellOnly();
        InitgridDealingInstructionBondBuyOnly();
        InitgridDealingInstructionBondSellOnly();
        InitgridDealingInstructionTimeDepositBuyOnly();
        InitgridDealingInstructionTimeDepositSellOnly();
        InitgridDealingInstructionReksadanaBuyOnly();
        InitgridDealingInstructionReksadanaSellOnly();
        RefreshNetBuySellDealingEquity();
        RefreshNetBuySellDealingBond();
        RefreshNetBuySellDealingReksadana();

        $("#ParamListDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date(),
            change: onChangeParamListDate,
        });

        function onChangeParamListDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamReferenceFrom").data("kendoComboBox").text("");
            $("#ParamReferenceTo").data("kendoComboBox").text("");
            GetReferenceFromDealing();

        }


        $("#LastCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeLastCouponDate,
        });

        $("#NextCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });

        $("#MaturityDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });

        $("#AcqDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            //parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate3").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate4").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate5").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate6").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate7").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate8").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });
        $("#AcqDate9").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate,
        });



        function OnChangeSettledDate() {
            //if (GlobInstrumentType == 2 || GlobInstrumentType == 3) {
            //    if ($("#SettledDate").data("kendoDatePicker").value() != null
            //          && $("#SettledDate").data("kendoDatePicker").value() != ''
            //          && $("#LastCouponDate").data("kendoDatePicker").value() != ''
            //          && $("#LastCouponDate").data("kendoDatePicker").value() != null
            //          && $('#InstrumentPK').val() != ''
            //          && $('#InstrumentPK').val() != null
            //          && $("#DoneVolume").data("kendoNumericTextBox").value() != ''
            //          && $("#DoneVolume").data("kendoNumericTextBox").value() != null
            //          ) {
            //        GetBondInterest();
            //    }
            //}
            RecalNetAmount();
        }



        function OnChangeLastCouponDate() {
            //if ($("#LastCouponDate").data("kendoDatePicker").value() != null) {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Instrument/GetNextCouponDateAndTenor/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + GlobInstrumentType + "/" + $("#InstrumentPK").val(),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {
            //            var _tenor = 0;
            //            var _amount = 0;
            //            var _interestPercent = 0;
            //            var _accruedInterest = 0;

            //            $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));
            //            $("#Tenor").data("kendoNumericTextBox").value(data.Tenor);
            //            var _amount = $("#Amount").data("kendoNumericTextBox").value();
            //            var _tenor = data.Tenor;
            //            var _interestPercent = $("#InterestPercent").data("kendoNumericTextBox").value();
            //            if (GlobInstrumentType == "2") {
            //                var _days = 360;
            //            }
            //            else {
            //                var _days = 365;
            //            }

            //            var _accruedInterest = ((_interestPercent / 100) * _amount * _tenor) / _days;
            //            $("#AccruedInterest").data("kendoNumericTextBox").value(_accruedInterest);

            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });

            //}

        }

        $("#SettledDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeSettledDate,
        });

        $("#InstructionDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });


        function OnChangeValueDate() {
            $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

                //Cek WorkingDays
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1,
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

        WinAllKertasKerja = $("#WinAllKertasKerja").kendoWindow({
            height: 300,
            title: "* Kertas Kerja By",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinAllKertasKerjaClose
        }).data("kendoWindow");


        WinDealingListing = $("#WinDealingListing").kendoWindow({
            height: 600,
            title: "* Listing Dealing",
            visible: false,
            width: 900,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinDealingListingClose
        }).data("kendoWindow");

        win = $("#WinDealingInstruction").kendoWindow({
            height: 1200,
            title: "Dealing Instruction Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");

        WinCounterpartExposure = $("#WinCounterpartExposure").kendoWindow({
            height: 450,
            title: "Counterpart Exposure",
            visible: false,
            width: 1050,
            open: function (e) {
                this.wrapper.css({ top: 70 })
            },
            close: onWinCounterpartExposureClose
        }).data("kendoWindow");

        winSplit = $("#WinSplit").kendoWindow({
            height: 350,
            title: "Split",
            visible: false,
            width: 750,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");

        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: 500,
            title: "Instrument List",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

        WinListAcq = $("#WinListAcq").kendoWindow({
            height: 500,
            title: "Investment Acquisition List",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListAcqClose
        }).data("kendoWindow");

        WinDetailPerInstrument = $("#WinDetailPerInstrument").kendoWindow({
            height: 450,
            title: "Dealing",
            visible: false,
            width: 1050,
            open: function (e) {
                this.wrapper.css({ top: 70 })
            },
            //close: onWinNewSatuClose
        }).data("kendoWindow");


        //WinListCounterpart = $("#WinListCounterpart").kendoWindow({
        //    height: 500,
        //    title: "Counterpart List",
        //    visible: false,
        //    width: 900,
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    //close: onWinListCounterpartClose
        //}).data("kendoWindow");


        WinUpdateCounterpart = $("#WinUpdateCounterpart").kendoWindow({
            height: 200,
            title: "Counterpart",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: onWinUpdateCounterpartClose
        }).data("kendoWindow");


    }

    var GlobValidator = $("#WinDealingInstruction").kendoValidator().data("kendoValidator");

    function validateData() {
        
        //if ($("#ValueDate").val() != "") {
        //    var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

        //    //Check if Date parse is successful
        //    if (!_date) {
        //        
        //        alertify.alert("Wrong Format Date MM/DD/YYYY");
        //        return 0;
        //    }
        //}
        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetailsEquityBuy(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }
    function showDetailsEquitySell(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }

    function showDetailsBondBuy(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }
    function showDetailsBondSell(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                if (dataItemX.StatusDealing == 3)
                    _showdetails(dataItemX);
                else if (dataItemX.CounterpartID == "")
                    alertify.alert("Cannot open data, please Update Broker first!")
                else
                    _showdetails(dataItemX);
            }
        }
    }

    function showDetailsTimeDepositBuy(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }
    function showDetailsTimeDepositSell(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }

    function showDetailsReksadanaBuy(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }
    function showDetailsReksadanaSell(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }

    function _showdetails(dataItemX) {
        if (dataItemX.StatusDealing == 1) {
            $("#StatusHeader").val("PENDING");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUnApproved").hide();
            $("#TrxInformation").hide();
            $("#BtnPosting").hide();
            $("#BtnRevise").hide();
            $("#BtnOldData").show();
            if (dataItemX.InstrumentTypePK == 1 || dataItemX.InstrumentTypePK == 2) {
                $("#BtnSplit").show();
            }
        }

        if (dataItemX.StatusDealing == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
            $("#StatusHeader").val("POSTED");
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").hide();
            $("#BtnUnApproved").hide();
            $("#BtnVoid").hide();
            $("#BtnOldData").hide();
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();

        }

        if (dataItemX.StatusDealing == 2 && dataItemX.Revised == 1) {
            $("#StatusHeader").val("REVISED");
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").show();
            $("#BtnUnApproved").show();
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();

        }

        if (dataItemX.StatusDealing == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
            $("#StatusHeader").val("APPROVED");
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").show();
            $("#BtnUnApproved").hide();
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnVoid").show();
            $("#BtnOldData").hide();
        }
        if (dataItemX.StatusDealing == 3) {
            $("#StatusHeader").val("VOID");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
        }
        if (dataItemX.StatusDealing == 4) {
            $("#StatusHeader").val("WAITING");
            $("#BtnVoid").hide();
            $("#BtnAdd").hide();
            $("#BtnUpdate").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
        }

        $("#InvestmentPK").val(dataItemX.InvestmentPK);
        $("#DealingPK").val(dataItemX.DealingPK);
        $("#SettlementPK").val(dataItemX.SettlementPK);
        $("#StatusInvestment").val(dataItemX.StatusInvestment);
        $("#StatusDealing").val(dataItemX.StatusDealing);
        GlobStatusDealing = dataItemX.StatusDealing;
        GlobStatusSettlement = dataItemX.StatusSettlement;
        $("#HistoryPK").val(dataItemX.HistoryPK);
        $("#Notes").val(dataItemX.Notes);
        $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
        $("#InstructionDate").data("kendoDatePicker").value(dataItemX.InstructionDate);
        $("#Reference").val(dataItemX.Reference);
        $("#BankBranchPK").val(dataItemX.BankBranchPK);
        $("#Category").val(dataItemX.Category);
        $("#CrossFundFromPK").val(dataItemX.CrossFundFromPK);
        //Cross Fund
        if (dataItemX.InstrumentTypePK == 2 || dataItemX.InstrumentTypePK == 3 || dataItemX.InstrumentTypePK == 8 || dataItemX.InstrumentTypePK == 9 || dataItemX.InstrumentTypePK == 13 || dataItemX.InstrumentTypePK == 15) {
            $("#LblCrossFundFrom").show();
        }
        else {
            $("#LblCrossFundFrom").hide();
        }

        if (dataItemX.InstrumentTypePK == 5) {
            $("#LblBankBranchPK").show();
            if (_GlobClientCode == "25") {
                if (dataItemX.TrxType == 1) {
                    $("#lblAmountToTransfer").show();
                }
                else
                    $("#lblAmountToTransfer").hide();
            }
            else {
                $("#lblAmountToTransfer").hide();
            }
        }
        else {
            $("#LblBankBranchPK").hide();
        }


        //if (dataItemX.InstrumentTypePK == 1)
        //{
        //    $("#InstrumentTypePK").val("EQUITY");
        //    if (dataItemX.TrxType == 1) {
        //        $("#TrxType").val("BUY");
        //    }
        //    else
        //    {
        //        $("#TrxType").val("SELL");
        //    }
        //}
        //else if (dataItemX.InstrumentTypePK == 2 || dataItemX.InstrumentTypePK == 3 || dataItemX.InstrumentTypePK == 15) {
        //    $("#InstrumentTypePK").val("BOND");
        //    if (dataItemX.TrxType == 1) {
        //        $("#TrxType").val("BUY");
        //    }
        //    else {
        //        $("#TrxType").val("SELL");
        //    }
        //}
        //else
        //{
        //    $("#InstrumentTypePK").val("DEPOSITO");
        //    if (dataItemX.TrxType == 1) {
        //        $("#TrxType").val("PLACEMENT");
        //    }
        //    else if (dataItemX.TrxType == 1) {
        //        $("#TrxType").val("LIQUIDATE");
        //    }
        //    else {
        //        $("#TrxType").val("ROLLOVER");
        //    }
        //}

        $("#HistoryPK").val(dataItemX.HistoryPK);
        GlobInstrumentType = dataItemX.InstrumentTypePK;
        GlobTrxType = dataItemX.TrxType;
        $("#CounterpartPK").val(dataItemX.CounterpartPK);
        $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
        $("#InstrumentPK").val(dataItemX.InstrumentPK);
        $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
        $("#RangePrice").val(dataItemX.RangePrice);
        $("#SettledDate").val(kendo.toString(kendo.parseDate(dataItemX.SettledDate), 'dd/MMM/yyyy'));
        $("#LastCouponDate").data("kendoDatePicker").value(dataItemX.LastCouponDate);
        $("#NextCouponDate").data("kendoDatePicker").value(dataItemX.NextCouponDate);
        $("#MaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
        $("#AcqDate").data("kendoDatePicker").value(dataItemX.AcqDate);
        $("#AcqDate1").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate1), 'dd/MMM/yyyy'));
        $("#AcqDate2").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate2), 'dd/MMM/yyyy'));
        $("#AcqDate3").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate3), 'dd/MMM/yyyy'));
        $("#AcqDate4").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate4), 'dd/MMM/yyyy'));
        $("#AcqDate5").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate5), 'dd/MMM/yyyy'));
        $("#AcqDate6").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate6), 'dd/MMM/yyyy'));
        $("#AcqDate7").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate7), 'dd/MMM/yyyy'));
        $("#AcqDate8").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate8), 'dd/MMM/yyyy'));
        $("#AcqDate9").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate9), 'dd/MMM/yyyy'));
        $("#InvestmentNotes").val(dataItemX.InvestmentNotes);
        $("#AmountToTransfer").val(dataItemX.AmountToTransfer);
        $("#LotInShare").val(100);
        $("#EntryDealingID").val(dataItemX.EntryDealingID);
        $("#UpdateDealingID").val(dataItemX.UpdateDealingID);
        $("#ApprovedDealingID").val(dataItemX.ApprovedDealingID);
        $("#VoidDealingID").val(dataItemX.VoidDealingID);
        $("#EntryDealingTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryDealingTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#UpdateDealingTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateDealingTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#ApprovedDealingTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedDealingTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#VoidDealingTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidDealingTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
   

        $("#ValueDate").data("kendoDatePicker").enable(false);
        $("#InstructionDate").data("kendoDatePicker").enable(false);
        //$("#SettledDate").data("kendoDatePicker").enable(false);
        $("#LastCouponDate").data("kendoDatePicker").enable(false);
        $("#NextCouponDate").data("kendoDatePicker").enable(false);
        $("#MaturityDate").data("kendoDatePicker").enable(false);
        //$("#AcqDate").data("kendoDatePicker").enable(false);
        $("#BitIsAmortized").prop('checked', dataItemX.BitIsAmortized);
        $("#BitIsRounding").prop('checked', dataItemX.BitIsRounding);
        $("#BitBreakable").prop('checked', dataItemX.BitBreakable);
        $("#BitForeignTrx").prop('checked', dataItemX.BitForeignTrx);
        //alertify.alert(dataItemX.HistoryPK);

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BondPrice",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PriceMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    enable: false,
                    change: onChangePriceMode,
                    value: setUpPriceMode(),
                });
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

        //InvestmentTrType   
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentTrType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InvestmentTrType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    //suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeInvestmentTrType,
                    value: setUpInvestmentTrType()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setUpInvestmentTrType() {
            if (dataItemX.InvestmentTrType == null) {
                return 0;
            } else {
                if (dataItemX.InvestmentTrType == 0) {
                    return 0;
                } else {
                    return dataItemX.InvestmentTrType;
                }
            }
        }

        function onChangeInvestmentTrType() {

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
                    value: setCmbCurrencyPK()
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

        function setCmbCurrencyPK() {
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


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + dataItemX.InstrumentTypePK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeTrxType,
                    value: setCmbTrxType()
                });

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


        $.ajax({
            url: window.location.origin + "/Radsoft/Market/GetMarketCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MarketPK").kendoComboBox({
                    dataValueField: "MarketPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeMarketPK,
                    value: setCmbMarketPK()
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
                $("#CrossFundFromPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    change: onChangeCrossFundFromPK,
                    value: setCrossFundFromPK(),
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

        function setCrossFundFromPK() {
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

        $("#AmountToTransfer").kendoNumericTextBox({
            format: "n0"
        });

        
        $("#Volume").kendoNumericTextBox({
            format: "n4",
            decimals:4,
            value: setVolume(),

        });

        function setVolume() {
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
        $("#Volume").data("kendoNumericTextBox").enable(false);


        $("#Lot").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setLot(),

        });

        function setLot() {
            if (dataItemX.Lot == null) {
                return "";
            } else {
                if (dataItemX.Lot == 0) {
                    return "";
                } else {
                    return dataItemX.Lot;
                }
            }
        }
        $("#Lot").data("kendoNumericTextBox").enable(false);



        $("#InterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setInterestPercent(),

        });
        function setInterestPercent() {
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
        $("#InterestPercent").data("kendoNumericTextBox").enable(false);

        $("#AccruedInterest").kendoNumericTextBox({
            format: "n0",
            value: setAccruedInterest(),

        });

        function setAccruedInterest() {
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
        $("#AccruedInterest").data("kendoNumericTextBox").enable(false);

        $("#DoneAccruedInterest").kendoNumericTextBox({
            format: "n0",
            value: setDoneAccruedInterest(),
        });

        function setDoneAccruedInterest() {
            if (dataItemX.DoneAccruedInterest == null) {
                return "";
            } else {
                if (dataItemX.DoneAccruedInterest == 0) {
                    return "";
                } else {
                    return dataItemX.DoneAccruedInterest;
                }
            }
        }
        $("#DoneAccruedInterest").data("kendoNumericTextBox").enable(false);

        $("#Tenor").kendoNumericTextBox({
            format: "n0",
            value: setTenor(),

        });
        function setTenor() {
            if (dataItemX.Tenor == null) {
                return "";
            } else {
                if (dataItemX.Tenor == 0) {
                    return "";
                } else {
                    return dataItemX.Tenor;
                }
            }
        }
        $("#Tenor").data("kendoNumericTextBox").enable(false);

        $("#OrderPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setOrderPrice(),

        });
        function setOrderPrice() {
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

        $("#OrderPrice").data("kendoNumericTextBox").enable(false);

        $("#AcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice()
        });
        function setAcqPrice() {
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

        $("#AcqPrice1").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice1()
        });
        function setAcqPrice1() {
            if (dataItemX.AcqPrice1 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice1 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice1;
                }
            }
        }
        $("#AcqPrice2").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice2()
        });
        function setAcqPrice2() {
            if (dataItemX.AcqPrice2 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice2 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice2;
                }
            }
        }
        $("#AcqPrice3").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice3()
        });
        function setAcqPrice3() {
            if (dataItemX.AcqPrice3 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice3 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice3;
                }
            }
        }
        $("#AcqPrice4").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice4()
        });
        function setAcqPrice4() {
            if (dataItemX.AcqPrice4 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice4 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice4;
                }
            }
        }
        $("#AcqPrice5").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice5()
        });
        function setAcqPrice5() {
            if (dataItemX.AcqPrice5 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice5 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice5;
                }
            }
        }

        $("#AcqPrice6").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice6()
        });
        function setAcqPrice6() {
            if (dataItemX.AcqPrice6 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice6 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice6;
                }
            }
        }

        $("#AcqPrice7").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice7()
        });
        function setAcqPrice7() {
            if (dataItemX.AcqPrice7 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice7 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice7;
                }
            }
        }

        $("#AcqPrice8").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice8()
        });
        function setAcqPrice8() {
            if (dataItemX.AcqPrice8 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice8 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice8;
                }
            }
        }

        $("#AcqPrice9").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: OnChangeAcqPrice,
            value: setAcqPrice9()
        });
        function setAcqPrice9() {
            if (dataItemX.AcqPrice9 == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice9 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice9;
                }
            }
        }
        $("#AcqVolume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume()
        });
        function setAcqVolume() {
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
        $("#AcqVolume1").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume1()
        });
        function setAcqVolume1() {
            if (dataItemX.AcqVolume1 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume1 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume1;
                }
            }
        }
        $("#AcqVolume2").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume2()
        });
        function setAcqVolume2() {
            if (dataItemX.AcqVolume2 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume2 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume2;
                }
            }
        }
        $("#AcqVolume3").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume3()
        });
        function setAcqVolume3() {
            if (dataItemX.AcqVolume3 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume3 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume3;
                }
            }
        }
        $("#AcqVolume4").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume4()
        });
        function setAcqVolume4() {
            if (dataItemX.AcqVolume4 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume4 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume4;
                }
            }
        }
        $("#AcqVolume5").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume5()
        });
        function setAcqVolume5() {
            if (dataItemX.AcqVolume5 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume5 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume5;
                }
            }
        }

        $("#AcqVolume6").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume6()
        });
        function setAcqVolume6() {
            if (dataItemX.AcqVolume6 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume6 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume6;
                }
            }
        }

        $("#AcqVolume7").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume7()
        });
        function setAcqVolume7() {
            if (dataItemX.AcqVolume7 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume7 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume7;
                }
            }
        }

        $("#AcqVolume8").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume8()
        });
        function setAcqVolume8() {
            if (dataItemX.AcqVolume8 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume8 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume8;
                }
            }
        }

        $("#AcqVolume9").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeAcqVolume,
            value: setAcqVolume9()
        });
        function setAcqVolume9() {
            if (dataItemX.AcqVolume9 == null) {
                return "";
            } else {
                if (dataItemX.AcqVolume9 == 0) {
                    return "";
                } else {
                    return dataItemX.AcqVolume9;
                }
            }
        }

        $("#YieldPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setYieldPercent(),

        });
        function setYieldPercent() {
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
        $("#YieldPercent").data("kendoNumericTextBox").enable(false);

        $("#AccruedHoldingAmount").kendoNumericTextBox({
            format: "n0",
            value: setAccruedHoldingAmount(),
        });

        function setAccruedHoldingAmount() {
            if (dataItemX.AccruedHoldingAmount == null) {
                return "";
            } else {
                if (dataItemX.AccruedHoldingAmount == 0) {
                    return "";
                } else {
                    return dataItemX.AccruedHoldingAmount;
                }
            }
        }
        $("#AccruedHoldingAmount").data("kendoNumericTextBox").enable(false);


        //-- BOND
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        if (dataItemX.AcqPrice1 != 0) {
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
        }
        if (dataItemX.AcqPrice2 != 0) {
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
        }
        if (dataItemX.AcqPrice3 != 0) {
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
        }
        if (dataItemX.AcqPrice4 != 0) {
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
        }
        if (dataItemX.AcqPrice5 != 0) {
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();
        }
        if (dataItemX.AcqPrice6 != 0) {
            $("#LblAcqPrice6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqVolume6").show();
        }
        if (dataItemX.AcqPrice7 != 0) {
            $("#LblAcqPrice7").show();
            $("#LblAcqDate7").show();
            $("#LblAcqVolume7").show();
        }
        if (dataItemX.AcqPrice8 != 0) {
            $("#LblAcqPrice8").show();
            $("#LblAcqDate8").show();
            $("#LblAcqVolume8").show();
        }
        if (dataItemX.AcqPrice9 != 0) {
            $("#LblAcqPrice9").show();
            $("#LblAcqDate9").show();
            $("#LblAcqVolume9").show();
        }


        //$("#AcqPrice").data("kendoNumericTextBox").enable(false);

        $("#Amount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAmount()

        });
        function setAmount() {
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
        $("#Amount").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxGainPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            change: OnChangeIncomeTaxGainPercent,
            value: setIncomeTaxGainPercent(),

        });
        function OnChangeIncomeTaxGainPercent() {

            if ($('#TrxType').val() == 1 || $('#TrxType').val() == 3) {
                RecalNetAmount();
            }
            else {
                RecalNetAmountSellBondByDealingPK();
            }

        }
        function setIncomeTaxGainPercent() {
            if (dataItemX.IncomeTaxGainPercent == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxGainPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxGainPercent;
                }
            }
        }
        //$("#IncomeTaxGainPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            change: OnChangeIncomeTaxInterestPercent,
            value: setIncomeTaxInterestPercent(),

        });
        function OnChangeIncomeTaxInterestPercent() {
            if ($('#TrxType').val() == 1 || $('#TrxType').val() == 3) {
                RecalNetAmount();
            }
            else {
                RecalNetAmountSellBondByDealingPK();
            }
        }
        function setIncomeTaxInterestPercent() {
            if (dataItemX.IncomeTaxInterestPercent == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxInterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxInterestPercent;
                }
            }
        }

        $("#IncomeTaxInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            change: OnChangeIncomeTaxInterestPercent,
            value: setIncomeTaxInterestPercent(),

        });
        function OnChangeIncomeTaxInterestPercent() {
            RecalNetAmount();
        }
        function setIncomeTaxInterestPercent() {
            if (dataItemX.IncomeTaxInterestPercent == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxInterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxInterestPercent;
                }
            }
        }

        //$("#IncomeTaxInterestPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxSellPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setIncomeTaxSellPercent(),

        });
        function setIncomeTaxSellPercent() {
            if (dataItemX.IncomeTaxSellPercent == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxSellPercent == 0) {
                    return 0;
                } else {
                    return dataItemX.IncomeTaxSellPercent;
                }
            }
        }

        $("#IncomeTaxSellPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxGainAmount").kendoNumericTextBox({
            format: "n0",
            value: setIncomeTaxGainAmount(),
            change: UpdateRecalTotalAmount,

        });
        function setIncomeTaxGainAmount() {
            if (dataItemX.IncomeTaxGainAmount == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxGainAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.IncomeTaxGainAmount;
                }
            }
        }



        //$("#IncomeTaxGainAmount").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxInterestAmount").kendoNumericTextBox({
            format: "n0",
            value: setIncomeTaxInterestAmount(),
            change: UpdateRecalTotalAmount,

        });
        function setIncomeTaxInterestAmount() {
            if (dataItemX.IncomeTaxInterestAmount == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxInterestAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.IncomeTaxInterestAmount;
                }
            }
        }


        //$("#IncomeTaxInterestAmount").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxSellAmount").kendoNumericTextBox({
            format: "n0",
            value: setIncomeTaxSellAmount()

        });
        function setIncomeTaxSellAmount() {
            if (dataItemX.IncomeTaxSellAmount == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxSellAmount == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxSellAmount;
                }
            }
        }

        $("#IncomeTaxSellAmount").data("kendoNumericTextBox").enable(false);

        $("#TotalAmount").kendoNumericTextBox({
            format: "n0",
            value: setTotalAmount()

        });
        function setTotalAmount() {
            if (dataItemX.TotalAmount == null) {
                return "";
            } else {
                if (dataItemX.TotalAmount == 0) {
                    return "";
                } else {
                    return dataItemX.TotalAmount;
                }
            }
        }
        $("#TotalAmount").data("kendoNumericTextBox").enable(false);

        //combo box PeriodPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    enable: false,
                    dataSource: data,
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
            if (dataItemX.PeriodPK == null) {
                return "";
            } else {
                if (dataItemX.PeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodPK;
                }
            }
        }


        //Instrument Type

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#InstrumentTypePK").kendoComboBox({
        //            dataValueField: "value",
        //            dataTextField: "text",
        //            filter: "contains",
        //            enable: false,
        //            suggest: true,
        //            dataSource: [
        //                    { text: "EQUITY", value: "1" },
        //                    { text: "BOND", value: "2" },
        //                    { text: "DEPOSITO", value: "3" },
        //            ],
        //            change: onChangeInstrumentType,
        //            value: setCmbInstrumentTypePK()
        //        });

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //function setCmbInstrumentTypePK() {
        //    if (dataItemX.InstrumentTypePK == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.InstrumentTypePK == 0) {
        //            return "";
        //        }
        //        else if (dataItemX.InstrumentTypePK == 13) {
        //            return 2;
        //        }
        //        else if (dataItemX.InstrumentTypePK == 5) {
        //            return 3;
        //        }
        //        else {
        //            return dataItemX.InstrumentTypePK;
        //        }
        //    }
        //}
        //function onChangeInstrumentType() {

        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }


        //    GlobInstrumentType = this.value();
        //    //BOND
        //    ShowHideLabelByInstrumentType(GlobInstrumentType,GlobTrxType);


        //    $.ajax({
        //        url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionType" + "/" + "/" + GlobInstrumentType,
        //        type: 'GET',
        //        contentType: "application/json;charset=utf-8",
        //        success: function (data) {
        //            $("#TrxType").kendoComboBox({
        //                dataValueField: "Code",
        //                dataTextField: "DescOne",
        //                filter: "contains",
        //                suggest: true,
        //                enable: false,
        //                dataSource: data,
        //                change: onChangeTrxType,
        //                value: setCmbTrxType()
        //            });

        //        },
        //        error: function (data) {
        //            alertify.alert("Please Choose Instrument Type First");

        //        }
        //    });

        //    function onChangeTrxType() {

        //        if (this.value() && this.selectedIndex == -1) {
        //            var dt = this.dataSource._data[0];
        //            this.text('');
        //        }
        //    }

        //    function setCmbTrxType() {
        //        if (dataItemX.TrxType == null) {
        //            return "";
        //        } else {
        //            if (dataItemX.TrxType == 0) {
        //                return "";
        //            } else {
        //                return dataItemX.TrxType;
        //            }
        //        }
        //    }

        //    //$("#InstrumentPK").val("");
        //    //$("#InstrumentID").val("");
        //    //$("#InstrumentName").val("");
        //    //$("#AcqPrice").data("kendoNumericTextBox").value(0);
        //    //$("#OrderPrice").data("kendoNumericTextBox").value(0);
        //    //$("#Volume").data("kendoNumericTextBox").value(0);
        //    //$("#Amount").data("kendoNumericTextBox").value(0);
        //    //$("#Lot").data("kendoNumericTextBox").value(0);
        //    //$("#InterestPercent").data("kendoNumericTextBox").value(0);
        //    //$("#MaturityDate").data("kendoDatePicker").value(null);
        //    //$("#LastCouponDate").data("kendoDatePicker").value(null);
        //    //$("#NextCouponDate").data("kendoDatePicker").value(null);
        //    //$("#Tenor").data("kendoNumericTextBox").value(0);
        //    //$("#AccruedInterest").data("kendoNumericTextBox").value(0);


        //}

        //if (e != null) {
        ShowHideLabelByInstrumentType(dataItemX.InstrumentTypePK, dataItemX.TrxType);
        //}

        //Combo Box Fund 
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
                    enable: false,
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

        // BankBranchPK
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
                    suggest: true,
                    change: onChangeBankBranchPK,
                    enabled: false,
                    value: setCmbBankBranchPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankBranchPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBankBranchPK() {
            if (dataItemX.BankBranchPK == null) {
                return "";
            } else {
                if (dataItemX.BankBranchPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankBranchPK;
                }
            }
        }


        //Combo Box Counterpart 
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    change: onChangeCounterpartPK,
                    value: setCmbCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCounterpartPK() {
            if (dataItemX.CounterpartPK == null) {
                return "";
            } else {
                if (dataItemX.CounterpartPK == 0) {
                    return "";
                } else {
                    return dataItemX.CounterpartPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BoardType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BoardType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeBoardType,
                    value: setCmbBoardType()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeBoardType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else if ($("#BoardType").data("kendoComboBox").text() == "Reguler") {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            
                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

                //Cek WorkingDays
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 3,
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
            else {
                $("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
            }            

        }

        function setCmbBoardType() {
            if (dataItemX.BoardType == null) {
                return "";
            } else {
                if (dataItemX.BoardType == 0) {
                    return "";
                } else {
                    return dataItemX.BoardType;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SettlementMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SettlementMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    dataSource: data,
                    change: onChangeSettlementMode,
                    value: setCmbSettlementMode()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeSettlementMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbSettlementMode() {
            if (dataItemX.SettlementMode == null) {
                return "";
            } else {
                if (dataItemX.SettlementMode == 0) {
                    return "";
                } else {
                    return dataItemX.SettlementMode;
                }
            }
        }

        //---//
        var _format;
        var _dec;
        if (GlobInstrumentType == 6) {
            _format = "n6";
            _dec = 6;
        }
        else
        {
            _format = "n4";
            _dec = 4;
        }
            

        $("#DoneVolume").kendoNumericTextBox({
            format: _format,
            decimals: _dec,
            value: setDoneVolume(),
            change: OnChangeDoneVolume
        });
        function setDoneVolume() {
            if (dataItemX.DoneVolume == null) {
                return "";
            } else {
                if (dataItemX.DoneVolume == 0) {
                    return "";
                } else {
                    return dataItemX.DoneVolume;
                }
            }
        }

        function OnChangeDoneVolume() {

            if ($("#InstrumentTypePK").data("kendoComboBox").value() == 5) {
                $("#TotalAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value());
                $("#DoneAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value());
            }
            //else if (($("#InstrumentTypePK").data("kendoComboBox").value() == 1) || ($("#InstrumentTypePK").data("kendoComboBox").value() == 4) || ($("#InstrumentTypePK").data("kendoComboBox").value() == 16)) {
            //    $("#DoneLot").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() / 100);
            //    $("#DoneAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() * $("#DonePrice").data("kendoNumericTextBox").value());
            //}
            else {
                $("#DoneLot").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() / 100);
                RecalTotalAmount();
                RecalNetAmount();
            }
        }

        function RecalTotalAmount() {
            if ($("#InstrumentTypePK").data("kendoComboBox").value() == 1 || $("#InstrumentTypePK").data("kendoComboBox").value() == 6) {
                if ($("#DoneVolume").data("kendoNumericTextBox").value() > 0 && $("#DonePrice").data("kendoNumericTextBox").value() > 0) {
                    $("#DoneAmount").data("kendoNumericTextBox").value(($("#DoneVolume").data("kendoNumericTextBox").value() * $("#DonePrice").data("kendoNumericTextBox").value()));
                }
            }
            else {
                if ($("#DoneVolume").data("kendoNumericTextBox").value() > 0 && $("#DonePrice").data("kendoNumericTextBox").value() > 0) {
                    $("#DoneAmount").data("kendoNumericTextBox").value(($("#DoneVolume").data("kendoNumericTextBox").value() * ($("#DonePrice").data("kendoNumericTextBox").value() / 100)));
                }
            }

        }


        $("#DoneLot").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setDoneLot(),
            change: OnChangeDoneLot

        });
        function setDoneLot() {
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

        function OnChangeDoneLot() {
            $("#DoneVolume").data("kendoNumericTextBox").value($("#DoneLot").data("kendoNumericTextBox").value() * 100);
            RecalTotalAmount();
            RecalNetAmount();
        }

        $("#DonePrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setDonePrice(),
            change: OnChangeDonePrice
        });
        function setDonePrice() {
            if (dataItemX.DonePrice == null) {
                return "";
            } else {
                if (dataItemX.DonePrice == 0 && GlobInstrumentType == 5) {
                    return 1;
                } else if (dataItemX.DonePrice == 0) {
                    "";
                } else {
                    return dataItemX.DonePrice;
                }
            }

        }


        function OnChangeDonePrice() {
            RecalTotalAmount();
            RecalNetAmount();

            if ($("#InstrumentTypePK").data("kendoComboBox").value() == 6) {
                if ($("#TrxType").data("kendoComboBox").value() == 1 || $("#TrxType").data("kendoComboBox").value() == 2) {
                    if ($("#DoneVolume").data("kendoNumericTextBox").value() == null || $("#DoneVolume").data("kendoNumericTextBox").value() == 0)
                    {
                        $("#DoneVolume").data("kendoNumericTextBox").value(dataItemX.DoneAmount / $("#DonePrice").data("kendoNumericTextBox").value());
                    }
                    else if ($("#DoneAmount").data("kendoNumericTextBox").value() == null || $("#DoneAmount").data("kendoNumericTextBox").value() == 0)
                    {
                        $("#DoneAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() * $("#DonePrice").data("kendoNumericTextBox").value());
                    }


                }
                else {
                    $("#DoneAmount").data("kendoNumericTextBox").value($("#DonePrice").data("kendoNumericTextBox").value() * dataItemX.DoneVolume);
                }
            }

        }

        $("#DoneAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setDoneAmount()

        });
        function setDoneAmount() {
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
        $("#DoneAmount").data("kendoNumericTextBox").enable(false);


        $("#BreakInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setBreakInterestPercent()
        });
        function setBreakInterestPercent() {
            if (dataItemX.BreakInterestPercent == null) {
                return "";
            } else {
                if (dataItemX.BreakInterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.BreakInterestPercent;
                }
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
                    change: onChangeInterestPaymentType,
                    value: setInterestPaymentType()
                });

                if (dataItemX.TrxType == 1) {
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
        }


        function setInterestPaymentType() {
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
                $("#InterestDaysType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    enable: true,
                    dataSource: data,
                    change: onChangeInterestDaysType,
                    value: setInterestDaysType()
                });

                if (dataItemX.TrxType == 1) {
                    $("#InterestDaysType").data("kendoComboBox").enable(true);
                } else {
                    $("#InterestDaysType").data("kendoComboBox").enable(false);
                }
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


        function setInterestDaysType() {
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
                $("#PaymentModeOnMaturity").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePaymentModeOnMaturity,
                    value: setPaymentModeOnMaturity()
                });
                if (dataItemX.TrxType == 1) {
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


        function setPaymentModeOnMaturity() {
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

        $("#Category").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Normal", value: "Deposit Normal" },
                { text: "On Call", value: "Deposit On Call" }
            ],
            filter: "contains",
            enable: false,
            suggest: true,
            value: setCategory(),
        });

        function setCategory() {
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


            win.center();
            win.open();


    }
                 

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }


    function ClearDataBond() {
        $("#DoneLot").val("");
        $("#Lot").val("");
        //$("#LotInShare").val("");

    }

    function ClearDataEquity() {
        $("#AccruedInterest").val("");
        $("#InterestPercent").val("");
        $("#MaturityDate").data("kendoDatePicker").value("");

    }

    function ClearDataMoney() {
        $("#DoneLot").val("");
        $("#Lot").val("");
        //$("#LotInShare").val("");
        //$("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());


    }

    function ClearRequiredAttribute() {
        $("#DonePrice").removeAttr("required");
        $("#DoneVolume").removeAttr("required");
        $("#OrderPrice").removeAttr("required");
        $("#AcqPrice").removeAttr("required");
        $("#DoneLot").removeAttr("required");
        $("#Lot").removeAttr("required");
        //$("#LotInShare").removeAttr("required");
        $("#SettledDate").removeAttr("required");
        $("#MaturityDate").removeAttr("required");
        $("#LastCouponDate").removeAttr("required");
        $("#NextCouponDate").removeAttr("required");
        $("#InterestPercent").removeAttr("required");
        $("#AccruedInterest").removeAttr("required");
        $("#BoardType").removeAttr("required");
        $("#SettlementMode").removeAttr("required");
        $("#CounterpartPK").removeAttr("required");

    }

    function ShowHideLabelByInstrumentType(_type,_trxType) {
        ClearRequiredAttribute();
        GlobValidator.hideMessages();
        //BOND
        if (_type == 2 || _type == 3 || _type == 9 || _type == 13 || _type == 15) {
            HideLabel();

            $("#LblCounterpart").show();
            $("#LblSettlementMode").show();
            $("#LblSettlementDate").show();

            $("#LblAcquisition").show();
            $("#tblAcq").show();
            $("#tblOther").show();
            $("#tblOther1").show();

            $("#LblPrice").show();
            $("#LblDonePrice").show();

            $("#LblBitIsAmortized").show();
            $("#LblBitIsRounding").show();
            $("#LblPriceMode").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblMaturityDate").show();

            $("#LblAccruedInterest").show();
            $("#LblDoneAccruedInterest").show();

            $("#LblInterestPercent").show();
            $("#LblYieldPercent").show();
            $("#LblIncomeTaxGainPercent").show();
            $("#LblIncomeTaxInterestPercent").show();
            $("#LblIncomeTaxSellPercent").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblIncomeTaxSellAmount").show();
            $("#LblAccruedHoldingAmount").show();
            $("#LblTotalAmount").show();
            
            $("#LblNominal").show();
            $("#LblDoneNominal").show();

            $("#LblBtnMatch").hide();
            $("#LblBtnApproved").show();
  

            $("#DonePrice").attr("required", true);
            $("#DoneVolume").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#SettlementMode").attr("required", true);

            if(_trxType ==2)
            {
                $("#BtnAddAcq1").hide();
                //$("#AcqPrice").data("kendoNumericTextBox").enable(false);
                //$("#AcqVolume").data("kendoNumericTextBox").enable(false);
                //$("#AcqDate").data("kendoDatePicker").enable(false);
            }
            else
            {
                $("#BtnAddAcq1").show();
                $("#AcqPrice").data("kendoNumericTextBox").enable(true);
                $("#AcqVolume").data("kendoNumericTextBox").enable(true);
                $("#AcqDate").data("kendoDatePicker").enable(true);
            }


        }
            //EQUITY
        else if (_type == 1 || _type == 4 || _type == 16) {
            HideLabel();
            HideLabelAcq();

            $("#LblCounterpart").show();
            $("#LblBoardType").show();
            $("#LblSettlementMode").show();
            $("#LblSettlementDate").show();

            $("#LblPrice").show();
            $("#LblDonePrice").show();
            $("#LblLot").show();
            $("#LblVolume").show();
            $("#LblDoneLot").show();
            $("#LblDoneVolume").show();

            $("#DonePrice").attr("required", true);
            $("#DoneVolume").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#BoardType").attr("required", true);
            $("#SettlementMode").attr("required", true);
        }
            //DEPOSITO
        if (_type == 5) {
            HideLabel();
            HideLabelAcq();

            $("#LblSettlementDate").show();

            $("#tblOther").show();

            $("#LblMaturityDate").show();


            $("#LblInterestPercent").show();
            $("#LblTotalAmount").show();

            $("#LblNominal").show();
            $("#LblDoneNominal").show();

            $("#LblBtnMatch").hide();
            $("#LblBtnApproved").show();

            $("#DoneVolume").attr("required", true);
            $("#SettledDate").attr("required", true);

            $("#LblBreakInterestPercent").show();

            $("#LblInterestDaysType").show();
            $("#LblInterestPaymentType").show();
            $("#LblPaymentModeOnMaturity").show();
        }

            //REKSADANA
        else if (_type == 6) {
            HideLabel();
            HideLabelAcq();

            $("#LblCounterpart").show();
            $("#LblBoardType").show();
            $("#LblSettlementMode").show();
            $("#LblSettlementDate").show();

            $("#LblPrice").show();
            $("#LblDonePrice").show();
            $("#LblVolume").show();
            $("#LblDoneVolume").show();

            $("#DonePrice").attr("required", true);
            $("#DoneVolume").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#BoardType").attr("required", true);
            $("#SettlementMode").attr("required", true);
        }


        if (_GlobClientCode == "26") {
            $("#LblInvestmentTrType").show();
        }
        else {
            $("#LblInvestmentTrType").hide();
        }

    }

    function clearData() {
        $("#DealingPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#BitBreakable").val("");
        $("#MarketPK").val("");
        $("#PeriodPK").val("");
        $("#PeriodID").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#InstructionDate").data("kendoDatePicker").value(null);
        $("#Reference").val("");
        $("#InstrumentTypePK").val("");
        $("#BankBranchPK").val("");
        $("#TrxType").val("");
        $("#TrxTypeID").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#OrderPrice").val("");
        $("#RangePrice").val("");
        $("#Lot").val("");
        //$("#LotInShare").val("");
        $("#Volume").val("");
        $("#Amount").val("");
        $("#InterestPercent").val("");
        $("#BreakInterestPercent").val("");
        $("#AccruedInterest").val("");
        $("#SettledDate").data("kendoDatePicker").value(null);
        $("#LastCouponDate").data("kendoDatePicker").value(null);
        $("#NextCouponDate").data("kendoDatePicker").value(null);
        $("#Tenor").val("0");
        $("#MaturityDate").data("kendoDatePicker").value(null);
        $("#InvestmentNotes").val("");
        $("#AmountToTransfer").val("");
        $("#DoneLot").val("");
        $("#DoneVolume").val("");
        $("#DonePrice").val("");
        $("#DoneAmount").val("");
        $("#AcqPrice").val("");
        $("#AcqDate").data("kendoDatePicker").value(null);
        $("#AcqVolume").val("");
        $("#AcqPrice1").val("");
        $("#AcqDate1").data("kendoDatePicker").value(null);
        $("#AcqVolume1").val("");
        $("#AcqPrice2").val("");
        $("#AcqDate2").data("kendoDatePicker").value(null);
        $("#AcqVolume2").val("");
        $("#AcqPrice3").val("");
        $("#AcqDate3").data("kendoDatePicker").value(null);
        $("#AcqVolume3").val("");
        $("#AcqPrice4").val("");
        $("#AcqDate4").data("kendoDatePicker").value(null);
        $("#AcqVolume4").val("");
        $("#AcqPrice5").val("");
        $("#AcqDate5").data("kendoDatePicker").value(null);
        $("#AcqVolume5").val("");
        $("#AcqPrice6").val("");
        $("#AcqDate6").data("kendoDatePicker").value(null);
        $("#AcqVolume6").val("");
        $("#AcqPrice7").val("");
        $("#AcqDate7").data("kendoDatePicker").value(null);
        $("#AcqVolume7").val("");
        $("#AcqPrice8").val("");
        $("#AcqDate8").data("kendoDatePicker").value(null);
        $("#AcqVolume8").val("");
        $("#AcqPrice9").val("");
        $("#AcqDate9").data("kendoDatePicker").value(null);
        $("#AcqVolume9").val("");
        $("#BoardType").val("");
        $("#SettlementMode").val("");
        $("#EntryDealingID").val("");
        $("#UpdateDealingID").val("");
        $("#ApprovedDealingID").val("");
        $("#VoidDealingID").val("");
        $("#EntryDealingTime").val("");
        $("#UpdateDealingTime").val("");
        $("#ApprovedDealingTime").val("");
        $("#VoidDealingTime").val("");
        $("#LastUpdate").val("");
    }

    function showButton() {
        $("#Update").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        $("#BtnSplit").show();
    }

    function getDataSource(_url) {
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
                             DealingPK: { type: "number" },
                             SettlementPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             SelectedDealing: { type: "boolean" },
                             StatusInvestment: { type: "number" },
                             StatusDealing: { type: "number" },
                             StatusSettlement: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             InstructionDate: { type: "date" },
                             Reference: { type: "string" },
                             InstrumentTypePK: { type: "number" },
                             InterestDaysTypeDesc: { type: "string" },
                             InstrumentTypeID: { type: "string" },
                             BankBranchPK: { type: "number" },
                             BankBranchID: { type: "string" },
                             TrxType: { type: "number" },
                             TrxTypeID: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             OrderPrice: { type: "number" },
                             RangePrice: { type: "string" },
                             AcqPrice: { type: "number" },
                             Lot: { type: "number" },
                             LotInShare: { type: "number" },
                             Volume: { type: "number" },
                             Amount: { type: "number" },
                             SettledDate: { type: "date" },
                             InterestPercent: { type: "number" },
                             BreakInterestPercent: { type: "number" },
                             AccruedInterest: { type: "number" },
                             DoneAccruedInterest: { type: "number" },
                             LastCouponDate: { type: "date" },
                             NextCouponDate: { type: "date" },
                             Tenor: { type: "number" },
                             MaturityDate: { type: "date" },
                             AcqPrice: { type: "number" },
                             AcqDate: { type: "date" },
                             AcqVolume: { type: "number" },
                             InvestmentNotes: { type: "string" },
                             DonePrice: { type: "number" },
                             DoneLot: { type: "number" },
                             DoneVolume: { type: "number" },
                             DoneAmount: { type: "number" },
                             AcqPrice1: { type: "number" },
                             AcqDate1: { type: "date" },
                             AcqVolume1: { type: "number" },
                             AcqPrice2: { type: "number" },
                             AcqDate2: { type: "date" },
                             AcqVolume2: { type: "number" },
                             AcqPrice3: { type: "number" },
                             AcqDate3: { type: "date" },
                             AcqVolume3: { type: "number" },
                             AcqPrice4: { type: "number" },
                             AcqDate4: { type: "date" },
                             AcqVolume4: { type: "number" },
                             AcqPrice5: { type: "number" },
                             AcqDate5: { type: "date" },
                             AcqVolume5: { type: "number" },
                             AcqPrice6: { type: "number" },
                             AcqDate6: { type: "date" },
                             AcqVolume6: { type: "number" },
                             AcqPrice7: { type: "number" },
                             AcqDate7: { type: "date" },
                             AcqVolume7: { type: "number" },
                             AcqPrice8: { type: "number" },
                             AcqDate8: { type: "date" },
                             AcqVolume8: { type: "number" },
                             AcqPrice9: { type: "number" },
                             AcqDate9: { type: "date" },
                             AcqVolume9: { type: "number" },
                             BoardType: { type: "number" },
                             BoardTypeDesc: { type: "string" },
                             BankPK: { type: "number" },
                             BankBranchPK: { type: "number" },
                             Category: { type: "string" },
                             CrossFundFromPK: { type: "number" },
                             IncomeTaxGainAmount: { type: "number" },
                             IncomeTaxInterestAmount: { type: "number" },
                             EntryDealingID: { type: "string" },
                             EntryDealingTime: { type: "date" },
                             UpdateDealingID: { type: "string" },
                             UpdateDealingTime: { type: "date" },
                             ApprovedDealingID: { type: "string" },
                             ApprovedDealingTime: { type: "date" },
                             VoidDealingID: { type: "string" },
                             VoidDealingTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" },
                             BankBranchPK: { type: "number" },
                             BankBranchID: { type: "string" },
                             PTPCode: { type: "string" },
                             BitForeignTrx: { type: "boolean" },
                         }


                     }

                 },
                 group: {
                     field: "OrderStatusDesc", aggregates: [
                     { field: "Lot", aggregate: "sum" },
                     { field: "Volume", aggregate: "sum" },
                     { field: "DoneLot", aggregate: "sum" },
                     { field: "DoneVolume", aggregate: "sum" },
                     { field: "DoneAmount", aggregate: "sum" },
                     { field: "DoneAccruedInterest", aggregate: "sum" },
                     { field: "IncomeTaxGainAmount", aggregate: "sum" },
                     { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                     ]
                 },
                 aggregate: [
                     { field: "Lot", aggregate: "sum" },
                     { field: "Volume", aggregate: "sum" },
                     { field: "DoneLot", aggregate: "sum" },
                     { field: "DoneVolume", aggregate: "sum" },
                     { field: "DoneAmount", aggregate: "sum" },
                     { field: "DoneAccruedInterest", aggregate: "sum" },
                     { field: "IncomeTaxGainAmount", aggregate: "sum" },
                     { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                 ],

             });
    }

    function refresh() {
        RefreshNetBuySellDealingEquity();
        RefreshNetBuySellDealingBond();
        RefreshNetBuySellDealingReksadana();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionEquitySellOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionBondBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionBondSellOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid").setDataSource(newDS);


        for (var i in checkedEquityBuy) {
            checkedEquityBuy[i] = null
        }
        for (var i in checkedEquitySell) {
            checkedEquitySell[i] = null
        }

        if ($('#chbEquityBuy').is(":checked")) {
            $("#chbEquityBuy").click();
        }
        if ($('#chbEquitySell').is(":checked")) {
            $("#chbEquitySell").click();
        }

        for (var i in checkedBondBuy) {
            checkedBondBuy[i] = null
        }
        for (var i in checkedBondSell) {
            checkedBondSell[i] = null
        }

        if ($('#chbBondBuy').is(":checked")) {
            $("#chbBondBuy").click();
        }
        if ($('#chbBondSell').is(":checked")) {
            $("#chbBondSell").click();
        }

        for (var i in checkedDepositoBuy) {
            checkedDepositoBuy[i] = null
        }
        for (var i in checkedDepositoSell) {
            checkedDepositoSell[i] = null
        }

        if ($('#chbDepositoBuy').is(":checked")) {
            $("#chbDepositoBuy").click();
        }
        if ($('#chbDepositoSell').is(":checked")) {
            $("#chbDepositoSell").click();
        }

        for (var i in checkedReksadanaBuy) {
            checkedReksadanaBuy[i] = null
        }
        for (var i in checkedReksadanaSell) {
            checkedReksadanaSell[i] = null
        }

        if ($('#chbReksadanaBuy').is(":checked")) {
            $("#chbReksadanaBuy").click();
        }
        if ($('#chbReksadanaSell').is(":checked")) {
            $("#chbReksadanaSell").click();
        }
       
    }

    function refreshEquityBuy() {
        RefreshNetBuySellDealingEquity();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshEquitySell() {
        RefreshNetBuySellDealingEquity();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionEquitySellOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshBondBuy() {
        RefreshNetBuySellDealingBond();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionBondBuyOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshBondSell() {
        RefreshNetBuySellDealingBond();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionBondSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshTimeDepositBuy() {
        RefreshNetBuySellDealingTimeDeposit();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshTimeDepositSell() {
        RefreshNetBuySellDealingTimeDeposit();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshReksadanaBuy() {
        RefreshNetBuySellDealingReksadana();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid").setDataSource(newDS);

    }

    function refreshReksadanaSell() {
        RefreshNetBuySellDealingReksadana();
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    function InitgridDealingInstructionEquityBuyOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(DealingInstructionApprovedURL);
        if (_GlobClientCode == "05") {
            var gridEquityBuyOnly = $("#gridDealingInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "DealingEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, width: 70, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionEquityBuyOnly }, title: " " },
                    //{ command: { text: "Match", click: ApprovedDealingInstructionEquityBuyOnly }, title: " " },
                    { command: { text: "Split", click: SplitDealingInstructionEquityBuyOnly }, width: 70, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "OrderPrice", title: "Open Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "Lot", title: "Open Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 { field: "CounterpartID", title: "Broker", },
                 { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                 },
                { field: "BoardTypeDesc", title: "Type" },
                { field: "InvestmentNotes", hidden: true, title: "Remark" },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        } else {
            var gridEquityBuyOnly = $("#gridDealingInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "DealingEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, width: 70, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionEquityBuyOnly }, title: " " },
                    //{ command: { text: "Match", click: ApprovedDealingInstructionEquityBuyOnly }, title: " " },
                    { command: { text: "Split", click: SplitDealingInstructionEquityBuyOnly }, width: 70, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", width: 100, headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock", width: 90, },
                    //{ field: "AvgPrice", title: "Avg Price", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "OrderPrice", title: "Open Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Lot", title: "Open Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", width: 50, },
                    { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "BoardTypeDesc", title: "Type", width: 80, },
                    { field: "InvestmentNotes", hidden: true, title: "Remark" },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "BitForeignTrx", title: "BitForeignTrx", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }



        gridEquityBuyOnly.table.on("click", ".checkboxEquityBuy", selectRowEquityBuy);
        var oldPageSizeApproved = 0;


        $('#chbEquityBuy').change(function (ev) {

            var grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxEquityBuy').each(function (idx, item) {
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
            for (var i in checkedEquityBuy) {
                if (checkedEquityBuy[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowEquityBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedEquityBuy[dataItemZ.DealingPK] = checked;
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

            //alertify.alert(_msg);
            SelectDeselectAllDataEquityBuyOnly(_checked, "Approved");

        });

        //gridEquityBuyOnly.table.on("click", ".cSelectedDetailApprovedEquityBuy", selectDataApprovedEquityBuy);

        function selectDataApprovedEquityBuy(e) {


            var grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataEquityBuyOnly(_checked, _dealingPK);

        }

    }



    function SelectDeselectDataEquityBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Dealing",
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
    function SelectDeselectAllDataEquityBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshEquityBuy();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }


    function InitgridDealingInstructionEquitySellOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(DealingInstructionApprovedURL);
        if (_GlobClientCode == "05") {
            var gridEquitySellOnly = $("#gridDealingInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "DealingEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, width: 70, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionEquitySellOnly }, title: " " },
                    //{ command: { text: "Match", click: ApprovedDealingInstructionEquitySellOnly }, title: " " },
                    { command: { text: "Split", click: SplitDealingInstructionEquitySellOnly }, width: 70, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "OrderPrice", title: "Open Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "Lot", title: "Open Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 { field: "CounterpartID", title: "Broker", },
                 { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                { field: "BoardTypeDesc", title: "Type" },
                { field: "InvestmentNotes", hidden: true, title: "Remark" },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        } else {
            var gridEquitySellOnly = $("#gridDealingInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "DealingEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, width: 70, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionEquitySellOnly }, title: " " },
                    //{ command: { text: "Match", click: ApprovedDealingInstructionEquitySellOnly }, title: " " },
                    { command: { text: "Split", click: SplitDealingInstructionEquitySellOnly }, width: 70, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100,
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock", width: 90 },
                    //{ field: "AvgPrice", title: "Avg Price", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "OrderPrice", title: "Open Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Lot", title: "Open Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", width: 50, },
                    { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { field: "BoardTypeDesc", title: "Type" },
                    { field: "InvestmentNotes", hidden: true, title: "Remark" },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }




        gridEquitySellOnly.table.on("click", ".checkboxEquitySell", selectRowEquitySell);
        var oldPageSizeApproved = 0;


        $('#chbEquitySell').change(function (ev) {

            var grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxEquitySell').each(function (idx, item) {
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
            for (var i in checkedEquitySell) {
                if (checkedEquitySell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowEquitySell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedEquitySell[dataItemZ.DealingPK] = checked;
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

            //alertify.alert(_msg);
            SelectDeselectAllDataEquitySellOnly(_checked, "Approved");

        });

        //gridEquitySellOnly.table.on("click", ".cSelectedDetailApprovedEquitySell", selectDataApprovedEquitySell);

        function selectDataApprovedEquitySell(e) {


            var grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataEquitySellOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataEquitySellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Dealing",
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
    function SelectDeselectAllDataEquitySellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshEquitySell();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }



    function InitgridDealingInstructionBondBuyOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
        dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        var gridBondBuyOnly = $("#gridDealingInstructionBondBuyOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridDealingInstructionBondBuyOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridDealingInstructionTemplateBondBuyOnly").html()),
            excel: {
                fileName: "DealingBondBuyInstruction.xlsx"
            },
            columns: [
                 { command: { text: "Show", click: showDetailsBondBuy }, title: " " },
                 //{ command: { text: "Reject", click: RejectDealingInstructionBondBuyOnly }, title: " " },
                 //{ command: { text: "Approved", click: ApprovedDealingInstructionBondBuyOnly }, title: " " },
                 //{ command: { text: "Split", click: SplitDealingInstructionBondBuyOnly }, title: " " },
                {
                    headerTemplate: "<input type='checkbox' id='chbBondBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondBuy'></label>",
                    template: "<input type='checkbox' class='checkboxBondBuy'/>", width: 45
                },
                 //{
                 //   field: "SelectedDealing",
                 //   width: 50,
                 //   template: "<input class='cSelectedDetailApprovedBondBuy' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                 //   headerTemplate: "<input id='SelectedAllApprovedBondBuy' type='checkbox'  />",
                 //   filterable: true,
                 //   sortable: false,
                 //   columnMenu: false
                 //},
                 { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc",hidden:true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                    field: "FlagAcq", title: "Update Acq", headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:center;" }, template: "#if(AcqPrice == '0') {#<div class='customClass'>#'Pending'#</div>#} else{##:'Done'##}#"
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "OrderPrice", title: "Open Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "Volume", title: "Open Nominal", hidden: true, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                 { field: "CounterpartID", title: "Broker", },
                 { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 {
                     field: "DoneAccruedInterest", title: "Acc.Interest", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 {
                    field: "IncomeTaxGainAmount", title: "Tax Gain", headerAttributes: {
                        style: "text-align: center"
                    }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 {
                     field: "IncomeTaxInterestAmount", title: "Tax Interest", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridBondBuyOnly.table.on("click", ".checkboxBondBuy", selectRowBondBuy);
        var oldPageSizeApproved = 0;


        $('#chbBondBuy').change(function (ev) {

            var grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxBondBuy').each(function (idx, item) {
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
            for (var i in checkedBondBuy) {
                if (checkedBondBuy[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowBondBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedBondBuy[dataItemZ.DealingPK] = checked;
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

            //alertify.alert(_msg);
            SelectDeselectAllDataBondBuyOnly(_checked, "Approved");

        });

        //gridBondBuyOnly.table.on("click", ".cSelectedDetailApprovedBondBuy", selectDataApprovedBondBuy);

        function selectDataApprovedBondBuy(e) {
            

            var grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataBondBuyOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataBondBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Dealing",
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
    function SelectDeselectAllDataBondBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshBondBuy();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }



    function InitgridDealingInstructionBondSellOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }

        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
        dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        var gridBondSellOnly = $("#gridDealingInstructionBondSellOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridDealingInstructionBondSellOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridDealingInstructionTemplateBondSellOnly").html()),
            excel: {
                fileName: "DealingBondSellInstruction.xlsx"
            },
            columns: [
                 { command: { text: "Show", click: showDetailsBondSell }, title: " " },
                 //{ command: { text: "Reject", click: RejectDealingInstructionBondSellOnly }, title: " " },
                 //{ command: { text: "Approved", click: ApprovedDealingInstructionBondSellOnly }, title: " " },
                 //{ command: { text: "Split", click: SplitDealingInstructionBondSellOnly }, title: " " },
                {
                    headerTemplate: "<input type='checkbox' id='chbBondSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondSell'></label>",
                    template: "<input type='checkbox' class='checkboxBondSell'/>", width: 45
                },
                 //{
                 //   field: "SelectedDealing",
                 //   width: 50,
                 //   template: "<input class='cSelectedDetailApprovedBondSell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                 //   headerTemplate: "<input id='SelectedAllApprovedBondSell' type='checkbox'  />",
                 //   filterable: true,
                 //   sortable: false,
                 //   columnMenu: false
                 //},
                 { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc",hidden:true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                {
                    field: "FlagAcq", title: "Update Acq", headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:center;" }, template: "#if(AcqPrice == '0') {#<div class='customClass'>#'Pending'#</div>#} else{##:'Done'##}#"
                },
                 { field: "InstrumentID", title: "Stock" },
                 //{ field: "OrderPrice", title: "Open Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 //{ field: "Volume", title: "Open Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 { field: "CounterpartID", title: "Broker", },
                 { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 {
                     field: "DoneAccruedInterest", title: "Acc.Interest", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 {
                    field: "IncomeTaxGainAmount", title: "Tax Gain", headerAttributes: {
                    style: "text-align: center"
                    }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 {
                    field: "IncomeTaxInterestAmount", title: "Tax Interest", headerAttributes: {
                    style: "text-align: center"
                 }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                 //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate", title: "Acq Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#" },
                { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               
                { field: "AcqPrice6", title: "Acq Price 6", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate6", title: "Acq Date 6", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate6), 'MM/dd/yyyy')#" },
                { field: "AcqVolume6", title: "Acq Volume 6", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice7", title: "Acq Price 7", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate7", title: "Acq Date 7", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate7), 'MM/dd/yyyy')#" },
                { field: "AcqVolume7", title: "Acq Volume 7", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice8", title: "Acq Price 8", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate8", title: "Acq Date 8", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate8), 'MM/dd/yyyy')#" },
                { field: "AcqVolume8", title: "Acq Volume 8", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice9", title: "Acq Price 9", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate9", title: "Acq Date 9", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate9), 'MM/dd/yyyy')#" },
                { field: "AcqVolume9", title: "Acq Volume 9", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridBondSellOnly.table.on("click", ".checkboxBondSell", selectRowBondSell);
        var oldPageSizeApproved = 0;


        $('#chbBondSell').change(function (ev) {

            var grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxBondSell').each(function (idx, item) {
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
            for (var i in checkedBondSell) {
                if (checkedBondSell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowBondSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedBondSell[dataItemZ.DealingPK] = checked;
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

            //alertify.alert(_msg);
            SelectDeselectAllDataBondSellOnly(_checked, "Approved");

        });

        //gridBondSellOnly.table.on("click", ".cSelectedDetailApprovedBondSell", selectDataApprovedBondSell);

        function selectDataApprovedBondSell(e) {
            

            var grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataBondSellOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataBondSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Dealing",
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
    function SelectDeselectAllDataBondSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshBondSell();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }




    function InitgridDealingInstructionTimeDepositBuyOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        if (_GlobClientCode == "03") {
            var gridTimeDepositBuyOnly = $("#gridDealingInstructionTimeDepositBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionTimeDepositBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateTimeDepositBuyOnly").html()),
                excel: {
                    fileName: "DealingTimeDepositBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositBuy }, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionTimeDepositBuyOnly }, title: " " },
                    //{ command: { text: "Approved", click: ApprovedDealingInstructionTimeDepositBuyOnly }, title: " " },
                    //{ command: { text: "Split", click: SplitDealingInstructionTimeDepositBuyOnly }, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoBuy'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "TrxTypeID", title: "Trx Type", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },
                    {
                        field: "BankID", title: "Bank Custody", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "OrderPrice", title: "Open Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Open Nominal", hidden: true, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                    { field: "CounterpartID", title: "Counterpart", hidden: true, },
                    { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAccruedInterest", hidden: true, title: "Acc.Interest", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },

                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var gridTimeDepositBuyOnly = $("#gridDealingInstructionTimeDepositBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionTimeDepositBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateTimeDepositBuyOnly").html()),
                excel: {
                    fileName: "DealingTimeDepositBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositBuy }, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionTimeDepositBuyOnly }, title: " " },
                    //{ command: { text: "Approved", click: ApprovedDealingInstructionTimeDepositBuyOnly }, title: " " },
                    //{ command: { text: "Split", click: SplitDealingInstructionTimeDepositBuyOnly }, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoBuy'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "OrderPrice", title: "Open Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Open Nominal", hidden: true, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                    { field: "CounterpartID", title: "Counterpart", hidden: true, },
                    { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAccruedInterest", hidden: true, title: "Acc.Interest", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },

                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        gridTimeDepositBuyOnly.table.on("click", ".checkboxDepositoBuy", selectRowDepositoBuy);
        var oldPageSizeApproved = 0;


        $('#chbDepositoBuy').change(function (ev) {

            var grid = $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxDepositoBuy').each(function (idx, item) {
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
            for (var i in checkedDepositoBuy) {
                if (checkedDepositoBuy[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowDepositoBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedDepositoBuy[dataItemZ.DealingPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }

        }

        $("#SelectedAllApprovedTimeDepositBuy").change(function () {
            var element = $("#SelectedAllApprovedTimeDepositBuy");
            var posY = element.offset();
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            //alertify.alert(_msg).moveTo(posY.left, posY.top - 200);
            SelectDeselectAllDataTimeDepositBuyOnly(_checked, "Approved");

        });

        //gridTimeDepositBuyOnly.table.on("click", ".cSelectedDetailApprovedTimeDepositBuy", selectDataApprovedTimeDepositBuy);

        function selectDataApprovedTimeDepositBuy(e) {


            var grid = $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositBuyOnly(_checked, _dealingPK);

        }

    }


    function SelectDeselectDataTimeDepositBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Dealing",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
               refreshTimeDepositBuy();
              
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }



    function InitgridDealingInstructionTimeDepositSellOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }

        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
        dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        if (_GlobClientCode == "03") {
            var gridTimeDepositSellOnly = $("#gridDealingInstructionTimeDepositSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionTimeDepositSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateTimeDepositSellOnly").html()),
                excel: {
                    fileName: "DealingTimeDepositSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositSell }, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionTimeDepositSellOnly }, title: " " },
                    //{ command: { text: "Approved", click: ApprovedDealingInstructionTimeDepositSellOnly }, title: " " },
                    //{ command: { text: "Split", click: SplitDealingInstructionTimeDepositSellOnly }, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoSell'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "TrxTypeID", title: "Trx Type", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },
                    {
                        field: "BankID", title: "Bank Custody", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "OrderPrice", title: "Open Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Open Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Counterpart", hidden: true, },
                    { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAccruedInterest", hidden: true, title: "Acc.Interest", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var gridTimeDepositSellOnly = $("#gridDealingInstructionTimeDepositSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridDealingInstructionTimeDepositSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridDealingInstructionTemplateTimeDepositSellOnly").html()),
                excel: {
                    fileName: "DealingTimeDepositSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositSell }, title: " " },
                    //{ command: { text: "Reject", click: RejectDealingInstructionTimeDepositSellOnly }, title: " " },
                    //{ command: { text: "Approved", click: ApprovedDealingInstructionTimeDepositSellOnly }, title: " " },
                    //{ command: { text: "Split", click: SplitDealingInstructionTimeDepositSellOnly }, title: " " },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoSell'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedDealing",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "OrderPrice", title: "Open Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Open Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Nominal", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Counterpart", hidden: true, },
                    { field: "UpdateDealingTime", hidden: true, title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { field: "ApprovedDealingTime", hidden: true, title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAccruedInterest", hidden: true, title: "Acc.Interest", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                    { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                    { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                    { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        gridTimeDepositSellOnly.table.on("click", ".checkboxDepositoSell", selectRowDepositoSell);
        var oldPageSizeApproved = 0;


        $('#chbDepositoSell').change(function (ev) {

            var grid = $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxDepositoSell').each(function (idx, item) {
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
            for (var i in checkedDepositoSell) {
                if (checkedDepositoSell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowDepositoSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedDepositoSell[dataItemZ.DealingPK] = checked;
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

          //  alertify.alert(_msg);
            SelectDeselectAllDataTimeDepositSellOnly(_checked, "Approved");

        });

        //gridTimeDepositSellOnly.table.on("click", ".cSelectedDetailApprovedTimeDepositSell", selectDataApprovedTimeDepositSell);

        function selectDataApprovedTimeDepositSell(e) {
            

            var grid = $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositSellOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataTimeDepositSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Dealing",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshTimeDepositSell();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }


    function InitgridDealingInstructionReksadanaBuyOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
        dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        var gridReksadanaBuyOnly = $("#gridDealingInstructionReksadanaBuyOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridDealingInstructionReksadanaBuyOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridDealingInstructionTemplateReksadanaBuyOnly").html()),
            excel: {
                fileName: "DealingReksadanaBuyInstruction.xlsx"
            },
            columns: [
                 { command: { text: "Show", click: showDetailsReksadanaBuy }, title: " " },
                 //{ command: { text: "Reject", click: RejectDealingInstructionEquityBuyOnly }, title: " " },
                 //{ command: { text: "Match", click: ApprovedDealingInstructionEquityBuyOnly }, title: " " },
                 //{ command: { text: "Split", click: SplitDealingInstructionReksadanaBuyOnly }, title: " " },
                {
                    headerTemplate: "<input type='checkbox' id='chbReksadanaBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbReksadanaBuy'></label>",
                    template: "<input type='checkbox' class='checkboxReksadanaBuy'/>", width: 45
                },
                 //{
                 //    field: "SelectedDealing",
                 //    width: 50,
                 //    template: "<input class='cSelectedDetailApprovedReksadanaBuy' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                 //    headerTemplate: "<input id='SelectedAllApprovedReksadanaBuy' type='checkbox'  />",
                 //    filterable: true,
                 //    sortable: false,
                 //    columnMenu: false
                 //},
                 { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "DonePrice", title: "NAV", format: "{0:n2}", attributes: { style: "text-align:right;" } },
                 //{ field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 //{ field: "CounterpartID", title: "Broker", },
                 { field: "DoneVolume", title: "Done Unit", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "Volume", title: "Unit", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "CounterpartID", title: "Broker", },
                 //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 { field: "Amount", title: "Amount", format: "{0:n2}", attributes: { style: "text-align:right;" } },
                 {
                     field: "DoneAmount", title: "Done Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }
                 },
                 { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                { field: "BoardTypeDesc", title: "Type" },
                { field: "InvestmentNotes", hidden: true, title: "Remark" },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridReksadanaBuyOnly.table.on("click", ".checkboxReksadanaBuy", selectRowReksadanaBuy);
        var oldPageSizeApproved = 0;


        $('#chbReksadanaBuy').change(function (ev) {

            var grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxReksadanaBuy').each(function (idx, item) {
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
            for (var i in checkedReksadanaBuy) {
                if (checkedReksadanaBuy[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowReksadanaBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedReksadanaBuy[dataItemZ.DealingPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }

        }

        $("#SelectedAllApprovedReksadanaBuy").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            //alertify.alert(_msg);
            SelectDeselectAllDataReksadanaBuyOnly(_checked, "Approved");

        });

        //gridReksadanaBuyOnly.table.on("click", ".cSelectedDetailApprovedReksadanaBuy", selectDataApprovedReksadanaBuy);

        function selectDataApprovedReksadanaBuy(e) {


            var grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaBuyOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataReksadanaBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Dealing",
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
    function SelectDeselectAllDataReksadanaBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshReksadanaBuy();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }


    function InitgridDealingInstructionReksadanaSellOnly() {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#FilterCounterpartID").val() == "") {
            _counterpartID = "0";
        }
        else {
            _counterpartID = $("#FilterCounterpartID").val();
        }
        var DealingInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataDealingByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
        dataSourceApproved = getDataSource(DealingInstructionApprovedURL);

        var gridReksadanaSellOnly = $("#gridDealingInstructionReksadanaSellOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridDealingInstructionReksadanaSellOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridDealingInstructionTemplateReksadanaSellOnly").html()),
            excel: {
                fileName: "DealingReksadanaSellInstruction.xlsx"
            },
            columns: [
                 { command: { text: "Show", click: showDetailsReksadanaSell }, title: " " },
                 //{ command: { text: "Reject", click: RejectDealingInstructionReksadanaSellOnly }, title: " " },
                 //{ command: { text: "Match", click: ApprovedDealingInstructionEquitySellOnly }, title: " " },
                 //{ command: { text: "Split", click: SplitDealingInstructionReksadanaSellOnly }, title: " " },
                {
                    headerTemplate: "<input type='checkbox' id='chbReksadanaSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbReksadanaSell'></label>",
                    template: "<input type='checkbox' class='checkboxReksadanaSell'/>", width: 45
                },
                 //{
                 //    field: "SelectedDealing",
                 //    width: 50,
                 //    template: "<input class='cSelectedDetailApprovedReksadanaSell' type='checkbox'   #= SelectedDealing ? 'checked=checked' : '' # />",
                 //    headerTemplate: "<input id='SelectedAllApprovedReksadanaSell' type='checkbox'  />",
                 //    filterable: true,
                 //    sortable: false,
                 //    columnMenu: false
                 //},
                 { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc", hidden: true, title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "OrderPrice", title: "Open Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 //{ field: "Lot", title: "Open Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 //{ field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 { field: "FundID", title: "Fund", },
                 { field: "CounterpartID", title: "Broker", },
                 { hidden: true, field: "UpdateDealingTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { field: "ApprovedDealingTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                 },
                { field: "BoardTypeDesc", title: "Type" },
                { field: "InvestmentNotes", hidden: true, title: "Remark" },
                { field: "StatusDealing", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "InstructionDate", title: "Instruction Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(InstructionDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "Reference", title: "Reference", hidden: true, width: 50 },
                { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                //{ field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                //{ field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                //{ field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                {
                    field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                    template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "BankPK", title: "BankPK", hidden: true, width: 50 },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntryDealingID", title: "Entry Dealing ID", hidden: true, width: 50 },
                { field: "EntryDealingTime", title: "E. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateDealingID", title: "Update Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingID", title: "Approved Dealing ID", hidden: true, width: 50 },
                { field: "ApprovedDealingTime", title: "A. Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidDealingID", title: "Void Dealing ID", hidden: true, width: 50 },
                { field: "VoidDealingTime", title: "V.Dealing Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridReksadanaSellOnly.table.on("click", ".checkboxReksadanaSell", selectRowDepositoSell);
        var oldPageSizeApproved = 0;


        $('#chbReksadanaSell').change(function (ev) {

            var grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxReksadanaSell').each(function (idx, item) {
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
            for (var i in checkedDepositoSell) {
                if (checkedDepositoSell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });


        function selectRowDepositoSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedDepositoSell[dataItemZ.DealingPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }
        }

        $("#SelectedAllApprovedReksadanaSell").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            //alertify.alert(_msg);
            SelectDeselectAllDataReksadanaSellOnly(_checked, "Approved");

        });

        //gridReksadanaSellOnly.table.on("click", ".cSelectedDetailApprovedReksadanaSell", selectDataApprovedReksadanaSell);

        function selectDataApprovedReksadanaSell(e) {


            var grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _dealingPK = dataItemX.DealingPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaSellOnly(_checked, _dealingPK);

        }

    }

    function SelectDeselectDataReksadanaSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataDealingReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Dealing",
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
    function SelectDeselectAllDataReksadanaSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refreshReksadanaSell();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }


    $("#BtnRefresh").click(function () {
        refresh();

    });

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });
    //disini. tambahin DoneAccruedInterest
    $("#BtnUpdate").click(function () {
        var val = validateData();


        if (GlobInstrumentType == 2 || GlobInstrumentType == 3 || GlobInstrumentType == 8 || GlobInstrumentType == 9 || GlobInstrumentType == 13 || GlobInstrumentType == 15) {
            //DonePrice = $('#DonePrice').val() / 100
            ClearDataBond();
            //RecalNetAmount();

        }
        else if (GlobInstrumentType == 1) {
            ClearDataEquity();
        }
        else if (GlobInstrumentType == 5) {
            ClearDataMoney();
        }
        if ($('#StatusDealing').val() == 2 && GlobStatusSettlement != 3) {
            alertify.alert("Data Already Match");
            return;
        }
        if ($('#StatusDealing').val() == 3) {
            alertify.alert("Data Already Void");
            return;
        }
        if ($('#DonePrice').val() == 0 || $('#DonePrice').val() == "") {
            alertify.alert("Please Fill Done Price First!");
            return;
        }

        if ($('#DoneVolume').val() == 0 || $('#DoneVolume').val() == "") {
            alertify.alert("Please Fill Done Volume First!");
            return;
        }


        if (GlobStatusSettlement == 0 || GlobStatusSettlement == 1) {
            if (val == 1) {
                alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#DonePrice').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data.Validate == 0) {
                                                var DealingInstruction = {
                                                    InvestmentPK: $('#InvestmentPK').val(),
                                                    SettlementPK: $('#SettlementPK').val(),
                                                    DealingPK: $('#DealingPK').val(),
                                                    StatusDealing: $('#StatusDealing').val(),
                                                    HistoryPK: $('#HistoryPK').val(),
                                                    PeriodPK: $('#PeriodPK').val(),
                                                    ValueDate: $('#ValueDate').val(),
                                                    Reference: $('#Reference').val(),
                                                    InstructionDate: $('#InstructionDate').val(),
                                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                    InstrumentPK: $('#InstrumentPK').val(),
                                                    BoardType: $('#BoardType').val(),
                                                    Type: GlobInstrumentType,
                                                    OrderPrice: $('#OrderPrice').val(),
                                                    RangePrice: $('#RangePrice').val(),
                                                    AcqPrice: $('#AcqPrice').val(),
                                                    Lot: $("#Lot").data("kendoNumericTextBox").value(),
                                                    LotInShare: 100,
                                                    Volume: $('#Volume').val(),
                                                    Amount: $('#Amount').val(),
                                                    DoneLot: $("#DoneLot").data("kendoNumericTextBox").value(),
                                                    DoneVolume: $('#DoneVolume').val(),
                                                    DonePrice: $('#DonePrice').val(),
                                                    DoneAmount: $('#DoneAmount').val(),
                                                    TrxType: GlobTrxType,
                                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                    CounterpartPK: $('#CounterpartPK').val(),
                                                    SettledDate: $('#SettledDate').val(),
                                                    LastCouponDate: $('#LastCouponDate').val(),
                                                    NextCouponDate: $('#NextCouponDate').val(),
                                                    FundCashRefPK: 1,
                                                    MaturityDate: $('#MaturityDate').val(),
                                                    AcqDate: $('#AcqDate').val(),
                                                    AcqVolume: $('#AcqVolume').val(),
                                                    InterestPercent: $('#InterestPercent').val(),
                                                    BreakInterestPercent: $('#BreakInterestPercent').val(),
                                                    AccruedInterest: $('#AccruedInterest').val(),
                                                    DoneAccruedInterest: $('#DoneAccruedInterest').val(),
                                                    FundPK: $('#FundPK').val(),
                                                    Tenor: $('#Tenor').val(),
                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                    AmountToTransfer: $('#AmountToTransfer').val(),
                                                    AcqPrice1: $('#AcqPrice1').val(),
                                                    AcqDate1: $('#AcqDate1').val(),
                                                    AcqVolume1: $('#AcqVolume1').val(),
                                                    AcqPrice2: $('#AcqPrice2').val(),
                                                    AcqDate2: $('#AcqDate2').val(),
                                                    AcqVolume2: $('#AcqVolume2').val(),
                                                    AcqPrice3: $('#AcqPrice3').val(),
                                                    AcqDate3: $('#AcqDate3').val(),
                                                    AcqVolume3: $('#AcqVolume3').val(),
                                                    AcqPrice4: $('#AcqPrice4').val(),
                                                    AcqDate4: $('#AcqDate4').val(),
                                                    AcqVolume4: $('#AcqVolume4').val(),
                                                    AcqPrice5: $('#AcqPrice5').val(),
                                                    AcqDate5: $('#AcqDate5').val(),
                                                    AcqVolume5: $('#AcqVolume5').val(),
                                                    AcqPrice6: $('#AcqPrice6').val(),
                                                    AcqDate6: $('#AcqDate6').val(),
                                                    AcqVolume6: $('#AcqVolume6').val(),
                                                    AcqPrice7: $('#AcqPrice7').val(),
                                                    AcqDate7: $('#AcqDate7').val(),
                                                    AcqVolume7: $('#AcqVolume7').val(),
                                                    AcqPrice8: $('#AcqPrice8').val(),
                                                    AcqDate8: $('#AcqDate8').val(),
                                                    AcqVolume8: $('#AcqVolume8').val(),
                                                    AcqPrice9: $('#AcqPrice9').val(),
                                                    AcqDate9: $('#AcqDate9').val(),
                                                    AcqVolume9: $('#AcqVolume9').val(),
                                                    SettlementMode: $('#SettlementMode').val(),
                                                    IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                                                    IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                                                    IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                                                    IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                    IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                    IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                                    TotalAmount: $('#TotalAmount').val(),
                                                    MarketPK: $('#MarketPK').val(),
                                                    PriceMode: $('#PriceMode').val(),
                                                    BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                    YieldPercent: $('#YieldPercent').val(),
                                                    BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                    Category: $('#Category').val(),
                                                    BankBranchPK: $('#BankBranchPK').val(),
                                                    CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                    BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                    InvestmentTrType: $('#InvestmentTrType').val(),
                                                    InterestDaysType: $('#InterestDaysType').val(),
                                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                                    PaymentInterestSpecificDate: $('#SpecificDate').val(),
                                                    BitBreakable: $('#BitBreakable').is(":checked"),
                                                    Notes: str,
                                                    EntryDealingID: sessionStorage.getItem("user"),
                                                    UpdateDealingID: sessionStorage.getItem("user"),
                                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                                };

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_U",
                                                    type: 'POST',
                                                    data: JSON.stringify(DealingInstruction),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {

                                                        alertify.alert(data);
                                                        win.close();
                                                        refresh();
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                alertify.confirm("Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice + " </br> Are You Sure To Continue ?", function (e) {
                                                    if (e) {
                                                        var DealingInstruction = {
                                                            InvestmentPK: $('#InvestmentPK').val(),
                                                            SettlementPK: $('#SettlementPK').val(),
                                                            DealingPK: $('#DealingPK').val(),
                                                            StatusDealing: $('#StatusDealing').val(),
                                                            HistoryPK: $('#HistoryPK').val(),
                                                            PeriodPK: $('#PeriodPK').val(),
                                                            ValueDate: $('#ValueDate').val(),
                                                            Reference: $('#Reference').val(),
                                                            InstructionDate: $('#InstructionDate').val(),
                                                            InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                            InstrumentPK: $('#InstrumentPK').val(),
                                                            BoardType: $('#BoardType').val(),
                                                            Type: GlobInstrumentType,
                                                            OrderPrice: $('#OrderPrice').val(),
                                                            RangePrice: $('#RangePrice').val(),
                                                            AcqPrice: $('#AcqPrice').val(),
                                                            Lot: $("#Lot").data("kendoNumericTextBox").value(),
                                                            LotInShare: 100,
                                                            Volume: $('#Volume').val(),
                                                            Amount: $('#Amount').val(),
                                                            DoneLot: $("#DoneLot").data("kendoNumericTextBox").value(),
                                                            DoneVolume: $('#DoneVolume').val(),
                                                            DonePrice: $('#DonePrice').val(),
                                                            DoneAmount: $('#DoneAmount').val(),
                                                            TrxType: GlobTrxType,
                                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                            CounterpartPK: $('#CounterpartPK').val(),
                                                            SettledDate: $('#SettledDate').val(),
                                                            LastCouponDate: $('#LastCouponDate').val(),
                                                            NextCouponDate: $('#NextCouponDate').val(),
                                                            FundCashRefPK: 1,
                                                            MaturityDate: $('#MaturityDate').val(),
                                                            AcqDate: $('#AcqDate').val(),
                                                            AcqVolume: $('#AcqVolume').val(),
                                                            InterestPercent: $('#InterestPercent').val(),
                                                            BreakInterestPercent: $('#BreakInterestPercent').val(),
                                                            AccruedInterest: $('#AccruedInterest').val(),
                                                            DoneAccruedInterest: $('#DoneAccruedInterest').val(),
                                                            FundPK: $('#FundPK').val(),
                                                            Tenor: $('#Tenor').val(),
                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                            AmountToTransfer: $('#AmountToTransfer').val(),
                                                            AcqPrice1: $('#AcqPrice1').val(),
                                                            AcqDate1: $('#AcqDate1').val(),
                                                            AcqVolume1: $('#AcqVolume1').val(),
                                                            AcqPrice2: $('#AcqPrice2').val(),
                                                            AcqDate2: $('#AcqDate2').val(),
                                                            AcqVolume2: $('#AcqVolume2').val(),
                                                            AcqPrice3: $('#AcqPrice3').val(),
                                                            AcqDate3: $('#AcqDate3').val(),
                                                            AcqVolume3: $('#AcqVolume3').val(),
                                                            AcqPrice4: $('#AcqPrice4').val(),
                                                            AcqDate4: $('#AcqDate4').val(),
                                                            AcqVolume4: $('#AcqVolume4').val(),
                                                            AcqPrice5: $('#AcqPrice5').val(),
                                                            AcqDate5: $('#AcqDate5').val(),
                                                            AcqVolume5: $('#AcqVolume5').val(),
                                                            AcqPrice6: $('#AcqPrice6').val(),
                                                            AcqDate6: $('#AcqDate6').val(),
                                                            AcqVolume6: $('#AcqVolume6').val(),
                                                            AcqPrice7: $('#AcqPrice7').val(),
                                                            AcqDate7: $('#AcqDate7').val(),
                                                            AcqVolume7: $('#AcqVolume7').val(),
                                                            AcqPrice8: $('#AcqPrice8').val(),
                                                            AcqDate8: $('#AcqDate8').val(),
                                                            AcqVolume8: $('#AcqVolume8').val(),
                                                            AcqPrice9: $('#AcqPrice9').val(),
                                                            AcqDate9: $('#AcqDate9').val(),
                                                            AcqVolume9: $('#AcqVolume9').val(),
                                                            SettlementMode: $('#SettlementMode').val(),
                                                            IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                                                            IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                                                            IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                                                            IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                            IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                            IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                                            TotalAmount: $('#TotalAmount').val(),
                                                            MarketPK: $('#MarketPK').val(),
                                                            PriceMode: $('#PriceMode').val(),
                                                            BitIsAmortized: $('#BitIsAmortized').is(":checked"),
                                                            YieldPercent: $('#YieldPercent').val(),
                                                            BitIsRounding: $('#BitIsRounding').is(":checked"),
                                                            Category: $('#Category').val(),
                                                            BankBranchPK: $('#BankBranchPK').val(),
                                                            CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                            BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                            InvestmentTrType: $('#InvestmentTrType').val(),
                                                            InterestDaysType: $('#InterestDaysType').val(),
                                                            InterestPaymentType: $('#InterestPaymentType').val(),
                                                            PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                                            PaymentInterestSpecificDate: $('#SpecificDate').val(),
                                                            BitBreakable: $('#BitBreakable').is(":checked"),
                                                            Notes: str,
                                                            EntryDealingID: sessionStorage.getItem("user"),
                                                            UpdateDealingID: sessionStorage.getItem("user"),
                                                            ApprovedDealingID: sessionStorage.getItem("user"),
                                                        };

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_U",
                                                            type: 'POST',
                                                            data: JSON.stringify(DealingInstruction),
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {

                                                                alertify.alert(data);
                                                                win.close();
                                                                refresh();
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                            }
                                                        });


                                                    }

                                                });
                                            }
                                        }
                                    });

                                } else {
                                    alertify.alert("Data has been Changed by other user, Please check it first!");
                                    win.close();
                                    refresh();
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                });
            }
        }
        else if (GlobStatusSettlement == 3) {
            alertify.alert("Data has been Reject by Settlement, Please Contact Settlement!");
        }
        else {
            alertify.alert("Data has been Approved by Settlement, Please Contact Settlement!");

        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Investment" + "/" + $("#InvestmentPK").val(),
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
                            schema: {
                                model: {
                                    fields: {
                                        FieldName: { type: "string" },
                                        OldValue: { type: "string" },
                                        NewValue: { type: "string" },
                                        Similarity: { type: "number" },
                                    }
                                }
                            }
                        },
                        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                        columnMenu: false,
                        pageable: {
                            input: true,
                            numeric: false
                        },
                        height: 470,
                        reorderable: true,
                        sortable: true,
                        resizable: true,
                        dataBound: gridOldDataDataBound,
                        columns: [
                            { field: "FieldName", title: "FieldName", width: 300 },
                            { field: "OldValue", title: "OldValue", width: 300 },
                            { field: "NewValue", title: "NewValue", width: 300 },
                            { field: "Similarity", title: "Similarity", width: 120 }
                        ]
                    });
                    winOldData.center();
                    winOldData.open();
                } else {
                    alertify.alert("Data has been Changed by other user, Please check it first!");
                    win.close();
                    refresh();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };

    $("#BtnApproved").click(function () {
        var val = validateData();
        
        var DealingInstruction = {
            InstrumentTypePK: $('#InstrumentTypePK').val(),
            DealingPK: $('#DealingPK').val(),
            HistoryPK: $('#HistoryPK').val()
        };
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Investment/Dealing_ApproveValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'POST',
        //    data: JSON.stringify(DealingInstruction),
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        if (data == 1) {
        //            alertify.alert("Validation Approve not Pass");
        //        } else {
                    alertify.confirm("Are you sure want to Approved data?", function (e) {
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                        var DealingInstruction = {
                                            StatusDealing: $('#StatusDealing').val(),
                                            InvestmentPK: $('#InvestmentPK').val(),
                                            DealingPK: $('#DealingPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedDealingID: sessionStorage.getItem("user"),
                                            VoidDealingID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_A",
                                            type: 'POST',
                                            data: JSON.stringify(DealingInstruction),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
                                                win.close();
                                                refresh();
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    } else {
                                        alertify.alert("Data has been Changed by other user, Please check it first!");
                                        win.close();
                                        refresh();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
        //            });
        //        }
        //    },
        //    error: function (data) {
        //    }
        });
    });

    $("#BtnVoid").click(function () {
        
        if (GlobStatusSettlement == 0 || GlobStatusSettlement == 1) {
            alertify.confirm("Are you sure want to Void data?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var DealingInstruction = {
                                    StatusDealing: $('#StatusDealing').val(),
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    DealingPK: $('#DealingPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    VoidDealingID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_V",
                                    type: 'POST',
                                    data: JSON.stringify(DealingInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh();

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                win.close();
                                refresh();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
            });
        }
        else if (GlobStatusSettlement == 3) {
            alertify.alert("Data has been Reject by Settlement, Please Contact Settlement!");
        }
        else {
            alertify.alert("Data has been Approved by Settlement, Please Contact Settlement!");

        }
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var DealingInstruction = {
                                StatusDealing: $('#StatusDealing').val(),
                                InvestmentPK: $('#InvestmentPK').val(),
                                DealingPK: $('#DealingPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidDealingID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_R",
                                type: 'POST',
                                data: JSON.stringify(DealingInstruction),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });

    $("#BtnUnApproved").click(function () {
        
        alertify.confirm("Are you sure want to UnApproved data?", function (e) {

            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alert(" Trx Portfolio  Already Posted / Revised, UnApprove Cancel");
            } else {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DealingPK").val() + "/" + $("#HistoryPK").val() + "/" + "Dealing",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var DealingInstruction = {
                                    StatusDealing: $('#StatusDealing').val(),
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    DealingPK: $('#DealingPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DealingInstruction_UnApproved",
                                    type: 'POST',
                                    data: JSON.stringify(DealingInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh();

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                win.close();
                                refresh();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            }
        });
    });

    //$("#BtnSplit").click(function () {
    //    
    //    alertify.confirm("Are you sure want to Split data?", function (e) {
    //        $("#SplitDonePrice").kendoNumericTextBox({
    //            format: "n0",
    //            value:0
    //        });
    //        $("#SplitDoneLot").kendoNumericTextBox({
    //            format: "n0",
    //            value: 0
    //        });
    //        //$("#SplitDoneVolume").kendoNumericTextBox({
    //        //    format: "n0",
    //        //    value: 0
    //        //});
    //        winSplit.center();
    //        winSplit.open();
    //    });
    //});


    $("#BtnSplitClose").click(function () {
        
        alertify.confirm("Are you sure want to cancel Split?", function (e) {
            if (e) {
                winSplit.close();
                alertify.alert("Cancel Split");
            }
        });
    });
     


    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();

        WinListInstrument.center();
        WinListInstrument.open();
        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";


    });
    function getDataSourceListInstrument() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 InstrumentPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" },
                                 Type: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListInstrumentPK() {
        var dsListInstrument = getDataSourceListInstrument();
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
               { field: "Type", title: "Type", width: 100 },
               { field: "ID", title: "ID", width: 300 }

               //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }
    function onWinListInstrumentClose() {
        $("#gridListInstrument").empty();
    }
    function ListInstrumentSelect(e) {

        var grid = $("#gridListInstrument").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlInstrumentName).val(dataItemX.Name);
        $(htmlInstrumentID).val(dataItemX.ID);
        $(htmlInstrumentPK).val(dataItemX.InstrumentPK);

        WinListInstrument.close();


    }


    function DealingRecalculate() {

        if (GlobInstrumentType == 2 || GlobInstrumentType == 3) {
            DonePrice = $("#DonePrice").data("kendoNumericTextBox").value() / 100
            $("#DoneAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() * DonePrice);

            if ($("#SettledDate").data("kendoDatePicker").value() != null
                  && $("#SettledDate").data("kendoDatePicker").value() != ''
                  && $("#LastCouponDate").data("kendoDatePicker").value() != ''
                  && $("#LastCouponDate").data("kendoDatePicker").value() != null
                  && $('#InstrumentPK').val() != ''
                  && $('#InstrumentPK').val() != null
                  && $("#DoneVolume").data("kendoNumericTextBox").value() != ''
                  && $("#DoneVolume").data("kendoNumericTextBox").value() != null
                  ) {
                GetBondInterest();
            }
        }
        else {
            $("#DoneAmount").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() * $("#DonePrice").data("kendoNumericTextBox").value());
        }

    }

    function GetBondInterest() {
        var InvestmentInstruction = {
            InstrumentPK: $('#InstrumentPK').val(),
            SettledDate: $('#SettledDate').val(),
            Volume: $("#DoneVolume").data("kendoNumericTextBox").value(),
            OrderPrice: DonePrice,
            LastCouponDate: $('#LastCouponDate').val()

        };
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/Investment_GetBondInterest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(InvestmentInstruction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#DoneAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                $("#Tenor").data("kendoNumericTextBox").value(data.Tenor);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
   

    function onWinUpdateCounterpartClose() {
        $("#ParamBoardType").val("")
        $("#ParamCounterpartPK").val("")
        $("#ParamSettlementMode").val("")
    }

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Dealing_ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Dealing_ApproveBySelected",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnRejectBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Dealing_RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Dealing_RejectBySelected",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnVoidBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Dealing_VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Dealing_VoidBySelected",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    function gridDealingInstructionEquityBuyOnlyDataBound() {
        var grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid");

        if (grid.dataSource.aggregates().DoneAmount == undefined || grid.dataSource.aggregates().DoneAmount == null)
            $('#AVGPriceBuyDealingEquity').text(kendo.toString(0, "n"));
        else
            $('#AVGPriceBuyDealingEquity').text(kendo.toString(grid.dataSource.aggregates().DoneAmount.sum / grid.dataSource.aggregates().DoneLot.sum / 100, "n"));


        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedEquityBuy[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedEquityBuy[row.DealingPK] = null;
            }

            if (checkedEquityBuy[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxEquityBuy")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            }  else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionEquitySellOnlyDataBound() {
        var grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid");

        if (grid.dataSource.aggregates().DoneAmount == undefined || grid.dataSource.aggregates().DoneAmount == null)
            $('#AVGPriceSellDealingEquity').text(kendo.toString(0, "n"));
        else
            $('#AVGPriceSellDealingEquity').text(kendo.toString(grid.dataSource.aggregates().DoneAmount.sum / grid.dataSource.aggregates().DoneLot.sum / 100, "n"));

        
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedEquitySell[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedEquitySell[row.DealingPK] = null;
            }

            if (checkedEquitySell[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxEquitySell")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionBondBuyOnlyDataBound() {
        var grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedBondBuy[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedBondBuy[row.DealingPK] = null;
            }

            if (checkedBondBuy[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxBondBuy")
                    .attr("checked", "checked");
            }

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionBondSellOnlyDataBound() {
        var grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedBondSell[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedBondSell[row.DealingPK] = null;
            }

            if (checkedBondSell[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxBondSell")
                    .attr("checked", "checked");
            }

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionTimeDepositBuyOnlyDataBound() {
        var grid = $("#gridDealingInstructionTimeDepositBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedDepositoBuy[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedDepositoBuy[row.DealingPK] = null;
            }

            if (checkedDepositoBuy[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxDepositoBuy")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionTimeDepositSellOnlyDataBound() {
        var grid = $("#gridDealingInstructionTimeDepositSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedDepositoSell[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedDepositoSell[row.DealingPK] = null;
            }

            if (checkedDepositoSell[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxDepositoSell")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionReksadanaBuyOnlyDataBound() {
        var grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedReksadanaBuy[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedReksadanaBuy[row.DealingPK] = null;
            }

            if (checkedReksadanaBuy[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxReksadanaBuy")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }

    function gridDealingInstructionReksadanaSellOnlyDataBound() {
        var grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedReksadanaSell[0] = null;

            if (row.OrderStatusDesc == "5.REJECT" || row.StatusDesc == "VOID") {
                checkedReksadanaSell[row.DealingPK] = null;
            }

            if (checkedReksadanaSell[row.DealingPK] && (row.OrderStatusDesc != "5.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxReksadanaSell")
                    .attr("checked", "checked");
            }

            if (row.OrderStatusDesc == "5.REJECT") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.OrderStatusDesc == "3.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "2.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
            } else if (row.OrderStatusDesc == "4.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }


    function SplitDealingInstructionEquityBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            var grid = $("#gridDealingInstructionEquityBuyOnly").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            _showSplit(_dataItem);

        }
        e.handled = true;

    }


    function SplitDealingInstructionEquitySellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            var grid = $("#gridDealingInstructionEquitySellOnly").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            _showSplit(_dataItem);

        }
        e.handled = true;

    }

    function SplitDealingInstructionReksadanaBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {

            var grid = $("#gridDealingInstructionReksadanaBuyOnly").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            _showSplit(_dataItem);

        }
        e.handled = true;

    }


    function SplitDealingInstructionReksadanaSellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {

            var grid = $("#gridDealingInstructionReksadanaSellOnly").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            _showSplit(_dataItem);

        }
        e.handled = true;

    }

    //function SplitDealingInstructionBondBuyOnly(e) {
    //    if (e.handled !== true) // This will prevent event triggering more then once
    //    {
    //        
    //        var grid = $("#gridDealingInstructionBondBuyOnly").data("kendoGrid");
    //        var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
    //        _showSplit(_dataItem);

    //    }
    //    e.handled = true;

    //}


    //function SplitDealingInstructionBondSellOnly(e) {
    //    if (e.handled !== true) // This will prevent event triggering more then once
    //    {
    //        
    //        var grid = $("#gridDealingInstructionBondSellOnly").data("kendoGrid");
    //        var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
    //        _showSplit(_dataItem);

    //    }
    //    e.handled = true;

    //}


    function _showSplit(_dataItem) {


        if (_dataItem.InstrumentTypePK == 1) {
            _approved = "MATCH";
        }
        else if (_dataItem.InstrumentTypePK == 2 || _dataItem.InstrumentTypePK == 3) {
            _approved = "APPROVED";
        }
        if (_dataItem.OrderStatusDesc == _approved) {
            alertify.alert("Data Already " + _approved);
            return;
        }

        if (_dataItem.StatusDealing == 3) {
            alertify.alert("Data Already Void");
            return;
        }
      

        //if (_dataItem.CounterpartID == "") {
        //    alertify.alert("Data Must Be Fill Broker First");
        //    return;
        //}

        //if (_dataItem.OrderStatusDesc == "PARTIAL") {
        //    alertify.alert("Data Already Split");
        //    return;
        //}

        //if (_dataItem.OrderStatusDesc == "OPEN") {
            var _trxTypeID;
            if (_dataItem.TrxType == 1) {
                _trxTypeID = "BUY";
            }
            else {
                _trxTypeID = "SELL";

            }
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alert(" Trx Portfolio  Already Posted / Revised, UnApprove Cancel");
            } else {
                _periodPK = _dataItem.PeriodPK;
                _valueDate = _dataItem.ValueDate;
                _reference = _dataItem.Reference;
                _instructionDate = _dataItem.InstructionDate;
                _instrumentPK = _dataItem.InstrumentPK;
                _instrumentID = _dataItem.InstrumentID;
                _investmentPK = _dataItem.InvestmentPK;
                _dealingPK = _dataItem.DealingPK;
                _historyPK = _dataItem.HistoryPK;
                _orderStatus = _dataItem.OrderStatusDesc;
                _statusInvestment = _dataItem.StatusInvestment;
                _statusDealing = _dataItem.StatusDealing;
                _instrumentTypePK = _dataItem.InstrumentTypePK;
                _orderPrice = _dataItem.OrderPrice;
                _rangePrice = _dataItem.RangePrice;
                _acqPrice = _dataItem.AcqPrice;
                _lot = _dataItem.Lot;
                _lotInShare = _dataItem.LotInShare;
                _volume = _dataItem.Volume;
                _amount = _dataItem.Amount;
                _trxType = _dataItem.TrxType;
                _counterpartPK = _dataItem.CounterpartPK;
                _settledDate = _dataItem.SettledDate;
                _acqDate = _dataItem.AcqDate;
                _lastCouponDate = _dataItem.LastCouponDate;
                _nextCouponDate = _dataItem.NextCouponDate;
                _maturityDate = _dataItem.MaturityDate;
                _interestPercent = _dataItem.InterestPercent;
                _accruedInterest = _dataItem.AccruedInterest;
                _fundPK = _dataItem.FundPK;
                _tenor = _dataItem.Tenor;
                _investmentNotes = _dataItem.InvestmentNotes;
                _donePrice = _dataItem.DonePrice;
                _doneVolume = _dataItem.DoneVolume;
                _doneLot = _dataItem.DoneLot;
                _doneAmount = _dataItem.DoneAmount;
                _acqPrice1 = _dataItem.AcqPrice1;
                _acqDate1 = _dataItem.AcqDate1;
                _acqPrice2 = _dataItem.AcqPrice2;
                _acqDate2 = _dataItem.AcqDate2;
                _acqPrice3 = _dataItem.AcqPrice3;
                _acqDate3 = _dataItem.AcqDate3;
                _acqPrice4 = _dataItem.AcqPrice4;
                _acqDate4 = _dataItem.AcqDate4;
                _acqPrice5 = _dataItem.AcqPrice5;
                _acqDate5 = _dataItem.AcqDate5;
                _entryUsersID = _dataItem.EntryUsersID;
                _entryTime = _dataItem.EntryTime;
                _approvedUsersID = _dataItem.ApprovedUsersID;
                _approvedTime = _dataItem.ApprovedTime;
                _entryDealingID = _dataItem.EntryDealingID;
                _entryDealingTime = _dataItem.EntryDealingTime;
                _updateDealingTime = _dataItem.UpdateDealingTime;
                _approvedDealingTime = _dataItem.ApprovedDealingTime;
                _boardType = _dataItem.BoardType;
                _settlementMode = _dataItem.SettlementMode;
                _doneAccruedInterest = _dataItem.DoneAccruedInterest;
                //_totalAmount = _dataItem.TotalAmount;

                $("#SplitPeriodPK").val(_periodPK);
                $("#SplitValueDate").data("kendoDatePicker").value(_valueDate);
                $("#SplitReference").val(_reference);
                $("#SplitInstructionDate").data("kendoDatePicker").value(_instructionDate);
                $("#SplitInstrumentPK").val(_instrumentPK);
                $("#SplitInstrumentID").val(_instrumentID);
                $("#SplitInvestmentPK").val(_investmentPK);
                $("#SplitDealingPK").val(_dealingPK);
                $("#SplitHistoryPK").val(_historyPK);
                $("#SplitOrderStatus").val(_orderStatus);
                $("#SplitStatusInvestment").val(_statusInvestment);
                $("#SplitStatusDealing").val(_statusDealing);
                $("#SplitInstrumentTypePK").val(_instrumentTypePK);
                $("#SplitOrderPrice").val(_orderPrice);
                $("#SplitRangePrice").val(_rangePrice);
                $("#SplitAcqPrice").val(_acqPrice);
                $("#SplitLot").val(_lot);
                $("#SplitLotInShare").val(_lotInShare);
                $("#SplitVolume").val(_volume);
                $("#SplitAmount").val(_amount);
                $("#SplitTrxType").val(_trxType);
                $("#SplitTrxTypeID").val(_trxTypeID);
                $("#SplitCounterpartPK").val(_counterpartPK);
                $("#SplitSettledDate").data("kendoDatePicker").value(_settledDate);
                $("#SplitAcqDate").data("kendoDatePicker").value(_acqDate);
                $("#SplitLastCouponDate").data("kendoDatePicker").value(_lastCouponDate);
                $("#SplitNextCouponDate").data("kendoDatePicker").value(_nextCouponDate);
                $("#SplitMaturityDate").data("kendoDatePicker").value(_maturityDate);
                $("#SplitInterestPercent").val(_interestPercent);
                $("#SplitAccruedInterest").val(_accruedInterest);
                $("#SplitFundPK").val(_fundPK);
                $("#SplitTenor").val(_tenor);
                $("#SplitInvestmentNotes").val(_investmentNotes);
                $("#SplitDonePrice1").val(_donePrice);
                $("#SplitDoneVolume1").val(_doneVolume);
                $("#SplitDoneLot1").val(_doneLot);
                $("#SplitDoneAmount").val(_doneAmount);
                $("#SplitAcqPrice1").val(_acqPrice1);
                $("#SplitAcqDate1").data("kendoDatePicker").value(_acqDate1);
                $("#SplitAcqPrice2").val(_acqPrice2);
                $("#SplitAcqDate2").data("kendoDatePicker").value(_acqDate2);
                $("#SplitAcqPrice3").val(_acqPrice3);
                $("#SplitAcqDate3").data("kendoDatePicker").value(_acqDate3);
                $("#SplitAcqPrice4").val(_acqPrice4);
                $("#SplitAcqDate4").data("kendoDatePicker").value(_acqDate4);
                $("#SplitAcqPrice5").val(_acqPrice5);
                $("#SplitAcqDate5").data("kendoDatePicker").value(_acqDate5);
                $("#SplitEntryUsersID").val(_entryUsersID);
                $("#SplitEntryTime").val(_entryTime);
                $("#SplitApprovedUsersID").val(_approvedUsersID);
                $("#SplitApprovedTime").val(_approvedTime);
                $("#SplitEntryDealingID").val(_entryDealingID);
                $("#SplitEntryDealingTime").val(_entryDealingTime);
                $("#SplitUpdateDealingTime").val(_updateDealingTime);
                $("#SplitApprovedDealingTime").val(_approvedDealingTime);
                $("#SplitBoardType").val(_boardType);
                $("#SplitSettlementMode").val(_settlementMode);
                $("#SplitDoneAccruedInterest").val(_doneAccruedInterest);
                //$("#SplitTotalAmount").val(_totalAmount);

                _resetSplitAttributes();
                if (_dataItem.InstrumentTypePK == 1) {
                    
                    alertify.confirm("Are you sure want to Split data?", function (e) {
                        if (e) {
                            $("#LblSplitDonePrice").show();
                            $("#LblSplitDoneLot").show();
                            $("#LblSplitDoneVolume").hide();
                            $("#LblSplitTotalAmount").hide();
                            $("#SplitOrderPrice").kendoNumericTextBox({
                                format: "n4",
                            });
                            $("#SplitOrderPrice").data("kendoNumericTextBox").enable(false);

                            $("#SplitLot").kendoNumericTextBox({
                                format: "n4",
                                decimals: 4
                            });
                            $("#SplitLot").data("kendoNumericTextBox").enable(false);

                            $("#SplitDonePrice1").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });
                            $("#SplitDoneLot1").kendoNumericTextBox({
                                format: "n4",
                                value: 0,
                                decimals: 4
                            });
                            $("#SplitDonePrice2").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });
                            $("#SplitDoneLot2").kendoNumericTextBox({
                                format: "n4",
                                value: 0,
                                decimals: 4
                            });
                            $("#SplitDonePrice3").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });
                            $("#SplitDoneLot3").kendoNumericTextBox({
                                format: "n4",
                                value: 0,
                                decimals: 4
                            });
                            $("#SplitDonePrice4").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });
                            $("#SplitDoneLot4").kendoNumericTextBox({
                                format: "n4",
                                value: 0,
                                decimals: 4
                            });
                            $("#SplitDonePrice5").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });
                            $("#SplitDoneLot5").kendoNumericTextBox({
                                format: "n4",
                                value: 0,
                                decimals: 4
                            });
                            $("#SplitTotalAmount1").kendoNumericTextBox({
                                format: "n0",

                            });
                            winSplit.center();
                            winSplit.open();
                        }
                    });
       

                }
                else {
                    
                    alertify.confirm("Are you sure want to Split data?", function (e) {
                        if (e) {
                            $("#LblSplitDonePrice").show();
                            $("#LblSplitDoneLot").hide();
                            $("#LblSplitDoneVolume").show();
                            $("#LblSplitTotalAmount").hide();
                            $("#SplitDonePrice").kendoNumericTextBox({
                                format: "n0",
                                value: 0
                            });

                            $("#SplitDoneVolume").kendoNumericTextBox({
                                format: "n4",
                                value: 4
                            });
                            $("#SplitTotalAmount").kendoNumericTextBox({
                                format: "n4",
                                value: 4

                            });
                            winSplit.center();
                            winSplit.open();
                        }
                    });
  
                }

            }
        //}
        //else {
        //    alertify.alert("Order Status Data Must Be OPEN First!")
        //}
 

    }

    $("#BtnCancelSplit").click(function () {
        
        alertify.confirm("Are you sure want to cancel split?", function (e) {
            if (e) {
                winSplit.close();
                alertify.alert("Close Detail");
            }
        });
    });




    $("#BtnSplitOk").click(function (e) {
        
        alertify.confirm("Are you sure want to Split Data ?", function (e) {
            if (e) {
                if ($('#SplitInstrumentTypePK').val() == 2) {

                    var InvestmentInstruction = {
                        InstrumentPK: $('#SplitInstrumentPK').val(),
                        SettledDate: $('#SplitSettledDate').val(),
                        Volume: $('#SplitDoneVolume').val(),
                        OrderPrice: $('#SplitDonePrice').val(),
                        LastCouponDate: $('#SplitLastCouponDate').val()
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Investment_GetBondInterest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(InvestmentInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#SplitDoneAccruedInterest").val(data.InterestAmount);
                            $("#SplitTenor").val(data.Tenor);
                            //InvestmentInstruction = null;

                            var _a = parseFloat($('#SplitDonePrice').val() / 100);
                            var _b = parseFloat($('#SplitDoneVolume').val());
                            var _c = parseFloat($('#SplitDoneAccruedInterest').val());
                            var InvestmentInstructionSplit = {
                                DoneAccruedInterest: data.InterestAmount,
                                PeriodPK: $('#SplitPeriodPK').val(),
                                ValueDate: $('#SplitValueDate').val(),
                                Reference: $('#SplitReference').val(),
                                InstructionDate: $('#SplitInstructionDate').val(),
                                InstrumentPK: $('#SplitInstrumentPK').val(),
                                DealingPK: $('#SplitDealingPK').val(),
                                StatusInvestment: $('#SplitStatusInvestment').val(),
                                StatusDealing: $('#SplitStatusDealing').val(),
                                InstrumentTypePK: $('#SplitInstrumentTypePK').val(),
                                OrderPrice: $('#SplitOrderPrice').val(),
                                RangePrice: $('#SplitRangePrice').val(),
                                AcqPrice: $('#SplitAcqPrice').val(),
                                Lot: $('#SplitLot').val(),
                                LotInShare: $('#SplitLotInShare').val(),
                                Volume: $('#SplitVolume').val(),
                                Amount: $('#SplitAmount').val(),
                                TrxType: $('#SplitTrxType').val(),
                                TrxTypeID: $("#SplitTrxTypeID").val(),
                                CounterpartPK: $('#SplitCounterpartPK').val(),
                                SettledDate: $('#SplitSettledDate').val(),
                                AcqDate: $('#SplitAcqDate').val(),
                                LastCouponDate: $('#SplitLastCouponDate').val(),
                                NextCouponDate: $('#SplitNextCouponDate').val(),
                                MaturityDate: $('#SplitMaturityDate').val(),
                                InterestPercent: $('#SplitInterestPercent').val(),
                                AccruedInterest: $('#SplitAccruedInterest').val(),
                                FundPK: $('#SplitFundPK').val(),
                                Tenor: $('#SplitTenor').val(),
                                InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                DonePrice: $('#SplitDonePrice').val(),
                                DoneLot: $('#SplitDoneVolume').val() / 100,
                                DoneVolume: $('#SplitDoneVolume').val(),
                                DoneAmount: ($('#SplitDonePrice').val() / 100) * $('#SplitDoneVolume').val(),
                                InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                AcqPrice1: $('#SplitAcqPrice1').val(),
                                AcqDate1: $('#SplitAcqDate1').val(),
                                AcqPrice2: $('#SplitAcqPrice2').val(),
                                AcqDate2: $('#SplitAcqDate2').val(),
                                AcqPrice3: $('#SplitAcqPrice3').val(),
                                AcqDate3: $('#SplitAcqDate3').val(),
                                AcqPrice4: $('#SplitAcqPrice4').val(),
                                AcqDate4: $('#SplitAcqDate4').val(),
                                AcqPrice5: $('#SplitAcqPrice5').val(),
                                AcqDate5: $('#SplitAcqDate5').val(),
                                SettlementMode: $('#SplitSettlementMode').val(),
                                DoneAccruedInterest: $('#SplitDoneAccruedInterest').val(),
                                TotalAmount: kendo.toString(parseFloat((_a * _b) + _c), "##,#"),
                                EntryUsersID: $('#SplitEntryUsersID').val(),
                                ApprovedUsersID: $('#SplitApprovedUsersID').val(),
                                EntryDealingID: $('#SplitEntryDealingID').val(),
                                UpdateDealingID: sessionStorage.getItem("user"),
                                EntryTime: kendo.toString(kendo.parseDate($('#SplitEntryTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                ApprovedTime: kendo.toString(kendo.parseDate($('#SplitApprovedTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                EntryDealingTime: kendo.toString(kendo.parseDate($('#SplitEntryDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                UpdateDealingTime: kendo.toString(kendo.parseDate($('#SplitUpdateDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                ApprovedDealingTime: kendo.toString(kendo.parseDate($('#SplitApprovedDealingTime').val()), 'MM/dd/yyyy HH:mm:ss')

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/InvestmentSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(InvestmentInstructionSplit),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Split Data Success");
                                    winSplit.close();
                                    win.close();
                                    refresh();

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
                else {
                    if ($("#SplitLot").val() == 0) //Amount
                    {
                        var CheckPrice = {
                            ValueDate: $('#DateFrom').val(),
                            InstrumentPK: $('#SplitInstrumentPK').val(),
                            DonePrice1: $('#SplitDonePrice1').val(),
                            DonePrice2: $('#SplitDonePrice2').val(),
                            DonePrice3: $('#SplitDonePrice3').val(),
                            DonePrice4: $('#SplitDonePrice4').val(),
                            DonePrice5: $('#SplitDonePrice5').val(),
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateCheckPriceExposureSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(CheckPrice),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data.Validate == 0) {
                                    var CheckTotalLot = {
                                        DealingPK: $('#SplitDealingPK').val(),
                                        HistoryPK: $('#SplitHistoryPK').val(),
                                        StatusDealing: $('#SplitStatusDealing').val(),
                                        DoneLot1: $('#SplitDoneLot1').val(),
                                        DoneLot2: $('#SplitDoneLot2').val(),
                                        DoneLot3: $('#SplitDoneLot3').val(),
                                        DoneLot4: $('#SplitDoneLot4').val(),
                                        DoneLot5: $('#SplitDoneLot5').val(),
                                        DonePrice1: $('#SplitDonePrice1').val(),
                                        DonePrice2: $('#SplitDonePrice2').val(),
                                        DonePrice3: $('#SplitDonePrice3').val(),
                                        DonePrice4: $('#SplitDonePrice4').val(),
                                        DonePrice5: $('#SplitDonePrice5').val()
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCheckTotalAmountSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(CheckTotalLot),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == true) {
                                                var InvestmentInstruction = {
                                                    PeriodPK: $('#SplitPeriodPK').val(),
                                                    ValueDate: $('#SplitValueDate').val(),
                                                    Reference: $('#SplitReference').val(),
                                                    InstructionDate: $('#SplitInstructionDate').val(),
                                                    InstrumentPK: $('#SplitInstrumentPK').val(),
                                                    InvestmentPK: $('#SplitInvestmentPK').val(),
                                                    DealingPK: $('#SplitDealingPK').val(),
                                                    HistoryPK: $('#SplitHistoryPK').val(),
                                                    StatusInvestment: $('#SplitStatusInvestment').val(),
                                                    StatusDealing: $('#SplitStatusDealing').val(),
                                                    InstrumentTypePK: $('#SplitInstrumentTypePK').val(),
                                                    OrderPrice: $('#SplitOrderPrice').val(),
                                                    RangePrice: $('#SplitRangePrice').val(),
                                                    AcqPrice: $('#SplitAcqPrice').val(),
                                                    Lot: $('#SplitLot').val(),
                                                    LotInShare: $('#SplitLotInShare').val(),
                                                    Volume: $('#SplitVolume').val(),
                                                    Amount: $('#SplitAmount').val(),
                                                    TrxType: $('#SplitTrxType').val(),
                                                    TrxTypeID: $("#SplitTrxTypeID").val(),
                                                    CounterpartPK: $('#SplitCounterpartPK').val(),
                                                    SettledDate: $('#SplitSettledDate').val(),
                                                    AcqDate: $('#SplitAcqDate').val(),
                                                    LastCouponDate: $('#SplitLastCouponDate').val(),
                                                    NextCouponDate: $('#SplitNextCouponDate').val(),
                                                    MaturityDate: $('#SplitMaturityDate').val(),
                                                    InterestPercent: $('#SplitInterestPercent').val(),
                                                    AccruedInterest: $('#SplitAccruedInterest').val(),
                                                    FundPK: $('#SplitFundPK').val(),
                                                    Tenor: $('#SplitTenor').val(),
                                                    InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                                    DonePrice: $('#SplitDonePrice').val(),
                                                    DoneLot: $('#SplitDoneLot').val(),
                                                    DoneVolume: $('#SplitDoneLot').val() * 100,
                                                    DoneAmount: $('#SplitDonePrice').val() * $('#SplitDoneLot').val() * 100,
                                                    DonePrice1: $('#SplitDonePrice1').val(),
                                                    DoneLot1: $('#SplitDoneLot1').val(),
                                                    DoneVolume1: $('#SplitDoneLot1').val() * 100,
                                                    DoneAmount1: $('#SplitDonePrice1').val() * $('#SplitDoneLot1').val() * 100,
                                                    DonePrice2: $('#SplitDonePrice2').val(),
                                                    DoneLot2: $('#SplitDoneLot2').val(),
                                                    DoneVolume2: $('#SplitDoneLot2').val() * 100,
                                                    DoneAmount2: $('#SplitDonePrice2').val() * $('#SplitDoneLot2').val() * 100,
                                                    DonePrice3: $('#SplitDonePrice3').val(),
                                                    DoneLot3: $('#SplitDoneLot3').val(),
                                                    DoneVolume3: $('#SplitDoneLot3').val() * 100,
                                                    DoneAmount3: $('#SplitDonePrice3').val() * $('#SplitDoneLot3').val() * 100,
                                                    DonePrice4: $('#SplitDonePrice4').val(),
                                                    DoneLot4: $('#SplitDoneLot4').val(),
                                                    DoneVolume4: $('#SplitDoneLot4').val() * 100,
                                                    DoneAmount4: $('#SplitDonePrice4').val() * $('#SplitDoneLot4').val() * 100,
                                                    DonePrice5: $('#SplitDonePrice5').val(),
                                                    DoneLot5: $('#SplitDoneLot5').val(),
                                                    DoneVolume5: $('#SplitDoneLot5').val() * 100,
                                                    DoneAmount5: $('#SplitDonePrice5').val() * $('#SplitDoneLot5').val() * 100,
                                                    InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                                    AcqPrice1: $('#SplitAcqPrice1').val(),
                                                    AcqDate1: $('#SplitAcqDate1').val(),
                                                    AcqPrice2: $('#SplitAcqPrice2').val(),
                                                    AcqDate2: $('#SplitAcqDate2').val(),
                                                    AcqPrice3: $('#SplitAcqPrice3').val(),
                                                    AcqDate3: $('#SplitAcqDate3').val(),
                                                    AcqPrice4: $('#SplitAcqPrice4').val(),
                                                    AcqDate4: $('#SplitAcqDate4').val(),
                                                    AcqPrice5: $('#SplitAcqPrice5').val(),
                                                    AcqDate5: $('#SplitAcqDate5').val(),
                                                    BoardType: $('#SplitBoardType').val(),
                                                    EntryUsersID: $('#SplitEntryUsersID').val(),
                                                    ApprovedUsersID: $('#SplitApprovedUsersID').val(),
                                                    EntryDealingID: $('#SplitEntryDealingID').val(),
                                                    UpdateDealingID: sessionStorage.getItem("user"),
                                                    EntryTime: kendo.toString(kendo.parseDate($('#SplitEntryTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    ApprovedTime: kendo.toString(kendo.parseDate($('#SplitApprovedTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    EntryDealingTime: kendo.toString(kendo.parseDate($('#SplitEntryDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    UpdateDealingTime: kendo.toString(kendo.parseDate($('#SplitUpdateDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    ApprovedDealingTime: kendo.toString(kendo.parseDate($('#SplitApprovedDealingTime').val()), 'MM/dd/yyyy HH:mm:ss')

                                                };

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/InvestmentSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(InvestmentInstruction),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        alertify.alert("Split Data Success");
                                                        winSplit.close();
                                                        win.close();
                                                        refresh();

                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            } else {
                                                alertify.alert("Total Amount Must be < = with Total Order Amount")
                                            }

                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                                else {
                                    alertify.alert("Can't Process This Data </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice)
                                    return;
                                }
                            }
                        });
                    }
                    else {
                        if ($('#SplitTrxType').val() == 1) {
                            if (parseFloat($('#SplitDonePrice1').val()) > parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice1').val()) > 0) {
                                alertify.alert("Split Price 1 must be less than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice2').val()) > parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice2').val()) > 0) {
                                alertify.alert("Split Price 2 must be less than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice3').val()) > parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice3').val()) > 0) {
                                alertify.alert("Split Price 3 must be less than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice4').val()) > parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice4').val()) > 0) {
                                alertify.alert("Split Price 4 must be less than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice5').val()) > parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice5').val()) > 0) {
                                alertify.alert("Split Price 5 must be less than Order Price in Investment");
                                return;
                            }
                        }
                        else {
                            if (parseFloat($('#SplitDonePrice1').val()) < parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice1').val() > 0)) {
                                alertify.alert("Split Price 1 must be higher than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice2').val()) < parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice2').val()) > 0) {
                                alertify.alert("Split Price 2 must be higher than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice3').val()) < parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice3').val()) > 0) {
                                alertify.alert("Split Price 3 must be higher than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice4').val()) < parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice4').val()) > 0) {
                                alertify.alert("Split Price 4 must be higher than Order Price in Investment");
                                return;
                            }
                            else if (parseFloat($('#SplitDonePrice5').val()) < parseFloat($('#SplitOrderPrice').val()) && parseFloat($('#SplitDonePrice5').val()) > 0) {
                                alertify.alert("Split Price 5 must be higher than Order Price in Investment");
                                return;
                            }
                        }


                        var CheckPrice = {
                            ValueDate: $('#DateFrom').val(),
                            InstrumentPK: $('#SplitInstrumentPK').val(),
                            DonePrice1: $('#SplitDonePrice1').val(),
                            DonePrice2: $('#SplitDonePrice2').val(),
                            DonePrice3: $('#SplitDonePrice3').val(),
                            DonePrice4: $('#SplitDonePrice4').val(),
                            DonePrice5: $('#SplitDonePrice5').val(),
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateCheckPriceExposureSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(CheckPrice),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data.Validate == 0) {

                                    var CheckTotalLot = {
                                        DealingPK: $('#SplitDealingPK').val(),
                                        HistoryPK: $('#SplitHistoryPK').val(),
                                        StatusDealing: $('#SplitStatusDealing').val(),
                                        DoneLot1: $('#SplitDoneLot1').val(),
                                        DoneLot2: $('#SplitDoneLot2').val(),
                                        DoneLot3: $('#SplitDoneLot3').val(),
                                        DoneLot4: $('#SplitDoneLot4').val(),
                                        DoneLot5: $('#SplitDoneLot5').val(),
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCheckTotalLotSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(CheckTotalLot),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == true) {
                                                var InvestmentInstruction = {
                                                    PeriodPK: $('#SplitPeriodPK').val(),
                                                    ValueDate: $('#SplitValueDate').val(),
                                                    Reference: $('#SplitReference').val(),
                                                    InstructionDate: $('#SplitInstructionDate').val(),
                                                    InstrumentPK: $('#SplitInstrumentPK').val(),
                                                    InvestmentPK: $('#SplitInvestmentPK').val(),
                                                    DealingPK: $('#SplitDealingPK').val(),
                                                    HistoryPK: $('#SplitHistoryPK').val(),
                                                    StatusInvestment: $('#SplitStatusInvestment').val(),
                                                    StatusDealing: $('#SplitStatusDealing').val(),
                                                    InstrumentTypePK: $('#SplitInstrumentTypePK').val(),
                                                    OrderPrice: $('#SplitOrderPrice').val(),
                                                    RangePrice: $('#SplitRangePrice').val(),
                                                    AcqPrice: $('#SplitAcqPrice').val(),
                                                    Lot: $('#SplitLot').val(),
                                                    LotInShare: $('#SplitLotInShare').val(),
                                                    Volume: $('#SplitVolume').val(),
                                                    Amount: $('#SplitAmount').val(),
                                                    TrxType: $('#SplitTrxType').val(),
                                                    TrxTypeID: $("#SplitTrxTypeID").val(),
                                                    CounterpartPK: $('#SplitCounterpartPK').val(),
                                                    SettledDate: $('#SplitSettledDate').val(),
                                                    AcqDate: $('#SplitAcqDate').val(),
                                                    LastCouponDate: $('#SplitLastCouponDate').val(),
                                                    NextCouponDate: $('#SplitNextCouponDate').val(),
                                                    MaturityDate: $('#SplitMaturityDate').val(),
                                                    InterestPercent: $('#SplitInterestPercent').val(),
                                                    AccruedInterest: $('#SplitAccruedInterest').val(),
                                                    FundPK: $('#SplitFundPK').val(),
                                                    Tenor: $('#SplitTenor').val(),
                                                    InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                                    DonePrice: $('#SplitDonePrice').val(),
                                                    DoneLot: $('#SplitDoneLot').val(),
                                                    DoneVolume: $('#SplitDoneLot').val() * 100,
                                                    DoneAmount: $('#SplitDonePrice').val() * $('#SplitDoneLot').val() * 100,
                                                    DonePrice1: $('#SplitDonePrice1').val(),
                                                    DoneLot1: $('#SplitDoneLot1').val(),
                                                    DoneVolume1: $('#SplitDoneLot1').val() * 100,
                                                    DoneAmount1: $('#SplitDonePrice1').val() * $('#SplitDoneLot1').val() * 100,
                                                    DonePrice2: $('#SplitDonePrice2').val(),
                                                    DoneLot2: $('#SplitDoneLot2').val(),
                                                    DoneVolume2: $('#SplitDoneLot2').val() * 100,
                                                    DoneAmount2: $('#SplitDonePrice2').val() * $('#SplitDoneLot2').val() * 100,
                                                    DonePrice3: $('#SplitDonePrice3').val(),
                                                    DoneLot3: $('#SplitDoneLot3').val(),
                                                    DoneVolume3: $('#SplitDoneLot3').val() * 100,
                                                    DoneAmount3: $('#SplitDonePrice3').val() * $('#SplitDoneLot3').val() * 100,
                                                    DonePrice4: $('#SplitDonePrice4').val(),
                                                    DoneLot4: $('#SplitDoneLot4').val(),
                                                    DoneVolume4: $('#SplitDoneLot4').val() * 100,
                                                    DoneAmount4: $('#SplitDonePrice4').val() * $('#SplitDoneLot4').val() * 100,
                                                    DonePrice5: $('#SplitDonePrice5').val(),
                                                    DoneLot5: $('#SplitDoneLot5').val(),
                                                    DoneVolume5: $('#SplitDoneLot5').val() * 100,
                                                    DoneAmount5: $('#SplitDonePrice5').val() * $('#SplitDoneLot5').val() * 100,
                                                    InvestmentNotes: $('#SplitInvestmentNotes').val(),
                                                    AcqPrice1: $('#SplitAcqPrice1').val(),
                                                    AcqDate1: $('#SplitAcqDate1').val(),
                                                    AcqPrice2: $('#SplitAcqPrice2').val(),
                                                    AcqDate2: $('#SplitAcqDate2').val(),
                                                    AcqPrice3: $('#SplitAcqPrice3').val(),
                                                    AcqDate3: $('#SplitAcqDate3').val(),
                                                    AcqPrice4: $('#SplitAcqPrice4').val(),
                                                    AcqDate4: $('#SplitAcqDate4').val(),
                                                    AcqPrice5: $('#SplitAcqPrice5').val(),
                                                    AcqDate5: $('#SplitAcqDate5').val(),
                                                    BoardType: $('#SplitBoardType').val(),
                                                    BankPK: 0,
                                                    BankBranchPK: 0,
                                                    EntryUsersID: $('#SplitEntryUsersID').val(),
                                                    ApprovedUsersID: $('#SplitApprovedUsersID').val(),
                                                    EntryDealingID: $('#SplitEntryDealingID').val(),
                                                    UpdateDealingID: sessionStorage.getItem("user"),
                                                    EntryTime: kendo.toString(kendo.parseDate($('#SplitEntryTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    ApprovedTime: kendo.toString(kendo.parseDate($('#SplitApprovedTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    EntryDealingTime: kendo.toString(kendo.parseDate($('#SplitEntryDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    UpdateDealingTime: kendo.toString(kendo.parseDate($('#SplitUpdateDealingTime').val()), 'MM/dd/yyyy HH:mm:ss'),
                                                    ApprovedDealingTime: kendo.toString(kendo.parseDate($('#SplitApprovedDealingTime').val()), 'MM/dd/yyyy HH:mm:ss')

                                                };

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/InvestmentSplit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(InvestmentInstruction),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        alertify.alert("Split Data Success");
                                                        winSplit.close();
                                                        win.close();
                                                        refresh();

                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                alertify.alert("Total Lot Must be < = with Previous Lot")
                                            }


                                        }
                                    });

                                }
                                else {
                                    alertify.alert("Can't Process This Data </br> Min Price  : " + data.MinPrice + " </br> and Max Price : " + data.MaxPrice)
                                    return;
                                }
                            }
                        });
                    }


                }
            }

        });
    });

    //function _recalTotalAmountBond() {
    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/Investment/RecalTotalAmountBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DonePrice").val() + "/" + $("#DoneVolume").val() + "/" + $("#DoneAccruedInterest").val(),
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            $("#SplitTotalAmount").data("kendoNumericTextBox").value(123);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });
    //}


    function _resetSplitAttributes() {
        $("#LblSplitDonePrice").hide();
        $("#LblSplitDoneLot").hide();
        $("#LblSplitDoneVolume").hide();

    }

    $('#BtnUpdateBrokerEquityBuy').on("click", function () {

        BtnUpdateBrokerEquityClick(1);
    });
    $('#BtnUpdateBrokerEquitySell').on("click", function () {

        BtnUpdateBrokerEquityClick(2);
    });
    function BtnUpdateBrokerEquityClick(_bs) {

        if (_GlobClientCode == "09") {
            GlobParamBoardType = 1;
        }
        else {
            GlobParamBoardType = 0;
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboByCounterpartCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobParamBoardType + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //filter: "contains",
                    suggest: true,
                    change: onChangeCounterpartPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }

       
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BoardType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamBoardType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeParamBoardType
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeParamBoardType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamCounterpartPK").val("");
            $("#ParamCounterpartPK").text("");
            $("#ParamCounterpartPK").data("kendoComboBox").value("");
            $("#ParamCounterpartPK").data("kendoComboBox").text("");
            GlobParamBoardType = this.value();

            $.ajax({
                url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboByCounterpartCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobParamBoardType + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamCounterpartPK").kendoComboBox({
                        dataValueField: "CounterpartPK",
                        dataTextField: "ID",
                        dataSource: data,
                        //filter: "contains",
                        suggest: true,
                        change: onChangeCounterpartPK
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            function onChangeCounterpartPK() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }


            }

        }

       
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboBySettlementMode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamSettlementMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        if (_bs == 1) {
            clearBtnUpdateCounterpart();
            $("#btnEquityBuy").show();
            $("#btnEquitySell").hide();
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }
        else {
            clearBtnUpdateCounterpart();
            $("#btnEquityBuy").hide();
            $("#btnEquitySell").show();
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }

        if (_GlobClientCode == "09") {
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").hide();

        }
        else
        {
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }
        
      
        WinUpdateCounterpart.center();
        WinUpdateCounterpart.open();

    }


    $("#BtnUpdateCounterpartOkEquityBuy").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkEquityBuy");
        var posY = element.offset();

        if (_GlobClientCode == "09") {
            _BoardType = 1;
            _SettlementMode = 1;      
        }
        else {
            _BoardType = $('#ParamBoardType').val();
            _SettlementMode = $('#ParamSettlementMode').val();
        }

        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);
        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else if ($("#ParamSettlementMode").val() == "" && _GlobClientCode != "09") {
                alertify.alert("Please Fill Settlement Mode First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {
                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        var ValidateDealing = {
                            InstrumentTypePK: 1,
                            TrxType: 1,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var ValidateCounterpart = {
                                        InstrumentTypePK: 1,
                                        TrxType: 1,
                                        CounterpartPK: $("#ParamCounterpartPK").val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateFrom').val(),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCounterpartPercentage/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(ValidateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataX) {

                                            var UpdateCounterpart = {
                                                InstrumentTypePK: 1,
                                                TrxType: 1,
                                                FundID: $("#FilterFundID").val(),
                                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateTo').val(),
                                                BoardType: _BoardType,
                                                SettlementMode: _SettlementMode,
                                                UpdateDealingID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(UpdateCounterpart),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (dataX.ValidateAmount == 1) {
                                                        var _msg = " Exposure " + dataX.CounterpartName + " : " + dataX.ExposurePercent + " % and Max Exposure Percent : 30 % </br> Update Broker Success ";
                                                        alertify.alert(_msg).moveTo(posY.left, posY.top);
                                                    }
                                                    else {
                                                        alertify.alert("Update Broker Success").moveTo(posY.left, posY.top);
                                                    }

                                                    WinUpdateCounterpart.close();
                                                    refreshEquityBuy();
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                }
                                            });
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                        }
                                    });
                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top);
            }
        }
        
    });

    $("#BtnUpdateCounterpartCloseEquityBuy").click(function () {
        var element = $("#BtnUpdateCounterpartCloseEquityBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top);
            }
        }).moveTo(posY.left, posY.top);
    });



    $("#BtnUpdateCounterpartOkEquitySell").click(function (e) {
        
        var element = $("#BtnUpdateCounterpartOkEquitySell");
        var posY = element.offset();
       
        if (_GlobClientCode == "09") {
            _BoardType = 1;
            _SettlementMode = 2;
        }
        else {
            _BoardType = $('#ParamBoardType').val();
            _SettlementMode = $('#ParamSettlementMode').val();
        }

        var All = 0;
        All = [];
        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else if ($("#ParamSettlementMode").val() == "" && _GlobClientCode != "09") {
                alertify.alert("Please Fill Settlement Mode First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {
                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        var ValidateDealing = {
                            InstrumentTypePK: 1,
                            TrxType: 2,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var ValidateCounterpart = {
                                        InstrumentTypePK: 1,
                                        TrxType: 2,
                                        CounterpartPK: $("#ParamCounterpartPK").val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateTo').val(),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCounterpartPercentage/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(ValidateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataX) {

                                            var UpdateCounterpart = {
                                                InstrumentTypePK: 1,
                                                TrxType: 2,
                                                FundID: $("#FilterFundID").val(),
                                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateTo').val(),
                                                BoardType: _BoardType,
                                                SettlementMode: _SettlementMode,
                                                UpdateDealingID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(UpdateCounterpart),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (dataX.ValidateAmount == 1) {
                                                        var _msg = " Exposure " + dataX.CounterpartName + " : " + dataX.ExposurePercent + " % and Max Exposure Percent : 30 % </br> Update Broker Success ";
                                                        alertify.alert(_msg).moveTo(posY.left, posY.top - 200);
                                                    }
                                                    else {
                                                        alertify.alert("Update Broker Success").moveTo(posY.left, posY.top - 200);
                                                    }
                                                    
                                                    WinUpdateCounterpart.close();
                                                    refreshEquitySell();
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                }
                                            });

                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                            $.unblockUI();
                                        }
                                    });

                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top - 200);
            }
        }
    });

    $("#BtnUpdateCounterpartCloseEquitySell").click(function () {
        var element = $("#BtnUpdateCounterpartCloseEquitySell");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top - 200);
            }
        }).moveTo(posY.left, posY.top - 200);
    });


    $("#BtnApproveBySelectedEquityBuy").click(function (e) {
        var element = $("#BtnApproveBySelectedEquityBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Please Fill Done Price / Volume First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 9) {
                                alertify.alert("Check Counterpart Commission").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top);
                                        refreshEquityBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnRejectBySelectedEquityBuy").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);


        var element = $("#BtnRejectBySelectedEquityBuy");
        var posY = element.offset();

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top);
                                        refreshEquityBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });


    $("#BtnApproveBySelectedEquitySell").click(function (e) {

        var element = $("#BtnApproveBySelectedEquitySell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Please Fill Done Price / Volume First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top - 200);

                                        refreshEquitySell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
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
            }).moveTo(posY.left, posY.top - 200);
        }

    });

    $("#BtnRejectBySelectedEquitySell").click(function (e) {
        var element = $("#BtnRejectBySelectedEquitySell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshEquitySell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    $("#BtnUnApproveBySelectedEquityBuy").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        var element = $("#BtnUnApproveBySelectedEquityBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
            if (e) {
                var ValidateDealing = {
                    InstrumentTypePK: 1,
                    TrxType: 1,
                    FundID: $("#FilterFundID").val(),
                    CounterpartID: $("#FilterCounterpartID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateFrom').val(),
                    stringInvestmentFrom: stringInvestmentFrom,

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(ValidateDealing),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 2) {
                            alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 3) {
                            alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 4) {
                            alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                            $.unblockUI();
                            return;
                        }
                        else {
                            var Dealing = {
                                InstrumentTypePK: 1,
                                TrxType: 1,
                                FundID: $("#FilterFundID").val(),
                                CounterpartID: $("#FilterCounterpartID").val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateFrom').val(),
                                ApprovedDealingID: sessionStorage.getItem("user"),
                                stringInvestmentFrom: stringInvestmentFrom,

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(Dealing),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top);
                                    refreshEquityBuy();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                }
                            });
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                        $.unblockUI();
                    }
                });
            }
        }).moveTo(posY.left, posY.top);
    });

    $("#BtnUnApproveBySelectedEquitySell").click(function (e) {
        var element = $("#BtnUnApproveBySelectedEquitySell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshEquitySell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    //---- BOND DISINI

    $('#BtnUpdateBrokerBondBuy').on("click", function () {

        BtnUpdateBrokerBondClick(1);
    });
    $('#BtnUpdateBrokerBondSell').on("click", function () {

        BtnUpdateBrokerBondClick(2);
    });
    function BtnUpdateBrokerBondClick(_bs) {


        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //filter: "contains",
                    suggest: true,
                    change: onChangeCounterpartPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboBySettlementMode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamSettlementMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        if (_bs == 1) {
            clearBtnUpdateCounterpart();
            $("#btnBondBuy").show();
            $("#btnBondSell").hide();
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").show();
        }
        else {
            clearBtnUpdateCounterpart();
            $("#btnBondBuy").hide();
            $("#btnBondSell").show();
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").show();

        }

        WinUpdateCounterpart.center();
        WinUpdateCounterpart.open();


    }

    function clearBtnUpdateCounterpart() {
        $("#btnEquityBuy").hide();
        $("#btnEquitySell").hide();
        $("#btnBondBuy").hide();
        $("#btnBondSell").hide();
        $("#btnTimeDepositBuy").hide();
        $("#btnTimeDepositSell").hide();
        $("#btnReksadanaBuy").hide();
        $("#btnReksadanaSell").hide();
        $("#LblParamBoardType").hide();
        $("#LblParamSettlementMode").hide();
    }

    $("#BtnUpdateCounterpartOkBondBuy").click(function (e) {

        var element = $("#BtnUpdateCounterpartOkBondBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else if ($("#ParamSettlementMode").val() == "") {
                alertify.alert("Please Fill Settlement Mode First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {

                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        var ValidateDealing = {
                            InstrumentTypePK: 2,
                            TrxType: 1,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 4) {
                                    alertify.alert("Data Already OPEN").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var UpdateCounterpart = {
                                        InstrumentTypePK: 2,
                                        TrxType: 1,
                                        FundID: $("#FilterFundID").val(),
                                        CounterpartPK: $('#ParamCounterpartPK').val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateTo').val(),
                                        SettlementMode: $('#ParamSettlementMode').val(),
                                        UpdateDealingID: sessionStorage.getItem("user"),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(UpdateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert("Update Broker Success").moveTo(posY.left, posY.top);
                                            WinUpdateCounterpart.close();
                                            refreshBondBuy();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                        }
                                    });
                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top);
            }
        }
    });

    $("#BtnUpdateCounterpartCloseBondBuy").click(function () {
        var element = $("#BtnUpdateCounterpartCloseBondBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top);
            }
        });
    });



    $("#BtnUpdateCounterpartOkBondSell").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkBondSell");
        var posY = element.offset();


        var All = 0;
        All = [];
        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else if ($("#ParamSettlementMode").val() == "") {
                alertify.alert("Please Fill Settlement Mode First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {

                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        // FIFO BOND

                        RecalNetAmountSellBond();

                        var ValidateDealing = {
                            InstrumentTypePK: 2,
                            TrxType: 2,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var UpdateCounterpart = {
                                        InstrumentTypePK: 2,
                                        TrxType: 2,
                                        FundID: $("#FilterFundID").val(),
                                        CounterpartPK: $('#ParamCounterpartPK').val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateTo').val(),
                                        SettlementMode: $('#ParamSettlementMode').val(),
                                        UpdateDealingID: sessionStorage.getItem("user"),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(UpdateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert("Update Broker Success").moveTo(posY.left, posY.top - 200);

                                            WinUpdateCounterpart.close();
                                            refreshBondSell();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                        }
                                    });
                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top - 200);
            }
        }
    });

    $("#BtnUpdateCounterpartCloseBondSell").click(function () {
        var element = $("#BtnUpdateCounterpartCloseBondSell");
        var posY = element.offset();

        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top - 200);
            }
        });
    });


    $("#BtnApproveBySelectedBondBuy").click(function (e) {

        var element = $("#BtnApproveBySelectedBondBuy");
        var posY = element.offset();
        
        var All = 0;
        All = [];
        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealingBond = {
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealingBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealingBond),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                alertify.alert("Please Check Acqusition First Before Matching").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var ValidateDealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    stringInvestmentFrom: stringInvestmentFrom,
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(ValidateDealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == 2) {
                                            alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 3) {
                                            alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 4) {
                                            alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                            return;
                                        }
                                        else if (data == 6) {
                                            alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                            return;
                                        }
                                        else {
                                            var Dealing = {
                                                InstrumentTypePK: 2,
                                                TrxType: 1,
                                                FundID: $("#FilterFundID").val(),
                                                CounterpartID: $("#FilterCounterpartID").val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateFrom').val(),
                                                ApprovedDealingID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(Dealing),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top);
                                                    refreshBondBuy();
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                }
                                            });
                                        }

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                        $.unblockUI();
                                    }
                                });
                            }
                        },

                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnRejectBySelectedBondBuy").click(function (e) {
        var element = $("#BtnRejectBySelectedBondBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top);
                                        refreshBondBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });


    $("#BtnApproveBySelectedBondSell").click(function (e) {

        var element = $("#BtnApproveBySelectedBondSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshBondSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    $("#BtnRejectBySelectedBondSell").click(function (e) {
        var element = $("#BtnRejectBySelectedBondSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshBondSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    $("#BtnUnApproveBySelectedBondBuy").click(function (e) {
        var element = $("#BtnUnApproveBySelectedBondBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top);
                                        refreshBondBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnUnApproveBySelectedBondSell").click(function (e) {
        var element = $("#BtnUnApproveBySelectedBondSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshBondSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });



    //---- DEPOSITO DISINI

    $('#BtnUpdateBrokerTimeDepositBuy').on("click", function () {

        BtnUpdateBrokerTimeDepositClick(1);
    });
    $('#BtnUpdateBrokerTimeDepositSell').on("click", function () {

        BtnUpdateBrokerTimeDepositClick(2);
    });
    function BtnUpdateBrokerTimeDepositClick(_bs) {


        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //filter: "contains",
                    suggest: true,
                    change: onChangeCounterpartPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        if (_bs == 1) {
            clearBtnUpdateCounterpart();
            $("#btnTimeDepositBuy").show();
            $("#btnTimeDepositSell").hide();
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").hide();
        }
        else {
            clearBtnUpdateCounterpart();
            $("#btnTimeDepositBuy").hide();
            $("#btnTimeDepositSell").show();
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").hide();
        }

        WinUpdateCounterpart.center();
        WinUpdateCounterpart.open();

    }

    //function clearBtnUpdateCounterpart() {
    //    $("#btnEquityBuy").hide();
    //    $("#btnEquitySell").hide();
    //    $("#btnTimeDepositBuy").hide();
    //    $("#btnTimeDepositSell").hide();
    //    $("#LblParamBoardType").hide();
    //}

    $("#BtnUpdateCounterpartOkTimeDepositBuy").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkTimeDepositBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
            if (e) {
                var ValidateDealing = {
                    InstrumentTypePK: 5,
                    TrxType: 1,
                    FundID: $("#FilterFundID").val(),
                    CounterpartID: $("#FilterCounterpartID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateTo').val(),

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(ValidateDealing),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 2) {
                            alertify.alert("Data Already Match").moveTo(posY.left, posY.top);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 3) {
                            alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 4) {
                            alertify.alert("Data Already OPEN").moveTo(posY.left, posY.top);
                            $.unblockUI();
                            return;
                        }

                        else {
                            var UpdateCounterpart = {
                                InstrumentTypePK: 5,
                                TrxType: 1,
                                FundID: $("#FilterFundID").val(),
                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateTo').val(),
                                UpdateDealingID: sessionStorage.getItem("user")

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(UpdateCounterpart),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Update Broker Success").moveTo(posY.left, posY.top);
                                    WinUpdateCounterpart.close();
                                    refreshTimeDepositBuy();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                }
                            });
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                        $.unblockUI();
                    }
                });
            }
        }).moveTo(posY.left, posY.top);
    });

    $("#BtnUpdateCounterpartCloseTimeDepositBuy").click(function () {
        var element = $("#BtnUpdateCounterpartCloseTimeDepositBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top);
            }
        });
    });



    $("#BtnUpdateCounterpartOkTimeDepositSell").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkTimeDepositSell");
        var posY = element.offset();
        alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
            if (e) {
                var ValidateDealing = {
                    InstrumentTypePK: 5,
                    TrxType: 2,
                    FundID: $("#FilterFundID").val(),
                    CounterpartID: $("#FilterCounterpartID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateTo').val(),

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(ValidateDealing),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 2) {
                            alertify.alert("Data Already Match").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 3) {
                            alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }

                        else {
                            var UpdateCounterpart = {
                                InstrumentTypePK: 5,
                                TrxType: 2,
                                FundID: $("#FilterFundID").val(),
                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateTo').val(),
                                UpdateDealingID: sessionStorage.getItem("user")

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(UpdateCounterpart),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Update Broker Success").moveTo(posY.left, posY.top - 200);
                                    WinUpdateCounterpart.close();
                                    refreshTimeDepositSell();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                }
                            });
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                        $.unblockUI();
                    }
                });
            }
        }).moveTo(posY.left, posY.top - 200);
    });

    $("#BtnUpdateCounterpartCloseTimeDepositSell").click(function () {
        var element = $("#BtnUpdateCounterpartCloseTimeDepositSell");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update");
            }
        });
    });


    $("#BtnApproveBySelectedTimeDepositBuy").click(function (e) {
        var element = $("#BtnApproveBySelectedTimeDepositBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {

                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top);
                                        refreshTimeDepositBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnRejectBySelectedTimeDepositBuy").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkEquityBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top);
                                        refreshTimeDepositBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });


    $("#BtnApproveBySelectedTimeDepositSell").click(function (e) {
        var element = $("#BtnApproveBySelectedTimeDepositSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top - 200);

                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);

                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top - 200);

                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top - 200);

                                        refreshTimeDepositSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);

                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);

                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }

    });

    $("#BtnRejectBySelectedTimeDepositSell").click(function (e) {
        var element = $("#BtnRejectBySelectedTimeDepositSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshTimeDepositSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    $("#BtnUnApproveBySelectedTimeDepositBuy").click(function (e) {
        var element = $("#BtnUnApproveBySelectedTimeDepositBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top);
                                        refreshTimeDepositBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnUnApproveBySelectedTimeDepositSell").click(function (e) {
        var element = $("#BtnUnApproveBySelectedTimeDepositSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshTimeDepositSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            });
        }
    });


    // --- REKSADANA DISINI

    $('#BtnUpdateBrokerReksadanaBuy').on("click", function () {

        BtnUpdateBrokerReksadanaClick(1);
    });
    $('#BtnUpdateBrokerReksadanaSell').on("click", function () {

        BtnUpdateBrokerReksadanaClick(2);
    });
    function BtnUpdateBrokerReksadanaClick(_bs) {

        if (_GlobClientCode == "09" || _GlobClientCode == "11") {
            GlobParamBoardType = 1;
        }
        else {
            GlobParamBoardType = 0;
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboByCounterpartCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobParamBoardType + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //filter: "contains",
                    suggest: true,
                    change: onChangeCounterpartPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCounterpartPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BoardType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamBoardType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeParamBoardType
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeParamBoardType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamCounterpartPK").val("");
            $("#ParamCounterpartPK").text("");
            $("#ParamCounterpartPK").data("kendoComboBox").value("");
            $("#ParamCounterpartPK").data("kendoComboBox").text("");
            GlobParamBoardType = this.value();

            $.ajax({
                url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboByCounterpartCommission/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobParamBoardType + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamCounterpartPK").kendoComboBox({
                        dataValueField: "CounterpartPK",
                        dataTextField: "ID",
                        dataSource: data,
                        //filter: "contains",
                        suggest: true,
                        change: onChangeCounterpartPK
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            function onChangeCounterpartPK() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }


            }

        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboBySettlementMode/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _bs,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamSettlementMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        if (_bs == 1) {
            clearBtnUpdateCounterpart();
            $("#btnReksadanaBuy").show();
            $("#btnReksadanaSell").hide();
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }
        else {
            clearBtnUpdateCounterpart();
            $("#btnReksadanaBuy").hide();
            $("#btnReksadanaSell").show();
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }

        if (_GlobClientCode == "09" || _GlobClientCode == "11") {
            $("#LblParamBoardType").hide();
            $("#LblParamSettlementMode").hide();

        }
        else {
            $("#LblParamBoardType").show();
            $("#LblParamSettlementMode").show();
        }



        WinUpdateCounterpart.center();
        WinUpdateCounterpart.open();

    }


    $("#BtnUpdateCounterpartOkReksadanaBuy").click(function (e) {
        var element = $("#BtnUpdateCounterpartOkReksadanaBuy");
        var posY = element.offset();

        if (_GlobClientCode == "09") {
            _BoardType = 1;
            _SettlementMode = 1;
        }
        else {
            _BoardType = $('#ParamBoardType').val();
            _SettlementMode = $('#ParamSettlementMode').val();
        }

        var All = 0;
        All = [];
        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {
                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        var ValidateDealing = {
                            InstrumentTypePK: 6,
                            TrxType: 1,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var ValidateCounterpart = {
                                        InstrumentTypePK: 6,
                                        TrxType: 1,
                                        CounterpartPK: $("#ParamCounterpartPK").val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateFrom').val(),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCounterpartPercentage/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(ValidateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataX) {

                                            var UpdateCounterpart = {
                                                InstrumentTypePK: 6,
                                                TrxType: 1,
                                                FundID: $("#FilterFundID").val(),
                                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateTo').val(),
                                                BoardType: _BoardType,
                                                SettlementMode: _SettlementMode,
                                                UpdateDealingID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(UpdateCounterpart),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (dataX.ValidateAmount == 1) {
                                                        var _msg = " Exposure " + dataX.CounterpartName + " : " + dataX.ExposurePercent + " % and Max Exposure Percent : 30 % </br> Update Broker Success ";
                                                        alertify.alert(_msg).moveTo(posY.left, posY.top);
                                                    }
                                                    else {
                                                        alertify.alert("Update Broker Success").moveTo(posY.left, posY.top);
                                                    }

                                                    WinUpdateCounterpart.close();
                                                    refreshReksadanaBuy();
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                }
                                            });
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                            $.unblockUI();
                                        }
                                    });
                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top);
            }
        }
    });

    $("#BtnUpdateCounterpartCloseReksadanaBuy").click(function () {
        var element = $("#BtnUpdateCounterpartCloseReksadanaBuy");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top);
            }
        }).moveTo(posY.left, posY.top);
    });



    $("#BtnUpdateCounterpartOkReksadanaSell").click(function (e) {

        var element = $("#BtnUpdateCounterpartOkReksadanaSell");
        var posY = element.offset();

        if (_GlobClientCode == "09") {
            _BoardType = 1;
            _SettlementMode = 2;
        }
        else {
            _BoardType = $('#ParamBoardType').val();
            _SettlementMode = $('#ParamSettlementMode').val();
        }

        var All = 0;
        All = [];
        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            if ($("#ParamCounterpartPK").val() == "") {
                alertify.alert("Please Fill Counterpart First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else if ($("#ParamSettlementMode").val() == "" && _GlobClientCode != "09") {
                alertify.alert("Please Fill Settlement Mode First !").moveTo(posY.left, posY.top);
                $.unblockUI();
                return;
            }
            else {
                alertify.confirm("Are you sure want to Update Broker by Selected Data ?", function (e) {
                    if (e) {
                        var ValidateDealing = {
                            InstrumentTypePK: 6,
                            TrxType: 2,
                            FundID: $("#FilterFundID").val(),
                            CounterpartID: $("#FilterCounterpartID").val(),
                            DateFrom: $('#DateFrom').val(),
                            DateTo: $('#DateTo').val(),
                            stringInvestmentFrom: stringInvestmentFrom,

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateBrokerBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(ValidateDealing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == 2) {
                                    alertify.alert("Data Already Match").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }
                                else if (data == 3) {
                                    alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                    $.unblockUI();
                                    return;
                                }

                                else {
                                    var ValidateCounterpart = {
                                        InstrumentTypePK: 6,
                                        TrxType: 2,
                                        CounterpartPK: $("#ParamCounterpartPK").val(),
                                        DateFrom: $('#DateFrom').val(),
                                        DateTo: $('#DateTo').val(),
                                        stringInvestmentFrom: stringInvestmentFrom,

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/ValidateCounterpartPercentage/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                        type: 'POST',
                                        data: JSON.stringify(ValidateCounterpart),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataX) {

                                            var UpdateCounterpart = {
                                                InstrumentTypePK: 6,
                                                TrxType: 2,
                                                FundID: $("#FilterFundID").val(),
                                                CounterpartPK: $('#ParamCounterpartPK').val(),
                                                DateFrom: $('#DateFrom').val(),
                                                DateTo: $('#DateTo').val(),
                                                BoardType: _BoardType,
                                                SettlementMode: _SettlementMode,
                                                UpdateDealingID: sessionStorage.getItem("user"),
                                                stringInvestmentFrom: stringInvestmentFrom,

                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/UpdateCounterpartBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                type: 'POST',
                                                data: JSON.stringify(UpdateCounterpart),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (dataX.ValidateAmount == 1) {
                                                        var _msg = " Exposure " + dataX.CounterpartName + " : " + dataX.ExposurePercent + " % and Max Exposure Percent : 30 % </br> Update Broker Success ";
                                                        alertify.alert(_msg).moveTo(posY.left, posY.top - 200);
                                                    }
                                                    else {
                                                        alertify.alert("Update Broker Success").moveTo(posY.left, posY.top - 200);
                                                    }

                                                    WinUpdateCounterpart.close();
                                                    refreshReksadanaSell();
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                                }
                                            });
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                            $.unblockUI();
                                        }
                                    });

                                }

                            },
                            error: function (data) {
                                alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                            }
                        });
                    }
                }).moveTo(posY.left, posY.top - 200);
            }
        }
    });

    $("#BtnUpdateCounterpartCloseReksadanaSell").click(function () {
        var element = $("#BtnUpdateCounterpartCloseReksadanaSell");
        var posY = element.offset();
        alertify.confirm("Are you sure want to cancel Update Counterpart?", function (e) {
            if (e) {
                WinUpdateCounterpart.close();
                alertify.alert("Cancel Update").moveTo(posY.left, posY.top - 200);
            }
        }).moveTo(posY.left, posY.top - 200);
    });


    $("#BtnApproveBySelectedReksadanaBuy").click(function (e) {
        var element = $("#BtnApproveBySelectedReksadanaBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            console.log(data);
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            //else if (data == 4) {
                            //    alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                            //    $.unblockUI();
                            //    return;
                            //}
                            else if (data == 5) {
                                alertify.alert("Please Fill Done Price / Volume First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            //else if (data == 9) {
                            //    alertify.alert("Check Counterpart Commission").moveTo(posY.left, posY.top);
                            //    $.unblockUI();
                            //    return;
                            //}
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top);
                                        refreshReksadanaBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnRejectBySelectedReksadanaBuy").click(function (e) {
        var element = $("#BtnRejectBySelectedReksadanaBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top);
                                        refreshReksadanaBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });


    $("#BtnApproveBySelectedReksadanaSell").click(function (e) {

        var element = $("#BtnApproveBySelectedReksadanaSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Match by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already MATCH").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Please Fill Done Price / Volume First").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 6) {
                                alertify.alert("Data Already PARTIAL").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Match Dealing Success").moveTo(posY.left, posY.top - 200);

                                        refreshReksadanaSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
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
            }).moveTo(posY.left, posY.top - 200);
        }

    });

    $("#BtnRejectBySelectedReksadanaSell").click(function (e) {
        var element = $("#BtnRejectBySelectedReksadanaSell");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else if (data == 4) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Dealing = {
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Dealing Success").moveTo(posY.left, posY.top - 200);
                                        refreshReksadanaSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top - 200);
        }
    });

    $("#BtnUnApproveBySelectedReksadanaBuy").click(function (e) {
        var element = $("#BtnUnApproveBySelectedReksadanaBuy");
        var posY = element.offset();

        var All = 0;
        All = [];
        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
                if (e) {
                    var ValidateDealing = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $("#FilterCounterpartID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateDealing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Dealing = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $("#FilterCounterpartID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedDealingID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Dealing),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top);
                                        refreshReksadanaBuy();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                            $.unblockUI();
                        }
                    });
                }
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnUnApproveBySelectedReksadanaSell").click(function (e) {
        var element = $("#BtnUnApproveBySelectedReksadanaSell");
        var posY = element.offset();
        alertify.confirm("Are you sure want to UnMatch by Selected Data ?", function (e) {
            if (e) {
                var ValidateDealing = {
                    InstrumentTypePK: 6,
                    TrxType: 2,
                    FundID: $("#FilterFundID").val(),
                    CounterpartID: $("#FilterCounterpartID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateFrom').val(),

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(ValidateDealing),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 2) {
                            alertify.alert("Data Already Approved By Settlement, Please Contact Settlement").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 3) {
                            alertify.alert("Data Already Reject").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }
                        else if (data == 4) {
                            alertify.alert("Please Fill Broker First").moveTo(posY.left, posY.top - 200);
                            $.unblockUI();
                            return;
                        }
                        else {
                            var Dealing = {
                                InstrumentTypePK: 6,
                                TrxType: 2,
                                FundID: $("#FilterFundID").val(),
                                CounterpartID: $("#FilterCounterpartID").val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateFrom').val(),
                                ApprovedDealingID: sessionStorage.getItem("user")

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/UnApproveDealingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(Dealing),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("UnMatch Dealing Success").moveTo(posY.left, posY.top - 200);
                                    refreshReksadanaSell();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                                }
                            });
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText).moveTo(posY.left, posY.top - 200);
                        $.unblockUI();
                    }
                });
            }
        }).moveTo(posY.left, posY.top - 200);
    });



    $("#BtnListing").click(function () {

        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
                if (e) {

                    var InvestmentListing = {
                        ParamListDate: $('#DateFrom').val(),
                        ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/DealingListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(InvestmentListing),
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
        }

    });


    $("#BtnCheckDataInvestment").click(function () {
        

            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/CheckDataInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == 1)
                    {
                        alertify.alert("Please Check Data Pending in Investment ");
                    }
                    else
                    {
                        alertify.alert("There's No Data Pending in Investment ")
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });



    });

    function ResetSelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/ResetSelectedInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Dealing",
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


    $("#BtnBrokerFeeRpt").click(function () {
        

        alertify.confirm("Are you sure want to Download Broker Fee data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/BrokerFeeRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
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


    $("#BtnDealingListing").click(function () {
        showDealingListing();
    });


    // Untuk Form Listing

    function showDealingListing(e) {

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

        WinDealingListing.center();
        WinDealingListing.open();

    }

    $("#BtnOkDealingListing").click(function () {

        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var DealingListing = {
                    Message: $('#Message').val(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                    ParamListDate: $('#DateFrom').val(),
                    BitIsMature: $('#BitIsMature').is(":checked"),
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    stringInvestmentFrom: stringInvestmentFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/DealingListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(DealingListing),
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

    $("#BtnCancelDealingListing").click(function () {

        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinDealingListing.close();
                alertify.success("Cancel Listing");
            }
        });
    });


    function onWinDealingListingClose() {
        $("#Message").val("")
    }


    $("#BtnAddAcq1").click(function () {
        if ($("#LblAcqDate1").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
        }


    });

    $("#BtnAddAcq2").click(function () {
        if ($("#LblAcqDate2").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
        }


    });

    $("#BtnAddAcq3").click(function () {
        if ($("#LblAcqDate3").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
        }


    });

    $("#BtnAddAcq4").click(function () {
        if ($("#LblAcqDate4").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
        }


    });

    $("#BtnAddAcq4").click(function () {
        if ($("#LblAcqDate5").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
        }


    });

    $("#BtnAddAcq5").click(function () {
        if ($("#LblAcqDate6").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();

        }

    });

    $("#BtnAddAcq6").click(function () {
        if ($("#LblAcqDate7").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();
            $("#LblAcqPrice6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqVolume6").show();
        }


    });

    $("#BtnAddAcq7").click(function () {
        if ($("#LblAcqDate8").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();
            $("#LblAcqPrice6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqVolume6").show();
            $("#LblAcqPrice7").show();
            $("#LblAcqDate7").show();
            $("#LblAcqVolume7").show();

        }

    });

    $("#BtnAddAcq8").click(function () {
        if ($("#LblAcqDate9").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();
            $("#LblAcqPrice6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqVolume6").show();
            $("#LblAcqPrice7").show();
            $("#LblAcqDate7").show();
            $("#LblAcqVolume7").show();
            $("#LblAcqPrice8").show();
            $("#LblAcqDate8").show();
            $("#LblAcqVolume8").show();
        }

    });

    $("#BtnAddAcq9").click(function () {
        if ($("#LblAcqDate9").is(":visible")) {
            alertify.alert("Can't Add More Acquisition")
        }
        else {
            HideLabelAcq();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcqPrice1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqVolume5").show();
            $("#LblAcqPrice6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqVolume6").show();
            $("#LblAcqPrice7").show();
            $("#LblAcqDate7").show();
            $("#LblAcqVolume7").show();
            $("#LblAcqPrice8").show();
            $("#LblAcqDate8").show();
            $("#LblAcqVolume8").show();
            $("#LblAcqPrice9").show();
            $("#LblAcqDate9").show();
            $("#LblAcqVolume9").show();
        }


    });


    $("#BtnClearAcq1").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#AcqPrice1").data("kendoNumericTextBox").value(0);
        $("#AcqDate1").data("kendoDatePicker").value("");
        $("#AcqVolume1").data("kendoNumericTextBox").value(0);
        $("#AcqPrice2").data("kendoNumericTextBox").value(0);
        $("#AcqDate2").data("kendoDatePicker").value("");
        $("#AcqVolume2").data("kendoNumericTextBox").value(0);
        $("#AcqPrice3").data("kendoNumericTextBox").value(0);
        $("#AcqDate3").data("kendoDatePicker").value("");
        $("#AcqVolume3").data("kendoNumericTextBox").value(0);
        $("#AcqPrice4").data("kendoNumericTextBox").value(0);
        $("#AcqDate4").data("kendoDatePicker").value("");
        $("#AcqVolume4").data("kendoNumericTextBox").value(0);
        $("#AcqPrice5").data("kendoNumericTextBox").value(0);
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").data("kendoNumericTextBox").value(0);
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq2").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#AcqPrice2").data("kendoNumericTextBox").value(0);
        $("#AcqDate2").data("kendoDatePicker").value("");
        $("#AcqVolume2").data("kendoNumericTextBox").value(0);
        $("#AcqPrice3").data("kendoNumericTextBox").value(0);
        $("#AcqDate3").data("kendoDatePicker").value("");
        $("#AcqVolume3").data("kendoNumericTextBox").value(0);
        $("#AcqPrice4").data("kendoNumericTextBox").value(0);
        $("#AcqDate4").data("kendoDatePicker").value("");
        $("#AcqVolume4").data("kendoNumericTextBox").value(0);
        $("#AcqPrice5").data("kendoNumericTextBox").value(0);
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").data("kendoNumericTextBox").value(0);
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq3").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#AcqPrice3").data("kendoNumericTextBox").value(0);
        $("#AcqDate3").data("kendoDatePicker").value("");
        $("#AcqVolume3").data("kendoNumericTextBox").value(0);
        $("#AcqPrice4").data("kendoNumericTextBox").value(0);
        $("#AcqDate4").data("kendoDatePicker").value("");
        $("#AcqVolume4").data("kendoNumericTextBox").value(0);
        $("#AcqPrice5").data("kendoNumericTextBox").value(0);
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").data("kendoNumericTextBox").value(0);
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq4").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#AcqPrice4").data("kendoNumericTextBox").value(0);
        $("#AcqDate4").data("kendoDatePicker").value("");
        $("#AcqVolume4").data("kendoNumericTextBox").value(0);
        $("#AcqPrice5").data("kendoNumericTextBox").value(0);
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").data("kendoNumericTextBox").value(0);
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq5").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#LblAcqPrice4").show();
        $("#LblAcqDate4").show();
        $("#LblAcqVolume4").show();
        $("#AcqPrice5").data("kendoNumericTextBox").value(0);
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").data("kendoNumericTextBox").value(0);
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq6").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#LblAcqPrice4").show();
        $("#LblAcqDate4").show();
        $("#LblAcqVolume4").show();
        $("#LblAcqPrice5").show();
        $("#LblAcqDate5").show();
        $("#LblAcqVolume5").show();
        $("#AcqPrice6").data("kendoNumericTextBox").value(0);
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").data("kendoNumericTextBox").value(0);
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq7").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#LblAcqPrice4").show();
        $("#LblAcqDate4").show();
        $("#LblAcqVolume4").show();
        $("#LblAcqPrice5").show();
        $("#LblAcqDate5").show();
        $("#LblAcqVolume5").show();
        $("#LblAcqPrice6").show();
        $("#LblAcqDate6").show();
        $("#LblAcqVolume6").show();
        $("#AcqPrice7").data("kendoNumericTextBox").value(0);
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").data("kendoNumericTextBox").value(0);
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq8").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#LblAcqPrice4").show();
        $("#LblAcqDate4").show();
        $("#LblAcqVolume4").show();
        $("#LblAcqPrice5").show();
        $("#LblAcqDate5").show();
        $("#LblAcqVolume5").show();
        $("#LblAcqPrice6").show();
        $("#LblAcqDate6").show();
        $("#LblAcqVolume6").show();
        $("#LblAcqPrice7").show();
        $("#LblAcqDate7").show();
        $("#LblAcqVolume7").show();
        $("#AcqPrice8").data("kendoNumericTextBox").value(0);
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").data("kendoNumericTextBox").value(0);
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    $("#BtnClearAcq9").click(function () {
        HideLabelAcq();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcqPrice1").show();
        $("#LblAcqDate1").show();
        $("#LblAcqVolume1").show();
        $("#LblAcqPrice2").show();
        $("#LblAcqDate2").show();
        $("#LblAcqVolume2").show();
        $("#LblAcqPrice3").show();
        $("#LblAcqDate3").show();
        $("#LblAcqVolume3").show();
        $("#LblAcqPrice4").show();
        $("#LblAcqDate4").show();
        $("#LblAcqVolume4").show();
        $("#LblAcqPrice5").show();
        $("#LblAcqDate5").show();
        $("#LblAcqVolume5").show();
        $("#LblAcqPrice6").show();
        $("#LblAcqDate6").show();
        $("#LblAcqVolume6").show();
        $("#LblAcqPrice7").show();
        $("#LblAcqDate7").show();
        $("#LblAcqVolume7").show();
        $("#LblAcqPrice8").show();
        $("#LblAcqDate8").show();
        $("#LblAcqVolume8").show();
        $("#AcqPrice9").data("kendoNumericTextBox").value(0);
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").data("kendoNumericTextBox").value(0);
    });

    function HideLabelAcq() {
        $("#LblAcqPrice").hide();
        $("#LblAcqDate").hide();
        $("#LblAcqVolume").hide();
        $("#LblAcqPrice1").hide();
        $("#LblAcqDate1").hide();
        $("#LblAcqVolume1").hide();
        $("#LblAcqPrice2").hide();
        $("#LblAcqDate2").hide();
        $("#LblAcqVolume2").hide();
        $("#LblAcqPrice3").hide();
        $("#LblAcqDate3").hide();
        $("#LblAcqVolume3").hide();
        $("#LblAcqPrice4").hide();
        $("#LblAcqDate4").hide();
        $("#LblAcqVolume4").hide();
        $("#LblAcqPrice5").hide();
        $("#LblAcqDate5").hide();
        $("#LblAcqVolume5").hide();
        $("#LblAcqPrice6").hide();
        $("#LblAcqDate6").hide();
        $("#LblAcqVolume6").hide();
        $("#LblAcqPrice7").hide();
        $("#LblAcqDate7").hide();
        $("#LblAcqVolume7").hide();
        $("#LblAcqPrice8").hide();
        $("#LblAcqDate8").hide();
        $("#LblAcqVolume8").hide();
        $("#LblAcqPrice9").hide();
        $("#LblAcqDate9").hide();
        $("#LblAcqVolume9").hide();

    }

    function OnChangeAcqPrice() {
            RecalNetAmount();

    }

    function OnChangeAcqVolume() {
            RecalNetAmount();

    }

    function OnChangeAcqDate() {
            RecalNetAmount();

    }

    function RecalNetAmount() {
        if ($('#AcqPrice').val() == null || $('#AcqPrice').val() == 0 || $('#AcqVolume').val() == null || $('#AcqVolume').val() == 0
                || $('#AcqDate').val() == null || $('#AcqDate').val() == 0
                ) {
            return;
        }


        if (GlobInstrumentType == 2 || GlobInstrumentType == 3 || GlobInstrumentType == 8 || GlobInstrumentType == 9 || GlobInstrumentType == 13 || GlobInstrumentType == 14 || GlobInstrumentType == 15) {
            var Bond = {
                DealingPK: $('#DealingPK').val(),
                HistoryPK: $('#HistoryPK').val(),
                InstrumentPK: $('#InstrumentPK').val(),
                TrxType: GlobTrxType,
                InstrumentTypePK: $('#InstrumentTypePK').val(),
                ValueDate: $('#DateFrom').val(),
                SettledDate: $('#SettledDate').val(),
                NextCouponDate: $('#NextCouponDate').val(),
                LastCouponDate: $('#LastCouponDate').val(),
                Price: $('#DonePrice').val(),
                Volume: $('#DoneVolume').val(),
                AcqPrice: $('#AcqPrice').val(),
                AcqDate: $('#AcqDate').val(),
                AcqVolume: $('#AcqVolume').val(),
                AcqPrice1: $('#AcqPrice1').val(),
                AcqDate1: kendo.toString($("#AcqDate1").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume1: $('#AcqVolume1').val(),
                AcqPrice2: $('#AcqPrice2').val(),
                AcqDate2: kendo.toString($("#AcqDate2").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume2: $('#AcqVolume2').val(),
                AcqPrice3: $('#AcqPrice3').val(),
                AcqDate3: kendo.toString($("#AcqDate3").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume3: $('#AcqVolume3').val(),
                AcqPrice4: $('#AcqPrice4').val(),
                AcqDate4: kendo.toString($("#AcqDate4").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume4: $('#AcqVolume4').val(),
                AcqPrice5: $('#AcqPrice5').val(),
                AcqDate5: kendo.toString($("#AcqDate5").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume5: $('#AcqVolume5').val(),
                AcqPrice6: $('#AcqPrice6').val(),
                AcqDate6: kendo.toString($("#AcqDate6").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume6: $('#AcqVolume6').val(),
                AcqPrice7: $('#AcqPrice7').val(),
                AcqDate7: kendo.toString($("#AcqDate7").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume7: $('#AcqVolume7').val(),
                AcqPrice8: $('#AcqPrice8').val(),
                AcqDate8: kendo.toString($("#AcqDate8").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume8: $('#AcqVolume8').val(),
                AcqPrice9: $('#AcqPrice9').val(),
                AcqDate9: kendo.toString($("#AcqDate9").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                AcqVolume9: $('#AcqVolume9').val(),
                TaxCapitaGainPercent: $('#IncomeTaxGainPercent').val(),
                TaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                BitIsRounding: $('#BitIsRounding').val(),

            };

            $.ajax({
                url: window.location.origin + "/Radsoft/OMSBond/GetNetAmount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Bond),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    //$("#DoneAmount").data("kendoNumericTextBox").value(data.GrossAmount);
                    $("#DoneAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                    $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                    $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                    $("#TotalAmount").data("kendoNumericTextBox").value(data.NetAmount);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    }

    function RecalNetAmountSellBondByDealingPK() {
        var Bond = {
            DealingPK: $('#DealingPK').val(),
            IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
            IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
            CounterpartPK: $('#CounterpartPK').val(),
            AcqPrice: $('#AcqPrice').val(),
            DonePrice: $('#DonePrice').val(),

        };
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/GetNetAmountSellBondByDealingPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(Bond),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#DoneAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                $("#TotalAmount").data("kendoNumericTextBox").value(data.NetAmount);

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    }


    function RecalNetAmountSellBond() {

        var All = 0;
        All = [];
        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        var Dealing = {
            CounterpartPK: $("#ParamCounterpartPK").val(),
            DateFrom: $('#DateFrom').val(),
            DateTo: $('#DateTo').val(),
            stringInvestmentFrom: stringInvestmentFrom,

        };


        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/GetNetAmountSellBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamCounterpartPK").val(),
            type: 'POST',
            data: JSON.stringify(Dealing),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#DoneAccruedInterest").val(0);
                $("#IncomeTaxGainAmount").val(0);
                $("#IncomeTaxInterestAmount").val(0);
                $("#TotalAmount").val(0);

                $("#DoneAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                $("#TotalAmount").data("kendoNumericTextBox").value(data.NetAmount);

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    }

    function GetFIFOBond(GlobTrxType) {

        $.ajax({
            url: window.location.origin + "/Radsoft/OMSBond/GetFIFOBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + GlobTrxType,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }

  

    $("#BtnImportOmsEquity").kendoButton({
        imageUrl: "../../Images/Icon/IcImport.png"
    });

    $("#BtnImportOmsEquity").click(function () {
        document.getElementById("FileImportOmsEquity").click();
    });

    $("#FileImportOmsEquity").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportOmsEquity").get(0).files;

        if (files.length > 0) {
            data.append("ImportDealingEquityTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ImportOmsEquity_Import/ " + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportOmsEquity").val("");
                    refresh();

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

    function HideLabel() {
        $("#LblPeriod").hide();
        $("#LblMarket").hide();
        $("#LblCounterpart").hide();
        $("#LblBoardType").hide();
        $("#LblSettlementMode").hide();
        $("#LblSettlementDate").hide();
        $("#LblInstructionDate").hide();
        $("#LblBitIsAmortized").hide();
        $("#LblBitIsRounding").hide();
        $("#LblPriceMode").hide();
        $("#LblInterestPercent").hide();
        $("#LblBreakInterestPercent").hide();
        $("#LblInterestDaysType").hide();
        $("#LblInterestPaymentType").hide();
        $("#LblPaymentModeOnMaturity").hide();
        $("#LblYieldPercent").hide();
        $("#LblLastCouponDate").hide();
        $("#LblNextCouponDate").hide();
        $("#LblMaturityDate").hide();
        $("#LblLotInShare").hide();
        $("#LblIncomeTaxGainPercent").hide();
        $("#LblIncomeTaxInterestPercent").hide();
        $("#LblIncomeTaxSellPercent").hide();
        $("#LblIncomeTaxGainAmount").hide();
        $("#LblIncomeTaxInterestAmount").hide();
        $("#LblIncomeTaxSellAmount").hide();
        $("#LblAccruedHoldingAmount").hide();
        $("#LblTotalAmount").hide();
        $("#LblTenor").hide();
        $("#LblRangePrice").hide();
        $("#LblAccruedInterest").hide();
        $("#LblDoneAccruedInterest").hide();
        $("#LblAcquisition").hide();
        $("#tblAcq").hide();
        $("#LblPrice").hide();
        $("#LblDonePrice").hide();
        $("#LblLot").hide();
        $("#LblVolume").hide();
        $("#LblNominal").hide();
        $("#LblDoneLot").hide();
        $("#LblDoneVolume").hide();
        $("#LblDoneNominal").hide();
        $("#tblOther").hide();
        $("#tblOther1").hide();
        $("#LblInvestmentTrType").hide();
        
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

    function getDataCounterpartExposure() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/Investment/GetDataCounterpartExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                             ID: { type: "string" },
                             Name: { type: "string" },
                             TotalPerBrokerFee: { type: "number" },
                             AllBrokerFee: { type: "number" },
                             Exposure: { type: "number" },
                         },
                         group: {
                             field: "Name", aggregates: [
                             { field: "TotalPerBrokerFee", aggregate: "sum" },
                             { field: "AllBrokerFee", aggregate: "sum" },
                             { field: "Exposure", aggregate: "sum" },
                             ]
                         },
                         aggregate: [{ field: "TotalPerBrokerFee", aggregate: "sum" },
                                 { field: "AllBrokerFee", aggregate: "sum" },
                                 { field: "Exposure", aggregate: "sum" },
                         ]
                     }
                 }
             });
    }

    $("#BtnCheckCounterpartExposure").click(function () {
        var dsCounterpartExposure = getDataCounterpartExposure();
        $("#gridListCounterpartExposure").kendoGrid({
            dataSource: dsCounterpartExposure,
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
              //{ command: { text: "Select", click: ListCounterpartExposureSelect }, title: " ", width: 100 },
                { field: "ID", ID: "", width: 70 },
                { field: "Name", title: "Name", width: 200 },
                {
                field: "TotalPerBrokerFee", title: "Total PerBrokerFee", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                attributes: { style: "text-align:right;" }, headerAttributes: {
                style: "text-align: center"
                }, width: 150
                },
                {
                field: "AllBrokerFee", title: "All BrokerFee", format: "{0:n0}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>",
                attributes: { style: "text-align:right;" }, headerAttributes: {
                style: "text-align: center"
                }, width: 150
                },
                {
                field: "Exposure", title: "Exposure %", format: "{0:n4}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>",
                attributes: { style: "text-align:right;" },
                headerAttributes: {
                style: "text-align: center"
                }, width: 100
                },
            ]
        });

        WinCounterpartExposure.center();
        WinCounterpartExposure.open();
    });

    function onWinCounterpartExposureClose() {
        $("#gridListCounterpartExposure").empty();
    }
    //function ListCounterpartExposureSelect(e) {
    //    var grid = $("#gridListCounterpartExposure").data("kendoGrid");
    //    var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
    //    dirty = true;
    //    $(htmlCountepartExposureID).val(dataItemX.ID);
    //    $(htmlCountepartExposureName).val(dataItemX.Name);
    //    $(htmlTotalPerBrokerFee).val(dataItemX.TotalPerBrokerFee);
    //    $(htmlAllBrokerFee).val(dataItemX.AllBrokerFee);
    //    $(htmlExposure).val(dataItemX.Exposure);

    //    WinCounterpartExposure.close();


    //}

    function UpdateRecalTotalAmount() {
        if ($("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value() == 0) {
            $("#IncomeTaxInterestPercent").data("kendoNumericTextBox").value(0);
        }
        if ($("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() == 0) {
            $("#IncomeTaxGainPercent").data("kendoNumericTextBox").value(0);
        }
        $("#TotalAmount").data("kendoNumericTextBox").value($("#DoneAmount").data("kendoNumericTextBox").value() + $("#DoneAccruedInterest").data("kendoNumericTextBox").value() - $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() - $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value())
    }

    $("#BtnPreview").click(function () {
        var date = new Date();
        alertify.confirm("Are you sure to Preview File ?", function (e) {
            if (e) {
                $.blockUI({});
                var EmailToClient = {
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/GenerateReportBroker/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(EmailToClient),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data;
                        var newwindow = window.open(data, '_blank');

                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnGetAvgByTrx").click(function () {

        var All = 0;
        All = [];
        for (var i in checkedEquityBuy) {
            if (checkedEquityBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedEquitySell) {
            if (checkedEquitySell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondBuy) {
            if (checkedBondBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedBondSell) {
            if (checkedBondSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoBuy) {
            if (checkedDepositoBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedDepositoSell) {
            if (checkedDepositoSell[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaBuy) {
            if (checkedReksadanaBuy[i]) {
                All.push(i);
            }
        }

        for (var i in checkedReksadanaSell) {
            if (checkedReksadanaSell[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        console.log(stringInvestmentFrom);

        alertify.confirm("Are you sure want to Get Avg Price by Trx ?", function (e) {
            if (e) {
                var ValidateAvgPrice = {
                    stringInvestmentFrom: stringInvestmentFrom,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/ValidateGetAvgPriceByTrx/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ValidateAvgPrice),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == null)
                        {
                            alertify.alert("There's no data selected !");
                        }
                        else if (data.Result == false) {
                            var AvgPrice = {
                                Date: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                FundPK: data.FundPK,
                                CounterpartPK: data.CounterpartPK,
                                InstrumentPK: data.InstrumentPK,
                                InstrumentID: data.InstrumentID,
                                stringInvestmentFrom: stringInvestmentFrom,
                            };
                            var _InstrumentID = data.InstrumentID;
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/GetAvgPriceByTrx/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AvgPrice),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Avg Price  " + _InstrumentID + " : " + kendo.toString(data, 'n8'));
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
                        else {
                            alertify.alert("Get AvgPrice Cancel, Data Selected More than 1 instrument !");
                        }
   
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });

    });
    

    $("#BtnShow").click(function () {
        GridDetailDealing();
        $('#gridDetailPerInstrument').show();
    });


    $("#BtnGetPerInstrument").click(function () {
        $('#gridDetailPerInstrument').hide();
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeInstrument
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeInstrument() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        WinDetailPerInstrument.center();
        WinDetailPerInstrument.open();
    });


    function getDataSourceByInstrument(_url) {
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
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             TotalOrderPrice: { type: "number" },

                         }
                     }
                 },
                 aggregate: [{ field: "TotalOrderPrice", aggregate: "sum" }
                 ]
             });
    }
    
    function GridDetailDealing() {
        $("#gridDetailPerInstrument").empty();
        var AgentFeeURL = window.location.origin + "/Radsoft/Investment/GetDataDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UInstrumentPK').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
          dataSourceApproved = getDataSourceByInstrument(AgentFeeURL);

        var gridDetail = $("#gridDetailPerInstrument").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Detail Dealing Per Instrument"
                }
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [

                 { field: "InstrumentID", title: "Instrument ID" },
                 { field: "InstrumentName", title: "Instrument Name" },
                 { field: "TotalOrderPrice", title: "Total Order Price", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
            ]
        }).data("kendoGrid");

    }


    // investment ACQ

    function onWinListAcqClose() {
        $("#gridInvestmentAcq").empty();
    }

    $("#BtnShowInvestmentAcq").click(function () {
        var dsInvesmentAcq = getDataInvesmentAcq();
        $("#gridInvestmentAcq").empty();
        var grid = $("#gridInvestmentAcq").kendoGrid({
            dataSource: dsInvesmentAcq,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            editable: "incell",
            toolbar: kendo.template($("#gridInvestmentAcq").html()),
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
            columns: [
                { field: "InvestmentPK", title: "InvestmentPK", hidden: true, width: 50 },
                { field: "AcqNo", title: "AcqNo", width: 50 },
                {
                    field: "AcqVolume", title: "Acq Volume", width: 200, format: "{0:n4}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
                {
                    field: "AcqPrice", title: "Acq Price", width: 150, format: "{0:n6}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
                { field: "AcqDate", title: "Acq Date", width: 100, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#"  },
                {
                    field: "IncomeTaxGainAmount", title: "Capital Gain", width: 200, format: "{0:n6}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
                { field: "DaysOfHoldingInterest", title: "Holding Days", width: 100 },
                {
                    field: "IncomeTaxInterestAmount", title: "Holding Interest Amount", width: 150, format: "{0:n6}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
                {
                    field: "TotalTaxIncomeAmount", title: "Total Taxable Income", width: 200, format: "{0:n6}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
                {
                    field: "TaxAmount", title: "Total Tax Amount", width: 200, format: "{0:n6}"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
                },
            ]
        }).data("kendoGrid");

        WinListAcq.center();
        WinListAcq.open();
    });

    function getDataInvesmentAcq() {
        return new kendo.data.DataSource(
            {
                transport:
                {

                    read:
                    {

                        url: window.location.origin + "/Radsoft/Investment/GetDataInvestmentAcq/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DealingPK").val(),
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                batch: true,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 10,
                schema: {
                    model: {
                        fields: {
                            Header: { type: "string", editable: false },
                            ID: { type: "string", editable: false },
                            Name: { type: "string", editable: false },
                            ParentName: { type: "string", editable: false },
                            AcqDate: { editable: false },
                            AcqVolume: { editable: false },
                            AcqPrice: { editable: false },
                            AcqNo: { editable: false },
                            BitIsChange: { type: "boolean" },
                            BKBalance: { type: "number" },
                            ZeroAmount: { type: "boolean" }
                        }
                    }
                }
            });
    }


    $("#BtnUpdateInvestmentAcq").click(function () {
        var _InvesmentAcq = [];
        var gridDataArray = $('#gridInvestmentAcq').data('kendoGrid')._data;
        var flagChange = 0;
        for (var index = 0; index < gridDataArray.length; index++) {
            if (gridDataArray[index]["AcqNo"] != null) {

                var _m = {
                    InvestmentPK: gridDataArray[index]["InvestmentPK"],
                    AcqNo: gridDataArray[index]["AcqNo"],
                    AcqVolume: gridDataArray[index]["AcqVolume"],
                    AcqPrice: gridDataArray[index]["AcqPrice"],
                    AcqDate: gridDataArray[index]["AcqDate"],
                    IncomeTaxGainAmount: gridDataArray[index]["IncomeTaxGainAmount"],
                    DaysOfHoldingInterest: gridDataArray[index]["DaysOfHoldingInterest"],
                    IncomeTaxInterestAmount: gridDataArray[index]["IncomeTaxInterestAmount"],
                    TotalTaxIncomeAmount: gridDataArray[index]["TotalTaxIncomeAmount"],
                    TaxAmount: gridDataArray[index]["TaxAmount"]
                }
                _InvesmentAcq.push(_m);
                flagChange = 1
            }

        };
        if (flagChange == 0) {
            alertify.alert('No Data Investment Acquisition');
            return;
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/ValidateUpdateInvestmentAcq/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DealingPK").val(),
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 0) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Investment_UpdateInvestmentAcq/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(_InvesmentAcq),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            RefreshInvestmentAcq();
                            alertify.alert(data);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                else {
                    alertify.alert("Data already Settled!")
                    return;
                }
            }
        });

        


    });

    function RefreshInvestmentAcq() {
        var newDS = getDataInvesmentAcq();
        $("#gridInvestmentAcq").data("kendoGrid").setDataSource(newDS);
    }

    $("#BtnAllKertasKerja").click(function () {
        showWinAllKertasKerja();
    });

    function showWinAllKertasKerja(e) {
        $("#LblParamKertasKerjaBitIsMature").hide();

        $("#KertasKerjaBy").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Saham", value: '1' },
                { text: "Obligasi", value: '2' },
                { text: "Time Deposit", value: '3' },

            ],
            filter: "contains",
            suggest: true,
            change: OnChangeKertasKerjaBy,
            index: 0
        });


        function OnChangeKertasKerjaBy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }



        WinAllKertasKerja.center();
        WinAllKertasKerja.open();

    }

    $("#BtnOkAllKertasKerja").click(function () {


        alertify.confirm("Are you sure want to Download Kertas Kerja ?", function (e) {
            if (e) {
                if ($("#KertasKerjaBy").val() == 1) {
                    console.log($("#KertasKerjaBy").val())
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

                    //for (var i in checkedBondBuy) {
                    //    if (checkedBondBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedBondSell) {
                    //    if (checkedBondSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoBuy) {
                    //    if (checkedDepositoBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoSell) {
                    //    if (checkedDepositoSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaBuy) {
                    //    if (checkedReksadanaBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaSell) {
                    //    if (checkedReksadanaSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //var ArrayInvestmentFrom = All;
                    //var stringInvestmentFrom = '';

                    //for (var i in ArrayInvestmentFrom) {
                    //    stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                    //}
                    //stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

                    //console.log(stringInvestmentFrom);



                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),
                        //BitIsMature: $('#BitIsMature').is(":checked"),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        //stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaDealingSaham/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.error(data.responseText);
                        }

                    });



                }
                else if ($("#KertasKerjaBy").val() == 2) {

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

                    //for (var i in checkedBondBuy) {
                    //    if (checkedBondBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedBondSell) {
                    //    if (checkedBondSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoBuy) {
                    //    if (checkedDepositoBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoSell) {
                    //    if (checkedDepositoSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaBuy) {
                    //    if (checkedReksadanaBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaSell) {
                    //    if (checkedReksadanaSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //var ArrayInvestmentFrom = All;
                    //var stringInvestmentFrom = '';

                    //for (var i in ArrayInvestmentFrom) {
                    //    stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                    //}
                    //stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

                    //console.log(stringInvestmentFrom);

                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),
                        //BitIsMature: $('#BitIsMature').is(":checked"),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        //stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaDealingObligasi/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.error(data.responseText);
                        }

                    });
                }
                else if ($("#KertasKerjaBy").val() == 3) {

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

                    //for (var i in checkedBondBuy) {
                    //    if (checkedBondBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedBondSell) {
                    //    if (checkedBondSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoBuy) {
                    //    if (checkedDepositoBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedDepositoSell) {
                    //    if (checkedDepositoSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaBuy) {
                    //    if (checkedReksadanaBuy[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //for (var i in checkedReksadanaSell) {
                    //    if (checkedReksadanaSell[i]) {
                    //        All.push(i);
                    //    }
                    //}

                    //var ArrayInvestmentFrom = All;
                    //var stringInvestmentFrom = '';

                    //for (var i in ArrayInvestmentFrom) {
                    //    stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                    //}
                    //stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

                    //console.log(stringInvestmentFrom);

                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),
                        //BitIsMature: $('#BitIsMature').is(":checked"),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        //stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaDealingTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.error(data.responseText);
                        }

                    });
                }



                //else {
                //    var All = 0;
                //    All = [];

                //    for (var i in checkedBondBuy) {
                //        if (checkedBondBuy[i]) {
                //            All.push(i);
                //        }
                //    }

                //    for (var i in checkedBondSell) {
                //        if (checkedBondSell[i]) {
                //            All.push(i);
                //        }
                //    }


                //    var ArrayInvestmentFrom = All;
                //    var stringInvestmentFrom = '';

                //    for (var i in ArrayInvestmentFrom) {
                //        stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                //    }
                //    stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

                //    //console.log(stringInvestmentFrom);

                //    if (stringInvestmentFrom == "") {
                //        alertify.alert("There's No Selected Data");
                //    }
                //    else {
                //        var InvestmentListing = {
                //            stringInvestmentFrom: stringInvestmentFrom,
                //        };
                //        $.ajax({
                //            url: window.location.origin + "/Radsoft/Investment/KertasKerjaByOverseasBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                //            type: 'POST',
                //            data: JSON.stringify(InvestmentListing),
                //            contentType: "application/json;charset=utf-8",
                //            success: function (data) {
                //                $("#downloadFileRadsoft").attr("href", data);
                //                $("#downloadFileRadsoft").attr("download", "Radsoft_KertasKerjaByOverseasBond.txt");
                //                document.getElementById("downloadFileRadsoft").click();
                //                // var newwindow = window.open(data, '_blank');
                //                //window.location = data
                //            },
                //            error: function (data) {
                //                alertify.alert(data.responseText);
                //            }
                //        });
                //    }

                //}
            }
        });

    });

    $("#BtnCancelAllKertasKerja").click(function () {

        alertify.confirm("Are you sure want to cancel KertasKerja?", function (e) {
            if (e) {
                WinAllKertasKerja.close();
                alertify.alert("Cancel KertasKerja");
            }
        });
    });

    function onWinAllKertasKerjaClose() {
        $("#Message").val("");
        $("#KertasKerjaBy").val(1);
    }

});
