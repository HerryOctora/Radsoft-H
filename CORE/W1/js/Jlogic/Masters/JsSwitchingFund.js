﻿$(document).ready(function () {
    document.title = 'FORM Switching Fund';
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

    if (_GlobClientCode == "10") {
        $("#LblType").show();

    }
    else {
        $("#LblType").hide();

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

        win = $("#WinSwitchingFund").kendoWindow({
            height: 450,
            title: "Switching Fund Detail",
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



    var GlobValidator = $("#WinSwitchingFund").kendoValidator().data("kendoValidator");

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

            $("#SwitchingFundPK").val(dataItemX.SwitchingFundPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
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

        $("#Type").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "APERD", value: 1 },
                { text: "Non-APERD", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeType,
            value: setCmbType()
        });
        function OnChangeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbType() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.Type;
            }
        }

        //combo box FundFromPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundFromPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundFromPK,
                    value: setCmbFundFromPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundFromPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFundFromPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundFromPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundFromPK;
                }
            }
        }
        win.center();
        win.open();

        //combo box FundToPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundToPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundToPK,
                    value: setCmbFundToPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeFundToPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFundToPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundToPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundToPK;
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
        $("#SwitchingFundPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundFromPK").val("");
        $("#FundToPK").val("");
        $("#Type").val("");
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
                            SwitchingFundPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            FundFromPK: { type: "number" },
                            FundFromID: { type: "string" },
                            FundToPK: { type: "number" },
                            FundToID: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
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
            var gridApproved = $("#gridSwitchingFundApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridSwitchingFundPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridSwitchingFundHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var SwitchingFundApprovedURL = window.location.origin + "/Radsoft/SwitchingFund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(SwitchingFundApprovedURL);

        if (_GlobClientCode == '10') {
            $("#gridSwitchingFundApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Switching Fund"
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
                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundFromID", title: "Fund From", width: 300 },
                    { field: "FundToID", title: "Fund To", width: 300 },
                    { field: "Type", title: "Type", hidden: true, width: 200 },
                    { field: "TypeDesc", title: "Type", width: 200 },
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

        } else {
            $("#gridSwitchingFundApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Switching Fund"
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
                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundFromID", title: "Fund From", width: 300 },
                    { field: "FundToID", title: "Fund To", width: 300 },
                    //{ field: "Type", title: "Type", hidden: true, width: 200 },
                    //{ field: "TypeDesc", title: "Type", width: 200 },
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
        $("#TabSwitchingFund").kendoTabStrip({
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
                        var SwitchingFundPendingURL = window.location.origin + "/Radsoft/SwitchingFund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(SwitchingFundPendingURL);

                        if (_GlobClientCode == '10') {
                            $("#gridSwitchingFundPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Switching Fund"
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
                                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundFromID", title: "Fund From", width: 300 },
                                    { field: "FundToID", title: "Fund To", width: 300 },
                                    //{ field: "Type", title: "Type", width: 300 },
                                    //{ field: "Type", title: "Type", width: 200, template: "#= Type ? 'APERD' : 'Non-APERD' #" },
                                    { field: "Type", title: "Type", hidden: true, width: 200 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
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

                        } else {
                            $("#gridSwitchingFundPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Switching Fund"
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
                                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundFromID", title: "Fund From", width: 300 },
                                    { field: "FundToID", title: "Fund To", width: 300 },
                                    //{ field: "Type", title: "Type", width: 300 },
                                    //{ field: "Type", title: "Type", width: 200, template: "#= Type ? 'APERD' : 'Non-APERD' #" },
                                    //{ field: "Type", title: "Type", hidden: true, width: 200 },
                                    //{ field: "TypeDesc", title: "Type", width: 200 },
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

                        var SwitchingFundHistoryURL = window.location.origin + "/Radsoft/SwitchingFund/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(SwitchingFundHistoryURL);


                        if (_GlobClientCode == '10') {
                            $("#gridSwitchingFundHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Switching Fund"
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
                                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundFromID", title: "Fund From", width: 300 },
                                    { field: "FundToID", title: "Fund To", width: 300 },
                                    { field: "Type", title: "Type", hidden: true, width: 200 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
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


                        } else {
                            $("#gridSwitchingFundHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Switching Fund"
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
                                    { field: "SwitchingFundPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundFromID", title: "Fund From", width: 300 },
                                    { field: "FundToID", title: "Fund To", width: 300 },
                                    //{ field: "Type", title: "Type", hidden: true, width: 200 },
                                    //{ field: "TypeDesc", title: "Type", width: 200 },
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

                    }
                } else {
                    refresh();
                }
            }
        });
    }


    function gridHistoryDataBound() {
        var grid = $("#gridSwitchingFundHistory").data("kendoGrid");
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
                    var SwitchingFund = {
                        FundFromPK: $('#FundFromPK').val(),
                        FundToPK: $('#FundToPK').val(),
                        Type: $('#Type').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/SwitchingFund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SwitchingFund_I",
                        type: 'POST',
                        data: JSON.stringify(SwitchingFund),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SwitchingFundPK").val() + "/" + $("#HistoryPK").val() + "/" + "SwitchingFund",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var SwitchingFund = {
                                    SwitchingFundPK: $('#SwitchingFundPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundFromPK: $('#FundFromPK').val(),
                                    FundToPK: $('#FundToPK').val(),
                                    Type: $('#Type').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SwitchingFund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SwitchingFund_U",
                                    type: 'POST',
                                    data: JSON.stringify(SwitchingFund),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SwitchingFundPK").val() + "/" + $("#HistoryPK").val() + "/" + "SwitchingFund",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "SwitchingFund" + "/" + $("#SwitchingFundPK").val(),
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
                    url: window.location.origin + "/Radsoft/SwitchingFund/GetCheckAlreadyHasApprovedSwitchingFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundFromPK").val() + "/" + $("#FundToPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SwitchingFundPK").val() + "/" + $("#HistoryPK").val() + "/" + "SwitchingFund",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                        var SwitchingFund = {
                                            SwitchingFundPK: $('#SwitchingFundPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/SwitchingFund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SwitchingFund_A",
                                            type: 'POST',
                                            data: JSON.stringify(SwitchingFund),
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

                        } else {
                            alertify.alert("Data has exist, no more addition!");
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SwitchingFundPK").val() + "/" + $("#HistoryPK").val() + "/" + "SwitchingFund",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SwitchingFund = {
                                SwitchingFundPK: $('#SwitchingFundPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SwitchingFund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SwitchingFund_V",
                                type: 'POST',
                                data: JSON.stringify(SwitchingFund),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SwitchingFundPK").val() + "/" + $("#HistoryPK").val() + "/" + "SwitchingFund",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SwitchingFund = {
                                SwitchingFundPK: $('#SwitchingFundPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SwitchingFund/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SwitchingFund_R",
                                type: 'POST',
                                data: JSON.stringify(SwitchingFund),
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
