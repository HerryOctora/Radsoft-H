$(document).ready(function () {
    document.title = 'FORM CLOSE PRICE';
    //Global Variabel
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

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnImportClosePrice").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportHaircutMKBD").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportClosePriceBond").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportIBPA").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnImportOldIBPA").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportClosePriceReksadana").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function resetNotification() {
        $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
        alertify.set({
            labels: {
                ok: "OK",
                cancel: "Cancel"
            },
            delay: 4000,
            buttonReverse: false,
            buttonFocus: "ok"
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
        $("#ParamDate").kendoDatePicker({
            format: "MM/dd/yyyy",
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

        win = $("#WinClosePrice").kendoWindow({
            height: 650,
            title: "Close Price Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: "520px",
            title: "Instrument List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListInstrumentClose
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

        winImport = $("#WinImportClosePrice").kendoWindow({
            height: 300,
            title: "Import Close Price",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpCloseImport


        }).data("kendoWindow");



    }

    var GlobValidator = $("#WinClosePrice").kendoValidator().data("kendoValidator");

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

    function showDetails(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridClosePriceApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridClosePricePending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridClosePriceHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
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

            $("#ClosePricePK").val(dataItemX.ClosePricePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#Type").val(dataItemX.Type);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }

        $("#ClosePriceValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setClosePriceValue()
        });
        function setClosePriceValue() {
            if (e == null) {
                return 0;
            } else {

  
                if (dataItemX.ClosePriceValue == 0) {
                    return "";
                } else {
                    return dataItemX.ClosePriceValue;
                }
            }

        }


        if ($("#Type").val() == "RG") {
            $("#ClosePriceValue").data("kendoNumericTextBox").enable(false);
        }
        else {
            $("#ClosePriceValue").data("kendoNumericTextBox").enable(true);
        }

        $("#LowPriceValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setLowPriceValue()
        });
        function setLowPriceValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.LowPriceValue;
            }
        }

        $("#HighPriceValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setHighPriceValue()
        });
        function setHighPriceValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HighPriceValue;
            }
        }
        $("#LiquidityPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setLiquidityPercent()

        });
        function setLiquidityPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.LiquidityPercent;
            }
        }
        $("#HaircutPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setHaircutPercent()
        });
        function setHaircutPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HaircutPercent;
            }
        }

        $("#CloseNAV").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setCloseNAV()
        });
        function setCloseNAV() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CloseNAV;
            }
        }

        $("#TotalNAVReksadana").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setTotalNAVReksadana()
        });
        function setTotalNAVReksadana() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TotalNAVReksadana;
            }
        }

        $("#NAWCHaircut").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNAWCHaircut()
        });
        function setNAWCHaircut() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NAWCHaircut;
            }
        }


        //Bond Rating
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BondRating",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BondRating").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBondRating,
                    value: setCmbBondRating()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBondRating() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBondRating() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BondRating == 0) {
                    return "";
                } else {
                    return dataItemX.BondRating;
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
        $("#ClosePricePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value(null);
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#ClosePriceValue").val("");
        $("#LowPriceValue").val("");
        $("#HighPriceValue").val("");
        $("#LiquidityPercent").val("");
        $("#HaircutPercent").val("");
        $("#CloseNAV").val("");
        $("#TotalNAVReksadana").val("");
        $("#NAWCHaircut").val("");
        $("#BondRating").val("");
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
                             ClosePricePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             ClosePriceValue: { type: "number" },
                             LowPriceValue: { type: "number" },
                             HighPriceValue: { type: "number" },
                             LiquidityPercent: { type: "number" },
                             HaircutPercent: { type: "number" },
                             CloseNAV: { type: "number" },
                             TotalNAVReksadana: { type: "number" },
                             NAWCHaircut: { type: "number" },
                             BondRating: { type: "string" },
                             BondRatingDesc: { type: "string" },
                             Type: { type: "string" },
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
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridClosePricePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridClosePriceHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        
        $("#gridClosePriceApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClosePriceApprovedURL = window.location.origin + "/Radsoft/ClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(ClosePriceApprovedURL);

        }

        var grid = $("#gridClosePriceApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Close Price"
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
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "ClosePricePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 200 },
                { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LowPriceValue", title: "Low Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "HighPriceValue", title: "High Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LiquidityPercent", title: "Liquidity Percent", template: "#: LiquidityPercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "HaircutPercent", title: "Haircut Percent", template: "#: HaircutPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                { field: "CloseNAV", title: "Close NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "TotalNAVReksadana", title: "Total NAV Reksadana", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                { field: "Type", title: "Type", hidden: true, width: 200 },
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
        }).data("kendoGrid");
        $("#SelectedAllApproved").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            

            var grid = $("#gridClosePriceApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _ClosePricePK = dataItemX.ClosePricePK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _ClosePricePK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabClosePrice").kendoTabStrip({
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
                        RecalGridPending();
                    }
                    if (tabindex == 2) {
                        RecalGridHistory();
                    }
                } else {
                    refresh();
                }
            }
        });
        ResetButtonBySelectedData();
        $("#BtnRejectBySelected").hide();
        $("#BtnApproveBySelected").hide();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnVoidBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClosePrice/" + _a + "/" + _b,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClosePrice/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function RecalGridPending() {
        $("#gridClosePricePending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClosePricePendingURL = window.location.origin + "/Radsoft/ClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(ClosePricePendingURL);

        }
            var grid = $("#gridClosePricePending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Close Price"
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
                {
                      field: "Selected",
                      width: 50,
                      template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                      headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                      filterable: true,
                      sortable: false,
                      columnMenu: false
                },
                { field: "ClosePricePK", title: "SysNo.", filterable: false, width: 85 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 200 },
                { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LowPriceValue", title: "Low Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "HighPriceValue", title: "High Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LiquidityPercent", title: "Liquidity Percent", template: "#: LiquidityPercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "HaircutPercent", title: "Haircut Percent", template: "#: HaircutPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                { field: "CloseNAV", title: "Close NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "TotalNAVReksadana", title: "Total NAV Reksadana", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                { field: "Type", title: "Type", hidden: true, width: 200 },
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
        }).data("kendoGrid");
        $("#SelectedAllPending").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }
            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Pending");

        });

        grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        function selectDataPending(e) {
            

            var grid = $("#gridClosePricePending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _ClosePricePK = dataItemX.ClosePricePK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _ClosePricePK);

        }

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }

    function RecalGridHistory() {

        $("#gridClosePriceHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var ClosePriceHistoryURL = window.location.origin + "/Radsoft/ClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(ClosePriceHistoryURL);

        }
        $("#gridClosePriceHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Close Price"
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
                { field: "ClosePricePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 200 },
                { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LowPriceValue", title: "Low Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "HighPriceValue", title: "High Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "LiquidityPercent", title: "Liquidity Percent", template: "#: LiquidityPercent  # %", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "HaircutPercent", title: "Haircut Percent", template: "#: HaircutPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                { field: "CloseNAV", title: "Close NAV", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "TotalNAVReksadana", title: "Total NAV Reksadana", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                { field: "Type", title: "Type", hidden: true, width: 200 },
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

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridClosePriceHistory").data("kendoGrid");
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
                    var ClosePrice = {
                        Date: $('#Date').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        ClosePriceValue: $('#ClosePriceValue').val(),
                        LowPriceValue: $('#LowPriceValue').val(),
                        HighPriceValue: $('#HighPriceValue').val(),
                        LiquidityPercent: $('#LiquidityPercent').val(),
                        HaircutPercent: $('#HaircutPercent').val(),
                        CloseNAV: $('#CloseNAV').val(),
                        TotalNAVReksadana: $('#TotalNAVReksadana').val(),
                        NAWCHaircut: $('#NAWCHaircut').val(),
                        BondRating: $('#BondRating').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ClosePrice/ValidateGetClosePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(ClosePrice),
                        success: function (data) {
                            $.blockUI();
                            if (data == false) {
                                var ClosePrice = {
                                    Date: $('#Date').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    ClosePriceValue: $('#ClosePriceValue').val(),
                                    LowPriceValue: $('#LowPriceValue').val(),
                                    HighPriceValue: $('#HighPriceValue').val(),
                                    LiquidityPercent: $('#LiquidityPercent').val(),
                                    HaircutPercent: $('#HaircutPercent').val(),
                                    CloseNAV: $('#CloseNAV').val(),
                                    TotalNAVReksadana: $('#TotalNAVReksadana').val(),
                                    NAWCHaircut: $('#NAWCHaircut').val(),
                                    BondRating: $('#BondRating').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_I",
                                    type: 'POST',
                                    data: JSON.stringify(ClosePrice),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh();
                                        $.unblockUI();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Already get data For this Day, Void / Reject First!");
                                $.unblockUI();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var ClosePrice = {
                                    ClosePricePK: $('#ClosePricePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Date: $('#Date').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    ClosePriceValue: $('#ClosePriceValue').val(),
                                    LowPriceValue: $('#LowPriceValue').val(),
                                    HighPriceValue: $('#HighPriceValue').val(),
                                    LiquidityPercent: $('#LiquidityPercent').val(),
                                    HaircutPercent: $('#HaircutPercent').val(),
                                    CloseNAV: $('#CloseNAV').val(),
                                    TotalNAVReksadana: $('#TotalNAVReksadana').val(),
                                    NAWCHaircut: $('#NAWCHaircut').val(),
                                    BondRating: $('#BondRating').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_U",
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ClosePrice" + "/" + $("#ClosePricePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#ClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_A",
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#ClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_V",
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var ClosePrice = {
                                ClosePricePK: $('#ClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_R",
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

    // Instrument list
    function getDataSourceListInstrument() {
        return new kendo.data.DataSource(
                 {
                     transport:
                             {
                                 read:
                                     {
                                         url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 InstrumentPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Type: { type: "string" }
                                 //Name: { type: "string" }
                             }
                         }
                     }
                 });
    }

    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();

        WinListInstrument.center();
        WinListInstrument.open();
        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";
        htmlInstrumentName = "#InstrumentName";
    });

    function initListInstrumentPK() {
        var dsListInstrument = getDataSourceListInstrument();
        $("#gridListInstrument").kendoGrid({
            dataSource: dsListInstrument,
            height: "90%",
            scrollable: { virtual: true },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: { input: true, numeric: false },
            columns: [
                        { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 60 },
                        { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                        { field: "ID", title: "ID", width: 300 },
                        //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }

    function onWinListInstrumentClose() { $("#gridListInstrument").empty(); }

    function ListInstrumentSelect(e) {
        var grid = $("#gridListInstrument").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;

        $("#ClosePriceValue").data("kendoNumericTextBox").enable(true);


        $(htmlInstrumentName).val(dataItemX.Name);
        $(htmlInstrumentID).val(dataItemX.ID);
        $(htmlInstrumentPK).val(dataItemX.InstrumentPK);
        WinListInstrument.close();
    }


    //$("#BtnImportClosePrice").click(function () {
    //    document.getElementById("FileImportClosePrice").click();
    //});

    //$("#FileImportClosePrice").change(function () {
    //    $.blockUI({});
        
    //    var data = new FormData();
    //    var files = $("#FileImportClosePrice").get(0).files;

    //    var fileSize = this.files[0].size / 1024 / 1024;

    //    if (fileSize > _GlobMaxFileSizeInMB) {
    //        $.unblockUI();
    //        alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
    //        return;
    //    }

    //    if (files.length > 0) {
    //        data.append("ClosePrice", files[0]);
    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_Import/0",
    //            type: 'POST',
    //            data: data,
    //            contentType: false,
    //            processData: false,
    //            success: function (data) {
    //                if (data == "Already Data") {
    //                    alertify.confirm("Already Data, Are you sure still want to Import Close Price ?", function (e) {
    //                        if (e) {
    //                            $.ajax({
    //                                url: window.location.origin + "/Radsoft/ClosePrice/ApproveClosePriceEquityData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                type: 'GET',
    //                                contentType: "application/json;charset=utf-8",
    //                                success: function (data) {
    //                                    alertify.alert(data);
    //                                    $.unblockUI();
    //                                    $("#FileImportClosePrice").val("");
    //                                    refresh();

    //                                },
    //                                error: function (data) {
    //                                    alertify.alert(data.responseText);
    //                                    $.unblockUI();
    //                                    $("#FileImportClosePrice").val("");
    //                                }
    //                            });
    //                        }
    //                    });

    //                }
    //                else {
    //                    alertify.alert(data);
    //                    $.unblockUI();
    //                    $("#FileImportClosePrice").val("");
    //                    refresh();
    //                }

    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //                $.unblockUI();
    //                $("#FileImportClosePrice").val("");
    //            }
    //        });
    //    } else {
    //        alertify.alert("Please Choose Correct File");
    //        $.unblockUI();
    //        $("#FileImportClosePrice").val("");
    //    }
    //});


    $("#BtnImportClosePriceBond").click(function () {
        document.getElementById("FileImportClosePriceBond").click();
    });

    $("#FileImportClosePriceBond").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportClosePriceBond").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }
        if (_GlobClientCode == '05') {
            if (files.length > 0) {
                data.append("ClosePriceBond", files[0]);
                $.ajax({
                    url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClosePriceBond_Import/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportClosePriceBond").val("");
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        $("#FileImportClosePriceBond").val("");
                    }
                });
            } else {
                alertify.alert("Please Choose Correct File");
                $.unblockUI();
                $("#FileImportClosePriceBond").val("");
            }
        }
        else {
            if (files.length > 0) {
                data.append("ClosePriceBond", files[0]);
                $.ajax({
                    url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePriceBond_Import/0",
                    type: 'POST',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportClosePriceBond").val("");
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        $("#FileImportClosePriceBond").val("");
                    }
                });
            } else {
                alertify.alert("Please Choose Correct File");
                $.unblockUI();
                $("#FileImportClosePriceBond").val("");
            }
        }


    });


    $("#BtnImportHaircutMKBD").click(function () {
        document.getElementById("FileImportHaircutMKBD").click();
    });

    $("#FileImportHaircutMKBD").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportHaircutMKBD").get(0).files;
        $.unblockUI();

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("HaircutMKBD", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HaircutMKBD_Import",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportHaircutMKBD").val("");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportHaircutMKBD").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportHaircutMKBD").val("");
        }
    });


    $("#BtnImportIBPA").click(function () {
        document.getElementById("FileImportIBPA").click();
    });

    $("#FileImportIBPA").change(function () {
 
        
        var data = new FormData();
        var files = $("#FileImportIBPA").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("IBPA", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "IBPA_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                 if (data == "Fair Price") {
                    alertify.confirm("Fair Data < Lower Price or > High Price. Are you sure still want to Import IBPA ?", function (e) {
                        $.blockUI({});
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/ApproveIBPAData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    $.unblockUI();
                                    $("#FileImportIBPA").val("");
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                    $("#FileImportIBPA").val("");
                                }
                            });

                        }

                    });
                    $.unblockUI();
                }

                    else if (data == "Already Data")
                    {
                        alertify.confirm("Already Data, Are you sure still want to Import IBPA ?", function (e) {
                            $.blockUI({});
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClosePrice/ApproveIBPAData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $.unblockUI();
                                        $("#FileImportIBPA").val("");
                                        refresh();
                    
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                        $("#FileImportIBPA").val("");
                                    }
                                });
                
                            }
                   
                        });
                        $.unblockUI();
                    }
                  
                    else
                    {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportIBPA").val("");
                        refresh();
                    }

     


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportIBPA").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportIBPA").val("");
        }
    });

    $("#BtnApproveBySelected").click(function (e) {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/ClosePrice/ValidateApproveClosePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    alertify.alert("Close Price Data Has No Updated")
                    alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            }); 
                        }
                    });
                }
                else {
                    alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ClosePrice/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
                    });
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    });

    $("#BtnRejectBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClosePrice/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnVoidBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/ClosePrice/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnImportOldIBPA").click(function () {
        document.getElementById("FileImportOldIBPA").click();
    });

    $("#FileImportOldIBPA").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportOldIBPA").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        if (files.length > 0) {
            data.append("OldIBPA", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "OldIBPA_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data == "Already Data") {
                        alertify.confirm("Already Data, Are you sure still want to Import IBPA ?", function (e) {
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClosePrice/ApproveOldIBPAData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $.unblockUI();
                                        $("#FileImportOldIBPA").val("");
                                        refresh();

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                        $("#FileImportOldIBPA").val("");
                                    }
                                });
                            }
                        });

                    }
                    else {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportOldIBPA").val("");
                        refresh();
                    }


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOldIBPA").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOldIBPA").val("");
        }
    });

    $("#BtnImportClosePriceReksadana").click(function () {
        document.getElementById("FileImportClosePriceReksadana").click();
    });

    $("#FileImportClosePriceReksadana").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportClosePriceReksadana").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ClosePriceReksadana", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePriceReksadana_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportClosePriceReksadana").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportClosePriceReksadana").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportClosePriceReksadana").val("");
        }
    });

    $("#BtnImportClosePrice").click(function () {
        showImportClosePriceEquity();
    });

    function showImportClosePriceEquity(e) {
        resetParamDate();
        $("#ParamExtensionType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamExtensionType,
            dataSource: [
                { text: "DBF", value: "1" },
                { text: "XLSX", value: "2" },
            ],

        });
        function OnChangeParamExtensionType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                resetParamDate();
                if (this.value() == 2) {
                    $("#lblParamDate").show();
                }




            }

        }

        winImport.center();
        winImport.open();

    }

    function resetParamDate() {
        $("#lblParamDate").hide();

    }

    function onPopUpCloseImport() {
        $("#ParamExtensionType").val("");
        $("#lblParamDate").hide();
    }

    $("#BtnCancelImportClosePrice").click(function () {

        alertify.confirm("Are you sure want to cancel Import?", function (e) {
            if (e) {
                winImport.close();
                alertify.alert("Cancel Import");
            }
        });
    });

    $("#BtnOkImportClosePrice").click(function () {
        document.getElementById("FileImportClosePrice").click();
    });

    $("#FileImportClosePrice").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportClosePrice").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if ($("#ParamExtensionType").val() == 1) {
            _url = window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_Import/0";
        }
        else {
            _url = window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_Import/" + kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yy");
        }

        if (files.length > 0) {
            data.append("ClosePrice", files[0]);
            $.ajax({
                url: _url,
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data == "Already Data") {
                        alertify.confirm("Already Data, Are you sure still want to Import Close Price ?", function (e) {
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ClosePrice/ApproveClosePriceEquityData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $.unblockUI();
                                        $("#FileImportClosePrice").val("");
                                        refresh();

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                        $("#FileImportClosePrice").val("");
                                    }
                                });
                            }
                        });

                    }
                    else {
                        alertify.alert(data);
                        $.unblockUI();
                        $("#FileImportClosePrice").val("");
                        refresh();
                    }

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportClosePrice").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportClosePrice").val("");
        }
    });




});
