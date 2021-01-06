$(document).ready(function () {
    document.title = 'FORM COUNTERPART COMMISSION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCounterpart;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    $("#LblBrokerFeePercent").hide();
    if (_GlobClientCode == '20') {
        $("#LblBrokerFeePercent").hide();
        $("#LblTotalSumFee").show();
    }
    else {
        $("#LblTotalSumFee").hide();
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
    }

    
      

    function initWindow() {
        $("#Date").kendoDatePicker({
            format: "MM/dd/yyyy",
        });
        
        win = $("#WinCounterpartCommission").kendoWindow({
            height: 950,
            title: "Counterpart Commission Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinListCounterpart = $("#WinListCounterpart").kendoWindow({
            height: "520px",
            title: "Counterpart List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCounterpartClose
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

        $("#TotalSumFee").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,

        });
        $("#TotalSumFee").data("kendoNumericTextBox").enable(false);
    }



    var GlobValidator = $("#WinCounterpartCommission").kendoValidator().data("kendoValidator");

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
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
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

            $("#TotalSumFee").data("kendoNumericTextBox").value(dataItemX.TotalSumFee);
            $("#CounterpartCommissionPK").val(dataItemX.CounterpartCommissionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
            $("#BitIncludeTax").prop('checked', dataItemX.BitIncludeTax);
            $("#BitNoCapitalGainTax").prop('checked', dataItemX.BitNoCapitalGainTax);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
            ShowHideBrokerFee(dataItemX.BitIncludeTax);
        }



        $("#BitIncludeTax").change(function () {
            if (this.checked) {
                ClearAttrTax();
                if (_GlobClientCode == '20') {
                    $("#LblBrokerFeePercent").hide();
                }
                else
                    $('#LblBrokerFeePercent').show();
                //$("#CommissionPercent").data("kendoNumericTextBox").enable(false);
                //$("#LevyPercent").data("kendoNumericTextBox").enable(false);
                //$("#KPEIPercent").data("kendoNumericTextBox").enable(false);
                //$("#VATPercent").data("kendoNumericTextBox").enable(false);
                //$("#WHTPercent").data("kendoNumericTextBox").enable(false);

            }
            else {
                ClearAttrTax();
                $('#LblBrokerFeePercent').hide();
                $("#CommissionPercent").data("kendoNumericTextBox").enable(true);
                $("#LevyPercent").data("kendoNumericTextBox").enable(true);
                $("#KPEIPercent").data("kendoNumericTextBox").enable(true);
                $("#VATPercent").data("kendoNumericTextBox").enable(true);
                $("#WHTPercent").data("kendoNumericTextBox").enable(true);
            }
        });





        //combo box FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
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
                    return "ALL";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }


        //Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BoardType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BoardType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBoardType,
                    value: setCmbBoardType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBoardType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBoardType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BoardType == 0) {
                    return "";
                } else {
                    return dataItemX.BoardType;
                }
            }
        }

        $("#BrokerFeePercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            change: onchangeBrokerFeePercent,
            value: setBrokerFeePercent(),
        });

        function setBrokerFeePercent() {
            if (e == null) {
                return "";
            } else {
                if (_GlobClientCode == '20') {
                    return dataItemX.BrokerFeePercent;
                }
                else {
                    if (dataItemX.BitIncludeTax == false) {
                        return "";
                    } else {
                        return dataItemX.BrokerFeePercent;
                    }
                }


            }
        }


        function onchangeBrokerFeePercent() {
            getCommissionIncludeTax();
        }


        $("#CommissionPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setCommissionPercent(),
            change: onchangeCommissionPercent,
        });
        function setCommissionPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CommissionPercent == 0) {
                    return "";
                } else {
                    return dataItemX.CommissionPercent;
                }
            }
        }
        function onchangeCommissionPercent() {
            recalTotalSumFee();
        }

        $("#LevyPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setLevyPercent(),
            change: onchangeLevyPercent,
        });
        function setLevyPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.LevyPercent == 0) {
                    return "";
                } else {
                    return dataItemX.LevyPercent;
                }
            }
        }
        function onchangeLevyPercent() {
            recalTotalSumFee();
        }

        $("#KPEIPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setKPEIPercent(),
            change: onchangeKPEIPercent,
        });
        function setKPEIPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.KPEIPercent == 0) {
                    return "";
                } else {
                    return dataItemX.KPEIPercent;
                }
            }
        }
        function onchangeKPEIPercent() {
            recalTotalSumFee();
        }

        $("#VATPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setVATPercent(),
            change: onchangeVATPercent,
        });
        function setVATPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.VATPercent == 0) {
                    return "";
                } else {
                    return dataItemX.VATPercent;
                }
            }
        }
        function onchangeVATPercent() {
            recalTotalSumFee();
        }


        $("#WHTPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setWHTPercent(),
            change: onchangeWHTPercent,
        });
        function setWHTPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTPercent == 0) {
                    return "";
                } else {
                    return dataItemX.WHTPercent;
                }
            }
        }
        function onchangeWHTPercent() {
            recalTotalSumFee();
        }

        $("#OTCPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setOTCPercent(),
            change: onchangeOTCPercent,
        });
        function setOTCPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OTCPercent == 0) {
                    return "";
                } else {
                    return dataItemX.OTCPercent;
                }
            }
        }
        function onchangeOTCPercent() {
            recalTotalSumFee();
        }



        $("#IncomeTaxSellPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setIncomeTaxSellPercent(),

        });
        function setIncomeTaxSellPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxSellPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxSellPercent;
                }
            }
        }


        $("#IncomeTaxInterestPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setIncomeTaxInterestPercent(),

        });
        function setIncomeTaxInterestPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxInterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxInterestPercent;
                }
            }
        }


        $("#IncomeTaxGainPercent").kendoNumericTextBox({
            format: "##.############ \\%",
            decimals: 12,
            value: setIncomeTaxGainPercent(),

        });
        function setIncomeTaxGainPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxGainPercent == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxGainPercent;
                }
            }
        }

        $("#OTCAmount").kendoNumericTextBox({
            format: "##.##",
            decimals: 2,
            value: setOTCAmount(),

        });
        function setOTCAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OTCAmount == 0) {
                    return "";
                } else {
                    return dataItemX.OTCAmount;
                }
            }
        }

        //combo box Rounding Mode
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RoundingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#RoundingCommission").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingCommission,
                    dataSource: data,
                    value: setCmbRoundingCommission()
                });
                $("#RoundingLevy").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingLevy,
                    dataSource: data,
                    value: setCmbRoundingLevy()
                });
                $("#RoundingKPEI").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingKPEI,
                    dataSource: data,
                    value: setCmbRoundingKPEI()
                });
                $("#RoundingVAT").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingVAT,
                    dataSource: data,
                    value: setCmbRoundingVAT()
                });
                $("#RoundingWHT").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingWHT,
                    dataSource: data,
                    value: setCmbRoundingWHT()
                });
                $("#RoundingOTC").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingOTC,
                    dataSource: data,
                    value: setCmbRoundingOTC()
                });
                $("#RoundingTaxSell").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingTaxSell,
                    dataSource: data,
                    value: setCmbRoundingTaxSell()
                });
                $("#RoundingTaxInterest").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingTaxInterest,
                    dataSource: data,
                    value: setCmbRoundingTaxInterest()
                });
                $("#RoundingTaxGain").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingTaxGain,
                    dataSource: data,
                    value: setCmbRoundingTaxGain()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeRoundingCommission() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingCommission() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingCommission == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingCommission;
                }
            }
        }

        function onChangeRoundingLevy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingLevy() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingLevy == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingLevy;
                }
            }
        }


        function onChangeRoundingKPEI() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingKPEI() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingKPEI == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingKPEI;
                }
            }
        }


        function onChangeRoundingVAT() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingVAT() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingVAT == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingVAT;
                }
            }
        }



        function onChangeRoundingWHT() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingWHT() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingWHT == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingWHT;
                }
            }
        }

        function onChangeRoundingOTC() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingOTC() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingOTC == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingOTC;
                }
            }
        }

        function onChangeRoundingTaxSell() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingTaxSell() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingTaxSell == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingTaxSell;
                }
            }
        }


        function onChangeRoundingTaxInterest() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingTaxInterest() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingTaxInterest == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingTaxInterest;
                }
            }
        }


        function onChangeRoundingTaxGain() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRoundingTaxGain() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.RoundingTaxGain == 0) {
                    return "";
                } else {
                    return dataItemX.RoundingTaxGain;
                }
            }
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DecimalPlaces",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DecimalCommission").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalCommission,
                    dataSource: data,
                    value: setCmbDecimalCommission()
                });

                $("#DecimalLevy").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalLevy,
                    dataSource: data,
                    value: setCmbDecimalLevy()
                });
                $("#DecimalKPEI").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalKPEI,
                    dataSource: data,
                    value: setCmbDecimalKPEI()
                });

                $("#DecimalVAT").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalVAT,
                    dataSource: data,
                    value: setCmbDecimalVAT()
                });
                $("#DecimalWHT").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalWHT,
                    dataSource: data,
                    value: setCmbDecimalWHT()
                });

                $("#DecimalOTC").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalOTC,
                    dataSource: data,
                    value: setCmbDecimalOTC()
                });
                $("#DecimalTaxSell").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalTaxSell,
                    dataSource: data,
                    value: setCmbDecimalTaxSell()
                });

                $("#DecimalTaxInterest").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalTaxInterest,
                    dataSource: data,
                    value: setCmbDecimalTaxInterest()
                });
                $("#DecimalTaxGain").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalTaxGain,
                    dataSource: data,
                    value: setCmbDecimalTaxGain()
                });

                $("#DecimalLevy").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalLevy,
                    dataSource: data,
                    value: setCmbDecimalLevy()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });





        function onChangeDecimalCommission() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalCommission() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalCommission == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalCommission;
                }
            }
        }





        function onChangeDecimalLevy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalLevy() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalLevy == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalLevy;
                }
            }
        }




        function onChangeDecimalKPEI() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalKPEI() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalKPEI == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalKPEI;
                }
            }
        }





        function onChangeDecimalVAT() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalVAT() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalVAT == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalVAT;
                }
            }
        }





        function onChangeDecimalWHT() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalWHT() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalWHT == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalWHT;
                }
            }
        }





        function onChangeDecimalOTC() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalOTC() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalOTC == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalOTC;
                }
            }
        }






        function onChangeDecimalTaxSell() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalTaxSell() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalTaxSell == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalTaxSell;
                }
            }
        }







        function onChangeDecimalTaxInterest() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalTaxInterest() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalTaxInterest == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalTaxInterest;
                }
            }
        }






        function onChangeDecimalTaxGain() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDecimalTaxGain() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.DecimalTaxGain == 0) {
                    return 0;
                } else {
                    return dataItemX.DecimalTaxGain;
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
        $("#CounterpartCommissionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#BoardType").val("");
        $("#CommissionPercent").val("0");
        $("#BitIncludeTax").prop('checked', false);
        $("#BrokerFeePercent").val("0");
        $("#LevyPercent").val("0");
        $("#KPEIPercent").val("0");
        $("#VATPercent").val("0");
        $("#WHTPercent").val("0");
        $("#OTCPercent").val("0");
        $("#OTCAmount").val("0");
        $("#IncomeTaxSellPercent").val("0");
        $("#IncomeTaxInterestPercent").val("0");
        $("#IncomeTaxGainPercent").val("0");
        $("#FundPK").val("");
        $("#RoundingCommission").val("0");
        $("#DecimalCommission").val("0");
        $("#RoundingLevy").val("0");
        $("#DecimalLevy").val("0");
        $("#RoundingKPEI").val("0");
        $("#DecimalKPEI").val("0");
        $("#RoundingVAT").val("");
        $("#DecimalVAT").val("");
        $("#RoundingWHT").val("0");
        $("#DecimalWHT").val("0");
        $("#RoundingOTC").val("0");
        $("#DecimalOTC").val("0");
        $("#RoundingTaxSell").val("0");
        $("#DecimalTaxSell").val("0");
        $("#RoundingTaxInterest").val("0");
        $("#DecimalTaxInterest").val("0");
        $("#RoundingTaxGain").val("0");
        $("#DecimalTaxGain").val("0");
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
                             CounterpartCommissionPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             BoardType: { type: "number" },
                             BoardTypeDesc: { type: "string" },
                             CommissionPercent: { type: "number" },
                             BitIncludeTax: { type: "boolean" },
                             LevyPercent: { type: "number" },
                             KPEIPercent: { type: "number" },
                             VATPercent: { type: "number" },
                             WHTPercent: { type: "number" },
                             OTCPercent: { type: "number" },
                             TotalSumFee: { type: "number" },
                             IncomeTaxSellPercent: { type: "number" },
                             IncomeTaxInterestPercent: { type: "number" },
                             IncomeTaxGainPercent: { type: "number" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             RoundingCommission: { type: "number" },
                             RoundingCommissionDesc: { type: "string" },
                             DecimalCommission: { type: "number" },
                             DecimalCommissionDesc: { type: "number" },
                             RoundingLevy: { type: "number" },
                             RoundingLevyDesc: { type: "string" },
                             DecimalLevy: { type: "number" },
                             RoundingKPEI: { type: "number" },
                             RoundingKPEIDesc: { type: "string" },
                             DecimalKPEI: { type: "number" },
                             RoundingVAT: { type: "number" },
                             RoundingVATDesc: { type: "string" },
                             DecimalVAT: { type: "number" },
                             RoundingWHT: { type: "string" },
                             DecimalWHT: { type: "number" },
                             RoundingOTC: { type: "string" },
                             DecimalOTC: { type: "number" },
                             RoundingTaxSell: { type: "string" },
                             DecimalTaxSell: { type: "number" },
                             RoundingTaxInterest: { type: "number" },
                             DecimalTaxInterest: { type: "number" },
                             RoundingTaxGain: { type: "number" },
                             DecimalTaxGain: { type: "number" },
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
            var gridApproved = $("#gridCounterpartCommissionApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCounterpartCommissionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCounterpartCommissionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CounterpartCommissionApprovedURL = window.location.origin + "/Radsoft/CounterpartCommission/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(CounterpartCommissionApprovedURL);

        $("#gridCounterpartCommissionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Counterpart Commission"
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
                { field: "CounterpartCommissionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "BoardTypeDesc", title: "Board Type", width: 200 },
                { field: "BitIncludeTax", title: "BitIncludeTax", width: 200, template: "#= BitIncludeTax ? 'Yes' : 'No' #" },
                { field: "BitNoCapitalGainTax", title: "Bit No Capital Gain Tax", width: 200, template: "#= BitNoCapitalGainTax ? 'Yes' : 'No' #" },
                {
                    field: "BrokerFeePercent", title: "Broker Fee %", width: 200,
                    template: "#: BrokerFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "CommissionPercent", title: "Commission %", width: 200,
                    template: "#: CommissionPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "LevyPercent", title: "Levy %", width: 200,
                    template: "#: LevyPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "KPEIPercent", title: "KPEI %", width: 200,
                    template: "#: KPEIPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "VATPercent", title: "VAT %", width: 200,
                    template: "#: VATPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WHTPercent", title: "WHT %", width: 200,
                    template: "#: WHTPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "OTCPercent", title: "OTC %", width: 200,
                    template: "#: OTCPercent  # %",
                    attributes: { style: "text-align:right;" }
                },

                 {
                     field: "TotalSumFee", title: "Total (sum) Fee", width: 200,
                     template: "#: TotalSumFee  # %",
                     attributes: { style: "text-align:right;" }
                 },
                {
                    field: "IncomeTaxSellPercent", title: "Income Tax Sell %", width: 200,
                    template: "#: IncomeTaxSellPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "IncomeTaxInterestPercent", title: "Income Tax Interest %", width: 200,
                    template: "#: IncomeTaxInterestPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "IncomeTaxGainPercent", title: "Income Tax Gain %", width: 200,
                    template: "#: IncomeTaxGainPercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "RoundingCommissionDesc", title: "RoundingCommission", width: 300 },
                { field: "DecimalCommission", title: "DecimalCommission", width: 300 },

                { field: "RoundingLevyDesc", title: "RoundingLevy", width: 300 },
                { field: "DecimalLevy", title: "DecimalLevy", width: 300 },
                { field: "RoundingKPEIDesc", title: "RoundingKPEI", width: 300 },

                { field: "DecimalKPEI", title: "DecimalKPEI", width: 300 },
                { field: "RoundingVATDesc", title: "RoundingVAT", width: 300 },
                { field: "DecimalVAT", title: "DecimalVAT", width: 300 },

                { field: "RoundingWHTDesc", title: "RoundingWHT", width: 300 },
                { field: "DecimalWHT", title: "DecimalWHT", width: 300 },
                { field: "RoundingOTCDesc", title: "RoundingOTC", width: 300 },

                { field: "DecimalOTC", title: "DecimalOTC", width: 300 },
                { field: "RoundingTaxSellDesc", title: "RoundingTaxSell", width: 300 },
                { field: "DecimalTaxSell", title: "DecimalTaxSell", width: 300 },

                { field: "RoundingTaxInterestDesc", title: "RoundingTaxInterest", width: 300 },
                { field: "DecimalTaxInterest", title: "DecimalTaxInterest", width: 300 },
                { field: "RoundingTaxGainDesc", title: "RoundingTaxGain", width: 300 },

                { field: "DecimalTaxGain", title: "DecimalTaxGain", width: 300 },
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
        $("#TabCounterpartCommission").kendoTabStrip({
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
                        var CounterpartCommissionPendingURL = window.location.origin + "/Radsoft/CounterpartCommission/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(CounterpartCommissionPendingURL);
                        $("#gridCounterpartCommissionPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Counterpart Commission"
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
                                { field: "CounterpartCommissionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                                { field: "BoardTypeDesc", title: "Board Type", width: 200 },
                                { field: "BitIncludeTax", title: "BitIncludeTax", width: 200, template: "#= BitIncludeTax ? 'Yes' : 'No' #" },
                                { field: "BitNoCapitalGainTax", title: "Bit No Capital Gain Tax", width: 200, template: "#= BitNoCapitalGainTax ? 'Yes' : 'No' #" },
                                {
                                    field: "BrokerFeePercent", title: "Broker Fee %", width: 200,
                                    template: "#: BrokerFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "CommissionPercent", title: "Commission %", width: 200,
                                    template: "#: CommissionPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "LevyPercent", title: "Levy %", width: 200,
                                    template: "#: LevyPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "KPEIPercent", title: "KPEI %", width: 200,
                                    template: "#: KPEIPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "VATPercent", title: "VAT %", width: 200,
                                    template: "#: VATPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WHTPercent", title: "WHT %", width: 200,
                                    template: "#: WHTPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OTCPercent", title: "OTC %", width: 200,
                                    template: "#: OTCPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                 {
                                     field: "TotalSumFee", title: "Total (sum) Fee", width: 200,
                                     template: "#: TotalSumFee  # %",
                                     attributes: { style: "text-align:right;" }
                                 },
                                {
                                    field: "IncomeTaxSellPercent", title: "Income Tax Sell %", width: 200,
                                    template: "#: IncomeTaxSellPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "IncomeTaxInterestPercent", title: "Income Tax Interest %", width: 200,
                                    template: "#: IncomeTaxInterestPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "IncomeTaxGainPercent", title: "Income Tax Gain %", width: 200,
                                    template: "#: IncomeTaxGainPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "RoundingCommissionDesc", title: "RoundingCommission", width: 300 },
                                { field: "DecimalCommission", title: "DecimalCommission", width: 300 },

                                { field: "RoundingLevyDesc", title: "RoundingLevy", width: 300 },
                                { field: "DecimalLevy", title: "DecimalLevy", width: 300 },
                                { field: "RoundingKPEIDesc", title: "RoundingKPEI", width: 300 },

                                { field: "DecimalKPEI", title: "DecimalKPEI", width: 300 },
                                { field: "RoundingVATDesc", title: "RoundingVAT", width: 300 },
                                { field: "DecimalVAT", title: "DecimalVAT", width: 300 },

                                { field: "RoundingWHTDesc", title: "RoundingWHT", width: 300 },
                                { field: "DecimalWHT", title: "DecimalWHT", width: 300 },
                                { field: "RoundingOTCDesc", title: "RoundingOTC", width: 300 },

                                { field: "DecimalOTC", title: "DecimalOTC", width: 300 },
                                { field: "RoundingTaxSellDesc", title: "RoundingTaxSell", width: 300 },
                                { field: "DecimalTaxSell", title: "DecimalTaxSell", width: 300 },

                                { field: "RoundingTaxInterestDesc", title: "RoundingTaxInterest", width: 300 },
                                { field: "DecimalTaxInterest", title: "DecimalTaxInterest", width: 300 },
                                { field: "RoundingTaxGainDesc", title: "RoundingTaxGain", width: 300 },

                                { field: "DecimalTaxGain", title: "DecimalTaxGain", width: 300 },
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

                        var CounterpartCommissionHistoryURL = window.location.origin + "/Radsoft/CounterpartCommission/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(CounterpartCommissionHistoryURL);

                        $("#gridCounterpartCommissionHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CounterpartCommission"
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
                                { field: "CounterpartCommissionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                                { field: "BoardTypeDesc", title: "Board Type", width: 200 },
                                { field: "BitIncludeTax", title: "BitIncludeTax", width: 200, template: "#= BitIncludeTax ? 'Yes' : 'No' #" },
                                { field: "BitNoCapitalGainTax", title: "Bit No Capital Gain Tax", width: 200, template: "#= BitNoCapitalGainTax ? 'Yes' : 'No' #" },
                                {
                                    field: "BrokerFeePercent", title: "Broker Fee %", width: 200,
                                    template: "#: BrokerFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },

                                {
                                    field: "CommissionPercent", title: "Commission %", width: 200,
                                    template: "#: CommissionPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "LevyPercent", title: "Levy %", width: 200,
                                    template: "#: LevyPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "KPEIPercent", title: "KPEI %", width: 200,
                                    template: "#: KPEIPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "VATPercent", title: "VAT %", width: 200,
                                    template: "#: VATPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WHTPercent", title: "WHT %", width: 200,
                                    template: "#: WHTPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "OTCPercent", title: "OTC %", width: 200,
                                    template: "#: OTCPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                 {
                                     field: "TotalSumFee", title: "Total (sum) Fee", width: 200,
                                     template: "#: TotalSumFee  # %",
                                     attributes: { style: "text-align:right;" }
                                 },
                                {
                                    field: "IncomeTaxSellPercent", title: "Income Tax Sell %", width: 200,
                                    template: "#: IncomeTaxSellPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "IncomeTaxInterestPercent", title: "Income Tax Interest %", width: 200,
                                    template: "#: IncomeTaxInterestPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "IncomeTaxGainPercent", title: "Income Tax Gain %", width: 200,
                                    template: "#: IncomeTaxGainPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "RoundingCommissionDesc", title: "RoundingCommission", width: 300 },
                                { field: "DecimalCommission", title: "DecimalCommission", width: 300 },

                                { field: "RoundingLevyDesc", title: "RoundingLevy", width: 300 },
                                { field: "DecimalLevy", title: "DecimalLevy", width: 300 },
                                { field: "RoundingKPEIDesc", title: "RoundingKPEI", width: 300 },

                                { field: "DecimalKPEI", title: "DecimalKPEI", width: 300 },
                                { field: "RoundingVATDesc", title: "RoundingVAT", width: 300 },
                                { field: "DecimalVATD", title: "DecimalVAT", width: 300 },

                                { field: "RoundingWHTDesc", title: "RoundingWHT", width: 300 },
                                { field: "DecimalWHT", title: "DecimalWHT", width: 300 },
                                { field: "RoundingOTCDesc", title: "RoundingOTC", width: 300 },

                                { field: "DecimalOTC", title: "DecimalOTC", width: 300 },
                                { field: "RoundingTaxSellDesc", title: "RoundingTaxSell", width: 300 },
                                { field: "DecimalTaxSell", title: "DecimalTaxSell", width: 300 },

                                { field: "RoundingTaxInterestDesc", title: "RoundingTaxInterest", width: 300 },
                                { field: "DecimalTaxInterest", title: "DecimalTaxInterest", width: 300 },
                                { field: "RoundingTaxGainDesc", title: "RoundingTaxGain", width: 300 },

                                { field: "DecimalTaxGain", title: "DecimalTaxGain", width: 300 },
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
        var grid = $("#gridCounterpartCommissionHistory").data("kendoGrid");
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

    
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    var CounterpartCommission = {
                        Date: $('#Date').val(),
                        CounterpartPK: $('#CounterpartPK').val(),
                        BoardType: $('#BoardType').val(),
                        BitIncludeTax: $('#BitIncludeTax').is(":checked"),
                        BitNoCapitalGainTax: $('#BitNoCapitalGainTax').is(":checked"),
                        CommissionPercent: $('#CommissionPercent').val(),
                        LevyPercent: $('#LevyPercent').val(),
                        KPEIPercent: $('#KPEIPercent').val(),
                        VATPercent: $('#VATPercent').val(),
                        WHTPercent: $('#WHTPercent').val(),
                        OTCPercent: $('#OTCPercent').val(),
                        OTCAmount: $('#OTCAmount').val(),
                        IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                        IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                        IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                        FundPK: $('#FundPK').val(),
                        RoundingCommission: $('#RoundingCommission').val(),
                        DecimalCommission: $('#DecimalCommission').val(),
                        RoundingLevy: $('#RoundingLevy').val(),
                        DecimalLevy: $('#DecimalLevy').val(),
                        RoundingKPEI: $('#RoundingKPEI').val(),
                        DecimalKPEI: $('#DecimalKPEI').val(),
                        RoundingVAT: $('#RoundingVAT').val(),
                        DecimalVAT: $('#DecimalVAT').val(),
                        RoundingWHT: $('#RoundingWHT').val(),
                        DecimalWHT: $('#DecimalWHT').val(),
                        RoundingOTC: $('#RoundingOTC').val(),
                        DecimalOTC: $('#DecimalOTC').val(),
                        RoundingTaxSell: $('#RoundingTaxSell').val(),
                        DecimalTaxSell: $('#DecimalTaxSell').val(),
                        RoundingTaxInterest: $('#RoundingTaxInterest').val(),
                        DecimalTaxInterest: $('#DecimalTaxInterest').val(),
                        RoundingTaxGain: $('#RoundingTaxGain').val(),
                        DecimalTaxGain: $('#DecimalTaxGain').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CounterpartCommission/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CounterpartCommission_I",
                        type: 'POST',
                        data: JSON.stringify(CounterpartCommission),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartCommissionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CounterpartCommission",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var CounterpartCommission = {
                                    Date: $('#Date').val(),
                                    CounterpartCommissionPK: $('#CounterpartCommissionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    BoardType: $('#BoardType').val(),
                                    BitIncludeTax: $('#BitIncludeTax').is(":checked"),
                                    BitNoCapitalGainTax: $('#BitNoCapitalGainTax').is(":checked"),
                                    CommissionPercent: $('#CommissionPercent').val(),
                                    LevyPercent: $('#LevyPercent').val(),
                                    KPEIPercent: $('#KPEIPercent').val(),
                                    VATPercent: $('#VATPercent').val(),
                                    WHTPercent: $('#WHTPercent').val(),
                                    OTCPercent: $('#OTCPercent').val(),
                                    OTCAmount: $('#OTCAmount').val(),
                                    IncomeTaxSellPercent: $('#IncomeTaxSellPercent').val(),
                                    IncomeTaxInterestPercent: $('#IncomeTaxInterestPercent').val(),
                                    IncomeTaxGainPercent: $('#IncomeTaxGainPercent').val(),
                                    FundPK: $('#FundPK').val(),
                                    RoundingCommission: $('#RoundingCommission').val(),
                                    DecimalCommission: $('#DecimalCommission').val(),
                                    RoundingLevy: $('#RoundingLevy').val(),
                                    DecimalLevy: $('#DecimalLevy').val(),
                                    RoundingKPEI: $('#RoundingKPEI').val(),
                                    DecimalKPEI: $('#DecimalKPEI').val(),
                                    RoundingVAT: $('#RoundingVAT').val(),
                                    DecimalVAT: $('#DecimalVAT').val(),
                                    RoundingWHT: $('#RoundingWHT').val(),
                                    DecimalWHT: $('#DecimalWHT').val(),
                                    RoundingOTC: $('#RoundingOTC').val(),
                                    DecimalOTC: $('#DecimalOTC').val(),
                                    RoundingTaxSell: $('#RoundingTaxSell').val(),
                                    DecimalTaxSell: $('#DecimalTaxSell').val(),
                                    RoundingTaxInterest: $('#RoundingTaxInterest').val(),
                                    DecimalTaxInterest: $('#DecimalTaxInterest').val(),
                                    RoundingTaxGain: $('#RoundingTaxGain').val(),
                                    DecimalTaxGain: $('#DecimalTaxGain').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CounterpartCommission/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CounterpartCommission_U",
                                    type: 'POST',
                                    data: JSON.stringify(CounterpartCommission),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartCommissionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CounterpartCommission",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CounterpartCommission" + "/" + $("#CounterpartCommissionPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartCommissionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CounterpartCommission",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                            var CounterpartCommission = {
                                CounterpartCommissionPK: $('#CounterpartCommissionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CounterpartCommission/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CounterpartCommission_A",
                                type: 'POST',
                                data: JSON.stringify(CounterpartCommission),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartCommissionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CounterpartCommission",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var CounterpartCommission = {
                                CounterpartCommissionPK: $('#CounterpartCommissionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CounterpartCommission/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CounterpartCommission_V",
                                type: 'POST',
                                data: JSON.stringify(CounterpartCommission),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CounterpartCommissionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CounterpartCommission",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var CounterpartCommission = {
                                CounterpartCommissionPK: $('#CounterpartCommissionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CounterpartCommission/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CounterpartCommission_R",
                                type: 'POST',
                                data: JSON.stringify(CounterpartCommission),
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


    $("#btnListCounterpartPK").click(function () {
        initListCounterpartPK();

        WinListCounterpart.center();
        WinListCounterpart.open();
        htmlCounterpartPK = "#CounterpartPK";
        htmlCounterpartID = "#CounterpartID";


    });
    function getDataSourceListCounterpart() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 CounterpartPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListCounterpartPK() {
        var dsListCounterpart = getDataSourceListCounterpart();
        $("#gridListCounterpart").kendoGrid({
            dataSource: dsListCounterpart,
            height: "90%",
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
               { command: { text: "Select", click: ListCounterpartSelect }, title: " ", width: 100 },
               { field: "CounterpartPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 100 },
               //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }
    function onWinListCounterpartClose() {
        $("#gridListCounterpart").empty();
    }
    function ListCounterpartSelect(e) {
        var grid = $("#gridListCounterpart").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCounterpartName).val(dataItemX.Name);
        $(htmlCounterpartID).val(dataItemX.ID);
        $(htmlCounterpartPK).val(dataItemX.CounterpartPK);


        WinListCounterpart.close();


    }
    function getCommissionIncludeTax() {
        $.ajax({
            url: window.location.origin + "/Radsoft/CounterpartCommission/GetCommissionIncludeTax/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#BrokerFeePercent").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CommissionPercent").data("kendoNumericTextBox").value(data.CommissionPercent);
                $("#LevyPercent").data("kendoNumericTextBox").value(data.LevyPercent);
                $("#KPEIPercent").data("kendoNumericTextBox").value(data.KPEIPercent);
                $("#VATPercent").data("kendoNumericTextBox").value(data.VATPercent);
                $("#WHTPercent").data("kendoNumericTextBox").value(data.WHTPercent);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



    }

    function ClearAttrTax() {
        $("#BrokerFeePercent").data("kendoNumericTextBox").value(0);
        $("#CommissionPercent").data("kendoNumericTextBox").value(0);
        $("#LevyPercent").data("kendoNumericTextBox").value(0);
        $("#KPEIPercent").data("kendoNumericTextBox").value(0);
        $("#VATPercent").data("kendoNumericTextBox").value(0);
        $("#WHTPercent").data("kendoNumericTextBox").value(0);



    }

    function recalTotalSumFee() {
        var _totalSumFee;
        var _CommissionPercent, _LevyPercent, _KPEIPercent, _VATPercent, _WHTPercent, _OTCPercent;

        if ($("#CommissionPercent").val() == "undefined" || $("#CommissionPercent").val() == "")
            _CommissionPercent = 0
        else
            _CommissionPercent = $("#CommissionPercent").val();

        if ($("#LevyPercent").val() == "undefined" || $("#LevyPercent").val() == "")
            _LevyPercent = 0;
        else
            _LevyPercent = $("#LevyPercent").val();

        if ($("#KPEIPercent").val() == "undefined" || $("#KPEIPercent").val() == "")
            _KPEIPercent = 0;
        else
            _KPEIPercent = $("#KPEIPercent").val();

        if ($("#VATPercent").val() == "undefined" || $("#VATPercent").val() == "")
            _VATPercent = 0;
        else
            _VATPercent = $("#VATPercent").val();

        if ($("#WHTPercent").val() == "undefined" || $("#WHTPercent").val() == "")
            _WHTPercent = 0;
        else
            _WHTPercent = $("#WHTPercent").val();

        if ($("#OTCPercent").val() == "undefined" || $("#OTCPercent").val() == "")
            _OTCPercent = 0;
        else
            _OTCPercent = $("#OTCPercent").val();


        _totalSumFee = parseFloat(_CommissionPercent) + parseFloat(_LevyPercent) + parseFloat(_KPEIPercent) + parseFloat(_VATPercent) + parseFloat(_WHTPercent) + parseFloat(_OTCPercent);


        $("#TotalSumFee").data("kendoNumericTextBox").value(_totalSumFee);

    }




    function ShowHideBrokerFee(_bitIncludeTax) {

        if (_GlobClientCode == '20') {
            $("#LblBrokerFeePercent").hide();
        }
        else {
            if (_bitIncludeTax == true) {
                $("#LblBrokerFeePercent").show();
            }

            else {
                $("#LblBrokerFeePercent").hide();
            }
        }
    }

});
