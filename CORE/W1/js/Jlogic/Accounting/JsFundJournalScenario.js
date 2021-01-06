$(document).ready(function () {
    document.title = 'FORM FUND JOURNAL';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 330;
    var winFundJournalScenarioDetail;
    var upOradd;
    var GlobStatus;


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

      
        $("#BtnRefreshDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAddDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnClose").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    
     
    function initWindow() {
      


        // disini
        winFundJournalScenarioDetail = $("#WinFundJournalScenarioDetail").kendoWindow({
            height: 350,
            title: "* FUND JOURNAL SCENARIO DETAIL",
            visible: false,
            width: 600,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinFundJournalScenarioDetailClose
        }).data("kendoWindow");

        win = $("#WinFundJournalScenario").kendoWindow({
            height: 1500,
            title: "* FUND JOURNAL HEADER",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            }
        }).data("kendoWindow");

    }
    var GlobValidatorFundJournalScenario = $("#WinFundJournalScenario").kendoValidator().data("kendoValidator");
    function validateDataFundJournalScenario() {
        
        if (GlobValidatorFundJournalScenario.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    var GlobValidatorFundJournalScenarioDetail = $("#WinFundJournalScenarioDetail").kendoValidator().data("kendoValidator");
    function validateDataFundJournalScenarioDetail() {
        
        if (GlobValidatorFundJournalScenarioDetail.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    function showDetails(e) {
        var dataItemX;
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnOldData").hide();
            GlobStatus = 0;
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            if (tabindex == 0 || tabindex == undefined) {
                var grid = $("#gridFundJournalScenarioApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {
                var grid = $("#gridFundJournalScenarioPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {
                var grid = $("#gridFundJournalScenarioHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
             
            }

            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnOldData").hide();
            }

          
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
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
    

            $("#FundJournalScenarioPK").val(dataItemX.FundJournalScenarioPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Status").val(dataItemX.Status);
            $("#Notes").val(dataItemX.Notes);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            if (dataItemX.ReversedTime == null) {
                $("#ReversedTime").text("");
            } else {
                $("#ReversedTime").text(kendo.toString(kendo.parseDate(dataItemX.ReversedTime), 'MM/dd/yyyy HH:mm:ss'));
            }
            if (dataItemX.PostedTime == null) {
                $("#PostedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
            $("#gridFundJournalScenarioDetail").empty();

            initGridFundJournalScenarioDetail();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
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

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundJournalScenario",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Scenario").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: OnChangeScenario,
                    value: setCmbScenario(),

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeScenario() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbScenario() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Scenario == 0) {
                    return "";
                } else {
                    return dataItemX.Scenario;
                }
            }
        }

        win.center();
        win.open();

    }
    function onPopUpClose() {
        clearData()
        showButton();
        $("#gridFundJournalScenarioDetail").empty();
        refresh();
    }
    function clearData() {
        GlobValidatorFundJournalScenario.hideMessages();
        $("#FundJournalScenarioPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundPK").val("");
        $("#Scenario").val("");
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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             FundJournalScenarioPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "String" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },    
                             Scenario: { type: "number" },
                             ScenarioDesc: { type: "string" },
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
            var gridApproved = $("#gridFundJournalScenarioApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundJournalScenarioPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundJournalScenarioHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundJournalScenarioApprovedURL = window.location.origin + "/Radsoft/FundJournalScenario/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FundJournalScenarioApprovedURL);

        $("#gridFundJournalScenarioApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FundJournalScenario"
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
                { field: "FundJournalScenarioPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundPK", title: "FundPK", hidden: true, width: 120 },
                { field: "FundID", title: "Fund ID", width: 300 },
                { field: "Scenario", title: "Scenario", hidden: true, width: 120 },
                { field: "ScenarioDesc", title: "Scenario", width: 300 },
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
        $("#TabFundJournalScenario").kendoTabStrip({
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
                        var FundJournalScenarioPendingURL = window.location.origin + "/Radsoft/FundJournalScenario/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FundJournalScenarioPendingURL);
                        $("#gridFundJournalScenarioPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FundJournalScenario"
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
                                { field: "FundJournalScenarioPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundPK", title: "FundPK", hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 300 },
                                { field: "Scenario", title: "Scenario", hidden: true, width: 120 },
                                { field: "ScenarioDesc", title: "Scenario", width: 300 },
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

                        var FundJournalScenarioHistoryURL = window.location.origin + "/Radsoft/FundJournalScenario/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FundJournalScenarioHistoryURL);

                        $("#gridFundJournalScenarioHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FundJournalScenario"
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
                                { field: "FundJournalScenarioPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundPK", title: "FundPK", hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 300 },
                                { field: "Scenario", title: "Scenario", hidden: true, width: 120 },
                                { field: "ScenarioDesc", title: "Scenario", width: 300 },
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
        var grid = $("#gridFundJournalScenarioHistory").data("kendoGrid");
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

    // bagian FundJournalScenarioDetail
    function refreshFundJournalScenarioDetailGrid() {
        var gridFundJournalScenarioDetail = $("#gridFundJournalScenarioDetail").data("kendoGrid");
        gridFundJournalScenarioDetail.dataSource.read();
    }
    function getDataSourceFundJournalScenarioDetail(_url) {
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
                 pageSize: 8,
                 schema: {
                     model: {
                         fields: {
                             FundJournalScenarioPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             FundJournalAccountPK: { type: "number" },
                             FundJournalAccountID: { type: "string" },
                             FundJournalAccountName: { type: "string" },
                             DebitCredit: { type: "string" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showFundJournalScenarioDetail(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridFundJournalScenarioDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#DDebitCredit").val(dataItemX.DebitCredit);
            $("#DLastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundJournalAccountPK").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: onChangeDFundJournalAccountPK,
                    filter: "contains",
                    suggest: true,
                    value: setDFundJournalAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setDFundJournalAccountPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FundJournalAccountPK;
            }
        }

        function onChangeDFundJournalAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
       

        }

     

        $("#DDebitCredit").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "DEBIT", value: "D" },
                { text: "CREDIT", value: "C" },
            ],
            filter: "contains",
            suggest: true,
            change: onChangeDDebitCredit,
        });


     
        winFundJournalScenarioDetail.center();
        winFundJournalScenarioDetail.open();

    }
    function DeleteFundJournalScenarioDetail(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        

        var dataItemX;
        var grid = $("#gridFundJournalScenarioDetail").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        alertify.confirm("Are you sure want to DELETE detail ?", function (e) {
            if (e) {

                var FundJournalScenarioDetail = {
                    FundJournalScenarioPK: dataItemX.FundJournalScenarioPK,
                    LastUsersID: sessionStorage.getItem("user"),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalScenarioDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenarioDetail_D",
                    type: 'POST',
                    data: JSON.stringify(FundJournalScenarioDetail),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        refreshFundJournalScenarioDetailGrid();
                        alertify.alert(data.Message);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    }
    function initGridFundJournalScenarioDetail() {
        var FundJournalScenarioDetailURL = window.location.origin + "/Radsoft/FundJournalScenarioDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#FundJournalScenarioPK").val(),
          dataSourceFundJournalScenarioDetail = getDataSourceFundJournalScenarioDetail(FundJournalScenarioDetailURL);

        $("#gridFundJournalScenarioDetail").kendoGrid({
            dataSource: dataSourceFundJournalScenarioDetail,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Fund Journal Scenario Detail"
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
               {
                   command: { text: "show", click: showFundJournalScenarioDetail }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeleteFundJournalScenarioDetail }, title: " ", width: 80, locked: true, lockable: false
               },

               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "FundJournalAccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "FundJournalScenarioPK", title: "FundJournalScenario No.", hidden: true, filterable: false, width: 120 },
               { field: "FundJournalAccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    function FundJournalScenarioDetailGrid(e) {
        var FundJournalScenarioDetailURL = window.location.origin + "/Radsoft/FundJournalScenarioDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.FundJournalScenarioPK,
         dataSourceFundJournalScenarioDetail = getDataSourceFundJournalScenarioDetail(FundJournalScenarioDetailURL);

        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: dataSourceFundJournalScenarioDetail,
            height: 250,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FundJournalScenario Detail"
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
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "FundJournalAccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "FundJournalScenarioPK", title: "FundJournalScenario No.", hidden: true, filterable: false, width: 120 },
               { field: "FundJournalAccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }
    function onChangeDAmount() {
        recalAmount(this.value());
    }
    function onChangeDDebitCredit() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }

    }

    function onWinFundJournalScenarioDetailClose() {
        GlobValidatorFundJournalScenarioDetail.hideMessages();
        $("#DFundJournalAccountPK").val("");
        $("#DDebitCredit").val("");
        $("#DLastUsersID").val("");
        $("#DLastUpdate").val("");
    }


    $("#BtnRefreshDetail").click(function () {
        refreshFundJournalScenarioDetailGrid();
    });
    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalScenarioPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalScenario",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundJournalScenario" + "/" + $("#FundJournalScenarioPK").val(),
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
    $("#BtnRefresh").click(function () {
        refresh();

    });
    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnClose").click(function () {
        
        alertify.confirm("Are you sure want to Close and Clear Detail?", function (e) {
            if (e) {
                winFundJournalScenarioDetail.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnNew").click(function () {
        showDetails(null);
    });

    $("#BtnAddDetail").click(function () {
        
        if ($("#FundJournalScenarioPK").val() == 0 || $("#FundJournalScenarioPK").val() == null) {
            alertify.alert("There's no Fund Journal Scenario Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("Fund Journal Scenario Already History");
        } else {
            showFundJournalScenarioDetail();
        }
    });
    $("#BtnSave").click(function () {
        var val = validateDataFundJournalScenarioDetail();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add FUND JOURNAL DETAIL ?", function (e) {
                if (e) {

                    var FundJournalScenarioDetail = {
                        FundJournalScenarioPK: $('#FundJournalScenarioPK').val(),
                        Status: 2,
                        FundJournalAccountPK: $('#DFundJournalAccountPK').val(),
                        DebitCredit: $('#DDebitCredit').val(),
                        LastUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundJournalScenarioDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenarioDetail_I",
                        type: 'POST',
                        data: JSON.stringify(FundJournalScenarioDetail),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            $("#gridFundJournalScenarioDetail").empty();
                            initGridFundJournalScenarioDetail();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });

        }
    });

    $("#BtnAdd").click(function () {
        if ($("#FundJournalScenarioPK").val() > 0) {
            alertify.alert("FUND JOURNAL SCENARIO HEADER ALREADY EXIST, Cancel and click add new to add more Header");
            return
        }
        var val = validateDataFundJournalScenario();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add FUND JOURNAL SCENARIO HEADER ?", function (e) {
                if (e) {
                    var FundJournalScenario = {
                        FundPK: $('#FundPK').val(),
                        Scenario: $("#Scenario").val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundJournalScenario/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenario_I",
                        type: 'POST',
                        data: JSON.stringify(FundJournalScenario),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data.Message);
                            $("#FundJournalScenarioPK").val(data.FundJournalScenarioPK);
                            $("#HistoryPK").val(data.HistoryPK);
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
        var val = validateDataFundJournalScenario();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalScenarioPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalScenario",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundJournalScenario = {
                                    FundJournalScenarioPK: $('#FundJournalScenarioPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    Scenario: $("#Scenario").val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournalScenario/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenario_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournalScenario),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
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
    $("#BtnApproved").click(function () {
        
      
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalScenarioPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournalScenario",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundJournalScenario = {
                                FundJournalScenarioPK: $('#FundJournalScenarioPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundJournalScenario/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenario_A",
                                type: 'POST',
                                data: JSON.stringify(FundJournalScenario),
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
                var FundJournalScenario = {
                    FundJournalScenarioPK: $('#FundJournalScenarioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalScenario/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenario_V",
                    type: 'POST',
                    data: JSON.stringify(FundJournalScenario),
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
    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var FundJournalScenario = {
                    FundJournalScenarioPK: $('#FundJournalScenarioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalScenario/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalScenario_R",
                    type: 'POST',
                    data: JSON.stringify(FundJournalScenario),
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

   

});