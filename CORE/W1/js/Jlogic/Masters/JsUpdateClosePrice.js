$(document).ready(function () {
    document.title = 'FORM UpdateClosePrice';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var checkedApproved = {};
    var checkedPending = {};

    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();
    $("#BtnWinImport").click(function () {
        showWinImport();
    });


    function showWinImport() {
        
        winImportInternalClosePrice.center();
        winImportInternalClosePrice.open();

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
        $("#BtnWinImport").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportInternalClosePrice").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
    }

    function initWindow() {

        $("#Date").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDate,
        });
        $("#DateImport").kendoDatePicker({
            format: "dd-MMMM-yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });


        $("#DateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateTo
        });
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

        function OnChangeDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh($("#FundPKFrom").val());
        }
        function OnChangeDateTo() {
            refresh($("#FundPKFrom").val());
        }

        win = $("#WinUpdateClosePrice").kendoWindow({
            height: 450,
            title: "UpdateClosePrice Detail",
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

        winImportInternalClosePrice = $("#WinImportInternalClosePrice").kendoWindow({
            height: 350,
            title: "ImportInternalClosePrice Detail",
            visible: false,
            width: 650,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinImportInternalClosePriceClose
        }).data("kendoWindow");

        
        //combo box FundPKFromFrom
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPKFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPKFrom,
                    value:0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPKFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh($("#FundPKFrom").val());
        }
    }

    function onWinImportInternalClosePriceClose() {
        $("#DateImport").data("kendoDatePicker").value("");
    }

    var GlobValidator = $("#WinUpdateClosePrice").kendoValidator().data("kendoValidator");
    var GlobValidatorImportClosePrice = $("#WinImportInternalClosePrice").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function validateDataImport() {


        if (GlobValidatorImportClosePrice.validate()) {
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
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridUpdateClosePriceApproved").data("kendoGrid");
            }
            if (tabindex == 1) {

                grid = $("#gridUpdateClosePricePending").data("kendoGrid");
            }
            if (tabindex == 2) {

                grid = $("#gridUpdateClosePriceHistory").data("kendoGrid");
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
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

            $("#UpdateClosePricePK").val(dataItemX.UpdateClosePricePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#FundID").val(dataItemX.FundID + " - " + dataItemX.FundName);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#ClosePriceValue").val(dataItemX.ClosePriceValue);
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

        //Combo Box Instrument 
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        })

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

        $("#ClosePriceValue").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setClosePriceValue()
        });
        function setClosePriceValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ClosePriceValue;
            }
        }

        //combo box FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboBitInternalClosePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
        refresh(0);
    }

    function clearData() {
        $("#UpdateClosePricePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value(null);
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#ClosePriceValue").val("");
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
                             UpdateClosePricePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             ClosePriceValue: { type: "number" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
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

    function getDataSourceSearch(_url) {
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
                             UpdateClosePricePK: { type: "number" },
                             StatusDesc: { type: "string" },
                           
                             Date: { type: "date" },
                             FundID: { type: "string" },
                             InstrumentID: { type: "string" },
                             ClosePriceValue: { type: "number" },
                             
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             LastUpdate: { type: "date" }

                         }
                     }
                 }
             });
    }

    function refresh() {
        for (var i in checkedApproved) {
            checkedApproved[i] = null
        }

        for (var i in checkedPending) {
            checkedPending[i] = null
        }

        if (tabindex == 0 || tabindex == undefined) {
            initGrid()


        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridUpdateClosePricePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridUpdateClosePriceHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }


    function initGrid() {
        if ($("#FundPKFrom").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FundPKFrom").val();
        }
        $("#gridUpdateClosePriceApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var UpdateClosePriceApprovedURL = window.location.origin + "/Radsoft/UpdateClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID,
                dataSourceApproved = getDataSource(UpdateClosePriceApprovedURL);

        }

        var grid = $("#gridUpdateClosePriceApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form UpdateClosePrice"
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
            dataBound: onDataBoundApproved,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                {
                    headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                    template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                },
                { field: "UpdateClosePricePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "FundID", title: "FundID", width: 200 },
                { field: "InstrumentID", title: "InstrumentID", width: 200 },
                { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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

        grid.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;

        $('#chbB').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxApproved').each(function (idx, item) {
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

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridUpdateClosePriceApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.UpdateClosePricePK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].UpdateClosePricePK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }
        }

        //$("#SelectedAllApproved").change(function () {

        //    var _checked = this.checked;
        //    var _msg;
        //    if (_checked) {
        //        _msg = "Check All";
        //    } else {
        //        _msg = "UnCheck All"
        //    }

        //    alertify.alert(_msg);
        //    SelectDeselectAllData(_checked, "Approved");

        //});

        //grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        //function selectDataApproved(e) {


        //    var grid = $("#gridUpdateClosePriceApproved").data("kendoGrid");
        //    var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        //    var _UpdateClosePricePK = dataItemX.UpdateClosePricePK;
        //    var _checked = this.checked;
        //    SelectDeselectData(_checked, _UpdateClosePricePK);

        //}


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabUpdateClosePrice").kendoTabStrip({
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
                    refresh(0);
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/UpdateClosePrice/" + _a + "/" + _b,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier', 'position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/UpdateClosePrice/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        if ($("#FundPKFrom").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FundPKFrom").val();
        }
        $("#gridUpdateClosePricePending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var UpdateClosePricePendingURL = window.location.origin + "/Radsoft/UpdateClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID,
                dataSourcePending = getDataSource(UpdateClosePricePendingURL);

        }
        var grid = $("#gridUpdateClosePricePending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form UpdateClosePrice"
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
            dataBound: onDataBoundPending,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                {
                    headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                    template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                },
                { field: "UpdateClosePricePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "FundID", title: "FundID", width: 200 },
                { field: "InstrumentID", title: "InstrumentID", width: 200 },
                { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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

        grid.table.on("click", ".checkboxPending", selectRowPending);
        var oldPageSizeApproved = 0;


        $('#chbPending').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxPending').each(function (idx, item) {
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

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowPending() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridUpdateClosePricePending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.UpdateClosePricePK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].UpdateClosePricePK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxPending")
                        .attr("checked", "checked");
                }
            }
        }


        //$("#SelectedAllPending").change(function () {

        //    var _checked = this.checked;
        //    var _msg;
        //    if (_checked) {
        //        _msg = "Check All";
        //    } else {
        //        _msg = "UnCheck All"
        //    }
        //    alertify.alert(_msg);
        //    SelectDeselectAllData(_checked, "Pending");

        //});

        //grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        //function selectDataPending(e) {


        //    var grid = $("#gridUpdateClosePricePending").data("kendoGrid");
        //    var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        //    var _UpdateClosePricePK = dataItemX.UpdateClosePricePK;
        //    var _checked = this.checked;
        //    SelectDeselectData(_checked, _UpdateClosePricePK);

        //}

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }

    function RecalGridHistory() {
        if ($("#FundPKFrom").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FundPKFrom").val();
        }
        $("#gridUpdateClosePriceHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var UpdateClosePriceHistoryURL = window.location.origin + "/Radsoft/UpdateClosePrice/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID,
                          dataSourceHistory = getDataSource(UpdateClosePriceHistoryURL);

        }
        $("#gridUpdateClosePriceHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form UpdateClosePrice"
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
            { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
            { field: "UpdateClosePricePK", title: "SysNo.", width: 95 },
            { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
            { field: "StatusDesc", title: "Status", width: 200 },
            { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
            { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
            { field: "FundID", title: "FundID", width: 200 },
            { field: "InstrumentID", title: "InstrumentID", width: 200 },
            { field: "ClosePriceValue", title: "Close Price Value", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
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
        
        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }




    function gridHistoryDataBound() {
        var grid = $("#gridUpdateClosePriceHistory").data("kendoGrid");
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
        refresh(0);
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
                    var UpdateClosePrice = {
                        Date: $('#Date').val(),
                        FundPK: $('#FundPK').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        ClosePriceValue: $('#ClosePriceValue').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/UpdateClosePrice/ValidateGetUpdateClosePrice/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(UpdateClosePrice),
                        success: function (data) {
                            $.blockUI();
                            if (data == false) {
                                var UpdateClosePrice = {
                                    Date: $('#Date').val(),
                                    FundPK: $('#FundPK').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    ClosePriceValue: $('#ClosePriceValue').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/UpdateClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateClosePrice_I",
                                    type: 'POST',
                                    data: JSON.stringify(UpdateClosePrice),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $.unblockUI();
                                        alertify.alert(data);
                                        win.close();
                                        refresh(0);
                                    },
                                    error: function (data) {
                                        $.unblockUI();
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UpdateClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "UpdateClosePrice",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var UpdateClosePrice = {
                                    UpdateClosePricePK: $('#UpdateClosePricePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Date: $('#Date').val(),
                                    FundPK: $('#FundPK').val(),
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    ClosePriceValue: $('#ClosePriceValue').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/UpdateClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateClosePrice_U",
                                    type: 'POST',
                                    data: JSON.stringify(UpdateClosePrice),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh(0);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });

                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                win.close();
                                refresh(0);
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UpdateClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "UpdateClosePrice",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "UpdateClosePrice" + "/" + $("#UpdateClosePricePK").val(),
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
                    refresh(0);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UpdateClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "UpdateClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var UpdateClosePrice = {
                                UpdateClosePricePK: $('#UpdateClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/UpdateClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateClosePrice_A",
                                type: 'POST',
                                data: JSON.stringify(UpdateClosePrice),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh(0);
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh(0);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UpdateClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "UpdateClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var UpdateClosePrice = {
                                UpdateClosePricePK: $('#UpdateClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/UpdateClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateClosePrice_V",
                                type: 'POST',
                                data: JSON.stringify(UpdateClosePrice),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh(0);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh(0);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UpdateClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "UpdateClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var UpdateClosePrice = {
                                UpdateClosePricePK: $('#UpdateClosePricePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/UpdateClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdateClosePrice_R",
                                type: 'POST',
                                data: JSON.stringify(UpdateClosePrice),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh(0);
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh(0);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });


    $("#BtnApproveBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringUpdateClosePriceSelected = '';

        for (var i in ArrayFundFrom) {
            stringUpdateClosePriceSelected = stringUpdateClosePriceSelected + ArrayFundFrom[i] + ',';

        }
        stringUpdateClosePriceSelected = stringUpdateClosePriceSelected.substring(0, stringUpdateClosePriceSelected.length - 1)


        if (stringUpdateClosePriceSelected == "")
            alertify.alert("There's No Selected Data");
        else {
            console.log(stringUpdateClosePriceSelected);
            alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
                if (e) {
                    var UpdateClosePrice = {
                        UpdateClosePriceSelected: stringUpdateClosePriceSelected
                    };
                    console.log(UpdateClosePrice);

                    $.ajax({
                        url: window.location.origin + "/Radsoft/UpdateClosePrice/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(UpdateClosePrice),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            refresh(0);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }

    });

    $("#BtnRejectBySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringUpdateClosePriceSelected = '';

        for (var i in ArrayFundFrom) {
            stringUpdateClosePriceSelected = stringUpdateClosePriceSelected + ArrayFundFrom[i] + ',';

        }
        stringUpdateClosePriceSelected = stringUpdateClosePriceSelected.substring(0, stringUpdateClosePriceSelected.length - 1)

        console.log(stringUpdateClosePriceSelected);

        if (stringUpdateClosePriceSelected == "")
            alertify.alert("There's No Selected Data");
        else {
            alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
                if (e) {
                    var UpdateClosePrice = {
                        UpdateClosePriceSelected: stringUpdateClosePriceSelected
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/UpdateClosePrice/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(UpdateClosePrice),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            refresh(0);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    });

    $("#BtnVoidBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringUpdateClosePriceSelected = '';

        for (var i in ArrayFundFrom) {
            stringUpdateClosePriceSelected = stringUpdateClosePriceSelected + ArrayFundFrom[i] + ',';

        }
        stringUpdateClosePriceSelected = stringUpdateClosePriceSelected.substring(0, stringUpdateClosePriceSelected.length - 1)

        console.log(stringUpdateClosePriceSelected);

        if (stringUpdateClosePriceSelected == "")
            alertify.alert("There's No Selected Data");
        else {
            alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
                if (e) {
                    var UpdateClosePrice = {
                        UpdateClosePriceSelected: stringUpdateClosePriceSelected
                    };


                    $.ajax({
                        url: window.location.origin + "/Radsoft/UpdateClosePrice/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(UpdateClosePrice),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            refresh(0);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
    });



    $("#BtnImportInternalClosePrice").click(function () {
        var val = validateDataImport();
        if (val == 1) {
            document.getElementById("FileImportInternalClosePrice").click();
        }
    });

    $("#FileImportInternalClosePrice").change(function () {
        $.blockUI({});

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/UpdateClosePrice/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateImport").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    var UpdateClosePrice = {
                        DateImport: $('#DateImport').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    var data = new FormData();
                    var files = $("#FileImportInternalClosePrice").get(0).files;
                        if (files.length > 0) {
                            data.append("InternalClosePrice", files[0]);
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InternalClosePrice_Import/01-01-1900/0",
                                type: 'POST',
                                data: data,
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "More Than 1 Data in 1 Fund")
                                    {
                                        alertify.alert(data);
                                        $.unblockUI();
                                        $("#FileImportInternalClosePrice").val("");
                                        refresh(0);


                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/UpdateClosePrice/Import/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                        type: 'POST',
                                                        data: JSON.stringify(UpdateClosePrice),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            alertify.alert(data);
                                                            winImportInternalClosePrice.close();
                                                            refresh(0);
                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                            $.unblockUI();
                                                        }
                                                    });
                                    }
                                    else
                                    {
                                        alertify.alert(data);
                                        winImportInternalClosePrice.close();
                                        $.unblockUI();
                                    }
                    
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                    $("#FileImportInternalClosePrice").val("");
                                }
                            });
                        } else {
                            alertify.alert("Please Choose Correct File");
                            $.unblockUI();
                            $("#FileImportInternalClosePrice").val("");
                        }
                }
                else
                {
                     alertify.alert("Data Has Been Add in This Date");
                    //WinAddFundFee.close();
                     refresh(0);
                    $.unblockUI();
                    $("#FileImportInternalClosePrice").val("");
                }
    }
    });

    });



});