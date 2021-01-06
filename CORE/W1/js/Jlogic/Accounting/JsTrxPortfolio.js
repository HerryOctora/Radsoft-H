$(document).ready(function () {
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var WinListCounterpart;
    var WinTrxPortfolioReport;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var GlobInstrumentType;
    var GlobTrxType;
    var gridHeight = screen.height - 330;
    var WinListReference;
    var GlobDecimalPlaces;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

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

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRevise").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRevise.png"
        });

        $("#BtnVoucher").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnReverseBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReverseAll.png"
        });
        $("#BtnRefreshDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAddDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnClose").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnReportTrxPortfolioValuation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnReportTrxPortfolioValuationByAccount").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnPostingWithoutBankBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });

    }

   

    function initWindow() {

        $("#FilterInstrumentType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                    { text: "EQUITY", value: "1" },
                    { text: "BOND", value: "2" },
                    { text: "DEPOSITO", value: "3" },
                    { text: "REKSADANA", value: "4" }
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

        ////GetJournalDecimalPlaces
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/AccountingSetup/GetJournalDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        GlobDecimalPlaces = data;
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        $("#MaturityDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });
        $("#LastCouponDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });
        $("#NextCouponDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });
        $("#SettledDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeSettledDate
        });
        $("#AcqDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate1").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate2").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate3").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate4").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate5").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate6").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate7").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate8").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#AcqDate9").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAcqDate
        });
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value : new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo
        });

        function OnChangeValueDate() {
            var _regDays;
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
            if ($("#ValueDate").data("kendoDatePicker").value() == null) {
                $("#SettledDate").data("kendoDatePicker").value(null);
            } else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/SettlementSetup/GetRegDays/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/0",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        _regDays = data;
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _regDays,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#SettledDate").data("kendoDatePicker").value(new Date(data));
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
        }
        function OnChangeDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            refresh();
        }

        win = $("#WinTrxPortfolio").kendoWindow({
            height: "1200px",
            title: " Portfolio Detail",
            visible: false,
            width: "1200px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },

            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: "400px",
            title: "Old Data",
            visible: false,
            width: "400px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

            close: CloseOldData
        }).data("kendoWindow");


        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: "450px",
            title: "List Instrument ",
            visible: false,
            width: "1150px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 120 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

        //WinListCounterpart = $("#WinListCounterpart").kendoWindow({
        //    height: "450px",
        //    title: "List Counterpart ",
        //    visible: false,
        //    width: "870px",
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 120 })
        //    },
        //    close: onWinListCounterpartClose
        //}).data("kendoWindow");

        WinTrxPortfolioReport = $("#WinTrxPortfolioReport").kendoWindow({
            height: "300px",
            title: "* Check TrxPortfolio ",
            visible: false,
            width: "500px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },
            close: onWinTrxPortfolioReportClose
        }).data("kendoWindow");


        WinListReference = $("#WinListReference").kendoWindow({
            height: 500,
            title: "List Reference ",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: onWinListReferenceClose
        }).data("kendoWindow");
    }

    function CloseOldData() {
        $("#OldData").text("");

    }

    var GlobValidator = $("#WinTrxPortfolio").kendoValidator().data("kendoValidator");


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

    function HideLabel()
    {
        $("#LblPrice").hide();
        $("#LblAcqPrice").hide();
        $("#LblLot").hide();
        $("#LblLotInShare").hide();
        $("#LblSettlementDate").hide();
        $("#LblAcqDate").hide();
        $("#LblAcqVolume").hide();
        $("#LblMaturityDate").hide();
        $("#LblLastCouponDate").hide();
        $("#LblNextCouponDate").hide();
        $("#LblInterestPercent").hide();
        $("#LblBrokerageFeePercent").hide();
        $("#LblBrokerageFeeAmount").hide();
        $("#LblInterestAmount").hide();
        $("#LblGrossAmount").hide();
        $("#LblIncomeTaxGainAmount").hide();
        $("#LblIncomeTaxInterestAmount").hide();
        $("#LblNetAmount").hide();
        $("#LblAcquisition").hide();
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
        $("#LblLevyAmount").hide();
        $("#LblVATAmount").hide();
        $("#LblKPEIAmount").hide();
        $("#LblWHTAmount").hide();
        $("#LblOTCAmount").hide();
        $("#LblIncomeTaxSellAmount").hide();
        $("#LblRealisedAmount").hide();

        $("#RowBank").hide();
        $("#RowBankBranch").hide();
        $("#RowCategory").hide();

        $("#LblCloseNav").hide();
        $("#LblVolume").hide();
        $("#LblNominal").hide();
        $("#LblUnit").hide();

    }

    function showDetails(e) {

        //GlobInstrumentType = 0;
        var buttonObjectListInstrumentPK = $("#btnListInstrumentPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")

        var buttonObjectListCounterpartPK = $("#BtnListCounterpartPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")

        if ($("#InstrumentTypePK").val() == null) {
            ($("#InstrumentTypePK").val() == 0)
        }



        var dataItemX;
        if (e == null) {
            HideLabel();
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#StatusHeader").val("NEW");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
            $("#LblPrice").show();
            $("#LblVolume").show();

            if (_GlobClientCode == "33") {
                $("#CounterpartPK").attr("required", false);
            }

            //$("#CounterpartPK").val(1);
            //$("#CounterpartID").val("RHB OSK SEC - PT. RHB OSK SECURITIES INDONESIA");

        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridTrxPortfolioApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridTrxPortfolioPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridTrxPortfolioHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnUnApproved").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").show();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();

            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("REVISED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").show();
                $("#BtnPosting").show();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();

            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").hide();
                $("#BtnPosting").show();
                $("#BtnRevise").hide();
                $("#BtnVoid").show();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("HISTORY");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
            }

            dirty = null;
            //initTrxType();
            $("#TrxPortfolioPK").val(dataItemX.TrxPortfolioPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#PeriodPK").val(dataItemX.PeriodPK);
            $("#PeriodID").val(dataItemX.PeriodID);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            $("#Reference").val(dataItemX.Reference);
            $("#InstrumentTypePK").val(dataItemX.InstrumentTypePK);

            if (dataItemX.InstrumentTypePK == 3) {
                GlobInstrumentType = 5;
            }
            else {
                GlobInstrumentType = dataItemX.InstrumentTypePK;
            }
            GlobTrxType = dataItemX.TrxType;
            $("#InstrumentTypeID").val(dataItemX.InstrumentTypeID);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            //$("#Price").val(dataItemX.Price);
            //$("#AcqPrice").val(dataItemX.AcqPrice);
            $("#Lot").val(dataItemX.Lot);
            //$("#Volume").val(dataItemX.Volume);
            //$("#Amount").val(dataItemX.Amount);
            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
            $("#SettledDate").data("kendoDatePicker").value(new Date(dataItemX.SettledDate));
            $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            $("#LastCouponDate").data("kendoDatePicker").value(new Date(dataItemX.LastCouponDate));
            $("#NextCouponDate").data("kendoDatePicker").value(new Date(dataItemX.NextCouponDate));
            $("#InterestPercent").val(dataItemX.InterestPercent);
            $("#CashRefPK").val(dataItemX.CashRefPK);
            $("#CashRefID").val(dataItemX.CashRefID);
            $("#BrokerageFeePercent").val(dataItemX.BrokerageFeePercent);
            $("#BrokerageFeeAmount").val(dataItemX.BrokerageFeeAmount);
            $("#AcqDate").data("kendoDatePicker").value(new Date(dataItemX.AcqDate));
            $("#AcqDate1").data("kendoDatePicker").value(new Date(dataItemX.AcqDate1));
            $("#AcqDate2").data("kendoDatePicker").value(new Date(dataItemX.AcqDate2));
            $("#AcqDate3").data("kendoDatePicker").value(new Date(dataItemX.AcqDate3));
            $("#AcqDate4").data("kendoDatePicker").value(new Date(dataItemX.AcqDate4));
            $("#AcqDate5").data("kendoDatePicker").value(new Date(dataItemX.AcqDate5));
            $("#AcqDate6").data("kendoDatePicker").value(new Date(dataItemX.AcqDate6));
            $("#AcqDate7").data("kendoDatePicker").value(new Date(dataItemX.AcqDate7));
            $("#AcqDate8").data("kendoDatePicker").value(new Date(dataItemX.AcqDate8));
            $("#AcqDate9").data("kendoDatePicker").value(new Date(dataItemX.AcqDate9));
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Revised == false) {
                $("#State").removeClass("Posted").removeClass("Revised").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");
                $("#Posted").prop('checked', false);
                $("#Revised").prop('checked', false);
            }

            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Revised").addClass("Posted");
                $("#State").text("POSTED");
                $("#Revised").prop('checked', false);
            }

            if (dataItemX.Revised == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Revised");
                $("#State").text("REVISED");
                $("#Revised").prop('checked', true);
            }

            if (dataItemX.UnPosted == true) {
                $("#UnPosted").prop('checked', true);
            }

            $("#PostedBy").text(dataItemX.PostedBy);
            $("#PostedTime").text(dataItemX.PostedTime);
            $("#RevisedBy").text(dataItemX.RevisedBy);
            $("#RevisedTime").text(dataItemX.RevisedTime);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(dataItemX.EntryTime);
            $("#UpdateTime").val(dataItemX.UpdateTime);
            $("#ApprovedTime").val(dataItemX.ApprovedTime);
            $("#VoidTime").val(dataItemX.VoidTime);
            $("#LastUpdate").val(dataItemX.LastUpdate);


        }
        if ($("#InstrumentTypePK").val() == "" || $("#InstrumentTypePK").val() == 0) {
            GlobInstrumentType = "0";
        }
        else if ($("#InstrumentTypePK").val() == 3) {
            GlobInstrumentType = 5;
        }
        else {
            GlobInstrumentType = $("#InstrumentTypePK").val();
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionType" + "/" + GlobInstrumentType,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
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
            ShowHideLabelInstrument(GlobInstrumentType, this.value());
            RecalNetAmount();
            if ($("#TrxType").val() == 1)
                $("#LblRealisedAmount").hide();
            else
                $("#LblRealisedAmount").show();

            if (GlobInstrumentType == 5)
                $("#LblRealisedAmount").hide();
        }

        function setCmbTrxType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TrxType;
            }
        }



        $("#Volume").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setVolume(),
            change: OnChangeVolume


        });
        function setVolume() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Volume;
            }
        }

        function OnChangeVolume() {
            $("#Lot").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() / 100);
            if (GlobInstrumentType == 2) {
                price = $("#Price").data("kendoNumericTextBox").value() / 100
                $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * price);
                RecalNetAmount();
            } else {
                $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * $("#Price").data("kendoNumericTextBox").value());
                RecalNetAmount();
            }
        }


        $("#Amount").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setAmount(),
            change: OnChangeAmount


        });
        function setAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Amount;
            }
        }


        function OnChangeAmount() {
            $("#Lot").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() / 100);
            if ($("#InstrumentTypePK").val() == 4) {
                price = $("#Price").data("kendoNumericTextBox").value();
                $("#Volume").data("kendoNumericTextBox").value($("#Amount").val() / price);
                RecalNetAmount();
            }
            else {
                $("#Volume").data("kendoNumericTextBox").value($("#Amount").data("kendoNumericTextBox").value() / $("#Price").data("kendoNumericTextBox").value());
                RecalNetAmount();
            }
        }




        $("#Lot").kendoNumericTextBox({
            format: "n0",
            value: setLot(),
            change: OnChangeLot

        });
        function setLot() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Lot;
            }
        }

        function OnChangeLot() {
            $("#Volume").data("kendoNumericTextBox").value($("#Lot").data("kendoNumericTextBox").value() * 100);
            $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * $("#Price").data("kendoNumericTextBox").value());
            RecalNetAmount();
        }

        $("#InterestPercent").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setInterestPercent(),

        });
        function setInterestPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InterestPercent;
            }
        }



        $("#LotInShare").kendoNumericTextBox({
            format: "n0",
            value: 100,
        });


        $("#Price").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setPrice(),
            change: OnChangePrice
        });
        function setPrice() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Price;
            }
        }



        $("#AcqPrice").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice;
            }
        }


        $("#AcqPrice1").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice1(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice1;
            }
        }


        $("#AcqPrice2").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice2(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice2;
            }
        }


        $("#AcqPrice3").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice3(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice3;
            }
        }




        $("#AcqPrice4").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice4(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice4;
            }
        }



        $("#AcqPrice5").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice5(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice5;
            }
        }



        $("#AcqPrice6").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice6(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice6() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice6;
            }
        }



        $("#AcqPrice7").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice7(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice7() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice7;
            }
        }



        $("#AcqPrice8").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice8(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice8() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice8;
            }
        }



        $("#AcqPrice9").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqPrice9(),
            change: OnChangeAcqPrice
        });
        function setAcqPrice9() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqPrice9;
            }
        }



        $("#AcqVolume").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume;
            }
        }


        $("#AcqVolume1").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume1(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume1;
            }
        }



        $("#AcqVolume2").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume2(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume2;
            }
        }



        $("#AcqVolume3").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume3(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume3;
            }
        }




        $("#AcqVolume4").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume4(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume4;
            }
        }



        $("#AcqVolume5").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume5(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume5;
            }
        }



        $("#AcqVolume6").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume6(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume6() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume6;
            }
        }



        $("#AcqVolume7").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume7(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume7() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume7;
            }
        }



        $("#AcqVolume8").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume8(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume8() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume8;
            }
        }



        $("#AcqVolume9").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAcqVolume9(),
            change: OnChangeAcqVolume
        });
        function setAcqVolume9() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AcqVolume9;
            }
        }


        function OnChangePrice() {
            if (GlobInstrumentType == 2) {
                price = $("#Price").data("kendoNumericTextBox").value() / 100
                $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * price);
                RecalNetAmount();
            } else {
                $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * $("#Price").data("kendoNumericTextBox").value());
                RecalNetAmount();
            }

        }

        function OnChangeAcqPrice() {
            if (GlobInstrumentType == 2) {
                RecalNetAmount();
            }
        }

        function OnChangeAcqVolume() {
            if (GlobInstrumentType == 2) {
                RecalNetAmount();
            }
        }



        //$("#Amount").kendoNumericTextBox({
        //    format: "n8",
        //    decimals: 8,
        //    value: setAmount()

        //});
        //function setAmount() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.Amount;
        //    }
        //}




        $("#BrokerageFeePercent").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setBrokerageFeePercent()

        });
        function setBrokerageFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.BrokerageFeePercent;
            }
        }


        $("#BrokerageFeeAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setBrokerageFeeAmount()

        });
        function setBrokerageFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.BrokerageFeeAmount;
            }
        }

        $("#InterestAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setInterestAmount()

        });
        function setInterestAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InterestAmount;
            }
        }
        $("#InterestAmount").data("kendoNumericTextBox").enable(true);

        $("#GrossAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setGrossAmount()

        });
        function setGrossAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.GrossAmount;
            }
        }
        $("#GrossAmount").data("kendoNumericTextBox").enable(false);

        $("#IncomeTaxGainAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setIncomeTaxGainAmount()

        });
        function setIncomeTaxGainAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.IncomeTaxGainAmount;
            }
        }
        $("#IncomeTaxGainAmount").data("kendoNumericTextBox").enable(true);


        $("#IncomeTaxInterestAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setIncomeTaxInterestAmount()

        });
        function setIncomeTaxInterestAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.IncomeTaxInterestAmount;
            }
        }
        $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").enable(true);

        $("#LevyAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setLevyAmount()

        });
        function setLevyAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.LevyAmount;
            }
        }

        $("#KPEIAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setKPEIAmount()

        });
        function setKPEIAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.KPEIAmount;
            }
        }

        $("#VATAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setVATAmount()

        });
        function setVATAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.VATAmount;
            }
        }

        $("#WHTAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setWHTAmount()

        });
        function setWHTAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.WHTAmount;
            }
        }

        $("#OTCAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setOTCAmount()

        });
        function setOTCAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.OTCAmount;
            }
        }


        $("#IncomeTaxSellAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            change: NetRecalculate,
            value: setIncomeTaxSellAmount()

        });
        function setIncomeTaxSellAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.IncomeTaxSellAmount;
            }
        }

        $("#RealisedAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setRealisedAmount()

        });
        function setRealisedAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RealisedAmount;
            }
        }

        $("#NetAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNetAmount()

        });
        function setNetAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NetAmount;
            }
        }
        $("#NetAmount").data("kendoNumericTextBox").enable(false);

        //combo box PeriodPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    template: "<table><tr><td width='100px'>${ID}</td><td width='100px'>${Description}</td></tr></table>",
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
            if (e == null) {
                return "";
            } else {
                return dataItemX.PeriodPK;
            }
        }

        //combo box Counterpart
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
                    change: OnChangeCounterpartPK,
                    value: setCmbCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCounterpartPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            RecalNetAmount();


        }
        function setCmbCounterpartPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CounterpartPK == 0) {
                    return "";
                } else {
                    return dataItemX.CounterpartPK;
                }
            }
        }


        //combo box CompanyAccountTrading
        $.ajax({
            url: window.location.origin + "/Radsoft/CompanyAccountTrading/GetCompanyAccountTradingCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CompanyAccountTradingPK").kendoComboBox({
                    dataValueField: "CompanyAccountTradingPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeCompanyAccountTrading,
                    value: setCmbCompanyAccountTradingPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCompanyAccountTrading() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#CounterpartPK").val("");
            $.ajax({
                url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboByCompanyAccountTradingPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CompanyAccountTradingPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#CounterpartPK").kendoComboBox({
                        dataValueField: "CounterpartPK",
                        dataTextField: "ID",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        change: OnChangeCounterpartPK,
                    });

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function OnChangeCounterpartPK() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
                RecalNetAmount();


            }


        }
        function setCmbCompanyAccountTradingPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CompanyAccountTradingPK == 0) {
                    return "";
                } else {
                    return dataItemX.CompanyAccountTradingPK;
                }
            }
        }


        //Instrument Type

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        $("#InstrumentTypePK").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                { text: "EQUITY", value: "1" },
                { text: "BOND", value: "2" },
                { text: "DEPOSITO", value: "3" },
                { text: "REKSADANA", value: "4" }
            ],
            change: onChangeInstrumentType,
            value: setCmbInstrumentTypePK()
        });

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        function setCmbInstrumentTypePK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InstrumentTypePK;
            }
        }
        function onChangeInstrumentType() {
            // disini CHRIST



            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $("#TrxType").val("");
                GlobInstrumentType = this.value();
                //BOND
                ShowHideLabelByInstrumentType(GlobInstrumentType);

                if (GlobInstrumentType == 3) {
                    GlobInstrumentType = 5;
                }
                if (GlobInstrumentType == 4) {
                    $("#Amount").attr('readonly', false);
                }
                else {
                    $("#Amount").attr('readonly', true);
                }


                $.ajax({
                    url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionType" + "/" + GlobInstrumentType,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#TrxType").kendoComboBox({
                            dataValueField: "Code",
                            dataTextField: "DescOne",
                            filter: "contains",
                            suggest: true,
                            dataSource: data,
                            change: onChangeTrxType,
                            value: setCmbTrxType()
                        });

                    },
                    error: function (data) {
                        alertify.alert("Please Choose Instrument Type First");

                    }
                });

                function onChangeTrxType() {

                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    ShowHideLabelInstrument(GlobInstrumentType, this.value());
                    RecalNetAmount();

                    if ($("#TrxType").val() == 1)
                        $("#LblRealisedAmount").hide();
                    else
                        $("#LblRealisedAmount").show();

                    if (GlobInstrumentType == 5)
                        $("#LblRealisedAmount").hide();
                }

                function setCmbTrxType() {
                    if (e == null) {
                        return "";
                    } else {
                        return dataItemX.TrxType;
                    }
                }

                $("#InstrumentPK").val("");
                $("#InstrumentID").val("");
                $("#InstrumentName").val("");
            }


        }

        if (e != null) {
            ShowHideLabelByInstrumentTypeA(dataItemX.InstrumentTypePK, dataItemX);
        }



        //-------- DEPOSITO

        $("#Category").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Normal", value: "Deposit Normal" },
                { text: "On Call", value: "Deposit On Call" }
            ],
            filter: "contains",
            suggest: true,
            index: 0,
            value: setCmbCategory()
        });
        function setCmbCategory() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Category;
            }
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
                    value: setCmbBankPK()
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setCmbBankPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BankPK;
            }
        }

        function onChangeBankPK() {
            $("#BankBranchPK").data("kendoComboBox").value("");
            //$("#CashRefPK").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankPK").data("kendoComboBox").value(),
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



                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });


            }

        }

        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                    value: setCmbBankBranchPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbBankBranchPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BankBranchPK;
            }
        }


        function onChangeBankBranchPK() {

            //$("#CashRefPK").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            //else {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/CashRef/GetCashRefComboByBankBranchPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BankBranchPK").val(),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {

            //            $("#CashRefPK").kendoComboBox({
            //                dataValueField: "CashRefPK",
            //                dataTextField: "ID",
            //                dataSource: data,
            //                filter: "contains",
            //                suggest: true,
            //                change: onChangeCashRefPK,
            //                index: 0
            //            });

            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });
            //}
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefComboByBankBranchPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/0",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#CashRefPK").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCashRefPK,
                    index: 0
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCashRefPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }



        win.center();
        win.open();
    }

    function OnChangeAcqDate() {
        if (GlobInstrumentType == 2) {
            RecalNetAmount();
        }
    }

    function OnChangeSettledDate() {
        if (GlobInstrumentType == 2) {
            RecalNetAmount();
        }
    }

    function ClearRequiredAttribute() {
        $("#Price").removeAttr("required");
        $("#AcqPrice").removeAttr("required");
        $("#AcqVolume").removeAttr("required");
        $("#AcqDate").removeAttr("required");
        $("#Lot").removeAttr("required");
        $("#LotInShare").removeAttr("required");
        $("#SettledDate").removeAttr("required");
        $("#MaturityDate").removeAttr("required");
        $("#LastCouponDate").removeAttr("required");
        $("#NextCouponDate").removeAttr("required");
        $("#InterestPercent").removeAttr("required");
        $("#InstrumentPK").removeAttr("required");
        $("#CounterpartPK").removeAttr("required");

        $("#BankPK").removeAttr("required");
        $("#BankBranchPK").removeAttr("required");
        $("#Category").removeAttr("required");

    }

    function ShowHideLabelByInstrumentType(_type) {
        HideLabel();
        ClearRequiredAttribute();
        //BOND
        if (_type == 2) {

            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblMaturityDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();

            $("#LblRealisedAmount").hide();

            $("#LblCounterpart").show();

            $("#Price").attr("required", true);
            $("#AcqPrice").attr("required", true);
            $("#AcqDate").attr("required", true);
            $("#AcqVolume").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#LastCouponDate").attr("required", true);
            $("#NextCouponDate").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#InstrumentPK").attr("required", true);
            if (_GlobClientCode == "33") {
                $("#CounterpartPK").attr("required", false);
            }
            else {
                $("#CounterpartPK").attr("required", true);
            }

            $("#LblNominal").show();

        }
        //EQUITY
        else if (_type == 1) {
            $("#LblLot").show();
            $("#LblLotInShare").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblBrokerageFeeAmount").show();
            $("#LblLevyAmount").show();
            $("#LblVATAmount").show();
            $("#LblKPEIAmount").show();
            $("#LblWHTAmount").show();
            $("#LblOTCAmount").show();
            $("#LblIncomeTaxSellAmount").show();
            $("#LblRealisedAmount").hide();
            $("#LblGrossAmount").show();
            $("#LblNetAmount").show();
            $("#LblCounterpart").show();

            $("#Price").attr("required", true);
            $("#Lot").attr("required", true);
            $("#LotInShare").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#InstrumentPK").attr("required", true);
            if (_GlobClientCode == "33") {
                $("#CounterpartPK").attr("required", false);
            }
            else {
                $("#CounterpartPK").attr("required", true);
            }

            $("#LblVolume").show();

        }
        //DEPOSITO
        else if (_type == 3) {
            $("#RowBank").show();
            $("#RowBankBranch").show();
            $("#RowCategory").show();
            $("#LblCounterpart").hide();
            $("#LblPrice").show();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblRealisedAmount").hide();

            $("#MaturityDate").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#Price").data("kendoNumericTextBox").value(1);

            $("#LblNominal").show();



        }

        //REKSADANA
        else {
            $("#LblSettlementDate").show();
            $("#LblCounterpart").hide();
            $("#Price").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#InstrumentPK").attr("required", true);
            $("#LblRealisedAmount").hide();

            $("#LblCloseNav").show();
            $("#LblUnit").show();
        }

    }


    function ShowHideLabelByInstrumentTypeA(_type, _dataItemX) {
        HideLabel();
        ClearRequiredAttribute();
        //BOND
        if (_type == 2) {
            if (_dataItemX.AcqPrice1 != 0) {
                $("#LblAcqPrice1").show();
                $("#LblAcqDate1").show();
                $("#LblAcqVolume1").show();
            }
            if (_dataItemX.AcqPrice2 != 0) {
                $("#LblAcqPrice2").show();
                $("#LblAcqDate2").show();
                $("#LblAcqVolume2").show();
            }
            if (_dataItemX.AcqPrice3 != 0) {
                $("#LblAcqPrice3").show();
                $("#LblAcqDate3").show();
                $("#LblAcqVolume3").show();
            }
            if (_dataItemX.AcqPrice4 != 0) {
                $("#LblAcqPrice4").show();
                $("#LblAcqDate4").show();
                $("#LblAcqVolume4").show();
            }
            if (_dataItemX.AcqPrice5 != 0) {
                $("#LblAcqPrice5").show();
                $("#LblAcqDate5").show();
                $("#LblAcqVolume5").show();
            }
            if (_dataItemX.AcqPrice6 != 0) {
                $("#LblAcqPrice6").show();
                $("#LblAcqDate6").show();
                $("#LblAcqVolume6").show();
            }
            if (_dataItemX.AcqPrice7 != 0) {
                $("#LblAcqPrice7").show();
                $("#LblAcqDate7").show();
                $("#LblAcqVolume7").show();
            }
            if (_dataItemX.AcqPrice8 != 0) {
                $("#LblAcqPrice8").show();
                $("#LblAcqDate8").show();
                $("#LblAcqVolume8").show();
            }
            if (_dataItemX.AcqPrice9 != 0) {
                $("#LblAcqPrice9").show();
                $("#LblAcqDate9").show();
                $("#LblAcqVolume9").show();
            }
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblMaturityDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            if ($("#TrxType").val() == 1)
                $("#LblRealisedAmount").hide();
            else
                $("#LblRealisedAmount").show();
            $("#LblCounterpart").show();

            $("#Price").attr("required", true);
            $("#AcqPrice").attr("required", true);
            $("#AcqDate").attr("required", true);
            $("#AcqVolume").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#LastCouponDate").attr("required", true);
            $("#NextCouponDate").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#CounterpartPK").attr("required", true);

            $("#LblNominal").show();

        }
        //EQUITY
        else if (_type == 1) {
            $("#LblLot").show();
            $("#LblLotInShare").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblBrokerageFeeAmount").show();
            $("#LblLevyAmount").show();
            $("#LblVATAmount").show();
            $("#LblKPEIAmount").show();
            $("#LblWHTAmount").show();
            $("#LblOTCAmount").show();
            $("#LblIncomeTaxSellAmount").show();
            if ($("#TrxType").val() == 1)
                $("#LblRealisedAmount").hide();
            else
                $("#LblRealisedAmount").show();
            $("#LblGrossAmount").show();
            $("#LblNetAmount").show();
            $("#LblCounterpart").show();

            $("#Price").attr("required", true);
            $("#Lot").attr("required", true);
            $("#LotInShare").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#InstrumentPK").attr("required", true);
            $("#CounterpartPK").attr("required", true);

            $("#LblVolume").show();

        }
        //DEPOSITO
        else if (_type == 3) {
            $("#RowBank").show();
            $("#RowBankBranch").show();
            $("#RowCategory").show();
            $("#LblCounterpart").hide();

            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblRealisedAmount").hide();


            $("#MaturityDate").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#Price").data("kendoNumericTextBox").value(1);

            $("#LblPrice").show();
            $("#LblNominal").show();


        }

        //REKSADANA
        else if (_type == 4) {
            $("#LblSettlementDate").show();
            $("#Price").attr("required", true);
            $("#SettledDate").attr("required", true);
            $("#LblCounterpart").hide();
            if (_dataItemX.TrxType == 1)
                $("#LblRealisedAmount").hide();
            else
                $("#LblRealisedAmount").show();

            $("#LblCloseNav").show();
            $("#LblUnit").show();
        }
    }


    function ShowHideLabelInstrument(_InstrumentType,_TrxType) {
        if (_InstrumentType == 5 && _TrxType == 1) {
            $("#LblInstrument").hide();
            $("#MaturityDate").data("kendoDatePicker").enable(true);
        }
        else if (_InstrumentType == 5 && _TrxType == 2) {
            $("#MaturityDate").data("kendoDatePicker").enable(false);
            $("#LblInstrument").show();
        }
        else {
            $("#MaturityDate").data("kendoDatePicker").enable(true);
            $("#LblInstrument").show();
        }
  
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        //refresh();
    }

    function ClearDataBond() {

        $("#Lot").val("");
        $("#LotInShare").val("");


    }

    function ClearDataEquity() {

        $("#InterestPercent").val("");
        $("#MaturityDate").data("kendoDatePicker").value("");

    }

    function ClearDataMoney() {
        $("#Lot").val("");
        $("#LotInShare").val("");
        $("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());


    }

    function ClearDataReksadana() {
        $("#Lot").val("");
        $("#LotInShare").val("");
        $("#InterestPercent").val("");
        $("#MaturityDate").data("kendoDatePicker").value("");

    }

    function clearData() {

        $("#TrxPortfolioPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#PeriodID").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#Reference").val("");
        $("#InstrumentTypePK").val("");
        $("#InstrumentTypeID").val("");
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#Price").val("0");
        $("#Lot").val("0");
        $("#LotInShare").val("0");
        $("#Volume").val("0");
        $("#Amount").val("0");
        $("#InterestAmount").val("0");
        $("#GrossAmount").val("0");
        $("#IncomeTaxGainAmount").val("0");
        $("#IncomeTaxInterestAmount").val("0");
        $("#GrossAmount").val("0");
        $("#NetAmount").val("0");
        $("#TrxType").val("");
        $("#TrxTypeID").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#SettledDate").data("kendoDatePicker").value("");
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#LastCouponDate").data("kendoDatePicker").value("");
        $("#NextCouponDate").data("kendoDatePicker").value("");
        $("#InterestPercent").val("0");
        $("#CashRefPK").val("");
        $("#CashRefID").val("");
        $("#BrokerageFeePercent").val("0");
        $("#BrokerageFeeAmount").val("0");
        $("#LevyAmount").val("0");
        $("#VATAmount").val("0");
        $("#KPEIAmount").val("0");
        $("#WHTAmount").val("0");
        $("#OTCAmount").val("0");
        $("#IncomeTaxSellAmount").val("0");
        $("#RealisedAmount").val("0");
        $("#AcqPrice").val("");
        $("#AcqDate").data("kendoDatePicker").value("");
        $("#AcqVolume").val("");
        $("#AcqPrice1").val("");
        $("#AcqDate1").data("kendoDatePicker").value("");
        $("#AcqVolume1").val("");
        $("#AcqPrice2").val("");
        $("#AcqDate2").data("kendoDatePicker").value("");
        $("#AcqVolume2").val("");
        $("#AcqPrice3").val("");
        $("#AcqDate3").data("kendoDatePicker").value("");
        $("#AcqVolume3").val("");
        $("#AcqPrice4").val("");
        $("#AcqDate4").data("kendoDatePicker").value("");
        $("#AcqVolume4").val("");
        $("#AcqPrice5").val("");
        $("#AcqDate5").data("kendoDatePicker").value("");
        $("#AcqVolume5").val("");
        $("#AcqPrice6").val("");
        $("#AcqDate6").data("kendoDatePicker").value("");
        $("#AcqVolume6").val("");
        $("#AcqPrice7").val("");
        $("#AcqDate7").data("kendoDatePicker").value("");
        $("#AcqVolume7").val("");
        $("#AcqPrice8").val("");
        $("#AcqDate8").data("kendoDatePicker").value("");
        $("#AcqVolume8").val("");
        $("#AcqPrice9").val("");
        $("#AcqDate9").data("kendoDatePicker").value("");
        $("#AcqVolume9").val("");
        $("#Posted").val("");
        $("#Revised").val("");
        $("#PostedBy").val("");
        $("#PostedTime").val("");
        $("#RevisedBy").val("");
        $("#RevisedTime").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");


        $("#BankPK").val("");
        $("#BankBranchPK").val("");
        $("#Category").val("");

    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        $("#BtnUnApproved").show();
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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {

                             TrxPortfolioPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "datetime" },
                             Reference: { type: "string" },
                             InstrumentTypePK: { type: "number" },
                             InstrumentTypeID: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             BankPK: { type: "number" },
                             BankID: { type: "string" },
                             BankName: { type: "string" },
                             BankBranchPK: { type: "number" },
                             BankBranchID: { type: "string" },
                             Price: { type: "number" },
                             Lot: { type: "number" },
                             LotInShare: { type: "number" },
                             Volume: { type: "number" },
                             Amount: { type: "number" },
                             InterestAmount: { type: "number" },
                             GrossAmount: { type: "number" },
                             IncomeTaxGainAmount: { type: "number" },
                             IncomeTaxInterestAmount: { type: "number" },
                             LevyAmount: { type: "number" },
                             VATAmount: { type: "number" },
                             KPEIAmount: { type: "number" },
                             WHTAmount: { type: "number" },
                             OTCAmount: { type: "number" },
                             IncomeTaxSellAmount: { type: "number" },
                             RealisedAmount: { type: "number" },
                             NetAmount: { type: "number" },
                             TrxType: { type: "number" },
                             TrxTypeID: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             SettledDate: { type: "string" },
                             LastCouponDate: { type: "string" },
                             NextCouponDate: { type: "string" },
                             MaturityDate: { type: "string" },
                             InterestPercent: { type: "number" },
                             CashRefPK: { type: "number" },
                             CashRefID: { type: "string" },
                             BrokerageFeePercent: { type: "number" },
                             BrokerageFeeAmount: { type: "number" },
                             AcqPrice: { type: "number" },
                             AcqDate: { type: "string" },
                             AcqVolume: { type: "number" },
                             AcqPrice1: { type: "number" },
                             AcqDate1: { type: "string" },
                             AcqVolume1: { type: "number" },
                             AcqPrice2: { type: "number" },
                             AcqDate2: { type: "string" },
                             AcqVolume2: { type: "number" },
                             AcqPrice3: { type: "number" },
                             AcqDate3: { type: "string" },
                             AcqVolume3: { type: "number" },
                             AcqPrice4: { type: "number" },
                             AcqDate4: { type: "string" },
                             AcqVolume4: { type: "number" },
                             AcqPrice5: { type: "number" },
                             AcqDate5: { type: "string" },
                             AcqVolume5: { type: "number" },
                             AcqPrice6: { type: "number" },
                             AcqDate6: { type: "string" },
                             AcqVolume6: { type: "number" },
                             AcqPrice7: { type: "number" },
                             AcqDate7: { type: "string" },
                             AcqVolume7: { type: "number" },
                             AcqPrice8: { type: "number" },
                             AcqDate8: { type: "string" },
                             AcqVolume8: { type: "number" },
                             AcqPrice9: { type: "number" },
                             AcqDate9: { type: "string" },
                             AcqVolume9: { type: "number" },
                             Category: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "string" },
                             Revised: { type: "boolean" },
                             RevisedBy: { type: "string" },
                             RevisedTime: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "string" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "string" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "string" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "string" },
                             LastUpdate: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()
            //var gridApproved = $("#gridTrxPortfolioApproved").data("kendoGrid");
            // gridApproved.dataSource.read();

        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridTrxPortfolioPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridTrxPortfolioHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridTrxPortfolioApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            } else if (row.Revised == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }
    function initGrid() {
        $("#gridTrxPortfolioApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var TrxPortfolioApprovedURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(TrxPortfolioApprovedURL);

        }
        else {
            var TrxPortfolioApprovedURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourceApproved = getDataSource(TrxPortfolioApprovedURL);

        }

        var grid = $("#gridTrxPortfolioApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Journal"
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
            dataBound: gridApprovedOnDataBound,
            toolbar: ["excel"],
            columns: [
               { command: { text: "Details", click: showDetails }, title: " ", width: 100 },
               {
                field: "Selected",
                width: 50,
                template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                filterable: true,
                sortable: false,
                columnMenu: false
               },
               { field: "TrxPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "StatusDesc", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 170 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "ValueDate", title: "Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "InstrumentTypePK", title: "InstrumentTypePK", hidden: true, width: 170 },
               { field: "InstrumentTypeID", title: "Instrument Type", width: 170 },
               { field: "TrxType", title: "Trx Type", hidden: true, width: 150 },
               { field: "TrxTypeID", title: "Trx Type", width: 150 },
               { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 120 },
               { field: "InstrumentID", title: "Instrument ID", width: 170 },
               { field: "InstrumentName", title: "Instrument Name", width: 250 },
               { field: "BankPK", title: "BankPK", hidden: true, width: 120 },
               { field: "BankID", title: "Bank ID", width: 170 },
               { field: "BankName", title: "Bank Name", width: 250 },
               { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 120 },
               { field: "BankBranchID", title: "Bank Branch ID", width: 170 },
               { field: "Price", title: "<div style='text-align: right'>Price</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Lot", title: "<div style='text-align: right'>Lot</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LotInShare", title: "<div style='text-align: right'>Lot In Share</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Volume", title: "<div style='text-align: right'>Volume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Amount", title: "<div style='text-align: right'>Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "InterestAmount", title: "<div style='text-align: right'>Interest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "GrossAmount", title: "<div style='text-align: right'>Gross Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxGainAmount", title: "<div style='text-align: right'>Income TaxGain Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxInterestAmount", title: "<div style='text-align: right'>Income TaxInterest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "BrokerageFeeAmount", title: "<div style='text-align: right'>Brokerage Fee Amount</div>", width: 170, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LevyAmount", title: "<div style='text-align: right'>Levy Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "VATAmount", title: "<div style='text-align: right'>VAT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "KPEIAmount", title: "<div style='text-align: right'>KPEI Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "WHTAmount", title: "<div style='text-align: right'>WHT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "OTCAmount", title: "<div style='text-align: right'>OTC Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxSellAmount", title: "<div style='text-align: right'>Tax Sell Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "RealisedAmount", title: "<div style='text-align: right'>Realised Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "NetAmount", title: "<div style='text-align: right'>Net Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "CounterpartPK", title: "CounterpartPK", hidden: true, width: 120 },
               { field: "CounterpartID", title: "Counterpart ID", width: 150 },
               { field: "CounterpartName", title: "Counterpart Name", width: 150 },
               { field: "SettledDate", title: "Settled Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MM/yyyy')#" }, 
               { field: "LastCouponDate", title: "Last Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MM/yyyy')#" },
               { field: "NextCouponDate", title: "Next Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(NextCouponDate), 'dd/MM/yyyy')#" },
               { field: "AcqDate", title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
               { field: "AcqPrice", title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume", title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate1", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'dd/MM/yyyy')#" },
               { field: "AcqPrice1", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume1", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate2", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'dd/MM/yyyy')#" },
               { field: "AcqPrice2", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume2", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate3", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'dd/MM/yyyy')#" },
               { field: "AcqPrice3", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume3", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate4", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'dd/MM/yyyy')#" },
               { field: "AcqPrice4", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume4", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate5", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'dd/MM/yyyy')#" },
               { field: "AcqPrice5", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume5", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate6", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate6), 'dd/MM/yyyy')#" },
               { field: "AcqPrice6", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume6", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate7", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate7), 'dd/MM/yyyy')#" },
               { field: "AcqPrice7", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume7", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate8", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate8), 'dd/MM/yyyy')#" },
               { field: "AcqPrice8", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume8", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate9", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate9), 'dd/MM/yyyy')#" },
               { field: "AcqPrice9", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume9", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "MaturityDate", title: "Maturity Date", width: 170, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#" },
               {
                   field: "InterestPercent", title: "<div style='text-align: right'>Interest Percent</div>", width: 150,
                   template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "CompanyAccountTradingPK", title: "CompanyAccountTrading",  width: 150 },
               { field: "CashRefPK", title: "CashRefPK", hidden: true, width: 150 },
               { field: "CashRefID", title: "Cash Ref", width: 150 },
               {
                   field: "BrokerageFeePercent", title: "<div style='text-align: right'>Brokerage Fee Percent</div>", width: 170,
                   template: "#: BrokerageFeePercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "Category", title: "Category", hidden: true, width: 150 },
               { field: "PostedBy", title: "Posted By", width: 120 },
               { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "RevisedBy", title: "Revised By", width: 120 },
               { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "EntryUsersID", title: "Entry ID", width: 120 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "UpdateUsersID", title: "Update ID", width: 120 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 120 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "VoidUsersID", title: "Void ID", width: 120 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 }
            ]

        }).data("kendoGrid");
        $("#SelectedAllApproved").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            

            var grid = $("#gridTrxPortfolioApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _trxPortfolioPK = dataItemX.TrxPortfolioPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _trxPortfolioPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabTrxPortfolio").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },

            activate: function (e) {

                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                    if (tabindex == 1) {
                        RecalGridPending();
                    }
                    if (tabindex == 2) {
                        RecalGridHistory();
                    }
                } else {
                    refresh();
                }
            }
        });
        ResetButtonBySelectedData();
        $("#BtnRejectBySelected").hide();
        $("#BtnApproveBySelected").hide();

    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnPostingWithoutBankBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TrxPortfolio/" + _a + "/" + _b,
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
    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TrxPortfolio/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
    function RecalGridPending() {
        $("#gridTrxPortfolioPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var TrxPortfolioPendingURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(TrxPortfolioPendingURL);

        }
        else {
            var TrxPortfolioPendingURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourcePending = getDataSource(TrxPortfolioPendingURL);

        }
        var grid = $("#gridTrxPortfolioPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Client Subscription"
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
            toolbar: ["excel"],
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "TrxPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "StatusDesc", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 170 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "ValueDate", title: "Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "InstrumentTypePK", title: "InstrumentTypePK", hidden: true, width: 170 },
               { field: "InstrumentTypeID", title: "Instrument Type", width: 170 },
               { field: "TrxType", title: "Trx Type", hidden: true, width: 150 },
               { field: "TrxTypeID", title: "Trx Type", width: 150 },
               { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 120 },
               { field: "InstrumentID", title: "Instrument ID", width: 170 },
               { field: "InstrumentName", title: "Instrument Name", width: 250 },
               { field: "BankPK", title: "BankPK", hidden: true, width: 120 },
               { field: "BankID", title: "Bank ID", width: 170 },
               { field: "BankName", title: "Bank Name", width: 250 },
               { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 120 },
               { field: "BankBranchID", title: "Bank Branch ID", width: 170 },
               { field: "Price", title: "<div style='text-align: right'>Price</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Lot", title: "<div style='text-align: right'>Lot</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LotInShare", title: "<div style='text-align: right'>Lot In Share</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Volume", title: "<div style='text-align: right'>Volume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Amount", title: "<div style='text-align: right'>Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "InterestAmount", title: "<div style='text-align: right'>Interest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "GrossAmount", title: "<div style='text-align: right'>Gross Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxGainAmount", title: "<div style='text-align: right'>Income TaxGain Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxInterestAmount", title: "<div style='text-align: right'>Income TaxInterest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "BrokerageFeeAmount", title: "<div style='text-align: right'>Brokerage Fee Amount</div>", width: 170, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LevyAmount", title: "<div style='text-align: right'>Levy Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "VATAmount", title: "<div style='text-align: right'>VAT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "KPEIAmount", title: "<div style='text-align: right'>KPEI Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "WHTAmount", title: "<div style='text-align: right'>WHT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "OTCAmount", title: "<div style='text-align: right'>OTC Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxSellAmount", title: "<div style='text-align: right'>Tax Sell Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "RealisedAmount", title: "<div style='text-align: right'>Realised Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "NetAmount", title: "<div style='text-align: right'>Net Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "CounterpartPK", title: "CounterpartPK", hidden: true, width: 120 },
               { field: "CounterpartID", title: "Counterpart ID", width: 150 },
               { field: "CounterpartName", title: "Counterpart Name", width: 150 },
               { field: "SettledDate", title: "Settled Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MM/yyyy')#" },
               { field: "LastCouponDate", title: "Last Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MM/yyyy')#" },
               { field: "NextCouponDate", title: "Next Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(NextCouponDate), 'dd/MM/yyyy')#" },
               { field: "AcqDate", title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
               { field: "AcqPrice", title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume", title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate1", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'dd/MM/yyyy')#" },
               { field: "AcqPrice1", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume1", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate2", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'dd/MM/yyyy')#" },
               { field: "AcqPrice2", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume2", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate3", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'dd/MM/yyyy')#" },
               { field: "AcqPrice3", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume3", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate4", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'dd/MM/yyyy')#" },
               { field: "AcqPrice4", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume4", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate5", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'dd/MM/yyyy')#" },
               { field: "AcqPrice5", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume5", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate6", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate6), 'dd/MM/yyyy')#" },
               { field: "AcqPrice6", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume6", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate7", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate7), 'dd/MM/yyyy')#" },
               { field: "AcqPrice7", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume7", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate8", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate8), 'dd/MM/yyyy')#" },
               { field: "AcqPrice8", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume8", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate9", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate9), 'dd/MM/yyyy')#" },
               { field: "AcqPrice9", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume9", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "MaturityDate", title: "Maturity Date", width: 170, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#" },
               {
                   field: "InterestPercent", title: "<div style='text-align: right'>Interest Percent</div>", width: 150,
                   template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "CompanyAccountTradingPK", title: "CompanyAccountTrading", width: 150 },
               { field: "CashRefPK", title: "CashRefPK", hidden: true, width: 150 },
               { field: "CashRefID", title: "Cash Ref", width: 150 },
               {
                   field: "BrokerageFeePercent", title: "<div style='text-align: right'>Brokerage Fee Percent</div>", width: 170,
                   template: "#: BrokerageFeePercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "Category", title: "Category", hidden: true, width: 150 },
               { field: "PostedBy", title: "Posted By", width: 120 },
               { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "RevisedBy", title: "Revised By", width: 120 },
               { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "EntryUsersID", title: "Entry ID", width: 120 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "UpdateUsersID", title: "Update ID", width: 120 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 120 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "VoidUsersID", title: "Void ID", width: 120 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 }
            ]
        }).data("kendoGrid");
        $("#SelectedAllPending").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }
            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Pending");

        });

        grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        function selectDataPending(e) {
            

            var grid = $("#gridTrxPortfolioPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _trxPortfolioPK = dataItemX.TrxPortfolioPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _trxPortfolioPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnPostingWithoutBankBySelected").hide();

    }
    function RecalGridHistory() {

        $("#gridTrxPortfolioHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var TrxPortfolioHistoryURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(TrxPortfolioHistoryURL);

        }
        else {
            var TrxPortfolioHistoryURL = window.location.origin + "/Radsoft/TrxPortfolio/GetDataByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourceHistory = getDataSource(TrxPortfolioHistoryURL);

        }
        $("#gridTrxPortfolioHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Trx Portfolio"
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
            toolbar: ["excel"],
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "TrxPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "StatusDesc", title: "Status", hidden: true, filterable: false, width: 100 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 170 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "ValueDate", title: "Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "InstrumentTypePK", title: "InstrumentTypePK", hidden: true, width: 170 },
               { field: "InstrumentTypeID", title: "Instrument Type", width: 170 },
               { field: "TrxType", title: "Trx Type", hidden: true, width: 150 },
               { field: "TrxTypeID", title: "Trx Type", width: 150 },
               { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 120 },
               { field: "InstrumentID", title: "Instrument ID", width: 170 },
               { field: "InstrumentName", title: "Instrument Name", width: 250 },
               { field: "BankPK", title: "BankPK", hidden: true, width: 120 },
               { field: "BankID", title: "Bank ID", width: 170 },
               { field: "BankName", title: "Bank Name", width: 250 },
               { field: "BankBranchPK", title: "BankBranchPK", hidden: true, width: 120 },
               { field: "BankBranchID", title: "Bank Branch ID", width: 170 },
               { field: "Price", title: "<div style='text-align: right'>Price</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Lot", title: "<div style='text-align: right'>Lot</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LotInShare", title: "<div style='text-align: right'>Lot In Share</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Volume", title: "<div style='text-align: right'>Volume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Amount", title: "<div style='text-align: right'>Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "InterestAmount", title: "<div style='text-align: right'>Interest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "GrossAmount", title: "<div style='text-align: right'>Gross Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxGainAmount", title: "<div style='text-align: right'>Income TaxGain Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxInterestAmount", title: "<div style='text-align: right'>Income TaxInterest Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "BrokerageFeeAmount", title: "<div style='text-align: right'>Brokerage Fee Amount</div>", width: 170, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "LevyAmount", title: "<div style='text-align: right'>Levy Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "VATAmount", title: "<div style='text-align: right'>VAT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "KPEIAmount", title: "<div style='text-align: right'>KPEI Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "WHTAmount", title: "<div style='text-align: right'>WHT Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "OTCAmount", title: "<div style='text-align: right'>OTC Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "IncomeTaxSellAmount", title: "<div style='text-align: right'>Tax Sell Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "RealisedAmount", title: "<div style='text-align: right'>Realised Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "NetAmount", title: "<div style='text-align: right'>Net Amount</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "CounterpartPK", title: "CounterpartPK", hidden: true, width: 120 },
               { field: "CounterpartID", title: "Counterpart ID", width: 150 },
               { field: "CounterpartName", title: "Counterpart Name", width: 150 },
               { field: "SettledDate", title: "Settled Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MM/yyyy')#" },
               { field: "LastCouponDate", title: "Last Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'dd/MM/yyyy')#" },
               { field: "NextCouponDate", title: "Next Coupon Date", width: 170, template: "#= kendo.toString(kendo.parseDate(NextCouponDate), 'dd/MM/yyyy')#" },
               { field: "AcqDate", title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MM/yyyy')#" },
               { field: "AcqPrice", title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume", title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate1", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate1), 'dd/MM/yyyy')#" },
               { field: "AcqPrice1", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume1", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate2", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate2), 'dd/MM/yyyy')#" },
               { field: "AcqPrice2", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume2", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate3", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate3), 'dd/MM/yyyy')#" },
               { field: "AcqPrice3", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume3", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate4", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate4), 'dd/MM/yyyy')#" },
               { field: "AcqPrice4", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume4", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate5", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate5), 'dd/MM/yyyy')#" },
               { field: "AcqPrice5", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume5", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate6", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate6), 'dd/MM/yyyy')#" },
               { field: "AcqPrice6", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume6", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate7", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate7), 'dd/MM/yyyy')#" },
               { field: "AcqPrice7", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume7", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate8", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate8), 'dd/MM/yyyy')#" },
               { field: "AcqPrice8", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume8", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqDate9", hidden: true, title: "Acq Date", width: 170, template: "#= kendo.toString(kendo.parseDate(AcqDate9), 'dd/MM/yyyy')#" },
               { field: "AcqPrice9", hidden: true, title: "<div style='text-align: right'>AcqPrice</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AcqVolume9", hidden: true, title: "<div style='text-align: right'>AcqVolume</div>", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "MaturityDate", title: "Maturity Date", width: 170, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MM/yyyy')#" },
               {
                   field: "InterestPercent", title: "<div style='text-align: right'>Interest Percent</div>", width: 150,
                   template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "CompanyAccountTradingPK", title: "CompanyAccountTrading", width: 150 },
               { field: "CashRefPK", title: "CashRefPK", hidden: true, width: 150 },
               { field: "CashRefID", title: "Cash Ref", width: 150 },
               {
                   field: "BrokerageFeePercent", title: "<div style='text-align: right'>Brokerage Fee Percent</div>", width: 170,
                   template: "#: BrokerageFeePercent  # %", attributes: { style: "text-align:right;" }
               },
               { field: "Category", title: "Category", hidden: true, width: 150 },
               { field: "PostedBy", title: "Posted By", width: 120 },
               { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "RevisedBy", title: "Revised By", width: 120 },
               { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "EntryUsersID", title: "Entry ID", width: 120 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "UpdateUsersID", title: "Update ID", width: 120 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 120 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "VoidUsersID", title: "Void ID", width: 120 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 },
               { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 }
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnPostingWithoutBankBySelected").hide();
        
    }

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    //add,update,old data,approve,reject 
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            var price = $('#Price').val();
            if (GlobInstrumentType == 2) {
                var _totalAcq = 0;
                var _volume = $('#Volume').data("kendoNumericTextBox").value();
                _totalAcq = parseFloat($('#AcqVolume').data("kendoNumericTextBox").value() + $('#AcqVolume1').data("kendoNumericTextBox").value()
                    + $('#AcqVolume2').data("kendoNumericTextBox").value() + $('#AcqVolume3').data("kendoNumericTextBox").value()
                    + $('#AcqVolume4').data("kendoNumericTextBox").value() + $('#AcqVolume5').data("kendoNumericTextBox").value());

                if (_totalAcq > _volume) {
                    alertify.alert("Total Acq Volume Must be < = with Volume")
                    return;
                }

                price = $('#Price').val() / 100
                ClearDataBond();
            }
            else if (GlobInstrumentType == 1) {
                ClearDataEquity();
            }
            else if (GlobInstrumentType == 3) {
                ClearDataMoney();

            }
            else if (GlobInstrumentType == 4) {
                ClearDataReksadana();
            }
            alertify.confirm("Are you sure want to Add data ?", function (e) {
                if (e) {

                    var Validate = {
                        ValueDate: $('#ValueDate').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        Volume: $('#Volume').val(),
                        Amount: $('#Amount').val(),

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/ValidateCheckAvailableInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(Validate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true && $('#TrxType').val() == 2) {
                                $.unblockUI();
                                alertify.alert("Short Sell").moveTo(posY.left, posY.top - 200);
                                return;
                            }
                            else {


                                if ($('#Reference').val() == null || $('#Reference').val() == "") {
                                    setTimeout(function () {
                                        alertify.confirm("Are you sure want to Generate New Reference ?", function (e) {
                                            if (e) {

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV" + "/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $("#Reference").val(data);
                                                        var _ref = data.replace(/\//g, "-");
                                                        alertify.alert("Your new reference is " + $("#Reference").val());

                                                        if ($('#InstrumentTypePK').val() == 3 && $('#TrxType').val() != 2) {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/Instrument/Instrument_AddFromOMSTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#MaturityDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BankPK').val() + "/" + $('#InterestPercent').val() + "/" + $('#Category').val() + "/1",
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    _instrumentPK = data;
                                                                    var TrxPortfolio = {
                                                                        TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                                                                        PeriodPK: $('#PeriodPK').val(),
                                                                        ValueDate: $('#ValueDate').val(),
                                                                        Reference: $('#Reference').val(),
                                                                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                        InstrumentPK: _instrumentPK,
                                                                        Type: GlobInstrumentType,
                                                                        BankPK: $('#BankPK').val(),
                                                                        BankBranchPK: $('#BankBranchPK').val(),
                                                                        Price: $('#Price').val(),
                                                                        AcqPrice: $('#AcqPrice').val(),
                                                                        Lot: $('#Lot').val(),
                                                                        LotInShare: $('#LotInShare').val(),
                                                                        Volume: $('#Volume').val(),
                                                                        Amount: $('#Amount').val(),
                                                                        InterestAmount: $('#InterestAmount').val(),
                                                                        IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                        IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                        LevyAmount: $('#LevyAmount').val(),
                                                                        VATAmount: $('#VATAmount').val(),
                                                                        KPEIAmount: $('#KPEIAmount').val(),
                                                                        WHTAmount: $('#WHTAmount').val(),
                                                                        OTCAmount: $('#OTCAmount').val(),
                                                                        IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                                                        RealisedAmount: $('#RealisedAmount').val(),
                                                                        GrossAmount: $('#GrossAmount').val(),
                                                                        NetAmount: $('#NetAmount').val(),
                                                                        TrxType: $('#TrxType').val(),
                                                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                        CounterpartPK: $('#CounterpartPK').val(),
                                                                        SettledDate: $('#SettledDate').val(),
                                                                        AcqDate: $('#AcqDate').val(),
                                                                        AcqVolume: $('#AcqVolume').val(),
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
                                                                        LastCouponDate: $('#LastCouponDate').val(),
                                                                        NextCouponDate: $('#NextCouponDate').val(),
                                                                        MaturityDate: $('#MaturityDate').val(),
                                                                        InterestPercent: $('#InterestPercent').val(),
                                                                        CompanyAccountTradingPK: $('#CompanyAccountTradingPK').val(),
                                                                        CashRefPK: $('#CashRefPK').val(),
                                                                        BrokerageFeePercent: $('#BrokerageFeePercent').val(),
                                                                        BrokerageFeeAmount: $('#BrokerageFeeAmount').val(),
                                                                        Category: $('#Category').val(),
                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                    };
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_I",
                                                                        type: 'POST',
                                                                        data: JSON.stringify(TrxPortfolio),
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
                                                        else {

                                                            var TrxPortfolio = {
                                                                TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                                                                PeriodPK: $('#PeriodPK').val(),
                                                                ValueDate: $('#ValueDate').val(),
                                                                Reference: $('#Reference').val(),
                                                                InstrumentTypePK: $('#InstrumentTypePK').val(),
                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                Type: GlobInstrumentType,
                                                                BankPK: $('#BankPK').val(),
                                                                BankBranchPK: $('#BankBranchPK').val(),
                                                                Price: $('#Price').val(),
                                                                AcqPrice: $('#AcqPrice').val(),
                                                                Lot: $('#Lot').val(),
                                                                LotInShare: $('#LotInShare').val(),
                                                                Volume: $('#Volume').val(),
                                                                Amount: $('#Amount').val(),
                                                                InterestAmount: $('#InterestAmount').val(),
                                                                IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                                                IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                                                LevyAmount: $('#LevyAmount').val(),
                                                                VATAmount: $('#VATAmount').val(),
                                                                KPEIAmount: $('#KPEIAmount').val(),
                                                                WHTAmount: $('#WHTAmount').val(),
                                                                OTCAmount: $('#OTCAmount').val(),
                                                                IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                                                RealisedAmount: $('#RealisedAmount').val(),
                                                                GrossAmount: $('#GrossAmount').val(),
                                                                NetAmount: $('#NetAmount').val(),
                                                                TrxType: $('#TrxType').val(),
                                                                TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                                                CounterpartPK: $('#CounterpartPK').val(),
                                                                SettledDate: $('#SettledDate').val(),
                                                                AcqDate: $('#AcqDate').val(),
                                                                AcqVolume: $('#AcqVolume').val(),
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
                                                                LastCouponDate: $('#LastCouponDate').val(),
                                                                NextCouponDate: $('#NextCouponDate').val(),
                                                                MaturityDate: $('#MaturityDate').val(),
                                                                InterestPercent: $('#InterestPercent').val(),
                                                                CompanyAccountTradingPK: $('#CompanyAccountTradingPK').val(),
                                                                CashRefPK: $('#CashRefPK').val(),
                                                                BrokerageFeePercent: $('#BrokerageFeePercent').val(),
                                                                BrokerageFeeAmount: $('#BrokerageFeeAmount').val(),
                                                                Category: $('#Category').val(),
                                                                EntryUsersID: sessionStorage.getItem("user")

                                                            };
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_I",
                                                                type: 'POST',
                                                                data: JSON.stringify(TrxPortfolio),
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
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            } else {
                                                alertify.alert("Add Trx Portfolio Cancel,Please Choose old Reference");
                                            }
                                        })
                                    }, 1000);
                                }

                            }
                        }
                    });
                }
            });
        }

    });


    $("#BtnUpdate").click(function () {
        var val = validateData();

        var price = $('#Price').val();
        if (GlobInstrumentType == 2) {
            price = $('#Price').val() / 100
            ClearDataBond();
        }
        else if (GlobInstrumentType == 1) {
            //var val = validateDataEquity();
            ClearDataEquity();
        }
        else if (GlobInstrumentType == 3) {
            //var val = validateDataMoney();
            ClearDataMoney();
        }
        else if (GlobInstrumentType == 4) {
            ClearDataReksadana();
        }
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update , please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TrxPortfolioPK").val() + "/" + $("#HistoryPK").val() + "/" + "TrxPortfolio",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == data) {

                                var TrxPortfolio = {
                                    TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PeriodPK: $('#PeriodPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    Reference: $('#Reference').val(),
                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    BankPK: $('#BankPK').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    Price: $('#Price').val(),
                                    AcqPrice: $('#AcqPrice').val(),
                                    Lot: $('#Lot').val(),
                                    LotInShare: $('#LotInShare').val(),
                                    Volume: $('#Volume').val(),
                                    Amount: $('#Amount').val(),
                                    InterestAmount: $('#InterestAmount').val(),
                                    IncomeTaxInterestAmount: $('#IncomeTaxInterestAmount').val(),
                                    IncomeTaxGainAmount: $('#IncomeTaxGainAmount').val(),
                                    LevyAmount: $('#LevyAmount').val(),
                                    VATAmount: $('#VATAmount').val(),
                                    KPEIAmount: $('#KPEIAmount').val(),
                                    WHTAmount: $('#WHTAmount').val(),
                                    OTCAmount: $('#OTCAmount').val(),
                                    IncomeTaxSellAmount: $('#IncomeTaxSellAmount').val(),
                                    RealisedAmount: $('#RealisedAmount').val(),
                                    GrossAmount: $('#GrossAmount').val(),
                                    NetAmount: $('#NetAmount').val(),
                                    TrxType: $('#TrxType').val(),
                                    TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    SettledDate: $('#SettledDate').val(),
                                    AcqDate: $('#AcqDate').val(),
                                    AcqVolume: $('#AcqVolume').val(),
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
                                    LastCouponDate: $('#LastCouponDate').val(),
                                    NextCouponDate: $('#NextCouponDate').val(),
                                    MaturityDate: $('#MaturityDate').val(),
                                    InterestPercent: $('#InterestPercent').val(),
                                    CompanyAccountTradingPK: $('#CompanyAccountTradingPK').val(),
                                    CashRefPK: $('#CashRefPK').val(),
                                    BrokerageFeePercent: $('#BrokerageFeePercent').val(),
                                    BrokerageFeeAmount: $('#BrokerageFeeAmount').val(),
                                    Category: $('#Category').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_U",
                                    type: 'POST',
                                    data: JSON.stringify(TrxPortfolio),
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
    });

    $("#BtnApproved").click(function () {
        
        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TrxPortfolioPK").val() + "/" + $("#HistoryPK").val() + "/" + "TrxPortfolio",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == data) {
                            var TrxPortfolio = {
                                TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_A",
                                type: 'POST',
                                data: JSON.stringify(TrxPortfolio),
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

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                var TrxPortfolio = {
                    TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_V",
                    type: 'POST',
                    data: JSON.stringify(TrxPortfolio),
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
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                var TrxPortfolio = {
                    TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_R",
                    type: 'POST',
                    data: JSON.stringify(TrxPortfolio),
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
    });

    $("#BtnUnApproved").click(function () {
        
        alertify.confirm("Are you sure want to UnApproved data?", function (e) {

            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alert(" Trx Portfolio  Already Posted / Revised, UnApprove Cancel");
            } else {


                if (e) {
                    var TrxPortfolio = {
                        TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_UnApproved",
                        type: 'POST',
                        data: JSON.stringify(TrxPortfolio),
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
            }
        });
    });

    function getDataSourceListInstrument() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Instrument/GetInstrumentTrxPortfolioByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobInstrumentType + "/" + $("#TrxType").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,


                     error: function (e) {
                         WinListInstrument.close();
                         alertify.alert("Please Choose Instrument Type First!");

                         //alert(e.errorThrown + " - " + e.xhr.responseText);
                         //this.cancelChanges();
                     },
                     pageSize: 10,
                     schema: {
                         model: {
                             fields: {
                                 InstrumentPK: { type: "number" },
                                 InstrumentID: { type: "string" },
                                 BegBalance: { type: "number" },
                                 MovBalance: { type: "number" },
                                 AcqDate: { type: "string" },
                                 TrxBuy: { type: "number" },
                                 Price: { type: "number" },
                                 Balance: { type: "number" },
                                 CurrencyID: { type: "string" },
                                 InterestPercent: { type: "number" },
                                 MaturityDate: { type: "string" },
                                
                             }
                         }
                     }
                 });



    }

    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();


        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";
        //htmlInstrumentName = "#InstrumentName";
        WinListInstrument.center();
        WinListInstrument.open();

    });

    $("#BtnClearListInstrumentPK").click(function () {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
    });

    function initListInstrumentPK() {

        var dsListInstrument = getDataSourceListInstrument();
        if (_GlobClientCode == "02") {
            if (GlobInstrumentType == 1) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "Balance", title: "Balance", width: 200, format: "{0:n4}", },
                        { field: "AcqDate", title: "Acq Date", width: 120, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
            }
            else if (GlobInstrumentType == 2) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "Balance", title: "Balance", width: 200, format: "{0:n4}", },
                        { field: "AcqDate", title: "Acq Date", width: 120, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
            }
            else if (GlobInstrumentType == 3) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n4}", },
                        { field: "MovBalance", title: "Move Balance", width: 200, format: "{0:n4}", },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n4}", },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }

            }
            else {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "Balance", title: "Balance", width: 200, format: "{0:n4}", },
                        { field: "AcqDate", title: "Acq Date", width: 120, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }

            }
        }
        else
        {
            if (GlobInstrumentType == 1) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n4}", },
                        { field: "MovBalance", title: "Move Balance", width: 200, format: "{0:n4}", },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n4}", },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
            }
            else if (GlobInstrumentType == 2) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n4}", },
                        { field: "MovBalance", title: "Move Balance", width: 200, format: "{0:n4}", },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n4}", },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
            }
            else if (GlobInstrumentType == 3) {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n4}", },
                        { field: "MovBalance", title: "Move Balance", width: 200, format: "{0:n4}", },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n4}", },
                        { field: "InterestPercent", title: "Interest (%)", width: 200, format: "{0:n4}", template: "#: InterestPercent  # %", },
                        { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                        { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }

            }
            else {
                if ($("#TrxType").data("kendoComboBox").value() == 1) {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "ID", width: 300 },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }
                else {
                    $("#gridListInstrument").kendoGrid({
                        dataSource: dsListInstrument,
                        height: 400,
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
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 85 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 300 },
                        { field: "InstrumentID", title: "Intrument", width: 300 },
                        { field: "BegBalance", title: "Beg Balance", width: 200, format: "{0:n4}", },
                        { field: "MovBalance", title: "Move Balance", width: 200, format: "{0:n4}", },
                        { field: "Balance", title: "End Balance", width: 200, format: "{0:n4}", },
                        { field: "CurrencyID", title: "Currency", width: 100 },
                        ]
                    });
                }

            }
        }
        
    }

    function onWinListInstrumentClose() {
        $("#gridListInstrument").empty();
    }

    function ListInstrumentSelect(e) {
        var grid = $("#gridListInstrument").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $("#InstrumentID").val(dataItemX.InstrumentID);
        $("#InstrumentPK").val(dataItemX.InstrumentPK);
        if ($("#TrxType").data("kendoComboBox").value() == 1) {
            if (GlobInstrumentType == 2) {
                $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
                $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
                GetLastCouponDate();
            }
        }
        else {
            if (GlobInstrumentType == 1) {
                $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#Lot").data("kendoNumericTextBox").value(dataItemX.Balance / 100);
            }
            else if (GlobInstrumentType == 5) {
                $("#BankPK").data("kendoComboBox").value(dataItemX.BankPK);
                $("#BankBranchPK").data("kendoComboBox").value(dataItemX.BankBranchPK);
                $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#Amount").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
                $("#AcqDate").data("kendoDatePicker").value(new Date(dataItemX.MaturitAcqDateyDate));
                $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
            }
            else {
                $("#Volume").data("kendoNumericTextBox").value(dataItemX.Balance);
                $("#InterestPercent").data("kendoNumericTextBox").value(dataItemX.InterestPercent);
                $("#MaturityDate").data("kendoDatePicker").value(new Date(dataItemX.MaturityDate));
                GetLastCouponDate();
            }
        }

        WinListInstrument.close();
        //RecalcInstrument();



    }

    function formatNumber(x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }

    $("input[type='text']").change(function () {
        dirty = true;
    });

    $("input[type='number']").change(function () {
        dirty = true;
    });


    $("#BtnPosting").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        
        alertify.confirm("Are you sure want to Posting Trx Portfolio?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alert("Trx Portfolio Already Posted / Revised, Posting Cancel");

            } else {

                if (e) {
                    if (dirty == true) {
                        alert("Change must be Update First, Posting Cancel");
                        return;
                    }
                    var _posted;
                    if ($("#Posted").is(":checked")) {
                        _posted = true;
                    }
                    if (_posted == true) {
                        alert("TrxPortfolio already Posted");
                        return;
                    }

                    var TrxPortfolio = {
                        TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                        PostedBy: sessionStorage.getItem("user"),
                        PeriodPK: $('#PeriodPK').val(),
                        InstrumentTypePK: $("#InstrumentTypePK").data("kendoComboBox").value(),
                        TrxType: $("#TrxType").data("kendoComboBox").value(),
                        ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_Posting",
                        type: 'POST',
                        data: JSON.stringify(TrxPortfolio),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#State").removeClass("ReadyForPosting").removeClass("Reserved").addClass("Posted");
                            $("#State").text("POSTED");
                            $("#PostedBy").text(sessionStorage.getItem("user"));
                            $("#Posted").prop('checked', true);
                            $("#Revised").prop('checked', false);
                            alertify.alert(data);
                            win.close();
                            refresh()
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            }
        });

    });

    $("#BtnRevise").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        
        alertify.confirm("Are you sure want to Revised TrxPortfolio?", function (e) {

            if ($("#State").text() == "Revised") {
                alert("TrxPortfolio Already Posted / Revised, Revised Cancel");
            } else {

                if (e) {
                    if (dirty == true) {

                        alert("Change must be Update First, Revised Cancel");
                        return;
                    }
                    var _Revised;
                    if ($("#Revised").is(":checked")) {
                        _Revised = true;
                    }
                    if (_Revised == true) {
                        alert("TrxPortfolio already Revised");
                        return;
                    }


                    var TrxPortfolio = {
                        TrxPortfolioPK: $('#TrxPortfolioPK').val(),
                        RevisedBy: sessionStorage.getItem("user"),
                        PeriodPK: $('#PeriodPK').val(),
                        ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TrxPortfolio_Revise",
                        type: 'POST',
                        data: JSON.stringify(TrxPortfolio),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                            $("#State").text("Revised");
                            $("#RevisedBy").text(sessionStorage.getItem("user"));
                            $("#Revised").prop('checked', true);
                            $("#Posted").prop('checked', false);
                            alertify.alert(data);
                            win.close();
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            }
        });
    });

    function RecalcInstrument() {


        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentForInvestmentByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#InstrumentPK").val() + "/" + kendo.toString($("#SettledDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + GlobInstrumentType,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#InterestPercent").data("kendoNumericTextBox").value(data.InterestPercent);
                $("#MaturityDate").data("kendoDatePicker").value(new Date(data.MaturityDate));
                //$("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                //$("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));

            },

        })

    }

    $("#BtnDownload").click(function (e) {
        showPortfolioReport();
    });
    
    function showPortfolioReport(e) {
        $("#ParamDateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeParamDateFrom
        });
        $("#ParamDateTo").kendoDatePicker({
            value: new Date(),
        });
        function OnChangeParamDateFrom() {
            if ($("#ParamDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamDateTo").data("kendoDatePicker").value($("#ParamDateFrom").data("kendoDatePicker").value());
            }

        }
        WinTrxPortfolioReport.center();
        WinTrxPortfolioReport.open();

    }


    $("#BtnOkReport").click(function () {
        
        alertify.confirm("Are you sure want to Download data TrxPortfolio ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/DownloadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.location = data
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnCancelReport").click(function () {
        
        alertify.confirm("Are you sure want to cancel Check Trx Portfolio?", function (e) {
            if (e) {
                WinTrxPortfolioReport.close();
                alertify.alert("Cancel Checking");
            }
        });
    });

    function onWinTrxPortfolioReportClose() {
        $("#ParamDateFrom").val("");
        $("#ParamDateTo").val("");

    }


    $("#BtnAddAcq1").click(function () {
        if ($("#LblAcqDate1").is(":visible"))
        {
            alertify.alert("Can't Add More Acquisition")
        }
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        else
        {
            HideLabel();
            $("#LblInterestPercent").show();
            $("#LblLastCouponDate").show();
            $("#LblNextCouponDate").show();
            $("#LblSettlementDate").show();
            $("#LblPrice").show();
            $("#LblAcqPrice").show();
            $("#LblAcqDate").show();
            $("#LblAcqVolume").show();
            $("#LblAcquisition").show();
            $("#LblInterestAmount").show();
            $("#LblGrossAmount").show();
            $("#LblIncomeTaxGainAmount").show();
            $("#LblIncomeTaxInterestAmount").show();
            $("#LblNetAmount").show();
            $("#LblAcquisition").show();
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
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq2").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq3").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq4").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq5").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq6").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq7").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq8").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });

    $("#BtnClearAcq9").click(function () {
        HideLabel();
        $("#LblInterestPercent").show();
        $("#LblLastCouponDate").show();
        $("#LblNextCouponDate").show();
        $("#LblSettlementDate").show();
        $("#LblPrice").show();
        $("#LblAcqPrice").show();
        $("#LblAcqDate").show();
        $("#LblAcqVolume").show();
        $("#LblAcquisition").show();
        $("#LblInterestAmount").show();
        $("#LblGrossAmount").show();
        $("#LblIncomeTaxGainAmount").show();
        $("#LblIncomeTaxInterestAmount").show();
        $("#LblNetAmount").show();
        $("#LblAcquisition").show();
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
        RecalNetAmount();
    });


    function GetLastCouponDate()
    {
        var TrxPortfolio = {
            InstrumentPK: $('#InstrumentPK').val(),
            TrxType: $("#TrxType").data("kendoComboBox").value(),
            ValueDate: $('#ValueDate').val()
        };


        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetLastCouponDateandNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#LastCouponDate").data("kendoDatePicker").value(new Date(data.LastCouponDate));
                $("#NextCouponDate").data("kendoDatePicker").value(new Date(data.NextCouponDate));
                return;
            },
            error: function (data) {
                alertify.alert("Please Fill Value Date or Instrument");
                $('#InstrumentPK').val("");
                $('#InstrumentID').val("");
            }
        });


   

    
    }


    function RecalNetAmount() {

        if ($('#InstrumentPK').val() == null || $('#InstrumentPK').val() == "" || $('#Price').val() == null || $('#Price').val() == 0 || $('#Volume').val() == null || $('#Volume').val() == 0) {
            return;
        }
        var _setCounterpart = 0;
        if ($("#CounterpartPK").val() == null || $("#CounterpartPK").val() == 0) {
            _setCounterpart = 0;
        }
        else {

            _setCounterpart = $("#CounterpartPK").val();
        }
        var TrxPortfolio = {
            InstrumentPK: $('#InstrumentPK').val(),
            InstrumentTypePK: $('#InstrumentTypePK').val(),
            CounterpartPK: _setCounterpart,
            TrxType: $("#TrxType").data("kendoComboBox").value(),
            ValueDate: $('#ValueDate').val(),
            SettledDate: $('#SettledDate').val(),
            NextCouponDate: $('#NextCouponDate').val(),
            LastCouponDate: $('#LastCouponDate').val(),
            Price: $('#Price').val(),
            Volume: $('#Volume').val(),
            Amount: $('#Amount').val(),
            AcqPrice: $('#AcqPrice').val(),
            AcqDate: $("#AcqDate").val(),
            AcqVolume: $('#AcqVolume').val(),
            AcqPrice1: $('#AcqPrice1').val(),
            AcqDate1: $("#AcqDate1").val(),
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
            CompanyAccountTradingPK: $('#CompanyAccountTradingPK').val(),

        };

        $.ajax({
            url: window.location.origin + "/Radsoft/TrxPortfolio/GetNetAmount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(TrxPortfolio),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                ResetNetAmount();
                $("#BrokerageFeeAmount").data("kendoNumericTextBox").value(data.CommissionAmount);
                $("#LevyAmount").data("kendoNumericTextBox").value(data.LevyAmount);
                $("#KPEIAmount").data("kendoNumericTextBox").value(data.KPEIAmount);
                $("#VATAmount").data("kendoNumericTextBox").value(data.VATAmount);
                $("#WHTAmount").data("kendoNumericTextBox").value(data.WHTAmount);
                $("#OTCAmount").data("kendoNumericTextBox").value(data.OTCAmount);
                $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(data.IncomeTaxSellAmount);
                $("#RealisedAmount").data("kendoNumericTextBox").value(data.RealisedAmount);
                $("#InterestAmount").data("kendoNumericTextBox").value(data.InterestAmount);
                $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(data.IncomeTaxInterestAmount);
                $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(data.IncomeTaxGainAmount);
                $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(data.IncomeTaxSellAmount);
                $("#GrossAmount").data("kendoNumericTextBox").value(data.GrossAmount);
                $("#NetAmount").data("kendoNumericTextBox").value(data.NetAmount);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function ResetNetAmount() {
        $("#BrokerageFeeAmount").data("kendoNumericTextBox").value(0);
        $("#LevyAmount").data("kendoNumericTextBox").value(0);
        $("#KPEIAmount").data("kendoNumericTextBox").value(0);
        $("#VATAmount").data("kendoNumericTextBox").value(0);
        $("#WHTAmount").data("kendoNumericTextBox").value(0);
        $("#OTCAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(0);
        $("#RealisedAmount").data("kendoNumericTextBox").value(0);
        $("#InterestAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(0);
        $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(0);
        $("#GrossAmount").data("kendoNumericTextBox").value(0);
        $("#NetAmount").data("kendoNumericTextBox").value(0);
    }

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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


    function NetRecalculate() {
        if ($("#CounterpartPK").val() == 0 || $("#CounterpartPK").val() == null) {
            return;
        }
        if (GlobInstrumentType == 3) {
            $("#NetAmount").data("kendoNumericTextBox").value($("#Amount").data("kendoNumericTextBox").value())
            return;
        }
        else {


            var NetAmount = {
                TrxType: $('#TrxType').val(),
                CommissionAmount: $("#BrokerageFeeAmount").data("kendoNumericTextBox").value(),
                LevyAmount: $("#LevyAmount").data("kendoNumericTextBox").value(),
                KPEIAmount: $("#KPEIAmount").data("kendoNumericTextBox").value(),
                VATAmount: $("#VATAmount").data("kendoNumericTextBox").value(),
                WHTAmount: $("#WHTAmount").data("kendoNumericTextBox").value(),
                OTCAmount: $("#OTCAmount").data("kendoNumericTextBox").value(),
                InterestAmount: $("#InterestAmount").data("kendoNumericTextBox").value(),
                IncomeTaxSellAmount: $("#IncomeTaxSellAmount").data("kendoNumericTextBox").value(),
                IncomeTaxInterestAmount: $("#IncomeTaxInterestAmount").data("kendoNumericTextBox").value(),
                IncomeTaxGainAmount: $("#IncomeTaxGainAmount").data("kendoNumericTextBox").value(),
                Amount: $('#Amount').val(),
                InstrumentTypePK: $("#InstrumentTypePK").data("kendoComboBox").value(),

            };

            $.ajax({
                url: window.location.origin + "/Radsoft/TrxPortfolio/NetRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(NetAmount),
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    ResetGrossNetAmount();
                    $("#GrossAmount").data("kendoNumericTextBox").value(data.GrossAmount)
                    $("#NetAmount").data("kendoNumericTextBox").value(data.NetAmount)
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }


    }

    function ResetGrossNetAmount() {
        $("#GrossAmount").data("kendoNumericTextBox").value(0);
        $("#NetAmount").data("kendoNumericTextBox").value(0)
    }


    $("#BtnPostingBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                $.blockUI();
                if (_GlobClientCode == "12")
                {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/ValidateCheckStatusPosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                PostingBySelected();
                            } else {
                                alertify.alert(data);
                                $.unblockUI();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
                else
                {
                    PostingBySelected();
                }

            }
               
        });
    });

    function PostingBySelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/TrxPortfolio/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $.unblockUI();
                alertify.alert(data);
                refresh();
            },
            error: function (data) {
                $.unblockUI();
                alertify.alert(data.responseText);
            }
        });

    }
    // Untuk List Reference

    function getDataSourceListReference() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/TrxPortfolio/ReferenceSelectFromTrxPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#PeriodPK").data("kendoComboBox").value(),
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert("Please Fill ValueDate First!");
                          this.cancelChanges();
                          WinListReference.close();
                      },
                      pageSize: 25,
                      schema: {
                          model: {
                              fields: {
                                  Reference: { type: "string" }

                              }
                          }
                      }
                  });
    }

    function initListReference() {
        var dsListReference = getDataSourceListReference();
        $("#gridListReference").kendoGrid({
            dataSource: dsListReference,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: true,
            pageable: true,
            columnMenu: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListReferenceSelect }, title: " ", width: 100 },
               { field: "Reference", title: "Reference", width: 100 }
            ]
        });
    }

    function onWinListReferenceClose() {
        $("#gridListReference").empty();
    }

    function ListReferenceSelect(e) {
        var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlReference).val(dataItemX.Reference);
        //$("#Reference").val(dataItemX.Reference);
        WinListReference.close();


    }

    $("#btnListReference").click(function () {
        WinListReference.center();
        WinListReference.open();
        initListReference();
        htmlReference = "#Reference";
    });


    $("#BtnReportTrxPortfolioValuation").click(function () {

        // Validasi untuk ngejaga FilterDate tetap diisi
        var _date = kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to get data Report Portfolio Valuation for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/ReportTrxPortfolioValuation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        window.location = data
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });


    $("#BtnReportTrxPortfolioValuationByAccount").click(function () {

        // Validasi untuk ngejaga FilterDate tetap diisi
        var _date = kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to get data Report Portfolio Valuation By Account for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/TrxPortfolio/ReportTrxPortfolioValuationByAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        window.location = data
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });




    $("#BtnPostingWithoutBankBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Posting Without Bank by Selected Data ?", function (e) {
            if (e) {
                $.blockUI();
                if (_GlobClientCode == "12") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TrxPortfolio/ValidateCheckStatusPosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                PostingBySelected();
                            } else {
                                alertify.alert(data);
                                $.unblockUI();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
                else {
                    PostingWithoutBankBySelected();
                }

            }

        });
    });

    function PostingWithoutBankBySelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/TrxPortfolio/PostingWithoutBankBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $.unblockUI();
                alertify.alert(data);
                refresh();
            },
            error: function (data) {
                $.unblockUI();
                alertify.alert(data.responseText);
            }
        });

    }

    
});
