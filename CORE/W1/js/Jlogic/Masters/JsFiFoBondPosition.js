$(document).ready(function () {
    document.title = 'FORM FIFO BOND POSITION';
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

    if (_GlobClientCode == "03") {
        $("#BtnViewHistorical").show();
    }
    else {
        $("#BtnViewHistorical").hide();
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

        $("#BtnViewFifo").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnViewHistorical").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnRptFifoBondPosition").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });


    }

    function initWindow() {
        $("#AcqDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
        });

        $("#CutoffDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeCutoffDate,
        });

        win = $("#WinFiFoBondPosition").kendoWindow({
            height: 450,
            title: "FiFoBondPosition Detail",
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

        winViewFifo = $("#WinViewFifo").kendoWindow({
            height: 600,
            title: "Fifo Bond Position Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");

        winViewHistorical = $("#WinViewHistorical").kendoWindow({
            height: 600,
            title: "Historical Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");

        function OnChangeDate() {
            var _date = Date.parse($("#AcqDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }

        function OnChangeCutoffDate() {
            var _date = Date.parse($("#CutoffDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#AcqDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }



    }

    var GlobValidator = $("#WinFiFoBondPosition").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {

            //if ($("#ID").val().length > 50) {
            //    alertify.alert("Validation not Pass, char more than 50 for ID");
            //    return 0;
            //}

            //if ($("#Name").val().length > 100) {
            //    alertify.alert("Validation not Pass, char more than 100 for Name");
            //    return 0;
            //}

            if ($("#Notes").val().length > 1000) {
                alertify.alert("Validation not Pass, char more than 1000 for Notes");
                return 0;
            }

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

            $("#FiFoBondPositionPK").val(dataItemX.FiFoBondPositionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            //$("#Date").val(dataItemX.Date);
            $("#AcqDate").val(kendo.toString(kendo.parseDate(dataItemX.AcqDate), 'dd/MMM/yyyy'));
            $("#CutoffDate").val(kendo.toString(kendo.parseDate(dataItemX.CutoffDate), 'dd/MMM/yyyy'));
            $("#AcqPrice").val(dataItemX.AcqPrice);
            $("#AcqVolume").val(dataItemX.AcqVolume);
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

        $("#AcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 2,
            value: setAcqPrice()
        });
        function setAcqPrice() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.AcqPrice;
            }
        }

        $("#AcqVolume").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setAcqVolume()
        });
        function setAcqVolume() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.AcqVolume;
            }
        }

        //Fund
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
                    change: onChangeFundPK,
                    dataSource: data,
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

        //Instrument
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeInstrumentPK,
                    dataSource: data,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
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
        $("#FiFoBondPositionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#AcqDate").val("");
        $("#CutoffDate").val("");
        $("#AcqPrice").val("");
        $("#AcqVolume").val("");
        $("#FundPK").val("");
        $("#InstrumentPK").val("");
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
                            FiFoBondPositionPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Date: { type: "string" },
                            AcqPrice: { type: "number" },
                            AcqVolume: { type: "number" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            InstrumentPK: { type: "number" },
                            InstrumentID: { type: "string" },
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
            var gridApproved = $("#gridFiFoBondPositionApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFiFoBondPositionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFiFoBondPositionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FiFoBondPositionApprovedURL = window.location.origin + "/Radsoft/FiFoBondPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FiFoBondPositionApprovedURL);

        $("#gridFiFoBondPositionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FORM FIFO BOND POSITION"
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
                { field: "FiFoBondPositionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundPK", title: "FundPK", width: 150, hidden: true },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 150 },
                { field: "InstrumentID", title: "Instrument", width: 200 },
                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                { field: "AcqVolume", title: "Acq Volume", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice", title: "AcqPrice", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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
        $("#TabFiFoBondPosition").kendoTabStrip({
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
                        var FiFoBondPositionPendingURL = window.location.origin + "/Radsoft/FiFoBondPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FiFoBondPositionPendingURL);
                        $("#gridFiFoBondPositionPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "FORM FIFO BOND POSITION"
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
                                { field: "FiFoBondPositionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundPK", title: "FundPK", width: 150, hidden: true },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 150 },
                                { field: "InstrumentID", title: "Instrument", width: 200 },
                                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                                { field: "AcqVolume", title: "Acq Volume", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "AcqPrice", title: "AcqPrice", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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

                        var FiFoBondPositionHistoryURL = window.location.origin + "/Radsoft/FiFoBondPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FiFoBondPositionHistoryURL);

                        $("#gridFiFoBondPositionHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "FORM FIFO BOND POSITION"
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
                                { field: "FiFoBondPositionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundPK", title: "FundPK", width: 150, hidden: true },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 150 },
                                { field: "InstrumentID", title: "Instrument", width: 200 },
                                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                                { field: "AcqVolume", title: "Acq Volume", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "AcqPrice", title: "AcqPrice", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridFiFoBondPositionHistory").data("kendoGrid");
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
        //$("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {



                    var FiFoBondPosition = {
                        AcqDate: $('#AcqDate').val(),
                        AcqPrice: $('#AcqPrice').val(),
                        AcqVolume: $('#AcqVolume').val(),
                        CutoffDate: $('#CutoffDate').val(),
                        FundPK: $('#FundPK').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FiFoBondPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FiFoBondPosition_I",
                        type: 'POST',
                        data: JSON.stringify(FiFoBondPosition),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FiFoBondPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FiFoBondPosition",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FiFoBondPosition = {
                                    FiFoBondPositionPK: $('#FiFoBondPositionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    AcqDate: $('#AcqDate').val(),
                                    AcqPrice: $('#AcqPrice').val(),
                                    CutoffDate: $('#CutoffDate').val(),
                                    AcqVolume: $('#AcqVolume').val(),
                                    FundPK: $('#FundPK').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FiFoBondPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FiFoBondPosition_U",
                                    type: 'POST',
                                    data: JSON.stringify(FiFoBondPosition),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FiFoBondPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FiFoBondPosition",
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
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FiFoBondPosition" + "/" + $("#FiFoBondPositionPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FiFoBondPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FiFoBondPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FiFoBondPosition = {
                                FiFoBondPositionPK: $('#FiFoBondPositionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FiFoBondPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FiFoBondPosition_A",
                                type: 'POST',
                                data: JSON.stringify(FiFoBondPosition),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FiFoBondPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FiFoBondPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FiFoBondPosition = {
                                FiFoBondPositionPK: $('#FiFoBondPositionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FiFoBondPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FiFoBondPosition_V",
                                type: 'POST',
                                data: JSON.stringify(FiFoBondPosition),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FiFoBondPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FiFoBondPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FiFoBondPosition = {
                                FiFoBondPositionPK: $('#FiFoBondPositionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FiFoBondPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FiFoBondPosition_R",
                                type: 'POST',
                                data: JSON.stringify(FiFoBondPosition),
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

    var _FundPK;
    _FundPK = 0;

    var _InstrumentPK;
    _InstrumentPK = 0;

    function getDataFifoBond() {

        return new kendo.data.DataSource(
            {
                transport:
                {

                    read:
                    {

                        url: window.location.origin + "/Radsoft/FifoBondPosition/GetFifoBondPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _FundPK + "/" + _InstrumentPK,
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                batch: true,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 10000,
                schema: {
                    model: {
                        fields: {
                            FundName: { type: "string", editable: false },
                            Instrument: { type: "string", editable: false },
                            Volume: { type: "number", editable: false },
                            AcqPrice: { type: "price", editable: false },
                            AcqDate: { type: "date", editable: false }

                        }
                    }
                },

            });
    }


    function initGridFifo() {
        var dsFifoBond = getDataFifoBond();
        $("#gridViewFifo").empty();
        $("#gridViewFifo").kendoGrid({
            dataSource: dsFifoBond,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            //toolbar: ["excel"],
            columns: [
                { field: "FundName", title: "Fund", width: 150 },
                { field: "Instrument", title: "Instrument", width: 150 },
                { field: "Volume", title: "Volume", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "AcqPrice", title: "Price", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate", title: "Date", width: 100, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },

            ]
        });
    }

    $("#BtnViewFifo").click(function () {
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#xFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangexFundPK,
                    dataSource: data
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangexFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            RefreshViewFifo()
        }

        //Instrument
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#xInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangexInstrumentPK,
                    dataSource: data
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangexInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            RefreshViewFifo()
        }



        initGridFifo()

        winViewFifo.center();
        winViewFifo.open();

    });

    function RefreshViewFifo() {
        if ($("#xFundPK").val() == "")
            _FundPK = 0;
        else
            _FundPK = $("#xFundPK").val();

        if ($("#xInstrumentPK").val() == "")
            _InstrumentPK = 0;
        else
            _InstrumentPK = $("#xInstrumentPK").val();

        initGridFifo();
    }

    function getDataHistorical() {

        return new kendo.data.DataSource(
            {
                transport:
                {

                    read:
                    {

                        url: window.location.origin + "/Radsoft/FifoBondPosition/GetHistorical/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _FundPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                batch: true,
                error: function (e) {
                    alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 10000,
                schema: {
                    model: {
                        fields: {
                            Date: { type: "date", editable: false },
                            FundPK: { type: "number", editable: false },
                            FundID: { type: "string", editable: false },
                            Instrument: { type: "string", editable: false },
                            AcqDate: { type: "date", editable: false },
                            AcqPrice: { type: "price", editable: false },
                            AcqVolume: { type: "number", editable: false }


                        }
                    }
                },

            });
    }


    function initGridHistorical() {
        var dsFifoHistorical = getDataHistorical();
        $("#gridViewHistorical").empty();
        $("#gridViewHistorical").kendoGrid({
            dataSource: dsFifoHistorical,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            toolbar: ["excel"],
            columns: [
                { field: "Date", title: "Date", width: 100, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", title: "FundPK", width: 150, hidden: true },
                { field: "FundID", title: "Fund", width: 150 },
                { field: "Instrument", title: "Instrument", width: 150 },
                { field: "AcqDate", title: "Date", width: 100, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                { field: "AcqPrice", title: "Price", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AcqVolume", title: "Volume", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },




            ]
        });
    }


    $("#BtnViewHistorical").click(function () {
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#zFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangexFundPK,
                    dataSource: data
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangexFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            RefreshViewHistorical()
        }


        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "dd/MMM/yyyy", "MM/dd/yyyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });


        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            RefreshViewHistorical()
        }



        initGridHistorical()

        winViewHistorical.center();
        winViewHistorical.open();

    });

    function RefreshViewHistorical() {
        if ($("#zFundPK").val() == "")
            _FundPK = 0;
        else
            _FundPK = $("#zFundPK").val();
        if (kendo.toString($("#DateFrom").data("kendoDatePicker").value()))
            _DateFrom = new Date();
        else
            _DateFrom = kendo.toString($("#DateFrom").data("kendoDatePicker").value());

        initGridHistorical();
    }


    $("#BtnRptFifoBondPosition").click(function () {


        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/FifoBondPosition/FifoBondPositionRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#xFundPK").data("kendoComboBox").value() + "/" + $("#xInstrumentPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        var newwindow = window.open(data, '_blank');

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });

    });


});