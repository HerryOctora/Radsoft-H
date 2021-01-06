$(document).ready(function () {
    document.title = 'FORM ACCOUNT BUDGET';
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


    //FOR CLient 03
    if (_GlobClientCode == "03") {
        $("#LblEmployee").show();
        $("#LblOffice").hide();
        $("#LblKindOfCost").show();
        $("#LblAgent").show();
        $("#LblInstrument").show();
        $("#LblClient").show();
    }
    else {
        $("#LblEmployee").hide();
        $("#LblOffice").show();
        $("#LblKindOfCost").hide();
        $("#LblAgent").hide();
        $("#LblInstrument").hide();
        $("#LblClient").hide();
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
        $("#BtnImportAccountBudget").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        }); 
        $("#BtnImportA").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        }); 
    }

    $("#BtnImportA").click(function () {
        showWinImport();
    });


    function showWinImport() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#IPeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeIPeriodPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeIPeriodPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
    

        WinGenerateImport.center();
        WinGenerateImport.open();

    }
    

    function initWindow() {

        win = $("#WinAccountBudget").kendoWindow({
            height: 600,
            title: "AccountBudget Detail",
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


        WinGenerateImport = $("#WinGenerateImport").kendoWindow({
            height: "450px",
            title: "* Report",
            visible: false,
            width: "650px",
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateImportClose
        }).data("kendoWindow");

    }



    var GlobValidator = $("#WinAccountBudget").kendoValidator().data("kendoValidator");
    var GlobValidatorImport = $("#WinGenerateImport").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function onWinGenerateImportClose() {
        $("#IPeriodPK").val("");
        $("#IPeriodPK").attr("required", false);
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
                //$("#Version").attr('readonly', true);

            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#Version").attr('readonly', true);
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

            $("#AccountBudgetPK").val(dataItemX.AccountBudgetPK);
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
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangePeriodPK,
                    value: setCmbPeriodPK()
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

        function setCmbPeriodPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PeriodPK;
            }
        }

        //Month//
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Month",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Month").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeMonth,
                    value: setCmbMonth()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeMonth() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbMonth() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Month == 0) {
                    return "";
                } else {
                    return dataItemX.Month;
                }
            }
        }
        //AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAccount,
                    value: setCmbAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeAccount() {
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

        //OfficePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#OfficePK").kendoComboBox({
                    dataValueField: "OfficePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeOffice,
                    value: setCmbOfficePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeOffice() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbOfficePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OfficePK == 0) {
                    return "";
                } else {
                    return dataItemX.OfficePK;
                }
            }
        }

        //DepartmentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDepartment,
                    value: setCmbDepartmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeDepartment() {
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

        //FOR CLient 03

        //Combo box AgentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAgentPK,
                    dataSource: data,
                    value: setAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeAgentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setAgentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.AgentPK;
                }
            }
        }

        //Combo box CounterpartPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeCounterpartPK,
                    suggest: true,
                    dataSource: data,
                    value: setCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCounterpartPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCounterpartPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CounterpartPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.CounterpartPK;
                }
            }
        }

        //Combo box InstrumentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeInstrumentPK,
                    suggest: true,
                    dataSource: data,
                    value: setInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.InstrumentPK;
                }
            }
        }

        //Combo box ConsigneePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Consignee/GetConsigneeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ConsigneePK").kendoComboBox({
                    dataValueField: "ConsigneePK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeConsigneePK,
                    suggest: true,
                    dataSource: data,
                    value: setCmbConsigneePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeConsigneePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbConsigneePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ConsigneePK == 0) {
                    return "";
                } else {
                    if (dataItemX.ConsigneePK == 0) {
                        return "";
                    }
                    else {
                        return dataItemX.ConsigneePK;
                    }
                }
            }
        }

        $("#Balance").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setBalance()
        });
        function setBalance() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Balance;
            }
        }

        //Version
        $.ajax({
            url: window.location.origin + "/Radsoft/AccountBudget/GetAccountBudgetCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Version").kendoComboBox({
                    dataValueField: "Version",
                    dataTextField: "Version",
                    dataSource: data,
                    value: setCmbVersion()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




        function setCmbVersion() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.Version;
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
        $("#AccountBudgetPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Version").val("");
        $("#PeriodPK").val("");
        $("#AccountPK").val("");
        $("#OfficePK").val("");
        $("#DepartmentPK").val("");
        $("#Balance").val("");
        $("#Month").val("");
        //FOR CLient 03
        $("#AgentPK").val("");
        $("#InstrumentPK").val("");
        $("#CounterpartPK").val("");
        $("#ConsigneePK").val("");
        //End FOR CLient 03
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
                            AccountBudgetPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Version: { type: "string" },
                            PeriodPK: { type: "number" },
                            PeriodID: { type: "string" },
                            AccountPK: { type: "number" },
                            AccountID: { type: "string" },
                            OfficePK: { type: "number" },
                            OfficeID: { type: "string" },
                            DepartmentPK: { type: "number" },
                            DepartmentID: { type: "string" },
                            Balance: { type: "number" },
                            Month: { type: "number" },
                            MonthDesc: { type: "string" },
                            //FOR CLient 03
                            AgentPK: { type: "number" },
                            AgentID: { type: "string" },
                            AgentName: { type: "string" },
                            CounterpartPK: { type: "number" },
                            CounterpartID: { type: "string" },
                            CounterpartName: { type: "string" },
                            InstrumentPK: { type: "number" },
                            InstrumentID: { type: "string" },
                            InstrumentName: { type: "string" },
                            ConsigneePK: { type: "number" },
                            ConsigneeID: { type: "string" },
                            ConsigneeName: { type: "string" },
                            //End FOR CLient 03
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
            var gridApproved = $("#gridAccountBudgetApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAccountBudgetPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAccountBudgetHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var AccountBudgetApprovedURL = window.location.origin + "/Radsoft/AccountBudget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(AccountBudgetApprovedURL);

        if (_GlobClientCode == "03") {
            $("#gridAccountBudgetApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form AccountBudget"
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
                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Version", title: "Version", width: 200 },
                    { field: "PeriodID", title: "Period ID", width: 200 },
                    { field: "AccountID", title: "Account ID", width: 250 },
                    { field: "AccountName", title: "Acc. Name", width: 400 },
                    { field: "OfficeID", title: "Office ID", width: 200 },
                    { field: "DepartmentID", title: "Department ID", width: 200 },
                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Month", hidden: true, title: "Month", width: 200 },
                    { field: "MonthDesc", title: "Month", width: 200 },
                    //FOR CLient 03
                    { field: "AgentID", title: "Agent ID", width: 200 },
                    { field: "AgentName", title: "Agent Name", width: 300 },
                    { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                    { field: "CounterpartName", title: "Countrpart Name", width: 300 },
                    { field: "InstrumentID", title: "Instrument ID", width: 200 },
                    { field: "InstrumentName", title: "Instrument Name", width: 300 },
                    { field: "ConsigneeID", title: "Consignee ID", width: 200 },
                    { field: "ConsigneeName", title: "Consignee Name", width: 300 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    //End FOR CLient 03
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
            $("#gridAccountBudgetApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form AccountBudget"
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
                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Version", title: "Version", width: 200 },
                    { field: "PeriodID", title: "Period ID", width: 200 },
                    { field: "AccountID", title: "Account ID", width: 250 },
                    { field: "AccountName", title: "Acc. Name", width: 400 },
                    { field: "OfficeID", title: "Office ID", width: 200 },
                    { field: "DepartmentID", title: "Department ID", width: 200 },
                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "Month", hidden: true, title: "Month", width: 200 },
                    { field: "MonthDesc", title: "Month", width: 200 },

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
        $("#TabAccountBudget").kendoTabStrip({
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
                        var AccountBudgetPendingURL = window.location.origin + "/Radsoft/AccountBudget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(AccountBudgetPendingURL);

                        if (_GlobClientCode == "03") {
                            $("#gridAccountBudgetPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form AccountBudget"
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
                                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "Version", title: "Version", width: 200 },
                                    { field: "PeriodID", title: "Period ID", width: 200 },
                                    { field: "AccountID", title: "Account ID", width: 250 },
                                    { field: "AccountName", title: "Acc. Name", width: 400 },
                                    { field: "OfficeID", title: "Office ID", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "Month", hidden: true, title: "Month", width: 200 },
                                    { field: "MonthDesc", title: "Month", width: 200 },
                                    //FOR CLient 03
                                    { field: "AgentID", title: "Agent ID", width: 200 },
                                    { field: "AgentName", title: "Agent Name", width: 300 },
                                    { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                    { field: "CounterpartName", title: "Countrpart Name", width: 300 },
                                    { field: "InstrumentID", title: "Instrument ID", width: 200 },
                                    { field: "InstrumentName", title: "Instrument Name", width: 300 },
                                    { field: "ConsigneeID", title: "Consignee ID", width: 200 },
                                    { field: "ConsigneeName", title: "Consignee Name", width: 300 },
                                    //End FOR CLient 03
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
                            $("#gridAccountBudgetPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form AccountBudget"
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
                                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "Version", title: "Version", width: 200 },
                                    { field: "PeriodID", title: "Period ID", width: 200 },
                                    { field: "AccountID", title: "Account ID", width: 250 },
                                    { field: "AccountName", title: "Acc. Name", width: 400 },
                                    { field: "OfficeID", title: "Office ID", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "Month", hidden: true, title: "Month", width: 200 },
                                    { field: "MonthDesc", title: "Month", width: 200 },

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

                        var AccountBudgetHistoryURL = window.location.origin + "/Radsoft/AccountBudget/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(AccountBudgetHistoryURL);

                        if (_GlobClientCode == "03") {
                            $("#gridAccountBudgetHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form AccountBudget"
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
                                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "Version", title: "Version", width: 200 },
                                    { field: "PeriodID", title: "Period ID", width: 200 },
                                    { field: "AccountID", title: "Account ID", width: 250 },
                                    { field: "AccountName", title: "Acc. Name", width: 400 },
                                    { field: "OfficeID", title: "Office ID", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "Month", hidden: true, title: "Month", width: 200 },
                                    { field: "MonthDesc", title: "Month", width: 200 },
                                    //FOR CLient 03
                                    { field: "AgentID", title: "Agent ID", width: 200 },
                                    { field: "AgentName", title: "Agent Name", width: 300 },
                                    { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                    { field: "CounterpartName", title: "Countrpart Name", width: 300 },
                                    { field: "InstrumentID", title: "Instrument ID", width: 200 },
                                    { field: "InstrumentName", title: "Instrument Name", width: 300 },
                                    { field: "ConsigneeID", title: "Consignee ID", width: 200 },
                                    { field: "ConsigneeName", title: "Consignee Name", width: 300 },
                                    //End FOR CLient 03
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
                            $("#gridAccountBudgetHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form AccountBudget"
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
                                    { field: "AccountBudgetPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "Version", title: "Version", width: 200 },
                                    { field: "PeriodID", title: "Period ID", width: 200 },
                                    { field: "AccountID", title: "Account ID", width: 250 },
                                    { field: "AccountName", title: "Acc. Name", width: 400 },
                                    { field: "OfficeID", title: "Office ID", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "DepartmentName", title: "Dept. Name", width: 300 },
                                    { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "Month", hidden: true, title: "Month", width: 200 },
                                    { field: "MonthDesc", title: "Month", width: 200 },

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
        var grid = $("#gridAccountBudgetHistory").data("kendoGrid");
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

                    var AccountBudget = {
                        Version: $('#Version').val(),
                        PeriodPK: $('#PeriodPK').val(),
                        AccountPK: $('#AccountPK').val(),
                        OfficePK: $('#OfficePK').val(),
                        DepartmentPK: $('#DepartmentPK').val(),
                        Balance: $('#Balance').val(),
                        Month: $('#Month').val(),
                        //FOR CLient 03
                        AgentPK: $('#AgentPK').val(),
                        CounterpartPK: $('#CounterpartPK').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        ConsigneePK: $('#ConsigneePK').val(),
                        //end FOR CLient 03
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AccountBudget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_I",
                        type: 'POST',
                        data: JSON.stringify(AccountBudget),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountBudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountBudget",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var AccountBudget = {
                                    AccountBudgetPK: $('#AccountBudgetPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Version: $('#Version').val(),
                                    PeriodPK: $('#PeriodPK').val(),
                                    AccountPK: $('#AccountPK').val(),
                                    OfficePK: $('#OfficePK').val(),
                                    DepartmentPK: $('#DepartmentPK').val(),
                                    Balance: $('#Balance').val(),
                                    Month: $('#Month').val(),
                                    //FOR CLient 03
                                    AgentPK: $('#AgentPK').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    ConsigneePK: $('#ConsigneePK').val(),
                                    //FOR CLient 03
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AccountBudget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_U",
                                    type: 'POST',
                                    data: JSON.stringify(AccountBudget),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountBudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountBudget",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "AccountBudget" + "/" + $("#AccountBudgetPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountBudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountBudget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AccountBudget = {
                                AccountBudgetPK: $('#AccountBudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AccountBudget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_A",
                                type: 'POST',
                                data: JSON.stringify(AccountBudget),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountBudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountBudget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AccountBudget = {
                                AccountBudgetPK: $('#AccountBudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AccountBudget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_V",
                                type: 'POST',
                                data: JSON.stringify(AccountBudget),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountBudgetPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountBudget",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AccountBudget = {
                                AccountBudgetPK: $('#AccountBudgetPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AccountBudget/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_R",
                                type: 'POST',
                                data: JSON.stringify(AccountBudget),
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

    $("#BtnImportAccountBudget").click(function () {
        document.getElementById("FileImportAccountBudget").click();
    });

    $("#FileImportAccountBudget").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportAccountBudget").get(0).files;

        if (files.length > 0) {
            data.append("AccountBudget", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountBudget_Import",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportAccountBudget").val("");
                    SaveImport();
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportAccountBudget").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportAccountBudget").val("");
        }
    });



    function SaveImport() {
        var val = validateDataImport();
        if (val == 1) {
                    var AccountBudget = {
                        IPeriodPK: $("#IPeriodPK").val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AccountBudget/UpdatePeriod/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(AccountBudget),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            WinGenerateImport.close();
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

        }
    };

    function validateDataImport() {


        if (GlobValidatorImport.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

});
