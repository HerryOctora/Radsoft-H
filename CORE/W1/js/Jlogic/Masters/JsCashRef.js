$(document).ready(function () {
    document.title = 'FORM CASH REF';
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
    }

    
     
    function initWindow() {

        win = $("#WinCashRef").kendoWindow({
            height: 500,
            title: "CashRef Detail",
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



    var GlobValidator = $("#WinCashRef").kendoValidator().data("kendoValidator");

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

            $("#CashRefPK").val(dataItemX.CashRefPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#BankAccountNo").val(dataItemX.BankAccountNo);
            $("#BankAccountBranchName").val(dataItemX.BankAccountBranchName);
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

        // AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeAccountPK,
                    value: setCmbAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

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
        function onChangeAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Account/GetAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AccountPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                    },
                    error: function (data) {
                        alertify.alert("Please Select Account First");
                        $("#CurrencyPK").data("kendoComboBox").value("");
                    }
                });
            }
        }

        // CurrencyPK
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
                    change: onChangeCurrencyPK,
                    enabled: false,
                    value: setCmbCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCurrencyPK() {

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

        // BankBranchPK
        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BANK",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankBranchPK").kendoComboBox({
                    dataValueField: "BankBranchPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeBankBranchPK,
                    value: setCmbBankBranchPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBankBranchPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBankBranchPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankBranchPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankBranchPK;
                }
            }
        }

        $("#FundOperationBank").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeFundOperationBank,
            value: setCmbFundOperationBank()
        });
        function OnChangeFundOperationBank() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFundOperationBank() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.FundOperationBank;
            }
        }

        $("#CashRefType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Petty Cash", value: 1 },
                { text: "Bank", value: 2 },
                { text: "Deposito on Call", value: 3 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCashRefType,
            value: setCmbCashRefType()
        });
        function OnChangeCashRefType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($('#CashRefType').val() == 2) {
                $("#BankBranchPK").attr("required", true);
                $("#BankAccountNo").attr("required", true);
            }
            else {
                $("#BankBranchPK").attr("required", false);
                $("#BankAccountNo").attr("required", false);
            }
        }

        function setCmbCashRefType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CashRefType;
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
        $("#CashRefPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#AccountPK").val("");
        $("#CurrencyPK").val("");
        $("#BankBranchPK").val("");
        $("#BankAccountNo").val("");
        $("#BankAccountBranchName").val("");
        $("#FundOperationBank").val("");
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
                             CashRefPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             AccountPK: { type: "number" },
                             AccountID: { type: "string" },
                             AccountName: { type: "string" },
                             CurrencyPK: { type: "number" },
                             CurrencyID: { type: "string" },
                             BankBranchPK: { type: "number" },
                             BankBranchID: { type: "string" },
                             BankBranchName: { type: "string" },
                             BankAccountNo: { type: "string" },
                             BankAccountBranchName: { type: "string" },
                             FundOperationBank: { type: "boolean" },
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
            var gridApproved = $("#gridCashRefApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCashRefPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCashRefHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CashRefApprovedURL = window.location.origin + "/Radsoft/CashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(CashRefApprovedURL);

        $("#gridCashRefApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form CashRef"
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
                { field: "CashRefPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "AccountID", title: "Account ID", width: 250 },
                { field: "AccountName", title: "Acc. Name", width: 400 },
                { field: "CurrencyID", title: "Currency ID", width: 200 },
                { field: "BankBranchID", title: "Bank ID", width: 250 },
                { field: "BankBranchName", title: "Bank Name", width: 400 },
                { field: "BankAccountNo", title: "Account No", width: 200 },
                { field: "BankAccountBranchName", title: "Bank Account Branch Name", width: 300 },
                { field: "CashRefType", title: "CashRef Type", hidden: true, width: 250 },
                { field: "CashRefTypeDesc", title: "CashRef Type", width: 250 },
                { field: "FundOperationBank", title: "Fund Operation Bank", width: 200, template: "#= FundOperationBank ? 'Yes' : 'No' #" },
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
        $("#TabCashRef").kendoTabStrip({
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
                        var CashRefPendingURL = window.location.origin + "/Radsoft/CashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(CashRefPendingURL);
                        $("#gridCashRefPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CashRef"
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
                                { field: "CashRefPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "AccountID", title: "Account ID", width: 250 },
                                { field: "AccountName", title: "Acc. Name", width: 400 },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "BankBranchID", title: "Bank ID", width: 250 },
                                { field: "BankBranchName", title: "Bank Name", width: 400 },
                                { field: "BankAccountNo", title: "Account No", width: 200 },
                                { field: "CashRefType", title: "CashRef Type", hidden: true, width: 250 },
                                { field: "CashRefTypeDesc", title: "CashRef Type", width: 250 },
                                { field: "BankAccountBranchName", title: "Bank Account Branch Name", width: 300 },
                                { field: "FundOperationBank", title: "Fund Operation Bank", width: 200, template: "#= FundOperationBank ? 'Yes' : 'No' #" },
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

                        var CashRefHistoryURL = window.location.origin + "/Radsoft/CashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(CashRefHistoryURL);

                        $("#gridCashRefHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CashRef"
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
                                { field: "CashRefPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "AccountID", title: "Account ID", width: 250 },
                                { field: "AccountName", title: "Acc. Name", width: 400 },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "BankBranchID", title: "Bank ID", width: 250 },
                                { field: "BankBranchName", title: "Bank Name", width: 400 },
                                { field: "BankAccountNo", title: "Account No", width: 200 },
                                { field: "BankAccountBranchName", title: "Bank Account Branch Name", width: 300 },
                                { field: "CashRefType", title: "CashRef Type", width: 250 },
                                { field: "CashRefType", title: "CashRef Type", hidden: true, width: 250 },
                                { field: "CashRefTypeDesc", title: "CashRef Type",  width: 250 },
                                { field: "FundOperationBank", title: "Fund Operation Bank", width: 200, template: "#= FundOperationBank ? 'Yes' : 'No' #" },
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
        var grid = $("#gridCashRefHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "CashRef",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var CashRef = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    AccountPK: $('#AccountPK').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    FundOperationBank: $('#FundOperationBank').val(),
                                    CashRefType: $('#CashRefType').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashRef_I",
                                    type: 'POST',
                                    data: JSON.stringify(CashRef),
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

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "CashRef",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var CashRef = {
                                    CashRefPK: $('#CashRefPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    AccountPK: $('#AccountPK').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    BankAccountBranchName: $('#BankAccountBranchName').val(),
                                    FundOperationBank: $('#FundOperationBank').val(),
                                    CashRefType: $('#CashRefType').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashRef_U",
                                    type: 'POST',
                                    data: JSON.stringify(CashRef),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "CashRef",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CashRef" + "/" + $("#CashRefPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "CashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CashRef = {
                                CashRefPK: $('#CashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashRef_A",
                                type: 'POST',
                                data: JSON.stringify(CashRef),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "CashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CashRef = {
                                CashRefPK: $('#CashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashRef_V",
                                type: 'POST',
                                data: JSON.stringify(CashRef),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "CashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CashRef = {
                                CashRefPK: $('#CashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CashRef_R",
                                type: 'POST',
                                data: JSON.stringify(CashRef),
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
