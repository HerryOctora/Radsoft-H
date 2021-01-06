$(document).ready(function () {

    document.title = 'FORM BALANCE FROM SOURCE';
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
        $("#BtnImportBalanceFromSource").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    
       


    function initWindow() {
        $("#ParamDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        win = $("#WinBalanceFromSource").kendoWindow({
            height: 450,
            title: "Balance From Source Detail",
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

        WinImportBalanceFromSource = $("#WinImportBalanceFromSource").kendoWindow({
            height: 200,
            title: "Import Balance From Source",
            visible: false,
            width: 550,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinImportBalanceFromSourceClose
        }).data("kendoWindow");

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

    }

    var GlobValidator = $("#WinBalanceFromSource").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        //$("#btnListFundClientPK").kendoButton({
        //    icon: "ungroup"
        //}).data("kendoButton")

        var dataItemX;

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

            $("#BalanceFromSourcePK").val(dataItemX.BalanceFromSourcePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#PrevBalance").val(dataItemX.PrevBalance);
            $("#CurrentBalance").val(dataItemX.CurrentBalance);
            $("#EndBalance").val(dataItemX.EndBalance);
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

        $("#PrevBalance").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setPrevBalance()
        });
        function setPrevBalance() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.PrevBalance;
            }
        }

        $("#CurrentBalance").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setCurrentBalance()
        });
        function setCurrentBalance() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.CurrentBalance;
            }
        }

        $("#EndBalance").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setEndBalance()
        });
        function setEndBalance() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.EndBalance;
            }
        }

       

        //combo box MISCostCenterPK
        $.ajax({
            url: window.location.origin + "/Radsoft/MISCostCenter/GetMISCostCenterCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MISCostCenterPK").kendoComboBox({
                    dataValueField: "MISCostCenterPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeMISCostCenterPK,
                    value: setCmbMISCostCenterPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeMISCostCenterPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMISCostCenterPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MISCostCenterPK == 0) {
                    return "";
                } else {
                    return dataItemX.MISCostCenterPK;
                }
            }
        }


        //combo box COAFromSourcePK
        $.ajax({
            url: window.location.origin + "/Radsoft/COAFromSource/GetCOAFromSourceCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#COAFromSourcePK").kendoComboBox({
                    dataValueField: "COAFromSourcePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCOAFromSourcePK,
                    value: setCmbCOAFromSourcePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCOAFromSourcePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCOAFromSourcePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.COAFromSourcePK == 0) {
                    return "";
                } else {
                    return dataItemX.COAFromSourcePK;
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
        $("#BalanceFromSourcePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#COAFromSourcePK").val("");
        $("#Date").val("");
        $("#PrevBalance").val("");
        $("#CurrentBalance").val("");
        $("#EndBalance").val("");
        $("#MISCostCenterPK").val("");
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
                             BalanceFromSourcePK: { type: "number" },
                             COAFromSourcePK: { type: "number" },
                             COAFromSourceID: { type: "string" },
                             COAFromSourceName: { type: "string" },
                             Date: { type: "date" },
                             PrevBalance: { type: "number" },
                             CurrentBalance: { type: "number" },
                             EndBalance: { type: "number" },
                             MISCostCenterPK: { type: "number" },
                             MISCostCenterID: { type: "string" },
                             MISCostCenterName: { type: "string" },        
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridBalanceFromSourceApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBalanceFromSourcePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBalanceFromSourceHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BalanceFromSourceApprovedURL = window.location.origin + "/Radsoft/BalanceFromSource/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(BalanceFromSourceApprovedURL);

        $("#gridBalanceFromSourceApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Balance From Source"
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
                { field: "BalanceFromSourcePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "COAFromSourceID", title: "COAFromSourceID", width: 200 },
                { field: "COAFromSourceName", title: "COAFromSourceName", width: 300 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },               
                { field: "PrevBalance", title: "PrevBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CurrentBalance", title: "CurrentBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "EndBalance", title: "EndBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MISCostCenterID", title: "MISCostCenterID", width: 200 },
                { field: "MISCostCenterName", title: "MISCostCenterName", width: 200 },
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
        $("#TabBalanceFromSource").kendoTabStrip({
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
                        var BalanceFromSourcePendingURL = window.location.origin + "/Radsoft/BalanceFromSource/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(BalanceFromSourcePendingURL);
                        $("#gridBalanceFromSourcePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Balance From Source"
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
                                { field: "BalanceFromSourcePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "COAFromSourceID", title: "COAFromSourceID", width: 200 },
                                { field: "COAFromSourceName", title: "COAFromSourceName", width: 300 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "PrevBalance", title: "PrevBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "CurrentBalance", title: "CurrentBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "EndBalance", title: "EndBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MISCostCenterID", title: "MISCostCenterID", width: 200 },
                                { field: "MISCostCenterName", title: "MISCostCenterName", width: 200 },
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

                        var BalanceFromSourceHistoryURL = window.location.origin + "/Radsoft/BalanceFromSource/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(BalanceFromSourceHistoryURL);

                        $("#gridBalanceFromSourceHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Balance From Source"
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
                                { field: "BalanceFromSourcePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "COAFromSourceID", title: "COAFromSourceID", width: 200 },
                                { field: "COAFromSourceName", title: "COAFromSourceName", width: 300 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "PrevBalance", title: "PrevBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "CurrentBalance", title: "CurrentBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "EndBalance", title: "EndBalance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MISCostCenterID", title: "MISCostCenterID", width: 200 },
                                { field: "MISCostCenterName", title: "MISCostCenterName", width: 200 },
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
        var grid = $("#gridBalanceFromSourceHistory").data("kendoGrid");
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
                    var BalanceFromSource = {
                        COAFromSourcePK: $('#COAFromSourcePK').val(),
                        Date: $('#Date').val(),
                        PrevBalance: $('#PrevBalance').val(),
                        CurrentBalance: $('#CurrentBalance').val(),
                        EndBalance: $('#EndBalance').val(),
                        MISCostCenterPK: $('#MISCostCenterPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/BalanceFromSource/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_I",
                        type: 'POST',
                        data: JSON.stringify(BalanceFromSource),
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
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BalanceFromSourcePK").val() + "/" + $("#HistoryPK").val() + "/" + "BalanceFromSource",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var BalanceFromSource = {
                                    BalanceFromSourcePK: $('#BalanceFromSourcePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    COAFromSourcePK: $('#COAFromSourcePK').val(),
                                    Date: $('#Date').val(),
                                    PrevBalance: $('#PrevBalance').val(),
                                    CurrentBalance: $('#CurrentBalance').val(),
                                    EndBalance: $('#EndBalance').val(),
                                    MISCostCenterPK: $('#MISCostCenterPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BalanceFromSource/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_U",
                                    type: 'POST',
                                    data: JSON.stringify(BalanceFromSource),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BalanceFromSourcePK").val() + "/" + $("#HistoryPK").val() + "/" + "BalanceFromSource",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BalanceFromSource" + "/" + $("#BalanceFromSourcePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BalanceFromSourcePK").val() + "/" + $("#HistoryPK").val() + "/" + "BalanceFromSource",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BalanceFromSource = {
                                BalanceFromSourcePK: $('#BalanceFromSourcePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BalanceFromSource/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_A",
                                type: 'POST',
                                data: JSON.stringify(BalanceFromSource),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BalanceFromSourcePK").val() + "/" + $("#HistoryPK").val() + "/" + "BalanceFromSource",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BalanceFromSource = {
                                BalanceFromSourcePK: $('#BalanceFromSourcePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BalanceFromSource/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_V",
                                type: 'POST',
                                data: JSON.stringify(BalanceFromSource),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BalanceFromSourcePK").val() + "/" + $("#HistoryPK").val() + "/" + "BalanceFromSource",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BalanceFromSource = {
                                BalanceFromSourcePK: $('#BalanceFromSourcePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BalanceFromSource/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_R",
                                type: 'POST',
                                data: JSON.stringify(BalanceFromSource),
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

    $("#BtnImportBalanceFromSource").click(function () {
        showImportBalanceFromSource();
    });


    // Untuk Form Listing Exposure Pre Trade

    function showImportBalanceFromSource(e) {
        $("#ParamDate").data("kendoDatePicker").value(new Date);

        $.ajax({
            url: window.location.origin + "/Radsoft/MISCostCenter/GetMISCostCenterCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamMISCostCenter").kendoComboBox({
                    dataValueField: "MISCostCenterPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeParamMISCostCenter,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeParamMISCostCenter() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }




        WinImportBalanceFromSource.center();
        WinImportBalanceFromSource.open();

    }

    $("#BtnOkImportBalanceFromSource").click(function () {
        document.getElementById("FileImportBalanceFromSource").click();
    });

    $("#FileImportBalanceFromSource").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportBalanceFromSource").get(0).files;

        if (files.length > 0) {
            data.append("BalanceFromSource", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BalanceFromSource_Import" + "/" + kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamMISCostCenter").val(),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBalanceFromSource").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBalanceFromSource").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBalanceFromSource").val("");
        }
    });

    $("#BtnCancelImportBalanceFromSource").click(function () {
        
        alertify.confirm("Are you sure want to cancel Import?", function (e) {
            if (e) {
                WinImportBalanceFromSource.close();
                alertify.alert("Cancel Import");
            }
        });
    });

    function onWinImportBalanceFromSourceClose() {
        $("#ParamDate").data("kendoDatePicker").value(null),
        $("#ParamMISCostCenter").val("")
    }

});