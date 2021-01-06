$(document).ready(function () {
    document.title = 'FORM CUSTOMER SERVICE BOOK';
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

        $("#BtnDone").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnUnDone").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });
    }


    function initWindow() {

        win = $("#WinCustomerServiceBook").kendoWindow({
            height: 600,
            title: "Customer Service Book Detail",
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

    }


    var GlobValidator = $("#WinCustomerServiceBook").kendoValidator().data("kendoValidator");

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
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            $("#BtnDone").hide();
            $("#BtnUnDone").hide();
            ShowHide(e);

        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            ShowHide(dataItemX.ClientType);
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnDone").hide();
                Done(dataItemX.StatusMessage);
                //  $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#BtnDone").show();
                Done(dataItemX.StatusMessage);
                //   $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnDone").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnDone").hide();
            }

            if (dataItemX.StatusMessage == 2)
            {
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#BtnDone").hide();
                $("#BtnUnDone").show();

            }

            if (dataItemX.StatusMessage == 3) {
                if (dataItemX.Status == 2)
                {
                    $("#StatusHeader").val("APPROVED");
                    $("#BtnApproved").hide();
                    $("#BtnReject").hide();
                    $("#BtnAdd").hide();
                    $("#BtnOldData").hide();
                    $("#BtnDone").show();
                    $("#BtnDone").show();
                    $("#BtnUnDone").hide();
                }
                

            }

            $("#CustomerServiceBookPK").val(dataItemX.CustomerServiceBookPK);
            $("#Message").val(dataItemX.Message);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ClientName").val(dataItemX.ClientName);
            $("#Email").val(dataItemX.Email);
            $("#Solution").val(dataItemX.Solution);
            $("#Phone").val(dataItemX.Phone);
            $("#InternalComment").val(dataItemX.InternalComment);
            $("#IsNeedToReport").prop('checked', dataItemX.IsNeedToReport);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#DoneUsersID").val(dataItemX.DoneUsersID);
            $("#UnDoneUsersID").val(dataItemX.UnDoneUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
            $("#DoneTime").val(kendo.toString(kendo.parseDate(dataItemX.DoneTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UnDoneTime").val(kendo.toString(kendo.parseDate(dataItemX.UnDoneTime), 'dd/MMM/yyyy HH:mm:ss'));
        }



        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ClientType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ClientType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeClientType,
                    dataSource: data,
                    value: setCmbClientType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeClientType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

                
            }
            if ($("#ClientType").val() == 1) {
                $("#lblFundClient").hide();
                $("#lbName").show();
                $("#lblEmail").show();
                $("#lblPhone").show();
                $("#FundClientPK").attr("required", false);
            }
            else if ($("#ClientType").val() == "") 
            {
                $("#lblFundClient").hide();
                $("#lblEmail").hide();
                $("#lblPhone").hide();
                $("#lbName").hide();
            }
            else if ($("#ClientType").val() == 2)
            {
                $("#lblFundClient").show();
                $("#lblEmail").show();
                $("#lbName").hide();
                $("#lblPhone").show();
                $("#ClientName").attr("required", false);
            }
        }
        function setCmbClientType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ClientType == 0) {
                    return "";
                } else {
                    return dataItemX.ClientType;
                }
            }
        }


        //Decimal Places
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AskLine",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AskLine").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeAskLine,
                    dataSource: data,
                    value: setCmbAskLine()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeAskLine() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAskLine() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AskLine == 0) {
                    return 0;
                } else {
                    return dataItemX.AskLine;
                }
            }
        }

        //InterestDaysType

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/StatusMessage",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#StatusMessage").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeStatusMessage,
                    value: setCmbStatusMessage()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeStatusMessage() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }
        }
        function setCmbStatusMessage() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.StatusMessage == 0) {
                    return "";
                } else {
                    return dataItemX.StatusMessage;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundClientPK").kendoComboBox({
                    dataValueField: "FundClientPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundClientPK,
                    dataSource: data,
                    value: setCmbFundClientPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundClientPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }
            GetIdentity($('#FundClientPK').val());
        }
        function setCmbFundClientPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundClientPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundClientPK;
                }
            }
        }


        function ShowHide(_clientType)
        {
            if(_clientType==null)
            {
                $("#lblFundClient").hide();
                $("#lblEmail").show();
                $("#lblPhone").show();
            }

            else if (_clientType == 1)
            {
                $("#lblFundClient").hide();
                $("#lbName").show();
                $("#lblEmail").show();
                $("#lblPhone").show();
            }

            else if (_clientType == 2)
            {
                $("#lblFundClient").show();
                $("#lbName").hide();
                $("#lblEmail").show();
                $("#lblPhone").show();
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

    function clearData()
    {
        $("#FundClientPK").data("kendoComboBox").value("");
        $("#AskLine").data("kendoComboBox").value("");
        $("#Message").val("");
        $("#ClientName").val("");
        $("#Email").val("");
        $("#Solution").val("");
        $("#Phone").val("");
        $("#StatusMessage").val("");
        $("#InternalComment").val("");
        $("#IsNeedToReport").prop('checked', false);
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
                             CustomerServiceBookPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
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
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridCustomerServiceBookApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCustomerServiceBookPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCustomerServiceBookHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CustomerServiceBookApprovedURL = window.location.origin + "/Radsoft/CustomerServiceBook/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(CustomerServiceBookApprovedURL);

        $("#gridCustomerServiceBookApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Customer Service Book"
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
                { field: "CustomerServiceBookPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ClientTypeDesc", title: "Client Type", width: 150 },
                { field: "ClientName", title: "Client Name", width: 250 },
                { field: "FundClientPK", title: "FuncdClient", hidden: true, width: 200 },
                { field: "AskLineDesc", title: "Ask Line", width: 100 },
                { field: "Message", title: "Message", width: 300 },
                { field: "Email", title: "Email", width: 250 },
                { field: "Solution", title: "Solution", width: 300 },
                { field: "Phone", title: "Phone", width: 200 },
                { field: "StatusMessageDesc", title: "Status Message", width: 200 },
                { field: "InternalComment", title: "Internal Comment", width: 300 },
                { field: "IsNeedToReport", title: "Is Need To Report", width: 150, template: "#= IsNeedToReport ? 'Yes' : 'No' #" },
                { field: "DoneUsersID", title: "Done ID", width: 200 },
                { field: "DoneTime", title: "DoneTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UnDoneUsersID", title: "UnDone ID", width: 200 },
                { field: "UnDoneTime", title: "UnDone Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCustomerServiceBook").kendoTabStrip({
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
                        var CustomerServiceBookPendingURL = window.location.origin + "/Radsoft/CustomerServiceBook/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(CustomerServiceBookPendingURL);
                        $("#gridCustomerServiceBookPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CustomerServiceBook"
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
                                { field: "CustomerServiceBookPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ClientTypeDesc", title: "Client Type", width: 150 },
                                { field: "ClientName", title: "Client Name", width: 250 },
                                { field: "FundClientPK", title: "FuncdClient", hidden: true, width: 200 },
                                { field: "AskLineDesc", title: "Ask Line", width: 100 },
                                { field: "Message", title: "Message", width: 300 },
                                { field: "Email", title: "Email", width: 250 },
                                { field: "Solution", title: "Solution", width: 300 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "StatusMessageDesc", title: "Status Message", width: 200 },
                                { field: "InternalComment", title: "Internal Comment", width: 300 },
                                { field: "IsNeedToReport", title: "Is Need To Report", width: 150, template: "#= IsNeedToReport ? 'Yes' : 'No' #" },
                                { field: "DoneUsersID", title: "Done ID", width: 200 },
                                { field: "DoneTime", title: "DoneTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UnDoneUsersID", title: "UnDone ID", width: 200 },
                                { field: "UnDoneTime", title: "UnDone Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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
                    if (tabindex == 2) {

                        var CustomerServiceBookHistoryURL = window.location.origin + "/Radsoft/CustomerServiceBook/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(CustomerServiceBookHistoryURL);

                        $("#gridCustomerServiceBookHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CustomerServiceBook"
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
                                { field: "CustomerServiceBookPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ClientTypeDesc", title: "Client Type", width: 150 },
                                { field: "ClientName", title: "Client Name", width: 250 },
                                { field: "FundClientPK", title: "FuncdClient", hidden: true, width: 200 },
                                { field: "AskLineDesc", title: "Ask Line", width: 100 },
                                { field: "Message", title: "Message", width: 300 },
                                { field: "Email", title: "Email", width: 250 },
                                { field: "Solution", title: "Solution", width: 300 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "StatusMessageDesc", title: "Status Message", width: 200 },
                                { field: "InternalComment", title: "Internal Comment", width: 300 },
                                { field: "IsNeedToReport", title: "Is Need To Report", width: 150, template: "#= IsNeedToReport ? 'Yes' : 'No' #" },
                                { field: "DoneUsersID", title: "Done ID", width: 200 },
                                { field: "DoneTime", title: "DoneTime", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UnDoneUsersID", title: "UnDone ID", width: 200 },
                                { field: "UnDoneTime", title: "UnDone Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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
                } else {
                    refresh();
                }
            }
        });
    }


    function gridHistoryDataBound() {
        var grid = $("#gridCustomerServiceBookHistory").data("kendoGrid");
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

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var CustomerServiceBook = {
                        ClientType: $('#ClientType').val(),
                        FundClientPK: $('#FundClientPK').val(),
                        AskLine: $('#AskLine').val(),
                        Message: $('#Message').val(),
                        Email: $('#Email').val(),
                        ClientName: $('#ClientName').val(),
                        Solution: $('#Solution').val(),
                        Phone: $('#Phone').val(),
                        StatusMessage: $('#StatusMessage').val(),
                        InternalComment: $('#InternalComment').val(),
                        IsNeedToReport: $('#IsNeedToReport').is(":checked"),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CustomerServiceBook/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustomerServiceBook_I",
                        type: 'POST',
                        data: JSON.stringify(CustomerServiceBook),
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
                    var CustomerServiceBook = {
                        CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        ClientType: $('#ClientType').val(),
                        FundClientPK: $('#FundClientPK').val(),
                        AskLine: $('#AskLine').val(),
                        Message: $('#Message').val(),
                        Email: $('#Email').val(),
                        ClientName: $('#ClientName').val(),
                        Solution: $('#Solution').val(),
                        Phone: $('#Phone').val(),
                        StatusMessage: $('#StatusMessage').val(),
                        InternalComment: $('#InternalComment').val(),
                        IsNeedToReport: $('#IsNeedToReport').is(":checked"),
                        Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CustomerServiceBook/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustomerServiceBook_U",
                        type: 'POST',
                        data: JSON.stringify(CustomerServiceBook),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CustomerServiceBookPK").val() + "/" + $("#HistoryPK").val() + "/" + "CustomerServiceBook",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CustomerServiceBook" + "/" + $("#CustomerServiceBookPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CustomerServiceBookPK").val() + "/" + $("#HistoryPK").val() + "/" + "CustomerServiceBook",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var CustomerServiceBook = {
                                CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CustomerServiceBook/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustomerServiceBook_A",
                                type: 'POST',
                                data: JSON.stringify(CustomerServiceBook),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CustomerServiceBookPK").val() + "/" + $("#HistoryPK").val() + "/" + "CustomerServiceBook",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CustomerServiceBook = {
                                CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CustomerServiceBook/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustomerServiceBook_V",
                                type: 'POST',
                                data: JSON.stringify(CustomerServiceBook),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CustomerServiceBookPK").val() + "/" + $("#HistoryPK").val() + "/" + "CustomerServiceBook",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CustomerServiceBook = {
                                CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CustomerServiceBook/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CustomerServiceBook_R",
                                type: 'POST',
                                data: JSON.stringify(CustomerServiceBook),
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


    $("#BtnDone").click(function () {

        alertify.confirm("Are you sure This Problem is Done ?", function (e) {
            if (e) {

                var CustomerServiceBook = {
                    CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    DoneUsersID: sessionStorage.getItem("user"),
                    Param: "2"
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/CustomerServiceBook/CheckDone/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(CustomerServiceBook),
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


    $("#BtnUnDone").click(function () {

        alertify.confirm("Are you sure To UnDone this Problem ?", function (e) {
            if (e) {

                var CustomerServiceBook = {
                    CustomerServiceBookPK: $('#CustomerServiceBookPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    UnDoneUsersID: sessionStorage.getItem("user"),
                    Param: "3"
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/CustomerServiceBook/CheckDone/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(CustomerServiceBook),
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


    function Done(_statusmessage) {
        if (_statusmessage == "2") {
            $("#ClientType").attr('readonly', true);
            $("#FundClientPK").attr('readonly', true);
            $("#AskLine").attr('readonly', true);
            $("#Message").attr('readonly', true);
            $("#ClientName").attr('readonly', true);
            $("#Email").attr('readonly', true);
            $("#Solution").attr('readonly', true);
            $("#Phone").attr('readonly', true);
            $("#StatusMessage").attr('readonly', true);
            $("#InternalComment").attr('readonly', true);
        }
        else {
            $("#ClientType").attr('readonly', false);
            $("#FundClientPK").attr('readonly', false);
            $("#AskLine").attr('readonly', false);
            $("#Message").attr('readonly', false);
            $("#ClientName").attr('readonly', false);
            $("#Email").attr('readonly', false);
            $("#Solution").attr('readonly', false);
            $("#Phone").attr('readonly', false);
            $("#StatusMessage").attr('readonly', false);
            $("#InternalComment").attr('readonly', false);
        }

    };

    function GetIdentity(_fundclientPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetIdentity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + _fundclientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (GetCustomerHistory) {
                $('#Email').val(GetCustomerHistory.Email)
                $('#Phone').val(GetCustomerHistory.Phone)
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }


});
