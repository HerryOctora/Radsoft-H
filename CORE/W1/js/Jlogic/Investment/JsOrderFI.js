$(document).ready(function () {
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var WinListCounterpart;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobInstrumentType;
    var GlobTrxType;
    var GlobStatusDealing;
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

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
 
    }


    function initWindow() {

        $("#FilterInstrumentType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                    { text: "BOND", value: "2" },
            ],
            change: onChangeFilterInstrumentType,

        });

        function onChangeFilterInstrumentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh();
        }
    
        $("#DateFrom").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateFrom,
            value: new Date()
        });
        $("#DateTo").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateTo,
            value: new Date()
        });
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
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
        function OnChangeValueDate() {
            $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            //var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            ////Check if Date parse is successful
            //if (!_date) {
            //    
            //    alertify.alert("Wrong Format Date DD/MM/YYYY");
            //}

        }

        win = $("#WinOrderFI").kendoWindow({
            height: 1000,
            title: "Order FI Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: 500,
            title: "Instrument List",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

        WinListCounterpart = $("#WinListCounterpart").kendoWindow({
            height: 500,
            title: "Counterpart List",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCounterpartClose
        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinOrderFI").kendoValidator().data("kendoValidator");

    function validateData() {
        
        //if ($("#ValueDate").val() != "") {
        //    var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

        //    //Check if Date parse is successful
        //    if (!_date) {
        //        
        //        alertify.alert("Wrong Format Date DD/MM/YYYY");
        //        return 0;
        //    }
        //}
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
            $("#StatusHeader").val("NEW");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            grid = $("#gridOrderFIApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.StatusInvestment == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
            }

            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#TrxInformation").hide();
                $("#BtnVoid").show();
            }

            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#TrxInformation").hide();
            }

        
            $("#OrderFIPK").val(dataItemX.OrderFIPK);
            $("#Status").val(dataItemX.Status);
            $("#StatusDesc").val(dataItemX.StatusDesc);
            $("#OrderStatus").val(dataItemX.OrderStatus);
            $("#OrderStatusDesc").val(dataItemX.OrderStatusDesc);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }
        $("#InstrumentTypePK").val(2);

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + 2,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    value: setCmbTrxType()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeTrxType() {
            GlobTrxType = this.value();

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }

        function setCmbTrxType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TrxType == 0) {
                    return "";
                } else {
                    return dataItemX.TrxType;
                }
            }
        }

        $("#Volume").kendoNumericTextBox({
            format: "n0",
            value: setVolume(),

        });

        function setVolume() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Volume == 0) {
                    return "";
                } else {
                    return dataItemX.Volume;
                }
            }
        }

        $("#DoneVolume").kendoNumericTextBox({
            format: "n0",
            value: setDoneVolume(),

        });

        function setDoneVolume() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DoneVolume == 0) {
                    return "";
                } else {
                    return dataItemX.DoneVolume;
                }
            }
        }



        $("#RangePriceFrom").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setRangePriceFrom(),
        });
        function setRangePriceFrom() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RangePriceFrom == 0) {
                    return "";
                } else {
                    return dataItemX.RangePriceFrom;
                }
            }
        }

        $("#RangePriceTo").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setRangePriceTo(),
        });
        function setRangePriceTo() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RangePriceTo == 0) {
                    return "";
                } else {
                    return dataItemX.RangePriceTo;
                }
            }
        }


        //combo box PeriodPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    enable: false,
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
                return _defaultPeriodPK;
            } else {
                if (dataItemX.PeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodPK;
                }
            }
        }


        //Instrument Type

        $("#InstrumentTypePK").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            enable: false,
            dataSource: [
                    { text: "BOND", value: "2" },
            ]
        
        });
     

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
        $("#OrderFIPK").val("");
        $("#Status").val("");
        $("#OrderStatus").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#PeriodID").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#InstrumentTypePK").val("");
        $("#TrxType").val("");
        $("#TrxTypeDesc").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#RangePriceFrom").val("");
        $("#RangePriceTo").val("");
        $("#Volume").val("");
        $("#DoneVolume").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
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
                             OrderFIPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             OrderStatus: { type: "number" },
                             OrderStatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             InstrumentTypePK: { type: "number" },
                             InstrumentTypeID: { type: "string" },
                             TrxType: { type: "number" },
                             TrxTypeDesc: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             RangePriceFrom: { type: "number" },
                             RangePriceTo: { type: "number" },
                             Volume: { type: "number" },
                             DoneVolume: { type: "number" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
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
            //var gridPending = $("#gridInvestmentInstructionPending").data("kendoGrid");
            //gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            //var gridHistory = $("#gridInvestmentInstructionHistory").data("kendoGrid");
            //gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        $("#gridOrderFIApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var OrderFIApprovedURL = window.location.origin + "/Radsoft/OrderFI/GetDataOrderFIByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(OrderFIApprovedURL);

        }

        $("#gridOrderFIApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Order Fixed Income"
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
            dataBound: gridDataBound,
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "OrderFIPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "OrderStatus", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "OrderStatusDesc", title: "Order Status", width: 200 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 200 },
                { field: "ValueDate", title: "Value Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                { field: "TrxTypeDesc", title: "Trx Type", width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 300 },
                { field: "RangePriceFrom", title: "Range Price From", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePriceTo", title: "Range Price To", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DoneVolume", title: "Done Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });


    }


    function gridDataBound() {
        var grid = $("#gridOrderFIApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
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
                    var OrderFI = {
                        OrderStatus: $('#OrderStatus').val(),
                        PeriodPK: $('#PeriodPK').val(),
                        ValueDate: $('#ValueDate').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                        RangePriceFrom: $('#RangePriceFrom').val(),
                        RangePriceTo: $('#RangePriceTo').val(),
                        AcqPrice: $('#AcqPrice').val(),
                        Volume: $('#Volume').val(),
                        DoneVolume: $('#DoneVolume').val(),
                        TrxType: $('#TrxType').val(),
                        CounterpartPK: $('#CounterpartPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OrderFI/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "OrderFI_I",
                        type: 'POST',
                        data: JSON.stringify(OrderFI),
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
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    var OrderFI = {
                        OrderFIPK: $('#OrderFIPK').val(),
                        Status: $('#Status').val(),
                        OrderStatus: $('#OrderStatus').val(),
                        PeriodPK: $('#PeriodPK').val(),
                        ValueDate: $('#ValueDate').val(),
                        InstrumentPK: $('#InstrumentPK').val(),
                        InstrumentTypePK: $('#InstrumentTypePK').val(),
                        RangePriceFrom: $('#RangePriceFrom').val(),
                        RangePriceTo: $('#RangePriceTo').val(),
                        AcqPrice: $('#AcqPrice').val(),
                        Volume: $('#Volume').val(),
                        DoneVolume: $('#DoneVolume').val(),
                        TrxType: $('#TrxType').val(),
                        CounterpartPK: $('#CounterpartPK').val(),
                        Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OrderFi/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "OrderFI_U",
                        type: 'POST',
                        data: JSON.stringify(OrderFI),
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

  

    $("#BtnVoid").click(function () {
        
            alertify.confirm("Are you sure want to Void data?", function (e) {
                if (e) {

                    var OrderFI = {
                        OrderFIPK: $('#OrderFIPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/OrderFI/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "OrderFI_V",
                        type: 'POST',
                        data: JSON.stringify(OrderFI),
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

  


    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();
        WinListInstrument.center();
        WinListInstrument.open();
        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";
    });
    function getDataSourceListInstrument() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Instrument/GetInstrumentByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
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
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListInstrumentPK() {
        var dsListInstrument = getDataSourceListInstrument();
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
               { field: "ID", title: "ID", width: 400 },
               { field: "Name", title: "Name", width: 100 }
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



    $("#btnListCounterpartPK").click(function () {
        initListCounterpartPK();
        WinListCounterpart.center();
        WinListCounterpart.open();
        htmlCounterpartPK = "#CounterpartPK";
        htmlCounterpartID = "#CounterpartID";
    });
    function getDataSourceListCounterpart() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                 CounterpartPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListCounterpartPK() {
        var dsListCounterpart = getDataSourceListCounterpart();
        $("#gridListCounterpart").kendoGrid({
            dataSource: dsListCounterpart,
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
               { command: { text: "Select", click: ListCounterpartSelect }, title: " ", width: 100 },
               { field: "CounterpartPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 400 },
               //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }
    function onWinListCounterpartClose() {
        $("#gridListCounterpart").empty();
    }
    function ListCounterpartSelect(e) {
        var grid = $("#gridListCounterpart").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCounterpartName).val(dataItemX.Name);
        $(htmlCounterpartID).val(dataItemX.ID);
        $(htmlCounterpartPK).val(dataItemX.CounterpartPK);

        WinListCounterpart.close();


    }
});
