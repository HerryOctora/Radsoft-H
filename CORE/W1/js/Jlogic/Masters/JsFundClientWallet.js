$(document).ready(function () {
    document.title = 'FORM FUND CLIENT WALLET';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var WinListFundClient;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;
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

        win = $("#WinFundClientWallet").kendoWindow({
            height: 600,
            title: "Fund Client Wallet Detail",
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

        WinListFundClient = $("#WinListFundClient").kendoWindow({
            height: 450,
            title: "List Fund Client ",
            visible: false,
            width: 750,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListFundClientClose
        }).data("kendoWindow");

        function onWinListFundClientClose() {
            $("#gridListFundClient").empty();
        }

    }

    var GlobValidator = $("#WinFundClientWallet").kendoValidator().data("kendoValidator");

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
        $("#btnListFundClientPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")

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

            $("#FundClientWalletPK").val(dataItemX.FundClientWalletPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
            $("#WalletPK").val(dataItemX.WalletPK);
            $("#WalletID").val(dataItemX.WalletID);
            $("#BankPK").val(dataItemX.BankPK);
            $("#BankID").val(dataItemX.BankID);           
            $("#Debit").val(dataItemX.Debit);            
            $("#Credit").val(dataItemX.Credit);
            $("#WalletTransactionType").val(dataItemX.WalletTransactionType);
            $("#WalletTransactionStatus").val(dataItemX.WalletTransactionStatus);
            $("#IsProcessed").val(dataItemX.IsProcessed);
            $("#Description").val(dataItemX.Description);
            $("#Balance").val(dataItemX.Balance);
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

        $("#Balance").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setBalance()
        });
        function setBalance() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.Balance;
            }
        }

        $("#Debit").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setDebit()
        });
        function setDebit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.Debit;
            }
        }

        $("#Credit").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setCredit()
        });
        function setCredit() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.Credit;
            }
        }

        //combo box BitDefault
        $("#IsProcessed").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false },
                
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeIsProcessed,
            value: setCmbIsProcessed()
        });

        function OnChangeIsProcessed() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            
        }

        function setCmbIsProcessed() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.IsProcessed;
            }

        }

        //WalletTransactionType
        $("#WalletTransactionType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Topup", value: 1 },
                { text: "Buy", value: 2 },
                { text: "Sell", value: 3 },
                { text: "Interest and adjustment", value: 4 },
                { text: "Bank Pembayar", value: 5 },
                { text: "Withdraw", value: 6 },

            ],
            filter: "contains",
            suggest: true,
            change: OnChangeWalletTransactionType,
            value: setCmbWalletTransactionType()
        });

        function OnChangeWalletTransactionType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }

        function setCmbWalletTransactionType() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.WalletTransactionType;
            }

        }

        //WalletTransactionStatus
        $("#WalletTransactionStatus").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Complete", value: 0 },
                { text: "WaitForRdn", value: 1 },
                { text: "Pending", value: 2 },
                { text: "Reject", value: 3 },
              

            ],
            filter: "contains",
            suggest: true,
            change: OnChangeWalletTransactionStatus,
            value: setCmbWalletTransactionStatus()
        });

        function OnChangeWalletTransactionStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }

        function setCmbWalletTransactionStatus() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.WalletTransactionStatus;
            }

        }

        ////combo box FundClientPK
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#FundClientPK").kendoComboBox({
        //            dataValueField: "FundClientPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: OnChangeFundClientPK,
        //            value: setCmbFundClientPK()
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeFundClientPK() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        //function setCmbFundClientPK() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.FundClientPK == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.FundClientPK;
        //        }
        //    }
        //}

        //combo box WalletPK
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Wallet/GetWalletCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#WalletPK").kendoComboBox({
        //            dataValueField: "WalletPK",
        //            dataTextField: "Name",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: OnChangeWalletPK,
        //            value: setCmbWalletPK()
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeWalletPK() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        //function setCmbWalletPK() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.WalletPK == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.WalletPK;
        //        }
        //    }
        //}

        //function OnChangeWalletPK() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        //combo box BankPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBankPK,
                    value: setCmbBankPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeBankPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBankPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankPK;
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
        $("#FundClientWalletPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#FundClientname").val("");
        $("#WalletPK").val("");
        $("#BankPK").val("");
        $("#BankID").val("");      
        $("#Debit").val("");
        $("#Credit").val("");
        $("#WalletTransactionType").val("");
        $("#WalletTransactionStatus").val("");
        $("#IsProcessed").val("");
        $("#Description").val("");
        $("#Balance").val("");
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
                             FundClientWalletPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             WalletPK: { type: "string" },
                             BankID: { type: "string" },
                             Debit: { type: "number" },
                             Credit: { type: "number" },
                             WalletTransactionType: { type: "string" },
                             WalletTransactionStatus: { type: "string" },
                             IsProcessed: { type: "string" },
                             Description: { type: "string" },
                             Balance: { type: "number" },
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
            var gridApproved = $("#gridFundClientWalletApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundClientWalletPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundClientWalletHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundClientWalletApprovedURL = window.location.origin + "/Radsoft/FundClientWallet/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FundClientWalletApprovedURL);

        $("#gridFundClientWalletApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Wallet"
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
                { field: "FundClientWalletPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundClientID", title: "Client ID", width: 120 },
                { field: "FundClientName", title: "Name", width: 300 },
                { field: "WalletPK", title: "Wallet", width: 200 },
                { field: "BankID", title: "Bank", width: 200 },
                { field: "Debit", title: "Debit", width: 200 },
                { field: "Credit", title: "Credit", width: 200 },
                { field: "WalletTransactionType", title: "WalletTransactionType", width: 200 },
                { field: "WalletTransactionStatus", title: "WalletTransactionStatus", width: 200 },
                { field: "IsProcessed", title: "IsProcessed", width: 120 },
                { field: "Description", title: "Description", width: 200 },
                { field: "Balance", title: "Balance", width: 200 },
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
        $("#TabFundClientWallet").kendoTabStrip({
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
                        var FundClientWalletPendingURL = window.location.origin + "/Radsoft/FundClientWallet/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FundClientWalletPendingURL);
                        $("#gridFundClientWalletPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Client Wallet"
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
                                { field: "FundClientWalletPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundClientID", title: "Client ID", width: 120 },
                                { field: "FundClientName", title: "Name", width: 300 },
                                { field: "WalletPK", title: "Wallet", width: 200 },
                                { field: "BankID", title: "Bank", width: 200 },
                                { field: "Debit", title: "Debit", width: 200 },
                                { field: "Credit", title: "Credit", width: 200 },
                                { field: "WalletTransactionType", title: "WalletTransactionType", width: 200 },
                                { field: "WalletTransactionStatus", title: "WalletTransactionStatus", width: 200 },
                                { field: "IsProcessed", title: "IsProcessed", width: 120 },
                                { field: "Description", title: "Description", width: 200 },
                                { field: "Balance", title: "Balance", width: 200 },
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

                        var FundClientWalletHistoryURL = window.location.origin + "/Radsoft/FundClientWallet/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FundClientWalletHistoryURL);

                        $("#gridFundClientWalletHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Client Wallet"
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
                                { field: "FundClientWalletPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundClientID", title: "Client ID", width: 120 },
                                { field: "FundClientName", title: "Name", width: 300 },
                                { field: "WalletPK", title: "Wallet", width: 200 },
                                { field: "BankID", title: "Bank", width: 200 },
                                { field: "Debit", title: "Debit", width: 200 },
                                { field: "Credit", title: "Credit", width: 200 },
                                { field: "WalletTransactionType", title: "WalletTransactionType", width: 200 },
                                { field: "WalletTransactionStatus", title: "WalletTransactionStatus", width: 200 },
                                { field: "IsProcessed", title: "IsProcessed", width: 120 },
                                { field: "Description", title: "Description", width: 200 },
                                { field: "Balance", title: "Balance", width: 200 },
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
        var grid = $("#gridFundClientWalletHistory").data("kendoGrid");
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
                    var FundClientWallet = {
                        FundClientPK: $('#FundClientPK').val(),
                        WalletPK: $('#WalletPK').val(),
                        BankPK: $('#BankPK').val(),
                        Debit: $('#Debit').val(),
                        Credit: $('#Credit').val(),
                        WalletTransactionType: $('#WalletTransactionType').val(),
                        WalletTransactionStatus: $('#WalletTransactionStatus').val(),
                        IsProcessed: $('#IsProcessed').val(),
                        Description: $('#Description').val(),
                        Balance: $('#Balance').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundClientWallet/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClientWallet_I",
                        type: 'POST',
                        data: JSON.stringify(FundClientWallet),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientWalletPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClientWallet",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundClientWallet = {
                                    FundClientWalletPK: $('#FundClientWalletPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    WalletPK: $('#WalletPK').val(),
                                    BankPK: $('#BankPK').val(),
                                    Debit: $('#Debit').val(),
                                    Credit: $('#Credit').val(),
                                    WalletTransactionType: $('#WalletTransactionType').val(),
                                    WalletTransactionStatus: $('#WalletTransactionStatus').val(),
                                    IsProcessed: $('#IsProcessed').val(),
                                    Description: $('#Description').val(),
                                    Balance: $('#Balance').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundClientWallet/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClientWallet_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundClientWallet),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientWalletPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClientWallet",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundClientWallet" + "/" + $("#FundClientWalletPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientWalletPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClientWallet",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundClientWallet = {
                                FundClientWalletPK: $('#FundClientWalletPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClientWallet/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClientWallet_A",
                                type: 'POST',
                                data: JSON.stringify(FundClientWallet),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientWalletPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClientWallet",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundClientWallet = {
                                FundClientWalletPK: $('#FundClientWalletPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClientWallet/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClientWallet_V",
                                type: 'POST',
                                data: JSON.stringify(FundClientWallet),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundClientWalletPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundClientWallet",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundClientWallet = {
                                FundClientWalletPK: $('#FundClientWalletPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundClientWallet/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundClientWallet_R",
                                type: 'POST',
                                data: JSON.stringify(FundClientWallet),
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

    $("#btnListFundClientPK").click(function () {
        initListFundClientPK();

        WinListFundClient.center();
        WinListFundClient.open();
        htmlFundClientPK = "#FundClientPK";
        htmlFundClientID = "#FundClientID";



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
                     pageSize: 10,
                     schema: {
                         model: {
                             fields: {
                                 FundClientPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }

    function initListFundClientPK() {
        var dsListFundClient = getDataSourceListFundClient();
        $("#gridListFundClient").kendoGrid({
            dataSource: dsListFundClient,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListFundClientSelect }, title: " ", width: 85 },
               { field: "FundClientPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "Client ID", width: 300 },
               { field: "Name", title: "Client Name", width: 300 }

            ]
        });
    }

    function ListFundClientSelect(e) {
        var grid = $("#gridListFundClient").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $("#FundClientName").val(dataItemX.Name);
        $("#FundClientID").val(dataItemX.ID);
        $(htmlFundClientPK).val(dataItemX.FundClientPK);
        WinListFundClient.close();


    }

});