$(document).ready(function () {
    document.title = 'FORM FA COA MAPPING';
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

        $("#BtnWinCopyFACOAMapping").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    
    

    function initWindow() {

        win = $("#WinFACOAMapping").kendoWindow({
            height: 450,
            title: "FA COA Mapping Detail",
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


        WinCopyFACOAMapping = $("#WinCopyFACOAMapping").kendoWindow({
            height: 150,
            title: "* Copy FA COA Mapping",
            visible: false,
            width: 600,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinFACOAMapping").kendoValidator().data("kendoValidator");

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

            $("#FACOAMappingPK").val(dataItemX.FACOAMappingPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundID").val(dataItemX.FundID + " - " + dataItemX.FundName);
            $("#FACOAAdjustmentPK").val(dataItemX.FACOAAdjustmentPK);
            $("#FundJournalAccountPK").val(dataItemX.FundJournalAccountPK);
            $("#DebitOrCredit").val(dataItemX.DebitOrCredit);
            $("#FundJournalAccountPercent").val(dataItemX.FundJournalAccountPercent);
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
        ////combo box FACOAAdjustmentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FACOAAdjustment/GetFACOAAdjustmentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FACOAAdjustmentPK").kendoComboBox({
                    dataValueField: "FACOAAdjustmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFACOAAdjustmentPK,
                    value: setCmbFACOAAdjustmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFACOAAdjustmentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFACOAAdjustmentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FACOAAdjustmentPK == 0) {
                    return "";
                } else {
                    return dataItemX.FACOAAdjustmentPK;
                }
            }
        }


        //combo box FundJournalAccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundJournalAccountPK").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbFundJournalAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundJournalAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFundJournalAccountPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundJournalAccountPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundJournalAccountPK;
                }
            }
        }

        //combo box FundPK
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
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }
        $("#FundJournalAccountPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setFundJournalAccountPercent(),
        });
        function setFundJournalAccountPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundJournalAccountPercent == 0) {
                    return "";
                } else {
                    return dataItemX.FundJournalAccountPercent;
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
        $("#FACOAMappingPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#FACOAAdjustmentPK").val("");
        $("#FundJournalAccountPK").val("");
        $("#DebitOrCredit").val("");
        $("#FundJournalAccountPercent").val("");
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
                             FACOAMappingPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             FACOAAdjustmentPK: { type: "number" },
                             FACOAAdjustmentID: { type: "string" },
                             FACOAAdjustmentName: { type: "string" },
                             FundJournalAccountPK: { type: "number" },
                             FundJournalAccountID: { type: "string" },
                             FundJournalAccountName: { type: "string" },
                             DebitOrCredit: { type: "string" },
                             FundJournalAccountPercent: { type: "string" },
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
            var gridApproved = $("#gridFACOAMappingApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFACOAMappingPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFACOAMappingHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FACOAMappingApprovedURL = window.location.origin + "/Radsoft/FACOAMapping/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FACOAMappingApprovedURL);

        $("#gridFACOAMappingApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FA COA Mapping"
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
                { field: "FACOAMappingPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "FundName", title: "Fund Name", hidden: true, width: 250 },
                { field: "FACOAAdjustmentID", title: "ID", width: 200 },
                { field: "FACOAAdjustmentName", title: "FA COA Adjustment Name", hidden: true, width: 200 },
                { field: "FundJournalAccountID", title: "Account ID", width: 200 },
                { field: "FundJournalAccountName", title: "Account Name", width: 200 },
                { field: "DebitOrCredit", title: "D / C", width: 120 },
                  {
                      field: "FundJournalAccountPercent", title: "Fund Journal Account %", width: 200,
                      template: "#: FundJournalAccountPercent  # %",
                      attributes: { style: "text-align:right;" }
                  },
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
        $("#TabFACOAMapping").kendoTabStrip({
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
                        var FACOAMappingPendingURL = window.location.origin + "/Radsoft/FACOAMapping/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FACOAMappingPendingURL);
                        $("#gridFACOAMappingPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FA COA Mapping"
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
                                { field: "FACOAMappingPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "FundName", title: "Fund Name", hidden: true, width: 250 },
                                { field: "FACOAAdjustmentID", title: "ID", width: 200 },
                                { field: "FACOAAdjustmentName", title: "FA COA Adjustment Name", hidden: true, width: 200 },
                                { field: "FundJournalAccountID", title: "Account ID", width: 200 },
                                { field: "FundJournalAccountName", title: "Account Name", width: 200 },
                                { field: "DebitOrCredit", title: "D / C", width: 120 },
                                  {
                                      field: "FundJournalAccountPercent", title: "Fund Journal Account %", width: 200,
                                      template: "#: FundJournalAccountPercent  # %",
                                      attributes: { style: "text-align:right;" }
                                  },
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

                        var FACOAMappingHistoryURL = window.location.origin + "/Radsoft/FACOAMapping/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FACOAMappingHistoryURL);

                        $("#gridFACOAMappingHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FA COA Mapping"
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
                                { field: "FACOAMappingPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "FundName", title: "Fund Name", hidden: true, width: 250 },
                                { field: "FACOAAdjustmentID", title: "ID", width: 200 },
                                { field: "FACOAAdjustmentName", title: "FA COA Adjustment Name", hidden: true, width: 200 },
                                { field: "FundJournalAccountID", title: "Account ID", width: 200 },
                                { field: "FundJournalAccountName", title: "Account Name", width: 200 },
                                { field: "DebitOrCredit", title: "D / C", width: 120 },
                                {
                                    field: "FundJournalAccountPercent", title: "Fund Journal Account %", width: 200,
                                    template: "#: FundJournalAccountPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
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
        var grid = $("#gridFACOAMappingHistory").data("kendoGrid");
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
                    var FACOAMapping = {
                        FundPK: $('#FundPK').val(),
                        FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                        FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                        DebitOrCredit: $('#DebitOrCredit').val(),
                        FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_I",
                        type: 'POST',
                        data: JSON.stringify(FACOAMapping),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FACOAMappingPK").val() + "/" + $("#HistoryPK").val() + "/" + "FACOAMapping",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FACOAMapping = {
                                    FACOAMappingPK: $('#FACOAMappingPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                                    FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                    DebitOrCredit: $('#DebitOrCredit').val(),
                                    FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_U",
                                    type: 'POST',
                                    data: JSON.stringify(FACOAMapping),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FACOAMappingPK").val() + "/" + $("#HistoryPK").val() + "/" + "FACOAMapping",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FACOAMapping" + "/" + $("#FACOAMappingPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FACOAMappingPK").val() + "/" + $("#HistoryPK").val() + "/" + "FACOAMapping",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FACOAMapping = {
                                FACOAMappingPK: $('#FACOAMappingPK').val(),
                                FundPK: $('#FundPK').val(),
                                FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                DebitOrCredit: $('#DebitOrCredit').val(),
                                FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_A",
                                type: 'POST',
                                data: JSON.stringify(FACOAMapping),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FACOAMappingPK").val() + "/" + $("#HistoryPK").val() + "/" + "FACOAMapping",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FACOAMapping = {
                                FACOAMappingPK: $('#FACOAMappingPK').val(),
                                FundPK: $('#FundPK').val(),
                                FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                DebitOrCredit: $('#DebitOrCredit').val(),
                                FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_V",
                                type: 'POST',
                                data: JSON.stringify(FACOAMapping),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FACOAMappingPK").val() + "/" + $("#HistoryPK").val() + "/" + "FACOAMapping",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FACOAMapping = {
                                FACOAMappingPK: $('#FACOAMappingPK').val(),
                                FundPK: $('#FundPK').val(),
                                FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                DebitOrCredit: $('#DebitOrCredit').val(),
                                FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_R",
                                type: 'POST',
                                data: JSON.stringify(FACOAMapping),
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

    $("#BtnCopyFACOAMapping").click(function () {
        
        alertify.confirm("Are you sure want to Copy data?", function (e) {
            if (e) {

                var FACOAMapping = {
                    FACOAMappingPK: $('#FACOAMappingPK').val(),
                    FundPK: $('#FundPK').val(),
                    FACOAAdjustmentPK: $('#FACOAAdjustmentPK').val(),
                    FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                    DebitOrCredit: $('#DebitOrCredit').val(),
                    FundJournalAccountPercent: $('#FundJournalAccountPercent').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FACOAMapping/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FACOAMapping_A",
                    type: 'POST',
                    data: JSON.stringify(FACOAMapping),
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


    $("#BtnWinCopyFACOAMapping").click(function () {
        showWinCopyFACOAMapping();
    });

    function showWinCopyFACOAMapping(e) {

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                });

                $("#ParamFundTo").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
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



        WinCopyFACOAMapping.center();
        WinCopyFACOAMapping.open();

    }

    $("#BtnOkCopyFACOAMapping").click(function () {
        $.blockUI({});
        

        alertify.confirm("Are you sure want to Copy Data COA Mapping ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FACOAMapping/ValidateCheckCopyFACOAMapping/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/"  + $("#ParamFundTo").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FACOAMapping/CopyFACOAMapping/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamFundFrom").val() + "/" + $("#ParamFundTo").val(),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    
                                    $.unblockUI();
                                    WinCopyFACOAMapping.close();
                                    alertify.alert(data);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });

                        } else {
                            alertify.alert(data);
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });
    });

    $("#BtnCancelCopyFACOAMapping").click(function () {
        
        alertify.confirm("Are you sure want to Copy Data COA Mapping ?", function (e) {
            if (e) {
                WinCopyFACOAMapping.close();
                alertify.alert("Cancel Copy FA COA Mapping ");
            }
        });
    });

});