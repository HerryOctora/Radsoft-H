$(document).ready(function () {
    document.title = 'FORM REBATE FEE SETUP';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;

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
        $("#BtnAddRebateFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSaveRebateFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });
        $("#BtnCancelRebateFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnRejectRebateFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
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
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDate,
        });
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#DateAmortization").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
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
        function OnChangeDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format ValueDate DD/MM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            
                            alertify.alert("ValueDate is Holiday, Please Insert ValueDate Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }

       


        win = $("#WinRebateFeeSetup").kendoWindow({
            height: 450,
            title: "RebateFeeSetup Detail",
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

        WinAddRebateFee = $("#WinAddRebateFee").kendoWindow({
            height: 750,
            title: "Add Rebate Fee",
            visible: false,
            width: 1300,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAddRebateFeeClose
        }).data("kendoWindow");
      
        WinListFundClient = $("#WinListFundClient").kendoWindow({
            height: 450,
            title: "List Fund Client ",
            visible: false,
            width: 750,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
            },
            close: onWinListFundClientClose
        }).data("kendoWindow");

        function onWinListFundClientClose() {
            $("#gridListFundClient").empty();
        }
    }


    var GlobValidator = $("#WinRebateFeeSetup").kendoValidator().data("kendoValidator");
    var GlobValidatorRebateFee = $("#WinAddRebateFee").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#ValueDate").val() != "") {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format ValueDate DD/MM/YYYY");
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

    function validateDataRebateFee() {


        if (GlobValidatorRebateFee.validate()) {
            //alert("Validation sucess");
            return 1;
        }

        else {
            alertify.alert("Validation not Pass");
            return 0;
        }

    }

    function showDetails(e) {
        if (e == null) {
            HideBtnAdd(0);
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
                grid = $("#gridRebateFeeSetupApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridRebateFeeSetupPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridRebateFeeSetupHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            HideBtnAdd(dataItemX.Status);
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

            $("#RebateFeeSetupPK").val(dataItemX.RebateFeeSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundID").val(dataItemX.FundID);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
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


        $("#RebateFeePercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setRebateFeePercent(),
            change: onChangeRebateFeePercent
        });
        function setRebateFeePercent() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.RebateFeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.RebateFeePercent;
                }
            }
        }
        function onChangeRebateFeePercent() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }

        }

        //combo box FundFeePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDFundPK,
                    value: setCmbDFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbDFundPK() {
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

        $("#FeeType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Tiering", value: 1 },
                { text: "Progresive", value: 2 },
                { text: "Up Front", value: 3 },
                { text: "Amortization", value: 4 },
                { text: "Flat", value: 5 }

            ],
            filter: "contains",
            change: OnChangeFeeType,
            suggest: true
        });


        function OnChangeFeeType() {
            clearDataRebateFeeType();
            clearDataRebateFeeSetup();
            RequiredAttributes(this.value());
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#lblDateAmortize").hide();
                $("#lblMiAmount").hide();
            }
            else if (this.value() == 2) {
                $("#lblDateAmortize").hide();
                $("#lblMiAmount").hide();
            }
            else if (this.value() == 3) {
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMiPercent").hide();
            }
            else if (this.value() == 4) {
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMiPercent").hide();
            }
            else if (this.value() == 5) {
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblDateAmortize").hide();
                $("#lblMiAmount").hide();


            }
        }

        $("#MiFeeAmount").kendoNumericTextBox({
            format: "n4",
            decimals: 4,

        });

        $("#RangeFrom").kendoNumericTextBox({
            format: "n0",
            decimals: 4,
        });


        $("#RangeTo").kendoNumericTextBox({
            format: "n0",
            decimals: 4,

        });

        $("#MiFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,

        });



        function clearDataRebateFeeSetup() {
            $("#lblRangeFrom").show();
            $("#lblRangeTo").show();
            $("#lblDateAmortize").show();
            $("#lblMiAmount").show();
            $("#lblFeeType").show();
            $("#lblDate").show();
            $("#lblMiPercent").show();
            $("#lblFundPK").show();

        }

        //readOnly();


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
        $("#RebateFeeSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#FundPK").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#RebateFeePercent").val("");
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
                             RebateFeeSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ValueDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             RebateFeePercent: { type: "number" },
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
            var gridPending = $("#gridRebateFeeSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridRebateFeeSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        
        $("#gridRebateFeeSetupApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var RebateFeeSetupApprovedURL = window.location.origin + "/Radsoft/RebateFeeSetup/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(RebateFeeSetupApprovedURL);

        }

        var grid = $("#gridRebateFeeSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Rebate Fee Setup"
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
                { field: "RebateFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ValueDate", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "FundID", title: "Fund (ID)", width: 200 },
                { field: "FundName", title: "Fund (Name)", width: 300 },
                { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
                { field: "RebateFeePercent", title: "Rebate Fee (%)", template: "#: RebateFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
            

            var grid = $("#gridRebateFeeSetupApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _RebateFeeSetupPK = dataItemX.RebateFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _RebateFeeSetupPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabRebateFeeSetup").kendoTabStrip({
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RebateFeeSetup/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RebateFeeSetup/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridRebateFeeSetupPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var RebateFeeSetupPendingURL = window.location.origin + "/Radsoft/RebateFeeSetup/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(RebateFeeSetupPendingURL);

        }
        var grid = $("#gridRebateFeeSetupPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form RebateFeeSetup"
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
                { field: "RebateFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ValueDate", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "FundID", title: "Fund (ID)", width: 200 },
                { field: "FundName", title: "Fund (Name)", width: 300 },
                { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
                { field: "RebateFeePercent", title: "Rebate Fee (%)", template: "#: RebateFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
            

            var grid = $("#gridRebateFeeSetupPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _RebateFeeSetupPK = dataItemX.RebateFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _RebateFeeSetupPK);

        }

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }
    function RecalGridHistory() {

        $("#gridRebateFeeSetupHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var RebateFeeSetupHistoryURL = window.location.origin + "/Radsoft/RebateFeeSetup/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(RebateFeeSetupHistoryURL);

        }
        $("#gridRebateFeeSetupHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form RebateFeeSetup"
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
                { field: "RebateFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ValueDate", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "FundID", title: "Fund (ID)", width: 200 },
                { field: "FundName", title: "Fund (Name)", width: 300 },
                { field: "FundClientName", title: "Fund Client (Name)", width: 300 },
                { field: "RebateFeePercent", title: "Rebate Fee (%)", template: "#: RebateFeePercent  # %", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridRebateFeeSetupHistory").data("kendoGrid");
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

    function HideBtnAdd(_status) {
        if (_status == 1) {
            $("#BtnAddRebateFee").show();
        }
        else if (_status == 2) {
            $("#BtnAddRebateFee").show();
        }
        else if (_status == 3) {
            $("#BtnAddRebateFee").hide();
        }
        else if (_status == 0) {
            $("#BtnAddRebateFee").hide();
        }
    }


    $("#BtnAddRebateFee").click(function () {
        clearDataRebateFee();
        GridRebateFee();
        if ($("#FundPK").val() == 0 || $("#FundPK").val() == null) {
            alertify.alert("There's no RebateFeeSetup");
        } else {
            showAddRebateFee();
        }
    });

    function showAddRebateFee(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid = $("#gridAddRebateFee").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }

        WinAddRebateFee.center();
        WinAddRebateFee.open();

    }

    function onWinAddRebateFeeClose() {
        GlobValidatorRebateFee.hideMessages();
        clearDataRebateFee();
        refresh();
    }

    function clearDataRebateFee() {
        
        $("#FeeType").data("kendoComboBox").value("");
        //$("#FundPK").data("kendoComboBox").value("");
        $("#MiFeeAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MiFeePercent").data("kendoNumericTextBox").value("");
        //$("#Date").val("");
        $("#DateAmortization").val("");

    }

    function clearDataRebateFeeType() {

        //$("#FeeType").data("kendoComboBox").value("");
        //$("#FundPK").data("kendoComboBox").value("");
        $("#MiFeeAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MiFeePercent").data("kendoNumericTextBox").value("");
        //$("#Date").val("");
        $("#DateAmortization").val("");

    }

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var RebateFeeSetup = {
                        ValueDate: $('#ValueDate').val(),
                        FundPK: $('#FundPK').val(),
                        FundClientPK: $('#FundClientPK').val(),
                        RebateFeePercent: $('#RebateFeePercent').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/RebateFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RebateFeeSetup_I",
                        type: 'POST',
                        data: JSON.stringify(RebateFeeSetup),
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
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RebateFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "RebateFeeSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var RebateFeeSetup = {
                                    RebateFeeSetupPK: $('#RebateFeeSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    FundPK: $('#FundPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    RebateFeePercent: $('#RebateFeePercent').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/RebateFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RebateFeeSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(RebateFeeSetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RebateFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "RebateFeeSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "RebateFeeSetup" + "/" + $("#RebateFeeSetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RebateFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "RebateFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var RebateFeeSetup = {
                                RebateFeeSetupPK: $('#RebateFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RebateFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RebateFeeSetup_A",
                                type: 'POST',
                                data: JSON.stringify(RebateFeeSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RebateFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "RebateFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var RebateFeeSetup = {
                                RebateFeeSetupPK: $('#RebateFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RebateFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RebateFeeSetup_V",
                                type: 'POST',
                                data: JSON.stringify(RebateFeeSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RebateFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "RebateFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var RebateFeeSetup = {
                                RebateFeeSetupPK: $('#RebateFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RebateFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RebateFeeSetup_R",
                                type: 'POST',
                                data: JSON.stringify(RebateFeeSetup),
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

   

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RebateFeeSetup/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
    $("#BtnRejectBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RebateFeeSetup/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/RebateFeeSetup/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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


    function getDataSourceListFundClient() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                 FundClientPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }

    $("#btnListFundClientPK").click(function () {
        initListFundClientPK();

        WinListFundClient.center();
        WinListFundClient.open();
        htmlFundClientPK = "#FundClientPK";
        htmlFundClientID = "#FundClientID";



    });

    function initListFundClientPK() {
        var dsListFundClient = getDataSourceListFundClient();
        $("#gridListFundClient").kendoGrid({
            dataSource: dsListFundClient,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListFundClientSelect }, title: " ", width: 85 },
               { field: "FundClientPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "Client ID", width: 300 }

            ]
        });
    }

    function ListFundClientSelect(e) {
        var grid = $("#gridListFundClient").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $("#FundClientName").val(dataItemX.Name);
        $("#FundClientID").val(dataItemX.ID);
        $(htmlFundClientPK).val(dataItemX.FundClientPK);
        WinListFundClient.close();


    }

    $("#BtnSaveRebateFee").click(function () {
        var val = validateDataRebateFee();
        checkmaxrange($('#BitMaxRangeTo').is(":checked"));
        RequiredAttributes($('#FeeType').val());
        var DateAmortize;
        var MiFeeAmount;
        var RangeFrom;
        var RangeTo;
        var MiFeePercent;

        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/RebateFeeSetup/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FeeType').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {

                                if ($('#DateAmortization').val() == 0 || $('#DateAmortization').val() == null || $('#DateAmortization').val() == "") {
                                    DateAmortize = ""
                                }
                                else {
                                    DateAmortize = $('#DateAmortization').val()
                                }

                                if ($('#MiFeeAmount').val() == 0 || $('#MiFeeAmount').val() == null || $('#MiFeeAmount').val() == "") {
                                    MiFeeAmount = 0
                                }
                                else {
                                    MiFeeAmount = $('#MiFeeAmount').val()
                                }

                                if ($('#RangeFrom').val() == 0 || $('#RangeFrom').val() == null || $('#RangeFrom').val() == "") {
                                    RangeFrom = 0
                                }
                                else {
                                    RangeFrom = $('#RangeFrom').val()
                                }

                                if ($('#RangeTo').val() == 0 || $('#RangeTo').val() == null || $('#RangeTo').val() == "") {
                                    RangeTo = 0
                                }
                                else {
                                    RangeTo = $('#RangeTo').val()
                                }

                                if ($('#MiFeePercent').val() == 0 || $('#MiFeePercent').val() == null || $('#MiFeePercent').val() == "") {
                                    MiFeePercent = 0
                                }
                                else {
                                    MiFeePercent = $('#MiFeePercent').val()
                                }
                                if ($('#BitMaxRangeTo').is(":checked") == true) {
                                    RangeTo = '9999'
                                }
                                else {
                                    RangeTo = $('#RangeTo').val()
                                }
                                var RebateFee = {
                                    RebateFeePK: $('#RebateFeePK').val(),
                                    TypeTrx: $('#TypeTrx').val(),
                                    FeeType: $('#FeeType').val(),
                                    Date: $('#Date').val(),
                                    DateAmortize: DateAmortize,
                                    MiFeeAmount: MiFeeAmount,
                                    FundPK: $('#FundPK').val(),
                                    RangeFrom: RangeFrom,
                                    RangeTo: RangeTo,
                                    MiFeePercent: MiFeePercent,
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/RebateFeeSetup/RebateFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                    type: 'POST',
                                    data: JSON.stringify(RebateFee),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        WinAddRebateFee.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });

                            }
                            else {
                                alertify.alert("Data Has Been Add, Check Your Date");
                                //WinAddRebateFee.close();
                                refresh();
                            }
                        }
                    });

                }
            });
        }

    });

    function checkmaxrange(_check) {
        if (_check == true) {
            $("#RangeTo").attr("required", false);
        }
        else {
            $("#RangeTo").attr("required", true);
        }
    }

    $("#BtnCancelRebateFee").click(function () {

        alertify.confirm("Are you sure want to close Add Rebate Fee ?",
            function (e) {
                if (e) {
                    WinAddRebateFee.close();
                    alertify.alert("Close Add Rebate Fee");
                }
            });
    });

    function getDataSourceRebateFee(_url) {
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
                             RebateFeeSetupPK: { type: "number" },
                             RebateFeeSetup: { type: "string" },
                             TypeTrx: { type: "number" },
                             TypeTrxDesc: { type: "string" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
                             Date: { type: "string" },
                             DateAmortization: { type: "string" },
                             MiFeeAmount: { type: "number" },
                             FundPK: { type: "number" },
                             FundName: { type: "string" },
                             RangeFrom: { type: "number" },
                             RangeTo: { type: "number" },
                             MiFeePercent: { type: "number" },
                         }
                     }
                 }
             });
    }

    function GridRebateFee() {
        $("#gridAddRebateFee").empty();
        var RebateFeeURL = window.location.origin + "/Radsoft/RebateFeeSetup/GetDataRebateFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
          dataSourceApproved = getDataSourceRebateFee(RebateFeeURL);

        var gridDetail = $("#gridAddRebateFee").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form RebateFeeSetup Fee"
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
            columns: [
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetail' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAll' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
                { field: "RebateFeeSetupDetailPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 110, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", title: "FundPK", hidden: true, width: 80 },
                { field: "FundName", title: "Fund", width: 250 },
                { field: "FeeTypeDesc", title: "Fee Type", width: 130 },
                { field: "RangeFrom", title: "Range From", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangeTo", title: "Range To", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MiFeeAmount", title: "Mi Fee Amount", width: 120, format: "{0:n2}", attributes: { style: "text-align:right;" } }, {
                    field: "MiFeePercent", title: "Mi Fee Percent", width: 120, format: "{0:n4}",
                    template: "#: MiFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == null) ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
            ]
        }).data("kendoGrid");
        $("#SelectedAll").change(function () {

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

        gridDetail.table.on("click", ".cSelectedDetail", selectDataPending);

        function selectDataPending(e) {


            var grid = $("#gridAddRebateFee").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _RebateFeeSetupDetailPK = dataItemX.RebateFeeSetupDetailPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _RebateFeeSetupDetailPK);

        }


        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RebateFeeSetupDetail/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RebateFeeSetupDetail/" + _a,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail").prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }



    $("#BtnRejectRebateFee").click(function () {

        alertify.confirm("Are you sure want to Reject This Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/RebateFeeSetup/RejectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinAddRebateFee.close();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
       
    });
    function RequiredAttributes(_type) {
        if (_type == 1 || _type == 2) {
            $("#DateAmortization").attr("required", false);
            $("#MiFeeAmount").attr("required", false);
        }
        else if (_type == 3 || _type == 4) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#MiFeePercent").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);

        }
        else if (_type == 5) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#DateAmortization").attr("required", false);
            $("#MiFeeAmount").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);
        }
    }

});
