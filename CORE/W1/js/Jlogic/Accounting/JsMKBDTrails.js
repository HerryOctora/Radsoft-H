$(document).ready(function () {
    document.title = 'FORM MKBD Trails';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;



    //1
    initButton();

    ResetSelected();
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

        $("#BtnToText").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnToExcel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnGenerateNAWCDaily").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnToExcelFromTo").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnToTextFromTo").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });
    }

    function initWindow() {
        if (_GlobClientCode == "25") {
            $("#LblVersionMode").show();
        }
        else {
            $("#LblVersionMode").hide();
        }
        $("#ValueDate").kendoDatePicker({

        });
        $("#ToTextTime").kendoDatePicker({
            format: "dd/MM/yyyy/HH:mm:ss"
        });

        $("#DateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateTo
        });

        $("#ParamDateTextFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateTextFrom
        });
        $("#ParamDateTextTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });


        $("#ParamDateExcelFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateExcelFrom
        });
        $("#ParamDateExcelTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        function OnChangeValueDateTextFrom() {
            if ($("#ParamDateTextFrom").data("kendoDatePicker").value() != null) {
                $("#ParamDateTextTo").data("kendoDatePicker").value($("#ParamDateTextFrom").data("kendoDatePicker").value());
            }


        }


        function OnChangeValueDateExcelFrom() {
            if ($("#ParamDateExcelFrom").data("kendoDatePicker").value() != null) {
                $("#ParamDateExcelTo").data("kendoDatePicker").value($("#ParamDateExcelFrom").data("kendoDatePicker").value());
            }


        }

        function OnChangeDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            refresh();
        }

        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateFrom
        });
        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateTo
        });

        function OnChangeParamDateFrom() {

            var _date = Date.parse($("#ParamDateFrom").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#ParamDateTo").data("kendoDatePicker").value($("#ParamDateFrom").data("kendoDatePicker").value());


        }
        function OnChangeParamDateTo() {
            var _date = Date.parse($("#ParamDateTo").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }

        }

        $("#VersionMode").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
                { text: "Radsoft" },
                { text: "Internal" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeVersionMode,
            index: 0
        });
        function OnChangeVersionMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        win = $("#WinMKBDTrails").kendoWindow({
            height: 450,
            title: "MKBD Trails Detail",
            visible: false,
            width: 950,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinGenerateNAWCDaily = $("#WinGenerateNAWCDaily").kendoWindow({
            height: 150,
            title: "* Generate NAWC Daily",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinToExcelFromTo = $("#WinToExcelFromTo").kendoWindow({
            height: 200,
            title: "* Generate MKBD To Excel From To",
            visible: false,
            width: 550,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinToTextFromTo = $("#WinToTextFromTo").kendoWindow({
            height: 200,
            title: "* Generate MKBD To Text From To",
            visible: false,
            width: 550,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinMKBDTrails").kendoValidator().data("kendoValidator");
    var GlobValidatorToExcel = $("#WinToExcelFromTo").kendoValidator().data("kendoValidator");
    var GlobValidatorToText = $("#WinToTextFromTo").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#Date").val() != "") {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
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

    function validateDataGenerate() {
        if (GlobValidatorToText.validate()) {
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
            $("#StatusHeader").text("NEW");
            $("#ValueDate").data("kendoDatePicker").value();
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridMKBDTrailsApproved").data("kendoGrid");
            }
            if (tabindex == 1) {

                grid = $("#gridMKBDTrailsPending").data("kendoGrid");
            }
            if (tabindex == 2) {

                grid = $("#gridMKBDTrailsHistory").data("kendoGrid");
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnToText").hide();
                $("#BtnToExcel").show();

            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnVoid").show();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#BtnToText").show();
                $("#BtnToExcel").show();

            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("HISTORY");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnToText").hide();
                $("#BtnToExcel").hide();
            }

            $("#MKBDTrailsPK").val(dataItemX.MKBDTrailsPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (dataItemX.BitValidate == true) { $("#BitValidate").val("Yes"); } else if (dataItemX.BitValidate == false) { $("#BitValidate").val("No"); }
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            $("#LogMessages").val(' ' + dataItemX.LogMessages.split('<br/>').join('\n'));
            $("#LastUsersToText").val(dataItemX.LastUsersToText);
            $("#ToTextTime").data("kendoDatePicker").value(new Date(dataItemX.ToTextTime));
            $("#GenerateToTextCount").val(dataItemX.GenerateToTextCount);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(dataItemX.EntryTime);
            $("#UpdateTime").val(dataItemX.UpdateTime);
            $("#ApprovedTime").val(dataItemX.ApprovedTime);
            $("#VoidTime").val(dataItemX.VoidTime);
            $("#LastUpdate").val(dataItemX.LastUpdate);
        }

        $("#GenerateToTextCount").kendoNumericTextBox({
            format: "n0",
            value: setGenerateToTextCount()

        });
        function setGenerateToTextCount() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.GenerateToTextCount;
            }
        }

        $("#BitValidate").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: onChangeBitValidate
        });
        function onChangeBitValidate() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#MKBDTrailsPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#BitValidate").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#LogMessages").val("");
        $("#LastUsersToText").val("");
        $("#ToTextTime").data("kendoDatePicker").value(null);
        $("#GenerateToTextCount").val("");
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
                 pageSize: 50,
                 schema: {
                     model: {
                         fields: {
                             MKBDTrailsPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             BitValidate: { type: "boolean" },
                             ValueDate: { type: "date" },
                             Row113B: { type: "number" },
                             Row173B: { type: "number" },
                             LogMessages: { type: "string" },
                             LastUsersToText: { type: "string" },
                             ToTextTime: { type: "string" },
                             GenerateToTextCount: { type: "number" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "string" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "string" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "string" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "string" },
                             LastUpdate: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()


        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridMKBDTrailsPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridMKBDTrailsHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

 

    function initGrid() {
        
        $("#gridMKBDTrailsApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var MKBDTrailsApprovedURL = window.location.origin + "/Radsoft/MKBDTrails/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(MKBDTrailsApprovedURL);

        }

        var grid = $("#gridMKBDTrailsApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form MKBD Trails"
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
               //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "MKBDTrailsPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Row113B", title: "<div style='text-align: right'>MKBD01", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row173B", title: "<div style='text-align: right'>MKBD02", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row104G", title: "<div style='text-align: right'>MKBD09", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "LogMessages", title: "Log Messages", width: 200 },
               { field: "LastUsersToText", title: "Last Users To Text", width: 200 },
               { field: "ToTextTime", title: "To Text Time", width: 150, template: "#= kendo.toString(kendo.parseDate(ToTextTime), 'dd/MM/yyyy')#" },
               { field: "GenerateToTextCount", title: "<div style='text-align: right'>Generate To Text Count", width: 170, attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
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
            

            var grid = $("#gridMKBDTrailsApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _MKBDTrailsPK = dataItemX.MKBDTrailsPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _MKBDTrailsPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabMKBDTrails").kendoTabStrip({
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MKBDTrails/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MKBDTrails/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridMKBDTrailsPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var MKBDTrailsPendingURL = window.location.origin + "/Radsoft/MKBDTrails/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(MKBDTrailsPendingURL);

        }
        var grid = $("#gridMKBDTrailsPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form MKBD Trails"
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
               //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "MKBDTrailsPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Row113B", title: "<div style='text-align: right'>MKBD01", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row173B", title: "<div style='text-align: right'>MKBD02", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row104G", title: "<div style='text-align: right'>MKBD09", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "LogMessages", title: "Log Messages", width: 200 },
               { field: "LastUsersToText", title: "Last Users To Text", width: 200 },
               { field: "ToTextTime", title: "To Text Time", width: 150, template: "#= kendo.toString(kendo.parseDate(ToTextTime), 'dd/MM/yyyy')#" },
               { field: "GenerateToTextCount", title: "<div style='text-align: right'>Generate To Text Count", width: 170, attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "Last Update", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
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
            

            var grid = $("#gridMKBDTrailsPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _MKBDTrailsPK = dataItemX.MKBDTrailsPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _MKBDTrailsPK);

        }

        ResetButtonBySelectedData();

    }

    function RecalGridHistory() {

        $("#gridMKBDTrailsHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var MKBDTrailsHistoryURL = window.location.origin + "/Radsoft/MKBDTrails/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(MKBDTrailsHistoryURL);

        }
        $("#gridMKBDTrailsHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form MKBD Trails"
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
               { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "MKBDTrailsPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "StatusDesc", title: "Status.", width: 150 },
               { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "Row113B", title: "<div style='text-align: right'>MKBD01", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row173B", title: "<div style='text-align: right'>MKBD02", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "Row104G", title: "<div style='text-align: right'>MKBD09", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "LogMessages", title: "Log Messages", width: 200 },
               { field: "LastUsersToText", title: "Last Users To Text", width: 200 },
               { field: "ToTextTime", title: "To Text Time", width: 150, template: "#= kendo.toString(kendo.parseDate(ToTextTime), 'dd/MM/yyyy')#" },
               { field: "GenerateToTextCount", title: "<div style='text-align: right'>Generate To Text Count", width: 170, attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }




    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    var MKBDTrails = {
                        BitValidate: $('#BitValidate').val(),
                        ValueDate: $('#ValueDate').val(),
                        LogMessages: $('#LogMessages').val(),
                        LastUsersToText: $('#LastUsersToText').val(),
                        ToTextTime: $('#ToTextTime').val(),
                        GenerateToTextCount: $('#GenerateToTextCount').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/MKBDTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MKBDTrails_I",
                        type: 'POST',
                        data: JSON.stringify(MKBDTrails),
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

    $("#BtnOldData").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/MKBDTrails/GetOldData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#MKBDTrailsPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data != undefined) {

                    $("#OldData").html("Validate : " + data.BitValidate +
                        " </br>Value Date : " + data.ValueDate +
                        " </br>Log Messages : " + data.LogMessages +
                        "</br>Last Users To Text : " + data.LastUsersToText +
                        "</br>To Text Time : " + data.ToTextTime +
                        "</br>Generate To Text Count : " + data.GenerateToTextCount +
                        "</br>EntryUserID : " + data.EntryUsersID +
                        "</br>EntryTime : " + data.EntryTime +
                        "</br>UpdateUserID : " + data.UpdateUsersID +
                        "</br>UpdateTime : " + data.UpdateTime);


                    winOldData.center();
                    winOldData.open();

                } else {
                    alert("Pending come from New data, there's no Old data then");
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    $("#BtnApproved").click(function () {
        
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#MKBDTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "MKBDTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == data) {
                            var MKBDTrails = {
                                MKBDTrailsPK: $('#MKBDTrailsPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/MKBDTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MKBDTrails_A",
                                type: 'POST',
                                data: JSON.stringify(MKBDTrails),
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
                var MKBDTrails = {
                    MKBDTrailsPK: $('#MKBDTrailsPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MKBDTrails_V",
                    type: 'POST',
                    data: JSON.stringify(MKBDTrails),
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
                var MKBDTrails = {
                    MKBDTrailsPK: $('#MKBDTrailsPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MKBDTrails_R",
                    type: 'POST',
                    data: JSON.stringify(MKBDTrails),
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

    $("#BtnToText").click(function () {

        alertify.confirm("Are you sure want to Generate NAWC To Text?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/GenerateToText/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#MKBDTrailsPK").val() + "/" + $("#VersionMode").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.location = data;
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnToExcel").click(function () {
        var MKBDTrails = {
            MKBDTrailsPK: $('#MKBDTrailsPK').val(),
            VersionMode: $('#VersionMode').val(),
        };
        alertify.confirm("Are you sure want to Generate NAWC To Excel?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/GenerateToExcel/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(MKBDTrails),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.location = data;
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/ValidateApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/MKBDTrails/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                        } else {
                            alertify.alert("Data has exist, Please Void First!");
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
    });
    $("#BtnRejectBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
   
    $("#BtnGenerateNAWCDaily").click(function () {
        showGenerateNAWCDaily();
    });

    function showGenerateNAWCDaily(e) {

        WinGenerateNAWCDaily.center();
        WinGenerateNAWCDaily.open();

    }

    $("#BtnOkGenerateNAWCDaily").click(function () {



        alertify.confirm("Are you sure want to Generate NAWC Daily ?", function (e) {
            $.blockUI({});
            if (e) {
                var str = "";
                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/ValidateCheckNAWCDaily/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/MKBDTrails/ValidateCheckJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (_GlobClientCode == '05') {
                                        str = data;
                                        data = str.replace(data, "")
                                    }
                                    if (data == "") {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/MKBDTrails/ValidateCheckCashier/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (_GlobClientCode == '05') {
                                                    str = data;
                                                    data = str.replace(data, "")
                                                }
                                                if (data == "") {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/MKBDTrails/GenerateNAWCDailyByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {

                                                            $.unblockUI();
                                                            WinGenerateNAWCDaily.close();
                                                            alertify.alert(data.Msg);
                                                            refresh();

                                                        },
                                                        error: function (data) {
                                                            $.unblockUI();
                                                            alertify.alert(data.responseText);

                                                        }
                                                    });
                                                } else {
                                                    $.unblockUI();
                                                    alertify.alert(data);

                                                }
                                            },
                                            error: function (data) {
                                                $.unblockUI();
                                                alertify.alert(data.responseText);

                                            }
                                        });
                                    } else {
                                        $.unblockUI();
                                        alertify.alert(data);

                                    }
                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText);

                                }
                            });

                        } else {
                            $.unblockUI();
                            alertify.alert(data);

                        }
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);

                    }
                });
            }
        });
    });

    $("#BtnCancelGenerateNAWCDaily").click(function () {
        
        alertify.confirm("Are you sure want to cancel Generate NAWC Daily ?", function (e) {
            if (e) {
                $.unblockUI();
                WinGenerateNAWCDaily.close();
                alertify.alert("Cancel Generate NAWC Daily ");
          
            }
        });
    });



    $("#BtnToExcelFromTo").click(function () {
            showGenerateToExcel();
        
    });

    function showGenerateToExcel(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }

        WinToExcelFromTo.center();
        WinToExcelFromTo.open();

    }

    $("#BtnOkToExcelFromTo").click(function () {
        var val = validateDataGenerate();
        if (val == 1) {

            alertify.confirm("Are you sure want to Generate MKBD By Date ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var MKBDTrails = {
                        DateFrom: $('#ParamDateExcelFrom').val(),
                        DateTo: $('#ParamDateExcelTo').val(),
                        VersionMode: $('#VersionMode').val(),
                    };


                    $.ajax({
                        url: window.location.origin + "/Radsoft/MKBDTrails/GenerateToExcelFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(MKBDTrails),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI({});
                            window.location = data;
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);

                        }
                    });
                }
                else {
                    $.unblockUI();
                    alertify.alert("Close Generate MKBD By Date");
                }
            });
        }

    });


    $("#BtnCancelToExcelFromTo").click(function () {

        alertify.confirm("Are you sure want to cancel Generate MKBD By Date ?", function (e) {
            if (e) {
                $.unblockUI();
                WinToExcelFromTo.close();
                alertify.alert("Cancel Generate MKBD By Date ");

            }
        });
    });



    //--- TO TEXT

    $("#BtnToTextFromTo").click(function () {
        showGenerateToText();

    });

    function showGenerateToText(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }

        WinToTextFromTo.center();
        WinToTextFromTo.open();

    }

    $("#BtnOkToTextFromTo").click(function () {
        var val = validateDataGenerate();
        if (val == 1) {

            alertify.confirm("Are you sure want to Generate MKBD By Date ?", function (e) {
                $.blockUI({});
                if (e) {

                    var MKBDTrails = {
                        DateFrom: $('#ParamDateTextFrom').val(),
                        DateTo: $('#ParamDateTextTo').val(),
                        VersionMode: $('#VersionMode').val(),
                    };


                    $.ajax({
                        url: window.location.origin + "/Radsoft/MKBDTrails/GenerateToTextFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(MKBDTrails),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI({});
                            window.location = data;
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);

                        }
                    });
                }
                else {
                    $.unblockUI();
                    alertify.alert("Close Generate MKBD By Date");
                }
            });
        }

    });

    $("#BtnOkToTextFromTo").click(function () {
        var val = validateDataGenerate();
        if (val == 1) {

            alertify.confirm("Are you sure want to Generate MKBD By Date ?", function (e) {
                $.blockUI({});
                if (e) {

                    var MKBDTrails = {
                        DateFrom: $('#ParamDateTextFrom').val(),
                        DateTo: $('#ParamDateTextTo').val(),
                        VersionMode: $('#VersionMode').val(),
                    };


                    $.ajax({
                        url: window.location.origin + "/Radsoft/MKBDTrails/GenerateToTextFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(MKBDTrails),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI({});
                            window.location = data;
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);

                        }
                    });
                }
                else {
                    $.unblockUI();
                    alertify.alert("Close Generate MKBD By Date");
                }
            });
        }

    });

    $("#BtnCancelToTextFromTo").click(function () {

        alertify.confirm("Are you sure want to cancel Generate MKBD By Date ?", function (e) {
            if (e) {
                $.unblockUI();
                WinToTextFromTo.close();
                alertify.alert("Cancel Generate MKBD By Date ");

            }
        });
    });



    $("#BtnVoidBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/MKBDTrails/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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


    function ResetSelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/ResetSelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/MKBDTrails",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }
});