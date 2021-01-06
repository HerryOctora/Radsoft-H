$(document).ready(function () {
    document.title = 'FORM CORPORATE ACTION';
    //Global Variabel
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 300;
    var dirty;
    var upOradd;
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

        $("#BtnGenerateForAcc").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

    }

    

    function initWindow() {

        $("#PaymentDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangePaymentDate
        });

       


        $("#ExDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeExDate
        });

        $("#RecordingDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeRecordingDate
        });

        $("#ExpiredDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeExpiredDate
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangePaymentDate() {
            var _date = Date.parse($("#PaymentDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }

        }

        

        function OnChangeExDate() {
            var _date = Date.parse($("#ExDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }

        }

        function OnChangeRecordingDate() {
            var _date = Date.parse($("#RecordingDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }

        }

        function OnChangeExpiredDate() {
            var _date = Date.parse($("#ExpiredDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }

        }

        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }

        }


        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo,
            value: new Date(),
        });


        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
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
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }


        win = $("#WinCorporateAction").kendoWindow({
            height: 1250,
            title: " Corporate Action Detail",
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
            }
        }).data("kendoWindow");


        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: 450,
            title: "List Instrument ",
            visible: false,
            width: 750,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

        function onWinListInstrumentClose() {
            $("#gridListInstrument").empty();
        }

    }


    var GlobValidator = $("#WinCorporateAction").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if (GlobValidator.validate()) {
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

            $("#StatusHeader").val("NEW");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridCorporateActionApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridCorporateActionPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridCorporateActionHistory").data("kendoGrid");
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
            dirty = null;

            $("#CorporateActionPK").val(dataItemX.CorporateActionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#ExDate").data("kendoDatePicker").value(dataItemX.ExDate);
            $("#RecordingDate").data("kendoDatePicker").value(dataItemX.RecordingDate);
            $("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            $("#ExpiredDate").data("kendoDatePicker").value(dataItemX.ExpiredDate);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").data("kendoDatePicker").value(dataItemX.LastUpdate);

        }


        //combo box Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CorporateActionType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeType,
                    value: setCmbType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                ShowHideLabelByType(this.value());

            }
        }
        function setCmbType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
                }
            }
        }

        $("#TaxDueDate").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Tax Deducted Behind", value: 1 },
                { text: "Tax Deducted Upfront", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeTaxDueDate,
            value: setCmbTaxDueDate()
        });
        function OnChangeTaxDueDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbTaxDueDate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TaxDueDate;
            }
        }

        $("#Amount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
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

        $("#Price").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setPrice(),
        });

        function setPrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Price == 0) {
                    return "";
                } else {
                    return dataItemX.Price;
                }
            }
        }

        $("#Earn").kendoNumericTextBox({
            format: "n6",
            decimals: 8,
            value: setEarn(),
        });

        function setEarn() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Earn == 0) {
                    return "";
                } else {
                    return dataItemX.Earn;
                }
            }
        }


        $("#Hold").kendoNumericTextBox({
            format: "n6",
            decimals: 8,
            value: setHold(),
        });

        function setHold() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Hold == 0) {
                    return "";
                } else {
                    return dataItemX.Hold;
                }
            }
        }

        if (e != null) {
            ShowHideLabelByType(dataItemX.Type);
        }

        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        //refresh();
    }


    function clearData() {

        $("#CorporateActionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Type").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#ExDate").data("kendoDatePicker").value(null);
        $("#RecordingDate").data("kendoDatePicker").value(null);
        $("#PaymentDate").data("kendoDatePicker").value(null);
        $("#TaxDueDate").val("");
        $("#ExpiredDate").data("kendoDatePicker").value(null);
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#Amount").val("");
        $("#Price").val("");
        $("#Earn").val("");
        $("#Hold").val("");
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
        $("#BtnUnApproved").show();
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
                             CorporateActionPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Type: { type: "number" },
                             TypeDesc: { type: "string" },
                             ValueDate: { type: "date" },
                             ExDate: { type: "date" },
                             RecordingDate: { type: "date" },
                             PaymentDate: { type: "date" },
                             TaxDueDate: { type: "number" },
                             TaxDueDateDesc: { type: "string" },
                             MFeeMethod: { type: "number" },
                             ExpiredDate: { type: "date" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             Amount: { type: "number" },
                             Price: { type: "number" },
                             Earn: { type: "number" },
                             Hold: { type: "number" },
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
            var gridPending = $("#gridCorporateActionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridCorporateActionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        $("#gridCorporateActionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateActionApprovedURL = window.location.origin + "/Radsoft/CorporateAction/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(CorporateActionApprovedURL);

        }

        $("#gridCorporateActionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Market Holiday"
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
               { field: "CorporateActionPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Type", title: "Type.", hidden: true, width: 95 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "InstrumentName", title: "Instrument (Name)", width: 300 },
               { field: "ValueDate", title: "Cum Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
               { field: "ExDate", title: "Ex Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy') : '' #" },
               { field: "RecordingDate", title: "Recording Date", width: 150, template: "#= kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy') : '' #" },
               { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy') : '' #" },
               { field: "TaxDueDate",hidden: true, title: "TaxDueDate", width: 200 },
               { field: "TaxDueDateDesc", title: "Tax DueDate", width: 200 },
               { field: "ExpiredDate", title: "Expired Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy') : '' #" },
               { field: "Amount", title: "Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Price", title: "Price", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Earn", title: "Earn", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Hold", title: "Hold", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCorporateAction").kendoTabStrip({
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
                        RecalGridPending()
                    }
                    if (tabindex == 2) {
                        RecalGridHistory()
                    }
                } else {
                    refresh();
                }
            }
        });
    }

    function RecalGridPending() {

        $("#gridCorporateActionPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateActionPendingURL = window.location.origin + "/Radsoft/CorporateAction/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(CorporateActionPendingURL);

        }


        $("#gridCorporateActionPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Market Holiday"
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
               { field: "CorporateActionPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Type", title: "Type.", hidden: true, width: 95 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "InstrumentName", title: "Instrument (Name)", width: 300 },
               { field: "ValueDate", title: "Cum Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
               { field: "ExDate", title: "Ex Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy') : '' #" },
               { field: "RecordingDate", title: "Recording Date", width: 150, template: "#= kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy') : '' #" },
               { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy') : '' #" },
               { field: "TaxDueDate", hidden: true, title: "TaxDueDate", width: 200 },
               { field: "TaxDueDateDesc", title: "Tax DueDate", width: 200 },
               { field: "ExpiredDate", title: "Expired Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy') : '' #" },
               { field: "Amount", title: "Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Price", title: "Price", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Earn", title: "Earn", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Hold", title: "Hold", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]

        });

    }

    function RecalGridHistory() {

        $("#gridCorporateActionHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateActionHistoryURL = window.location.origin + "/Radsoft/CorporateAction/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(CorporateActionHistoryURL);

        }


        $("#gridCorporateActionHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Market Holiday"
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
               //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
               { field: "CorporateActionPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Type", title: "Type.", hidden: true, width: 95 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "InstrumentName", title: "Instrument (Name)", width: 300 },
               { field: "ValueDate", title: "Cum Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
               { field: "ExDate", title: "Ex Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExDate), 'dd/MMM/yyyy') : '' #" },
               { field: "RecordingDate", title: "Recording Date", width: 150, template: "#= kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(RecordingDate), 'dd/MMM/yyyy') : '' #" },
               { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy') : '' #" },
               { field: "TaxDueDate", hidden: true, title: "TaxDueDate", width: 200 },
               { field: "TaxDueDateDesc", title: "Tax DueDate", width: 200 },
               { field: "ExpiredDate", title: "Expired Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')?kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy') : '' #" },
               { field: "Amount", title: "Amount", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Price", title: "Price", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Earn", title: "Earn", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "Hold", title: "Hold", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
               { field: "EntryUsersID", title: "Entry ID", width: 200 },
               { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "UpdateUsersID", title: "Update ID", width: 200 },
               { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
               { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "VoidUsersID", title: "Void ID", width: 200 },
               { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
               { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]

        });

    }

    function gridHistoryDataBound() {
        var grid = $("#gridCorporateActionHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            }
            else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    //add,update,old data,approve,reject 
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#TotalCashAmount').val() != 0) {
                
                alertify.confirm("Are you sure want to Add data ?", function (e) {
                    if (e) {

                        var CorporateAction = {
                            CorporateActionPK: $('#CorporateActionPK').val(),
                            Type: $('#Type').val(),
                            ValueDate: $('#ValueDate').val(),
                            ExDate: $('#ExDate').val(),
                            RecordingDate: $('#RecordingDate').val(),
                            PaymentDate: $('#PaymentDate').val(),
                            TaxDueDate: $('#TaxDueDate').val(),
                            ExpiredDate: $('#ExpiredDate').val(),
                            InstrumentPK: $('#InstrumentPK').val(),
                            Amount: $('#Amount').val(),
                            Price: $('#Price').val(),
                            Earn: $('#Earn').val(),
                            Hold: $('#Hold').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_I",
                            type: 'POST',
                            data: JSON.stringify(CorporateAction),
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
            else {
                alertify.alert("Please Recalculate First!")
            }
        }

    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#TotalCashAmount').val() != 0) {
                
                alertify.prompt("Are you sure want to Update , please give notes:","", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                    var CorporateAction = {
                                        CorporateActionPK: $('#CorporateActionPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        Type: $('#Type').val(),
                                        ValueDate: $('#ValueDate').val(),
                                        ExDate: $('#ExDate').val(),
                                        RecordingDate: $('#RecordingDate').val(),
                                        PaymentDate: $('#PaymentDate').val(),
                                        TaxDueDate: $('#TaxDueDate').val(),
                                        ExpiredDate: $('#ExpiredDate').val(),
                                        InstrumentPK: $('#InstrumentPK').val(),
                                        Amount: $('#Amount').val(),
                                        Price: $('#Price').val(),
                                        Earn: $('#Earn').val(),
                                        Hold: $('#Hold').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_U",
                                        type: 'POST',
                                        data: JSON.stringify(CorporateAction),
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
            else {
                alertify.alert("Please Recalculate First!")
            }
        }

    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CorporateAction" + "/" + $("#CorporateActionPK").val(),
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
        
        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                if ($('#Type').val() == 3 || $('#Type').val() == 5) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CorporateAction/ValidateCheckRights/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#InstrumentPK').val() + "/" + $('#Type').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                alertify.alert("There's no data Instrument Rights / Warrant");
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                            var CorporateAction = {
                                                CorporateActionPK: $('#CorporateActionPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                ApprovedUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_A",
                                                type: 'POST',
                                                data: JSON.stringify(CorporateAction),
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
                        }
                    });
                }
                else
                {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var CorporateAction = {
                                    CorporateActionPK: $('#CorporateActionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_A",
                                    type: 'POST',
                                    data: JSON.stringify(CorporateAction),
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
            }
        });
    });



    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateAction = {
                                CorporateActionPK: $('#CorporateActionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_V",
                                type: 'POST',
                                data: JSON.stringify(CorporateAction),
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
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateActionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateAction",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateAction = {
                                CorporateActionPK: $('#CorporateActionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateAction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateAction_R",
                                type: 'POST',
                                data: JSON.stringify(CorporateAction),
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

    function formatNumber(x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }

    $("input[type='text']").change(function () {
        dirty = true;
    });

    $("input[type='number']").change(function () {
        dirty = true;
    });

    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();

        WinListInstrument.center();
        WinListInstrument.open();
        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";


    });
    function getDataSourceListInstrument(_url) {



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
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 InstrumentPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" },
                                 Type: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListInstrumentPK() {

        var _url = "";
        if ($('#Type').val() == 6) {
            _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentComboByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2";
        } else {
            _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentComboByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/1";
        }
        
        var dsListInstrument = getDataSourceListInstrument(_url);
        $("#gridListInstrument").kendoGrid({
            dataSource: dsListInstrument,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
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
            columns: [
               { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
               { field: "InstrumentPK", title: "", hidden: true, width: 100 },
               { field: "Type", title: "Type", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 300 }

               //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }
    function onWinListInstrumentClose() {
        $("#gridListInstrument").empty();
    }
    function ListInstrumentSelect(e) {
        var grid = $("#gridListInstrument").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlInstrumentName).val(dataItemX.Name);
        $(htmlInstrumentID).val(dataItemX.ID);
        $(htmlInstrumentPK).val(dataItemX.InstrumentPK);

        WinListInstrument.close();


    }



    function _resetAttribute() {
        $("#lblCumDate").hide();
        $("#lblExDate").hide();
        $("#lblRecordingDate").hide();
        $("#lblPaymentDate").hide();
        $("#lblExpiredDate").hide();
        $("#lblAmount").hide();
        $("#lblPrice").hide();
        $("#lblEarn").hide();
        $("#lblHold").hide();
        $("#lblTaxDueDate").hide();
        $("#CumDate").attr("required", false);
        $("#ExDate").attr("required", false);
        $("#RecordingDate").attr("required", false);
        $("#PaymentDate").attr("required", false);
        $("#ExpiredDate").attr("required", false);
        $("#Earn").attr("required", false);
        $("#TaxDueDate").attr("required", false);
        $("#Earn").attr("required", false);
        $("#Hold").attr("required", false);
        $("#Price").attr("required", false);
        $("#Amount").attr("required", false);

    }

  
    function ShowHideLabelByType(_type) {
        if (_type == 1) {
            _resetAttribute();

            $("#lblCumDate").show();
            $("#lblExDate").show();
            $("#lblRecordingDate").show();
            $("#lblPaymentDate").show();
            $("#lblTaxDueDate").show();
            //$("#lblAmount").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#CumDate").attr("required", true);
            $("#ExDate").attr("required", true);
            $("#RecordingDate").attr("required", true);
            $("#PaymentDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);
            $("#TaxDueDate").attr("required", true);
            //$("#Amount").attr("required", true);


        } else if (_type == 2) {
            _resetAttribute();

            $("#lblCumDate").show();
            $("#lblExDate").show();
            $("#lblRecordingDate").show();
            $("#lblPaymentDate").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#lblPrice").show();
            $("#CumDate").attr("required", true);
            $("#ExDate").attr("required", true);
            $("#RecordingDate").attr("required", true);
            $("#PaymentDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);
            $("#Price").attr("required", true);

        }
        else if (_type == 3 || _type == 5) {
            _resetAttribute();

            $("#lblCumDate").show();
            $("#lblExDate").show();
            $("#lblRecordingDate").show();
            $("#lblPaymentDate").show();
            $("#lblExpiredDate").show();
            $("#lblPrice").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#CumDate").attr("required", true);
            $("#ExDate").attr("required", true);
            $("#RecordingDate").attr("required", true);
            $("#ExpiredDate").attr("required", true);
            $("#PaymentDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);
            $("#Price").attr("required", true);

        }
        else if (_type == 4) {
            _resetAttribute();

            $("#lblCumDate").show();
            $("#lblExDate").show();
            //$("#lblExpiredDate").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#CumDate").attr("required", true);
            $("#ExDate").attr("required", true);
            //$("#ExpiredDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);

        }

    
        else if (_type == 6) {
            _resetAttribute();
            $("#lblCumDate").show();
            $("#lblRecordingDate").show();
            $("#lblPaymentDate").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#CumDate").attr("required", true);
            $("#RecordingDate").attr("required", true);
            $("#PaymentDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);


        }


        else if (_type == 7) {
            _resetAttribute();

            $("#lblCumDate").show();
            $("#lblExDate").show();
            $("#lblExpiredDate").show();
            $("#lblEarn").show();
            $("#lblHold").show();
            $("#CumDate").attr("required", true);
            $("#ExDate").attr("required", true);
            $("#ExpiredDate").attr("required", true);
            $("#Earn").attr("required", true);
            $("#Hold").attr("required", true);

        }
    }



    $("#BtnGenerateForAcc").click(function () {
        $.blockUI({});
        alertify.confirm("Are you sure want to Generate Corporate Action For Acc ?", function (e) {
            if (e) {
                var CorporateAction = {
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateTo').val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/CorporateAction/GenerateForAcc/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(CorporateAction),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == 1) {
                            alertify.alert("Generate Corporate Action For Acc Success ");
                        }
                        else {
                            alertify.alert("There's No Data Corporate Action ");
                        }
                        $.unblockUI();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });
        $.unblockUI();
    });

});
