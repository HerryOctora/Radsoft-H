$(document).ready(function () {
    document.title = 'FORM HIGH RISK MONITORING';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
    var htmlFundClientPK;
    var htmlFundClientID;

    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();


    if (_GlobClientCode == "20") {
        $("#LblFilterType").show();
        $("#LblFilterTypeInput").show();
        $("#LblHighRiskMonStatus").show();
        $("#lblUpdateKYC").show();

    }
    else {
        $("#LblFilterType").hide();
        $("#LblFilterTypeInput").hide();
        $("#LblHighRiskMonStatus").hide();
        $("#lblUpdateKYC").hide();
    }


    if (_GlobClientCode == "29") {
        $("#BtnCheck100MilClient").show();
    }
    else {
        $("#BtnCheck100MilClient").hide();
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

        $("#BtnUnApproved").kendoButton({
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

        $("#BtnSuspendBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnUpdateKYCRiskProfile").kendoButton({

        });

        $("#BtnCheck100MilClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnCheck.png"
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


        win = $("#WinHighRiskMonitoring").kendoWindow({
            height: 900,
            title: "High Risk Monitoring Detail",
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

        WinListFundClient = $("#WinListFundClient").kendoWindow({
            height: "520px",
            title: "Fund Client List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListFundClientClose
        }).data("kendoWindow");




        $("#FilterType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "All", value: 0 },
                { text: "KYC & Unit Registry ", value: 1 },
                { text: "OMS Transaction", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeFilterType,
            value: setCmbFilterType()
        });
        function OnChangeFilterType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh();
        }
        function setCmbFilterType() {
            
                return 1;
            
        }
    }

    var GlobValidator = $("#WinHighRiskMonitoring").kendoValidator().data("kendoValidator");
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
                grid = $("#gridHighRiskMonitoringApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridHighRiskMonitoringPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridHighRiskMonitoringHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").hide();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#BtnUnApproved").show();
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnUnApproved").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnUnApproved").hide();
            }

            $("#HighRiskMonitoringPK").val(dataItemX.HighRiskMonitoringPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientName);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#Reason").val(dataItemX.Reason);
            $("#Description").val(dataItemX.Description);
            $("#BitIsSuspend").prop('checked', dataItemX.BitIsSuspend);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            if (dataItemX.HighRiskType == "98" || dataItemX.HighRiskType == "99") {
                initReferenceDetail();
                $("#lblUpdateKYC").show();
            }

            else {
                $("#gridFundClientDocument").hide();
                $("#lblUpdateKYC").hide();
            }
        }
        //combo box HighRiskMonStatus
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HighRiskMonStatus",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#HighRiskMonStatus").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeHighRiskMonStatus,
                    value: setCmbHighRiskMonStatus()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeHighRiskMonStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbHighRiskMonStatus() {
            if (e == null) {
                return 1;
            } else {
                if (dataItemX.HighRiskMonStatus == 0) {
                    return 1;
                } else {
                    return dataItemX.HighRiskMonStatus;
                }
            }
        }

        //combo box KYC Risk Profile
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/KYCRiskProfile",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#KYCRiskProfile").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeKYCRiskProfile,
                    value: setCmbKYCRiskProfile()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeKYCRiskProfile() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbKYCRiskProfile() {
            if (dataItemX.KYCRiskProfile == null) {
                return "";
            } else {
                if (dataItemX.KYCRiskProfile == 0) {
                    return "";
                } else {
                    return dataItemX.KYCRiskProfile;
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
        $("#HighRiskMonitoringPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#Date").val("");
        $("#Reason").val("");
        $("#Description").val("");
        $("#HighRiskMonStatus").val("");
        $("#BitIsSuspend").prop('checked', false);
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
                            HighRiskMonitoringPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Selected: { type: "boolean" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            FundClientPK: { type: "number" },
                            FundClientID: { type: "string" },
                            Date: { type: "date" },
                            Reason: { type: "string" },
                            Description: { type: "string" },
                            HighRiskMonStatus: { type: "number" },
                            HighRiskMonStatusDesc: { type: "string" },
                            BitIsSuspend: { type: "bool" },
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
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()
        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridHighRiskMonitoringPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridHighRiskMonitoringHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridHighRiskMonitoringApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsSuspend == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("highRiskSuspend");
            }  else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            }
        });
    }

    function gridPendingOnDataBound() {
        var grid = $("#gridHighRiskMonitoringPending").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsSuspend == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("highRiskSuspend");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            }
        });
    }

    function initGrid() {

        if ($("#FilterType").val() == "") {
            _Type = 0;
        }
        else {
            _Type = $("#FilterType").val();
        }

        $("#gridHighRiskMonitoringApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var HighRiskMonitoringApprovedURL = window.location.origin + "/Radsoft/HighRiskMonitoring/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _Type,
                dataSourceApproved = getDataSource(HighRiskMonitoringApprovedURL);
        }


        if (_GlobClientCode == "20") {
            var grid = $("#gridHighRiskMonitoringApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                dataBound: gridApprovedOnDataBound,
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
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 200 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "33") {
                var grid = $("#gridHighRiskMonitoringApproved").kendoGrid({
                    dataSource: dataSourceApproved,
                    height: gridHeight,
                    scrollable: {
                        virtual: true
                    },
                    groupable: {
                        messages: {
                            empty: "Form High Risk Monitoring"
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
                    dataBound: gridApprovedOnDataBound,
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
                        { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                        { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                        { field: "InvestmentNo", title: "Key Number", width: 200 },
                        { field: "FundClientID", title: "Name", width: 200 },
                        { field: "ClientType", title: "Client Type", width: 200 },
                        { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                        { field: "Reason", title: "Reason", width: 400 },
                        { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                        { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                    ]
                }).data("kendoGrid");
        }
        else {
                var grid = $("#gridHighRiskMonitoringApproved").kendoGrid({
                    dataSource: dataSourceApproved,
                    height: gridHeight,
                    scrollable: {
                        virtual: true
                    },
                    groupable: {
                        messages: {
                            empty: "Form High Risk Monitoring"
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
                    dataBound: gridApprovedOnDataBound,
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
                        { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                        { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                        { field: "InvestmentNo", title: "Key Number", width: 200 },
                        { field: "FundClientID", title: "Name", width: 200 },
                        { field: "ClientType", title: "Client Type", width: 200 },
                        { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                        { field: "Reason", title: "Reason", width: 200 },
                        { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                    ]
                }).data("kendoGrid");
        }





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


            var grid = $("#gridHighRiskMonitoringApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _HighRiskMonitoringPK = dataItemX.HighRiskMonitoringPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _HighRiskMonitoringPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabHighRiskMonitoring").kendoTabStrip({
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

    }

    function ResetButtonBySelectedData() {
        $("#BtnSuspendBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HighRiskMonitoring/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/HighRiskMonitoring/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridHighRiskMonitoringPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            if ($("#FilterType").val() == "") {
                _Type = 0;
            }
            else {
                _Type = $("#FilterType").val();
            }
            var HighRiskMonitoringPendingURL = window.location.origin + "/Radsoft/HighRiskMonitoring/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _Type,
                dataSourcePending = getDataSource(HighRiskMonitoringPendingURL);

        }

        if (_GlobClientCode == "20") {
            var grid = $("#gridHighRiskMonitoringPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                dataBound: gridPendingOnDataBound,
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
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 200 },
                    { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "33") {
                var grid = $("#gridHighRiskMonitoringPending").kendoGrid({
                    dataSource: dataSourcePending,
                    height: gridHeight,
                    scrollable: {
                        virtual: true
                    },
                    groupable: {
                        messages: {
                            empty: "Form High Risk Monitoring"
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
                    dataBound: gridPendingOnDataBound,
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
                        { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                        { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                        { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                        { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                        { field: "InvestmentNo", title: "Key Number", width: 200 },
                        { field: "FundClientID", title: "Name", width: 200 },
                        { field: "ClientType", title: "Client Type", width: 200 },
                        { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                        { field: "Reason", title: "Reason", width: 400 },
                        { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                        { field: "EntryUsersID", title: "Entry ID", width: 200 },
                        { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                        { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                        { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "VoidUsersID", title: "VoidID", width: 200 },
                        { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                        { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                    ]
                }).data("kendoGrid");
        }
        else {
            var grid = $("#gridHighRiskMonitoringPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                dataBound: gridPendingOnDataBound,
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
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            }).data("kendoGrid");
        }


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


            var grid = $("#gridHighRiskMonitoringPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _HighRiskMonitoringPK = dataItemX.HighRiskMonitoringPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _HighRiskMonitoringPK);

        }

        ResetButtonBySelectedData();


    }

    function RecalGridHistory() {

        $("#gridHighRiskMonitoringHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            if ($("#FilterType").val() == "") {
                _Type = 0;
            }
            else {
                _Type = $("#FilterType").val();
            }
            var HighRiskMonitoringHistoryURL = window.location.origin + "/Radsoft/HighRiskMonitoring/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _Type,
                dataSourceHistory = getDataSource(HighRiskMonitoringHistoryURL);

        }

        if (_GlobClientCode == "20") {
            $("#gridHighRiskMonitoringHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 200 },
                    { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },

                ]
            });
        }
        else if (_GlobClientCode == "33") {
            $("#gridHighRiskMonitoringHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 400 },
                    { field: "HighRiskMonStatusDesc", title: "High Risk Mon Status", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },

                ]
            });
        }
        else {
            $("#gridHighRiskMonitoringHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form High Risk Monitoring"
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
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:MM/dd/yyyy HH:mm:ss}", width: 180 },
                    { field: "HighRiskMonitoringPK", title: "SysNo.", width: 125 },
                    { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                    { field: "InvestmentNo", title: "Key Number", width: 200 },
                    { field: "FundClientID", title: "Name", width: 200 },
                    { field: "ClientType", title: "Client Type", width: 200 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                    { field: "Reason", title: "Reason", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },

                ]
            });
        }

        $("#BtnSuspendBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridHighRiskMonitoringHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsSuspend == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("highRiskSuspend");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
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

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "HighRiskMonitoring" + "/" + $("#HighRiskMonitoringPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var HighRiskMonitoring = {
                                HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HighRiskMonitoring_A",
                                type: 'POST',
                                data: JSON.stringify(HighRiskMonitoring),
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

    $("#BtnUnApproved").click(function () {
        
        alertify.confirm("Are you sure want to UnApprove data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var HighRiskMonitoring = {
                                HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                UnApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HighRiskMonitoring_UnApproved",
                                type: 'POST',
                                data: JSON.stringify(HighRiskMonitoring),
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

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var HighRiskMonitoring = {
                                    HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Description: $('#Description').val(),
                                    HighRiskMonStatus: $('#HighRiskMonStatus').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HighRiskMonitoring_U",
                                    type: 'POST',
                                    data: JSON.stringify(HighRiskMonitoring),
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

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var HighRiskMonitoring = {
                                HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HighRiskMonitoring_V",
                                type: 'POST',
                                data: JSON.stringify(HighRiskMonitoring),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#HighRiskMonitoringPK").val() + "/" + $("#HistoryPK").val() + "/" + "HighRiskMonitoring",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var HighRiskMonitoring = {
                                HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/HighRiskMonitoring/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "HighRiskMonitoring_R",
                                type: 'POST',
                                data: JSON.stringify(HighRiskMonitoring),
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
                 pageSize: 25,
                 schema: {
                     model: {
                         fields: {
                             FundClientPK: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },

                         }
                     }
                 }
             });
    }

    $("#btnListFundClient").click(function () {
        WinListFundClient.center();
        WinListFundClient.open();
        initListFundClient();
        htmlFundClientPK = "#FundClientPK";
        htmlFundClientID = "#FundClientID";
    });


    function initListFundClient() {
        var dsListFundClient = getDataSourceListFundClient();
        $("#gridListFundClient").kendoGrid({
            dataSource: dsListFundClient,
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
               { command: { text: "Select", click: ListFundClientSelect }, title: " ", width: 60 },
               { field: "ID", title: "Fund Client ID", width: 200 },
               { field: "Name", title: "Fund Client Name", width: 200 }
            ]
        });
    }

    function onWinListFundClientClose() {
        $("#gridListFundClient").empty();
    }

    function ListFundClientSelect(e) {
        var grid = $("#gridListFundClient").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlFundClientID).val(dataItemX.ID);
        $(htmlFundClientPK).val(dataItemX.FundClientPK);
        WinListFundClient.close();

    }


    $("#BtnSuspendBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Suspend by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/SuspendBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    function getDataSourceReferenceDetail() {

        var _urlRef = "";
        _urlRef = window.location.origin + "/Radsoft/FundClientDocument/GetListForHighRiskMonitoring/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundClientPK').val();

        return new kendo.data.DataSource(

            {

                transport:
                {

                    read:
                    {

                        url: _urlRef,
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 100,
                schema: {
                    model: {
                        fields: {
                            EntryTime: { type: "date" }

                        }
                    }
                }
            });
    }

    function initReferenceDetail() {
        $("#gridFundClientDocument").empty();

        var dsListReference = getDataSourceReferenceDetail();
        $("#gridFundClientDocument").kendoGrid({
            dataSource: dsListReference,
            height: 200,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            columns: [
                { field: "EntryTime", title: "Upload Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "DocPath", title: "FilePath", width: 250, template: "<a href='" + window.location.origin + "/DocumentFundClient/${DocPath}'>${DocPath}</a>" },
                { field: "Description", title: "Description", width: 300 },
            ]
        });
    }

    $("#BtnUpdateKYCRiskProfile").click(function (e) {

        alertify.prompt("Are you sure want to Update KYC, please give notes:", "", function (e, str) {
            if (e) {
                var HighRiskMonitoring = {
                    HighRiskMonitoringPK: $('#HighRiskMonitoringPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    KYCRiskProfile: $('#KYCRiskProfile').val(),
                    Notes: str,
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/UpdateKYCFromHighRiskMonitoring/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(HighRiskMonitoring),
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

    $("#BtnCheck100MilClient").click(function (e) {

        alertify.confirm("Are you sure want check Client ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/HighRiskMonitoring/Check100MilClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();

                        $.ajax({
                            url: window.location.origin + "/Radsoft/HostToHostSwivel/HelpDeskCreate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/3",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                console.log(data);
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


});
