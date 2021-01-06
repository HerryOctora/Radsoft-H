$(document).ready(function () {
    document.title = 'FORM CASHIER RECEIPT';
    var win;
    var tabindex;
    var winOldData;
    var WinPostingReceipt;
    var WinListCashierID;
    var htmlCashierID;
    var gridHeight = screen.height - 300;
    //var WinListInstrument;
    //var htmlInstrumentPK;
    //var htmlInstrumentID;
    //var htmlInstrumentName;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobDebit;
    var GlobOpenMonth;
    var GlobOpenPeriod;
    var GlobDecimalPlaces;

    var checkedApproved = {};
    var checkedPending = {};
    var filterField;
    var filterOperator;
    var filtervalue;
    var filterlogic;
    var _defaultPeriodPK;
    //GlobOpenMonth = 0;
    //1
    initButton();
    //2
    initWindow();
    //3
    refresh();


    if (_GlobClientCode == "03") {
        $("#LblClient").show();

    }
    else {
        $("#LblClient").hide();

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
        $("#BtnReviseBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReverseAll.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnNewVoucher").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }
    function resetNotification() {
        $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
        alertify.set({
            labels: {
                ok: "OK",
                cancel: "Cancel"
            },
            delay: 4000,
            buttonReverse: false,
            buttonFocus: "ok"
        });
    }
    function initWindow() {

        if (_GlobClientCode == "19") {
            $("#lblConsignee").text("Project");
            $("#LblDocRef").hide();
            $("#LblKepada").show();
        }
        else {
            $("#LblDocRef").show();
            $("#LblKepada").hide();
        }


        if (_GlobClientCode == "05") {
            $("#LblNoReference").show();
            $("#LblBudget").show();
        }
        else {
            $("#LblNoReference").hide();
            $("#LblBudget").hide();
        }

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
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
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }

            //Recal amount after update valuedate
            if ($("#StatusHeader").val() == "APPROVED" || ($("#StatusHeader").val() == "PENDING" && $("#CashierPK").val() != ""))
            {
                recalCurrencyRate();
            }

        }
        function OnChangeDateFrom() {

            var _date = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());

            refresh();
        }
        function OnChangeDateTo() {
            var _date = Date.parse($("#DateTo").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            refresh();
        }
        win = $("#WinCashierReceipt").kendoWindow({
            height: 1500,
            title: "* CASHIER RECEIPT",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            }
        }).data("kendoWindow");


        WinListCashierID = $("#WinListCashierID").kendoWindow({
            height: 500,
            title: "List CashierID ",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: onWinListCashierIDClose
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

        //WinListInstrument = $("#WinListInstrument").kendoWindow({
        //    height: "520px",
        //    title: "List Instrument ",
        //    visible: false,
        //    width: "870px",
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 450 })
        //    },
        //    close: onWinListInstrumentClose
        //}).data("kendoWindow");
    }

    var GlobValidator = $("#WinCashierReceipt").kendoValidator().data("kendoValidator");
    function validateData() {

        var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
        if (!_date) {

            alertify.alert("Wrong Format Date DD/MM/YYYY");
            return;
        }
        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    //GetJournalDecimalPlaces
    $.ajax({
        url: window.location.origin + "/Radsoft/AccountingSetup/GetJournalDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GlobDecimalPlaces = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

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


    //GetJournalMoth
    $.ajax({
        url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalMonthOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GlobOpenMonth = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
    //GetJournalPeriod
    $.ajax({
        url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalPeriodOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GlobOpenPeriod = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });
  
    
    



    function showDetails(e) {
        var dataItemX;
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#StatusHeader").val("PENDING");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#TrxInformation").hide();
            $("#BtnNewVoucher").show();
            $("#ValueDate").data("kendoDatePicker").value(_d);
            $("#detailRef").hide();
            if (_GlobClientCode == "01") {
                $("#gridReferenceDetail").empty();
            }
            else {
                $("#gridCashierIDDetail").empty();
            }
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridCashierReceiptApproved").data("kendoGrid");

            }
            if (tabindex == 1) {

                grid = $("#gridCashierReceiptPending").data("kendoGrid");
            }
            if (tabindex == 2) {

                grid = $("#gridCashierReceiptHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnNewVoucher").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnVoid").hide();
                $("#BtnRevise").show();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
                $("#BtnNewVoucher").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
                $("#BtnNewVoucher").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnPosting").show();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnUpdate").show();
                $("#BtnOldData").hide();
                $("#BtnNewVoucher").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
                $("#BtnNewVoucher").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnNewVoucher").hide();
            }
            dirty = null;
            $("#CashierPK").val(dataItemX.CashierPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (_GlobClientCode != "01") {
                $("#CashierID").val(dataItemX.CashierID);
                $("#PeriodPK").val(dataItemX.PeriodPK);
            }
            $("#Reference").val(dataItemX.Reference);
            $("#Description").val(dataItemX.Description);
            $("#DebitAccountPK").val(dataItemX.DebitAccountPK);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Revised == false) {
                $("#State").removeClass("Posted").removeClass("Reversed").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");

            }
            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Reversed").addClass("Posted");
                $("#State").text("POSTED");
            }
            if (dataItemX.Revised == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Reversed");
                $("#State").text("REVISED");
            }
            $("#PercentAmount").val(dataItemX.PercentAmount);
            $("#FinalAmount").val(dataItemX.FinalAmount);
            $("#OfficePK").val(dataItemX.OfficePK);
            $("#OfficeID").val(dataItemX.OfficeID);
            $("#DepartmentPK").val(dataItemX.DepartmentPK);
            $("#DepartmentID").val(dataItemX.DepartmentID);
            $("#AgentPK").val(dataItemX.AgentPK);
            $("#AgentID").val(dataItemX.AgentID);
            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#CounterpartID").val(dataItemX.CounterpartID);
            $("#ConsigneePK").val(dataItemX.ConsigneePK);
            $("#ConsigneeID").val(dataItemX.ConsigneeID);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#DocRef").val(dataItemX.DocRef);
            $("#NoReference").val(dataItemX.NoReference);
            $("#ItemBudgetPK").val(dataItemX.ItemBudgetPK);
            $("#ItemBudgetID").val(dataItemX.ItemBudgetID);
            $("#PostedBy").text(dataItemX.PostedBy);
            $("#RevisedBy").text(dataItemX.RevisedBy);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);

            if (dataItemX.RevisedTime == null) {
                $("#RevisedTime").text("");
            } else {
                $("#RevisedTime").text(kendo.toString(kendo.parseDate(dataItemX.RevisedTime), 'MM/dd/yyyy HH:mm:ss'));
            }
            if (dataItemX.PostedTime == null) {
                $("#PostedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            $("#detailRef").show();
            if (_GlobClientCode == "01") {
                $("#gridReferenceDetail").empty();
                initReferenceDetail();
            }
            else {
                $("#gridCashierIDDetail").empty();
                initCashierIDDetail();
            }

        }

        $("#CheckPercentage").prop('checked', false);
        $("#DebitCredit").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "DEBIT", value: "D" },
                { text: "CREDIT", value: "C" },
            ],
            filter: "contains",
            index: 1,
            suggest: true,
            change: onChangeDebitCredit,
            value: setCmbDebitCredit()
        });


        function setCmbDebitCredit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCredit == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCredit;
                }
            }
        }

        function onChangeDebitCredit() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            //recalAmount($("#DAmount").data("kendoNumericTextBox").value());
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    template: "<table><tr><td width='70px'>${ID}</td></tr></table>",
                    dataSource: data,
                    change: OnChangePeriodPK,
                    enabled: false,
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
                return _defaultPeriodPK;
            } else {
                if (dataItemX.PeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodPK;
                }
            }
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CashierType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    value: setCmbType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbType() {
            if (e == null) {
                return "CR";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
                }
            }
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DebitCashRefPK").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeDebitCashRefPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbDebitCashRefPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbDebitCashRefPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCashRefPK == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCashRefPK;
                }
            }
        }
        function OnChangeDebitCashRefPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CashRef/GetCashRefByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitCashRefPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DebitCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        $("#DebitAccountPK").val(data.AccountPK);
                        var LastRate = {
                            Date: $('#ValueDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DebitCurrencyRate").data("kendoNumericTextBox").value(data);
                                recalAmount();
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnlyExcludeCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CreditAccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCreditAccountPK,
                    value: setCmbCreditAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbCreditAccountPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditAccountPK == 0) {
                    return "";
                } else {
                    return dataItemX.CreditAccountPK;
                }
            }
        }
        function OnChangeCreditAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                if ($("#CreditAccountPK").val() == "") {
                    alertify.alert("Please Fill Account Correctly!");
                    return;
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccountPK").data("kendoComboBox").value(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#CreditCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                            var LastRate = {
                                Date: $('#ValueDate').val(),
                                CurrencyPK: data.CurrencyPK,
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(LastRate),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $("#CreditCurrencyRate").data("kendoNumericTextBox").value(data);
                                    recalAmount();
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

        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CreditCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbCreditCurrencyPK()
                });
                $("#DebitCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbDebitCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCreditCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.CreditCurrencyPK;
                }
            }
        }
        function setCmbDebitCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCurrencyPK;
                }
            }
        }

        $("#DebitCurrencyRate").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            value: setDebitCurrencyRate()
        });
        function setDebitCurrencyRate() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.DebitCurrencyRate;
            }
        }



        $("#CreditCurrencyRate").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            value: setCreditCurrencyRate()
        });
        function setCreditCurrencyRate() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.CreditCurrencyRate;
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
                }

            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#OfficePK").kendoComboBox({
                    dataValueField: "OfficePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeOfficePK,
                    dataSource: data,
                    value: setCmbOfficePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeOfficePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbOfficePK() {
            if (e == null) {
                return 1;
            } else {
                if (dataItemX.OfficePK == 0) {
                    return "";
                } else {
                    return dataItemX.OfficePK;
                }

            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentComboShowFinanceOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDepartmentPK,
                    dataSource: data,
                    value: setCmbDepartmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDepartmentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDepartmentPK() {
            if (e == null) {
                if (_GlobClientCode == '12') {
                    return 1;
                }
                else {
                    return "";
                }
            } else {
                if (dataItemX.DepartmentPK == 0) {
                    return "";
                } else {
                    return dataItemX.DepartmentPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAgentPK,
                    dataSource: data,
                    value: setCmbAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeAgentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAgentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentPK == 0) {
                    return "";
                } else {
                    return dataItemX.AgentPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeCounterpartPK,
                    dataSource: data,
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Consignee/GetConsigneeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ConsigneePK").kendoComboBox({
                    dataValueField: "ConsigneePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeConsigneePK,
                    dataSource: data,
                    value: setCmbConsigneePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeConsigneePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbConsigneePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ConsigneePK == 0) {
                    return "";
                } else {
                    return dataItemX.ConsigneePK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/ItemBudget/GetItemBudgetCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ItemBudgetPK").kendoComboBox({
                    dataValueField: "ItemBudgetPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeItemBudgetPK,
                    dataSource: data,
                    value: setCmbItemBudgetPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeItemBudgetPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbItemBudgetPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ItemBudgetPK == 0) {
                    return "";
                } else {
                    return dataItemX.ItemBudgetPK;
                }

            }
        }


        $("#PercentAmount").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            change: OnChangePercentAmount,
            value: setPercentAmount()
        });

        function OnChangePercentAmount() {
            recalAmount();
        }
        function setPercentAmount() {
            if (e == null) {
                return 100;
            } else {
                return dataItemX.PercentAmount;
            }
        }


        $("#FinalAmount").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            value: setFinalAmount()
        });
        function setFinalAmount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.FinalAmount;
            }
        }



        $("#Amount").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            change: OnChangeAmount,
            value: setAmount()
        });


        function OnChangeAmount() {
            recalAmount();
        }

        function setAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Credit;
            }
        }


        $("#BaseDebit").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            value: setBaseDebit()
        });
        function setBaseDebit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.BaseDebit;
            }
        }


        $("#BaseCredit").kendoNumericTextBox({
            format: "n" + GlobDecimalPlaces,
            decimals: GlobDecimalPlaces,
            value: setBaseCredit()
        });
        function setBaseCredit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.BaseCredit;
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
    function clearDataWhenAdd() {
        if (_GlobClientCode == "01") {
            $("#gridReferenceDetail").empty();
        }
        else {
            $("#gridCashierIDDetail").empty();
        }
        if (_GlobClientCode != "19") {
            $("#DocRef").val("");
        }

        $("#Notes").val("");
        $("#Amount").data("kendoNumericTextBox").value("");
        $("#PercentAmount").data("kendoNumericTextBox").value("100");
        $("#FinalAmount").data("kendoNumericTextBox").value("");
        $("#BaseDebit").data("kendoNumericTextBox").value("");
        $("#BaseCredit").data("kendoNumericTextBox").value("");
        $("#DepartmentPK").data("kendoComboBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        $("#CounterpartPK").data("kendoComboBox").value("");
        $("#ConsigneePK").data("kendoComboBox").value("");
        $("#InstrumentPK").data("kendoComboBox").value("");
        $("#ItemBudgetPK").data("kendoComboBox").value("");
        $("#NoReference").val("");
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
    }
    function clearData() {
        $("#CashierPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        if (_GlobClientCode == "01") {
            $("#Reference").val("");
        }
        else {
            $("#CashierID").val("");
            $("#Reference").val("");
        }
        $("#Amount").data("kendoNumericTextBox").value("");
        $("#PercentAmount").data("kendoNumericTextBox").value("100");
        $("#FinalAmount").data("kendoNumericTextBox").value("");
        $("#BaseDebit").data("kendoNumericTextBox").value("");
        $("#BaseCredit").data("kendoNumericTextBox").value("");
        $("#Description").val("");
        $("#OfficePK").data("kendoComboBox").value("");
        $("#DepartmentPK").data("kendoComboBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        $("#CounterpartPK").data("kendoComboBox").value("");
        $("#ConsigneePK").data("kendoComboBox").value("");
        $("#InstrumentPK").data("kendoComboBox").value("");
        $("#ItemBudgetPK").data("kendoComboBox").value("");
        $("#NoReference").val("");
        $("#DocRef").val("");
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

    }


    $("#BtnNewVoucher").click(function () {
        $("#detailRef").hide();
        $("#gridCashierIDDetail").empty();
        $("#CashierPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        if (_GlobClientCode == "01") {
            $("#Reference").val("");
        }
        else {
            $("#CashierID").val("");
            $("#Reference").val("");
        }
        $("#Amount").data("kendoNumericTextBox").value("");
        $("#PercentAmount").data("kendoNumericTextBox").value("100");
        $("#FinalAmount").data("kendoNumericTextBox").value("");
        $("#BaseDebit").data("kendoNumericTextBox").value("");
        $("#BaseCredit").data("kendoNumericTextBox").value("");
        $("#Description").val("");
        // $("#OfficePK").data("kendoComboBox").value("");
        $("#DepartmentPK").data("kendoComboBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        $("#CounterpartPK").data("kendoComboBox").value("");
        $("#ConsigneePK").data("kendoComboBox").value("");
        $("#InstrumentPK").data("kendoComboBox").value("");
        $("#ItemBudgetPK").data("kendoComboBox").value("");
        $("#NoReference").val("");
        $("#Reference").val("");
        $("#DocRef").val("");
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

    });
    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        //$("#Detail").show();
    }

    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashierPK").val() + "/" + $("#HistoryPK").val() + "/" + "Cashier",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Cashier" + "/" + $("#CashierPK").val(),
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

    function getDataSource(_url) {
        if (_GlobClientCode == "01") {
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
                                         CashierPK: { type: "number" },
                                         HistoryPK: { type: "number" },
                                         Selected: { type: "boolean" },
                                         Status: { type: "number" },
                                         StatusDesc: { type: "string" },
                                         Notes: { type: "String" },
                                         PeriodPK: { type: "number" },
                                         PeriodID: { type: "string" },
                                         ValueDate: { type: "date" },
                                         Type: { type: "string" },
                                         Reference: { type: "string" },
                                         DebitCredit: { type: "string" },
                                         Description: { type: "string" },
                                         DebitCurrencyPK: { type: "number" },
                                         DebitCurrencyID: { type: "string" },
                                         CreditCurrencyPK: { type: "number" },
                                         CreditCurrencyID: { type: "string" },
                                         DebitCashRefPK: { type: "number" },
                                         DebitCashRefID: { type: "string" },
                                         DebitCashRefName: { type: "string" },
                                         CreditCashRefPK: { type: "number" },
                                         CreditCashRefID: { type: "string" },
                                         CreditCashRefName: { type: "string" },
                                         DebitAccountPK: { type: "number" },
                                         DebitAccountID: { type: "string" },
                                         DebitAccountName: { type: "string" },
                                         CreditAccountPK: { type: "number" },
                                         CreditAccountID: { type: "string" },
                                         CreditAccountName: { type: "string" },
                                         Debit: { type: "number" },
                                         Credit: { type: "number" },
                                         DebitCurrencyRate: { type: "number" },
                                         CreditCurrencyRate: { type: "number" },
                                         BaseDebit: { type: "number" },
                                         BaseCredit: { type: "number" },
                                         PercentAmount: { type: "number" },
                                         FinalAmount: { type: "number" },
                                         OfficePK: { type: "number" },
                                         OfficeID: { type: "string" },
                                         DepartmentPK: { type: "number" },
                                         DepartmentID: { type: "string" },
                                         AgentPK: { type: "number" },
                                         AgentID: { type: "string" },
                                         CounterpartPK: { type: "number" },
                                         CounterpartID: { type: "string" },
                                         ConsigneePK: { type: "number" },
                                         ConsigneeID: { type: "string" },
                                         InstrumentPK: { type: "number" },
                                         InstrumentID: { type: "string" },
                                         DocRef: { type: "string" },
                                         Posted: { type: "boolean" },
                                         PostedBy: { type: "string" },
                                         PostedTime: { type: "date" },
                                         Revised: { type: "boolean" },
                                         RevisedBy: { type: "string" },
                                         RevisedTime: { type: "date" },
                                         EntryUsersID: { type: "string" },
                                         EntryTime: { type: "date" },
                                         UpdateUsersID: { type: "string" },
                                         UpdateTime: { type: "date" },
                                         ApprovedUsersID: { type: "string" },
                                         ApprovedTime: { type: "date" },
                                         VoidUsersID: { type: "string" },
                                         VoidTime: { type: "date" },
                                         LastUpdate: { type: "date" },
                                         Timestamp: { type: "string" }
                                     }
                                 }
                             }
                         });
        }
        else {
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
                             CashierPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             Type: { type: "string" },
                             CashierID: { type: "number" },
                             DebitCredit: { type: "string" },
                             Description: { type: "string" },
                             DebitCurrencyPK: { type: "number" },
                             DebitCurrencyID: { type: "string" },
                             CreditCurrencyPK: { type: "number" },
                             CreditCurrencyID: { type: "string" },
                             DebitCashRefPK: { type: "number" },
                             DebitCashRefID: { type: "string" },
                             DebitCashRefName: { type: "string" },
                             CreditCashRefPK: { type: "number" },
                             CreditCashRefID: { type: "string" },
                             CreditCashRefName: { type: "string" },
                             DebitAccountPK: { type: "number" },
                             DebitAccountID: { type: "string" },
                             DebitAccountName: { type: "string" },
                             CreditAccountPK: { type: "number" },
                             CreditAccountID: { type: "string" },
                             CreditAccountName: { type: "string" },
                             Debit: { type: "number" },
                             Credit: { type: "number" },
                             DebitCurrencyRate: { type: "number" },
                             CreditCurrencyRate: { type: "number" },
                             BaseDebit: { type: "number" },
                             BaseCredit: { type: "number" },
                             PercentAmount: { type: "number" },
                             FinalAmount: { type: "number" },
                             OfficePK: { type: "number" },
                             OfficeID: { type: "string" },
                             DepartmentPK: { type: "number" },
                             DepartmentID: { type: "string" },
                             AgentPK: { type: "number" },
                             AgentID: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             ConsigneePK: { type: "number" },
                             ConsigneeID: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             DocRef: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Revised: { type: "boolean" },
                             RevisedBy: { type: "string" },
                             RevisedTime: { type: "date" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
        }

    }
    function onDataBound() {
        var grid = $("#gridCashierReceiptApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowPending");
            } else if (row.Revised == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowPosted");
            }
        });
    }
    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()
            // var gridApproved = $("#gridCashierReceiptApproved").data("kendoGrid");
            // gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            RecalGridPending();
        }
        if (tabindex == 2) {
            RecalGridHistory();
        }
    }
    function initGrid() {
        if (_GlobClientCode == "01") {
            $("#gridCashierReceiptApproved").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(CashierApprovedURL);
            }

            //var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/CR",
            //  dataSourceApproved = getDataSource(CashierApprovedURL);
            var grid = $("#gridCashierReceiptApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                       //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
                     //  { command: { text: "Reverse", click: showReverse }, title: " ", width: 90 },
                    //   {
                    //       field: "Selected",
                    //       width: 50,
                    //       template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //       headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    //       filterable: true,
                    //       sortable: false,
                    //       columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 50, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 50, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "RefNo", title: "RefNo", width: 50 },
                        { field: "Reference", title: "Reference", width: 100 },


                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditAccountID", title: "RECEIPT FOR", width: 150 },
                        { field: "CreditAccountName", title: "RECEIPT FOR", width: 150 },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefID", title: "BANK/CASH ID", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountID", title: "PAYMENT FROM", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "BaseDebit", title: "Receipt Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },

                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        else if (_GlobClientCode == "05") {
            $("#gridCashierReceiptApproved").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(CashierApprovedURL);
            }

            //var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/CR",
            //  dataSourceApproved = getDataSource(CashierApprovedURL);
            var grid = $("#gridCashierReceiptApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                       //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
                     //  { command: { text: "Reverse", click: showReverse }, title: " ", width: 90 },
                    //   {
                    //       field: "Selected",
                    //       width: 50,
                    //       template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //       headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    //       filterable: true,
                    //       sortable: false,
                    //       columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 100 },
                        { field: "CashierID", title: "CashierID", width: 120 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "Reference", title: "RefNo", width: 120 },
                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 150 },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 150 },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "NoReference", title: "NoReference", width: 150 },
                        { field: "Description", title: "Description", width: 250 },
                        { field: "BaseDebit", title: "Amount IDR", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                        { field: "Posted", title: "Posted", width: 100, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 100, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n2}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "Type", title: "Type", width: 100 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n2}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        else {
            $("#gridCashierReceiptApproved").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(CashierApprovedURL);
            }

            //var CashierApprovedURL = window.location.origin + "/Radsoft/Cashier/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/CR",
            //  dataSourceApproved = getDataSource(CashierApprovedURL);
            var grid = $("#gridCashierReceiptApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                       //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
                     //  { command: { text: "Reverse", click: showReverse }, title: " ", width: 90 },
                    //   {
                    //       field: "Selected",
                    //       width: 50,
                    //       template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //       headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    //       filterable: true,
                    //       sortable: false,
                    //       columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 80, hidden: true },
                        { field: "CashierID", title: "CashierID", width: 100 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "Reference", title: "RefNo", width: 50 },
                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 150 },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 150 },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "Posted", title: "Posted", width: 50, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 50, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        var oldPageSizeApproved = 0;
        grid.table.on("click", ".checkboxApproved", selectRowApproved);

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridCashierReceiptApproved").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourceApproved.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end grid filter

        $('#chbBApproved').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

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

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridCashierReceiptApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.CashierPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

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


            var grid = $("#gridCashierReceiptApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _cashierPK = dataItemX.CashierPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _cashierPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCashierReceipt").kendoTabStrip({
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
                        RecalGridPending()
                    }
                    if (tabindex == 2) {
                        RecalGridHistory()
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
        $("#BtnVoidBySelected").show();
        $("#BtnReviseBySelected").show();
    }
    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataCashier/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Cashier/" + _a + "/" + _b + "/CR",
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
    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateCashier/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Cashier/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
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
        if (_GlobClientCode == "01") {
            $("#gridCashierReceiptPending").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierPendingURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(CashierPendingURL);
            }
            var grid = $("#gridCashierReceiptPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                dataBound: onDataBoundPending,
                resizable: true,
                toolbar: ["excel"],
                columns: [
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                       //{
                       //    field: "Selected",
                       //    width: 50,
                       //    template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                       //    headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                       //    filterable: true,
                       //    sortable: false,
                       //    columnMenu: false
                       //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 50, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 50, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "RefNo", title: "RefNo", width: 50 },
                        { field: "Reference", title: "Reference", width: 100 },

                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 100 },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "05") {
            $("#gridCashierReceiptPending").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierPendingURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(CashierPendingURL);
            }
            var grid = $("#gridCashierReceiptPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                dataBound: onDataBoundPending,
                resizable: true,
                toolbar: ["excel"],
                columns: [
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //   {
                    //       field: "Selected",
                    //       width: 50,
                    //       template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //       headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                    //       filterable: true,
                    //       sortable: false,
                    //       columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 50, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 50, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "RefNo", title: "RefNo", width: 50 },
                        { field: "Reference", title: "Reference", width: 100 },

                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 100 },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "NoReference", title: "NoReference", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 300, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n2}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n2}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            $("#gridCashierReceiptPending").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierPendingURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(CashierPendingURL);
            }
            var grid = $("#gridCashierReceiptPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                dataBound: onDataBoundPending,
                resizable: true,
                toolbar: ["excel"],
                columns: [
                       { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //   {
                    //       field: "Selected",
                    //       width: 50,
                    //       template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //       headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                    //       filterable: true,
                    //       sortable: false,
                    //       columnMenu: false
                    //},
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 50, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 50, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "Reference", title: "RefNo", width: 50 },
                        { field: "CashierID", title: "CashierID", width: 100 },
                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 100 },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        var oldPageSizePending = 0;
        grid.table.on("click", ".checkboxPending", selectRowPending);
        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridCashierReceiptPending").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourcePending.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end filter

        $('#chbBPending').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizePending = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxPending').each(function (idx, item) {
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

            grid.dataSource.pageSize(oldPageSizePending);

        });

        function selectRowPending() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridCashierReceiptPending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.CashierPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].CashierPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxPending")
                        .attr("checked", "checked");
                }
            }
        }

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


            var grid = $("#gridCashierReceiptPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _cashierPK = dataItemX.CashierPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _cashierPK);

        }
        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnReviseBySelected").hide();

    }
    function RecalGridHistory() {
        if (_GlobClientCode == "01") {
            $("#gridCashierReceiptHistory").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierHistoryURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(CashierHistoryURL);
            }
            $("#gridCashierReceiptHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                        { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "StatusDesc", title: "StatusDesc", width: 200 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "RefNo", title: "RefNo", width: 50 },
                        { field: "Reference", title: "RefNo", width: 50 },
                        //{ field: "CashierID", title: "CashierID", width: 100 },
                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 100 },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else {

            $("#gridCashierReceiptHistory").empty();
            if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                alertify.alert("Please Fill Date");
            }

            else {
                var CashierHistoryURL = window.location.origin + "/Radsoft/Cashier/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + "CR" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(CashierHistoryURL);
            }
            $("#gridCashierReceiptHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Cashier Receipt"
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                        { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                        { field: "CashierPK", title: "SysNo.", width: 80 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                        { field: "StatusDesc", title: "StatusDesc", width: 200 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                        { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                        { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                        { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                        { field: "Reference", title: "RefNo", width: 50 },
                        { field: "CashierID", title: "CashierID", width: 100 },
                        { field: "DebitCashRefID", title: "BANK/CASH", width: 150 },
                        { field: "CreditCashRefID", title: "BANK/CASH", width: 150, hidden: true },
                        { field: "CreditAccountID", title: "RECEIPT FROM", width: 100 },
                        { field: "DebitAccountID", title: "PAYMENT FOR", width: 150, hidden: true },
                        { field: "DocRef", title: "DocRef", width: 100 },
                        { field: "Description", title: "Description", width: 200 },
                        { field: "BaseDebit", title: "Amount IDR", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
                        { field: "Credit", title: "Amount", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "CreditCashRefName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "DebitCashRefName", title: "NAME (Debit)", width: 200, hidden: true },
                        { field: "CreditAccountName", title: "NAME (Credit)", width: 200, hidden: true },
                        { field: "CreditCurrencyPK", title: "CreditCurrencyPK", hidden: true, width: 120, hidden: true },
                        { field: "CreditCurrencyID", title: "Currency ID", width: 200, hidden: true },
                        { field: "CreditCurrencyRate", title: "Rate", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "BaseCredit", title: "Base Amount (IDR)", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 200, hidden: true },
                        { field: "Type", title: "Type", width: 200 },
                        { field: "PostedBy ", title: "Posted By", width: 200 },
                        { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "CreditCashRefPK", title: "CreditCashRefPK", hidden: true, width: 120 },
                        { field: "CreditAccountPK", title: "CreditAccountPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyPK", title: "DebitCurrencyPK", hidden: true, width: 120 },
                        { field: "DebitCurrencyID", title: "Currency (Debit)", width: 170, hidden: true },
                        { field: "DebitCashRefPK", title: "DebitCashRefPK", hidden: true, width: 120 },
                        { field: "DebitAccountPK", title: "DebitAccountPK", hidden: true, width: 120 },
                        { field: "Debit", title: "Receipt Original Amount (Debit)", width: 300, format: "{0:n" + GlobDecimalPlaces + "}", hidden: true },
                        { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120, hidden: true },
                        { field: "PeriodID", title: "Period ID", width: 200, hidden: true },
                        { field: "DebitCredit", title: "Debit Credit", hidden: true, width: 200, hidden: true },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }


        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnReviseBySelected").hide();
    }
    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };
    function gridApprovedOnDataBound() {
        var grid = $("#gridCashierReceiptApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            if (checkedApproved[row.CashierPK]) {
                $('tr[data-uid="' + row.uid + '"] ')
                    .addClass("k-state-selected")
                    .find(".checkboxApproved")
                    .attr("checked", "checked");
            }

            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            } else if (row.Revised == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }
    function gridHistoryDataBound() {
        var grid = $("#gridCashierReceiptHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }
    function showPosting(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {

            alertify.confirm("Are you sure want to Posting ?", function (a) {
                if (a) {
                    var grid = $("#gridCashierReceiptApproved").data("kendoGrid");
                    var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
                    var pos = _dataItem.Posted;

                    if (pos == true) {
                        alertify.alert("Data Already Posted");
                    }
                    else {
                        var _ref = _dataItem.CashierID.toString();
                        var _type = _dataItem.Type.toString();
                        if (_ref != "") {
                            var Posting = {
                                ParamMode: "By CashierID",
                                ParamCashierID: _ref,
                                Type: _type,
                                HistoryPK: _dataItem.HistoryPK,
                                CashierPK: _dataItem.CashierPK,
                                ParamUserID: sessionStorage.getItem("user")
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_Posting",
                                type: 'POST',
                                data: JSON.stringify(Posting),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("No CashierID");
                        }
                    }
                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }
    function showRevise(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {

            alertify.confirm("Are you sure want to Revise ?", function (a) {
                if (a) {
                    var grid = $("#gridCashierReceiptApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    if (_dataItemX.Revised == true) {
                        alertify.alert("Data Already Revised");
                    }
                    else if (_dataItemX.Posted == false) {
                        alertify.alert("Data Not Posting Yet");
                    }
                    else {
                        var _ref = _dataItemX.CashierID.toString();
                        if (_ref != "") {
                            var Revise = {
                                ParamMode: "By CashierID",
                                ParamCashierID: _ref,
                                HistoryPK: _dataItem.HistoryPK,
                                ParamUserID: sessionStorage.getItem("user"),
                                CashierPK: _dataItemX.CashierPK
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_Revise",
                                type: 'POST',
                                data: JSON.stringify(Revise),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("No CashierID");
                        }
                    }
                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }
    function recalAmount() {
        GlobDebit = 0;
        var _finalAmount = $("#Amount").data("kendoNumericTextBox").value() * ($("#PercentAmount").data("kendoNumericTextBox").value() / 100);
        var _baseCredit = _finalAmount * $("#CreditCurrencyRate").data("kendoNumericTextBox").value();
        $("#BaseCredit").data("kendoNumericTextBox").value(_baseCredit);
        $("#BaseDebit").data("kendoNumericTextBox").value(_baseCredit);
        $("#FinalAmount").data("kendoNumericTextBox").value(_finalAmount);
        GlobDebit = _baseCredit / $("#DebitCurrencyRate").data("kendoNumericTextBox").value()
    }
    $("#BtnVoucher").click(function () {
        var _status;

        if ($("#StatusHeader").val() == "PENDING") {
            _status = 1;
        } else {
            _status = 2;
        }
        if ($('#CashierPK').val() == null || $('#CashierPK').val() == "") {
            alertify.alert("Download Receipt Voucher Cancel,Please Add Cashier First");
        }
        else {

            alertify.confirm("Are you sure want to Download Receipt Voucher ?", function (e) {

                if (e) {
                    if (_GlobClientCode == "01") {
                        var Cashier = {
                            Reference: $('#Reference').val(),
                            Status: _status,
                            DecimalPlaces: GlobDecimalPlaces,
                            //PeriodPK: $('#PeriodPK').val(),
                        };

                    }
                    else {
                        var Cashier = {
                            CashierID: $('#CashierID').val(),
                            Status: _status,
                            PeriodPK: $('#PeriodPK').val(),
                            DecimalPlaces: GlobDecimalPlaces,
                        };


                    }
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Cashier/ReceiptVoucher/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(Cashier),
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

    $("#BtnPosting").click(function () {
        var _urlValidate = "";
        //if (_GlobClientCode == "01" || _GlobClientCode == "21") {
        //    _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingCashierByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR" + "/" + $("#CashierPK").val();
        //} else {
        //    _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingCashierById/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR" + "/" + $("#CashierID").val();
        //}

        _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingCashierByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR" + "/" + $("#CashierPK").val();
               
        $.ajax({
            url: _urlValidate,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == "") {
                    alertify.confirm("Are you sure want to Posting ?", function (a) {
                        if (a) {
                            var Posting = {
                                ParamReference: $('#Reference').val(),
                                Type: $('#Type').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ParamUserID: sessionStorage.getItem("user"),
                                CashierPK: $('#CashierPK').val()
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_Posting",
                                type: 'POST',
                                data: JSON.stringify(Posting),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                    win.close();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("You've clicked Cancel");
                        }
                    });
                }
                else
                    alertify.alert(data);
            }
        });
        
    });

    $("#BtnRevise").click(function () {

        alertify.confirm("Are you sure want to Revise ?", function (a) {
            if (a) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Cashier/Cashier_ValidateOtherTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CashierPK').val() + "/CR",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            var Posting = {
                                ParamReference: $('#Reference').val(),
                                Type: $('#Type').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ParamUserID: sessionStorage.getItem("user"),
                                CashierPK: $('#CashierPK').val()
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_Revise",
                                type: 'POST',
                                data: JSON.stringify(Posting),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                    win.close();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
                        else {
                            alertify.alert(data);
                        }
                    }
                });
                
            } else {
                alertify.alert("You've clicked Cancel");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    $("#BtnGenerateNewCashierID").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/CashierCashierID/CashierCashierID_GenerateNewCashierID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#Type").data("kendoComboBox").value() + "/" + $("#PeriodPK").data("kendoComboBox").value(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashierID").val(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    $("#BtnAdd").click(function () {

        //GetJournalMoth
        $.ajax({
            url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalMonthOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                GlobOpenMonth = data;
                //GetJournalPeriod
                $.ajax({
                    url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalPeriodOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        GlobOpenPeriod = data;

                        //add
                        if (_GlobClientCode == "01") {
                            if ($("#CreditAccountPK").val() == "") {
                                alertify.alert("Please Fill Account Correctly!");
                                return;
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccountPK").val(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data.Type > 2 && ($("#DepartmentPK").val() == null || $("#DepartmentPK").val() == '')) {
                                            alertify.alert("Please choose cost center for Account Income/Expense");
                                        }
                                        else {

                                            if ($("#ValueDate").data("kendoDatePicker").value().getMonth().toString() == GlobOpenMonth - 1 && $("#PeriodPK").data("kendoComboBox").value() == GlobOpenPeriod) {
                                                var val = validateData();

                                                if (val == 1) {

                                                    alertify.confirm("Are you sure want to Add Cashier Receipt ( " + $("#DebitCredit").data("kendoComboBox").text() + " ) ?", function (e) {
                                                        if (e) {

                                                            if ($('#Reference').val() == null || $('#Reference').val() == "") {
                                                                setTimeout(function () {
                                                                    alertify.confirm("Are you sure want to Generate New Reference ?", function (e) {
                                                                        if (e) {
                                                                            var _urlRef = "";
                                                                            _urlRef = "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#Type").data("kendoComboBox").value() + "/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference" + "/" + $("#DebitCashRefPK").data("kendoComboBox").value();


                                                                            $.ajax({
                                                                                url: window.location.origin + _urlRef,
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    $("#Reference").val(data);
                                                                                    var _ref = data.replace(/\//g, "-");
                                                                                    alertify.alert("Your new reference is " + $("#Reference").val());
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Cashier/GetBankBalanceByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/CR",
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            var _potAmount = 0;
                                                                                            var _percentAmount = 0;
                                                                                            _percentAmount = $('#PercentAmount').val();
                                                                                            var _creditAccountPK = $('#CreditAccountPK').val();
                                                                                            if ($('#DebitCredit').val() == 'D') {
                                                                                                _potAmount = $('#BaseDebit').val() * -1
                                                                                            } else {
                                                                                                _potAmount = $('#BaseDebit').val()
                                                                                            }
                                                                                            if ((parseFloat(data + _potAmount)) > 0) {
                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/Cashier/GetSumPercentAmountByReferenceByAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/" + _creditAccountPK + "/" + _percentAmount + "/CR",
                                                                                                    type: 'GET',
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {
                                                                                                        if ((data) <= 100) {
                                                                                                            alertify.alert("Percent Amount This Account : " + data + " %");
                                                                                                            var Cashier = {
                                                                                                                PeriodPK: $('#PeriodPK').val(),
                                                                                                                ValueDate: $('#ValueDate').val(),
                                                                                                                Type: $('#Type').val(),
                                                                                                                Reference: $("#Reference").val(),
                                                                                                                DebitCredit: $('#DebitCredit').val(),
                                                                                                                Description: $('#Description').val(),
                                                                                                                DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                                                                CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                                                                DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                                                                CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                                                                DebitAccountPK: $('#DebitAccountPK').val(),
                                                                                                                CreditAccountPK: $('#CreditAccountPK').val(),
                                                                                                                Debit: GlobDebit,
                                                                                                                Credit: $('#FinalAmount').val(),
                                                                                                                PercentAmount: $('#PercentAmount').val(),
                                                                                                                FinalAmount: GlobDebit,
                                                                                                                DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                                                                CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                                                                BaseDebit: $('#BaseDebit').val(),
                                                                                                                BaseCredit: $('#BaseCredit').val(),
                                                                                                                OfficePK: $('#OfficePK').val(),
                                                                                                                DepartmentPK: $('#DepartmentPK').val(),
                                                                                                                CounterpartPK: $('#CounterpartPK').val(),
                                                                                                                AgentPK: $('#AgentPK').val(),
                                                                                                                ConsigneePK: $('#ConsigneePK').val(),
                                                                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                                                                DocRef: $('#DocRef').val(),
                                                                                                                NoReference: $('#NoReference').val(),
                                                                                                                ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                                                                EntryUsersID: sessionStorage.getItem("user")

                                                                                                            };
                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_I",
                                                                                                                type: 'POST',
                                                                                                                data: JSON.stringify(Cashier),
                                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                                success: function (data) {
                                                                                                                    alertify.alert(data.Message);
                                                                                                                    $("#CashierPK").val(data.CashierPK);
                                                                                                                    $("#HistoryPK").val(data.HistoryPK);
                                                                                                                    $("#DebitCredit").data("kendoComboBox").value('C');
                                                                                                                    $("#DebitCredit").data("kendoComboBox").text('Credit');
                                                                                                                    refresh();
                                                                                                                    clearDataWhenAdd();
                                                                                                                    $("#detailRef").show();
                                                                                                                    $("#gridReferenceDetail").empty();
                                                                                                                    initReferenceDetail();
                                                                                                                },
                                                                                                                error: function (data) {
                                                                                                                    alertify.alert(data.responseText);
                                                                                                                }
                                                                                                            });
                                                                                                        } else {
                                                                                                            alertify.alert("Check Sum Percent. Percent is " + data + " % from this account");
                                                                                                        }
                                                                                                    }
                                                                                                });
                                                                                            }
                                                                                            else {
                                                                                                alertify.alert("Check Debit Credit Position. Bank is Minus");
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
                                                                        } else {
                                                                            alertify.alert("Add Cashier Receipt Cancel,Please Choose old Reference");
                                                                        }
                                                                    });
                                                                }, 1000);
                                                            }
                                                            //Cek Reference bila manual, Cek ValueDate , Cek CashRef                      
                                                            else {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/Cashier/AddCashierValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#DebitCashRefPK").val() + "/" + $("#Reference").val().split('/').join('-') + "/CR",
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data == 1) {
                                                                            var _ref = $("#Reference").val().replace(/\//g, "-");
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/Cashier/GetBankBalanceByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/CR",
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    var _potAmount = 0;
                                                                                    var _percentAmount = 0;
                                                                                    _percentAmount = $('#PercentAmount').val();
                                                                                    var _creditAccountPK = $('#CreditAccountPK').val();
                                                                                    if ($('#DebitCredit').val() == 'D') {
                                                                                        _potAmount = $('#BaseDebit').val() * -1
                                                                                    } else {
                                                                                        _potAmount = $('#BaseDebit').val()
                                                                                    }

                                                                                    if ((parseFloat(data + _potAmount)) > 0) {
                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/Cashier/GetSumPercentAmountByReferenceByAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/" + _creditAccountPK + "/" + _percentAmount + "/CR",
                                                                                            type: 'GET',
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                if ((data) <= 100 || $('#CheckPercentage').is(":checked") == true) {
                                                                                                    alertify.alert("Percent Amount This Account : " + data + " %");
                                                                                                    var Cashier = {
                                                                                                        PeriodPK: $('#PeriodPK').val(),
                                                                                                        ValueDate: $('#ValueDate').val(),
                                                                                                        Type: $('#Type').val(),
                                                                                                        Reference: $('#Reference').val(),
                                                                                                        Description: $('#Description').val(),
                                                                                                        DebitCredit: $('#DebitCredit').val(),
                                                                                                        DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                                                        CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                                                        DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                                                        CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                                                        DebitAccountPK: $('#DebitAccountPK').val(),
                                                                                                        CreditAccountPK: $('#CreditAccountPK').val(),
                                                                                                        Debit: GlobDebit,
                                                                                                        Credit: $('#FinalAmount').val(),
                                                                                                        PercentAmount: $('#PercentAmount').val(),
                                                                                                        FinalAmount: GlobDebit,
                                                                                                        DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                                                        CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                                                        BaseDebit: $('#BaseDebit').val(),
                                                                                                        BaseCredit: $('#BaseCredit').val(),
                                                                                                        OfficePK: $('#OfficePK').val(),
                                                                                                        DepartmentPK: $('#DepartmentPK').val(),
                                                                                                        CounterpartPK: $('#CounterpartPK').val(),
                                                                                                        AgentPK: $('#AgentPK').val(),
                                                                                                        ConsigneePK: $('#ConsigneePK').val(),
                                                                                                        InstrumentPK: $('#InstrumentPK').val(),
                                                                                                        DocRef: $('#DocRef').val(),
                                                                                                        NoReference: $('#NoReference').val(),
                                                                                                        ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                                                    };
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_I",
                                                                                                        type: 'POST',
                                                                                                        data: JSON.stringify(Cashier),
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            alertify.alert(data.Message);
                                                                                                            $("#CashierPK").val(data.CashierPK);
                                                                                                            $("#HistoryPK").val(data.HistoryPK);
                                                                                                            $("#DebitCredit").data("kendoComboBox").value('C');
                                                                                                            $("#DebitCredit").data("kendoComboBox").text('Credit');
                                                                                                            refresh();
                                                                                                            clearDataWhenAdd();
                                                                                                            $("#detailRef").show();
                                                                                                            $("#gridReferenceDetail").empty();
                                                                                                            initReferenceDetail();
                                                                                                        },
                                                                                                        error: function (data) {
                                                                                                            alertify.alert(data.responseText);
                                                                                                        }
                                                                                                    });
                                                                                                } else {
                                                                                                    alertify.alert("Check Sum Percent. there were " + data + " % for this account, ADD CANCEL");
                                                                                                }
                                                                                            }
                                                                                        });
                                                                                    }
                                                                                    else {
                                                                                        alertify.alert("Check Debit Credit Position. Bank is Minus");
                                                                                    }

                                                                                },
                                                                                error: function (data) {
                                                                                    alertify.alert(data.responseText);
                                                                                }
                                                                            });

                                                                        }

                                                                        else {
                                                                            alertify.alert("Value Date / Cash Ref Must Be Equal with Same Reference or Input Data Not Correct in Same Reference");

                                                                        }


                                                                    }
                                                                });

                                                            }

                                                        }

                                                    });
                                                }
                                            }
                                            else {
                                                alertify.alert("Month & Period in Value Date Must be Same With Finance Setup")
                                            }

                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        }
                        else {
                            if ($("#CreditAccountPK").val() == "") {
                                alertify.alert("Please Fill Account Correctly!");
                                return;
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccountPK").val(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        var vals = validate(data);
                                        if (vals == 0) {
                                            if ($("#ValueDate").data("kendoDatePicker").value().getMonth().toString() == GlobOpenMonth - 1 && $("#PeriodPK").data("kendoComboBox").value() == GlobOpenPeriod) {
                                                var val = validateData();

                                                if (val == 1) {

                                                    alertify.confirm("Are you sure want to Add Cashier Receipt ( " + $("#DebitCredit").data("kendoComboBox").text() + " ) ?", function (e) {
                                                        if (e) {

                                                            if ($('#CashierID').val() == null || $('#CashierID').val() == "0" || $('#CashierID').val() == "") {
                                                                setTimeout(function () {
                                                                    alertify.confirm("Are you sure want to Generate New CashierID ?", function (e) {
                                                                        if (e) {
                                                                            var _urlRef = "";
                                                                            if (_GlobClientCode == "04") {
                                                                                _urlRef = "/Radsoft/CustomClient04/CashierCashierID_GenerateNewCashierID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#Type").data("kendoComboBox").value() + "/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#DebitCashRefPK").val() + "/" + "CashierCashierID_GenerateNewCashierID";
                                                                            }
                                                                            else {
                                                                                _urlRef = "/Radsoft/Cashier/Cashier_GenerateNewCashierID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#Type").data("kendoComboBox").value() + "/" + $("#PeriodPK").data("kendoComboBox").value();
                                                                            }

                                                                            $.ajax({
                                                                                url: window.location.origin + _urlRef,
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    $("#CashierID").val(data);
                                                                                    var _ref = data;
                                                                                    alertify.alert("Your new CashierID is " + $("#CashierID").val());
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/Cashier/GetBankBalanceByCashierID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/CR/" + $("#PeriodPK").data("kendoComboBox").value(),
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            var _potAmount = 0;
                                                                                            var _percentAmount = 0;
                                                                                            _percentAmount = $('#PercentAmount').val();
                                                                                            var _creditAccountPK = $('#CreditAccountPK').val();
                                                                                            if ($('#DebitCredit').val() == 'D') {
                                                                                                _potAmount = $('#BaseDebit').val() * -1
                                                                                            } else {
                                                                                                _potAmount = $('#BaseDebit').val()
                                                                                            }
                                                                                            if ((parseFloat(data + _potAmount)) > 0) {
                                                                                                $.ajax({
                                                                                                    url: window.location.origin + "/Radsoft/Cashier/GetSumPercentAmountByCashierIDByAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/" + _creditAccountPK + "/" + _percentAmount + "/CR/" + $("#PeriodPK").data("kendoComboBox").value(),
                                                                                                    type: 'GET',
                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                    success: function (data) {
                                                                                                        if ((data) <= 100) {
                                                                                                            alertify.alert("Percent Amount This Account : " + data + " %");
                                                                                                            var Cashier = {
                                                                                                                PeriodPK: $('#PeriodPK').val(),
                                                                                                                ValueDate: $('#ValueDate').val(),
                                                                                                                Type: $('#Type').val(),
                                                                                                                Reference: $("#Reference").val(),
                                                                                                                CashierID: $("#CashierID").val(),
                                                                                                                DebitCredit: $('#DebitCredit').val(),
                                                                                                                Description: $('#Description').val(),
                                                                                                                DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                                                                CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                                                                DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                                                                CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                                                                DebitAccountPK: $('#DebitAccountPK').val(),
                                                                                                                CreditAccountPK: $('#CreditAccountPK').val(),
                                                                                                                Debit: GlobDebit,
                                                                                                                Credit: $('#FinalAmount').val(),
                                                                                                                PercentAmount: $('#PercentAmount').val(),
                                                                                                                FinalAmount: GlobDebit,
                                                                                                                DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                                                                CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                                                                BaseDebit: $('#BaseDebit').val(),
                                                                                                                BaseCredit: $('#BaseCredit').val(),
                                                                                                                OfficePK: $('#OfficePK').val(),
                                                                                                                DepartmentPK: $('#DepartmentPK').val(),
                                                                                                                CounterpartPK: $('#CounterpartPK').val(),
                                                                                                                AgentPK: $('#AgentPK').val(),
                                                                                                                ConsigneePK: $('#ConsigneePK').val(),
                                                                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                                                                DocRef: $('#DocRef').val(),
                                                                                                                NoReference: $('#NoReference').val(),
                                                                                                                ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                                                                EntryUsersID: sessionStorage.getItem("user")

                                                                                                            };
                                                                                                            $.ajax({
                                                                                                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_I",
                                                                                                                type: 'POST',
                                                                                                                data: JSON.stringify(Cashier),
                                                                                                                contentType: "application/json;charset=utf-8",
                                                                                                                success: function (data) {
                                                                                                                    alertify.alert(data.Message);
                                                                                                                    $("#CashierPK").val(data.CashierPK);
                                                                                                                    $("#HistoryPK").val(data.HistoryPK);
                                                                                                                    $("#DebitCredit").data("kendoComboBox").value('C');
                                                                                                                    $("#DebitCredit").data("kendoComboBox").text('Credit');
                                                                                                                    refresh();
                                                                                                                    clearDataWhenAdd();
                                                                                                                    $("#detailRef").show();
                                                                                                                    $("#gridCashierIDDetail").empty();
                                                                                                                    initCashierIDDetail();
                                                                                                                },
                                                                                                                error: function (data) {
                                                                                                                    alertify.alert(data.responseText);
                                                                                                                }
                                                                                                            });
                                                                                                        } else {
                                                                                                            alertify.alert("Check Sum Percent. Percent is " + data + " % from this account");
                                                                                                        }
                                                                                                    }
                                                                                                });
                                                                                            }
                                                                                            else {
                                                                                                alertify.alert("Check Debit Credit Position. Bank is Minus");
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
                                                                        } else {
                                                                            alertify.alert("Add Cashier Receipt Cancel,Please Choose old CashierID");
                                                                        }
                                                                    });
                                                                }, 1000);
                                                            }
                                                            //Cek CashierID bila manual, Cek ValueDate , Cek CashRef                      
                                                            else {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/Cashier/AddCashierValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#DebitCashRefPK").val() + "/" + $("#CashierID").val().split('/').join('-') + "/CR",
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data == 1) {
                                                                            var _ref = $("#CashierID").val();
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/Cashier/GetBankBalanceByCashierID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/CR/" + $("#PeriodPK").data("kendoComboBox").value(),
                                                                                type: 'GET',
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    var _potAmount = 0;
                                                                                    var _percentAmount = 0;
                                                                                    _percentAmount = $('#PercentAmount').val();
                                                                                    var _creditAccountPK = $('#CreditAccountPK').val();
                                                                                    if ($('#DebitCredit').val() == 'D') {
                                                                                        _potAmount = $('#BaseDebit').val() * -1
                                                                                    } else {
                                                                                        _potAmount = $('#BaseDebit').val()
                                                                                    }

                                                                                    if ((parseFloat(data + _potAmount)) > 0) {
                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/Cashier/GetSumPercentAmountByCashierIDByAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _ref + "/" + _creditAccountPK + "/" + _percentAmount + "/CR/" + $("#PeriodPK").data("kendoComboBox").value(),
                                                                                            type: 'GET',
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                if ((data) <= 100 || $('#CheckPercentage').is(":checked") == true) {
                                                                                                    alertify.alert("Percent Amount This Account : " + data + " %");
                                                                                                    var Cashier = {
                                                                                                        PeriodPK: $('#PeriodPK').val(),
                                                                                                        ValueDate: $('#ValueDate').val(),
                                                                                                        Type: $('#Type').val(),
                                                                                                        Reference: $("#Reference").val(),
                                                                                                        CashierID: $('#CashierID').val(),
                                                                                                        Description: $('#Description').val(),
                                                                                                        DebitCredit: $('#DebitCredit').val(),
                                                                                                        DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                                                        CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                                                        DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                                                        CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                                                        DebitAccountPK: $('#DebitAccountPK').val(),
                                                                                                        CreditAccountPK: $('#CreditAccountPK').val(),
                                                                                                        Debit: GlobDebit,
                                                                                                        Credit: $('#FinalAmount').val(),
                                                                                                        PercentAmount: $('#PercentAmount').val(),
                                                                                                        FinalAmount: GlobDebit,
                                                                                                        DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                                                        CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                                                        BaseDebit: $('#BaseDebit').val(),
                                                                                                        BaseCredit: $('#BaseCredit').val(),
                                                                                                        OfficePK: $('#OfficePK').val(),
                                                                                                        DepartmentPK: $('#DepartmentPK').val(),
                                                                                                        CounterpartPK: $('#CounterpartPK').val(),
                                                                                                        AgentPK: $('#AgentPK').val(),
                                                                                                        ConsigneePK: $('#ConsigneePK').val(),
                                                                                                        InstrumentPK: $('#InstrumentPK').val(),
                                                                                                        DocRef: $('#DocRef').val(),
                                                                                                        NoReference: $('#NoReference').val(),
                                                                                                        ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                                                    };
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_I",
                                                                                                        type: 'POST',
                                                                                                        data: JSON.stringify(Cashier),
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            alertify.alert(data.Message);
                                                                                                            $("#CashierPK").val(data.CashierPK);
                                                                                                            $("#HistoryPK").val(data.HistoryPK);
                                                                                                            $("#DebitCredit").data("kendoComboBox").value('C');
                                                                                                            $("#DebitCredit").data("kendoComboBox").text('Credit');
                                                                                                            refresh();
                                                                                                            clearDataWhenAdd();
                                                                                                            $("#detailRef").show();
                                                                                                            $("#gridCashierIDDetail").empty();
                                                                                                            initCashierIDDetail();
                                                                                                        },
                                                                                                        error: function (data) {
                                                                                                            alertify.alert(data.responseText);
                                                                                                        }
                                                                                                    });
                                                                                                } else {
                                                                                                    alertify.alert("Check Sum Percent. there were " + data + " % for this account, ADD CANCEL");
                                                                                                }
                                                                                            }
                                                                                        });
                                                                                    }
                                                                                    else {
                                                                                        alertify.alert("Check Debit Credit Position. Bank is Minus");
                                                                                    }

                                                                                },
                                                                                error: function (data) {
                                                                                    alertify.alert(data.responseText);
                                                                                }
                                                                            });

                                                                        }

                                                                        else {
                                                                            alertify.alert("Value Date / Cash Ref Must Be Equal with Same CashierID or Input Data Not Correct in Same CashierID");

                                                                        }


                                                                    }
                                                                });

                                                            }

                                                        }

                                                    });
                                                }
                                            }
                                            else {
                                                alertify.alert("Month & Period in Value Date Must be Same With Finance Setup")
                                            }
                                        }
                                        else {
                                            alertify.alert("Please choose cost center for Account Income/Expense");
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        }
                        //end add
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
        

        

    });


    function validate(_data) {

        if (_GlobClientCode != "12") {
            
                if (_data.Type > 2 && ($("#DepartmentPK").val() == null || $("#DepartmentPK").val() == '')) {
                    return 1;
                }
                else {
                    return 0;
                }

            } else {
            return 0;
        }
    }



    $("#BtnUpdate").click(function () {

        //GetJournalMoth
        $.ajax({
            url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalMonthOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                GlobOpenMonth = data;

                //GetJournalPeriod
                $.ajax({
                    url: window.location.origin + "/Radsoft/FinanceSetup/GetJournalPeriodOpen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        GlobOpenPeriod = data;

                        //update
                        if ($("#ValueDate").data("kendoDatePicker").value().getMonth().toString() == GlobOpenMonth - 1) {
                            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                                alertify.alert("Cashier Already Posted / Revised, Update Cancel");
                            }
                            else {
                                var val = validateData();
                                if (val == 1) {

                                    alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                                        if (e) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashierPK").val() + "/" + $("#HistoryPK").val() + "/" + "Cashier",
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                                        if (_GlobClientCode == "01") {
                                                            var Cashier = {
                                                                CashierPK: $('#CashierPK').val(),
                                                                HistoryPK: $('#HistoryPK').val(),
                                                                PeriodPK: $('#PeriodPK').val(),
                                                                ValueDate: $('#ValueDate').val(),
                                                                Type: $('#Type').val(),
                                                                //CashierID: $('#CashierID').val(),
                                                                Reference: $('#Reference').val(),
                                                                DebitCredit: $('#DebitCredit').val(),
                                                                Description: $('#Description').val(),
                                                                DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                DebitAccountPK: $('#DebitAccountPK').val(),
                                                                CreditAccountPK: $('#CreditAccountPK').val(),
                                                                Debit: $('#FinalAmount').val(),
                                                                Credit: $('#FinalAmount').val(),
                                                                PercentAmount: $('#PercentAmount').val(),
                                                                FinalAmount: $('#FinalAmount').val(),
                                                                DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                BaseDebit: $('#BaseDebit').val(),
                                                                BaseCredit: $('#BaseCredit').val(),
                                                                OfficePK: $('#OfficePK').val(),
                                                                DepartmentPK: $('#DepartmentPK').val(),
                                                                CounterpartPK: $('#CounterpartPK').val(),
                                                                AgentPK: $('#AgentPK').val(),
                                                                ConsigneePK: $('#ConsigneePK').val(),
                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                DocRef: $('#DocRef').val(),
                                                                NoReference: $('#NoReference').val(),
                                                                ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                Notes: str,
                                                                EntryUsersID: sessionStorage.getItem("user")
                                                            };
                                                        }
                                                        else {
                                                            var Cashier = {
                                                                CashierPK: $('#CashierPK').val(),
                                                                HistoryPK: $('#HistoryPK').val(),
                                                                PeriodPK: $('#PeriodPK').val(),
                                                                ValueDate: $('#ValueDate').val(),
                                                                Type: $('#Type').val(),
                                                                CashierID: $('#CashierID').val(),
                                                                Reference: $('#Reference').val(),
                                                                DebitCredit: $('#DebitCredit').val(),
                                                                Description: $('#Description').val(),
                                                                DebitCurrencyPK: $('#DebitCurrencyPK').val(),
                                                                CreditCurrencyPK: $('#CreditCurrencyPK').val(),
                                                                DebitCashRefPK: $('#DebitCashRefPK').val(),
                                                                CreditCashRefPK: $('#CreditCashRefPK').val(),
                                                                DebitAccountPK: $('#DebitAccountPK').val(),
                                                                CreditAccountPK: $('#CreditAccountPK').val(),
                                                                Debit: $('#FinalAmount').val(),
                                                                Credit: $('#FinalAmount').val(),
                                                                PercentAmount: $('#PercentAmount').val(),
                                                                FinalAmount: $('#FinalAmount').val(),
                                                                DebitCurrencyRate: $('#DebitCurrencyRate').val(),
                                                                CreditCurrencyRate: $('#CreditCurrencyRate').val(),
                                                                BaseDebit: $('#BaseDebit').val(),
                                                                BaseCredit: $('#BaseCredit').val(),
                                                                OfficePK: $('#OfficePK').val(),
                                                                DepartmentPK: $('#DepartmentPK').val(),
                                                                CounterpartPK: $('#CounterpartPK').val(),
                                                                AgentPK: $('#AgentPK').val(),
                                                                ConsigneePK: $('#ConsigneePK').val(),
                                                                InstrumentPK: $('#InstrumentPK').val(),
                                                                DocRef: $('#DocRef').val(),
                                                                NoReference: $('#NoReference').val(),
                                                                ItemBudgetPK: $('#ItemBudgetPK').val(),
                                                                Notes: str,
                                                                EntryUsersID: sessionStorage.getItem("user")
                                                            };
                                                        }

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_U",
                                                            type: 'POST',
                                                            data: JSON.stringify(Cashier),
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
                            }
                        }
                        else {
                            alertify.alert("Month in Value Date Must be Same With Finance Setup")
                        }
                        //end
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
        
    });

    $("#BtnApproved").click(function () {

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashierPK").val() + "/" + $("#HistoryPK").val() + "/" + "Cashier",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Cashier = {
                                CashierPK: $('#CashierPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_A",
                                type: 'POST',
                                data: JSON.stringify(Cashier),
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
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("Cashier Already Posted / Revised, Void Cancel");
        }
        else {

            alertify.confirm("Are you sure want to Void data?", function (e) {
                if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Cashier/Cashier_ValidateOtherTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CashierPK').val() + "/CR",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == "") {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashierPK").val() + "/" + $("#HistoryPK").val() + "/" + "Cashier",
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                                        var Cashier = {
                                                            CashierPK: $('#CashierPK').val(),
                                                            HistoryPK: $('#HistoryPK').val(),
                                                            VoidUsersID: sessionStorage.getItem("user")
                                                        };
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_V",
                                                            type: 'POST',
                                                            data: JSON.stringify(Cashier),
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
                                        } else {
                                            alertify.alert(data);
                                        }
                                    }
                                });

                }
            });
        }
    });

    $("#BtnReject").click(function () {
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("Cashier Already Posted / Revised, Reject Cancel");
        }
        else {
            alertify.confirm("Are you sure want to Reject data?", function (e) {
                if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashierPK").val() + "/" + $("#HistoryPK").val() + "/" + "Cashier",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                            var Cashier = {
                                                CashierPK: $('#CashierPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                VoidUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Cashier/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashierReceipt_R",
                                                type: 'POST',
                                                data: JSON.stringify(Cashier),
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


    function onWinPostingReceiptClose() {
        $("#LblCashierIDFrom").hide();
        $("#LblCashierIDTo").hide();
        $("#LblValueDateFrom").hide();
        $("#LblValueDateTo").hide();
        $('#CashierIDFrom').val(""),
        $('#CashierIDTo').val(""),
        $('#Posting').val("")
    }

    //CashierID Detail
    function getDataSourceCashierIDDetail() {
        var _status;

        if ($("#StatusHeader").val() == "APPROVED") {
            _status = 2;
        } else {
            _status = 1;
        }

        var _urlRef = "";
        if (_GlobClientCode == "04") {
            _urlRef = "/Radsoft/CustomClient04/GetCashierIDDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _status + "/" + $('#CashierID').val() + "/" + $("#PeriodPK").val() + "/CR";
        } else {
            _urlRef = "/Radsoft/Cashier/GetCashierIDDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CashierID').val() + "/" + $("#PeriodPK").val() + "/CR";
        }
        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + _urlRef,
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 100,
                      schema: {
                          model: {
                              fields: {
                                  CashierID: { type: "string" },
                                  ValueDate: { type: "date" },
                                  Debit: { type: "number" },
                                  Credit: { type: "number" },
                                  AccountID: { type: "string" },
                                  AccountName: { type: "string" },
                                  Description: { type: "string" },
                                  DepartmentID: { type: "string" }

                              }
                          }
                      }
                  });
    }

    //function initCashierIDDetail() {
    //    var dsListCashierID = getDataSourceCashierIDDetail();
    //    $("#gridCashierIDDetail").kendoGrid({
    //        dataSource: dsListCashierID,
    //        height: 300,
    //        scrollable: {
    //            virtual: true
    //        },
    //        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
    //        columnMenu: false,
    //        reorderable: true,
    //        sortable: true,
    //        resizable: true,
    //        columns: [
    //           { field: "ValueDate", title: "ValueDate", format: "{0:dd/MMM/yyyy}", width: 120, locked: true, lockable: false},
    //           { field: "AccountID", title: "AccountID", width: 120, locked: true, lockable: false },
    //           { field: "AccountName", title: "AccountName", width: 150, locked: true, lockable: false }, 
    //           { field: "Debit", title: "Debit", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 150 },
    //           { field: "Credit", title: "Credit", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 150 },
    //           { field: "Description", title: "Description", width: 200 },
    //           { field: "DepartmentID", title: "DepartmentID", width: 150 }
    //        ]
    //    });
    //}

    function initCashierIDDetail() {
        var dsListCashierID = getDataSourceCashierIDDetail();

        if (_GlobClientCode == "12") {
            $("#gridCashierIDDetail").kendoGrid({
                dataSource: dsListCashierID,
                height: 300,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                dataBound: gridCashierIDDataBound,
                columns: [
                   { field: "ValueDate", title: "ValueDate", format: "{0:dd/MMM/yyyy}", width: 120 },
                   { field: "AccountID", title: "AccountID", width: 120 },
                   { field: "AccountName", title: "AccountName", width: 150 },
                   { field: "Debit", title: "Debit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "Credit", title: "Credit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
                   //{ field: "Description", title: "Description", width: 200 },
                   //{ field: "DepartmentID", title: "DepartmentID", width: 120 }
                ]
            });
        }
        else
            $("#gridCashierIDDetail").kendoGrid({
                dataSource: dsListCashierID,
                height: 300,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                dataBound: gridCashierIDDataBound,
                columns: [
                   { field: "ValueDate", title: "ValueDate", format: "{0:dd/MMM/yyyy}", width: 120 },
                   { field: "AccountID", title: "AccountID", width: 120 },
                   { field: "AccountName", title: "AccountName", width: 150 },
                   { field: "Debit", title: "Debit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "Credit", title: "Credit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
                   //{ field: "Description", title: "Description", width: 200 },
                   //{ field: "DepartmentID", title: "DepartmentID", width: 120 }
                ]
            });
    }

    // Untuk List CashierID

    function getDataSourceListCashierID() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/Cashier/CashierIDSelectFromCashierByType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CR" + "/" + $("#PeriodPK").data("kendoComboBox").value(),
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
                                  CashierID: { type: "string" },
                                  Reference: { type: "string" }
                              }
                          }
                      }
                  });
    }

    function initListCashierID() {
        var dsListCashierID = getDataSourceListCashierID();
        $("#gridListCashierID").kendoGrid({
            dataSource: dsListCashierID,
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
               { command: { text: "Select", click: ListCashierIDSelect }, title: " ", width: 100 },
               { field: "CashierID", title: "CashierID", width: 100 },
               { field: "Reference", title: "Reference", width: 100 }
            ]
        });
    }

    function onWinListCashierIDClose() {
        $("#gridListCashierID").empty();
    }

    function ListCashierIDSelect(e) {
        var grid = $("#gridListCashierID").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCashierID).val(dataItemX.CashierID);
        $(htmlReference).val(dataItemX.Reference);
        //$("#CashierID").val(dataItemX.CashierID);
        WinListCashierID.close();

        $("#detailRef").show();
        $("#gridCashierIDDetail").empty();
        initCashierIDDetail();

    }

    $("#btnListCashierID").click(function () {
        WinListCashierID.center();
        WinListCashierID.open();
        initListCashierID();
        htmlCashierID = "#CashierID";
        htmlReference = "#Reference";
    });

    function GetCashierCashierID() {

        //CashierID
        $.ajax({
            url: window.location.origin + "/Radsoft/Cashier/GetCashierIDComboByCashierType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/IN",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashierIDFrom").kendoComboBox({
                    dataValueField: "CashierID",
                    dataTextField: "CashierID",
                    dataSource: data,
                    change: OnChangeCashierIDFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#CashierIDTo").kendoComboBox({
                    dataValueField: "CashierID",
                    dataTextField: "CashierID",
                    dataSource: data,
                    change: OnChangeCashierIDTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCashierIDFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function OnChangeCashierIDTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
    }

    //Reference Detail
    function getDataSourceReferenceDetail() {
        var _status;

        if ($("#StatusHeader").val() == "APPROVED") {
            _status = 2;
        } else {
            _status = 1;
        }
        var _urlRef = "";
        if (_GlobClientCode == "04") {
            _urlRef = "/Radsoft/CustomClient04/GetReferenceDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _status + "/" + $('#Reference').val() + "/CR";
        } else {
            _urlRef = "/Radsoft/Cashier/GetReferenceDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _status + "/" + $('#Reference').val() + "/CR";
        }
        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + _urlRef,
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 100,
                      schema: {
                          model: {
                              fields: {
                                  Reference: { type: "string" },
                                  ValueDate: { type: "date" },
                                  Debit: { type: "number" },
                                  Credit: { type: "number" },
                                  AccountID: { type: "string" },
                                  AccountName: { type: "string" },
                                  Description: { type: "string" },
                                  DepartmentID: { type: "string" }

                              }
                          }
                      }
                  });
    }

    function initReferenceDetail() {
        var dsListReference = getDataSourceReferenceDetail();
        $("#gridReferenceDetail").kendoGrid({
            dataSource: dsListReference,
            height: 600,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [
               { field: "ValueDate", title: "ValueDate", format: "{0:dd/MMM/yyyy}", width: 120 },
               { field: "AccountID", title: "AccountID", width: 120 },
               { field: "AccountName", title: "AccountName", width: 150 },
               { field: "Debit", title: "Debit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
               { field: "Credit", title: "Credit", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" }, width: 120 },
               //{ field: "Description", title: "Description", width: 200 },
               //{ field: "DepartmentID", title: "DepartmentID", width: 120 }
            ]
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

                                          url: window.location.origin + "/Radsoft/Cashier/ReferenceSelectFromCashierByType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CR" + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#PeriodPK").data("kendoComboBox").value(),
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
        var grid = $("#gridListReference").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlReference).val(dataItemX.Reference);
        //$("#Reference").val(dataItemX.Reference);
        WinListReference.close();

        $("#detailRef").show();
        $("#gridReferenceDetail").empty();
        initReferenceDetail();


    }

    $("#btnListReference").click(function () {
        WinListReference.center();
        WinListReference.open();
        initListReference();
        htmlReference = "#Reference";
    });

    function GetCashierReference() {

        //Reference
        $.ajax({
            url: window.location.origin + "/Radsoft/Cashier/GetReferenceComboByCashierType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/IN",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReferenceFrom").kendoComboBox({
                    dataValueField: "Reference",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#ReferenceTo").kendoComboBox({
                    dataValueField: "Reference",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeReferenceFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function OnChangeReferenceTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
    }

    $("#BtnApproveBySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCashierPaymentSelected = '';

        for (var i in ArrayFundFrom) {
            stringCashierPaymentSelected = stringCashierPaymentSelected + ArrayFundFrom[i] + ',';

        }
        stringCashierPaymentSelected = stringCashierPaymentSelected.substring(0, stringCashierPaymentSelected.length - 1)


        if (stringCashierPaymentSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {

                var CashierPayment = {
                    stringCashierPaymentSelected: stringCashierPaymentSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Cashier/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
                    type: 'POST',
                    data: JSON.stringify(CashierPayment),
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

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCashierPaymentSelected = '';

        for (var i in ArrayFundFrom) {
            stringCashierPaymentSelected = stringCashierPaymentSelected + ArrayFundFrom[i] + ',';

        }
        stringCashierPaymentSelected = stringCashierPaymentSelected.substring(0, stringCashierPaymentSelected.length - 1)


        if (stringCashierPaymentSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                var CashierPayment = {
                    stringCashierPaymentSelected: stringCashierPaymentSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Cashier/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
                    type: 'POST',
                    data: JSON.stringify(CashierPayment),
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

        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCashierPaymentSelected = '';

        for (var i in ArrayFundFrom) {
            stringCashierPaymentSelected = stringCashierPaymentSelected + ArrayFundFrom[i] + ',';

        }
        stringCashierPaymentSelected = stringCashierPaymentSelected.substring(0, stringCashierPaymentSelected.length - 1)


        if (stringCashierPaymentSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {

                var CashierPayment = {
                    stringCashierPaymentSelected: stringCashierPaymentSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Cashier/Cashier_ValidateOtherTransactionBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CR",
                    type: 'POST',
                    data: JSON.stringify(CashierPayment),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
                                type: 'POST',
                                data: JSON.stringify(CashierPayment),
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
                        else {
                            alertify.alert(data);
                        }
                    }
                });
                
            }
        });
    });



    $("#BtnPostingBySelected").click(function () {

        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCashierPaymentSelected = '';

        for (var i in ArrayFundFrom) {
            stringCashierPaymentSelected = stringCashierPaymentSelected + ArrayFundFrom[i] + ',';

        }
        stringCashierPaymentSelected = stringCashierPaymentSelected.substring(0, stringCashierPaymentSelected.length - 1)


        if (stringCashierPaymentSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {

                var _urlValidate = "";
                //if (_GlobClientCode == "01") {
                //    _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingBySelectedCashierByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR";
                //} else {
                //    _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingCashier/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR";
                //}

                var CashierPayment = {
                    stringCashierPaymentSelected: stringCashierPaymentSelected,
                };

                _urlValidate = window.location.origin + "/Radsoft/Cashier/ValidateCheckPostingBySelectedCashierByReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR";

                    $.ajax({
                        url: _urlValidate,
                        type: 'POST',
                        data: JSON.stringify(CashierPayment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                if (_GlobClientCode == "12") {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Cashier/ValidateCheckStatusPosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CR",
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == "") {
                                                PostingBySelected();
                                            } else {
                                                alertify.alert(data);
                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });
                                }
                                else {
                                    PostingBySelected();
                                }
                                
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

    function PostingBySelected() {

        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCashierPaymentSelected = '';

        for (var i in ArrayFundFrom) {
            stringCashierPaymentSelected = stringCashierPaymentSelected + ArrayFundFrom[i] + ',';

        }
        stringCashierPaymentSelected = stringCashierPaymentSelected.substring(0, stringCashierPaymentSelected.length - 1)

        var CashierPayment = {
            stringCashierPaymentSelected: stringCashierPaymentSelected,
        };

        $.ajax({
            url: window.location.origin + "/Radsoft/Cashier/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
            type: 'POST',
            data: JSON.stringify(CashierPayment),
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

    $("#BtnReviseBySelected").click(function () {

        alertify.confirm("Are you sure want to Revise by Selected Data ?", function (e) {
            if (e) { 
                $.ajax({
                    url: window.location.origin + "/Radsoft/Cashier/Cashier_ValidateOtherTransactionBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CR",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Cashier/ReviseBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CR",
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
                        else {
                            alertify.alert(data);
                        }
                    }

                });
                
            }
        });
    });

    function gridCashierIDDataBound() {
        var grid = $("#gridCashierIDDetail").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AccountName == "TOTAL : ") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSEquityByInstrumentYellow");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            }
        });
    }



    function recalCurrencyRate() {

        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitCashRefPK").data("kendoComboBox").value(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var LastRate = {
                    Date: $('#ValueDate').val(),
                    CurrencyPK: data.CurrencyPK,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(LastRate),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        _debitCurrencyRate = data;
                        $("#DebitCurrencyRate").data("kendoNumericTextBox").value(data);

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccountPK").data("kendoComboBox").value(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#CreditCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                                var LastRate = {
                                    Date: $('#ValueDate').val(),
                                    CurrencyPK: data.CurrencyPK,
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(LastRate),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        _creditCurrencyRate = data;
                                        $("#CreditCurrencyRate").data("kendoNumericTextBox").value(data);


                                        GlobDebit = 0;
                                        var _finalAmount = $("#Amount").data("kendoNumericTextBox").value() * ($("#PercentAmount").data("kendoNumericTextBox").value() / 100);
                                        var _baseCredit = _finalAmount * _creditCurrencyRate;
                                        $("#BaseCredit").data("kendoNumericTextBox").value(_baseCredit);
                                        $("#BaseDebit").data("kendoNumericTextBox").value(_baseCredit);
                                        $("#FinalAmount").data("kendoNumericTextBox").value(_finalAmount);
                                        GlobDebit = _baseCredit / _debitCurrencyRate;
                                   
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
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });





    }

});