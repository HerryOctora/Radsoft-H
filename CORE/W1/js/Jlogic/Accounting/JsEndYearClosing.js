$(document).ready(function () {
    document.title = 'FORM END YEAR CLOSING';
    
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var _defaultPeriodPK;

    $.ajax({
        url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            _defaultPeriodPK = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

    //1
    initButton();
    //2
    initWindow();
    //3
    refresh();

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

        $("#BtnGenerate").kendoButton({
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

        $("#BtnApproveInGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });

        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnGenerateUnit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnProcess").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnListEndYearClosingMatching").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnNavProjection").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnReportPortfolioValuation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
    }
    
    function initWindow() {
        $("#Date").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDate,
            value: new Date(),
        });
        win = $("#WinEndYearClosing").kendoWindow({
            height: 750,
            title: "End Year Closing Detail",//ini nanti diganti
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");
    }

    function OnChangeDate() {
        var _date = Date.parse($("#Date").data("kendoDatePicker").value());

        //Check if Date parse is successful
        if (!_date) {

            alertify.alert("Wrong Format Date DD/MMM/YYYY");
        }

    }

    var GlobValidator = $("#WinEndYearClosing").kendoValidator().data("kendoValidator");
    function validateData() {        
        if (GlobValidator.validate()) {
            return 1;
        } else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        if (e == null) {
            clearData();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnGenerate").show();
            $("#StatusHeader").val("NEW");
        } else {

            if (e.handled == true) {
                return;
            }
            e.handled = true;

            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridEndYearClosingApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridEndYearClosingPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridEndYearClosingHistory").data("kendoGrid");
            }

            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnGenerate").hide();
                $("#BtnApproved").show();
                $("#BtnReject").show();
                $("#BtnCancel").show();
                $("#lblDate").hide();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnGenerate").hide();
                $("#BtnVoid").show();
                $("#BtnCancel").show();
                $("#lblDate").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnGenerate").hide();
                $("#BtnVoid").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnCancel").show();
                $("#lblDate").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnGenerate").hide();
                $("#BtnVoid").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnCancel").show();
                $("#lblDate").hide();
            }

            $("#EndYearClosingPK").val(dataItemX.EndYearClosingPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#LogMessages").val(' ' + dataItemX.LogMessages.split('<br/>').join('\n'));
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#LastUsersID").val(dataItemX.LastUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

        // ComboBox Period
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: true,
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
                return _defaultPeriodPK;
            } else {
                return dataItemX.PeriodPK;
            }
        }

        // ComboBox Mode
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EndYearClosingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Mode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeMode,
                    value: setCmbMode()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeMode() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            var _mode = this.value();
            setWindowByMode(_mode,0);
        }
        function setCmbMode() {
            if (e == null) {
                setWindowByMode("","");
                return "";
            } else {
                //setWindowByMode("");
                setWindowByMode(dataItemX.Mode, dataItemX.Status);
                if (dataItemX.Mode == 0) {
                    return "";
                } else {
                    return dataItemX.Mode;
                }
            }
        }
        


        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboGroupsOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
             
                $("#AccountPKTo").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAccountPKTo
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        // ComboBox Account
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountPKFrom").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeAccountPKFrom
                });
               
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeAccountPKFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function onChangeAccountPKTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        // ComboBox Fund Journal Account
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundJournalAccountPKFrom").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundJournalAccountPKFrom
                });
                $("#FundJournalAccountPKTo").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundJournalAccountPKTo
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeFundJournalAccountPKFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function onChangeFundJournalAccountPKTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        // FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }


        win.center().open();
    }

    function setWindowByMode(_mode, _status) {
        if (_mode != undefined || _mode != "" || _mode != "0" || _mode != 0 || _mode != null) {
            // Hide All Validation Messages
            GlobValidator.hideMessages();

            // Remove Attributes
            $("#AccountPKFrom").removeAttr("required");
            $("#AccountPKTo").removeAttr("required");
            $("#FundJournalAccountPKFrom").removeAttr("required");
            $("#FundJournalAccountPKTo").removeAttr("required");
            
            // Set Attributes
            if (_mode == 1 || _mode == "1") { // ALL
                $("#divCorporateJournal").show();
                $("#divFundJournal").show();
                $("#lblFundPK").hide();
                $("#lblDate").hide();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", true);
                $("#AccountPKTo").attr("required", true);
                $("#FundJournalAccountPKFrom").attr("required", true);
                $("#FundJournalAccountPKTo").attr("required", true);
                $("#FundPK").attr("required", false);
                $("#Date").attr("required", false);

                $("#Date").data("kendoDatePicker").value("");
            } else if (_mode == 2 || _mode == "2") { // Fund Portfolio
                $("#divCorporateJournal").hide();
                $("#divFundJournal").hide();
                $("#lblFundPK").hide();
                $("#lblDate").hide();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", false);
                $("#AccountPKTo").attr("required", false);
                $("#FundJournalAccountPKFrom").attr("required", false);
                $("#FundJournalAccountPKTo").attr("required", false);
                $("#FundPK").attr("required", false);
                $("#Date").attr("required", false);

                $("#Date").data("kendoDatePicker").value("");
            } else if (_mode == 3 || _mode == "3") { // Fund Journal
                $("#divCorporateJournal").hide();
                $("#divFundJournal").show();
                $("#lblFundPK").hide();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", false);
                $("#AccountPKTo").attr("required", false);
                $("#FundJournalAccountPKFrom").attr("required", true);
                $("#FundJournalAccountPKTo").attr("required", true);
                $("#FundPK").attr("required", false);

                $("#Date").data("kendoDatePicker").value("");
            } else if (_mode == 4 || _mode == "4") { // Corporate Journal
                if (_status == 0) {
                    $("#lblAccountPKFrom").show();
                    $("#lblAccountPKTo").show();
                    $("#AccountPKFrom").attr("required", true);
                    $("#AccountPKTo").attr("required", true);
                }
                else {

                    $("#lblAccountPKFrom").hide();
                    $("#lblAccountPKTo").hide();
                    $("#AccountPKFrom").attr("required", false);
                    $("#AccountPKTo").attr("required", false);
                }
                $("#divCorporateJournal").show();
                $("#divFundJournal").hide();
                $("#lblFundPK").hide();
                $("#lblDate").hide();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", true);
                $("#AccountPKTo").attr("required", true);
                $("#FundJournalAccountPKFrom").attr("required", false);
                $("#FundJournalAccountPKTo").attr("required", false);
                $("#FundPK").attr("required", false);
                $("#Date").attr("required", false);

                $("#Date").data("kendoDatePicker").value("");
            } else if (_mode == 5 || _mode == "5") {
                if(_status == 0)
                {
                    $("#lblDate").show();
                    $("#Date").attr("required", true);
                    $("#Date").data("kendoDatePicker").value(Date());
                }
                else
                {

                    $("#lblDate").hide();
                    $("#Date").attr("required", false);
                    $("#Date").data("kendoDatePicker").value(Date());
                }
                $("#divCorporateJournal").hide();
                $("#divFundJournal").hide();
                $("#lblFundPK").show();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", false);
                $("#AccountPKTo").attr("required", false);
                $("#FundJournalAccountPKFrom").attr("required", false);
                $("#FundJournalAccountPKTo").attr("required", false);
                $("#FundPK").attr("required", true);
            }
            else {
                $("#divCorporateJournal").hide();
                $("#divFundJournal").hide();
                $("#lblFundPK").hide();
                $("#lblDate").hide();

                // Clear Attributes
                $("#AccountPKFrom").attr("required", false);
                $("#AccountPKTo").attr("required", false);
                $("#FundJournalAccountPKFrom").attr("required", false);
                $("#FundJournalAccountPKTo").attr("required", false);
                $("#FundPK").attr("required", false);
                $("#Date").attr("required", false);

                $("#Date").data("kendoDatePicker").value("");
        }
        } else {
            $("#divCorporateJournal").hide();
            $("#divFundJournal").hide();

            // Clear Attributes
            $("#AccountPKFrom").attr("required", false);
            $("#AccountPKTo").attr("required", false);
            $("#FundJournalAccountPKFrom").attr("required", false);
            $("#FundJournalAccountPKTo").attr("required", false);
        }
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#EndYearClosingPK").val("");
        $("#HistoryPK").val("");
        $("#Status").val("");
        $("#Notes").val("");
        $("#Mode").val("");
        $("#FundPK").val("");
        $("#LogMessages").val("");
        $("#EntryUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnProcess").click(function () {
        showDetails(null);
    });

    $("#BtnCancel").click(function () {        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             EndYearClosingPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             Mode: { type: "number" },
                             ModeDesc: { type: "string" },
                             FundPK: { type: "number" },
                             LogMessages: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             LastUsersID: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
            $("#BtnApproveInGrid").hide();
        }
        else if (tabindex == 1) {
            RecalGridPending();
            $("#BtnApproveInGrid").show();
        }
        else if (tabindex == 2) {
            RecalGridHistory();
            $("#BtnApproveInGrid").hide();
        }
    }

    function initGrid() {
        $("#gridEndYearClosingApproved").empty();
        var EndYearClosingApprovedURL = window.location.origin + "/Radsoft/EndYearClosing/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(EndYearClosingApprovedURL);

        $("#gridEndYearClosingApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Year Closing"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "EndYearClosingPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "PeriodPK", title: "PeriodPK", filterable: false, hidden: true, width: 75 },
                { field: "PeriodID", title: "Period", width: 100 },
                { field: "Mode", title: "Mode", filterable: false, hidden: true, width: 75 },
                { field: "ModeDesc", title: "Mode", width: 150 },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabEndYearClosing").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                    refresh();
                } else {
                    refresh();
                }
            }
        });
    }

    function RecalGridPending() {
        $("#gridEndYearClosingPending").empty();
        var EndYearClosingPendingURL = window.location.origin + "/Radsoft/EndYearClosing/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
            dataSourcePending = getDataSource(EndYearClosingPendingURL);

        $("#gridEndYearClosingPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Year Closing"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "EndYearClosingPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "PeriodPK", title: "PeriodPK", filterable: false, hidden: true, width: 75 },
                { field: "PeriodID", title: "Period", width: 100 },
                { field: "Mode", title: "Mode", filterable: false, hidden: true, width: 75 },
                { field: "ModeDesc", title: "Mode", width: 150 },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }

    function RecalGridHistory() {
        $("#gridEndYearClosingHistory").empty();
        var EndYearClosingHistoryURL = window.location.origin + "/Radsoft/EndYearClosing/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
            dataSourceHistory = getDataSource(EndYearClosingHistoryURL);

        $("#gridEndYearClosingHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Year Closing"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "EndYearClosingPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "PeriodPK", title: "PeriodPK", filterable: false, hidden: true, width: 75 },
                { field: "PeriodID", title: "Period", width: 100 },
                { field: "Mode", title: "Mode", filterable: false, hidden: true, width: 75 },
                { field: "ModeDesc", title: "Mode", width: 150 },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }

    $("#BtnApproved").click(function () {        
        alertify.confirm("Are you sure want to approved data?", function (e) {
            if (e) {
                var EndYearClosing = {
                    EndYearClosingPK: $('#EndYearClosingPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndYearClosing/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndYearClosing_A",
                    type: 'POST',
                    data: JSON.stringify(EndYearClosing),
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

    $("#BtnVoid").click(function () {        
        alertify.confirm("Are you sure want to void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndYearClosingPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndYearClosing",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var EndYearClosing = {
                                EndYearClosingPK: $('#EndYearClosingPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ValueDate: $('#ValueDate').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndYearClosing/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndYearClosing_V",
                                type: 'POST',
                                data: JSON.stringify(EndYearClosing),
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
                var EndYearClosing = {
                    EndYearClosingPK: $('#EndYearClosingPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndYearClosing/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndYearClosing_R",
                    type: 'POST',
                    data: JSON.stringify(EndYearClosing),
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

    $("#BtnGenerate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to generate?", function (e) {
                if (e) {

                    var EndYearClosingValidate = {
                        PeriodPK: $('#PeriodPK').val(),
                        Mode: $('#Mode').val(),
                        Date: $('#Date').val(),
                        FundPK: $('#FundPK').val()
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndYearClosing/CheckGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EndYearClosingValidate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "false") {
                                var _date = "";
                                if ($('#Mode').val() == "4")
                                {
                                    _date = "01/01/1900"
                                }
                                else
                                {
                                    _date = $('#Date').val()
                                }
                                //alertify.alert($('#Date').val());
                                var EndYearClosing = {
                                    PeriodPK: $('#PeriodPK').val(),
                                    Mode: $('#Mode').val(),
                                    AccountPKFrom: $('#AccountPKFrom').val(),
                                    AccountPKTo: $('#AccountPKTo').val(),
                                    FundJournalAccountPKFrom: $('#FundJournalAccountPKFrom').val(),
                                    FundJournalAccountPKTo: $('#FundJournalAccountPKTo').val(),
                                    Date: _date,
                                    FundPK: $('#FundPK').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndYearClosing/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndYearClosing_Generate",
                                    type: 'POST',
                                    data: JSON.stringify(EndYearClosing),
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
                            else
                            {
                                alertify.alert("Data Has Add");
                            }
                        }
                    });
                    

                }
            });
        }        
    });

});