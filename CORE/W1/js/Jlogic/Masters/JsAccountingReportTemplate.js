$(document).ready(function () {
    document.title = 'FORM ACCOUNTING REPORT TEMPLATE';
    
    //Global Variabel
    var win, winCopyRecord;
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
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
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
            imageUrl: "../../Images/Icon/IcBtnApproved.png"
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

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/download1.png"
        });

        $("#BtnCopyRecord").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnCancel_CopyRecord").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
        });

        $("#BtnOK_CopyRecord").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
    }
          
    function initWindow() {
        win = $("#WinAccountingReportTemplate").kendoWindow({
            height: "1800px",
            title: "Accounting Report Template Detail",
            visible: false,
            width: "1000px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: "600px",
            title: "Old Data",
            visible: false,
            width: "500px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            }
        }).data("kendoWindow");

        winCopyRecord = $("#WinCopyRecord").kendoWindow({
            height: "275px",
            title: "Copy Record",
            visible: false,
            width: "750px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 50 })
            },
            close: onPopUpCloseCopyRecord
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinAccountingReportTemplate").kendoValidator().data("kendoValidator");
    function validateData() {        
        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        } else {
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
                $("#StatusHeader").val("HISTORY");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            $("#AccountingReportTemplatePK").val(dataItemX.AccountingReportTemplatePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(dataItemX.EntryTime);
            $("#UpdateTime").val(dataItemX.UpdateTime);
            $("#ApprovedTime").val(dataItemX.ApprovedTime);
            $("#VoidTime").val(dataItemX.VoidTime);
            $("#LastUpdate").val(dataItemX.LastUpdate);
        }

        // ComboBox Report Name
        $.ajax({
            url: window.location.origin + "/Radsoft/AccountingReportTemplate/GetReportNameCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReportName").kendoComboBox({
                    dataSource: data,
                    dataValueField: "ReportName",
                    dataTextField: "ReportName",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeReportName,
                    value: setCmbReportName()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeReportName() {
            //if (this.value() && this.selectedIndex == -1) {
            //    var dt = this.dataSource._data[0];
            //    this.text('');
            //}
        }
        function setCmbReportName() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ReportName == undefined || dataItemX.ReportName == "") {
                    return "";
                } else {
                    return dataItemX.ReportName;
                }
            }
        }

        // ComboBox Row Type
        $("#RowType").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "Header" },
               { text: "Child" },
               { text: "Total Header" }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeRowType,
            value: setCmbRowType()
        });
        function OnChangeRowType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbRowType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RowType == undefined || dataItemX.RowType == "") {
                    return "";
                } else {
                    return dataItemX.RowType;
                }
            }
        }

        // ComboBox Operator
        $("#Operator").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "+" },
               { text: "-" }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeOperator,
            value: setCmbOperator()
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
                if (dataItemX.Operator == undefined || dataItemX.Operator == "") {
                    return "";
                } else {
                    return dataItemX.Operator;
                }
            }
        }

        // ComboBox Source Account
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SourceAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeSourceAccount,
                    value: setCmbSourceAccount()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeSourceAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSourceAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SourceAccount == 0) {
                    return "";
                } else {
                    return dataItemX.SourceAccount;
                }
            }
        }
        
        $("#Row").kendoNumericTextBox({
            format: "n0",
            value: setRow()
        });
        function setRow() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.Row;
            }
        }

        $("#Column").kendoNumericTextBox({
            format: "n0",
            value: setColumn()
        });
        function setColumn() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.Column;
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

    var GlobValidatorCopyRecord = $("#WinCopyRecord").kendoValidator().data("kendoValidator");
    function validateDataCopyRecord() {        
        if (GlobValidatorCopyRecord.validate()) {
            //alert("Validation sucess");
            return 1;
        } else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    
    function onPopUpCloseCopyRecord() {
        GlobValidatorCopyRecord.hideMessages();
        clearDataCopyRecord();
        showButtonCopyRecord();
    }

    function clearDataCopyRecord() {
        $("#CopyRecord_RecordFrom").val("");
        $("#CopyRecord_NewSourceAccount").val("");
    }
    
    function showButtonCopyRecord() {
        $("#BtnOK_CopyRecord").show();
        $("#BtnCancel_CopyRecord").show();
    }

    function clearData() {
        $("#AccountingReportTemplatePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
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
                             AccountingReportTemplatePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },

                             ReportName: { type: "string" },
                             Row: { type: "number" },
                             Column: { type: "number" },
                             RowType: { type: "string" },
                             SourceAccount: { type: "number" },
                             SourceAccountName: { type: "string" },
                             Operator: { type: "string" },

                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "string" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "string" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "string" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "string" },
                             LastUpdate: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridAccountingReportTemplateApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAccountingReportTemplatePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAccountingReportTemplateHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var AccountingReportTemplateApprovedURL = window.location.origin + "/Radsoft/AccountingReportTemplate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(AccountingReportTemplateApprovedURL);

        $("#gridAccountingReportTemplateApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Accounting Report Template"
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
               { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
               { field: "AccountingReportTemplatePK", title: "SysNo.", filterable: false, width: 85 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },

               { field: "ReportName", title: "Report Name", width: 300 },
               { field: "Row", title: "<div style='text-align: right'>Row</div>", width: 150, attributes: { style: "text-align:right;" } },
               { field: "Column", title: "<div style='text-align: right'>Column</div>", width: 150, attributes: { style: "text-align:right;" } },
               { field: "RowType", title: "Row Type", width: 200 },
               { field: "SourceAccount", title: "Source Account", width: 150, hidden: true },
               { field: "SourceAccountName", title: "Source Account Name", width: 250 },
               { field: "Operator", title: "Operator (+/-)", width: 200 },

               { field: "EntryUsersID", title: "EntryID", width: 120 },
               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
               { field: "UpdateUsersID", title: "UpdateID", width: 120 },
               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
               { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
               { field: "VoidUsersID", title: "VoidID", width: 120 },
               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabAccountingReportTemplate").kendoTabStrip({
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
                        var AccountingReportTemplatePendingURL = window.location.origin + "/Radsoft/AccountingReportTemplate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(AccountingReportTemplatePendingURL);
                        $("#gridAccountingReportTemplatePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Accounting Report Template"
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
                               { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
                               { field: "AccountingReportTemplatePK", title: "SysNo.", filterable: false, width: 85 },
                               { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },

                               { field: "ReportName", title: "Report Name", width: 300 },
                               { field: "Row", title: "<div style='text-align: right'>Row</div>", width: 150, attributes: { style: "text-align:right;" } },
                               { field: "Column", title: "<div style='text-align: right'>Column</div>", width: 150, attributes: { style: "text-align:right;" } },
                               { field: "RowType", title: "Row Type", width: 200 },
                               { field: "SourceAccount", title: "Source Account", width: 150, hidden: true },
                               { field: "SourceAccountName", title: "Source Account Name", width: 250 },
                               { field: "Operator", title: "Operator (+/-)", width: 200 },

                               { field: "EntryUsersID", title: "EntryID", width: 120 },
                               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "UpdateUsersID", title: "UpdateID", width: 120 },
                               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
                               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "VoidUsersID", title: "VoidID", width: 120 },
                               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var AccountingReportTemplateHistoryURL = window.location.origin + "/Radsoft/AccountingReportTemplate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3,
                          dataSourceHistory = getDataSource(AccountingReportTemplateHistoryURL);

                        $("#gridAccountingReportTemplateHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Accounting Report Template"
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
                               { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
                               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                               { field: "AccountingReportTemplatePK", title: "SysNo.", filterable: false, width: 85 },
                               { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                               { field: "StatusDesc", title: "Status", width: 200 },
                               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                               
                               { field: "ReportName", title: "Report Name", width: 300 },
                               { field: "Row", title: "<div style='text-align: right'>Row</div>", width: 150, attributes: { style: "text-align:right;" } },
                               { field: "Column", title: "<div style='text-align: right'>Column</div>", width: 150, attributes: { style: "text-align:right;" } },
                               { field: "RowType", title: "Row Type", width: 200 },
                               { field: "SourceAccount", title: "Source Account", width: 150, hidden: true },
                               { field: "SourceAccountName", title: "Source Account Name", width: 250 },
                               { field: "Operator", title: "Operator (+/-)", width: 200 },

                               { field: "EntryUsersID", title: "EntryID", width: 120 },
                               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "UpdateUsersID", title: "UpdateID", width: 120 },
                               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
                               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                               { field: "VoidUsersID", title: "VoidID", width: 120 },
                               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
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
        var grid = $("#gridBoardHistory").data("kendoGrid");
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
                    var AccountingReportTemplate = {
                        ReportName: $('#ReportName').val(),
                        Row: $('#Row').val(),
                        Column: $('#Column').val(),
                        RowType: $('#RowType').val(),
                        SourceAccount: $('#SourceAccount').val(),
                        Operator: $('#Operator').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_I",
                        type: 'POST',
                        data: JSON.stringify(AccountingReportTemplate),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountingReportTemplatePK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountingReportTemplate",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == data) {
                                var AccountingReportTemplate = {
                                    AccountingReportTemplatePK: $('#AccountingReportTemplatePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ReportName: $('#ReportName').val(),
                                    Row: $('#Row').val(),
                                    Column: $('#Column').val(),
                                    RowType: $('#RowType').val(),
                                    SourceAccount: $('#SourceAccount').val(),
                                    Operator: $('#Operator').val(),
                                    Notes: str,
                                    UpdateUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_U",
                                    type: 'POST',
                                    data: JSON.stringify(AccountingReportTemplate),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountingReportTemplatePK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountingReportTemplate",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "AccountingReportTemplate" + "/" + $("#AccountingReportTemplatePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountingReportTemplatePK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountingReportTemplate",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == data) {
                            var AccountingReportTemplate = {
                                AccountingReportTemplatePK: $('#AccountingReportTemplatePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_A",
                                type: 'POST',
                                data: JSON.stringify(AccountingReportTemplate),
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
                var AccountingReportTemplate = {
                    AccountingReportTemplatePK: $('#AccountingReportTemplatePK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_V",
                    type: 'POST',
                    data: JSON.stringify(AccountingReportTemplate),
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
    });

    $("#BtnReject").click(function () {        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var AccountingReportTemplate = {
                    AccountingReportTemplatePK: $('#AccountingReportTemplatePK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_R",
                    type: 'POST',
                    data: JSON.stringify(AccountingReportTemplate),
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
    });

    $("#BtnCopyRecord").click(function () {
        GlobValidatorCopyRecord.hideMessages();
        clearDataCopyRecord();
        showButtonCopyRecord();

        // ComboBox Record From
        $.ajax({
            url: window.location.origin + "/Radsoft/AccountingReportTemplate/GetRecordFromCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CopyRecord_RecordFrom").kendoComboBox({
                    dataSource: data,
                    dataValueField: "AccountingReportTemplatePK",
                    dataTextField: "RecordFrom",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeRecordFrom
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeRecordFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        // ComboBox New Source Account
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CopyRecord_NewSourceAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeNewSourceAccount
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeNewSourceAccount() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        winCopyRecord.center();
        winCopyRecord.open();
    });

    $("#BtnCancel_CopyRecord").click(function () {
        alertify.confirm("Are you sure want to cancel copy record?", function (e) {
            if (e) {
                winCopyRecord.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnOK_CopyRecord").click(function () {
        var val = validateDataCopyRecord();
        if (val == 1) {
            alertify.confirm("Are you sure want to copy record?", function (e) {
                if (e) {
                    var AccountingReportTemplate = {
                        RecordFrom: $('#CopyRecord_RecordFrom').val(),
                        NewSourceAccount: $('#CopyRecord_NewSourceAccount').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AccountingReportTemplate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingReportTemplate_CopyRecord",
                        type: 'POST',
                        data: JSON.stringify(AccountingReportTemplate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            winCopyRecord.close();
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

});
