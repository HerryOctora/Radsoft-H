//import { type } from "os";

$(document).ready(function () {
    document.title = 'FORM FUND EXPOSURE';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var GlobType;
    var GlobTypeDesc;
    var GlobFundPK;
    var GlobFundID;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    if (_GlobClientCode == "02") {
        $("#LblMinExposure").show();
        $("#LblMaxExposure").show();
        $("#LblWarningMinExposurePercent").show();
        $("#LblWarningMaxExposurePercent").show();
        $("#LblWarningMaxValue").show();
        $("#LblMaxValue").show();
        $("#MinExposurePercent").attr("required", false);
        $("#MaxExposurePercent").attr("required", false);
        $("#WarningMinExposurePercent").attr("required", false);
        $("#WarningMaxExposurePercent").attr("required", false);
        $("#MaxValue").attr("required", false);
        
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

        win = $("#WinFundExposure").kendoWindow({
            height: 600,
            title: "Fund Exposure Detail",
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

    }

    var GlobValidator = $("#WinFundExposure").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.notify("Validation not Pass");
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
            GlobType = 0;
            GlobTypeDesc = 0;
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


            $("#FundExposurePK").val(dataItemX.FundExposurePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            GlobType = dataItemX.Type;
            GlobTypeDesc = dataItemX.TypeDesc;
            GlobFundPK = dataItemX.FundPK;
            GlobFundID = dataItemX.FundID;
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));

            $("#Parameter").attr("disabled", false);
            $("#LblWarningMinExposurePercent").show();
            $("#LblMinExposure").show();
            if (GlobTypeDesc == "EQUITY PER FUND COMPARE TO MARKET CAP") {
                $("#LblMinExposure").hide();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").hide();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").show();
                $("#LblMaxValue").show();

                //$("#Parameter").data("kendoComboBox").value(0);
                //$("#Parameter").data("kendoComboBox").text("ALL");
                $("#Parameter").attr("disabled", true);
            }
        }
        //ucup
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 'ExposureType',
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeType,
                    dataSource: data,
                    value: setCmbType()


                });

            },

            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

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

        function onChangeType() {
            $("#FundPK").val("");
            $("#FundPK").text("");
            $("#FundPK").data("kendoComboBox").value("");
            $("#FundPK").data("kendoComboBox").text("");

            $("#Parameter").val("");
            $("#Parameter").text("");
            $("#Parameter").data("kendoComboBox").value("");
            $("#Parameter").data("kendoComboBox").text("");
            $("#Parameter").attr("disabled", false);

            $("#LblMinExposure").show();
            $("#LblMaxExposure").show();
            $("#LblWarningMinExposurePercent").show();
            $("#LblWarningMaxExposurePercent").show();
            $("#LblWarningMaxValue").show();
            $("#LblMaxValue").show();

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            GlobType = this.value();
            GlobTypeDesc = this.text();
            hideexposure($('#Type').val());

            if (GlobTypeDesc == "SYARIAH ONLY") {
                ResetHideExposure();

            }
            else if (GlobTypeDesc == "EQUITY UNIVERSAL BASED ON INDEX") {
                ResetHideExposure();

            }

            else if (GlobTypeDesc == "COUNTERPART EXPOSURE") {
                ResetHideExposure();
                $("#LblMaxExposure").show();
                $("#LblWarningMaxExposurePercent").show();

            }

            else if (GlobTypeDesc == "EQUITY ALL FUND COMPARE TO MARKET CAP") {
                ResetHideExposure();
                $("#LblMaxExposure").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").show();
                $("#LblMaxValue").show();

            }
            else if (GlobTypeDesc == "CAMEL SCORE BANK PER FUND") {
                ResetHideExposure();

            }
            else if (GlobTypeDesc == "BOND RATING") {
                ResetHideExposure();

            }

            else if (GlobTypeDesc == "TOTAL FOREIGN PORTFOLIO PER FUND") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();

            }

            else if (GlobTypeDesc == "INVESTMENT OTHER THAN DEPOSIT") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
            }
            else if (GlobTypeDesc == "AFFILIATED INVESTMENT") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
            }
            else if (GlobTypeDesc == "TOTAL AUM ALL FUND") {
                ResetHideExposure();
                $("#LblMaxValue").show();
                $("#LblMinValue").show();
            }
            else {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").show();
                $("#LblMaxValue").show();
                $("#LblMinValue").hide();
            }

            //ucup tambahan untuk fund
            $.ajax({
                url: window.location.origin + "/Radsoft/FundExposure/GetExposureIDByTypeForFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobTypeDesc,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#FundPK").kendoComboBox({
                        dataValueField: "FundPK",
                        dataTextField: "Name",
                        filter: "contains",
                        suggest: true,
                        change: onChangeFund,
                        dataSource: data,
                        value: setCmbFundPK()
                    });
                },
                error: function (data) {
                    alertify.alert("Please Choose Type First");
                }
            });

            function onChangeFund() {

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


            //ucup
            $.ajax({
                url: window.location.origin + "/Radsoft/FundExposure/GetExposureIDByTypeForParameter/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobTypeDesc,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Parameter").kendoComboBox({
                        dataValueField: "Parameter",
                        dataTextField: "ParameterDesc",
                        filter: "contains",
                        suggest: true,
                        change: onChangeParameter,
                        dataSource: data,
                        dataSource: data,
                        //value: setCmbParameter()
                    });
                },
                error: function (data) {
                    alertify.alert("Please Choose Type First");
                }
            });

            function onChangeParameter() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }

        }






        //FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundExposure/GetExposureIDByTypeForFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobTypeDesc,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "Name",
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    dataSource: data,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            //$("#Type").val("");
            //$("#Type").text("");
            //$("#Type").data("kendoComboBox").value("");
            //$("#Type").data("kendoComboBox").text("");
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            GlobFundPK = this.value();
            GlobFundID = this.text();


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

        if (e != null) {
            hideexposure($('#Type').val());
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/FundExposure/GetExposureIDByTypeForParameter/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobTypeDesc,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Parameter").kendoComboBox({
                    dataValueField: "Parameter",
                    dataTextField: "ParameterDesc",
                    filter: "contains",
                    suggest: true,
                    change: onChangeParameter,
                    dataSource: data,
                    value: setCmbParameter()
                });
            },
            error: function (data) {
                alertify.alert("Please Choose Type First");
            }
        });

        function onChangeParameter() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbParameter() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Parameter == 0) {
                    return "ALL";
                } else {
                    return dataItemX.Parameter;
                }
            }
        }


        $("#MinExposurePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setMinExposurePercent()
        });
        function setMinExposurePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinExposurePercent;
            }
        }

        $("#MaxExposurePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setMaxExposurePercent()
        });
        function setMaxExposurePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MaxExposurePercent;
            }
        }

        $("#WarningMinExposurePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setWarningMinExposurePercent()
        });
        function setWarningMinExposurePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.WarningMinExposurePercent;
            }
        }

        $("#WarningMaxExposurePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setWarningMaxExposurePercent()
        });
        function setWarningMaxExposurePercent() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.WarningMaxExposurePercent;
            }
        }


        $("#MinValue").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setMinValue()
        });
        function setMinValue() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MinValue;
            }
        }

        $("#MaxValue").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setMaxValue()
        });
        function setMaxValue() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.MaxValue;
            }
        }

        $("#WarningMinValue").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setWarningMinValue()
        });
        function setWarningMinValue() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.WarningMinValue;
            }
        }

        $("#WarningMaxValue").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setWarningMaxValue()
        });
        function setWarningMaxValue() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.WarningMaxValue;
            }
        }


        if (e != null) {
            hideexposure(dataItemX.Type)
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
        $("#FundExposurePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundPK").val("");
        $("#Type").val("");
        $("#Parameter").val("");
        $("#MinExposurePercent").val("");
        $("#MaxExposurePercent").val("");
        $("#WarningMinExposurePercent").val("");
        $("#WarningMaxExposurePercent").val("");
        $("#MinValue").val("");
        $("#MaxValue").val("");
        $("#WarningMinValue").val("");
        $("#WarningMaxValue").val("");
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
                             FundExposurePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Type: { type: "number" },
                             TypeDesc: { type: "string" },
                             Parameter: { type: "number" },
                             ParameterDesc: { type: "string" },
                             MinExposurePercent: { type: "number" },
                             MaxExposurePercent: { type: "number" },
                             WarningMinExposurePercent: { type: "number" },
                             WarningMaxExposurePercent: { type: "number" },
                             MinValue: { type: "number" },
                             MaxValue: { type: "number" },
                             WarningMinValue: { type: "number" },
                             WarningMaxValue: { type: "number" },
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
            var gridApproved = $("#gridFundExposureApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundExposurePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundExposureHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundExposureApprovedURL = window.location.origin + "/Radsoft/FundExposure/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FundExposureApprovedURL);

        $("#gridFundExposureApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Exposure"
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
                { field: "FundExposurePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "TypeDesc", title: "Type", width: 300 },
                { field: "ParameterDesc", title: "Parameter", width: 300 },
                {
                    field: "MinExposurePercent", title: "Min Exposure Percent %", width: 200,
                    template: "#: MinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MaxExposurePercent", title: "Max Exposure Percent %", width: 200,
                    template: "#: MaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMinExposurePercent", title: "Warning Min Exposure Percent %", width: 200,
                    template: "#: WarningMinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMaxExposurePercent", title: "Warning Max Exposure Percent %", width: 200,
                    template: "#: WarningMaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                  { field: "MinValue", title: "Min. Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                  { field: "MaxValue", title: "Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                  { field: "WarningMinValue", title: "Warning Min Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                  { field: "WarningMaxValue", title: "Warning Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        $("#TabFundExposure").kendoTabStrip({
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
                        var FundExposurePendingURL = window.location.origin + "/Radsoft/FundExposure/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FundExposurePendingURL);
                        $("#gridFundExposurePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Exposure"
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
                                { field: "FundExposurePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "ParameterDesc", title: "Parameter", width: 300 },
                                {
                                    field: "MinExposurePercent", title: "Min Exposure Percent %", width: 200,
                                    template: "#: MinExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "MaxExposurePercent", title: "Max Exposure Percent %", width: 200,
                                    template: "#: MaxExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WarningMinExposurePercent", title: "Warning Min Exposure Percent %", width: 200,
                                    template: "#: WarningMinExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WarningMaxExposurePercent", title: "Warning Max Exposure Percent %", width: 200,
                                    template: "#: WarningMaxExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                              { field: "MinValue", title: "Min. Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "MaxValue", title: "Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "WarningMinValue", title: "Warning Min Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "WarningMaxValue", title: "Warning Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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

                        var FundExposureHistoryURL = window.location.origin + "/Radsoft/FundExposure/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FundExposureHistoryURL);

                        $("#gridFundExposureHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Exposure"
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
                                { field: "FundExposurePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "ParameterDesc", title: "Parameter", width: 300 },
                                {
                                    field: "MinExposurePercent", title: "Min Exposure Percent %", width: 200,
                                    template: "#: MinExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "MaxExposurePercent", title: "Max Exposure Percent %", width: 200,
                                    template: "#: MaxExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WarningMinExposurePercent", title: "Warning Min Exposure Percent %", width: 200,
                                    template: "#: WarningMinExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "WarningMaxExposurePercent", title: "Warning Max Exposure Percent %", width: 200,
                                    template: "#: WarningMaxExposurePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                              { field: "MinValue", title: "Min. Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "MaxValue", title: "Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "WarningMinValue", title: "Warning Min Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                              { field: "WarningMaxValue", title: "Warning Max Value", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridFundExposureHistory").data("kendoGrid");
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


    $("#BtnAdd").click(function () {
        if ($("#Parameter").data("kendoComboBox").text() == "") {
            if ($("#Type").data("kendoComboBox").text() == "INSTRUMENT TYPE GROUP"
                || $("#Type").data("kendoComboBox").text() == "BOND"
                || $("#Type").data("kendoComboBox").text() == "SECTOR"
                || $("#Type").data("kendoComboBox").text() == "INSTRUMENT TYPE"
                || $("#Type").data("kendoComboBox").text() == "EQUITY"
                || $("#Type").data("kendoComboBox").text() == "ALL FUND PER BANK"
                || $("#Type").data("kendoComboBox").text() == "PER FUND PER BANK"
                || $("#Type").data("kendoComboBox").text() == "ISSUER"
                || $("#Type").data("kendoComboBox").text() == "INDEX"
                || $("#Type").data("kendoComboBox").text() == "SYARIAH ONLY"
                || $("#Type").data("kendoComboBox").text() == "TOTAL PORTFOLIO PER FUND"
                || $("#Type").data("kendoComboBox").text() == "EQUITY ALL FUND COMPARE TO MARKET CAP"
                || $("#Type").data("kendoComboBox").text() == "ISSUER ALL FUND"
                || $("#Type").data("kendoComboBox").text() == "MUTUAL FUND PER COUNTERPART"
                || $("#Type").data("kendoComboBox").text() == "EQUITY UNIVERSAL BASED ON INDEX"
                || $("#Type").data("kendoComboBox").text() == "DIRECT INVESTMENT"
                || $("#Type").data("kendoComboBox").text() == "LAND AND PROPERTY"
                || $("#Type").data("kendoComboBox").text() == "CAMEL SCORE BANK PER FUND"
                || $("#Type").data("kendoComboBox").text() == "BOND RATING"
                || $("#Type").data("kendoComboBox").text() == "TOTAL FOREIGN PORTFOLIO PER FUND"
                || $("#Type").data("kendoComboBox").text() == "KIK EBA PER COUNTERPART"
                || $("#Type").data("kendoComboBox").text() == "INVESTMENT OTHER THAN DEPOSIT"
                || $("#Type").data("kendoComboBox").text() == "AFFILIATED INVESTMENT"
            ) {
                alertify.alert("Parameter Must Be Fill First!");
                return;
            }
            else {
                _btnAddExposure();
            }
        }
        else {
            _btnAddExposure();
        }

    });

    function _btnAddExposure() {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var _param;
                    if ($("#Parameter").val() == "" && $("#Parameter").val() != null) {
                        _param = 0;
                    } else {
                        _param = $("#Parameter").val();
                    }
                
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundExposure/CheckExistingID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val() + "/" + $("#Type").val() + "/" + _param,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var FundExposure = {
                                    FundPK: $('#FundPK').val(),
                                    Type: $('#Type').val(),
                                    Parameter: $('#Parameter').val(),
                                    MinExposurePercent: $('#MinExposurePercent').val(),
                                    MaxExposurePercent: $('#MaxExposurePercent').val(),
                                    WarningMinExposurePercent: $('#WarningMinExposurePercent').val(),
                                    WarningMaxExposurePercent: $('#WarningMaxExposurePercent').val(),
                                    MinValue: $('#MinValue').val(),
                                    MaxValue: $('#MaxValue').val(),
                                    WarningMinValue: $('#WarningMinValue').val(),
                                    WarningMaxValue: $('#WarningMaxValue').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundExposure/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundExposure_I",
                                    type: 'POST',
                                    data: JSON.stringify(FundExposure),
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
                                alertify.alert("Exposure already Exist!");
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

    $("#BtnUpdate").click(function () {
        if ($("#Parameter").data("kendoComboBox").text() == "") {
            if (($("#Type").data("kendoComboBox").text() == "ALL FUND PER BANK" && $("#Parameter").data("kendoComboBox").text() == "") || ($("#Type").data("kendoComboBox").text() == "PER FUND PER BANK" && $("#Parameter").data("kendoComboBox").text() == "")) {
                alertify.alert("Parameter Must Be Fill First!");
                return;
            }
            else {
                _btnUpdateExposure();
            }

        }
        else {
            _btnUpdateExposure();
        }

       

    });

    function _btnUpdateExposure() {
        var val = validateData();
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundExposurePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundExposure",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                                if (e) {
                                   
                                                var FundExposure = {
                                                    FundExposurePK: $('#FundExposurePK').val(),
                                                    HistoryPK: $('#HistoryPK').val(),
                                                    FundPK: $('#FundPK').val(),
                                                    Type: $('#Type').val(),
                                                    Parameter: $('#Parameter').val(),
                                                    MinExposurePercent: $('#MinExposurePercent').val(),
                                                    MaxExposurePercent: $('#MaxExposurePercent').val(),
                                                    WarningMinExposurePercent: $('#WarningMinExposurePercent').val(),
                                                    WarningMaxExposurePercent: $('#WarningMaxExposurePercent').val(),
                                                    MinValue: $('#MinValue').val(),
                                                    MaxValue: $('#MaxValue').val(),
                                                    WarningMinValue: $('#WarningMinValue').val(),
                                                    WarningMaxValue: $('#WarningMaxValue').val(),
                                                    Notes: str,
                                                    EntryUsersID: sessionStorage.getItem("user")
                                                };
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/FundExposure/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundExposure_U",
                                                    type: 'POST',
                                                    data: JSON.stringify(FundExposure),
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

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundExposurePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundExposure",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundExposure" + "/" + $("#FundExposurePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundExposurePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundExposure",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var FundExposure = {
                                FundExposurePK: $('#FundExposurePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundExposure/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundExposure_A",
                                type: 'POST',
                                data: JSON.stringify(FundExposure),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundExposurePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundExposure",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var FundExposure = {
                                FundExposurePK: $('#FundExposurePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundExposure/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundExposure_V",
                                type: 'POST',
                                data: JSON.stringify(FundExposure),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundExposurePK").val() + "/" + $("#HistoryPK").val() + "/" + "FundExposure",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var FundExposure = {
                                FundExposurePK: $('#FundExposurePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundExposure/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundExposure_R",
                                type: 'POST',
                                data: JSON.stringify(FundExposure),
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


    function hideexposure(_type) {
        if (_GlobClientCode == "02") {
            if (_type == "9") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").show();
                $("#LblMaxValue").show();
            } else {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").hide();
                $("#LblMaxValue").hide();
            }
        }
        else if (_GlobClientCode == "00") {
            if (_type == "15") {
                ResetHideExposure();
                $("#LblMinExposure").hide();
                $("#LblMaxExposure").hide();
                $("#LblWarningMinExposurePercent").hide();
                $("#LblWarningMaxExposurePercent").hide();
                $("#LblWarningMaxValue").hide();
                $("#LblMaxValue").hide();
            }
            else {
                ResetHideExposure();
                $("#LblMinExposure").hide();
                $("#LblMaxExposure").hide();
                $("#LblWarningMinExposurePercent").hide();
                $("#LblWarningMaxExposurePercent").hide();
                $("#LblWarningMaxValue").hide();
                $("#LblMaxValue").hide();
            }

            if (_type == "19") {
                ResetHideExposure();
                $("#LblMinExposure").hide();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").hide();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").hide();
                $("#LblMaxValue").hide();
            } else {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").hide();
                $("#LblMaxValue").hide();
            }
        }
        else {
            if (_type == "15") {
                ResetHideExposure();

            }
            else if (_type == "24") {
                ResetHideExposure();

            }
            else if (_type == "25") {
                ResetHideExposure();

            }
            else if (_type == "26") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();

            }
            else if (_type == "28") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();

            }
            else if (_type == "29") {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();

            }

            else if (_type == "30") {
                ResetHideExposure();
                $("#LblMinValue").show();
                $("#LblMaxValue").show();
            }
            else {
                ResetHideExposure();
                $("#LblMinExposure").show();
                $("#LblMaxExposure").show();
                $("#LblWarningMinExposurePercent").show();
                $("#LblWarningMaxExposurePercent").show();
                $("#LblWarningMaxValue").show();
                $("#LblMaxValue").show();
            }
        }


    };


    function ResetHideExposure() {
        $("#LblMinExposure").hide();
        $("#LblMaxExposure").hide();
        $("#LblWarningMinExposurePercent").hide();
        $("#LblWarningMaxExposurePercent").hide();
        $("#LblWarningMaxValue").hide();
        $("#LblMaxValue").hide();
        $("#LblMinValue").hide();
    }

});
