$(document).ready(function () {
    document.title = 'FORM DORMANT ACCOUNT';
    
    //Global Variabel
    var win;
    var tabindex;
    var _d = new Date();
    var gridHeight = screen.height - 300;
    
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnAutoSuspend").click(function () {
        AutoSuspend();
    });

    if (_GlobClientCode == '20') {

        $("#LblformPoolingHeader").show();
    }
    else if (_GlobClientCode == '21') {
        
        $("#LblformNameHeader").hide();
    }

    if (_GlobClientCode == '20') {

        $("#LblParamAmount").show();
    }
    else
    {
        $("#LblParamAmount").hide();
    }

    
    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAutoSuspend").kendoButton({
            imageUrl: "../../Images/Icon/refresh2.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    function initWindow() {
        $("#ParamMonth").kendoNumericTextBox({
            format: "n0",
            value: setParamMonth()
        });
        function setParamMonth() {
            if ($("#ParamMonth").val() == null || $("#ParamMonth").val() == "") {
                return 1;
            } else {
                return dataItemX.ParamMonth;
            }
        }

        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                alertify.alert("Wrong Format Date MM/DD/YYYY");
                return;
            } else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                            return;
                        } else {
                            refresh();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        return;
                    }
                });
            }
        }


        $("#ParamAmount").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
        });


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
                             FundClientPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             BitIsSuspend: { type: "boolean" },
                             LastHoldingDate: { type: "date" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        //var newDS = getDataSource(window.location.origin + "/Radsoft/DormantAccount/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2/" + $("#ParamMonth").val());
        //$("#gridDormantAccount").data("kendoGrid").setDataSource(newDS);
        if ($("#ParamAmount").val() == null || $("#ParamAmount").val() == "" || $("#ParamAmount").val() == 0)
        {
            _paramAmount = 1000000
        }
        else
        {
            _paramAmount = $("#ParamAmount").val();
        }

        if ($("#ValueDate").val() == undefined || $("#ValueDate").val() == "" || $("#ValueDate").val() == null) {
            var newDS = getDataSource(window.location.origin + "/Radsoft/DormantAccount/GetDataByValueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/0/" + kendo.toString(new Date(), "MM-dd-yy") + "/" + _paramAmount);
            $("#gridDormantAccount").data("kendoGrid").setDataSource(newDS);
        } else {
            var newDS = getDataSource(window.location.origin + "/Radsoft/DormantAccount/GetDataByValueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramAmount);
            $("#gridDormantAccount").data("kendoGrid").setDataSource(newDS);

        }
    }
    
    function AutoSuspend() {        
        alertify.confirm("Are you sure want to Auto Suspend All Data ?", function (e) {
            if (e) {
                var DormantAccount = {
                    ParamMonth: $("#ParamMonth").val(),
                    ValueDate: $("#ValueDate").val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/DormantAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantAccount_AutoSuspend",
                    type: 'POST',
                    data: JSON.stringify(DormantAccount),
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

    function initGrid() {
        //var DormantAccountURL = window.location.origin + "/Radsoft/DormantAccount/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2/" + $("#ParamMonth").val(),
        //dataSource = getDataSource(DormantAccountURL);
        if ($("#ParamAmount").val() == null || $("#ParamAmount").val() == "" || $("#ParamAmount").val() == 0) {
            _paramAmount = 1000000
        }
        else {
            _paramAmount = $("#ParamAmount").val();
        }


        $("#gridDormantAccount").empty();
        if ($("#ValueDate").val() == undefined || $("#ValueDate").val() == "" || $("#ValueDate").val() == null) {
            alertify.alert("Please Fill Date");

            var DormantAccountURL = window.location.origin + "/Radsoft/DormantAccount/GetDataByValueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 0 + "/" + kendo.toString(new Date(), "MM-dd-yy") + "/" + _paramAmount,
            dataSource = getDataSource(DormantAccountURL);
        } else {
            var DormantAccountURL = window.location.origin + "/Radsoft/DormantAccount/GetDataByValueDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramAmount,
            dataSource = getDataSource(DormantAccountURL);
        }

        if (_GlobClientCode == "20") {
            var grid = $("#gridDormantAccount").kendoGrid({
                dataSource: dataSource,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Pooling Account"
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

                   //{ command: { text: "Approve", click: showUnSuspend }, title: " ", width: 100 },
                   { field: "FundClientPK", title: "Sys. No", hidden: true, width: 100 },
                   { field: "HistoryPK", title: "His. No", hidden: true, width: 100 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 85 },
                   { field: "StatusDesc", title: "Status", hidden: true, width: 125 },
                   { field: "FundClientID", title: "Client ID", hidden: true, width: 125 },
                   { field: "FundClientName", title: "Client Name", width: 200 },
                   { field: "IFUACode", title: "IFUA", width: 200 },
                   { field: "FundName", title: "Fund", width: 250 },
                   { field: "UnitAmount", title: "Unit", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                   { field: "Nav", title: "Nav", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "LastNavDate", title: "Last Nav Date", width: 120, template: "#= kendo.toString(kendo.parseDate(LastNavDate), 'dd/MMM/yyyy')#" },
                   { field: "CashAmount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                   { field: "BitIsSuspend", title: "Suspend", width: 100, hidden: true, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                   { field: "LastHoldingDate", title: "Last Holding Date", hidden: true, format: "{0:dd/MMM/yyyy}", width: 150 },
                   { field: "LastUpdate", title: "Last Update", hidden: true, format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                ]
            }).data("kendoGrid");
        }
        else if (_GlobClientCode == "21") {
            var grid = $("#gridDormantAccount").kendoGrid({
                dataSource: dataSource,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Dormant Account"
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

                   { command: { text: "Approve", click: showUnSuspend }, title: " ", width: 100 },
                   { field: "FundClientPK", title: "Sys. No", width: 100 },
                   { field: "HistoryPK", title: "His. No", width: 100 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 85 },
                   { field: "StatusDesc", title: "Status", width: 125 },
                   { field: "FundClientID", title: "Client ID", width: 125 },
                   { field: "FundClientName", title: "Client Name", width: 200 },
                   { field: "BitIsSuspend", title: "Suspend", width: 100, hidden: true, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                   { field: "LastHoldingDate", title: "Last Holding Date", format: "{0:dd/MMM/yyyy}", width: 150 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                ]
            }).data("kendoGrid");
        }

        else {
            var grid = $("#gridDormantAccount").kendoGrid({
                dataSource: dataSource,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Dormant Account"
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

                   //{ command: { text: "Suspend", click: showSuspend }, title: " ", width: 100 },
                   { field: "FundClientPK", title: "Sys. No", width: 100 },
                   { field: "HistoryPK", title: "His. No", width: 100 },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 85 },
                   { field: "StatusDesc", title: "Status", width: 125 },
                   { field: "FundClientID", title: "Client ID", width: 125 },
                   { field: "FundClientName", title: "Client Name", width: 200 },
                   { field: "BitIsSuspend", title: "Suspend", width: 100, hidden: true, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                   { field: "LastHoldingDate", title: "Last Holding Date", format: "{0:dd/MMM/yyyy}", width: 150 },
                   { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                ]
            }).data("kendoGrid");
        }
    
    }

    function showSuspend(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {            
            alertify.confirm("Are you sure want to Suspend ?", function (a) {
                if (a) {
                    var grid = $("#gridDormantAccount").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                    var _suspend = _dataItemX.BitIsSuspend;
                    var _clientPK = _dataItemX.FundClientPK;
                    var _historyPK = _dataItemX.HistoryPK;
                    var _status = _dataItemX.Status;

                    if (_suspend == true) {
                        alertify.alert("Data Already Suspend");
                    } else{
                        if (_status != 2) {
                            alertify.alert("Data Can't be Suspend Because Status Not Approved");
                        } else {
                            var DormantAccount = {
                                FundClientPK: _clientPK,
                                HistoryPK: _historyPK,
                                Status: _status,
                                EntryUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DormantAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantAccount_Suspend",
                                type: 'POST',
                                data: JSON.stringify(DormantAccount),
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
                    }
                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }



    function showUnSuspend(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            alertify.confirm("Are you sure want to UnSuspend ?", function (a) {
                if (a) {
                    var grid = $("#gridDormantAccount").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                    var _suspend = _dataItemX.BitIsSuspend;
                    var _clientPK = _dataItemX.FundClientPK;
                    var _historyPK = _dataItemX.HistoryPK;
                    var _status = _dataItemX.Status;


                    var DormantAccount = {
                        FundClientPK: _clientPK,
                        HistoryPK: _historyPK,
                        Status: _status,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DormantAccount/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DormantAccount_UnSuspend",
                        type: 'POST',
                        data: JSON.stringify(DormantAccount),
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
            e.handled = true;
        }
    }

    function onDataBound() {
        var grid = $("#gridDormantAccount").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsSuspend == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowRevised");
            }  else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowPending");
            }
        });
    }

});
