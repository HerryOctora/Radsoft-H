﻿$(document).ready(function () {
    document.title = 'FORM FUND JOURNAL ACCOUNT';
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
        $("#BtnFundJournalAccountRestructure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }


    function initWindow() {

        win = $("#WinFundJournalAccount").kendoWindow({
            height: 600,
            title: "Fund Journal Account Detail",
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



    var GlobValidator = $("#WinFundJournalAccount").kendoValidator().data("kendoValidator");

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

            $("#FundJournalAccountPK").val(dataItemX.FundJournalAccountPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Levels").val(dataItemX.Levels);
            $("#Depth").val(dataItemX.Depth);
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





        $("#Groups").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeGroups,
            value: setCmbGroups()
        });
        function OnChangeGroups() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbGroups() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Groups;
            }
        }

        $("#BitIsChange").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsChange,
            value: setCmbBitIsChange()
        });
        function OnChangeBitIsChange() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsChange() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsChange;
            }
        }

        $("#Show").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangShow,
            value: setCmbShow()
        });
        function OnChangShow() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbShow() {
            if (e == null) {
                return true;
            } else {
                return dataItemX.Show;
            }
        }




        //Type//
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundJournalAccountType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundJournalAccountType,
                    value: setCmbFundJournalAccountType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeFundJournalAccountType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFundJournalAccountType() {
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
        //CurrencyPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeCurrencyPK,
                    value: setCmbCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.CurrencyPK;
                }
            }
        }

        //ParentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountParentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParentPK").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeParentPK,
                    value: setCmbParentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeParentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbParentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ParentPK == 0) {
                    return "";
                } else {
                    return dataItemX.ParentPK;
                }
            }
        }

        //Fxd11AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fxd11Account/GetFxd11AccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Fxd11AccountPK").kendoComboBox({
                    dataValueField: "Fxd11AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFxd11AccountPK,
                    value: setCmbFxd11AccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeFxd11AccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFxd11AccountPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Fxd11AccountPK == 0) {
                    return "";
                } else {
                    return dataItemX.Fxd11AccountPK;
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
        $("#FundJournalAccountPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Type").val("");
        $("#Groups").val("");
        $("#BitIsChange").val("");
        $("#Levels").val("");
        $("#ParentPK").val("");
        $("#Depth").val("");
        $("#CurrencyPK").val("");
        $("#Show").val(""); 
        $("#Fxd11AccountPK").val("");
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
                             FundJournalAccountPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             Groups: { type: "boolean" },
                             BitIsChange: { type: "boolean" },
                             Levels: { type: "number" },
                             ParentPK: { type: "number" },
                             ParentID: { type: "string" },
                             ParentName: { type: "string" },
                             Depth: { type: "number" },
                             CurrencyPK: { type: "number" },
                             CurrencyID: { type: "string" },
                             Show: { type: "boolean" },
                             ParentPK1: { type: "number" },
                             ParentPK2: { type: "number" },
                             ParentPK3: { type: "number" },
                             ParentPK4: { type: "number" },
                             ParentPK5: { type: "number" },
                             ParentPK6: { type: "number" },
                             ParentPK7: { type: "number" },
                             ParentPK8: { type: "number" },
                             ParentPK9: { type: "number" },
                             Fxd11AccountPK: { type: "number" },
                             Fxd11AccountID: { type: "string" },
                             Fxd11AccountName: { type: "string" },
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
            var gridApproved = $("#gridFundJournalAccountApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundJournalAccountPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundJournalAccountHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundJournalAccountApprovedURL = window.location.origin + "/Radsoft/FundJournalAccount/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FundJournalAccountApprovedURL);

        $("#gridFundJournalAccountApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Journal Account"
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
                { field: "FundJournalAccountPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 400 },
                { field: "TypeDesc", title: "Type", width: 200 },
                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                { field: "BitIsChange", title: "Is Change", width: 200, template: "#= BitIsChange ? 'Yes' : 'No' #" },
                { field: "Levels", title: "Levels", width: 200, attributes: { style: "text-align:right;" } },
                { field: "ParentID", title: "Parent ID", width: 200 },
                { field: "ParentName", title: "Parent Name", width: 400 },
                { field: "Depth", title: "Depth", width: 200, attributes: { style: "text-align:right;" } },
                { field: "CurrencyID", title: "Currency ID", width: 200 },
                { field: "Show", title: "Show", width: 200, template: "#= Show ? 'Yes' : 'No' #" },
                { field: "Fxd11AccountID", title: "Parent ID", width: 200 },
                { field: "Fxd11AccountName", title: "Parent Name", width: 400 },
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
        $("#TabFundJournalAccount").kendoTabStrip({
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
                        var FundJournalAccountPendingURL = window.location.origin + "/Radsoft/FundJournalAccount/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FundJournalAccountPendingURL);
                        $("#gridFundJournalAccountPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Journal Account"
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
                                { field: "FundJournalAccountPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "BitIsChange", title: "Is Change", width: 200, template: "#= BitIsChange ? 'Yes' : 'No' #" },
                                { field: "Levels", title: "Levels", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "ParentID", title: "Parent ID", width: 200 },
                                { field: "ParentName", title: "Parent Name", width: 400 },
                                { field: "Depth", title: "Depth", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "Show", title: "Show", width: 200, template: "#= Show ? 'Yes' : 'No' #" },
                                { field: "Fxd11AccountID", title: "Parent ID", width: 200 },
                                { field: "Fxd11AccountName", title: "Parent Name", width: 400 },
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

                        var FundJournalAccountHistoryURL = window.location.origin + "/Radsoft/FundJournalAccount/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FundJournalAccountHistoryURL);

                        $("#gridFundJournalAccountHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Journal Account"
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
                                { field: "FundJournalAccountPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "BitIsChange", title: "Is Change", width: 200, template: "#= BitIsChange ? 'Yes' : 'No' #" },
                                { field: "Levels", title: "Levels", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "ParentID", title: "Parent ID", width: 200 },
                                { field: "ParentName", title: "Parent Name", width: 400 },
                                { field: "Depth", title: "Depth", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "Show", title: "Show", width: 200, template: "#= Show ? 'Yes' : 'No' #" },
                                { field: "Fxd11AccountID", title: "Parent ID", width: 200 },
                                { field: "Fxd11AccountName", title: "Parent Name", width: 400 },
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
        var grid = $("#gridFundJournalAccountHistory").data("kendoGrid");
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
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "FundJournalAccount",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var FundJournalAccount = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Type: $('#Type').val(),
                                    Groups: $('#Groups').val(),
                                    BitIsChange: $('#BitIsChange').val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    Show: $('#Show').val(),
                                    Fxd11AccountPK: $('#Fxd11AccountPK').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournalAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalAccount_I",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournalAccount),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalAccountPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalAccount",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundJournalAccount = {
                                    FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Type: $('#Type').val(),
                                    Groups: $('#Groups').val(),
                                    BitIsChange: $('#BitIsChange').val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    Show: $('#Show').val(),
                                    Fxd11AccountPK: $('#Fxd11AccountPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournalAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalAccount_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournalAccount),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalAccountPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalAccount",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundJournalAccount" + "/" + $("#FundJournalAccountPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalAccountPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalAccount",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var FundJournalAccount = {
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundJournalAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalAccount_A",
                                type: 'POST',
                                data: JSON.stringify(FundJournalAccount),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalAccountPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalAccount",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundJournalAccount = {
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundJournalAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalAccount_V",
                                type: 'POST',
                                data: JSON.stringify(FundJournalAccount),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalAccountPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalAccount",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundJournalAccount = {
                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundJournalAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalAccount_R",
                                type: 'POST',
                                data: JSON.stringify(FundJournalAccount),
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


    $("#BtnFundJournalAccountRestructure").click(function () {
        
        alertify.confirm("Are you sure want to Restructure FundJournalAccount?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalAccount/FundJournalAccountUpdateParentAndDepth/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
});
