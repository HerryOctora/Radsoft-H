$(document).ready(function () {
    document.title = 'FORM CLIENT SUBSCRIPTION';
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
    var globReference;

    //end Fund grid



    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    if (_GlobClientCode == "10") {
        $("#BtnOpenImportTransactionPromo").show();
        $("#SumberDana").attr("required", true);
        $("#LblTrxUnitPaymentType").show();
        $("#LblTrxUnitPaymentProvider").show();
        $("#LblDownloadMode").hide();
        $("#tdDownloadMode").hide();
    }
    else {
        $("#BtnOpenImportTransactionPromo").hide();
        $("#LblTrxUnitPaymentType").hide();
        $("#LblTrxUnitPaymentProvider").hide();
        $("#LblDownloadMode").show();
        $("#tdDownloadMode").show();
    }

    if (_GlobClientCode == "08") {
        $("#lblTransactionPK").show();
    }
    else {
        $("#lblTransactionPK").hide();
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
    }
    else {
        $("#lblEntryDate").show();
        $("#lblTradeDate").hide();
        $("#NAVDate").data("kendoDatePicker").enable(true);
        $("#lblSignature").hide();
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

        $("#BtnApproveBySelected").kendoButton({//Done
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelected").kendoButton({//Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({//Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({ //Skip Karena Validasi Gk ada
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });

        $("#BtnVoidBySelected").kendoButton({ //Done
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        }); 

        $("#BtnCashInstructionBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnGetNavBySelected").kendoButton({ //Aprroved
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
        $("#BtnSInvestSubscriptionRptBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        
        $("#BtnRegularForm").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOpenImportTransactionPromo").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportGoodFund").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportPromoMoInvest").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnAddAgentTrx").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnUploadSubcription").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });


        if (_GlobClientCode == "25" || _GlobClientCode == "21") {
            $("#BtnUploadSubcription").show();
        }
        else {
            $("#BtnUploadSubcription").hide();
        }

    }

    function initWindow() {
        ////Reguler Date
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/RegulerInstruction/GetAutoDebitDateFromRegulerInstruction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#ParamAutoDebitDate").kendoComboBox({
        //            dataValueField: "RefNo",
        //            dataTextField: "Reference",
        //            dataSource: data,
        //            filter: "contains",
        //            suggest: true,
        //            index: 0
        //        });

        //    },
        //    error: function (data) {
        //        alertify.alert("Please Fill Data Report Correctly");
        //    }
        //});

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



        //$("#PaymentDate").kendoDatePicker({
        //    format: "dd/MMM/yyyy",
        //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]

        //});
        $("#NAVDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeNAVDate
        });

        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        function OnChangeValueDate() {
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 3,
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
                //    },
                //    error: function (data) {
                //        alertify.alert(data.responseText);
                //    }
                //});

                // Cek Jam, klo diatas jam 12 Pake NAV bsok,klo dibawah Jam 12 Pake NAV hari itu juga
                var currentTime = new Date();
                var hours = currentTime.getHours();
                if (hours <= 12) {
                    $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
                    ClientSubscriptionRecalculate();
                } else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 0,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAVDate").data("kendoDatePicker").value(new Date(data));
                            ClientSubscriptionRecalculate();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }

                $("#NAV").data("kendoNumericTextBox").value(0);
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
                            ClientSubscriptionRecalculate();
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

        win = $("#WinClientSubscription").kendoWindow({
            height: 1250,
            title: " Subscription Detail",
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


        WinListRegulerInstruction = $("#WinListRegulerInstruction").kendoWindow({
            height: 600,
            title: "List Reguler Instruction ",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListRegulerInstructionClose
        }).data("kendoWindow");

        function onWinListRegulerInstructionClose() {
            $("#gridListRegulerInstruction").empty();
        }

        WinGenerateTransactionPromo = $("#WinGenerateTransactionPromo").kendoWindow({
            height: 300,
            title: "Generate Transaction Promo ",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
        }).data("kendoWindow");

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

        WinValidationSubs = $("#WinValidationSubs").kendoWindow({
            height: 500,
            title: "* Validation",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 200 })
            }
        }).data("kendoWindow");

        WinValidationSubs = $("#WinValidationSubs").kendoWindow({
            height: 500,
            title: "* Validation",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 200 })
            }
        }).data("kendoWindow");

        WinValidationSubs = $("#WinValidationSubs").kendoWindow({
            height: 500,
            title: "* Validation",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 200 })
            }
        }).data("kendoWindow");


        //$("#searchGridData").kendoComboBox({
        //    dataTextField: "text",
        //    dataValueField: "value",
        //    dataSource: [
        //        { text: "ALL", value: "ALL" },
        //        { text: "TOP 200", value: "TOP200" }
        //    ],
        //    filter: "contains",
        //    suggest: true,
        //    change: OnChangeSearchGridData,
        //    value: setCmbSearchGridData()
        //});
        //function OnChangeSearchGridData() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //    refresh();
        //}
        //function setCmbSearchGridData() {
        //    if (_GlobClientCode == "21" || _GlobClientCode == "10")
        //        return "TOP200";
        //    else
        //        return "ALL";
        //}


    }

    var GlobValidator = $("#WinClientSubscription").kendoValidator().data("kendoValidator");

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
            $("#StatusHeader").val("NEW");
            $("#ValueDate").data("kendoDatePicker").value(_d);
            $("#NAVDate").data("kendoDatePicker").value($("#ValueDate").val());

        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            hidetransactionpromo($("#Type").val());
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridClientSubscriptionApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridClientSubscriptionPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridClientSubscriptionHistory").data("kendoGrid");
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

            $("#ClientSubscriptionPK").val(dataItemX.ClientSubscriptionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#NAVDate").data("kendoDatePicker").value(dataItemX.NAVDate);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            //$("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            $("#NAV").val(dataItemX.NAV);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundID").val(dataItemX.FundID);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
            //$("#CashRefPK").val(dataItemX.CashRefPK);
            //$("#CashRefID").val(dataItemX.CashRefID);
            $("#CurrencyPK").val(dataItemX.CurrencyPK);
            $("#CurrencyID").val(dataItemX.CurrencyID);
            $("#Description").val(dataItemX.Description);
            $("#ReferenceSInvest").val(dataItemX.ReferenceSInvest);
            $("#AgentPK").val(dataItemX.AgentPK);
            $("#AgentID").val(dataItemX.AgentID);
            $("#TransactionPK").val(dataItemX.TransactionPK);
            $("#SumberDana").val(dataItemX.SumberDana);
            $("#BitImmediateTransaction").prop('checked', dataItemX.BitImmediateTransaction);
            if (_GlobClientCode != "10") {
                GetSourceofFund(dataItemX.FundClientPK)
            }
            //$("#FundClientCashRefPK").val(dataItemX.FundClientCashRefPK);

            //if (_GlobClientCode == "08") {
            //    $("#lblTransactionPK").show();
            //}
            //else
            //{
            //    $("#lblTransactionPK").hide();
            //}



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


            CheckFundETF(dataItemX.FundPK);
        }




        //combo box Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SubscriptionType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeType,
                    value: setCmbType()
                });

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
            if (_GlobClientCode == "24") {
                if ($("#Type").val() == 3)
                    $("#CashAmount").attr("required", false);
                else
                    $("#CashAmount").attr("required", true);
            }
            else {
                $("#CashAmount").attr("required", true);
            }

            hidetransactionpromo($("#Type").val());
            SetTypeOnchange($("#Type").val());

        }

        function setCmbType() {
            if (e == null) {
                return 1;
            } else {
                if (dataItemX.Type == 0) {
                    SetType(0);
                    return 1;
                } else {
                    SetType(dataItemX.Type);
                    return dataItemX.Type;
                }
            }
        }

        function hidetransactionpromo(_type) {
            if (_type == 5) {
                $("#lblTransactionPromo").show();
            }
            else {
                $("#lblTransactionPromo").hide();
            }


        }

        function SetType(_type) {
            if (_type == 6) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(true);

            }
            else {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
            }
        }

        function SetTypeOnchange(_type) {
            if (_type == 3) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(false);

            }
            else if (_type == 6) {
                $("#UnitAmount").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(true);

            }
            else {
                $("#UnitAmount").data("kendoNumericTextBox").enable(false);
                $("#UnitAmount").data("kendoNumericTextBox").value(0);
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
            CheckFundETF(this.value());
            if (e != null) {
                if (this.value() == dataItemX.FundPK) {
                    return;
                } else {
                    ClientSubscriptionRecalculate();
                }
            }

            //clearData();
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
                            //$("#SubscriptionFeePercent").data("kendoNumericTextBox").value(data.SubscriptionFeePercent);
                            //$("#CashRefPK").data("kendoComboBox").value(data.DefaultBankSubscriptionPK);
                            $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                            $("#NAV").data("kendoNumericTextBox").value(0);
                            if ($("#FundClientPK").val() == "") {
                                getFundClientCashRefByFundClientPK(data.FundPK, 0);
                            }
                            else {
                                getFundClientCashRefByFundClientPK(data.FundPK, $("#FundClientPK").val());
                            }

                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
                else {
                    //getFundClientCashRefByFundClientPK(0,0);
                    $("#SubscriptionFeePercent").data("kendoNumericTextBox").value(0);
                    //$("#CashRefPK").data("kendoComboBox").value("");
                    $("#CurrencyPK").data("kendoComboBox").value("");

                }

            }

            //ClientSubscriptionRecalculate();
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
                            //ClientSubscriptionRecalculate();
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
                else {
                    $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
                    //ClientSubscriptionRecalculate();
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
        $("#CashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            change: onchangeCashAmount,
            value: setCashAmount(),
        });





        function onchangeCashAmount() {
            if (_GlobClientCode == "24") {
                if ($("#Type").val() == 3)
                    $("#CashAmount").attr("required", false);
                else
                    $("#CashAmount").attr("required", true);
            }
            else {
                $("#CashAmount").attr("required", true);
            }
            ClientSubscriptionRecalculate();


            //$("#TotalCashAmount").data("kendoNumericTextBox").value(0);
            //$("#TotalUnitAmount").data("kendoNumericTextBox").value(0);
        }



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
        $("#CashAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            }
        }


        $("#UnitAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            // format: "#.00000000",
            value: setUnitAmount(),
            change: onChangeUnitAmount
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

        function onChangeUnitAmount() {
            if ($("#Type").val() == 3)
                $("#TotalUnitAmount").data("kendoNumericTextBox").value($("#UnitAmount").val());

        }
        $("#UnitAmount").data("kendoNumericTextBox").enable(false);


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

        $("#SubscriptionFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setSubscriptionFeePercent(),
            change: onChangeSubscriptionFeePercent
        });
        function setSubscriptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeePercent;
                }
            }
        }

        $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        $("#SubscriptionFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            change: OnChangeSubsFeeAmount,
            value: setSubscriptionFeeAmount(),
        });
        $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(true);
        function setSubscriptionFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeeAmount;
                }
            }
        }
        function OnChangeSubsFeeAmount() {
            $("#SubscriptionFeePercent").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 2) {
                ClientSubscriptionRecalculate();
            }

        }

        $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(true);
        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);
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
            format: "n2",
            decimals: 2,
            change: OnChangeAgentFeeAmount,
            value: setAgentFeeAmount(),
        });

        if (dataItemX != null) {
            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
            }
        }
        $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
        function setAgentFeeAmount() {
            if (e == null) {
                return 0;
            } else {

                return dataItemX.AgentFeeAmount;
            }

        }
        function OnChangeAgentFeeAmount() {
            $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 2) {
                ClientSubscriptionRecalculate();
            }

        }
        function onChangeSubscriptionFeePercent() {
            $("#SubscriptionFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientSubscriptionRecalculate();
            }
        }
        function onChangeAgentFeePercent() {
            if ($("#AgentPK").data("kendoComboBox").value() == null || $("#AgentPK").data("kendoComboBox").value() == "") {
                alertify.alert("Cannot Change Agent Fee Percent If Agent is empty");
                $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
            }
            $("#AgentFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientSubscriptionRecalculate();
            }
        }

        //Combo Box Cash Ref 
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
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
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
            //  getFundClientCashRefByFundClientPK($("#FundPK").data("kendoComboBox").value(), $("#FundClientPK").val());

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
                    $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);
                    $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(true);

                    $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
                    $("#AgentFeePercent").data("kendoNumericTextBox").enable(true);
                } else {
                    $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(false);
                    $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(true);

                    $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
                    $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);

                }
                ClientSubscriptionRecalculate();
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
            $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);
        } else {
            if (dataItemX.FeeType == 1) {
                $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);
                $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(true);

                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
                $("#AgentFeePercent").data("kendoNumericTextBox").enable(true);
            } else {
                $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(false);
                $("#SubscriptionFeeAmount").data("kendoNumericTextBox").enable(true);

                $("#AgentFeeAmount").data("kendoNumericTextBox").enable(true);
                $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);
            }
        }

        var _Source = "";
        if (dataItemX == null) {
            _Source = "/IncomeSourceIND"
        }
        else if (dataItemX.InvestorType == 1) {
            _Source = "/IncomeSourceIND";
        }
        else if (dataItemX.InvestorType == 2) {
            _Source = "/IncomeSourceINS"
        }

        //combo box SumberDana Individual
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + _Source,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SumberDana").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSumberDana,
                    //value: setCmbSumberDana()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeSumberDana() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#FundClientPK").val() == 0) {
                alertify.alert("Choose Client First")
                $("#SumberDana").val("");
                $("#SumberDana").data("kendoComboBox").value("");

            }
            $("#SumberDana").val();

        }
        //function setCmbSumberDana() {
        //    if (dataItemX.SumberDana == 0) {
        //        return "";
        //    }
        //    else {
        //        return dataItemX.SumberDana;
        //    }

        //}


        //Combo Box TransactionPromo 
        $.ajax({
            url: window.location.origin + "/Radsoft/TransactionPromo/GetTransactionPromoCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TransactionPromoPK").kendoComboBox({
                    dataValueField: "TransactionPromoPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeTransactionPromoPK,
                    value: setCmbTransactionPromoPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeTransactionPromoPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbTransactionPromoPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TransactionPromoPK == 0) {
                    return "";
                } else {
                    return dataItemX.TransactionPromoPK;
                }
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
                if (dataItemX.TotalCashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.TotalCashAmount;
                }
            }
        }
        $("#TrxTotalCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxSubscriptionFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setTrxSubscriptionFeePercent(),
        });
        function setTrxSubscriptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeePercent;
                }
            }
        }

        $("#TrxSubscriptionFeePercent").data("kendoNumericTextBox").enable(false);


        $("#TrxSubscriptionFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxSubscriptionFeeAmount(),
        });

        function setTrxSubscriptionFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeeAmount;
                }
            }
        }
        $("#TrxSubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);

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
                if (dataItemX.TotalCashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.TotalCashAmount;
                }
            }
        }
        $("#TrxFeeTotalCashAmount").data("kendoNumericTextBox").enable(false);

        $("#TrxFeeSubscriptionFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setTrxFeeSubscriptionFeePercent(),
        });
        function setTrxFeeSubscriptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeePercent;
                }
            }
        }

        $("#TrxFeeSubscriptionFeePercent").data("kendoNumericTextBox").enable(false);


        $("#TrxFeeSubscriptionFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTrxFeeSubscriptionFeeAmount(),
        });

        function setTrxFeeSubscriptionFeeAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.SubscriptionFeeAmount == 0) {
                    return 0;
                } else {
                    return dataItemX.SubscriptionFeeAmount;
                }
            }
        }
        $("#TrxFeeSubscriptionFeeAmount").data("kendoNumericTextBox").enable(false);

        //Other Amount

        $("#OtherAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setOtherAmount(),
        });



        function setOtherAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.OtherAmount == 0) {
                    return "";
                } else {
                    return dataItemX.OtherAmount;
                }
            }
        }




        if (_GlobClientCode == '21') {
            initGridAgentTrx($("#ClientSubscriptionPK").val());
            initGridAgentFeeTrx($("#ClientSubscriptionPK").val());
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $("#AgentFeeAmount").data("kendoNumericTextBox").enable(false);
            $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);
        }
        else {
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).hide();
            $(GlobTabStrip.items()[2]).hide();

        }

        //combo box TrxUnitPaymentProviderPK
        $.ajax({
            url: window.location.origin + "/Radsoft/TrxUnitPaymentProvider/GetTrxUnitPaymentProviderCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxUnitPaymentProviderPK").kendoComboBox({
                    dataValueField: "TrxUnitPaymentProviderPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeTrxUnitPaymentProviderPK,
                    value: setCmbTrxUnitPaymentProviderPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeTrxUnitPaymentProviderPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbTrxUnitPaymentProviderPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TrxUnitPaymentProviderPK == 0) {
                    return "";
                } else {
                    return dataItemX.TrxUnitPaymentProviderPK;
                }
            }
        }


        //combo box TrxUnitPaymentTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/TrxUnitPaymentType/GetTrxUnitPaymentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxUnitPaymentTypePK").kendoComboBox({
                    dataValueField: "TrxUnitPaymentTypePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeTrxUnitPaymentTypePK,
                    value: setCmbTrxUnitPaymentTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeTrxUnitPaymentTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbTrxUnitPaymentTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TrxUnitPaymentTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.TrxUnitPaymentTypePK;
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

    function clearDataNav() {


        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#NAV").data("kendoNumericTextBox").value("");
        $("#SumberDana").data("kendoComboBox").value("");
        $("#CashAmount").data("kendoNumericTextBox").value("");
        $("#UnitAmount").data("kendoNumericTextBox").value("");
        $("#AgentPK").data("kendoComboBox").value("");
        //$("#CashAmount").data("kendoNumericTextBox").value("");
        //$("#SubscriptionFeePercent").data("kendoNumericTextBox").value("");
        //$("#SubscriptionFeeAmount").data("kendoNumericTextBox").value("");
        //$("#AgentFeeAmount").data("kendoNumericTextBox").value("");
        //$("#TotalCashAmount").data("kendoNumericTextBox").value($("#CashAmount").data("kendoNumericTextBox").value());
        //$("#TotalCashAmount").data("kendoNumericTextBox").value("");

    }

    function clearData() {
        $("#ReferenceSInvest").val("");
        $("#ClientSubscriptionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#NAVDate").data("kendoDatePicker").value(null);
        $("#ValueDate").data("kendoDatePicker").value(null);
        // $("#PaymentDate").data("kendoDatePicker").value(null);
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
        $("#SubscriptionFeePercent").val("");
        $("#SubscriptionFeeAmount").val("");
        $("#AgentPK").val("");
        $("#AgentID").val("");
        $("#AgentFeePercent").val("");
        $("#AgentFeeAmount").val("");
        $("#FeeType").val("");
        $("#SumberDana").data("kendoComboBox").value("");
        $("#Posted").val("");
        $("#Revised").val("");
        $("#PostedBy").val("");
        $("#PostedTime").val("");
        $("#RevisedBy").val("");
        $("#RevisedTime").val("");
        $("#BitImmediateTransaction").prop('checked', false);
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#TransactionPromo").val("");

        $("#TrxCashAmount").val("");
        $("#TrxTotalCashAmount").val("");
        $("#TrxSubscriptionFeePercent").val("");
        $("#TrxSubscriptionFeeAmount").val("");

        $("#TrxFeeCashAmount").val("");
        $("#TrxFeeTotalCashAmount").val("");
        $("#TrxFeeSubscriptionFeePercent").val("");
        $("#TrxFeeSubscriptionFeeAmount").val("");

        $("#TransactionPK").val("");
        $("#OtherAmount").val("");

        $("#TrxUnitPaymentProviderPK").val("");
        $("#TrxUnitPaymentTypePK").val("");
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
                            ClientSubscriptionPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Selected: { type: "boolean" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            NAVDate: { type: "date" },
                            ValueDate: { type: "date" },
                            //PaymentDate: { type: "date" },
                            NAV: { type: "number" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            InvestorType: { type: "number" },
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
                            SubscriptionFeePercent: { type: "number" },
                            SubscriptionFeeAmount: { type: "number" },
                            AgentPK: { type: "number" },
                            AgentID: { type: "string" },
                            AgentFeePercent: { type: "number" },
                            AgentFeeAmount: { type: "number" },
                            Posted: { type: "boolean" },
                            PostedBy: { type: "string" },
                            PostedTime: { type: "date" },
                            Revised: { type: "boolean" },
                            RevisedBy: { type: "string" },
                            RevisedTime: { type: "date" },
                            BitImmediateTransaction: { type: "boolean" },
                            FeeType: { type: "number" },
                            FeeTypeDesc: { type: "string" },
                            SumberDana: { type: "number" },
                            SumberDanaDesc: { type: "string" },
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
                            TransactionPromoPK: { type: "number" },
                            TransactionPromoID: { type: "string" },
                            IFUACode: { type: "string" },
                            FrontID: { type: "string" },
                            TrxUnitPaymentProviderPK: { type: "number" },
                            TrxUnitPaymentProviderID: { type: "string" },
                            TrxUnitPaymentProviderName: { type: "string" },
                            TrxUnitPaymentTypePK: { type: "number" },
                            TrxUnitPaymentTypeID: { type: "string" },
                            TrxUnitPaymentTypeName: { type: "string" },
                        }
                    }
                }
            });
    }

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            //grid filter
            var gridTesFilter = $("#gridClientSubscriptionApproved").data("kendoGrid");

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
            
            var gridPending = $("#gridClientSubscriptionPending").data("kendoGrid");
            //grid filter
            var gridTesFilter = $("#gridClientSubscriptionPending").data("kendoGrid");

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
            var gridHistory = $("#gridClientSubscriptionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        $("#gridClientSubscriptionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionApprovedURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(ClientSubscriptionApprovedURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientSubscriptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 250 },
                    { field: "FrontID", title: "Front ID", width: 250 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },

                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
                    { field: "TrxUnitPaymentProviderID", title: "TrxUnitPaymentProvider ID", width: 200 },
                    { field: "TrxUnitPaymentProviderName", title: "TrxUnitPaymentProvider Name", width: 200 },
                    { field: "TrxUnitPaymentTypeID", title: "TrxUnitPaymentType ID", width: 200 },
                    { field: "TrxUnitPaymentTypeName", title: "TrxUnitPaymentType Name", width: 200 },
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
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;
        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientSubscriptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 250 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;
        }
        else {
            var grid = $("#gridClientSubscriptionApproved").kendoGrid({
                dataSource: dataSourceApproved,
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
                dataBound: onDataBoundApproved,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBApproved' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBApproved'></label>",
                        template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                    },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 250 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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
            grid.table.on("click", ".checkboxApproved", selectRowApproved);
            var oldPageSizeApproved = 0;
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientSubscriptionApproved").data("kendoGrid");
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
                grid = $("#gridClientSubscriptionApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.ClientSubscriptionPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var grid = $("#gridClientSubscriptionApproved").data("kendoGrid");
            var data = grid.dataSource.data();
            $.each(data, function (i, row) {

                if (checkedApproved[row.ClientSubscriptionPK]) {
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
        $("#TabClientSubscription").kendoTabStrip({
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
        $("#BtnBatchFormBySelected").show();
        $("#BtnSInvestSubscriptionRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();


    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchFormBySelected").show();
        $("#BtnSInvestSubscriptionRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
        $("#BtnVoidBySelected").show();

    }

    function RecalGridPending() {
        $("#gridClientSubscriptionPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionPendingURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(ClientSubscriptionPendingURL);

        }

        if (_GlobClientCode == '10') {
            var grid = $("#gridClientSubscriptionPending").kendoGrid({
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "IFUACode", title: "IFUA", width: 250 },
                    { field: "FrontID", title: "Front ID", width: 250 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },

                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
                    { field: "TrxUnitPaymentProviderID", title: "TrxUnitPaymentProvider ID", width: 200 },
                    { field: "TrxUnitPaymentProviderName", title: "TrxUnitPaymentProvider Name", width: 200 },
                    { field: "TrxUnitPaymentTypeID", title: "TrxUnitPaymentType ID", width: 200 },
                    { field: "TrxUnitPaymentTypeName", title: "TrxUnitPaymentType Name", width: 200 },
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
            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;
        }
        else if (_GlobClientCode == '24') {
            var grid = $("#gridClientSubscriptionPending").kendoGrid({
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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
            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;
        }
        else {
            var grid = $("#gridClientSubscriptionPending").kendoGrid({
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [


                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        headerTemplate: "<input type='checkbox' id='chbBPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbBPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },

                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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

            grid.table.on("click", ".checkboxPending", selectRowPending);
            var oldPageSizePending = 0;
        }

        //grid filter
        if (filterlogic != undefined || filterField != undefined || filterOperator != undefined || filtervalue != undefined) {

            var gridTesFilter = $("#gridClientSubscriptionPending").data("kendoGrid");
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
                grid = $("#gridClientSubscriptionPending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.ClientSubscriptionPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].ClientSubscriptionPK]) {
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
        $("#BtnSInvestSubscriptionRptBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();




    }

    function RecalGridHistory() {

        $("#gridClientSubscriptionHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionHistoryURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(ClientSubscriptionHistoryURL);

        }


        if (_GlobClientCode == '10') {
            $("#gridClientSubscriptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "IFUACode", title: "IFUA", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FrontID", title: "Front ID", width: 250 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },

                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Total Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Total Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
                    { field: "TrxUnitPaymentProviderID", title: "TrxUnitPaymentProvider ID", width: 200 },
                    { field: "TrxUnitPaymentProviderName", title: "TrxUnitPaymentProvider Name", width: 200 },
                    { field: "TrxUnitPaymentTypeID", title: "TrxUnitPaymentType ID", width: 200 },
                    { field: "TrxUnitPaymentTypeName", title: "TrxUnitPaymentType Name", width: 200 },
                    
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
            $("#gridClientSubscriptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "CashAmount", title: "Total Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Total Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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
            $("#gridClientSubscriptionHistory").kendoGrid({
                dataSource: dataSourceHistory,
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
                    { field: "Type", title: "Type.", hidden: true, width: 95 },
                    { field: "TypeDesc", title: "Type", width: 100 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundClientName", title: "Fund Client (Name)", width: 250 },
                    { field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                    { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Total Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Total Unit Amount", width: 165, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 145, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "NAV", title: "NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AgentName", title: "Agent Name", width: 200 },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
                    { field: "CashRefID", title: "Cash Ref", width: 200 },
                    { field: "CurrencyID", title: "Currency", width: 200 },
                    { field: "Description", title: "Description", width: 350 },
                    { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
                    { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                    { field: "SumberDana", title: "SumberDana", hidden: true, width: 200 },
                    { field: "SumberDanaDesc", title: "Source of Fund", width: 200 },
                    { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
                    { field: "InvestorType", title: "InvestorType", hidden: true, width: 200, hidden: true },
                    { field: "TransactionPromoID", title: "Transaction Promo (ID)", hidden: true, width: 200 },
                    { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "BitImmediateTransaction", title: "BitImmediateTransaction", width: 200, template: "#= BitImmediateTransaction ? 'Yes' : 'No' #" },
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

            var gridTesFilter = $("#gridClientSubscriptionHistory").data("kendoGrid");
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
        $("#BtnSInvestSubscriptionRptBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnVoidBySelected").hide();

    }

    function gridHistoryDataBound() {
        var grid = $("#gridClientSubscriptionHistory").data("kendoGrid");
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
    //$("#BtnAdd").click(function () {
    //    var _reference;
    //    if (_GlobClientCode == "20") {
    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SUB/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
    //            type: 'GET',
    //            contentType: "application/json;charset=utf-8",
    //            success: function (data) {
    //                _reference = data;
    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //            }
    //        });
    //    }
    //    else {
    //        _reference = $('#ReferenceSInvest').val();
    //    }

    //    var _cashAmount;
    //    if ($("#Type").val() == 3) {
    //        _cashAmount = 0;
    //    }
    //    else {
    //        _cashAmount = $("#CashAmount").val();
    //    }

    //    var val = validateData();
    //    if (val == 1) {

    //        // bikin high risk buat aurora di awal aja
    //        // 02 AURORA
    //        if (_GlobClientCode == "02") {
    //            var HighRisk = {
    //                Date: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
    //                FundClientPK: $('#FundClientPK').val(),
    //                CashAmount: $('#CashAmount').val(),

    //            };

    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_IncomeExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                type: 'POST',
    //                data: JSON.stringify(HighRisk),
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    if (data.Result == 1) {
    //                        _reason = data.Reason;
    //                        alertify.confirm(data.Reason + " </br> Are You Sure To Continue ?", function (e) {
    //                            if (e) {
    //                                //if ($('#TotalCashAmount').val() != 0) {
    //                                //alertify.confirm("Are you sure want to Add data ?", function (e) {
    //                                //    if (e) {
    //                                var _sumberdana = "";
    //                                if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (FundClient) {
    //                                            _sumberdana = FundClient.SumberDana;
    //                                        }
    //                                    })
    //                                }
    //                                else {
    //                                    _sumberdana = $('#SumberDana').val();
    //                                }
    //                                $.ajax({
    //                                    url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                    type: 'GET',
    //                                    contentType: "application/json;charset=utf-8",
    //                                    success: function (data) {
    //                                        if (data == false) {
    //                                            $.ajax({
    //                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckMaxUnitFundandIncomePerAnnum/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundClientPK").val(),
    //                                                type: 'GET',
    //                                                contentType: "application/json;charset=utf-8",
    //                                                success: function (data) {
    //                                                    if (data.Result == 1) {
    //                                                        alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                                type: 'GET',
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (data.length != 0) {

    //                                                                        alertify.confirm(data + " Are you sure want to Add data ?", function (e) {
    //                                                                            if (e) {
    //                                                                                var ClientSubscription = {
    //                                                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                    Type: $('#Type').val(),
    //                                                                                    NAVDate: $('#NAVDate').val(),
    //                                                                                    ValueDate: $('#ValueDate').val(),
    //                                                                                    NAV: $('#NAV').val(),
    //                                                                                    FundPK: $('#FundPK').val(),
    //                                                                                    FundClientPK: $('#FundClientPK').val(),
    //                                                                                    CashRefPK: $('#CashRefPK').val(),
    //                                                                                    CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                    Description: $('#Description').val(),
    //                                                                                    ReferenceSInvest: _reference,
    //                                                                                    CashAmount: $('#CashAmount').val(),
    //                                                                                    UnitAmount: $('#UnitAmount').val(),
    //                                                                                    TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                    AgentPK: $('#AgentPK').val(),
    //                                                                                    AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                    FeeType: $('#FeeType').val(),
    //                                                                                    SumberDana: $('#SumberDanaPK').val(),
    //                                                                                    SumberDana: $('#SumberDanaPK').val(),
    //                                                                                    TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                    EntryUsersID: sessionStorage.getItem("user")

    //                                                                                };
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                    type: 'GET',
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (data != 0) {
    //                                                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                                if (e) {

    //                                                                                                    $.ajax({
    //                                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                        type: 'POST',
    //                                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                                        success: function (data) {
    //                                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                                $("#BtnAdd").hide();
    //                                                                                                                $("#BtnUpdate").show();
    //                                                                                                            }
    //                                                                                                            else {
    //                                                                                                                alertify.alert(data);
    //                                                                                                                win.close();
    //                                                                                                            }
    //                                                                                                            refresh();
    //                                                                                                        },
    //                                                                                                        error: function (data) {
    //                                                                                                            alertify.alert(data.responseText);
    //                                                                                                        }
    //                                                                                                    });
    //                                                                                                    //    },
    //                                                                                                    //    error: function (data) {
    //                                                                                                    //        $.unblockUI();
    //                                                                                                    //        alertify.alert(data.responseText);
    //                                                                                                    //    }
    //                                                                                                    //});
    //                                                                                                }
    //                                                                                            });
    //                                                                                        } else {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        })
    //                                                                    } else {
    //                                                                        var ClientSubscription = {
    //                                                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                            Type: $('#Type').val(),
    //                                                                            NAVDate: $('#NAVDate').val(),
    //                                                                            ValueDate: $('#ValueDate').val(),
    //                                                                            // PaymentDate: $('#PaymentDate').val(),
    //                                                                            NAV: $('#NAV').val(),
    //                                                                            FundPK: $('#FundPK').val(),
    //                                                                            FundClientPK: $('#FundClientPK').val(),
    //                                                                            CashRefPK: $('#CashRefPK').val(),
    //                                                                            CurrencyPK: $('#CurrencyPK').val(),
    //                                                                            Description: $('#Description').val(),
    //                                                                            ReferenceSInvest: _reference,
    //                                                                            CashAmount: $('#CashAmount').val(),
    //                                                                            UnitAmount: $('#UnitAmount').val(),
    //                                                                            TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                            TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                            SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                            SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                            AgentPK: $('#AgentPK').val(),
    //                                                                            AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                            AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                            BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                            FeeType: $('#FeeType').val(),
    //                                                                            SumberDana: $('#SumberDanaPK').val(),
    //                                                                            TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                            EntryUsersID: sessionStorage.getItem("user")

    //                                                                        };
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                            type: 'GET',
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (data != 0) {
    //                                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                        if (e) {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    });
    //                                                                                } else {
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            }
    //                                                                        });

    //                                                                    }
    //                                                                },
    //                                                                error: function (data) {
    //                                                                    alertify.alert(data.responseText);
    //                                                                }
    //                                                            });
    //                                                        });
    //                                                    }
    //                                                    else {
    //                                                        $.ajax({
    //                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                            type: 'GET',
    //                                                            contentType: "application/json;charset=utf-8",
    //                                                            success: function (data) {
    //                                                                if (data.length != 0) {
    //                                                                    alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
    //                                                                        if (e) {
    //                                                                            var ClientSubscription = {
    //                                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                Type: $('#Type').val(),
    //                                                                                NAVDate: $('#NAVDate').val(),
    //                                                                                ValueDate: $('#ValueDate').val(),
    //                                                                                // PaymentDate: $('#PaymentDate').val(),
    //                                                                                NAV: $('#NAV').val(),
    //                                                                                FundPK: $('#FundPK').val(),
    //                                                                                FundClientPK: $('#FundClientPK').val(),
    //                                                                                CashRefPK: $('#CashRefPK').val(),
    //                                                                                CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                Description: $('#Description').val(),
    //                                                                                ReferenceSInvest: _reference,
    //                                                                                CashAmount: $('#CashAmount').val(),
    //                                                                                UnitAmount: $('#UnitAmount').val(),
    //                                                                                TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                AgentPK: $('#AgentPK').val(),
    //                                                                                AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                FeeType: $('#FeeType').val(),
    //                                                                                SumberDana: $('#SumberDanaPK').val(),
    //                                                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                EntryUsersID: sessionStorage.getItem("user")

    //                                                                            };
    //                                                                            $.ajax({
    //                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                type: 'GET',
    //                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                success: function (data) {
    //                                                                                    if (data != 0) {
    //                                                                                        alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                            if (e) {
    //                                                                                                $.ajax({
    //                                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                    type: 'POST',
    //                                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                                    success: function (data) {
    //                                                                                                        if (_GlobClientCode == '21') {
    //                                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                                            $("#BtnAdd").hide();
    //                                                                                                            $("#BtnUpdate").show();
    //                                                                                                        }
    //                                                                                                        else {
    //                                                                                                            alertify.alert(data);
    //                                                                                                            win.close();
    //                                                                                                        }
    //                                                                                                        refresh();
    //                                                                                                    },
    //                                                                                                    error: function (data) {
    //                                                                                                        alertify.alert(data.responseText);
    //                                                                                                    }
    //                                                                                                });
    //                                                                                            }
    //                                                                                        });
    //                                                                                    } else {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                }
    //                                                                            });
    //                                                                        }
    //                                                                    })
    //                                                                } else {
    //                                                                    var ClientSubscription = {
    //                                                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                        Type: $('#Type').val(),
    //                                                                        NAVDate: $('#NAVDate').val(),
    //                                                                        ValueDate: $('#ValueDate').val(),
    //                                                                        // PaymentDate: $('#PaymentDate').val(),
    //                                                                        NAV: $('#NAV').val(),
    //                                                                        FundPK: $('#FundPK').val(),
    //                                                                        FundClientPK: $('#FundClientPK').val(),
    //                                                                        CashRefPK: $('#CashRefPK').val(),
    //                                                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                                                        Description: $('#Description').val(),
    //                                                                        ReferenceSInvest: _reference,
    //                                                                        CashAmount: $('#CashAmount').val(),
    //                                                                        UnitAmount: $('#UnitAmount').val(),
    //                                                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                        AgentPK: $('#AgentPK').val(),
    //                                                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                        FeeType: $('#FeeType').val(),
    //                                                                        SumberDana: $('#SumberDanaPK').val(),
    //                                                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                        EntryUsersID: sessionStorage.getItem("user")

    //                                                                    };
    //                                                                    $.ajax({
    //                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                        type: 'GET',
    //                                                                        contentType: "application/json;charset=utf-8",
    //                                                                        success: function (data) {
    //                                                                            if (data != 0) {
    //                                                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                    if (e) {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                });
    //                                                                            } else {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                    type: 'POST',
    //                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (_GlobClientCode == '21') {
    //                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                            $("#BtnAdd").hide();
    //                                                                                            $("#BtnUpdate").show();
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert(data);
    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        refresh();
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        }
    //                                                                    });

    //                                                                }
    //                                                            },
    //                                                            error: function (data) {
    //                                                                alertify.alert(data.responseText);
    //                                                            }
    //                                                        });
    //                                                    }
    //                                                }
    //                                            });
    //                                        }
    //                                        else {
    //                                            alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                        }
    //                                    },
    //                                    error: function (data) {
    //                                        alertify.alert(data.responseText);
    //                                    }
    //                                    //});
    //                                    //}
    //                                });
    //                                //}
    //                                //else {
    //                                //    alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //                                //}

    //                            }
    //                        });
    //                    }
    //                    else {
    //                        var _sumberdana = "";
    //                        if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                type: 'GET',
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (FundClient) {
    //                                    _sumberdana = FundClient.SumberDana;
    //                                }
    //                            })
    //                        }
    //                        else {
    //                            _sumberdana = $('#SumberDana').val();
    //                        }
    //                        $.ajax({
    //                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                            type: 'GET',
    //                            contentType: "application/json;charset=utf-8",
    //                            success: function (data) {
    //                                if (data == false) {
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckMaxUnitFundandIncomePerAnnum/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundClientPK").val(),
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (data) {
    //                                            if (data.Result == 1) {
    //                                                alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                    $.ajax({
    //                                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                        type: 'GET',
    //                                                        contentType: "application/json;charset=utf-8",
    //                                                        success: function (data) {
    //                                                            if (data.length != 0) {

    //                                                                alertify.confirm(data + " Are you sure want to Add data ?", function (e) {
    //                                                                    if (e) {
    //                                                                        var ClientSubscription = {
    //                                                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                            Type: $('#Type').val(),
    //                                                                            NAVDate: $('#NAVDate').val(),
    //                                                                            ValueDate: $('#ValueDate').val(),
    //                                                                            NAV: $('#NAV').val(),
    //                                                                            FundPK: $('#FundPK').val(),
    //                                                                            FundClientPK: $('#FundClientPK').val(),
    //                                                                            CashRefPK: $('#CashRefPK').val(),
    //                                                                            CurrencyPK: $('#CurrencyPK').val(),
    //                                                                            Description: $('#Description').val(),
    //                                                                            ReferenceSInvest: _reference,
    //                                                                            CashAmount: $('#CashAmount').val(),
    //                                                                            UnitAmount: $('#UnitAmount').val(),
    //                                                                            TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                            TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                            SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                            SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                            AgentPK: $('#AgentPK').val(),
    //                                                                            AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                            AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                            BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                            FeeType: $('#FeeType').val(),
    //                                                                            SumberDana: $('#SumberDanaPK').val(),
    //                                                                            TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                            EntryUsersID: sessionStorage.getItem("user")

    //                                                                        };
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                            type: 'GET',
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (data != 0) {
    //                                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                        if (e) {


    //                                                                                            //var HighRisk = {
    //                                                                                            //    Date: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
    //                                                                                            //    FundClientPK: $('#FundClientPK').val(),
    //                                                                                            //    CashAmount: $('#CashAmount').val(),

    //                                                                                            //};

    //                                                                                            ////High Risk Monitoring
    //                                                                                            //$.ajax({
    //                                                                                            //    url: window.location.origin + "/Radsoft/HighRiskMonitoring/InsertHighRiskMonitoring_IncomeExposure/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                                                            //    type: 'POST',
    //                                                                                            //    data: JSON.stringify(HighRisk),
    //                                                                                            //    contentType: "application/json;charset=utf-8",
    //                                                                                            //    success: function (data) {

    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                            //    },
    //                                                                                            //    error: function (data) {
    //                                                                                            //        $.unblockUI();
    //                                                                                            //        alertify.alert(data.responseText);
    //                                                                                            //    }
    //                                                                                            //});
    //                                                                                        }
    //                                                                                    });
    //                                                                                } else {
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            }
    //                                                                        });
    //                                                                    }
    //                                                                })
    //                                                            } else {
    //                                                                var ClientSubscription = {
    //                                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                    Type: $('#Type').val(),
    //                                                                    NAVDate: $('#NAVDate').val(),
    //                                                                    ValueDate: $('#ValueDate').val(),
    //                                                                    // PaymentDate: $('#PaymentDate').val(),
    //                                                                    NAV: $('#NAV').val(),
    //                                                                    FundPK: $('#FundPK').val(),
    //                                                                    FundClientPK: $('#FundClientPK').val(),
    //                                                                    CashRefPK: $('#CashRefPK').val(),
    //                                                                    CurrencyPK: $('#CurrencyPK').val(),
    //                                                                    Description: $('#Description').val(),
    //                                                                    ReferenceSInvest: _reference,
    //                                                                    CashAmount: $('#CashAmount').val(),
    //                                                                    UnitAmount: $('#UnitAmount').val(),
    //                                                                    TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                    AgentPK: $('#AgentPK').val(),
    //                                                                    AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                    FeeType: $('#FeeType').val(),
    //                                                                    SumberDana: $('#SumberDanaPK').val(),
    //                                                                    TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                    EntryUsersID: sessionStorage.getItem("user")

    //                                                                };
    //                                                                $.ajax({
    //                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                    type: 'GET',
    //                                                                    contentType: "application/json;charset=utf-8",
    //                                                                    success: function (data) {
    //                                                                        if (data != 0) {
    //                                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                if (e) {
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            });
    //                                                                        } else {
    //                                                                            $.ajax({
    //                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                type: 'POST',
    //                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                success: function (data) {
    //                                                                                    if (_GlobClientCode == '21') {
    //                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                        $("#BtnAdd").hide();
    //                                                                                        $("#BtnUpdate").show();
    //                                                                                    }
    //                                                                                    else {
    //                                                                                        alertify.alert(data);
    //                                                                                        win.close();
    //                                                                                    }
    //                                                                                    refresh();
    //                                                                                },
    //                                                                                error: function (data) {
    //                                                                                    alertify.alert(data.responseText);
    //                                                                                }
    //                                                                            });
    //                                                                        }
    //                                                                    }
    //                                                                });

    //                                                            }
    //                                                        },
    //                                                        error: function (data) {
    //                                                            alertify.alert(data.responseText);
    //                                                        }
    //                                                    });
    //                                                });
    //                                            }
    //                                            else {
    //                                                $.ajax({
    //                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                    type: 'GET',
    //                                                    contentType: "application/json;charset=utf-8",
    //                                                    success: function (data) {
    //                                                        if (data.length != 0) {
    //                                                            alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
    //                                                                if (e) {
    //                                                                    var ClientSubscription = {
    //                                                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                        Type: $('#Type').val(),
    //                                                                        NAVDate: $('#NAVDate').val(),
    //                                                                        ValueDate: $('#ValueDate').val(),
    //                                                                        // PaymentDate: $('#PaymentDate').val(),
    //                                                                        NAV: $('#NAV').val(),
    //                                                                        FundPK: $('#FundPK').val(),
    //                                                                        FundClientPK: $('#FundClientPK').val(),
    //                                                                        CashRefPK: $('#CashRefPK').val(),
    //                                                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                                                        Description: $('#Description').val(),
    //                                                                        ReferenceSInvest: _reference,
    //                                                                        CashAmount: $('#CashAmount').val(),
    //                                                                        UnitAmount: $('#UnitAmount').val(),
    //                                                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                        AgentPK: $('#AgentPK').val(),
    //                                                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                        FeeType: $('#FeeType').val(),
    //                                                                        SumberDana: $('#SumberDanaPK').val(),
    //                                                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                        EntryUsersID: sessionStorage.getItem("user")

    //                                                                    };
    //                                                                    $.ajax({
    //                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                        type: 'GET',
    //                                                                        contentType: "application/json;charset=utf-8",
    //                                                                        success: function (data) {
    //                                                                            if (data != 0) {
    //                                                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                    if (e) {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                });
    //                                                                            } else {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                    type: 'POST',
    //                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (_GlobClientCode == '21') {
    //                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                            $("#BtnAdd").hide();
    //                                                                                            $("#BtnUpdate").show();
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert(data);
    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        refresh();
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        }
    //                                                                    });
    //                                                                }
    //                                                            })
    //                                                        } else {
    //                                                            var ClientSubscription = {
    //                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                Type: $('#Type').val(),
    //                                                                NAVDate: $('#NAVDate').val(),
    //                                                                ValueDate: $('#ValueDate').val(),
    //                                                                // PaymentDate: $('#PaymentDate').val(),
    //                                                                NAV: $('#NAV').val(),
    //                                                                FundPK: $('#FundPK').val(),
    //                                                                FundClientPK: $('#FundClientPK').val(),
    //                                                                CashRefPK: $('#CashRefPK').val(),
    //                                                                CurrencyPK: $('#CurrencyPK').val(),
    //                                                                Description: $('#Description').val(),
    //                                                                ReferenceSInvest: _reference,
    //                                                                CashAmount: $('#CashAmount').val(),
    //                                                                UnitAmount: $('#UnitAmount').val(),
    //                                                                TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                AgentPK: $('#AgentPK').val(),
    //                                                                AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                FeeType: $('#FeeType').val(),
    //                                                                SumberDana: $('#SumberDanaPK').val(),
    //                                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                EntryUsersID: sessionStorage.getItem("user")

    //                                                            };
    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                type: 'GET',
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (data != 0) {
    //                                                                        alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                            if (e) {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                    type: 'POST',
    //                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (_GlobClientCode == '21') {
    //                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                            $("#BtnAdd").hide();
    //                                                                                            $("#BtnUpdate").show();
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert(data);
    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        refresh();
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        });
    //                                                                    } else {
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                            type: 'POST',
    //                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (_GlobClientCode == '21') {
    //                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                    $("#BtnAdd").hide();
    //                                                                                    $("#BtnUpdate").show();
    //                                                                                }
    //                                                                                else {
    //                                                                                    alertify.alert(data);
    //                                                                                    win.close();
    //                                                                                }
    //                                                                                refresh();
    //                                                                            },
    //                                                                            error: function (data) {
    //                                                                                alertify.alert(data.responseText);
    //                                                                            }
    //                                                                        });
    //                                                                    }
    //                                                                }
    //                                                            });

    //                                                        }
    //                                                    },
    //                                                    error: function (data) {
    //                                                        alertify.alert(data.responseText);
    //                                                    }
    //                                                });
    //                                            }
    //                                        }
    //                                    });
    //                                }
    //                                else {
    //                                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                }
    //                            },
    //                            error: function (data) {
    //                                alertify.alert(data.responseText);
    //                            }
    //                            //});
    //                            //}
    //                        });



    //                    }
    //                }
    //            });
    //        }
    //        else if (_GlobClientCode == "08") {
    //            if ($('#TotalCashAmount').val() != 0) {

    //                $.ajax({
    //                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateClientSubscriptionCustom08/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + $("#FundClientPK").val(),
    //                    type: 'GET',
    //                    contentType: "application/json;charset=utf-8",
    //                    success: function (data) {


    //                        if (data[0] != undefined) {
    //                            initGridValidation();
    //                        }
    //                        else {
    //                            //insert client subscription
    //                            var ClientSubscription = {
    //                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                Type: $('#Type').val(),
    //                                NAVDate: $('#NAVDate').val(),
    //                                ValueDate: $('#ValueDate').val(),
    //                                NAV: $('#NAV').val(),
    //                                FundPK: $('#FundPK').val(),
    //                                FundClientPK: $('#FundClientPK').val(),
    //                                CashRefPK: $('#CashRefPK').val(),
    //                                CurrencyPK: $('#CurrencyPK').val(),
    //                                Description: $('#Description').val(),
    //                                ReferenceSInvest: $("#ReferenceSInvest").val(),
    //                                CashAmount: $('#CashAmount').val(),
    //                                UnitAmount: $('#UnitAmount').val(),
    //                                TotalCashAmount: $('#TotalCashAmount').val(),
    //                                TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                AgentPK: $('#AgentPK').val(),
    //                                AgentFeePercent: $('#AgentFeePercent').val(),
    //                                AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                FeeType: $('#FeeType').val(),
    //                                SumberDana: $('#SumberDanaPK').val(),
    //                                TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                EntryUsersID: sessionStorage.getItem("user")
    //                            };
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                type: 'POST',
    //                                data: JSON.stringify(ClientSubscription),
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (data) {
    //                                    _ClientSubscriptionPK = data.ClientSubscriptionPK;

    //                                    if (_GlobClientCode == '21') {
    //                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                        $("#HistoryPK").val(data.HistoryPK);
    //                                        $("#StatusHeader").val("PENDING");
    //                                        alertify.alert("Insert Client Subscription Success");
    //                                        $("#BtnAdd").hide();
    //                                        $("#BtnUpdate").show();
    //                                    }
    //                                    else {
    //                                        alertify.alert(data);
    //                                        WinValidationSubs.close();
    //                                        win.close();
    //                                    }
    //                                    refresh();
    //                                },
    //                                error: function (data) {
    //                                    alertify.alert(data.responseText);
    //                                }
    //                            });
    //                        }

    //                    },
    //                    error: function (data) {
    //                        alertify.alert(data.responseText);
    //                    }
    //                });

    //            }
    //            else {
    //                alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //            }
    //        }
    //        else if (_GlobClientCode == "10") { // MANDIRI VALIDATE CUTOFF TIME
    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/ClientSUbscription/ValidationCheckCutOffTime/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    if (data == true) {
    //                        alertify.confirm("Transaction has passed the cut-off time. Are you sure want to proceed? ?", function (e) {
    //                            if (e) {
    //                                //if ($('#TotalCashAmount').val() != 0) {
    //                                //    alertify.confirm("Are you sure want to Add data ?", function (e) {
    //                                //        if (e) {
    //                                var _sumberdana = "";
    //                                if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (FundClient) {
    //                                            _sumberdana = FundClient.SumberDana;
    //                                        }
    //                                    })
    //                                }
    //                                else {
    //                                    _sumberdana = $('#SumberDana').val();
    //                                }

    //                                console.log(_sumberdana);
    //                                $.ajax({
    //                                    url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                    type: 'GET',
    //                                    contentType: "application/json;charset=utf-8",
    //                                    success: function (data) {
    //                                        if (data == false) {
    //                                            $.ajax({
    //                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckMaxUnitFundandIncomePerAnnum/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundClientPK").val(),
    //                                                type: 'GET',
    //                                                contentType: "application/json;charset=utf-8",
    //                                                success: function (data) {
    //                                                    if (data.Result == 1) {
    //                                                        alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                                type: 'GET',
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (data.length != 0) {

    //                                                                        alertify.confirm(data + " Are you sure want to Add data ?", function (e) {
    //                                                                            if (e) {
    //                                                                                var ClientSubscription = {
    //                                                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                    Type: $('#Type').val(),
    //                                                                                    NAVDate: $('#NAVDate').val(),
    //                                                                                    ValueDate: $('#ValueDate').val(),
    //                                                                                    NAV: $('#NAV').val(),
    //                                                                                    FundPK: $('#FundPK').val(),
    //                                                                                    FundClientPK: $('#FundClientPK').val(),
    //                                                                                    CashRefPK: $('#CashRefPK').val(),
    //                                                                                    CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                    Description: $('#Description').val(),
    //                                                                                    ReferenceSInvest: _reference,
    //                                                                                    CashAmount: $('#CashAmount').val(),
    //                                                                                    UnitAmount: $('#UnitAmount').val(),
    //                                                                                    TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                    AgentPK: $('#AgentPK').val(),
    //                                                                                    AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                    FeeType: $('#FeeType').val(),
    //                                                                                    SumberDana: _sumberdana,
    //                                                                                    //SumberDana: $('#SumberDanaPK').val(),
    //                                                                                    TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                    TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                                    TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                                    EntryUsersID: sessionStorage.getItem("user")

    //                                                                                };
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                    type: 'GET',
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (data != 0) {
    //                                                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                                if (e) {


    //                                                                                                    $.ajax({
    //                                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                        type: 'POST',
    //                                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                                        success: function (data) {
    //                                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                                $("#BtnAdd").hide();
    //                                                                                                                $("#BtnUpdate").show();
    //                                                                                                            }
    //                                                                                                            else {
    //                                                                                                                alertify.alert(data);
    //                                                                                                                win.close();
    //                                                                                                            }
    //                                                                                                            refresh();
    //                                                                                                        },
    //                                                                                                        error: function (data) {
    //                                                                                                            alertify.alert(data.responseText);
    //                                                                                                        }
    //                                                                                                    });
    //                                                                                                    //    },
    //                                                                                                    //    error: function (data) {
    //                                                                                                    //        $.unblockUI();
    //                                                                                                    //        alertify.alert(data.responseText);
    //                                                                                                    //    }
    //                                                                                                    //});
    //                                                                                                }
    //                                                                                            });
    //                                                                                        } else {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        })
    //                                                                    } else {
    //                                                                        var ClientSubscription = {
    //                                                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                            Type: $('#Type').val(),
    //                                                                            NAVDate: $('#NAVDate').val(),
    //                                                                            ValueDate: $('#ValueDate').val(),
    //                                                                            // PaymentDate: $('#PaymentDate').val(),
    //                                                                            NAV: $('#NAV').val(),
    //                                                                            FundPK: $('#FundPK').val(),
    //                                                                            FundClientPK: $('#FundClientPK').val(),
    //                                                                            CashRefPK: $('#CashRefPK').val(),
    //                                                                            CurrencyPK: $('#CurrencyPK').val(),
    //                                                                            Description: $('#Description').val(),
    //                                                                            ReferenceSInvest: _reference,
    //                                                                            CashAmount: $('#CashAmount').val(),
    //                                                                            UnitAmount: $('#UnitAmount').val(),
    //                                                                            TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                            TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                            SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                            SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                            AgentPK: $('#AgentPK').val(),
    //                                                                            AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                            AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                            BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                            FeeType: $('#FeeType').val(),
    //                                                                            SumberDana: _sumberdana,
    //                                                                            //SumberDana: $('#SumberDanaPK').val(),
    //                                                                            TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                            TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                            TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                            EntryUsersID: sessionStorage.getItem("user")

    //                                                                        };
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                            type: 'GET',
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (data != 0) {
    //                                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                        if (e) {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    });
    //                                                                                } else {
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            }
    //                                                                        });

    //                                                                    }
    //                                                                },
    //                                                                error: function (data) {
    //                                                                    alertify.alert(data.responseText);
    //                                                                }
    //                                                            });
    //                                                        });
    //                                                    }
    //                                                    else {
    //                                                        $.ajax({
    //                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                            type: 'GET',
    //                                                            contentType: "application/json;charset=utf-8",
    //                                                            success: function (data) {
    //                                                                if (data.length != 0) {
    //                                                                    alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
    //                                                                        if (e) {
    //                                                                            var ClientSubscription = {
    //                                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                Type: $('#Type').val(),
    //                                                                                NAVDate: $('#NAVDate').val(),
    //                                                                                ValueDate: $('#ValueDate').val(),
    //                                                                                // PaymentDate: $('#PaymentDate').val(),
    //                                                                                NAV: $('#NAV').val(),
    //                                                                                FundPK: $('#FundPK').val(),
    //                                                                                FundClientPK: $('#FundClientPK').val(),
    //                                                                                CashRefPK: $('#CashRefPK').val(),
    //                                                                                CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                Description: $('#Description').val(),
    //                                                                                ReferenceSInvest: _reference,
    //                                                                                CashAmount: $('#CashAmount').val(),
    //                                                                                UnitAmount: $('#UnitAmount').val(),
    //                                                                                TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                AgentPK: $('#AgentPK').val(),
    //                                                                                AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                FeeType: $('#FeeType').val(),
    //                                                                                SumberDana: _sumberdana,
    //                                                                                //SumberDana: $('#SumberDanaPK').val(),
    //                                                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                                TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                                EntryUsersID: sessionStorage.getItem("user")

    //                                                                            };
    //                                                                            $.ajax({
    //                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                type: 'GET',
    //                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                success: function (data) {
    //                                                                                    if (data != 0) {
    //                                                                                        alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                            if (e) {
    //                                                                                                $.ajax({
    //                                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                    type: 'POST',
    //                                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                                    success: function (data) {
    //                                                                                                        if (_GlobClientCode == '21') {
    //                                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                                            $("#BtnAdd").hide();
    //                                                                                                            $("#BtnUpdate").show();
    //                                                                                                        }
    //                                                                                                        else {
    //                                                                                                            alertify.alert(data);
    //                                                                                                            win.close();
    //                                                                                                        }
    //                                                                                                        refresh();
    //                                                                                                    },
    //                                                                                                    error: function (data) {
    //                                                                                                        alertify.alert(data.responseText);
    //                                                                                                    }
    //                                                                                                });
    //                                                                                            }
    //                                                                                        });
    //                                                                                    } else {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                }
    //                                                                            });
    //                                                                        }
    //                                                                    })
    //                                                                } else {
    //                                                                    var ClientSubscription = {
    //                                                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                        Type: $('#Type').val(),
    //                                                                        NAVDate: $('#NAVDate').val(),
    //                                                                        ValueDate: $('#ValueDate').val(),
    //                                                                        // PaymentDate: $('#PaymentDate').val(),
    //                                                                        NAV: $('#NAV').val(),
    //                                                                        FundPK: $('#FundPK').val(),
    //                                                                        FundClientPK: $('#FundClientPK').val(),
    //                                                                        CashRefPK: $('#CashRefPK').val(),
    //                                                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                                                        Description: $('#Description').val(),
    //                                                                        ReferenceSInvest: _reference,
    //                                                                        CashAmount: $('#CashAmount').val(),
    //                                                                        UnitAmount: $('#UnitAmount').val(),
    //                                                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                        AgentPK: $('#AgentPK').val(),
    //                                                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                        FeeType: $('#FeeType').val(),
    //                                                                        SumberDana: _sumberdana,
    //                                                                        //SumberDana: $('#SumberDanaPK').val(),
    //                                                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                        TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                        TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                        EntryUsersID: sessionStorage.getItem("user")

    //                                                                    };
    //                                                                    $.ajax({
    //                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                        type: 'GET',
    //                                                                        contentType: "application/json;charset=utf-8",
    //                                                                        success: function (data) {
    //                                                                            if (data != 0) {
    //                                                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                    if (e) {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                });
    //                                                                            } else {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                    type: 'POST',
    //                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (_GlobClientCode == '21') {
    //                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                            $("#BtnAdd").hide();
    //                                                                                            $("#BtnUpdate").show();
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert(data);
    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        refresh();
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        }
    //                                                                    });

    //                                                                }
    //                                                            },
    //                                                            error: function (data) {
    //                                                                alertify.alert(data.responseText);
    //                                                            }
    //                                                        });
    //                                                    }
    //                                                }
    //                                            });
    //                                        }
    //                                        else {
    //                                            alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                        }
    //                                    },
    //                                    error: function (data) {
    //                                        alertify.alert(data.responseText);
    //                                    }
    //                                });
    //                                //    }
    //                                //});
    //                                //}
    //                                //else {
    //                                //    alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //                                //}
    //                            }
    //                        });

    //                    }
    //                    else {

    //                        if (($('#TotalCashAmount').val() != 0) || ($("#Type").val() == 3)) {
    //                            alertify.confirm("Are you sure want to Add data ?", function (e) {
    //                                if (e) {
    //                                    var _sumberdana = "";
    //                                    if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                                        $.ajax({
    //                                            url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                            type: 'GET',
    //                                            contentType: "application/json;charset=utf-8",
    //                                            success: function (FundClient) {
    //                                                _sumberdana = FundClient.SumberDana;
    //                                            }
    //                                        })
    //                                    }
    //                                    else {
    //                                        _sumberdana = $('#SumberDana').val();
    //                                    }
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (data) {
    //                                            if (data == false) {
    //                                                $.ajax({
    //                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckMaxUnitFundandIncomePerAnnum/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundClientPK").val(),
    //                                                    type: 'GET',
    //                                                    contentType: "application/json;charset=utf-8",
    //                                                    success: function (data) {
    //                                                        if (data.Result == 1) {
    //                                                            alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                                $.ajax({
    //                                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                                    type: 'GET',
    //                                                                    contentType: "application/json;charset=utf-8",
    //                                                                    success: function (data) {
    //                                                                        if (data.length != 0) {

    //                                                                            alertify.confirm(data + " Are you sure want to Add data ?", function (e) {
    //                                                                                if (e) {
    //                                                                                    var ClientSubscription = {
    //                                                                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                        Type: $('#Type').val(),
    //                                                                                        NAVDate: $('#NAVDate').val(),
    //                                                                                        ValueDate: $('#ValueDate').val(),
    //                                                                                        NAV: $('#NAV').val(),
    //                                                                                        FundPK: $('#FundPK').val(),
    //                                                                                        FundClientPK: $('#FundClientPK').val(),
    //                                                                                        CashRefPK: $('#CashRefPK').val(),
    //                                                                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                        Description: $('#Description').val(),
    //                                                                                        ReferenceSInvest: _reference,
    //                                                                                        CashAmount: $('#CashAmount').val(),
    //                                                                                        UnitAmount: $('#UnitAmount').val(),
    //                                                                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                        AgentPK: $('#AgentPK').val(),
    //                                                                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                        FeeType: $('#FeeType').val(),
    //                                                                                        SumberDana: _sumberdana,
    //                                                                                        //SumberDana: $('#SumberDanaPK').val(),
    //                                                                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                        TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                                        TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                                        EntryUsersID: sessionStorage.getItem("user")

    //                                                                                    };
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                        type: 'GET',
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (data != 0) {
    //                                                                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                                    if (e) {


    //                                                                                                        $.ajax({
    //                                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                            type: 'POST',
    //                                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                                            success: function (data) {
    //                                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                                    $("#BtnAdd").hide();
    //                                                                                                                    $("#BtnUpdate").show();
    //                                                                                                                }
    //                                                                                                                else {
    //                                                                                                                    alertify.alert(data);
    //                                                                                                                    win.close();
    //                                                                                                                }
    //                                                                                                                refresh();
    //                                                                                                            },
    //                                                                                                            error: function (data) {
    //                                                                                                                alertify.alert(data.responseText);
    //                                                                                                            }
    //                                                                                                        });
    //                                                                                                        //    },
    //                                                                                                        //    error: function (data) {
    //                                                                                                        //        $.unblockUI();
    //                                                                                                        //        alertify.alert(data.responseText);
    //                                                                                                        //    }
    //                                                                                                        //});
    //                                                                                                    }
    //                                                                                                });
    //                                                                                            } else {
    //                                                                                                $.ajax({
    //                                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                    type: 'POST',
    //                                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                                    success: function (data) {
    //                                                                                                        if (_GlobClientCode == '21') {
    //                                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                                            $("#BtnAdd").hide();
    //                                                                                                            $("#BtnUpdate").show();
    //                                                                                                        }
    //                                                                                                        else {
    //                                                                                                            alertify.alert(data);
    //                                                                                                            win.close();
    //                                                                                                        }
    //                                                                                                        refresh();
    //                                                                                                    },
    //                                                                                                    error: function (data) {
    //                                                                                                        alertify.alert(data.responseText);
    //                                                                                                    }
    //                                                                                                });
    //                                                                                            }
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            })
    //                                                                        } else {
    //                                                                            var ClientSubscription = {
    //                                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                Type: $('#Type').val(),
    //                                                                                NAVDate: $('#NAVDate').val(),
    //                                                                                ValueDate: $('#ValueDate').val(),
    //                                                                                // PaymentDate: $('#PaymentDate').val(),
    //                                                                                NAV: $('#NAV').val(),
    //                                                                                FundPK: $('#FundPK').val(),
    //                                                                                FundClientPK: $('#FundClientPK').val(),
    //                                                                                CashRefPK: $('#CashRefPK').val(),
    //                                                                                CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                Description: $('#Description').val(),
    //                                                                                ReferenceSInvest: _reference,
    //                                                                                CashAmount: $('#CashAmount').val(),
    //                                                                                UnitAmount: $('#UnitAmount').val(),
    //                                                                                TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                AgentPK: $('#AgentPK').val(),
    //                                                                                AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                FeeType: $('#FeeType').val(),
    //                                                                                SumberDana: _sumberdana,
    //                                                                                //SumberDana: $('#SumberDanaPK').val(),
    //                                                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                                TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                                EntryUsersID: sessionStorage.getItem("user")

    //                                                                            };
    //                                                                            $.ajax({
    //                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                type: 'GET',
    //                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                success: function (data) {
    //                                                                                    if (data != 0) {
    //                                                                                        alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                            if (e) {
    //                                                                                                $.ajax({
    //                                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                    type: 'POST',
    //                                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                                    success: function (data) {
    //                                                                                                        if (_GlobClientCode == '21') {
    //                                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                                            $("#BtnAdd").hide();
    //                                                                                                            $("#BtnUpdate").show();
    //                                                                                                        }
    //                                                                                                        else {
    //                                                                                                            alertify.alert(data);
    //                                                                                                            win.close();
    //                                                                                                        }
    //                                                                                                        refresh();
    //                                                                                                    },
    //                                                                                                    error: function (data) {
    //                                                                                                        alertify.alert(data.responseText);
    //                                                                                                    }
    //                                                                                                });
    //                                                                                            }
    //                                                                                        });
    //                                                                                    } else {
    //                                                                                        $.ajax({
    //                                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                            type: 'POST',
    //                                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                                            contentType: "application/json;charset=utf-8",
    //                                                                                            success: function (data) {
    //                                                                                                if (_GlobClientCode == '21') {
    //                                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                                    $("#BtnAdd").hide();
    //                                                                                                    $("#BtnUpdate").show();
    //                                                                                                }
    //                                                                                                else {
    //                                                                                                    alertify.alert(data);
    //                                                                                                    win.close();
    //                                                                                                }
    //                                                                                                refresh();
    //                                                                                            },
    //                                                                                            error: function (data) {
    //                                                                                                alertify.alert(data.responseText);
    //                                                                                            }
    //                                                                                        });
    //                                                                                    }
    //                                                                                }
    //                                                                            });

    //                                                                        }
    //                                                                    },
    //                                                                    error: function (data) {
    //                                                                        alertify.alert(data.responseText);
    //                                                                    }
    //                                                                });
    //                                                            });
    //                                                        }
    //                                                        else {
    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
    //                                                                type: 'GET',
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (data.length != 0) {
    //                                                                        alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
    //                                                                            if (e) {
    //                                                                                var ClientSubscription = {
    //                                                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                                    Type: $('#Type').val(),
    //                                                                                    NAVDate: $('#NAVDate').val(),
    //                                                                                    ValueDate: $('#ValueDate').val(),
    //                                                                                    // PaymentDate: $('#PaymentDate').val(),
    //                                                                                    NAV: $('#NAV').val(),
    //                                                                                    FundPK: $('#FundPK').val(),
    //                                                                                    FundClientPK: $('#FundClientPK').val(),
    //                                                                                    CashRefPK: $('#CashRefPK').val(),
    //                                                                                    CurrencyPK: $('#CurrencyPK').val(),
    //                                                                                    Description: $('#Description').val(),
    //                                                                                    ReferenceSInvest: _reference,
    //                                                                                    CashAmount: $('#CashAmount').val(),
    //                                                                                    UnitAmount: $('#UnitAmount').val(),
    //                                                                                    TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                                    AgentPK: $('#AgentPK').val(),
    //                                                                                    AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                                    FeeType: $('#FeeType').val(),
    //                                                                                    SumberDana: _sumberdana,
    //                                                                                    //SumberDana: $('#SumberDanaPK').val(),
    //                                                                                    TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                                    TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                                    TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                                    EntryUsersID: sessionStorage.getItem("user")

    //                                                                                };
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                                    type: 'GET',
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (data != 0) {
    //                                                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                                if (e) {
    //                                                                                                    $.ajax({
    //                                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                        type: 'POST',
    //                                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                                        success: function (data) {
    //                                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                                $("#BtnAdd").hide();
    //                                                                                                                $("#BtnUpdate").show();
    //                                                                                                            }
    //                                                                                                            else {
    //                                                                                                                alertify.alert(data);
    //                                                                                                                win.close();
    //                                                                                                            }
    //                                                                                                            refresh();
    //                                                                                                        },
    //                                                                                                        error: function (data) {
    //                                                                                                            alertify.alert(data.responseText);
    //                                                                                                        }
    //                                                                                                    });
    //                                                                                                }
    //                                                                                            });
    //                                                                                        } else {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                        })
    //                                                                    } else {
    //                                                                        var ClientSubscription = {
    //                                                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                                                            Type: $('#Type').val(),
    //                                                                            NAVDate: $('#NAVDate').val(),
    //                                                                            ValueDate: $('#ValueDate').val(),
    //                                                                            // PaymentDate: $('#PaymentDate').val(),
    //                                                                            NAV: $('#NAV').val(),
    //                                                                            FundPK: $('#FundPK').val(),
    //                                                                            FundClientPK: $('#FundClientPK').val(),
    //                                                                            CashRefPK: $('#CashRefPK').val(),
    //                                                                            CurrencyPK: $('#CurrencyPK').val(),
    //                                                                            Description: $('#Description').val(),
    //                                                                            ReferenceSInvest: _reference,
    //                                                                            CashAmount: $('#CashAmount').val(),
    //                                                                            UnitAmount: $('#UnitAmount').val(),
    //                                                                            TotalCashAmount: $('#TotalCashAmount').val(),
    //                                                                            TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                                                            SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                                                            SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                                                            AgentPK: $('#AgentPK').val(),
    //                                                                            AgentFeePercent: $('#AgentFeePercent').val(),
    //                                                                            AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                                                            BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                                                            FeeType: $('#FeeType').val(),
    //                                                                            SumberDana: _sumberdana,
    //                                                                            //SumberDana: $('#SumberDanaPK').val(),
    //                                                                            TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                                                            TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
    //                                                                            TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
    //                                                                            EntryUsersID: sessionStorage.getItem("user")

    //                                                                        };
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                                                            type: 'GET',
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (data != 0) {
    //                                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                                                        if (e) {
    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                    });
    //                                                                                } else {
    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });
    //                                                                                }
    //                                                                            }
    //                                                                        });

    //                                                                    }
    //                                                                },
    //                                                                error: function (data) {
    //                                                                    alertify.alert(data.responseText);
    //                                                                }
    //                                                            });
    //                                                        }
    //                                                    }
    //                                                });
    //                                            }
    //                                            else {
    //                                                alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                            }
    //                                        },
    //                                        error: function (data) {
    //                                            alertify.alert(data.responseText);
    //                                        }
    //                                    });
    //                                }
    //                            });
    //                        }
    //                        else {
    //                            alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //                        }

    //                    }
    //                },
    //                error: function (data) {
    //                    alertify.alert(data.responseText);
    //                }
    //            });
    //        }
    //        else if (_GlobClientCode == "20") {
    //            if (($('#TotalCashAmount').val() != 0) || ($("#Type").val() == 3)) {
    //                alertify.confirm("Are you sure want to Add data ?", function (e) {
    //                    if (e) {
    //                        var _sumberdana = "";
    //                        if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                type: 'GET',
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (FundClient) {
    //                                    _sumberdana = FundClient.SumberDana;
    //                                }
    //                            })
    //                        }
    //                        else {
    //                            _sumberdana = $('#SumberDana').val();
    //                        }
    //                        $.ajax({
    //                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                            type: 'GET',
    //                            contentType: "application/json;charset=utf-8",
    //                            success: function (data) {
    //                                if (data == false) {
    //                                    var ClientSubscription = {
    //                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                        Type: $('#Type').val(),
    //                                        NAVDate: $('#NAVDate').val(),
    //                                        ValueDate: $('#ValueDate').val(),
    //                                        NAV: $('#NAV').val(),
    //                                        FundPK: $('#FundPK').val(),
    //                                        FundClientPK: $('#FundClientPK').val(),
    //                                        CashRefPK: $('#CashRefPK').val(),
    //                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                        Description: $('#Description').val(),
    //                                        ReferenceSInvest: _reference,
    //                                        CashAmount: $('#CashAmount').val(),
    //                                        UnitAmount: $('#UnitAmount').val(),
    //                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                        AgentPK: $('#AgentPK').val(),
    //                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                        FeeType: $('#FeeType').val(),
    //                                        SumberDana: $('#SumberDanaPK').val(),
    //                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                        OtherAmount: $('#OtherAmount').val(),
    //                                        EntryUsersID: sessionStorage.getItem("user")

    //                                    };
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (data) {
    //                                            if (data != 0) {
    //                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                    if (e) {
    //                                                        $.ajax({
    //                                                            url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                            type: 'GET',
    //                                                            contentType: "application/json;charset=utf-8",
    //                                                            success: function (data) {
    //                                                                if (data == false) {

    //                                                                    $.ajax({
    //                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                        type: 'POST',
    //                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                        contentType: "application/json;charset=utf-8",
    //                                                                        success: function (data) {
    //                                                                            if (_GlobClientCode == '21') {
    //                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                $("#BtnAdd").hide();
    //                                                                                $("#BtnUpdate").show();
    //                                                                            }
    //                                                                            else {
    //                                                                                alertify.alert(data);
    //                                                                                win.close();
    //                                                                            }
    //                                                                            refresh();
    //                                                                        },
    //                                                                        error: function (data) {
    //                                                                            alertify.alert(data.responseText);
    //                                                                        }
    //                                                                    });
    //                                                                }
    //                                                                else {
    //                                                                    alertify.alert("Transaction has passed the cut-off time !");
    //                                                                }
    //                                                            },
    //                                                            error: function (data) {
    //                                                                alertify.alert(data.responseText);
    //                                                            }
    //                                                        });


    //                                                    }
    //                                                });
    //                                            } else { // tidak kena validasi check add subs
    //                                                $.ajax({
    //                                                    url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                    type: 'GET',
    //                                                    contentType: "application/json;charset=utf-8",
    //                                                    success: function (data) {
    //                                                        if (data == false) {

    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                type: 'POST',
    //                                                                data: JSON.stringify(ClientSubscription),
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (_GlobClientCode == '21') {
    //                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                        $("#StatusHeader").val("PENDING");
    //                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                        $("#BtnAdd").hide();
    //                                                                        $("#BtnUpdate").show();
    //                                                                    }
    //                                                                    else {
    //                                                                        alertify.alert(data);
    //                                                                        win.close();
    //                                                                    }
    //                                                                    refresh();
    //                                                                },
    //                                                                error: function (data) {
    //                                                                    alertify.alert(data.responseText);
    //                                                                }
    //                                                            });

    //                                                        }
    //                                                        else {
    //                                                            alertify.alert("Transaction has passed the cut-off time !");
    //                                                        }
    //                                                    },
    //                                                    error: function (data) {
    //                                                        alertify.alert(data.responseText);
    //                                                    }
    //                                                });
    //                                            }
    //                                        }
    //                                    });


    //                                }
    //                                else {
    //                                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                }
    //                            },
    //                            error: function (data) {
    //                                alertify.alert(data.responseText);
    //                            }
    //                        });
    //                    }
    //                });
    //            }
    //            else {
    //                alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //            }
    //        }

    //        else //STANDAR
    //        {
    //            if (($('#TotalCashAmount').val() != 0) || ($("#Type").val() == 3)) {
    //                alertify.confirm("Are you sure want to Add data ?", function (e) {
    //                    if (e) {
    //                        var _sumberdana = "";
    //                        if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                                type: 'GET',
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (FundClient) {
    //                                    _sumberdana = FundClient.SumberDana;
    //                                }
    //                            })
    //                        }
    //                        else {
    //                            _sumberdana = $('#SumberDana').val();
    //                        }
    //                        $.ajax({
    //                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
    //                            type: 'GET',
    //                            contentType: "application/json;charset=utf-8",
    //                            success: function (data) {
    //                                if (data == false) {
    //                                    var ClientSubscription = {
    //                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
    //                                        Type: $('#Type').val(),
    //                                        NAVDate: $('#NAVDate').val(),
    //                                        ValueDate: $('#ValueDate').val(),
    //                                        NAV: $('#NAV').val(),
    //                                        FundPK: $('#FundPK').val(),
    //                                        FundClientPK: $('#FundClientPK').val(),
    //                                        CashRefPK: $('#CashRefPK').val(),
    //                                        CurrencyPK: $('#CurrencyPK').val(),
    //                                        Description: $('#Description').val(),
    //                                        ReferenceSInvest: _reference,
    //                                        CashAmount: $('#CashAmount').val(),
    //                                        UnitAmount: $('#UnitAmount').val(),
    //                                        TotalCashAmount: $('#TotalCashAmount').val(),
    //                                        TotalUnitAmount: $('#TotalUnitAmount').val(),
    //                                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
    //                                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
    //                                        AgentPK: $('#AgentPK').val(),
    //                                        AgentFeePercent: $('#AgentFeePercent').val(),
    //                                        AgentFeeAmount: $('#AgentFeeAmount').val(),
    //                                        BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
    //                                        FeeType: $('#FeeType').val(),
    //                                        SumberDana: $('#SumberDanaPK').val(),
    //                                        TransactionPromoPK: $('#TransactionPromoPK').val(),
    //                                        OtherAmount: $('#OtherAmount').val(),
    //                                        EntryUsersID: sessionStorage.getItem("user")

    //                                    };
    //                                    $.ajax({
    //                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + _cashAmount,
    //                                        type: 'GET',
    //                                        contentType: "application/json;charset=utf-8",
    //                                        success: function (data) {
    //                                            if (data != 0) {
    //                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
    //                                                    if (e) {
    //                                                        $.ajax({
    //                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/HighRiskMonitoring_CheckHighRiskMonitoringFromSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                            type: 'POST',
    //                                                            data: JSON.stringify(ClientSubscription),
    //                                                            contentType: "application/json;charset=utf-8",
    //                                                            success: function (data) {
    //                                                                if (data.Result == 1) {
    //                                                                    alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/HighRiskMonitoring/HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                                            type: 'POST',
    //                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                                                    type: 'GET',
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (data == false) {

    //                                                                                            $.ajax({
    //                                                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                                type: 'POST',
    //                                                                                                data: JSON.stringify(ClientSubscription),
    //                                                                                                contentType: "application/json;charset=utf-8",
    //                                                                                                success: function (data) {
    //                                                                                                    if (_GlobClientCode == '21') {
    //                                                                                                        $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                        $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                        $("#StatusHeader").val("PENDING");
    //                                                                                                        alertify.alert("Insert Client Subscription Success");
    //                                                                                                        $("#BtnAdd").hide();
    //                                                                                                        $("#BtnUpdate").show();
    //                                                                                                    }
    //                                                                                                    else if (_GlobClientCode == '32') {
    //                                                                                                        alertify.alert("Sending instruction to hold the money");
    //                                                                                                        //proses api send nanti disini

    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    else {
    //                                                                                                        alertify.alert(data);
    //                                                                                                        win.close();
    //                                                                                                    }
    //                                                                                                    refresh();
    //                                                                                                },
    //                                                                                                error: function (data) {
    //                                                                                                    alertify.alert(data.responseText);
    //                                                                                                }
    //                                                                                            });
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert("Transaction has passed the cut-off time !");
    //                                                                                        }
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            },
    //                                                                            error: function (data) {
    //                                                                                alertify.alert(data.responseText);
    //                                                                            }
    //                                                                        });
    //                                                                    });
    //                                                                }
    //                                                                else {

    //                                                                    $.ajax({
    //                                                                        url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                                        type: 'GET',
    //                                                                        contentType: "application/json;charset=utf-8",
    //                                                                        success: function (data) {
    //                                                                            if (data == false) {
    //                                                                                $.ajax({
    //                                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                    type: 'POST',
    //                                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                                    contentType: "application/json;charset=utf-8",
    //                                                                                    success: function (data) {
    //                                                                                        if (_GlobClientCode == '21') {
    //                                                                                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                            $("#HistoryPK").val(data.HistoryPK);
    //                                                                                            $("#StatusHeader").val("PENDING");
    //                                                                                            alertify.alert("Insert Client Subscription Success");
    //                                                                                            $("#BtnAdd").hide();
    //                                                                                            $("#BtnUpdate").show();
    //                                                                                        }
    //                                                                                        else if (_GlobClientCode == '32') {
    //                                                                                            alertify.alert("Sending instruction to hold the money");
    //                                                                                            //proses api send nanti disini

    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        else {
    //                                                                                            alertify.alert(data);
    //                                                                                            win.close();
    //                                                                                        }
    //                                                                                        refresh();
    //                                                                                    },
    //                                                                                    error: function (data) {
    //                                                                                        alertify.alert(data.responseText);
    //                                                                                    }
    //                                                                                });
    //                                                                            }
    //                                                                            else {
    //                                                                                alertify.alert("Transaction has passed the cut-off time !");
    //                                                                            }
    //                                                                        },
    //                                                                        error: function (data) {
    //                                                                            alertify.alert(data.responseText);
    //                                                                        }
    //                                                                    });
    //                                                                }
    //                                                            },
    //                                                            error: function (data) {
    //                                                                alertify.alert(data.responseText);
    //                                                            }
    //                                                        });
    //                                                    }
    //                                                });
    //                                            } else { // tidak kena validasi check add subs
    //                                                $.ajax({
    //                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/HighRiskMonitoring_CheckHighRiskMonitoringFromSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                    type: 'POST',
    //                                                    data: JSON.stringify(ClientSubscription),
    //                                                    contentType: "application/json;charset=utf-8",
    //                                                    success: function (data) {
    //                                                        if (data.Result == 1) {
    //                                                            alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
    //                                                                $.ajax({
    //                                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                                    type: 'POST',
    //                                                                    data: JSON.stringify(ClientSubscription),
    //                                                                    contentType: "application/json;charset=utf-8",
    //                                                                    success: function (data) {
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                                            type: 'GET',
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (data == false) {

    //                                                                                    $.ajax({
    //                                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                                        type: 'POST',
    //                                                                                        data: JSON.stringify(ClientSubscription),
    //                                                                                        contentType: "application/json;charset=utf-8",
    //                                                                                        success: function (data) {
    //                                                                                            if (_GlobClientCode == '21') {
    //                                                                                                $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                                $("#HistoryPK").val(data.HistoryPK);
    //                                                                                                $("#StatusHeader").val("PENDING");
    //                                                                                                alertify.alert("Insert Client Subscription Success");
    //                                                                                                $("#BtnAdd").hide();
    //                                                                                                $("#BtnUpdate").show();
    //                                                                                            }
    //                                                                                            else if (_GlobClientCode == '32') {
    //                                                                                                alertify.alert("Sending instruction to hold the money");
    //                                                                                                //proses api send nanti disini

    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            else {
    //                                                                                                alertify.alert(data);
    //                                                                                                win.close();
    //                                                                                            }
    //                                                                                            refresh();
    //                                                                                        },
    //                                                                                        error: function (data) {
    //                                                                                            alertify.alert(data.responseText);
    //                                                                                        }
    //                                                                                    });

    //                                                                                }
    //                                                                                else {
    //                                                                                    alertify.alert("Transaction has passed the cut-off time !");
    //                                                                                }
    //                                                                            },
    //                                                                            error: function (data) {
    //                                                                                alertify.alert(data.responseText);
    //                                                                            }
    //                                                                        });
    //                                                                    },
    //                                                                    error: function (data) {
    //                                                                        alertify.alert(data.responseText);
    //                                                                    }
    //                                                                });
    //                                                            });
    //                                                        }
    //                                                        else {
    //                                                            $.ajax({
    //                                                                url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
    //                                                                type: 'GET',
    //                                                                contentType: "application/json;charset=utf-8",
    //                                                                success: function (data) {
    //                                                                    if (data == false) {
    //                                                                        $.ajax({
    //                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
    //                                                                            type: 'POST',
    //                                                                            data: JSON.stringify(ClientSubscription),
    //                                                                            contentType: "application/json;charset=utf-8",
    //                                                                            success: function (data) {
    //                                                                                if (_GlobClientCode == '21') {
    //                                                                                    $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
    //                                                                                    $("#HistoryPK").val(data.HistoryPK);
    //                                                                                    $("#StatusHeader").val("PENDING");
    //                                                                                    alertify.alert("Insert Client Subscription Success");
    //                                                                                    $("#BtnAdd").hide();
    //                                                                                    $("#BtnUpdate").show();
    //                                                                                }
    //                                                                                else if (_GlobClientCode == '32') {
    //                                                                                    alertify.alert("Sending instruction to hold the money");
    //                                                                                    //proses api send nanti disini

    //                                                                                    win.close();
    //                                                                                }
    //                                                                                else {
    //                                                                                    alertify.alert(data);
    //                                                                                    win.close();
    //                                                                                }
    //                                                                                refresh();
    //                                                                            },
    //                                                                            error: function (data) {
    //                                                                                alertify.alert(data.responseText);
    //                                                                            }
    //                                                                        });

    //                                                                    }
    //                                                                    else {
    //                                                                        alertify.alert("Transaction has passed the cut-off time !");
    //                                                                    }
    //                                                                },
    //                                                                error: function (data) {
    //                                                                    alertify.alert(data.responseText);
    //                                                                }
    //                                                            });
    //                                                        }
    //                                                    },
    //                                                    error: function (data) {
    //                                                        alertify.alert(data.responseText);
    //                                                    }
    //                                                });
    //                                            }
    //                                        }
    //                                    });
    //                                }
    //                                else {
    //                                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
    //                                }
    //                            },
    //                            error: function (data) {
    //                                alertify.alert(data.responseText);
    //                            }
    //                        });
    //                    }
    //                });
    //            }
    //            else {
    //                alertify.alert("Please Recalculate / Check Total Cash Amount First !")
    //            }
    //        }
    //    }
    //});

    $("#BtnUpdate").click(function () {
        var val2 = 0;
        if (_GlobClientCode == "24" && $("#Type").val() == 3) {
            $("#CashAmount").attr("required", false);
            val2 = 1;
        }
        else if ($("#Type").val() == 3) {
            $("#CashAmount").attr("required", false);
            val2 = 1;
        }
        else {
            $("#CashAmount").attr("required", true);
            if ($('#TotalCashAmount').val() != 0)
                val2 = 1;
        }
        var val = validateData();
        console.log(val2);
        if (val == 1) {
            if (val2 == 1) {
                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ClientSubscription/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#ClientSubscriptionPK').val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == false) {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                                            _trxPK = '';
                                                            if ($('#TransactionPK').val() != null) {
                                                                _trxPK = $('#TransactionPK').val();
                                                            }
                                                            var ClientSubscription = {
                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                                Type: $('#Type').val(),
                                                                HistoryPK: $('#HistoryPK').val(),
                                                                NAVDate: $('#NAVDate').val(),
                                                                ValueDate: $('#ValueDate').val(),
                                                                //PaymentDate: $('#PaymentDate').val(),
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
                                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                                AgentPK: $('#AgentPK').val(),
                                                                AgentFeePercent: $('#AgentFeePercent').val(),
                                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                                FeeType: $('#FeeType').val(),
                                                                TransactionPK: _trxPK,
                                                                SumberDana: $('#SumberDana').val(),
                                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
                                                                TrxUnitPaymentProviderPK: $('#TrxUnitPaymentProviderPK').val(),
                                                                TrxUnitPaymentTypePK: $('#TrxUnitPaymentTypePK').val(),
                                                                OtherAmount: $('#OtherAmount').val(),
                                                                Notes: str,
                                                                EntryUsersID: sessionStorage.getItem("user")
                                                            };
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_U",
                                                                type: 'POST',
                                                                data: JSON.stringify(ClientSubscription),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    alertify.alert(data);
                                                                    win.close();
                                                                    refresh();
                                                                    //if (_GlobClientCode == '21') {
                                                                    //    RecalAgentSubscription($('#ClientSubscriptionPK').val());
                                                                    //}
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
                                else {
                                    //alertify.alert("Doi Bukan Anak Sini, Jangan Edit Disini Cuy"); // ni kerjaan siapa yaa ???
                                    alertify.alert("Update Cancel, This Data has already with another system !");
                                }
                            }
                        })
                    }
                });
            }
            else {
                alertify.alert("Please Recalculate / Check Total Cash Amount First !")
            }
        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ClientSubscription" + "/" + $("#ClientSubscriptionPK").val(),
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
                    url: window.location.origin + "/Radsoft/ClientSubscription/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#ClientSubscriptionPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                             $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ClientSubscription = {
                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_A",
                                type: 'POST',
                                data: JSON.stringify(ClientSubscription),
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
                })
               
            }
        });
    });

    $("#BtnApproveBySelected").click(function (e) {

        var _statusHighRiskNikko;
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }


        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {

                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateCheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            if (_GlobClientCode == "08") {
                                var ClientSubscription = {
                                    UnitRegistrySelected: stringClientSubscriptionSelected,
                                };
                                $.ajax({

                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckStatusHighRiskMonitoring/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSubscription/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                data: JSON.stringify(ClientSubscription),
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
                                            alertify.alert("Client is on High Risk Monitoring, please approve High Risk Monitoring First!");
                                        }
                                    }
                                });
                            }
                            else if (_GlobClientCode == "21") {
                                var ClientSubscription = {
                                    UnitRegistrySelected: stringClientSubscriptionSelected,
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateCheckClientAPERD/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        console.log(data);
                                        if (data == "") {
                                            var ClientSubscription = {
                                                UnitRegistrySelected: stringClientSubscriptionSelected,
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSubscription/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                data: JSON.stringify(ClientSubscription),
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
                            else if (_GlobClientCode == "20") {

                                var ClientSubscription = {
                                    DateFrom: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    DateTo: kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    UnitRegistrySelected: stringClientSubscriptionSelected,
                                    UnitRegistryType: "ClientSubscription",
                                    ApprovedUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidasiHighRiskMonitoringForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        _statusHighRiskNikko = data

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoffBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'POST',
                                            data: JSON.stringify(ClientSubscription),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == "") {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                        type: 'POST',
                                                        data: JSON.stringify(ClientSubscription),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {

                                                            alertify.alert(data + "<br/><br/>" + _statusHighRiskNikko);
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
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });



                            }
                            else {

                                var ClientSubscription = {
                                    DateFrom: kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    DateTo: kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    UnitRegistrySelected: stringClientSubscriptionSelected,
                                    UnitRegistryType: "ClientSubscription",
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoffBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == "") {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSubscription/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                data: JSON.stringify(ClientSubscription),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    var _alertForClient32 = '';

                                                    if (_GlobClientCode == '32') {
                                                        _alertForClient32 = "</br></br>Sending instruction to release the money";
                                                        //proses api send nanti disini

                                                    }

                                                    alertify.alert(data + _alertForClient32);
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

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#ClientSubscriptionPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                        var ClientSubscription = {
                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            VoidUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_V",
                                            type: 'POST',
                                            data: JSON.stringify(ClientSubscription),
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
                })
                
            }
        });
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#ClientSubscriptionPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                        var ClientSubscription = {
                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            VoidUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_R",
                                            type: 'POST',
                                            data: JSON.stringify(ClientSubscription),
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
                })
               
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

    function getDataSourceListFundClient() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/FundClient/GetFundClientComboForTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                 FundClientPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" },
                                 IFUA: { type: "string" }
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
        var dsListFundClient = getDataSourceListFundClient();
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
               { field: "FundClientPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "Client ID", width: 300 },
               { field: "IFUA", title: "IFUA", width: 300 },

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
                                    url: window.location.origin + "/Radsoft/FundClient/CheckClientCantSubs/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {

                                            if (_GlobClientCode == "20") {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/ValidationHighRiskNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.FundClientPK + "/" + 1,
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if (data == 1)
                                                            alertify.alert("Fund Client is on High Risk Monitoring(KYC,PPATK)!");
                                                        else if (data == 2)
                                                            alertify.alert("IFUA/SID is empty, please check data Fund Client!");
                                                        else {
                                                            //clearData();
                                                            $("#FundClientName").val(dataItemX.Name);
                                                            $("#FundClientID").val(dataItemX.ID);
                                                            $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                            $("#CashRefPK").data("kendoComboBox").value("");
                                                            $("#AgentPK").data("kendoComboBox").value("");
                                                            getFundClientCashRefByFundClientPK($("#FundPK").val(), dataItemX.FundClientPK);
                                                            getAgentByFundClientPK();
                                                            //mekel
                                                            //$("#FundClientCashRefPK").data("kendoComboBox").value("");
                                                            //getFundClientCashRefByFundClientPK();
                                                            WinListFundClient.close();
                                                            if (_GlobClientCode != "10") {
                                                                GetSourceofFund(dataItemX.FundClientPK);
                                                            }
                                                        }
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                            else {
                                                //clearData();
                                                $("#FundClientName").val(dataItemX.Name);
                                                $("#FundClientID").val(dataItemX.ID);
                                                $(htmlFundClientPK).val(dataItemX.FundClientPK);
                                                $("#CashRefPK").data("kendoComboBox").value("");
                                                $("#AgentPK").data("kendoComboBox").value("");
                                                getFundClientCashRefByFundClientPK($("#FundPK").val(), dataItemX.FundClientPK);
                                                getAgentByFundClientPK();
                                                //mekel
                                                //$("#FundClientCashRefPK").data("kendoComboBox").value("");
                                                //getFundClientCashRefByFundClientPK();
                                                WinListFundClient.close();
                                                if (_GlobClientCode != "10") {
                                                    GetSourceofFund(dataItemX.FundClientPK);
                                                }

                                            }




                                        } else {
                                            alertify.alert("Fund Client Can't Subs!");
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

    function GetSourceofFund(_pk) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetInvestorTypeSourceofFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _pk,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (FundClient) {
                var _SourceofFund = "";
                if (FundClient.InvestorType == 1)
                {
                    _SourceofFund = "IncomeSourceIND";
                }
                else if (FundClient.InvestorType == 2)
                {
                    _SourceofFund = "IncomeSourceINS"
                }
                else
                {
                    _SourceofFund = "IncomeSourceIND"
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Mastervalue/GetMastervalueComboSourceofFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _SourceofFund + "/" + FundClient.SumberDana,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (MasterValue) {
                        //$("#SumberDanaPK").data("kendoComboBox").value(MasterValue.Code)
                        $('#SumberDanaPK').val(MasterValue.Code)
                        $("#SumberDana").data("kendoComboBox").value(MasterValue.DescOne)
                    }

                });
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
        
        alertify.confirm("Are you sure want to Posting Client Subscription?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alertify.alert("Client Subscription Already Posted / Revised, Posting Cancel");
            } else {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var ClientSubscription = {
                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PostedBy: sessionStorage.getItem("user"),
                                    FundPK: $('#FundPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    TotalCashAmount: $('#TotalCashAmount').val(),
                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                                    ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_Posting",
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
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

    $("#BtnRevise").click(function (e) {
        if (e != undefined) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;
        }

        alertify.confirm("Are you sure want to Revised Client Subscription?", function (e) {
            if ($("#State").text() == "Revised") {
                alert("Client Subscription Already Posted / Revised, Revised Cancel");
            } else {
                if (e) {
                    $.blockUI();
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
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
                                var ClientSubscription = {
                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    RevisedBy: sessionStorage.getItem("user"),
                                    FundPK: $('#FundPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    TotalCashAmount: $('#TotalCashAmount').val(),
                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                                    ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_Revise",
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
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
                                        $.unblockUI();
                                        $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                                        $("#State").text("Revised");
                                        $("#RevisedBy").text(sessionStorage.getItem("user"));
                                        $("#Revised").prop('checked', true);
                                        $("#Posted").prop('checked', false);
                                        alertify.alert(data);
                                        refresh();

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClientPosition/UpdateAvgAfterRevise/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/1/" + $('#ClientSubscriptionPK').val() + "/" + $('#HistoryPK').val(),
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
        ClientSubscriptionRecalculate();
    });

    function ClientSubscriptionRecalculate() {
        if ($("#NAVDate").val() == null || $("#NAVDate").val() == "") {
            alertify.alert("Please Fill NAV Date First");
            return;
        }
        else if ($("#FundPK").val() == null || $("#FundPK").val() == "") {
            alertify.alert("Please Fill Fund First");
            return;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _feepercent = 0;
                _feepercent = $('#SubscriptionFeePercent').val();
                if ((parseFloat(data.SubscriptionFeePercent - _feepercent)) >= 0) {
                    var ParamClientSubscriptionRecalculate = {
                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                        FundPK: $('#FundPK').val(),
                        NavDate: kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        CashAmount: $('#CashAmount').val(),
                        FeeType: $('#FeeType').val(),
                        SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                        AgentFeePercent: $('#AgentFeePercent').val(),
                        SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                        AgentFeeAmount: $('#AgentFeeAmount').val(),
                        UpdateUsersID: sessionStorage.getItem("user"),
                        LastUpdate: kendo.toString($("#LastUpdate").data("kendoDatePicker").value(), "MM-dd-yy HH:mm:ss")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(ParamClientSubscriptionRecalculate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAV").data("kendoNumericTextBox").value(data.Nav);
                            $("#UnitAmount").data("kendoNumericTextBox").value(data.UnitAmount);
                            $("#SubscriptionFeePercent").data("kendoNumericTextBox").value(data.SubsFeePercent);
                            $("#AgentFeePercent").data("kendoNumericTextBox").value(data.AgentFeePercent);
                            $("#SubscriptionFeeAmount").data("kendoNumericTextBox").value(data.SubsFeeAmount);
                            $("#AgentFeeAmount").data("kendoNumericTextBox").value(data.AgentFeeAmount);
                            $("#TotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmount);
                            $("#TotalUnitAmount").data("kendoNumericTextBox").value(data.TotalUnitAmount);

                            if (_GlobClientCode == '21') {
                                $("#TrxCashAmount").data("kendoNumericTextBox").value($('#CashAmount').val());
                                $("#TrxTotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmount);
                                $("#TrxSubscriptionFeePercent").data("kendoNumericTextBox").value(data.SubsFeePercent);
                                $("#TrxSubscriptionFeeAmount").data("kendoNumericTextBox").value(data.SubsFeeAmount);

                                $("#TrxFeeCashAmount").data("kendoNumericTextBox").value($('#CashAmount').val());
                                $("#TrxFeeTotalCashAmount").data("kendoNumericTextBox").value(data.TotalCashAmount);
                                $("#TrxFeeSubscriptionFeePercent").data("kendoNumericTextBox").value(data.SubsFeePercent);
                                $("#TrxFeeSubscriptionFeeAmount").data("kendoNumericTextBox").value(data.SubsFeeAmount);

                            }
                           // refresh();
                            //alertify.alert("Recalculate Done");
                            //$.ajax({
                            //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").data("kendoComboBox").value() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
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
                    alertify.alert("Subs Fee Percent > Max Subs Fee Percent in this Fund");
                    $.unblockUI();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

    function showBatch(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to open Batch Form ?", function (a) {
                if (a) {
                    // initGrid()
                    if (tabindex == 0 || tabindex == undefined) {
                        var grid = $("#gridClientSubscriptionApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientSubscriptionPending").data("kendoGrid");
                    }
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    var _clientSubscriptionPK = _dataItemX.ClientSubscriptionPK;
                    var _status = _dataItemX.Status;
                    var _fund = _dataItemX.FundPK;
                    var _navDate = _dataItemX.NAVDate;
                    var Batch = {
                        ClientSubscriptionPK: _clientSubscriptionPK,
                        Status: _status,
                        FundPK: _fund,
                        NAVDate: kendo.toString(kendo.parseDate(_navDate), 'MM/dd/yy'),
                        PostedBy: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscription_BatchForm/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    $("#BtnRejectBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }


        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {
                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateCheckDescription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            var ClientSubscription = {
                                UnitRegistrySelected: stringClientSubscriptionSelected,
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSubscription),
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
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                if (kendo.toString($("#DateFrom").data("kendoDatePicker").value(), 'MM-dd-yy') == kendo.toString($("#DateTo").data("kendoDatePicker").value(), 'MM-dd-yy')) {
                    $.blockUI();
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CloseNav/ValidateCheckCloseNavApproveForUnitRegistry/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                        type: 'POST',
                        data: JSON.stringify(ClientSubscription),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidatePostingNAVBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'POST',
                                    data: JSON.stringify(ClientSubscription),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSubscription/ValidatePostingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == 1) {
                                                        alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Subscription Already Posting");
                                                        $.unblockUI();

                                                    }
                                                    else if (data == 2) {
                                                        alertify.alert("Posting Cancel, Date is Holiday !");
                                                        $.unblockUI();

                                                    } else {
                                                        if (_GlobClientCode == "03") {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ValidateEDTUnitBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data == 1) {
                                                                        alertify.alert("Posting Cancel, Must be Generate End Day Trails Today First / Client Redemption Already Posting");
                                                                        $.unblockUI();

                                                                    }
                                                                    else {
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                            type: 'POST',
                                                                            data: JSON.stringify(ClientSubscription),
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                $.unblockUI();
                                                                                alertify.alert(data);
                                                                                refresh();

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
                                                                url: window.location.origin + "/Radsoft/ClientSubscription/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                                type: 'POST',
                                                                data: JSON.stringify(ClientSubscription),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    $.unblockUI();
                                                                    alertify.alert(data);
                                                                    refresh();

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
                                        $.unblockUI();
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
                    alertify.alert("Please Posting Client Subscription by Day");
                }
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
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {

                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateVoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSubscription),
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
                            alertify.alert("Client Subscription Already Posted");
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

    $("#BtnGetNavBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }

        alertify.confirm("Are you sure want to Get Nav by Selected Data ?", function (e) {
            if (e) {
                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/GetNavBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/UnitRegistry_GetNavBySelected",
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
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

    $("#BtnBatchFormBySelected").click(function (e) {

        var stringClientSubscriptionSelected = '';
        var AllPending = 0;
        AllPending = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                AllPending.push(i);
            }
        }

        var ArrayPendingFundFrom = AllPending;
        var stringClientSubscriptionSelectedPending = '';

        for (var i in ArrayPendingFundFrom) {
            stringClientSubscriptionSelectedPending = stringClientSubscriptionSelectedPending + ArrayPendingFundFrom[i] + ',';

        }
        stringClientSubscriptionSelectedPending = stringClientSubscriptionSelectedPending.substring(0, stringClientSubscriptionSelectedPending.length - 1)


        var AllApproved = 0;
        AllApproved = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                AllApproved.push(i);
            }
        }

        var ArrayApprovedFundFrom = AllApproved;
        var stringClientSubscriptionSelectedApproved = '';

        for (var i in ArrayApprovedFundFrom) {
            stringClientSubscriptionSelectedApproved = stringClientSubscriptionSelectedApproved + ArrayApprovedFundFrom[i] + ',';

        }
        stringClientSubscriptionSelectedApproved = stringClientSubscriptionSelectedApproved.substring(0, stringClientSubscriptionSelectedApproved.length - 1)


        if (tabindex == 0 || tabindex == undefined) {
            if (stringClientSubscriptionSelectedApproved == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientSubscriptionSelected = stringClientSubscriptionSelectedApproved;

        }
        else if (tabindex = 1) {
            if (stringClientSubscriptionSelectedPending == "") {
                alertify.alert("There's No Selected Data")
                return;
            }
            stringClientSubscriptionSelected = stringClientSubscriptionSelectedPending;

        }



        alertify.confirm("Are you sure want to Batch Form by Selected Data ?", function (e) {
            if (e) {
                var _url = "";
                var Batch = {
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    DownloadMode: $('#DownloadMode').val(),
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                if (_GlobClientCode == "10") {
                    _url = window.location.origin + "/Radsoft/ClientSubscription/BatchFormBySelectedDataMandiri/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                }
                else if (_GlobClientCode == "11") {
                    _url = window.location.origin + "/Radsoft/ClientSubscription/BatchFormBySelectedDataTaspen/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
                }
                else {
                    _url = window.location.origin + "/Radsoft/ClientSubscription/BatchFormBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy")
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

    function showBatch(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to open Batch Form ?", function (a) {
                if (a) {
                    // initGrid()
                    if (tabindex == 0 || tabindex == undefined) {
                        var grid = $("#gridClientSubscriptionApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientSubscriptionPending").data("kendoGrid");
                    }
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    var _clientSubscriptionPK = _dataItemX.ClientSubscriptionPK;
                    var _status = _dataItemX.Status;
                    var _fund = _dataItemX.FundPK;
                    var _navDate = _dataItemX.NAVDate;
                    var Batch = {
                        ClientSubscriptionPK: _clientSubscriptionPK,
                        Status: _status,
                        FundPK: _fund,
                        NAVDate: kendo.toString(kendo.parseDate(_navDate), 'MM/dd/yy'),
                        PostedBy: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscription_BatchForm/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

    $("#BtnRegulerInstructionBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Batch Form by Selected Data ?", function (e) {
            if (e) {
                var Batch = {
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/BatchFormBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    $("#BtnSInvestSubscriptionRptBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)
        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/SInvestSubscriptionRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadRadsoftFile").attr("href", data);
                        $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_TrxSubscription.txt");
                        document.getElementById("downloadRadsoftFile").click();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnRegularForm").click(function (e) {
        
        alertify.confirm("Are you sure want to Regular Form ?", function (e) {
            if (e) {
                var Batch = {
                    Signature1Desc: $("#Signature1").data("kendoComboBox").text(),
                    Signature2Desc: $("#Signature2").data("kendoComboBox").text(),
                    Signature3Desc: $("#Signature3").data("kendoComboBox").text(),
                    Signature4Desc: $("#Signature4").data("kendoComboBox").text(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/RegularForm/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    function getFundClientCashRefByFundClientPK(_fundPK, _fundClientPK) {
        //check data fundclietcashref
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClientCashRef/CheckDataFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 1) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefByFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK + "/SUB",
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
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
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

    $("#BtnOpenImportTransactionPromo").click(function () {
        WinGenerateTransactionPromo.center();
        WinGenerateTransactionPromo.open();
    });

    $("#BtnImportGoodFund").click(function () {
        document.getElementById("FileImportTransactionPromo").click();
    });

    $("#FileImportTransactionPromo").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportTransactionPromo").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("TransactionPromo", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionPromo_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportTransactionPromo").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportTransactionPromo").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportTransactionPromo").val("");
        }


    });

    $("#BtnImportPromoMoInvest").click(function () {
        document.getElementById("FileImportPromoMoInvest").click();
    });

    $("#FileImportPromoMoInvest").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportPromoMoInvest").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PromoMoInvest", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionPromo_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportPromoMoInvest").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportPromoMoInvest").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportPromoMoInvest").val("");
        }


    });

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
                             AgentSubscriptionPK: { type: "number", editable: false },
                             ClientSubscriptionPK: { type: "number", editable: false },
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

    function initGridAgentTrx(_clientSubscriptionPK) {
        $("#gridAgentTrx").empty();

        if (_clientSubscriptionPK == 0 || _clientSubscriptionPK == null) {
            _clientSubs = 0;
        }
        else {
            _clientSubs = _clientSubscriptionPK;
        }

        var ApprovedURL = window.location.origin + "/Radsoft/AgentSubscription/GetDataAgentSubscriptionByClientSubscriptionPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubs,
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
                     field: "AgentSubscriptionPK", title: "AgentSubscriptionPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "ClientSubscriptionPK", title: "ClientSubscriptionPK", headerAttributes: {
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
        if ($("#StatusHeader").val() == "NEW")
        {
            alertify.alert("Please Add Client Subscription First !");
            return;
        }
        else
        {
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
                    AgentSubscriptionPK: 0,
                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                    AgentPK: $('#ParamTrxAgent').val(),
                    AgentTrxPercent: $('#ParamTrxAgentPercent').val(),
                    NetAmount: $('#TrxTotalCashAmount').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentSubscription/ValidateMaxPercentAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AddAgentTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentSubscription/AddAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AddAgentTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Insert Agent Subscription Success");
                                    InsertPercentAgentSubscriptionHO($('#ClientSubscriptionPK').val());
                                    initGridAgentTrx($('#ClientSubscriptionPK').val());
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
                AgentSubscriptionPK: dataItemX.AgentSubscriptionPK,
                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                AgentTrxPercent: dataItemX.AgentTrxPercent,
                NetAmount: $('#TrxTotalCashAmount').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/AgentSubscription/ValidateMaxPercentAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(AgentTrx),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {

                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentSubscription/UpdateAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AgentTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    InsertPercentAgentSubscriptionHO($('#ClientSubscriptionPK').val());
                                    initGridAgentTrx($("#ClientSubscriptionPK").val());

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
                AgentSubscriptionPK: dataItemX.AgentSubscriptionPK,
                VoidUsersID: sessionStorage.getItem("user")
            };

            alertify.confirm("Are you sure want to Void Data ?", function (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentSubscription/VoidAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AgentTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Void Success");
                        InsertPercentAgentSubscriptionHO($('#ClientSubscriptionPK').val());
                        initGridAgentTrx($("#ClientSubscriptionPK").val());
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
                             AgentFeeSubscriptionPK: { type: "number", editable: false },
                             ClientSubscriptionPK: { type: "number", editable: false },
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

    function initGridAgentFeeTrx(_clientSubscriptionPK) {
        $("#gridAgentFeeTrx").empty();

        if (_clientSubscriptionPK == 0 || _clientSubscriptionPK == null) {
            _clientSubs = 0;
        }
        else {
            _clientSubs = _clientSubscriptionPK;
        }

        var ApprovedURL = window.location.origin + "/Radsoft/AgentFeeSubscription/GetDataAgentFeeSubscriptionByClientSubscriptionPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubs,
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
                     field: "AgentFeeSubscriptionPK", title: "AgentFeeSubscriptionPK", headerAttributes: {
                         style: "text-align: center"
                     }, width: 50, hidden: true
                 },
                 {
                     field: "ClientSubscriptionPK", title: "ClientSubscriptionPK", headerAttributes: {
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
        if ($("#StatusHeader").val() != "NEW" && $("#SubscriptionFeePercent").val() != 0) {
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
            alertify.alert("Please Add Client Subscription First or Check Subscription Fee Percent !");
            return;
        }

    });

    $("#BtnOkAddAgentFeeTrx").click(function () {


        alertify.confirm("Are you sure want to Add this data ?", function (e) {
            if (e) {
                var AddAgentFeeTrx = {
                    AgentFeeSubscriptionPK: 0,
                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                    AgentPK: $('#ParamTrxAgentFee').val(),
                    AgentFeeTrxPercent: $('#ParamTrxAgentFeePercent').val(),
                    NetAmount: $('#TrxFeeSubscriptionFeeAmount').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentFeeSubscription/ValidateMaxPercentAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AddAgentFeeTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSubscription/AddAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AddAgentFeeTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Insert Agent Fee Subscription Success");
                                    InsertPercentAgentFeeSubscriptionHO($('#ClientSubscriptionPK').val());
                                    initGridAgentFeeTrx($('#ClientSubscriptionPK').val());
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
                AgentFeeSubscriptionPK: dataItemX.AgentFeeSubscriptionPK,
                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                AgentFeeTrxPercent: dataItemX.AgentFeeTrxPercent,
                NetAmount: $('#TrxFeeSubscriptionFeeAmount').val(),
                UpdateUsersID: sessionStorage.getItem("user")
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/AgentFeeSubscription/ValidateMaxPercentAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(AgentFeeTrx),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {

                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSubscription/UpdateAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AgentFeeTrx),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    InsertPercentAgentFeeSubscriptionHO($('#ClientSubscriptionPK').val());
                                    initGridAgentFeeTrx($("#ClientSubscriptionPK").val());

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
                AgentFeeSubscriptionPK: dataItemX.AgentFeeSubscriptionPK,
                VoidUsersID: sessionStorage.getItem("user")
            };

            alertify.confirm("Are you sure want to Void Data ?", function (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/AgentFeeSubscription/VoidAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AgentFeeTrx),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Void Success");
                        InsertPercentAgentFeeSubscriptionHO($('#ClientSubscriptionPK').val());
                        initGridAgentFeeTrx($("#ClientSubscriptionPK").val());
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            });
        }


    }


    //function RecalAgentSubscription(_clientSubscriptionPK) {
    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/AgentSubscription/AgentSubscriptionRecalculate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubscriptionPK,
    //            type: 'GET',
    //            contentType: "application/json;charset=utf-8",
    //            success: function (data) {
   
    //                initGridAgentTrx(_clientSubscriptionPK);
    //                initGridAgentFeeTrx(_clientSubscriptionPK);
    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //            }
    //        });


    //}




    function InsertPercentAgentSubscriptionHO(_clientSubscriptionPK) {
        var AddAgentTrx = {
            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
            NetAmount: $('#TrxTotalCashAmount').val(),
            EntryUsersID: sessionStorage.getItem("user"),
            UpdateUsersID: sessionStorage.getItem("user")
        };
            $.ajax({
                url: window.location.origin + "/Radsoft/AgentSubscription/InsertPercentAgentSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubscriptionPK,
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

    function InsertPercentAgentFeeSubscriptionHO(_clientSubscriptionPK) {
        var AddAgentFeeTrx = {
            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
            NetAmount: $('#TrxFeeSubscriptionFeeAmount').val(),
            EntryUsersID: sessionStorage.getItem("user"),
            UpdateUsersID: sessionStorage.getItem("user")
        };
        $.ajax({
            url: window.location.origin + "/Radsoft/AgentFeeSubscription/InsertPercentAgentFeeSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _clientSubscriptionPK,
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

    $("#BtnUploadSubcription").click(function () {
        document.getElementById("UploadSubcription").click();
    });

    $("#UploadSubcription").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#UploadSubcription").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("UploadSubs", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#UploadSubcription").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#UploadSubcription").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#UploadSubcription").val("");
        }
    });
   
    function CheckFundETF(_fundPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/CheckFundETF/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    $("#lblOtherAmount").show();
                    $("#NumOtherAmount").show();
                }
                else {
                    $("#lblOtherAmount").hide();
                    $("#NumOtherAmount").hide();

                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
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
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        alertify.confirm("Are you sure want to Download Cash Instruction ?", function (e) {
            if (e) {
                var ClientSubscription = {
                    ClientSubscriptionSelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/CashInstructionBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
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

    // validation add subscription

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
        $("#gridValidationClientSubscription").empty();

        var ClientSubscriptionPendingURL = window.location.origin + "/Radsoft/ClientSubscription/ValidateClientSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + $("#FundClientPK").val(),
            dataSourceApproved = getDataSourceValidation(ClientSubscriptionPendingURL);

        var grid = $("#gridValidationClientSubscription").kendoGrid({
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


        WinValidationSubs.center();
        WinValidationSubs.open();
    }

    $("#BtnContinueValidation").click(function () {

        var _FundClientPK = 0;
        var _ValueDate = '';

        _ValueDate = kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
        _FundClientPK = $('#FundClientPK').val();


        alertify.confirm("Are you sure want to Add data ?", function (e) {
            if (e) {

                var _Validation = [];
                var flagChange = 0;
                var gridDataArray = $('#gridValidationClientSubscription').data('kendoGrid')._data;
                for (var index = 0; index < gridDataArray.length; index++) {
                    console.log(gridDataArray[index]["Reason"]);

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
                        async: false,
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

                //insert client subscription
                var ClientSubscription = {
                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                    Type: $('#Type').val(),
                    NAVDate: $('#NAVDate').val(),
                    ValueDate: $('#ValueDate').val(),
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
                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                    AgentPK: $('#AgentPK').val(),
                    AgentFeePercent: $('#AgentFeePercent').val(),
                    AgentFeeAmount: $('#AgentFeeAmount').val(),
                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                    FeeType: $('#FeeType').val(),
                    SumberDana: $('#SumberDanaPK').val(),
                    TransactionPromoPK: $('#TransactionPromoPK').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        _ClientSubscriptionPK = data.ClientSubscriptionPK;

                        if (_GlobClientCode == '21') {
                            $("#ClientSubscriptionPK").val(data.ClientSubscriptionPK);
                            $("#HistoryPK").val(data.HistoryPK);
                            $("#StatusHeader").val("PENDING");
                            alertify.alert("Insert Client Subscription Success");
                            $("#BtnAdd").hide();
                            $("#BtnUpdate").show();
                        }
                        else {
                            alertify.alert(data);
                            WinValidationSubs.close();
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

        WinValidationSubs.close();
    });


    $("#BtnAdd").click(function () {
        var val = validateData();
        //validation yg gk bsa lanjut, harus dikelarin dlu

        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
            type: 'GET',
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


        if (!(_GlobClientCode == "02" || _GlobClientCode == "10")) {
            if (($('#TotalCashAmount').val() == 0) && ($("#Type").val() != 3)) {
                alertify.alert("Please Recalculate / Check Total Cash Amount First !");
                return;
            }
        }


        //if (!(_GlobClientCode == "02" || _GlobClientCode == "08" || _GlobClientCode == "10")) { // validasi ini hanya untuk Standar dan Custom 20
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/CheckEntryApproveTimeCutoff/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    alertify.alert("Transaction has passed the cut-off time !");
                    return;
                }
                else {
                    if (val == 1) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ClientSubscription/ValidateClientSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + $("#FundClientPK").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                var _sumberdana = "";
                                if (_GlobClientCode == "10") {  // untuk Custom10 pakai GetSumberDana klo SumberDana Kosong
                                    if ($('#SumberDana').val() == 0 || $('#SumberDana').val() == "" || $('#SumberDana').val() == null) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundClient/GetSumberDana/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (FundClient) {
                                                _sumberdana = FundClient.SumberDana;
                                            }
                                        })
                                    }
                                    else {
                                        _sumberdana = $('#SumberDana').val();
                                    }
                                }
                                else { // Selain Custom 10, pakai SumberDanaPK
                                    _sumberdana = $('#SumberDanaPK').val();
                                }



                                if (data[0] != undefined) {
                                    initGridValidation();
                                }
                                else {
                                    alertify.confirm("Are you sure to add data ? ", function (e) {
                                        if (e) {
                                            GetSinvestReference();
                                            var ClientSubscription = {
                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                Type: $('#Type').val(),
                                                NAVDate: $('#NAVDate').val(),
                                                ValueDate: $('#ValueDate').val(),
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
                                                SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                AgentPK: $('#AgentPK').val(),
                                                AgentFeePercent: $('#AgentFeePercent').val(),
                                                AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                FeeType: $('#FeeType').val(),
                                                SumberDana: _sumberdana,
                                                TransactionPromoPK: $('#TransactionPromoPK').val(),
                                                OtherAmount: $('#OtherAmount').val(),
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscription_I",
                                                type: 'POST',
                                                data: JSON.stringify(ClientSubscription),
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

                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        //}

    });


    function GetSinvestReference() {
        if (_GlobClientCode == "20") {
            $.ajax({
                url: window.location.origin + "/Radsoft/CashRef/GetSinvestReferenceForNikko/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SUB/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    $("#BtnUnApproveBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringClientSubscriptionSelected = '';

        for (var i in ArrayFundFrom) {
            stringClientSubscriptionSelected = stringClientSubscriptionSelected + ArrayFundFrom[i] + ',';

        }
        stringClientSubscriptionSelected = stringClientSubscriptionSelected.substring(0, stringClientSubscriptionSelected.length - 1)

        if (stringClientSubscriptionSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }
        alertify.confirm("Are you sure want to unapproved by Selected Data ?", function (e) {
            if (e) {

                var ClientSubscription = {
                    UnitRegistrySelected: stringClientSubscriptionSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateUnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(ClientSubscription),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(ClientSubscription),
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
                            alertify.alert("Client Subscription Already Posted");
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



});
