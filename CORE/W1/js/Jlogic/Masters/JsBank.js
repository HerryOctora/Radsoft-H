$(document).ready(function () {
    document.title = 'FORM BANK';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCountry;
    var htmlCountry;
    var htmlCountryDesc;
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
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }


    function validateData() {

        if (($("#PaymentInterestSpecificDate").data("kendoDatePicker").value() == "" || $("#PaymentInterestSpecificDate").data("kendoDatePicker").value() == null) &&
            ($("#InterestPaymentType").data("KendoComboBox").value() == 4 || $("#InterestPaymentType").data("KendoComboBox").value() == 5 || $("#InterestPaymentType").data("KendoComboBox").value() == 6)) {
            alertify.alert("Specific date must be filled");
            return 0;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function initWindow() {

        $("#PaymentInterestSpecificDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        win = $("#WinBank").kendoWindow({
            height: 600,
            title: "Bank Detail",
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

        WinListCountry = $("#WinListCountry").kendoWindow({
            height: "520px",
            title: "Country List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCountryClose
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinBank").kendoValidator().data("kendoValidator");

    function validateData() {

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.error("Validation not Pass");
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

            $("#BankPK").val(dataItemX.BankPK);
            $("#SinvestID").val(dataItemX.SinvestID);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#ClearingCode").val(dataItemX.ClearingCode);
            $("#RTGSCode").val(dataItemX.RTGSCode);
            $("#PTPCode").val(dataItemX.PTPCode);
            $("#USDPTPCode").val(dataItemX.USDPTPCode);
            $("#NKPDCode").val(dataItemX.NKPDCode);
            $("#Country").val(dataItemX.Country);
            $("#CountryDesc").val(dataItemX.CountryDesc);
            $("#InterestDaysType").val(dataItemX.InterestDaysType);
            $("#InterestPaymentType").val(dataItemX.InterestPaymentType);
            $("#PaymentModeOnMaturity").val(dataItemX.PaymentModeOnMaturity);
            $("#PaymentInterestSpecificDate").val(dataItemX.PaymentInterestSpecificDate);
            $("#BICode").val(dataItemX.BICode);
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



        $("#FeeLLG").kendoNumericTextBox({
            format: "n4",
            value: setFeeLLG()

        });
        function setFeeLLG() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FeeLLG;
            }
        }

        $("#FeeRTGS").kendoNumericTextBox({
            format: "n4",
            value: setFeeRTGS()

        });
        function setFeeRTGS() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FeeRTGS;
            }
        }

        $("#MinforRTGS").kendoNumericTextBox({
            format: "n4",
            value: setMinforRTGS()

        });
        function setMinforRTGS() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MinforRTGS;
            }
        }

        $("#InterestDays").kendoNumericTextBox({
            format: "n0",
            value: setInterestDays()

        });
        function setInterestDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InterestDays;
            }
        }



        $("#BitRDN").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitRDN,
            value: setCmbBitRDN()
        });
        function OnChangeBitRDN() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitRDN() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitRDN;
            }
        }

        $("#BitSyariah").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitSyariah,
            value: setCmbBitSyariah()
        });
        function OnChangeBitSyariah() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitSyariah() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitSyariah;
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RoundingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JournalRoundingMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingMode,
                    dataSource: data,
                    value: setCmbJournalRoundingMode()
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
        function setCmbJournalRoundingMode() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.JournalRoundingMode == 0) {
                    return "";
                } else {
                    return dataItemX.JournalRoundingMode;
                }
            }
        }


        //Decimal Places
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DecimalPlaces",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JournalDecimalPlaces").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalPlaces,
                    dataSource: data,
                    value: setCmbJournalDecimalPlaces()
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
        function setCmbJournalDecimalPlaces() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.JournalDecimalPlaces == 0) {
                    return 0;
                } else {
                    return dataItemX.JournalDecimalPlaces;
                }
            }
        }

        //InterestDaysType

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboBankBranch/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestDaysType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestDaysType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInterestDaysType,
                    value: setCmbInterestDaysType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInterestDaysType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }
        }
        function setCmbInterestDaysType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestDaysType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestDaysType;
                }
            }
        }

        //InterestPaymentType

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestPaymentType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestPaymentType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInterestPaymentType,
                    value: setCmbInterestPaymentType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInterestPaymentType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }
            hideSpecificdate($('#InterestPaymentType').val());
        }
        function setCmbInterestPaymentType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestPaymentType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPaymentType;
                }
            }
        }
        if (e != null) {
            hideSpecificdate($('#InterestPaymentType').val());
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
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePaymentModeOnMaturity,
                    value: setCmbPaymentModeOnMaturity()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePaymentModeOnMaturity() {
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


        $.ajax({
            url: window.location.origin + "/Radsoft/Issuer/GetIssuerCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#IssuerPK").kendoComboBox({
                    dataValueField: "IssuerPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeIssuerPK,
                    dataSource: data,
                    value: setCmbIssuerPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeIssuerPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbIssuerPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IssuerPK == 0) {
                    return "";
                } else {
                    return dataItemX.IssuerPK;
                }
            }
        }

        $("#CapitalClassification").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Capital Classification 1", value: 1 },
                { text: "Capital Classification 2", value: 2 },
                { text: "Capital Classification 3", value: 3 },
                { text: "Capital Classification 4", value: 4 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCapitalClassification,
            value: setCmbCapitalClassification()
        });
        function OnChangeCapitalClassification() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        function setCmbCapitalClassification() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CapitalClassification == 0) {
                    return "";
                } else {
                    return dataItemX.CapitalClassification;
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
        $("#BankPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#BICode").val("");
        $("#ClearingCode").val("");
        $("#RTGSCode").val("");
        $("#Country").val("");
        $("#CountryDesc").val("");
        $("#BitRDN").val("");
        $("#BitSyariah").val("");
        $("#FeeLLG").val("");
        $("#FeeRTGS").val("");
        $("#MinforRTGS").val("");
        $("#SinvestID").val("");
        $("#InterestDays").val("");
        $("#JournalRoundingMode").val("");
        $("#JournalDecimalPlaces").val("");
        $("#InterestDaysType").val("");
        $("#InterestPaymentType").val("");
        $("#PaymentModeOnMaturity").val("");
        $("#PaymentInterestSpecificDate").val("");
        $("#NKPDCode").val("");
        $("#IssuerPK").val("");
        $("#CapitalClassification").val("");
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
                            BankPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            BICode: { type: "string" },
                            ClearingCode: { type: "string" },
                            RTGSCode: { type: "string" },
                            Country: { type: "string" },
                            CountryDesc: { type: "string" },
                            BitRDN: { type: "boolean" },
                            BitSyariah: { type: "boolean" },
                            FeeLLG: { type: "number" },
                            FeeRTGS: { type: "number" },
                            MinforRTGS: { type: "number" },
                            InterestDays: { type: "number" },
                            InterestDaysType: { type: "string" },
                            InterestPaymentType: { type: "string" },
                            PaymentModeOnMaturity: { type: "string" },
                            NKPDCode: { type: "string" },
                            IssuerPK: { type: "number" },
                            IssuerID: { type: "string" },
                            CapitalClassification: { type: "number" },
                            CapitalClassificationDesc: { type: "string" },
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
            var gridApproved = $("#gridBankApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBankPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBankHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BankApprovedURL = window.location.origin + "/Radsoft/Bank/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(BankApprovedURL);

        $("#gridBankApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Bank"
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
                { field: "BankPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 400 },
                { field: "BICode", title: "BI Code", width: 200 },
                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                { field: "PTPCode", title: "IDR PTP Code", width: 200 },
                { field: "USDPTPCode", title: "USD PTP Code", width: 200 },
                { field: "NKPDCode", title: "NKPD Code", width: 200 },
                { field: "Country", title: "Country", hidden: true, width: 200 },
                { field: "CountryDesc", title: "Country", width: 200 },
                { field: "PaymentInterestSpecificDate", title: "Specific Date", width: 400 },
                { field: "BitRDN", title: "RDN", hidden: true, width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "InterestDays", title: "Interest Days", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 200 },
                { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 200 },
                { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 200 },
                { field: "IssuerPK", title: "IssuerPK", hidden: true, width: 200 },
                { field: "IssuerID", title: "Issuer", width: 200 },
                { field: "CapitalClassification", title: "CapitalClassification", hidden: true, width: 200 },
                { field: "CapitalClassificationDesc", title: "Capital Classification", width: 200 },
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
        $("#TabBank").kendoTabStrip({
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
                        var BankPendingURL = window.location.origin + "/Radsoft/Bank/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(BankPendingURL);
                        $("#gridBankPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Bank"
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
                                { field: "BankPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "BICode", title: "BI Code", width: 200 },
                                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                                { field: "PTPCode", title: "IDR PTP Code", width: 200 },
                                { field: "USDPTPCode", title: "USD PTP Code", width: 200 },
                                { field: "NKPDCode", title: "NKPD Code", width: 200 },
                                { field: "Country", title: "Country", hidden: true, width: 200 },
                                { field: "CountryDesc", title: "Country", width: 200 },
                                { field: "PaymentInterestSpecificDate", title: "Specific Date", width: 400 },
                                { field: "BitRDN", title: "RDN", hidden: true, width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "InterestDays", title: "Interest Days", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 200 },
                                { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 200 },
                                { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 200 },
                                { field: "IssuerPK", title: "IssuerPK", hidden: true, width: 200 },
                                { field: "IssuerID", title: "Issuer", width: 200 },
                                { field: "CapitalClassification", title: "CapitalClassification", hidden: true, width: 200 },
                                { field: "CapitalClassificationDesc", title: "Capital Classification", width: 200 },
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

                        var BankHistoryURL = window.location.origin + "/Radsoft/Bank/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(BankHistoryURL);

                        $("#gridBankHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Bank"
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
                                { field: "BankPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "BICode", title: "BI Code", width: 200 },
                                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                                { field: "PTPCode", title: "IDR PTP Code", width: 200 },
                                { field: "USDPTPCode", title: "USD PTP Code", width: 200 },
                                { field: "NKPDCode", title: "NKPD Code", width: 200 },
                                { field: "Country", title: "Country", hidden: true, width: 200 },
                                { field: "CountryDesc", title: "Country", width: 200 },
                                { field: "PaymentInterestSpecificDate", title: "Specific Date", width: 400 },
                                { field: "BitRDN", title: "RDN", hidden: true, width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "InterestDays", title: "Interest Days", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "InterestDaysType", title: "InterestDaysType", hidden: true, width: 200 },
                                { field: "InterestPaymentType", title: "InterestPaymentType", hidden: true, width: 200 },
                                { field: "PaymentModeOnMaturity", title: "PaymentModeOnMaturity", hidden: true, width: 200 },
                                { field: "IssuerPK", title: "IssuerPK", hidden: true, width: 200 },
                                { field: "IssuerID", title: "Issuer", width: 200 },
                                { field: "CapitalClassification", title: "CapitalClassification", hidden: true, width: 200 },
                                { field: "CapitalClassificationDesc", title: "Capital Classification", width: 200 },
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
        var grid = $("#gridBankHistory").data("kendoGrid");
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
                alertify.success("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
        hideSpecificdate($('#InterestPaymentType').val());

    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Bank",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Bank = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    BICode: $('#BICode').val(),
                                    ClearingCode: $('#ClearingCode').val(),
                                    RTGSCode: $('#RTGSCode').val(),
                                    PTPCode: $('#PTPCode').val(),
                                    USDPTPCode: $('#USDPTPCode').val(),
                                    Country: $('#Country').val(),
                                    BitRDN: $('#BitRDN').val(),
                                    BitSyariah: $('#BitSyariah').val(),
                                    FeeLLG: $('#FeeLLG').val(),
                                    FeeRTGS: $('#FeeRTGS').val(),
                                    MinforRTGS: $('#MinforRTGS').val(),
                                    SinvestID: $('#SinvestID').val(),
                                    InterestDays: $('#InterestDays').val(),
                                    JournalRoundingMode: $('#JournalRoundingMode').val(),
                                    JournalDecimalPlaces: $('#JournalDecimalPlaces').val(),
                                    InterestDaysType: $('#InterestDaysType').val(),
                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                    PaymentInterestSpecificDate: $('#PaymentInterestSpecificDate').val(),
                                    NKPDCode: $('#NKPDCode').val(),
                                    IssuerPK: $('#IssuerPK').val(),
                                    CapitalClassification: $('#CapitalClassification').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Bank/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Bank_I",
                                    type: 'POST',
                                    data: JSON.stringify(Bank),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankPK").val() + "/" + $("#HistoryPK").val() + "/" + "Bank",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Bank = {
                                    BankPK: $('#BankPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    BICode: $('#BICode').val(),
                                    ClearingCode: $('#ClearingCode').val(),
                                    RTGSCode: $('#RTGSCode').val(),
                                    PTPCode: $('#PTPCode').val(),
                                    USDPTPCode: $('#USDPTPCode').val(),
                                    Country: $('#Country').val(),
                                    BitRDN: $('#BitRDN').val(),
                                    BitSyariah: $('#BitSyariah').val(),
                                    FeeLLG: $('#FeeLLG').val(),
                                    FeeRTGS: $('#FeeRTGS').val(),
                                    MinforRTGS: $('#MinforRTGS').val(),
                                    InterestDays: $('#InterestDays').val(),
                                    SinvestID: $('#SinvestID').val(),
                                    JournalRoundingMode: $('#JournalRoundingMode').val(),
                                    JournalDecimalPlaces: $('#JournalDecimalPlaces').val(),
                                    InterestDaysType: $('#InterestDaysType').val(),
                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                    PaymentModeOnMaturity: $('#PaymentModeOnMaturity').val(),
                                    PaymentInterestSpecificDate: $('#PaymentInterestSpecificDate').val(),
                                    NKPDCode: $('#NKPDCode').val(),
                                    IssuerPK: $('#IssuerPK').val(),
                                    CapitalClassification: $('#CapitalClassification').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Bank/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Bank_U",
                                    type: 'POST',
                                    data: JSON.stringify(Bank),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankPK").val() + "/" + $("#HistoryPK").val() + "/" + "Bank",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Bank" + "/" + $("#BankPK").val(),
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
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankPK").val() + "/" + $("#HistoryPK").val() + "/" + "Bank",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Bank = {
                                BankPK: $('#BankPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Bank_A",
                                type: 'POST',
                                data: JSON.stringify(Bank),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $.unblockUI();
                                    alertify.success(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            $.unblockUI();
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }
                    },
                    error: function (data) {
                        $.unblockUI();
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankPK").val() + "/" + $("#HistoryPK").val() + "/" + "Bank",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Bank = {
                                BankPK: $('#BankPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Bank_V",
                                type: 'POST',
                                data: JSON.stringify(Bank),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankPK").val() + "/" + $("#HistoryPK").val() + "/" + "Bank",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Bank = {
                                BankPK: $('#BankPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Bank/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Bank_R",
                                type: 'POST',
                                data: JSON.stringify(Bank),
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

    function getDataSourceListCountry() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
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
                             Code: { type: "string" },
                             DescOne: { type: "string" },

                         }
                     }
                 }
             });
    }

    $("#btnListCountry").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#Country";
        htmlCountryDesc = "#CountryDesc";
    });
    $("#btnClearListCountry").click(function () {
        $("#Country").val("");
        $("#CountryDesc").val("");
    });


    function initListCountry() {
        var dsListCountry = getDataSourceListCountry();
        $("#gridListCountry").empty();
        $("#gridListCountry").kendoGrid({
            dataSource: dsListCountry,
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
               { command: { text: "Select", click: ListCountrySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "Country", width: 200 }
            ]
        });
    }

    function onWinListCountryClose() {
        $("#gridListCountry").empty();
    }

    function ListCountrySelect(e) {
        var grid = $("#gridListCountry").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCountryDesc).val(dataItemX.DescOne);
        $(htmlCountry).val(dataItemX.Code);
        WinListCountry.close();
    }

    function hideSpecificdate(_type) {
        if (_type == "4" || _type == "5" || _type == "6") {
            $("#LblSpecificDate").show();
        }

        else {
            $("#LblSpecificDate").hide();
        }
    };

});
