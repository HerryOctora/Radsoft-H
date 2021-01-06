$(document).ready(function () {
    document.title = 'FORM CLIENT SUBSCRIPTION';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;
    var dirty;
    var upOradd;
    var _d = new Date();
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
        $("#BtnGetNavBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnBatchFormBySelected").kendoButton({
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
                    ClientSubscriptionWithInterest();
                } else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 0,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#NAVDate").data("kendoDatePicker").value(new Date(data));
                            ClientSubscriptionWithInterest();
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
                            ClientSubscriptionWithInterest();
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


        win = $("#WinClientSubscriptionWithInterest").kendoWindow({
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
            width: 750,
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

    }

    var GlobValidator = $("#WinClientSubscriptionWithInterest").kendoValidator().data("kendoValidator");

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
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridClientSubscriptionWithInterestApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridClientSubscriptionWithInterestHistory").data("kendoGrid");
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
                $("#BtnUpdate").show();
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
                $("#BtnUpdate").show();
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
            $("#BitImmediateTransaction").prop('checked', dataItemX.BitImmediateTransaction);

            //$("#FundClientCashRefPK").val(dataItemX.FundClientCashRefPK);
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
                    enable: false,
                    index: 0,
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
                } else {
                    ClientSubscriptionWithInterest();
                }
            }

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
            format: "n4",
            decimals: 4,
            change: onchangeCashAmount,
            value: setCashAmount(),
        });
        $("#CashAmount").data("kendoNumericTextBox").enable(false);




        function onchangeCashAmount() {
            ClientSubscriptionWithInterest();
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
            format: "n8",
            decimals: 8,
            // format: "#.00000000",
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

        $("#SubscriptionFeePercent").data("kendoNumericTextBox").enable(false);
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
                ClientSubscriptionWithInterest();
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

        $("#AgentFeePercent").data("kendoNumericTextBox").enable(false);
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
                ClientSubscriptionWithInterest();
            }

        }
        function onChangeSubscriptionFeePercent() {
            $("#SubscriptionFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientSubscriptionWithInterest();
            }
        }
        function onChangeAgentFeePercent() {
            if ($("#AgentPK").data("kendoComboBox").value() == null || $("#AgentPK").data("kendoComboBox").value() == "") {
                alertify.alert("Cannot Change Agent Fee Percent If Agent is empty");
                $("#AgentFeePercent").data("kendoNumericTextBox").value(0);
            }
            $("#AgentFeeAmount").data("kendoNumericTextBox").value(0);
            if ($("#FeeType").data("kendoComboBox").value() == 1) {
                ClientSubscriptionWithInterest();
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
                ClientSubscriptionWithInterest();
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

        $("#Tenor").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "3", value: 1 },
                { text: "6", value: 2 },
                { text: "12", value: 3 },

            ],
            filter: "contains",
            change: OnChangeTenor,
            value: setCmbTenor()
        });


        function OnChangeTenor() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        function setCmbTenor() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Tenor == 0) {
                    return "";
                } else {
                    return dataItemX.Tenor;
                }
            }
        }


        $("#InterestRate").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setInterestRate()

        });
        function setInterestRate() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.InterestRate;
            }
        }


        $("#PaymentTerm").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "3", value: 1 },
                { text: "6", value: 2 },
                { text: "12", value: 3 },

            ],
            filter: "contains",
            change: OnChangePaymentTerm,
            value: setCmbPaymentTerm()
        });


        function OnChangePaymentTerm() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        function setCmbPaymentTerm() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PaymentTerm == 0) {
                    return "";
                } else {
                    return dataItemX.PaymentTerm;
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


        $("#NAV").data("kendoNumericTextBox").value("");
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
        $("#Tenor").val("");
        $("#InterestRate").val("");
        $("#PaymentTerm").val("");
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
                             Tenor: { type: "number" },
                             TenorDesc: { type: "string" },
                             InterestRate: { type: "number" },
                             PaymentTerm: { type: "number" },
                             PaymentTermDesc: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Revised: { type: "boolean" },
                             RevisedBy: { type: "string" },
                             RevisedTime: { type: "date" },
                             BitImmediateTransaction: { type: "boolean" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
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

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()
        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridClientSubscriptionWithInterestHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridClientSubscriptionWithInterestApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            } else if (row.Reversed == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            }
            else if (row.TypeDesc == "Regular") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowRegular");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function gridPendingOnDataBound() {
        var grid = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.TypeDesc == "ORDINARY") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            }
            else if (row.TypeDesc == "Regular") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowRegular");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function initGrid() {

        $("#gridClientSubscriptionWithInterestApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionWithInterestApprovedURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(ClientSubscriptionWithInterestApprovedURL);

        }

        var grid = $("#gridClientSubscriptionWithInterestApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TRX Marketing"
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
               //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
               { field: "Type", title: "Type.", hidden: true, width: 95 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
               { field: "NAVDate", title: "NAV Date", width: 150, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
               { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               //{ field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
               { field: "FundID", title: "Fund ID", width: 150 },
               { field: "TotalCashAmount", title: "Total Cash Amount", width: 250, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "TotalUnitAmount", title: "Total Unit Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n10}", attributes: { style: "text-align:right;" } },
               { field: "AgentName", title: "Agent Name", width: 200 },
               { field: "FundPK", title: "Fund", hidden: true, width: 200 },
               { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
               { field: "CashRefID", title: "Cash Ref", width: 200 },
               { field: "CurrencyID", title: "Currency", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
               { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
               { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
               { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "NAV", title: "NAV", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "TenorDesc", title: "Tenor", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "InterestRate", title: "Interest Rate (%)", template: "#: InterestRate  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PaymentTermDesc", title: "Payment Term", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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


            var grid = $("#gridClientSubscriptionWithInterestApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _clientSubscriptionPK = dataItemX.ClientSubscriptionPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _clientSubscriptionPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabClientSubscriptionWithInterest").kendoTabStrip({
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
        $("#BtnGetNavBySelected").hide();
        $("#BtnUnApproveBySelected").show();
        $("#BtnSInvestSubscriptionRptBySelected").show();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchFormBySelected").show();
        $("#BtnSInvestSubscriptionRptBySelected").show();
        $("#BtnUnApproveBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClientSubscription/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClientSubscription/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridClientSubscriptionWithInterestPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionWithInterestPendingURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(ClientSubscriptionWithInterestPendingURL);

        }
        var grid = $("#gridClientSubscriptionWithInterestPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TRX Marketing"
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
            dataBound: gridPendingOnDataBound,
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
               { field: "ClientSubscriptionPK", title: "SysNo.", width: 95 },
               { field: "Type", title: "Type.", hidden: true, width: 95 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
               { field: "NAVDate", title: "NAV Date", width: 150, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
               { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              // { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
               { field: "FundID", title: "Fund ID", width: 150 },
               { field: "TotalCashAmount", title: "Total Cash Amount", width: 250, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "TotalUnitAmount", title: "Total Unit Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "AgentName", title: "Agent Name", width: 200 },
               { field: "FundPK", title: "Fund", hidden: true, width: 200 },
               { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
               { field: "CashRefID", title: "Cash Ref", width: 200 },
               { field: "CurrencyID", title: "Currency", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
               { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
               { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
               { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "NAV", title: "NAV", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "TenorDesc", title: "Tenor", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "InterestRate", title: "Interest Rate (%)", template: "#: InterestRate  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PaymentTermDesc", title: "Payment Term", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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


            var grid = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _clientSubscriptionPK = dataItemX.ClientSubscriptionPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _clientSubscriptionPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnGetNavBySelected").show();
        $("#BtnBatchFormBySelected").show();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnSInvestSubscriptionRptBySelected").show();

    }

    function RecalGridHistory() {

        $("#gridClientSubscriptionWithInterestHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClientSubscriptionWithInterestHistoryURL = window.location.origin + "/Radsoft/ClientSubscription/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(ClientSubscriptionWithInterestHistoryURL);

        }
        $("#gridClientSubscriptionWithInterestHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TRX Marketing"
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
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
               { field: "NAVDate", title: "NAV Date", width: 150, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
               { field: "ValueDate", title: "Settlement Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              // { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
               { field: "FundID", title: "Fund ID", width: 150 },
               { field: "TotalCashAmount", title: "Total Cash Amount", width: 250, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "TotalUnitAmount", title: "Total Unit Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeePercent", title: "Subs.Fee (%)", template: "#: SubscriptionFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "SubscriptionFeeAmount", title: "Subs.Fee Amount", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "CashAmount", title: "Cash Amount", width: 250, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "UnitAmount", title: "Unit Amount", width: 200, hidden: true, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n8')#</div>", format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "AgentName", title: "Agent Name", width: 200 },
               { field: "FundPK", title: "Fund", hidden: true, width: 200 },
               { field: "FundClientID", title: "Fund Client (ID)", hidden: true, width: 200 },
               { field: "CashRefID", title: "Cash Ref", width: 200 },
               { field: "CurrencyID", title: "Currency", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "FeeType", title: "FeeType", hidden: true, width: 200 },
               { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
               { field: "AgentID", title: "Agent ID", hidden: true, width: 200 },
               { field: "AgentFeePercent", title: "Agent Fee (%)", template: "#: AgentFeePercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "AgentFeeAmount", title: "Agent Fee Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "NAV", title: "NAV", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "TenorDesc", title: "Tenor", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "InterestRate", title: "Interest Rate (%)", template: "#: InterestRate  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "PaymentTermDesc", title: "Payment Term", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnGetNavBySelected").hide();
        $("#BtnBatchFormBySelected").hide();
        $("#BtnUnApproveBySelected").hide();
        $("#BtnSInvestSubscriptionRptBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridClientSubscriptionWithInterestHistory").data("kendoGrid");
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
        var val = validateData();
        if (val == 1) {
            if ($('#TotalCashAmount').val() != 0) {
                alertify.confirm("Are you sure want to Add data ?", function (e) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckMaxUnitFundandIncomePerAnnum/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#CashAmount").val() + "/" + $("#FundPK").val() + "/" + kendo.toString($("#NAVDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundClientPK").val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data.Result == 1) {
                                                alertify.confirm(data.Reason + " Are you sure want to Add data ?", function (e) {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data.length != 0) {
                                                                alertify.confirm(data + " Are you sure want to Add data ?", function (e) {
                                                                    if (e) {
                                                                        var ClientSubscription = {
                                                                            ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                                            Type: 1,
                                                                            NAVDate: $('#NAVDate').val(),
                                                                            ValueDate: $('#ValueDate').val(),
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
                                                                            Tenor: $('#Tenor').val(),
                                                                            InterestRate: $('#InterestRate').val(),
                                                                            PaymentTerm: $('#PaymentTerm').val(),
                                                                            EntryUsersID: sessionStorage.getItem("user")

                                                                        };
                                                                        $.ajax({
                                                                            url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                                            type: 'GET',
                                                                            contentType: "application/json;charset=utf-8",
                                                                            success: function (data) {
                                                                                if (data != 0) {
                                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                                        if (e) {
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
                                                                                } else {
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
                                                                            }
                                                                        });
                                                                    }
                                                                })
                                                            } else {
                                                                var ClientSubscription = {
                                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                                    Type: 1,
                                                                    NAVDate: $('#NAVDate').val(),
                                                                    ValueDate: $('#ValueDate').val(),
                                                                    // PaymentDate: $('#PaymentDate').val(),
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
                                                                    Tenor: $('#Tenor').val(),
                                                                    InterestRate: $('#InterestRate').val(),
                                                                    PaymentTerm: $('#PaymentTerm').val(),
                                                                    EntryUsersID: sessionStorage.getItem("user")

                                                                };
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data != 0) {
                                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                                if (e) {
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
                                                                        } else {
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
                                                                    }
                                                                });

                                                            }
                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                        }
                                                    });
                                                });
                                            }
                                            else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if (data.length != 0) {
                                                            alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
                                                                if (e) {
                                                                    var ClientSubscription = {
                                                                        ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                                        Type: 1,
                                                                        NAVDate: $('#NAVDate').val(),
                                                                        ValueDate: $('#ValueDate').val(),
                                                                        // PaymentDate: $('#PaymentDate').val(),
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
                                                                        Tenor: $('#Tenor').val(),
                                                                        InterestRate: $('#InterestRate').val(),
                                                                        PaymentTerm: $('#PaymentTerm').val(),
                                                                        EntryUsersID: sessionStorage.getItem("user")

                                                                    };
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                                        type: 'GET',
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (data) {
                                                                            if (data != 0) {
                                                                                alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                                    if (e) {
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
                                                                            } else {
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
                                                                        }
                                                                    });
                                                                }
                                                            })
                                                        } else {
                                                            var ClientSubscription = {
                                                                ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                                Type: 1,
                                                                NAVDate: $('#NAVDate').val(),
                                                                ValueDate: $('#ValueDate').val(),
                                                                // PaymentDate: $('#PaymentDate').val(),
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
                                                                Tenor: $('#Tenor').val(),
                                                                InterestRate: $('#InterestRate').val(),
                                                                PaymentTerm: $('#PaymentTerm').val(),
                                                                EntryUsersID: sessionStorage.getItem("user")

                                                            };
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/ClientSubscription/ClientSubscriptionValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                                type: 'GET',
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    if (data != 0) {
                                                                        alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                            if (e) {
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
                                                                    } else {
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
                alertify.alert("Please Recalculate / Check Total Cash Amount First !")
            }
        }

    });

    $("#BtnUpdate").click(function () {
        //var val = validateData();
        //if (val == 1) {
            //if ($('#TotalCashAmount').val() != 0) {

                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {
                        //$.ajax({
                        //    url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                        //    type: 'GET',
                        //    contentType: "application/json;charset=utf-8",
                        //    success: function (data) {
                        //        if (data == false) {
                        //            $.ajax({
                        //                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                        //                type: 'GET',
                        //                contentType: "application/json;charset=utf-8",
                        //                success: function (data) {
                        //                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                        //                        _trxPK = '';
                        //                        if ($('#TransactionPK').val() != null) {
                        //                            _trxPK = $('#TransactionPK').val();
                        //                        }

                                                var ClientSubscription = {
                                                    ClientSubscriptionPK: $('#ClientSubscriptionPK').val(),
                                                    //Type: $('#Type').val(),
                                                    HistoryPK: $('#HistoryPK').val(),
                                                    //NAVDate: $('#NAVDate').val(),
                                                    //ValueDate: $('#ValueDate').val(),
                                                    ////PaymentDate: $('#PaymentDate').val(),
                                                    //NAV: $('#NAV').val(),
                                                    //FundPK: $('#FundPK').val(),
                                                    //FundClientPK: $('#FundClientPK').val(),
                                                    //CashRefPK: $('#CashRefPK').val(),
                                                    //CurrencyPK: $('#CurrencyPK').val(),
                                                    //Description: $('#Description').val(),
                                                    //ReferenceSInvest: $("#ReferenceSInvest").val(),
                                                    //CashAmount: $('#CashAmount').val(),
                                                    //UnitAmount: $('#UnitAmount').val(),
                                                    //TotalCashAmount: $('#TotalCashAmount').val(),
                                                    //TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                    //SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                    //SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                    //AgentPK: $('#AgentPK').val(),
                                                    //AgentFeePercent: $('#AgentFeePercent').val(),
                                                    //AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                    //BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                    //FeeType: $('#FeeType').val(),
                                                    //TransactionPK: _trxPK,
                                                    Tenor: $('#Tenor').val(),
                                                    InterestRate: $('#InterestRate').val(),
                                                    PaymentTerm: $('#PaymentTerm').val(),
                                                    //Notes: str,
                                                    EntryUsersID: sessionStorage.getItem("user")
                                                };
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/ClientSubscription/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClientSubscriptionWithInterest_U",
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

                //                }
                //                else {
                //                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
                //                }
                //            },
                //            error: function (data) {
                //                alertify.alert(data.responseText);
                //            }
                //        });
                    }
                });
            //}
            //else {
            //    alertify.alert("Please Recalculate / Check Total Cash Amount First !")
            //}
        //}

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
        });
    });

    $("#BtnUnApproveBySelected").click(function (e) {

        alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateUnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data ?", function (e) {
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
        });
    });

    $("#BtnReject").click(function () {

        alertify.confirm("Are you sure want to Reject data ?", function (e) {
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
                                 Name: { type: "string" }
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
               { field: "ID", title: "Client ID", width: 300 }

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
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClientSubscriptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "ClientSubscription",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
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

                                        } else {
                                            alertify.alert("Please check today/yesterday EndDayTrails!");

                                        }
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

    $("#BtnRecalculate").click(function (e) {
        ClientSubscriptionWithInterestRecalculate();
    });

    function ClientSubscriptionWithInterestRecalculate() {
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
                        var grid = $("#gridClientSubscriptionWithInterestApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
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

    $("#BtnApproveBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateCheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/ValidateApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/ClientSubscription/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                        }
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
                    url: window.location.origin + "/Radsoft/ClientSubscription/ValidateCheckDescription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClientSubscription",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClientSubscription/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                if (kendo.toString($("#DateFrom").data("kendoDatePicker").value(), 'MM-dd-yy') == kendo.toString($("#DateTo").data("kendoDatePicker").value(), 'MM-dd-yy')) {
                    $.blockUI();
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
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClientSubscription/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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


    $("#BtnGetNavBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Get Nav by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/GetNavBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/UnitRegistry_GetNavBySelected",
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

    $("#BtnBatchFormBySelected").click(function (e) {

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

    function showBatch(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {

            alertify.confirm("Are you sure want to open Batch Form ?", function (a) {
                if (a) {
                    // initGrid()
                    if (tabindex == 0 || tabindex == undefined) {
                        var grid = $("#gridClientSubscriptionWithInterestApproved").data("kendoGrid");
                    }
                    if (tabindex == 1) {
                        var grid = $("#gridClientSubscriptionWithInterestPending").data("kendoGrid");
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

        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ClientSubscription/SInvestSubscriptionRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
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
});
