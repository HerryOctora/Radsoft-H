$(document).ready(function () {
    var chat = $.connection.chatHub;

    function SendNotification() {
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

    var GlobValidator = $("#WinInvestmentInstruction").kendoValidator().data("kendoValidator");
    var _d = new Date();
    var _fy = _d.getFullYear();
    var _defaultPeriodPK;

    $("#DateFrom").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        change: onChangeDateFrom,
        value: new Date()
    });

    //$("#AcqDate").kendoDatePicker({
    //    format: "dd/MMM/yyyy",
    //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

    //});

    function onChangeDateFrom() {
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
            var dt = this.dataSource._data[0];
            this.text('');
        } else {
            refresh();
            
        }
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
    RefreshNetBuySell();


    function validateData() {

        if ($("#TrxType").val() == 1)
        {
            if ($("#Amount").data("kendoNumericTextBox").value() == 0 && $("#MethodType").val() == 2) {
                alertify.alert("Amount <> 0").moveTo(300, 500);
                return 0;
            }
        }

        if ($("#FundPK").data("kendoComboBox").value() == "" || $("#FundPK").data("kendoComboBox").value() == 0) {
            alertify.alert("Fund must be filled").moveTo(300, 500);
            return 0;
        }
        if ($("#TrxType").data("kendoComboBox").value() == "" || $("#TrxType").data("kendoComboBox").value() == 0) {
            alertify.alert("TrxType must be filled").moveTo(300, 500);
            return 0;
        }
        if (GlobValidator.validate()) {
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
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_GetNetBuySell/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_GetAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == null || data == 0) {
                    $('#NetCashAvailable').val(0);
                    $('#NetCashAvailable').text(kendo.toString(data, "n"));
                }
                else {
                    $('#NetCashAvailable').val(data);
                    $('#NetCashAvailable').text(kendo.toString(data, "n"));
                }

            },
            error: function (data) {
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
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_GetAUMYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#InstrumentPK").val('');
        $("#InstrumentID").val('');
        $("#MarketPK").val('');
        $("#RangePrice").val('');
        $("#OrderPrice").data("kendoNumericTextBox").value(null);
        $("#Amount").data("kendoNumericTextBox").value(null);
        $("#Lot").data("kendoNumericTextBox").value(null);
        $("#Volume").data("kendoNumericTextBox").value(null);
        $("#TrxType").data("kendoComboBox").value(null);
        GlobValidator.hideMessages();
    }

    function onCloseUpdateInvestment() {
        $("#UpInvestmentNotes").val('');
        $("#UpInstrumentPK").val('');
        $("#UpFundPK").val('');
        $("#UpMarketPK").val('');
        $("#UpRangePrice").val('');
        $("#UpOrderPrice").data("kendoNumericTextBox").value(null);
        $("#UpAmount").data("kendoNumericTextBox").value(null);
        $("#UpLot").data("kendoNumericTextBox").value(null);
        $("#UpVolume").data("kendoNumericTextBox").value(null);
        GlobValidator.hideMessages();
    }




    WinListAvailableCashDetail = $("#WinListAvailableCashDetail").kendoWindow({
        height: 500,
        title: "AvailableCashDetail",
        visible: false,
        width: 700,
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
        format: "dd/MMM/yyyy"
    });

    $("#UpSettledDate").kendoDatePicker({
        format: "dd/MMM/yyyy"
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
        $("#BtnPTPReksadana").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
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

    //$("#BtnToBySector").click(function () {
    //    splitter.toggle("#PaneBySector");
    //});

    //$("#BtnToByIndex").click(function () {
    //    splitter.toggle("#PaneByIndex");
    //});

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
        //splitter.expand("#PaneBySector");
        //splitter.expand("#PaneByIndex");
        splitter.expand("#PaneByInstrument");
        splitter.expand("#PaneExposure");
        splitter.expand("#PaneFundPosition");
    });

    $("#BtnToHide").click(function () {
        splitter.collapse("#PaneInvestment");
        //splitter.collapse("#PaneBySector");
        //splitter.collapse("#PaneByIndex");
        splitter.collapse("#PaneByInstrument");
        splitter.collapse("#PaneExposure");
        splitter.collapse("#PaneFundPosition");
    });

    function GetPriceFromYahooFinance() {


        $.blockUI({});
        $.ajax({
            url: window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_ListStockForYahooFinance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        alertify.set('notifier', 'position', 'top-center'); alertify.success("Refresh Done");
    });

    $("#BtnRenewMarket").click(function () {
        GetPriceFromYahooFinance();
        GenerateNavProjection();
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
                InstrumentPK: $('#UpInstrumentPK').val(),

                OrderPrice: $('#UpOrderPrice').val(),
                RangePrice: $('#UpRangePrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };
            $.ajax({
                url: window.location.origin + "/Radsoft/Investment/Investment_AmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                type: 'POST',
                data: JSON.stringify(Investment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UpAmount').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpFundPK').val(),
                        type: 'GET',
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
                                        alertify.alert("Amend Cancel,Cash not available ").moveTo(posY.left, posY.top);
                                        refresh();
                                        WinUpdateInvestment.close();
                                        return;

                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                    }
                                });

                            }
                            else {
                                if ($('#UpOrderPrice').val() > 0) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataCheckPrice) {
                                            dataCheckPrice.Validate = 0;
                                            if (dataCheckPrice.Validate == 0) {

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val(),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (dataCheckExposure) {

                                                        if (dataCheckExposure.AlertExposure == 1) {
                                                            alertify.confirm("Exposure " + dataCheckExposure.ExposureID + " : " + dataCheckExposure.ExposurePercent + " % and Max Exposure Percent : " + dataCheckExposure.MaxExposurePercent + " %  Are You Sure To Continue ?", function () {
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

                                                            }, function () {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                                    type: 'POST',
                                                                    data: JSON.stringify(Investment),
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        $.unblockUI();
                                                                        refresh();
                                                                        alertify.alert('Amend Cancel').moveTo(posY.left, posY.top);

                                                                    },
                                                                    error: function (data) {
                                                                        $.unblockUI();
                                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                                    }
                                                                });
                                                            }).moveTo(posY.left, posY.top - 100);

                                                        } else if (dataCheckExposure.AlertExposure == 2) {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                                type: 'POST',
                                                                data: JSON.stringify(Investment),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert("Amend Cancel,Can't Process This Data </br> Exposure " + dataCheckExposure.ExposureID + " : " + dataCheckExposure.ExposurePercent + " % </br> and MaxExposure Percent : " + dataCheckExposure.MaxExposurePercent + " %").moveTo(posY.left, posY.top);
                                                                    refresh();

                                                                },
                                                                error: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                                }
                                                            });
                                                            return;
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
                                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpAmount').val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (dataCheckExposure) {
                                            if (dataCheckExposure.AlertExposure == 1) {
                                                alertify.confirm("Exposure " + dataCheckExposure.ExposureID + " : " + dataCheckExposure.ExposurePercent + " % and Max Exposure Percent : " + dataCheckExposure.MaxExposurePercent + " % </br> Are You Sure To Continue ?", function () {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Investment/Investment_AddAmend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                                        type: 'POST',
                                                        data: JSON.stringify(Investment),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            $.unblockUI();
                                                            refresh();
                                                            alertify.alert('Amend Success').moveTo(posY.left, posY.top);
                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                        }
                                                    });
                                                }, function () {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                        type: 'POST',
                                                        data: JSON.stringify(Investment),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            $.unblockUI();
                                                            refresh();
                                                            alertify.alert('Amend Cancel').moveTo(posY.left, posY.top);
                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                        }
                                                    });
                                                }
                                                ).moveTo(posY.left, posY.top);
                                            } else if (dataCheckExposure.AlertExposure == 2) {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Investment/Investment_CancelAmendReject/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                                    type: 'POST',
                                                    data: JSON.stringify(Investment),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert("Amend Cancel, Can't Process This Data </br> Exposure " + dataCheckExposure.ExposureID + " : " + dataCheckExposure.ExposurePercent + " % </br> and MaxExposure Percent : " + dataCheckExposure.MaxExposurePercent + " %").moveTo(posY.left, posY.top);
                                                        refresh();

                                                    },
                                                    error: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data.responseText).moveTo(posY.left, posY.top);
                                                    }
                                                });
                                                return;
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
                $("#lblUpSettledDate").hide();
            }
            else {
                $("#BtnUpdateInvestmentSell").show();
                $("#lblUpSettledDate").show();
            }

        }
        


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
        $("#UpSettledDate").val(kendo.toString(kendo.parseDate(dataItemX.SettledDate), 'dd/MMM/yyyy'));
        $("#EntryUsersID").val(dataItemX.EntryUsersID);
        $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
        $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
        $("#VoidUsersID").val(dataItemX.VoidUsersID);
        $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));




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

        $("#UpMethodType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                    { text: "Unit", value: "1" },
                    { text: "Amount", value: "2" },
            ],
            change: onChangeUpMethodType,
            value: setCmbUpMethodType()
        });

        function setCmbUpMethodType() {
            
            if (_GlobClientCode == "11") {
                if (dataItemX.TrxType == 2) {
                    $("#LblUpUnit").show();
                    $("#LblUpPrice").show();
                    return 1;
                }
                else {
                    return 2;
                }

            }
            else
            {
                if (dataItemX.OrderPrice == null) {
                    return 2;
                } else {
                    if (dataItemX.Volume == "" || dataItemX.Volume == 0) {
                        return 2;
                    } else {
                        return 1;
                    }
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
                $("#LblUpVolume").show();
                $("#LblUpUnit").show();
            }
            else {
                ResetLblUpMethodType();
                $("#LblUpVolume").show();

            }
        }

        if ($("#UpMethodType").val() == 2) {
            $("#LblUpUnit").hide();
            $("#LblUpLot").hide();
            $("#LblUpVolume").show();
        }
        else {

            $("#LblUpLot").hide();
            $("#LblUpUnit").show();
        }

        function ResetLblUpMethodType() {
            $("#LblUpPrice").hide();
            $("#LblUpLot").hide();
            $("#LblUpLotInShare").hide();
            $("#LblUpVolume").hide();
            $("#LblUpUnit").hide();

            $("#UpOrderPrice").val(0)
            $("#UpLot").val(0)
            $("#UpVolume").val(0)


        }

        $("#UpLot").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: 0,
        });

        $("#UpVolume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeUpVolume,
            value: setUpVolume(),
        });

        function setUpVolume() {
            if (dataItemX.Lot == null) {
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
            format: "n4",
            decimals: 4,
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
            format: "n4",
            decimals: 4,
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

        WinUpdateInvestment.center();
        WinUpdateInvestment.open();
    }

    function OnChangeUpOrderPrice() {
        RecalculateUpPriceAndUnit();
    }

    function RecalculateUpPriceAndUnit() {
        if ($("#UpVolume").data("kendoNumericTextBox").value() != "" && $("#UpVolume").data("kendoNumericTextBox").value() != null && $("#UpVolume").data("kendoNumericTextBox").value() != 0 && $("#UpOrderPrice").data("kendoNumericTextBox").value() != 0) {
            $("#UpAmount").data("kendoNumericTextBox").value($("#UpOrderPrice").data("kendoNumericTextBox").value() * $("#UpVolume").data("kendoNumericTextBox").value())
        }
    }

    function OnChangeUpVolume() {
        RecalculateUpPriceAndUnit();
    }

    $("#BtnUpdateInvestmentSell").click(function () {
        var element = $("#BtnUpdateInvestmentSell");
        var posY = element.offset() - 50;
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

                RangePrice: $('#UpRangePrice').val(),
                OrderPrice: $('#UpOrderPrice').val(),
                Lot: $('#UpLot').val(),
                LotInShare: $('#UpLotInShare').val(),
                Volume: $('#UpVolume').val(),
                Amount: $('#UpAmount').val(),
                InvestmentNotes: $('#UpInvestmentNotes').val(),
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
                            url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#UpInstrumentPK').val() + "/" + $('#UpFundPK').val() + "/" + $('#UpOrderPrice').val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (dataCheckPrice) {
                                dataCheckPrice.Validate = 0;
                                if (dataCheckPrice.Validate == 0) {
                                    var Validate = {
                                        ValueDate: $('#DateFrom').val(),
                                        FundPK: $('#UpFundPK').val(),
                                        InstrumentPK: $('#UpInstrumentPK').val(),
                                        Volume: $('#UpVolume').val(),
                                        TrxBuy: $('#UpTrxBuy').val(),
                                        TrxBuyType: $('#UpTrxBuyType').val(),
                                        Amount: $('#UpAmount').val(),
                                        MethodType: $('#UpMethodType').val(),

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                            InstrumentPK: $('#UpInstrumentPK').val(),
                            Volume: $('#UpVolume').val(),
                            TrxBuy: $('#UpTrxBuy').val(),
                            TrxBuyType: $('#UpTrxBuyType').val(),
                            Amount: $('#UpAmount').val(),
                            MethodType: $('#UpMethodType').val(),

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                $.unblockUI();
                            }
                        });
                    }
                }
            });
            $.unblockUI();

        }).moveTo(posY.left, posY.top - 200);
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
        GenerateNavProjection();
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

        var newDS = getDataSourceInvesmentInstruction(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID);
        $("#gridInvestmentInstruction").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID);
        $("#gridInvestmentInstructionBuyOnly").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

        // Refresh Grid Bank Position Per Fund ( atas Layar )
        var newDS = getDataSourceA(window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridFundPosition").data("kendoGrid").setDataSource(newDS);

        // Re
        var newDS = getDataSourceFundExposurePerFund(window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridFundExposure").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSReksadanaBySectorID(window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_BySectorID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSReksadanaBySectorID").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSReksadanaByIndex(window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_ByIndex/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSReksadanaByIndex").data("kendoGrid").setDataSource(newDS);

        var newDS = getDataSourceOMSReksadanaByInstrument(window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridOMSReksadanaByInstrument").data("kendoGrid").setDataSource(newDS);

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
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID);
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
        var newDS = getDataSourceInvestmentInstructionBuySell(window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID);
        $("#gridInvestmentInstructionSellOnly").data("kendoGrid").setDataSource(newDS);

    }

    InitgridFundPosition();

    function InitgridFundPosition() {
        var AccountApprovedURL = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                fileName: "OMSReksadanaFundPosition.xlsx"
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
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_Exposure_PerFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                fileName: "OMSOMSReksadanaExposure.xlsx"
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

    InitgridOMSReksadanaBySectorID();

    function gridOMSReksadanaBySectorIDOnDataBound(e) {
        var grid = $("#gridOMSReksadanaBySectorID").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSReksadanaBySectorID(_url) {
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

    function InitgridOMSReksadanaBySectorID() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_BySectorID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
         dataSourceApproved = getDataSourceOMSReksadanaBySectorID(FundApprovedURL);

        gridOMSOMSReksadanaBySectorID = $("#gridOMSReksadanaBySectorID").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSReksadanaBySectorIDOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSReksadanaBySectorIDTemplate").html()),
            excel: {
                fileName: "OMSReksadanaBySector.xlsx"
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

    InitgridOMSReksadanaByIndex();

    function gridOMSReksadanaByIndexOnDataBound(e) {
        var grid = $("#gridOMSReksadanaByIndex").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSReksadanaByIndex(_url) {
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

    function InitgridOMSReksadanaByIndex() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_ByIndex/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
         dataSourceApproved = getDataSourceOMSReksadanaByIndex(FundApprovedURL);

        gridOMSReksadanaByIndex = $("#gridOMSReksadanaByIndex").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSReksadanaByIndexOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSReksadanaByIndexTemplate").html()),
            excel: {
                fileName: "OMSReksadanaByIndex.xlsx"
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


    InitgridOMSReksadanaByInstrument();

    function gridOMSReksadanaByInstrumentOnDataBound(e) {
        var grid = $("#gridOMSReksadanaByInstrument").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == 1) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
            }
            else if (row.UnRealized < 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedMin");
            } else if (row.UnRealized > 0) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaBySectorUnrealizedPlus");
            }
        });
    }

    function getDataSourceOMSReksadanaByInstrument(_url) {
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

    function InitgridOMSReksadanaByInstrument() {
        var FundApprovedURL = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadana_ByInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
         dataSourceApproved = getDataSourceOMSReksadanaByInstrument(FundApprovedURL);

        gridOMSReksadanaByInstrument = $("#gridOMSReksadanaByInstrument").kendoGrid({
            dataSource: dataSourceApproved,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            dataBound: gridOMSReksadanaByInstrumentOnDataBound,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "800px",
            toolbar: kendo.template($("#gridOMSReksadanaByInstrumentTemplate").html()),
            excel: {
                fileName: "OMSReksadanaByInstrument.xlsx"
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
                    fileName: "OMSReksadanaInvestmentInstruction.xlsx"
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
                        hidden: true, field: "Volume", title: "Unit", headerAttributes: {
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
    else {
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
                var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 4 + "/" + _fundID,
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
                    fileName: "OMSReksadanaInvestmentInstruction.xlsx"
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
                        field: "LotReksadana", title: "Volume/Unit", headerAttributes: {
                            style: "text-align: center"
                        }, width: 50, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }
                    },
                    {
                        hidden: true, field: "Volume", title: "Unit", headerAttributes: {
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeBuyOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID,
            dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }


        var gridReksadanaBuyOnly = $("#gridInvestmentInstructionBuyOnly").kendoGrid({
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
                fileName: "OMSReksadanaInvestmentInstruction.xlsx"
            },
            columns: [
                 ////{ command: { text: "R", click: RejectInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                 ////{ command: { text: "A", click: ApprovedInvestmentInstructionBuyOnly }, title: " ", width: 30 },
                 { command: { text: "Show", click: showDetailsBuy }, title: " ", width: 45 },
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
                    hidden: true, field: "SettledDate", title: "SettledDate", headerAttributes: {
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
                //    field: "Volume", title: "Unit", headerAttributes: {
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

        gridReksadanaBuyOnly.table.on("click", ".checkboxBuy", selectRowBuy);
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
            console.log(checked + ' ' + checked.length);
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


            var grid = $("#gridInvestmentInstructionBuyOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaBuyOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataReksadanaBuyOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 1 + "/Investment",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1 + "/Investment",
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
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateByFundFromToAndInstrumentTypeSellOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 6 + "/" + _fundID,
            dataSourceApproved = getDataSourceInvestmentInstructionBuySell(InvestmentInstructionApprovedURL);
        }


        var gridReksadanaSellOnly = $("#gridInvestmentInstructionSellOnly").kendoGrid({
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
                fileName: "OMSReksadanaInvestmentInstruction.xlsx"
            },
            columns: [
                 //{ command: { text: "R", click: RejectInvestmentInstructionSellOnly }, title: " ", width: 30 },
                 //{ command: { text: "A", click: ApprovedInvestmentInstructionSellOnly }, title: " ", width: 30 },
                 { command: { text: "Show", click: showDetailsSell }, title: " ", width: 45 },
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
                    hidden: true, field: "SettledDate", title: "SettledDate", headerAttributes: {
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
                //    hidden: true, field: "DoneUnit", title: "Unit", headerAttributes: {
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


        gridReksadanaSellOnly.table.on("click", ".checkboxSell", selectRowSell);
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
            console.log(checked + ' ' + checked.length);
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


            var grid = $("#gridInvestmentInstructionSellOnly").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectDataReksadanaSellOnly(_checked, _investmentPK);

        }

    }

    function SelectDeselectDataReksadanaSellOnly(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + 2 + "/Investment",
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2 + "/Investment",
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
                             SettledDate: { type: "date" },
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

                         }
                     }
                 },
                 group:
                     [
                     {
                         field: "OrderStatusDesc", aggregates: [
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

    function gridInvestmentInstructionDataBound() {
        var grid = $("#gridInvestmentInstruction").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.OrderStatusDesc == "4.MATCH") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.OrderStatusDesc == "3.OPEN") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
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

        if (_bs == 2) {
            $("#BitBreakable").prop("disabled", true);
            $("#lblSettledDate").show();
        }
        else {
            if (_bs == 1) {
                $("#lblSettledDate").hide();
            }
            $("#BitBreakable").prop("disabled", false);
            $("#BitBreakable").prop('checked', true);
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


        if (_GlobClientCode == "11") {
            InitgridFundExposure();
        }

        $("#MethodType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                { text: "Unit", value: "1" },
                { text: "Amount", value: "2" },
            ],
            change: onChangeMethodType,
            value: setCmbMethodType()

        });

        function onChangeMethodType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#MethodType").val() == 1) {
                ResetLblMethodType();
                $("#LblLot").hide();
                $("#LblLotInShare").show();
                $("#LblUnit").show();
                $("#LblAmount").hide();
                $("#Amount").attr('readonly', false);
            }
            else if ($("#MethodType").val() == 2) {
                $("#LblAmount").show();
                $("#LblUnit").hide();
                $("#Amount").attr('readonly', false);
                ResetLblMethodType();
            }
            else {
                $("#LblAmount").hide();
                $("#LblUnit").hide();
            }
        }


        function setCmbMethodType() {
            ResetMethodType();
            if (_GlobClientCode == "11") {
                if (_bs == 2) {
                    $("#LblUnit").show();
                    $("#LblPrice").show();
                    return 1;
                }
                else {
                    return 2;
                }

            }
            else {
                return "";
            }
        }

        function ResetMethodType() {
            $("#LblLot").hide();
            $("#LblLotInShare").hide();
            $("#LblUnit").hide();
            $("#LblAmount").hide();
        }

        function ResetLblMethodType() {
            $("#LblPrice").hide();
            $("#LblLot").hide();
            $("#LblLotInShare").hide();
            $("#LblUnit").hide();
            $("#OrderPrice").data("kendoNumericTextBox").value(0);
            $("#Amount").data("kendoNumericTextBox").value(0);
            $("#Volume").data("kendoNumericTextBox").value(0);

        }


        $("#Lot").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: 0
        });




        $("#Volume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeVolume
        });

        $("#OrderPrice").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: OnChangeOrderPrice

        });

        $("#Amount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: 0,
        });

        $("#AcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });

        //$("#Amount").data("kendoNumericTextBox").enable(false);

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

    function OnChangeOrderPrice() {
        RecalculatePriceAndUnit();
    }

    function RecalculatePriceAndUnit() {
        if ($("#Volume").data("kendoNumericTextBox").value() != "" && $("#Volume").data("kendoNumericTextBox").value() != null && $("#Volume").data("kendoNumericTextBox").value() != 0 && $("#OrderPrice").data("kendoNumericTextBox").value() != 0) {
            $("#Amount").data("kendoNumericTextBox").value($("#OrderPrice").data("kendoNumericTextBox").value() * $("#Volume").data("kendoNumericTextBox").value())
        }
    }

    function OnChangeVolume() {
        //$("#Amount").data("kendoNumericTextBox").value($("#Unit").data("kendoNumericTextBox").value() * $("#OrderPrice").val());
        RecalculatePriceAndUnit();
    }

    

    $("#BtnSaveBuy").click(function (e) {
        var element = $("#BtnSaveBuy");
        var posY = element.offset();
        var val = validateData();
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
                        //$.ajax({
                        //    url: window.location.origin + "/Radsoft/OMSEquity/ValidateCheckAvailableCash/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Amount').val() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val(),
                        //    type: 'GET',
                        //    contentType: "application/json;charset=utf-8",
                        //    success: function (data) {
                        //        if (data == true) {
                        //            $.unblockUI();
                        //            alertify.alert("Cash Not Available").moveTo(posY.left, posY.top - 200);
                        //            return;
                        //        }
                        //        else {
                        if ($('#OrderPrice').val() > 0) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    data.Validate = 0;
                                    if (data.Validate == 0) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                //alertify.alert(data.AlertExposure);
                                                if (data.AlertExposure == 1) {
                                                    alertify.confirm("Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % and Max Exposure Percent : " + data.MaxExposurePercent + " % </br> Are You Sure To Continue ?", function (e) {
                                                        if (e) {
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
                                                                        InstrumentTypePK: 6,
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
                                                                        PriceMode: 1,
                                                                        FundPK: $('#FundPK').val(),
                                                                        MarketPK: $('#MarketPK').val(),
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        BitBreakable: $('#BitBreakable').is(":checked"),
                                                                        SettledDate: $('#SettledDate').val(),
                                                                        EntryUsersID: sessionStorage.getItem("user")
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
                                                        $.unblockUI();
                                                    }).moveTo(posY.left, posY.top - 200);
                                                }
                                                else if (data.AlertExposure == 2) {
                                                    $.unblockUI();
                                                    alertify.alert("Can't Process This Data </br> Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % </br> and MaxExposure Percent : " + data.MaxExposurePercent + " %").moveTo(posY.left, posY.top - 200);
                                                    return;
                                                }
                                                else if (data.AlertExposure == 3) {

                                                    alertify.confirm("Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % and Min Exposure Percent : " + data.MinExposurePercent + " % </br> Are You Sure To Continue ?", function (e) {
                                                        if (e) {
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
                                                                        InstrumentTypePK: 6,
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
                                                                        PriceMode: 1,
                                                                        FundPK: $('#FundPK').val(),
                                                                        MarketPK: $('#MarketPK').val(),
                                                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                                                        BitBreakable: $('#BitBreakable').is(":checked"),
                                                                        SettledDate: $('#SettledDate').val(),
                                                                        EntryUsersID: sessionStorage.getItem("user")
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

                                                    }).moveTo(posY.left, posY.top - 200);
                                                }
                                                else if (data.AlertExposure == 4) {
                                                    $.unblockUI();
                                                    alertify.alert("Can't Process This Data </br> Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % </br> and MinExposure Percent : " + data.MinExposurePercent + " %").moveTo(posY.left, posY.top - 200);
                                                    return;
                                                }
                                                else {
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
                                                                InstrumentTypePK: 6,
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
                                                                PriceMode: 1,
                                                                FundPK: $('#FundPK').val(),
                                                                MarketPK: $('#MarketPK').val(),
                                                                InvestmentNotes: $('#InvestmentNotes').val(),
                                                                BitBreakable: $('#BitBreakable').is(":checked"),
                                                                SettledDate: $('#SettledDate').val(),
                                                                EntryUsersID: sessionStorage.getItem("user")
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
                                url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#Amount').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data.AlertExposure == 1) {

                                        alertify.confirm("Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % and Max Exposure Percent : " + data.MaxExposurePercent + " % </br> Are You Sure To Continue ?", function (e) {
                                            if (e) {
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
                                                            InstrumentTypePK: 6,
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
                                                            PriceMode: 1,
                                                            FundPK: $('#FundPK').val(),
                                                            MarketPK: $('#MarketPK').val(),
                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                            BitBreakable: $('#BitBreakable').is(":checked"),
                                                            SettledDate: $('#SettledDate').val(),
                                                            EntryUsersID: sessionStorage.getItem("user")
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

                                        }).moveTo(posY.left, posY.top - 200);
                                    }
                                    else if (data.AlertExposure == 2) {
                                        $.unblockUI();
                                        alertify.alert("Can't Process This Data </br> Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % </br> and MaxExposure Percent : " + data.MaxExposurePercent + " %").moveTo(posY.left, posY.top - 200);
                                        return;
                                    }
                                    else if (data.AlertExposure == 3) {
                                        alertify.confirm("Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % and Min Exposure Percent : " + data.MinExposurePercent + " % </br> Are You Sure To Continue ?", function (e) {
                                            if (e) {
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
                                                            InstrumentTypePK: 6,
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
                                                            PriceMode: 1,
                                                            FundPK: $('#FundPK').val(),
                                                            MarketPK: $('#MarketPK').val(),
                                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                                            BitBreakable: $('#BitBreakable').is(":checked"),
                                                            SettledDate: $('#SettledDate').val(),
                                                            EntryUsersID: sessionStorage.getItem("user")
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

                                        }).moveTo(posY.left, posY.top - 200);
                                    }
                                    else if (data.AlertExposure == 4) {
                                        $.unblockUI();
                                        alertify.alert("Can't Process This Data </br> Exposure " + data.ExposureID + " : " + data.ExposurePercent + " % </br> and MinExposure Percent : " + data.MinExposurePercent + " %").moveTo(posY.left, posY.top - 200);
                                        return;
                                    }
                                    else {
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
                                                    InstrumentTypePK: 6,
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
                                                    PriceMode: 1,
                                                    FundPK: $('#FundPK').val(),
                                                    MarketPK: $('#MarketPK').val(),
                                                    InvestmentNotes: $('#InvestmentNotes').val(),
                                                    BitBreakable: $('#BitBreakable').is(":checked"),
                                                    SettledDate: $('#SettledDate').val(),
                                                    EntryUsersID: sessionStorage.getItem("user")
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

                        //        }
                        //        $.unblockUI();
                        //    }

                        //});
                    }
                    $.unblockUI();
                }

            });
        }
    });




    $("#BtnSaveSell").click(function () {
        var element = $("#BtnSaveSell");
        var posY = element.offset();
        var val = validateData();
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
                        if ($('#OrderPrice').val() > 0) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckPriceExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#InstrumentPK').val() + "/" + $('#FundPK').val() + "/" + $('#OrderPrice').val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    data.Validate = 0;
                                    if (data.Validate == 0) {
                                        var Validate = {
                                            ValueDate: $('#DateFrom').val(),
                                            FundPK: $('#FundPK').val(),
                                            InstrumentPK: $('#InstrumentPK').val(),
                                            Volume: $('#Volume').val(),
                                            TrxBuy: $('#TrxBuy').val(),
                                            TrxBuyType: $('#TrxBuyType').val(),
                                            Amount: $('#Amount').val(),
                                            MethodType: $('#MethodType').val(),

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                                InstrumentTypePK: 6,
                                                                OrderPrice: $('#OrderPrice').val(),
                                                                RangePrice: $('#RangePrice').val(),

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
                                                                TrxBuy: $('#TrxBuy').val(),
                                                                TrxBuyType: $('#TrxBuyType').val(),

                                                                PriceMode: 1,
                                                                //AcqDate: $('#AcqDate').val(),
                                                                AcqPrice: $('#AcqPrice').val(),
                                                                BitBreakable: $('#BitBreakable').is(":checked"),
                                                                SettledDate: $('#SettledDate').val(),
                                                                EntryUsersID: sessionStorage.getItem("user")
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
                                InstrumentPK: $('#InstrumentPK').val(),
                                Volume: $('#Volume').val(),
                                TrxBuy: $('#TrxBuy').val(),
                                TrxBuyType: $('#TrxBuyType').val(),
                                Amount: $('#Amount').val(),
                                MethodType: $('#MethodType').val(),

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSReksadana/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                    InstrumentTypePK: 6,
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
                                                    TrxBuy: $('#TrxBuy').val(),
                                                    TrxBuyType: $('#TrxBuyType').val(),

                                                    PriceMode: 1,
                                                    //AcqDate: $('#AcqDate').val(),
                                                    AcqPrice: $('#AcqPrice').val(),
                                                    BitBreakable: $('#BitBreakable').is(":checked"),
                                                    SettledDate: $('#SettledDate').val(),
                                                    EntryUsersID: sessionStorage.getItem("user")
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
                                }
                            });
                        }
                    }
                    $.unblockUI();
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
                                 Amount: { type: "number" }
                             }
                         }
                     }
                 });
    }

    $("#NetCashAvailable").on("click", function () {
        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }

        var _url = window.location.origin + "/Radsoft/OMSReksadana/OMSReksadanaGetNetAvailableCashDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundID + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy");
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
            columns: [
               { field: "FSource", title: "Source", width: 400 },
               { field: "Amount", title: "Amount", format: "{0:n2}", width: 200 }
            ]
        });

        WinListAvailableCashDetail.open();
    });


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
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
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
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSReksadanaByInstrumentYellow");
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


    $("#BtnApproveBySelectedReksadanaBuy").click(function (e) {

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
                    InstrumentTypePK: 4,
                    TrxType: 1,
                    FundID: $("#FilterFundID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateFrom').val(),
                    stringInvestmentFrom: stringInvestmentFrom,

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSReksadana/ValidateApproveBySelectedDataOMSReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                InstrumentTypePK: 6,
                                TrxType: 1,
                                FundID: $("#FilterFundID").val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateFrom').val(),
                                ApprovedUsersID: sessionStorage.getItem("user"),
                                stringInvestmentFrom: stringInvestmentFrom,

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSReksadana/ApproveOMSReksadanaBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(Investment),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    refresh();
                                    alertify.alert("Approved Investment Success");
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
        });
    }
    });

    $("#BtnRejectBySelectedReksadanaBuy").click(function (e) {

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
                    InstrumentTypePK: 6,
                    TrxType: 1,
                    FundID: $("#FilterFundID").val(),
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateFrom').val(),
                    stringInvestmentFrom: stringInvestmentFrom,

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/OMSReksadana/ValidateRejectBySelectedDataOMSReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                InstrumentTypePK: 6,
                                TrxType: 1,
                                FundID: $("#FilterFundID").val(),
                                DateFrom: $('#DateFrom').val(),
                                DateTo: $('#DateFrom').val(),
                                VoidUsersID: sessionStorage.getItem("user"),
                                stringInvestmentFrom: stringInvestmentFrom,

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/OMSReksadana/RejectOMSReksadanaBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    $("#BtnApproveBySelectedReksadanaSell").click(function (e) {

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
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateApproveBySelectedDataOMSReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSReksadana/ApproveOMSReksadanaBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(Investment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refresh();
                                        alertify.alert("Approved Investment Success");
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
            });
        }
    });

    $("#BtnRejectBySelectedReksadanaSell").click(function (e) {

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
                        InstrumentTypePK: 6,
                        TrxType: 2,
                        FundID: $("#FilterFundID").val(),
                        DateFrom: $('#DateFrom').val(),
                        DateTo: $('#DateFrom').val(),
                        stringInvestmentFrom: stringInvestmentFrom,

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/OMSReksadana/ValidateRejectBySelectedDataOMSReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                    InstrumentTypePK: 6,
                                    TrxType: 2,
                                    FundID: $("#FilterFundID").val(),
                                    DateFrom: $('#DateFrom').val(),
                                    DateTo: $('#DateFrom').val(),
                                    VoidUsersID: sessionStorage.getItem("user"),
                                    stringInvestmentFrom: stringInvestmentFrom,

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/OMSReksadana/RejectOMSReksadanaBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


        //var _date = kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
        //if (_date == null || _date == "") {
        //    alertify.alert("Please Fill Date");
        //    return;
        //}
        //if ($("#FilterFundID").val() == "") {
        //    _fundID = "0";
        //}
        //else {
        //    _fundID = $("#FilterFundID").val();
        //}
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        //if (data == false) {
        //        $.ajax({
        //            url: window.location.origin + "/Radsoft/OMSEquity/GenerateNavProjection/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date + "/" + _fundID,
        //            type: 'GET',
        //            contentType: "application/json;charset=utf-8",
        //            success: function (data) {
        //                if (data != null) {
        //                    if (data.Nav != 0 || data.Nav != null) {
        //                        $('#NavProjection').val(data.Nav);
        //                        $('#NavProjection').text(kendo.toString(data.Nav, "n"));
        //                        $('#ComparePercent').val(data.Compare);
        //                        $('#ComparePercent').text(kendo.toString(data.Compare / 100, "p"));
        //                    }
        //                    else {
        //                        $('#NavProjection').val(0);
        //                        $('#NavProjection').text(kendo.toString(0, "n"));
        //                        $('#ComparePercent').val(0);
        //                        $('#ComparePercent').text(kendo.toString(0, "p"));
        //                    }
        //                }

        //            }
        //        });


        //        //} else {
        //        //    alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
        //        //}
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


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
                        //AcqDate: { type: "date" },
                        Balance: { type: "number" },
                        TrxBuy: { type: "number" },
                        TrxBuyType: { type: "string" },
                    }
                }

            },
        });
    }


    function initListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSReksadanaByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TrxType").data("kendoComboBox").value() + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#MarketPK").val();
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
                    { field: "Balance", title: "Unit", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    //{ field: "AcqDate", title: "AcqDate", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                    //{ field: "Price", title: "Price", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    //{ field: "TrxBuy", title: "TrxBuy", hidden: true, width: 150 },
                    //{ field: "TrxBuyType", title: "TrxBuyType", hidden: true, width: 150 },
                ]
            });
        }
    }


    function ListInstrumentSelect(e) {


        if (e.handled !== true) // This will prevent event triggering more then once
        {
            var grid = $("#gridListInstrument").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.BitBreakable == false) {
                alertify.alert("Instrument is Not Breakable !");
                return;
            }
            else {
                $("#InstrumentPK").val(dataItemX.InstrumentPK);
                $("#InstrumentID").val(dataItemX.ID);
                $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#Lot").data("kendoNumericTextBox").value(0);
                $("#TrxBuy").val(dataItemX.TrxBuy);
                $("#TrxBuyType").val(dataItemX.TrxBuyType);
                //$("#AcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
                $("#AcqPrice").data("kendoNumericTextBox").value(dataItemX.Price);
                $("#BitBreakable").prop('checked', dataItemX.BitBreakable);
                WinListInstrument.close();
            }
        }
        e.handled = true;
    }



    $("#btnClearListInstrument").click(function () {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#Lot").data("kendoNumericTextBox").value(0);
        $("#Volume").data("kendoNumericTextBox").value(0);
        $("#Amount").data("kendoNumericTextBox").value(0);
        $("#OrderPrice").data("kendoNumericTextBox").value(0);
        $("#TrxBuy").val("");
        $("#TrxBuyType").val("");
        //$("#AcqDate").data("kendoDatePicker").value("");
        $("#AcqPrice").data("kendoNumericTextBox").value(0);
    });




    function initUpListInstrumentPK() {

        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentLookupForOMSReksadanaByMarketPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpTrxType").data("kendoComboBox").value() + "/" + $("#UpFundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#UpMarketPK").val();
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
                   { field: "Balance", title: "Unit", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
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
        $("#UpInstrumentID").val("");
        $("#UpLot").data("kendoNumericTextBox").value(0);
        $("#UpVolume").data("kendoNumericTextBox").value(0);
        $("#UpAmount").data("kendoNumericTextBox").value(0);
        $("#UpTrxBuy").val("");
        $("#UpTrxBuyType").val("");
        $("#UpOrderPrice").data("kendoNumericTextBox").value(0);
    });




    $("#BtnPTPReksadana").click(function () {

        alertify.confirm("Are you sure want to Download PTP ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/PTPByReksadana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
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
        });

    });

    $("#BtnCancelPTPReksadana").click(function () {

        alertify.confirm("Are you sure want to cancel PTP?", function (e) {
            if (e) {
                WinPTPReksadana.close();
                alertify.alert("Cancel PTP");
            }
        });
    });


});

