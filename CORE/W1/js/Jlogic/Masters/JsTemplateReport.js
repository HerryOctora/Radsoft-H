$(document).ready(function () {
    document.title = 'FORM Fund Client Bank Default';
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
        $("#BtnInsertRow").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnDelete").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnImportTemplateReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

    }



    function initWindow() {

        win = $("#WinTemplateReport").kendoWindow({
            height: 600,
            title: "Template Report Detail",
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

        WinInsertRow = $("#WinInsertRow").kendoWindow({
            height: "300px",
            title: "Insert Row",
            visible: false,
            width: "700px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinTemplateReport").kendoValidator().data("kendoValidator");

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
                //$("#ReportName").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                //$("#ReportName").attr('readonly', true);
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

            $("#TemplateReportPK").val(dataItemX.TemplateReportPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            //$("#ReportName").val(dataItemX.ReportName);
            $("#Operator").val(dataItemX.Operator);
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

        //AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAccount,
                    value: setCmbAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbAccountPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountPK == 0) {
                    return "";
                } else {
                    return dataItemX.AccountPK;
                }
            }
        }

        $("#Operator").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "+", value: 1 },
                { text: "-", value: 2 },

            ],
            filter: "contains",
            change: OnChangeOperator,
            value: setCmbOperator(),
            suggest: true
        });

        
        function OnChangeOperator() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbOperator() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Operator == 0) {
                    return "";
                } else {
                    return dataItemX.Operator;
                }
            }
        }

        $("#Row").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setRow(),
        });

        function setRow() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Row == 0) {
                    return "";
                } else {
                    return dataItemX.Row;
                }
            }
        }

        $("#Col").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setCol(),
        });

        function setCol() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Col == 0) {
                    return "";
                } else {
                    return dataItemX.Col;
                }
            }
        }

        //ReportName
        $.ajax({
            url: window.location.origin + "/Radsoft/TemplateReport/GetTemplateNameCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReportName").kendoComboBox({
                    dataValueField: "ReportName",
                    dataTextField: "ReportName",
                    dataSource: data,
                    value: setCmbReportName()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        function setCmbReportName() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.ReportName;
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
        $("#TemplateReportPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ReportName").val("");
        $("#AccountPK").val("");
        $("#AccountID").val("");
        $("#Row").val("");
        $("#Col").val(""); 
        $("#Operator").val("");
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
                             TemplateReportPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             AccountPK: { type: "number" },
                             AccountID: { type: "string" },
                             ReportName: { type: "string" },
                             Row: { type: "number" },
                             Col: { type: "number" },
                             Operator: { type: "string" },
                             OperatorDesc: { type: "string" },
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
            var gridApproved = $("#gridTemplateReportApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridTemplateReportPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridTemplateReportHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var TemplateReportApprovedURL = window.location.origin + "/Radsoft/TemplateReport/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(TemplateReportApprovedURL);

        $("#gridTemplateReportApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TemplateReport"
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
                { field: "TemplateReportPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ReportName", title: "Report Name", width: 200 },
                { field: "AccountName", title: "Account", width: 200 },
                { field: "Row", title: "Row", width: 250 },
                { field: "Col", title: "Col", width: 250 },
                { field: "OperatorDesc", title: "Operator", width: 250 },
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
        $("#TabTemplateReport").kendoTabStrip({
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
                        var TemplateReportPendingURL = window.location.origin + "/Radsoft/TemplateReport/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(TemplateReportPendingURL);
                        $("#gridTemplateReportPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form TemplateReport"
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
                                { field: "TemplateReportPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ReportName", title: "Report Name", width: 200 },
                                { field: "AccountName", title: "Account", width: 200 },
                                { field: "Row", title: "Row", width: 250 },
                                { field: "Col", title: "Col", width: 250 },
                                { field: "OperatorDesc", title: "Operator", width: 250 },
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

                        var TemplateReportHistoryURL = window.location.origin + "/Radsoft/TemplateReport/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(TemplateReportHistoryURL);

                        $("#gridTemplateReportHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form TemplateReport"
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
                                { field: "TemplateReportPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ReportName", title: "Report Name", width: 200 },
                                { field: "AccountName", title: "Account", width: 200 },
                                { field: "Row", title: "Row", width: 250 },
                                { field: "Col", title: "Col", width: 250 },
                                { field: "OperatorDesc", title: "Operator", width: 250 },
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

    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var TemplateReport = {
                        ReportName: $('#ReportName').val(),
                        AccountPK: $('#AccountPK').val(),
                        Row: $('#Row').val(),
                        Col: $('#Col').val(),
                        Operator: $('#Operator').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TemplateReport/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_I",
                        type: 'POST',
                        data: JSON.stringify(TemplateReport),
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
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    var TemplateReport = {
                        TemplateReportPK: $('#TemplateReportPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        ReportName: $('#ReportName').val(),
                        AccountPK: $('#AccountPK').val(),
                        Row: $('#Row').val(),
                        Col: $('#Col').val(),
                        Operator: $('#Operator').val(),

                        Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TemplateReport/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_U",
                        type: 'POST',
                        data: JSON.stringify(TemplateReport),
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
                }
            });
        }
    });

    $("#BtnOldData").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateReportPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateReport",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "TemplateReport" + "/" + $("#TemplateReportPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateReportPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateReport",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateReport = {
                                TemplateReportPK: $('#TemplateReportPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateReport/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_A",
                                type: 'POST',
                                data: JSON.stringify(TemplateReport),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateReportPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateReport",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateReport = {
                                TemplateReportPK: $('#TemplateReportPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateReport/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_V",
                                type: 'POST',
                                data: JSON.stringify(TemplateReport),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateReportPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateReport",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateReport = {
                                TemplateReportPK: $('#TemplateReportPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateReport/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_R",
                                type: 'POST',
                                data: JSON.stringify(TemplateReport),
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


    function getDataSourceListFundClient() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                             FundClientPK: { type: "number" },
                             ID: { type: "string" },

                         }
                     }
                 }
             });
    }

    $("#BtnInsertRow").click(function () {

        $("#Action").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Insert", value: 1 },
                { text: "Delete", value: 2 },

            ],
            filter: "contains",
            change: OnChangeAction,
            suggest: true
        });


        function OnChangeAction() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($('#Action').val() == 1)
            {
                $("#lblCol").show();
                $("#lblAccountPK").show();
                $("#lblOperator").show();
            }
            else
            {
                $("#lblCol").hide();
                $("#lblAccountPK").hide();
                $("#lblOperator").hide();
            }
        }



        //AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpAccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAccount
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }



        //AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/TemplateReport/GetTemplateNameCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpTemplateName").kendoComboBox({
                    dataTextField: "ReportName",
                    filter: "contains",
                    suggest: true,
                    dataSource: data
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $("#UpRow").kendoNumericTextBox({
            format: "n0",
            decimals: 0
        });

        $("#UpCol").kendoNumericTextBox({
            format: "n0",
            decimals: 0
        });



        $("#UpOperator").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "+", value: 1 },
                { text: "-", value: 2 },

            ],
            filter: "contains",
            change: OnChangeOperator,
            suggest: true
        });


        function OnChangeOperator() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }



        WinInsertRow.center();
        WinInsertRow.open();
    });
    


    $("#BtnSave").click(function () {
        
            alertify.prompt("Are you sure want to Insert Row, please give notes:", "", function (e, str) {
                if (e) {

                    var _accountPK = 0;
                    if ($('#UpAccountPK').val() == 0 || $('#UpAccountPK').val() == null) {
                        _accountPK = 0;
                    } else {
                        _accountPK = $('#UpAccountPK').val();
                    }

                    $.ajax({
                        url: window.location.origin + "/Radsoft/TemplateReport/CheckData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UpTemplateName").data('kendoComboBox').text() + "/" + _accountPK + "/" + $('#UpRow').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false)
                            {
                                var TemplateReport = {
                                    ReportName: $("#UpTemplateName").data('kendoComboBox').text(),
                                    AccountPK: $('#UpAccountPK').val(),
                                    Row: $('#UpRow').val(),
                                    Col: $('#UpCol').val(),
                                    Operator: $('#UpOperator').val(),

                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/TemplateReport/InsertRow/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(TemplateReport),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
                                        WinInsertRow.close();
                                        refresh();
                                        clearDataInsert();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                            else
                            {
                                alertify.alert("data has add");
                            }
                            
                        }
                    });
                    
                }
            });
        
    });

    $("#BtnDelete").click(function () {
        alertify.prompt("Are you sure want to Delete Row, please give notes:", "", function (e, str) {
            if (e) {
                var TemplateReport = {
                    ReportName: $("#UpTemplateName").data('kendoComboBox').text(),
                    Row: $('#UpRow').val(),

                    Notes: str,
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/TemplateReport/DeleteRow/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(TemplateReport),
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
            }
        });

    });

    function clearDataInsert()
    {
        $('#UpTemplateName').val('');
        $('#UpAccountPK').val('');
        $('#UpRow').val('');
        $('#UpCol').val('');
        $('#UpOperator').val('');
    }


    $("#BtnImportTemplateReport").click(function () {
        document.getElementById("TemplateReport").click();
    });

    $("#TemplateReport").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#TemplateReport").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("TemplateReport", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateReport_Import" + "/01-01-1900",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#TemplateReport").val("");

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#TemplateReport").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#TemplateReport").val("");
        }
    });


});
