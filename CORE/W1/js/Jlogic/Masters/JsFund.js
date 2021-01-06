$(document).ready(function () {
    document.title = 'FORM FUND';
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

    if (_GlobClientCode == "29") {
        $("#LblBitSyariahFund").show();
    }
    else {
        $("#LblBitSyariahFund").hide();
    }


    if (_GlobClientCode == "17") {
        $("#BtnCheckMatureFund").show();
    }
    else {
        $("#BtnCheckMatureFund").hide();
    }

    if (_GlobClientCode == "32") {
        $("#LblCounterpart").show();
        $("#LblSInvestFee").hide();
        $("#LblInternalClosePrice").hide();
        $("#LblInvestmentHighRisk").hide();
    }
    else {
        $("#LblCounterpart").hide();
        $("#LblSInvestFee").show();
        $("#LblInternalClosePrice").show();
        $("#LblInvestmentHighRisk").show();
    }


    if (_GlobClientCode == "20") {
        $("#EntryApproveTimeCutoff").attr("required", true);
    }
    else {
        $("#EntryApproveTimeCutoff").attr("required", false);
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

        $("#BtnCheckMatureFund").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function initWindow() {
        $("#EffectiveDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });
        $("#MaturityDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: '12/31/2099'
        });
        $("#IssueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        $("#DividenDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });


        $("#KPDDateFromContract").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        $("#KPDDateToContract").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        $("#KPDDateAdendum").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        win = $("#WinFund").kendoWindow({
            height: 900,
            title: "Fund Detail",
            visible: false,
            width: 1250,
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
            },

        }).data("kendoWindow");

        WinCheckMatureFund = $("#WinCheckMatureFund").kendoWindow({
            height: 500,
            title: "* Check Mature Fund",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinMatureFundClose,
            modal: true,
        }).data("kendoWindow");


        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateTo
        });


        function OnChangeParamDateTo() {
            var _date = Date.parse($("#ParamDateTo").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            refreshMatureFund();

        }

    }

    if (_GlobClientCode == "05") {
        $("#lblOJKEffectiveStatementLetterDate").show();
    }
    else {
        $("#lblOJKEffectiveStatementLetterDate").hide();
    }

    $("#OJKEffectiveStatementLetterDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        value: null
    });

    var GlobValidator = $("#WinFund").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#ID").val().length > 80) {
            alertify.alert("Validation not Pass, char more than 80 for ID");
            return 0;
        }

        if ($("#Name").val().length > 100) {
            alertify.alert("Validation not Pass, char more than 100 for Name");
            return 0;
        }

        if ($("#NKPDName").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for NKPDName");
            return 0;
        }

        if ($("#SInvestCode").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for SInvestCode");
            return 0;
        }

        if ($("#BloombergCode").val().length > 50) {
            alertify.alert("Validation not Pass, char more than 50 for BloombergCode");
            return 0;
        }
        if ($("#EffectiveDate").val() != "") {
            var _effectiveDate = Date.parse($("#EffectiveDate").data("kendoDatePicker").value());
            //var _maturityDate = Date.parse($("#MaturityDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_effectiveDate) {
                
                alertify.success("Wrong Format Date");
                return 0;
            }
        }
        if (_GlobClientCode == "20") {
            if ($("#EntryApproveTimeCutoff").val().length >= 7) {
                alertify.alert("Validation not Pass, char more than 6 for EntryApproveTimeCutoff");
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
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideLabel();
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                HideLabel();
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

            $("#FundPK").val(dataItemX.FundPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#NKPDName").val(dataItemX.NKPDName);
            $("#SInvestCode").val(dataItemX.SInvestCode);
            $("#BloombergCode").val(dataItemX.BloombergCode);
            $("#IsPublic").prop('checked', dataItemX.IsPublic);
            $("#Nav").val(dataItemX.Nav);
            $("#EffectiveDate").data("kendoDatePicker").value(dataItemX.EffectiveDate);
            if (dataItemX.MaturityDate == '1/1/1900 12:00:00 AM') {
                $("#MaturityDate").val("");
            }
            else {
                $("#MaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
            }
            $("#IssueDate").data("kendoDatePicker").value(dataItemX.IssueDate);

            $("#BitNeedRecon").prop('checked', dataItemX.BitNeedRecon);
            $("#BitSinvestFee").prop('checked', dataItemX.BitSinvestFee);
            $("#BitInternalClosePrice").prop('checked', dataItemX.BitInternalClosePrice);
            $("#BitInvestmentHighRisk").prop('checked', dataItemX.BitInvestmentHighRisk);

            $("#DefaultPaymentDate").val(dataItemX.DefaultPaymentDate);
            $("#DividenDate").data("kendoDatePicker").value(dataItemX.DividenDate);
            $("#NPWP").val(dataItemX.NPWP);
            $("#OJKLetter").val(dataItemX.OJKLetter);

            $("#KPDNoContract").val(dataItemX.KPDNoContract);
            $("#KPDDateFromContract").data("kendoDatePicker").value(dataItemX.KPDDateFromContract);
            $("#KPDDateToContract").data("kendoDatePicker").value(dataItemX.KPDDateToContract);
            $("#KPDNoAdendum").val(dataItemX.KPDNoAdendum);
            $("#KPDDateAdendum").data("kendoDatePicker").value(dataItemX.KPDDateAdendum);
            $("#CutOffTime").val(dataItemX.CutOffTime);

            $("#ProspectusUrl").val(dataItemX.ProspectusUrl);
            $("#FactsheetUrl").val(dataItemX.FactsheetUrl);
            $("#ImageUrl").val(dataItemX.ImageUrl);
            $("#ISIN").val(dataItemX.ISIN);
            $("#EntryApproveTimeCutoff").val(dataItemX.EntryApproveTimeCutoff);
            $("#OJKEffectiveStatementLetterDate").data("kendoDatePicker").value(dataItemX.OJKEffectiveStatementLetterDate);

            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            $("#BitSyariahFund").prop('checked', dataItemX.BitSyariahFund);
        }




        if (_GlobClientCode == '07' || _GlobClientCode == '10' || _GlobClientCode == '14') {
            $("#lblIsPublic").show();
        }
        else {
            $("#lblIsPublic").hide();
        }

        if (_GlobClientCode == '20') {
            $("#lblWHTDueDate").show();
            $("#lblEntryApproveTimeCutoff").show();

        }
        else {
            $("#lblWHTDueDate").hide();
            $("#lblEntryApproveTimeCutoff").hide();
        }

        if (_GlobClientCode == '21') {
            $("#lblEntryApproveTimeCutoff").show();
        }

        $("#WHTDueDate").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Value Date", value: 1 },
                { text: "Settled Date", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeWHTDueDate,
            value: setCmbWHTDueDate()
        });
        function OnChangeWHTDueDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbWHTDueDate() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.WHTDueDate;
            }
        }

        $("#MaxUnits").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMaxUnits()
        });
        function setMaxUnits() {
            if (e == null) {
                return 9000000000;
            } else {
                return dataItemX.MaxUnits;
            }
        }

        $("#TotalUnits").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setTotalUnits()
        });
        function setTotalUnits() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TotalUnits;
            }
        }

        $("#Nav").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNav()
        });
        function setNav() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Nav;
            }
        }

        $("#CustodyFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setCustodyFeePercent()
        });
        function setCustodyFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.CustodyFeePercent;
            }
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

        $("#SubscriptionFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setSubscriptionFeePercent()
        });
        function setSubscriptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SubscriptionFeePercent;
            }
        }

        $("#RedemptionFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setRedemptionFeePercent()
        });
        function setRedemptionFeePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.RedemptionFeePercent;
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



        $("#MinSwitch").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinSwitch()
        });
        function setMinSwitch() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinSwitch;
            }
        }


        $("#MinBalSwitchAmt").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinBalSwitchAmt()
        });
        function setMinBalSwitchAmt() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinBalSwitchAmt;
            }
        }

        $("#MinBalSwitchToAmt").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinBalSwitchToAmt()
        });
        function setMinBalSwitchToAmt() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinBalSwitchToAmt;
            }
        }


        $("#MinBalSwitchUnit").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinBalSwitchUnit()
        });
        function setMinBalSwitchUnit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinBalSwitchUnit;
            }
        }


        $("#MinSubs").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinSubs()
        });
        function setMinSubs() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinSubs;
            }
        }

        $("#MinReds").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinReds()
        });
        function setMinReds() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinReds;
            }
        }

        $("#MinBalRedsAmt").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinBalRedsAmt()
        });
        function setMinBalRedsAmt() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinBalRedsAmt;
            }
        }


        $("#RemainingBalanceUnit").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setRemainingBalanceUnit()
        });
        function setRemainingBalanceUnit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.RemainingBalanceUnit;
            }
        }


        $("#MinBalRedsUnit").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setMinBalRedsUnit()
        });
        function setMinBalRedsUnit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinBalRedsUnit;
            }
        }

        //Currency
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCurrencyPK,
                    value: setCmbCurrencyPK()
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


        //Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
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
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
                }
            }
        }


        //Fund Type Internal
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundTypeInternal",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundTypeInternal").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundTypeInternal,
                    value: setCmbFundTypeInternal()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundTypeInternal() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            GlobFundTypeInternal = this.value();
            ShowHideLabelByGlobFundTypeInternal(GlobFundTypeInternal);
        }

        function setCmbFundTypeInternal() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundTypeInternal == 0) {
                    return 1;
                } else {
                    return dataItemX.FundTypeInternal;
                }
            }
        }
        if (e != null) {
            ShowHideLabelByGlobFundTypeInternal(dataItemX.FundTypeInternal);
        }

        //BankBranchPK
        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboCustodiOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankBranchPK").kendoComboBox({
                    dataValueField: "BankBranchPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankBranchPK,
                    dataSource: data,
                    value: setCmbBankBranchPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankBranchPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBankBranchPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankBranchPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankBranchPK;
                }
            }
        }

        //combo box Rounding Mode
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RoundingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#NavRoundingMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingMode,
                    dataSource: data,
                    value: setCmbRoundingMode()
                });
                $("#UnitRoundingMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingMode,
                    dataSource: data,
                    value: setCmbUnitRoundingMode()
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
                if (dataItemX.NavRoundingMode == 0) {
                    return "";
                } else {
                    return dataItemX.NavRoundingMode;
                }
            }
        }

        function setCmbUnitRoundingMode() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnitRoundingMode == 0) {
                    return "";
                } else {
                    return dataItemX.UnitRoundingMode;
                }
            }
        }


        //combo box Decimal Places
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DecimalPlaces",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#NavDecimalPlaces").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalPlaces,
                    dataSource: data,
                    value: setCmbDecimalPlaces()
                });

                $("#UnitDecimalPlaces").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalPlaces,
                    dataSource: data,
                    value: setCmbUnitDecimalPlaces()
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
                if (dataItemX.NavDecimalPlaces == 0) {
                    return 0;
                } else {
                    return dataItemX.NavDecimalPlaces;
                }
            }
        }

        function setCmbUnitDecimalPlaces() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnitDecimalPlaces == 0) {
                    return 0;
                } else {
                    return dataItemX.UnitDecimalPlaces;
                }
            }
        }

        //Currency
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
                    dataSource: data,
                    change: OnChangeCounterpartPK,
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


        $("#MFeeMethod").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Mark to Market", value: 1 },
                { text: "NAV 1000", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeMFeeMethod,
            value: setCmbMFeeMethod()
        });
        function OnChangeMFeeMethod() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMFeeMethod() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.MFeeMethod;
            }
        }


        $("#DefaultPaymentDate").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setDefaultPaymentDate()
        });
        function setDefaultPaymentDate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DefaultPaymentDate;
            }
        }

        $("#SharingFeeCalculation").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Management Fee Calculation", value: 1 },
                { text: "AUM Calculation", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeSharingFeeCalculation,
            value: setCmbSharingFeeCalculation()
        });
        function OnChangeSharingFeeCalculation() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbSharingFeeCalculation() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.SharingFeeCalculation;
            }
        }


        if (_GlobClientCode == "10") {
            $("#LblCutOffTime").show();
        }
        else {
            $("#LblCutOffTime").hide();
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
        $("#FundPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#NKPDName").val("");
        $("#SInvestCode").val("");
        $("#BloombergCode").val("");
        $("#FundTypeInternal").val("");
        $("#CurrencyPK").val("");
        $("#Type").val("");
        $("#BankBranchPK").val("");
        $("#MaxUnits").val("");
        $("#TotalUnits").val("");
        $("#Nav").val("");
        $("#EffectiveDate").data("kendoDatePicker").value(null);
        $("#MaturityDate").data("kendoDatePicker").value(null);
        $("#CustodyFeePercent").val("");
        $("#ManagementFeePercent").val("");
        $("#SubscriptionFeePercent").val("");
        $("#RedemptionFeePercent").val("");
        $("#SwitchingFeePercent").val("");
        $("#NavRoundingMode").val("");
        $("#NavDecimalPlaces").val("");
        $("#UnitDecimalPlaces").val("");
        $("#MinBalSwitchUnit").val("");
        $("#MinSwitch").val("");
        $("#MinBalSwitchAmt").val("");
        $("#MinBalRedsUnit").val("");
        $("#MinBalRedsAmt").val("");
        $("#MinSubs").val("");
        $("#MinReds").val("");
        $("#UnitDecimalPlaces").val("");
        $("#CounterpartPK").val("");
        $("#WHTDueDate").val("");
        $("#MFeeMethod").val("");
        $("#IssueDate").val("");
        $("#RemainingBalanceUnit").val("");
        $("#DefaultPaymentDate").val("");
        $("#SharingFeeCalculation").val("");
        $("#DividenDate").data("kendoDatePicker").value(null);
        $("#NPWP").val("");
        $("#OJKLetter").val("");
        $("#IsPublic").prop('checked', false);
        $("#BitNeedRecon").prop('checked', false);
        $("#BitSinvestFee").prop('checked', false);


        $("#BitInternalClosePrice").prop('checked', false);
        $("#BitInvestmentHighRisk").prop('checked', false);

        $("#KPDNoContract").val("");
        $("#KPDDateFromContract").data("kendoDatePicker").value(null);
        $("#KPDDateToContract").data("kendoDatePicker").value(null);
        $("#KPDNoAdendum").val("");
        $("#KPDDateAdendum").data("kendoDatePicker").value(null);
        $("#CutOffTime").val("");

        $("#ProspectusUrl").val("");
        $("#FactsheetUrl").val("");
        $("#ImageUrl").val("");
        $("#ISIN").val("");
        $("#EntryApproveTimeCutoff").val("");
        $("#OJKEffectiveStatementLetterDate").data("kendoDatePicker").value(null);

        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");

        $("#BitSyariahFund").prop('checked', false);
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
                            FundPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            NKPDName: { type: "string" },
                            SInvestCode: { type: "string" },
                            BloombergCode: { type: "string" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            DefaultBankSubscriptionPK: { type: "number" },
                            DefaultBankSubscriptionID: { type: "string" },
                            DefaultBankSubscriptionName: { type: "string" },
                            DefaultBankRedemptionPK: { type: "number" },
                            DefaultBankRedemptionID: { type: "string" },
                            DefaultBankRedemptionName: { type: "string" },
                            DefaultFundCashRefPK: { type: "number" },
                            DefaultFundCashRefID: { type: "string" },
                            DefaultFundCashRefName: { type: "string" },
                            BankBranchPK: { type: "number" },
                            BankBranchID: { type: "string" },
                            BankBranchName: { type: "string" },
                            MaxUnits: { type: "number" },
                            TotalUnits: { type: "number" },
                            Nav: { type: "number" },
                            EffectiveDate: { type: "date" },
                            MaturityDate: { type: "date" },
                            CustodyFeePercent: { type: "number" },
                            ManagementFeePercent: { type: "number" },
                            SubscriptionFeePercent: { type: "number" },
                            RedemptionFeePercent: { type: "number" },
                            SwitchingFeePercent: { type: "number" },
                            MinSwitch: { type: "number" },
                            MinBalSwitchAmt: { type: "number" },
                            MinBalSwitchUnit: { type: "number" },
                            MinSubs: { type: "number" },
                            MinReds: { type: "number" },
                            MinBalRedsAmt: { type: "number" },
                            MinBalRedsUnit: { type: "number" },
                            CounterpartPK: { type: "number" },
                            CounterpartName: { type: "string" },
                            NavRoundingMode: { type: "number" },
                            NavRoundingModeDesc: { type: "string" },
                            NavDecimalPlaces: { type: "number" },
                            NavDecimalPlacesDesc: { type: "string" },
                            UnitDecimalPlaces: { type: "number" },
                            UnitDecimalPlacesDesc: { type: "string" },
                            UnitRoundingMode: { type: "number" },
                            UnitRoundingModeDesc: { type: "string" },
                            WHTDueDate: { type: "number" },
                            WHTDueDateDesc: { type: "string" },
                            MFeeMethod: { type: "number" },
                            MFeeMethodDesc: { type: "string" },
                            SharingFeeCalculation: { type: "number" },
                            SharingFeeCalculationDesc: { type: "string" },
                            IssueDate: { type: "date" },
                            RemainingBalanceUnit: { type: "number" },
                            DefaultPaymentDate: { type: "number" },
                            DividenDate: { type: "date" },
                            NPWP: { type: "string" },
                            OJKLetter: { type: "string" },
                            BitNeedRecon: { type: "bool" },
                            BitSinvestFee: { type: "bool" },


                            BitInternalClosePrice: { type: "bool" },
                            BitInvestmentHighRisk: { type: "bool" },

                            KPDNoContract: { type: "string" },
                            KPDDateFromContract: { type: "date" },
                            KPDDateToContract: { type: "date" },
                            KPDNoAdendum: { type: "string" },
                            KPDDateAdendum: { type: "date" },
                            CutOffTime: { type: "string" },

                            ProspectusUrl: { type: "string" },
                            FactsheetUrl: { type: "string" },
                            ImageUrl: { type: "string" },
                            ISIN: { type: "string" },
                            EntryApproveTimeCutoff: { type: "string" },
                            OJKEffectiveStatementLetterDate: { type: "date" },
                            BitSyariahFund: { type: "bool" },

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
            var gridApproved = $("#gridFundApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundApprovedURL = window.location.origin + "/Radsoft/Fund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FundApprovedURL);
        if (_GlobClientCode == "05") {
            $("#gridFundApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund"
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
                    { field: "FundPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ID", title: "ID", width: 200 },
                    { field: "Name", title: "Name", width: 300 },
                    //{ field: "NKPDName", title: "NKPD Name", width: 300 },
                    { field: "SInvestCode", title: "SInvestCode", width: 300 },
                    //{ field: "BloombergCode", title: "BloombergCode", width: 300 },
                    { field: "CurrencyID", title: "Currency ID", width: 150 },
                    //{ field: "CounterpartName", title: "Counterpart Name", width: 150 },
                    { field: "TypeDesc", title: "Type", width: 300 },
                    { field: "NPWP", title: "NPWP", width: 300 },
                    { field: "SID", title: "SID", width: 300 },
                    { field: "KSEINo", title: "KSEI No", width: 300 },
                    { field: "OJKEffectiveStatementLetterDate", title: "OJK Effective Statement Letter Date", width: 200, template: "#= kendo.toString(kendo.parseDate(OJKEffectiveStatementLetterDate), 'MM/dd/yyyy')#" },
                    { field: "BitSinvestFee", title: "Sinvest Fee", width: 150, template: "#= BitSinvestFee ? 'Yes' : 'No' #" },
                    { field: "BitInternalClosePrice", title: "Internal Close Price", width: 150, template: "#= BitInternalClosePrice ? 'Yes' : 'No' #" },
                    //{ field: "MinSwitch", title: "Min Switch", width: 300 },
                    //{ field: "MinBalSwitchAmt", title: "Min Bal Switch Amt", width: 300 },
                    //{ field: "MinBalSwitchUnit", title: "Min Bal Switch Unit", width: 300 },
                    //{ field: "MinSubs", title: "Min Switch", width: 300 },
                    //{ field: "MinReds", title: "Min Bal Switch Amt", width: 300 },
                    //{ field: "MinBalRedsAmt", title: "Min Bal Switch Unit", width: 300 },
                    //{ field: "MinBalRedsUnit", title: "Min Bal Switch Unit", width: 300 },
                    //{ field: "IsPublic", title: "IsPublic", width: 150, template: "#= IsPublic ? 'Yes' : 'No' #" },
                    // { field: "BitNeedRecon", title: "BitNeedRecon", width: 150, template: "#= BitNeedRecon ? 'Yes' : 'No' #" },
                    //{ field: "FundTypeInternalDesc", title: "TypeInternal", width: 300 },
                    //{ field: "DefaultBankSubscriptionID", title: "Def Bank Subscription", width: 250 },
                    //{ field: "DefaultBankSubscriptionName", title: "Name", width: 300 },
                    //{ field: "DefaultBankRedemptionID", title: "Def Bank Redemption", width: 250 },
                    //{ field: "DefaultBankRedemptionName", title: "Name", width: 300 },
                    //{ field: "DefaultFundCashRefID", title: "Def Cash Ref", width: 250 },
                    //{ field: "DefaultFundCashRefName", title: "Name", width: 300 },
                    //{ field: "BankBranchID", title: "Bank Custodian ID", width: 200 },
                    //{ field: "BankBranchName", title: "Name", width: 300 },
                    //{ field: "MaxUnits", title: "Max Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    //{ field: "TotalUnits", title: "Total Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    //{ field: "Nav", title: "NAV", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    //{ field: "EffectiveDate", title: "Effective Date", width: 200, template: "#= kendo.toString(kendo.parseDate(EffectiveDate), 'MM/dd/yyyy')#" },
                    //{ field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    //{ field: "IssueDate", title: "Issue Date", width: 200, template: "#= kendo.toString(kendo.parseDate(IssueDate), 'MM/dd/yyyy')#" },
                    //{
                    //   field: "SubscriptionFeePercent", title: "Subscription Fee %", width: 200,
                    //   template: "#: SubscriptionFeePercent  # %",
                    //   attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //   field: "RedemptionFeePercent", title: "Redemption Fee %", width: 200,
                    //   template: "#: RedemptionFeePercent  # %",
                    //   attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //   field: "SwitchingFeePercent", title: "Switching Fee %", width: 200,
                    //   template: "#: SwitchingFeePercent  # %",
                    //   attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    field: "RemainingBalanceUnit", title: "Remaining Balance Unit", width: 200,
                    //    template: "#: RemainingBalanceUnit  # %",
                    //    attributes: { style: "text-align:right;" }
                    //},
                    //{ field: "NavRoundingModeDesc", title: "Nav Rounding Mode", width: 300 },
                    //{ field: "NavDecimalPlacesDesc", title: "Nav Decimal Places", width: 300 },
                    //{ field: "UnitRoundingModeDesc", title: "Unit Rounding Mode", width: 300 },
                    //{ field: "UnitDecimalPlaces", title: "Unit Decimal Places", width: 300 },
                    //{ field: "WHTDueDate", hidden: true, title: "WHTDueDate", width: 200 },
                    //{ field: "WHTDueDateDesc", title: "WHT DueDate", width: 200 },
                    //{ field: "MFeeMethod", hidden: true, title: "MFeeMethod", width: 200 },
                    //{ field: "MFeeMethodDesc", title: "Management Fee Calculation", width: 200 },
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
            $("#gridFundApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund"
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
                dataBound: function (e) {
                    if (_GlobClientCode != "29") {
                        this.hideColumn('BitSyariahFund');
                    }
                },
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "FundPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ID", title: "ID", width: 200 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "NKPDName", title: "NKPD Name", width: 300 },
                    { field: "SInvestCode", title: "SInvestCode", width: 300 },
                    { field: "BloombergCode", title: "BloombergCode", width: 300 },
                    { field: "CurrencyID", title: "Currency ID", width: 150 },
                    { field: "CounterpartName", title: "Counterpart Name", width: 150 },
                    { field: "TypeDesc", title: "Type", width: 300 },
                    { field: "MinSwitch", title: "Min Switch", width: 300 },
                    { field: "MinBalSwitchAmt", title: "Min Bal Switch Amt", width: 300 },
                    { field: "MinBalSwitchUnit", title: "Min Bal Switch Unit", width: 300 },
                    { field: "MinSubs", title: "Min Switch", width: 300 },
                    { field: "MinReds", title: "Min Bal Switch Amt", width: 300 },
                    { field: "MinBalRedsAmt", title: "Min Bal Switch Unit", width: 300 },
                    { field: "MinBalRedsUnit", title: "Min Bal Switch Unit", width: 300 },
                    { field: "IsPublic", title: "IsPublic", width: 150, template: "#= IsPublic ? 'Yes' : 'No' #" },
                    { field: "BitNeedRecon", title: "BitNeedRecon", width: 150, template: "#= BitNeedRecon ? 'Yes' : 'No' #" },
                    { field: "BitSinvestFee", title: "Sinvest Fee", width: 150, template: "#= BitSinvestFee ? 'Yes' : 'No' #" },
                    { field: "BitInternalClosePrice", title: "Internal Close Price", width: 150, template: "#= BitInternalClosePrice ? 'Yes' : 'No' #" },
                    { field: "BitInvestmentHighRisk", title: "Investment High Risk", width: 150, template: "#= BitInvestmentHighRisk ? 'Yes' : 'No' #" },
                    { field: "BitSyariahFund", title: "BitSyariahFund", width: 150, template: "#= BitSyariahFund ? 'Yes' : 'No' #" },

                    { field: "FundTypeInternalDesc", title: "TypeInternal", width: 300 },
                    { field: "DefaultBankSubscriptionID", title: "Def Bank Subscription", width: 250 },
                    { field: "DefaultBankSubscriptionName", title: "Name", width: 300 },
                    { field: "DefaultBankRedemptionID", title: "Def Bank Redemption", width: 250 },
                    { field: "DefaultBankRedemptionName", title: "Name", width: 300 },
                    { field: "DefaultFundCashRefID", title: "Def Cash Ref", width: 250 },
                    { field: "DefaultFundCashRefName", title: "Name", width: 300 },
                    { field: "BankBranchID", title: "Bank Custodian ID", width: 200 },
                    { field: "BankBranchName", title: "Name", width: 300 },
                    { field: "MaxUnits", title: "Max Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "TotalUnits", title: "Total Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Nav", title: "NAV", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "EffectiveDate", title: "Effective Date OJK", width: 200, template: "#= kendo.toString(kendo.parseDate(EffectiveDate), 'MM/dd/yyyy')#" },
                    { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                    { field: "IssueDate", title: "Issueance Date (Nav Rp 1000/$1)", width: 200, template: "#= kendo.toString(kendo.parseDate(IssueDate), 'MM/dd/yyyy')#" },
                    //{ field: "DividenDate", title: "Dividen Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DividenDate), 'MM/dd/yyyy')#" },



                    {
                        field: "CustodyFeePercent", title: "Custody Fee Exclude PPN(%)", width: 200,
                        template: "#: CustodyFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "ManagementFeePercent", title: "Management Fee Exclude PPN(%)", width: 200,
                        template: "#: ManagementFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "SubscriptionFeePercent", title: "Subscription Fee %", width: 200,
                        template: "#: SubscriptionFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "RedemptionFeePercent", title: "Redemption Fee %", width: 200,
                        template: "#: RedemptionFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "SwitchingFeePercent", title: "Switching Fee %", width: 200,
                        template: "#: SwitchingFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    {
                        field: "RemainingBalanceUnit", title: "Remaining Balance Unit", width: 200,
                        template: "#: RemainingBalanceUnit  # ",
                        attributes: { style: "text-align:right;" }
                    },
                    { field: "NavRoundingModeDesc", title: "Nav Rounding Mode", width: 300 },
                    { field: "NavDecimalPlacesDesc", title: "Nav Decimal Places", width: 300 },
                    { field: "UnitRoundingModeDesc", title: "Unit Rounding Mode", width: 300 },
                    { field: "UnitDecimalPlaces", title: "Unit Decimal Places", width: 300 },
                    { field: "WHTDueDate", hidden: true, title: "WHTDueDate", width: 200 },
                    { field: "WHTDueDateDesc", title: "WHT DueDate", width: 200 },
                    { field: "MFeeMethod", hidden: true, title: "MFeeMethod", width: 200 },
                    { field: "MFeeMethodDesc", title: "Management Fee Calculation", width: 200 },
                    { field: "SharingFeeCalculation", hidden: true, title: "SharingFeeCalculation", width: 200 },
                    { field: "SharingFeeCalculationDesc", title: "Sharing Fee Calculation", width: 200 },
                    { field: "DefaultPaymentDate", title: "Default Redemption Payment Date", width: 200 },
                    { field: "NPWP", title: "NPWP", width: 300 },
                    { field: "OJKLetter", title: "OJK Letter", width: 300 },

                    { field: "KPDNoContract", title: "KPD No Contract", width: 300 },
                    { field: "KPDDateFromContract", title: "KPD Date From Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateFromContract), 'MM/dd/yyyy')#" },
                    { field: "KPDDateToContract", title: "KPD Date To Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateToContract), 'MM/dd/yyyy')#" },
                    { field: "KPDNoAdendum", title: "KPDNoAdendum", width: 300 },
                    { field: "KPDDateAdendum", title: "KPD Date Adendum", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateAdendum), 'MM/dd/yyyy')#" },

                    { field: "CutOffTime", hidden: true, title: "CutOffTime", width: 200 },

                    { field: "ProspectusUrl", title: "Prospectus Url", width: 300 },
                    { field: "FactsheetUrl", title: "Factsheet Url", width: 300 },
                    { field: "ImageUrl", title: "Image Url", width: 300 },
                    { field: "ISIN", title: "ISIN", width: 300 },
                    { field: "EntryApproveTimeCutoff", title: "Entry Approve Time Cutoff", width: 300 },


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

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFund").kendoTabStrip({
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
                        var FundPendingURL = window.location.origin + "/Radsoft/Fund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FundPendingURL);

                        if (_GlobClientCode == "05") {
                            $("#gridFundPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Fund"
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
                                    { field: "FundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    //{ field: "NKPDName", title: "NKPD Name", width: 300 },
                                    { field: "SInvestCode", title: "SInvestCode", width: 300 },
                                    //{ field: "BloombergCode", title: "BloombergCode", width: 300 },
                                    { field: "CurrencyID", title: "Currency ID", width: 150 },
                                    //{ field: "CounterpartName", title: "Counterpart Name", width: 150 },
                                    { field: "TypeDesc", title: "Type", width: 300 },
                                    { field: "NPWP", title: "NPWP", width: 300 },
                                    { field: "SID", title: "SID", width: 300 },
                                    { field: "KSEINo", title: "KSEI No", width: 300 },
                                    { field: "OJKEffectiveStatementLetterDate", title: "OJK Effective Statement Letter Date", width: 200, template: "#= kendo.toString(kendo.parseDate(OJKEffectiveStatementLetterDate), 'MM/dd/yyyy')#" },
                                    { field: "BitSinvestFee", title: "Sinvest Fee", width: 150, template: "#= BitSinvestFee ? 'Yes' : 'No' #" },
                                    { field: "BitInternalClosePrice", title: "Internal Close Price", width: 150, template: "#= BitInternalClosePrice ? 'Yes' : 'No' #" },
                                    //{ field: "MinSwitch", title: "Min Switch", width: 300 },
                                    //{ field: "MinBalSwitchAmt", title: "Min Bal Switch Amt", width: 300 },
                                    //{ field: "MinBalSwitchUnit", title: "Min Bal Switch Unit", width: 300 },
                                    //{ field: "MinSubs", title: "Min Switch", width: 300 },
                                    //{ field: "MinReds", title: "Min Bal Switch Amt", width: 300 },
                                    //{ field: "MinBalRedsAmt", title: "Min Bal Switch Unit", width: 300 },
                                    //{ field: "MinBalRedsUnit", title: "Min Bal Switch Unit", width: 300 },
                                    //{ field: "IsPublic", title: "IsPublic", width: 150, template: "#= IsPublic ? 'Yes' : 'No' #" },
                                    // { field: "BitNeedRecon", title: "BitNeedRecon", width: 150, template: "#= BitNeedRecon ? 'Yes' : 'No' #" },
                                    //{ field: "FundTypeInternalDesc", title: "TypeInternal", width: 300 },
                                    //{ field: "DefaultBankSubscriptionID", title: "Def Bank Subscription", width: 250 },
                                    //{ field: "DefaultBankSubscriptionName", title: "Name", width: 300 },
                                    //{ field: "DefaultBankRedemptionID", title: "Def Bank Redemption", width: 250 },
                                    //{ field: "DefaultBankRedemptionName", title: "Name", width: 300 },
                                    //{ field: "DefaultFundCashRefID", title: "Def Cash Ref", width: 250 },
                                    //{ field: "DefaultFundCashRefName", title: "Name", width: 300 },
                                    //{ field: "BankBranchID", title: "Bank Custodian ID", width: 200 },
                                    //{ field: "BankBranchName", title: "Name", width: 300 },
                                    //{ field: "MaxUnits", title: "Max Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    //{ field: "TotalUnits", title: "Total Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    //{ field: "Nav", title: "NAV", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    //{ field: "EffectiveDate", title: "Effective Date", width: 200, template: "#= kendo.toString(kendo.parseDate(EffectiveDate), 'MM/dd/yyyy')#" },
                                    //{ field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                                    //{ field: "IssueDate", title: "Issue Date", width: 200, template: "#= kendo.toString(kendo.parseDate(IssueDate), 'MM/dd/yyyy')#" },
                                    //{
                                    //    field: "SubscriptionFeePercent", title: "Subscription Fee %", width: 200,
                                    //    template: "#: SubscriptionFeePercent  # %",
                                    //    attributes: { style: "text-align:right;" }
                                    //},
                                    //{
                                    //    field: "RedemptionFeePercent", title: "Redemption Fee %", width: 200,
                                    //    template: "#: RedemptionFeePercent  # %",
                                    //    attributes: { style: "text-align:right;" }
                                    //},
                                    //{
                                    //    field: "SwitchingFeePercent", title: "Switching Fee %", width: 200,
                                    //    template: "#: SwitchingFeePercent  # %",
                                    //    attributes: { style: "text-align:right;" }
                                    //},
                                    //{
                                    //    field: "RemainingBalanceUnit", title: "Remaining Balance Unit", width: 200,
                                    //    template: "#: RemainingBalanceUnit  # %",
                                    //    attributes: { style: "text-align:right;" }
                                    //},
                                    //{ field: "NavRoundingModeDesc", title: "Nav Rounding Mode", width: 300 },
                                    //{ field: "NavDecimalPlacesDesc", title: "Nav Decimal Places", width: 300 },
                                    //{ field: "UnitRoundingModeDesc", title: "Unit Rounding Mode", width: 300 },
                                    //{ field: "UnitDecimalPlaces", title: "Unit Decimal Places", width: 300 },
                                    //{ field: "WHTDueDate", hidden: true, title: "WHTDueDate", width: 200 },
                                    //{ field: "WHTDueDateDesc", title: "WHT DueDate", width: 200 },
                                    //{ field: "MFeeMethod", hidden: true, title: "MFeeMethod", width: 200 },
                                    //{ field: "MFeeMethodDesc", title: "Management Fee Calculation", width: 200 },
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
                            $("#gridFundPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Fund"
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
                                dataBound: function (e) {
                                    if (_GlobClientCode != "29") {
                                        this.hideColumn('BitSyariahFund');
                                    }
                                },
                                toolbar: ["excel"],
                                columns: [
                                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                    { field: "FundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    { field: "NKPDName", title: "NKPD Name", width: 300 },
                                    { field: "SInvestCode", title: "SInvestCode", width: 300 },
                                    { field: "BloombergCode", title: "BloombergCode", width: 300 },
                                    { field: "CurrencyID", title: "Currency ID", width: 150 },
                                    { field: "CounterpartName", title: "Counterpart Name", width: 150 },
                                    { field: "TypeDesc", title: "Type", width: 300 },
                                    { field: "MinSwitch", title: "Min Switch", width: 300 },
                                    { field: "MinBalSwitchAmt", title: "Min Bal Switch Amt", width: 300 },
                                    { field: "MinBalSwitchUnit", title: "Min Bal Switch Unit", width: 300 },
                                    { field: "MinSubs", title: "Min Switch", width: 300 },
                                    { field: "MinReds", title: "Min Bal Switch Amt", width: 300 },
                                    { field: "MinBalRedsAmt", title: "Min Bal Switch Unit", width: 300 },
                                    { field: "MinBalRedsUnit", title: "Min Bal Switch Unit", width: 300 },
                                    { field: "IsPublic", title: "IsPublic", width: 150, template: "#= IsPublic ? 'Yes' : 'No' #" },
                                    { field: "BitNeedRecon", title: "BitNeedRecon", width: 150, template: "#= BitNeedRecon ? 'Yes' : 'No' #" },
                                    { field: "BitSinvestFee", title: "Sinvest Fee", width: 150, template: "#= BitSinvestFee ? 'Yes' : 'No' #" },
                                    { field: "BitInternalClosePrice", title: "Internal Close Price", width: 150, template: "#= BitInternalClosePrice ? 'Yes' : 'No' #" },
                                    { field: "BitInvestmentHighRisk", title: "Investment High Risk", width: 150, template: "#= BitInvestmentHighRisk ? 'Yes' : 'No' #" },
                                    { field: "BitSyariahFund", title: "BitSyariahFund", width: 150, template: "#= BitSyariahFund ? 'Yes' : 'No' #" },
                                    { field: "FundTypeInternalDesc", title: "TypeInternal", width: 300 },
                                    { field: "DefaultBankSubscriptionID", title: "Def Bank Subscription", width: 250 },
                                    { field: "DefaultBankSubscriptionName", title: "Name", width: 300 },
                                    { field: "DefaultBankRedemptionID", title: "Def Bank Redemption", width: 250 },
                                    { field: "DefaultBankRedemptionName", title: "Name", width: 300 },
                                    { field: "DefaultFundCashRefID", title: "Def Cash Ref", width: 250 },
                                    { field: "DefaultFundCashRefName", title: "Name", width: 300 },
                                    { field: "BankBranchID", title: "Bank Custodian ID", width: 200 },
                                    { field: "BankBranchName", title: "Name", width: 300 },
                                    { field: "MaxUnits", title: "Max Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "TotalUnits", title: "Total Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "Nav", title: "NAV", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "EffectiveDate", title: "Effective Date OJK", width: 200, template: "#= kendo.toString(kendo.parseDate(EffectiveDate), 'MM/dd/yyyy')#" },
                                    { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                                    { field: "IssueDate", title: "Issueance Date (Nav Rp 1000/$1)", width: 200, template: "#= kendo.toString(kendo.parseDate(IssueDate), 'MM/dd/yyyy')#" },
                                    //{ field: "DividenDate", title: "Dividen Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DividenDate), 'MM/dd/yyyy')#" },



                                    {
                                        field: "CustodyFeePercent", title: "Custody Fee Exclude PPN(%)", width: 200,
                                        template: "#: CustodyFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    {
                                        field: "ManagementFeePercent", title: "Management Fee Exclude PPN(%)", width: 200,
                                        template: "#: ManagementFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    {
                                        field: "SubscriptionFeePercent", title: "Subscription Fee %", width: 200,
                                        template: "#: SubscriptionFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    {
                                        field: "RedemptionFeePercent", title: "Redemption Fee %", width: 200,
                                        template: "#: RedemptionFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    {
                                        field: "SwitchingFeePercent", title: "Switching Fee %", width: 200,
                                        template: "#: SwitchingFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    {
                                        field: "RemainingBalanceUnit", title: "Remaining Balance Unit", width: 200,
                                        template: "#: RemainingBalanceUnit  # ",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "NavRoundingModeDesc", title: "Nav Rounding Mode", width: 300 },
                                    { field: "NavDecimalPlacesDesc", title: "Nav Decimal Places", width: 300 },
                                    { field: "UnitRoundingModeDesc", title: "Unit Rounding Mode", width: 300 },
                                    { field: "UnitDecimalPlaces", title: "Unit Decimal Places", width: 300 },
                                    { field: "WHTDueDate", hidden: true, title: "WHTDueDate", width: 200 },
                                    { field: "WHTDueDateDesc", title: "WHT DueDate", width: 200 },
                                    { field: "MFeeMethod", hidden: true, title: "MFeeMethod", width: 200 },
                                    { field: "MFeeMethodDesc", title: "Management Fee Calculation", width: 200 },
                                    { field: "SharingFeeCalculation", hidden: true, title: "SharingFeeCalculation", width: 200 },
                                    { field: "SharingFeeCalculationDesc", title: "Sharing Fee Calculation", width: 200 },
                                    { field: "DefaultPaymentDate", title: "Default Redemption Payment Date", width: 200 },
                                    { field: "NPWP", title: "NPWP", width: 300 },
                                    { field: "OJKLetter", title: "OJK Letter", width: 300 },

                                    { field: "KPDNoContract", title: "KPD No Contract", width: 300 },
                                    { field: "KPDDateFromContract", title: "KPD Date From Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateFromContract), 'MM/dd/yyyy')#" },
                                    { field: "KPDDateToContract", title: "KPD Date To Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateToContract), 'MM/dd/yyyy')#" },
                                    { field: "KPDNoAdendum", title: "KPDNoAdendum", width: 300 },
                                    { field: "KPDDateAdendum", title: "KPD Date Adendum", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateAdendum), 'MM/dd/yyyy')#" },

                                    { field: "CutOffTime", hidden: true, title: "CutOffTime", width: 200 },

                                    { field: "ProspectusUrl", title: "Prospectus Url", width: 300 },
                                    { field: "FactsheetUrl", title: "Factsheet Url", width: 300 },
                                    { field: "ImageUrl", title: "Image Url", width: 300 },
                                    { field: "ISIN", title: "ISIN", width: 300 },
                                    { field: "EntryApproveTimeCutoff", title: "Entry Approve Time Cutoff", width: 300 },

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

                    }
                    if (tabindex == 2) {

                        var FundHistoryURL = window.location.origin + "/Radsoft/Fund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FundHistoryURL);

                        $("#gridFundHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund"
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
                                { field: "FundPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "NKPDName", title: "NKPD Name", width: 300 },
                                { field: "SInvestCode", title: "SInvestCode", width: 300 },
                                { field: "BloombergCode", title: "BloombergCode", width: 300 },
                                { field: "CurrencyID", title: "Currency ID", width: 150 },
                                { field: "CounterpartName", title: "Counterpart Name", width: 150 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "MinSwitch", title: "Min Switch", width: 300 },
                                { field: "MinBalSwitchAmt", title: "Min Bal Switch Amt", width: 300 },
                                { field: "MinBalSwitchUnit", title: "Min Bal Switch Unit", width: 300 },
                                { field: "MinSubs", title: "Min Switch", width: 300 },
                                { field: "MinReds", title: "Min Bal Switch Amt", width: 300 },
                                { field: "MinBalRedsAmt", title: "Min Bal Switch Unit", width: 300 },
                                { field: "MinBalRedsUnit", title: "Min Bal Switch Unit", width: 300 },
                                { field: "IsPublic", title: "IsPublic", width: 150, template: "#= IsPublic ? 'Yes' : 'No' #" },
                                { field: "BitNeedRecon", title: "BitNeedRecon", width: 150, template: "#= BitNeedRecon ? 'Yes' : 'No' #" },
                                { field: "BitSinvestFee", title: "Sinvest Fee", width: 150, template: "#= BitSinvestFee ? 'Yes' : 'No' #" },
                                { field: "BitInternalClosePrice", title: "Internal Close Price", width: 150, template: "#= BitInternalClosePrice ? 'Yes' : 'No' #" },
                                { field: "BitInvestmentHighRisk", title: "Investment High Risk", width: 150, template: "#= BitInvestmentHighRisk ? 'Yes' : 'No' #" },
                                { field: "BitSyariahFund", title: "BitSyariahFund", width: 150, template: "#= BitSyariahFund ? 'Yes' : 'No' #" },

                                { field: "FundTypeInternalDesc", title: "TypeInternal", width: 300 },
                                { field: "DefaultBankSubscriptionID", title: "Def Bank Subscription", width: 250 },
                                { field: "DefaultBankSubscriptionName", title: "Name", width: 300 },
                                { field: "DefaultBankRedemptionID", title: "Def Bank Redemption", width: 250 },
                                { field: "DefaultBankRedemptionName", title: "Name", width: 300 },
                                { field: "DefaultFundCashRefID", title: "Def Cash Ref", width: 250 },
                                { field: "DefaultFundCashRefName", title: "Name", width: 300 },
                                { field: "BankBranchID", title: "Bank Custodian ID", width: 200 },
                                { field: "BankBranchName", title: "Name", width: 300 },
                                { field: "MaxUnits", title: "Max Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TotalUnits", title: "Total Units", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "Nav", title: "NAV", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "EffectiveDate", title: "Effective Date OJK", width: 200, template: "#= kendo.toString(kendo.parseDate(EffectiveDate), 'MM/dd/yyyy')#" },
                                { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                                { field: "IssueDate", title: "Issueance Date (Nav Rp 1000/$1)", width: 200, template: "#= kendo.toString(kendo.parseDate(IssueDate), 'MM/dd/yyyy')#" },
                                //{ field: "DividenDate", title: "Dividen Date", width: 200, template: "#= kendo.toString(kendo.parseDate(DividenDate), 'MM/dd/yyyy')#" },



                                {
                                    field: "CustodyFeePercent", title: "Custody Fee Exclude PPN(%)", width: 200,
                                    template: "#: CustodyFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "ManagementFeePercent", title: "Management Fee Exclude PPN(%)", width: 200,
                                    template: "#: ManagementFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SubscriptionFeePercent", title: "Subscription Fee %", width: 200,
                                    template: "#: SubscriptionFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "RedemptionFeePercent", title: "Redemption Fee %", width: 200,
                                    template: "#: RedemptionFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "SwitchingFeePercent", title: "Switching Fee %", width: 200,
                                    template: "#: SwitchingFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "RemainingBalanceUnit", title: "Remaining Balance Unit", width: 200,
                                    template: "#: RemainingBalanceUnit  # ",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "NavRoundingModeDesc", title: "Nav Rounding Mode", width: 300 },
                                { field: "NavDecimalPlacesDesc", title: "Nav Decimal Places", width: 300 },
                                { field: "UnitRoundingModeDesc", title: "Unit Rounding Mode", width: 300 },
                                { field: "UnitDecimalPlaces", title: "Unit Decimal Places", width: 300 },
                                { field: "WHTDueDate", hidden: true, title: "WHTDueDate", width: 200 },
                                { field: "WHTDueDateDesc", title: "WHT DueDate", width: 200 },
                                { field: "MFeeMethod", hidden: true, title: "MFeeMethod", width: 200 },
                                { field: "MFeeMethodDesc", title: "Management Fee Calculation", width: 200 },
                                { field: "SharingFeeCalculation", hidden: true, title: "SharingFeeCalculation", width: 200 },
                                { field: "SharingFeeCalculationDesc", title: "Sharing Fee Calculation", width: 200 },
                                { field: "NPWP", title: "NPWP", width: 300 },
                                { field: "OJKLetter", title: "OJK Letter", width: 300 },
                                { field: "DefaultPaymentDate", title: "Default Redemption Payment Date", width: 200 },

                                { field: "KPDNoContract", title: "KPD No Contract", width: 300 },
                                { field: "KPDDateFromContract", title: "KPD Date From Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateFromContract), 'MM/dd/yyyy')#" },
                                { field: "KPDDateToContract", title: "KPD Date To Contract", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateToContract), 'MM/dd/yyyy')#" },
                                { field: "KPDNoAdendum", title: "KPDNoAdendum", width: 300 },
                                { field: "KPDDateAdendum", title: "KPD Date Adendum", width: 200, template: "#= kendo.toString(kendo.parseDate(KPDDateAdendum), 'MM/dd/yyyy')#" },

                                { field: "CutOffTime", hidden: true, title: "CutOffTime", width: 200 },

                                { field: "ProspectusUrl", title: "Prospectus Url", width: 300 },
                                { field: "FactsheetUrl", title: "Factsheet Url", width: 300 },
                                { field: "ImageUrl", title: "Image Url", width: 300 },
                                { field: "ISIN", title: "ISIN", width: 300 },
                                { field: "EntryApproveTimeCutoff", title: "Entry Approve Time Cutoff", width: 300 },

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
        var grid = $("#gridFundHistory").data("kendoGrid");
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

        if (_GlobClientCode != "29") {
            grid.hideColumn('BitSyariahFund');
        }
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Fund",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Fund = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    NKPDName: $('#NKPDName').val(),
                                    SInvestCode: $('#SInvestCode').val(),
                                    BloombergCode: $('#BloombergCode').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    Type: $('#Type').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    MaxUnits: $('#MaxUnits').val(),
                                    TotalUnits: $('#TotalUnits').val(),
                                    Nav: $('#Nav').val(),
                                    EffectiveDate: $('#EffectiveDate').val(),
                                    MaturityDate: $('#MaturityDate').val(),
                                    FundTypeInternal: $('#FundTypeInternal').val(),
                                    CustodyFeePercent: $('#CustodyFeePercent').val(),
                                    ManagementFeePercent: $('#ManagementFeePercent').val(),
                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                    RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                                    SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                    NavRoundingMode: $('#NavRoundingMode').val(),
                                    NavDecimalPlaces: $('#NavDecimalPlaces').val(),
                                    UnitDecimalPlaces: $('#UnitDecimalPlaces').val(),
                                    UnitRoundingMode: $('#UnitRoundingMode').val(),
                                    MinSwitch: $('#MinSwitch').val(),
                                    MinBalSwitchAmt: $('#MinBalSwitchAmt').val(),
                                    MinBalSwitchUnit: $('#MinBalSwitchUnit').val(),
                                    MinSubs: $('#MinSubs').val(),
                                    MinReds: $('#MinReds').val(),
                                    MinBalRedsAmt: $('#MinBalRedsAmt').val(),
                                    MinBalRedsUnit: $('#MinBalRedsUnit').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    IsPublic: $('#IsPublic').is(":checked"),
                                    BitNeedRecon: $('#BitNeedRecon').is(":checked"),
                                    DefaultPaymentDate: $('#DefaultPaymentDate').val(),
                                    WHTDueDate: $('#WHTDueDate').val(),
                                    IssueDate: $('#IssueDate').val(),
                                    MFeeMethod: $('#MFeeMethod').val(),
                                    RemainingBalanceUnit: $('#RemainingBalanceUnit').val(),
                                    SharingFeeCalculation: $('#SharingFeeCalculation').val(),
                                    DividenDate: $('#DividenDate').val(),
                                    NPWP: $('#NPWP').val(),
                                    OJKLetter: $('#OJKLetter').val(),
                                    BitSinvestFee: $('#BitSinvestFee').is(":checked"),
                                    BitInternalClosePrice: $('#BitInternalClosePrice').is(":checked"),
                                    BitInvestmentHighRisk: $('#BitInvestmentHighRisk').is(":checked"),

                                    KPDNoContract: $('#KPDNoContract').val(),
                                    KPDDateFromContract: $('#KPDDateFromContract').val(),
                                    KPDDateToContract: $('#KPDDateToContract').val(),
                                    KPDNoAdendum: $('#KPDNoAdendum').val(),
                                    KPDDateAdendum: $('#KPDDateAdendum').val(),
                                    CutOffTime: $('#CutOffTime').val(),

                                    ProspectusUrl: $('#ProspectusUrl').val(),
                                    FactsheetUrl: $('#FactsheetUrl').val(),
                                    ImageUrl: $('#ImageUrl').val(),
                                    ISIN: $('#ISIN').val(),

                                    EntryApproveTimeCutoff: $('#EntryApproveTimeCutoff').val(),
                                    OJKEffectiveStatementLetterDate: $('#OJKEffectiveStatementLetterDate').val(),
                                    MinBalSwitchToAmt: $('#MinBalSwitchToAmt').val(),

                                    BitSyariahFund: $('#BitSyariahFund').is(":checked"),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Fund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fund_I",
                                    type: 'POST',
                                    data: JSON.stringify(Fund),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundPK").val() + "/" + $("#HistoryPK").val() + "/" + "Fund",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Fund = {
                                    FundPK: $('#FundPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    NKPDName: $('#NKPDName').val(),
                                    SInvestCode: $('#SInvestCode').val(),
                                    BloombergCode: $('#BloombergCode').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    Type: $('#Type').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    MaxUnits: $('#MaxUnits').val(),
                                    TotalUnits: $('#TotalUnits').val(),
                                    Nav: $('#Nav').val(),
                                    EffectiveDate: $('#EffectiveDate').val(),
                                    FundTypeInternal: $('#FundTypeInternal').val(),
                                    MaturityDate: $('#MaturityDate').val(),
                                    CustodyFeePercent: $('#CustodyFeePercent').val(),
                                    ManagementFeePercent: $('#ManagementFeePercent').val(),
                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                    RedemptionFeePercent: $('#RedemptionFeePercent').val(),
                                    SwitchingFeePercent: $('#SwitchingFeePercent').val(),
                                    NavRoundingMode: $('#NavRoundingMode').val(),
                                    NavDecimalPlaces: $('#NavDecimalPlaces').val(),
                                    UnitDecimalPlaces: $('#UnitDecimalPlaces').val(),
                                    UnitRoundingMode: $('#UnitRoundingMode').val(),
                                    MinSwitch: $('#MinSwitch').val(),
                                    MinBalSwitchAmt: $('#MinBalSwitchAmt').val(),
                                    MinBalSwitchUnit: $('#MinBalSwitchUnit').val(),
                                    MinSubs: $('#MinSubs').val(),
                                    MinReds: $('#MinReds').val(),
                                    MinBalRedsAmt: $('#MinBalRedsAmt').val(),
                                    MinBalRedsUnit: $('#MinBalRedsUnit').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    IsPublic: $('#IsPublic').is(":checked"),
                                    BitNeedRecon: $('#BitNeedRecon').is(":checked"),
                                    DefaultPaymentDate: $('#DefaultPaymentDate').val(),
                                    WHTDueDate: $('#WHTDueDate').val(),
                                    IssueDate: $('#IssueDate').val(),
                                    MFeeMethod: $('#MFeeMethod').val(),
                                    RemainingBalanceUnit: $('#RemainingBalanceUnit').val(),
                                    SharingFeeCalculation: $('#SharingFeeCalculation').val(),
                                    DividenDate: $('#DividenDate').val(),
                                    NPWP: $('#NPWP').val(),
                                    OJKLetter: $('#OJKLetter').val(),
                                    BitSinvestFee: $('#BitSinvestFee').is(":checked"),
                                    BitInternalClosePrice: $('#BitInternalClosePrice').is(":checked"),
                                    BitInvestmentHighRisk: $('#BitInvestmentHighRisk').is(":checked"),

                                    KPDNoContract: $('#KPDNoContract').val(),
                                    KPDDateFromContract: $('#KPDDateFromContract').val(),
                                    KPDDateToContract: $('#KPDDateToContract').val(),
                                    KPDNoAdendum: $('#KPDNoAdendum').val(),
                                    KPDDateAdendum: $('#KPDDateAdendum').val(),

                                    CutOffTime: $('#CutOffTime').val(),

                                    ProspectusUrl: $('#ProspectusUrl').val(),
                                    FactsheetUrl: $('#FactsheetUrl').val(),
                                    ImageUrl: $('#ImageUrl').val(),
                                    ISIN: $('#ISIN').val(),
                                    EntryApproveTimeCutoff: $('#EntryApproveTimeCutoff').val(),
                                    OJKEffectiveStatementLetterDate: $('#OJKEffectiveStatementLetterDate').val(),
                                    MinBalSwitchToAmt: $('#MinBalSwitchToAmt').val(),
                                    BitSyariahFund: $('#BitSyariahFund').is(":checked"),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Fund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fund_U",
                                    type: 'POST',
                                    data: JSON.stringify(Fund),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundPK").val() + "/" + $("#HistoryPK").val() + "/" + "Fund",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Fund" + "/" + $("#FundPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundPK").val() + "/" + $("#HistoryPK").val() + "/" + "Fund",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var Fund = {
                                FundPK: $('#FundPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fund_A",
                                type: 'POST',
                                data: JSON.stringify(Fund),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundPK").val() + "/" + $("#HistoryPK").val() + "/" + "Fund",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Fund = {
                                FundPK: $('#FundPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fund_V",
                                type: 'POST',
                                data: JSON.stringify(Fund),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundPK").val() + "/" + $("#HistoryPK").val() + "/" + "Fund",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Fund = {
                                FundPK: $('#FundPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Fund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fund_R",
                                type: 'POST',
                                data: JSON.stringify(Fund),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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


    function ShowHideLabelByGlobFundTypeInternal(_type) {
        ClearRequiredAttribute();
        HideLabel();
        //BOND
        $("#LotInShare").val(1);
        if (_type == 2) {

            $("#LblKPDNoContract").show();
            $("#LblKPDDateFromContract").show();
            $("#LblKPDDateToContract").show();
            $("#LblKPDNoAdendum").show();
            $("#LblKPDDateAdendum").show();

            //$("#KPDNoContract").attr("required", true);
            //$("#KPDDateFromContract").attr("required", true);
            //$("#KPDDateToContract").attr("required", true);
            //$("#KPDNoAdendum").attr("required", true);
            //$("#KPDDateAdendum").attr("required", true);

        }
        else {

            $("#LblKPDNoContract").hide();
            $("#LblKPDDateFromContract").hide();
            $("#LblKPDDateToContract").hide();
            $("#LblKPDNoAdendum").hide();
            $("#LblKPDDateAdendum").hide();

            $("#KPDNoContract").attr("required", false);
            $("#KPDDateFromContract").attr("required", false);
            $("#KPDDateToContract").attr("required", false);
            $("#KPDNoAdendum").attr("required", false);
            $("#KPDDateAdendum").attr("required", false);
        }

    }

    function HideLabel() {
        $("#LblKPDNoContract").hide();
        $("#LblKPDDateFromContract").hide();
        $("#LblKPDDateToContract").hide();
        $("#LblKPDNoAdendum").hide();
        $("#LblKPDDateAdendum").hide();
       
    }

    function ClearAttribute() {
        $("#KPDNoContract").val("");
        $("#KPDDateFromContract").val("");
        $("#KPDDateToContract").val("");
        $("#KPDNoAdendum").val("");
        $("#KPDDateAdendum").val("");
       
    }

    function ClearRequiredAttribute() {
        $("#KPDNoContract").removeAttr("required");
        $("#KPDDateFromContract").removeAttr("required");
        $("#KPDDateToContract").removeAttr("required");
        $("#KPDNoAdendum").removeAttr("required");
        $("#KPDDateAdendum").removeAttr("required");
       
    }

    $("#BtnCheckMatureFund").click(function () {
        showCheckMatureFund();
    });

    function showCheckMatureFund(e) {



        initMatureFund();
        WinCheckMatureFund.center();
        WinCheckMatureFund.open();

    }

    function initMatureFund() {
        var MatureFundURL = window.location.origin + "/Radsoft/Fund/GetDataMatureFundByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceMatureFund = getDataSourceMatureFund(MatureFundURL);

        $("#gridMatureFund").kendoGrid({
            dataSource: dataSourceMatureFund,
            height: 300,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [
                { field: "ID", title: "Fund ID", width: 200 },
                { field: "Name", title: "Fund Name", width: 200 }
            ]
        });


    }

    function getDataSourceMatureFund(_url) {
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
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 20,
                schema: {
                    model: {
                        fields: {
                            ID: { type: "string" },
                            Name: { type: "string" }
                        }
                    }
                }
            });
    }

    function refreshMatureFund() {
        var newMatureFund = getDataSourceMatureFund(window.location.origin + "/Radsoft/Fund/GetDataMatureFundByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridMatureFund").data("kendoGrid").setDataSource(newMatureFund);




    }

    function onWinMatureFundClose() {
        $("#ParamDateTo").data("kendoDatePicker").value(new Date());
        $("#gridMatureFund").empty();
    }


});
