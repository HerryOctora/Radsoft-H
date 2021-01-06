$(document).ready(function () {
    document.title = 'FORM Dormant Fund Trails';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var checkedIds = {};
    var WinGenerateDormant;
    var WinActivateFund;
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

        $("#BtnGenerateDormant").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkGenerateDormant").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnActivateFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkActivateFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
    }




    function initWindow() {

        win = $("#WinDormantFundTrails").kendoWindow({
            height: 600,
            title: "Dormant Fund Trails Detail",
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

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy"],
            change: OnChangeValueDate,
        });

        $("#ActivateDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy"],
            change: OnChangeActivateDate,
        });

        $("#DormantDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy"],
            change: OnChangeDormantDate,
        });

        $("#DormantDateGenerate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy"],
            change: OnChangeDormantDateGenerate,
        });

        $("#ActivateDateGenerate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy"],
            change: OnChangeActivateDateGenerate,
        });

        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        function OnChangeActivateDate() {
            var _date = Date.parse($("#ActivateDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        function OnChangeDormantDate() {
            var _date = Date.parse($("#DormantDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        function OnChangeDormantDateGenerate() {
            var _date = Date.parse($("#DormantDateGenerate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        function OnChangeActivateDateGenerate() {
            var _date = Date.parse($("#ActivateDateGenerate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        WinGenerateDormant = $("#WinGenerateDormant").kendoWindow({
            height: 550,
            title: "Generate Dormant",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGenerateDormantClose
        }).data("kendoWindow");

        function onWinGenerateDormantClose() {
        

        }

        WinActivateFund = $("#WinActivateFund").kendoWindow({
            height: 550,
            title: "Activate Fund",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinActivateFundClose
        }).data("kendoWindow");

        function onWinActivateFundClose() {


        }
    }



    var GlobValidator = $("#WinDormantFundTrails").kendoValidator().data("kendoValidator");

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
            $("#ValueDate").data("kendoDatePicker").value();
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
                $("#BtnVoid").show();
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

            $("#DormantFundTrailsPK").val(dataItemX.DormantFundTrailsPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            if (dataItemX.BitDormant == true) { $("#BitDormant").val("Yes"); } else if (dataItemX.BitDormant == false) { $("#BitDormant").val("No"); }
            $("#ActivateDate").val(kendo.toString(kendo.parseDate(dataItemX.ActivateDate), 'dd/MMM/yyyy'));
            $("#DormantDate").val(kendo.toString(kendo.parseDate(dataItemX.DormantDate), 'dd/MMM/yyyy'));
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

        $("#BitDormant").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: onChangeBitDormant
        });
        function onChangeBitDormant() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //combo box FundPK
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
        $("#DormantFundTrailsPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#BitDormant").val("");
        $("#ActivateDate").data("kendoDatePicker").value(null);
        $("#FundPK").val("");
        $("#DormantDate").data("kendoDatePicker").value(null);
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
                             DormantFundTrailsPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ValueDate: { type: "date" },
                             BitDormant: { type: "boolean" },
                             ActivateDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             DormantDate: { type: "date" },
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
            var gridApproved = $("#gridDormantFundTrailsApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridDormantFundTrailsPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridDormantFundTrailsHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var DormantFundTrailsApprovedURL = window.location.origin + "/Radsoft/DormantFundTrails/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(DormantFundTrailsApprovedURL);

        $("#gridDormantFundTrailsApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Dormant Fund Trails"
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
                { field: "DormantFundTrailsPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "BitDormant", title: "BitDormant", width: 150, template: "#= BitDormant? 'Yes' : 'No' #" },
                { field: "FundID", title: "FundPK", width: 200 },
                { field: "DormantDate", title: "Dormant Date", width: 150, template: "#= kendo.toString(kendo.parseDate(DormantDate), 'dd/MMM/yyyy')#" },
                { field: "ActivateDate", title: "Activate Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ActivateDate), 'dd/MMM/yyyy')#" },
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
        $("#TabDormantFundTrails").kendoTabStrip({
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
                        var DormantFundTrailsPendingURL = window.location.origin + "/Radsoft/DormantFundTrails/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(DormantFundTrailsPendingURL);
                        $("#gridDormantFundTrailsPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Dormant Fund Trails"
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
                                { field: "DormantFundTrailsPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                                { field: "BitDormant", title: "BitDormant", width: 150, template: "#= BitDormant? 'Yes' : 'No' #" },
                                { field: "FundID", title: "FundPK", width: 200 },
                                { field: "DormantDate", title: "Dormant Date", width: 150, template: "#= kendo.toString(kendo.parseDate(DormantDate), 'dd/MMM/yyyy')#" },
                                { field: "ActivateDate", title: "Activate Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ActivateDate), 'dd/MMM/yyyy')#" },
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

                        var DormantFundTrailsHistoryURL = window.location.origin + "/Radsoft/DormantFundTrails/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(DormantFundTrailsHistoryURL);

                        $("#gridDormantFundTrailsHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Dormant Fund Trails"
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
                                { field: "DormantFundTrailsPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                                { field: "BitDormant", title: "BitDormant", width: 150, template: "#= BitDormant? 'Yes' : 'No' #" },
                                { field: "FundID", title: "FundPK", width: 200 },
                                { field: "DormantDate", title: "Dormant Date", width: 150, template: "#= kendo.toString(kendo.parseDate(DormantDate), 'dd/MMM/yyyy')#" },
                                { field: "ActivateDate", title: "Activate Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ActivateDate), 'dd/MMM/yyyy')#" },
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
        var grid = $("#gridDormantFundTrailsHistory").data("kendoGrid");
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
                    var DormantFundTrails = {
                        //GroupsPK: $('#GroupsPK').val(),
                        //RolesPK: $('#RolesPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DormantFundTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantFundTrails_I",
                        type: 'POST',
                        data: JSON.stringify(DormantFundTrails),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DormantFundTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "DormantFundTrails",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var DormantFundTrails = {
                                    DormantFundTrailsPK: $('#DormantFundTrailsPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    //GroupsPK: $('#GroupsPK').val(),
                                    //RolesPK: $('#RolesPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DormantFundTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantFundTrails_U",
                                    type: 'POST',
                                    data: JSON.stringify(DormantFundTrails),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DormantFundTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "DormantFundTrails",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "DormantFundTrails" + "/" + $("#DormantFundTrailsPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DormantFundTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "DormantFundTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DormantFundTrails = {
                                DormantFundTrailsPK: $('#DormantFundTrailsPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DormantFundTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantFundTrails_A",
                                type: 'POST',
                                data: JSON.stringify(DormantFundTrails),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DormantFundTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "DormantFundTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DormantFundTrails = {
                                DormantFundTrailsPK: $('#DormantFundTrailsPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DormantFundTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantFundTrails_V",
                                type: 'POST',
                                data: JSON.stringify(DormantFundTrails),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DormantFundTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "DormantFundTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DormantFundTrails = {
                                DormantFundTrailsPK: $('#DormantFundTrailsPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DormantFundTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantFundTrails_R",
                                type: 'POST',
                                data: JSON.stringify(DormantFundTrails),
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

    //Generate Dormant
    $("#BtnGenerateDormant").click(function () {
        showGenerateDormant();
    });

    function showGenerateDormant(e) {
        //fund grid disable dibawah ini
        //Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#FundFrom").kendoMultiSelect({
        //            dataValueField: "FundPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,
        //        });
        //        $("#FundFrom").data("kendoMultiSelect").value("0");


        //    },

        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        LoadGenerateDormant();
        //end fund grid

        WinGenerateDormant.center();
        WinGenerateDormant.open();

    }

    function LoadGenerateDormant() {

        //var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");

        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },

                    }
                }
            }
        });


        var gridGenerateDormant = $("#gridGenerateDormant").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    template: "<input type='checkbox' class='checkbox' />"
                }

                , {
                    field: "ID",
                    title: "Fund ID",
                    width: "300px"
                }, {
                    field: "Name",
                    title: "Fund Name",
                    width: "300px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");

        gridGenerateDormant.table.on("click", ".checkbox", selectRow);

        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSize = gridGenerateDormant.dataSource.pageSize();
            gridGenerateDormant.dataSource.pageSize(gridGenerateDormant.dataSource.data().length);

            $('.checkbox').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            gridGenerateDormant.dataSource.pageSize(oldPageSize);

        });


        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridGenerateDormant = $("#gridGenerateDormant").data("kendoGrid"),
                dataItemZ = gridGenerateDormant.dataItem(rowA);

            checkedIds[dataItemZ.FundPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkbox")
                        .attr("checked", "checked");
                }
            }
        }
    }

    $("#BtnOkGenerateDormant").click(function () {

        //fund grid
        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)
        // end fund grid

        var _date = kendo.toString($("#DormantDateGenerate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate Dormant for " + _date, function (e) {
            if (e) {

                                        $.blockUI({});
                                        var DormantFundTrails = {
                                            FundFrom: stringFundFrom,
                                            DormantDateGenerate: $('#DormantDateGenerate').val(),
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/DormantFundTrails/GenerateDormant/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                            type: 'POST',
                                            data: JSON.stringify(DormantFundTrails),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
                                                win.close();
                                                WinGenerateDormant.close();
                                                refresh();
                                                $.unblockUI();
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });

                       

            }
        });
    });



    //Activate Fund
    $("#BtnActivateFund").click(function () {
        showActivateFund();
    });

    function showActivateFund(e) {
        //fund grid disable dibawah ini
        //Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#FundFrom").kendoMultiSelect({
        //            dataValueField: "FundPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,
        //        });
        //        $("#FundFrom").data("kendoMultiSelect").value("0");


        //    },

        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        LoadActivateFund();
        //end fund grid

        WinActivateFund.center();
        WinActivateFund.open();

    }

    function LoadActivateFund() {

        //var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");

        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/DormantFundTrails/GetActivateFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },
                        StatusDormant: { type: "boolean" },

                    }
                }
            }
        });


        var gridActivateFund = $("#gridActivateFund").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    template: "<input type='checkbox' class='checkbox' />"
                }

                , {
                    field: "ID",
                    title: "Fund ID",
                    width: "150px"
                }, {
                    field: "Name",
                    title: "Fund Name",
                    width: "300px"
                },
               { field: "StatusDormant", title: "Status Dormant", width: 150, template: "#= StatusDormant? 'Yes' : 'No' #" },
            ],
            editable: "inline"
        }).data("kendoGrid");

        gridActivateFund.table.on("click", ".checkbox", selectRow);

        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSize = gridActivateFund.dataSource.pageSize();
            gridActivateFund.dataSource.pageSize(gridActivateFund.dataSource.data().length);

            $('.checkbox').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            gridActivateFund.dataSource.pageSize(oldPageSize);

        });


        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridActivateFund = $("#gridActivateFund").data("kendoGrid"),
                dataItemZ = gridActivateFund.dataItem(rowA);

            checkedIds[dataItemZ.FundPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkbox")
                        .attr("checked", "checked");
                }
            }
        }
    }

    $("#BtnOkActivateFund").click(function () {

        //fund grid
        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)
        // end fund grid

        var _date = kendo.toString($("#ActivateDateGenerate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Activate Fund for " + _date, function (e) {
            if (e) {

                $.blockUI({});
                var DormantFundTrails = {
                    FundFrom: stringFundFrom,
                    ActivateDateGenerate: $('#ActivateDateGenerate').val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/DormantFundTrails/ActivateFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'POST',
                    data: JSON.stringify(DormantFundTrails),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        win.close();
                        WinActivateFund.close();
                        refresh();
                        $.unblockUI();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });



            }
        });
    });
});
