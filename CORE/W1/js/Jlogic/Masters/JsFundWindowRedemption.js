$(document).ready(function () {
    document.title = 'FORM FundWindowRedemption';
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
        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnExportToExcel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnOkGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnCancelGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnImportWindowRedemption").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function initWindow() {

        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });


        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });


        $("#FirstRedemptionDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#FirstDivDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        win = $("#WinFundWindowRedemption").kendoWindow({
            height: 550,
            title: "Fund Window Redemption Detail",
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

        WinGenerate = $("#WinGenerate").kendoWindow({
            height: 250,
            title: "Generate",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            //close: onWinGenerateClose
        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinFundWindowRedemption").kendoValidator().data("kendoValidator");

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
        //console.log($(e.currentTarget).closest("tr"));  
        $("#LblBtnImportWindowRedemption").hide();
        if (e == null) {
            HideBtnAdd(0);
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            //console.log(dataItemX);
            HideBtnAdd(dataItemX.Status, dataItemX.FundWindowRedemptionPK);
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#ID").attr('readonly', true);
                if (dataItemX.PaymentPeriod == 5) {
                    $("#LblBtnImportWindowRedemption").show();
                }
                else {
                    $("#LblBtnImportWindowRedemption").hide();
                }

            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#ID").attr('readonly', true);
                $("#LblBtnImportWindowRedemption").hide();
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#LblBtnImportWindowRedemption").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide()
                $("#LblBtnImportWindowRedemption").hide();;
            }

            $("#FundWindowRedemptionPK").val(dataItemX.FundWindowRedemptionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FirstRedemptionDate").data("kendoDatePicker").value(dataItemX.FirstRedemptionDate);
            $("#FirstDivDate").data("kendoDatePicker").value(dataItemX.FirstDivDate);
            $("#VariableDate").val(dataItemX.VariableDate);
            $("#RedempDate").prop('checked', dataItemX.RedempDate);
            $("#PaymentDate").val(dataItemX.PaymentDate);
            $("#Description").val(dataItemX.Description);
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

        // FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
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


        // PeriodPK


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemptionPeriod",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PaymentPeriod").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
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
            //if ($("#InvestorType").data("kendoComboBox").value() == 5) {
            //    $("#LblBtnImportWindowRedemption").show();
            //}
            //else
            //{

            //}
        }

        function setCmbPeriodPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PaymentPeriod;
            }
        }


        $("#VariableDate").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setVariableDate()
        });
        function setVariableDate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.VariableDate;
            }
        }

        $("#PaymentDate").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setPaymentDate()
        });
        function setPaymentDate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PaymentDate;
            }
        }

        $('#RedempDate').change(function () {
            if (this.checked) {
                clearFirstDate();
                $("#FirstRedemptionDate").attr('readonly', true);
                $("#FirstRedemptionDate ").data("kendoDatePicker").value(kendo.parseDate("01/01/1900"));
            }
            else {
                $("#FirstRedemptionDate").attr('readonly', false);
                clearFirstDate();
            }
        });


        win.center();
        win.open();
    }

    function clearFirstDate()
    {
        $("#FirstRedemptionDate").val("");
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundWindowRedemptionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundPK").val("");
        $("#FirstRedemptionDate").val("");
        $("#FirstDivDate").val("");
        $("#PaymentPeriod").val("");
        $("#VariableDate").val("");
        $("#PaymentDate").val("");
        $("#Description").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#RedempDate").prop('checked', false);
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
                            FundWindowRedemptionPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            FirstRedemptionDate: { type: "date" },
                            FirstDivDate: { type: "date" },
                            FundPK: { type: "string" },
                            FundID: { type: "string" },
                            PaymentPeriod: { type: "number" },
                            PaymentPeriodDesc: { type: "string" },
                            VariableDate: { type: "number" },
                            PaymentDate: { type: "number" },
                            Description: { type: "string" },
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
            var gridApproved = $("#gridFundWindowRedemptionApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundWindowRedemptionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundWindowRedemptionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        $("#gridFundWindowRedemptionApproved").empty();
        var FundWindowRedemptionApprovedURL = window.location.origin + "/Radsoft/FundWindowRedemption/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FundWindowRedemptionApprovedURL);

        
        var grid = $("#gridFundWindowRedemptionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FundWindowRedemption"
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
                { field: "FundWindowRedemptionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FirstRedemptionDate", title: "First Redemption Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstRedemptionDate), 'dd/MMM/yyyy')#" },
                { field: "FirstDivDate", title: "First Div Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstDivDate), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "PaymentPeriodDesc", title: "Payment Period", width: 200 },
                { field: "VariableDate", title: "Variable Date", width: 150 },
                { field: "PaymentDate", title: "Payment Date", width: 150 },
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


            var grid = $("#gridFundWindowRedemptionApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _fundWindowRedemptionPK = dataItemX.FundWindowRedemptionPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _fundWindowRedemptionPK);

        }

        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundWindowRedemption/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundWindowRedemption/" + _a,
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

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundWindowRedemption").kendoTabStrip({
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
        $("#gridFundWindowRedemptionPending").empty();
        var FundWindowRedemptionPendingURL = window.location.origin + "/Radsoft/FundWindowRedemption/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
            dataSourcePending = getDataSource(FundWindowRedemptionPendingURL);

        $("#gridFundWindowRedemptionPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FundWindowRedemption"
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
                { field: "FundWindowRedemptionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FirstRedemptionDate", title: "First Redemption Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstRedemptionDate), 'dd/MMM/yyyy')#" },
                { field: "FirstDivDate", title: "First Div Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstDivDate), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "PaymentPeriodDesc", title: "Payment Period", width: 200 },
                { field: "VariableDate", title: "Variable Date", width: 150 },
                { field: "PaymentDate", title: "Payment Date", width: 150 },
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

    function RecalGridHistory() {
        $("#gridFundWindowRedemptionHistory").empty();
        var FundWindowRedemptionHistoryURL = window.location.origin + "/Radsoft/FundWindowRedemption/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
            dataSourceHistory = getDataSource(FundWindowRedemptionHistoryURL);

        $("#gridFundWindowRedemptionHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FundWindowRedemption"
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
                { field: "FundWindowRedemptionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FirstRedemptionDate", title: "First Redemption Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstRedemptionDate), 'dd/MMM/yyyy')#" },
                { field: "FirstDivDate", title: "First Div Date", width: 200, template: "#= kendo.toString(kendo.parseDate(FirstDivDate), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "PaymentPeriodDesc", title: "Payment Period", width: 200 },
                { field: "VariableDate", title: "Variable Date", width: 150 },
                { field: "PaymentDate", title: "Payment Date", width: 150 },
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

    function gridHistoryDataBound() {
        var grid = $("#gridFundWindowRedemptionHistory").data("kendoGrid");
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
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            $.ajax({
                url: window.location.origin + "/Radsoft/FundWindowRedemption/CheckDataFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Add data?", function (e) {
                            if (e) {

                                var FundWindowRedemption = {
                                    FirstRedemptionDate: $('#FirstRedemptionDate').val(),
                                    FirstDivDate: $('#FirstDivDate').val(),
                                    FundPK: $('#FundPK').val(),
                                    VariableDate: $('#VariableDate').val(),
                                    PaymentPeriod: $('#PaymentPeriod').val(),
                                    PaymentDate: $('#PaymentDate').val(),
                                    Description: $('#Description').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundWindowRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemption_I",
                                    type: 'POST',
                                    data: JSON.stringify(FundWindowRedemption),
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
                        alertify.alert('Check Fund')
                    }
                }
            });

        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {

            //$.ajax({
            //    url: window.location.origin + "/Radsoft/FundWindowRedemption/CheckDataFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
            //    type: 'GET',
            //    contentType: "application/json;charset=utf-8",
            //    success: function (data) {
            //        if (data == false)
            //        {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundWindowRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundWindowRedemption",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundWindowRedemption = {
                                    FundWindowRedemptionPK: $('#FundWindowRedemptionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FirstRedemptionDate: $('#FirstRedemptionDate').val(),
                                    FirstDivDate: $('#FirstDivDate').val(),
                                    FundPK: $('#FundPK').val(),
                                    VariableDate: $('#VariableDate').val(),
                                    PaymentPeriod: $('#PaymentPeriod').val(),
                                    PaymentDate: $('#PaymentDate').val(),
                                    Description: $('#Description').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundWindowRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemption_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundWindowRedemption),
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
            //        }
            //        else
            //        {
            //            alertify.alert('Check Fund')
            //        }
            //    }
            //});

        }
    });

    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundWindowRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundWindowRedemption",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundWindowRedemption" + "/" + $("#FundWindowRedemptionPK").val(),
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
                    url: window.location.origin + "/Radsoft/FundWindowRedemption/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundWindowRedemptionPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundWindowRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundWindowRedemption",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                        var FundWindowRedemption = {
                                            FundWindowRedemptionPK: $('#FundWindowRedemptionPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundWindowRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemption_A",
                                            type: 'POST',
                                            data: JSON.stringify(FundWindowRedemption),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
                                                win.close();

                                                var d = new Date();
                                                var c = new Date(d.getFullYear() + 1, d.getMonth(), d.getDate());

                                                var FundWindow = {
                                                    ParamDateFrom: $("#ParamDateFrom").val(),
                                                    ParamDateTo: kendo.toString(c, 'yyyy-MM-dd'),
                                                    EntryUsersID: sessionStorage.getItem("user")
                                                };

                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/FundWindowRedemption/GenerateWindowRedemption/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                    type: 'POST',
                                                    data: JSON.stringify(FundWindow),
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {

                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });

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
                        else {
                            alertify.alert("Please Check Fund")
                        }

                    }
                });

            }
        });
    });

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundWindowRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundWindowRedemption",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundWindowRedemption = {
                                FundWindowRedemptionPK: $('#FundWindowRedemptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundWindowRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemption_V",
                                type: 'POST',
                                data: JSON.stringify(FundWindowRedemption),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundWindowRedemptionPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundWindowRedemption",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundWindowRedemption = {
                                FundWindowRedemptionPK: $('#FundWindowRedemptionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundWindowRedemption/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundWindowRedemption_R",
                                type: 'POST',
                                data: JSON.stringify(FundWindowRedemption),
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

    function HideBtnAdd(_status, _fundWindowRedemptionPK) {
        ResetBtn();
        if (_status == 1) {
            $("#BtnGenerate").hide();
            $("#gridFundWindowRedemptionDetail").show();
            GridFundWindowRedemptionDetail(_fundWindowRedemptionPK)
        }
        else if (_status == 2) {
            $("#BtnGenerate").show();
            $("#gridFundWindowRedemptionDetail").show();
            GridFundWindowRedemptionDetail(_fundWindowRedemptionPK)
        }
        else if (_status == 3) {
            $("#BtnGenerate").hide();
            $("#gridFundWindowRedemptionDetail").hide();
        }
        else if (_status == 0) {
            $("#BtnGenerate").hide();
            $("#gridFundWindowRedemptionDetail").hide();
        }
    }

    function ResetBtn()
    {
        $("#BtnGenerate").hide();
    }

    function getDataSourceFundWindowRedemptionDetail(_url) {
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
                             FundWindowRedemptionPK: { type: "number" },
                             MaxRedemptionDate: { type: "string" },
                             DividenDate: { type: "string" },
                             PaymentDate: { type: "string" },
                             EntryUsersID: { type: "string" },
                             LastUpdate: { type: "string" },
                         }
                     }
                 }
             });
    }

    function GridFundWindowRedemptionDetail(_fundWindowRedemptionPK) {
        $("#gridFundWindowRedemptionDetail").empty();
        var FundWindowRedemptionDetailURL = window.location.origin + "/Radsoft/FundWindowRedemption/GetDataWindowRedemptionDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundWindowRedemptionPK,
          dataSourceApproved = getDataSourceFundWindowRedemptionDetail(FundWindowRedemptionDetailURL);

        var gridDetail = $("#gridFundWindowRedemptionDetail").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Window Redemption Detail"
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
                { field: "FundWindowRedemptionPK", title: "SysNo.", width: 95 },
                { field: "MaxRedemptionDate", title: "MaxRedemptionDate", width: 130, template: "#= kendo.toString(kendo.parseDate(MaxRedemptionDate), 'dd/MMM/yyyy')#" },
                { field: "DividenDate", title: "DividenDate", width: 130, template: "#= kendo.toString(kendo.parseDate(DividenDate), 'dd/MMM/yyyy')#" },
                { field: "PaymentDate", title: "Payment Date", width: 130, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MMM/yyyy')#" },
                { field: "EntryUsersID", title: "EntryUsersID", width: 130 },
                { field: "LastUpdate", title: "LastUpdate", width: 130, template: "#= (LastUpdate == null) ? ' ' : kendo.toString(kendo.parseDate(LastUpdate), 'dd/MMM/yyyy')#" },
            ]
        }).data("kendoGrid");


    }

    $("#BtnGenerate").click(function () {
        showGenerate();
    });

    function showGenerate(e) {
        



        WinGenerate.center();
        WinGenerate.open();

    }

    $("#BtnOkGenerate").click(function () {

        alertify.confirm("Are you sure want to Generate data?", function (e) {
            if (e) {
                $.blockUI({});
                var FundWindowRedemption = {
                    ParamDateFrom: $("#ParamDateFrom").val(),
                    ParamDateTo: $("#ParamDateTo").val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundWindowRedemption/GenerateWindowRedemption/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundWindowRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Generate Fund Window Redemption Success");
                        win.close();
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

    $("#BtnCancelGenerate").click(function () {

        alertify.confirm("Are you sure want to cancel Generate?", function (e) {
            if (e) {
                WinGenerate.close();
                alertify.alert("Cancel Generate");
            }
        });
    });

    $("#BtnExportToExcel").click(function () {
        
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission;
                var FundWindowRedemption = {
                    FundWindowRedemptionPK: $('#FundWindowRedemptionPK').val(),
                    Status: $('#Status').val(),

                };


                $.ajax({
                    url: window.location.origin + "/Radsoft/FundWindowRedemption/ExportToExcelDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundWindowRedemption),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });

    $("#BtnImportWindowRedemption").click(function () {
        document.getElementById("FileImportWindowRedemption").click();
    });

    $("#FileImportWindowRedemption").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportWindowRedemption").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }
        if (files.length > 0) {
            data.append("WindowRedemption", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "WindowRedemption_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportWindowRedemption").val("");
                    refresh();
                    GridFundWindowRedemptionDetail($("#FundWindowRedemptionPK").val());

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportWindowRedemption").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportWindowRedemption").val("");
        }
    });



});
