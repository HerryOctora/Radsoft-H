$(document).ready(function () {
    document.title = 'FORM FUND CASH REF';
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

    if (_GlobClientCode == "22") {
        $("#LblSafeKeepingAccountNo").show();
    }
    else {
        $("#LblSafeKeepingAccountNo").hide();
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

        win = $("#WinFundCashRef").kendoWindow({
            height: 700,
            title: "Fund Cash Ref Detail",
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

    var GlobValidator = $("#WinFundCashRef").kendoValidator().data("kendoValidator");

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

            $("#FundCashRefPK").val(dataItemX.FundCashRefPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#BankAccountNo").val(dataItemX.BankAccountNo);
            $("#BankAccountName").val(dataItemX.BankAccountName);
            $("#SafeKeepingAccountNo").val(dataItemX.SafeKeepingAccountNo);
            $("#Remark").val(dataItemX.Remark);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#IsPublic").prop('checked', dataItemX.IsPublic);
            $("#bitdefaultinvestment").prop('checked', dataItemX.bitdefaultinvestment);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

        // Fund Journal AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundJournalAccountPK").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundJournalAccountPK,
                    value: setCmbFundJournalAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

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
        function onChangeFundJournalAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountByPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundJournalAccountPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#CurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                    },
                    error: function (data) {
                        $("#CurrencyPK").data("kendoComboBox").value("");
                    }
                });
            }
        }

        //Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TypeUnitRegistry",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeType,
                    value: setCmbType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbType() {
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
                    enabled: true,
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

        // BankCustodianPK
        //BankBranchPK
        $.ajax({
            url: window.location.origin + "/Radsoft/BankBranch/GetBankBranchComboCustodiOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankBranchPK").kendoComboBox({
                    dataValueField: "BankBranchPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankBranchPK,
                    dataSource: data,
                    value: setCmbBankBranchPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankBranchPK() {

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

        // FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
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
                    return "All";
                } else {
                    return dataItemX.FundPK;
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
        $("#FundCashRefPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Type").val("");
        $("#FundJournalAccountPK").val("");
        $("#CurrencyPK").val("");
        $("#BankBranchPK").val("");
        $("#BankAccountNo").val("");
        $("#BankAccountName").val("");
        $("#SafeKeepingAccountNo").val("");
        $("#FundPK").val("");
        $("#Remark").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#IsPublic").val("");
        $("#bitdefaultinvestment").val("");
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
                            FundCashRefPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            FundJournalAccountPK: { type: "number" },
                            FundJournalAccountID: { type: "string" },
                            FundJournalAccountName: { type: "string" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            BankBranchPK: { type: "number" },
                            BankBranchID: { type: "string" },
                            BankBranchName: { type: "string" },
                            BankAccountNo: { type: "string" },
                            BankAccountName: { type: "string" },
                            SafeKeepingAccountNo: { type: "string" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            FundName: { type: "string" },
                            Remark: { type: "string" },
                            EntryUsersID: { type: "string" },
                            EntryTime: { type: "date" },
                            UpdateUsersID: { type: "string" },
                            UpdateTime: { type: "date" },
                            ApprovedUsersID: { type: "string" },
                            ApprovedTime: { type: "date" },
                            VoidUsersID: { type: "string" },
                            VoidTime: { type: "date" },
                            IsPublic: { type: "bool" },
                            bitdefaultinvestment: { type: "bool" },
                            LastUpdate: { type: "date" },
                            Timestamp: { type: "string" }
                        }
                    }
                }
            });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridFundCashRefApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundCashRefPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundCashRefHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundCashRefApprovedURL = window.location.origin + "/Radsoft/FundCashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FundCashRefApprovedURL);

        $("#gridFundCashRefApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Cash Ref"
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
            dataBound: function (e) {
                if (_GlobClientCode != "22") {
                    this.hideColumn('SafeKeepingAccountNo');
                }
            },
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "FundCashRefPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "FundID", title: "Fund ID", width: 250 },
                { field: "FundName", title: "Fund Name", width: 400 },
                { field: "TypeDesc", title: "Type", width: 300 },
                { field: "FundJournalAccountID", title: "Fund Journal Account ID", width: 250 },
                { field: "FundJournalAccountName", title: "Fund Journal Acc. Name", width: 400 },
                { field: "CurrencyID", title: "Currency ID", width: 200 },
                { field: "BankCustodianID", title: "Bank ID", width: 250 },
                { field: "BankCustodianName", title: "Bank Name", width: 400 },
                { field: "BankAccountNo", title: "Account No", width: 200 },
                { field: "BankAccountName", title: "Account Name", width: 200 },
                { field: "SafeKeepingAccountNo", title: "Safe Keeping Account No", width: 200 },
                { field: "IsPublic", title: "IsPublic", width: 100, template: "#= IsPublic ? 'Yes' : 'No' #" },
                { field: "bitdefaultinvestment", title: "bitdefaultinvestment", width: 100, template: "#= bitdefaultinvestment ? 'Yes' : 'No' #" },
                { field: "Remark", title: "Remark", width: 300 },
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
        $("#TabFundCashRef").kendoTabStrip({
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
                        var FundCashRefPendingURL = window.location.origin + "/Radsoft/FundCashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FundCashRefPendingURL);
                        $("#gridFundCashRefPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FundCashRef"
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
                            dataBound: function (e) {
                                if (_GlobClientCode != "22") {
                                    this.hideColumn('SafeKeepingAccountNo');
                                }
                            },
                            columns: [
                                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                { field: "FundCashRefPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "FundID", title: "Fund ID", width: 250 },
                                { field: "FundName", title: "Fund Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "FundJournalAccountID", title: "Fund Journal Account ID", width: 250 },
                                { field: "FundJournalAccountName", title: "Fund Journal Acc. Name", width: 400 },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "BankCustodianID", title: "Bank ID", width: 250 },
                                { field: "BankCustodianName", title: "Bank Name", width: 400 },
                                { field: "BankAccountNo", title: "Account No", width: 200 },
                                { field: "BankAccountName", title: "Account Name", width: 200 },
                                { field: "SafeKeepingAccountNo", title: "Safe Keeping Account No", width: 200 },
                                { field: "IsPublic", title: "IsPublic", width: 100, template: "#= IsPublic ? 'Yes' : 'No' #" },
                                { field: "bitdefaultinvestment", title: "bitdefaultinvestment", width: 100, template: "#= bitdefaultinvestment ? 'Yes' : 'No' #" },
                                { field: "Remark", title: "Remark", width: 300 },
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

                        var FundCashRefHistoryURL = window.location.origin + "/Radsoft/FundCashRef/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FundCashRefHistoryURL);

                        $("#gridFundCashRefHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FundCashRef"
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
                                { field: "FundCashRefPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "FundID", title: "Fund ID", width: 250 },
                                { field: "FundName", title: "Fund Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 300 },
                                { field: "FundJournalAccountID", title: "Fund Journal Account ID", width: 250 },
                                { field: "FundJournalAccountName", title: "Fund Journal Acc. Name", width: 400 },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "BankCustodianID", title: "Bank ID", width: 250 },
                                { field: "BankCustodianName", title: "Bank Name", width: 400 },
                                { field: "BankAccountNo", title: "Account No", width: 200 },
                                { field: "BankAccountName", title: "Account Name", width: 200 },
                                { field: "SafeKeepingAccountNo", title: "Safe Keeping Account No", width: 200 },
                                { field: "IsPublic", title: "IsPublic", width: 100, template: "#= IsPublic ? 'Yes' : 'No' #" },
                                { field: "bitdefaultinvestment", title: "bitdefaultinvestment", width: 100, template: "#= bitdefaultinvestment ? 'Yes' : 'No' #" },
                                { field: "Remark", title: "Remark", width: 300 },
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
        var grid = $("#gridFundCashRefHistory").data("kendoGrid");
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

        if (_GlobClientCode != "22") {
            grid.hideColumn('SafeKeepingAccountNo');
        }
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
            var CheckBitDefaultInvestment = {
                FundPK: $('#FundPK').val(),
                bitdefaultinvestment: $('#bitdefaultinvestment').is(":checked"),

            };
            $.ajax({
                url: window.location.origin + "/Radsoft/FundCashRef/CheckBitDefaultInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(CheckBitDefaultInvestment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Add data?", function (e) {
                            if (e) {

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "FundCashRef",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == true) {

                                            var FundCashRef = {
                                                ID: $('#ID').val(),
                                                Name: $('#Name').val(),
                                                Type: $('#Type').val(),
                                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                                CurrencyPK: $('#CurrencyPK').val(),
                                                BankCustodianPK: $('#BankCustodianPK').val(),
                                                BankAccountNo: $('#BankAccountNo').val(),
                                                BankAccountName: $('#BankAccountName').val(),
                                                SafeKeepingAccountNo: $('#SafeKeepingAccountNo').val(),
                                                FundPK: $('#FundPK').val(),
                                                BankBranchPK: $('#BankBranchPK').val(),
                                                IsPublic: $('#IsPublic').is(":checked"),
                                                bitdefaultinvestment: $('#bitdefaultinvestment').is(":checked"),
                                                Remark: $('#Remark').val(),
                                                EntryUsersID: sessionStorage.getItem("user")

                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FundCashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundCashRef_I",
                                                type: 'POST',
                                                data: JSON.stringify(FundCashRef),
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

                    } else {
                        alertify.alert("Other default fund cash ref already exist, please uncheck the other one first if you wish to choose this one to use as default.");
                        return;
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            var CheckBitDefaultInvestment = {
                FundPK: $('#FundPK').val(),
                bitdefaultinvestment: $('#bitdefaultinvestment').is(":checked"),

            };
            $.ajax({
                url: window.location.origin + "/Radsoft/FundCashRef/CheckBitDefaultInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(CheckBitDefaultInvestment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundCashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundCashRef",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                            var FundCashRef = {
                                                FundCashRefPK: $('#FundCashRefPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                ID: $('#ID').val(),
                                                Name: $('#Name').val(),
                                                Type: $('#Type').val(),
                                                FundJournalAccountPK: $('#FundJournalAccountPK').val(),
                                                CurrencyPK: $('#CurrencyPK').val(),
                                                BankCustodianPK: $('#BankCustodianPK').val(),
                                                BankAccountNo: $('#BankAccountNo').val(),
                                                BankAccountName: $('#BankAccountName').val(),
                                                SafeKeepingAccountNo: $('#SafeKeepingAccountNo').val(),
                                                BankBranchPK: $('#BankBranchPK').val(),
                                                FundPK: $('#FundPK').val(),
                                                Remark: $('#Remark').val(),
                                                IsPublic: $('#IsPublic').is(":checked"),
                                                bitdefaultinvestment: $('#bitdefaultinvestment').is(":checked"),
                                                Notes: str,
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FundCashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundCashRef_U",
                                                type: 'POST',
                                                data: JSON.stringify(FundCashRef),
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

                    } else {
                        alertify.alert("Other default fund cash ref already exist, please uncheck the other one first if you wish to choose this one to use as default.");
                        return;
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundCashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundCashRef",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundCashRef" + "/" + $("#FundCashRefPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundCashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundCashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundCashRef = {
                                FundCashRefPK: $('#FundCashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundCashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundCashRef_A",
                                type: 'POST',
                                data: JSON.stringify(FundCashRef),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundCashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundCashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundCashRef = {
                                FundCashRefPK: $('#FundCashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundCashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundCashRef_V",
                                type: 'POST',
                                data: JSON.stringify(FundCashRef),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundCashRefPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundCashRef",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundCashRef = {
                                FundCashRefPK: $('#FundCashRefPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundCashRef/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundCashRef_R",
                                type: 'POST',
                                data: JSON.stringify(FundCashRef),
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
