$(document).ready(function () {
    document.title = 'FORM BACK LOAD SETUP';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var checkedIds = {};
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

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
        });

        win = $("#WinBackLoadSetup").kendoWindow({
            height: 450,
            title: "BackLoadSetup Detail",
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
    
    var GlobValidator = $("#WinBackLoadSetup").kendoValidator().data("kendoValidator");

    function validateData() {

    //    if (GlobValidator.validate()) {

    //        if ($("#Date").val().length > 50) {
    //            alertify.alert("Validation not Pass, char more than 50 for ID");
    //            return 0;
    //        }

    //        if ($("#FundPK").val().length > 100) {
    //            alertify.alert("Validation not Pass, char more than 100 for Name");
    //            return 0;
    //        }

    //        if ($("#HoldingPeriod").val().length > 100) {
    //            alertify.alert("Validation not Pass, char more than 100 for Name");
    //            return 0;
    //        }

    //        if ($("#MinimumSubs").val().length > 100) {
    //            alertify.alert("Validation not Pass, char more than 100 for Name");
    //            return 0;
    //        }

    //        if ($("#RedempFeePercent").val().length > 100) {
    //            alertify.alert("Validation not Pass, char more than 1000 for Notes");
    //            return 0;
    //        }

    //        if ($("#Notes").val().length > 1000) {
    //            alertify.alert("Validation not Pass, char more than 1000 for Notes");
    //            return 0;
    //        }

    //        return 1;
    //    }
    //    else {
    //        alertify.alert("Validation not Pass");
    //        return 0;
    //    }
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

            $("#BackLoadSetupPK").val(dataItemX.BackLoadSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);

            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            //$("#Date").val(dataItemX.Date);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundName").val(dataItemX.FundName);
            //$("#HoldingPeriod").val(dataItemX.HoldingPeriod);
            $("#HoldingPeriod").val(dataItemX.HoldingPeriod);
            $("#MinimumSubs").val(dataItemX.MinimumSubs);
            $("#RedempFeePercent").val(dataItemX.RedempFeePercent);
            
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

        //combo
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

        $("#MinimumSubs").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setMinimumSubs(),
        });

        function setMinimumSubs() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MinimumSubs == 0) {
                    return "";
                } else {
                    return dataItemX.MinimumSubs;
                }
            }
        }

        $("#RedempFeePercent").kendoNumericTextBox({
            format: "##.######## \\%",
            decimals: 8,
            value: setRedempFeePercent(),
        });

        function setRedempFeePercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RedempFeePercent == 0) {
                    return "";
                } else {
                    return dataItemX.RedempFeePercent;
                }
            }
        }

        $("#HoldingPeriod").kendoNumericTextBox({
            format: "##.########",
            decimals: 0,
            value: setHoldingPeriod(),
        });
        $("#HoldingPeriod").data("kendoNumericTextBox").enable(true);

        function setHoldingPeriod() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HoldingPeriod;

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
        $("#BackLoadSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");

        $("#Date").val("");
        $("#FundPK").val("");
        $("#FundName").val("");
        $("#HoldingPeriod").val("");
        $("#MinimumSubs").val("");
        $("#RedempFeePercent").val("");

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
                             BackLoadSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },

                             Date: { type: "date" },
                             FundPK: { type: "number" },
                             FundName: { type: "string" },
                             HoldingPeriod: { type: "number" },
                             MinimumSubs: { type: "number" },
                             RedempFeePercent: { type: "number" },
                             
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
            var gridApproved = $("#gridBackLoadSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBackLoadSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBackLoadSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BackLoadSetupApprovedURL = window.location.origin + "/Radsoft/BackLoadSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(BackLoadSetupApprovedURL);

        $("#gridBackLoadSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form BackLoadSetup"
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
                { field: "BackLoadSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                //{ field: "NAVDate", title: "NAV Date", width: 130, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MM/yyyy')#" },
                { field: "Date", title: "Date", width: 130, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                //{ field: "FundPK", title: "FundPK", width: 300 },
                { field: "FundName", title: "FundName", width: 300 },
                { field: "HoldingPeriod", title: "HoldingPeriod", width: 300 },
                { field: "MinimumSubs", title: "MinimumSubs", width: 300 },
                { field: "RedempFeePercent", title: "RedempFeePercent", width: 300 },
                
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
        $("#TabBackLoadSetup").kendoTabStrip({
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
                        var BackLoadSetupPendingURL = window.location.origin + "/Radsoft/BackLoadSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(BackLoadSetupPendingURL);
                        $("#gridBackLoadSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BackLoadSetup"
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
                                { field: "BackLoadSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                { field: "Date", title: "Date", width: 130, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                                //{ field: "FundPK", title: "FundPK", width: 300 },
                                { field: "FundName", title: "FundName", width: 300 },
                                { field: "HoldingPeriod", title: "HoldingPeriod", width: 300 },
                                { field: "MinimumSubs", title: "MinimumSubs", width: 300 },
                                { field: "RedempFeePercent", title: "RedempFeePercent", width: 300 },

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

                        var BackLoadSetupHistoryURL = window.location.origin + "/Radsoft/BackLoadSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(BackLoadSetupHistoryURL);

                        $("#gridBackLoadSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BackLoadSetup"
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
                                { field: "BackLoadSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                
                                { field: "Date", title: "Date", width: 130, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                                //{ field: "FundPK", title: "FundPK", width: 300 },
                                { field: "FundName", title: "FundName", width: 300 },
                                { field: "HoldingPeriod", title: "HoldingPeriod", width: 300 },
                                { field: "MinimumSubs", title: "MinimumSubs", width: 300 },
                                { field: "RedempFeePercent", title: "RedempFeePercent", width: 300 },

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
        var grid = $("#gridBackLoadSetupHistory").data("kendoGrid");
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
        //var All = 0;
        //All = [];
        //for (var i in checkedIds) {
        //    if (checkedIds[i]) {
        //        All.push(i);
        //    }
        //}

        //var ArrayFundFrom = All;
        //var stringFundFrom = '';
        //// end fund grid

        ////clearDataMulti();
        ////var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        ////var stringFundFrom = '';
        //for (var i in ArrayFundFrom) {
        //    stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        //}
        //stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)
        var val = 1;
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "BackLoadSetup",
                    //    type: 'GET',
                    //    contentType: "application/json;charset=utf-8",
                    //    success: function (data) {
                    //        if (data == true) {

                    //var BackLoadSetup = {
                    //    Date: $('#Date').val(),
                    //    FundPK: $('#FundPK').val(),
                    //    HoldingPeriod: $('#HoldingPeriod').val(),
                    //    MinimumSubs: $('#MinimumSubs').val(),
                    //    RedempFeePercent: $('#RedempFeePercent').val(),
                    //    EntryUsersID: sessionStorage.getItem("user"),
                    //    FundFrom: stringFundFrom,
                    //};
                    //$.ajax({
                    //    url: window.location.origin + "/Radsoft/BackLoadSetup/ValidateAddBackLoadSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    //    type: 'POST',
                    //    contentType: "application/json;charset=utf-8",
                    //    data: JSON.stringify(BackLoadSetup),
                    //    success: function (data) {
                    $.blockUI();
                    //if (data == false) {
                    var BackLoadSetup = {
                        Date: $('#Date').val(),
                        FundPK: $('#FundPK').val(),
                        HoldingPeriod: $('#HoldingPeriod').val(),
                        MinimumSubs: $('#MinimumSubs').val(),
                        RedempFeePercent: $('#RedempFeePercent').val(),
                        EntryUsersID: sessionStorage.getItem("user"),
                        //FundFrom: stringFundFrom,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/BackLoadSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BackLoadSetup_I",
                        type: 'POST',
                        data: JSON.stringify(BackLoadSetup),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            win.close();
                            refresh();
                            $.unblockUI();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                    //} else {
                    //    alertify.alert("Already get data For this Day, Void / Reject First!");
                    //    $.unblockUI();
                    //}
                    //},

                    //        } else {
                    //            alertify.alert("Data ID Same Not Allow!");
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
        var val = 1;
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BackLoadSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "BackLoadSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var BackLoadSetup = {
                                    BackLoadSetupPK: $('#BackLoadSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Date: $('#Date').val(),
                                    FundPK: $('#FundPK').val(),
                                    HoldingPeriod: $('#HoldingPeriod').val(),
                                    MinimumSubs: $('#MinimumSubs').val(),
                                    RedempFeePercent: $('#RedempFeePercent').val(),

                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BackLoadSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BackLoadSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(BackLoadSetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BackLoadSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "BackLoadSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BackLoadSetup" + "/" + $("#BackLoadSetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BackLoadSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "BackLoadSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BackLoadSetup = {
                                BackLoadSetupPK: $('#BackLoadSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BackLoadSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BackLoadSetup_A",
                                type: 'POST',
                                data: JSON.stringify(BackLoadSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BackLoadSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "BackLoadSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BackLoadSetup = {
                                BackLoadSetupPK: $('#BackLoadSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BackLoadSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BackLoadSetup_V",
                                type: 'POST',
                                data: JSON.stringify(BackLoadSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BackLoadSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "BackLoadSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BackLoadSetup = {
                                BackLoadSetupPK: $('#BackLoadSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BackLoadSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BackLoadSetup_R",
                                type: 'POST',
                                data: JSON.stringify(BackLoadSetup),
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