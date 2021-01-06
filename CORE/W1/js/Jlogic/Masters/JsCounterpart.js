$(document).ready(function () {
    document.title = 'FORM COUNTERPART';
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
        $("#LblCounterpartType").hide();
        $("#LblTargetAllocation").hide();
        $("#CounterpartType").attr("required", false);
        $("#LblAPERDMFeeMethod").show();

    }
    else {
        $("#LblCounterpartType").show();
        $("#LblTargetAllocation").show();
        $("#CounterpartType").attr("required", true);
        $("#LblAPERDMFeeMethod").hide();
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

        $("#BtnAddCounterpartFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRejectCounterpartFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnSaveCounterpartFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });
        $("#BtnCancelCounterpartFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
    }

    
     

    function initWindow() {

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

        win = $("#WinCounterpart").kendoWindow({
            height: 850,
            title: "Counterpart Detail",
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

        WinAddCounterpartFee = $("#WinAddCounterpartFee").kendoWindow({
            height: 750,
            title: "Add Counterpart Fee",
            visible: false,
            width: 1300,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAddCounterpartFeeClose
        }).data("kendoWindow");

    }



    var GlobValidator = $("#WinCounterpart").kendoValidator().data("kendoValidator");
    var GlobValidatorCounterpartFee = $("#WinAddCounterpartFee").kendoValidator().data("kendoValidator");

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

    function validateDataCounterpartFee(_BitMaxRangeTo) {
        if (Number.isInteger(parseInt($("#FeeType").val())) == false) {
            alertify.alert("Validation not Pass, Please Choose Fee Type");
            return 0;
        }



        if (GlobValidatorCounterpartFee.validate()) {
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
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#ID").attr('readonly', true);
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

            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#ContactPerson").val(dataItemX.ContactPerson);
            $("#Address").val(dataItemX.Address);
            $("#Email").val(dataItemX.Email);
            $("#NPWP").val(dataItemX.NPWP);
            $("#Description").val(dataItemX.Description);
            $("#CBestAccount").val(dataItemX.CBestAccount);
            $("#BankAccountOffice").val(dataItemX.BankAccountOffice);
            $("#BankAccountNo").val(dataItemX.BankAccountNo);
            $("#BankAccountRecipient").val(dataItemX.BankAccountRecipient);
            $("#CBestAccount").val(dataItemX.CBestAccount);
            $("#Phone").val(dataItemX.Phone);
            $("#Fax").val(dataItemX.Fax);
            $("#TDays").val(dataItemX.TDays);
            $("#RoundingMode").val(dataItemX.RoundingMode);
            $("#SInvestCode").val(dataItemX.SInvestCode);
            $("#DecimalPlaces").val(dataItemX.DecimalPlaces);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }



        //BankAccount
        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankPK").kendoComboBox({
                    dataValueField: "BankBranchPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBankPK,
                    value: setCmbBankPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBankPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBankPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankPK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RoundingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#RoundingMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingMode,
                    dataSource: data,
                    value: setCmbRoundingMode()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeRoundingMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingMode() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RoundingMode == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingMode;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DecimalPlaces",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DecimalPlaces").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalPlaces,
                    dataSource: data,
                    value: setCmbDecimalPlaces()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeDecimalPlaces() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalPlaces() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DecimalPlaces == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalPlaces;
                }
            }
        }

        //combo box CounterpartFeePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDCounterpartPK,
                    value: setCmbDCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDCounterpartPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbDCounterpartPK() {
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
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


      


        function onChangeFeeType() {
            clearDataCounterpartFee();
            clearDataCounterpartFeeSetup();
            RequiredAttributes(this.value());
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#lblDateAmortize").hide();
                $("#lblAPERDPortionAmount").hide();
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
            }
            else if (this.value() == 2) {
                $("#lblDateAmortize").hide();
                $("#lblAPERDPortionAmount").hide();

            }
            else if (this.value() == 3) {
                $("#lblDateAmortize").hide();
                $("#lblAPERDPortionPercent").hide();
            }

        }

        $("#APERDPortionAmount").kendoNumericTextBox({
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

        $("#APERDPortionPercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,

        });


        function clearDataCounterpartFeeSetup() {
            $("#lblRangeFrom").show();
            $("#lblRangeTo").show();
            $("#lblDateAmortize").hide();
            $("#lblAPERDPortionAmount").show();
            $("#lblFeeType").show();
            $("#lblDate").show();
            $("#lblAPERDPortionPercent").show();
            $("#lblFundPK").show();

        }


        // Market Combo
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
                    change: OnChangeMarketPK,
                    value: setCmbMarketPK(),
                    index: 0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeMarketPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbMarketPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MarketPK == 0) {
                    return "";
                } else {
                    return dataItemX.MarketPK;
                }
            }
        }

        //combo box CounterpartType//
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CounterpartType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CounterpartType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCounterpartType,
                    value: setCmbCounterpartType(),
                    index: 0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCounterpartType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCounterpartType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CounterpartType == 0) {
                    return "";
                } else {
                    return dataItemX.CounterpartType;
                }
            }
        }

        //CashRef Payment
        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashRefPaymentPK").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeCashRefPaymentPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbCashRefPaymentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbCashRefPaymentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashRefPaymentPK == 0) {
                    return "";
                } else {
                    return dataItemX.CashRefPaymentPK;
                }
            }
        }

        function OnChangeCashRefPaymentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }



        $("#BitSuspended").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitSuspended,
            value: setCmbBitSuspended()
        });
        function OnChangeBitSuspended() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitSuspended() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitSuspended;
            }
        }

        $("#BitRegistered").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitRegistered,
            value: setCmbBitRegistered()
        });
        function OnChangeBitRegistered() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitRegistered() {
            if (e == null) {
                return true;
            } else {
                return dataItemX.BitRegistered;
            }
        }


        $("#TDays").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setTDays()

        });
        function setTDays() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.TDays;
            }
        }

        $("#LevyVATPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setLevyVATPercent()

        });
        function setLevyVATPercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.LevyVATPercent;
            }
        }


        $("#TargetAllocationPercentage").kendoNumericTextBox({
            format: "%",
            value: setTargetAllocationPercentage(),
            //change: OnChangeAmount
        });
        function setTargetAllocationPercentage() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TargetAllocationPercentage == 0) {
                    return "";
                } else {
                    return dataItemX.TargetAllocationPercentage;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/APERDMFeeMethod",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#APERDMFeeMethod").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeAPERDMFeeMethod,
                    dataSource: data,
                    value: setCmbAPERDMFeeMethod()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeAPERDMFeeMethod() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAPERDMFeeMethod() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APERDMFeeMethod == 0) {
                    return "";
                } else {
                    return dataItemX.APERDMFeeMethod;
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

    function clearData() {
        $("#CounterpartPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#ContactPerson").val("");
        $("#Address").val("");
        $("#Phone").val("");
        $("#Fax").val("");
        $("#Email").val("");
        $("#NPWP").val("");
        $("#CounterpartType").val("");
        $("#Description").val("");
        $("#BitSuspended").val("");
        $("#BitRegistered").val("");
        $("#CBestAccount").val("");
        $("#MarketPK").val("");
        $("#TDays").val("");
        $("#CashRefPaymentPK").val("");
        $("#CashRefPaymentID").val("");
        $("#BankPK").val("");
        $("#BankAccountID").val("");
        $("#BankAccountOffice").val("");
        $("#BankAccountNo").val("");
        $("#BankAccountRecipient").val("");
        $("#RoundingMode").val("");
        $("#DecimalPlaces").val("");
        $("#SInvestCode").val("");
        $("#LevyVATPercent").val("");
        $("#TargetAllocationPercentage").val("");
        $("#APERDMFeeMethod").val("");
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
                             CounterpartPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             ContactPerson: { type: "string" },
                             Address: { type: "string" },
                             Phone: { type: "string" },
                             Fax: { type: "string" },
                             Email: { type: "string" },
                             NPWP: { type: "string" },
                             CounterpartType: { type: "number" },
                             CounterpartTypeDesc: { type: "string" },
                             Description: { type: "string" },
                             BitSuspended: { type: "Boolean" },
                             BitRegistered: { type: "Boolean" },
                             CBestAccount: { type: "string" },
                             MarketPK: { type: "number" },
                             MarketID: { type: "string" },
                             TDays: { type: "number" },
                             CashRefPaymentPK: { type: "number" },
                             CashRefPaymentID: { type: "string" },
                             BankPK: { type: "number" },
                             BankAccountID: { type: "string" },
                             BankAccountOffice: { type: "string" },
                             BankAccountNo: { type: "string" },
                             BankAccountRecipient: { type: "string" },
                             LevyVATPercent: { type: "number" },
                             RoundingMode: { type: "number" },
                             RoundingModeDesc: { type: "string" },
                             DecimalPlaces: { type: "number" },
                             TargetAllocationPercentage: { type: "number" },
                             APERDMFeeMethod: { type: "number" },
                             SInvestCode: { type: "string" },
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
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridCounterpartApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCounterpartPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCounterpartHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CounterpartApprovedURL = window.location.origin + "/Radsoft/Counterpart/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(CounterpartApprovedURL);

        $("#gridCounterpartApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Counterpart"
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
                { field: "CounterpartPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "ContactPerson", title: "Contact Person", width: 200 },
                { field: "Address", title: "Address", width: 300 },
                { field: "Phone", title: "Phone", width: 200 },
                { field: "Fax", title: "Fax", width: 200 },
                { field: "Email", title: "Email", width: 200 },
                { field: "NPWP", title: "NPWP", width: 200 },
                { field: "CounterpartTypeDesc", title: "Counterpart Type", width: 200 },
                { field: "Description", title: "Description", width: 200 },
                { field: "BitSuspended", title: "Suspended", width: 200, template: "#= BitSuspended ? 'Yes' : 'No' #" },
                { field: "BitRegistered", title: "Registered", width: 200, template: "#= BitRegistered ? 'Yes' : 'No' #" },
                { field: "CBestAccount", title: "CBest Account", width: 200 },
                { field: "MarketID", title: "Market ID", width: 200 },
                { field: "TDays", title: "T Days", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 200 },
                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                { field: "BankAccountID", title: "Bank Acc. ID", width: 200 },
                { field: "BankAccountOffice", title: "Bank Office", width: 200 },
                { field: "BankAccountNo", title: "Bank No", width: 200 },
                { field: "BankAccountRecipient", title: "Bank Recipient", width: 200 },
                { field: "RoundingModeDesc", title: "Rounding Mode", width: 200 },
                { field: "DecimalPlaces", title: "Decimal Places", width: 200 },
                { field: "SInvestCode", title: "S-Invest Code", width: 200 },
                {
                    field: "LevyVATPercent", title: "Levy VAT %", width: 200,
                    template: "#: LevyVATPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "TargetAllocationPercentage", title: "Target Allocation Percentage %", width: 200,
                    template: "#: TargetAllocationPercentage  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "APERDMFeeMethod", title: "APERD MFee Method", width: 200 },
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
        $("#TabCounterpart").kendoTabStrip({
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
                        var CounterpartPendingURL = window.location.origin + "/Radsoft/Counterpart/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(CounterpartPendingURL);
                        $("#gridCounterpartPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Counterpart"
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
                                { field: "CounterpartPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "Address", title: "Address", width: 300 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "Fax", title: "Fax", width: 200 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "CounterpartTypeDesc", title: "Counterpart Type", width: 200 },
                                { field: "Description", title: "Description", width: 200 },
                                { field: "BitSuspended", title: "Suspended", width: 200, template: "#= BitSuspended ? 'Yes' : 'No' #" },
                                { field: "BitRegistered", title: "Registered", width: 200, template: "#= BitRegistered ? 'Yes' : 'No' #" },
                                { field: "CBestAccount", title: "CBest Account", width: 200 },
                                { field: "MarketID", title: "Market ID", width: 200 },
                                { field: "TDays", title: "T Days", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 200 },
                                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                                { field: "BankID", title: "Bank ID", width: 200 },
                                { field: "BankAccountID", title: "Bank Acc. ID", width: 200 },
                                { field: "BankAccountOffice", title: "Bank Office", width: 200 },
                                { field: "BankAccountNo", title: "Bank No", width: 200 },
                                { field: "BankAccountRecipient", title: "Bank Recipient", width: 200 },
                                { field: "RoundingModeDesc", title: "Rounding Mode", width: 200 },
                                { field: "DecimalPlaces", title: "Decimal Places", width: 200 },
                                { field: "SInvestCode", title: "S-Invest Code", width: 200 },
                                {
                                    field: "LevyVATPercent", title: "Levy VAT %", width: 200,
                                    template: "#: LevyVATPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TargetAllocationPercentage", title: "Target Allocation Percentage %", width: 200,
                                    template: "#: TargetAllocationPercentage  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "APERDMFeeMethod", title: "APERD MFee Method", width: 200 },
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

                        var CounterpartHistoryURL = window.location.origin + "/Radsoft/Counterpart/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(CounterpartHistoryURL);

                        $("#gridCounterpartHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Counterpart"
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
                                { field: "CounterpartPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "Address", title: "Address", width: 300 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "Fax", title: "Fax", width: 200 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "CounterpartTypeDesc", title: "Counterpart Type", width: 200 },
                                { field: "Description", title: "Description", width: 200 },
                                { field: "BitSuspended", title: "Suspended", width: 200, template: "#= BitSuspended ? 'Yes' : 'No' #" },
                                { field: "BitRegistered", title: "Registered", width: 200, template: "#= BitRegistered ? 'Yes' : 'No' #" },
                                { field: "CBestAccount", title: "CBest Account", width: 200 },
                                { field: "MarketID", title: "Market ID", width: 200 },
                                { field: "TDays", title: "T Days", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 200 },
                                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                                { field: "BankAccountID", title: "Bank Acc. ID", width: 200 },
                                { field: "BankAccountOffice", title: "Bank Office", width: 200 },
                                { field: "BankAccountNo", title: "Bank No", width: 200 },
                                { field: "BankAccountRecipient", title: "Bank Recipient", width: 200 },
                                { field: "RoundingModeDesc", title: "Rounding Mode", width: 200 },
                                { field: "DecimalPlaces", title: "Decimal Places", width: 200 },
                                { field: "SInvestCode", title: "S-Invest Code", width: 200 },
                                {
                                    field: "LevyVATPercent", title: "Levy VAT %", width: 200,
                                    template: "#: LevyVATPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TargetAllocationPercentage", title: "Target Allocation Percentage %", width: 200,
                                    template: "#: TargetAllocationPercentage  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "APERDMFeeMethod", title: "APERD MFee Method", width: 200 },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

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
        var grid = $("#gridCounterpartHistory").data("kendoGrid");
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
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    function HideBtnAdd(_status) {
        if (_status == 1) {
            $("#BtnAddCounterpartFee").show();
        }
        else if (_status == 2) {
            $("#BtnAddCounterpartFee").show();
        }
        else if (_status == 3) {
            $("#BtnAddCounterpartFee").hide();
        }
        else if (_status == 0) {
            $("#BtnAddCounterpartFee").hide();
        }
    }


    $("#BtnAddCounterpartFee").click(function () {
        clearDataCounterpartFee();
        GridCounterpartFee();
        if ($("#CounterpartPK").val() == 0 || $("#CounterpartPK").val() == null) {
            alertify.alert("There's no Counterpart");
        } else {
            showAddCounterpartFee($("#CounterpartPK").val());
        }
    });

    function showAddCounterpartFee(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid = $("#gridAddCounterpartFee").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }


        WinAddCounterpartFee.center();
        WinAddCounterpartFee.open();

    }

    function onWinAddCounterpartFeeClose() {
        GlobValidatorCounterpartFee.hideMessages();
        clearDataCounterpartFee();
        $("#FeeType").data("kendoComboBox").value("");
        refresh();
    }

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Counterpart",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Counterpart = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    ContactPerson: $('#ContactPerson').val(),
                                    Address: $('#Address').val(),
                                    Phone: $('#Phone').val(),
                                    Fax: $('#Fax').val(),
                                    Email: $('#Email').val(),
                                    NPWP: $('#NPWP').val(),
                                    CounterpartType: $('#CounterpartType').val(),
                                    Description: $('#Description').val(),
                                    BitSuspended: $('#BitSuspended').val(),
                                    BitRegistered: $('#BitRegistered').val(),
                                    CBestAccount: $('#CBestAccount').val(),
                                    MarketPK: $('#MarketPK').val(),
                                    TDays: $('#TDays').val(),
                                    CashRefPaymentPK: $('#CashRefPaymentPK').val(),
                                    BankPK: $('#BankPK').val(),
                                    BankAccountOffice: $('#BankAccountOffice').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    BankAccountRecipient: $('#BankAccountRecipient').val(),
                                    DecimalPlaces: $('#DecimalPlaces').val(),
                                    SInvestCode: $('#SInvestCode').val(),
                                    RoundingMode: $('#RoundingMode').val(),
                                    LevyVATPercent: $('#LevyVATPercent').val(),
                                    TargetAllocationPercentage: $('#TargetAllocationPercentage').val(),
                                    APERDMFeeMethod: $('#APERDMFeeMethod').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_I",
                                    type: 'POST',
                                    data: JSON.stringify(Counterpart),
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
                                alertify.alert("Data ID Same Not Allow!");
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

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartPK").val() + "/" + $("#HistoryPK").val() + "/" + "Counterpart",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var Counterpart = {
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    ContactPerson: $('#ContactPerson').val(),
                                    Address: $('#Address').val(),
                                    Phone: $('#Phone').val(),
                                    Fax: $('#Fax').val(),
                                    Email: $('#Email').val(),
                                    NPWP: $('#NPWP').val(),
                                    CounterpartType: $('#CounterpartType').val(),
                                    Description: $('#Description').val(),
                                    BitSuspended: $('#BitSuspended').val(),
                                    BitRegistered: $('#BitRegistered').val(),
                                    CBestAccount: $('#CBestAccount').val(),
                                    MarketPK: $('#MarketPK').val(),
                                    TDays: $('#TDays').val(),
                                    CashRefPaymentPK: $('#CashRefPaymentPK').val(),
                                    BankPK: $('#BankPK').val(),
                                    BankAccountOffice: $('#BankAccountOffice').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    BankAccountRecipient: $('#BankAccountRecipient').val(),
                                    LevyVATPercent: $('#LevyVATPercent').val(),
                                    DecimalPlaces: $('#DecimalPlaces').val(),
                                    SInvestCode: $('#SInvestCode').val(),
                                    RoundingMode: $('#RoundingMode').val(),
                                    TargetAllocationPercentage: $('#TargetAllocationPercentage').val(),
                                    APERDMFeeMethod: $('#APERDMFeeMethod').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_U",
                                    type: 'POST',
                                    data: JSON.stringify(Counterpart),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartPK").val() + "/" + $("#HistoryPK").val() + "/" + "Counterpart",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Counterpart" + "/" + $("#CounterpartPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartPK").val() + "/" + $("#HistoryPK").val() + "/" + "Counterpart",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                            var Counterpart = {
                                CounterpartPK: $('#CounterpartPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_A",
                                type: 'POST',
                                data: JSON.stringify(Counterpart),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartPK").val() + "/" + $("#HistoryPK").val() + "/" + "Counterpart",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var Counterpart = {
                                CounterpartPK: $('#CounterpartPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_V",
                                type: 'POST',
                                data: JSON.stringify(Counterpart),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartPK").val() + "/" + $("#HistoryPK").val() + "/" + "Counterpart",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var Counterpart = {
                                CounterpartPK: $('#CounterpartPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_R",
                                type: 'POST',
                                data: JSON.stringify(Counterpart),
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


    function clearDataCounterpartFee() {
        //consol.log("A")
        //$("#FeeType").data("kendoComboBox").value("");
        //$("#FundPK").data("kendoComboBox").value("");
        $("#APERDPortionAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#APERDPortionPercent").data("kendoNumericTextBox").value("");
        //$("#Date").val("");
        $("#lblAPERDPortionAmount").hide();
        $("#lblRangeFrom").hide();
        $("#lblRangeTo").hide();
        $("#lblAPERDPortionPercent").hide();
        $("#lblDateAmortize").hide();
        $("#DateAmortization").val("");
        $("#BitMaxRangeTo").prop('checked', false);
        $("#BitActDivDays").prop('checked', true);
        $("#RangeTo").attr('readonly', false);

    }


    $("#BtnSaveCounterpartFee").click(function () { 
        checkmaxrange($('#BitMaxRangeTo').is(":checked"));
        RequiredAttributes($('#FeeType').val());
        var val = validateDataCounterpartFee();


        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                var CounterpartFeeSetup = {
                    CounterpartPK: $('#CounterpartPK').val(),
                    Date: $('#ValueDate').val(),
                    RangeFrom: $('#RangeFrom').val(),
                    RangeTo: $('#RangeTo').val(),
                    EntryUsersID: sessionStorage.getItem("user"),
                    FeeType: $('#FeeType').val()
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Counterpart/AddValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(CounterpartFeeSetup),
                    success: function (data) {
                        if (data == "FALSE") {

                            var Counterpart = {
                                CounterpartPK: $('#CounterpartPK').val(),
                                FeeType: $('#FeeType').val(),
                                Date: $('#ValueDate').val(),
                                DateAmortize: $('#DateAmortize').val(),
                                APERDPortionAmount: $('#APERDPortionAmount').val(),
                                RangeFrom: $('#RangeFrom').val(),
                                RangeTo: $('#RangeTo').val(),
                                APERDPortionPercent: $('#APERDPortionPercent').val(),
                                EntryUsersID: sessionStorage.getItem("user")

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Counterpart/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Counterpart_AddCounterpartFeeSetup",
                                type: 'POST',
                                data: JSON.stringify(Counterpart),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    WinAddCounterpartFee.close();
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


    function checkmaxrange(_check) {
        if (_check == true) {
            $("#RangeTo").attr("required", false);
        }
        else {
            $("#RangeTo").attr("required", true);
        }
    }

    $("#BtnCancelCounterpartFee").click(function () {

        alertify.confirm("Are you sure want to close Add Counterpart Fee ?",
            function (e) {
                if (e) {
                    WinAddCounterpartFee.close();
                    alertify.alert("Close Add Counterpart Fee");
                }
            });
    });

    function getDataSourceCounterpartFee(_url) {
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
                             CounterpartPK: { type: "number" },
                             Counterpart: { type: "string" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
                             Date: { type: "string" },
                             DateAmortize: { type: "string" },
                             APERDPortionAmount: { type: "number" },
                             CounterpartPK: { type: "number" },
                             CounterpartName: { type: "string" },
                             RangeFrom: { type: "number" },
                             RangeTo: { type: "number" },
                             APERDPortionPercent: { type: "number" },
                         }
                     }
                 }
             });
    }


    function GridCounterpartFee() {       
        ClearDataGrid();
        $("#gridAddCounterpartFee").empty();
        var CounterpartFeeURL = window.location.origin + "/Radsoft/Counterpart/GetDataCounterpartFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#CounterpartPK').val(),
          dataSourceApproved = getDataSourceCounterpartFee(CounterpartFeeURL);

        var gridDetail = $("#gridAddCounterpartFee").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Counterpart Fee"
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
               { field: "CounterpartFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 110, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "CounterpartPK", title: "CounterpartPK", hidden: true, width: 80 },
                { field: "CounterpartName", title: "Counterpart", width: 250 },
                { field: "FeeTypeDesc", title: "Fee Type", width: 130 },
                { field: "RangeFrom", title: "Range From", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangeTo", title: "Range To", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "APERDPortionAmount", title: "APERD Portion Amount", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } }, {
                    field: "APERDPortionPercent", title: "APERD Portion Percent", width: 120,
                    template: "#: APERDPortionPercent  # %",
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
            $("#gridAddCounterpartFee").empty();
        }

        function selectDataPending(e) {


            var grid = $("#gridAddCounterpartFee").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _CounterpartFeeSetupPK = dataItemX.CounterpartFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _CounterpartFeeSetupPK);

        }


        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CounterpartFeeSetup/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CounterpartFeeSetup/" + _a,
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


    $("#BtnRejectCounterpartFee").click(function () {

        alertify.confirm("Are you sure want to Reject This Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Counterpart/RejectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DCounterpartPK").val(),
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


    function RequiredAttributes(_type) {
        ResetRequired();
        if (_type == 1) {
            $("#APERDPortionPercent").attr("required", true);
        }
        else if (_type == 2) {
            $("#RangeFrom").attr("required", true);
            $("#RangeTo").attr("required", true);
            $("#APERDPortionPercent").attr("required", true);

        }
        else if (_type == 3) {
            $("#RangeFrom").attr("required", true);
            $("#RangeTo").attr("required", true);
            $("#APERDPortionAmount").attr("required", true);
        }

        function ResetRequired() {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#DateAmortization").attr("required", false);
            $("#APERDPortionAmount").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);
            $("#APERDPortionPercent").attr("required", false);

        }
    }


});


