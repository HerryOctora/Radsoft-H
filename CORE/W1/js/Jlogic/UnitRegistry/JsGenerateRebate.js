$(document).ready(function () {
    document.title = 'FORM GenerateRebate';
    //Global Variabel
    var win;
    var WinTempGenerateRebate;
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

        $("#BtnGenerateRebate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnOkTempGenerateRebate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCancelTempGenerateRebate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnInsertGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCloseGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

    }

    

    function initWindow() {
        $("#PeriodFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value:''
        });
        $("#PeriodTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: ''
        });
        win = $("#WinGenerateRebate").kendoWindow({
            height: 900,
            title: "Rebate",
            visible: false,
            width: 1250,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //WinGenerateRebateDetail = $("#WinGenerateRebateDetail").kendoWindow({
        //    height: 500,
        //    title: "Rebate",
        //    visible: false,
        //    width: 1300,
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //}).data("kendoWindow");
        

        WinShowGenerateRebateDetail = $("#WinShowGenerateRebateDetail").kendoWindow({
            height: 300,
            title: "Rebate",
            visible: false,
            width: 800,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");
        WinTempGenerateRebate = $("#WinTempGenerateRebate").kendoWindow({
            height: 300,
            title: "Rebate",
            visible: false,
            width: 800,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

        WinTempGenerateRebateDetail = $("#WinTempGenerateRebateDetail").kendoWindow({
            height: 500,
            title: "Rebate Detail",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
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

    var GlobValidator = $("#WinGenerateRebate").kendoValidator().data("kendoValidator");

    function validateData() {
        
        //if ($("#PeriodFrom").val() != "" || $("#PeriodTo").val() != "") {
        //    var PeriodFrom = Date.parse($("#PeriodFrom").data("kendoDatePicker").value());
        //    var PeriodTo = Date.parse($("#PeriodTo").data("kendoDatePicker").value());

        //    //Check if Date parse is successful
        //    if (!_PeriodFrom || !_PeriodTo) {
        //        
        //        alertify.alert("Wrong Format Date");
        //        return 0;
        //    }
        //}
        //if (GlobValidator.validate()) {
        //    //alert("Validation sucess");
        //    return 1;
        //}
        //else {
        //    alertify.alert("Validation not Pass");
        //    return 0;
        //}
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

            $("#GenerateRebatePK").val(dataItemX.GenerateRebatePK);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#PeriodFrom").data("kendoDatePicker").value(dataItemX.PeriodFrom);
            $("#PeriodTo").data("kendoDatePicker").value(dataItemX.PeriodTo);
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
        $("#GenerateRebatePK").val("");
        $("#FundPK").val("");
        $("#FundClientPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodFrom").data("kendoDatePicker").value(null);
        $("#PeriodTo").data("kendoDatePicker").value(null);
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

    function onWinGenerateRebateTextClose() {
        $("#PeriodFrom").val("");
        $("#PeriodTo").val("");
        $("#FundClientPK").val("");
        $("#FundPK").val("");
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
                             GenerateRebatePK: { type: "number" },
                             FundPK: { type: "number" },
                             FundClientPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             PeriodFrom: { type: "date" },
                             PeriodTo: { type: "date" },
                             ManagementFee: { type: "number" },
                             FeeRebate: { type: "number" },
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
            var gridApproved = $("#gridGenerateRebateApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridGenerateRebatePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridGenerateRebateHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var GenerateRebateApprovedURL = window.location.origin + "/Radsoft/GenerateRebate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(GenerateRebateApprovedURL);
       
        $("#gridGenerateRebateApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form GenerateRebate"
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
                { command: { text: "Show", click: showGenerateDetail }, title: " ", width: 80 },
                { field: "GenerateRebatePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "JournalDate", title: "JournalDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                { field: "FundName", title: "Fund", width: 300 },
                { field: "FundClientName", title: "Client", width: 300 },
                { field: "PeriodFrom", title: "PeriodFrom", format: "{0:dd/MMM/yyyy}", width: 150 },
                { field: "PeriodTo", title: "PeriodTo", format: "{0:dd/MMM/yyyy}", width: 150 },
                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
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
        $("#TabGenerateRebate").kendoTabStrip({
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
                        var GenerateRebatePendingURL = window.location.origin + "/Radsoft/GenerateRebate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(GenerateRebatePendingURL);
                        $("#gridGenerateRebatePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form GenerateRebate"
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
                                { command: { text: "Show", click: showGenerateDetail }, title: " ", width: 80 },
                                { field: "GenerateRebatePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "JournalDate", title: "JournalDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                { field: "FundName", title: "Fund", width: 300 },
                                { field: "FundClientName", title: "Client", width: 300 },
                                { field: "PeriodFrom", title: "PeriodFrom", format: "{0:dd/MMM/yyyy}", width: 150 },
                                { field: "PeriodTo", title: "PeriodTo", format: "{0:dd/MMM/yyyy}", width: 150 },
                                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
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

                        var GenerateRebateHistoryURL = window.location.origin + "/Radsoft/GenerateRebate/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(GenerateRebateHistoryURL);

                        $("#gridGenerateRebateHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form GenerateRebate"
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
                                { command: { text: "Show", click: showGenerateDetail }, title: " ", width: 80 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "GenerateRebatePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "JournalDate", title: "JournalDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                { field: "FundName", title: "Fund", width: 300 },
                                { field: "FundClientName", title: "Client", width: 300 },
                                { field: "PeriodFrom", title: "PeriodFrom", format: "{0:dd/MMM/yyyy}", width: 150 },
                                { field: "PeriodTo", title: "PeriodTo", format: "{0:dd/MMM/yyyy}", width: 150 },
                                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
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
        var grid = $("#gridGenerateRebateHistory").data("kendoGrid");
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

    $("#BtnGenerateRebate").click(function () {
        showWinTempGenerateRebate();
    });

    function showWinTempGenerateRebate() {

        //FundPK
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
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        
        //FundClientPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundClientPK").kendoComboBox({
                    dataValueField: "FundClientPK",
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

       

        WinTempGenerateRebate.center();
        WinTempGenerateRebate.open();

    }

    //Detail dari Grid Temp
    function showDetailsGenerate(e) {
        grid = $("#gridTempGenerateRebateDetail").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        $("#gridShowGenerateRebateDetail").empty();

        var GenerateRebateDetailURL = window.location.origin + "/Radsoft/GenerateRebate/ShowGenerateRebateDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.GenerateRebateTempPK,
          dataSourceGenerate = getDataSource(GenerateRebateDetailURL);
        $("#gridShowGenerateRebateDetail").kendoGrid({
            dataSource: dataSourceGenerate,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form GenerateRebate"
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
                { field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 150 },
                { field: "FundName", title: "Fund", width: 300 },
                { field: "FundClientName", title: "Client", width: 300 },
                { field: "AUM", title: "AUM", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },

            ]
        });

        WinShowGenerateRebateDetail.center();
        WinShowGenerateRebateDetail.open();
    }

    function showGenerateDetail(e) {

        var grid;
        if (tabindex == 0 || tabindex == undefined) {
            grid = $("#gridGenerateRebateApproved").data("kendoGrid");
        }
        if (tabindex == 1) {
            grid = $("#gridGenerateRebatePending").data("kendoGrid");
        }
        if (tabindex == 2) {
            grid = $("#gridGenerateRebateHistory").data("kendoGrid");
        }
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        $("#gridGenRebateDetail").empty();

        var GenerateRebateDetailURL = window.location.origin + "/Radsoft/GenerateRebate/GetDetailRebate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.GenerateRebatePK,
          dataSourceGenerate = getDataSource(GenerateRebateDetailURL);
        $("#gridGenRebateDetail").kendoGrid({
            dataSource: dataSourceGenerate,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form GenerateRebate"
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
                { field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 150 },
                { field: "FundName", title: "Fund", width: 300 },
                { field: "FundClientName", title: "Client", width: 300 },
                { field: "AUM", title: "AUM", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },

            ]
        });

        WinGenRebateDetail.center();
        WinGenRebateDetail.open();
    }


    function showWinTempGenerateRebateDetail() {
        var GenerateRebateDetailURL = window.location.origin + "/Radsoft/GenerateRebate/GetGenerateRebateDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + $('#FundClientPK').val(),
          dataSourceGenerate = getDataSource(GenerateRebateDetailURL);
        $("#gridTempGenerateRebateDetail").empty();
        $("#gridTempGenerateRebateDetail").kendoGrid({
            dataSource: dataSourceGenerate,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Generate Rebate"
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
                { command: { text: "Show", click: showDetailsGenerate }, title: " ", width: 80 },
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "GenerateRebateTempPK", title: "SysNo.", width: 95 },
                { field: "FundName", title: "Fund", width: 200 },
                { field: "FundClientName", title: "Client", width: 200 },
                { field: "PeriodFrom", title: "PeriodFrom", format: "{0:dd/MMM/yyyy}", width: 150 },
                { field: "PeriodTo", title: "PeriodTo", format: "{0:dd/MMM/yyyy}", width: 150, },
                { field: "FeeRebate", title: "Fee Rebate", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                { field: "ManagementFee", title: "Management Fee", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 150 },
                
            ]
        });

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

        var grid = $("#gridTempGenerateRebateDetail").data("kendoGrid");
        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            

            var grid = $("#gridTempGenerateRebateDetail").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _GenerateRebatePK = dataItemX.GenerateRebateTempPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _GenerateRebatePK);

        }

        WinTempGenerateRebateDetail.center();
        WinTempGenerateRebateDetail.open();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/GenerateRebateTemp/" + _a + "/" + _b,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/GenerateRebateTemp/" + _a,
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

    $("#BtnOkTempGenerateRebate").click(function () {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var GenerateRebate = {
                        PeriodFrom: $('#PeriodFrom').val(),
                        PeriodTo: $('#PeriodTo').val(),
                        FundPK: $('#FundPK').val(),
                        FundClientPK: $('#FundClientPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/GenerateRebate/GetGenerateRebate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateRebate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            showWinTempGenerateRebateDetail();
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                    
                    
                } 
               
            });
        
    });

    $("#BtnCancelTempGenerateRebate").click(function () {
        
        alertify.confirm("Are you sure want to close Generate Rebate?", function (e) {
            if (e) {
                WinGenerateRebateText.close();
                alertify.alert("Close Generate Rebate");
            }
        });
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#GenerateRebatePK").val() + "/" + $("#HistoryPK").val() + "/" + "GenerateRebate",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "GenerateRebate" + "/" + $("#GenerateRebatePK").val(),
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

    //$("#BtnApproved").click(function () {
    //    
    //    alertify.confirm("Are you sure want to Approved data?", function (e) {
    //        if (e) {
    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#GenerateRebatePK").val() + "/" + $("#HistoryPK").val() + "/" + "GenerateRebate",
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

    //                        var GenerateRebate = {
    //                            GenerateRebatePK: $('#GenerateRebatePK').val(),
    //                            HistoryPK: $('#HistoryPK').val(),
    //                            ApprovedUsersID: sessionStorage.getItem("user")
    //                        };
    //                        $.ajax({
    //                            url: window.location.origin + "/Radsoft/GenerateRebate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "GenerateRebate_A",
    //                            type: 'POST',
    //                            data: JSON.stringify(GenerateRebate),
    //                            contentType: "application/json;charset=utf-8",
    //                            success: function (data) {
    //                                alertify.alert(data);
    //                                win.close();
    //                                refresh();
    //                            },
    //                            error: function (data) {
    //                                alertify.alert(data.responseText);
    //                            }
    //                        });

    //                    } else {
    //                        alertify.alert("Data has been Changed by other user, Please check it first!");
    //                        win.close();
    //                        refresh();
    //                    }
    //                },
    //                error: function (data) {
    //                    alertify.alert(data.responseText);
    //                }
    //            });
    //        }
    //    });


    //});

    $("#BtnInsertGenerate").click(function () {
        
        alertify.confirm("Are you sure want to Insert data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/GenerateRebate/GenerateRebateInsert/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
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

    $("#BtnCloseGenerate").click(function () {
        
        alertify.confirm("Are you sure want to close Generate Rebate?", function (e) {
            if (e) {
                WinShowGenerateRebateDetail.close();
                alertify.alert("Close Generate Rebate");
            }
        });
    });

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#GenerateRebatePK").val() + "/" + $("#HistoryPK").val() + "/" + "GenerateRebate",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var GenerateRebate = {
                                GenerateRebatePK: $('#GenerateRebatePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/GenerateRebate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "GenerateRebate_V",
                                type: 'POST',
                                data: JSON.stringify(GenerateRebate),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#GenerateRebatePK").val() + "/" + $("#HistoryPK").val() + "/" + "GenerateRebate",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var GenerateRebate = {
                                GenerateRebatePK: $('#GenerateRebatePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/GenerateRebate/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "GenerateRebate_R",
                                type: 'POST',
                                data: JSON.stringify(GenerateRebate),
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
