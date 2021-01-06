$(document).ready(function () {
    document.title = 'FORM SETTLEMENT INSTRUCTION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinInvestmentListing;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var WinListCounterpart;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobInstrumentType;
    var checkedBondBuy = {};
    var checkedBondSell = {};
    var checkedEquityBuy = {};
    var checkedEquitySell = {};
    var checkedDepositoBuy = {};
    var checkedDepositoSell = {};
    var checkedReksadanaBuy = {};
    var checkedReksadanaSell = {};
    var checkedAmmendDeposito = {};
    var checkedApproved = {};

    //1
    initButton();
    //2
    initWindow();
    //3
    ResetSelected();

    if (_GlobClientCode == "20") {

        $("#BtnAllKertasKerja").show();
    }
    else {
        $("#BtnAllKertasKerja").hide();
    }

    function initButton() {
        $("#BtnCancel").kendoButton({
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

        $("#BtnListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnListingEquity").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnOkListingEquity").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelListingEquity").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnListingBond").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnOkListingBond").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelListingBond").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnListingTimeDeposit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnOkListingTimeDeposit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelListingTimeDeposit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
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

        $("#BtnApproveBySelectedEquityBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedEquityBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedEquityBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnApproveBySelectedEquitySell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedEquitySell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedEquitySell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnApproveBySelectedBondBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedBondBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedBondBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnApproveBySelectedBondSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedBondSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedBondSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnApproveBySelectedReksadanaBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedReksadanaBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedReksadanaBuy").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnApproveBySelectedReksadanaSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelectedReksadanaSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelectedReksadanaSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnAmortize").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });


        $("#BtnNetting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnAllPTP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOkAllPTP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelAllPTP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnAllListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnOkAllListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelAllListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnSettlementListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnImportSIDeposito").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
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
        $("#BtnUpdateDepositoMature").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });
        $("#BtnAmmendMatureDeposito").kendoButton({

        });

    }

    var splitter = $("#splitter").kendoSplitter({
        orientation: "vertical",
        panes: [
            { collapsible: true, size: "850px" },
            { collapsible: true, size: "810px" },
            { collapsible: true, size: "430px" },
            { collapsible: true, size: "430px" },
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

    function RefreshNetBuySellSettlementEquity() {
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
            url: window.location.origin + "/Radsoft/OMSEquity/OMSEquity_GetSettlementNetBuySellEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellSettlementEquity').val(data);
                $('#NetBuySellSettlementEquity').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellSettlementBond() {
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
            url: window.location.origin + "/Radsoft/OMSBond/OMSBond_GetSettlementNetBuySellBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellSettlementBond').val(data);
                $('#NetBuySellSettlementBond').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellSettlementTimeDeposit() {
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
            url: window.location.origin + "/Radsoft/OMSTimeDeposit/OMSTimeDeposit_GetSettlementNetBuySellTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellSettlementTimeDeposit').val(data);
                $('#NetBuySellSettlementTimeDeposit').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RefreshNetBuySellSettlementReksadana() {
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
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_GetSettlementNetBuySellReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _counterpartID,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $('#NetBuySellSettlementReksadana').val(data);
                $('#NetBuySellSettlementReksadana').text(kendo.toString(data, "n"));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function initWindow() {

        $("#ParamListDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            change: onChangeParamListDate,
        });

        function onChangeParamListDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            //$("#ParamReferenceFrom").data("kendoComboBox").text("");
            //$("#ParamReferenceTo").data("kendoComboBox").text("");
            //GetReferenceFromSettlement();
            //GetMessageFromInvestmentNotes();
        }

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
            refresh();
        }

        $("#LastCouponDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            change: OnChangeLastCouponDate,
        });

        $("#NextCouponDate").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });

        $("#MaturityDate").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });

        $("#AcqDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            change: onChangeAcqDate,
        });

        function onChangeAcqDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            RecalNetAmount();
        }

        $("#AcqDate1").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate2").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate3").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate4").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate5").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });

        $("#AcqDate6").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate7").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate8").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });
        $("#AcqDate9").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });

        $("#SettledDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            change: OnChangeSettledDate
        });

        $("#BtnCheckDataDealing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        function OnChangeSettledDate() {
            if ($("#SettledDate").data("kendoDatePicker").value() != null
                && $("#SettledDate").data("kendoDatePicker").value() != ''
                && $("#LastCouponDate").data("kendoDatePicker").value() != ''
                && $("#LastCouponDate").data("kendoDatePicker").value() != null
                && $('#InstrumentPK').val() != ''
                && $('#InstrumentPK').val() != null
                && $("#DonePrice").data("kendoNumericTextBox").value() != ''
                && $("#DonePrice").data("kendoNumericTextBox").value() != null
                && $("#DoneVolume").data("kendoNumericTextBox").value() != ''
                && $("#DoneVolume").data("kendoNumericTextBox").value() != null
            ) {
                RecalNetAmount();
                //GetBondInterest();
            }

        }

        function OnChangeLastCouponDate() {
            //if ($("#LastCouponDate").data("kendoDatePicker").value() != null) {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Instrument/GetNextCouponDateAndTenor/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentTypePK").val() + "/" + $("#InstrumentPK").val(),
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

        $("#InstructionDate").kendoDatePicker({
            format: "dd/MMM/yyyy",

        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            change: OnChangeDateTo,
            value: new Date(),
        });
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
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

                //Cek 
                Days
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


        InitgridSettlementInstructionEquityBuyOnly();
        InitgridSettlementInstructionEquitySellOnly();
        InitgridSettlementInstructionBondBuyOnly();
        InitgridSettlementInstructionBondSellOnly();
        InitgridSettlementInstructionTimeDepositBuyOnly();
        InitgridSettlementInstructionTimeDepositSellOnly();
        RefreshNetBuySellSettlementEquity();
        RefreshNetBuySellSettlementBond();
        InitgridSettlementInstructionReksadanaBuyOnly();
        InitgridSettlementInstructionReksadanaSellOnly();


        if (_GlobClientCode == "09" || _GlobClientCode == "12" || _GlobClientCode == "19") {
            $("#lblSignature").show();
            $("#lblPosition").show();
        }
        else {
            $("#lblSignature").show();
            $("#lblPosition").hide();
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

        win = $("#WinSettlementInstruction").kendoWindow({
            height: 2000,
            title: "Settlement Instruction Detail",
            visible: false,
            width: 1400,
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

        //WinListCounterpart = $("#WinListCounterpart").kendoWindow({
        //    height: 500,
        //    title: "Counterpart List",
        //    visible: false,
        //    width: 900,
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    close: onWinListCounterpartClose
        //}).data("kendoWindow");

        //WinSettlementListing = $("#WinSettlementListing").kendoWindow({
        //    height: 400,
        //    title: "* Listing Settlement",
        //    visible: false,
        //    width: 900,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    modal: true,
        //    close: onWinSettlementListingClose
        //}).data("kendoWindow");


        WinSettlementListing = $("#WinSettlementListing").kendoWindow({
            height: 600,
            title: "* Listing Settlement",
            visible: false,
            width: 900,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinSettlementListingClose
        }).data("kendoWindow");

        WinAllListing = $("#WinAllListing").kendoWindow({
            height: 200,
            title: "* Listing Settlement By",
            visible: false,
            width: 300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinAllPTP = $("#WinAllPTP").kendoWindow({
            height: 600,
            title: "* PTP By",
            visible: false,
            width: 1300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinAllPTPClose
        }).data("kendoWindow");


        WinSettlementListingEquity = $("#WinSettlementListingEquity").kendoWindow({
            height: 150,
            title: "* Listing Settlement Equity",
            visible: false,
            width: 300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinSettlementListingBond = $("#WinSettlementListingBond").kendoWindow({
            height: 150,
            title: "* Listing Settlement Bond",
            visible: false,
            width: 300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinSettlementListingTimeDeposit = $("#WinSettlementListingTimeDeposit").kendoWindow({
            height: 150,
            title: "* Listing Settlement TimeDeposit",
            visible: false,
            width: 300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

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

        WinSettlementAmmendMatureDeposito = $("#WinSettlementAmmendMatureDeposito").kendoWindow({
            height: 500,
            title: "* Deposito Mature",
            visible: false,
            width: 1150,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinWinSettlementAmmendMatureDepositoClose
        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinSettlementInstruction").kendoValidator().data("kendoValidator");

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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid");
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
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                _showdetails(dataItemX);
            }
        }
    }

    function _showdetails(dataItemX) {


        if (dataItemX.StatusSettlement == 1) {
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

        if (dataItemX.StatusSettlement == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
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

        if (dataItemX.StatusSettlement == 2 && dataItemX.Revised == 1) {
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

        if (dataItemX.StatusSettlement == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
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
        if (dataItemX.StatusSettlement == 3) {
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
        if (dataItemX.StatusSettlement == 4) {
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
        $("#OrderStatus").val(dataItemX.OrderStatus);
        $("#SettlementPK").val(dataItemX.SettlementPK);
        $("#StatusInvestment").val(dataItemX.StatusInvestment);
        $("#StatusSettlement").val(dataItemX.StatusSettlement);
        GlobStatusSettlement = dataItemX.StatusSettlement;
        $("#HistoryPK").val(dataItemX.HistoryPK);
        $("#Notes").val(dataItemX.Notes);
        $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
        $("#InstructionDate").data("kendoDatePicker").value(dataItemX.InstructionDate);
        $("#Reference").val(dataItemX.Reference);
        $("#SIReference").val(dataItemX.SIReference);
        GlobInstrumentType = dataItemX.InstrumentTypePK;;
        $("#CounterpartPK").val(dataItemX.CounterpartPK);
        $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
        $("#InstrumentPK").val(dataItemX.InstrumentPK);
        $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
        $("#BankBranchPK").val(dataItemX.BankBranchPK);
        $("#RangePrice").val(dataItemX.RangePrice);
        $("#AmountToTransfer").val(dataItemX.AmountToTransfer);
        $("#SettledDate").data("kendoDatePicker").value(dataItemX.SettledDate);
        $("#LastCouponDate").data("kendoDatePicker").value(dataItemX.LastCouponDate);
        $("#NextCouponDate").data("kendoDatePicker").value(dataItemX.NextCouponDate);
        $("#MaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
        $("#AcqDate").data("kendoDatePicker").value(dataItemX.AcqDate);
        $("#AcqDate1").data("kendoDatePicker").value(dataItemX.AcqDate1);
        $("#AcqDate2").data("kendoDatePicker").value(dataItemX.AcqDate2);
        $("#AcqDate3").data("kendoDatePicker").value(dataItemX.AcqDate3);
        $("#AcqDate4").data("kendoDatePicker").value(dataItemX.AcqDate4);
        $("#AcqDate5").data("kendoDatePicker").value(dataItemX.AcqDate5);
        $("#CommissionPercent").val(dataItemX.CommissionPercent);
        $("#LevyPercent").val(dataItemX.LevyPercent);
        $("#KPEIPercent").val(dataItemX.KPEIPercent);
        $("#VATPercent").val(dataItemX.VATPercent);
        $("#WHTPercent").val(dataItemX.WHTPercent);
        $("#OTCPercent").val(dataItemX.OTCPercent);
        $("#IncomeTaxSellPercent").val(dataItemX.IncomeTaxSellPercent);
        $("#IncomeTaxInterestPercent").val(dataItemX.IncomeTaxInterestPercent);
        $("#IncomeTaxGainPercent").val(dataItemX.IncomeTaxGainPercent);
        $("#CommissionAmount").val(dataItemX.CommissionAmount);
        $("#LevyAmount").val(dataItemX.LevyAmount);
        $("#KPEIAmount").val(dataItemX.KPEIAmount);
        $("#VATAmount").val(dataItemX.VATAmount);
        $("#WHTAmount").val(dataItemX.WHTAmount);
        $("#OTCAmount").val(dataItemX.OTCAmount);
        $("#IncomeTaxSellAmount").val(dataItemX.IncomeTaxSellAmount);
        $("#IncomeTaxInterestAmount").val(dataItemX.IncomeTaxInterestAmount);
        $("#IncomeTaxGainAmount").val(dataItemX.IncomeTaxGainAmount);
        $("#InvestmentNotes").val(dataItemX.InvestmentNotes);
        $("#LotInShare").val(dataItemX.LotInShare);
        $("#Category").val(dataItemX.Category);
        $("#CrossFundFromPK").val(dataItemX.CrossFundFromPK);
        $("#BitBreakable").val(dataItemX.BitBreakable);
        $("#BitForeignTrx").prop('checked', dataItemX.BitForeignTrx);
        $("#CPSafekeepingAccNumber").val(dataItemX.CPSafekeepingAccNumber);
        $("#PlaceOfSettlement").val(dataItemX.PlaceOfSettlement);
        $("#FundSafekeepingAccountNumber").val(dataItemX.FundSafekeepingAccountNumber);
        $("#EntrySettlementID").val(dataItemX.EntrySettlementID);
        $("#UpdateSettlementID").val(dataItemX.UpdateSettlementID);
        $("#ApprovedSettlementID").val(dataItemX.ApprovedSettlementID);
        $("#VoidSettlementID").val(dataItemX.VoidSettlementID);
        $("#EntrySettlementTime").val(kendo.toString(kendo.parseDate(dataItemX.EntrySettlementTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#UpdateSettlementTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateSettlementTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#ApprovedSettlementTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedSettlementTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#VoidSettlementTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidSettlementTime), 'MM/dd/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        $("#OtherInvestmentStyle").val(dataItemX.OtherInvestmentStyle);
        $("#OtherInvestmentObjective").val(dataItemX.OtherInvestmentObjective);
        $("#OtherRevision").val(dataItemX.OtherRevision);


        //BIRate  
        $.ajax({
            url: window.location.origin + "/Radsoft/BenchmarkIndex/GetBenchmarkIndexCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BIRate").kendoComboBox({
                    dataValueField: "BenchmarkIndexPK",
                    dataTextField: "CloseInd",
                    dataSource: data,
                    filter: "contains",
                    //suggest: true,
                    //index: 0,
                    enable: false,
                    change: onChangeBIRate,
                    value: setUpBIRate()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setUpBIRate() {
            if (dataItemX.BIRate == null) {
                return 0;
            } else {
                if (dataItemX.BIRate == 0) {
                    return 0;
                } else {
                    return dataItemX.BIRate;
                }
            }
        }

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
                    //suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeInvestmentStrategy,
                    value: setUpInvestmentStrategy()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setUpInvestmentStrategy() {
            if (dataItemX.InvestmentStrategy == null) {
                return 0;
            } else {
                if (dataItemX.InvestmentStrategy == 0) {
                    return 0;
                } else {
                    return dataItemX.InvestmentStrategy;
                }
            }
        }

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
                    enable: false,
                    dataSource: data,
                    change: onChangeInvestmentStyle,
                    value: setInvestmentStyle()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setInvestmentStyle() {
            if (dataItemX == null) {
                return 0;
            } else {
                if (dataItemX.InvestmentStyle == 0) {
                    return 0;
                }

                else {
                    return dataItemX.InvestmentStyle;
                }
            }
        }
        function onChangeInvestmentStyle() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
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
                    //suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeInvestmentObjective,
                    value: setUpInvestmentObjective()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setUpInvestmentObjective() {
            if (dataItemX.InvestmentObjective == null) {
                return 0;
            } else {
                if (dataItemX.InvestmentObjective == 0) {
                    return 0;
                } else {
                    return dataItemX.InvestmentObjective;
                }
            }
        }

        function onChangeInvestmentObjective() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
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
                    //suggest: true,
                    enable: false,
                    dataSource: data,
                    change: onChangeRevision,
                    value: setUpRevision()

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setUpRevision() {
            if (dataItemX.Revision == null) {
                return 0;
            } else {
                if (dataItemX.Revision == 0) {
                    return 0;
                } else {
                    return dataItemX.Revision;
                }
            }
        }

        function onChangeRevision() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //Cross Fund
        if (dataItemX.InstrumentTypePK == 2 || dataItemX.InstrumentTypePK == 3 || dataItemX.InstrumentTypePK == 8 || dataItemX.InstrumentTypePK == 9 || dataItemX.InstrumentTypePK == 13 || dataItemX.InstrumentTypePK == 15) {
            $("#LblCrossFundFrom").show();
        }
        else {
            $("#LblCrossFundFrom").hide();
        }

        //Cross Fund
        if (dataItemX.InstrumentTypePK == 5) {
            $("#lblBankBranchPK").show();
            if (_GlobClientCode == "25") {
                if (dataItemX.TrxType == 1) {
                    $("#lblAmountToTransfer").show();
                }
                else
                    $("#lblAmountToTransfer").hide();
            }
            else if (_GlobClientCode == "17") {
                console.log($("#TrxType").val());
                if (dataItemX.TrxType == 1) {
                    $("#lblAmountToTransfer").show();
                }
                else if (dataItemX.TrxType == 2) {
                    $("#lblAmountToTransfer").show();
                }
                else if (dataItemX.TrxType == 3) {
                    $("#lblAmountToTransfer").show();
                }
                else {
                    $("#lblAmountToTransfer").hide();
                }
            }
            else {
                $("#lblAmountToTransfer").hide();
            }

            $("#MaturityDate").data("kendoDatePicker").enable(true);
            $("#MaturityDate").removeClass("MediumComboAndDateReadOnly");
            $("#MaturityDate").addClass("MediumComboAndDate");
        }

        else {
            $("#lblBankBranchPK").hide();
            $("#MaturityDate").data("kendoDatePicker").enable(false);
            $("#MaturityDate").removeClass("MediumComboAndDate");
            $("#MaturityDate").addClass("MediumComboAndDateReadOnly");
        }



        $("#ValueDate").data("kendoDatePicker").enable(false);
        $("#InstructionDate").data("kendoDatePicker").enable(false);
        $("#LastCouponDate").data("kendoDatePicker").enable(false);
        $("#NextCouponDate").data("kendoDatePicker").enable(false);




        $("#AcqDate").data("kendoDatePicker").enable(true);
        $("#AcqDate1").data("kendoDatePicker").enable(true);
        $("#AcqDate2").data("kendoDatePicker").enable(true);
        $("#AcqDate3").data("kendoDatePicker").enable(true);
        $("#AcqDate4").data("kendoDatePicker").enable(true);
        $("#AcqDate5").data("kendoDatePicker").enable(true);



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
                    value: setCmbInstrumentTypePK()
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

        function setCmbInstrumentTypePK() {
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

                if (_GlobClientCode == 17) {
                    $("#BankBranchPK").data("kendoComboBox").enable(true);
                } else {
                    $("#BankBranchPK").data("kendoComboBox").enable(false);
                }
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

        $("#Volume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
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

        if (dataItemX.InstrumentTypePK == 5 || dataItemX.InstrumentTypePK == 10) {
            $("#InterestPercent").data("kendoNumericTextBox").enable(true);
            $("#InterestPercent").removeClass("textboxMediumNumberReadOnly");
            $("#InterestPercent").addClass("textboxMediumNumber");
        }
        else {
            $("#InterestPercent").data("kendoNumericTextBox").enable(false);
            $("#InterestPercent").removeClass("textboxMediumNumber");
            $("#InterestPercent").addClass("textboxMediumNumberReadOnly");
        }

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
            change: onChangeDoneAccruedInterest,
            value: setDoneAccruedInterest(),
        });

        function onChangeDoneAccruedInterest() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            SettlementRecalculate(1);
        }

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
        //$("#DoneAccruedInterest").data("kendoNumericTextBox").enable(false);

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
            format: "n4",
            decimals: 4,
            change: onChangeAcqPrice,
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
        function onChangeAcqPrice() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            RecalNetAmount();
        }
        //$("#AcqPrice").data("kendoNumericTextBox").enable(false);

        $("#AcqPrice1").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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
        //$("#AcqPrice1").data("kendoNumericTextBox").enable(true);

        $("#AcqPrice2").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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
        //$("#AcqPrice2").data("kendoNumericTextBox").enable(true);

        $("#AcqPrice3").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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
        //$("#AcqPrice3").data("kendoNumericTextBox").enable(true);

        $("#AcqPrice4").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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
        //$("#AcqPrice4").data("kendoNumericTextBox").enable(true);

        $("#AcqPrice5").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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
        //$("#AcqPrice5").data("kendoNumericTextBox").enable(true);


        $("#AcqVolume").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
        $("#AcqVolume").data("kendoNumericTextBox").enable(true);


        $("#AcqVolume1").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
        $("#AcqVolume1").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume2").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
        $("#AcqVolume2").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume3").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
        $("#AcqVolume3").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume4").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
        $("#AcqVolume4").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume5").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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

        $("#AcqVolume5").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume6").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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
            format: "n2",
            decimals: 2,
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

        $("#AcqVolume7").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume8").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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

        $("#AcqVolume8").data("kendoNumericTextBox").enable(true);

        $("#AcqVolume9").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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

        $("#AcqVolume9").data("kendoNumericTextBox").enable(true);

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

        $("#TaxExpensePercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setTaxExpensePercent(),

        });
        function setTaxExpensePercent() {
            if (dataItemX.TaxExpensePercent == null) {
                return "";
            } else {
                if (dataItemX.TaxExpensePercent == 0) {
                    return "";
                } else {
                    return dataItemX.TaxExpensePercent;
                }
            }
        }


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


        ////Instrument Type

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
        //        } else {
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
        //    ShowHideLabelByInstrumentType(GlobInstrumentType);


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
                    enable: false,
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
                    //enable: false,
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
                SettlementRecalculate(1);
            }
            else {
                $("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
                SettlementRecalculate(1);
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

        //InvestmentTrType  
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InvestmentTrType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InvestmentTrType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    suggest: true,
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

        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboDefaultForInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#FundCashRefPK").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    change: onChangeFundCashRefPK,
                    suggest: true,
                    value: setCmbFundCashRefPK()
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

        function setCmbFundCashRefPK() {
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

        //---//

        $("#DoneVolume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
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
        $("#DoneVolume").data("kendoNumericTextBox").enable(false);

        function OnChangeDoneVolume() {
            $("#DoneLot").data("kendoNumericTextBox").value($("#DoneVolume").data("kendoNumericTextBox").value() / 100);
            SettlementRecalculate(1);
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
        $("#DoneLot").data("kendoNumericTextBox").enable(false);

        function OnChangeDoneLot() {
            $("#DoneVolume").data("kendoNumericTextBox").value($("#DoneLot").data("kendoNumericTextBox").value() * 100);
            SettlementRecalculate(1);
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
                if (dataItemX.DonePrice == 0 && GlobInstrumentType == 3) {
                    return 1;
                } else if (dataItemX.DonePrice == 0) {
                    "";
                } else {
                    return dataItemX.DonePrice;
                }
            }

        }
        $("#DonePrice").data("kendoNumericTextBox").enable(false);

        function OnChangeDonePrice() {

            SettlementRecalculate(1);
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

        $("#TotalAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
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

        $("#CommissionAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setCommissionAmount(),
            change: onChangeCommissionAmount
        });
        function setCommissionAmount() {
            if (dataItemX.CommissionAmount == null) {
                return "";
            } else {
                if (dataItemX.CommissionAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CommissionAmount;
                }
            }
        }

        function onChangeCommissionAmount() {

            SettlementRecalculate(1);
        }


        $("#LevyAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setLevyAmount(),
            change: onChangeLevyAmount

        });
        function setLevyAmount() {
            if (dataItemX.LevyAmount == null) {
                return "";
            } else {
                if (dataItemX.LevyAmount == 0) {
                    return "";
                } else {
                    return dataItemX.LevyAmount;
                }
            }
        }
        function onChangeLevyAmount() {

            SettlementRecalculate(1);
        }

        $("#KPEIAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setKPEIAmount(),
            change: onChangeKPEIAmount

        });
        function setKPEIAmount() {
            if (dataItemX.KPEIAmount == null) {
                return "";
            } else {
                if (dataItemX.KPEIAmount == 0) {
                    return "";
                } else {
                    return dataItemX.KPEIAmount;
                }
            }
        }
        function onChangeKPEIAmount() {

            SettlementRecalculate(1);
        }

        $("#VATAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setVATAmount(),
            change: onChangeVATAmount

        });
        function setVATAmount() {
            if (dataItemX.VATAmount == null) {
                return "";
            } else {
                if (dataItemX.VATAmount == 0) {
                    return "";
                } else {
                    return dataItemX.VATAmount;
                }
            }
        }
        function onChangeVATAmount() {

            SettlementRecalculate(1);
        }

        $("#WHTAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setWHTAmount(),
            change: onChangeWHTAmount

        });
        function setWHTAmount() {
            if (dataItemX.WHTAmount == null) {
                return "";
            } else {
                if (dataItemX.WHTAmount == 0) {
                    return "";
                } else {
                    return dataItemX.WHTAmount;
                }
            }
        }
        function onChangeWHTAmount() {

            SettlementRecalculate(1);
        }

        $("#OTCAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setOTCAmount(),
            change: onChangeOTCAmount

        });
        function setOTCAmount() {
            if (dataItemX.OTCAmount == null) {
                return "";
            } else {
                if (dataItemX.OTCAmount == 0) {
                    return "";
                } else {
                    return dataItemX.OTCAmount;
                }
            }
        }
        function onChangeOTCAmount() {


            SettlementRecalculate(1);
        }

        $("#IncomeTaxSellAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setIncomeTaxSellAmount(),
            change: onChangeIncomeTaxSellAmount

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
        function onChangeIncomeTaxSellAmount() {

            SettlementRecalculate(1);
        }

        $("#IncomeTaxInterestAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setIncomeTaxInterestAmount(),
            change: onChangeIncomeTaxInterestAmount

        });
        function setIncomeTaxInterestAmount() {
            if (dataItemX.IncomeTaxInterestAmount == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxInterestAmount == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxInterestAmount;
                }
            }
        }
        function onChangeIncomeTaxInterestAmount() {

            SettlementRecalculate(1);
        }

        $("#IncomeTaxGainAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setIncomeTaxGainAmount(),
            change: onChangeIncomeTaxGainAmount
        });
        function setIncomeTaxGainAmount() {
            if (dataItemX.IncomeTaxGainAmount == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxGainAmount == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxGainAmount;
                }
            }
        }
        function onChangeIncomeTaxGainAmount() {


            UpdateRecalTotalAmount();
        }

        $("#CurrencyRate").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setCurrencyRate()

        });
        function setCurrencyRate() {
            if (dataItemX.CurrencyRate == null) {
                return "";
            } else {
                if (dataItemX.CurrencyRate == 0) {
                    return 1;
                } else {
                    return dataItemX.CurrencyRate;
                }
            }
        }

        $("#CurrencyRate").data("kendoNumericTextBox").enable(false);

        //--------------------------------------------PERCENT
        $("#CommissionPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setCommissionPercent(),
            change: onChangeCommissionPercent
        });
        function setCommissionPercent() {
            if (dataItemX.CommissionPercent == null) {
                return "";
            } else {
                if (dataItemX.CommissionPercent == 0) {
                    return "";
                } else {
                    return dataItemX.CommissionPercent;
                }
            }
        }

        function onChangeCommissionPercent() {

            SettlementRecalculate(3);
        }

        $("#CommissionPercent").data("kendoNumericTextBox").enable(false);


        $("#LevyPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setLevyPercent(),
            change: onChangeLevyPercent

        });
        function setLevyPercent() {
            if (dataItemX.LevyPercent == null) {
                return "";
            } else {
                if (dataItemX.LevyPercent == 0) {
                    return "";
                } else {
                    return dataItemX.LevyPercent;
                }
            }
        }

        function onChangeLevyPercent() {

            SettlementRecalculate(3);
        }

        $("#LevyPercent").data("kendoNumericTextBox").enable(false);

        $("#KPEIPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setKPEIPercent(),
            change: onChangeKPEIPercent

        });
        function setKPEIPercent() {
            if (dataItemX.KPEIPercent == null) {
                return "";
            } else {
                if (dataItemX.KPEIPercent == 0) {
                    return "";
                } else {
                    return dataItemX.KPEIPercent;
                }
            }
        }

        function onChangeKPEIPercent() {

            SettlementRecalculate(3);
        }

        $("#KPEIPercent").data("kendoNumericTextBox").enable(false);

        $("#VATPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setVATPercent(),
            change: onChangeVATPercent

        });
        function setVATPercent() {
            if (dataItemX.VATPercent == null) {
                return "";
            } else {
                if (dataItemX.VATPercent == 0) {
                    return "";
                } else {
                    return dataItemX.VATPercent;
                }
            }
        }
        function onChangeVATPercent() {

            SettlementRecalculate(3);
        }

        $("#VATPercent").data("kendoNumericTextBox").enable(false);

        $("#WHTPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setWHTPercent(),
            change: onChangeWHTPercent

        });
        function setWHTPercent() {
            if (dataItemX.WHTPercent == null) {
                return "";
            } else {
                if (dataItemX.WHTPercent == 0) {
                    return "";
                } else {
                    return dataItemX.WHTPercent;
                }
            }
        }
        function onChangeWHTPercent() {

            SettlementRecalculate(3);
        }
        $("#WHTPercent").data("kendoNumericTextBox").enable(false);

        $("#OTCPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setOTCPercent(),
            change: onChangeOTCPercent

        });
        function setOTCPercent() {
            if (dataItemX.OTCPercent == null) {
                return "";
            } else {
                if (dataItemX.OTCPercent == 0) {
                    return "";
                } else {
                    return dataItemX.OTCPercent;
                }
            }
        }
        function onChangeOTCPercent() {

            SettlementRecalculate(3);
        }
        $("#OTCPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxSellPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setIncomeTaxSellPercent(),
            change: onChangeIncomeTaxSellPercent

        });
        function setIncomeTaxSellPercent() {
            if (dataItemX.IncomeTaxSellPercent == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxSellPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxSellPercent;
                }
            }
        }
        function onChangeIncomeTaxSellPercent() {

            RecalNetAmount();
        }
        //$("#IncomeTaxSellPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxInterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setIncomeTaxInterestPercent(),
            change: onChangeIncomeTaxInterestPercent

        });
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
        function onChangeIncomeTaxInterestPercent() {

            RecalNetAmount();
        }
        //$("#IncomeTaxInterestPercent").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxGainPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setIncomeTaxGainPercent(),
            change: onChangeIncomeTaxGainPercent

        });
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
        function onChangeIncomeTaxGainPercent() {

            RecalNetAmount();
        }
        //$("#IncomeTaxGainPercent").data("kendoNumericTextBox").enable(false);

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



        $("#PriceMode").kendoNumericTextBox({
            format: "n0",
            value: setPriceMode(),

        });
        function setPriceMode() {
            if (dataItemX.PriceMode == null) {
                return "";
            } else {
                if (dataItemX.PriceMode == 0) {
                    return "";
                } else {
                    return dataItemX.PriceMode;
                }
            }
        }


        //$("#InterestDaysType").kendoNumericTextBox({
        //    format: "n0",
        //    value: setInterestDaysType(),

        //});
        //function setInterestDaysType() {
        //    if (dataItemX.InterestDaysType == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.InterestDaysType == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.InterestDaysType;
        //        }
        //    }
        //}

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestPaymentType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestPaymentType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    enable: true,
                    dataSource: data,
                    value: setInterestPaymentType(),
                    change: onChangeInterestPaymentType
                });

                if (dataItemX.TrxType == 2) {
                    $("#InterestPaymentType").data("kendoComboBox").enable(false);
                }
                else {
                    $("#InterestPaymentType").data("kendoComboBox").enable(true);
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



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

        function onChangeInterestPaymentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
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
                    enable: true,
                    dataSource: data,
                    value: setPaymentModeOnMaturity(),
                    change: onChangePaymentModeOnMaturity
                });
                if (dataItemX.TrxType == 2) {
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(false);
                }
                else {
                    $("#PaymentModeOnMaturity").data("kendoComboBox").enable(true);
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



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

        function onChangePaymentModeOnMaturity() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        //Purpose Of Transaction
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PurposeOfTransaction",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PurposeOfTransaction").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: onChangePurposeOfTransaction,
                    value: setPurposeOfTransaction(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setPurposeOfTransaction() {
            if (dataItemX.PurposeOfTransaction == null) {
                return 3;
            } else {
                if (dataItemX.PurposeOfTransaction == 0) {
                    return 3;
                } else {
                    return dataItemX.PurposeOfTransaction;
                }
            }
        }

        function onChangePurposeOfTransaction() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        //Statutory Type
        $("#StatutoryType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: 1 },
                { text: "No", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatutoryType,
            value: setCmbStatutoryType()
        });
        function OnChangeStatutoryType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbStatutoryType() {
            if (dataItemX.StatutoryType == null) {
                return 2;
            } else {
                if (dataItemX.StatutoryType == 0) {
                    return 2;
                } else {
                    return dataItemX.StatutoryType;
                }
            }
        }


        //Security Code Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SecurityCodeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SecurityCodeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: onChangeSecurityCodeType,
                    value: setSecurityCodeType(),
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setSecurityCodeType() {
            if (dataItemX.SecurityCodeType == null) {
                return "";
            } else {
                if (dataItemX.SecurityCodeType == 0) {
                    return "";
                } else {
                    return dataItemX.SecurityCodeType;
                }
            }
        }

        function onChangeSecurityCodeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#AmountToTransfer").kendoNumericTextBox({
            format: "n0"
        });

        if (dataItemX.TrxType == 3) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/GetAcqDateBeforeForRollover/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InvestmentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#AcqDateBefore").val(kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy'));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }


            });
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
        $("#LotInShare").val("");

    }

    function ClearDataEquity() {
        $("#AccruedInterest").val("");
        $("#InterestPercent").val("");
        $("#MaturityDate").data("kendoDatePicker").value("");

    }

    function ClearDataMoney() {
        $("#DoneLot").val("");
        $("#Lot").val("");
        $("#LotInShare").val("");
        $("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());


    }

    function ClearRequiredAttribute() {
        $("#DonePrice").removeAttr("required");
        $("#DoneVolume").removeAttr("required");
        $("#OrderPrice").removeAttr("required");
        $("#AcqPrice").removeAttr("required");
        $("#DoneLot").removeAttr("required");
        $("#Lot").removeAttr("required");
        $("#LotInShare").removeAttr("required");
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

    function ShowHideLabelByInstrumentType(_type, _trxType) {
        ClearRequiredAttribute();
        GlobValidator.hideMessages();
        //BOND
        if (_type == 2 || _type == 3 || _type == 9 || _type == 13 || _type == 15) {

            $("#LblLot").hide();
            $("#LblDoneLot").hide();
            $("#LblLotInShare").hide();
            $("#LblInterestPercent").show();
            $("#LblBreakInterestPercent").hide();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            //  $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblDonePrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqVolume").show();
            $("#LblRangePrice").hide();
            $("#LblAccruedInterest").show();
            $("#LblDoneAccruedInterest").show();

            $("#LblMaturityDate").show();
            $("#LblAcqDate").show();
            $("#LblTenor").show();
            $("#LblBankBranch").hide();

            $("#LblAcqPrice1").show();
            $("#LblAcqVolume1").show();
            $("#LblAcqDate1").show();
            $("#LblAcqPrice2").show();
            $("#LblAcqVolume2").show();
            $("#LblAcqDate2").show();
            $("#LblAcqPrice3").show();
            $("#LblAcqVolume3").show();
            $("#LblAcqDate3").show();
            $("#LblAcqPrice4").show();
            $("#LblAcqVolume4").show();
            $("#LblAcqDate4").show();
            $("#LblAcqPrice5").show();
            $("#LblAcqVolume5").show();
            $("#LblAcqDate5").show();
            $("#LblAcqPrice6").show();
            $("#LblAcqVolume6").show();
            $("#LblAcqDate6").show();
            $("#LblAcqPrice7").show();
            $("#LblAcqVolume7").show();
            $("#LblAcqDate7").show();
            $("#LblAcqPrice8").show();
            $("#LblAcqVolume8").show();
            $("#LblAcqDate8").show();
            $("#LblAcqPrice9").show();
            $("#LblAcqVolume9").show();
            $("#LblAcqDate9").show();
            $("#LblHeaderAcq").show();
            $("#TblAcq").show();
            $("#LblVolume").hide();
            $("#LblNominal").show();
            $("#LblDoneVolume").hide();
            $("#LblDoneNominal").show();
            $("#LblBoardType").hide();
            $("#LblSettlementMode").show();
            $("#LblBtnMatch").hide();
            $("#LblBtnApproved").show();
            $("#LblPurposeOfTransaction").show();
            $("#LblStatutoryType").show();
            $("#LblCPSafekeepingAccNumber").show();
            $("#LblPlaceOfSettlement").show();
            $("#LblFundSafekeepingAccountNumber").show();
            $("#LblSecurityCodeType").show();
            $("#DonePrice").attr("required", true);
            $("#DoneVolume").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#SettlementMode").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            //$("#AcqPrice").attr("required", true);
            //$("#SettledDate").attr("required", true);
            $("#LblInterestPaymentType").hide();
            $("#LblPaymentModeOnMaturity").hide();

            if (_GlobClientCode == "20") {
                $("#LblBIRate").hide();
                $("#LblInvestmentStrategy").hide();
                $("#LblInvestmentStyle").hide();
                $("#LblInvestmentObjective").hide();
                $("#LblRevision").hide();
                $("#LblOtherInvestmentStyle").hide();
                $("#LblOtherInvestmentObjective").hide();
                $("#LblOtherRevision").hide();
            }
            else {

                $("#LblBIRate").hide();
                $("#LblInvestmentStrategy").hide();
                $("#LblInvestmentStyle").hide();
                $("#LblInvestmentObjective").hide();
                $("#LblRevision").hide();
                $("#LblOtherInvestmentStyle").hide();
                $("#LblOtherInvestmentObjective").hide();
                $("#LblOtherRevision").hide();
            }

        }

        //EQUITY
        else if (_type == 1) {
            $("#LblLot").show();
            $("#LblDoneLot").show();
            $("#LblLotInShare").show();
            $("#LblInterestPercent").hide();
            $("#LblBreakInterestPercent").hide();
            $("#LblMaturityDate").hide();
            $("#LblSettlementDate").show();
            $("#LblAcqDate").hide();
            $("#LblPrice").show();
            $("#LblDonePrice").show();
            $("#LblAcqPrice").hide();
            $("#LblAcqVolume").hide();
            $("#LblRangePrice").show();
            $("#LblLastCouponDate").hide();
            $("#LblNextCouponDate").hide();
            $("#LblAccruedInterest").hide();
            $("#LblDoneAccruedInterest").hide();
            $("#LblTenor").hide();
            $("#LblHeaderAcq").hide();
            $("#TblAcq").hide();
            $("#LblVolume").show();
            $("#LblNominal").hide();
            $("#LblDoneVolume").show();
            $("#LblDoneNominal").hide();
            $("#LblBoardType").show();
            $("#LblSettlementMode").show();
            $("#LblBtnMatch").show();
            $("#LblBtnApproved").hide();
            $("#LblBankBranch").hide();
            $("#LblPurposeOfTransaction").show();
            $("#LblStatutoryType").show();
            $("#LblCPSafekeepingAccNumber").show();
            $("#LblPlaceOfSettlement").show();
            $("#LblFundSafekeepingAccountNumber").show();
            $("#LblSecurityCodeType").show();
            //$("#AcqPrice").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            //$("#Lot").attr("required", true);
            //$("#DonePrice").attr("required", true);
            //$("#DoneVolume").attr("required", true);
            //$("#DoneLot").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            //$("#LotInShare").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#BoardType").attr("required", true);

            $("#LblInterestPaymentType").hide();
            $("#LblPaymentModeOnMaturity").hide();

            if (_GlobClientCode == "20") {
                $("#LblBIRate").show();
                $("#LblInvestmentStrategy").show();
                $("#LblInvestmentStyle").show();
                $("#LblInvestmentObjective").show();
                $("#LblRevision").show();

                $("#LblOtherInvestmentStyle").show();
                $("#LblOtherInvestmentObjective").show();
                $("#LblOtherRevision").show();
            } else {

                $("#LblBIRate").hide();
                $("#LblInvestmentStrategy").hide();
                $("#LblInvestmentStyle").hide();
                $("#LblInvestmentObjective").hide();
                $("#LblRevision").hide();

                $("#LblOtherInvestmentStyle").hide();
                $("#LblOtherInvestmentObjective").hide();
                $("#LblOtherRevision").hide();
            }

        }
        //DEPOSITO
        else if (_type == 5) {

            $("#LblLot").hide();
            $("#LblDoneLot").hide();
            $("#LblLotInShare").hide();
            // $("#LblSettlementDate").hide();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblAccruedInterest").hide();
            $("#LblDoneAccruedInterest").hide();
            $("#LblPrice").show();
            $("#LblAcqDate").show();
            $("#LblDonePrice").hide();
            $("#LblRangePrice").hide();
            $("#LblLastCouponDate").hide();
            $("#LblNextCouponDate").hide();
            $("#LblAcqPrice").hide();
            $("#LblAcqVolume").hide();
            $("#LblHeaderAcq").hide();
            $("#TblAcq").hide();
            $("#LblVolume").hide();
            $("#LblNominal").show();
            $("#LblDoneVolume").hide();
            $("#LblDoneNominal").show();
            $("#LblBtnMatch").hide();
            $("#LblBtnApproved").show();
            $("#DoneVolume").attr("required", true);
            $("#LblBoardType").hide();
            $("#LblSettlementDate").hide();
            $("#LblBankBranch").show();
            $("#LblPurposeOfTransaction").show();
            $("#LblStatutoryType").show();
            $("#LblCPSafekeepingAccNumber").show();
            $("#LblPlaceOfSettlement").show();
            $("#LblFundSafekeepingAccountNumber").show();
            $("#LblSecurityCodeType").show();

            $("#LblInterestPaymentType").show();
            $("#LblPaymentModeOnMaturity").show();
            if (_trxType == 1) {
                $("#LblBreakInterestPercent").hide();
            }
            else {
                $("#LblBreakInterestPercent").show();
            }

            if (_trxType == 3) {
                $("#LblAcqDateBefore").show();
            }
            else {
                $("#LblAcqDateBefore").hide();
            }


            //$("#DonePrice").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            //$("#MaturityDate").attr("required", true);
            //$("#InterestPercent").attr("required", true);
            //$("#OrderPrice").data("kendoNumericTextBox").value(1);
            //$("#DonePrice").data("kendoNumericTextBox").value(1);

            $("#LblBIRate").hide();
            $("#LblInvestmentStrategy").hide();
            $("#LblInvestmentStyle").hide();
            $("#LblInvestmentObjective").hide();
            $("#LblRevision").hide();

            $("#LblOtherInvestmentStyle").hide();
            $("#LblOtherInvestmentObjective").hide();
            $("#LblOtherRevision").hide();

        }

        //REKSADANA
        else if (_type == 6) {
            $("#LblLot").hide();
            $("#LblDoneLot").hide();
            $("#LblLotInShare").hide();
            $("#LblInterestPercent").hide();
            $("#LblBreakInterestPercent").hide();
            $("#LblMaturityDate").hide();
            $("#LblSettlementDate").show();
            $("#LblAcqDate").hide();
            $("#LblPrice").show();
            $("#LblDonePrice").show();
            $("#LblAcqPrice").hide();
            $("#LblAcqVolume").hide();
            $("#LblRangePrice").show();
            $("#LblLastCouponDate").hide();
            $("#LblNextCouponDate").hide();
            $("#LblAccruedInterest").hide();
            $("#LblDoneAccruedInterest").hide();
            $("#LblTenor").hide();
            $("#LblHeaderAcq").hide();
            $("#TblAcq").hide();
            $("#LblVolume").show();
            $("#LblNominal").hide();
            $("#LblDoneVolume").show();
            $("#LblDoneNominal").hide();
            $("#LblBoardType").show();
            $("#LblSettlementMode").show();
            $("#LblBtnMatch").show();
            $("#LblBtnApproved").hide();
            //$("#AcqPrice").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            //$("#Lot").attr("required", true);
            //$("#DonePrice").attr("required", true);
            //$("#DoneVolume").attr("required", true);
            //$("#DoneLot").attr("required", true);
            $("#CounterpartPK").attr("required", true);
            //$("#LotInShare").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#BoardType").attr("required", true);

            $("#LblBIRate").hide();
            $("#LblInvestmentStrategy").hide();
            $("#LblInvestmentStyle").hide();
            $("#LblInvestmentObjective").hide();
            $("#LblRevision").hide();

            $("#LblOtherInvestmentStyle").hide();
            $("#LblOtherInvestmentObjective").hide();
            $("#LblOtherRevision").hide();

            $("#LblInterestPaymentType").hide();
            $("#LblPaymentModeOnMaturity").hide();
        }

    }

    function clearData() {
        $("#SettlementPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#PeriodID").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#InstructionDate").data("kendoDatePicker").value(null);
        $("#Reference").val("");
        $("#SIReference").val("");
        $("#InstrumentTypePK").val("");
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
        $("#LotInShare").val("");
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
        $("#DoneLot").val("");
        $("#DoneVolume").val("");
        $("#DonePrice").val("");
        $("#DoneAmount").val("");
        $("#AcqPrice").val("");
        $("#AcqVolume").val("");
        $("#AcqDate").data("kendoDatePicker").value(null);
        $("#AcqPrice1").val("");
        $("#AcqVolume1").val("");
        $("#AcqDate1").data("kendoDatePicker").value(null);
        $("#AcqPrice2").val("");
        $("#AcqVolume2").val("");
        $("#AcqDate2").data("kendoDatePicker").value(null);
        $("#AcqPrice3").val("");
        $("#AcqVolume3").val("");
        $("#AcqDate3").data("kendoDatePicker").value(null);
        $("#AcqPrice4").val("");
        $("#AcqVolume4").val("");
        $("#AcqDate4").data("kendoDatePicker").value(null);
        $("#AcqPrice5").val("");
        $("#AcqVolume5").val("");
        $("#AcqDate5").data("kendoDatePicker").value(null);
        $("#AcqPrice6").val("");
        $("#AcqVolume6").val("");
        $("#AcqDate6").data("kendoDatePicker").value(null);
        $("#AcqPrice7").val("");
        $("#AcqVolume7").val("");
        $("#AcqDate7").data("kendoDatePicker").value(null);
        $("#AcqPrice8").val("");
        $("#AcqVolume8").val("");
        $("#AcqDate8").data("kendoDatePicker").value(null);
        $("#AcqPrice9").val("");
        $("#AcqVolume9").val("");
        $("#AcqDate9").data("kendoDatePicker").value(null);
        $("#CommissionPercent").val("0");
        $("#LevyPercent").val("0");
        $("#KPEIPercent").val("0");
        $("#VATPercent").val("0");
        $("#WHTPercent").val("0");
        $("#OTCPercent").val("0");
        $("#IncomeTaxSellPercent").val("0");
        $("#IncomeTaxInterestPercent").val("0");
        $("#IncomeTaxGainPercent").val("0");
        $("#CommissionAmount").val("0");
        $("#LevyAmount").val("0");
        $("#KPEIAmount").val("0");
        $("#VATAmount").val("0");
        $("#WHTAmount").val("0");
        $("#OTCAmount").val("0");
        $("#IncomeTaxSellAmount").val("0");
        $("#IncomeTaxInterestAmount").val("0");
        $("#IncomeTaxGainAmount").val("0");
        $("#CurrencyRate").val("0");
        $("#PurposeOfTransaction").val("0");
        $("#StatutoryType").val("0");
        $("#BoardType").val("");
        $("#SettlementMode").val("");
        $("#CPSafekeepingAccNumber").val("");
        $("#PlaceOfSettlement").val("");
        $("#FundSafekeepingAccountNumber").val("");
        $("#SecurityCodeType").val("");
        $("#AmountToTransfer").val("");
        $("#EntrySettlementID").val("");
        $("#UpdateSettlementID").val("");
        $("#ApprovedSettlementID").val("");
        $("#VoidSettlementID").val("");
        $("#EntrySettlementTime").val("");
        $("#UpdateSettlementTime").val("");
        $("#ApprovedSettlementTime").val("");
        $("#VoidSettlementTime").val("");
        $("#LastUpdate").val("");
        $("#InvestmentTrType").val("");

        $("#BIRate").val("");
        $("#InvestmentStrategy").val("");
        $("#InvestmentStyle").val("");
        $("#InvestmentObjective").val("");
        $("#Revision").val("");

        $("#OtherInvestmentStyle").val("");
        $("#OtherInvestmentObjective").val("");
        $("#OtherRevision").val("");

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
                pageSize: 10000,

                schema: {
                    model: {
                        fields: {
                            InvestmentPK: { type: "number" },
                            DealingPK: { type: "number" },
                            SettlementPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            SelectedSettlement: { type: "boolean" },
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
                            FundCashRefPK: { type: "number" },
                            FundCashRefID: { type: "string" },
                            OrderPrice: { type: "number" },
                            RangePrice: { type: "string" },
                            AcqPrice: { type: "number" },
                            Lot: { type: "number" },
                            LotInShare: { type: "number" },
                            Volume: { type: "number" },
                            Amount: { type: "number" },
                            DonePrice: { type: "number" },
                            DoneLot: { type: "number" },
                            DoneVolume: { type: "number" },
                            DoneAmount: { type: "number" },
                            SettledDate: { type: "date" },
                            InterestPercent: { type: "number" },
                            BreakInterestPercent: { type: "number" },
                            AccruedInterest: { type: "number" },
                            DoneAccruedInterest: { type: "number" },
                            LastCouponDate: { type: "date" },
                            NextCouponDate: { type: "date" },
                            Tenor: { type: "number" },
                            MaturityDate: { type: "date" },
                            AcqDate: { type: "date" },
                            AcqVolume: { type: "number" },
                            InvestmentNotes: { type: "string" },
                            CommissionPercent: { type: "number" },
                            LevyPercent: { type: "number" },
                            KPEIPercent: { type: "number" },
                            VATPercent: { type: "number" },
                            WHTPercent: { type: "number" },
                            OTCPercent: { type: "number" },
                            IncomeTaxSellPercent: { type: "number" },
                            IncomeTaxInterestPercent: { type: "number" },
                            IncomeTaxGainPercent: { type: "number" },
                            CommissionAmount: { type: "number" },
                            LevyAmount: { type: "number" },
                            KPEIAmount: { type: "number" },
                            VATAmount: { type: "number" },
                            WHTAmount: { type: "number" },
                            OTCAmount: { type: "number" },
                            IncomeTaxSellAmount: { type: "number" },
                            IncomeTaxInterestAmount: { type: "number" },
                            IncomeTaxGainAmount: { type: "number" },
                            CurrencyRate: { type: "number" },
                            SettlementMode: { type: "number" },
                            SettlementModeDesc: { type: "string" },
                            BoardType: { type: "number" },
                            BoardTypeDesc: { type: "string" },
                            AcqPrice1: { type: "number" },
                            AcqVolume1: { type: "number" },
                            AcqDate1: { type: "date" },
                            AcqPrice2: { type: "number" },
                            AcqVolume2: { type: "number" },
                            AcqDate2: { type: "date" },
                            AcqPrice3: { type: "number" },
                            AcqVolume3: { type: "number" },
                            AcqDate3: { type: "date" },
                            AcqPrice4: { type: "number" },
                            AcqVolume4: { type: "number" },
                            AcqDate4: { type: "date" },
                            AcqPrice5: { type: "number" },
                            AcqVolume5: { type: "number" },
                            AcqDate5: { type: "date" },
                            AcqPrice6: { type: "number" },
                            AcqVolume6: { type: "number" },
                            AcqDate6: { type: "date" },
                            AcqPrice7: { type: "number" },
                            AcqVolume7: { type: "number" },
                            AcqDate7: { type: "date" },
                            AcqPrice8: { type: "number" },
                            AcqVolume8: { type: "number" },
                            AcqDate8: { type: "date" },
                            AcqPrice9: { type: "number" },
                            AcqVolume9: { type: "number" },
                            AcqDate9: { type: "date" },
                            TaxExpensePercent: { type: "number" },
                            TotalAmount: { type: "number" },
                            Category: { type: "string" },
                            BankBranchPK: { type: "number" },
                            CrossFundFromPK: { type: "number" },
                            PriceMode: { type: "number" },
                            BitBreakable: { type: "boolean" },
                            InterestDaysType: { type: "number" },
                            InterestPaymentType: { type: "number" },
                            PaymentModeOnMaturity: { type: "number" },
                            PurposeOfTransaction: { type: "number" },
                            PurposeOfTransactionDesc: { type: "string" },
                            StatutoryType: { type: "number" },
                            BitForeignTrx: { type: "boolean" },
                            CPSafekeepingAccNumber: { type: "string" },
                            PlaceOfSettlement: { type: "string" },
                            FundSafekeepingAccountNumber: { type: "string" },
                            SecurityCodeType: { type: "number" },
                            SecurityCodeTypeDesc: { type: "string" },
                            EntrySettlementID: { type: "string" },
                            EntrySettlementTime: { type: "date" },
                            UpdateSettlementID: { type: "string" },
                            UpdateSettlementTime: { type: "date" },
                            ApprovedSettlementID: { type: "string" },
                            ApprovedSettlementTime: { type: "date" },
                            VoidSettlementID: { type: "string" },
                            VoidSettlementTime: { type: "date" },
                            LastUpdate: { type: "date" },
                            Timestamp: { type: "string" }
                        }

                    }

                },
                group: {
                    field: "StatusDesc", aggregates: [
                        { field: "DoneLot", aggregate: "sum" },
                        { field: "DoneAmount", aggregate: "sum" },
                        { field: "CommissionAmount", aggregate: "sum" },
                        { field: "LevyAmount", aggregate: "sum" },
                        { field: "KPEIAmount", aggregate: "sum" },
                        { field: "VATAmount", aggregate: "sum" },
                        { field: "WHTAmount", aggregate: "sum" },
                        { field: "OTCAmount", aggregate: "sum" },
                        { field: "IncomeTaxSellAmount", aggregate: "sum" },
                        { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                        { field: "IncomeTaxGainAmount", aggregate: "sum" },
                        { field: "DoneAccruedInterest", aggregate: "sum" },
                        { field: "TotalAmount", aggregate: "sum" }
                    ]
                },
                aggregate: [
                    { field: "DoneLot", aggregate: "sum" },
                    { field: "DoneAmount", aggregate: "sum" },
                    { field: "CommissionAmount", aggregate: "sum" },
                    { field: "LevyAmount", aggregate: "sum" },
                    { field: "KPEIAmount", aggregate: "sum" },
                    { field: "VATAmount", aggregate: "sum" },
                    { field: "WHTAmount", aggregate: "sum" },
                    { field: "OTCAmount", aggregate: "sum" },
                    { field: "IncomeTaxSellAmount", aggregate: "sum" },
                    { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                    { field: "IncomeTaxGainAmount", aggregate: "sum" },
                    { field: "DoneAccruedInterest", aggregate: "sum" },
                    { field: "TotalAmount", aggregate: "sum" }
                ],
            });
    }

    function getDataSourceCustomJarvis(_url) {
        console.log("Test Custom")
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
                pageSize: 10000,

                schema: {
                    model: {
                        fields: {
                            InvestmentPK: { type: "number", editable: false },
                            DealingPK: { type: "number", editable: false },
                            SettlementPK: { type: "number", editable: false },
                            HistoryPK: { type: "number", editable: false },
                            SelectedSettlement: { type: "boolean", editable: false },
                            StatusInvestment: { type: "number", editable: false },
                            StatusDealing: { type: "number", editable: false },
                            StatusSettlement: { type: "number", editable: false },
                            StatusDesc: { type: "string", editable: false },
                            Notes: { type: "string", editable: false },
                            PeriodPK: { type: "number", editable: false },
                            PeriodID: { type: "string", editable: false },
                            ValueDate: { type: "date", editable: false },
                            InstructionDate: { type: "date", editable: false },
                            Reference: { type: "string", editable: false },
                            BankBranchID: { type: "string", editable: false },
                            PTPCode: { type: "string", editable: false },
                            InstrumentTypePK: { type: "number", editable: false },
                            InterestDaysTypeDesc: { type: "string", editable: false },
                            InstrumentTypeID: { type: "string", editable: false },
                            TrxType: { type: "number", editable: false },
                            TrxTypeID: { type: "string", editable: false },
                            CounterpartPK: { type: "number", editable: false },
                            CounterpartID: { type: "string", editable: false },
                            CounterpartName: { type: "string", editable: false },
                            InstrumentPK: { type: "number", editable: false },
                            InstrumentID: { type: "string", editable: false },
                            InstrumentName: { type: "string", editable: false },
                            FundPK: { type: "number", editable: false },
                            FundID: { type: "string", editable: false },
                            FundCashRefPK: { type: "number", editable: false },
                            FundCashRefID: { type: "string", editable: false },
                            OrderPrice: { type: "number", editable: false },
                            RangePrice: { type: "string", editable: false },
                            AcqPrice: { type: "number", editable: false },
                            Lot: { type: "number", editable: false },
                            LotInShare: { type: "number", editable: false },
                            Volume: { type: "number", editable: false },
                            Amount: { type: "number", editable: false },
                            AmountToTransfer: { type: "number" },
                            DonePrice: { type: "number", editable: false },
                            DoneLot: { type: "number", editable: false },
                            DoneVolume: { type: "number", editable: false },
                            DoneAmount: { type: "number", editable: false },
                            SettledDate: { type: "date", editable: false },
                            InterestPercent: { type: "number", editable: false },
                            BreakInterestPercent: { type: "number", editable: false },
                            AccruedInterest: { type: "number", editable: false },
                            DoneAccruedInterest: { type: "number", editable: false },
                            LastCouponDate: { type: "date", editable: false },
                            NextCouponDate: { type: "date", editable: false },
                            Tenor: { type: "number", editable: false },
                            MaturityDate: { type: "date", editable: false },
                            AcqDate: { type: "date", editable: false },
                            AcqVolume: { type: "number", editable: false },
                            InvestmentNotes: { type: "string", editable: false },
                            CommissionPercent: { type: "number", editable: false },
                            LevyPercent: { type: "number", editable: false },
                            KPEIPercent: { type: "number", editable: false },
                            VATPercent: { type: "number", editable: false },
                            WHTPercent: { type: "number", editable: false },
                            OTCPercent: { type: "number", editable: false },
                            IncomeTaxSellPercent: { type: "number", editable: false },
                            IncomeTaxInterestPercent: { type: "number", editable: false },
                            IncomeTaxGainPercent: { type: "number", editable: false },
                            CommissionAmount: { type: "number", editable: false },
                            LevyAmount: { type: "number", editable: false },
                            KPEIAmount: { type: "number", editable: false },
                            VATAmount: { type: "number", editable: false },
                            WHTAmount: { type: "number", editable: false },
                            OTCAmount: { type: "number", editable: false },
                            IncomeTaxSellAmount: { type: "number", editable: false },
                            IncomeTaxInterestAmount: { type: "number", editable: false },
                            IncomeTaxGainAmount: { type: "number", editable: false },
                            CurrencyRate: { type: "number", editable: false },
                            SettlementMode: { type: "number", editable: false },
                            SettlementModeDesc: { type: "string", editable: false },
                            BoardType: { type: "number", editable: false },
                            BoardTypeDesc: { type: "string", editable: false },
                            AcqPrice1: { type: "number", editable: false },
                            AcqVolume1: { type: "number", editable: false },
                            AcqDate1: { type: "date", editable: false },
                            AcqPrice2: { type: "number", editable: false },
                            AcqVolume2: { type: "number", editable: false },
                            AcqDate2: { type: "date", editable: false },
                            AcqPrice3: { type: "number", editable: false },
                            AcqVolume3: { type: "number", editable: false },
                            AcqDate3: { type: "date", editable: false },
                            AcqPrice4: { type: "number", editable: false },
                            AcqVolume4: { type: "number", editable: false },
                            AcqDate4: { type: "date", editable: false },
                            AcqPrice5: { type: "number", editable: false },
                            AcqVolume5: { type: "number", editable: false },
                            AcqDate5: { type: "date", editable: false },
                            AcqPrice6: { type: "number", editable: false },
                            AcqVolume6: { type: "number", editable: false },
                            AcqDate6: { type: "date", editable: false },
                            AcqPrice7: { type: "number", editable: false },
                            AcqVolume7: { type: "number", editable: false },
                            AcqDate7: { type: "date", editable: false },
                            AcqPrice8: { type: "number", editable: false },
                            AcqVolume8: { type: "number", editable: false },
                            AcqDate8: { type: "date", editable: false },
                            AcqPrice9: { type: "number", editable: false },
                            AcqVolume9: { type: "number", editable: false },
                            AcqDate9: { type: "date", editable: false },
                            TaxExpensePercent: { type: "number", editable: false },
                            TotalAmount: { type: "number", editable: false },
                            Category: { type: "string", editable: false },
                            BankBranchPK: { type: "number", editable: false },
                            CrossFundFromPK: { type: "number", editable: false },
                            PriceMode: { type: "number", editable: false },
                            BitBreakable: { type: "boolean", editable: false },
                            InterestDaysType: { type: "number", editable: false },
                            InterestPaymentType: { type: "number", editable: false },
                            PaymentModeOnMaturity: { type: "number", editable: false },
                            PurposeOfTransaction: { type: "number", editable: false },
                            PurposeOfTransactionDesc: { type: "string", editable: false },
                            StatutoryType: { type: "number", editable: false },
                            BitForeignTrx: { type: "boolean", editable: false },
                            CPSafekeepingAccNumber: { type: "string", editable: false },
                            PlaceOfSettlement: { type: "string", editable: false },
                            FundSafekeepingAccountNumber: { type: "string", editable: false },
                            SecurityCodeType: { type: "number", editable: false },
                            SecurityCodeTypeDesc: { type: "string", editable: false },
                            EntrySettlementID: { type: "string", editable: false },
                            EntrySettlementTime: { type: "date", editable: false },
                            UpdateSettlementID: { type: "string", editable: false },
                            UpdateSettlementTime: { type: "date", editable: false },
                            ApprovedSettlementID: { type: "string", editable: false },
                            ApprovedSettlementTime: { type: "date", editable: false },
                            VoidSettlementID: { type: "string", editable: false },
                            VoidSettlementTime: { type: "date", editable: false },
                            LastUpdate: { type: "date", editable: false },
                            Timestamp: { type: "string", editable: false }
                        }

                    }

                },
                group: {
                    field: "StatusDesc", aggregates: [
                        { field: "DoneLot", aggregate: "sum" },
                        { field: "DoneAmount", aggregate: "sum" },
                        { field: "CommissionAmount", aggregate: "sum" },
                        { field: "LevyAmount", aggregate: "sum" },
                        { field: "KPEIAmount", aggregate: "sum" },
                        { field: "VATAmount", aggregate: "sum" },
                        { field: "WHTAmount", aggregate: "sum" },
                        { field: "OTCAmount", aggregate: "sum" },
                        { field: "IncomeTaxSellAmount", aggregate: "sum" },
                        { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                        { field: "IncomeTaxGainAmount", aggregate: "sum" },
                        { field: "DoneAccruedInterest", aggregate: "sum" },
                        { field: "TotalAmount", aggregate: "sum" }
                    ]
                },
                aggregate: [
                    { field: "DoneLot", aggregate: "sum" },
                    { field: "DoneAmount", aggregate: "sum" },
                    { field: "CommissionAmount", aggregate: "sum" },
                    { field: "LevyAmount", aggregate: "sum" },
                    { field: "KPEIAmount", aggregate: "sum" },
                    { field: "VATAmount", aggregate: "sum" },
                    { field: "WHTAmount", aggregate: "sum" },
                    { field: "OTCAmount", aggregate: "sum" },
                    { field: "IncomeTaxSellAmount", aggregate: "sum" },
                    { field: "IncomeTaxInterestAmount", aggregate: "sum" },
                    { field: "IncomeTaxGainAmount", aggregate: "sum" },
                    { field: "DoneAccruedInterest", aggregate: "sum" },
                    { field: "TotalAmount", aggregate: "sum" }
                ],
            });
    }


    function refresh() {
        RefreshNetBuySellSettlementEquity();
        RefreshNetBuySellSettlementBond();

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
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid").setDataSource(newDS);
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid").setDataSource(newDS);
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionBondSellOnly").data("kendoGrid").setDataSource(newDS);

        if (_GlobClientCode == "17") {
            var newDS = getDataSourceCustomJarvis(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);

            var newDS = getDataSourceCustomJarvis(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);
        } else {
            var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);

            var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);
        }



        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid").setDataSource(newDS);
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid").setDataSource(newDS);


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
        RefreshNetBuySellSettlementEquity();

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
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshEquitySell() {
        RefreshNetBuySellSettlementEquity();

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
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshBondBuy() {
        RefreshNetBuySellSettlementBond();

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
       
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid").setDataSource(newDS);

    

    }

    function refreshBondSell() {
        RefreshNetBuySellSettlementBond();

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

        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionBondSellOnly").data("kendoGrid").setDataSource(newDS);



    }

    function refreshTimeDepositBuy() {
        RefreshNetBuySellSettlementTimeDeposit();

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


        if (_GlobClientCode == "17") {
            var newDS = getDataSourceCustomJarvis(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);
        }

        else {
            var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid").setDataSource(newDS);
        }


    }

    function refreshTimeDepositSell() {
        RefreshNetBuySellSettlementTimeDeposit();

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



        if (_GlobClientCode == "17") {
            var newDS = getDataSourceCustomJarvis(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);
        }

        else {
            var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
            $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid").setDataSource(newDS);
        }

    }
    
    function refreshReksadanaBuy() {
        RefreshNetBuySellSettlementReksadana();

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
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid").setDataSource(newDS);


    }

    function refreshReksadanaSell() {
        RefreshNetBuySellSettlementReksadana();

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
        var newDS = getDataSource(window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID);
        $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid").setDataSource(newDS);


    }
    
    function InitgridSettlementInstructionEquityBuyOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquityBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);

        if (_GlobClientCode == "05") {
            var gridEquityBuyOnly = $("#gridSettlementInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                editable: true,
                dataBound: gridSettlementInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "SettlementEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Update", click: _updateEquityBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "SettlementPK", title: "SettlementPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "InstrumentID", title: "Stock", width: 100 },
                    { field: "FundID", title: "Fund", width: 100 },
                    { field: "CounterpartID", title: "Broker", width: 100 },
                    { field: "BoardTypeDesc", title: "Type", width: 100 },
                    { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 },
                    { field: "DoneVolume", title: "Done Volume", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 },
                    //{ field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", width: 85, hidden: true, format: "{0:n2}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n2}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n2}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n2}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TotalAmount", title: "Total", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataEquityBuyOnly(_checked, _settlementPK);

            }
        }
        else if (_GlobClientCode == "03") {
            var gridEquityBuyOnly = $("#gridSettlementInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "SettlementEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "AutoNo", title: "AutoNo", width: 50 },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "SettledDate", title: "Settled Date", template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", width: 50 },
                    { field: "BoardTypeDesc", title: "Type", width: 50 },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataEquityBuyOnly(_checked, _settlementPK);

            }
        }

        else if (_GlobClientCode == "20") {
            var gridEquityBuyOnly = $("#gridSettlementInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "SettlementEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },

                    { field: "OtherInvestmentStyle", hidden: true, title: "OtherInvestmentStyle" },
                    { field: "OtherInvestmentObjective", hidden: true, title: "OtherInvestmentObjective" },
                    { field: "OtherRevision", hidden: true, title: "OtherRevision" },

                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataEquityBuyOnly(_checked, _settlementPK);

            }

        }
        else if (_GlobClientCode == "28") {
            var gridEquityBuyOnly = $("#gridSettlementInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "SettlementEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "RangePrice", title: "Range Price" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },

                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataEquityBuyOnly(_checked, _settlementPK);

            }

        }
        else {
            var gridEquityBuyOnly = $("#gridSettlementInstructionEquityBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquityBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquityBuyOnly").html()),
                excel: {
                    fileName: "SettlementEquityBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquityBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquityBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquityBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquityBuy'></label>",
                        template: "<input type='checkbox' class='checkboxEquityBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquityBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquityBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataEquityBuyOnly(_checked, _settlementPK);

            }
        }

        gridEquityBuyOnly.table.on("click", ".checkboxEquityBuy", selectRowEquityBuy);
        var oldPageSizeApproved = 0;


        $('#chbEquityBuy').change(function (ev) {

            var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowEquityBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedEquityBuy[dataItemZ.SettlementPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }

        }


    }

    function SelectDeselectDataEquityBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Settlement",
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

    function InitgridSettlementInstructionEquitySellOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartEquitySellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);

        if (_GlobClientCode == "05") {
            var gridEquitySellOnly = $("#gridSettlementInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                editable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "SettlementEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Update", click: _updateEquitySell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "SettlementPK", title: "SettlementPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }, width: 100
                    },

                    { field: "InstrumentID", title: "Stock", width: 100 },
                    { field: "FundID", title: "Fund", width: 100 },
                    { field: "CounterpartID", title: "Broker", width: 100 },
                    { field: "BoardTypeDesc", title: "Type", width: 100 },
                    { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 },
                    { field: "DoneVolume", title: "Done Volume", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 },
                    //{ field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", format: "{0:n2}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", width: 100, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", width: 100, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", width: 100, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", width: 100, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", width: 100, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TotalAmount", title: "Total", headerAttributes: {
                            style: "text-align: center"
                        }, format: "{0:n2}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RangePrice", title: "Range Price", hidden: true, width: 50 },
                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },

                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");

        }
        else if (_GlobClientCode == "03") {
            var gridEquitySellOnly = $("#gridSettlementInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                editable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "SettlementEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateEquitySell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "AutoNo", title: "AutoNo", width: 50 },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "SettledDate", title: "Settled Date", width: 100, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", width: 50 },
                    { field: "BoardTypeDesc", title: "Type", width: 50 },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        else if (_GlobClientCode == "20") {
            var gridEquitySellOnly = $("#gridSettlementInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                editable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "SettlementEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateEquitySell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },

                    { field: "OtherInvestmentStyle", hidden: true, title: "OtherInvestmentStyle" },
                    { field: "OtherInvestmentObjective", hidden: true, title: "OtherInvestmentObjective" },
                    { field: "OtherRevision", hidden: true, title: "OtherRevision" },

                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "28") {
            var gridEquitySellOnly = $("#gridSettlementInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                editable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "SettlementEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateEquitySell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "RangePrice", title: "Range Price" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
                    { field: "Reference", title: "Reference", hidden: true, width: 50 },
                    { field: "InstrumentTypeID", title: "Instrument Type", hidden: true, width: 50 },
                    { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 50 },
                    { field: "InstrumentID", title: "Instrument ID", hidden: true, width: 50 },
                    { field: "InstrumentName", title: "Instrument Name", hidden: true, width: 300 },
                    { field: "OrderPrice", title: "Order Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },

                    { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var gridEquitySellOnly = $("#gridSettlementInstructionEquitySellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                editable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionEquitySellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateEquitySellOnly").html()),
                excel: {
                    fileName: "SettlementEquitySellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsEquitySell }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateEquitySell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionEquitySellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbEquitySell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbEquitySell'></label>",
                        template: "<input type='checkbox' class='checkboxEquitySell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedEquitySell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedEquitySell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                    { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", title: "Stock" },
                    { field: "FundID", title: "Fund", },
                    { field: "CounterpartID", title: "Broker", },
                    { field: "BoardTypeDesc", title: "Type", },
                    {
                        field: "AvgPrice", title: "AvgPrice", format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "DonePrice", title: "Done Price", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CommissionAmount", title: "Comm.", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    //{ field: "KPEIAmount", title: "KPEI", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }



        gridEquitySellOnly.table.on("click", ".checkboxEquitySell", selectRowEquitySell);
        var oldPageSizeApproved = 0;


        $('#chbEquitySell').change(function (ev) {

            var grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowEquitySell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedEquitySell[dataItemZ.SettlementPK] = checked;
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


            var grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataEquitySellOnly(_checked, _settlementPK);

        }

    }

    function SelectDeselectDataEquitySellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Settlement",
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

    function InitgridSettlementInstructionBondBuyOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
          dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);

        if (_GlobClientCode == "20")
        {
            var gridBondBuyOnly = $("#gridSettlementInstructionBondBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionBondBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateBondBuyOnly").html()),
                excel: {
                    fileName: "SettlementBondBuyInstruction.xlsx"
                },
                columns: [
                     { command: { text: "Show", click: showDetailsBondBuy }, title: " ", width: 80 },
                     //{ command: { text: "R", click: RejectSettlementInstructionBondBuyOnly }, title: " ", width: 50 },
                     //{ command: { text: "A", click: ApprovedSettlementInstructionBondBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBondBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondBuy'></label>",
                        template: "<input type='checkbox' class='checkboxBondBuy'/>", width: 45
                    },
                     //{
                     //    field: "SelectedSettlement",
                     //    width: 50,
                     //    template: "<input class='cSelectedDetailApprovedBondBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                     //    headerTemplate: "<input id='SelectedAllApprovedBondBuy' type='checkbox'  />",
                     //    filterable: true,
                     //    sortable: false,
                     //    columnMenu: false
                     //},
                     { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                     { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                     {
                         hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                             style: "text-align: center"
                         }, attributes: { style: "text-align:center;" }
                     },
                     {
                         field: "StatusDesc", width: 70, title: "Status", headerAttributes: {
                             style: "text-align: center"
                         }, attributes: { style: "text-align:center;" }
                     },
                     { field: "SettledDate", title: "Settled Date", width: 90, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                     { field: "InstrumentID", width: 70, title: "Stock" },
                     { field: "FundID", width: 70, title: "Fund", },
                     { field: "CounterpartID", width: 70, title: "Broker", },
                     { field: "SettlementModeDesc", width: 70, title: "Mode", },
                     { field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                     { field: "DoneVolume", title: "Done Volume", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                     { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                     { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                     { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                     {
                         field: "DoneAmount", title: "Amount", width: 100, headerAttributes: {
                             style: "text-align: center"
                         }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                     },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax on Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax on Gain", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },

                    { field: "OtherInvestmentStyle", hidden: true, title: "OtherInvestmentStyle" },
                    { field: "OtherInvestmentObjective", hidden: true, title: "OtherInvestmentObjective" },
                    { field: "OtherRevision", hidden: true, title: "OtherRevision" },

                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var gridBondBuyOnly = $("#gridSettlementInstructionBondBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionBondBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateBondBuyOnly").html()),
                excel: {
                    fileName: "SettlementBondBuyInstruction.xlsx"
                },
                columns: [
                     { command: { text: "Show", click: showDetailsBondBuy }, title: " ", width: 80 },
                     //{ command: { text: "R", click: RejectSettlementInstructionBondBuyOnly }, title: " ", width: 50 },
                     //{ command: { text: "A", click: ApprovedSettlementInstructionBondBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBondBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondBuy'></label>",
                        template: "<input type='checkbox' class='checkboxBondBuy'/>", width: 45
                    },
                     //{
                     //    field: "SelectedSettlement",
                     //    width: 50,
                     //    template: "<input class='cSelectedDetailApprovedBondBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                     //    headerTemplate: "<input id='SelectedAllApprovedBondBuy' type='checkbox'  />",
                     //    filterable: true,
                     //    sortable: false,
                     //    columnMenu: false
                     //},
                     { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                     { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                     {
                         hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                             style: "text-align: center"
                         }, attributes: { style: "text-align:center;" }
                     },
                     {
                         field: "StatusDesc", width: 70, title: "Status", headerAttributes: {
                             style: "text-align: center"
                         }, attributes: { style: "text-align:center;" }
                     },
                     { field: "SettledDate", title: "Settled Date", width: 90, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                     { field: "InstrumentID", width: 70, title: "Stock" },
                     { field: "FundID", width: 70, title: "Fund", },
                     { field: "CounterpartID", width: 70, title: "Broker", },
                     { field: "SettlementModeDesc", width: 70, title: "Mode", },
                     { field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                     { field: "DoneVolume", title: "Done Volume", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                     { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                     { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                     { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                     {
                         field: "DoneAmount", title: "Amount", width: 100, headerAttributes: {
                             style: "text-align: center"
                         }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                     },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax on Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax on Gain", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        

        gridBondBuyOnly.table.on("click", ".checkboxBondBuy", selectRowBondBuy);
        var oldPageSizeApproved = 0;


        $('#chbBondBuy').change(function (ev) {

            var grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowBondBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedBondBuy[dataItemZ.SettlementPK] = checked;
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
            

            var grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataBondBuyOnly(_checked, _settlementPK);

        }

    }

    function SelectDeselectDataBondBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Settlement",
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

    function InitgridSettlementInstructionBondSellOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartBondSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);
        if (_GlobClientCode == "03") {
            var gridBondSellOnly = $("#gridSettlementInstructionBondSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionBondSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateBondSellOnly").html()),
                excel: {
                    fileName: "SettlementBondSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsBondSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBondSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondSell'></label>",
                        template: "<input type='checkbox' class='checkboxBondSell'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", width: 70, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "SettledDate", title: "Settled Date", width: 90, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                    { field: "InstrumentID", width: 70, title: "Stock" },
                    { field: "FundID", width: 70, title: "Fund", },
                    { field: "CounterpartID", width: 70, title: "Broker", },
                    { field: "SettlementModeDesc", width: 70, title: "Mode", },
                    { field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", width: 100, headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax on Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax on Gain", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataBondSellOnly(_checked, _settlementPK);

            }
        }
        else if (_GlobClientCode == "20") {
            var gridBondSellOnly = $("#gridSettlementInstructionBondSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionBondSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateBondSellOnly").html()),
                excel: {
                    fileName: "SettlementBondSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsBondSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBondSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondSell'></label>",
                        template: "<input type='checkbox' class='checkboxBondSell'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", width: 70, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", width: 70, title: "Stock" },
                    { field: "FundID", width: 70, title: "Fund", },
                    { field: "CounterpartID", width: 70, title: "Broker", },
                    { field: "SettlementModeDesc", width: 70, title: "Mode", },
                    { field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", width: 100, headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax on Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax on Gain", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },

                    { field: "OtherInvestmentStyle", hidden: true, title: "OtherInvestmentStyle" },
                    { field: "OtherInvestmentObjective", hidden: true, title: "OtherInvestmentObjective" },
                    { field: "OtherRevision", hidden: true, title: "OtherRevision" },

                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataBondSellOnly(_checked, _settlementPK);

            }
        }

        else {
            var gridBondSellOnly = $("#gridSettlementInstructionBondSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionBondSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateBondSellOnly").html()),
                excel: {
                    fileName: "SettlementBondSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsBondSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionBondSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBondSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBondSell'></label>",
                        template: "<input type='checkbox' class='checkboxBondSell'/>", width: 45
                    },
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", width: 70, title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "InstrumentID", width: 70, title: "Stock" },
                    { field: "FundID", width: 70, title: "Fund", },
                    { field: "CounterpartID", width: 70, title: "Broker", },
                    { field: "SettlementModeDesc", width: 70, title: "Mode", },
                    { field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "DoneVolume", title: "Done Volume", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", width: 100, headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                    },
                    { field: "CrossFundFromPK", hidden: true, title: "CrossFundFromPK", width: 50 },
                    { field: "DoneAccruedInterest", title: "Acc. Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax on Interest", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax on Gain", width: 100, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", format: "{0:n4}", width: 100, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
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


                var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                var _settlementPK = dataItemX.SettlementPK;
                var _checked = this.checked;
                SelectDeselectDataBondSellOnly(_checked, _settlementPK);

            }
        }
        gridBondSellOnly.table.on("click", ".checkboxBondSell", selectRowBondSell);
        var oldPageSizeApproved = 0;


        $('#chbBondSell').change(function (ev) {

            var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowBondSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedBondSell[dataItemZ.SettlementPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }
        }



    }

    function SelectDeselectDataBondSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Settlement",
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

    function InitgridSettlementInstructionTimeDepositBuyOnly() {
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

        if (_GlobClientCode == "17") {
            var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
                dataSourceApproved = getDataSourceCustomJarvis(SettlementInstructionApprovedURL);
        }

        else {
            var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
                dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);
        }

        if (_GlobClientCode == "05") {
            var gridTimeDepositBuyOnly = $("#gridSettlementInstructionTimeDepositBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositBuyOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoBuy'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 100 },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    { field: "FundID", title: "Fund", },
                    { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },

                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },

                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        else if (_GlobClientCode == "17") {

            var gridTimeDepositBuyOnly = $("#gridSettlementInstructionTimeDepositBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                //scrollable: {
                //    virtual: true
                //},
                pageable: false,
                height: "600px",
                editable: "incell",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositBuyOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositBuy }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateTimeDepositBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoBuy'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                        , width: 100
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 80 },
                    { field: "Category", title: "Category", width: 100 },
                    { field: "InstrumentID", title: "Bank", width: 80 },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 200
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "FundID", title: "Fund", width: 100 },
                    { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 80 },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                        , width: 100
                    },
                    {
                        field: "AmountToTransfer", title: "AmountToTransfer",
                        format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#", width: 100 },
                    {
                        field: "InterestPercent", title: "Interest Percent", width: 75,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }


        else {
            var gridTimeDepositBuyOnly = $("#gridSettlementInstructionTimeDepositBuyOnly").kendoGrid({
                dataSource: dataSourceApproved,
                //scrollable: {
                //    virtual: true
                //},
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositBuyOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositBuyOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositBuyInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositBuy }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositBuyOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoBuy'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoBuy'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositBuy' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                        , width: 100
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 80 },
                    { field: "Category", title: "Category", width: 100 },
                    { field: "InstrumentID", title: "Bank", width: 80 },
                    {
                        field: "BankBranchID", title: "Bank Branch", headerAttributes: {
                            style: "text-align: center"
                        }, width: 200
                    },
                    {
                        field: "PTPCode", title: "PTP Code", headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { field: "FundID", title: "Fund", width: 100 },
                    { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 80 },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                        , width: 100
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#", width: 100 },
                    {
                        field: "InterestPercent", title: "Interest Percent", width: 75,
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        gridTimeDepositBuyOnly.table.on("click", ".checkboxDepositoBuy", selectRowDepositoBuy);
        var oldPageSizeApproved = 0;


        $('#chbDepositoBuy').change(function (ev) {

            var grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowDepositoBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedDepositoBuy[dataItemZ.SettlementPK] = checked;
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


            var grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositBuyOnly(_checked, _settlementPK);

        }

    }
    
    function SelectDeselectDataTimeDepositBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Settlement",
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

    function InitgridSettlementInstructionTimeDepositSellOnly() {
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

        if (_GlobClientCode == "17") {
            var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
                dataSourceApproved = getDataSourceCustomJarvis(SettlementInstructionApprovedURL);
        }

        else {
            var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartTimeDepositSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
                dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);
        }


        if (_GlobClientCode == "05") {

            var gridTimeDepositSellOnly = $("#gridSettlementInstructionTimeDepositSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                scrollable: {
                    virtual: true
                },
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositSellOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoSell'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 100 },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    { field: "FundID", title: "Fund", },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },

                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },

                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        else if (_GlobClientCode == "17") {
            var gridTimeDepositSellOnly = $("#gridSettlementInstructionTimeDepositSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                //scrollable: {
                //    virtual: true
                //},
                editable: "incell",
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositSellOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositSell }, title: " ", width: 80 },
                    { command: { text: "Update", click: _updateTimeDepositSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoSell'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 100 },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    { field: "FundID", title: "Fund", },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "AmountToTransfer", title: "AmountToTransfer",
                        format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },

                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },

                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }


        else {
            var gridTimeDepositSellOnly = $("#gridSettlementInstructionTimeDepositSellOnly").kendoGrid({
                dataSource: dataSourceApproved,
                //scrollable: {
                //    virtual: true
                //},
                pageable: false,
                height: "600px",
                reorderable: true,
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                sortable: true,
                dataBound: gridSettlementInstructionTimeDepositSellOnlyDataBound,
                resizable: true,
                toolbar: kendo.template($("#gridSettlementInstructionTemplateTimeDepositSellOnly").html()),
                excel: {
                    fileName: "SettlementTimeDepositSellInstruction.xlsx"
                },
                columns: [
                    { command: { text: "Show", click: showDetailsTimeDepositSell }, title: " ", width: 80 },
                    //{ command: { text: "R", click: RejectSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    //{ command: { text: "A", click: ApprovedSettlementInstructionTimeDepositSellOnly }, title: " ", width: 50 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbDepositoSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbDepositoSell'></label>",
                        template: "<input type='checkbox' class='checkboxDepositoSell'/>", width: 45
                    },
                    //{
                    //    field: "SelectedSettlement",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailApprovedTimeDepositSell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllApprovedTimeDepositSell' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                    {
                        hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "StatusDesc", title: "Status", headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:center;" }
                    },
                    { field: "TrxTypeID", title: "Trx Type", width: 100 },
                    { field: "Category", title: "Category" },
                    { field: "InstrumentID", title: "Bank" },
                    { field: "FundID", title: "Fund", },
                    { hidden: true, field: "CounterpartID", title: "Counterpart", },
                    { hidden: true, field: "SettlementModeDesc", title: "Mode", },
                    { hidden: true, field: "DonePrice", title: "Done Price", width: 70, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "DoneVolume", title: "Done Volume", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                    { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                    {
                        field: "DoneAmount", title: "Amount", headerAttributes: {
                            style: "text-align: center"
                        }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                    },
                    { hidden: true, field: "DoneAccruedInterest", title: "Acc. Interest", width: 150, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "CommissionAmount", title: "Comm.", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "LevyAmount", title: "Levy", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "KPEIAmount", title: "KPEI", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "VATAmount", title: "VAT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "WHTAmount", title: "WHT", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxSellAmount", title: "Tax Sell", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "TotalAmount", title: "Total", hidden: true, format: "{0:n0}", width: 150, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                    { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                    { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                    { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                    { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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

                    { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },

                    { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "CounterpartID", title: "Counterpart ID", hidden: true, width: 50 },
                    { field: "CounterpartName", title: "Counterpart Name", hidden: true, width: 300 },
                    { field: "SettledDate", title: "Settled Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                    { field: "LastCouponDate", title: "Last Coupon Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MMM/yyyy')#" },
                    { field: "AcqPrice", title: "Acq Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                    {
                        field: "InterestPercent", title: "Interest Percent",
                        template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "BreakInterestPercent", title: "Interest Percent", hidden: true, width: 50,
                        template: "#: BreakInterestPercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "FundID", title: "Fund ID", hidden: true, width: 50 },
                    { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                    { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                    {
                        field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                        template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                    },
                    { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                    { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                    { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                    { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                    { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                    { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                    { field: "PurposeOfTransaction", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "PurposeOfTransactionDesc", title: "Purpose Of Transaction", hidden: true, width: 50 },
                    { field: "StatutoryType", title: "StatutoryType", hidden: true, width: 50 },

                    { field: "CPSafekeepingAccNumber", title: "CPSafekeeping AccNumber", hidden: true, width: 50 },
                    { field: "PlaceOfSettlement", title: "Place Of Settlement", hidden: true, width: 50 },
                    { field: "FundSafekeepingAccountNumber", title: "FundSafekeeping AccountNumber", hidden: true, width: 50 },
                    { field: "SecurityCodeType", title: "Security Code Type", hidden: true, width: 50 },
                    { field: "SecurityCodeTypeDesc", title: "Security Code Type", hidden: true, width: 50 },

                    { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                    { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                    { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                    { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                    { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                    { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }



        gridTimeDepositSellOnly.table.on("click", ".checkboxDepositoSell", selectRowDepositoSell);
        var oldPageSizeApproved = 0;


        $('#chbDepositoSell').change(function (ev) {

            var grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowDepositoSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedDepositoSell[dataItemZ.SettlementPK] = checked;
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

        //gridTimeDepositSellOnly.table.on("click", ".cSelectedDetailApprovedTimeDepositSell", selectDataApprovedTimeDepositSell);

        function selectDataApprovedTimeDepositSell(e) {


            var grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataTimeDepositSellOnly(_checked, _settlementPK);

        }

    }

    function SelectDeselectDataTimeDepositSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Settlement",
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

    function InitgridSettlementInstructionReksadanaBuyOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
          dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);


        var gridReksadanaBuyOnly = $("#gridSettlementInstructionReksadanaBuyOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridSettlementInstructionReksadanaBuyOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridSettlementInstructionTemplateReksadanaBuyOnly").html()),
            excel: {
                fileName: "SettlementReksadanaBuyInstruction.xlsx"
            },
            columns: [
                 { command: { text: "Show", click: showDetailsReksadanaBuy }, title: " ", width: 80 },
                 //{ command: { text: "R", click: RejectSettlementInstructionReksadanaBuyOnly }, title: " ", width: 50 },
                 //{ command: { text: "A", click: ApprovedSettlementInstructionReksadanaBuyOnly }, title: " ", width: 50 },
                {
                    headerTemplate: "<input type='checkbox' id='chbReksadanaBuy' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbReksadanaBuy'></label>",
                    template: "<input type='checkbox' class='checkboxReksadanaBuy'/>", width: 45
                },
                 //{
                 //    field: "SelectedSettlement",
                 //    width: 50,
                 //    template: "<input class='cSelectedDetailApprovedReksadanaBuy' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                 //    headerTemplate: "<input id='SelectedAllApprovedReksadanaBuy' type='checkbox'  />",
                 //    filterable: true,
                 //    sortable: false,
                 //    columnMenu: false
                 //},
                 { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                 { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                 { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                 { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                 {
                     hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 {
                     field: "StatusDesc", title: "Status", headerAttributes: {
                         style: "text-align: center"
                     }, attributes: { style: "text-align:center;" }
                 },
                 { field: "InstrumentID", title: "Stock" },
                 { field: "FundID", title: "Fund", },
                 { field: "CounterpartID", title: "Broker", },
                 { field: "BoardTypeDesc", title: "Type", },
                 { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                 //{ field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                 { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                 {
                     field: "DoneAmount", title: "Amount", headerAttributes: {
                         style: "text-align: center"
                     }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" }
                 },
                { field: "CommissionAmount", title: "Comm.", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "LevyAmount", title: "Levy", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                //{ field: "KPEIAmount", title: "KPEI", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "VATAmount", title: "VAT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "WHTAmount", title: "WHT", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxSellAmount", hidden: true, title: "Income Tax Sell", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxInterestAmount", hidden: true, title: "Income Tax Interest", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxGainAmount", hidden: true, title: "Income Tax Gain", format: "{0:n4}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", attributes: { style: "text-align:right;" } },
                { field: "TotalAmount", title: "Total", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 50 },
                { field: "ValueDate", title: "Value Date", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", hidden: true, width: 150 },
                { field: "SIReference", title: "SIReference", hidden: true, width: 50 },
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
                { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                {
                    field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                    template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridReksadanaBuyOnly.table.on("click", ".checkboxReksadanaBuy", selectRowReksadanaBuy);
        var oldPageSizeApproved = 0;


        $('#chbReksadanaBuy').change(function (ev) {

            var grid = $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid");

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
            console.log(checked + ' ' + checked.length);
        });

        function selectRowReksadanaBuy() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedReksadanaBuy[dataItemZ.SettlementPK] = checked;
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


            SelectDeselectAllDataReksadanaBuyOnly(_checked, "Approved");

        });

        //gridReksadanaBuyOnly.table.on("click", ".cSelectedDetailApprovedReksadanaBuy", selectDataApprovedReksadanaBuy);

        function selectDataApprovedReksadanaBuy(e) {


            var grid = $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaBuyOnly(_checked, _settlementPK);

        }

    }

    function SelectDeselectDataReksadanaBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Settlement",
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

    function InitgridSettlementInstructionReksadanaSellOnly() {
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
        var SettlementInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataSettlementByDateFromToByFundByCounterpartReksadanaSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _counterpartID,
            dataSourceApproved = getDataSource(SettlementInstructionApprovedURL);

        var gridReksadanaSellOnly = $("#gridSettlementInstructionReksadanaSellOnly").kendoGrid({
            dataSource: dataSourceApproved,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "600px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridSettlementInstructionReksadanaSellOnlyDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridSettlementInstructionTemplateReksadanaSellOnly").html()),
            excel: {
                fileName: "SettlementReksadanaSellInstruction.xlsx"
            },
            columns: [
                { command: { text: "Show", click: showDetailsReksadanaSell }, title: " ", width: 80 },
                //{ command: { text: "R", click: RejectSettlementInstructionReksadanaSellOnly }, title: " ", width: 50 },
                //{ command: { text: "A", click: ApprovedSettlementInstructionReksadanaSellOnly }, title: " ", width: 50 },
                {
                    headerTemplate: "<input type='checkbox' id='chbReksadanaSell' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbReksadanaSell'></label>",
                    template: "<input type='checkbox' class='checkboxReksadanaSell'/>", width: 45
                },
                //{
                //    field: "SelectedSettlement",
                //    width: 50,
                //    template: "<input class='cSelectedDetailApprovedReksadanaSell' type='checkbox'   #= SelectedSettlement ? 'checked=checked' : '' # />",
                //    headerTemplate: "<input id='SelectedAllApprovedReksadanaSell' type='checkbox'  />",
                //    filterable: true,
                //    sortable: false,
                //    columnMenu: false
                //},
                { field: "InvestmentPK", title: "InvestmentPK.", filterable: false, hidden: true },
                { field: "DealingPK", title: "DealingPK.", filterable: false, hidden: true },
                { field: "OrderStatus", title: "OrderStatus.", filterable: false, hidden: true },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                {
                    hidden: true, field: "OrderStatusDesc", title: "Order Status", headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:center;" }
                },
                {
                    field: "StatusDesc", title: "Status", headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:center;" }
                },
                { field: "InstrumentID", title: "Stock" },
                { field: "FundID", title: "Fund", },
                { field: "CounterpartID", title: "Broker", },
                { field: "BoardTypeDesc", title: "Type", },
                { field: "DonePrice", title: "Done Price", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneLot", title: "Done Lot", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { hidden: true, field: "FundCashRefID", title: "Cash Ref", },
                { hidden: true, field: "UpdateSettlementTime", title: "Open Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                { hidden: true, field: "ApprovedSettlementTime", title: "Done Time", format: "{0:MM/dd/yyyy HH:mm:ss}" },
                {
                    field: "DoneAmount", title: "Amount", headerAttributes: {
                        style: "text-align: center"
                    }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }
                },
                { field: "CommissionAmount", title: "Comm.", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "LevyAmount", title: "Levy", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                //{ field: "KPEIAmount", title: "KPEI", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "VATAmount", title: "VAT", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "WHTAmount", title: "WHT", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "OTCAmount", title: "OTC", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxSellAmount", title: "Tax Sell", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxInterestAmount", title: "Tax Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "IncomeTaxGainAmount", title: "Tax Gain", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "TotalAmount", title: "Total", format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                { field: "StatusSettlement", title: "Status", hidden: true, filterable: false, width: 100 },
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
                { field: "Lot", title: "Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", title: "Done Price", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "DoneLot", title: "Done Lot", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneAmount", title: "Done Amount", hidden: true, width: 50, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
                { field: "AcqVolume", title: "Acq Volume", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice1", title: "Acq Price 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume1", title: "Acq Volume 1", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate1", title: "Acq Date 1", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'MM/dd/yyyy')#" },
                { field: "AcqPrice2", title: "Acq Price 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume2", title: "Acq Volume 2", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate2", title: "Acq Date 2", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'MM/dd/yyyy')#" },
                { field: "AcqPrice3", title: "Acq Price 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume3", title: "Acq Volume 3", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate3", title: "Acq Date 3", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'MM/dd/yyyy')#" },
                { field: "AcqPrice4", title: "Acq Price 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume4", title: "Acq Volume 4", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate4", title: "Acq Date 4", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'MM/dd/yyyy')#" },
                { field: "AcqPrice5", title: "Acq Price 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume5", title: "Acq Volume 5", hidden: true, width: 50, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate5", title: "Acq Date 5", hidden: true, width: 50, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'MM/dd/yyyy')#" },
                { field: "DoneAccruedInterest", title: "Acc. Interest", hidden: true, format: "{0:n0}", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", attributes: { style: "text-align:right;" } },
                {
                    field: "TaxExpensePercent", title: "Tax Expense Percent", hidden: true, width: 50,
                    template: "#: TaxExpensePercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 50 },
                { field: "PriceMode", title: "Price Mode", hidden: true, width: 50 },
                { field: "BitBreakable", title: "BitBreakable", hidden: true, width: 50 },
                { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 50 },
                { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 50 },
                { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 50 },
                { field: "EntryUsersID", title: "Entry Users ID", hidden: true, width: 50 },
                { field: "EntryTime", title: "E. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "Approved Users ID", hidden: true, width: 50 },
                { field: "ApprovedTime", title: "A. Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "EntrySettlementID", title: "Entry Settlement ID", hidden: true, width: 50 },
                { field: "EntrySettlementTime", title: "E. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateSettlementID", title: "Update Settlement ID", hidden: true, width: 50 },
                { field: "ApprovedSettlementID", title: "Approved Settlement ID", hidden: true, width: 50 },
                { field: "ApprovedSettlementTime", title: "A. Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidSettlementID", title: "Void Settlement ID", hidden: true, width: 50 },
                { field: "VoidSettlementTime", title: "V.Settlement Time", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", hidden: true, format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

        gridReksadanaSellOnly.table.on("click", ".checkboxReksadanaSell", selectRowReksadanaSell);
        var oldPageSizeApproved = 0;


        $('#chbReksadanaSell').change(function (ev) {

            var grid = $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid");

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
            for (var i in checkedReksadanaSell) {
                if (checkedReksadanaSell[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });


        function selectRowReksadanaSell() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedReksadanaSell[dataItemZ.SettlementPK] = checked;
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


            SelectDeselectAllDataReksadanaSellOnly(_checked, "Approved");

        });

        //gridReksadanaSellOnly.table.on("click", ".cSelectedDetailApprovedReksadanaSell", selectDataApprovedReksadanaSell);

        function selectDataApprovedReksadanaSell(e) {


            var grid = $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _settlementPK = dataItemX.SettlementPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaSellOnly(_checked, _settlementPK);

        }

    }

    function SelectDeselectDataReksadanaSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataSettlementReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Settlement",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Settlement",
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

    function ApprovedSettlementInstructionBondBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Approved, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Approved data?", function (a) {
                if (a) {
                    var grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");
                    var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                    if (_dataItem.StatusSettlement == 2) {
                        alertify.alert("Data Already Match");
                        return;
                    }
                    if (_dataItem.StatusSettlement == 3) {
                        alertify.alert("Data Already Void");
                        return;
                    }
                    if (_dataItem.CounterpartID == "") {
                        alertify.alert("Data Must Be Fill Broker First");
                        return;
                    }
                    if (_dataItem.FundCashRefID == "") {
                        alertify.alert("Data Must Be Fill Cash Ref First");
                        return;
                    }

                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + _dataItem.SettlementPK + "/" + _dataItem.HistoryPK + "/" + "Settlement",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                    var SettlementInstruction = {
                        InvestmentPK: _dataItem.InvestmentPK,
                        SettlementPK: _dataItem.SettlementPK,
                        HistoryPK: _dataItem.HistoryPK,
                        InstrumentTypePK: _dataItem.InstrumentTypePK,
                        CounterpartPK: _dataItem.CounterpartPK,
                        Notes: str,
                        ApprovedSettlementID: sessionStorage.getItem("user"),
                        VoidSettlementID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                        type: 'POST',
                        data: JSON.stringify(SettlementInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshBondBuy();


                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });


                }

            });
        }
        e.handled = true;

    }

    function RejectSettlementInstructionBondBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Reject, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Reject data?", function (a) {
                if (a) {
                    var grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");
                    var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                    if (_dataItem.StatusSettlement == 3) {
                        alertify.alert("Data Already Reject");
                        return;
                    }
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + _dataItem.SettlementPK + "/" + _dataItem.HistoryPK + "/" + "Settlement",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                    var SettlementInstruction = {
                        InvestmentPK: _dataItem.InvestmentPK,
                        SettlementPK: _dataItem.SettlementPK,
                        HistoryPK: _dataItem.HistoryPK,
                        StatusSettlement: _dataItem.StatusSettlement,
                        Notes: str,
                        VoidUsersID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_R",
                        type: 'POST',
                        data: JSON.stringify(SettlementInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshBondBuy();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                    //        } else {
                    //            alertify.alert("Data has been Changed by other user, Please check it first!");
                    //            win.close();
                    //            refresh();
                    //        }
                    //    },
                    //    error: function (data) {
                    //        alertify.alert(data.responseText);
                    //    }
                    //});
                }

            });
        }
        e.handled = true;

    }

    function ApprovedSettlementInstructionBondSellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Match, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Approved data?", function (a) {
                if (a) {
                    var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                    var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                    if (_dataItem.StatusSettlement == 2) {
                        alertify.alert("Data Already Match");
                        return;
                    }
                    if (_dataItem.StatusSettlement == 3) {
                        alertify.alert("Data Already Void");
                        return;
                    }
                    if (_dataItem.CounterpartID == "") {
                        alertify.alert("Data Must Be Fill Broker First");
                        return;
                    }
                    if (_dataItem.FundCashRefID == "") {
                        alertify.alert("Data Must Be Fill Cash Ref First");
                        return;
                    }

                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + _dataItem.SettlementPK + "/" + _dataItem.HistoryPK + "/" + "Settlement",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                    var SettlementInstruction = {
                        InvestmentPK: _dataItem.InvestmentPK,
                        SettlementPK: _dataItem.SettlementPK,
                        HistoryPK: _dataItem.HistoryPK,
                        InstrumentTypePK: _dataItem.InstrumentTypePK,
                        CounterpartPK: _dataItem.CounterpartPK,
                        Notes: str,
                        ApprovedSettlementID: sessionStorage.getItem("user"),
                        VoidSettlementID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                        type: 'POST',
                        data: JSON.stringify(SettlementInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshBondSell();


                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }

            });
        }
        e.handled = true;

    }

    function RejectSettlementInstructionBondSellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Reject, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Reject data?", function (a) {
                if (a) {
                    var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
                    var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                    if (_dataItem.StatusSettlement == 3) {
                        alertify.alert("Data Already Reject");
                        return;
                    }
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + _dataItem.SettlementPK + "/" + _dataItem.HistoryPK + "/" + "Settlement",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                    var SettlementInstruction = {
                        InvestmentPK: _dataItem.InvestmentPK,
                        SettlementPK: _dataItem.SettlementPK,
                        HistoryPK: _dataItem.HistoryPK,
                        StatusSettlement: _dataItem.StatusSettlement,
                        Notes: str,
                        VoidUsersID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_R",
                        type: 'POST',
                        data: JSON.stringify(SettlementInstruction),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshBondSell();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                    //        } else {
                    //            alertify.alert("Data has been Changed by other user, Please check it first!");
                    //            win.close();
                    //            refresh();
                    //        }
                    //    },
                    //    error: function (data) {
                    //        alertify.alert(data.responseText);
                    //    }
                    //});
                }

            });
        }
        e.handled = true;

    }

    function ApprovedSettlementInstructionTimeDepositBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Approved, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Approved data?", function (a) {
                if (a) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");
                                var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                                if (_dataItem.StatusSettlement == 2) {
                                    alertify.alert("Data Already Approved");
                                    return;
                                }
                                var SettlementInstruction = {
                                    InvestmentPK: _dataItem.InvestmentPK,
                                    SettlementPK: _dataItem.SettlementPK,
                                    HistoryPK: _dataItem.HistoryPK,
                                    StatusSettlement: _dataItem.StatusSettlement,
                                    Notes: str,
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    VoidSettlementID: sessionStorage.getItem("user")
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
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
        e.handled = true;

    }

    function RejectSettlementInstructionTimeDepositBuyOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Reject, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Reject data?", function (a) {
                if (a) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");
                                var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                                if (_dataItem.StatusSettlement == 2) {
                                    alertify.alert("Data Already Reject");
                                    return;
                                }
                                var SettlementInstruction = {
                                    InvestmentPK: _dataItem.InvestmentPK,
                                    SettlementPK: _dataItem.SettlementPK,
                                    HistoryPK: _dataItem.HistoryPK,
                                    StatusSettlement: _dataItem.StatusSettlement,
                                    Notes: str,
                                    RejectSettlementID: sessionStorage.getItem("user"),
                                    VoidSettlementID: sessionStorage.getItem("user")
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
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
        e.handled = true;

    }

    function ApprovedSettlementInstructionTimeDepositSellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Approved, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Approved data?", function (a) {
                if (a) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");
                                var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                                if (_dataItem.StatusSettlement == 2) {
                                    alertify.alert("Data Already Approved");
                                    return;
                                }
                                var SettlementInstruction = {
                                    InvestmentPK: _dataItem.InvestmentPK,
                                    SettlementPK: _dataItem.SettlementPK,
                                    HistoryPK: _dataItem.HistoryPK,
                                    StatusSettlement: _dataItem.StatusSettlement,
                                    Notes: str,
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    VoidSettlementID: sessionStorage.getItem("user")
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
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
        e.handled = true;

    }

    function RejectSettlementInstructionTimeDepositSellOnly(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.prompt("Are you sure want to Reject, please give notes:", function (a, str) {
                //alertify.confirm("Are you sure want to Reject data?", function (a) {
                if (a) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");
                                var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                                if (_dataItem.StatusSettlement == 2) {
                                    alertify.alert("Data Already Reject");
                                    return;
                                }
                                var SettlementInstruction = {
                                    InvestmentPK: _dataItem.InvestmentPK,
                                    SettlementPK: _dataItem.SettlementPK,
                                    HistoryPK: _dataItem.HistoryPK,
                                    StatusSettlement: _dataItem.StatusSettlement,
                                    Notes: str,
                                    RejectSettlementID: sessionStorage.getItem("user"),
                                    VoidSettlementID: sessionStorage.getItem("user")
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
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
        e.handled = true;

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

    $("#BtnUpdate").click(function () {
        if ($('#TrxType').val() == 1 && ($('#CrossFundFromPK').val() != 0 || $('#CrossFundFromPK').val() != "")) {
            alertify.alert("Please Update Cross Fund From Trx Sell !");
            return;
        }
        var val = validateData();
        if ($("#InstrumentTypePK").val() == 2 || $("#InstrumentTypePK").val() == 3) {
            //DonePrice = $('#DonePrice').val() / 100
            ClearDataBond();
        }
        else if (GlobInstrumentType == 1) {
            ClearDataEquity();
        }
        else if ($("#InstrumentTypePK").val() == 5) {
            ClearDataMoney();
        }
        if ($('#StatusSettlement').val() == 2) {
            alertify.alert("Data Already Match");
            return;
        }
        if ($('#StatusSettlement').val() == 3) {
            alertify.alert("Data Already Void");
            return;
        }

        if (val == 1) {

            //$.ajax({
            //    url: window.location.origin + "/Radsoft/CounterpartCommission/ValidateUpdateSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CounterpartPK").val() + "/" + $("#BoardType").val(),
            //    type: 'GET',
            //    contentType: "application/json;charset=utf-8",
            //    success: function (data) {
            //        if (data == true) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var _instrument, _reference;

                                if (($('#InstrumentTypePK').val() == 5 || $('#InstrumentTypePK').val() == 10) && ($('#TrxType').val() == 1 || $('#TrxType').val() == 3)) {

                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            _instrument = data;



                                            //var Settlement = {
                                            //    Reference: $('#Reference').val()
                                            //};

                                            //$.ajax({
                                            //    url: window.location.origin + "/Radsoft/Investment/GetReferenceForDepoAmmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            //    type: 'POST',
                                            //    data: JSON.stringify(Settlement),
                                            //    contentType: "application/json;charset=utf-8",
                                            //    success: function (data) {
                                            //        _reference = data;




                                            var SettlementInstruction = {
                                                InvestmentPK: $('#InvestmentPK').val(),
                                                DealingPK: $('#DealingPK').val(),
                                                SettlementPK: $('#SettlementPK').val(),
                                                StatusDealing: $('#StatusDealing').val(),
                                                StatusSettlement: $('#StatusSettlement').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                PeriodPK: $('#PeriodPK').val(),
                                                ValueDate: $('#ValueDate').val(),
                                                Reference: $('#Reference').val(),
                                                SIReference: $('#SIReference').val(),
                                                InstructionDate: $('#InstructionDate').val(),
                                                InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                InstrumentPK: _instrument,
                                                Type: GlobInstrumentType,
                                                OrderPrice: $('#OrderPrice').val(),
                                                RangePrice: $('#RangePrice').val(),
                                                AcqPrice: $('#AcqPrice').val(),
                                                Lot: $('#Lot').val(),
                                                LotInShare: 100,
                                                Volume: $('#Volume').val(),
                                                Amount: $('#Amount').val(),
                                                DoneLot: $('#DoneLot').val(),
                                                DoneVolume: $('#DoneVolume').val(),
                                                DonePrice: $('#DonePrice').val(),
                                                DoneAmount: $('#DoneAmount').val(),
                                                TrxType: $('#TrxType').val(),
                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                CounterpartPK: $('#CounterpartPK').val(),
                                                SettledDate: $('#SettledDate').val(),
                                                LastCouponDate: $('#LastCouponDate').val(),
                                                NextCouponDate: $('#NextCouponDate').val(),
                                                MaturityDate: $('#MaturityDate').val(),
                                                AcqDate: $('#AcqDate').val(),
                                                AcqVolume: $('#AcqVolume').val(),
                                                InterestPercent: $('#InterestPercent').val(),
                                                BreakInterestPercent: $('#BreakInterestPercent').val(),
                                                AccruedInterest: $('#AccruedInterest').val(),
                                                DoneAccruedInterest: $('#DoneAccruedInterest').val(),
                                                FundPK: $('#FundPK').val(),
                                                FundCashRefPK: $('#FundCashRefPK').val(),
                                                Tenor: $('#Tenor').val(),
                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                CommissionPercent: $('#CommissionPercent').val(),
                                                LevyPercent: $('#LevyPercent').val(),
                                                KPEIPercent: $('#KPEIPercent').val(),
                                                VATPercent: $('#VATPercent').val(),
                                                WHTPercent: $('#WHTPercent').val(),
                                                OTCPercent: $('#OTCPercent').val(),
                                                IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                                                IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                                                IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                                                CommissionAmount: $('#CommissionAmount').val(),
                                                LevyAmount: $('#LevyAmount').val(),
                                                KPEIAmount: $('#KPEIAmount').val(),
                                                VATAmount: $('#VATAmount').val(),
                                                WHTAmount: $('#WHTAmount').val(),
                                                OTCAmount: $('#OTCAmount').val(),
                                                IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                                IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                TotalAmount: $('#TotalAmount').val(),
                                                CurrencyRate: $('#CurrencyRate').val(),
                                                SettlementMode: $('#SettlementMode').val(),
                                                BoardType: $('#BoardType').val(),
                                                AcqPrice1: $('#AcqPrice1').val(),
                                                AcqVolume1: $('#AcqVolume1').val(),
                                                AcqDate1: $('#AcqDate1').val(),
                                                AcqPrice2: $('#AcqPrice2').val(),
                                                AcqVolume2: $('#AcqVolume2').val(),
                                                AcqDate2: $('#AcqDate2').val(),
                                                AcqPrice3: $('#AcqPrice3').val(),
                                                AcqVolume3: $('#AcqVolume3').val(),
                                                AcqDate3: $('#AcqDate3').val(),
                                                AcqPrice4: $('#AcqPrice4').val(),
                                                AcqVolume4: $('#AcqVolume4').val(),
                                                AcqDate4: $('#AcqDate4').val(),
                                                AcqPrice5: $('#AcqPrice5').val(),
                                                AcqVolume5: $('#AcqVolume5').val(),
                                                AcqDate5: $('#AcqDate5').val(),
                                                AcqPrice6: $('#AcqPrice6').val(),
                                                AcqVolume6: $('#AcqVolume6').val(),
                                                AcqDate6: $('#AcqDate6').val(),
                                                AcqPrice7: $('#AcqPrice7').val(),
                                                AcqVolume7: $('#AcqVolume7').val(),
                                                AcqDate7: $('#AcqDate7').val(),
                                                AcqPrice8: $('#AcqPrice8').val(),
                                                AcqVolume8: $('#AcqVolume8').val(),
                                                AcqDate8: $('#AcqDate8').val(),
                                                AcqPrice9: $('#AcqPrice9').val(),
                                                AcqVolume9: $('#AcqVolume9').val(),
                                                AcqDate9: $('#AcqDate9').val(),
                                                TaxExpensePercent: $('#TaxExpensePercent').val(),
                                                Category: $('#Category').val(),
                                                BankBranchPK: $('#BankBranchPK').val(),
                                                CrossFundFromPK: $('#CrossFundFromPK').val(),
                                                PriceMode: $('#PriceMode').val(),
                                                BitBreakable: $('#BitBreakable').val(),
                                                InterestDaysType: $('#InterestDaysType').val(),
                                                InterestPaymentType: $('#InterestPaymentType').val(),
                                                PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                                PurposeOfTransaction: $('#PurposeOfTransaction').val(),
                                                StatutoryType: $('#StatutoryType').val(),
                                                BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                                CPSafekeepingAccNumber: $('#CPSafekeepingAccNumber').val(),
                                                PlaceOfSettlement: $('#PlaceOfSettlement').val(),
                                                FundSafekeepingAccountNumber: $('#FundSafekeepingAccountNumber').val(),
                                                SecurityCodeType: $('#SecurityCodeType').val(),
                                                AmountToTransfer: $('#AmountToTransfer').val(),
                                                BIRate: $('#BIRate').val(),
                                                InvestmentStrategy: $('#InvestmentStrategy').val(),
                                                InvestmentStyle: $('#InvestmentStyle').val(),
                                                InvestmentObjective: $('#InvestmentObjective').val(),
                                                Revision: $('#Revision').val(),

                                                OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                                OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                                OtherRevision: $('#OtherRevision').val(),
                                                Notes: str,
                                                EntrySettlementID: sessionStorage.getItem("user"),
                                                UpdateSettlementID: sessionStorage.getItem("user"),
                                                ApprovedSettlementID: sessionStorage.getItem("user"),
                                                InvestmentTrType: $('#InvestmentTrType').val(),
                                            };

                                            console.log(_instrument, + ' - ' + _reference);
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_U",
                                                type: 'POST',
                                                data: JSON.stringify(SettlementInstruction),
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
                                else {
                                    var SettlementInstruction = {
                                        InvestmentPK: $('#InvestmentPK').val(),
                                        DealingPK: $('#DealingPK').val(),
                                        SettlementPK: $('#SettlementPK').val(),
                                        StatusDealing: $('#StatusDealing').val(),
                                        StatusSettlement: $('#StatusSettlement').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        PeriodPK: $('#PeriodPK').val(),
                                        ValueDate: $('#ValueDate').val(),
                                        Reference: $('#Reference').val(),
                                        SIReference: $('#SIReference').val(),
                                        InstructionDate: $('#InstructionDate').val(),
                                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                                        InstrumentPK: $('#InstrumentPK').val(),
                                        Type: GlobInstrumentType,
                                        OrderPrice: $('#OrderPrice').val(),
                                        RangePrice: $('#RangePrice').val(),
                                        AcqPrice: $('#AcqPrice').val(),
                                        Lot: $('#Lot').val(),
                                        LotInShare: 100,
                                        Volume: $('#Volume').val(),
                                        Amount: $('#Amount').val(),
                                        DoneLot: $('#DoneLot').val(),
                                        DoneVolume: $('#DoneVolume').val(),
                                        DonePrice: $('#DonePrice').val(),
                                        DoneAmount: $('#DoneAmount').val(),
                                        TrxType: $('#TrxType').val(),
                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                        CounterpartPK: $('#CounterpartPK').val(),
                                        SettledDate: $('#SettledDate').val(),
                                        LastCouponDate: $('#LastCouponDate').val(),
                                        NextCouponDate: $('#NextCouponDate').val(),
                                        MaturityDate: $('#MaturityDate').val(),
                                        AcqDate: $('#AcqDate').val(),
                                        AcqVolume: $('#AcqVolume').val(),
                                        InterestPercent: $('#InterestPercent').val(),
                                        BreakInterestPercent: $('#BreakInterestPercent').val(),
                                        AccruedInterest: $('#AccruedInterest').val(),
                                        DoneAccruedInterest: $('#DoneAccruedInterest').val(),
                                        FundPK: $('#FundPK').val(),
                                        FundCashRefPK: $('#FundCashRefPK').val(),
                                        Tenor: $('#Tenor').val(),
                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                        CommissionPercent: $('#CommissionPercent').val(),
                                        LevyPercent: $('#LevyPercent').val(),
                                        KPEIPercent: $('#KPEIPercent').val(),
                                        VATPercent: $('#VATPercent').val(),
                                        WHTPercent: $('#WHTPercent').val(),
                                        OTCPercent: $('#OTCPercent').val(),
                                        IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                                        IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                                        IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                                        CommissionAmount: $('#CommissionAmount').val(),
                                        LevyAmount: $('#LevyAmount').val(),
                                        KPEIAmount: $('#KPEIAmount').val(),
                                        VATAmount: $('#VATAmount').val(),
                                        WHTAmount: $('#WHTAmount').val(),
                                        OTCAmount: $('#OTCAmount').val(),
                                        IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                        IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                        IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                        TotalAmount: $('#TotalAmount').val(),
                                        CurrencyRate: $('#CurrencyRate').val(),
                                        SettlementMode: $('#SettlementMode').val(),
                                        BoardType: $('#BoardType').val(),
                                        AcqPrice1: $('#AcqPrice1').val(),
                                        AcqVolume1: $('#AcqVolume1').val(),
                                        AcqDate1: $('#AcqDate1').val(),
                                        AcqPrice2: $('#AcqPrice2').val(),
                                        AcqVolume2: $('#AcqVolume2').val(),
                                        AcqDate2: $('#AcqDate2').val(),
                                        AcqPrice3: $('#AcqPrice3').val(),
                                        AcqVolume3: $('#AcqVolume3').val(),
                                        AcqDate3: $('#AcqDate3').val(),
                                        AcqPrice4: $('#AcqPrice4').val(),
                                        AcqVolume4: $('#AcqVolume4').val(),
                                        AcqDate4: $('#AcqDate4').val(),
                                        AcqPrice5: $('#AcqPrice5').val(),
                                        AcqVolume5: $('#AcqVolume5').val(),
                                        AcqDate5: $('#AcqDate5').val(),
                                        AcqPrice6: $('#AcqPrice6').val(),
                                        AcqVolume6: $('#AcqVolume6').val(),
                                        AcqDate6: $('#AcqDate6').val(),
                                        AcqPrice7: $('#AcqPrice7').val(),
                                        AcqVolume7: $('#AcqVolume7').val(),
                                        AcqDate7: $('#AcqDate7').val(),
                                        AcqPrice8: $('#AcqPrice8').val(),
                                        AcqVolume8: $('#AcqVolume8').val(),
                                        AcqDate8: $('#AcqDate8').val(),
                                        AcqPrice9: $('#AcqPrice9').val(),
                                        AcqVolume9: $('#AcqVolume9').val(),
                                        AcqDate9: $('#AcqDate9').val(),
                                        TaxExpensePercent: $('#TaxExpensePercent').val(),
                                        Category: $('#Category').val(),
                                        BankBranchPK: $('#BankBranchPK').val(),
                                        CrossFundFromPK: $('#CrossFundFromPK').val(),
                                        PriceMode: $('#PriceMode').val(),
                                        BitBreakable: $('#BitBreakable').val(),
                                        InterestDaysType: $('#InterestDaysType').val(),
                                        InterestPaymentType: $('#InterestPaymentType').val(),
                                        PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                        PurposeOfTransaction: $('#PurposeOfTransaction').val(),
                                        StatutoryType: $('#StatutoryType').val(),
                                        BitForeignTrx: $('#BitForeignTrx').is(":checked"),
                                        CPSafekeepingAccNumber: $('#CPSafekeepingAccNumber').val(),
                                        PlaceOfSettlement: $('#PlaceOfSettlement').val(),
                                        FundSafekeepingAccountNumber: $('#FundSafekeepingAccountNumber').val(),
                                        SecurityCodeType: $('#SecurityCodeType').val(),
                                        AmountToTransfer: $('#AmountToTransfer').val(),
                                        BIRate: $('#BIRate').val(),
                                        InvestmentStrategy: $('#InvestmentStrategy').val(),
                                        InvestmentStyle: $('#InvestmentStyle').val(),
                                        InvestmentObjective: $('#InvestmentObjective').val(),
                                        Revision: $('#Revision').val(),

                                        OtherInvestmentStyle: $('#OtherInvestmentStyle').val(),
                                        OtherInvestmentObjective: $('#OtherInvestmentObjective').val(),
                                        OtherRevision: $('#OtherRevision').val(),
                                        Notes: str,
                                        EntrySettlementID: sessionStorage.getItem("user"),
                                        UpdateSettlementID: sessionStorage.getItem("user"),
                                        ApprovedSettlementID: sessionStorage.getItem("user"),
                                        InvestmentTrType: $('#InvestmentTrType').val(),
                                    };

                                    console.log(_instrument, + ' - ' + _reference);
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_U",
                                        type: 'POST',
                                        data: JSON.stringify(SettlementInstruction),
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
            //        } else {
            //            alertify.alert("Update Cancel, Please Choose Counterpart and Board Type Correctly!");
            //        }
            //    },
            //    error: function (data) {
            //        alertify.alert(data.responseText);
            //    }
            //});
        }

    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
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
        
        var SettlementInstruction = {
            InstrumentTypePK: $('#InstrumentTypePK').val(),
            SettlementPK: $('#SettlementPK').val(),
            HistoryPK: $('#HistoryPK').val()
        };
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Investment/Settlement_ApproveValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'POST',
        //    data: JSON.stringify(SettlementInstruction),
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        if (data == 1) {
        //            alertify.alert("Validation Approve not Pass");
        //        } else {
                    alertify.confirm("Are you sure want to Approved data?", function (e) {
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                        var SettlementInstruction = {
                                            StatusSettlement: $('#StatusSettlement').val(),
                                            InvestmentPK: $('#InvestmentPK').val(),
                                            SettlementPK: $('#SettlementPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedSettlementID: sessionStorage.getItem("user"),
                                            VoidSettlementID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_A",
                                            type: 'POST',
                                            data: JSON.stringify(SettlementInstruction),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var SettlementInstruction = {
                                    StatusSettlement: $('#StatusSettlement').val(),
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    SettlementPK: $('#SettlementPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    VoidSettlementID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_V",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var SettlementInstruction = {
                                StatusSettlement: $('#StatusSettlement').val(),
                                InvestmentPK: $('#InvestmentPK').val(),
                                SettlementPK: $('#SettlementPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidSettlementID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_R",
                                type: 'POST',
                                data: JSON.stringify(SettlementInstruction),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SettlementPK").val() + "/" + $("#HistoryPK").val() + "/" + "Settlement",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                var SettlementInstruction = {
                                    StatusSettlement: $('#StatusSettlement').val(),
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    SettlementPK: $('#SettlementPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_UnApproved",
                                    type: 'POST',
                                    data: JSON.stringify(SettlementInstruction),
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


    //function GetBondInterest() {
    //    var InvestmentInstruction = {
    //        InstrumentPK: $('#InstrumentPK').val(),
    //        SettledDate: $('#SettledDate').val(),
    //        Volume: $("#DoneVolume").data("kendoNumericTextBox").value(),
    //        OrderPrice: $("#DonePrice").data("kendoNumericTextBox").value(),
    //        LastCouponDate: $('#LastCouponDate').val()

    //    };
    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/Investment/Investment_GetBondInterest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //        type: 'POST',
    //        data: JSON.stringify(InvestmentInstruction),
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {

    //            $("#DoneAccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
    //            $("#Tenor").data("kendoNumericTextBox").value(data.Tenor);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });
    //}

    function SettlementRecalculate(_mode) {
        console.log($("#DoneAccruedInterest").data("kendoNumericTextBox").value() + ',' + $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() + ',' + $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value() + ',' + _a);

        if ($("#CounterpartPK").val() == 0 || $("#CounterpartPK").val() == null) {
            return;
        }
        if ($("#InstrumentTypePK").val() == 2 || $("#InstrumentTypePK").val() == 3 || $("#InstrumentTypePK").val() == 9 || $("#InstrumentTypePK").val() == 13 || $("#InstrumentTypePK").val() == 15) {

            var _a = $("#DoneAccruedInterest").data("kendoNumericTextBox").value() - $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() - $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value();
            $("#TotalAmount").data("kendoNumericTextBox").value($("#DoneAmount").data("kendoNumericTextBox").value() + _a);
            return;
        }
        if ($("#InstrumentTypePK").val() == 5) {
            $("#TotalAmount").data("kendoNumericTextBox").value($("#DoneAmount").data("kendoNumericTextBox").value());
            return;
        }
        else {


            var SettlementRecalculate = {
                CommissionAmount: $("#CommissionAmount").data("kendoNumericTextBox").value(),
                LevyAmount: $("#LevyAmount").data("kendoNumericTextBox").value(),
                KPEIAmount: $("#KPEIAmount").data("kendoNumericTextBox").value(),
                VATAmount: $("#VATAmount").data("kendoNumericTextBox").value(),
                WHTAmount: $("#WHTAmount").data("kendoNumericTextBox").value(),
                OTCAmount: $("#OTCAmount").data("kendoNumericTextBox").value(),
                TaxSellAmount: $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(),
                TaxInterestAmount: $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(),
                TaxGainAmount: $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(),
                CommissionPercent: $("#CommissionPercent").data("kendoNumericTextBox").value(),
                LevyPercent: $("#LevyPercent").data("kendoNumericTextBox").value(),
                KPEIPercent: $("#KPEIPercent").data("kendoNumericTextBox").value(),
                VATPercent: $("#VATPercent").data("kendoNumericTextBox").value(),
                WHTPercent: $("#WHTPercent").data("kendoNumericTextBox").value(),
                OTCPercent: $("#OTCPercent").data("kendoNumericTextBox").value(),
                TaxSellPercent: $("#IncomeTaxSellPercent").data("kendoNumericTextBox").value(),
                TaxInterestPercent: $("#IncomeTaxInterestPercent").data("kendoNumericTextBox").value(),
                TaxGainPercent: $("#IncomeTaxGainPercent").data("kendoNumericTextBox").value(),
                DoneAmount: $('#DoneAmount').val(),
                CounterpartPK: $('#CounterpartPK').val(),
                BoardType: $("#BoardType").data("kendoComboBox").value(),
                TrxType: $('#TrxType').val(),
                CurrencyRate: $('#CurrencyRate').val(),
                Mode: _mode
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/SettlementRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(SettlementRecalculate),
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    ResetCommission();
                    $("#CommissionPercent").data("kendoNumericTextBox").value(data.CommissionPercent);
                    $("#LevyPercent").data("kendoNumericTextBox").value(data.LevyPercent);
                    $("#KPEIPercent").data("kendoNumericTextBox").value(data.KPEIPercent);
                    $("#VATPercent").data("kendoNumericTextBox").value(data.VATPercent);
                    $("#WHTPercent").data("kendoNumericTextBox").value(data.WHTPercent);
                    $("#OTCPercent").data("kendoNumericTextBox").value(data.OTCPercent);
                    $("#IncomeTaxSellPercent").data("kendoNumericTextBox").value(data.TaxSellPercent);
                    $("#IncomeTaxInterestPercent").data("kendoNumericTextBox").value(data.TaxInterestPercent);
                    $("#IncomeTaxGainPercent").data("kendoNumericTextBox").value(data.TaxGainPercent);
                    $("#CommissionAmount").data("kendoNumericTextBox").value(data.CommissionAmount);
                    $("#LevyAmount").data("kendoNumericTextBox").value(data.LevyAmount);
                    $("#KPEIAmount").data("kendoNumericTextBox").value(data.KPEIAmount);
                    $("#VATAmount").data("kendoNumericTextBox").value(data.VATAmount);
                    $("#WHTAmount").data("kendoNumericTextBox").value(data.WHTAmount);
                    $("#OTCAmount").data("kendoNumericTextBox").value(data.OTCAmount);
                    $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(data.TaxSellAmount);
                    $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.TaxInterestAmount);
                    $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.TaxGainAmount);
                    $("#TotalAmount").data("kendoNumericTextBox").value(data.TotalAmount)
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }


    }

    function ResetCommission() {
        $("#CommissionPercent").data("kendoNumericTextBox").value(0);
        $("#LevyPercent").data("kendoNumericTextBox").value(0);
        $("#KPEIPercent").data("kendoNumericTextBox").value(0);
        $("#VATPercent").data("kendoNumericTextBox").value(0);
        $("#WHTPercent").data("kendoNumericTextBox").value(0);
        $("#OTCPercent").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxSellPercent").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxInterestPercent").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxGainPercent").data("kendoNumericTextBox").value(0);
        $("#CommissionAmount").data("kendoNumericTextBox").value(0);
        $("#LevyAmount").data("kendoNumericTextBox").value(0);
        $("#KPEIAmount").data("kendoNumericTextBox").value(0);
        $("#VATAmount").data("kendoNumericTextBox").value(0);
        $("#WHTAmount").data("kendoNumericTextBox").value(0);
        $("#OTCAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(0);
        $("#TotalAmount").data("kendoNumericTextBox").value(0)
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
                    url: window.location.origin + "/Radsoft/Investment/Settlement_ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Settlement_ApproveBySelected",
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
                    url: window.location.origin + "/Radsoft/Investment/Settlement_RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Settlement_RejectBySelected",
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

    $("#BtnUnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Settlement_UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Settlement_UnApproveBySelected",
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
                    url: window.location.origin + "/Radsoft/Investment/Settlement_VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Settlement_VoidBySelected",
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

    function gridSettlementInstructionEquityBuyOnlyDataBound() {
        var grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedEquityBuy[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedEquityBuy[row.SettlementPK] = null;
            }

            if (checkedEquityBuy[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxEquityBuy")
                    .attr("checked", "checked");
            }

            if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionEquitySellOnlyDataBound() {
        var grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedEquitySell[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedEquitySell[row.SettlementPK] = null;
            }

            if (checkedEquitySell[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxEquitySell")
                    .attr("checked", "checked");
            }

            if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionBondBuyOnlyDataBound() {
        var grid = $("#gridSettlementInstructionBondBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedBondBuy[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedBondBuy[row.SettlementPK] = null;
            }

            if (checkedBondBuy[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxBondBuy")
                    .attr("checked", "checked");
            }

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionBondSellOnlyDataBound() {
        var grid = $("#gridSettlementInstructionBondSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedBondSell[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedBondSell[row.SettlementPK] = null;
            }

            if (checkedBondSell[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxBondSell")
                    .attr("checked", "checked");
            }

            if (row.CrossFundFromPK != 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSBondByInstrumentCross");
            } else if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionTimeDepositBuyOnlyDataBound() {
        var grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedDepositoBuy[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedDepositoBuy[row.SettlementPK] = null;
            }

            if (checkedDepositoBuy[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxDepositoBuy")
                    .attr("checked", "checked");
            }

            if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionTimeDepositSellOnlyDataBound() {
        var grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedDepositoSell[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedDepositoSell[row.SettlementPK] = null;
            }

            if (checkedDepositoSell[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxDepositoSell")
                    .attr("checked", "checked");
            }
            
            if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionReksadanaBuyOnlyDataBound() {
        var grid = $("#gridSettlementInstructionReksadanaBuyOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedReksadanaBuy[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedReksadanaBuy[row.SettlementPK] = null;
            }

            if (checkedReksadanaBuy[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxReksadanaBuy")
                    .attr("checked", "checked");
            }

            if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function gridSettlementInstructionReksadanaSellOnlyDataBound() {
        var grid = $("#gridSettlementInstructionReksadanaSellOnly").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedReksadanaSell[0] = null;

            if (row.OrderStatusDesc == "6.REJECT" || row.StatusDesc == "VOID") {
                checkedReksadanaSell[row.SettlementPK] = null;
            }

            if (checkedReksadanaSell[row.SettlementPK] && (row.OrderStatusDesc != "6.REJECT" || row.StatusDesc == "VOID") && (row.StatusDealing != 3 || row.StatusInvestment != 3 || row.StatusSettlement != 3)) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxReksadanaSell")
                    .attr("checked", "checked");
            }

            if (row.StatusDesc == "3.VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            } else if (row.StatusDesc == "2.APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN" || row.StatusDesc == "1.PENDING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else if (row.OrderStatusDesc == "5.PARTIAL") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowPartial");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    $("#BtnApproveBySelectedEquityBuy").click(function (e) {

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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        ////console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
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
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success");
                                        refreshEquityBuy();
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshEquityBuy();
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshEquityBuy();
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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);
        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
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
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
                                        refreshEquitySell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
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
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnUnApproveBySelectedEquitySell").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshEquitySell();
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

    $("#BtnRejectBySelectedEquitySell").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 1,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 1,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshEquitySell();
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

    $("#BtnNetting").click(function () {

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

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)


        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to Download Netting data ?", function (e) {
                if (e) {
                    var Netting = {
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/NettingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(Netting),
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

    $("#BtnCheckDataDealing").click(function () {
        


        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/CheckDataDealing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 1) {
                    alertify.alert("Please Check Data Pending in Dealing ");
                }
                else {
                    alertify.alert("There's No Data Pending in Dealing ")
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



    });

    $("#BtnListingEquity").click(function () {
        showSettlementListingEquity();
    });

    function showSettlementListingEquity(e) {


        

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

        WinSettlementListingEquity.center();
        WinSettlementListingEquity.open();

    }

    $("#BtnOkListingEquity").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var InvestmentListing = {
                    ParamListDate: $('#DateFrom').val(),
                    ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                    ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                    ParamInstType: 1,
                    DownloadMode: $('#DownloadMode').val(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/SettlementListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    });

    $("#BtnCancelListingEquity").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinSettlementListingEquity.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    $("#BtnListingBond").click(function () {
        showSettlementListingBond();
    });

    function showSettlementListingBond(e) {




        $("#DownloadModeBond").kendoComboBox({
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

        WinSettlementListingBond.center();
        WinSettlementListingBond.open();

    }

    $("#BtnOkListingBond").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var InvestmentListing = {
                    ParamListDate: $('#DateFrom').val(),
                    ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                    ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                    ParamInstType: 2,
                    DownloadMode: $('#DownloadModeBond').val(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/SettlementListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    });

    $("#BtnCancelListingBond").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinSettlementListingBond.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    $("#BtnListingTimeDeposit").click(function () {
        showSettlementListingTimeDeposit();
    });

    function showSettlementListingTimeDeposit(e) {




        $("#DownloadModeTimeDeposit").kendoComboBox({
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

        WinSettlementListingTimeDeposit.center();
        WinSettlementListingTimeDeposit.open();

    }

    $("#BtnOkListingTimeDeposit").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var InvestmentListing = {
                    ParamListDate: $('#DateFrom').val(),
                    ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                    ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                    ParamInstType: 5,
                    DownloadMode: $('#DownloadModeTimeDeposit').val(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/SettlementListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    });

    $("#BtnCancelListingTimeDeposit").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinSettlementListingTimeDeposit.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    //---BOND

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

        //console.log(stringInvestmentFrom);
        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
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

    $("#BtnUnApproveBySelectedBondBuy").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshBondBuy();
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

    $("#BtnRejectBySelectedBondBuy").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshBondBuy();
                                        refreshBondSell();
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


  $("#BtnRejectBySelectedBondSell").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshBondSell();
                                        refreshBondBuy();
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

    $("#BtnAmortize").click(function (e) {

        var element = $("#BtnAmortize");
        var posY = element.offset();
        alertify.confirm("Are you sure want to Amortize by Selected Data ?", function (e) {
            
            $.ajax({
                url: window.location.origin + "/Radsoft/BondInterestAndAmortizeDiscount/AmortizeBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert(data);
                    //refreshBondBuy();
                },
                error: function (data) {
                    alertify.alert("Amortize Unsuccessfull");
                }
            });
        });
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

        //console.log(stringInvestmentFrom);
        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }

        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
                                        refreshBondSell();
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 2,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 2,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshBondSell();
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


    //---TIME DEPOSIT

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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2 && _GlobClientCode != '05') {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
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
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
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

    $("#BtnUnApproveBySelectedTimeDepositBuy").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {

            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshTimeDepositBuy();
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshTimeDepositBuy();
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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2 && _GlobClientCode != '05') {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 2) {
                                alertify.alert("Data Already Approved").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First").moveTo(posY.left, posY.top);
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
                                        refreshTimeDepositSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshTimeDepositSell();
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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 5,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 5,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshTimeDepositSell();
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

    // --- REKSADANA

    $("#BtnApproveBySelectedReksadanaBuy").click(function (e) {

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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);
        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
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
                            //else if (data == 4) {
                            //    alertify.alert("Data Must Be Fill Broker First");
                            //    $.unblockUI();
                            //    return;
                            //}
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success");
                                        refreshReksadanaBuy();
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

    $("#BtnUnApproveBySelectedReksadanaBuy").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshReksadanaBuy();
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

    $("#BtnRejectBySelectedReksadanaBuy").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 1,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 1,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshReksadanaBuy();
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

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
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
                            //else if (data == 4) {
                            //    alertify.alert("Data Must Be Fill Broker First");
                            //    $.unblockUI();
                            //    return;
                            //}
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    ApprovedSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/ApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Approved Settlement Success").moveTo(posY.left, posY.top);
                                        refreshReksadanaSell();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
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
            }).moveTo(posY.left, posY.top);
        }
    });

    $("#BtnUnApproveBySelectedReksadanaSell").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateUnApproveBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 1) {
                                alertify.alert("Data Still Pending");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 4) {
                                alertify.alert("Data Must Be Fill Broker First");
                                $.unblockUI();
                                return;
                            }
                            else if (data == 5) {
                                alertify.alert("Data Must Be Fill Cash Ref First");
                                $.unblockUI();
                                return;
                            }
                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    UpdateSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/UnApproveSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("UnApproved Settlement Success");
                                        refreshReksadanaSell();
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

    $("#BtnRejectBySelectedReksadanaSell").click(function (e) {

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

        //console.log(stringInvestmentFrom);

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var ValidateSettlement = {
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        CounterpartID: $('#FilterCounterpartID').val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateTo').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/ValidateRejectBySelectedDataSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/" + 2,
                        type: 'POST',
                        data: JSON.stringify(ValidateSettlement),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == 3) {
                                alertify.alert("Data Already Reject");
                                $.unblockUI();
                                return;
                            }

                            else {
                                var Settlement = {
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    CounterpartID: $('#FilterCounterpartID').val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateTo').val(),
                                    VoidSettlementID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/RejectSettlementBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Settlement),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert("Reject Settlement Success");
                                        refreshReksadanaSell();
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

    $("#BtnSaveBuy").click(function () {
        var val = validateData();
        if (val == 1) {
            
            $.ajax({
                url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Amount').val() + "/" + $('#NetCashAvailable').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        alertify.alert("Cash Not Avaliable")
                        return;
                    }
                    else {
                        //$.ajax({
                        //    url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val(),
                        //    type: 'GET',
                        //    contentType: "application/json;charset=utf-8",
                        //    success: function (data) {
                                //if (data.AlertExposure == 1) {
                                //    alertify.confirm("Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % and Max Exposure Percent : " + data.MaxExposurePercent + " % </br> Are You Sure To Continue ?", function (e) {
                                //        if (e) {
                                //            $.ajax({
                                //                url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
                                //                type: 'GET',
                                //                contentType: "application/json;charset=utf-8",
                                //                success: function (data) {
                                //                    var InvestmentInstruction = {
                                //                        PeriodPK: _defaultPeriodPK,
                                //                        ValueDate: $('#DateFrom').val(),
                                //                        Reference: data,
                                //                        InstructionDate: $('#DateFrom').val(),
                                //                        //InstrumentPK: $(htmlInstrumentPK).val(),
                                //                        InstrumentPK: $('#InstrumentPK').val(),
                                //                        InstrumentTypePK: 1,
                                //                        OrderPrice: $('#OrderPrice').val(),
                                //                        RangePrice: 0,
                                //                        AcqPrice: 0,
                                //                        Lot: $('#Lot').val(),
                                //                        LotInShare: $('#LotInShare').val(),
                                //                        Volume: $('#Volume').val(),
                                //                        Amount: $('#Amount').val(),
                                //                        TrxType: $('#TrxType').val(),
                                //                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                //                        CounterpartPK: 0,
                                //                        FundPK: $('#FundPK').val(),
                                //                        InvestmentNotes: $('#InvestmentNotes').val(),
                                //                        EntryUsersID: sessionStorage.getItem("user")
                                //                    };
                                //                    $.ajax({
                                //                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I_EMCO",
                                //                        type: 'POST',
                                //                        data: JSON.stringify(InvestmentInstruction),
                                //                        contentType: "application/json;charset=utf-8",
                                //                        success: function (data) {
                                //                            alertify.alert(data);
                                //                            win.close();
                                //                            refresh();
                                //                        },
                                //                        error: function (data) {
                                //                            alertify.alert(data.responseText);
                                //                        }
                                //                    });
                                //                },
                                //                error: function (data) {
                                //                    alertify.alert(data.responseText);
                                //                }


                                //            });
                                //        }

                                //    });
                                //}
                                //else if (data.AlertExposure == 2) {
                                //    alertify.alert("Can't Process This Data </br> Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % </br> and MaxExposure Percent : " + data.MaxExposurePercent + " %")
                                //    return;
                                //}
                                //else {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + _defaultPeriodPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
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
                                                InstrumentTypePK: 1,
                                                OrderPrice: $('#OrderPrice').val(),
                                                RangePrice: 0,
                                                AcqPrice: 0,
                                                Lot: $('#Lot').val(),
                                                LotInShare: 100,
                                                Volume: $('#Volume').val(),
                                                Amount: $('#Amount').val(),
                                                TrxType: $('#TrxType').val(),
                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                CounterpartPK: 0,
                                                FundPK: $('#FundPK').val(),
                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I_EMCO",
                                                type: 'POST',
                                                data: JSON.stringify(InvestmentInstruction),
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
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }


                                    });
                                
                        //        }
                        //    }
                        //});
                    }

                }

            });
        }
    });
    
    function ResetSelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/ResetSelectedInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Settlement",
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

    //additional code from Boris
    $("#BtnAllListing").click(function () {
        showWinAllListing();
    });

    function showWinAllListing(e) {
        $("#ListingBy").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "Equity", value: '1' },
               { text: "Bond", value: '2' },
               { text: "Deposito", value: '3' },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeListingBy,
            index: 0
        });
        function OnChangeListingBy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#DownloadModeAll").kendoComboBox({
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

        WinAllListing.center();
        WinAllListing.open();

    }

    $("#BtnOkAllListing").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                if ($("#ListingBy").val() == 1) {
                    var InvestmentListing = {
                        ParamListDate: $('#DateFrom').val(),
                        ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                        ParamInstType: 1,
                        DownloadMode: $('#DownloadModeAll').val(),
                    };
                }
                else if ($("#ListingBy").val() == 2) {
                    var InvestmentListing = {
                        ParamListDate: $('#DateFrom').val(),
                        ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                        ParamInstType: 2,
                        DownloadMode: $('#DownloadModeAll').val(),
                    };
                }
                else {
                    var InvestmentListing = {
                        ParamListDate: $('#DateFrom').val(),
                        ParamFundIDFrom: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamCounterpartIDFrom: $("#FilterCounterpartID").data("kendoComboBox").text(),
                        ParamInstType: 5,
                        DownloadMode: $('#DownloadModeAll').val(),
                    };

                }

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/SettlementListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    });

    $("#BtnCancelAllListing").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinAllListing.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    $("#BtnAllPTP").click(function () {
        showWinAllPTP();
    });

    function showWinAllPTP(e) {
        $("#LblParamPTPBitIsMature").hide();

        if (_GlobClientCode == "05") {
            $("#PTPBy").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Equity", value: '1' },
                    { text: "Bond", value: '2' },
                    { text: "Deposito", value: '3' },
                    { text: "Cross Fund", value: '4' },
                    { text: "Overseas Equity", value: '6' },
                    { text: "Overseas Bond", value: '7' },

                ],
                filter: "contains",
                suggest: true,
                change: OnChangePTPBy,
                index: 0
            });
        }
        else if (_GlobClientCode == "20") {
            $("#PTPBy").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Equity", value: '1' },
                    { text: "Bond", value: '2' },
                    { text: "Deposito", value: '3' },
                    { text: "Reksadana", value: '8' },
                    { text: "Cross Fund", value: '4' },
                    { text: "Deposito Amendment", value: '9' }

                ],
                filter: "contains",
                suggest: true,
                change: OnChangePTPBy,
                index: 0
            });
        }

        else {
            $("#PTPBy").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "Equity", value: '1' },
                    { text: "Bond IDR", value: '2' },
                    { text: "Bond USD", value: '10' },
                    { text: "EBA", value: '11' },
                    { text: "Deposito", value: '3' },
                    { text: "Reksadana", value: '8' },
                    { text: "Cross Fund", value: '4' },
                    { text: "Avg Price Equity", value: '5' },
                    { text: "Deposito Amendment", value: '9' },

                ],
                filter: "contains",
                suggest: true,
                change: OnChangePTPBy,
                index: 0
            });
        }

        function OnChangePTPBy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#PTPBy").val() == 3) {
                if (_GlobClientCode == "05") {
                    $("#LblParamPTPBitIsMature").hide();
                    $("#detailDepositoPTP").show();
                    _showAllTrxDeposito();
                }
                else {
                    $("#LblParamPTPBitIsMature").show();
                    $("#detailDepositoPTP").hide();
                }

            }
            else {
                $("#LblParamPTPBitIsMature").hide();
                $("#detailDepositoPTP").hide();
            }
        }



        WinAllPTP.center();
        WinAllPTP.open();

    }

    $("#BtnOkAllPTP").click(function () {


        alertify.confirm("Are you sure want to Download PTP ?", function (e) {
            if (e) {
                if ($("#PTPBy").val() == 1) {

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
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "Radsoft_PTP_Equity.txt");
                                document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                }
                else if ($("#PTPBy").val() == 2) {

                    var All = 0;
                    All = [];

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
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "PTP_Bond.txt");
                                document.getElementById("downloadFileRadsoft").click();

                                // var newwindow = window.open(data, '_blank');
                                //window.location = data

                                //$("#downloadFileRadsoft").attr("href", data);
                                //$("#downloadFileRadsoft").attr("download", "PTP_Bond.txt");
                                //document.getElementById("downloadFileRadsoft").click();
                                //$("#downloadFileRadsoft").attr("href", data);
                                //$("#downloadFileRadsoft").attr("download", "PTP_Bond.txt");
                                //document.getElementById("downloadFileRadsoft").click();

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
                else if ($("#PTPBy").val() == 3) {

                    var All = 0;
                    All = [];

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

                    var ArrayInvestmentFrom = All;
                    var stringInvestmentFrom = '';

                    for (var i in ArrayInvestmentFrom) {
                        stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                    }
                    stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)


                    AllPTP = [];
                    for (var i in checkedApproved) {
                        if (checkedApproved[i]) {
                            AllPTP.push(i);
                        }
                    }

                    var ArrayRefNoFrom = AllPTP;
                    var stringPTPMatureSelected = '';

                    for (var i in ArrayRefNoFrom) {
                        stringPTPMatureSelected = stringPTPMatureSelected + ArrayRefNoFrom[i] + ',';

                    }
                    stringPTPMatureSelected = stringPTPMatureSelected.substring(0, stringPTPMatureSelected.length - 1)
                    //console.log(stringInvestmentFrom);

                    if (stringInvestmentFrom == "" && $('#ParamPTPBitIsMature').is(":checked") == false && _GlobClientCode != "05") {
                        alertify.alert("There's No Selected Data");
                    }
                    else if (stringPTPMatureSelected == "" && $('#ParamPTPBitIsMature').is(":checked") == true && _GlobClientCode != "05") {
                        alertify.alert("There's No Selected PTP Data");
                    }

                    else {
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                            PTPMatureSelected: stringPTPMatureSelected
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#ParamPTPBitIsMature').is(":checked"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "PTP_Deposito.txt");
                                document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
                else if ($("#PTPBy").val() == 4) {

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

                    //console.log(stringInvestmentFrom);

                    if (stringInvestmentFrom == "") {
                        alertify.alert("There's No Selected Data");
                    }
                    else {

                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByCrossFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                // var newwindow = window.open(data, '_blank');
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "PTP_CrossFundBond.txt");
                                document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
                else if ($("#PTPBy").val() == 5) {
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

                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };


                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPAvgPriceByEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "Radsoft_PTP_Equity.txt");
                                document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
                else if ($("#PTPBy").val() == 6) {
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

                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByOverseasEquity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "Radsoft_PTPByOverseasEquity.txt");
                                document.getElementById("downloadFileRadsoft").click();
                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }

                //Buat Reksadana
                else if ($("#PTPBy").val() == 8) {

                    var All = 0;
                    All = [];
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

                    //console.log(stringInvestmentFrom);

                    if (stringInvestmentFrom == "") {
                        alertify.alert("There's No Selected Data");
                    }
                    else {

                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "Radsoft_PTP_Reksadana.txt");
                                document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                }

                else if ($("#PTPBy").val() == 9) {

                    var All = 0;
                    All = [];

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

                    var ArrayInvestmentFrom = All;
                    var stringInvestmentFrom = '';

                    for (var i in ArrayInvestmentFrom) {
                        stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

                    }
                    stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

                    //console.log(stringInvestmentFrom);

                    var InvestmentListing = {
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/PTPByDepositoAmmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(InvestmentListing),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {

                            // var newwindow = window.open(data, '_blank');
                            //window.location = data
                            $("#downloadFileRadsoft").attr("href", data);
                            $("#downloadFileRadsoft").attr("download", "PTP_Deposito_Ammend.txt");
                            document.getElementById("downloadFileRadsoft").click();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                else if ($("#PTPBy").val() == 10) {

                    var All = 0;
                    All = [];

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
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByBondUSD/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "PTP_Bond_Dollar.txt");
                                document.getElementById("downloadFileRadsoft").click();
                                // var newwindow = window.open(data, '_blank');
                                //window.location = data

                                //$("#downloadFileRadsoft").attr("href", data);
                                //$("#downloadFileRadsoft").attr("download", "PTP_Bond.txt");
                                //document.getElementById("downloadFileRadsoft").click();
                                //$("#downloadFileRadsoft").attr("href", data);
                                //$("#downloadFileRadsoft").attr("download", "PTP_Bond.txt");
                                //document.getElementById("downloadFileRadsoft").click();

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }

                else if ($("#PTPBy").val() == 11) {

                    var All = 0;
                    All = [];

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
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByEBA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "PTP_EBA.txt");
                                document.getElementById("downloadFileRadsoft").click();

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }

                else {
                    var All = 0;
                    All = [];

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
                        var InvestmentListing = {
                            stringInvestmentFrom: stringInvestmentFrom,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/PTPByOverseasBond/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#downloadFileRadsoft").attr("href", data);
                                $("#downloadFileRadsoft").attr("download", "Radsoft_PTPByOverseasBond.txt");
                                document.getElementById("downloadFileRadsoft").click();
                                // var newwindow = window.open(data, '_blank');
                                //window.location = data
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                }
            }
        });

    });

    $("#BtnCancelAllPTP").click(function () {
        
        alertify.confirm("Are you sure want to cancel PTP?", function (e) {
            if (e) {
                WinAllPTP.close();
                alertify.alert("Cancel PTP");
            }
        });
    });

    //end additional Code from Boris

    $("#BtnSettlementListing").click(function () {
        showSettlementListing();
    });

    // Untuk Form Listing

    function showSettlementListing(e) {
        $("#LblParamListingBitIsMature").hide();
        $("#ParamInsType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamInsType,
            dataSource: [
                    { text: "EQUITY", value: "1" },
                    { text: "BOND", value: "2" },
                    { text: "DEPOSITO", value: "3" },
            ],

        });
        function OnChangeParamInsType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            if ($("#ParamInsType").val() == 3) {
                $("#LblParamListingBitIsMature").show();
            }
            else {
                $("#LblParamListingBitIsMature").hide();
            }
        }


        if (_GlobClientCode == "19") {
            $("#lblParamTrxType").show();
        }
        else {
            $("#lblParamTrxType").hide();
        }


        $("#ParamTrxType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamTrxType,
            dataSource: [
                    { text: "BUY", value: "1" },
                    { text: "SELL", value: "2" },
            ],

        });
        function OnChangeParamTrxType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }


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

        WinSettlementListing.center();
        WinSettlementListing.open();

    }

    $("#BtnOkSettlementListing").click(function () {

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

        //console.log(stringInvestmentFrom);

        var date1 = new Date($('#DateFrom').val()).setHours(0, 0, 0, 0);
        var date2 = new Date($('#DateTo').val()).setHours(0, 0, 0, 0);


        if (stringInvestmentFrom == "" && $('#ParamListingBitIsMature').is(":checked") == false && _GlobClientCode != "05") {
            alertify.alert("There's No Selected Data");
        }
        else if (date1 != date2) {
            alertify.alert("Filter date must be same day");
        }
        else {
            alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
                if (e) {
                    var SettlementListing = {
                        Message: $('#Message').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        ParamInstType: $('#ParamInsType').val(),
                        ParamListDate: $('#DateFrom').val(),
                        BitIsMature: $('#ParamListingBitIsMature').is(":checked"),
                        Signature1: $("#Signature1").data("kendoComboBox").value(),
                        Signature2: $("#Signature2").data("kendoComboBox").value(),
                        Signature3: $("#Signature3").data("kendoComboBox").value(),
                        Signature4: $("#Signature4").data("kendoComboBox").value(),
                        ParamTrxType: $('#ParamTrxType').val(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/SettlementListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(SettlementListing),
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

    $("#BtnCancelSettlementListing").click(function () {
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinSettlementListing.close();
                alertify.success("Cancel Listing");
            }
        });
    });

    function onWinSettlementListingClose() {
        $('#ParamInsType').val(""),
        $("#Message").val("")
        $('#ParamListingBitIsMature').prop('checked', false);
        $("#detailMaturePTP").hide();
        $("#gridMaturePTP").empty();
        $("#detailDepositoPTP").hide();
        $("#gridDepositoPTP").empty();
        $("#detailMature").hide();
        $("#gridMature").empty();
    }

    function onWinAllPTPClose() {
        $("#Message").val("");
        $("#PTPBy").val(1);
        $('#ParamPTPBitIsMature').prop('checked', false);
        $("#detailMaturePTP").hide();
        $("#gridMaturePTP").empty();
        $("#detailDepositoPTP").hide();
        $("#gridDepositoPTP").empty();
    }

    $("#ParamListingBitIsMature").click(function () {
        if ($("#ParamListingBitIsMature").prop('checked') == true) {
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

    // PTP MATURE

    $("#ParamPTPBitIsMature").click(function () {
        if ($("#ParamPTPBitIsMature").prop('checked') == true) {
            $("#detailMaturePTP").show();
            $("#gridMaturePTP").empty();
            initMaturePTP();
        }
        else {
            $("#detailMaturePTP").hide();
            $("#gridMaturePTP").empty();
        }


    });

    function initMaturePTP() {
        if (_GlobClientCode == "17") {
            var dsListMaturePTP = InitDataSourceMaturePTPCustomJarvis();
            var gridMaturePTP = $("#gridMaturePTP").kendoGrid({
                dataSource: dsListMaturePTP,
                height: 300,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                dataBound: onDataBoundApproved,
                editable: "incell",
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Update", click: _updatePTPMature }, title: " ", width: 80 },
                    { field: "Reference", title: "Reference", width: 150 },
                    { field: "RefNo", title: "Ref No", width: 150 },
                    { field: "AcqDate", title: "Acq Date", width: 150, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund", width: 120 },
                    { field: "InstrumentID", title: "Bank", width: 120 },
                    { field: "Volume", title: "Nominal", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                    { field: "InterestPercent", title: "Interest (%)", format: "{0:n4}", template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }, width: 120 },
                    {
                        field: "AmountToTransfer", title: "AmountToTransfer",
                        format: "{0:n2}", attributes: { style: "text-align:right;" }, headerAttributes: {
                            style: "text-align: center"
                        }, width: 100
                    },
                ]
            }).data("kendoGrid");

            gridMaturePTP.table.on("click", ".checkboxApproved", selectRowApproved);
        }

        else {
            var dsListMaturePTP = InitDataSourceMaturePTP();
            var gridMaturePTP = $("#gridMaturePTP").kendoGrid({
                dataSource: dsListMaturePTP,
                height: 300,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                dataBound: onDataBoundApproved,
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { field: "Reference", title: "Reference", width: 150 },
                    { field: "RefNo", title: "Ref No", width: 150 },
                    { field: "AcqDate", title: "Acq Date", width: 150, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund", width: 120 },
                    { field: "InstrumentID", title: "Bank", width: 120 },
                    { field: "Volume", title: "Nominal", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                    { field: "InterestPercent", title: "Interest (%)", format: "{0:n4}", template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }, width: 120 },
                ]
            }).data("kendoGrid");

            gridMaturePTP.table.on("click", ".checkboxApproved", selectRowApproved);
        }



        var oldPageSizeApproved = 0;


        $('#chbB').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = gridMaturePTP.dataSource.pageSize();
            gridMaturePTP.dataSource.pageSize(gridMaturePTP.dataSource.data().length);

            $('.checkboxApproved').each(function (idx, item) {
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

            gridMaturePTP.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridMaturePTP = $("#gridMaturePTP").data("kendoGrid"),
                dataItemZ = gridMaturePTP.dataItem(rowA);

            checkedApproved[dataItemZ.RefNo] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].RefNo]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }
        }

    }

    function SelectDeselectDataMaturePTP(_a, _b) {
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

    function SelectDeselectAllDataMaturePTP(_a) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectAllDataByDateMature/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetailPTP").prop('checked', _a);
                refreshMaturePTP();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function refreshMaturePTP() {
        if (_GlobClientCode == "17") {
            var newDSPTP = getDataSourceMaturePTPCustomJarvis(window.location.origin + "/Radsoft/Investment/GetDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
            $("#gridMaturePTP").data("kendoGrid").setDataSource(newDSPTP);
        }
        else {
            var newDSPTP = getDataSourceMaturePTP(window.location.origin + "/Radsoft/Investment/GetDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
            $("#gridMaturePTP").data("kendoGrid").setDataSource(newDSPTP);
        }



    }


    // Untuk List MATURE

    function InitDataSourceMaturePTP() {
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

    function InitDataSourceMaturePTPCustomJarvis() {
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
                            Reference: { type: "string", editable: false },
                            RefNo: { type: "number", editable: false },
                            FundID: { type: "string", editable: false },
                            InstrumentID: { type: "string", editable: false },
                            Volume: { type: "number", editable: false },
                            AcqDate: { type: "date", editable: false },
                            InterestPercent: { type: "number", editable: false },
                            AmountToTransfer: { type: "number", editable: true },

                        }
                    }
                }
            });
    }

    function getDataSourceMaturePTP() {

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

    function getDataSourceMaturePTPCustomJarvis() {

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
                            Reference: { type: "string", editable: false },
                            FundID: { type: "string", editable: false },
                            InstrumentID: { type: "string", editable: false },
                            Volume: { type: "number", editable: false },
                            AcqDate: { type: "date", editable: false },
                            InterestPercent: { type: "number", editable: false },
                            AmountToTransfer: { type: "number", editable: true }
                        }
                    }
                }
            });
    }
    //PTP LIST ALL TD (MNC)

    function _showAllTrxDeposito() {

        var dsListDepositoPTP = InitDataSourceDepositoPTP();
        var gridDepositoPTP = $("#gridDepositoPTP").kendoGrid({
            dataSource: dsListDepositoPTP,
            height: 500,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            editable: true,
            columns: [
                {
                    field: "Selected",
                    width: 30,
                    template: "<input class='cSelectedDetailDepositoPTP' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedDetailDepositoPTP' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
               { command: { text: "Update", click: _updateMaturity }, title: " ", width: 100 },
               { field: "DepNo", title: "No.", width: 50 },
               { field: "TrxTypeID", title: "TrxTypeID", width: 150 },
               { field: "Reference", title: "Reference", width: 150 },
               { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
               { field: "FundID", title: "Fund", width: 120 },
               { field: "InstrumentID", title: "Bank", width: 120 },
               { field: "Volume", title: "Nominal", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
               { field: "InterestPercent", title: "Interest (%)", format: "{0:n4}", template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }, width: 120 },
               { field: "MaturityDate", title: "Maturity Date", width: 150, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
            ]
        }).data("kendoGrid");

        $("#SelectedDetailDepositoPTP").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataDepositoPTP(_checked);

        });

        gridDepositoPTP.table.on("click", ".cSelectedDetailDepositoPTP", selectDataDepositoPTP);

        function selectDataDepositoPTP(e) {


            var gridDepositoPTP = $("#gridDepositoPTP").data("kendoGrid");
            var dataItemX = gridDepositoPTP.dataItem($(e.currentTarget).closest("tr"));
            var _reference = dataItemX.Reference;
            var _checked = this.checked;
            SelectDeselectDataDepositoPTP(_checked, _reference);

        }



    }

    function SelectDeselectDataDepositoPTP(_a, _b) {
        var _s = _b.replace(/\//g, '-');
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectDataDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _s,
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

    function SelectDeselectAllDataDepositoPTP(_a) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectAllDataByDateDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetailDepositoPTP").prop('checked', _a);
                refreshDepositoPTP();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function refreshDepositoPTP() {

        var newDSPTP = getDataSourceDepositoPTP(window.location.origin + "/Radsoft/Investment/GetDataDepositoByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
        $("#gridDepositoPTP").data("kendoGrid").setDataSource(newDSPTP);


    }


    // Untuk List MATURE

    function InitDataSourceDepositoPTP() {
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

                                          url: window.location.origin + "/Radsoft/Investment/InitDataDepositoByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/0",
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 50,
                      schema: {
                          model: {
                              fields: {
                                  Selected: { type: "boolean" },
                                  InvestmentPK: { type: "number" },
                                  DepNo: { type: "number" },
                                  TrxTypeID: { type: "string" },
                                  Reference: { type: "string" },
                                  FundID: { type: "string" },
                                  InstrumentID: { type: "string" },
                                  Volume: { type: "number" },
                                  ValueDate: { type: "date" },
                                  InterestPercent: { type: "number" },
                                  MaturityDate: { type: "date" }

                              }
                          }
                      }
                  });
    }

    function getDataSourceDepositoPTP() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/Investment/GetDataDepositoByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                  InvestmentPK: { type: "number" },
                                  DepNo: { type: "number" },
                                  TrxTypeID: { type: "string" },
                                  Reference: { type: "string" },
                                  FundID: { type: "string" },
                                  InstrumentID: { type: "string" },
                                  Volume: { type: "number" },
                                  ValueDate: { type: "date" },
                                  InterestPercent: { type: "number" },
                                  MaturityDate: { type: "date" },

                              }
                          }
                      }
                  });
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
                TrxType: $('#TrxType').val(),
                InstrumentTypePK: $('#InstrumentTypePK').val(),
                ValueDate: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                SettledDate: kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                NextCouponDate: kendo.toString($("#NextCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                LastCouponDate: kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
                Price: $('#DonePrice').val(),
                Volume: $('#DoneVolume').val(),
                AcqPrice: $('#AcqPrice').val(),
                AcqDate: kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM/dd/yyyy"),
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
                url: window.location.origin + "/Radsoft/OMSBond/GetNetAmountForSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    function _updateEquityBuy(e) {
        if (e) {
            var grid;
            grid = $("#gridSettlementInstructionEquityBuyOnly").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Settlement = {
                InvestmentPK: dataItemX.InvestmentPK,
                DealingPK: dataItemX.DealingPK,
                SettlementPK: dataItemX.SettlementPK,
                DoneAmount: dataItemX.DoneAmount,
                CommissionAmount: dataItemX.CommissionAmount,
                LevyAmount: dataItemX.LevyAmount,
                KPEIAmount: dataItemX.KPEIAmount,
                VATAmount: dataItemX.VATAmount,
                WHTAmount: dataItemX.WHTAmount,
                TotalAmount: dataItemX.TotalAmount,
                SettledDate: kendo.toString(kendo.parseDate(dataItemX.SettledDate), 'MM/dd/yyyy'),
            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdateSettlementEquityBuy/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Settlement),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function _updateEquitySell(e) {
        if (e) {
            var grid;
            grid = $("#gridSettlementInstructionEquitySellOnly").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Settlement = {
                InvestmentPK: dataItemX.InvestmentPK,
                DealingPK: dataItemX.DealingPK,
                SettlementPK: dataItemX.SettlementPK,
                DoneAmount: dataItemX.DoneAmount,
                CommissionAmount: dataItemX.CommissionAmount,
                LevyAmount: dataItemX.LevyAmount,
                KPEIAmount: dataItemX.KPEIAmount,
                VATAmount: dataItemX.VATAmount,
                WHTAmount: dataItemX.WHTAmount,
                IncomeTaxSellAmount: dataItemX.IncomeTaxSellAmount,
                TotalAmount: dataItemX.TotalAmount,
                SettledDate: kendo.toString(kendo.parseDate(dataItemX.SettledDate), 'MM/dd/yyyy'),
            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdateSettlementEquitySell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Settlement),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function _updateTimeDepositBuy(e) {
        if (e) {
            var grid;
            grid = $("#gridSettlementInstructionTimeDepositBuyOnly").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Settlement = {
                InvestmentPK: dataItemX.InvestmentPK,
                DealingPK: dataItemX.DealingPK,
                SettlementPK: dataItemX.SettlementPK,
                AmountToTransfer: dataItemX.AmountToTransfer,
            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdateSettlementTimeDepositBuy/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Settlement),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function _updateTimeDepositSell(e) {
        if (e) {
            var grid;
            grid = $("#gridSettlementInstructionTimeDepositSellOnly").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Settlement = {
                InvestmentPK: dataItemX.InvestmentPK,
                DealingPK: dataItemX.DealingPK,
                SettlementPK: dataItemX.SettlementPK,
                AmountToTransfer: dataItemX.AmountToTransfer,
            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdateSettlementTimeDepositSell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Settlement),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function _updatePTPMature(e) {
        if (e) {
            var grid;
            grid = $("#gridMaturePTP").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Investment = {
                RefNo: dataItemX.RefNo,
                AmountToTransfer: dataItemX.AmountToTransfer,
            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdatePTPMature/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Investment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refreshMaturePTP();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function _updateMaturity(e) {
        if (e) {
            var grid;
            grid = $("#gridDepositoPTP").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            var Maturity = {
                InvestmentPK: dataItemX.InvestmentPK,
                Reference: dataItemX.Reference,
                MaturityDate: kendo.toString(kendo.parseDate(dataItemX.MaturityDate), 'dd/MMM/yyyy'),

            };


            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/UpdateMaturityForDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Maturity),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }

    function UpdateRecalTotalAmount() {
        if ($("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value() == 0) {
            $("#IncomeTaxInterestPercent").data("kendoNumericTextBox").value(0);
        }
        if ($("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() == 0) {
            $("#IncomeTaxGainPercent").data("kendoNumericTextBox").value(0);
        }
        $("#TotalAmount").data("kendoNumericTextBox").value($("#DoneAmount").data("kendoNumericTextBox").value() + $("#DoneAccruedInterest").data("kendoNumericTextBox").value() - $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value() - $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value())
    }

    $("#BtnAllKertasKerja").click(function () {
        showWinAllKertasKerja();
    });

    function showWinAllKertasKerja(e) {
        $("#LblParamListingKertasKerjaBitIsMature").hide();

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

            if ($("#KertasKerjaBy").val() == 3) {
                $("#LblParamListingKertasKerjaBitIsMature").show();
            }
            else {
                $("#LblParamListingKertasKerjaBitIsMature").hide();
            }

        }

        WinAllKertasKerja.center();
        WinAllKertasKerja.open();

    }

    $("#BtnOkAllKertasKerja").click(function () {


        alertify.confirm("Are you sure want to Download KertasKerja ?", function (e) {
            if (e) {
                if ($("#KertasKerjaBy").val() == 1) {
                    console.log($("#KertasKerjaBy").val())
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

                    //console.log(stringInvestmentFrom);




                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),

                        Signature1: $("#Signature1").data("kendoComboBox").value(),
                        Signature2: $("#Signature2").data("kendoComboBox").value(),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaSettlementSaham/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }

                    });



                }
                else if ($("#KertasKerjaBy").val() == 2) {

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

                    //console.log(stringInvestmentFrom);


                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),
                        //BitIsMature: $('#BitIsMature').is(":checked"),
                        Signature1: $("#Signature1").data("kendoComboBox").value(),
                        Signature2: $("#Signature2").data("kendoComboBox").value(),
                        Signature3: $("#Signature3").data("kendoComboBox").value(),
                        Signature4: $("#Signature4").data("kendoComboBox").value(),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        Signature3Desc: $("#Signature3").data("kendoComboBox").text(),
                        Signature4Desc: $("#Signature4").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaSettlementObligasi/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }

                    });
                }
                else if ($("#KertasKerjaBy").val() == 3) {

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

                    //console.log(stringInvestmentFrom);

                    var KertasKerja = {
                        //Message: $('#Message').val(),
                        //DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //ParamFundID: $("#FilterFundID").data("kendoComboBox").text(),
                        //ParamListDate: $('#DateFrom').val(),
                        BitIsMature: $('#ParamListingKertasKerjaBitIsMature').is(":checked"),
                        Signature1: $("#Signature1").data("kendoComboBox").value(),
                        Signature2: $("#Signature2").data("kendoComboBox").value(),
                        Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                        Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                        stringInvestmentFrom: stringInvestmentFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/KertasKerjaSettlementTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(KertasKerja),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
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

    $("#BtnImportSIDeposito").click(function () {
        document.getElementById("ImportSIDeposito").click();
    });

    $("#ImportSIDeposito").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportSIDeposito").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("SInvestDeposito", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SettlementInstruction_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportSIDeposito").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportSIDeposito").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportSIDeposito").val("");
        }
    });

    function onWinWinSettlementAmmendMatureDepositoClose() {
        $("#AmmendPaymentModeOnMaturity").val("");
        $("#gridAmmendMatureDeposito").empty();
    }

    $("#BtnUpdateDepositoMature").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PaymentModeOnMaturity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AmmendPaymentModeOnMaturity").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    enable: true,
                    dataSource: data,
                    index: 1
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        initAmmendDeposito();

        WinSettlementAmmendMatureDeposito.center();
        WinSettlementAmmendMatureDeposito.open();
    });

    function initAmmendDeposito() {
        $("#gridAmmendMatureDeposito").empty();

        var SettlementInstructionAmmendDepositoURL = window.location.origin + "/Radsoft/Investment/GetDataAmmendDepositoFromSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceAmmendDeposito = getDataSourceAmmend(SettlementInstructionAmmendDepositoURL);

        var gridAmmendDeposito = $("#gridAmmendMatureDeposito").kendoGrid({
            dataSource: dataSourceAmmendDeposito,
            scrollable: {
                virtual: true
            },
            pageable: false,
            height: "400px",
            reorderable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            sortable: true,
            dataBound: gridSettlementInstructionAmmendDepositoDataBound,
            resizable: true,
            toolbar: kendo.template($("#gridAmmendMatureDeposito").html()),
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='chbAmmendDeposito' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbAmmendDeposito'></label>",
                    template: "<input type='checkbox' class='checkboxAmmendDeposito'/>", width: 45
                },
                { field: "SettlementPK", title: "SettlementPK.", filterable: false, hidden: true },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true },
                { field: "ValueDate", title: "Placement Date", template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#", width: 130 },
                { field: "InstrumentID", title: "Bank", width: 130 },
                { field: "FundID", title: "Fund", width: 100 },
                {
                    field: "DoneAmount", title: "Amount", headerAttributes: {
                        style: "text-align: center"
                    }, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 150
                },
                { field: "MaturityDate", title: "Maturity Date", template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#", width: 130 },
                {
                    field: "InterestPercent", title: "Interest Percent",
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }, width: 130
                },
                { field: "PaymentModeOnMaturityDesc", title: "Payment Mode On Maturity", width: 130 },
            ]
        }).data("kendoGrid");

        gridAmmendDeposito.table.on("click", ".checkboxAmmendDeposito", selectRowAmmendDeposito);
        var oldPageSizeApproved = 0;


        $('#chbAmmendDeposito').change(function (ev) {

            var grid = $("#gridAmmendMatureDeposito").data("kendoGrid");

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);


            $('.checkboxAmmendDeposito').each(function (idx, item) {
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
            for (var i in checkedAmmendDeposito) {
                if (checkedAmmendDeposito[i]) {
                    checked.push(i);
                }
            }
            //console.log(checked + ' ' + checked.length);
        });

        function selectRowAmmendDeposito() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridAmmendMatureDeposito").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedAmmendDeposito[dataItemZ.SettlementPK] = checked;

            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected")
            }

        }
    }

    function getDataSourceAmmend(_url) {
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
                pageSize: 10000,

                schema: {
                    model: {
                        fields: {
                            SettlementPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            ValueDate: { type: "date" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            InstrumentPK: { type: "number" },
                            InstrumentID: { type: "string" },
                            DoneAmount: { type: "number" },
                            InterestPercent: { type: "number" },
                            MaturityDate: { type: "date" },
                            PaymentModeOnMaturity: { type: "number" }
                        }

                    }

                }
            });
    }

    function gridSettlementInstructionAmmendDepositoDataBound() {
        var grid = $("#gridAmmendMatureDeposito").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            checkedAmmendDeposito[0] = null;

            if (checkedAmmendDeposito[row.SettlementPK]) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxAmmendDeposito")
                    .attr("checked", "checked");
            }

        });
    }

    $("#BtnAmmendMatureDeposito").click(function () {

        var All = 0;
        All = [];

        for (var i in checkedAmmendDeposito) {
            if (checkedAmmendDeposito[i]) {
                All.push(i);
            }
        }

        var ArrayInvestmentFrom = All;
        var stringInvestmentFrom = '';

        for (var i in ArrayInvestmentFrom) {
            stringInvestmentFrom = stringInvestmentFrom + ArrayInvestmentFrom[i] + ',';

        }
        stringInvestmentFrom = stringInvestmentFrom.substring(0, stringInvestmentFrom.length - 1)

        if (stringInvestmentFrom == "") {
            alertify.alert("There's No Selected Data");
        }
        else {
            alertify.confirm("Are you sure want to update selected data ?", function (e) {
                if (e) {
                    var AmmendDeposito = {
                        stringInvestmentFrom: stringInvestmentFrom,
                        PaymentModeOnMaturity: $("#AmmendPaymentModeOnMaturity").val(),
                        UpdateUsersID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Investment/Settlement_AmmendDeposito/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(AmmendDeposito),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            initAmmendDeposito();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }

    });

    // NIKKO

    $("#ParamListingKertasKerjaBitIsMature").click(function () {
        if ($("#ParamListingKertasKerjaBitIsMature").prop('checked') == true) {
            $("#detailKertasKerjaMature").show();
            $("#gridKertasKerjaMature").empty();
            initKertasKerjaMature();
        }
        else {
            $("#detailKertasKerjaMature").hide();
            $("#gridKertasKerjaMature").empty();
        }


    });

    function initKertasKerjaMature() {
        var dsListKertasKerjaMature = InitDataSourceKertasKerjaMature();
        var gridKertasKerjaMature = $("#gridKertasKerjaMature").kendoGrid({
            dataSource: dsListKertasKerjaMature,
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
                    template: "<input class='cSelectedDetailKertasKerjaMature' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedDetailKertasKerjaMature' type='checkbox'  />",
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

        $("#SelectedDetailKertasKerjaMature").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataKertasKerjaMature(_checked);

        });

        gridKertasKerjaMature.table.on("click", ".cSelectedDetailKertasKerjaMature", selectDataKertasKerjaMature);

        function selectDataKertasKerjaMature(e) {


            var gridKertasKerja = $("#gridKertasKerjaMature").data("kendoGrid");
            var dataItemX = gridKertasKerja.dataItem($(e.currentTarget).closest("tr"));
            var _Creference = dataItemX.Reference;
            var _Cchecked = this.checked;
            SelectDeselectDataKertasKerjaMature(_Cchecked, _Creference);

        }



    }

    function SelectDeselectDataKertasKerjaMature(_a, _b) {
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

    function SelectDeselectAllDataKertasKerjaMature(_a) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/SelectDeselectAllDataByDateMature/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetailKertasKerja").prop('checked', _a);
                refreshKertasKerjaMature();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function refreshKertasKerjaMature() {

        var newDS = getDataSourceKertasKerjaMature(window.location.origin + "/Radsoft/Investment/GetDataMatureByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
        $("#gridKertasKerjaMature").data("kendoGrid").setDataSource(newDS);


    }

    // Untuk List MATURE

    function InitDataSourceKertasKerjaMature() {
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

    function getDataSourceKertasKerjaMature() {

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



});
