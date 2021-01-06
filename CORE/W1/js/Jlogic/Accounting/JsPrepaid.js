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
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/save_accept.png"
        });

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnApproved").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproved.png"
        });

        $("#BtnUnApproved").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnReject").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/refresh2.png"
        });
        $("#BtnRefreshDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproved.png"
        });

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/download1.png"
        });

        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnClose").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
        });

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRevise").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUnPost.png"
        });

        $("#BtnAddAllocation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAllocate.png"
        });
        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

    }
    
    function initWindow() {
        $("#PaymentDate").kendoDatePicker({
            change: OnChangePaymentDate
        });
        $("#PeriodStartDate").kendoDatePicker({});
        $("#PeriodEndDate").kendoDatePicker({});


        $("#DateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateTo
        });

        function OnChangeDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }

        function OnChangeDateTo() {
            refresh();
        }

        function OnChangePaymentDate() {
            if ($("#PaymentDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#PaymentDate").data("kendoDatePicker").value().getFullYear().toString());
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
            height: "150px",
            title: "Allocate To Department",
            visible: false,
            width: "600px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: CloseWinAllocate
        }).data("kendoWindow");

        win = $("#WinPrepaid").kendoWindow({
            height: "1900px",
            title: "Prepaid Detail",
            visible: false,
            width: "1250px",
            modal: true,
            //activate: function () {
            //    $("#ID").focus();
            //},
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },
            close: CloseWinAllocate
            //close: function (e) {
            //    if ($("#sumAllocationPercent").text() != 100 && $("#PrepaidPK").val() != 0) {
            //        alertify.alert("Allocation must 100%");
            //        e.preventDefault();
            //    } else {
            //        GlobValidator.hideMessages();
            //        clearData();
            //        showButton();
            //        refresh();
            //    }

            //}
        }).data("kendoWindow");


    }

    function CloseOldData() {
        $("#OldData").text("");

    }
    var GlobValidator = $("#WinPrepaid").kendoValidator().data("kendoValidator");
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
            $("#PaymentDate").data("kendoDatePicker").value(_d);
            GlobStatus = 0;
        }

        else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;



            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridPrepaidApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {

                grid = $("#gridPrepaidPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {

                grid = $("#gridPrepaidHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            if (dataItemX.Status == 1) {
                $("#StatusHeader").text("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").text("POSTED");
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
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 1) {
                $("#StatusHeader").text("REVISED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnUnApproved").hide();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnVoid").hide();
                $("#BtnAddAllocation").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").text("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").show();
                $("#BtnRevise").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").show();
                // SEKURITAS : ini harusnya di Hide aja.
                //$("#BtnAddAllocation").hide();

            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").text("HISTORY");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#BtnAddAllocation").hide();
            }



            dirty = null;
            $("#PrepaidPK").val(dataItemX.PrepaidPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (dataItemX.PaymentDate == '1/1/1900 12:00:00 AM') {
                $("#PaymentDate").val("");
            } else {
                $("#PaymentDate").data("kendoDatePicker").value(new Date(dataItemX.PaymentDate));
            }
            if (dataItemX.PeriodStartDate == '1/1/1900 12:00:00 AM') {
                $("#PeriodStartDate").val("");
            } else {
                $("#PeriodStartDate").data("kendoDatePicker").value(new Date(dataItemX.PeriodStartDate));
            }
            if (dataItemX.PeriodEndDate == '1/1/1900 12:00:00 AM') {
                $("#PeriodEndDate").val("");
            } else {
                $("#PeriodEndDate").data("kendoDatePicker").value(new Date(dataItemX.PeriodEndDate));
            }
            $("#PaymentJournalNo").val(dataItemX.PaymentJournalNo);
            $("#Reference").val(dataItemX.Reference);
            $("#Description").val(dataItemX.Description);
            $("#PrepaidAccount").val(dataItemX.PrepaidAccount);
            $("#PrepaidAccountID").val(dataItemX.PrepaidAccountID);
            $("#PrepaidCurrencyPK").val(dataItemX.PrepaidCurrencyPK);
            $("#PrepaidCurrencyID").val(dataItemX.PrepaidCurrencyID);
            $("#PrepaidCurrencyRate").val(dataItemX.PrepaidCurrencyRate);
            $("#PrepaidAmount").val(dataItemX.PrepaidAmount);
            $("#DebitAccount1").val(dataItemX.DebitAccount1);
            $("#DebitAccount1ID").val(dataItemX.DebitAccount1ID);
            $("#DebitCurrencyPk1").val(dataItemX.DebitCurrencyPk1);
            $("#DebitCurrencyPk1ID").val(dataItemX.DebitCurrencyPk1ID);
            $("#DebitCurrencyRate1").val(dataItemX.DebitCurrencyRate1);
            $("#DebitAmount1").val(dataItemX.DebitAmount1);
            $("#DebitAccount2").val(dataItemX.DebitAccount2);
            $("#DebitAccount2ID").val(dataItemX.DebitAccount2ID);
            $("#DebitCurrencyPk2").val(dataItemX.DebitCurrencyPk2);
            $("#DebitCurrencyPk2ID").val(dataItemX.DebitCurrencyPk2ID);
            $("#DebitCurrencyRate2").val(dataItemX.DebitCurrencyRate2);
            $("#DebitAmount2").val(dataItemX.DebitAmount2);
            $("#DebitAccount3").val(dataItemX.DebitAccount3);
            $("#DebitAccount3ID").val(dataItemX.DebitAccount3ID);
            $("#DebitCurrencyPk3").val(dataItemX.DebitCurrencyPk3);
            $("#DebitCurrencyPk3ID").val(dataItemX.DebitCurrencyPk3ID);
            $("#DebitCurrencyRate3").val(dataItemX.DebitCurrencyRate3);
            $("#DebitAmount3").val(dataItemX.DebitAmount3);
            $("#DebitAccount4").val(dataItemX.DebitAccount4);
            $("#DebitAccount4ID").val(dataItemX.DebitAccount4ID);
            $("#DebitCurrencyPk4").val(dataItemX.DebitCurrencyPk4);
            $("#DebitCurrencyPk4ID").val(dataItemX.DebitCurrencyPk4ID);
            $("#DebitCurrencyRate4").val(dataItemX.DebitCurrencyRate4);
            $("#DebitAmount4").val(dataItemX.DebitAmount4);
            $("#CreditAccount1").val(dataItemX.CreditAccount1);
            $("#CreditAccount1ID").val(dataItemX.CreditAccount1ID);
            $("#CreditCurrencyPk1").val(dataItemX.CreditCurrencyPk1);
            $("#CreditCurrencyPk1ID").val(dataItemX.CreditCurrencyPk1ID);
            $("#CreditCurrencyRate1").val(dataItemX.CreditCurrencyRate1);
            $("#CreditAmount1").val(dataItemX.CreditAmount1);
            $("#CreditAccount2").val(dataItemX.CreditAccount2);
            $("#CreditAccount2ID").val(dataItemX.CreditAccount2ID);
            $("#CreditCurrencyPk2").val(dataItemX.CreditCurrencyPk2);
            $("#CreditCurrencyPk2ID").val(dataItemX.CreditCurrencyPk2ID);
            $("#CreditCurrencyRate2").val(dataItemX.CreditCurrencyRate2);
            $("#CreditAmount2").val(dataItemX.CreditAmount2);
            $("#CreditAccount3").val(dataItemX.CreditAccount3);
            $("#CreditAccount3ID").val(dataItemX.CreditAccount3ID);
            $("#CreditCurrencyPk3").val(dataItemX.CreditCurrencyPk3);
            $("#CreditCurrencyPk3ID").val(dataItemX.CreditCurrencyPk3ID);
            $("#CreditCurrencyRate3").val(dataItemX.CreditCurrencyRate3);
            $("#CreditAmount3").val(dataItemX.CreditAmount3);
            $("#CreditAccount4").val(dataItemX.CreditAccount4);
            $("#CreditAccount4ID").val(dataItemX.CreditAccount4ID);
            $("#CreditCurrencyPk4").val(dataItemX.CreditCurrencyPk4);
            $("#CreditCurrencyPk4ID").val(dataItemX.CreditCurrencyPk4ID);
            $("#CreditCurrencyRate4").val(dataItemX.CreditCurrencyRate4);
            $("#CreditAmount4").val(dataItemX.CreditAmount4);
            $("#PrepaidExpAccount").val(dataItemX.PrepaidExpAccount);
            $("#PrepaidExpAccountID").val(dataItemX.PrepaidExpAccountID);
            $("#OfficePK").val(dataItemX.OfficePK);
            $("#OfficeID").val(dataItemX.OfficeID);
            $("#DepartmentPK").val(dataItemX.DepartmentPK);
            $("#DepartmentID").val(dataItemX.DepartmentID);
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


            initGridPrepaidAllocation();
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
                    enabled: false,
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
                return _fy;
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
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PrepaidAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePrepaidAccount,
                    value: setCmbPrepaidAccount()
                });

                $("#DebitAccount1").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDebitAccount1,
                    value: setCmbDebitAccount1()
                });

                $("#DebitAccount2").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDebitAccount2,
                    value: setCmbDebitAccount2()
                });

                $("#DebitAccount3").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDebitAccount3,
                    value: setCmbDebitAccount3()
                });

                $("#DebitAccount4").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDebitAccount4,
                    value: setCmbDebitAccount4()
                });

                $("#CreditAccount1").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCreditAccount1,
                    value: setCmbCreditAccount1()
                });

                $("#CreditAccount2").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCreditAccount2,
                    value: setCmbCreditAccount2()
                });

                $("#CreditAccount3").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCreditAccount3,
                    value: setCmbCreditAccount3()
                });

                $("#CreditAccount4").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCreditAccount4,
                    value: setCmbCreditAccount4()
                });
                $("#PrepaidExpAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePrepaidExpAccount,
                    value: setCmbPrepaidExpAccount()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbPrepaidAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PrepaidAccount == 0) {
                    return "";
                } else {
                    return dataItemX.PrepaidAccount;
                }
            }
        }
        function setCmbDebitAccount1() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitAccount1 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitAccount1;
                }
            }
        }
        function setCmbDebitAccount2() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitAccount2 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitAccount2;
                }
            }
        }
        function setCmbDebitAccount3() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitAccount3 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitAccount3;
                }
            }
        }
        function setCmbDebitAccount4() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitAccount4 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitAccount4;
                }
            }
        }
        function setCmbCreditAccount1() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditAccount1 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditAccount1;
                }
            }
        }
        function setCmbCreditAccount2() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditAccount2 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditAccount2;
                }
            }
        }
        function setCmbCreditAccount3() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditAccount3 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditAccount3;
                }
            }
        }
        function setCmbCreditAccount4() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditAccount4 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditAccount4;
                }
            }
        }

        function setCmbPrepaidExpAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PrepaidExpAccount == 0) {
                    return "";
                } else {
                    return dataItemX.PrepaidExpAccount;
                }
            }
        }

        function OnChangeDebitAccount1() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitAccount1").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DebitCurrencyPk1").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DebitCurrencyRate1").data("kendoNumericTextBox").value(data);
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

        function OnChangeDebitAccount2() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitAccount2").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DebitCurrencyPk2").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DebitCurrencyRate2").data("kendoNumericTextBox").value(data);
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
        function OnChangeDebitAccount3() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitAccount3").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DebitCurrencyPk3").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DebitCurrencyRate3").data("kendoNumericTextBox").value(data);
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

        function OnChangeDebitAccount4() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DebitAccount4").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DebitCurrencyPk4").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DebitCurrencyRate4").data("kendoNumericTextBox").value(data);
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

        function OnChangeCreditAccount1() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccount1").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CreditCurrencyPk1").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#CreditCurrencyRate1").data("kendoNumericTextBox").value(data);
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

        function OnChangeCreditAccount2() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccount2").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CreditCurrencyPk2").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#CreditCurrencyRate2").data("kendoNumericTextBox").value(data);
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
        function OnChangeCreditAccount3() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccount3").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CreditCurrencyPk3").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#CreditCurrencyRate3").data("kendoNumericTextBox").value(data);
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

        function OnChangeCreditAccount4() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CreditAccount4").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CreditCurrencyPk4").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#CreditCurrencyRate4").data("kendoNumericTextBox").value(data);
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

        function OnChangePrepaidExpAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function OnChangePrepaidAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#PrepaidAccount").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#PrepaidCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#PaymentDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#PrepaidCurrencyRate").data("kendoNumericTextBox").value(data);
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
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PrepaidCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbPrepaidCurrencyPK()
                });
                $("#DebitCurrencyPk1").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbDebitCurrencyPk1()
                });

                $("#DebitCurrencyPk2").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbDebitCurrencyPk2()
                });
                $("#DebitCurrencyPk3").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbDebitCurrencyPk3()
                });
                $("#DebitCurrencyPk4").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbDebitCurrencyPk4()
                });

                $("#CreditCurrencyPk1").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbCreditCurrencyPk1()
                });

                $("#CreditCurrencyPk2").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbCreditCurrencyPk2()
                });
                $("#CreditCurrencyPk3").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbCreditCurrencyPk3()
                });
                $("#CreditCurrencyPk4").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    enabled: false,
                    change: OnChangeCurrencyPK,
                    value: setCmbCreditCurrencyPk4()
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

        function setCmbPrepaidCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PrepaidCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.PrepaidCurrencyPK;
                }
            }
        }
        function setCmbDebitCurrencyPk1() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCurrencyPk1 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCurrencyPk1;
                }
            }
        }

        function setCmbDebitCurrencyPk2() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCurrencyPk2 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCurrencyPk2;
                }
            }
        }
        function setCmbDebitCurrencyPk3() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCurrencyPk3 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCurrencyPk3;
                }
            }
        }
        function setCmbDebitCurrencyPk4() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DebitCurrencyPk4 == 0) {
                    return "";
                } else {
                    return dataItemX.DebitCurrencyPk4;
                }
            }
        }

        function setCmbCreditCurrencyPk1() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditCurrencyPk1 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditCurrencyPk1;
                }
            }
        }

        function setCmbCreditCurrencyPk2() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditCurrencyPk2 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditCurrencyPk2;
                }
            }
        }
        function setCmbCreditCurrencyPk3() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditCurrencyPk3 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditCurrencyPk3;
                }
            }
        }
        function setCmbCreditCurrencyPk4() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CreditCurrencyPk4 == 0) {
                    return "";
                } else {
                    return dataItemX.CreditCurrencyPk4;
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


        $("#PrepaidAmount").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setPrepaidAmount()
        });

        function setPrepaidAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PrepaidAmount;
            }
        }

        $("#DebitAmount1").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setDebitAmount1()
        });

        function setDebitAmount1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitAmount1;
            }
        }

        $("#DebitAmount2").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setDebitAmount2()
        });

        function setDebitAmount2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitAmount2;
            }
        }

        $("#DebitAmount3").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setDebitAmount3()
        });

        function setDebitAmount3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitAmount3;
            }
        }

        $("#DebitAmount4").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setDebitAmount4()
        });

        function setDebitAmount4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitAmount4;
            }
        }

        $("#CreditAmount1").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setCreditAmount1()
        });

        function setCreditAmount1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditAmount1;
            }
        }

        $("#CreditAmount2").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setCreditAmount2()
        });

        function setCreditAmount2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditAmount2;
            }
        }

        $("#CreditAmount3").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setCreditAmount3()
        });

        function setCreditAmount3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditAmount3;
            }
        }

        $("#CreditAmount4").kendoNumericTextBox({
            format: "n0",
            change: OnChangeAmount,
            value: setCreditAmount4()
        });

        function setCreditAmount4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditAmount4;
            }
        }

        function OnChangeAmount() {
            recalAmount();
        }

       

        $("#PrepaidCurrencyRate").kendoNumericTextBox({
            format: "n4",
            value: setPrepaidCurrencyRate()
        });

        function setPrepaidCurrencyRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PrepaidCurrencyRate;
            }
        }

        $("#DebitCurrencyRate1").kendoNumericTextBox({
            format: "n4",
            value: setDebitCurrencyRate1()
        });

        function setDebitCurrencyRate1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitCurrencyRate1;
            }
        }

        $("#DebitCurrencyRate2").kendoNumericTextBox({
            format: "n4",
            value: setDebitCurrencyRate2()
        });

        function setDebitCurrencyRate2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitCurrencyRate2;
            }
        }

        $("#DebitCurrencyRate3").kendoNumericTextBox({
            format: "n4",
            value: setDebitCurrencyRate3()
        });

        function setDebitCurrencyRate3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitCurrencyRate3;
            }
        }

        $("#DebitCurrencyRate4").kendoNumericTextBox({
            format: "n4",
            value: setDebitCurrencyRate4()
        });

        function setDebitCurrencyRate4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DebitCurrencyRate4;
            }
        }
      
        $("#CreditCurrencyRate1").kendoNumericTextBox({
            format: "n4",
            value: setCreditCurrencyRate1()
        });

        function setCreditCurrencyRate1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditCurrencyRate1;
            }
        }

        $("#CreditCurrencyRate2").kendoNumericTextBox({
            format: "n4",
            value: setCreditCurrencyRate2()
        });

        function setCreditCurrencyRate2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditCurrencyRate2;
            }
        }

        $("#CreditCurrencyRate3").kendoNumericTextBox({
            format: "n4",
            value: setCreditCurrencyRate3()
        });

        function setCreditCurrencyRate3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditCurrencyRate3;
            }
        }

        $("#CreditCurrencyRate4").kendoNumericTextBox({
            format: "n4",
            value: setCreditCurrencyRate4()
        });

        function setCreditCurrencyRate4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CreditCurrencyRate4;
            }
        }


        $("#AllocationPercent").kendoNumericTextBox({
            format: "n4",
            max: 100
        });

        win.center();
        win.open();
    }

    function clearData() {
        $("#PrepaidPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#PaymentDate").data("kendoDatePicker").value(null);
        $("#PeriodStartDate").data("kendoDatePicker").value(null);
        $("#PeriodEndDate").data("kendoDatePicker").value(null);
        $("#PaymentJournalNo").val("");
        $("#Reference").val("");
        $("#Description").val("");
        $("#PrepaidAccount").val("");
        $("#PrepaidAccountID").val("");
        $("#PrepaidCurrencyPK").val("");
        $("#PrepaidCurrencyID").val("");
        $("#PrepaidCurrencyRate").val("");
        $("#PrepaidAmount").val("");
        $("#DebitAccount1").val("");
        $("#DebitAccount1ID").val("");
        $("#DebitCurrencyPk1").val("");
        $("#DebitCurrencyPk1ID").val("");
        $("#DebitCurrencyRate1").val("");
        $("#DebitAmount1").val("");
        $("#DebitAccount2").val("");
        $("#DebitAccount2ID").val("");
        $("#DebitCurrencyPk2").val("");
        $("#DebitCurrencyPk2ID").val("");
        $("#DebitCurrencyRate2").val("");
        $("#DebitAmount2").val("");
        $("#DebitAccount3").val("");
        $("#DebitAccount3ID").val("");
        $("#DebitCurrencyPk3").val("");
        $("#DebitCurrencyPk3ID").val("");
        $("#DebitCurrencyRate3").val("");
        $("#DebitAmount3").val("");
        $("#DebitAccount4").val("");
        $("#DebitAccount4ID").val("");
        $("#DebitCurrencyPk4").val("");
        $("#DebitCurrencyPk4ID").val("");
        $("#DebitCurrencyRate4").val("");
        $("#DebitAmount4").val("");
        $("#CreditAccount1").val("");
        $("#CreditAccount1ID").val("");
        $("#CreditCurrencyPk1").val("");
        $("#CreditCurrencyPk1ID").val("");
        $("#CreditCurrencyRate1").val("");
        $("#CreditAmount1").val("");
        $("#CreditAccount2").val("");
        $("#CreditAccount2ID").val("");
        $("#CreditCurrencyPk2").val("");
        $("#CreditCurrencyPk2ID").val("");
        $("#CreditCurrencyRate2").val("");
        $("#CreditAmount2").val("");
        $("#CreditAccount3").val("");
        $("#CreditAccount3ID").val("");
        $("#CreditCurrencyPk3").val("");
        $("#CreditCurrencyPk3ID").val("");
        $("#CreditCurrencyRate3").val("");
        $("#CreditAmount3").val("");
        $("#CreditAccount4").val("");
        $("#CreditAccount4ID").val("");
        $("#CreditCurrencyPk4").val("");
        $("#CreditCurrencyPk4ID").val("");
        $("#CreditCurrencyRate4").val("");
        $("#CreditAmount4").val("");
        $("#PrepaidExpAccount").val("");
        $("#PrepaidExpAccountID").val("");
        $("#OfficePK").val("");
        $("#OfficeID").val("");
        $("#DepartmentPK").val("");
        $("#DepartmentID").val("");
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

    function CloseWinAllocate() {
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
                             PrepaidPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             PaymentDate: { type: "string" },
                             PeriodStartDate: { type: "string" },
                             PeriodEndDate: { type: "string" },
                             PaymentJournalNo: { type: "number" },
                             Reference: { type: "string" },
                             Description: { type: "string" },
                             PrepaidAccount: { type: "number" },
                             PrepaidAccountID: { type: "string" },
                             PrepaidCurrencyPK: { type: "number" },
                             PrepaidCurrencyID: { type: "string" },
                             PrepaidCurrencyRate: { type: "number" },
                             PrepaidAmount: { type: "number" },
                             DebitAccount1: { type: "number" },
                             DebitAccount1ID: { type: "string" },
                             DebitCurrencyPk1: { type: "number" },
                             DebitCurrencyPk1ID: { type: "string" },
                             DebitCurrencyRate1: { type: "number" },
                             DebitAmount1: { type: "number" },
                             DebitAccount2: { type: "number" },
                             DebitAccount2ID: { type: "string" },
                             DebitCurrencyPk2: { type: "number" },
                             DebitCurrencyPk2ID: { type: "string" },
                             DebitCurrencyRate2: { type: "number" },
                             DebitAmount2: { type: "number" },
                             DebitAccount3: { type: "number" },
                             DebitAccount3ID: { type: "string" },
                             DebitCurrencyPk3: { type: "number" },
                             DebitCurrencyPk3ID: { type: "string" },
                             DebitCurrencyRate3: { type: "number" },
                             DebitAmount3: { type: "number" },
                             DebitAccount4: { type: "number" },
                             DebitAccount4ID: { type: "string" },
                             DebitCurrencyPk4: { type: "number" },
                             DebitCurrencyPk4ID: { type: "string" },
                             DebitCurrencyRate4: { type: "number" },
                             DebitAmount4: { type: "number" },
                             CreditAccount1: { type: "number" },
                             CreditAccount1ID: { type: "string" },
                             CreditCurrencyPk1: { type: "number" },
                             CreditCurrencyPk1ID: { type: "string" },
                             CreditCurrencyRate1: { type: "number" },
                             CreditAmount1: { type: "number" },
                             CreditAccount2: { type: "number" },
                             CreditAccount2ID: { type: "string" },
                             CreditCurrencyPk2: { type: "number" },
                             CreditCurrencyPk2ID: { type: "string" },
                             CreditCurrencyRate2: { type: "number" },
                             CreditAmount2: { type: "number" },
                             CreditAccount3: { type: "number" },
                             CreditAccount3ID: { type: "string" },
                             CreditCurrencyPk3: { type: "number" },
                             CreditCurrencyPk3ID: { type: "string" },
                             CreditCurrencyRate3: { type: "number" },
                             CreditAmount3: { type: "number" },
                             CreditAccount4: { type: "number" },
                             CreditAccount4ID: { type: "string" },
                             CreditCurrencyPk4: { type: "number" },
                             CreditCurrencyPk4ID: { type: "string" },
                             CreditCurrencyRate4: { type: "number" },
                             CreditAmount4: { type: "number" },
                             PrepaidExpAccount: { type: "number" },
                             PrepaidExpAccountID: { type: "string" },
                             OfficePK: { type: "number" },
                             OfficeID: { type: "string" },
                             DepartmentPK: { type: "number" },
                             DepartmentID: { type: "string" },
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

    function onDataBound() {
        var grid = $("#gridPrepaidApproved").data("kendoGrid");
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
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
            //var gridApproved = $("#gridPrepaidApproved").data("kendoGrid");
            // gridApproved.dataSource.read();

        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridPrepaidPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridPrepaidHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }

    }
    function initGrid() {
        $("#gridPrepaidApproved").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var PrepaidApprovedURL = window.location.origin + "/Radsoft/Prepaid/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(PrepaidApprovedURL);
        }

        //var PrepaidApprovedURL = window.location.origin + "/Radsoft/Prepaid/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/CP",
        //  dataSourceApproved = getDataSource(PrepaidApprovedURL);

        $("#gridPrepaidApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: 450,
            scrollable: {
                virtual: true
            },
            dataBound: onDataBound,
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
               { command: { text: "Posting", click: showPosting }, title: " ", width: 80 },
               { command: { text: "Revise", click: showRevise }, title: " ", width: 80 },
               { field: "PrepaidPK", title: "SysNo.", filterable: false, width: 80 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 120 },
               { field: "Revised", title: "Revised", width: 120 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "PaymentDate", title: "Payment Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'MM/dd/yyyy')#" },
               { field: "PeriodStartDate", title: "Start Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodStartDate), 'MM/dd/yyyy')#" },
               { field: "PeriodEndDate", title: "End Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodEndDate), 'MM/dd/yyyy')#" },
               { field: "PaymentJournalNo", title: "Payment Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "Description", title: "Description", width: 200 },
               { field: "PrepaidAccount", title: "PrepaidAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidAccountID", title: "Prepaid Account (ID)", width: 170 },
               { field: "PrepaidCurrencyPK", title: "PrepaidCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "PrepaidCurrencyID", title: "Prepaid Currency (ID)", width: 250 },
               { field: "PrepaidCurrencyRate", title: "<div style='text-align: right'>Prepaid Currency Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "PrepaidAmount", title: "Prepaid Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } }, 
               { field: "DebitAccount1", title: "DebitAccount1", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "DebitCurrencyPk1", title: "DebitCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount2", title: "DebitAccount2", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "DebitCurrencyPk2", title: "DebitCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount3", title: "DebitAccount3", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "DebitCurrencyPk3", title: "DebitCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "DebitCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount4", title: "DebitAccount4", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "DebitCurrencyPk4", title: "DebitCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "DebitCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount1", title: "CreditAccount1", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "CreditCurrencyPk1", title: "CreditCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount2", title: "CreditAccount2", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "CreditCurrencyPk2", title: "CreditCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount3", title: "CreditAccount3", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "CreditCurrencyPk3", title: "CreditCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "CreditCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount4", title: "CreditAccount4", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "CreditCurrencyPk4", title: "CreditCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "CreditCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PrepaidExpAccount", title: "PrepaidExpAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidExpAccountID", title: "Prepaid Exp Account (ID)", width: 170 },
               { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
               { field: "OfficeID", title: "Office (ID)", width: 170 },
               { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
               { field: "DepartmentID", title: "Department (ID)", width: 170 },
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



        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabPrepaid").kendoTabStrip({
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

    }
    function RecalGridPending() {
        $("#gridPrepaidPending").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var PrepaidPendingURL = window.location.origin + "/Radsoft/Prepaid/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(PrepaidPendingURL);
        }
        $("#gridPrepaidPending").kendoGrid({
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
               { field: "PrepaidPK", title: "SysNo.", filterable: false, width: 80 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 120 },
               { field: "Revised", title: "Revised", width: 120 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "PaymentDate", title: "Payment Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'MM/dd/yyyy')#" },
               { field: "PeriodStartDate", title: "Start Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodStartDate), 'MM/dd/yyyy')#" },
               { field: "PeriodEndDate", title: "End Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodEndDate), 'MM/dd/yyyy')#" },
               { field: "PaymentJournalNo", title: "Payment Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "Description", title: "Description", width: 200 },
               { field: "PrepaidAccount", title: "PrepaidAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidAccountID", title: "Prepaid Account (ID)", width: 170 },
               { field: "PrepaidCurrencyPK", title: "PrepaidCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "PrepaidCurrencyID", title: "Prepaid Currency (ID)", width: 250 },
               { field: "PrepaidCurrencyRate", title: "<div style='text-align: right'>Prepaid Currency Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "PrepaidAmount", title: "Prepaid Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount1", title: "DebitAccount1", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "DebitCurrencyPk1", title: "DebitCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount2", title: "DebitAccount2", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "DebitCurrencyPk2", title: "DebitCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount3", title: "DebitAccount3", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "DebitCurrencyPk3", title: "DebitCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "DebitCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount4", title: "DebitAccount4", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "DebitCurrencyPk4", title: "DebitCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "DebitCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount1", title: "CreditAccount1", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "CreditCurrencyPk1", title: "CreditCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount2", title: "CreditAccount2", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "CreditCurrencyPk2", title: "CreditCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount3", title: "CreditAccount3", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "CreditCurrencyPk3", title: "CreditCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "CreditCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount4", title: "CreditAccount4", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "CreditCurrencyPk4", title: "CreditCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "CreditCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PrepaidExpAccount", title: "PrepaidExpAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidExpAccountID", title: "Prepaid Exp Account (ID)", width: 170 },
               { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
               { field: "OfficeID", title: "Office (ID)", width: 170 },
               { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
               { field: "DepartmentID", title: "Department (ID)", width: 170 },
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
    }
    function RecalGridHistory() {
        $("#gridPrepaidHistory").empty();

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }

        else {
            var PrepaidHistoryURL = window.location.origin + "/Radsoft/Prepaid/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3 + "/" + "CP" + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(PrepaidHistoryURL);
        }
        $("#gridPrepaidHistory").kendoGrid({
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
               { field: "PrepaidPK", title: "SysNo.", filterable: false, width: 80 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "PeriodPK", title: "PeriodPK", hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 120 },
               { field: "Revised", title: "Revised", width: 120 },
               { field: "PeriodID", title: "Period ID", width: 120 },
               { field: "PaymentDate", title: "Payment Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'MM/dd/yyyy')#" },
               { field: "PeriodStartDate", title: "Start Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodStartDate), 'MM/dd/yyyy')#" },
               { field: "PeriodEndDate", title: "End Date", width: 170, template: "#= kendo.toString(kendo.parseDate(PeriodEndDate), 'MM/dd/yyyy')#" },
               { field: "PaymentJournalNo", title: "Payment Journal No", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 170 },
               { field: "Reference", title: "Reference", width: 170 },
               { field: "Description", title: "Description", width: 200 },
               { field: "PrepaidAccount", title: "PrepaidAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidAccountID", title: "Prepaid Account (ID)", width: 170 },
               { field: "PrepaidCurrencyPK", title: "PrepaidCurrencyPK", hidden: true, width: 170, hidden: true },
               { field: "PrepaidCurrencyID", title: "Prepaid Currency (ID)", width: 250 },
               { field: "PrepaidCurrencyRate", title: "<div style='text-align: right'>Prepaid Currency Rate", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "PrepaidAmount", title: "Prepaid Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount1", title: "DebitAccount1", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "DebitCurrencyPk1", title: "DebitCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount2", title: "DebitAccount2", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "DebitCurrencyPk2", title: "DebitCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "DebitCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount3", title: "DebitAccount3", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "DebitCurrencyPk3", title: "DebitCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "DebitCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "DebitAccount4", title: "DebitAccount4", hidden: true, width: 170, hidden: true },
               { field: "DebitAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "DebitCurrencyPk4", title: "DebitCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "DebitCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "DebitCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "DebitAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount1", title: "CreditAccount1", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount1ID", title: "VAT in (1)", width: 170 },
               { field: "CreditCurrencyPk1", title: "CreditCurrencyPk1", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk1ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate1", title: "<div style='text-align: right'>VAT Rate (1)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount1", title: "VAT Amount (1)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount2", title: "CreditAccount2", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount2ID", title: "VAT in (2)", width: 170 },
               { field: "CreditCurrencyPk2", title: "CreditCurrencyPk2", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk2ID", title: "VAT Currency (ID)", width: 250 },
               { field: "CreditCurrencyRate2", title: "<div style='text-align: right'>VAT Rate (2)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount2", title: "VAT Amount (2)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount3", title: "CreditAccount3", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount3ID", title: "VAT in (3)", width: 170 },
               { field: "CreditCurrencyPk3", title: "CreditCurrencyPk3", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk3ID", title: "VAT Currency (ID)", width: 350 },
               { field: "CreditCurrencyRate3", title: "<div style='text-align: right'>VAT Rate (3)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount3", title: "VAT Amount (3)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CreditAccount4", title: "CreditAccount4", hidden: true, width: 170, hidden: true },
               { field: "CreditAccount4ID", title: "VAT in (4)", width: 170 },
               { field: "CreditCurrencyPk4", title: "CreditCurrencyPk4", hidden: true, width: 170, hidden: true },
               { field: "CreditCurrencyPk4ID", title: "VAT Currency (ID)", width: 450 },
               { field: "CreditCurrencyRate4", title: "<div style='text-align: right'>VAT Rate (4)", format: "{0:n0}", width: 150, attributes: { style: "text-align:right;" } },
               { field: "CreditAmount4", title: "VAT Amount (4)", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PrepaidExpAccount", title: "PrepaidExpAccount", hidden: true, width: 170, hidden: true },
               { field: "PrepaidExpAccountID", title: "Prepaid Exp Account (ID)", width: 170 },
               { field: "OfficePK", title: "OfficePK", hidden: true, width: 170, hidden: true },
               { field: "OfficeID", title: "Office (ID)", width: 170 },
               { field: "DepartmentPK", title: "DepartmentPK", hidden: true, width: 170, hidden: true },
               { field: "DepartmentID", title: "Department (ID)", width: 170 },
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
    }


    //AZIZ
    function showPosting(e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
            var grid = $("#gridPrepaidApproved").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            var pos = _dataItem.Posted;
            var _pk = _dataItem.PrepaidPK;
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
                        PrepaidPK: _pk,
                        HistoryPK: _his,
                        EntryUsersID: sessionStorage.getItem("user")
                    };

                }
                else {
                    var Posting = {
                        PrepaidPK: $("#PrepaidPK").val(),
                        HistoryPK: $("#HistoryPK").val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                }



                $.ajax({
                    url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_Posting",
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
            var grid = $("#gridPrepaidApproved").data("kendoGrid");
            var _dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            var rev = _dataItem.Revised;
            var _pk = _dataItem.PrepaidPK;
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
                        PrepaidPK: _pk,
                        HistoryPK: _his,
                        EntryUsersID: sessionStorage.getItem("user")
                    };

                }
                else {
                    var Revise = {
                        PrepaidPK: $("#PrepaidPK").val(),
                        HistoryPK: $("#HistoryPK").val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                }

                $.ajax({
                    url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_Revise",
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
        
        alertify.confirm("Are you sure want to Download data Prepaid ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Prepaid/DownloadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                if ($("#PrepaidPK").val() != "") {
                    if ($("#sumAllocationPercent").text() != 100) {
                        alertify.alert("Allocation must 100%");
                        return;
                    }
                }
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });



    $("#BtnNew").click(function () {
        showDetails(null);
    });


    //AZIZ
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add Prepaid  ?", function (e) {
                if (e) {

                    var Prepaid = {
                        PeriodPK: $('#PeriodPK').val(),
                        PaymentDate: $('#PaymentDate').val(),
                        PeriodStartDate: $('#PeriodStartDate').val(),
                        PeriodEndDate: $('#PeriodEndDate').val(),
                        PaymentJournalNo: $('#PaymentJournalNo').val(),
                        Reference: $('#Reference').val(),
                        Description: $('#Description').val(),
                        PrepaidAccount: $('#PrepaidAccount').val(),
                        PrepaidCurrencyPK: $('#PrepaidCurrencyPK').val(),
                        PrepaidCurrencyRate: $('#PrepaidCurrencyRate').val(),
                        PrepaidAmount: $('#PrepaidAmount').val(),
                        DebitAccount1: $('#DebitAccount1').val(),
                        DebitCurrencyPk1: $('#DebitCurrencyPk1').val(),
                        DebitCurrencyRate1: $('#DebitCurrencyRate1').val(),
                        DebitAmount1: $('#DebitAmount1').val(),
                        DebitAccount2: $('#DebitAccount2').val(),
                        DebitCurrencyPk2: $('#DebitCurrencyPk2').val(),
                        DebitCurrencyRate2: $('#DebitCurrencyRate2').val(),
                        DebitAmount2: $('#DebitAmount2').val(),
                        DebitAccount3: $('#DebitAccount3').val(),
                        DebitCurrencyPk3: $('#DebitCurrencyPk3').val(),
                        DebitCurrencyRate3: $('#DebitCurrencyRate3').val(),
                        DebitAmount3: $('#DebitAmount3').val(),
                        DebitAccount4: $('#DebitAccount4').val(),
                        DebitCurrencyPk4: $('#DebitCurrencyPk4').val(),
                        DebitCurrencyRate4: $('#DebitCurrencyRate4').val(),
                        DebitAmount4: $('#DebitAmount4').val(),
                        CreditAccount1: $('#CreditAccount1').val(),
                        CreditCurrencyPk1: $('#CreditCurrencyPk1').val(),
                        CreditCurrencyRate1: $('#CreditCurrencyRate1').val(),
                        CreditAmount1: $('#CreditAmount1').val(),
                        CreditAccount2: $('#CreditAccount2').val(),
                        CreditCurrencyPk2: $('#CreditCurrencyPk2').val(),
                        CreditCurrencyRate2: $('#CreditCurrencyRate2').val(),
                        CreditAmount2: $('#CreditAmount2').val(),
                        CreditAccount3: $('#CreditAccount3').val(),
                        CreditCurrencyPk3: $('#CreditCurrencyPk3').val(),
                        CreditCurrencyRate3: $('#CreditCurrencyRate3').val(),
                        CreditAmount3: $('#CreditAmount3').val(),
                        CreditAccount4: $('#CreditAccount4').val(),
                        CreditCurrencyPk4: $('#CreditCurrencyPk4').val(),
                        CreditCurrencyRate4: $('#CreditCurrencyRate4').val(),
                        CreditAmount4: $('#CreditAmount4').val(),
                        PrepaidExpAccount: $('#PrepaidExpAccount').val(),
                        OfficePK: $('#OfficePK').val(),
                        DepartmentPK: $('#DepartmentPK').val(), 
                        EntryUsersID: sessionStorage.getItem("user")

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_I",
                        type: 'POST',
                        data: JSON.stringify(Prepaid),
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

    $("#BtnUpdate").click(function () {
        if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
            alertify.alert("Prepaid Already Posted / Revised, Update Cancel");
        }
        else {
            var val = validateData();
            if (val == 1) {
                
                alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PrepaidPK").val() + "/" + $("#HistoryPK").val() + "/" + "Prepaid",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == data) {

                                    var Prepaid = {
                                        PrepaidPK: $('#PrepaidPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        PeriodPK: $('#PeriodPK').val(),
                                        PaymentDate: $('#PaymentDate').val(),
                                        PeriodStartDate: $('#PeriodStartDate').val(),
                                        PeriodEndDate: $('#PeriodEndDate').val(),
                                        PaymentJournalNo: $('#PaymentJournalNo').val(),
                                        Reference: $('#Reference').val(),
                                        Description: $('#Description').val(),
                                        PrepaidAccount: $('#PrepaidAccount').val(),
                                        PrepaidCurrencyPK: $('#PrepaidCurrencyPK').val(),
                                        PrepaidCurrencyRate: $('#PrepaidCurrencyRate').val(),
                                        PrepaidAmount: $('#PrepaidAmount').val(),
                                        DebitAccount1: $('#DebitAccount1').val(),
                                        DebitCurrencyPk1: $('#DebitCurrencyPk1').val(),
                                        DebitCurrencyRate1: $('#DebitCurrencyRate1').val(),
                                        DebitAmount1: $('#DebitAmount1').val(),
                                        DebitAccount2: $('#DebitAccount2').val(),
                                        DebitCurrencyPk2: $('#DebitCurrencyPk2').val(),
                                        DebitCurrencyRate2: $('#DebitCurrencyRate2').val(),
                                        DebitAmount2: $('#DebitAmount2').val(),
                                        DebitAccount3: $('#DebitAccount3').val(),
                                        DebitCurrencyPk3: $('#DebitCurrencyPk3').val(),
                                        DebitCurrencyRate3: $('#DebitCurrencyRate3').val(),
                                        DebitAmount3: $('#DebitAmount3').val(),
                                        DebitAccount4: $('#DebitAccount4').val(),
                                        DebitCurrencyPk4: $('#DebitCurrencyPk4').val(),
                                        DebitCurrencyRate4: $('#DebitCurrencyRate4').val(),
                                        DebitAmount4: $('#DebitAmount4').val(),
                                        CreditAccount1: $('#CreditAccount1').val(),
                                        CreditCurrencyPk1: $('#CreditCurrencyPk1').val(),
                                        CreditCurrencyRate1: $('#CreditCurrencyRate1').val(),
                                        CreditAmount1: $('#CreditAmount1').val(),
                                        CreditAccount2: $('#CreditAccount2').val(),
                                        CreditCurrencyPk2: $('#CreditCurrencyPk2').val(),
                                        CreditCurrencyRate2: $('#CreditCurrencyRate2').val(),
                                        CreditAmount2: $('#CreditAmount2').val(),
                                        CreditAccount3: $('#CreditAccount3').val(),
                                        CreditCurrencyPk3: $('#CreditCurrencyPk3').val(),
                                        CreditCurrencyRate3: $('#CreditCurrencyRate3').val(),
                                        CreditAmount3: $('#CreditAmount3').val(),
                                        CreditAccount4: $('#CreditAccount4').val(),
                                        CreditCurrencyPk4: $('#CreditCurrencyPk4').val(),
                                        CreditCurrencyRate4: $('#CreditCurrencyRate4').val(),
                                        CreditAmount4: $('#CreditAmount4').val(),
                                        PrepaidExpAccount: $('#PrepaidExpAccount').val(),
                                        OfficePK: $('#OfficePK').val(),
                                        DepartmentPK: $('#DepartmentPK').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_U",
                                        type: 'POST',
                                        data: JSON.stringify(Prepaid),
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
        
        if ($("#sumAllocationPercent").text() != 100) {
            alertify.alert("Allocation Total Must 100%");
            return;
        }
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PrepaidPK").val() + "/" + $("#HistoryPK").val() + "/" + "Prepaid",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == data) {
                            var Prepaid = {
                                PrepaidPK: $('#PrepaidPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_A",
                                type: 'POST',
                                data: JSON.stringify(Prepaid),
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
            alertify.alert("Prepaid Already Posted / Revised, Void Cancel");
        }
        else {
            
            alertify.confirm("Are you sure want to Void data?", function (e) {

                if (e) {
                    var Prepaid = {
                        PrepaidPK: $('#PrepaidPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_V",
                        type: 'POST',
                        data: JSON.stringify(Prepaid),
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
            alertify.alert("Prepaid Already Posted / Revised, Reject Cancel");
        }
        else {

            
            alertify.confirm("Are you sure want to Reject data?", function (e) {

                if (e) {
                    var Prepaid = {
                        PrepaidPK: $('#PrepaidPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_R",
                        type: 'POST',
                        data: JSON.stringify(Prepaid),
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
            alertify.alert("Prepaid Already Posted / Revised, UnApproved Cancel");
        }
        else {

            
            alertify.confirm("Are you sure want to UnApproved data?", function (e) {


                if (e) {
                    var Prepaid = {
                        PrepaidPK: $('#PrepaidPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Prepaid/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Prepaid_UnApproved",
                        type: 'POST',
                        data: JSON.stringify(Prepaid),
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

    function initGridPrepaidAllocation() {
        $("#gridPrepaidAllocation").empty();
        var PrepaidAllocationURL = window.location.origin + "/Radsoft/PrepaidAllocation/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#PrepaidPK").val(),
          dataSourcePrepaidAllocation = getDataSourcePrepaidAllocation(PrepaidAllocationURL);

        $("#gridPrepaidAllocation").kendoGrid({
            dataSource: dataSourcePrepaidAllocation,
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
                   command: { text: "Delete", click: DeletePrepaidAllocation }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AutoNo", title: "No.", filterable: false, width: 50, locked: true, lockable: false
               },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "DepartmentID", title: "Department ID", width: 120 },
               { field: "AllocationPercent", title: "<div style='text-align: right'>%</div>", width: 50, footerTemplate: "<div id='sumAllocationPercent' style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "LastUsersID", title: "LastUsersID", width: 120 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 120 }
            ]
        });

    }


    function DeletePrepaidAllocation(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        
        if (GlobStatus == 3) {
            alertify.alert("Prepaid Already History");
        } else {
            var dataItemX;
            var grid = $("#gridPrepaidAllocation").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            alertify.confirm("Are you sure want to DELETE Allocation ?", function (e) {
                if (e) {

                    var PrepaidAllocation = {
                        PrepaidPK: dataItemX.PrepaidPK,
                        AutoNo: dataItemX.AutoNo
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/PrepaidAllocation/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PrepaidAllocation_D",
                        type: 'POST',
                        data: JSON.stringify(PrepaidAllocation),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshPrepaidAllocationGrid();
                            alertify.alert(data.Message);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    }

    function refreshPrepaidAllocationGrid() {
        var grid = $("#gridPrepaidAllocation").data("kendoGrid");
        grid.dataSource.read();
    }

    function getDataSourcePrepaidAllocation(_url) {
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
                             PrepaidPK: { type: "number" },
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
        if ($("#PrepaidPK").val() == 0 || $("#PrepaidPK").val() == null) {
            alertify.alert("There's no Prepaid No, Please Add First");
        } else if (GlobStatus == 3) {
            alertify.alert("Prepaid Already History");
        } else {
            winAllocate.center();
            winAllocate.open();
        }
    });

    $("#BtnOkAllocate").click(function () {
        
        if ($("#DepartmentPK").val() == 0 || $("#DepartmentPK").val() == null || $("#AllocationPercent").data("kendoNumericTextBox").value() == 0 || $("#AllocationPercent").data("kendoNumericTextBox").value() == null) {
            alertify.alert("Please Choose Department Or Fill Allocation Percent");

        } else {
            alertify.confirm("Are you sure want to Allocate this Prepaid ?", function (e) {
                if (e) {
                    var PrepaidAllocation =
                        {
                            PrepaidPK: $('#PrepaidPK').val(),
                            Status: 2,
                            DepartmentPK: $('#DepartmentPK').val(),
                            AllocationPercent: $("#AllocationPercent").data("kendoNumericTextBox").value(),
                            LastUsersID: sessionStorage.getItem("user")
                        };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/PrepaidAllocation/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PrepaidAllocation_I",
                        type: 'POST',
                        data: JSON.stringify(PrepaidAllocation),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            winAllocate.close();
                            alertify.alert(data);
                            $("#gridPrepaidAllocation").empty();
                            initGridPrepaidAllocation();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    });
});

