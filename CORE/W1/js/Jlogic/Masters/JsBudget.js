$(document).ready(function () {
    document.title = 'FORM Budget';
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
        $("#BtnBudgetReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnDownloadRpt").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnImportBudgetTemp").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });


    }



    function initWindow() {

        win = $("#WinBudget").kendoWindow({
            height: 450,
            title: "Budget Detail",
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

        WinBudgetRpt = $("#WinBudgetRpt").kendoWindow({
            height: 250,
            title: "Budget Report",
            visible: false,
            width: 500,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinBudgetRptClose
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinBudget").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    var GlobValidatorBudgetRpt = $("#WinBudgetRpt").kendoValidator().data("kendoValidator");
    function validateDataBudgetRpt() {

        if (GlobValidatorBudgetRpt.validate()) {
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

            $("#BudgetPK").val(dataItemX.BudgetPK);
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

        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReportPeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeReportPeriodPK,
                    value: setCmbReportPeriodPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeReportPeriodPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbReportPeriodPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ReportPeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.ReportPeriodPK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePeriodPK,
                    value: setCmbPeriodPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangePeriodPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPeriodPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodPK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeDepartmentPK,
                    value: setCmbDepartmentPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeDepartmentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDepartmentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepartmentPK == 0) {
                    return "";
                } else {
                    return dataItemX.DepartmentPK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAccountPK,
                    value: setCmbAccountPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAccountPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
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



        $("#MGTFee").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setMGTFee(),
        });

        function setMGTFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MGTFee == 0) {
                    return "";
                } else {
                    return dataItemX.MGTFee;
                }
            }
        }



        $("#Amount").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setAmount(),
        });

        function setAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Amount == 0) {
                    return "";
                } else {
                    return dataItemX.Amount;
                }
            }
        }

        $("#January").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setJanuary(),
        });

        function setJanuary() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.January == 0) {
                    return "";
                } else {
                    return dataItemX.January;
                }
            }
        }


        $("#February").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setFebruary(),
        });

        function setFebruary() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.February == 0) {
                    return "";
                } else {
                    return dataItemX.February;
                }
            }
        }


        $("#March").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setMarch(),
        });

        function setMarch() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.March == 0) {
                    return "";
                } else {
                    return dataItemX.March;
                }
            }
        }


        $("#April").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setApril(),
        });

        function setApril() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.April == 0) {
                    return "";
                } else {
                    return dataItemX.April;
                }
            }
        }

        $("#May").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setMay(),
        });

        function setMay() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.May == 0) {
                    return "";
                } else {
                    return dataItemX.May;
                }
            }
        }


        $("#June").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setJune(),
        });

        function setJune() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.June == 0) {
                    return "";
                } else {
                    return dataItemX.June;
                }
            }
        }

        $("#July").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setJuly(),
        });

        function setJuly() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.July == 0) {
                    return "";
                } else {
                    return dataItemX.July;
                }
            }
        }


        $("#August").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setAugust(),
        });

        function setAugust() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.August == 0) {
                    return "";
                } else {
                    return dataItemX.August;
                }
            }
        }

        $("#September").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setSeptember(),
        });

        function setSeptember() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.September == 0) {
                    return "";
                } else {
                    return dataItemX.September;
                }
            }
        }


        $("#October").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setOctober(),
        });

        function setOctober() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.October == 0) {
                    return "";
                } else {
                    return dataItemX.October;
                }
            }
        }

        $("#November").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setNovember(),
        });

        function setNovember() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.November == 0) {
                    return "";
                } else {
                    return dataItemX.November;
                }
            }
        }


        $("#December").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setDecember(),
        });

        function setDecember() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.December == 0) {
                    return "";
                } else {
                    return dataItemX.December;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/ItemBudget/GetItemBudgetCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ItemBudgetPK").kendoComboBox({
                    dataValueField: "ItemBudgetPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeItemBudgetPK,
                    value: setCmbItemBudgetPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function OnChangeItemBudgetPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbItemBudgetPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ItemBudgetPK == 0) {
                    return "";
                } else {
                    return dataItemX.ItemBudgetPK;
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
        $("#BudgetPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#DepartmentPK").val("");
        $("#AccountPK").val("");
        $("#ItemBudgetPK").val("");
        $("#Amount").val("");
        $("#January").val("");
        $("#February").val("");
        $("#March").val("");
        $("#April").val("");
        $("#May").val("");
        $("#June").val("");
        $("#July").val("");
        $("#August").val("");
        $("#September").val("");
        $("#October").val("");
        $("#November").val("");
        $("#December").val("");
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
                            BudgetPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ReportPeriodPK: { type: "number" },
                            ReportPeriodID: { type: "string" },
                            PeriodPK: { type: "number" },
                            PeriodID: { type: "string" },
                            DepartmentPK: { type: "number" },
                            DepartmentID: { type: "string" },
                            AccountPK: { type: "number" },
                            AccountID: { type: "string" },
                            ItemBudgetPK: { type: "number" },
                            ItemBudgetID: { type: "string" },
                            BudgetPK: { type: "number" },
                            BudgetID: { type: "string" },
                            Amount: { type: "number" },
                            January: { type: "number" },
                            February: { type: "number" },
                            March: { type: "number" },
                            April: { type: "number" },
                            May: { type: "number" },
                            June: { type: "number" },
                            July: { type: "number" },
                            August: { type: "number" },
                            September: { type: "number" },
                            October: { type: "number" },
                            November: { type: "number" },
                            December: { type: "number" },
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
            var gridApproved = $("#gridBudgetApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBudgetPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBudgetHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BudgetApprovedURL = window.location.origin + "/Radsoft/Budget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(BudgetApprovedURL);

        $("#gridBudgetApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Budget"
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
                { field: "BudgetPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ReportPeriodID", title: "Report Period", width: 200 },
                { field: "PeriodID", title: "Period", width: 200 },
                { field: "DepartmentID", title: "Department", width: 300 },
                { field: "AccountID", title: "Account", width: 300 },
                { field: "ItemBudgetID", title: "Item Budget", width: 300 },
                { field: "Amount", title: "Amount", width: 300 },
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
        $("#TabBudget").kendoTabStrip({
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
                        var BudgetPendingURL = window.location.origin + "/Radsoft/Budget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(BudgetPendingURL);
                        $("#gridBudgetPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Budget"
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
                                { field: "BudgetPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ReportPeriodID", title: "Report Period", width: 200 },
                                { field: "PeriodID", title: "Period", width: 200 },
                                { field: "DepartmentID", title: "Department", width: 300 },
                                { field: "AccountID", title: "Account", width: 300 },
                                { field: "ItemBudgetID", title: "Item Budget", width: 300 },
                                { field: "Amount", title: "Amount", width: 300 },
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

                        var BudgetHistoryURL = window.location.origin + "/Radsoft/Budget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(BudgetHistoryURL);

                        $("#gridBudgetHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Budget"
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
                                { field: "BudgetPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ReportPeriodID", title: "Report Period", width: 200 },
                                { field: "PeriodID", title: "Period", width: 200 },
                                { field: "DepartmentID", title: "Department", width: 300 },
                                { field: "AccountID", title: "Account", width: 300 },
                                { field: "ItemBudgetID", title: "Item Budget", width: 300 },
                                { field: "Amount", title: "Amount", width: 300 },
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
        var grid = $("#gridBudgetHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Budget/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DepartmentPK').val() + "/" + $('#AccountPK').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {

                                var Budget = {
                                    ReportPeriodPK: $('#ReportPeriodPK').val(),
                                    PeriodPK: $('#PeriodPK').val(),
                                    FundTypePK: $('#FundTypePK').val(),
                                    DepartmentPK: $('#DepartmentPK').val(),
                                    AccountPK: $('#AccountPK').val(),
                                    ItemBudgetPK: $('#ItemBudgetPK').val(),
                                    Amount: $('#Amount').val(),
                                    January: $('#January').val(),
                                    February: $('#February').val(),
                                    March: $('#March').val(),
                                    April: $('#April').val(),
                                    May: $('#May').val(),
                                    June: $('#June').val(),
                                    July: $('#July').val(),
                                    August: $('#August').val(),
                                    September: $('#September').val(),
                                    October: $('#October').val(),
                                    November: $('#November').val(),
                                    December: $('#December').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Budget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Budget_I",
                                    type: 'POST',
                                    data: JSON.stringify(Budget),
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
                                alertify.alert("Data Has Add!");
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
                        url: window.location.origin + "/Radsoft/Budget/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DepartmentPK').val() + "/" + $('#AccountPK').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {

                                var Budget = {
                                    BudgetPK: $('#BudgetPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ReportPeriodPK: $('#ReportPeriodPK').val(),
                                    PeriodPK: $('#PeriodPK').val(),
                                    FundTypePK: $('#FundTypePK').val(),
                                    DepartmentPK: $('#DepartmentPK').val(),
                                    AccountPK: $('#AccountPK').val(),
                                    ItemBudgetPK: $('#ItemBudgetPK').val(),
                                    Amount: $('#Amount').val(),
                                    January: $('#January').val(),
                                    February: $('#February').val(),
                                    March: $('#March').val(),
                                    April: $('#April').val(),
                                    May: $('#May').val(),
                                    June: $('#June').val(),
                                    July: $('#July').val(),
                                    August: $('#August').val(),
                                    September: $('#September').val(),
                                    October: $('#October').val(),
                                    November: $('#November').val(),
                                    December: $('#December').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Budget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Budget_U",
                                    type: 'POST',
                                    data: JSON.stringify(Budget),
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
                                alertify.alert("Data Has Add!");
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
            url: window.location.origin +"/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "Budget",
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
                                                url: window.location.origin +"/Radsoft/Host/GetCompareData/" + "Budget" + "/" + $("#BudgetPK").val(),
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
                    url: window.location.origin +"/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "Budget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Budget = {
                                BudgetPK: $('#BudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin +"/Radsoft/Budget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Budget_A",
                                type: 'POST',
                                data: JSON.stringify(Budget),
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
                    url: window.location.origin +"/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "Budget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Budget = {
                                BudgetPK: $('#BudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin +"/Radsoft/Budget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Budget_V",
                                type: 'POST',
                                data: JSON.stringify(Budget),
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
                    url: window.location.origin +"/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "Budget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Budget = {
                                BudgetPK: $('#BudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin +"/Radsoft/Budget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Budget_R",
                                type: 'POST',
                                data: JSON.stringify(Budget),
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

    
    function onWinBudgetRptClose() {
        GlobValidatorBudgetRpt.hideMessages();
        clearDataBudgetRpt();
    }

    function clearDataBudgetRpt() {
        $("#UpPeriodPK").val("");
    }

    $("#BtnBudgetReport").click(function () {

        // ComboBox Period
        $.ajax({
            url: window.location.origin +"/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UpPeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: true,
                    change: OnChangePeriodPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePeriodPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        $("#ReportName").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
                   { text: "01 Laporan Ledger Budget" },
                   { text: "02 Laporan Per Coa Per Cabang" },
                   { text: "03 Laporan FundFlow Cabang" },
                   { text: "04 Laporan FundFlow Per Sales" },
                   { text: "05 Laporan FundFlow Summary" },
                   { text: "06 Laporan AUM Cabang dan Sales" },
                   { text: "07 Laporan Aum Summary" },
                   { text: "08 Laporan MI Fee Cabang dan Sales" },
                   { text: "09 Laporan MI Fee Summary" },
                   { text: "10 Laporan Profit And Loss" }


            ],
            filter: "contains",
            suggest: true
        });

        clearDataBudgetRpt();
        showAddBudgetRpt();
    });

    function showAddBudgetRpt(e) {

        WinBudgetRpt.center();
        WinBudgetRpt.open();

    }


    $("#BtnDownloadRpt").click(function () {


        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var AccountingRpt = {
                    PeriodID: $("#UpPeriodPK").data("kendoComboBox").text(),
                    PeriodPK: $("#UpPeriodPK").data("kendoComboBox").value(),
                    ReportName: $("#ReportName").data("kendoComboBox").text(),

                };

                $.ajax({
                    url: window.location.origin +"/Radsoft/Budget/BudgetReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AccountingRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnImportBudgetTemp").click(function () {
        document.getElementById("FileImportBudgetTemp").click();
    });

    $("#FileImportBudgetTemp").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportBudgetTemp").get(0).files;
        //var _d = files[0].name.substring(0, 50);
        if (files.length > 0) {
            data.append("BudgetTemp", files[0]);
            $.ajax({
                url: window.location.origin +"/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FileImportBudgetTemp_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    alertify.alert(data);
                    $("#FileImportBudgetTemp").val("");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBudgetTemp").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportBudgetTemp").val("");
        }
    });

  
});
