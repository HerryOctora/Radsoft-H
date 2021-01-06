$(document).ready(function () {
    document.title = 'FORM BLOOMBERG EQUITY';
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;

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

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnImportBloombergEquity").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        
    }

    function initWindow() {
        $("#DateFrom").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateTo,
            value: new Date(),
        });
        $("#Date").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDate,
        });

        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            refresh();
        }
        function OnChangeDateTo() {
            var _DateTo = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateTo) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
        function OnChangeDate() {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
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

        win = $("#WinBloombergEquity").kendoWindow({
            height: 650,
            title: "Bloomberg Equity",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //WinListInstrument = $("#WinListInstrument").kendoWindow({
        //    height: "520px",
        //    title: "Instrument List",
        //    visible: false,
        //    width: "570px",
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    close: onWinListInstrumentClose
        //}).data("kendoWindow");

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

    var GlobValidator = $("#WinBloombergEquity").kendoValidator().data("kendoValidator");

    function validateData() {

        if ($("#Date").val() != "") {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
                return 0;
            }
        }
        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#BloombergEquityPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        //$("#Date").data("kendoDatePicker").value(null);
        $("#TickerName").val("");
        $("#TickerCode").val("");
        $("#FundName").val("");
        $("#Weight").val("");
        $("#Shares").val("");
        $("#Price").val("");
        $("#MarketCap").val("");
        $("#PercentWeight").val("");
        $("#GICSSector").val("");
        $("#IndustryGroupIndex").val("");
        $("#Y1").val("");

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
                pageSize: 1000,
                schema: {
                    model: {
                        fields: {
                            BloombergEquityPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },

                            TickerName: { type: "string" },
                            TickerCode: { type: "string" },
                            FundName: { type: "string" },
                            Weight: { type: "number" },
                            Shares: { type: "number" },
                            Price: { type: "number" },
                            MarketCap: { type: "number" },
                            PercentWeight: { type: "number" },
                            GICSSector: { type: "string" },
                            IndustryGroupIndex: { type: "string" },
                            Y1: { type: "string" },

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
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
        //if (tabindex == 1) {
        //    RecalGridPending();
        //    var gridPending = $("#gridBloombergEquityPending").data("kendoGrid");
        //    gridPending.dataSource.read();
        //}
        //if (tabindex == 2) {
        //    RecalGridHistory();
        //    var gridHistory = $("#gridBloombergEquityHistory").data("kendoGrid");
        //    gridHistory.dataSource.read();
        //}
    }

    function initGrid() {
        var BloombergEquityApprovedURL = window.location.origin + "/Radsoft/BloombergEquity/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(BloombergEquityApprovedURL);
        $("#gridBloombergEquityApproved").empty();

        $("#gridBloombergEquityApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form BloombergEquity"
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
                //{ command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "BloombergEquityPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                //{ field: "Date", title: "Date", width: 200 },
                { field: "TickerName", title: "TickerName",width: 180 },
                { field: "TickerCode", title: "TickerCode", width: 200 },
                { field: "FundName", title: "FundName", width: 200 },
                { field: "Weight", title: "Weight", width: 300 },
                { field: "Shares", title: "Shares", width: 180 },
                { field: "Price", title: "TickerCode", width: 200 },
                { field: "MarketCap", title: "MarketCap", width: 200 },
                { field: "PercentWeight", title: "PercentWeight", width: 300 },
                { field: "GICSSector", title: "GICSSector", width: 180 },
                { field: "IndustryGroupIndex", title: "IndustryGroupIndex", width: 200 },
                { field: "Y1", title: "Y1", width: 200 },

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

    function gridHistoryDataBound() {
        var grid = $("#gridBloombergEquityHistory").data("kendoGrid");
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

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var BloombergEquity = {
                        TickerName: $('#TickerName').val(),
                        TickerCode: $('#TickerCode').val(),
                        FundName: $('#FundName').val(),
                        Weight: $('#Weight').val(),
                        Shares: $('#Shares').val(),
                        Price: $('#Price').val(),
                        MarketCap: $('#MarketCap').val(),
                        PercentWeight: $('#PercentWeight').val(),
                        GICSSector: $('#GICSSector').val(),
                        IndustryGroupIndex: $('#IndustryGroupIndex').val(),
                        Y1: $('#Y1').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/BloombergEquity/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BloombergEquity_I",
                        type: 'POST',
                        data: JSON.stringify(BloombergEquity),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BloombergEquityPK").val() + "/" + $("#HistoryPK").val() + "/" + "BloombergEquity",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var BloombergEquity = {
                                    BloombergEquityPK: $('#ClosePricePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Notes: str,
                                        TickerName: $('#TickerName').val(),
                                        TickerCode: $('#TickerCode').val(),
                                        FundName: $('#FundName').val(),
                                        Weight: $('#Weight').val(),
                                        Shares: $('#Shares').val(),
                                        Price: $('#Price').val(),
                                        MarketCap: $('#MarketCap').val(),
                                        PercentWeight: $('#PercentWeight').val(),
                                        GICSSector: $('#GICSSector').val(),
                                        IndustryGroupIndex: $('#IndustryGroupIndex').val(),
                                        Y1: $('#Y1').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BloombergEquity/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BloombergEquity_U",
                                    type: 'POST',
                                    data: JSON.stringify(ClosePrice),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BloombergEquityPK").val() + "/" + $("#HistoryPK").val() + "/" + "BloombergEquity",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                            {
                                read:
                                {
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BloombergEquity" + "/" + $("#BloombergEquityPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BloombergEquityPK").val() + "/" + $("#HistoryPK").val() + "/" + "BloombergEquity",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#BloombergEquityPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BloombergEquity/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BloombergEquity_A",
                                type: 'POST',
                                data: JSON.stringify(ClosePrice),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BloombergEquityPK").val() + "/" + $("#HistoryPK").val() + "/" + "BloombergEquity",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#0.00194546PK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/0.00194546/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "0.00194546_V",
                                type: 'POST',
                                data: JSON.stringify(ClosePrice),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BloombergEquityPK").val() + "/" + $("#HistoryPK").val() + "/" + "BloombergEquity",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#BloombergEquityPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BloombergEquity/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BloombergEquity_R",
                                type: 'POST',
                                data: JSON.stringify(ClosePrice),
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


    //TAMBAHAN

    $("#BtnImportBloombergEquity").click(function () {
        document.getElementById("FileImportBloombergEquity").click();
    });

    $("#FileImportBloombergEquity").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportBloombergEquity").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("BloombergEquity", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BloombergEquity_Import/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBloombergEquity").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBloombergEquity").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBloombergEquity").val("");
        }
    });


});