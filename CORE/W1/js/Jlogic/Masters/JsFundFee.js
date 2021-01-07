$(document).ready(function () {
    document.title = 'FORM Fund Fee';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();


    if (_GlobClientCode == "32") {
        $("#LblMovementFeeAmount").hide();
        $("#LblOtherFeeOneAmount").hide();
        $("#LblOtherFeeTwoAmount").hide();
        $("#LblOtherFeeThreeAmount").hide();
        $("#LblCBESTEquityAmount").hide();
        $("#LblCBESTCorpBondAmount").hide();
        $("#LblCBESTGovBondAmount").hide();
        $("#LblAccruedInterestCalculation").hide();
    }
    else {
        $("#LblMovementFeeAmount").show();
        $("#LblOtherFeeOneAmount").show();
        $("#LblOtherFeeTwoAmount").show();
        $("#LblOtherFeeThreeAmount").show();
        $("#LblCBESTEquityAmount").show();
        $("#LblCBESTCorpBondAmount").show();
        $("#LblCBESTGovBondAmount").show();
        $("#LblAccruedInterestCalculation").show();
    }

    if (_GlobClientCode == '17') {
        $("#LblTaxInterestDeposit").show();


    }
    else {
        $("#LblTaxInterestDeposit").hide();
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
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnAddFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSaveFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });
        $("#BtnCancelFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnRejectFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });
        $("#BtnCopyFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSaveCopyFundFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnAddCustodiFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSaveCustodiFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });
        $("#BtnCancelCustodiFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnRejectCustodiFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });
    }

    function initWindow() {

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        $("#PaymentDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DateAmortization").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#ValueDateCopy").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#EValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        WinAddCustodiFee = $("#WinAddCustodiFee").kendoWindow({
            height: 750,
            title: "Add Other Fee",
            visible: false,
            width: 1300,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAddCustodiFeeClose
        }).data("kendoWindow");

        win = $("#WinFundFee").kendoWindow({
            height: 800,
            title: "Fund Fee Detail",
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
            },

        }).data("kendoWindow");

        WinAddFundFee = $("#WinAddFundFee").kendoWindow({
            height: 750,
            title: "Add Fund Fee",
            visible: false,
            width: 1300,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAddFundFeeClose
        }).data("kendoWindow");

        WinCopyFundFee = $("#WinCopyFundFee").kendoWindow({
            height: 350,
            title: "Copy Fund Fee",
            visible: false,
            width: 500,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinCopyFundFeeClose
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinFundFee").kendoValidator().data("kendoValidator");
    var GlobValidatorFundFee = $("#WinAddFundFee").kendoValidator().data("kendoValidator");
    var GlobValidatorCopyFundFee = $("#WinCopyFundFee").kendoValidator().data("kendoValidator");
    var GlobValidatorCustodiFee = $("#WinAddCustodiFee").kendoValidator().data("kendoValidator");

    function validateData() {
        if (Number.isInteger(parseInt($("#FundPK").val())) == false) {
            alertify.alert("Please Choose Fund!");
            return 0;
        }

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function validateDataFundFee(_BitMaxRangeTo) {
        if (Number.isInteger(parseInt($("#FeeType").val())) == false) {
            alertify.alert("Validation not Pass, Please Choose Fee Type");
            return 0;
        }


        //    if (GlobValidatorFundFee.validate()) {
        //        //alert("Validation sucess");
        //        return 1;
        //    }

        //    else {
        //        alertify.alert("Validation not Pass");
        //        return 0;
        //    }

        //}


        if (GlobValidatorFundFee.validate()) {
            //alert("Validation sucess");
            return 1;
        }

        else {
            alertify.alert("Validation not Pass");
            return 0;
        }

    }

    function validateDataCopyFundFee() {


        if (GlobValidatorCopyFundFee.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        if (e == null) {
            HideBtnAdd(0);
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            HideBtnAdd(dataItemX.Status);
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            $("#FundFeePK").val(dataItemX.FundFeePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            $("#ManagementFeePercent").val(dataItemX.ManagementFeePercent);
            $("#CustodiFeePercent").val(dataItemX.CustodiFeePercent);
            $("#AuditFeeAmount").val(dataItemX.AuditFeeAmount);
            $("#ManagementFeeDays").val(dataItemX.ManagementFeeDays);
            $("#CustodiFeeDays").val(dataItemX.CustodiFeeDays);
            $("#AuditFeeDays").val(dataItemX.AuditFeeDays);
            $("#SInvestFeeDays").val(dataItemX.SInvestFeeDays);
            $("#PaymentModeOnMaturity").val(dataItemX.PaymentModeOnMaturity);
            $("#SwitchingFeePercent").val(dataItemX.SwitchingFeePercent);
            $("#FeeTypeManagement").val(dataItemX.FeeTypeManagement);
            $("#FeeTypeSubscription").val(dataItemX.FeeTypeSubscription);
            $("#FeeTypeRedemption").val(dataItemX.FeeTypeRedemption);
            $("#FeeTypeSwitching").val(dataItemX.FeeTypeSwitching);
            $("#DateOfPayment").val(dataItemX.DateOfPayment);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.Date);
            $("#BitActDivDays").prop('checked', true, dataItemX.BitActDivDays);
            $("#TaxInterestDeposit").prop('checked', true, dataItemX.TaxInterestDeposit);

            //$("#BitMaxRangeTo").prop('checked', dataItemX.BitMaxRangeTo);
            $('#BitMaxRangeTo').change(function () {
                if (this.checked == false) {
                    $("#RangeTo").attr('readonly', false);
                }
                else {
                    $("#RangeTo").attr('readonly', true);
                }
            });

            $("#CBESTEquityAmount").val(dataItemX.CBESTEquityAmount);
            $("#CBESTCorpBondAmount").val(dataItemX.CBESTCorpBondAmount);
            $("#CBESTGovBondAmount").val(dataItemX.CBESTGovBondAmount);

            $("#Notes").val(dataItemX.Notes);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }


        $("#ManagementFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setManagementFeePercent()
        });
        function setManagementFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.ManagementFeePercent;
            }
        }

        $("#CustodiFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setCustodiFeePercent()
        });
        function setCustodiFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.CustodiFeePercent;
            }
        }

        $("#BitActDivDays").change(function () {
            if (this.checked) {

                $("#ManagementFeeDays").data("kendoNumericTextBox").enable(false);
                $("#CustodiFeeDays").data("kendoNumericTextBox").enable(false);
                $("#SInvestFeeDays").data("kendoNumericTextBox").enable(false);

            }
            else {

                $("#ManagementFeeDays").data("kendoNumericTextBox").enable(true);
                $("#CustodiFeeDays").data("kendoNumericTextBox").enable(true);
                $("#SInvestFeeDays").data("kendoNumericTextBox").enable(true);
            }
        });



        $("#AuditFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAuditFeeAmount()
        });
        function setAuditFeeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AuditFeeAmount;
            }
        }

        $("#ManagementFeeDays").kendoNumericTextBox({
            format: "##",

            value: setManagementFeeDays()
        });
        function setManagementFeeDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ManagementFeeDays;
            }
        }

        $("#CustodiFeeDays").kendoNumericTextBox({
            format: "##",

            value: setCustodiFeeDays()
        });
        function setCustodiFeeDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CustodiFeeDays;
            }
        }

        $("#AuditFeeDays").kendoNumericTextBox({
            format: "##",

            value: setAuditFeeDays()
        });
        function setAuditFeeDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AuditFeeDays;
            }
        }

        $("#SInvestFeeDays").kendoNumericTextBox({
            format: "##",

            value: setSInvestFeeDays()
        });
        function setSInvestFeeDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SInvestFeeDays;
            }
        }

        $("#SwitchingFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setSwitchingFeePercent()
        });
        function setSwitchingFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SwitchingFeePercent;
            }
        }

        $("#DateOfPayment").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setDateOfPayment()
        });
        function setDateOfPayment() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DateOfPayment;
            }
        }


        $("#SinvestMoneyMarketFeePercent").kendoNumericTextBox({
            format: "##.######## \\%",
            decimals: 8,
            value: setSinvestMoneyMarketFeePercent()
        });
        function setSinvestMoneyMarketFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SinvestMoneyMarketFeePercent;
            }
        }


        $("#SinvestBondFeePercent").kendoNumericTextBox({
            format: "##.######## \\%",
            decimals: 8,
            value: setSinvestBondFeePercent()
        });
        function setSinvestBondFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SinvestBondFeePercent;
            }
        }


        $("#SinvestEquityFeePercent").kendoNumericTextBox({
            format: "##.######## \\%",
            decimals: 8,
            value: setSinvestEquityFeePercent()
        });
        function setSinvestEquityFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SinvestEquityFeePercent;
            }
        }


        $("#MovementFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setMovementFeeAmount()
        });
        function setMovementFeeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MovementFeeAmount;
            }
        }

        //$("#AccruedInterestCalculation").kendoNumericTextBox({
        //    format: "n2",
        //    decimals: 2,
        //    value: setAccruedInterestCalculation()
        //});
        //function setAccruedInterestCalculation() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.AccruedInterestCalculation;
        //    }
        //}


        $("#AccruedInterestCalculation").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Exact Calculation", value: 1 },
                { text: "Rounded Calculation", value: 2 },
                { text: "Rounded Calculation BCA", value: 3 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeAccruedInterestCalculation,
            value: setCmbAccruedInterestCalculation()
        });
        function OnChangeAccruedInterestCalculation() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbAccruedInterestCalculation() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.AccruedInterestCalculation;
            }
        }

        $("#BitPendingSubscription").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitPendingSubscription,
            value: setCmbBitPendingSubscription()
        });
        function OnChangeBitPendingSubscription() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitPendingSubscription() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitPendingSubscription;
            }
        }


        $("#BitPendingSwitchIn").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitPendingSwitchIn,
            value: setCmbBitPendingSwitchIn()
        });
        function OnChangeBitPendingSwitchIn() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitPendingSwitchIn() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitPendingSwitchIn;
            }
        }

        $("#CBESTEquityAmount").kendoNumericTextBox({
            format: "n2",
            value: setCBESTEquityAmount(),
            //change: OnChangeAmount
        });
        function setCBESTEquityAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CBESTEquityAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CBESTEquityAmount;
                }
            }
        }

        $("#CBESTCorpBondAmount").kendoNumericTextBox({
            format: "n2",
            value: setCBESTCorpBondAmount(),
            //change: OnChangeAmount
        });
        function setCBESTCorpBondAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CBESTCorpBondAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CBESTCorpBondAmount;
                }
            }
        }

        $("#CBESTGovBondAmount").kendoNumericTextBox({
            format: "n2",
            value: setCBESTGovBondAmount(),
            //change: OnChangeAmount
        });
        function setCBESTGovBondAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CBESTGovBondAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CBESTGovBondAmount;
                }
            }
        }


        //combo box FundFeePK
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
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });

                $("#EFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
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

        //PaymentModeOnMaturity
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PaymentModeOnMaturity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PaymentModeOnMaturity").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangePaymentModeOnMaturity,
                    value: setCmbPaymentModeOnMaturity()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangePaymentModeOnMaturity() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbPaymentModeOnMaturity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PaymentModeOnMaturity == 0) {
                    return "";
                } else {
                    return dataItemX.PaymentModeOnMaturity;
                }
            }
        }

        //FeeTypeManagement
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeTypeManagement").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFeeTypeManagement,
                    value: setCmbFeeTypeManagement()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFeeTypeManagement() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFeeTypeManagement() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeTypeManagement == 0) {
                    return "";
                } else {
                    return dataItemX.FeeTypeManagement;
                }
            }
        }

        //FeeTypeSubscription
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeTypeSubscription").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFeeTypeSubscription,
                    value: setCmbFeeTypeSubscription()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFeeTypeSubscription() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFeeTypeSubscription() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeTypeSubscription == 0) {
                    return "";
                } else {
                    return dataItemX.FeeTypeSubscription;
                }
            }
        }

        //FeeTypeRedemption
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeTypeRedemption").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFeeTypeRedemption,
                    value: setCmbFeeTypeRedemption()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFeeTypeRedemption() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFeeTypeRedemption() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeTypeRedemption == 0) {
                    return "";
                } else {
                    return dataItemX.FeeTypeRedemption;
                }
            }
        }

        //FeeTypeSwitching
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeTypeSwitching").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFeeTypeSwitching,
                    value: setCmbFeeTypeSwitching()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFeeTypeSwitching() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFeeTypeSwitching() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeTypeSwitching == 0) {
                    return "";
                } else {
                    return dataItemX.FeeTypeSwitching;
                }
            }
        }


        //combo box FundFeePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDFundPK,
                    value: setCmbDFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbDFundPK() {
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

        //combo box FeeType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFeeType,
                    //value: setFeeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        //function setFeeType() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.FeeType == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.FeeType;
        //        }
        //    }
        //}

        //$("#FeeType").kendoComboBox({
        //    dataValueField: "value",
        //    dataTextField: "text",
        //    dataSource: [
        //        { text: "Tiering", value: 1 },
        //        { text: "Progresive", value: 2 },
        //        { text: "Amortization", value: 4 },
        //        { text: "Flat", value: 5 }

        //    ],
        //    filter: "contains",
        //    change: OnChangeFeeType,
        //    suggest: true
        //});


        function onChangeFeeType() {
            clearDataFundFee();
            clearDataFundFeeSetup();
            RequiredAttributes(this.value());
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#lblDateAmortize").hide();
                $("#lblMiAmount").hide();
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
            }
            else if (this.value() == 2) {
                $("#lblDateAmortize").hide();
                $("#lblMiAmount").hide();

            }
            else if (this.value() == 3) {
                $("#lblDateAmortize").hide();
                $("#lblMiPercent").hide();
            }

        }

        $("#MiFeeAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,

        });

        $("#RangeFrom").kendoNumericTextBox({
            format: "n0",
            decimals: 4,
        });


        $("#RangeTo").kendoNumericTextBox({
            format: "n0",
            decimals: 4,

        });

        $("#MiFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,

        });



        function clearDataFundFeeSetup() {
            $("#lblRangeFrom").show();
            $("#lblRangeTo").show();
            $("#lblDateAmortize").hide();
            $("#lblMiAmount").show();
            $("#lblFeeType").show();
            $("#lblDate").show();
            $("#lblMiPercent").show();
            $("#lblFundPK").show();

        }

        //readOnly();


        $("#OtherFeeOneAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setOtherFeeOneAmount()
        });
        function setOtherFeeOneAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.OtherFeeOneAmount;
            }
        }

        $("#OtherFeeTwoAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setOtherFeeTwoAmount()
        });
        function setOtherFeeTwoAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.OtherFeeTwoAmount;
            }
        }
        $("#OtherFeeThreeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setOtherFeeThreeAmount()
        });
        function setOtherFeeThreeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.OtherFeeThreeAmount;
            }
        }

        //additional Add Custody Fee
        //combo box FeeType dari Add Custody Fee
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboFundFeeByCustodyFeeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#EFeeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    //change: onChangeFeeType,
                    //value: setFeeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //combo box Custody Fee
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CustodiFeeSetupType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CustodiFeeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    //change: onChangeCustodiFeeType,
                    //value: setFeeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $("#AUMFrom").kendoNumericTextBox({
            format: "n0",
            decimals: 4,
        });


        $("#AUMTo").kendoNumericTextBox({
            format: "n0",
            decimals: 4,

        });


        $("#FeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,

        });

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
        $("#FundFeePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").val("");
        $("#PaymentDate").val("");
        $("#FundPK").val("");
        $("#ManagementFeePercent").val("");
        $("#CustodiFeePercent").val("");
        $("#AuditFeeAmount").val("");
        $("#ManagementFeeDays").val("");
        $("#CustodiFeeDays").val("");
        $("#AuditFeeDays").val("");
        $("#SInvestFeeDays").val("");
        $("#PaymentModeOnMaturity").val("");
        $("#PaymentModeOnMaturityDesc").val("");
        $("#SwitchingFeePercent").val("");
        $("#FeeTypeManagement").val("");
        $("#FeeTypeManagementDesc").val("");
        $("#FeeTypeSubscription").val("");
        $("#FeeTypeSubscriptionDesc").val("");
        $("#FeeTypeRedemption").val("");
        $("#FeeTypeRedemptionDesc").val("");
        $("#FeeTypeSwitching").val("");
        $("#FeeTypeSwitchingDesc").val("");
        $("#DateOfPayment").val("");
        $("#SinvestMoneyMarketFeePercent").val("");
        $("#SinvestBondFeePercent").val("");
        $("#SinvestEquityFeePercent").val("");
        $("#BitPendingSubscription").val("");
        $("#BitPendingSwitchIn").val("");
        $("#BitActDivDays").prop('checked', true);
        $("#MovementFeeAmount").val("");
        $("#OtherFeeOneAmount").val("");
        $("#OtherFeeTwoAmount").val("");
        $("#OtherFeeThreeAmount").val("");
        $("#CBESTEquityAmount").val("");
        $("#CBESTCorpBondAmount").val("");
        $("#CBESTGovBondAmount").val("");

        $("#AccruedInterestCalculation").val("");
        $("#TaxInterestDeposit").prop('checked', true);

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
                schema: {
                    model: {
                        fields: {
                            FundFeePK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Date: { type: "date" },
                            PaymentDate: { type: "date" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            FundName: { type: "string" },
                            ManagementFeePercent: { type: "decimal" },
                            CustodiFeePercent: { type: "decimal" },
                            AuditFeeAmount: { type: "decimal" },
                            ManagementFeeDays: { type: "number" },
                            CustodiFeeDays: { type: "number" },
                            AuditFeeDays: { type: "number" },
                            SInvestFeeDays: { type: "number" },
                            PaymentModeOnMaturity: { type: "number" },
                            PaymentModeOnMaturityDesc: { type: "string" },
                            SwitchingFeePercent: { type: "decimal" },
                            FeeTypeManagement: { type: "number" },
                            FeeTypeManagementDesc: { type: "string" },
                            FeeTypeSubscription: { type: "number" },
                            FeeTypeSubscriptionDesc: { type: "string" },
                            FeeTypeRedemption: { type: "number" },
                            FeeTypeRedemptionDesc: { type: "string" },
                            FeeTypeSwitching: { type: "number" },
                            FeeTypeSwitchingDesc: { type: "string" },
                            DateOfPayment: { type: "decimal" },
                            SinvestMoneyMarketFeePercent: { type: "decimal" },
                            SinvestBondFeePercent: { type: "decimal" },
                            SinvestEquityFeePercent: { type: "decimal" },
                            MovementFeeAmount: { type: "decimal" },
                            OtherFeeOneAmount: { type: "decimal" },
                            OtherFeeTwoAmount: { type: "decimal" },
                            OtherFeeThreeAmount: { type: "decimal" },
                            BitPendingSubscription: { type: "boolean" },
                            BitPendingSwitchIn: { type: "boolean" },
                            BitActDivDays: { type: "boolean" },

                            CBESTEquityAmount: { type: "decimal" },
                            CBESTCorpBondAmount: { type: "decimal" },
                            CBESTGovBondAmount: { type: "decimal" },

                            AccruedInterestCalculation: { type: "number" },
                            AccruedInterestCalculationDesc: { type: "string" },
                            TaxInterestDeposit: { type: "boolean" },

                            UpdateUsersID: { type: "string" },
                            UpdateTime: { type: "date" },
                            ApprovedUsersID: { type: "string" },
                            ApprovedTime: { type: "date" },
                            VoidUsersID: { type: "string" },
                            VoidTime: { type: "date" },
                            EntryUsersID: { type: "string" },
                            EntryTime: { type: "date" },
                            LastUpdate: { type: "date" },

                            Timestamp: { type: "string" }
                        }
                    }
                }
            });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridFundFeeApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundFeePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundFeeHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundFeeApprovedURL = window.location.origin + "/Radsoft/FundFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FundFeeApprovedURL);

        $("#gridFundFeeApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Fee"
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
                { field: "FundFeePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "FundName", title: "Fund Name", width: 300 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "DateOfPayment", title: "DateOfPayment", width: 300 },
                { field: "PaymentDate", title: "Payment Date", width: 200, hidden: true, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')#" },
                {
                    field: "ManagementFeePercent", title: "Management Fee", hidden: true, width: 200,
                    template: "#: ManagementFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "CustodiFeePercent", title: "Custodi Fee", width: 200,
                    template: "#: CustodiFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "AuditFeeAmount", title: "Audit Fee", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                {
                    field: "SwitchingFeePercent", title: "Switching Fee", width: 200,
                    template: "#: SwitchingFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "SinvestMoneyMarketFeePercent", title: "S-Invest Money Market Fee Percent", hidden: true, width: 200,
                    template: "#: SinvestMoneyMarketFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "SinvestBondFeePercent", title: "S-Invest Bond Fee Percent", hidden: true, width: 200,
                    template: "#: SinvestBondFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "SinvestEquityFeePercent", title: "S-Invest Equity Fee Percent", hidden: true, width: 200,
                    template: "#: SinvestEquityFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MovementFeeAmount", title: "Movement Fee Amount", width: 200,
                    template: "#: MovementFeeAmount  # ",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "OtherFeeOneAmount", title: "Other Fee One Amount", width: 200,
                    template: "#: OtherFeeOneAmount  # ",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "OtherFeeTwoAmount", title: "Other Fee Two Amount", width: 200,
                    template: "#: OtherFeeTwoAmount  # ",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "OtherFeeThreeAmount", title: "Other Fee Three Amount", width: 200,
                    template: "#: OtherFeeThreeAmount  # ",
                    attributes: { style: "text-align:right;" }
                },
                { field: "BitPendingSubscription", title: "BitPendingSubscription", width: 150, template: "#= BitPendingSubscription ? 'Yes' : 'No' #" },
                { field: "BitPendingSwitchIn", title: "BitPendingSwitchIn", width: 150, template: "#= BitPendingSwitchIn ? 'Yes' : 'No' #" },

                { field: "PaymentModeOnMaturityDesc", title: "PaymentModeOnMaturity", hidden: true, width: 300 },
                { field: "FeeTypeManagementDesc", title: "FeeTypeManagement", width: 300 },
                { field: "FeeTypeSubscriptionDesc", title: "FeeTypeSubscription", width: 300 },
                { field: "FeeTypeRedemptionDesc", title: "FeeTypeRedemption", width: 300 },
                { field: "FeeTypeSwitchingDesc", title: "FeeTypeSwitching", width: 300 },

                { field: "CBESTEquityAmount", title: "CBEST Equity Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "CBESTCorpBondAmount", title: "CBEST Corp Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "CBESTGovBondAmount", title: "CBEST Gov Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },

                { field: "AccruedInterestCalculation", hidden: true, title: "AccruedInterestCalculation", width: 200 },
                { field: "AccruedInterestCalculationDesc", title: "Accrued Interest Calculation", width: 200 },

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

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundFee").kendoTabStrip({
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
                        var FundFeePendingURL = window.location.origin + "/Radsoft/FundFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FundFeePendingURL);
                        $("#gridFundFeePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Fee"
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
                                { field: "FundFeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "FundName", title: "Fund Name", width: 300 },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "DateOfPayment", title: "DateOfPayment", width: 300 },
                                { field: "PaymentDate", title: "Payment Date", width: 200, hidden: true, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')#" },
                                {
                                    field: "ManagementFeePercent", title: "Management Fee", hidden: true, width: 200,
                                    template: "#: ManagementFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "CustodiFeePercent", title: "Custodi Fee", width: 200,
                                    template: "#: CustodiFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "AuditFeeAmount", title: "Audit Fee", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                {
                                    field: "SwitchingFeePercent", title: "Switching Fee", width: 200,
                                    template: "#: SwitchingFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestMoneyMarketFeePercent", title: "S-Invest Money Market Fee Percent", hidden: true, width: 200,
                                    template: "#: SinvestMoneyMarketFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestBondFeePercent", title: "S-Invest Bond Fee Percent", hidden: true, width: 200,
                                    template: "#: SinvestBondFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestEquityFeePercent", title: "S-Invest Equity Fee Percent", hidden: true, width: 200,
                                    template: "#: SinvestEquityFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "MovementFeeAmount", title: "Movement Fee Amount", width: 200,
                                    template: "#: MovementFeeAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeOneAmount", title: "Other Fee One Amount", width: 200,
                                    template: "#: OtherFeeOneAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeTwoAmount", title: "Other Fee Two Amount", width: 200,
                                    template: "#: OtherFeeTwoAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeThreeAmount", title: "Other Fee Three Amount", width: 200,
                                    template: "#: OtherFeeThreeAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "BitPendingSubscription", title: "BitPendingSubscription", width: 150, template: "#= BitPendingSubscription ? 'Yes' : 'No' #" },
                                { field: "BitPendingSwitchIn", title: "BitPendingSwitchIn", width: 150, template: "#= BitPendingSwitchIn ? 'Yes' : 'No' #" },

                                { field: "PaymentModeOnMaturityDesc", title: "PaymentModeOnMaturity", hidden: true, width: 300 },
                                { field: "FeeTypeManagementDesc", title: "FeeTypeManagement", width: 300 },
                                { field: "FeeTypeSubscriptionDesc", title: "FeeTypeSubscription", width: 300 },
                                { field: "FeeTypeRedemptionDesc", title: "FeeTypeRedemption", width: 300 },
                                { field: "FeeTypeSwitchingDesc", title: "FeeTypeSwitching", width: 300 },

                                { field: "CBESTEquityAmount", title: "CBEST Equity Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "CBESTCorpBondAmount", title: "CBEST Corp Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "CBESTGovBondAmount", title: "CBEST Gov Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },

                                { field: "AccruedInterestCalculation", hidden: true, title: "AccruedInterestCalculation", width: 200 },
                                { field: "AccruedInterestCalculationDesc", title: "Accrued Interest Calculation", width: 200 },

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
                    if (tabindex == 2) {

                        var FundFeeHistoryURL = window.location.origin + "/Radsoft/FundFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FundFeeHistoryURL);

                        $("#gridFundFeeHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Fee"
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
                                { field: "FundFeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "FundName", title: "Fund Name", width: 300 },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "DateOfPayment", title: "DateOfPayment", width: 300 },
                                { field: "PaymentDate", title: "Payment Date", hidden: true, width: 200, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')#" },
                                {
                                    field: "ManagementFeePercent", title: "Management Fee", hidden: true, width: 200,
                                    template: "#: ManagementFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "CustodiFeePercent", title: "Custodi Fee", width: 200,
                                    template: "#: CustodiFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "AuditFeeAmount", title: "Audit Fee", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                {
                                    field: "SwitchingFeePercent", title: "Switching Fee", width: 200,
                                    template: "#: SwitchingFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestMoneyMarketFeePercent", title: "S-Invest Money Market Fee Percent", width: 200,
                                    template: "#: SinvestMoneyMarketFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestBondFeePercent", title: "S-Invest Bond Fee Percent", width: 200,
                                    template: "#: SinvestBondFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SinvestEquityFeePercent", title: "S-Invest Equity Fee Percent", width: 200,
                                    template: "#: SinvestEquityFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "MovementFeeAmount", title: "Movement Fee Amount", width: 200,
                                    template: "#: MovementFeeAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeOneAmount", title: "Other Fee One Amount", width: 200,
                                    template: "#: OtherFeeOneAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeTwoAmount", title: "Other Fee Two Amount", width: 200,
                                    template: "#: OtherFeeTwoAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OtherFeeThreeAmount", title: "Other Fee Three Amount", width: 200,
                                    template: "#: OtherFeeThreeAmount  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "BitPendingSubscription", title: "BitPendingSubscription", width: 150, template: "#= BitPendingSubscription ? 'Yes' : 'No' #" },
                                { field: "BitPendingSwitchIn", title: "BitPendingSwitchIn", width: 150, template: "#= BitPendingSwitchIn ? 'Yes' : 'No' #" },

                                { field: "PaymentModeOnMaturityDesc", title: "PaymentModeOnMaturity", hidden: true, width: 300 },
                                { field: "FeeTypeManagementDesc", title: "FeeTypeManagement", width: 300 },
                                { field: "FeeTypeSubscriptionDesc", title: "FeeTypeSubscription", width: 300 },
                                { field: "FeeTypeRedemptionDesc", title: "FeeTypeRedemption", width: 300 },
                                { field: "FeeTypeSwitchingDesc", title: "FeeTypeSwitching", width: 300 },

                                { field: "CBESTEquityAmount", title: "CBEST Equity Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "CBESTCorpBondAmount", title: "CBEST Corp Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                                { field: "CBESTGovBondAmount", title: "CBEST Gov Bond Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },

                                { field: "AccruedInterestCalculation", hidden: true, title: "AccruedInterestCalculation", width: 200 },
                                { field: "AccruedInterestCalculationDesc", title: "Accrued Interest Calculation", width: 200 },

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
                } else {
                    refresh();
                }
            }
        });
    }

    function gridHistoryDataBound() {
        var grid = $("#gridFundFeeHistory").data("kendoGrid");
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

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    function HideBtnAdd(_status) {
        if (_status == 1) {
            $("#BtnAddFundFee").show();
        }
        else if (_status == 2) {
            $("#BtnAddFundFee").show();
        }
        else if (_status == 3) {
            $("#BtnAddFundFee").hide();
        }
        else if (_status == 0) {
            $("#BtnAddFundFee").hide();
        }
    }

    $("#BtnAddFundFee").click(function () {
        clearDataFundFee();
        GridFundFee();
        if ($("#FundPK").val() == 0 || $("#FundPK").val() == null) {
            alertify.alert("There's no Fund");
        } else {
            showAddFundFee($("#FundPK").val());
        }
    });

    function showAddFundFee(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid = $("#gridAddFundFee").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }

       
        WinAddFundFee.center();
        WinAddFundFee.open();

    }

    function onWinAddFundFeeClose() {
        GlobValidatorFundFee.hideMessages();
        clearDataFundFee();
        $("#FeeType").data("kendoComboBox").value("");
        refresh();
    }

    function onWinCopyFundFeeClose() {
        GlobValidatorCopyFundFee.hideMessages();
        clearDataCopyFundFee();
        refresh();
    }

    function clearDataFundFee() {
        //$("#FeeType").data("kendoComboBox").value("");
        //$("#FundPK").data("kendoComboBox").value("");
        $("#MiFeeAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MiFeePercent").data("kendoNumericTextBox").value("");
        //$("#Date").val("");
        $("#lblMiAmount").hide();
        $("#lblRangeFrom").hide();
        $("#lblRangeTo").hide();
        $("#lblMiPercent").hide();
        $("#lblDateAmortize").hide();
        $("#DateAmortization").val("");
        $("#BitMaxRangeTo").prop('checked', false);
        $("#BitActDivDays").prop('checked', true);
        $("#TaxInterestDeposit").prop('checked', true);
        $("#RangeTo").attr('readonly', false);

    }

    function clearDataCopyFundFee() {
        $("#ValueDateCopy").val("");
    }


    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var FundFee = {
                        FundPK: $('#FundPK').val(),
                        Date: $('#Date').val(),
                        PaymentDate: $('#PaymentDate').val(),
                        ManagementFeePercent: $('#ManagementFeePercent').val(),
                        CustodiFeePercent: $('#CustodiFeePercent').val(),
                        AuditFeeAmount: $('#AuditFeeAmount').val(),
                        ManagementFeeDays: $('#ManagementFeeDays').val(),
                        CustodiFeeDays: $('#CustodiFeeDays').val(),
                        AuditFeeDays: $('#AuditFeeDays').val(),
                        SInvestFeeDays: $('#SInvestFeeDays').val(),
                        PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                        SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                        FeeTypeManagement: $('#FeeTypeManagement').val(),
                        FeeTypeSubscription: $('#FeeTypeSubscription').val(),
                        FeeTypeRedemption: $('#FeeTypeRedemption').val(),
                        FeeTypeSwitching: $('#FeeTypeSwitching').val(),
                        DateOfPayment: $('#DateOfPayment').val(),
                        SinvestMoneyMarketFeePercent: $('#SinvestMoneyMarketFeePercent').val(),
                        SinvestBondFeePercent: $('#SinvestBondFeePercent').val(),
                        SinvestEquityFeePercent: $('#SinvestEquityFeePercent').val(),
                        BitPendingSubscription: $("#BitPendingSubscription").val(),
                        BitPendingSwitchIn: $("#BitPendingSwitchIn").val(),
                        BitActDivDays: $('#BitActDivDays').is(":checked"),
                        MovementFeeAmount: $("#MovementFeeAmount").val(),
                        OtherFeeOneAmount: $("#OtherFeeOneAmount").val(),
                        OtherFeeTwoAmount: $("#OtherFeeTwoAmount").val(),
                        OtherFeeThreeAmount: $("#OtherFeeThreeAmount").val(),
                        CBESTEquityAmount: $("#CBESTEquityAmount").val(),
                        CBESTCorpBondAmount: $("#CBESTCorpBondAmount").val(),
                        CBESTGovBondAmount: $("#CBESTGovBondAmount").val(),

                        AccruedInterestCalculation: $("#AccruedInterestCalculation").val(),
                        TaxInterestDeposit: $('#TaxInterestDeposit').is(":checked"),

                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_I",
                        type: 'POST',
                        data: JSON.stringify(FundFee),
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
        var val = validateData();
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundFee",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundFee = {
                                    FundFeePK: $('#FundFeePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    Date: $('#Date').val(),
                                    PaymentDate: $('#PaymentDate').val(),
                                    ManagementFeePercent: $('#ManagementFeePercent').val(),
                                    CustodiFeePercent: $('#CustodiFeePercent').val(),
                                    AuditFeeAmount: $('#AuditFeeAmount').val(),
                                    ManagementFeeDays: $('#ManagementFeeDays').val(),
                                    CustodiFeeDays: $('#CustodiFeeDays').val(),
                                    AuditFeeDays: $('#AuditFeeDays').val(),
                                    SInvestFeeDays: $('#SInvestFeeDays').val(),
                                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                    SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                    FeeTypeManagement: $('#FeeTypeManagement').val(),
                                    FeeTypeSubscription: $('#FeeTypeSubscription').val(),
                                    FeeTypeRedemption: $('#FeeTypeRedemption').val(),
                                    FeeTypeSwitching: $('#FeeTypeSwitching').val(),
                                    DateOfPayment: $('#DateOfPayment').val(),
                                    SinvestMoneyMarketFeePercent: $('#SinvestMoneyMarketFeePercent').val(),
                                    SinvestBondFeePercent: $('#SinvestBondFeePercent').val(),
                                    SinvestEquityFeePercent: $('#SinvestEquityFeePercent').val(),
                                    BitPendingSubscription: $("#BitPendingSubscription").val(),
                                    BitPendingSwitchIn: $("#BitPendingSwitchIn").val(),
                                    BitActDivDays: $('#BitActDivDays').is(":checked"),
                                    MovementFeeAmount: $("#MovementFeeAmount").val(),
                                    OtherFeeOneAmount: $("#OtherFeeOneAmount").val(),
                                    OtherFeeTwoAmount: $("#OtherFeeTwoAmount").val(),
                                    OtherFeeThreeAmount: $("#OtherFeeThreeAmount").val(),
                                    CBESTEquityAmount: $("#CBESTEquityAmount").val(),
                                    CBESTCorpBondAmount: $("#CBESTCorpBondAmount").val(),
                                    CBESTGovBondAmount: $("#CBESTGovBondAmount").val(),

                                    AccruedInterestCalculation: $("#AccruedInterestCalculation").val(),
                                    TaxInterestDeposit: $('#TaxInterestDeposit').is(":checked"),

                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundFee),
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

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundFee",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundFee" + "/" + $("#FundFeePK").val(),
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
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundFee = {
                                FundFeePK: $('#FundFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_A",
                                type: 'POST',
                                data: JSON.stringify(FundFee),
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
        
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundFee = {
                                FundFeePK: $('#FundFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_V",
                                type: 'POST',
                                data: JSON.stringify(FundFee),
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
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundFee = {
                                FundFeePK: $('#FundFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_R",
                                type: 'POST',
                                data: JSON.stringify(FundFee),
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


    $("#BtnSaveFundFee").click(function () {
        checkmaxrange($('#BitMaxRangeTo').is(":checked"));
        RequiredAttributes($('#FeeType').val());
        var val = validateDataFundFee();


        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                var FundFeeSetup = {
                    FundPK: $('#FundPK').val(),
                    Date: $('#ValueDate').val(),
                    RangeFrom: $('#RangeFrom').val(),
                    RangeTo: $('#RangeTo').val(),
                    EntryUsersID: sessionStorage.getItem("user"),
                    FeeType: $('#FeeType').val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundFee/AddValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(FundFeeSetup),
                        success: function (data) {
                        if (data == "FALSE") {
       
                            var Fund = {
                                FundPK: $('#FundPK').val(),
                                FeeType: $('#FeeType').val(),
                                Date: $('#ValueDate').val(),
                                DateAmortize: $('#DateAmortize').val(),
                                MiFeeAmount: $('#MiFeeAmount').val(),
                                RangeFrom: $('#RangeFrom').val(),
                                RangeTo: $('#RangeTo').val(),
                                MiFeePercent: $('#MiFeePercent').val(),
                                EntryUsersID: sessionStorage.getItem("user")

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_AddFundFeeSetup",
                                type: 'POST',
                                data: JSON.stringify(Fund),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    WinAddFundFee.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        }
                        else {
                            alertify.alert(data);
                            refresh();
                        }
                    }
                });

            });
        }
        

    });


    function checkmaxrange(_check)
    {
        if (_check == true) {
            $("#RangeTo").attr("required", false);
        }
        else
        {
            $("#RangeTo").attr("required", true);
        }
    }

    $("#BtnCancelFundFee").click(function () {

        alertify.confirm("Are you sure want to close Add Fund Fee ?",
            function (e) {
                if (e) {
                    WinAddFundFee.close();
                    alertify.alert("Close Add Fund Fee");
                }
            });
    });

    function getDataSourceFundFee(_url) {
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
                             FundPK: { type: "number" },
                             Fund: { type: "string" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
                             Date: { type: "string" },
                             DateAmortize: { type: "string" },
                             MiFeeAmount: { type: "number" },
                             FundPK: { type: "number" },
                             FundName: { type: "string" },
                             RangeFrom: { type: "number" },
                             RangeTo: { type: "number" },
                             MiFeePercent: { type: "number" },
                         }
                     }
                 }
             });
    }

    function GridFundFee() {
        ClearDataGrid();
        $("#gridAddFundFee").empty();
        var FundFeeURL = window.location.origin + "/Radsoft/FundFee/GetDataFundFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
          dataSourceApproved = getDataSourceFundFee(FundFeeURL);

        var gridDetail = $("#gridAddFundFee").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Fee"
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
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetail' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAll' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "FundFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 110, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", title: "FundPK", hidden: true, width: 80 },
                { field: "FundName", title: "Fund", width: 250 },
                { field: "FeeTypeDesc", title: "Fee Type", width: 130 },
                { field: "RangeFrom", title: "Range From", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangeTo", title: "Range To", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MiFeeAmount", title: "Mi Fee Amount", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } }, {
                    field: "MiFeePercent", title: "Mi Fee Percent", width: 120,
                    template: "#: MiFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == '1/1/1900 12:00:00 AM') ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
            ]
        }).data("kendoGrid");
        $("#SelectedAll").change(function () {

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

        gridDetail.table.on("click", ".cSelectedDetail", selectDataPending);

        function ClearDataGrid() {
            $("#gridAddFundFee").empty();
        }

        function selectDataPending(e) {


            var grid = $("#gridAddFundFee").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _FundFeeSetupPK = dataItemX.FundFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _FundFeeSetupPK);

        }


        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeSetup/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeSetup/" + _a,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail").prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }



    $("#BtnRejectFundFee").click(function () {

        alertify.confirm("Are you sure want to Reject This Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FundFee/RejectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DFundPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinAddFundFee.close();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });


    });

    $("#BtnCopyFundFee").click(function () {

        showCopyFundFee()

    });

    function showCopyFundFee(e) {


        WinCopyFundFee.center();
        WinCopyFundFee.open();

    }

    $("#BtnSaveCopyFundFee").click(function () {
        var val = validateDataCopyFundFee();
        if (val == 1) {
            alertify.confirm("Are you sure want to Copy data?", function (e) {
                if (e) {
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/FundFee/CheckHasAddCopy/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + kendo.toString($("#ValueDateCopy").data("kendoDatePicker").value(), "MM-dd-yy"),
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                            //if (data == false) {
                                var Fund = {
                                    FundPK: $('#FundPK').val(),
                                    Date: $('#Date').val(),
                                    ValueDateCopy: $('#ValueDateCopy').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundFee_CopyFundFeeSetup",
                                type: 'POST',
                                contentType: "application/json;charset=utf-8",
                                data: JSON.stringify(Fund),
                                success: function (data) {
                                    alertify.alert(data);
                                    WinCopyFundFee.close();
                                    WinAddFundFee.open();
                                    WinAddFundFee.center();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    WinCopyFundFee.close();
                                    WinAddFundFee.open();
                                    WinAddFundFee.center();
                                }
                            });

                    //        }
                    //        else {
                    //            alertify.alert("Data Has Been Add, Check Your Date");
                    //            WinCopyFundFee.close();
                    //            //WinAddFundFee.close();
                    //            refresh();
                    //        }
                    //    }
                    //});

                }
            });
        }

    });

    function RequiredAttributes(_type) {
        ResetRequired();
        if (_type == 1) {
            $("#MiFeePercent").attr("required", true);
        }
        else if (_type == 2) {
            $("#RangeFrom").attr("required", true);
            $("#RangeTo").attr("required", true);
            $("#MiFeePercent").attr("required", true);

        }
        else if (_type == 3) {
            $("#RangeFrom").attr("required", true);
            $("#RangeTo").attr("required", true);
            $("#MiFeeAmount").attr("required", true);
        }

        function ResetRequired() {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#DateAmortization").attr("required", false);
            $("#MiFeeAmount").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);
            $("#MiFeePercent").attr("required", false);
            
        }
    }

    //Custodi Fee
    $("#BtnAddCustodiFee").click(function () {
        clearDataCustodiFee();
        GridCustodiFee();
        if ($("#FundPK").val() == 0 || $("#FundPK").val() == null) {
            alertify.alert("There's no Fund");
        } else {
            showAddCustodiFee($("#FundPK").val());
        }
    });

    function showAddCustodiFee(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid = $("#gridAddCustodiFee").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }


        WinAddCustodiFee.center();
        WinAddCustodiFee.open();

    }

    function onWinAddCustodiFeeClose() {
        GlobValidatorCustodiFee.hideMessages();
        clearDataCustodiFee();
        refresh();
    }

    function clearDataCustodiFee() {
        //$("#CustodiFeeType").data("kendoComboBox").value("");
        //$("#EFundPK").data("kendoComboBox").value("");
        $("#AUMFrom").data("kendoNumericTextBox").value("");
        $("#AUMTo").data("kendoNumericTextBox").value("");
        $("#FeePercent").data("kendoNumericTextBox").value("");
        $("#EValueDate").val("");

    }

    //Custodi Fee
    function validateDataCustodiFee(_BitMaxRangeTo) {


        if (GlobValidatorCustodiFee.validate()) {
            //alert("Validation sucess");
            return 1;
        }

        else {
            alertify.alert("Validation not Pass");
            return 0;
        }

    }

    $("#BtnSaveCustodiFee").click(function () {
        checkmaxAUM($('#BitMaxAUMTo').is(":checked"));
        RequiredAttributes($('#EFeeType').val());
        RequiredAttributes($('#CustodianFeeType').val());
        var val = validateDataCustodiFee();


        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                var CustodiFeeSetup = {
                    FundPK: $('#EFundPK').val(),
                    Date: $('#EValueDate').val(),
                    AUMFrom: $('#AUMFrom').val(),
                    AUMTo: $('#AUMTo').val(),
                    EntryUsersID: sessionStorage.getItem("user"),
                    FeeType: $('#EFeeType').val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/CustodiFeeSetup/AddValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(CustodiFeeSetup),
                    success: function (data) {
                        if (data == "FALSE") {

                            var Fund = {
                                FundPK: $('#EFundPK').val(),
                                CustodiFeeType: $('#CustodiFeeType').val(),
                                FeeType: $('#EFeeType').val(),
                                Date: $('#EValueDate').val(),
                                AUMFrom: $('#AUMFrom').val(),
                                AUMTo: $('#AUMTo').val(),
                                FeePercent: $('#FeePercent').val(),
                                EntryUsersID: sessionStorage.getItem("user")

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CustodiFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustodiFee_AddCustodiFeeSetup",
                                type: 'POST',
                                data: JSON.stringify(Fund),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    GridCustodiFee();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });

                        }
                        else {
                            alertify.alert(data);
                            refresh();
                        }
                    }
                });

            });
        }


    });


    function checkmaxAUM(_check) {
        if (_check == true) {
            $("#AUMTo").attr("required", false);
        }
        else {
            $("#AUMTo").attr("required", true);
        }
    }

    $("#BtnCancelCustodiFee").click(function () {

        alertify.confirm("Are you sure want to close Add Custodi Fee ?",
            function (e) {
                if (e) {
                    WinAddCustodiFee.close();
                    alertify.alert("Close Add Custodi Fee");
                }
            });
    });

    function getDataSourceCustodiFee(_url) {
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
                            FundPK: { type: "number" },
                            Fund: { type: "string" },
                            FeeType: { type: "number" },
                            FeeTypeDesc: { type: "string" },
                            CustodiFeeType: { type: "number" },
                            CustodiFeeTypeDesc: { type: "string" },
                            Date: { type: "string" },
                            FundPK: { type: "number" },
                            FundName: { type: "string" },
                            AUMFrom: { type: "number" },
                            AUMTo: { type: "number" },
                            FeePercent: { type: "number" },
                        }
                    }
                }
            });
    }

    function GridCustodiFee() {
        ClearDataGrid();
        $("#gridAddCustodiFee").empty();
        var CustodiFeeURL = window.location.origin + "/Radsoft/CustodiFeeSetup/GetDataCustodiFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
            dataSourceApproved = getDataSourceCustodiFee(CustodiFeeURL);

        var gridDetail = $("#gridAddCustodiFee").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Custodi Fee"
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
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetail' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAll' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "CustodiFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 110, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", title: "FundPK", hidden: true, width: 80 },
                { field: "FundName", title: "Fund", width: 250 },
                { field: "CustodiFeeTypeDesc", title: "Other Fee Type", width: 130 },
                { field: "FeeTypeDesc", title: "Fee Type", width: 130 },
                { field: "AUMFrom", title: "AUM From", width: 120, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AUMTo", title: "AUM To", width: 120, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                {
                    field: "FeePercent", title: "Fee Percent", width: 120,
                    template: "#: FeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
            ]
        }).data("kendoGrid");
        $("#SelectedAll").change(function () {

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

        gridDetail.table.on("click", ".cSelectedDetail", selectDataPending);

        function ClearDataGrid() {
            $("#gridAddCustodiFee").empty();
        }

        function selectDataPending(e) {


            var grid = $("#gridAddCustodiFee").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _CustodiFeeSetupPK = dataItemX.CustodiFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _CustodiFeeSetupPK);

        }


        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CustodiFeeSetup/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CustodiFeeSetup/" + _a,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail").prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }



    $("#BtnRejectCustodiFee").click(function () {

        alertify.confirm("Are you sure want to Reject This Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/CustodiFeeSetup/RejectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DFundPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinAddCustodiFee.close();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });


    });

    //end CustodiFee

});
