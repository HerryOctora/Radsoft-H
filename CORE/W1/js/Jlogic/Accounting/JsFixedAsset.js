$(document).ready(function () {
    var win;
    var tabindex;
    var winOldData;
    var winAllocate;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobStatus;
    //1
    initButton();
    //2
    initWindow();
    //3
    refresh();

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
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPosting").kendoButton({
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

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnAddAllocation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnRefreshAllocation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnShowDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnFAReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
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
        $("#BtnSell").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

    }
    
     
    function initWindow() {
        $("#BuyValueDate").kendoDatePicker({
            change: OnChangeBuyValueDate
        });
        $("#SellValueDate").kendoDatePicker({});
        $("#IntervalSpecificDate").kendoDatePicker({});


        $("#DateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateTo
        });


        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ValueDateTo").data("kendoDatePicker").value();
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

        function OnChangeBuyValueDate() {
            if ($("#BuyValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#BuyValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
        }


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

        winAllocate = $("#WinAllocate").kendoWindow({
            height: "200px",
            title: "Allocate To Department",
            visible: false,
            width: "600px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: CloseWinAllocate
        }).data("kendoWindow");

        win = $("#WinFixedAsset").kendoWindow({
            height: "1600px",
            title: "Fixed Asset Detail",
            visible: false,
            width: "1200px",
            modal: true,
            //activate: function () {
            //    $("#ID").focus();
            //},
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinFARpt = $("#WinFARpt").kendoWindow({
            height: 250,
            title: "Fixed Assets Report",
            visible: false,
            width: 400,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinFARptClose
        }).data("kendoWindow");

    

    }


    function CloseOldData() {
        $("#OldData").text("");

    }
    var GlobValidator = $("#WinFixedAsset").kendoValidator().data("kendoValidator");
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

    var GlobValidatAllocationr = $("#WinFixedAssetAllocation").kendoValidator().data("kendoValidator");
    function validateDataAllocation() {
        
        if (GlobValidatorAllocation.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }


    var GlobValidatorFARpt = $("#WinFARpt").kendoValidator().data("kendoValidator");
    function validateDataFARpt() {

        if (GlobValidatorFARpt.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    var _defaultPeriodPK;

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

    function showDetails(e) {
        var dataItemX;
        //BUAT BUY
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnPosting").hide();
            $("#BtnRevise").hide();
            $("#TrxInformation").hide();
            $("#BtnSell").hide();
            $("#BuyFixedAsset").show();
            $("#SellFixedAsset").hide();
            $("#BtnShowDetail").hide();
            $("#gridDetailFixedAsset").hide();
            $("#BuyValueDate").data("kendoDatePicker").value(_d);
            GlobStatus = 0;
        }

        else {
            if (e.handled == true) {
                $("#BtnShowDetail").show();
                return;
            }
            e.handled = true;



            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridFixedAssetApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {

                grid = $("#gridFixedAssetPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {

                grid = $("#gridFixedAssetHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#BtnSell").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("POSTED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnVoid").hide();
                $("#BtnUnApproved").hide();
                $("#BtnRevise").show();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnAddAllocation").hide();
                $("#BtnSell").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("REVISED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").show();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnSell").hide();
                $("#BtnPosting").hide();
                $("#BtnAddAllocation").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").show();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnSell").hide();
                $("#BtnPosting").show();
                // SEKURITAS : ini harusnya di Hide aja.
                //$("#BtnAddAllocation").hide();
                
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
                $("#BtnRevise").hide();
                $("#BtnSell").hide();
                $("#BtnPosting").hide();
                $("#BtnAddAllocation").hide();
            }



            dirty = null;
            $("#FixedAssetPK").val(dataItemX.FixedAssetPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (dataItemX.BuyValueDate == '1/1/1900 12:00:00 AM') {
                $("#BuyValueDate").val("");
            } else {
                $("#BuyValueDate").data("kendoDatePicker").value(new Date(dataItemX.BuyValueDate));
            }
            $("#BuyJournalNo").val(dataItemX.BuyJournalNo);
            $("#BuyReference").val(dataItemX.BuyReference);
            $("#BuyDescription").val(dataItemX.BuyDescription);
            $("#FixedAssetAccount").val(dataItemX.FixedAssetAccount);
            $("#FixedAssetAccountID").val(dataItemX.FixedAssetAccountID);
            $("#AccountBuyCredit").val(dataItemX.AccountBuyCredit);
            $("#AccountBuyCreditID").val(dataItemX.AccountBuyCreditID);
            $("#BuyAmount").val(dataItemX.BuyAmount);
            $("#BitWithPPNBuy").val(dataItemX.BitWithPPNBuy);
            $("#BitWithPPNSell").val(dataItemX.BitWithPPNSell);
            $("#AmountPPNBuy").val(dataItemX.AmountPPNBuy);
            $("#AmountPPNSell").val(dataItemX.AmountPPNSell);
            $("#DepreciationMode").val(dataItemX.DepreciationMode);
            $("#DepreciationModeDesc").val(dataItemX.DepreciationModeDesc);
            $("#JournalInterval").val(dataItemX.JournalInterval);
            $("#JournalIntervalDesc").val(dataItemX.JournalIntervalDesc);
            if (dataItemX.IntervalSpecificDate == '1/1/1900 12:00:00 AM') {
                $("#IntervalSpecificDate").val("");
            } else {
                $("#IntervalSpecificDate").data("kendoDatePicker").value(new Date(dataItemX.IntervalSpecificDate));
            }
            $("#DepreciationPeriod").val(dataItemX.DepreciationPeriod);
            $("#PeriodUnit").val(dataItemX.PeriodUnit);
            $("#PeriodUnitDesc").val(dataItemX.PeriodUnitDesc);
            $("#DepreciationExpAccount").val(dataItemX.DepreciationExpAccount);
            $("#DepreciationExpAccountID").val(dataItemX.DepreciationExpAccountID);
            $("#AccumulatedDeprAccount").val(dataItemX.AccumulatedDeprAccount);
            $("#AccumulatedDeprAccountID").val(dataItemX.AccumulatedDeprAccountID);
            $("#OfficePK").val(dataItemX.OfficePK);
            $("#OfficeID").val(dataItemX.OfficeID);
            $("#Location").val(dataItemX.Location);
            $("#DepartmentPK").val(dataItemX.DepartmentPK);
            $("#DepartmentID").val(dataItemX.DepartmentID);
            if (dataItemX.SellValueDate == '1/1/1900 12:00:00 AM') {
                $("#SellValueDate").val("");
            } else {
                $("#SellValueDate").data("kendoDatePicker").value(new Date(dataItemX.SellValueDate));
            }
            $("#SellReference").val(dataItemX.SellReference);
            $("#SellDescription").val(dataItemX.SellDescription);
            $("#SellJournalNo").val(dataItemX.SellJournalNo);
            $("#SellAmount").val(dataItemX.SellAmount);
            $("#AccountSellDebit").val(dataItemX.AccountSellDebit);
            $("#AccountSellDebitID").val(dataItemX.AccountSellDebitID);

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
            }

            if (dataItemX.Revised == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Revised");
                $("#State").text("REVISED");
            }

            if (dataItemX.UnPosted == true) {
                $("#UnPosted").prop('checked', true);
            }

            if (dataItemX.Revised == true) {
                $("#Revised").prop('checked', true);
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

            if (e.data.commandName == "Sell") {
                $("#BuyFixedAsset").hide();
                //$("#SellFixedAsset").show();
                $("#SellValueDate").data("kendoDatePicker").value(_d);
                $("#BtnUpdate").hide();
                $("#BtnVoid").hide();
                $("#BtnSell").show();
                $("#BtnUnApproved").hide();
                $("#BtnPosting").hide();
            }

            else {
                $("#BuyFixedAsset").show();
                //$("#SellFixedAsset").show();
                //$("#BtnSell").hide();
            }

            $("#gridFixedAssetAllocation").empty();
            initGridFixedAssetAllocation();
        }

        //combo box BitWithPPNBuy//
        $("#BitWithPPNBuy").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            change: OnBitWithPPNBuy,
            value: setBitWithPPNBuy()
        });

        function OnBitWithPPNBuy() {
            $("#AmountPPNBuy").data("kendoNumericTextBox").value('');
            var _Amount, _vat;
            _Amount = $("#BuyAmount").val();
            _vat = $("#BitWithPPNBuy").val();
            if ($("#BuyAmount").val() == null || $("#BuyAmount").val() == '' || $("#BuyAmount").val() == undefined || $("#BuyAmount").val() == 0) {
                _Amount = 0;
            }
            if ($("#BitWithPPNBuy").val() == null || $("#BitWithPPNBuy").val() == '' || $("#BitWithPPNBuy").val() == undefined || $("#BitWithPPNBuy").val() == 0) {
                _vat = 0;
            }
            _vat = (_vat / 100) + 1;
            $("#AmountPPNBuy").data("kendoNumericTextBox").value(_Amount * _vat);
        }

        function setBitWithPPNBuy() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BitWithPPNBuy;
            }
        }
        //combo box BitWithPPNSell//
        $("#BitWithPPNSell").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            change: OnBitWithPPNSell,
            value: setBitWithPPNSell()
        });

        function OnBitWithPPNSell() {
            $("#AmountPPNSell").data("kendoNumericTextBox").value('');
            var _Amount, _vat;
            _Amount = $("#SellAmount").val();
            _vat = $("#BitWithPPNSell").val();
            if ($("#SellAmount").val() == null || $("#SellAmount").val() == '' || $("#SellAmount").val() == undefined || $("#SellAmount").val() == 0) {
                _Amount = 0;
            }
            if ($("#BitWithPPNSell").val() == null || $("#BitWithPPNSell").val() == '' || $("#BitWithPPNSell").val() == undefined || $("#BitWithPPNSell").val() == 0) {
                _vat = 0;
            }
            _vat = (_vat / 100) + 1;
            $("#AmountPPNSell").data("kendoNumericTextBox").value(_Amount * _vat);
        }

        function setBitWithPPNSell() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BitWithPPNSell;
            }
        }

        //combo box BitSold//
        $("#BitSold").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: onChangeBitSold,
            value: setCmbBitSold(),

        });
        function onChangeBitSold() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitSold() {

            if (e == null) {
                return "No";
            } else {
                if (dataItemX.BitSold == true) {
                    $("#BitSold").val("Yes");
                } else if (dataItemX.BitSold == false) {
                    $("#BitSold").val("No");
                }
            }
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

        //ACCOUNT

        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboAssetChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FixedAssetAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFixedAssetAccount,
                    value: setCmbFixedAssetAccount()
                });


                $("#AccumulatedDeprAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAccountPK,
                    value: setCmbAccumulatedDeprAccount()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboExpenseChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {


                $("#DepreciationExpAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAccountPK,
                    value: setCmbDepreciationExpAccount()
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        function setCmbFixedAssetAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FixedAssetAccount == 0) {
                    return "";
                } else {
                    return dataItemX.FixedAssetAccount;
                }
            }
        }
        function setCmbDepreciationExpAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepreciationExpAccount == 0) {
                    return "";
                } else {
                    return dataItemX.DepreciationExpAccount;
                }
            }
        }
        function setCmbAccumulatedDeprAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccumulatedDeprAccount == 0) {
                    return "";
                } else {
                    return dataItemX.AccumulatedDeprAccount;
                }
            }
        }
        function OnChangeAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function OnChangeFixedAssetAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FixedAssetAccount").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#BuyDebitCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#BuyValueDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BuyDebitRate").data("kendoNumericTextBox").value(data);
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

        function OnChangeFixedAssetAccountSell() {


            $.ajax({
                url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FixedAssetAccount").data("kendoComboBox").value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#SellCreditCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                    var LastRate = {
                        Date: $('#SellValueDate').val(),
                        CurrencyPK: data.CurrencyPK,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(LastRate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#SellCreditRate").data("kendoNumericTextBox").value(data);
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


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DepreciationMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepreciationMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: onChangeDepreciationMode,
                    value: setCmbDepreciationMode()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeDepreciationMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDepreciationMode() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepreciationMode == 0) {
                    return "";
                } else {
                    return dataItemX.DepreciationMode;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/JournalInterval",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JournalInterval").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: onchangeJournalInterval,
                    value: setCmbJournalInterval()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onchangeJournalInterval() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
                else
                {
                    if ($("#JournalInterval").data("kendoComboBox").text() == "SPECIFIC DATE") {
                        $("#LblIntervalSpecificDate").show();
                    }
                    else {
                        $("#IntervalSpecificDate").data("kendoDatePicker").value(null);
                        $("#LblIntervalSpecificDate").hide();
                    }
                }


        }
        function setCmbJournalInterval() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.JournalInterval == 0) {
                    return "";
                } else {
                    return dataItemX.JournalInterval;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PeriodUnit",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodUnit").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: onChangePeriodUnit,
                    value: setCmbPeriodUnit()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePeriodUnit() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPeriodUnit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PeriodUnit == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodUnit;
                }
            }
        }

        //CASH REF

        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboAssetAndLiabilitiesChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountBuyCredit").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeAccountBuyCredit,
                    filter: "contains",
                    suggest: true,
                    value: setCmbAccountBuyCredit()
                });
                $("#AccountSellDebit").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeAccountSellDebit,
                    filter: "contains",
                    suggest: true,
                    value: setCmbAccountSellDebit()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbAccountBuyCredit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountBuyCredit == 0) {
                    return "";
                } else {
                    return dataItemX.AccountBuyCredit;
                }
            }
        }
        function setCmbAccountSellDebit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountSellDebit == 0) {
                    return "";
                } else {
                    return dataItemX.AccountSellDebit;
                }
            }
        }
        function OnChangeAccountBuyCredit() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AccountBuyCredit").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#BuyCreditCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        //$("#CreditAccountPK").val(data.AccountPK);
                        var LastRate = {
                            Date: $('#BuyValueDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BuyCreditRate").data("kendoNumericTextBox").value(data);
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

        function OnChangeAccountSellDebit() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AccountSellDebit").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#SellDebitCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        //$("#CreditAccountPK").val(data.AccountPK);
                        var LastRate = {
                            Date: $('#SellValueDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#SellDebitRate").data("kendoNumericTextBox").value(data);
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

                OnChangeFixedAssetAccountSell();
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BuyDebitCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbBuyDebitCurrencyPK()
                });
                $("#BuyCreditCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbBuyCreditCurrencyPK()
                });

                $("#SellDebitCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbSellDebitCurrencyPK()
                });
                $("#SellCreditCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbSellCreditCurrencyPK()
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

        function setCmbBuyDebitCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BuyDebitCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.BuyDebitCurrencyPK;
                }
            }
        }
        function setCmbBuyCreditCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BuyCreditCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.BuyCreditCurrencyPK;
                }
            }
        }

        function setCmbSellDebitCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SellDebitCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.SellDebitCurrencyPK;
                }
            }
        }
        function setCmbSellCreditCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SellCreditCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.SellCreditCurrencyPK;
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
                    dataSource: data,
                    change: OnChangeOfficePK,
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
                return "";
            } else {
                if (dataItemX.OfficePK == 0) {
                    return "";
                } else {
                    return dataItemX.OfficePK;
                }

            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDepartmentPK,
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
                return "";
            } else {
                if (dataItemX.DepartmentPK == 0) {
                    return "";
                } else {
                    return dataItemX.DepartmentPK;
                }
            }
        }


        $("#BuyAmount").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmountBuy,
            value: setBuyAmount()
        });

        function setBuyAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BuyAmount;
            }
        }

        function OnChangeAmountBuy() {
            $("#AmountPPNBuy").data("kendoNumericTextBox").value('');
            var _Amount, _vat;
            _Amount = $("#BuyAmount").val();
            _vat = $("#BitWithPPNBuy").val();
            if ($("#BuyAmount").val() == null || $("#BuyAmount").val() == '' || $("#BuyAmount").val() == undefined || $("#BuyAmount").val() == 0) {
                _Amount = 0;
            }
            if ($("#BitWithPPNBuy").val() == null || $("#BitWithPPNBuy").val() == '' || $("#BitWithPPNBuy").val() == undefined || $("#BitWithPPNBuy").val() == 0) {
                _vat = 0;
            }
            _vat = (_vat / 100) + 1;
            $("#AmountPPNBuy").data("kendoNumericTextBox").value(_Amount * _vat);
        }

        $("#SellAmount").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmountSell,
            value: setSellAmount()
        });

        function setSellAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellAmount;
            }
        }
        function OnChangeAmountSell() {
            $("#AmountPPNSell").data("kendoNumericTextBox").value('');
            var _Amount, _vat;
            _Amount = $("#SellAmount").val();
            _vat = $("#BitWithPPNSell").val();
            if ($("#SellAmount").val() == null || $("#SellAmount").val() == '' || $("#SellAmount").val() == undefined || $("#SellAmount").val() == 0) {
                _Amount = 0;
            }
            if ($("#BitWithPPNSell").val() == null || $("#BitWithPPNSell").val() == '' || $("#BitWithPPNSell").val() == undefined || $("#BitWithPPNSell").val() == 0) {
                _vat = 0;
            }
            _vat = (_vat / 100) + 1;
            $("#AmountPPNSell").data("kendoNumericTextBox").value(_Amount * _vat);
        }

        function OnChangeAmount() {
            recalAmount();
        }

        $("#AmountPPNBuy").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setAmountPPNBuy()
        });

        function setAmountPPNBuy() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountPPNBuy;
            }
        }

        $("#AmountPPNSell").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setAmountPPNSell()
        });

        function setAmountPPNSell() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountPPNSell;
            }
        }

        $("#BuyDebitRate").kendoNumericTextBox({
            format: "n4",
            value: setBuyDebitRate()
        });

        function setBuyDebitRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BuyDebitRate;
            }
        }

        $("#BuyCreditRate").kendoNumericTextBox({
            format: "n4",
            value: setBuyCreditRate()
        });

        function setBuyCreditRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BuyCreditRate;
            }
        }

        $("#SellDebitRate").kendoNumericTextBox({
            format: "n4",
            value: setSellDebitRate()
        });

        function setSellDebitRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellDebitRate;
            }
        }

        $("#SellCreditRate").kendoNumericTextBox({
            format: "n4",
            value: setSellCreditRate()
        });

        function setSellCreditRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellCreditRate;
            }
        }

        $("#DepreciationPeriod").kendoNumericTextBox({
            format: "n0",
            value: setDepreciationPeriod()
        });

        function setDepreciationPeriod() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DepreciationPeriod;
            }
        }

        $("#AllocationPercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            max:100
        });

        $("#ResiduAmount").kendoNumericTextBox({
            format: "n0",
            value: setResiduAmount()
        });

        function setResiduAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ResiduAmount;
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
                    dataSource: data,
                    change: OnChangeConsigneePK,
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
            url: window.location.origin + "/Radsoft/FixedAsset/GetTypeOfAssetsCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TypeOfAssetsPK").kendoComboBox({
                    dataValueField: "TypeOfAssetsPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeTypeOfAssetsPK,
                    value: setCmbTypeOfAssetsPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeTypeOfAssetsPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/TypeOfAssets/GetInformationTypeOfAssets/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#TypeOfAssetsPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#FixedAssetAccount").data("kendoComboBox").value(data.FixedAssetAccountPK);
                        $("#DepreciationPeriod").data("kendoNumericTextBox").value(data.DepreciationPeriod);
                        $("#PeriodUnit").data("kendoComboBox").value(data.PeriodUnit);
                        $("#DepreciationExpAccount").data("kendoComboBox").value(data.DepreciationExpAccountPK);
                        $("#AccumulatedDeprAccount").data("kendoComboBox").value(data.AccumulatedDeprAccountPK);


                        $.ajax({
                            url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + data.FixedAssetAccountPK,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#BuyDebitCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                                //$("#CreditAccountPK").val(data.AccountPK);
                                var LastRate = {
                                    Date: $('#BuyValueDate').val(),
                                    CurrencyPK: data.CurrencyPK,
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(LastRate),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#BuyDebitRate").data("kendoNumericTextBox").value(data);
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


        }
        function setCmbTypeOfAssetsPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TypeOfAssetsPK == 0) {
                    return "";
                } else {
                    return dataItemX.TypeOfAssetsPK;
                }


            }
        }


        //combo box BitWithPPNBuy//
        $("#VATPercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            change: OnVATPercent,
            value: setVATPercent()
        });

        function OnVATPercent() {
            $("#AmountPPNBuy").data("kendoNumericTextBox").value('');
            var _Amount, _vat;
            _Amount = $("#BuyAmount").val();
            _vat = $("#VATPercent").val();
            if ($("#BuyAmount").val() == null || $("#BuyAmount").val() == '' || $("#BuyAmount").val() == undefined || $("#BuyAmount").val() == 0) {
                _Amount = 0;
            }
            if ($("#VATPercent").val() == null || $("#VATPercent").val() == '' || $("#VATPercent").val() == undefined || $("#VATPercent").val() == 0) {
                _vat = 0;
            }
            _vat = (_vat / 100) + 1;
            $("#AmountPPNBuy").data("kendoNumericTextBox").value(_Amount * _vat);
        }

        function setVATPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.VATPercent;
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
   
    function clearData() {
        $("#FixedAssetPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#BuyValueDate").data("kendoDatePicker").value(null);
        $("#BuyJournalNo").val("");
        
        
        $("#BuyReference").val("");
        $("#BuyDescription").val("");
        
        $("#FixedAssetAccount").val("");
        $("#AccountBuyCredit").val("");
        $("#BuyAmount").val("");
        $("#BitWithPPNBuy").val("");
        $("#BitWithPPNSell").val("");
        $("#AmountPPNBuy").val("");
        $("#AmountPPNSell").val("");
        $("#DepreciationMode").val("");
        $("#JournalInterval").val("");
        $("#IntervalSpecificDate").data("kendoDatePicker").value(null);
        $("#DepreciationPeriod").val("");
        $("#PeriodUnit").val("");
        $("#DepreciationExpAccount").val("");
        $("#AccumulatedDeprAccount").val("");
        $("#OfficePK").val("");  
        $("#SellValueDate").data("kendoDatePicker").value(null);
        $("#SellReference").val("");
        $("#SellDescription").val("");
        $("#SellJournalNo").val("");
        $("#SellAmount").val("");
        $("#AccountSellDebit").val("");
        $("#BitSold").val("");
        $("#BuyDebitCurrencyPK").val("");
        $("#BuyCreditCurrencyPK").val("");
        $("#BuyDebitRate").val("");
        $("#BuyCreditRate").val("");
        $("#SellDebitCurrencyPK").val("");
        $("#SellCreditCurrencyPK").val("");
        $("#SellDebitRate").val("");
        $("#SellCreditRate").val("");
        $("#DepartmentPK").val("");
        $("#TypeOfAssetsPK").val("");
        $("#Location").val("");
        $("#ConsigneePK").val("");
        $("#ResiduAmount").val("");
        $("#VATPercent").val("");
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

    function CloseWinAllocate()
    {
        $("#AllocationPercent").data("kendoNumericTextBox").value(null);
        $("#DepartmentPK").data("kendoComboBox").value(null);

    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        $("#BtnAddAllocation").show();

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
                     alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                     this.cancelChanges();
                 },
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             FixedAssetPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             BuyValueDate: { type: "string" },
                             BuyJournalNo: { type: "number" },
                             BuyReference: { type: "string" },
                             BuyDescription: { type: "string" },
                             FixedAssetAccount: { type: "number" },
                             FixedAssetAccountID: { type: "string" },
                             AccountBuyCredit: { type: "number" },
                             AccountBuyCreditID: { type: "string" },
                             BuyAmount: { type: "number" },
                             BitWithPPNBuy: { type: "number" },
                             BitWithPPNSell: { type: "number" },
                             AmountPPNBuy: { type: "number" },
                             AmountPPNSell: { type: "number" },
                             DepreciationMode: { type: "number" },
                             DepreciationModeDesc: { type: "string" },
                             JournalInterval: { type: "number" },
                             JournalIntervalDesc: { type: "string" },
                             IntervalSpecificDate: { type: "string" },
                             DepreciationPeriod: { type: "number" },
                             PeriodUnit: { type: "string" },
                             PeriodUnitDesc: { type: "string" },
                             DepreciationExpAccount: { type: "number" },
                             DepreciationExpAccountID: { type: "string" },
                             AccumulatedDeprAccount: { type: "number" },
                             AccumulatedDeprAccountID: { type: "string" },
                             OfficePK: { type: "number" },
                             OfficeID: { type: "string" },
                             DepartmentPK: { type: "number" },
                             DepartmentID: { type: "string" },
                             SellValueDate: { type: "string" },
                             SellReference: { type: "string" },
                             SellDescription: { type: "string" },
                             SellJournalNo: { type: "number" },
                             SellAmount: { type: "number" },
                             AccountSellDebit: { type: "number" },
                             AccountSellDebitID: { type: "string" },
                             BitSold: { type: "boolean" },
                             BuyDebitCurrencyPK: { type: "number" },
                             BuyDebitCurrencyID: { type: "string" },
                             BuyCreditCurrencyPK: { type: "number" },
                             BuyCreditCurrencyID: { type: "string" },
                             BuyDebitRate: { type: "number" },
                             BuyCreditRate: { type: "number" },
                             SellDebitCurrencyPK: { type: "number" },
                             SellDebitCurrencyID: { type: "string" },
                             SellCreditCurrencyPK: { type: "number" },
                             SellCreditCurrencyID: { type: "string" },
                             SellDebitRate: { type: "number" },
                             SellCreditRate: { type: "number" },
                             ConsigneePK: { type: "number" },
                             ConsigneeID: { type: "string" },
                             TypeOfAssetsPK: { type: "number" },
                             TypeOfAssetsID: { type: "string" },
                             ResiduAmount: { type: "number" },
                             Location: { type: "string" },
                             VATPercent: { type: "number" },
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


    function gridApprovedOnDataBound() {
        var grid = $("#gridFixedAssetApproved").data("kendoGrid");
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

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
            //var gridApproved = $("#gridFixedAssetApproved").data("kendoGrid");
            // gridApproved.dataSource.read();

        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridFixedAssetPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridFixedAssetHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }

    }
    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
            //var gridApproved = $("#gridFixedAssetApproved").data("kendoGrid");
            // gridApproved.dataSource.read();

        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridFixedAssetPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridFixedAssetHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }

    }
    function initGrid() {
        $("#gridFixedAssetApproved").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var FixedAssetApprovedURL = window.location.origin +"/Radsoft/FixedAsset/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(FixedAssetApprovedURL);
        }

        //var FixedAssetApprovedURL = window.location.origin +"/Radsoft/FixedAsset/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/CP",
        //  dataSourceApproved = getDataSource(FixedAssetApprovedURL);
        var grid = $("#gridFixedAssetApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: 450,
            scrollable: {
                virtual: true
            },
            dataBound: gridApprovedOnDataBound,
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
               { command: { text: "Detail", click: showDetails }, title: " ", width: 80 },
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
               //{ command: { text: "Sell", click: showDetails }, title: " ", width: 80 },
               //{ command: { text: "Posting", click: showPosting }, title: " ", width: 80 },
               //{ command: { text: "Revise", click: showRevise }, title: " ", width: 80 },
               { field: "FixedAssetPK", title: "SysNo.", filterable: false, width: 80 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "BuyValueDate", title: "Buy Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(BuyValueDate), 'MM/dd/yyyy')#" },
               { field: "BuyJournalNo", title: "Buy Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
               { field: "BuyReference", title: "Buy Reference", width: 170 },
               { field: "BuyDescription", title: "Buy Description", width: 200 },
               { field: "FixedAssetAccount", title: "FixedAssetAccount", hidden: true, width: 170, hidden: true },
               { field: "FixedAssetAccountID", title: "Fixed Aset Account (ID)", width: 170 },
               { field: "AccountBuyCredit", title: "AccountBuyCredit", hidden: true, width: 170, hidden: true },
               { field: "AccountBuyCreditID", title: "Buy Cash Ref (ID)", width: 170 },
               { field: "BuyAmount", title: "Buy Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "BitWithPPNBuy", title: "With PPN Buy", width: 100, hidden: true, template: "#= BitWithPPNBuy ? 'Yes' : 'No' #" },
               { field: "BitWithPPNSell", title: "With PPN Sell", width: 100, hidden: true, template: "#= BitWithPPNSell ? 'Yes' : 'No' #" },
               { field: "VATPercent", title: "VAT Percent", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AmountPPNBuy", title: "Amount PPN Buy", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AmountPPNSell", title: "Amount PPN Sell", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DepreciationMode", title: "DepreciationMode", hidden: true, width: 170, hidden: true },
               { field: "DepreciationModeDesc", title: "Depreciation Mode", width: 170 },
               { field: "JournalInterval", title: "JournalInterval", hidden: true, width: 170, hidden: true },
               { field: "JournalIntervalDesc", title: "Journal Interval", width: 170 },
               { field: "IntervalSpecificDate", title: "Interval Specific Date", width: 170, template: "#= kendo.toString(kendo.parseDate(IntervalSpecificDate), 'MM/dd/yyyy')#" },
               { field: "DepreciationPeriod", title: "Depreciation Period", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PeriodUnit", title: "PeriodUnit", hidden: true, width: 170, hidden: true },
               { field: "PeriodUnitDesc", title: "Period Unit", width: 170 },
               { field: "DepreciationExpAccount", title: "DepreciationExpAccount", hidden: true, width: 170, hidden: true },
               { field: "DepreciationExpAccountID", title: "Depreciation Exp Account (ID)", width: 250 },
               { field: "AccumulatedDeprAccount", title: "AccumulatedDeprAccount", hidden: true, width: 170, hidden: true },
               { field: "AccumulatedDeprAccountID", title: "Accumulated Depr Account (ID)", width: 250 },
               { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
               { field: "OfficeID", title: "Office (ID)", width: 170 },
               { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
               { field: "DepartmentID", title: "Department (ID)", width: 170 },
               { field: "SellValueDate", title: "Sell Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SellValueDate), 'MM/dd/yyyy')#" },
               { field: "SellJournalNo", title: "Sell Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
               { field: "SellReference", title: "Sell Reference", width: 170 },
               { field: "SellDescription", title: "Sell Description", width: 200 },
               { field: "SellAmount", title: "Sell Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AccountSellDebit", title: "AccountSellDebit", hidden: true, width: 170, hidden: true },
               { field: "AccountSellDebitID", title: "Sell Cash Ref (ID)", width: 170 },
               { field: "BitSold", title: "Sold", width: 100, template: "#= BitSold ? 'Yes' : 'No' #" },
               { field: "BuyDebitCurrencyPK", title: "BuyDebitCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "BuyDebitCurrencyID", title: "Buy Debit Currency (ID)", width: 250 },
               { field: "BuyCreditCurrencyPK", title: "BuyCreditCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "BuyCreditCurrencyID", title: "Buy Credit Currency (ID)", width: 250 },
               { field: "BuyDebitRate", title: "<div style='text-align: right'>Buy Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "BuyCreditRate", title: "<div style='text-align: right'>Buy Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "SellDebitCurrencyPK", title: "SellDebitCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "SellDebitCurrencyID", title: "Sell Debit Currency (ID)", width: 250 },
               { field: "SellCreditCurrencyPK", title: "SellCreditCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "SellCreditCurrencyID", title: "Sell Credit Currency (ID)", width: 250 },
               { field: "SellDebitRate", title: "<div style='text-align: right'>Sell Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "SellCreditRate", title: "<div style='text-align: right'>Sell Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "PostedBy ", title: "Posted By", width: 170 },
               { field: "PostedTime", title: "P. Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
               { field: "EntryUsersID", title: "Entry ID", width: 170 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
               { field: "UpdateUsersID", title: "UpdateID", width: 170 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 170 },
               { field: "ApprovedUsersID", title: "ApprovedID", width: 170 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
               { field: "VoidUsersID", title: "VoidID", width: 170 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 }
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


            var grid = $("#gridFixedAssetApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _fixedAssetPK = dataItemX.FixedAssetPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _fixedAssetPK);

        }




        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFixedAsset").kendoTabStrip({
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
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin +"/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FixedAsset/" + _a + "/" + _b,
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
            url: window.location.origin +"/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FixedAsset/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridFixedAssetPending").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var FixedAssetPendingURL = window.location.origin +"/Radsoft/FixedAsset/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(FixedAssetPendingURL);
        }
        var grid = $("#gridFixedAssetPending").kendoGrid({
            dataSource: dataSourcePending,
            height: 420,
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
            { command: { text: "Detail", click: showDetails }, title: " ", width: 80 },
            {
                field: "Selected",
                width: 50,
                template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                filterable: true,
                sortable: false,
                columnMenu: false
            },
            { field: "FixedAssetPK", title: "SysNo.", filterable: false, width: 80 },
            { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
            { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
            { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
            //{ field: "Posted", title: "Posted", width: 120 },
            //{ field: "Revised", title: "Revised", width: 120 },
            { field: "PeriodID", title: "Period ID", width: 120 },
            { field: "BuyValueDate", title: "Buy Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(BuyValueDate), 'MM/dd/yyyy')#" },
            { field: "BuyJournalNo", title: "Buy Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
            { field: "BuyReference", title: "Buy Reference", width: 170 },
            { field: "BuyDescription", title: "Buy Description", width: 200 },
            { field: "FixedAssetAccount", title: "FixedAssetAccount", hidden: true, width: 170, hidden: true },
            { field: "FixedAssetAccountID", title: "Fixed Aset Account (ID)", width: 170 },
            { field: "AccountBuyCredit", title: "AccountBuyCredit", hidden: true, width: 170, hidden: true },
            { field: "AccountBuyCreditID", title: "Buy Cash Ref (ID)", width: 170 },
            { field: "BuyAmount", title: "Buy Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "BitWithPPNBuy", title: "With PPN Buy", width: 100, hidden: true, template: "#= BitWithPPNBuy ? 'Yes' : 'No' #" },
            { field: "BitWithPPNSell", title: "With PPN Sell", width: 100, hidden: true, template: "#= BitWithPPNSell ? 'Yes' : 'No' #" },
            { field: "VATPercent", title: "VAT Percent", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AmountPPNBuy", title: "Amount PPN Buy", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AmountPPNSell", title: "Amount PPN Sell", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "DepreciationMode", title: "DepreciationMode", hidden: true, width: 170, hidden: true },
            { field: "DepreciationModeDesc", title: "Depreciation Mode", width: 170 },
            { field: "JournalInterval", title: "JournalInterval", hidden: true, width: 170, hidden: true },
            { field: "JournalIntervalDesc", title: "Journal Interval", width: 170 },
            { field: "IntervalSpecificDate", title: "Interval Specific Date", width: 170, template: "#= kendo.toString(kendo.parseDate(IntervalSpecificDate), 'MM/dd/yyyy')#" },
            { field: "DepreciationPeriod", title: "Depreciation Period", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "PeriodUnit", title: "PeriodUnit", hidden: true, width: 170, hidden: true },
            { field: "PeriodUnitDesc", title: "Period Unit", width: 170 },
            { field: "DepreciationExpAccount", title: "DepreciationExpAccount", hidden: true, width: 170, hidden: true },
            { field: "DepreciationExpAccountID", title: "Depreciation Exp Account (ID)", width: 250 },
            { field: "AccumulatedDeprAccount", title: "AccumulatedDeprAccount", hidden: true, width: 170, hidden: true },
            { field: "AccumulatedDeprAccountID", title: "Accumulated Depr Account (ID)", width: 250 },
            { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
            { field: "OfficeID", title: "Office (ID)", width: 170 },
            { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
            { field: "DepartmentID", title: "Department (ID)", width: 170 },
            { field: "SellValueDate", title: "Sell Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SellValueDate), 'MM/dd/yyyy')#" },
            { field: "SellJournalNo", title: "Sell Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
            { field: "SellReference", title: "Sell Reference", width: 170 },
            { field: "SellDescription", title: "Sell Description", width: 200 },
            { field: "SellAmount", title: "Sell Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AccountSellDebit", title: "AccountSellDebit", hidden: true, width: 170, hidden: true },
            { field: "AccountSellDebitID", title: "Sell Cash Ref (ID)", width: 170 },
            { field: "BitSold", title: "Sold", width: 100, template: "#= BitSold ? 'Yes' : 'No' #" },
            { field: "BuyDebitCurrencyPK", title: "BuyDebitCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "BuyDebitCurrencyID", title: "Buy Debit Currency (ID)", width: 250 },
            { field: "BuyCreditCurrencyPK", title: "BuyCreditCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "BuyCreditCurrencyID", title: "Buy Credit Currency (ID)", width: 250 },
            { field: "BuyDebitRate", title: "<div style='text-align: right'>Buy Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "BuyCreditRate", title: "<div style='text-align: right'>Buy Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "SellDebitCurrencyPK", title: "SellDebitCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "SellDebitCurrencyID", title: "Sell Debit Currency (ID)", width: 250 },
            { field: "SellCreditCurrencyPK", title: "SellCreditCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "SellCreditCurrencyID", title: "Sell Credit Currency (ID)", width: 250 },
            { field: "SellDebitRate", title: "<div style='text-align: right'>Sell Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "SellCreditRate", title: "<div style='text-align: right'>Sell Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "PostedBy ", title: "Posted By", width: 170 },
            { field: "PostedTime", title: "P. Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "EntryUsersID", title: "Entry ID", width: 170 },
            { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "UpdateUsersID", title: "UpdateID", width: 170 },
            { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 170 },
            { field: "ApprovedUsersID", title: "ApprovedID", width: 170 },
            { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "VoidUsersID", title: "VoidID", width: 170 },
            { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 }
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


            var grid = $("#gridFixedAssetPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _fixedAssetPK = dataItemX.FixedAssetPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _fixedAssetPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();


    }
    function RecalGridHistory() {
        $("#gridFixedAssetHistory").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var FixedAssetHistoryURL = window.location.origin +"/Radsoft/FixedAsset/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(FixedAssetHistoryURL);
        }
        $("#gridFixedAssetHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: 420,
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
            { command: { text: "Detail", click: showDetails }, title: " ", width: 80 },
            { field: "FixedAssetPK", title: "SysNo.", filterable: false, width: 80 },
            { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
            { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
            { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
            //{ field: "Posted", title: "Posted", width: 120 },
            //{ field: "Revised", title: "Revised", width: 120 },
            { field: "PeriodID", title: "Period ID", width: 120 },
            { field: "BuyValueDate", title: "Buy Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(BuyValueDate), 'MM/dd/yyyy')#" },
            { field: "BuyJournalNo", title: "Buy Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
            { field: "BuyReference", title: "Buy Reference", width: 170 },
            { field: "BuyDescription", title: "Buy Description", width: 200 },
            { field: "FixedAssetAccount", title: "FixedAssetAccount", hidden: true, width: 170, hidden: true },
            { field: "FixedAssetAccountID", title: "Fixed Aset Account (ID)", width: 170 },
            { field: "AccountBuyCredit", title: "AccountBuyCredit", hidden: true, width: 170, hidden: true },
            { field: "AccountBuyCreditID", title: "Buy Cash Ref (ID)", width: 170 },
            { field: "BuyAmount", title: "Buy Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "BitWithPPNBuy", title: "With PPN Buy", width: 100, hidden: true, template: "#= BitWithPPNBuy ? 'Yes' : 'No' #" },
            { field: "BitWithPPNSell", title: "With PPN Sell", width: 100, hidden: true, template: "#= BitWithPPNSell ? 'Yes' : 'No' #" },
            { field: "VATPercent", title: "VAT Percent", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AmountPPNBuy", title: "Amount PPN Buy", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AmountPPNSell", title: "Amount PPN Sell", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "DepreciationMode", title: "DepreciationMode", hidden: true, width: 170, hidden: true },
            { field: "DepreciationModeDesc", title: "Depreciation Mode", width: 170 },
            { field: "JournalInterval", title: "JournalInterval", hidden: true, width: 170, hidden: true },
            { field: "JournalIntervalDesc", title: "Journal Interval", width: 170 },
            { field: "IntervalSpecificDate", title: "Interval Specific Date", width: 170, template: "#= kendo.toString(kendo.parseDate(IntervalSpecificDate), 'MM/dd/yyyy')#" },
            { field: "DepreciationPeriod", title: "Depreciation Period", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "PeriodUnit", title: "PeriodUnit", hidden: true, width: 170, hidden: true },
            { field: "PeriodUnitDesc", title: "Period Unit", width: 170 },
            { field: "DepreciationExpAccount", title: "DepreciationExpAccount", hidden: true, width: 170, hidden: true },
            { field: "DepreciationExpAccountID", title: "Depreciation Exp Account (ID)", width: 250 },
            { field: "AccumulatedDeprAccount", title: "AccumulatedDeprAccount", hidden: true, width: 170, hidden: true },
            { field: "AccumulatedDeprAccountID", title: "Accumulated Depr Account (ID)", width: 250 },
            { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
            { field: "OfficeID", title: "Office (ID)", width: 170 },
            { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
            { field: "DepartmentID", title: "Department (ID)", width: 170 },
            { field: "SellValueDate", title: "Sell Value Date", width: 170, template: "#= kendo.toString(kendo.parseDate(SellValueDate), 'MM/dd/yyyy')#" },
            { field: "SellJournalNo", title: "Sell Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
            { field: "SellReference", title: "Sell Reference", width: 170 },
            { field: "SellDescription", title: "Sell Description", width: 200 },
            { field: "SellAmount", title: "Sell Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            { field: "AccountSellDebit", title: "AccountSellDebit", hidden: true, width: 170, hidden: true },
            { field: "AccountSellDebitID", title: "Sell Cash Ref (ID)", width: 170 },
            { field: "BitSold", title: "Sold", width: 100, template: "#= BitSold ? 'Yes' : 'No' #" },
            { field: "BuyDebitCurrencyPK", title: "BuyDebitCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "BuyDebitCurrencyID", title: "Buy Debit Currency (ID)", width: 250 },
            { field: "BuyCreditCurrencyPK", title: "BuyCreditCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "BuyCreditCurrencyID", title: "Buy Credit Currency (ID)", width: 250 },
            { field: "BuyDebitRate", title: "<div style='text-align: right'>Buy Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "BuyCreditRate", title: "<div style='text-align: right'>Buy Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "SellDebitCurrencyPK", title: "SellDebitCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "SellDebitCurrencyID", title: "Sell Debit Currency (ID)", width: 250 },
            { field: "SellCreditCurrencyPK", title: "SellCreditCurrencyPK", hidden: true, width: 170, hidden: true },
            { field: "SellCreditCurrencyID", title: "Sell Credit Currency (ID)", width: 250 },
            { field: "SellDebitRate", title: "<div style='text-align: right'>Sell Debit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "SellCreditRate", title: "<div style='text-align: right'>Sell Credit Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
            { field: "PostedBy ", title: "Posted By", width: 170 },
            { field: "PostedTime", title: "P. Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "EntryUsersID", title: "Entry ID", width: 170 },
            { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "UpdateUsersID", title: "UpdateID", width: 170 },
            { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 170 },
            { field: "ApprovedUsersID", title: "ApprovedID", width: 170 },
            { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "VoidUsersID", title: "VoidID", width: 170 },
            { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 },
            { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 140 }
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
    }


    //AZIZ
    function showPosting(e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
            var grid = $("#gridFixedAssetApproved").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            var pos = _dataItem.Posted;
            var _pk = _dataItem.FixedAssetPK;
            var _his = _dataItem.HistoryPK;

            if (pos == true) {
                alertify.alert("Data Already Posted");
                return;
            }
        }
        else if ($("#StatusHeader").text() == "POSTED") {
            alertify.alert("Data Already Posted");
            return;
        }

        
        alertify.confirm("Are you sure want to Posting ?", function (a) {
            if (a) {
                if (e != undefined) {
                    var Posting = {
                        FixedAssetPK: _pk,
                        HistoryPK: _his,
                        EntryUsersID: sessionStorage.getItem("user")
                    };

                }
                else {
                    var Posting = {
                        FixedAssetPK: $("#FixedAssetPK").val(),
                        HistoryPK: $("#HistoryPK").val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                }



                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_Posting",
                    type: 'POST',
                    data: JSON.stringify(Posting),
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
                alertify.alert("You've clicked Cancel");
            }
        });

    }

    //AZIZ
    function showRevise(e) {

        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
            var grid = $("#gridFixedAssetApproved").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            var rev = _dataItem.Revised;
            var _pk = _dataItem.FixedAssetPK;
            var pos = _dataItem.Posted;
            var _his = _dataItem.HistoryPK;

            if (rev == true) {
                alertify.alert("Data Already Revised");
                return;
            }
            if (pos == false) {
                alertify.alert("Data Not Posting Yet");
                return;
            }
        }
        if ($("#StatusHeader").text() == "REVISED") {
            alertify.alert("Data Already Revised");
            return;
        }

        
        alertify.confirm("Are you sure want to Revise ?", function (a) {
            if (a) {
                if (e != undefined) {
                    var Revise = {
                        FixedAssetPK: _pk,
                        HistoryPK: _his,
                        EntryUsersID: sessionStorage.getItem("user")
                    };

                }
                else {
                    var Revise = {
                        FixedAssetPK: $("#FixedAssetPK").val(),
                        HistoryPK: $("#HistoryPK").val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                }

                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_Revise",
                    type: 'POST',
                    data: JSON.stringify(Revise),
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
                alertify.alert("You've clicked Cancel");
            }

        });
    }

    function recalAmount() {
        //GlobDebit = 0;
        //var _baseCredit = $("#Amount").data("kendoNumericTextBox").value() * $("#BuyCreditRate").data("kendoNumericTextBox").value();
        //$("#BaseCredit").data("kendoNumericTextBox").value(_baseCredit);
        //$("#BaseDebit").data("kendoNumericTextBox").value(_baseCredit);
        //GlobDebit = _baseCredit / $("#BuyDebitRate").data("kendoNumericTextBox").value()
    }

    $("#BtnDownload").click(function () {
        
        alertify.confirm("Are you sure want to Download data FixedAsset ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/DownloadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    $("#BtnPosting").click(function () {
        showPosting();
    });

    $("#BtnRevise").click(function () {
        showRevise();
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

    $("#BtnSell").click(function () {

        if ($("#StatusHeader").text() == "APPROVED" || $("#StatusHeader").text() == "REVISED") {
            alertify.alert("Fixed Aset Status Approved / Revised, Sell Cancel");
        }
        else {
            alertify.confirm("Are you sure want to Sell Fixed Aset  ?", function (e) {
                if (e) {

                    var FixedAsset = {
                        FixedAssetPK: $('#FixedAssetPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        PeriodPK: $('#PeriodPK').val(),
                        SellValueDate: $('#SellValueDate').val(),
                        SellReference: $('#SellReference').val(),
                        SellDescription: $('#SellDescription').val(),
                        SellJournalNo: $('#SellJournalNo').val(),
                        SellAmount: $('#SellAmount').val(),
                        BitWithPPNSell: $('#BitWithPPNSell').val(),
                        AmountPPNSell: $('#AmountPPNSell').val(),
                        AccountSellDebit: $('#AccountSellDebit').val(),
                        BitSold: $('#BitSold').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_Sell",
                        type: 'POST',
                        data: JSON.stringify(FixedAsset),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#FixedAssetPK").val(data.FixedAssetPK);
                            $("#HistoryPK").val(data.HistoryPK);
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




    });



    //AZIZ
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add Fixed Aset  ?", function (e) {
                if (e) {

                    var FixedAsset = {
                        PeriodPK: $('#PeriodPK').val(),
                        BuyValueDate: $('#BuyValueDate').val(),
                        BuyJournalNo: $('#BuyJournalNo').val(),
                        BuyReference: $('#BuyReference').val(),
                        BuyDescription: $('#BuyDescription').val(),
                        FixedAssetAccount: $('#FixedAssetAccount').val(),
                        AccountBuyCredit: $('#AccountBuyCredit').val(),
                        CurrencyPK: $('#CurrencyPK').val(),
                        BuyAmount: $('#BuyAmount').val(),
                        BitWithPPNBuy: $('#BitWithPPNBuy').val(),
                        BitWithPPNSell: $('#BitWithPPNSell').val(),
                        AmountPPNBuy: $('#AmountPPNBuy').val(),
                        AmountPPNSell: $('#AmountPPNSell').val(),
                        DepreciationMode: $('#DepreciationMode').val(),
                        JournalInterval: $('#JournalInterval').val(),
                        IntervalSpecificDate: $('#IntervalSpecificDate').val(),
                        DepreciationPeriod: $('#DepreciationPeriod').val(),
                        PeriodUnit: $('#PeriodUnit').val(),
                        DepreciationExpAccount: $('#DepreciationExpAccount').val(),
                        AccumulatedDeprAccount: $('#AccumulatedDeprAccount').val(),
                        OfficePK: $('#OfficePK').val(),
                        DepartmentPK: $('#DepartmentPK').val(),
                        SellValueDate: $('#SellValueDate').val(),
                        SellReference: $('#SellReference').val(),
                        SellDescription: $('#SellDescription').val(),
                        SellJournalNo: $('#SellJournalNo').val(),
                        SellAmount: $('#SellAmount').val(),
                        AccountSellDebit: $('#AccountSellDebit').val(),
                        BitSold: $('#BitSold').val(),
                        BuyDebitCurrencyPK: $('#BuyDebitCurrencyPK').val(),
                        BuyCreditCurrencyPK: $('#BuyCreditCurrencyPK').val(),
                        BuyDebitRate: $('#BuyDebitRate').val(),
                        BuyCreditRate: $('#BuyCreditRate').val(),
                        SellDebitCurrencyPK: $('#SellDebitCurrencyPK').val(),
                        SellCreditCurrencyPK: $('#SellCreditCurrencyPK').val(),
                        SellDebitRate: $('#SellDebitRate').val(),
                        SellCreditRate: $('#SellCreditRate').val(),

                        DepartmentPK: $('#DepartmentPK').val(),
                        TypeOfAssetsPK: $('#TypeOfAssetsPK').val(),
                        Location: $('#Location').val(),
                        ConsigneePK: $('#ConsigneePK').val(),
                        ResiduAmount: $('#ResiduAmount').val(),
                        VATPercent: $('#VATPercent').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_I",
                        type: 'POST',
                        data: JSON.stringify(FixedAsset),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            $("#FixedAssetPK").val(data.FixedAssetPK);
                            $("#HistoryPK").val(data.HistoryPK);
                            alertify.alert(data.Message);
                            $("#BtnAdd").hide();
                            refresh();
                            //$('#gridDetailFixedAsset').data('kendoGrid').dataSource.read();
                            //$('#gridDetailFixedAsset').data('kendoGrid').refresh();
                            win.close();
                            //$("#gridFixedAssetAllocation").empty();
                            //initGridFixedAssetAllocation();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }


            });
        }

    });

    $("#BtnUpdate").click(function () {
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("Fixed Aset Already Posted / Revised, Update Cancel");
        }
        else {
            var val = validateData();
            if (val == 1) {
                
                alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FixedAssetPK").val() + "/" + $("#HistoryPK").val() + "/" + "FixedAsset",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == data) {

                                    var FixedAsset = {
                                        FixedAssetPK: $('#FixedAssetPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        PeriodPK: $('#PeriodPK').val(),
                                        BuyValueDate: $('#BuyValueDate').val(),
                                        BuyJournalNo: $('#BuyJournalNo').val(),
                                        BuyReference: $('#BuyReference').val(),
                                        BuyDescription: $('#BuyDescription').val(),
                                        FixedAssetAccount: $('#FixedAssetAccount').val(),
                                        AccountBuyCredit: $('#AccountBuyCredit').val(),
                                        CurrencyPK: $('#CurrencyPK').val(),
                                        BuyAmount: $('#BuyAmount').val(),
                                        BitWithPPNBuy: $('#BitWithPPNBuy').val(),
                                        BitWithPPNSell: $('#BitWithPPNSell').val(),
                                        AmountPPNBuy: $('#AmountPPNBuy').val(),
                                        AmountPPNSell: $('#AmountPPNSell').val(),
                                        DepreciationMode: $('#DepreciationMode').val(),
                                        JournalInterval: $('#JournalInterval').val(),
                                        IntervalSpecificDate: $('#IntervalSpecificDate').val(),
                                        DepreciationPeriod: $('#DepreciationPeriod').val(),
                                        PeriodUnit: $('#PeriodUnit').val(),
                                        DepreciationExpAccount: $('#DepreciationExpAccount').val(),
                                        AccumulatedDeprAccount: $('#AccumulatedDeprAccount').val(),
                                        OfficePK: $('#OfficePK').val(),
                                        DepartmentPK: $('#DepartmentPK').val(),
                                        SellValueDate: $('#SellValueDate').val(),
                                        SellReference: $('#SellReference').val(),
                                        SellDescription: $('#SellDescription').val(),
                                        SellJournalNo: $('#SellJournalNo').val(),
                                        SellAmount: $('#SellAmount').val(),
                                        AccountSellDebit: $('#AccountSellDebit').val(),
                                        BitSold: $('#BitSold').val(),
                                        BuyDebitCurrencyPK: $('#BuyDebitCurrencyPK').val(),
                                        BuyCreditCurrencyPK: $('#BuyCreditCurrencyPK').val(),
                                        BuyDebitRate: $('#BuyDebitRate').val(),
                                        BuyCreditRate: $('#BuyCreditRate').val(),
                                        SellDebitCurrencyPK: $('#SellDebitCurrencyPK').val(),
                                        SellCreditCurrencyPK: $('#SellCreditCurrencyPK').val(),
                                        SellDebitRate: $('#SellDebitRate').val(),
                                        SellCreditRate: $('#SellCreditRate').val(),
                                        DepartmentPK: $('#DepartmentPK').val(),
                                        TypeOfAssetsPK: $('#TypeOfAssetsPK').val(),
                                        Location: $('#Location').val(),
                                        ConsigneePK: $('#ConsigneePK').val(),
                                        ResiduAmount: $('#ResiduAmount').val(),
                                        VATPercent: $('#VATPercent').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_U",
                                        type: 'POST',
                                        data: JSON.stringify(FixedAsset),
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
    });

    $("#BtnApproved").click(function () {
        //Buat Ngetes Di comment dulu
        //if ($("#sumAllocationPercent").text() != 100) {
        //    alertify.alert("Allocation Total Must 100%");
        //    return;
        //}
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FixedAssetPK").val() + "/" + $("#HistoryPK").val() + "/" + "FixedAsset",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == data) {
                            var FixedAsset = {
                                FixedAssetPK: $('#FixedAssetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_A",
                                type: 'POST',
                                data: JSON.stringify(FixedAsset),
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
            alertify.alert("FixedAsset Already Posted / Revised, Void Cancel");
        }
        else {
            
            alertify.confirm("Are you sure want to Void data?", function (e) {

                if (e) {
                    var FixedAsset = {
                        FixedAssetPK: $('#FixedAssetPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_V",
                        type: 'POST',
                        data: JSON.stringify(FixedAsset),
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

    });

    $("#BtnReject").click(function () {
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("FixedAsset Already Posted / Revised, Reject Cancel");
        }
        else {

            
            alertify.confirm("Are you sure want to Reject data?", function (e) {

                if (e) {
                    var FixedAsset = {
                        FixedAssetPK: $('#FixedAssetPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_R",
                        type: 'POST',
                        data: JSON.stringify(FixedAsset),
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
    });

    $("#BtnUnApproved").click(function () {
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("FixedAsset Already Posted / Revised, UnApproved Cancel");
        }
        else {

            
            alertify.confirm("Are you sure want to UnApproved data?", function (e) {


                if (e) {
                    var FixedAsset = {
                        FixedAssetPK: $('#FixedAssetPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAsset/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAsset_UnApproved",
                        type: 'POST',
                        data: JSON.stringify(FixedAsset),
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
    });

    function initGridFixedAssetAllocation() {
        
        var FixedAssetAllocationURL = window.location.origin + "/Radsoft/FixedAssetAllocation/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#FixedAssetPK").val(),
          dataSourceFixedAssetAllocation = getDataSourceFixedAssetAllocation(FixedAssetAllocationURL);

        $("#gridFixedAssetAllocation").kendoGrid({
            dataSource: dataSourceFixedAssetAllocation,
            height: 300,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            filterable: true,
            pageable: true,
            columnMenu: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               {
                 command: { text: "Delete", click: DeleteFixedAssetAllocation }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                 field: "AutoNo", title: "No.", filterable: false, width: 100, locked: true, lockable: false
               },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "DepartmentID", title: "Department ID", width: 200 },
               { field: "AllocationPercent", title: "Allocation Percent %", width: 150, footerTemplate: "<div id='sumAllocationPercent' style='text-align: right'>#=kendo.toString(sum,'n0')# %</div>", format: "{0:n0}", template: "#: AllocationPercent  # %", attributes: { style: "text-align:right;" } },
               { field: "LastUsersID", title: "LastUsersID", width: 150 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 150 }
            ]
        });

    }


    function DeleteFixedAssetAllocation(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        
        if (GlobStatus == 3) {
            alertify.alert("Fixed Aset Already History");
        } else {
            var dataItemX;
            var grid = $("#gridFixedAssetAllocation").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            alertify.confirm("Are you sure want to DELETE Allocation ?", function (e) {
                if (e) {

                    var FixedAssetAllocation = {
                        FixedAssetPK: dataItemX.FixedAssetPK,
                        AutoNo: dataItemX.AutoNo
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAssetAllocation/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAssetAllocation_D",
                        type: 'POST',
                        data: JSON.stringify(FixedAssetAllocation),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshFixedAssetAllocationGrid();
                            alertify.alert(data);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    }

    function refreshFixedAssetAllocationGrid() {
        var grid = $("#gridFixedAssetAllocation").data("kendoGrid");
        grid.dataSource.read();
    }

    function getDataSourceFixedAssetAllocation(_url) {
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
                 pageSize: 6,
                 aggregate: [{ field: "AllocationPercent", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             FixedAssetPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             DepartmentPK: { type: "number" },
                             DepartmentID: { type: "string" },
                             AllocationPercent: { type: "number" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "string" }
                         }
                     }
                 }
             });
    }
    
    $("#BtnAddAllocation").click(function () {
        
        if ($("#sumAllocationPercent").text() >= 100) {
            alertify.alert("Allocation Already 100%");
            return;
        }
        if ($("#FixedAssetPK").val() == 0 || $("#FixedAssetPK").val() == null)
        {
            alertify.alert("There's no Fixed Aset No, Please Add First");
        } else if (GlobStatus == 3) {
            alertify.alert("Fixed Aset Already History");
        } else {
            winAllocate.center();
            winAllocate.open();
        }
    });

  
    $("#BtnOkAllocate").click(function () {

        if ($("#DepartmentPK").val() == 0 || $("#DepartmentPK").val() == null || $("#AllocationPercent").data("kendoNumericTextBox").value() == 0 || $("#AllocationPercent").data("kendoNumericTextBox").value() == null) {
            alertify.alert("Please Choose Department Or Fill Allocation Percent");

        } else {
            alertify.confirm("Are you sure want to Allocate this Fixed Aset ?", function (e) {
                if (e) {
                    var FixedAssetAllocation =
                        {
                            FixedAssetPK: $('#FixedAssetPK').val(),
                            Status: 2,
                            DepartmentPK: $('#DepartmentPK').val(),
                            AllocationPercent: $("#AllocationPercent").data("kendoNumericTextBox").value(),
                            LastUsersID: sessionStorage.getItem("user")
                        };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FixedAssetAllocation/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FixedAssetAllocation_I",
                        type: 'POST',
                        data: JSON.stringify(FixedAssetAllocation),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            winAllocate.close();
                            alertify.alert(data);
                            $("#gridFixedAssetAllocation").empty();
                            initGridFixedAssetAllocation();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    });



    $("#BtnShowDetail").click(function () {
        gridDetailFixedAsset();
        $("#gridDetailFixedAsset").show();
    });


    function getDataSourceFixedAsset(_url) {
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
                             FixedAssetPK: { type: "number" },
                             Year: { type: "string" },
                             Month: { type: "string" },
                             CostValue: { type: "number" },
                             Depreciation: { type: "number" },
                             TotalDepreciation: { type: "number" },
                             BookValue: { type: "number" },
                         }
                     }
                 }
             });
    }

    function gridDetailFixedAsset() {
        $("#gridDetailFixedAsset").empty();
        var AgentFeeURL = window.location.origin + "/Radsoft/FixedAsset/GetFixedAsset/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FixedAssetPK').val(),
            dataSourceApproved = getDataSourceFixedAsset(AgentFeeURL);

        var gridDetail = $("#gridDetailFixedAsset").kendoGrid({
            dataSource: dataSourceApproved,
            height: 450,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Detail Fixed Asset"
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

                //{ field: "Year", title: "Year", width: 110, template: "#= kendo.toString(kendo.parseDate(Year), 'yyyy')#" },
                //{ field: "Month", title: "Month", width: 110, template: "#= kendo.toString(kendo.parseDate(Month), 'MMM')#" },
                { field: "Year", title: "Year", width: 80 },
                { field: "Month", title: "Month", width: 80 },
                { field: "CostValue", title: "CostValue", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Depreciation", title: "Depreciation", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TotalDepreciation", title: "Total Depreciation", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "BookValue", title: "Book Value", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
            ]
        }).data("kendoGrid");



    }


    function onWinFARptClose() {
        GlobValidatorFARpt.hideMessages();
        clearDataFARpt();
    }

    function clearDataFARpt() {
        $("#ValueDateFrom").data("kendoDatePicker").value(null);
        $("#ValueDateTo").data("kendoDatePicker").value(null);
        $("#PeriodPK").val("");
    }

    $("#BtnFAReport").click(function () {

        // ComboBox Period
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpPeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: true,
                    change: OnChangePeriodPK
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

        clearDataFARpt();
        showAddFARpt();
    });

    function showAddFARpt(e) {

        WinFARpt.center();
        WinFARpt.open();

    }


    $("#BtnDownloadRpt").click(function () {
        

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var AccountingRpt = {
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    Period: $("#UpPeriodPK").data("kendoComboBox").text(),
                    PeriodPK: $("#UpPeriodPK").data("kendoComboBox").value(),

                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/FixedAssetsReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AccountingRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data
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



    $("#BtnPostingBySelected").click(function () {
        alertify.confirm("Are you sure want to Posting By Selected ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/PostingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

 



    $("#BtnApproveBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FixedAsset/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FixedAsset/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
});

