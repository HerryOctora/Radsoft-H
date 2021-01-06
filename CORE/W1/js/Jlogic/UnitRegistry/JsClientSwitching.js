$(document).ready(function () {
    document.title = 'FORM CLIENT SWITCHING';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height;
    var WinListFundClient;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
    //grid filter
    var filterField;
    var filterOperator;
    var filtervalue;
    var filterlogic;
    //end grid filter
    //fund grid
    var checkedIds = {};
    var checkedApproved = {};
    var checkedPending = {};

    //end Fund grid



    //1
    initButton();
    //2
    initWindow();

    if (_GlobClientCode == "20") {
        $("#lblEntryDate").hide();
        $("#lblTradeDate").show();
        $("#NAVDate").data("kendoDatePicker").enable(false);
        $("#lblSignature").show();
        $("#LblDownloadMode").show();
        $("#tdDownloadMode").show();
    }
    else {
        $("#lblEntryDate").show();
        $("#lblTradeDate").hide();
        $("#NAVDate").data("kendoDatePicker").enable(true);
        $("#lblSignature").hide();
        $("#LblDownloadMode").hide();
        $("#tdDownloadMode").hide();
    }

    if (_GlobClientCode == "08") {
        $("#lblTransactionPK").show();
    }
    else {
        $("#lblTransactionPK").hide();
    }

    //3
    initGrid();

    function initButton() {

        if (_GlobClientCode == "01") {
            $("#trTransferType").show();
        }

        else if (_GlobClientCode == "24") {
            $("#trTransferType").show();
        }

        else {
            $("#trTransferType").hide();
        }
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

        $("#BtnApproveBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({ //Tolong Di Check
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnReviseBySelected").kendoButton({ //Tolong Di Check
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnVoidBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnGetNavBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnBatchFormBySelected").kendoButton({ //Skip 
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnRecalculate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSInvestSwitchingRptBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnAddAgentTrx").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnAddAgentFeeTrx").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }


    function initWindow() {

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
                    suggest: true,
                    index: 0
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
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position1").val(data.Position);
                }
            });
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
            index: 1
        });
        function OnChangeDownloadMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
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
                    suggest: true,
                    index: 0
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
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position2").val(data.Position);
                }
            });
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
                    suggest: true,
                    index: 0
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
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position3").val(data.Position);
                }
            });
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
                    suggest: true,
                    index: 0
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
            $.ajax({
                url: window.location.origin + "/Radsoft/Signature/GetPosition4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Position4").val(data.Position);
                }
            });
        }



        $("#NAVDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeNAVDate
        });

        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            clearDataNav();
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
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
                    url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //Cek WorkingDays
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + data,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
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

                // Cek Jam, klo diatas jam 12 Pake NAV bsok,klo dibawah Jam 12 Pake NAV hari itu juga
                //var currentTime = new Date();
                //var hours = currentTime.getHours();
                //if (hours <= 12) {

                $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
                $("#NAVFundFrom").data("kendoNumericTextBox").value(0);
                $("#NAVFundTo").data("kendoNumericTextBox").value(0);
                ClientSwitchingRecalculate();


                //} else {
                //    $.ajax({
                //        url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 0,
                //        type: 'GET',
                //        contentType: "application/json;charset=utf-8",
                //        success: function (data) {
                //            $("#NAVDate").data("kendoDatePicker").value(new Date(data));
                //        },
                //        error: function (data) {
                //            alertify.alert(data.responseText);
                //        }
                //    });
                //}


            }
        }


        function OnChangeNAVDate() {
            var _date = Date.parse($("#NAVDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }
            if ($("#NAVDate").data("kendoDatePicker").value() != null) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        } else {
     
                            ClientSwitchingRecalculate();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }
        var dateF = new Date();
        dateF.setDate(dateF.getDate() - 3);
        $("#PaymentDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: _setPaymentDate(),
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        }); 
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateTo
        });


        function OnChangeDateFrom() {
            
            var currentDate = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            
            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }


        win = $("#WinClientSwitching").kendoWindow({
            height: 1250,
            title: " Switching Detail",
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
            }
        }).data("kendoWindow");


        WinListFundClient = $("#WinListFundClient").kendoWindow({
            height: 450,
            title: "List Fund Client ",
            visible: false,
            width: 950,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListFundClientClose
        }).data("kendoWindow");

        function onWinListFundClientClose() {
            $("#gridListFundClient").empty();
        }


        WinAddAgentTrx = $("#WinAddAgentTrx").kendoWindow({
            height: 150,
            title: "* ADD AGENT TRX",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            },
            close: onPopUpCloseAddAgentTrx
        }).data("kendoWindow");

        WinAddAgentFeeTrx = $("#WinAddAgentFeeTrx").kendoWindow({
            height: 150,
            title: "* ADD AGENT FEE TRX",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            },
            close: onPopUpCloseAddAgentFeeTrx
        }).data("kendoWindow");


        WinValidationSwi = $("#WinValidationSwi").kendoWindow({
            height: 500,
            title: "* Validation",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 200 })
            }
        }).data("kendoWindow");



    }


    var GlobValidator = $("#WinClientSwitching").kendoValidator().data("kendoValidator");

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

        $("#btnListFundClientPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")


        var dataItemX;
        if (e == null) {

            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#BtnRecalculate").show();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
            $("#ValueDate").data("kendoDatePicker").value(_d);
            $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").val());
            $("#PaymentDate").data("kendoDatePicker").value(_setPaymentDate());
            $("#StatusHeader").val("NEW");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridClientSwitchingApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridClientSwitchingPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridClientSwitchingHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnOldData").show();
                $("#BtnApproved").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").show();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnPosting").show();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnRecalculate").hide();
                $("#BtnOldData").hide();
            }

            dirty = null;

            $("#ClientSwitchingPK").val(dataItemX.ClientSwitchingPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#NAVDate").data("kendoDatePicker").value(dataItemX.NAVDate);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            $("#NAVFundFrom").val(dataItemX.NAVFundFrom);
            $("#NAVFundTo").val(dataItemX.NAVFundTo);
            $("#FundPKFrom").val(dataItemX.FundPKFrom);
            $("#FundIDFrom").val(dataItemX.FundIDFrom);
            $("#FundPKTo").val(dataItemX.FundPKTo);
            $("#FundIDTo").val(dataItemX.FundIDTo);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
            //$("#CashRefPK").val(dataItemX.CashRefPK);
            //$("#CashRefID").val(dataItemX.CashRefID);
            $("#CurrencyPK").val(dataItemX.CurrencyPK);
            $("#CurrencyID").val(dataItemX.CurrencyID);
            $("#TransferType").val(dataItemX.TransferType);
            $("#Description").val(dataItemX.Description);
            $("#TransactionPK").val(dataItemX.TransactionPK);
            $("#UserSwitchingPK").val(dataItemX.UserSwitchingPK);
            $("#ReferenceSInvest").val(dataItemX.ReferenceSInvest);

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

            if (dataItemX.Revised == true) {
                $("#Revised").prop('checked', true);
            }
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
                $("#RevisedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").data("kendoDatePicker").value(dataItemX.LastUpdate);

        }

        //combo box Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SubscriptionType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (_GlobClientCode == "10") {
                    $("#Type").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeType,
                        value: setCmbType()
                    });
                }
                else {
                    $("#Type").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        enable: false,
                        index: 0,
                        change: onChangeType,
                        value: setCmbType()
                    });
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbType() {
            if (e == null) {
                return 1;
            } else {
                if (dataItemX.Type == 0) {
                    return 1;
                } else {
                    return dataItemX.Type;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPKFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundPKFrom,
                    value: setCmbFundPKFrom()
                });
                $("#FundPKTo").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundPKTo,
                    value: setCmbFundPKTo()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundPKFrom() {
            if (e != null) {
                if (this.value() == dataItemX.FundPK) {
                    return;
                }
            }

            _setPaymentDate();
            clearDataNav();
            $("#Description").val("");
            $("#FundPKTo").data("kendoComboBox").value("");
            $("#CashRefPKFrom").data("kendoComboBox").value("");
            $("#CashRefPKTo").data("kendoComboBox").value("");
            $("#CurrencyPK").data("kendoComboBox").value("");

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }

            else {

                if ($("#FundPKFrom").data("kendoComboBox").value() != "") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            //$("#SwitchingFeePercent").data("kendoNumericTextBox").value(data.SwitchingFeePercent);
                            //$("#CashRefPK").data("kendoComboBox").value(data.DefaultBankSwitchingPK);
                            $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                            $("#NAVFundFrom").data("kendoNumericTextBox").value(0);
                            getCashRefPKByFundPKFrom(data.FundPK);
                            getFundRefByFundPK(data.FundPK);
                            ClientSwitchingRecalculate();
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }

                else {
                    getCashRefPKByFundPKFrom(0);
                    getFundRefByFundPK(data.FundPK);
                    $("#SwitchingFeePercent").data("kendoNumericTextBox").value(0);
                    //$("#CashRefPK").data("kendoComboBox").value("");
                    $("#CurrencyPK").data("kendoComboBox").value("");

                }

            }

            //ClientSwitchingRecalculate();
        }

        function onChangeFundPKTo() {
            $("#CashRefPKTo").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }


            else {

                if ($("#FundPKTo").data("kendoComboBox").value() != "") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKTo").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            //$("#SwitchingFeePercent").data("kendoNumericTextBox").value(data.SwitchingFeePercent);
                            //$("#CashRefPK").data("kendoComboBox").value(data.DefaultBankSwitchingPK);
                            $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                            $("#NAVFundTo").data("kendoNumericTextBox").value(0);
                            getCashRefPKByFundPKTo(data.FundPK);
                            ClientSwitchingRecalculate();
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }

            }



        }

        function setCmbFundPKFrom() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPKFrom == 0) {
                    return "";
                } else {
                    return dataItemX.FundPKFrom;
                }
            }
        }

        function setCmbFundPKTo() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPKTo == 0) {
                    return "";
                } else {
                    return dataItemX.FundPKTo;
                }
            }
        }

        function getFundRefByFundPK(_fundPK) {
            $.ajax({
                url: window.location.origin + "/Radsoft/SwitchingFund/GetFundRefByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#FundPKTo").kendoComboBox({
                        dataValueField: "FundPKTo",
                        dataTextField: "FundIDTo",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0,
                        change: onChangeFundPKTo,
                    });
                    getCashRefPKByFundPKTo(data[0].FundPKTo);


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            //function onChangeFundPKTo() {

            //    if (this.value() && this.selectedIndex == -1) {
            //        var dt = this.dataSource._data[0];
            //        this.text('');
            //    }
            //}


        }


        //Combo Box Cash Ref From
        if ($("#FundPKFrom").val() == "" || $("#FundPKFrom").val() == 0) {
            var _fundPK = 0
        }
        else {
            var _fundPK = dataItemX.FundPKFrom;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/RED",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashRefPKFrom").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCashRefPKFrom,
                    value: setCmbCashRefPKFrom()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCashRefPKFrom() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbCashRefPKFrom() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashRefPKFrom == 0) {
                    return "";
                } else {
                    return dataItemX.CashRefPKFrom;
                }
            }
        }
        function getCashRefPKByFundPKFrom(_fundPK) {
            $.ajax({
                url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/RED",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#CashRefPKFrom").kendoComboBox({
                        dataValueField: "FundCashRefPK",
                        dataTextField: "ID",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0,
                        change: onChangeCashRefPKFrom,
                        //value: setCmbCashRefPK()
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            function onChangeCashRefPKFrom() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
                else if ($("#FundPKFrom").data("kendoComboBox").value() != "" && $("#CashRefPKFrom").data("kendoComboBox").value() != "") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetRemarkByFundCashRefPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashRefPKFrom").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#Description").val(data);
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
            }
            //function setCmbCashRefPK() {
            //    if (e == null) {
            //        return "";
            //    } else {
            //        if (dataItemX.CashRefPK == 0) {
            //            return "";
            //        } else {
            //            return dataItemX.CashRefPK;
            //        }
            //    }
            //}
        }


        //Combo Box Cash Ref To
        if ($("#FundPKTo").val() == "" || $("#FundPKTo").val() == 0) {
            var _fundPK = 0
        }
        else {
            var _fundPK = dataItemX.FundPKTo;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashRefPKTo").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCashRefPKTo,
                    value: setCmbCashRefPKTo()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCashRefPKTo() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbCashRefPKTo() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashRefPKTo == 0) {
                    return "";
                } else {
                    return dataItemX.CashRefPKTo;
                }
            }
        }
        function getCashRefPKByFundPKTo(_fundPK) {
            $.ajax({
                url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#CashRefPKTo").kendoComboBox({
                        dataValueField: "FundCashRefPK",
                        dataTextField: "ID",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0,
                        change: onChangeCashRefPKTo,
                        //value: setCmbCashRefPK()
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            function onChangeCashRefPKTo() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

            }

        }

        //Combo Box Currency 
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCurrencyPK,
                    value: setCmbCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCurrencyPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.CurrencyPK;
                }
            }
        }


        //Combo Box Agent 
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeAgentPK,
                    value: setCmbAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAgentPK() {

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



        $("#NAVFundFrom").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNAVFundFrom(),
        });

        function setNAVFundFrom() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.NAVFundFrom == 0) {
                    return 0;
                } else {
                    return dataItemX.NAVFundFrom;
                }
            }
        }
        $("#NAVFundFrom").data("kendoNumericTextBox").enable(false);

        $("#NAVFundTo").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNAVFundTo(),
        });

        function setNAVFundTo() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.NAVFundTo;
            }

        }
        $("#NAVFundTo").data("kendoNumericTextBox").enable(false);

        $("#UnitPosition").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setUnitPosition(),
        });
        $("#UnitPosition").data("kendoNumericTextBox").enable(false);
        function setUnitPosition() {
            if (e == null) {
                return "";
            } else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/FundClient_GetUnitPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundPKFrom + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.FundClientPK,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#UnitPosition").data("kendoNumericTextBox").value(data);
                        getEstimatedCashProjection(data, dataItemX.FundPKFrom);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }



        $("#EstimatedCashProjection").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
        });

        $("#EstimatedCashProjection").data("kendoNumericTextBox").enable(false);




        $("#CashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            change: onchangeCashAmount,
            value: setCashAmount(),
        });
        $("#CashAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            }
        }
        function setCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.CashAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.CashAmount;
                }
            }
        }
        function onchangeCashAmount() {
            $("#UnitAmount").data("kendoNumericTextBox").value(0);
            ClientSwitchingRecalculate();

            if ($("#CashAmount").data("kendoNumericTextBox").value() < 100000000) {
                $("#TransferType").data("kendoComboBox").value(3);
            }
            else if ($("#CashAmount").data("kendoNumericTextBox").value() >= 100000000) {
                $("#TransferType").data("kendoComboBox").value(3);
            }
        }
        //$("#CashAmount").data("kendoNumericTextBox").enable(false);

        $("#UnitAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: onchangeUnitAmount,
            value: setUnitAmount(),
        });

        $("#UnitAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
            }
        }
        function setUnitAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.UnitAmount == 0) {
                    return "";
                } else {
                    return dataItemX.UnitAmount;
                }
            }
        }
        function onchangeUnitAmount() {
            $("#CashAmount").data("kendoNumericTextBox").value(0);
            ClientSwitchingRecalculate();
            GetTransferType($("#UnitAmount").data("kendoNumericTextBox").value());
        }
        $("#TotalCashAmountFundFrom").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTotalCashAmountFundFrom(),
        });
        $("#TotalCashAmountFundFrom").data("kendoNumericTextBox").enable(false);
        function setTotalCashAmountFundFrom() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TotalCashAmountFundFrom == 0) {
                    return 0;
                } else {
                    return dataItemX.TotalCashAmountFundFrom;
                }
            }
        }

        $("#TotalCashAmountFundTo").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTotalCashAmountFundTo(),
        });
        $("#TotalCashAmountFundTo").data("kendoNumericTextBox").enable(false);
        function setTotalCashAmountFundTo() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TotalCashAmountFundTo == 0) {
                    return 0;
                } else {
                    return dataItemX.TotalCashAmountFundTo;
                }
            }
        }



        $("#TotalUnitAmountFundFrom").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setTotalUnitAmountFundFrom(),
        });
        $("#TotalUnitAmountFundFrom").data("kendoNumericTextBox").enable(false);
        function setTotalUnitAmountFundFrom() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.TotalUnitAmountFundFrom;

            }
        }

        $("#TotalUnitAmountFundTo").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setTotalUnitAmountFundTo(),
        });
        $("#TotalUnitAmountFundTo").data("kendoNumericTextBox").enable(false);
        function setTotalUnitAmountFundTo() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.TotalUnitAmountFundTo;

            }
        }

        $("#SwitchingFeePercent").kendoNumericTextBox({
            format: "###.######### \\%",
            decimals: 8,
            value: setSwitchingFeePercent(),
            change: onChangeSwitchingFeePercent
        });

        $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(false);
            }
            else if (dataItemX.FeeTypeMethod == 1) {
                $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(true);
            }
            else if (dataItemX.FeeTypeMethod == 2) {
                $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        function setSwitchingFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeePercent;
                }
            }
        }

        $("#SwitchingFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            change: OnChangeSwitchingFeeAmount,
            value: setSwitchingFeeAmount(),
        });

        $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(false);
            }
            else if (dataItemX.FeeTypeMethod == 1) {
                $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(false);
            }
            else if (dataItemX.FeeTypeMethod == 2) {
                $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(true);
            }

        }
        //$("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(false);

        function setSwitchingFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeeAmount;
                }
            }
        }

        function OnChangeSwitchingFeeAmount() {
            $("#SwitchingFeePercent").data("kendoNumericTextBox").value(0);
            if ($("#FeeTypeMethod").data("kendoComboBox").value() == 2) {
                ClientSwitchingRecalculate();
            }
        }

        function onChangeSwitchingFeePercent() {
            $("#SwitchingFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeTypeMethod").data("kendoComboBox").value() == 1) {
                ClientSwitchingRecalculate();
            }
        }

        $("#FeeType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "OUT", value: "OUT" },
                { text: "IN", value: "IN" }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeFeeType,
            value: setCmbFeeType()
        });

        function OnChangeFeeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                ClientSwitchingRecalculate();
            }
        }

        function setCmbFeeType() {
            if (e == null) {
                return "OUT";
            } else {
                return dataItemX.FeeType;
            }
        }

        $("#BitSwitchingAll").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitSwitchingAll,
            value: setCmbBitSwitchingAll()
        });
        function OnChangeBitSwitchingAll() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            if ($("#BitSwitchingAll").val() == "true") {
                $("#UnitAmount").data("kendoNumericTextBox").value($("#UnitPosition").val());
                $("#CashAmount").data("kendoNumericTextBox").value(0);
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
                GetTransferType($("#UnitPosition").val());

            } else {
                $("#UnitAmount").data("kendoNumericTextBox").value(0);
                $("#CashAmount").data("kendoNumericTextBox").value(0);
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(true);
            }
            ClientSwitchingRecalculate();



        }

        function setCmbBitSwitchingAll() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitSwitchingAll;
            }
        }

        //combo box TransferType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransferTypeRedemption",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TransferType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTransferType,
                    value: setCmbTransferType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeTransferType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbTransferType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TransferType == 0) {
                    return "";
                } else {
                    return dataItemX.TransferType;
                }
            }
        }

        $("#FeeTypeMethod").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Percent", value: 1 },
                { text: "Amount", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeFeeTypeMethod,
            value: setCmbFeeTypeMethod()
        });
        function OnChangeFeeTypeMethod() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            } else {
                if ($("#FeeTypeMethod").val() == 1) {
                    $("#SwitchingFeeAmount").data("kendoNumericTextBox").value(0);
                    $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(false);
                    $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(true);

                } else {
                    $("#SwitchingFeePercent").data("kendoNumericTextBox").value(0);
                    $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(false);
                    $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(true);

                }
                ClientSwitchingRecalculate();

            }
        }

        function setCmbFeeTypeMethod() {
            if (e == null) {
                $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(false);
                return 1;
            } else {
                return dataItemX.FeeTypeMethod;
            }
        }


        if (dataItemX != null) {
            if (dataItemX.BitSwitchingAll == true) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            } else {
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(true);
            }
        }


        // Agent Trx

        $("#TrxCashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxCashAmount(),
        });

        function setTrxCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.CashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CashAmount;
                }
            }
        }
        $("#TrxCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxTotalCashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxTotalCashAmount(),
        });

        function setTrxTotalCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.TotalCashAmountFundFrom == 0) {
                    return "";
                } else {
                    return dataItemX.TotalCashAmountFundFrom;
                }
            }
        }
        $("#TrxTotalCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxSwitchingFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setTrxSwitchingFeePercent(),
        });
        function setTrxSwitchingFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeePercent;
                }
            }
        }

        $("#TrxSwitchingFeePercent").data("kendoNumericTextBox").enable(false);


        $("#TrxSwitchingFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxSwitchingFeeAmount(),
        });

        function setTrxSwitchingFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeeAmount;
                }
            }
        }
        $("#TrxSwitchingFeeAmount").data("kendoNumericTextBox").enable(false);

        // Agent Fee Trx

        $("#TrxFeeCashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxFeeCashAmount(),
        });

        function setTrxFeeCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.CashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CashAmount;
                }
            }
        }
        $("#TrxFeeCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxFeeTotalCashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxFeeTotalCashAmount(),
        });

        function setTrxFeeTotalCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.TotalCashAmountFundFrom == 0) {
                    return "";
                } else {
                    return dataItemX.TotalCashAmountFundFrom;
                }
            }
        }
        $("#TrxFeeTotalCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxFeeSwitchingFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setTrxFeeSwitchingFeePercent(),
        });
        function setTrxFeeSwitchingFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeePercent;
                }
            }
        }

        $("#TrxFeeSwitchingFeePercent").data("kendoNumericTextBox").enable(false);


        $("#TrxFeeSwitchingFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxFeeSwitchingFeeAmount(),
        });

        function setTrxFeeSwitchingFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SwitchingFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SwitchingFeeAmount;
                }
            }
        }
        $("#TrxFeeSwitchingFeeAmount").data("kendoNumericTextBox").enable(false);


        if (_GlobClientCode == '21') {
            initGridAgentTrx($("#ClientSwitchingPK").val());
            initGridAgentFeeTrx($("#ClientSwitchingPK").val());
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();

        }
        else {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).hide();
            $(GlobTabStrip.items()[2]).hide();

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

    function clearDataNav() {


        $("#NAVFundFrom").data("kendoNumericTextBox").value("0");
        $("#NAVFundTo").data("kendoNumericTextBox").value("0");
        $("#TotalUnitAmountFundFrom").data("kendoNumericTextBox").value("0");
        $("#TotalUnitAmountFundTo").data("kendoNumericTextBox").value("0");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#EstimatedCashProjection").data("kendoNumericTextBox").value("");
        $("#CashAmount").data("kendoNumericTextBox").value("");
        $("#UnitAmount").data("kendoNumericTextBox").value("");
        $("#UnitPosition").data("kendoNumericTextBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        //$("#CashAmount").data("kendoNumericTextBox").value("");
        //$("#SwitchingFeePercent").data("kendoNumericTextBox").value("");
        //$("#SwitchingFeeAmount").data("kendoNumericTextBox").value("");
        //$("#AgentFeeAmount").data("kendoNumericTextBox").value("");
        //$("#TotalCashAmountFundFrom").data("kendoNumericTextBox").value($("#CashAmount").data("kendoNumericTextBox").value());
        //$("#TotalCashAmountFundFrom").data("kendoNumericTextBox").value("");

    }

    function clearData() {
        $("#ReferenceSInvest").val("");
        $("#ClientSwitchingPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#NAVDate").data("kendoDatePicker").value(null);
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#PaymentDate").data("kendoDatePicker").value(null);
        $("#AgentPK").val("");
        $("#AgentID").val("");
        $("#NAVFundFrom").val("");
        $("#NAVFundTo").val("");
        $("#FundPKFrom").data("kendoComboBox").value("");
        $("#FundIDFrom").val("");
        $("#FundPKTo").data("kendoComboBox").value("");
        $("#FundIDTo").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#FundClientName").val("");
        $("#CashRefPKFrom").val("");
        $("#CashRefIDFrom").val("");
        $("#CashRefPKTo").val("");
        $("#CashRefIDTo").val("");
        $("#CurrencyPK").val("");
        $("#CurrencyID").val("");
        $("#TransferType").val("");
        $("#Description").val("");
        $("#CashAmount").val("");
        $("#UnitPosition").val("");
        $("#UnitAmount").val("");
        $("#TotalCashAmountFundFrom").val("");
        $("#TotalCashAmountFundTo").val("");
        $("#TotalUnitAmountFundFrom").val("");
        $("#TotalUnitAmountFundTo").val("");
        $("#SwitchingFeePercent").val("");
        $("#SwitchingFeeAmount").val("");
        $("#BitSwitchingAll").val("");
        $("#FeeTypeMethod").val("");
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

        $("#TrxCashAmount").val("");
        $("#TrxTotalCashAmount").val("");
        $("#TrxSwitchingFeePercent").val("");
        $("#TrxSwitchingFeeAmount").val("");

        $("#TrxFeeCashAmount").val("");
        $("#TrxFeeTotalCashAmount").val("");
        $("#TrxFeeSwitchingFeePercent").val("");
        $("#TrxFeeSwitchingFeeAmount").val("");

        $("#TransactionPK").val("");
        $("#SwitchingFeePercent").data("kendoNumericTextBox").enable(true);
        $("#SwitchingFeeAmount").data("kendoNumericTextBox").enable(true);
    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnUnApproved").show();
        $("#BtnOldData").show();


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
                aggregate: [{ field: "CashAmount", aggregate: "sum" },
                { field: "UnitAmount", aggregate: "sum" }],
                schema: {
                    model: {
                        fields: {
                            ClientSwitchingPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Selected: { type: "boolean" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            NAVDate: { type: "date" },
                            ValueDate: { type: "date" },
                            PaymentDate: { type: "date" },
                            NAVFundFrom: { type: "number" },
                            NAVFundTo: { type: "number" },
                            FundPKFrom: { type: "number" },
                            FundIDFrom: { type: "string" },
                            FundPKTo: { type: "number" },
                            FundIDTo: { type: "string" },
                            FundClientPK: { type: "number" },
                            FundClientID: { type: "string" },
                            FundClientName: { type: "string" },
                            CashRefPKFrom: { type: "number" },
                            CashRefIDFrom: { type: "string" },
                            CashRefPKTo: { type: "number" },
                            CashRefIDTo: { type: "string" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            AgentPK: { type: "number" },
                            AgentID: { type: "string" },
                            TransferType: { type: "number" },
                            TransferTypeDesc: { type: "string" },
                            Description: { type: "string" },
                            CashAmount: { type: "number" },
                            UnitAmount: { type: "number" },
                            UnitPosition: { type: "number" },
                            TotalCashAmountFundFrom: { type: "number" },
                            TotalCashAmountFundTo: { type: "number" },
                            TotalUnitAmountFundFrom: { type: "number" },
                            TotalUnitAmountFundTo: { type: "number" },
                            SwitchingFeePercent: { type: "number" },
                            SwitchingFeeAmount: { type: "number" },
                            BitSwitchingAll: { type: "boolean" },
                            FeeTypeMethod: { type: "number" },
                            FeeTypeMethodDesc: { type: "string" },
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
                            Timestamp: { type: "string" },
                            IFUACode: { type: "string" },
                            FrontID: { type: "string" },
                        }
                    }
                }
            });
    }

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            //grid filter
            var gridTesFilter = $("#gridClientSwitchingApproved").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            initGrid()


        }
        if (tabindex == 1) {
            
            var gridPending = $("#gridClientSwitchingPending").data("kendoGrid");
            //grid filter
            var gridTesFilter = $("#gridClientSwitchingPending").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            RecalGridPending();
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridClientSwitchingHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }



    function initGrid() {

        $("#gridClientSwitchingApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSwitchingApprovedURL = window.location.origin + "/Radsoft/ClientSwitching/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(ClientSwitchingApprovedURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientSwitchingApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                   //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 150 },
                   { field: "FrontID", title: "Front ID", width: 150 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;
        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientSwitchingApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },

                    { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                    { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundIDFrom", title: "Fund From", width: 95 },
                    { field: "FundNameFrom", title: "Fund Name From", width: 200 },
                    { field: "FundIDTo", title: "Fund To", width: 90 },
                    { field: "FundNameTo", title: "Fund Name To", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentID", title: "Agent", width: 150 },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                    { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;

        }
        else {
            var grid = $("#gridClientSwitchingApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                   //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                   {
                       headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                       template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                   },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                   { field: "TypeDesc", title: "Type", width: 85 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                   { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientSwitchingApproved").data("kendoGrid");
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


        //$("#showSelectionApproved").bind("click", function () {
        //    var checked = [];
        //    for (var i in checkedApproved) {
        //        if (checkedApproved[i]) {
        //            checked.push(i);
        //        }
        //    }
        //    console.log(checked + ' ' + checked.length);
        //});

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridClientSwitchingApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.ClientSwitchingPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var grid = $("#gridClientSwitchingApproved").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {

                if (checkedApproved[row.ClientSwitchingPK]) {
                    $('tr[data-uid="' + row.uid + '"] ')
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }

                if (row.Posted == false) {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
                } else if (row.Reversed == true && row.Posted == true) {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
                } else {
                    $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
                }


            });
        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabClientSwitching").kendoTabStrip({
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
        $("#BtnGetNavBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();
        $("#BtnSInvestSwitchingRptBySelected").show();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchFormBySelected").show();
        $("#BtnSInvestSwitchingRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();
    }


    function RecalGridPending() {
        $("#gridClientSwitchingPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSwitchingPendingURL = window.location.origin + "/Radsoft/ClientSwitching/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(ClientSwitchingPendingURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientSwitchingPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                   //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                   {
                       headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                       template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                   },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 150 },
                   { field: "FrontID", title: "Front ID", width: 150 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;

        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientSwitchingPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                    { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundIDFrom", title: "Fund From", width: 95 },
                    { field: "FundNameFrom", title: "Fund Name From", width: 200 },
                    { field: "FundIDTo", title: "Fund To", width: 90 },
                    { field: "FundNameTo", title: "Fund Name To", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentID", title: "Agent", width: 150 },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                    { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;
        }
        else {
            var grid = $("#gridClientSwitchingPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                   //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },

                     {
                         headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                         template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                     },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                   { field: "TypeDesc", title: "Type", width: 85 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                   { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientSwitchingPending").data("kendoGrid");
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


        //$("#showSelectionPending").bind("click", function () {
        //    var checked = [];
        //    for (var i in checkedPending) {
        //        if (checkedPending[i]) {
        //            checked.push(i);
        //        }
        //    }
        //    console.log(checked + ' ' + checked.length);
        //});

        function selectRowPending() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridClientSwitchingPending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.ClientSwitchingPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].ClientSwitchingPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxPending")
                        .attr("checked", "checked");
                }
            }
        }


        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnGetNavBySelected").hide();
        $("#BtnBatchFormBySelected").show();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnSInvestSwitchingRptBySelected").hide();

    }


    function RecalGridHistory() {

        $("#gridClientSwitchingHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSwitchingHistoryURL = window.location.origin + "/Radsoft/ClientSwitching/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(ClientSwitchingHistoryURL);

        }


        if (_GlobClientCode == '10') {
            $("#gridClientSwitchingHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 150 },
                   { field: "FrontID", title: "Front ID", width: 150 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else if (_GlobClientCode == '24') {
            $("#gridClientSwitchingHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 85 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                    { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundIDFrom", title: "Fund From", width: 95 },
                    { field: "FundNameFrom", title: "Fund Name From", width: 200 },
                    { field: "FundIDTo", title: "Fund To", width: 90 },
                    { field: "FundNameTo", title: "Fund Name To", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentID", title: "Agent", width: 150 },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                    { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else {
            $("#gridClientSwitchingHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Switching"
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
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ClientSwitchingPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                   { field: "TypeDesc", title: "Type", width: 85 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 190 },
                   { field: "NAVDate", title: "NAV Date", width: 100, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "PaymentDate", title: "Payment Date", width: 110, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "FundIDFrom", title: "Fund From", width: 95 },
                   { field: "FundIDTo", title: "Fund To", width: 90 },
                   { field: "CashAmount", title: "Cash Amount", width: 125, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 125, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "TotalCashAmountFundTo", title: "Total Cash Amount Fund To", width: 130, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "TotalUnitAmountFundTo", title: "Total Unit Amount Fund To", width: 130, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeePercent", title: "Switch.Fee (%)", template: "#: SwitchingFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switch.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundFrom", title: "NAV Fund From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "NAVFundTo", title: "NAV Fund To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentID", title: "Agent", width: 150 },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefIDFrom", title: "Cash Ref From", width: 200 },
                   { field: "CashRefIDTo", title: "Cash Ref To", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeTypeMethod", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeMethodDesc", title: "Fee Type Method", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "BitSwitchingAll", title: "Switching All", width: 200, template: "#= BitSwitchingAll ? 'Yes' : 'No' #" },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                   { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientSwitchingHistory").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourceHistory.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end filter


        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnGetNavBySelected").hide();
        $("#BtnBatchFormBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnSInvestSwitchingRptBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridClientSwitchingHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            }
            else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
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

        //validation yg gk bsa lanjut, harus dikelarin dlu
        if ($('#BitSwitchingAll').val() == "false") {
            $.ajax({
                url: window.location.origin + "/Radsoft/ClientSwitching/CheckMinimumBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CashAmount').val() + "/" + $('#UnitAmount').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPKFrom').val() + "/" + $('#FundClientPK').val(),
                type: 'GET',
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        alertify.alert("Remaining Balance Unit > Unit Amount");
                        return;
                    }
                }
            });

            $.ajax({
                url: window.location.origin + "/Radsoft/FundClientPosition/ValidateCashAmountByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPKFrom').val() + "/" + $('#FundClientPK').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if ((parseFloat($("#CashAmount").val() - data)) > 0 && $("#CashAmount").val() > 0) {
                        alertify.alert("Save Cancel, Cash Amount > Cash Position");
                        return;
                    }
                }
            });

            $.ajax({
                url: window.location.origin + "/Radsoft/Host/GetUnitAmountByFundPKandFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").data("kendoComboBox").value() + "/" + $("#FundClientPK").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var _unitAmount = 0;
                    _unitAmount = $("#UnitAmount").data("kendoNumericTextBox").value();

                    if ((parseFloat(data - _unitAmount)) < 0) {
                        alertify.alert("Unit Amount in FundClientPosition is lower than Unit Amount in this Redemption");
                        return;
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

        //if ($('#TotalCashAmountFundFrom').val() == 0 && $('#TotalUnitAmountFundFrom').val() == 0) {
        //    alertify.alert("Please Recalculate / Check Total Cash Amount / Total Unit Amount First !");
        //    return;
        //}

        if ($("#CashAmount").val() == 0 && $("#UnitAmount").val() == 0) {
            alertify.alert("Please Recalculate / Check Cash Amount / Unit Amount First !");
            return;
        }

        var _reference;
        if (_GlobClientCode == "20") {
            $.ajax({
                url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SWI/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    _reference = data;
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        else {
            _reference = $('#ReferenceSInvest').val();
        }



        var val = validateData();
        if (val == 1) {
            var ValidateClientSwitching = {
                ValueDate: $('#ValueDate').val(),
                FundPKFrom: $('#FundPKFrom').val(),
                FundPKTo: $('#FundPKTo').val(),
                FundClientPK: $('#FundClientPK').val(),
                CashAmount: $('#CashAmount').val(),
                UnitAmount: $('#UnitAmount').val(),
                BitSwitchingAll: $('#BitSwitchingAll').val()
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/ClientSwitching/ValidateClientSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(ValidateClientSwitching),
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    if (data[0] != undefined) {
                        initGridValidation();
                    }
                    else {
                        alertify.confirm("Are you sure to add data? ", function (e) {
                            if (e) {

                                //insert into clientswitching
                                var ClientSwitching = {
                                    ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                    Type: $('#Type').val(),
                                    NAVDate: $('#NAVDate').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    PaymentDate: $('#PaymentDate').val(),
                                    NAVFundFrom: $('#NAVFundFrom').val(),
                                    NAVFundTo: $('#NAVFundTo').val(),
                                    FundPKFrom: $('#FundPKFrom').val(),
                                    FundPKTo: $('#FundPKTo').val(),
                                    AgentPK: $('#AgentPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    CashRefPKFrom: $('#CashRefPKFrom').val(),
                                    CashRefPKTo: $('#CashRefPKTo').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    TransferType: $('#TransferType').val(),
                                    Description: $('#Description').val(),
                                    ReferenceSInvest: _reference,
                                    CashAmount: $('#CashAmount').val(),
                                    UnitAmount: $('#UnitAmount').val(),
                                    TotalCashAmountFundFrom: $('#TotalCashAmountFundFrom').val(),
                                    TotalCashAmountFundTo: $('#TotalCashAmountFundTo').val(),
                                    TotalUnitAmountFundFrom: $('#TotalUnitAmountFundFrom').val(),
                                    TotalUnitAmountFundTo: $('#TotalUnitAmountFundTo').val(),
                                    SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                    SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),
                                    FeeType: $('#FeeType').val(),
                                    BitSwitchingAll: $('#BitSwitchingAll').val(),
                                    FeeTypeMethod: $('#FeeTypeMethod').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_I",
                                    type: 'POST',
                                    data: JSON.stringify(ClientSwitching),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (_GlobClientCode == '21') {
                                            $("#ClientSwitchingPK").val(data.ClientSwitchingPK);
                                            $("#HistoryPK").val(data.HistoryPK);
                                            $("#StatusHeader").val("PENDING");
                                            alertify.alert("Insert Client Switching Success");
                                            $("#BtnAdd").hide();
                                            $("#BtnUpdate").show();
                                        }
                                        else {
                                            alertify.alert(data);
                                            win.close();
                                        }
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {

            if ($("#CashAmount").val() == 0 && $("#UnitAmount").val() == 0) {
                alertify.alert("Please Recalculate / Check Cash Amount / Unit Amount First !");
                return;
            }



            if ($('#BitSwitchingAll').val() == "true") {
                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                    _trxPK = '';
                                    _trxUserPK = '';
                                    if ($('#TransactionPK').val() != null) {
                                        _trxPK = $('#TransactionPK').val();
                                    }
                                    if ($("#UserSwitchingPK").val() != null) {
                                        _trxUserPK = $('#UserSwitchingPK').val();
                                    }
                                    var ClientSwitching = {
                                        ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                        Type: $('#Type').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        NAVDate: $('#NAVDate').val(),
                                        ValueDate: $('#ValueDate').val(),
                                        PaymentDate: $('#PaymentDate').val(),
                                        NAVFundFrom: $('#NAVFundFrom').val(),
                                        NAVFundTo: $('#NAVFundTo').val(),
                                        FundPKFrom: $('#FundPKFrom').val(),
                                        FundPKTo: $('#FundPKTo').val(),
                                        AgentPK: $('#AgentPK').val(),
                                        FundClientPK: $('#FundClientPK').val(),
                                        CashRefPKFrom: $('#CashRefPKFrom').val(),
                                        CashRefPKTo: $('#CashRefPKTo').val(),
                                        CurrencyPK: $('#CurrencyPK').val(),
                                        TransferType: $('#TransferType').val(),
                                        Description: $('#Description').val(),
                                        ReferenceSInvest: $("#ReferenceSInvest").val(),
                                        CashAmount: $('#CashAmount').val(),
                                        UnitAmount: $('#UnitAmount').val(),
                                        TotalCashAmountFundFrom: $('#TotalCashAmountFundFrom').val(),
                                        TotalCashAmountFundTo: $('#TotalCashAmountFundTo').val(),
                                        TotalUnitAmountFundFrom: $('#TotalUnitAmountFundFrom').val(),
                                        TotalUnitAmountFundTo: $('#TotalUnitAmountFundTo').val(),
                                        SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                        SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),
                                        FeeType: $('#FeeType').val(),
                                        BitSwitchingAll: $('#BitSwitchingAll').val(),
                                        FeeTypeMethod: $('#FeeTypeMethod').val(),
                                        TransactionPK: _trxPK,
                                        UserSwitchingPK: _trxUserPK,
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_U",
                                        type: 'POST',
                                        data: JSON.stringify(ClientSwitching),
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
            else {
                //if (($('#TotalUnitAmountFundFrom').val() != 0) || ($('#TotalCashAmountFundFrom').val() != 0)) {
                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                    _trxPK = '';
                                    _trxUserPK = '';
                                    if ($('#TransactionPK').val() != null) {
                                        _trxPK = $('#TransactionPK').val();
                                    }
                                    if ($("#UserSwitchingPK").val() != null) {
                                        _trxUserPK = $('#UserSwitchingPK').val();
                                    }
                                    if ($("#CashAmount").val() != 0 || $("#UnitAmount").val() != 0) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClientPosition/ValidateCashAmountByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPKFrom').val() + "/" + $('#FundClientPK').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if ((parseFloat($("#CashAmount").val() - data)) > 0 && $("#CashAmount").val() > 0 && $("#UnitAmount").val() != 0) {
                                                    alertify.alert("Save Cancel, Cash Amount > Cash Position");
                                                    return;
                                                }
                                                else {

                                                    var ClientSwitching = {
                                                        ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                                        Type: $('#Type').val(),
                                                        HistoryPK: $('#HistoryPK').val(),
                                                        NAVDate: $('#NAVDate').val(),
                                                        ValueDate: $('#ValueDate').val(),
                                                        PaymentDate: $('#PaymentDate').val(),
                                                        NAVFundFrom: $('#NAVFundFrom').val(),
                                                        NAVFundTo: $('#NAVFundTo').val(),
                                                        FundPKFrom: $('#FundPKFrom').val(),
                                                        FundPKTo: $('#FundPKTo').val(),
                                                        AgentPK: $('#AgentPK').val(),
                                                        FundClientPK: $('#FundClientPK').val(),
                                                        CashRefPKFrom: $('#CashRefPKFrom').val(),
                                                        CashRefPKTo: $('#CashRefPKTo').val(),
                                                        CurrencyPK: $('#CurrencyPK').val(),
                                                        TransferType: $('#TransferType').val(),
                                                        Description: $('#Description').val(),
                                                        ReferenceSInvest: $("#ReferenceSInvest").val(),
                                                        CashAmount: $('#CashAmount').val(),
                                                        UnitAmount: $('#UnitAmount').val(),
                                                        TotalCashAmountFundFrom: $('#TotalCashAmountFundFrom').val(),
                                                        TotalCashAmountFundTo: $('#TotalCashAmountFundTo').val(),
                                                        TotalUnitAmountFundFrom: $('#TotalUnitAmountFundFrom').val(),
                                                        TotalUnitAmountFundTo: $('#TotalUnitAmountFundTo').val(),
                                                        SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                                        SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),
                                                        FeeType: $('#FeeType').val(),
                                                        BitSwitchingAll: $('#BitSwitchingAll').val(),
                                                        FeeTypeMethod: $('#FeeTypeMethod').val(),
                                                        TransactionPK: _trxPK,
                                                        UserSwitchingPK: _trxUserPK,
                                                        Notes: str,
                                                        EntryUsersID: sessionStorage.getItem("user")
                                                    };
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_U",
                                                        type: 'POST',
                                                        data: JSON.stringify(ClientSwitching),
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

                //}
                //else {
                //    alertify.alert("Please Recalculate / Check Total Cash Amount / Total Unit Amount First !")
                //}
            }

        }

    });


    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ClientSwitching" + "/" + $("#ClientSwitchingPK").val(),
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
        
        //if ($("#NAV").val() == 0 || $("#NAV").val() == null || $("#NAV").val() == "") {
        //    alertify.alert("NAV Must Ready First, Please Click Get NAV");
        //    return;
        //}

        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientSwitching = {
                                ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_A",
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
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

    $("#BtnUnApproveBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }

        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        if (stringClientSwitchingSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
            if (e) {
                var ClientSwitching = {
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/ValidateUnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();

                                    for (var i in checkedApproved) {
                                        checkedApproved[i] = null
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Client Switching Already Posted");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });
    });

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientSwitching = {
                                ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_V",
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
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

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientSwitching = {
                                ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_R",
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
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

    function getDataSourceListFundClient(_url) {



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
                         alertify.alert("Please Fill NAV Date and Fund First!");
                         WinListFundClient.close();
                     },
                     pageSize: 100,
                     schema: {
                         model: {
                             fields: {
                                 FundClientPK: { type: "number" },
                                 FundClientName: { type: "string" },
                                 UnitAmount: { type: "number" },
                                 ID: { type: "string" },
                                 IFUA: { type: "string" },
                             }
                         }
                     }
                 });



    }

    $("#btnListFundClientPK").click(function () {
        initListFundClientPK();

        WinListFundClient.center();
        WinListFundClient.open();
        htmlFundClientPK = "#FundClientPK";
        htmlFundClientID = "#FundClientID";



    });

    function initListFundClientPK() {
        if ($("#FundPKFrom").data("kendoComboBox").value() == "" || $("#FundPKFrom").data("kendoComboBox").value() == null) {
            alertify.alert("Please Input FundFrom First");
            WinListFundClient.close();
            return;
        }
        if ($("#NAVDate").data("kendoDatePicker").value() == "" || $("#NAVDate").data("kendoDatePicker").value() == null) {
            alertify.alert("Please Input NAV Date First");
            WinListFundClient.close();
            return;
        }
        var _url = window.location.origin + "/Radsoft/FundClient/GetFundClientFromFundClientPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").data("kendoComboBox").value() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy");
        var dsListFundClient = getDataSourceListFundClient(_url);
        $("#gridListFundClient").kendoGrid({
            dataSource: dsListFundClient,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListFundClientSelect }, title: " ", width: 85 },
               { field: "FundClientPK", title: "Client Name", hidden: true, width: 300 },
               //{ field: "FundClientName", title: "Client Name", width: 300 },
               { field: "ID", title: "Client ID", width: 300 },
               { field: "IFUA", title: "IFUA", width: 300 },
               { field: "UnitAmount", title: "Unit", width: 200, format: "{0:n4}" }

            ]
        });
    }


    function ListFundClientSelect(e) {
        var grid = $("#gridListFundClient").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $.ajax({
            url: window.location.origin + "/Radsoft/HighRiskMonitoring/DormantValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckClientSuspend/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundClient/CheckClientCantSwitch/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {

                                            if (_GlobClientCode == "20") {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/ValidationHighRiskNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK + "/" + 2,
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if (data == 1)
                                                            alertify.alert("Fund Client is on High Risk Monitoring(KYC,PPATK)!");
                                                        else if (data == 2)
                                                            alertify.alert("IFUA/SID is empty, please check data Fund Client!");
                                                        else {
                                                            clearDataNav();
                                                            $("#FundClientID").val(dataItemX.FundClientName);
                                                            $("#UnitPosition").data("kendoNumericTextBox").value(dataItemX.UnitAmount);
                                                            $("#UnitAmount").data("kendoNumericTextBox").value(0);
                                                            $("#CashAmount").data("kendoNumericTextBox").value(0);
                                                            $("#TotalUnitAmountFundFrom").data("kendoNumericTextBox").value(0);
                                                            $("#TotalUnitAmountFundTo").data("kendoNumericTextBox").value(0);
                                                            $("#TotalCashAmountFundFrom").data("kendoNumericTextBox").value(0);
                                                            $("#TotalCashAmountFundTo").data("kendoNumericTextBox").value(0);
                                                            $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                            getAgentByFundClientPK();
                                                            getEstimatedCashProjection(dataItemX.UnitAmount, $('#FundPKFrom').val());
                                                            WinListFundClient.close();

                                                        }
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                clearDataNav();
                                                $("#FundClientID").val(dataItemX.FundClientName);
                                                $("#UnitPosition").data("kendoNumericTextBox").value(dataItemX.UnitAmount);
                                                $("#UnitAmount").data("kendoNumericTextBox").value(0);
                                                $("#CashAmount").data("kendoNumericTextBox").value(0);
                                                $("#TotalUnitAmountFundFrom").data("kendoNumericTextBox").value(0);
                                                $("#TotalUnitAmountFundTo").data("kendoNumericTextBox").value(0);
                                                $("#TotalCashAmountFundFrom").data("kendoNumericTextBox").value(0);
                                                $("#TotalCashAmountFundTo").data("kendoNumericTextBox").value(0);
                                                $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                getAgentByFundClientPK();
                                                getEstimatedCashProjection(dataItemX.UnitAmount, $('#FundPKFrom').val());
                                                WinListFundClient.close();

                                            }


                                        } else {
                                            alertify.alert("Fund Client Can't Switching!");
                                        }
                                    }
                                });

                            } else {
                                alertify.alert("Fund Client is Suspend!");
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                } else {
                    alertify.alert("This is dormant client");
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }



    $("#BtnRevise").click(function (e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
        }

        alertify.confirm("Are you sure want to Revise Client Switching?", function (e) {
            if ($("#State").text() == "Revised") {
                alert("Client Switching Already Posted / Revised, Revise Cancel");
            } else {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSwitchingPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSwitching",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                //$.ajax({
                                //    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                //    type: 'GET',
                                //    contentType: "application/json;charset=utf-8",
                                //    success: function (data) {
                                //        if (data == false) {
                                var ClientSwitching = {
                                    ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                                    HistoryPK: $('#HistoryPK').val()
                                };
                                $.blockUI({});
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_Revise",
                                    type: 'POST',
                                    data: JSON.stringify(ClientSwitching),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (_RDOEnable == true) {
                                            $.ajax({
                                                url: "http://" + _GlobUrlServerRDOApi + "transaction/revise?id=" + $("#TransactionPK").val(),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    console.log(data);
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                }
                                            });
                                        }
                                        $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                                        $("#State").text("Revised");
                                        $("#RevisedBy").text(sessionStorage.getItem("user"));
                                        $("#Revised").prop('checked', true);
                                        $("#Posted").prop('checked', false);
                                        alertify.alert(data);
                                        refresh();
                                        $.unblockUI();

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClientPosition/UpdateAvgAfterRevise/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/3/" + $('#ClientSwitchingPK').val() + "/" + $('#HistoryPK').val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                console.log(data);
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });
                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data.responseText);
                                    }
                                });
                                //        } else {
                                //            alertify.alert("Please check today/yesterday EndDayTrails!");
                                //            win.close();
                                //            refresh();
                                //        }
                                //    },
                                //    error: function (data) {
                                //        alertify.alert(data.responseText);
                                //    }
                                //});
                            } else {
                                $.unblockUI();
                                alertify.alert("Data has been Changed by other user, Please check it first!");

                            }
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);


                        }
                    });
                }
            }
        });
    });

    $("#BtnRecalculate").click(function (e) {
        ClientSwitchingRecalculate();
    });

    function ClientSwitchingRecalculate() {
        if ($("#NAVDate").val() == null || $("#NAVDate").val() == "") {
            alertify.alert("Please Fill NAV Date First");
            return;
        }
        else if ($("#FundPKFrom").val() == null || $("#FundPKFrom").val() == "") {
            //alertify.alert("Please Fill FundFrom First");
            return;
        }
        else if ($("#FundPKTo").val() == null || $("#FundPKTo").val() == "") {
            // alertify.alert("Please Fill FundTo First");
            return;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _feepercent = 0;
                _feepercent = $('#SwitchingFeePercent').val();
                if ((parseFloat(data.SwitchingFeePercent - _feepercent)) >= 0) {
                    var ParamClientSwitchingRecalculate = {
                        ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                        FundPKFrom: $('#FundPKFrom').val(),
                        FundPKTo: $('#FundPKTo').val(),
                        NavDate: kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        CashAmount: $('#CashAmount').val(),
                        UnitAmount: $('#UnitAmount').val(),
                        SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                        SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),
                        FeeType: $('#FeeType').val(),
                        FeeTypeMode: $('#FeeTypeMethod').val(),
                        UpdateUsersID: sessionStorage.getItem("user"),
                        BitSwitchingAll: $('#BitSwitchingAll').val(),
                        LastUpdate: kendo.toString($("#LastUpdate").data("kendoDatePicker").value(), "MM-dd-yy HH:mm:ss")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSwitching/ClientSwitchingRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ParamClientSwitchingRecalculate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAVFundFrom").data("kendoNumericTextBox").value(data.NavFundFrom);
                            $("#NAVFundTo").data("kendoNumericTextBox").value(data.NavFundTo);
                            $("#CashAmount").data("kendoNumericTextBox").value(data.CashAmount);
                            $("#UnitAmount").data("kendoNumericTextBox").value(data.UnitAmount);
                            //$("#SwitchingFeePercent").data("kendoNumericTextBox").value(data.SwitchingFeePercent);
                            $("#SwitchingFeeAmount").data("kendoNumericTextBox").value(data.SwitchingFeeAmount);
                            $("#TotalCashAmountFundFrom").data("kendoNumericTextBox").value(data.TotalCashAmountFundFrom);
                            $("#TotalCashAmountFundTo").data("kendoNumericTextBox").value(data.TotalCashAmountFundTo);
                            $("#TotalUnitAmountFundFrom").data("kendoNumericTextBox").value(data.TotalUnitAmountFundFrom);
                            $("#TotalUnitAmountFundTo").data("kendoNumericTextBox").value(data.TotalUnitAmountFundTo);

                            if (_GlobClientCode == '21') {
                                $("#TrxCashAmount").data("kendoNumericTextBox").value($('#CashAmount').val());
                                $("#TrxTotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmountFundFrom);
                                $("#TrxSwitchingFeePercent").data("kendoNumericTextBox").value(data.SwitchingFeePercent);
                                $("#TrxSwitchingFeeAmount").data("kendoNumericTextBox").value(data.SwitchingFeeAmount);

                                $("#TrxFeeCashAmount").data("kendoNumericTextBox").value($('#CashAmount').val());
                                $("#TrxFeeTotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmountFundFrom);
                                $("#TrxFeeSwitchingFeePercent").data("kendoNumericTextBox").value(data.SwitchingFeePercent);
                                $("#TrxFeeSwitchingFeeAmount").data("kendoNumericTextBox").value(data.SwitchingFeeAmount);

                            }
                            //refresh();
                            //alertify.alert("Recalculate Done");
                            //$.ajax({
                            //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").data("kendoComboBox").value() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                            //    type: 'GET',
                            //    contentType: "application/json;charset=utf-8",
                            //    success: function (data) {
                            //        $("#LastUpdate").text(kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss'));
                            //        $("#LastUpdate").val(kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss'));
                            //        alertify.alert("Recalculate Done");
                            //    },
                            //    error: function (data) {
                            //        alertify.alert(data.responseText);
                            //    }
                            //});
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                } else {
                    alertify.alert("Switching Fee Percent > Max Switching Fee Percent in this Fund");
                    $.unblockUI();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

   $("#BtnBatchFormBySelected").click(function (e) {

       var stringClientSwitchingSelected = '';
       var AllPending = 0;
       AllPending = [];
       for (var i in checkedPending) {
           if (checkedPending[i]) {
               AllPending.push(i);
           }
       }

       var ArrayPendingFundFrom = AllPending;
       var stringClientSwitchingSelectedPending = '';

       for (var i in ArrayPendingFundFrom) {
           stringClientSwitchingSelectedPending = stringClientSwitchingSelectedPending + ArrayPendingFundFrom[i] + ',';

       }
       stringClientSwitchingSelectedPending = stringClientSwitchingSelectedPending.substring(0, stringClientSwitchingSelectedPending.length - 1)


       var AllApproved = 0;
       AllApproved = [];
       for (var i in checkedApproved) {
           if (checkedApproved[i]) {
               AllApproved.push(i);
           }
       }

       var ArrayApprovedFundFrom = AllApproved;
       var stringClientSwitchingSelectedApproved = '';

       for (var i in ArrayApprovedFundFrom) {
           stringClientSwitchingSelectedApproved = stringClientSwitchingSelectedApproved + ArrayApprovedFundFrom[i] + ',';

       }
       stringClientSwitchingSelectedApproved = stringClientSwitchingSelectedApproved.substring(0, stringClientSwitchingSelectedApproved.length - 1)


       if (tabindex == 0 || tabindex == undefined) {
           if (stringClientSwitchingSelectedApproved == "") {
               alertify.alert("There's No Selected Data")
               return;
           }
           stringClientSwitchingSelected = stringClientSwitchingSelectedApproved;

       }
       else if (tabindex = 1) {
           if (stringClientSwitchingSelectedPending == "") {
               alertify.alert("There's No Selected Data")
               return;
           }
           stringClientSwitchingSelected = stringClientSwitchingSelectedPending;

       }


        alertify.confirm("Are you sure want to Batch Form by Selected Data ?", function (e) {
            if (e) {
                var _url = "";
                var Batch = {
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                    Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                    Signature3Desc: $("#Signature1").data("kendoComboBox").text(),
                    Signature4Desc: $("#Signature2").data("kendoComboBox").text(),
                    DownloadMode: $('#DownloadMode').val(),
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };

                if (_GlobClientCode == "10") {
                    _url = window.location.origin + "/Radsoft/ClientSwitching/BatchFormBySelectedDataMandiri/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                }
                else {
                    _url = window.location.origin + "/Radsoft/ClientSwitching/BatchFormBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                }
                $.ajax({
                    url: _url,
                    type: 'POST',
                    data: JSON.stringify(Batch),
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

   $("#BtnApproveBySelected").click(function (e) {
       var All = 0;
       All = [];
       for (var i in checkedPending) {
           if (checkedPending[i]) {
               All.push(i);
           }
       }

       var ArrayFundFrom = All;
       var stringClientSwitchingSelected = '';

       for (var i in ArrayFundFrom) {
           stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

       }
       stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)


       if (stringClientSwitchingSelected == "") {
           alertify.alert("There's No Selected Data")
           return;
       }
       alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
           if (e) {
               var ClientSwitching = {
                   DateFrom: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                   DateTo: kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                   UnitRegistrySelected: stringClientSwitchingSelected,
                   UnitRegistryType: "ClientSwitching",
               };
               if (_GlobClientCode == "21") {
                   var ClientSwitching = {
                       UnitRegistrySelected: stringClientSwitchingSelected,
                   };
                   $.ajax({
                       url: window.location.origin + "/Radsoft/ClientSwitching/ValidateCheckClientAPERD/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSwitching",
                       type: 'POST',
                       data: JSON.stringify(ClientSwitching),
                       contentType: "application/json;charset=utf-8",
                       success: function (data) {
                           console.log(data);
                           if (data == "") {
                               var ClientSwitching = {
                                   UnitRegistrySelected: stringClientSwitchingSelected,
                               };
                               $.ajax({
                                   url: window.location.origin + "/Radsoft/ClientSwitching/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                   type: 'POST',
                                   data: JSON.stringify(ClientSwitching),
                                   contentType: "application/json;charset=utf-8",
                                   success: function (data) {
                                       alertify.alert(data);
                                       refresh();

                                       for (var i in checkedPending) {
                                           checkedPending[i] = null
                                       }
                                   },
                                   error: function (data) {
                                       alertify.alert(data.responseText);
                                   }
                               });
                           } else {
                               alertify.alert(data);
                           }
                       },
                       error: function (data) {
                           alertify.alert(data.responseText);
                       }
                   });
               }
               else {
                   $.ajax({
                       url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoffBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                       type: 'POST',
                       data: JSON.stringify(ClientSwitching),
                       contentType: "application/json;charset=utf-8",
                       success: function (data) {
                           if (data == false) {
                               $.ajax({
                                   url: window.location.origin + "/Radsoft/ClientSwitching/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                   type: 'POST',
                                   data: JSON.stringify(ClientSwitching),
                                   contentType: "application/json;charset=utf-8",
                                   success: function (data) {
                                       alertify.alert(data);
                                       refresh();

                                       for (var i in checkedPending) {
                                           checkedPending[i] = null
                                       }
                                   },
                                   error: function (data) {
                                       alertify.alert(data.responseText);
                                   }
                               });
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

           }
       });
   });

    $("#BtnRejectBySelected").click(function (e) { //Done
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)


        if (stringClientSwitchingSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {
                var ClientSwitching = {
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/ValidateCheckDescription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSwitching",
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();


                                    for (var i in checkedPending) {
                                        checkedPending[i] = null
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
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
    }); //Done

    $("#BtnPostingBySelected").click(function (e) { //Tolong Di Check
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        if (stringClientSwitchingSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                if (kendo.toString($("#DateFrom").data("kendoDatePicker").value(), 'MM-dd-yy') == kendo.toString($("#DateTo").data("kendoDatePicker").value(), 'MM-dd-yy')) {
                    $.blockUI();
                    var ClientSwitching = {
                        UnitRegistrySelected: stringClientSwitchingSelected,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSwitching/ValidateApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(ClientSwitching),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSwitching/ValidatePostingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == 1) {
                                            alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Switching Already Posting");
                                            $.unblockUI();
                                        }
                                        else if (data == 2) {
                                            alertify.alert("Posting Cancel, Date is Holiday !");
                                            $.unblockUI();

                                        } else {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSwitching/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                data: JSON.stringify(ClientSwitching),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    $.unblockUI();
                                                    alertify.alert(data);
                                                    refresh();

                                                    for (var i in checkedApproved) {
                                                        checkedApproved[i] = null
                                                    }

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/FundClientPosition/UpdateAvgAfterPosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {

                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                            $.unblockUI();
                                                        }
                                                    });
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });

                            } else {
                                alertify.alert("NAV or Total Cash Amount Not Ready, Please Click Get NAV");
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
                    alertify.alert("Please Posting Client Switching by Day");
                    $.unblockUI();
                }

            }
        });
    });

    $("#BtnReviseBySelected").click(function (e) { //Tolong Di Check
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        alertify.confirm("Are you sure want to Revise by Selected Data ?", function (e) {
            if (e) {
                if ($("#DateFrom").data("kendoDatePicker").value() == $("#DateTo").data("kendoDatePicker").value()) {
                    $.blockUI();
                    var ClientSwitching = {
                        ClientSwitchingSelected: stringClientSwitchingSelected,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSwitching/ReviseBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(ClientSwitching),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                         
                }
                else {
                    alertify.alert("Please Revise Client Switching by Day");
                    $.unblockUI();
                }

            }
        });
    }); //Tolong Di Check


    $("#BtnVoidBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        if (stringClientSwitchingSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                var ClientSwitching = {
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/ValidateVoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSwitching/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSwitching),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();

                                    for (var i in checkedApproved) {
                                        checkedApproved[i] = null
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Client Switching Already Posted");
               
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
               
                    }
                });
            }
        });
    }); //Done
    $("#BtnGetNavBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        if (stringClientSwitchingSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Get Nav by Selected Data ?", function (e) {
            if (e) {
                var ClientSwitching = {
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/GetNavBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/UnitRegistry_GetNavBySelected",
                    
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();

                        for (var i in checkedApproved) {
                            checkedApproved[i] = null
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    }); //Done
    $("#BtnSInvestSwitchingRptBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSwitchingSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSwitchingSelected = stringClientSwitchingSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSwitchingSelected = stringClientSwitchingSelected.substring(0, stringClientSwitchingSelected.length - 1)

        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                var ClientSwitching = {
                    UnitRegistrySelected: stringClientSwitchingSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/SInvestSwitchingRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadRadsoftFile").attr("href", data);
                        $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_TrxSwitching.txt");
                        document.getElementById("downloadRadsoftFile").click();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    function _setPaymentDate() {
        if (_GlobClientCode == "02") {
            if ($("#FundPKFrom").val() == 3) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
            else {
                if ($("#FundPKFrom").val() == "" || $("#FundPKFrom").val() == 0) {
                    //Cek WorkingDays
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 3,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            //Cek WorkingDays
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + data,
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
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
        else {
            if ($("#FundPKFrom").val() == "" || $("#FundPKFrom").val() == 0) {
                //Cek WorkingDays
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 3,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPKFrom").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //Cek WorkingDays
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + data,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
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

    function getAgentByFundClientPK() {
        if ($("#FundClientPK").val() != "") {
            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/GetAgentByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#AgentPK").data("kendoComboBox").value(data);
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
    }

    function getEstimatedCashProjection(_unitPosition, _fundPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/FundClient_GetEstimatedCashProjection/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _unitPosition,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#EstimatedCashProjection").data("kendoNumericTextBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    // Agent Trx

    function getDataSourceAgentTrx(_url) {
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
                 aggregate: [{ field: "AgentTrxPercent", aggregate: "sum" },
                            { field: "AgentTrxAmount", aggregate: "sum" }],
                 schema: {
                     model: {

                         fields: {
                             AgentSwitchingPK: { type: "number", editable: false },
                             ClientSwitchingPK: { type: "number", editable: false },
                             AgentPK: { type: "number", editable: false },
                             AgentID: { type: "string", editable: false },
                             AgentName: { type: "string", editable: false },
                             AgentTrxPercent: { type: "number", editable: true },
                             AgentTrxAmount: { type: "number", editable: false },

                         }
                     }
                 },

             });
    }

    function initGridAgentTrx(_clientSwitchingPK) {
        $("#gridAgentTrx").empty();

        if (_clientSwitchingPK == 0 || _clientSwitchingPK == null) {
            _clientSubs = 0;
        }
        else {
            _clientSubs = _clientSwitchingPK;
        }

        var ApprovedURL = window.location.origin + "/Radsoft/AgentSwitching/GetDataAgentSwitchingByClientSwitchingPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubs,
             dataSourceAgentTrx = getDataSourceAgentTrx(ApprovedURL);



        gridAgentTrx = $("#gridAgentTrx").kendoGrid({
            dataSource: dataSourceAgentTrx,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            editable: "incell",
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "500px",
            columns: [
                 { command: { text: "Update", click: _updateAgentTrx }, title: " ", width: 100 },
                 { command: { text: "Void", click: _voidAgentTrx }, title: " ", width: 100 },
                 {
                     field: "AgentSwitchingPK", title: "AgentSwitchingPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "ClientSwitchingPK", title: "ClientSwitchingPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "AgentPK", title: "AgentPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "AgentID", title: "Agent ID", headerAttributes: {
                         style: "text-align: center"
                     }, width: 100
                 },
                {
                    field: "AgentName", title: "Agent Name", headerAttributes: {
                        style: "text-align: center"
                    }, width: 300
                },
                { field: "AgentTrxPercent", title: "Agent Trx (%)", template: "#: AgentTrxPercent  # %", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                {
                    field: "AgentTrxAmount", title: "Agent Trx Amount", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 200
                },


            ]
        }).data("kendoGrid");
    }





    $("#BtnAddAgentTrx").click(function () {
        if ($("#StatusHeader").val() == "NEW") {
            alertify.alert("Please Add Client Switching First !");
            return;

        }
        else {

            //combo box AgentPK
            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/GetAgentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamTrxAgent").kendoComboBox({
                        dataValueField: "AgentPK",
                        dataTextField: "ID",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: OnChangeParamTrxAgent,
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function OnChangeParamTrxAgent() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }


            $("#ParamTrxAgentPercent").kendoNumericTextBox({
                format: "n4",
                decimals: 4,
            });



            WinAddAgentTrx.center();
            WinAddAgentTrx.open();

        }

    });

    $("#BtnOkAddAgentTrx").click(function () {
        if ($("#ParamTrxAgent").val() == null || $("#ParamTrxAgent").val() == "" || $("#ParamTrxAgentPercent").val() == null || $("#ParamTrxAgentPercent").val() == "") {
            alertify.alert("Please Fill Agent Name and Percent !");
            return;
        }

        alertify.confirm("Are you sure want to Add this data ?", function (e) {
            if (e) {
                var AddAgentTrx = {
                    AgentSwitchingPK: 0,
                    ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                    AgentPK: $('#ParamTrxAgent').val(),
                    AgentTrxPercent: $('#ParamTrxAgentPercent').val(),
                    NetAmount: $('#TrxTotalCashAmount').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentSwitching/ValidateMaxPercentAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AddAgentTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentSwitching/AddAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AddAgentTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Insert Agent Switching Success");
                                    InsertPercentAgentSwitchingHO($('#ClientSwitchingPK').val());
                                    initGridAgentTrx($('#ClientSwitchingPK').val());
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Total Percent > 100% !");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });

    });

    $("#BtnCancelAddAgentTrx").click(function () {

        alertify.confirm("Are you sure want to Close Data ?", function (e) {
            if (e) {
                WinAddAgentTrx.close();
                alertify.alert("Close Data");
            }
        });
    });



    function onPopUpCloseAddAgentTrx() {
        $("#ParamTrxAgent").val("");
        $("#ParamTrxAgentPercent").val(0);

    }


    function _updateAgentTrx(e) {

        if (e) {
            var grid;
            grid = $("#gridAgentTrx").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            var AgentTrx = {
                AgentSwitchingPK: dataItemX.AgentSwitchingPK,
                ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                AgentTrxPercent: dataItemX.AgentTrxPercent,
                NetAmount: $('#TrxTotalCashAmount').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/AgentSwitching/ValidateMaxPercentAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(AgentTrx),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {

                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentSwitching/UpdateAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AgentTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    InsertPercentAgentSwitchingHO($('#ClientSwitchingPK').val());
                                    initGridAgentTrx($("#ClientSwitchingPK").val());

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        });
                    } else {
                        alertify.alert("Total Fee Percent > 100% !");
                        $.unblockUI();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });
        }

    }

    function _voidAgentTrx(e) {

        if (e) {
            var grid;
            grid = $("#gridAgentTrx").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var AgentTrx = {
                AgentSwitchingPK: dataItemX.AgentSwitchingPK,
                VoidUsersID: sessionStorage.getItem("user")
            };

            alertify.confirm("Are you sure want to Void Data ?", function (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentSwitching/VoidAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AgentTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Void Success");
                        InsertPercentAgentSwitchingHO($('#ClientSwitchingPK').val());
                        initGridAgentTrx($("#ClientSwitchingPK").val());
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            });
        }


    }





    // AGENT FEE TRX
    function getDataSourceAgentFeeTrx(_url) {
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
                 aggregate: [{ field: "AgentFeeTrxPercent", aggregate: "sum" },
                            { field: "AgentFeeTrxAmount", aggregate: "sum" }],
                 schema: {
                     model: {

                         fields: {
                             AgentFeeSwitchingPK: { type: "number", editable: false },
                             ClientSwitchingPK: { type: "number", editable: false },
                             AgentPK: { type: "number", editable: false },
                             AgentID: { type: "string", editable: false },
                             AgentName: { type: "string", editable: false },
                             AgentFeeTrxPercent: { type: "number", editable: true },
                             AgentFeeTrxAmount: { type: "number", editable: false },

                         }
                     }
                 },

             });
    }


    function initGridAgentFeeTrx(_clientSwitchingPK) {
        $("#gridAgentFeeTrx").empty();

        if (_clientSwitchingPK == 0 || _clientSwitchingPK == null) {
            _clientSubs = 0;
        }
        else {
            _clientSubs = _clientSwitchingPK;
        }

        var ApprovedURL = window.location.origin + "/Radsoft/AgentFeeSwitching/GetDataAgentFeeSwitchingByClientSwitchingPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubs,
             dataSourceAgentFeeTrx = getDataSourceAgentFeeTrx(ApprovedURL);



        gridAgentFeeTrx = $("#gridAgentFeeTrx").kendoGrid({
            dataSource: dataSourceAgentFeeTrx,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            editable: "incell",
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "500px",
            columns: [
                 { command: { text: "Update", click: _updateAgentFeeTrx }, title: " ", width: 100 },
                 { command: { text: "Void", click: _voidAgentFeeTrx }, title: " ", width: 100 },
                 {
                     field: "AgentFeeSwitchingPK", title: "AgentFeeSwitchingPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "ClientSwitchingPK", title: "ClientSwitchingPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "AgentPK", title: "AgentPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "AgentID", title: "Agent ID", headerAttributes: {
                         style: "text-align: center"
                     }, width: 100
                 },
                {
                    field: "AgentName", title: "Agent Name", headerAttributes: {
                        style: "text-align: center"
                    }, width: 300
                },
                { field: "AgentFeeTrxPercent", title: "Agent Fee (%)", template: "#: AgentFeeTrxPercent  # %", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" } },
                {
                    field: "AgentFeeTrxAmount", title: "Agent Fee Amount", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 200
                },


            ]
        }).data("kendoGrid");
    }





    $("#BtnAddAgentFeeTrx").click(function () {
        if ($("#StatusHeader").val() != "NEW" && $("#SwitchingFeePercent").val() != 0) {
            //combo box AgentPK
            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/GetAgentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamTrxAgentFee").kendoComboBox({
                        dataValueField: "AgentPK",
                        dataTextField: "ID",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: OnChangeParamTrxAgentFee,
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function OnChangeParamTrxAgentFee() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }


            $("#ParamTrxAgentFeePercent").kendoNumericTextBox({
                format: "n4",
                decimals: 4,
            });



            WinAddAgentFeeTrx.center();
            WinAddAgentFeeTrx.open();




        }
        else {
            alertify.alert("Please Add Client Switching First or Check Switching Fee Percent !");
            return;
        }

    });

    $("#BtnOkAddAgentFeeTrx").click(function () {


        alertify.confirm("Are you sure want to Add this data ?", function (e) {
            if (e) {
                var AddAgentFeeTrx = {
                    AgentFeeSwitchingPK: 0,
                    ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                    AgentPK: $('#ParamTrxAgentFee').val(),
                    AgentFeeTrxPercent: $('#ParamTrxAgentFeePercent').val(),
                    NetAmount: $('#TrxFeeSwitchingFeeAmount').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentFeeSwitching/ValidateMaxPercentAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AddAgentFeeTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSwitching/AddAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AddAgentFeeTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Insert Agent Fee Switching Success");
                                    InsertPercentAgentFeeSwitchingHO($('#ClientSwitchingPK').val());
                                    initGridAgentFeeTrx($('#ClientSwitchingPK').val());
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Total Fee Percent > 100% !");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });

    });

    $("#BtnCancelAddAgentFeeTrx").click(function () {

        alertify.confirm("Are you sure want to Close Data ?", function (e) {
            if (e) {
                WinAddAgentFeeTrx.close();
                alertify.alert("Close Data");
            }
        });
    });



    function onPopUpCloseAddAgentFeeTrx() {
        $("#ParamTrxAgentFee").val("");
        $("#ParamTrxAgentFeePercent").val(0);

    }


    function _updateAgentFeeTrx(e) {

        if (e) {
            var grid;
            grid = $("#gridAgentFeeTrx").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));


            var AgentFeeTrx = {
                AgentFeeSwitchingPK: dataItemX.AgentFeeSwitchingPK,
                ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                AgentFeeTrxPercent: dataItemX.AgentFeeTrxPercent,
                NetAmount: $('#TrxFeeSwitchingFeeAmount').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/AgentFeeSwitching/ValidateMaxPercentAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(AgentFeeTrx),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {

                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSwitching/UpdateAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AgentFeeTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    InsertPercentAgentFeeSwitchingHO($('#ClientSwitchingPK').val());
                                    initGridAgentFeeTrx($("#ClientSwitchingPK").val());

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        });
                    } else {
                        alertify.alert("Total Fee Percent > 100% !");
                        $.unblockUI();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });
        }

    }

    function _voidAgentFeeTrx(e) {

        if (e) {
            var grid;
            grid = $("#gridAgentFeeTrx").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var AgentFeeTrx = {
                AgentFeeSwitchingPK: dataItemX.AgentFeeSwitchingPK,
                VoidUsersID: sessionStorage.getItem("user")
            };

            alertify.confirm("Are you sure want to Void Data ?", function (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentFeeSwitching/VoidAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AgentFeeTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Void Success");
                        InsertPercentAgentFeeSwitchingHO($('#ClientSwitchingPK').val());
                        initGridAgentFeeTrx($("#ClientSwitchingPK").val());
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            });
        }


    }


    //function RecalAgentSwitching(_clientSwitchingPK) {
    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/AgentSwitching/AgentSwitchingRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSwitchingPK,
    //            type: 'GET',
    //            contentType: "application/json;charset=utf-8",
    //            success: function (data) {

    //                initGridAgentTrx(_clientSwitchingPK);
    //                initGridAgentFeeTrx(_clientSwitchingPK);
    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //            }
    //        });


    //}




    function InsertPercentAgentSwitchingHO(_clientSwitchingPK) {
        var AddAgentTrx = {
            ClientSwitchingPK: $('#ClientSwitchingPK').val(),
            NetAmount: $('#TrxTotalCashAmount').val(),
            EntryUsersID: sessionStorage.getItem("user"),
            UpdateUsersID: sessionStorage.getItem("user")
        };
        $.ajax({
            url: window.location.origin + "/Radsoft/AgentSwitching/InsertPercentAgentSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSwitchingPK,
            type: 'POST',
            data: JSON.stringify(AddAgentTrx),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

    function InsertPercentAgentFeeSwitchingHO(_clientSwitchingPK) {
        var AddAgentFeeTrx = {
            ClientSwitchingPK: $('#ClientSwitchingPK').val(),
            NetAmount: $('#TrxFeeSwitchingFeeAmount').val(),
            EntryUsersID: sessionStorage.getItem("user"),
            UpdateUsersID: sessionStorage.getItem("user")
        };
        $.ajax({
            url: window.location.origin + "/Radsoft/AgentFeeSwitching/InsertPercentAgentFeeSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSwitchingPK,
            type: 'POST',
            data: JSON.stringify(AddAgentFeeTrx),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

    function GetTransferType(_unitAmount) {
        console.log(_unitAmount);
        if (_unitAmount == null || _unitAmount == "") {
            _unitAmount = 0;
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/ClientRedemption/GetTransferType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _unitAmount + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPKFrom').val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data < 100000000) {
                    $("#TransferType").data("kendoComboBox").value(3);
                }
                else if (data >= 100000000) {
                    $("#TransferType").data("kendoComboBox").value(3);
                }
            }
        })
    }

    function getDataSourceValidation() {
        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        type: 'POST',
                        url: window.location.origin + "/Radsoft/ClientSwitching/ValidateClientSwitching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    }, parameterMap: function (data, operation) {
                        return {
                            ValueDate: $('#ValueDate').val(),
                            FundPKFrom: $('#FundPKFrom').val(),
                            FundPKTo: $('#FundPKTo').val(),
                            FundClientPK: $('#FundClientPK').val(),
                            CashAmount: $('#CashAmount').val(),
                            UnitAmount: $('#UnitAmount').val(),
                            BitSwitchingAll: $('#BitSwitchingAll').val()

                        }
                    },
                    dataType: "jsonp"
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
                            Reason: { type: "string", editable: false },
                            Notes: { type: "string", editable: true },
                        }
                    }
                }
            });
    }


    function initGridValidation() {
        $("#gridValidationClientSwitching").empty();

        var dataSourceApproved = getDataSourceValidation();

        var grid = $("#gridValidationClientSwitching").kendoGrid({
            dataSource: dataSourceApproved,
            height: 400,
            scrollable: {
                virtual: true
            },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            editable: "incell",
            toolbar: ["excel"],
            columns: [
                { field: "No", title: "No", width: 80 },
                { field: "Reason", title: "Description", width: 400 },
                { field: "Notes", title: "Notes", width: 400 },
                { field: "InsertHighRisk", title: "InsertHighRisk", width: 350, hidden: true },
                { field: "Validate", title: "Validate", width: 350, hidden: true }
            ]
        }).data("kendoGrid");


        WinValidationSwi.center();
        WinValidationSwi.open();
    }


    $("#BtnContinueValidation").click(function () {

        var _FundClientPK = 0;
        var _ValueDate = '';

        _ValueDate = kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
        _FundClientPK = $('#FundClientPK').val();

        var _reference;
        if (_GlobClientCode == "20") {
            $.ajax({
                url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SWI/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    _reference = data;
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        else {
            _reference = $('#ReferenceSInvest').val();
        }

        var val = validateData();

        alertify.confirm("Are you sure want to Add data?", function (e) {
            if (e) {

                var _Validation = [];
                var flagChange = 0;
                var gridDataArray = $('#gridValidationClientSwitching').data('kendoGrid')._data;
                for (var index = 0; index < gridDataArray.length; index++) {
                    if (gridDataArray[index]["Reason"] != null) {

                        if (gridDataArray[index]["Validate"] == 1) {
                            if (gridDataArray[index]["Notes"] == "") {
                                alertify.alert("Please fill notes for reason no : " + gridDataArray[index]["No"]).moveTo((screen.width / 4), (screen.height / 3));
                                flagChange = 0;
                                return;
                            }
                        }

                        if (gridDataArray[index]["InsertHighRisk"] == 1) {
                            var _m = {
                                Reason: gridDataArray[index]["Reason"],
                                Notes: gridDataArray[index]["Notes"],
                                FundClientPK: _FundClientPK

                            }
                            _Validation.push(_m);
                            flagChange = 1
                        }
                    }

                };

                if (flagChange != 0) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertIntoHighRiskMonitoringByUnitRegistry/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString(_ValueDate, "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(_Validation),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            console.log("Insert into highriskmonitoring Success")
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }

                //insert into clientswitching
                var ClientSwitching = {
                    ClientSwitchingPK: $('#ClientSwitchingPK').val(),
                    Type: $('#Type').val(),
                    NAVDate: $('#NAVDate').val(),
                    ValueDate: $('#ValueDate').val(),
                    PaymentDate: $('#PaymentDate').val(),
                    NAVFundFrom: $('#NAVFundFrom').val(),
                    NAVFundTo: $('#NAVFundTo').val(),
                    FundPKFrom: $('#FundPKFrom').val(),
                    FundPKTo: $('#FundPKTo').val(),
                    AgentPK: $('#AgentPK').val(),
                    FundClientPK: $('#FundClientPK').val(),
                    CashRefPKFrom: $('#CashRefPKFrom').val(),
                    CashRefPKTo: $('#CashRefPKTo').val(),
                    CurrencyPK: $('#CurrencyPK').val(),
                    TransferType: $('#TransferType').val(),
                    Description: $('#Description').val(),
                    ReferenceSInvest: _reference,
                    CashAmount: $('#CashAmount').val(),
                    UnitAmount: $('#UnitAmount').val(),
                    TotalCashAmountFundFrom: $('#TotalCashAmountFundFrom').val(),
                    TotalCashAmountFundTo: $('#TotalCashAmountFundTo').val(),
                    TotalUnitAmountFundFrom: $('#TotalUnitAmountFundFrom').val(),
                    TotalUnitAmountFundTo: $('#TotalUnitAmountFundTo').val(),
                    SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                    SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),
                    FeeType: $('#FeeType').val(),
                    BitSwitchingAll: $('#BitSwitchingAll').val(),
                    FeeTypeMethod: $('#FeeTypeMethod').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSwitching/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSwitching_I",
                    type: 'POST',
                    data: JSON.stringify(ClientSwitching),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (_GlobClientCode == '21') {
                            $("#ClientSwitchingPK").val(data.ClientSwitchingPK);
                            $("#HistoryPK").val(data.HistoryPK);
                            $("#StatusHeader").val("PENDING");
                            alertify.alert("Insert Client Switching Success");
                            $("#BtnAdd").hide();
                            $("#BtnUpdate").show();
                        }
                        else {
                            alertify.alert(data);
                            WinValidationSwi.close();
                            win.close();
                        }
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });


            }
        }).moveTo((screen.width / 4), (screen.height / 3));

    });

    $("#BtnCancelValidation").click(function () {

        WinValidationSwi.close();
    });




});
