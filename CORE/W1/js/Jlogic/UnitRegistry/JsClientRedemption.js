$(document).ready(function () {
    document.title = 'FORM CLIENT REDEMPTION';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height;
    var WinListFundClient;
    var WinListFundClientBankVA;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;
    var dirty;
    var upOradd;
    var _d = new Date();
    //grid filter
    var filterField;
    var filterOperator;
    var filtervalue;
    var filterlogic;
    var checkedIds = {};
    var checkedApproved = {};
    var checkedPending = {};
    var globReference;

    //end grid filter


    //1
    initButton();
    //2
    initWindow();
    if (_GlobClientCode == "08") {
        $("#lblTransactionPK").show();
    }
    else {
        $("#lblTransactionPK").hide();
    }

    if (_GlobClientCode == "10") {

        $("#LblDownloadMode").hide();
        $("#tdDownloadMode").hide();
    }
    else {

        $("#LblDownloadMode").show();
        $("#tdDownloadMode").show();
    }

    if (_GlobClientCode == "27") {
        $("#BtnCashInstructionBySelected").show();
    }
    else {
        $("#BtnCashInstructionBySelected").hide();
    }

    if (_GlobClientCode == "20") {
        $("#lblEntryDate").hide();
        $("#lblTradeDate").show();
        $("#NAVDate").data("kendoDatePicker").enable(false);
        $("#lblSignature").show();
        $("#BankRecipientPK").attr("required", true);
    }
    else {
        $("#lblEntryDate").show();
        $("#lblTradeDate").hide();
        $("#NAVDate").data("kendoDatePicker").enable(true);
        $("#lblSignature").hide();
        $("#BankRecipientPK").attr("required", true);
    }


    //3
    initGrid();

    function initButton() {
        if (_GlobClientCode == "01") {
            $("#trTransferType").show();
        }
        if (_GlobClientCode == "24") {
            $("#trTransferType").show();
        }

        else if (_GlobClientCode == "21") {
            $("#BtnHoldingPeriod").show();
        }
        else {
            $("#BtnHoldingPeriod").hide();
            $("#trTransferType").hide();
        }

        $("#BtnCashInstructionBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

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

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnGetNavBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnBatchUnitFormBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnBatchAmountFormBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnRecalculate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnPaymentReportListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkPaymentReportListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelPaymentReportListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnSInvestRedemptionRptBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnHoldingPeriod").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnRefreshHoldingPeriod").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnMaturedFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnOkMaturedFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelMaturedFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#ShowGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
    }

    
    function initWindow() {
        $("#ParamPaymentDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeParamPaymentDateFrom,
        });
        function OnChangeParamPaymentDateFrom() {
            if ($("#ParamPaymentDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamPaymentDateTo").data("kendoDatePicker").value($("#ParamPaymentDateFrom").data("kendoDatePicker").value());
            }
        }


        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        $("#ParamPaymentDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
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
                    suggest: true,
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
        //$("#HideDate").kendoComboBox({
        //    dataTextField: "text",
        //    dataValueField: "value",
        //    dataSource: [
        //        { text: "Yes", value: true },
        //        { text: "No", value: false },

        //    ],
        //    filter: "contains",
        //    suggest: true,
        //    index: 1,
        //    change: OnChangeHideDate,
        //});
        //function OnChangeHideDate() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            //clearDataNav();
            clearDataAfterUpdate();
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {


                //Check if Date parse is successful
                if (!_date) {

                    alertify.alert("Wrong Format Date DD/MM/YYYY");
                }
                    //Cek Holidays
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
                }
                //Cek WorkingDays
                $.ajax({
                    url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
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
                var currentTime = new Date();
                var hours = currentTime.getHours();
                if (hours <= 12) {
                    $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
                    ClientRedemptionRecalculate();
                } else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 0,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAVDate").data("kendoDatePicker").value(new Date(data));
                            ClientRedemptionRecalculate();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                $("#NAV").data("kendoNumericTextBox").value(0);
            }
        }


        $("#PaymentDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: _setPaymentDate(),
        });



        $("#NAVDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeNAVDate,
            value: new Date(),
        });




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
                        }
                        else {
                            ClientRedemptionRecalculate();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
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

        win = $("#WinClientRedemption").kendoWindow({
            height: 1300,
            title: " Redemption Detail",
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
            title: "List FundClient ",
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

        WinFundClient = $("#WinFundClient").kendoWindow({
            height: 500,
            title: "Fund Client List Detail",
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");



        WinListFundClientBankVA = $("#WinListFundClientBankVA").kendoWindow({
            height: 450,
            title: "List FundClient Bank List",
            visible: false,
            width: 950,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListFundClientBankVAClose
        }).data("kendoWindow");

        function onWinListFundClientBankVAClose() {
            $("#gridListFundClientBankVA").empty();
        }

        WinPaymentReportListing = $("#WinPaymentReportListing").kendoWindow({
            height: 300,
            title: "* Listing Payment Report",
            visible: false,
            width: 650,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinPaymentReportListingClose
        }).data("kendoWindow");

        WinHoldingPeriod = $("#WinHoldingPeriod").kendoWindow({
            height: 800,
            title: "* Listing Holding Period",
            visible: false,
            width: 1500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinHoldingPeriodClose
        }).data("kendoWindow");

        WinMaturedFund = $("#WinMaturedFund").kendoWindow({
            height: 300,
            title: "* Matured Fund",
            visible: false,
            width: 650,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinMaturedFundClose
        }).data("kendoWindow");


    }


    var GlobValidator = $("#WinClientRedemption").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#ValueDate").val() != "") {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format Date");
                return 0;
            }
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

    function showDetails(e) {

        $("#btnListFundClientPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")

        $("#btnListFundClientBankVA").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")


        var dataItemX;
        if (e == null) {

            $("#FundClientBankVA").hide();
            $("#btnListFundClientBankVA").hide();
            $("#lblBankRecipientPK").show();
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnUnApproved").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#BtnRecalculate").show();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();

            $("#StatusHeader").val("NEW");
            $("#ValueDate").data("kendoDatePicker").value(_d);
            $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").val());
            $("#PaymentDate").data("kendoDatePicker").value(_setPaymentDate());
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridClientRedemptionApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridClientRedemptionPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridClientRedemptionHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));



            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").show();
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
                $("#BtnUnApproved").hide();
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
                $("#BtnUnApproved").hide();
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
                $("#BtnUnApproved").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnRecalculate").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            dirty = null;

            $("#ClientRedemptionPK").val(dataItemX.ClientRedemptionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FundClientBankVA").val(dataItemX.BankRecipientDesc);
            $("#NAVDate").data("kendoDatePicker").value(dataItemX.NAVDate);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
            $("#BankRecipientPK").val(dataItemX.BankRecipientDesc + " - " + dataItemX.BankRecipientAccountNo);
            $("#TransferType").val(dataItemX.TransferType);
            $("#Description").val(dataItemX.Description);
            $("#ReferenceSInvest").val(dataItemX.ReferenceSInvest);
            $("#TransactionPK").val(dataItemX.TransactionPK);
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



            if (_GlobClientCode == "10" && dataItemX.EntryUsersID == "BKLP") {
                $("#lblBankRecipientPK").hide();
                $("#FundClientBankVA").show();
                $("#btnListFundClientBankVA").show();
            }
            else {
                $("#lblBankRecipientPK").show();
                $("#FundClientBankVA").hide();
                $("#btnListFundClientBankVA").hide();
            }

            getFundClientCashRefByFundClientPK(dataItemX.FundPK, dataItemX.FundClientPK);
        }


        //combo box Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SubscriptionType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                //if (_GlobClientCode == "10") {
                    $("#Type").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeType,
                        value: setCmbType()
                    });
                //}
                //else {
                //    $("#Type").kendoComboBox({
                //        dataValueField: "Code",
                //        dataTextField: "DescOne",
                //        filter: "contains",
                //        suggest: true,
                //        dataSource: data,
                //        enable: false,
                //        index: 0,
                //        change: onChangeType,
                //        value: setCmbType()
                //    });
                //}
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
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundPK,
                    value: setCmbFundPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundPK() {
            if (e != null) {
                if (this.value() == dataItemX.FundPK) {
                    return;
                }
            }
            _setPaymentDate();
            clearDataNav();
            $("#Description").val("");
            $("#CashRefPK").data("kendoComboBox").value("");
            $("#CurrencyPK").data("kendoComboBox").value("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            else {

                if ($("#FundPK").data("kendoComboBox").value() != "") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            //$("#RedemptionFeePercent").data("kendoNumericTextBox").value(data.RedemptionFeePercent);
                            //$("#CashRefPK").data("kendoComboBox").value(data.DefaultBankRedemptionPK);
                            $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                            $("#NAV").data("kendoNumericTextBox").value(0);
                            if ($("#FundClientPK").val() == "") {
                                getFundClientCashRefByFundClientPK(data.FundPK, 0);
                            }
                            else {
                                getFundClientCashRefByFundClientPK(data.FundPK, $("#FundClientPK").val());
                            }
                            ClientRedemptionRecalculate();

                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });



                }
                else {
                    //getCashRefPKByFundPK(0);
                    $("#RedemptionFeePercent").data("kendoNumericTextBox").value(0);
                    //$("#CashRefPK").data("kendoComboBox").value("");
                    $("#CurrencyPK").data("kendoComboBox").value("");
                }

            }
            if ($("#FundClientPK").val() != null && $("#FundClientPK").val() != '' && $("#FundPK").data("kendoComboBox").value() != "")
            {
                getDefaultBankRecipientComboByFundClientPK($("#FundClientPK").val(), $("#FundPK").data("kendoComboBox").value());
            }
            

            //ClientRedemptionRecalculate(); 
        }

        function setCmbFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }

        if ($("#FundPK").val() == "" || $("#FundPK").val() == 0) {
            var _fundPK = 0
        }
        else {
            var _fundPK = $("#FundPK").val()
        }
        if ($("#FundClientPK").val() == "" || $("#FundClientPK").val() == 0) {
            var _fundClientPK = 0
        }
        else {
            var _fundClientPK = $("#FundClientPK").val()
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/RED",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashRefPK").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCashRefPK,
                    value: setCmbCashRefPK()
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
            //getFundClientCashRefByFundClientPK($("#FundPK").data("kendoComboBox").value(), $("#FundClientPK").val());

        }
        function setCmbCashRefPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashRefPK == 0) {
                    return "";
                } else {
                    return dataItemX.CashRefPK;
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
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
            else {

                if ($("#AgentPK").data("kendoComboBox").value() != "") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Agent/GetAgentByAgentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AgentPK").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#AgentFeePercent").data("kendoNumericTextBox").value(data.AgentFee);
                            //ClientRedemptionRecalculate();
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
                else {
                    $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
                    //ClientRedemptionRecalculate();
                }
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

        $("#NAV").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNAV(),
        });

        function setNAV() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.NAV == 0) {
                    return "";
                } else {
                    return dataItemX.NAV;
                }
            }
        }

        $("#NAV").data("kendoNumericTextBox").enable(false);

        $("#UnitPosition").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setUnitPosition(),
        });

        $("#EstimatedCashProjection").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
        });

        $("#UnitPosition").data("kendoNumericTextBox").enable(false);
        $("#EstimatedCashProjection").data("kendoNumericTextBox").enable(false);

        function setUnitPosition() {
            if (e == null) {
                return "";
            } else {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClient/FundClient_GetUnitPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundPK + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + dataItemX.FundClientPK,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#UnitPosition").data("kendoNumericTextBox").value(data);
                        getEstimatedCashProjection(data, dataItemX.FundPK);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }




        $("#BitRedemptionAll").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitRedemptionAll,
            value: setCmbBitRedemptionAll()
        });


        function OnChangeBitRedemptionAll() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#BitRedemptionAll").val() == "true") {
                //CheckRedemptionPending();
                //getRedemptionAll($("#FundPK").val(), $("#FundClientPK").val());
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
            ClientRedemptionRecalculate();
        }

        function setCmbBitRedemptionAll() {
            if (e == null) {
                return false;
            } else {

                return dataItemX.BitRedemptionAll;
            }

        }
        $("#CashAmount").kendoNumericTextBox({
            format: "n2",
            decimals:2,
            change: onchangeCashAmount,
            value: setCashAmount(),
        });

        function setCashAmount() {
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
        function onchangeCashAmount() {
            $("#UnitAmount").data("kendoNumericTextBox").value(0);
            ClientRedemptionRecalculate();
		if ($("#CashAmount").data("kendoNumericTextBox").value() < 100000000)
	            {
	                $("#TransferType").data("kendoComboBox").value(1);
	            }
	            else if ($("#CashAmount").data("kendoNumericTextBox").value() >= 100000000)
	            {
	                $("#TransferType").data("kendoComboBox").value(2);
	            }	             

        }
        $("#CashAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == true && dataItemX.Revised == false) {
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            }
        }

        //$("#CashAmount").data("kendoNumericTextBox").enable(false);

        $("#UnitAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: onchangeUnitAmount,
            value: setUnitAmount(),
        });
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
            ClientRedemptionRecalculate();
	    GetTransferType($("#UnitAmount").data("kendoNumericTextBox").value());
        }
        $("#UnitAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null)
        {
            if (dataItemX.Status == 2 && dataItemX.Posted == true && dataItemX.Revised == false) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
            }
        }

	function GetTransferType(_unitAmount)
	        {
	            $.ajax({
	                url: window.location.origin + "/Radsoft/ClientRedemption/GetTransferType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _unitAmount + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val(),
	                type: 'GET',
	                contentType: "application/json;charset=utf-8",
	                success: function (data) {
	                    if (data < 100000000) {
	                        $("#TransferType").data("kendoComboBox").value(1);
	                    }
	                    else if (data >= 100000000) {
	                        $("#TransferType").data("kendoComboBox").value(2);
	                    }
	                }
	            })
	        }




        
        $("#TotalCashAmount").kendoNumericTextBox({
            format: "n2",
	    decimals: 2,
            value: setTotalCashAmount(),
        });
        $("#TotalCashAmount").data("kendoNumericTextBox").enable(false);
        function setTotalCashAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TotalCashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.TotalCashAmount;
                }
            }
        }

        $("#TotalUnitAmount").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setTotalUnitAmount(),
        });
        $("#TotalUnitAmount").data("kendoNumericTextBox").enable(false);
        function setTotalUnitAmount() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.TotalUnitAmount;

            }
        }

        $("#RedemptionFeePercent").kendoNumericTextBox({
            format: "###.######## \\%",
            decimals: 8,
            value: setRedemptionFeePercent(),
            change: onChangeRedemptionFeePercent
        });
        function setRedemptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.RedemptionFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.RedemptionFeePercent;
                }
            }
        }

        $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        $("#RedemptionFeeAmount").kendoNumericTextBox({
            format: "n0",
            value: setRedemptionFeeAmount(),
            change: onChangeRedemptionFeeAmount
        });
        //$("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(false);
        function setRedemptionFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.RedemptionFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.RedemptionFeeAmount;
                }
            }
        }

        $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(false);
            }
        }

        $("#AgentFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            value: setAgentFeePercent(),
            change: onChangeAgentFeePercent
        });
        function setAgentFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.AgentFeePercent;
            }

        }

        $("#AgentFeePercent").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        $("#AgentFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setAgentFeeAmount(),
            change: onChangeAgentFeeAmount
        });

        $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
            }
        }

        //$("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
        function setAgentFeeAmount() {
            if (e == null) {
                return 0;
            } else {

                return dataItemX.AgentFeeAmount;
            }

        }
        function onChangeRedemptionFeePercent() {
            $("#RedemptionFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientRedemptionRecalculate();
            }
        }
        function onChangeRedemptionFeeAmount() {
            $("#RedemptionFeePercent").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 2) {
                ClientRedemptionRecalculate();
            }
        }
        function onChangeAgentFeeAmount() {
            $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 2) {
                ClientRedemptionRecalculate();
            }
        }
        function onChangeAgentFeePercent() {
            if ($("#AgentPK").data("kendoComboBox").value() == null || $("#AgentPK").data("kendoComboBox").value() == "") {
                alertify.alert("Cannot Change Agent Fee Percent If Agent is empty");
                $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
            }
            $("#AgentFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientRedemptionRecalculate();
            }
        }



        //Combo Box Bank Recipient
        if ($("#FundClientPK").val() == "" || $("#FundClientPK").val() == 0) {
            var _fundClientPK = 0
        }
        else {
            var _fundClientPK = dataItemX.FundClientPK;
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetBankRecipientComboByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRecipientPK").kendoComboBox({
                    dataValueField: "BankRecipientPK",
                    dataTextField: "AccountNo",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankRecipient,
                    value: setCmbBankRecipient()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankRecipient() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbBankRecipient() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankRecipientPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankRecipientPK;
                }
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

        $("#FeeType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Percent", value: 1 },
                { text: "Amount", value: 2 }
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
            } else {
                if (this.value() == 1) {
                    $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(false);
                    $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(true);

                    $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
                    $("#AgentFeePercent").data("kendoNumericTextBox").enable(true);
                } else {
                    $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(false);
                    $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(true);

                    $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
                    $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);

                }
                ClientRedemptionRecalculate();
            }
        }

        function setCmbFeeType() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.FeeType;
            }
        }


        if (dataItemX == null) {
            $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(false);
        } else {
            if (dataItemX.FeeType == 1) {
                $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(false);
                $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(true);

                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
                $("#AgentFeePercent").data("kendoNumericTextBox").enable(true);
            } else {
                $("#RedemptionFeePercent").data("kendoNumericTextBox").enable(false);
                $("#RedemptionFeeAmount").data("kendoNumericTextBox").enable(true);

                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
                $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);
            }

            if (dataItemX.BitRedemptionAll == true) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            } else {
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(true);
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

    function clearDataAfterUpdate() {
        //$("#NAV").data("kendoNumericTextBox").value("");
        //$("#EstimatedCashProjection").data("kendoNumericTextBox").value("");
        //$("#CashAmount").data("kendoNumericTextBox").value("");
        //$("#UnitAmount").data("kendoNumericTextBox").value("");
        //$("#RedemptionFeePercent").data("kendoNumericTextBox").value("");
        //$("#RedemptionFeeAmount").data("kendoNumericTextBox").value("");
        //$("#AgentFeeAmount").data("kendoNumericTextBox").value("");
        //$("#TotalCashAmount").data("kendoNumericTextBox").value($("#CashAmount").data("kendoNumericTextBox").value());
        //$("#TotalCashAmount").data("kendoNumericTextBox").value("");
    }

    function clearDataNav() {
        $("#NAV").data("kendoNumericTextBox").value("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#BankRecipientPK").data("kendoComboBox").value("");
        $("#EstimatedCashProjection").data("kendoNumericTextBox").value("");
        $("#CashAmount").data("kendoNumericTextBox").value("");
        $("#UnitAmount").data("kendoNumericTextBox").value("");
        $("#UnitPosition").data("kendoNumericTextBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        //$("#RedemptionFeePercent").data("kendoNumericTextBox").value("");
        //$("#RedemptionFeeAmount").data("kendoNumericTextBox").value("");
        //$("#AgentFeeAmount").data("kendoNumericTextBox").value("");
        //$("#TotalCashAmount").data("kendoNumericTextBox").value($("#CashAmount").data("kendoNumericTextBox").value());
        //$("#TotalCashAmount").data("kendoNumericTextBox").value("");
    }

    function clearData() {
        $("#ReferenceSInvest").val("");
        $("#ClientRedemptionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#NAVDate").data("kendoDatePicker").value(null);
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#PaymentDate").data("kendoDatePicker").value(null);
        $("#NAV").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#FundClientName").val("");
        $("#CashRefPK").val("");
        $("#CashRefID").val("");
        $("#CurrencyPK").val("");
        $("#CurrencyID").val("");
        $("#Description").val("");
        $("#CashAmount").val("");
        $("#UnitAmount").val("");
        $("#TotalCashAmount").val("");
        $("#TotalUnitAmount").val("");
        $("#UnitPosition").val("");
        $("#RedemptionFeePercent").val("");
        $("#RedemptionFeeAmount").val("");
        $("#AgentPK").val("");
        $("#AgentID").val("");
        $("#AgentFeePercent").val("");
        $("#AgentFeeAmount").val("");
        $("#BankRecipientPK").val("");
        $("#TransferType").val("");
        $("#FeeType").val("");
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

        $("#TransactionPK").val("");

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
                 pageSize: 100,
                 aggregate: [{ field: "CashAmount", aggregate: "sum" },
                    { field: "UnitAmount", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             ClientRedemptionPK: { type: "number" },
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
                             NAV: { type: "number" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             CashRefPK: { type: "number" },
                             CashRefID: { type: "string" },
                             CurrencyPK: { type: "number" },
                             CurrencyID: { type: "string" },
                             Description: { type: "string" },
                             CashAmount: { type: "number" },
                             UnitAmount: { type: "number" },
                             TotalCashAmount: { type: "number" },
                             TotalUnitAmount: { type: "number" },
                             RedemptionFeePercent: { type: "number" },
                             RedemptionFeeAmount: { type: "number" },
                             AgentPK: { type: "number" },
                             AgentID: { type: "string" },
                             AgentFeePercent: { type: "number" },
                             AgentFeeAmount: { type: "number" },
                             UnitPosition: { type: "number" },
                             BankRecipientPK: { type: "number" },
                             BankRecipientDesc: { type: "string" },
                             BankRecipientAccountNo: { type: "string" },
                             TransferType: { type: "number" },
                             TransferTypeDesc: { type: "string" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
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
            var gridTesFilter = $("#gridClientRedemptionApproved").data("kendoGrid");

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
            
            var gridPending = $("#gridClientRedemptionPending").data("kendoGrid");
            //grid filter
            var gridTesFilter = $("#gridClientRedemptionPending").data("kendoGrid");

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
            var gridTesFilter = $("#gridClientRedemptionHistory").data("kendoGrid");

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
            RecalGridHistory();
            var gridHistory = $("#gridClientRedemptionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridClientRedemptionApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {

            if (checkedApproved[row.ClientRedemptionPK]) {
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

    function initGrid() {

        $("#gridClientRedemptionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientRedemptionApprovedURL = window.location.origin + "/Radsoft/ClientRedemption/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(ClientRedemptionApprovedURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientRedemptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                resizable: true,
                dataBound: gridApprovedOnDataBound,
                toolbar: ["excel"],
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },

                    { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "FrontID", title: "Front ID", width: 150 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                    { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientRedemptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                resizable: true,
                dataBound: gridApprovedOnDataBound,
                toolbar: ["excel"],
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch Amount", click: showBatchAmount }, title: " ", width: 150 },
                    //{ command: { text: "Batch Unit", click: showBatchUnit }, title: " ", width: 150 },

                    { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                    { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var grid = $("#gridClientRedemptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                resizable: true,
                dataBound: gridApprovedOnDataBound,
                toolbar: ["excel"],
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 150 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                    { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");



        }

        grid.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientRedemptionApproved").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourceApproved.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end grid filter


        $('#chbB').change(function (ev) {

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
                grid = $("#gridClientRedemptionApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.ClientRedemptionPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }



        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabClientRedemption").kendoTabStrip({
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
                    } K
                } else {
                    refresh();
                }
            }
        });
        ResetButtonBySelectedData();
        $("#BtnRejectBySelected").hide();
        $("#BtnApproveBySelected").hide();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchUnitFormBySelected").show();
        $("#BtnBatchAmountFormBySelected").show();
        $("#BtnSInvestRedemptionRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchUnitFormBySelected").show();
        $("#BtnBatchAmountFormBySelected").show();
        $("#BtnSInvestRedemptionRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();
    }
   
    function RecalGridPending() {
        $("#gridClientRedemptionPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientRedemptionPendingURL = window.location.origin + "/Radsoft/ClientRedemption/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(ClientRedemptionPendingURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientRedemptionPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                    {
                        headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                   { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 150 },
                   { field: "FrontID", title: "Front ID", width: 150 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                   { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "FundID", title: "Fund ID", width: 115 },
                   { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentName", title: "Agent Name", width: 200 },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefID", title: "Cash Ref", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                   { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                   { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientRedemptionPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                    {
                        headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                    { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var grid = $("#gridClientRedemptionPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                    {
                        headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                   { command: { text: "Show", click: showDetails }, title: " ", width: 80 },

                   { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                   { field: "TypeDesc", title: "Type", width: 100 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                   { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "FundID", title: "Fund ID", width: 115 },
                   { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentName", title: "Agent Name", width: 200 },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefID", title: "Cash Ref", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                   { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                   { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        };


        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientRedemptionPending").data("kendoGrid");
            gridTesFilter.one("dataBound", dataSourcePending.filter({
                logic: filterlogic,
                filters: [
                    { field: filterField, operator: filterOperator, value: filtervalue }
                ]
            })
            );
        }
        //end filter

        grid.table.on("click", ".checkboxPending", selectRowPending);
        var oldPageSizeApproved = 0;


        $('#chbPending').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
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

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowPending() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridClientRedemptionPending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.ClientRedemptionPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].ClientRedemptionPK]) {
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
        $("#BtnBatchUnitFormBySelected").show();
        $("#BtnBatchAmountFormBySelected").show();
        $("#BtnSInvestRedemptionRptBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();

    }

    function RecalGridHistory() {

        $("#gridClientRedemptionHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientRedemptionHistoryURL = window.location.origin + "/Radsoft/ClientRedemption/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(ClientRedemptionHistoryURL);

        }

        if (_GlobClientCode == '10') {
            $("#gridClientRedemptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 150 },
                   { field: "FrontID", title: "Front ID", width: 150 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                   { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "FundID", title: "Fund ID", width: 115 },
                   { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentName", title: "Agent Name", width: 200 },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefID", title: "Cash Ref", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                   { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                   { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else if (_GlobClientCode == '24') {
            $("#gridClientRedemptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                    { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                    { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                    { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else {
            $("#gridClientRedemptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Client Redemption"
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
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ClientRedemptionPK", title: "SysNo.", width: 95 },
                   { field: "Type", title: "Type.", hidden: true, width: 95 },
                   { field: "TypeDesc", title: "Type", width: 100 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                   { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                   { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                   { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                   { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                   { field: "FundID", title: "Fund ID", width: 115 },
                   { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeePercent", title: "Red.Fee (%)", template: "#: RedemptionFeePercent  # %", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "RedemptionFeeAmount", title: "Red.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                   { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "AgentName", title: "Agent Name", width: 200 },
                   { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                   { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                   { field: "CashRefID", title: "Cash Ref", width: 200 },
                   { field: "CurrencyID", title: "Currency", width: 200 },
                   { field: "TransferType", title: "TransferType", hidden: true, width: 200 },
                   { field: "TransferTypeDesc", title: "TransferType", width: 200 },
                   { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                   { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                   { field: "Description", title: "Description", width: 350 },
                   { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                   { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "UnitPosition", title: "Unit Position", width: 200, hidden: true, attributes: { style: "text-align:right;" } },
                   { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                   { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                   { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                   { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                   { field: "PostedBy", title: "Posted By", width: 200 },
                   { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "RevisedBy", title: "Revised By", width: 200 },
                   { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "EntryUsersID", title: "Entry ID", width: 200 },
                   { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "UpdateUsersID", title: "Update ID", width: 200 },
                   { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                   { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                   { field: "VoidUsersID", title: "Void ID", width: 200 },
                   { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientRedemptionHistory").data("kendoGrid");
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
        $("#BtnBatchUnitFormBySelected").hide();
        $("#BtnBatchAmountFormBySelected").hide();
        $("#BtnSInvestRedemptionRptBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridClientRedemptionHistory").data("kendoGrid");
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
        if (_GlobClientCode == "32") {
            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/ValidationWaperdLicense/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AgentPK").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == "1") {
                        alertify.alert("This agent has expired WAPERD license!");
                        return
                    }
                }
            });
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
            type: 'GET',
            async: false,
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
                    return;
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        if ($('#BitRedemptionAll').val() == "false") {
            $.ajax({
                url: window.location.origin + "/Radsoft/ClientRedemption/CheckMinimumBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CashAmount').val() + "/" + $('#UnitAmount').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FundPK').val() + "/" + $('#FundClientPK').val(),
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
                url: window.location.origin + "/Radsoft/FundClientPosition/ValidateCashAmountByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + $('#FundClientPK').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                url: window.location.origin + "/Radsoft/Host/GetUnitAmountByFundPKandFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + $("#FundClientPK").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
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

        if ($('#TotalCashAmount').val() == 0 && $('#TotalUnitAmount').val() == 0 && _GlobClientCode != "10") {
            alertify.alert("Please Recalculate / Check Total Cash Amount / Total Unit Amount First !");
            return;
        }



        var val = validateData();
        var _CustomMessage = '';

        if (_GlobClientCode == "20")
            _CustomMessage = " and transfer redemption amount to " + $('#BankRecipientPK').data("kendoComboBox").text();

        if (val == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/ClientRedemption/ValidateClientRedemption/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashAmount").val() + "/" + $("#UnitAmount").val() + "/" + $("#FundPK").val() + "/" + $("#FundClientPK").val() + "/" + $("#BitRedemptionAll").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    if (data[0] != undefined) {
                        initGridValidation();
                    }
                    else {
                        alertify.confirm("Are you sure to add data" + _CustomMessage + "? ", function (e) {
                            if (e) {
                                GetSinvestReference();
                                //insert client redemption
                                var ClientRedemption = {
                                    ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                    Type: $('#Type').val(),
                                    NAVDate: $('#NAVDate').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    PaymentDate: $('#PaymentDate').val(),
                                    NAV: $('#NAV').val(),
                                    FundPK: $('#FundPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    CashRefPK: $('#CashRefPK').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    Description: $('#Description').val(),
                                    ReferenceSInvest: globReference,
                                    CashAmount: $('#CashAmount').val(),
                                    UnitAmount: $('#UnitAmount').val(),
                                    TotalCashAmount: $('#TotalCashAmount').val(),
                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                                    RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                                    RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                                    AgentPK: $('#AgentPK').val(),
                                    AgentFeePercent: $('#AgentFeePercent').val(),
                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
                                    UnitPosition: $('#UnitPosition').val(),
                                    BankRecipientPK: $('#BankRecipientPK').val(),
                                    TransferType: $('#TransferType').val(),
                                    FeeType: $('#FeeType').val(),
                                    BitRedemptionAll: $('#BitRedemptionAll').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_I",
                                    type: 'POST',
                                    data: JSON.stringify(ClientRedemption),
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
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }

    });

    $("#BtnUpdate").click(function () {
        //alertify.alert($('#BitRedemptionAll').val());
        var val = validateData();
        if (val == 1) {
            if ($('#BitRedemptionAll').val() == "true") {
                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {    
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                                    _trxPK = '';
                                                    if ($('#TransactionPK').val() != null) {
                                                        _trxPK = $('#TransactionPK').val();
                                                    }
                                                    var ClientRedemption = {
                                                        ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                                        Type: $('#Type').val(),
                                                        HistoryPK: $('#HistoryPK').val(),
                                                        NAVDate: $('#NAVDate').val(),
                                                        ValueDate: $('#ValueDate').val(),
                                                        PaymentDate: $('#PaymentDate').val(),
                                                        NAV: $('#NAV').val(),
                                                        FundPK: $('#FundPK').val(),
                                                        FundClientPK: $('#FundClientPK').val(),
                                                        CashRefPK: $('#CashRefPK').val(),
                                                        CurrencyPK: $('#CurrencyPK').val(),
                                                        Description: $('#Description').val(),
                                                        ReferenceSInvest: $("#ReferenceSInvest").val(),
                                                        CashAmount: $('#CashAmount').val(),
                                                        UnitAmount: $('#UnitAmount').val(),
                                                        TotalCashAmount: $('#TotalCashAmount').val(),
                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                        RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                                                        RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                                                        AgentPK: $('#AgentPK').val(),
                                                        AgentFeePercent: $('#AgentFeePercent').val(),
                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                        UnitPosition: $('#UnitPosition').val(),
                                                        BankRecipientPK: $('#BankRecipientPK').val(),
                                                        TransferType: $('#TransferType').val(),
                                                        FeeType: $('#FeeType').val(),
                                                        BitRedemptionAll: $('#BitRedemptionAll').val(),
                                                        TransactionPK: _trxPK,
                                                        Notes: str,
                                                        EntryUsersID: sessionStorage.getItem("user")
                                                    };
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_U",
                                                        type: 'POST',
                                                        data: JSON.stringify(ClientRedemption),
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
                                    else {
                                        alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
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
                if ($('#TotalCashAmount').val() != 0 || $('#TotalUnitAmount').val() != 0 || $("#UnitAmount").val() != 0) {

                    alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                        if (e) {
                            if ($("#CashAmount").val() != 0 || $("#UnitAmount").val() != 0) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundClientPosition/ValidateCashAmountByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + $('#FundClientPK').val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {

                                        if ((parseFloat($("#CashAmount").val() - data)) > 0 && $("#CashAmount").val() > 0) {
                                            alertify.alert("Save Cancel, Cash Amount > Cash Position");
                                            return;
                                        }
                                        else {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == false) {

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                                                    _trxPK = '';
                                                                    if ($('#TransactionPK').val() != null) {
                                                                        _trxPK = $('#TransactionPK').val();
                                                                    }
                                                                    var ClientRedemption = {
                                                                        ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                                                        Type: $('#Type').val(),
                                                                        HistoryPK: $('#HistoryPK').val(),
                                                                        NAVDate: $('#NAVDate').val(),
                                                                        ValueDate: $('#ValueDate').val(),
                                                                        PaymentDate: $('#PaymentDate').val(),
                                                                        NAV: $('#NAV').val(),
                                                                        FundPK: $('#FundPK').val(),
                                                                        FundClientPK: $('#FundClientPK').val(),
                                                                        CashRefPK: $('#CashRefPK').val(),
                                                                        CurrencyPK: $('#CurrencyPK').val(),
                                                                        Description: $('#Description').val(),
                                                                        ReferenceSInvest: $("#ReferenceSInvest").val(),
                                                                        CashAmount: $('#CashAmount').val(),
                                                                        UnitAmount: $('#UnitAmount').val(),
                                                                        TotalCashAmount: $('#TotalCashAmount').val(),
                                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                                        RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                                                                        RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                                                                        AgentPK: $('#AgentPK').val(),
                                                                        AgentFeePercent: $('#AgentFeePercent').val(),
                                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                                        UnitPosition: $('#UnitPosition').val(),
                                                                        BankRecipientPK: $('#BankRecipientPK').val(),
                                                                        TransferType: $('#TransferType').val(),
                                                                        FeeType: $('#FeeType').val(),
                                                                        BitRedemptionAll: $('#BitRedemptionAll').val(),
                                                                        TransactionPK: _trxPK,
                                                                        Notes: str,
                                                                        EntryUsersID: sessionStorage.getItem("user")
                                                                    };
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_U",
                                                                        type: 'POST',
                                                                        data: JSON.stringify(ClientRedemption),
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
                                                    else {
                                                        alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
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
                                    }
                                });

                            }


                        }
                    });
                }
                else {
                    alertify.alert("Please Recalculate / Check Total Cash Amount / Total Unit Amount First !")
                }
            }

        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ClientRedemption" + "/" + $("#ClientRedemptionPK").val(),
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
        
        if ($("#NAV").val() == 0 || $("#NAV").val() == null || $("#NAV").val() == "") {
            alertify.alert("NAV Must Ready First, Please Click Get NAV");
            return;
        }

        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientRedemption = {
                                ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_A",
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
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
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientRedemption = {
                                ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_V",
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientRedemption = {
                                ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_R",
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
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
                alert(" Redemption  Already Posted / Revised, UnApprove Cancel");
            } else {


                if (e) {
                    var ClientRedemption = {
                        ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_UnApproved",
                        type: 'POST',
                        data: JSON.stringify(ClientRedemption),
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
        if ($("#FundPK").data("kendoComboBox").value() == "" || $("#FundPK").data("kendoComboBox").value() == null) {
            alertify.alert("Please Input Fund First");
            WinListFundClient.close();
            return;
        }
        if ($("#NAVDate").data("kendoDatePicker").value() == "" || $("#NAVDate").data("kendoDatePicker").value() == null) {
            alertify.alert("Please Input NAV Date First");
            WinListFundClient.close();
            return;
        }
        var _url = window.location.origin + "/Radsoft/FundClient/GetFundClientFromFundClientPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy");
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
               { field: "ID", title: "Client ID", width: 300 },
               { field: "FundClientPK", title: "Client Name", hidden: true, width: 300 },
               //{ field: "FundClientName", title: "Client Name", width: 300 },
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
                                    url: window.location.origin + "/Radsoft/FundClient/CheckClientCantRedempt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
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
                                                            //clearData();
                                                            $("#FundClientID").val(dataItemX.FundClientName);
                                                            $("#UnitPosition").data("kendoNumericTextBox").value(dataItemX.UnitAmount);
                                                            $("#UnitAmount").data("kendoNumericTextBox").value(0);
                                                            $("#CashAmount").data("kendoNumericTextBox").value(0);
                                                            $("#TotalUnitAmount").data("kendoNumericTextBox").value(0);
                                                            $("#TotalCashAmount").data("kendoNumericTextBox").value(0);
                                                            $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                            $("#CashRefPK").data("kendoComboBox").value("");
                                                            $("#AgentPK").data("kendoComboBox").value("");
                                                            getFundClientCashRefByFundClientPK($("#FundPK").val(), dataItemX.FundClientPK);
                                                            getAgentByFundClientPK();
                                                            //getDefaultBankRecipientComboByFundClientPK(dataItemX.FundClientPK, $("#FundPK").data("kendoComboBox").value());
                                                            getBankRecipientComboByFundClientPK(dataItemX.FundClientPK);
                                                            getEstimatedCashProjection(dataItemX.UnitAmount, $("#FundPK").data("kendoComboBox").value());
                                                            WinListFundClient.close();

                                                        }
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                //clearData();
                                                $("#FundClientID").val(dataItemX.FundClientName);
                                                $("#UnitPosition").data("kendoNumericTextBox").value(dataItemX.UnitAmount);
                                                $("#UnitAmount").data("kendoNumericTextBox").value(0);
                                                $("#CashAmount").data("kendoNumericTextBox").value(0);
                                                $("#TotalUnitAmount").data("kendoNumericTextBox").value(0);
                                                $("#TotalCashAmount").data("kendoNumericTextBox").value(0);
                                                $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                $("#CashRefPK").data("kendoComboBox").value("");
                                                $("#AgentPK").data("kendoComboBox").value("");
                                                getFundClientCashRefByFundClientPK($("#FundPK").val(), dataItemX.FundClientPK);
                                                getAgentByFundClientPK();
                                                getDefaultBankRecipientComboByFundClientPK(dataItemX.FundClientPK, $("#FundPK").data("kendoComboBox").value());
                                                getBankRecipientComboByFundClientPK(dataItemX.FundClientPK);
                                                getEstimatedCashProjection(dataItemX.UnitAmount, $("#FundPK").data("kendoComboBox").value());
                                                WinListFundClient.close();

                                            }


                                        } else {
                                            alertify.alert("Fund Client Can't Redempt!");
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

    function getDataSourceListFundClientBankVA(_url) {



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
                    WinListFundClientBankVA.close();
                },
                pageSize: 100,
                schema: {
                    model: {
                        fields: {
                            FundClientBankVAPK: { type: "number" },
                            FundClientBankVAName: { type: "string" },
                            UnitAmount: { type: "number" },
                            ID: { type: "string" },
                            IFUA: { type: "string" },
                        }
                    }
                }
            });



    }

    $("#btnListFundClientBankVA").click(function () {
        initListFundClientBankVA();

        WinListFundClientBankVA.center();
        WinListFundClientBankVA.open();
        //htmlFundClientBankVAPK = "#BankRecipientPK";
        htmlFundClientBankVAID = "#FundClientBankVA";



    });

    function initListFundClientBankVA() {
        if ($("#FundPK").data("kendoComboBox").value() == "" || $("#FundPK").data("kendoComboBox").value() == null) {
            alertify.alert("Please Input Fund First");
            WinListFundClientBankVA.close();
            return;
        }
        if ($("#NAVDate").data("kendoDatePicker").value() == "" || $("#NAVDate").data("kendoDatePicker").value() == null) {
            alertify.alert("Please Input NAV Date First");
            WinListFundClientBankVA.close();
            return;
        }
        var _url = window.location.origin + "/Radsoft/FundClient/GetFundClientBankVA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val();
        var dsListFundClientBankVA = getDataSourceListFundClientBankVA(_url);
        $("#gridListFundClientBankVA").kendoGrid({
            dataSource: dsListFundClientBankVA,
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
                { command: { text: "Select", click: ListFundClientBankVASelect }, title: " ", width: 85 },
                { field: "BankRecipientPK", title: "Bank ID", hidden: true, width: 300 },
                { field: "FundClientPK", title: "Client Name", hidden: true, width: 300 },
                { field: "BankName", title: "BankName", width: 300 },
                { field: "AccountNo", title: "AccountNo", width: 200 },
                { field: "AccountName", title: "AccountName", width: 300 }

            ]
        });
    }

    function ListFundClientBankVASelect(e) {
        var grid = $("#gridListFundClientBankVA").data("kendoGrid");
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
                                    url: window.location.origin + "/Radsoft/FundClient/CheckClientCantRedempt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            //clearData();
                                            $("#BankRecipientPK").data("kendoComboBox").value(dataItemX.BankRecipientPK);
                                            $(htmlFundClientBankVAID).val(dataItemX.BankName + " - " + dataItemX.AccountNo);
                                            WinListFundClientBankVA.close();
                                        } else {
                                            alertify.alert("Fund Client Can't Redempt!");
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

    $("#BtnPosting").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        
        alertify.confirm("Are you sure want to Posting Client Redemption?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alertify.alert("Client Redemption Already Posted / Revised, Posting Cancel");
            } else {
                if (e) {

                    var ClientRedemption = {
                        ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        PostedBy: sessionStorage.getItem("user"),
                        FundPK: $('#FundPK').val(),
                        FundClientPK: $('#FundClientPK').val(),
                        TotalCashAmount: $('#TotalCashAmount').val(),
                        TotalUnitAmount: $('#TotalUnitAmount').val(),
                        ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_Posting",
                        type: 'POST',
                        data: JSON.stringify(ClientRedemption),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#State").removeClass("ReadyForPosting").removeClass("UnPosted").addClass("Posted");
                            $("#State").text("POSTED");
                            $("#PostedBy").text(sessionStorage.getItem("user"));
                            $("#Posted").prop('checked', true);
                            $("#UnPosted").prop('checked', false);
                            alertify.alert(data);
                            win.close();
                            refresh();
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
            }
        });

    });

    $("#BtnRevise").click(function (e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
        }
        
        alertify.confirm("Are you sure want to Revised Client Redemption?", function (e) {
            if ($("#State").text() == "Revised") {
                alert("Client Redemption Already Posted / Revised, Revised Cancel");
            } else {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientRedemption",
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
                                            var ClientRedemption = {
                                                ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                RevisedBy: sessionStorage.getItem("user"),
                                                FundPK: $('#FundPK').val(),
                                                FundClientPK: $('#FundClientPK').val(),
                                                UnitAmount:$('#UnitAmount').val(),
                                                TotalCashAmount: $('#TotalCashAmount').val(),
                                                TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_Revise",
                                                type: 'POST',
                                                data: JSON.stringify(ClientRedemption),
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
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                }
                                            });
                                //        } else {
                                //            alertify.alert("Please check today/yesterday EndDayTrails!");

                                //        }
                                //    },
                                //    error: function (data) {
                                //        alertify.alert(data.responseText);

                                //    }
                                //});
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

    $("#BtnRecalculate").click(function (e) {
        ClientRedemptionRecalculate();
    });

    function ClientRedemptionRecalculate() {
        if ($("#NAVDate").val() == null || $("#NAVDate").val() == "") {
            alertify.alert("Please Fill NAV Date First");
            return;
        }
        else if ($("#FundPK").val() == null || $("#FundPK").val() == "") {
            alertify.alert("Please Fill Fund First");
            return;
        }
        else if ($('#UnitPosition').val() > 0)
        {
            if ((parseFloat($('#UnitAmount').val() - $('#UnitPosition').val())) > 0) {
                alertify.alert("Unit Amount > Unit Position");
                return;
            }
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _feepercent = 0;
                _feepercent = $('#RedemptionFeePercent').val();
                if ((parseFloat(data.RedemptionFeePercent - _feepercent)) >= 0) {
                    var ParamClientRedemptionRecalculate = {
                        ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                        FundPK: $('#FundPK').val(),
                        NavDate: kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        CashAmount: $('#CashAmount').val(),
                        UnitAmount: $('#UnitAmount').val(),
                        FeeType: $('#FeeType').val(),
                        RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                        AgentFeePercent: $('#AgentFeePercent').val(),
                        RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                        AgentFeeAmount: $('#AgentFeeAmount').val(),
                        UpdateUsersID: sessionStorage.getItem("user"),
                        LastUpdate: kendo.toString($("#LastUpdate").data("kendoDatePicker").value(), "MM-dd-yy HH:mm:ss")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/ClientRedemptionRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ParamClientRedemptionRecalculate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAV").data("kendoNumericTextBox").value(data.Nav);
                            $("#CashAmount").data("kendoNumericTextBox").value(data.CashAmount);
                            $("#UnitAmount").data("kendoNumericTextBox").value(data.UnitAmount);
                            $("#RedemptionFeePercent").data("kendoNumericTextBox").value(data.RedemptionFeePercent);
                            $("#AgentFeePercent").data("kendoNumericTextBox").value(data.AgentFeePercent);
                            $("#RedemptionFeeAmount").data("kendoNumericTextBox").value(data.RedemptionFeeAmount);
                            $("#AgentFeeAmount").data("kendoNumericTextBox").value(data.AgentFeeAmount);
                            $("#TotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmount);
                            $("#TotalUnitAmount").data("kendoNumericTextBox").value(data.TotalUnitAmount);
                            //refresh();
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
                    alertify.alert("Redemption Fee Percent > Max Redemption Fee Percent in this Fund");
                    $.unblockUI();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
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
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)


        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    DateFrom: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    DateTo: kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    UnitRegistrySelected: stringClientRedemptionSelected,
                    UnitRegistryType: "ClientRedemption",
                };



                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/ValidateCheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientRedemption",
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoffBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == "") {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/ClientRedemption/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                            type: 'POST',
                                            data: JSON.stringify(ClientRedemption),
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

    $("#BtnUnApproveBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)


        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
            if (e) {

                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };


                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/ValidateUnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
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
                            alertify.alert("Client Redemption Already Posted");
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

    $("#BtnRejectBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)


        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
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
        });
    });

    $("#BtnPostingBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)

        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };
                if (kendo.toString($("#DateFrom").data("kendoDatePicker").value(), 'MM-dd-yy') == kendo.toString($("#DateTo").data("kendoDatePicker").value(), 'MM-dd-yy')) {
                    $.blockUI();
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CloseNav/ValidateCheckCloseNavApproveForUnitRegistry/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                        type: 'POST',
                        data: JSON.stringify(ClientRedemption),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientRedemption/ValidateApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'POST',
                                    data: JSON.stringify(ClientRedemption),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientRedemption/ValidatePostingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == 1) {
                                                        alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Redemption Already Posting");
                                                        $.unblockUI();
                                                    }
                                                    else if (data == 2) {
                                                        alertify.alert("Posting Cancel, Date is Holiday !");
                                                        $.unblockUI();

                                                    } else {
                                                        if (_GlobClientCode == "03") {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/ClientRedemption/ValidateEDTUnitBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data == 1) {
                                                                        alertify.alert("Posting Cancel, Must be Generate End Day Trails Today First / Client Redemption Already Posting");
                                                                        $.unblockUI();
                                                                    }
                                                                    else {
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/ClientRedemption/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                            type: 'POST',
                                                                            data: JSON.stringify(ClientRedemption),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data);
                                                                                refresh();


                                                                                for (var i in checkedApproved) {
                                                                                    checkedApproved[i] = null
                                                                                }

                                                                                if (_GlobClientCode == "29") {
                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/HostToHostTeravin/BalanceUpdate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                                        type: 'GET',
                                                                                        contentType: "application/json;charset=utf-8",
                                                                                        success: function (data) {
                                                                                            console.log(data);
                                                                                        },
                                                                                        error: function (data) {
                                                                                            alertify.alert(data.responseText);
                                                                                        }
                                                                                    });

                                                                                    $.ajax({
                                                                                        url: window.location.origin + "/Radsoft/HostToHostTeravin/TransactionUpdate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/ClientRedemption/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                type: 'POST',
                                                                data: JSON.stringify(ClientRedemption),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data);
                                                                    refresh();


                                                                    for (var i in checkedApproved) {
                                                                        checkedApproved[i] = null
                                                                    }

                                                                    if (_GlobClientCode == "29") {
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/HostToHostTeravin/BalanceUpdate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                console.log(data);
                                                                            },
                                                                            error: function (data) {
                                                                                alertify.alert(data.responseText);
                                                                            }
                                                                        });

                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/HostToHostTeravin/TransactionUpdate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

                                                                },
                                                                error: function (data) {
                                                                    alertify.alert(data.responseText);
                                                                }
                                                            });
                                                        }
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });

                                        } else {
                                            alertify.alert("NAV Must Ready First, Please Click Get NAV");
                                            $.unblockUI();
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
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
                    alertify.alert("Please Posting Client Redemption by Day");
                }
            }
        });
    });

    $("#BtnGetNavBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)

        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }


        alertify.confirm("Are you sure want to Get Nav by Selected Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/GetNavBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
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
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)

        if (stringClientRedemptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/ValidateVoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientRedemption),
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
                            alertify.alert("Client Redemption Already Posted");
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

    // Untuk Form Listing
    function showBatchAmount(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to open Amount Batch Form ?", function (a) {
                if (a) {
                    // initGrid()
                    if (tabindex == 0 || tabindex == undefined) {
                        var grid = $("#gridClientRedemptionApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientRedemptionPending").data("kendoGrid");
                    }
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    var _clientRedemptionPK = _dataItemX.ClientRedemptionPK;
                    var _status = _dataItemX.Status;
                    var _fund = _dataItemX.FundPK;
                    var _navDate = _dataItemX.NAVDate;
                    var Batch = {
                        ClientRedemptionPK: _clientRedemptionPK,
                        Status: _status,
                        FundPK: _fund,
                        NAVDate: kendo.toString(kendo.parseDate(_navDate), 'MM/dd/yy'),
                        PostedBy: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/ClientRedemption_AmountBatchForm/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }

    function showBatchUnit(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to open Unit Batch Form ?", function (a) {
                if (a) {
                    // initGrid()
                    if (tabindex == 0 || tabindex == undefined) {
                        var grid = $("#gridClientRedemptionApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientRedemptionPending").data("kendoGrid");
                    }
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    var _clientRedemptionPK = _dataItemX.ClientRedemptionPK;
                    var _status = _dataItemX.Status;
                    var _fund = _dataItemX.FundPK;
                    var _navDate = _dataItemX.NAVDate;
                    var Batch = {
                        ClientRedemptionPK: _clientRedemptionPK,
                        Status: _status,
                        FundPK: _fund,
                        NAVDate: kendo.toString(kendo.parseDate(_navDate), 'MM/dd/yy'),
                        PostedBy: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/ClientRedemption_UnitBatchForm/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }

    $("#BtnBatchUnitFormBySelected").click(function (e) {

        var stringClientRedemptionSelected = '';
        var AllPending = 0;
        AllPending = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                AllPending.push(i);
            }
        }

        var ArrayPendingFundFrom = AllPending;
        var stringClientRedemptionSelectedPending = '';

        for (var i in ArrayPendingFundFrom) {
            stringClientRedemptionSelectedPending = stringClientRedemptionSelectedPending + ArrayPendingFundFrom[i] + ',';

        }
        stringClientRedemptionSelectedPending = stringClientRedemptionSelectedPending.substring(0, stringClientRedemptionSelectedPending.length - 1)


        var AllApproved = 0;
        AllApproved = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                AllApproved.push(i);
            }
        }

        var ArrayApprovedFundFrom = AllApproved;
        var stringClientRedemptionSelectedApproved = '';

        for (var i in ArrayApprovedFundFrom) {
            stringClientRedemptionSelectedApproved = stringClientRedemptionSelectedApproved + ArrayApprovedFundFrom[i] + ',';

        }
        stringClientRedemptionSelectedApproved = stringClientRedemptionSelectedApproved.substring(0, stringClientRedemptionSelectedApproved.length - 1)


        if (tabindex == 0 || tabindex == undefined) {
            if (stringClientRedemptionSelectedApproved == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientRedemptionSelected = stringClientRedemptionSelectedApproved;

        }
        else if (tabindex = 1) {
            if (stringClientRedemptionSelectedPending == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientRedemptionSelected = stringClientRedemptionSelectedPending;

        }


        alertify.confirm("Are you sure want to Batch Form by Selected Data ?", function (e) {
            if (e) {
                var Batch = {
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),

                    UnitRegistrySelected: stringClientRedemptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/BatchUnitFormBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    $("#BtnBatchAmountFormBySelected").click(function (e) {

        var stringClientRedemptionSelected = '';
        var AllPending = 0;
        AllPending = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                AllPending.push(i);
            }
        }

        var ArrayPendingFundFrom = AllPending;
        var stringClientRedemptionSelectedPending = '';

        for (var i in ArrayPendingFundFrom) {
            stringClientRedemptionSelectedPending = stringClientRedemptionSelectedPending + ArrayPendingFundFrom[i] + ',';

        }
        stringClientRedemptionSelectedPending = stringClientRedemptionSelectedPending.substring(0, stringClientRedemptionSelectedPending.length - 1)


        var AllApproved = 0;
        AllApproved = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                AllApproved.push(i);
            }
        }

        var ArrayApprovedFundFrom = AllApproved;
        var stringClientRedemptionSelectedApproved = '';

        for (var i in ArrayApprovedFundFrom) {
            stringClientRedemptionSelectedApproved = stringClientRedemptionSelectedApproved + ArrayApprovedFundFrom[i] + ',';

        }
        stringClientRedemptionSelectedApproved = stringClientRedemptionSelectedApproved.substring(0, stringClientRedemptionSelectedApproved.length - 1)


        if (tabindex == 0 || tabindex == undefined) {
            if (stringClientRedemptionSelectedApproved == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientRedemptionSelected = stringClientRedemptionSelectedApproved;

        }
        else if (tabindex = 1) {
            if (stringClientRedemptionSelectedPending == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientRedemptionSelected = stringClientRedemptionSelectedPending;

        }


        alertify.confirm("Are you sure want to Batch Form by Selected Data ?", function (e) {
            if (e) {
                var Batch = {
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    DownloadMode: $('#DownloadMode').val(),
                    UnitRegistrySelected: stringClientRedemptionSelected,

                };
                if (_GlobClientCode == "10") {
                    _url = window.location.origin + "/Radsoft/ClientRedemption/BatchAmountFormBySelectedDataMandiri/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                } else if (_GlobClientCode == "11") {
                    _url = window.location.origin + "/Radsoft/ClientRedemption/BatchAmountFormBySelectedDataTaspen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                }
                else {
                    _url = window.location.origin + "/Radsoft/ClientRedemption/BatchAmountFormBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
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

    $("#BtnPaymentReportListing").click(function () {
        ShowPaymentReportListing();
    });

    // Untuk Form Listing
    function ShowPaymentReportListing(e) {
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
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundIDFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeParamFundIDFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#ParamFundIDTo").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeParamFundIDTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeParamFundIDFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamFundIDTo").data("kendoComboBox").value($("#ParamFundIDFrom").data("kendoComboBox").value());

        }


        function OnChangeParamFundIDTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        $("#ParamPaymentDateFrom").data("kendoDatePicker").value(new Date);
        $("#ParamPaymentDateTo").data("kendoDatePicker").value(new Date);


        WinPaymentReportListing.center();
        WinPaymentReportListing.open();

    }

    $("#BtnOkPaymentReportListing").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var PaymentReportListing = {
                    ParamPaymentDateFrom: $('#ParamPaymentDateFrom').val(),
                    ParamPaymentDateTo: $('#ParamPaymentDateTo').val(),
                    ParamFundIDFrom: $("#ParamFundIDFrom").data("kendoComboBox").text(),
                    ParamFundIDTo: $("#ParamFundIDTo").data("kendoComboBox").text(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").text(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/PaymentReportListing/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(PaymentReportListing),
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

    $("#BtnCancelPaymentReportListing").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinPaymentReportListing.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    function onWinPaymentReportListingClose() {
        $("#ParamPaymentDateFrom").data("kendoDatePicker").value(null),
        $("#ParamPaymentDateTo").data("kendoDatePicker").value(null),
        $("#ParamFundIDFrom").data("kendoComboBox").text(""),
        $("#ParamFundIDTo").data("kendoComboBox").text("")
    }

    function getDefaultBankRecipientComboByFundClientPK(_fundClientPK, _fundPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetDefaultBankRecipientComboByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRecipientPK").data("kendoComboBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    }

    function getBankRecipientComboByFundClientPK(_fundClientPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetBankRecipientComboByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRecipientPK").kendoComboBox({
                    dataValueField: "BankRecipientPK",
                    dataTextField: "AccountNo",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankRecipient,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankRecipient() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

    }

    $("#BtnSInvestRedemptionRptBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)

        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    UnitRegistrySelected: stringClientRedemptionSelected,
                };
           
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientRedemption/SInvestRedemptionRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(ClientRedemption),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#downloadRadsoftFile").attr("href", data);
                            $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_TrxRedemption.txt");
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
            if ($("#FundPK").val() == 3) {
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
                if ($("#FundPK").val() == "" || $("#FundPK").val() == 0) {
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
                        url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
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
            if ($("#FundPK").val() == "" || $("#FundPK").val() == 0) {
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
                    url: window.location.origin + "/Radsoft/Fund/GetDefaultPaymentDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
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

    function getFundClientCashRefByFundClientPK(_fundPK, _fundClientPK) {
        //check data fundclietcashref
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClientCashRef/CheckDataFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 1) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefByFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK + "/RED",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#CashRefPK").kendoComboBox({
                                dataValueField: "FundCashRefPK",
                                dataTextField: "ID",
                                dataSource: data,
                                filter: "contains",
                                suggest: true,
                                index: 0,

                            });
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/RED",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#CashRefPK").kendoComboBox({
                                dataValueField: "FundCashRefPK",
                                dataTextField: "ID",
                                dataSource: data,
                                filter: "contains",
                                suggest: true,
                            });
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

    function getEstimatedCashProjection(_unitPosition, _fundPK)
    {
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


    //function getRedemptionAll(_fundPK,_fundClientPK) {

    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/ClientRedemption/GetUnitRedemptionAll/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            $("#UnitPosition").data("kendoNumericTextBox").value(data);
    //            $("#UnitAmount").data("kendoNumericTextBox").value(data);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });

    //}

    //function CheckRedemptionPending() {

    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/ClientRedemption/GetUnitRedemptionAll/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            $("#UnitPosition").data("kendoNumericTextBox").value(data);
    //            $("#UnitAmount").data("kendoNumericTextBox").value(data);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });

    //}



    $("#BtnHoldingPeriod").click(function () {
        showHoldingPeriod();
    });

    // Untuk Form Listing
    function showHoldingPeriod(e) {
        LoadData();
            
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#FundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $("#BitAllClient").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGrid").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGrid").data("kendoButton").enable(true);
            }
        });


        $("#ShowGrid").click(function () {
            WinFundClient.center();
            WinFundClient.open();
            LoadData();
        });

        WinHoldingPeriod.center();
        WinHoldingPeriod.open();

    }

    $("#CloseGrid").click(function () {
        var All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }
        WinFundClient.close();
    });

    function LoadData() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/FundClient/GetFundClientComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            //cache: false,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundClientPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },
                        IFUA: { type: "string" },
                        SID: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridFundClient = $("#gridFundClient").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            //define dataBound event handler
            dataBound: onDataBound,
            selectable: "multiple",
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                //define template column with checkbox and attach click event handler
                {
                    //select all on grid
                    headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    //end select all on grid
                    template: "<input type='checkbox' class='checkbox'/>"
                }

                , {
                    field: "ID",
                    title: "Client ID",
                    width: "120px"
                }, {
                    field: "Name",
                    title: "Client Name",
                    width: "300px"
                }, {
                    field: "IFUA",
                    title: "IFUA Code",
                    width: "185px"
                }, {
                    field: "SID",
                    title: "SID",
                    width: "150px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");



        //bind click event to the checkbox
        gridFundClient.table.on("click", ".checkbox", selectRow);

        //select all on grid
        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {



            //var checked = ev.target.checked;

            oldPageSize = gridFundClient.dataSource.pageSize();
            gridFundClient.dataSource.pageSize(gridFundClient.dataSource.data().length);

            $('.checkbox').each(function (idx, item) {
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

            //$('.checkbox').click();

            gridFundClient.dataSource.pageSize(oldPageSize);

        });



        //end select all on grid


        //on click of the checkbox:
        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridFundClient = $("#gridFundClient").data("kendoGrid"),
                dataItemZ = gridFundClient.dataItem(rowA);

            checkedIds[dataItemZ.FundClientPK] = checked;
            if (checked) {
                //-select the row
                rowA.addClass("k-state-selected");



            } else {
                //-remove selection
                rowA.removeClass("k-state-selected");
            }
        }

        //on dataBound event restore previous selected rows:
        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundClientPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkbox")
                        .attr("checked", "checked");
                }
            }
        }
    }

    function InitDataSourceHoldingPeriod() {

        var All = 0;
        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
        }

        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }
        }


        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        if (stringFundFrom == "") {
            stringFundFrom = "0";
        }

        if (stringFundClientFrom == "") {
            stringFundClientFrom = "0";
        }

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                      url: window.location.origin + "/Radsoft/ClientRedemption/InitDataHoldingPeriod/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + stringFundFrom + "/" + stringFundClientFrom + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

                                  FundID: { type: "string" },
                                  FundClientName: { type: "string" },
                                  ValueDate: { type: "date" },
                                  RedempDate: { type: "date" },
                                  HoldingPeriod: { type: "number" },
                                  TotalSubs: { type: "number" },
                                  TakenOut: { type: "number" },
                                  Remaining: { type: "number" },
                                  TotalFeeAmount: { type: "number" },


                              }
                          }
                },
                group: {
                    field: "FundID", aggregates: [
                        { field: "Remaining", aggregate: "sum" },
                        { field: "TotalFeeAmount", aggregate: "sum" },
                    ]
                },
                aggregate: [
                    { field: "Remaining", aggregate: "sum" },
                    { field: "TotalFeeAmount", aggregate: "sum" },
                ],
            });
    }
   
    //$("#ParamBitListing").click(function () {
    //    if ($("#ParamBitListing").prop('checked') == true) {
    //        $("#gridHoldingPeriod").empty();
    //        $("#detailHoldingPeriod").show();
    //        InitHoldingPeriod();
    //    }
    //    else {
    //        $("#detailHoldingPeriod").hide();
    //        $("#gridHoldingPeriod").empty();
    //    }


    //});

    function InitHoldingPeriod() {
        $("#gridHoldingPeriod").empty();
        var dsListHoldingPeriod = InitDataSourceHoldingPeriod();
        var gridHoldingPeriod = $("#gridHoldingPeriod").kendoGrid({
            dataSource: dsListHoldingPeriod,
            height: 500,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [
                { field: "FundID", title: "Fund",  width: 120 },
                { field: "FundClientName", title: "Fund Client", width: 150 },
                { field: "ValueDate", title: "Subs Date", width: 120, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "RedempDate", title: "Red Date", width: 120, template: "#= kendo.toString(kendo.parseDate(RedempDate), 'dd/MMM/yyyy')#" },
                { field: "HoldingPeriod", title: "Holding Period (Month)", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 }, 
                { field: "TotalSubs", title: "Total Subs", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "TakenOut", title: "Taken Out", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "Remaining", title: "Remaining Balance", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", },
                { field: "RedempFeePercent", title: "Red Fee Percent", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 100 },
                { field: "TotalFeeAmount", title: "Total Fee Amount", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120, groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", },

            ]
        }).data("kendoGrid");



    }

    function onWinHoldingPeriodClose() {
        $("#ParamBitListing").prop('checked', false);
        $("#ParamHoldingFund").val("")
        $("#ParamHoldingFundClient").val("")
        $("#detailHoldingPeriod").hide();
        $("#gridHoldingPeriod").empty();
    }

    $("#BtnRefreshHoldingPeriod").click(function () {
        refreshHoldingPeriod();
    });

    function refreshHoldingPeriod() {
            InitHoldingPeriod();
    }

    // Matured Fund
    $("#BtnMaturedFund").click(function () {
        showMaturedFund();
    });

    // Untuk Form Listing
    function showMaturedFund(e) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamMaturedFund").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamMaturedFund,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeParamMaturedFund() {

            if (this.value() && this.selectedIndex == -1) {
                alert("Please Fill Fund");
                var dt = this.dataSource._data[0];
                this.text('');

            }
        }


        WinMaturedFund.center();
        WinMaturedFund.open();

    }

    $("#BtnOkMaturedFund").click(function () {


        alertify.confirm("Are you sure want to Insert Client Redemption For Matured Fund ?", function (e) {
            if (e) {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/ValidateInsertClientRedemptionAllMaturedFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamMaturedFund").val() + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientRedemption/InsertClientRedemptionAllMaturedFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamMaturedFund").val() + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
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

                        } else {
                            alertify.alert("Insert Cancel, Please Check Client Redemption Pending/Approve in this day !");
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

    $("#BtnCancelMaturedFund").click(function () {

        alertify.confirm("Are you sure want to cancel Insert Client Redemption ALL for Matured Fund?", function (e) {
            if (e) {
                WinMaturedFund.close();
                alertify.alert("cancel Insert Client Redemption");
            }
        });
    });

    function onWinMaturedFundClose() {
        $("#ParamMaturedFund").data("kendoComboBox").text("")
    }

    $("#BtnCashInstructionBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientRedemptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientRedemptionSelected = stringClientRedemptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientRedemptionSelected = stringClientRedemptionSelected.substring(0, stringClientRedemptionSelected.length - 1)

        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                var ClientRedemption = {
                    ClientRedemptionSelected: stringClientRedemptionSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/CashInstructionBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alert("Success")
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    WinValidationRed = $("#WinValidationRed").kendoWindow({
        height: 500,
        title: "* Validation",
        visible: false,
        width: 1200,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 200 })
        }
    }).data("kendoWindow");

    function getDataSourceValidation(_url) {
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
                            Reason: { type: "string", editable: false },
                            Notes: { type: "string", editable: true },
                        }
                    }
                }
            });
    }

    function initGridValidation() {
        $("#gridValidationClientRedemption").empty();

        var ClientRedemptionPendingURL = window.location.origin + "/Radsoft/ClientRedemption/ValidateClientRedemption/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashAmount").val() + "/" + $("#UnitAmount").val() + "/" + $("#FundPK").val() + "/" + $("#FundClientPK").val() + "/" + $("#BitRedemptionAll").val(),
            dataSourceApproved = getDataSourceValidation(ClientRedemptionPendingURL);

        var grid = $("#gridValidationClientRedemption").kendoGrid({
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


        WinValidationRed.center();
        WinValidationRed.open();
    }


    $("#BtnContinueValidation").click(function () {

        var _FundClientPK = 0;
        var _ValueDate = '';

        _ValueDate = kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
        _FundClientPK = $('#FundClientPK').val();

        //var _reference;
        //if (_GlobClientCode == "20") {

        //    $.ajax({
        //        url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RED/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //        type: 'GET',
        //        async: false,
        //        contentType: "application/json;charset=utf-8",
        //        success: function (data) {
        //            _reference = data;
        //        },
        //        error: function (data) {
        //            alertify.alert(data.responseText);
        //        }
        //    });
        //}
        //else {
        //    _reference = $('#ReferenceSInvest').val();
        //}
        var val = validateData();
        var _CustomMessage = '';

        if (_GlobClientCode == "20")
            _CustomMessage = " and transfer redemption amount to " + $('#BankRecipientPK').data("kendoComboBox").text();


        alertify.confirm("Are you sure want to Add data " + _CustomMessage + "?", function (e) {
            if (e) {

                var _Validation = [];
                var flagChange = 0;
                var gridDataArray = $('#gridValidationClientRedemption').data('kendoGrid')._data;
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

                GetSinvestReference();

                //insert client redemption
                var ClientRedemption = {
                    ClientRedemptionPK: $('#ClientRedemptionPK').val(),
                    Type: $('#Type').val(),
                    NAVDate: $('#NAVDate').val(),
                    ValueDate: $('#ValueDate').val(),
                    PaymentDate: $('#PaymentDate').val(),
                    NAV: $('#NAV').val(),
                    FundPK: $('#FundPK').val(),
                    FundClientPK: $('#FundClientPK').val(),
                    CashRefPK: $('#CashRefPK').val(),
                    CurrencyPK: $('#CurrencyPK').val(),
                    Description: $('#Description').val(),
                    ReferenceSInvest: globReference,
                    CashAmount: $('#CashAmount').val(),
                    UnitAmount: $('#UnitAmount').val(),
                    TotalCashAmount: $('#TotalCashAmount').val(),
                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                    RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                    RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                    AgentPK: $('#AgentPK').val(),
                    AgentFeePercent: $('#AgentFeePercent').val(),
                    AgentFeeAmount: $('#AgentFeeAmount').val(),
                    UnitPosition: $('#UnitPosition').val(),
                    BankRecipientPK: $('#BankRecipientPK').val(),
                    TransferType: $('#TransferType').val(),
                    FeeType: $('#FeeType').val(),
                    BitRedemptionAll: $('#BitRedemptionAll').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientRedemption_I",
                    type: 'POST',
                    data: JSON.stringify(ClientRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinValidationRed.close();
                        win.close();
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

        WinValidationRed.close();
    });

    function GetSinvestReference() {
        if (_GlobClientCode == "20") {
            $.ajax({
                url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RED/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    globReference = data;
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        else {
            globReference = $('#ReferenceSInvest').val();
        }
    }



});
