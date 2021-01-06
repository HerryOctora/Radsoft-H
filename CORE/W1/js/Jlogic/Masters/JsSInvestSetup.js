$(document).ready(function () {
    document.title = 'FORM SINVEST SETUP';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 300;
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

    function initWindow() {
        win = $("#WinSInvestSetup").kendoWindow({
            height: 450,
            title: "SInvest Setup Detail",
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
    var GlobValidator = $("#WinSInvestSetup").kendoValidator().data("kendoValidator");

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
            $("#StatusHeader").text("NEW");
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

            $("#SInvestSetupPK").val(dataItemX.SInvestSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#SinvestMoneyMarketFeePercent").val(dataItemX.SinvestMoneyMarketFeePercent);
            $("#SinvestBondFeePercent").val(dataItemX.SinvestBondFeePercent);
            $("#SinvestEquityFeePercent").val(dataItemX.SinvestEquityFeePercent);
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

        $("#SInvestFeeDays").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setSInvestFeeDays()
        });
        function setSInvestFeeDays() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.SInvestFeeDays;
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
        $("#SInvestSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#SinvestMoneyMarketFeePercent").val("");
        $("#SinvestBondFeePercent").val("");
        $("#SinvestEquityFeePercent").val(""); 
        $("#SInvestFeeDays").val("");
        $("#FundPK").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#LastUpdateTime").val("");

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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             SInvestSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             SinvestMoneyMarketFeePercent: { type: "decimal" },
                             SinvestBondFeePercent: { type: "decimal" },
                             SinvestEquityFeePercent: { type: "decimal" },
                             SInvestFeeDays: { type: "decimal" },
                             FundPK: { type: "number" },
                             //PeriodPK: { type: "number" },
                             //PeriodPKDesc: { type: "string" },
                             //DecimalPlaces: { type: "number" },
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
        if (tabindex == undefined) {
            tabindex = 0;
        }
        if (tabindex == 0) {
            var gridApproved = $("#gridSInvestSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridSInvestSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridSInvestSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }
    function initGrid() {
        var SInvestSetupApprovedURL = window.location.origin + "/Radsoft/SInvestSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(SInvestSetupApprovedURL);

        $("#gridSInvestSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form SInvest Setup"
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
                { field: "SInvestSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
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
                    field: "SInvestFeeDays", title: "S-Invest Fee Days", width: 200,
                    template: "#: SInvestFeeDays  # Days",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    field: "FundPK", title: "S-Invest Fee Days", width: 200,
                //    template: "#: FundPK  # Days",
                //    attributes: { style: "text-align:right;" }
                //},
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
        $("#TabSInvestSetup").kendoTabStrip({
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
                        var SInvestSetupPendingURL = window.location.origin + "/Radsoft/SInvestSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(SInvestSetupPendingURL);
                        $("#gridSInvestSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form SInvest Setup"
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
                                { field: "SInvestSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
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
                                    field: "SInvestFeeDays", title: "S-Invest Fee Days", width: 200,
                                    template: "#: SInvestFeeDays  # Days",
                                    attributes: { style: "text-align:right;" }
                                },
                                //{
                                //    field: "FundPK", title: "S-Invest Fee Days", width: 200,
                                //    template: "#: FundPK  # Days",
                                //    attributes: { style: "text-align:right;" }
                                //},
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

                        var SInvestSetupHistoryURL = window.location.origin + "/Radsoft/SInvestSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(SInvestSetupHistoryURL);

                        $("#gridSInvestSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Users"
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
                                { field: "SInvestSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
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
                                    field: "SInvestFeeDays", title: "S-Invest Fee Days", width: 200,
                                    template: "#: SInvestFeeDays  # Days",
                                    attributes: { style: "text-align:right;" }
                                },
                                //{
                                //    field: "FundPK", title: "S-Invest Fee Days", width: 200,
                                //    template: "#: FundPK  # Days",
                                //    attributes: { style: "text-align:right;" }
                                //},
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
        var grid = $("#gridUsersHistory").data("kendoGrid");
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
    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SInvestSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SInvestSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "SInvestSetup" + "/" + $("#SInvestSetupPK").val(),
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


    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
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
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "SInvestSetup",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if (data == true) {
                                var SInvestSetup = {
                                    SinvestMoneyMarketFeePercent: $('#SinvestMoneyMarketFeePercent').val(),
                                    SinvestBondFeePercent: $('#SinvestBondFeePercent').val(),
                                    SinvestEquityFeePercent: $('#SinvestEquityFeePercent').val(),
                                    SInvestFeeDays: $('#SInvestFeeDays').val(),
                                    FundPK: $('#FundPK').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SInvestSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SInvestSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(SInvestSetup),
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
                    //            alertify.alert("Data has exist, no more addition!");
                    //            win.close();
                    //            refresh();
                    //        }
                    //    },
                    //    error: function (data) {
                    //        alertify.alert(data.responseText);
                    //    }
                    //});
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SInvestSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SInvestSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var SInvestSetup = {
                                    SInvestSetupPK: $('#SInvestSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    SinvestMoneyMarketFeePercent: $('#SinvestMoneyMarketFeePercent').val(),
                                    SinvestBondFeePercent: $('#SinvestBondFeePercent').val(),
                                    SinvestEquityFeePercent: $('#SinvestEquityFeePercent').val(),
                                    SInvestFeeDays: $('#SInvestFeeDays').val(),
                                    FundPK: $('#FundPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SInvestSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SInvestSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(SInvestSetup),
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

    $("#BtnApproved").click(function () {

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SInvestSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SInvestSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            //$.ajax({
                            //    url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "SInvestSetup",
                            //    type: 'GET',
                            //    contentType: "application/json;charset=utf-8",
                            //    success: function (data) {
                            //        if (data == true) {
                                        var SInvestSetup = {
                                            SInvestSetupPK: $('#SInvestSetupPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/SInvestSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SInvestSetup_A",
                                            type: 'POST',
                                            data: JSON.stringify(SInvestSetup),
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
                            //            alertify.alert("Data has exist, no more addition!");
                            //            win.close();
                            //            refresh();
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
        });


    });

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SInvestSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SInvestSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SInvestSetup = {
                                SInvestSetupPK: $('#SInvestSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SInvestSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SInvestSetup_V",
                                type: 'POST',
                                data: JSON.stringify(SInvestSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SInvestSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SInvestSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SInvestSetup = {
                                SInvestSetupPK: $('#SInvestSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SInvestSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SInvestSetup_R",
                                type: 'POST',
                                data: JSON.stringify(SInvestSetup),
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






});