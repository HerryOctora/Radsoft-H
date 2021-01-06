$(document).ready(function () {
    document.title = 'DIRECT INVESTMENT';
    //Global Variabel
    var WinDirectInvestment;
        var winOldData;
        var WinEquityDirectInvestment;
        var WinDividenDirectInvestment;
        var upOradd;
        var _d = new Date();
        var _fy = _d.getFullYear();
        var tabindex;
        var gridHeight = screen.height - 300;
        var GlobStatus;
        var _defaultPeriodPK;
        var GlobDecimalPlaces;

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

            $("#BtnAddEquityDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnAdd.png"
            });

            $("#BtnSaveEquityDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnSave.png"
            });
            $("#BtnCloseEquityDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnClose.png"
            });

            $("#BtnRefreshEquityDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnRefresh.png"
            });

            $("#BtnAddDividenDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnAdd.png"
            });

            $("#BtnSaveDividenDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnSave.png"
            });
            $("#BtnCloseDividenDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnClose.png"
            });

            $("#BtnRefreshDividenDirectInvestment").kendoButton({
                imageUrl: "../../Images/Icon/IcBtnRefresh.png"
            });
        }

        function initWindow() {
            //$("#Date").kendoDatePicker({
            //    format: "dd/MMM/yyyy",
            //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //    change: OnChangeDate,
            //});

            $("#BookingDate").kendoDatePicker({
                format: "MM/dd/yyyy",
                change: OnChangeDate,
            });

            WinDirectInvestment = $("#WinDirectInvestment").kendoWindow({
                height: 700,
                title: "Direct Investment Header",
                visible: false,
                width: 1200,
                modal: true,
                open: function (e) {
                    this.wrapper.css({ top: 80 })
                },
                close: onPopUpClose
            }).data("kendoWindow");

            //window Detail
            WinEquityDirectInvestment = $("#WinEquityDirectInvestment").kendoWindow({ 
                height: 450,
                title: "* EQUITY DIRECT INVESTMENT",
                visible: false,
                width: 1280,
                modal: false,
                open: function (e) {
                    this.wrapper.css({ top: 80 })
                },
                close: onWinEquityDirectInvestmentClose
            }).data("kendoWindow");

            //window Detail
            WinDividenDirectInvestment = $("#WinDividenDirectInvestment").kendoWindow({
                height: 450,
                title: "* Dividen DIRECT INVESTMENT",
                visible: false,
                width: 1280,
                modal: false,
                open: function (e) {
                    this.wrapper.css({ top: 80 })
                },
                close: onWinDividenDirectInvestmentClose
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

            function OnChangeDate() { 
                var _date = Date.parse($("#BookingDate").data("kendoDatePicker").value());

                //Check if Date parse is successful
                if (!_date) {

                    alertify.alert("Wrong Format Date DD/MMM/YYYY");
                }
                //else {
                //    $.ajax({
                //        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                //        type: 'GET',
                //        contentType: "application/json;charset=utf-8",
                //        success: function (data) {
                //            if (data == true) {

                //                alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                //            }
                //        },
                //        error: function (data) {
                //            alertify.alert(data.responseText);
                //        }
                //    });
                //}

            }

            $("#DateOfDeclaration").kendoDatePicker({
                format: "MM/dd/yyyy",
                change: OnChangeDateOfDeclaration,
            });


            function OnChangeDateOfDeclaration() {
                var _date = Date.parse($("#DateOfDeclaration").data("kendoDatePicker").value());

                //Check if Date parse is successful
                if (!_date) {
                    alertify.alert("Wrong Format Date MM/DD/YYYY");
                }
            }

            $("#DateOfBooking").kendoDatePicker({
                format: "MM/dd/yyyy",
                change: OnChangeDateOfBooking,
            });



            function OnChangeDateOfBooking() {
                var _date = Date.parse($("#DateOfBooking").data("kendoDatePicker").value());

                //Check if Date parse is successful
                if (!_date) {
                    alertify.alert("Wrong Format Date MM/DD/YYYY");
                }
            }



        }

        //var GlobValidator = $("#WinDirectInvestment").kendoValidator().data("kendoValidator");

        function validateData() {


            if (GlobValidator.validate()) {

                //if ($("#ID").val().length > 50) {
                //    alertify.alert("Validation not Pass, char more than 50 for ID");
                //    return 0;
                //}

                //if ($("#Name").val().length > 100) {
                //    alertify.alert("Validation not Pass, char more than 100 for Name");
                //    return 0;
                //}

                if ($("#Notes").val().length > 1000) {
                    alertify.alert("Validation not Pass, char more than 1000 for Notes");
                    return 0;
                }

                return 1;
            }
            else {
                alertify.alert("Validation not Pass");
                return 0;
            }
        }

        function showDetails(e) {
            if (e == null) {
                var dataItemX;
                $("#BtnUpdate").hide();
                $("#BtnVoid").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
                $("#StatusHeader").val("");
            } else {
                if (e.handled == true) {
                    return;
                }
                e.handled = true;

                if (tabindex == 0 || tabindex == undefined) {
                    var grid = $("#gridDirectInvestmentApproved").data("kendoGrid");
                    GlobStatus = 2;
                }
                if (tabindex == 1) {
                    var grid = $("#gridDirectInvestmentPending").data("kendoGrid");
                    GlobStatus = 1;
                }
                if (tabindex == 2) {
                    var grid = $("#gridDirectInvestmentHistory").data("kendoGrid");
                    GlobStatus = 3;
                }
                var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
                if (dataItemX.Status == 1) {

                    $("#StatusHeader").val("PENDING");
                    $("#BtnVoid").hide();
                    $("#BtnAdd").hide();
                    $("#BtnUpdate").show();
                    //$("#ID").attr('readonly', true);
                }
                if (dataItemX.Status == 2) {
                    $("#StatusHeader").val("APPROVED");
                    $("#BtnApproved").hide();
                    $("#BtnReject").hide();
                    $("#BtnAdd").hide();
                    $("#BtnOldData").hide();
                    //$("#ID").attr('readonly', true);
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

                $("#DirectInvestmentPK").val(dataItemX.DirectInvestmentPK);
                $("#HistoryPK").val(dataItemX.HistoryPK);
                $("#Notes").val(dataItemX.Notes);

                //$("#Date").val(dataItemX.Date);
                $("#ProjectName").val(dataItemX.ProjectName);
                $("#ClientName").val(dataItemX.ClientName);
                $("#AcqValue").val(dataItemX.AcqValue);
                $("#AcqSharePercentage").val(dataItemX.AcqSharePercentage);
                $("#StatusProject").val(dataItemX.StatusProject);
                $("#Remarks").val(dataItemX.Remarks);

                $("#EntryUsersID").val(dataItemX.EntryUsersID);
                $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
                $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
                $("#VoidUsersID").val(dataItemX.VoidUsersID);
                $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
                $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
                $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
                $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
                $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

                initGridEquityDirectInvestment();
                initGridDividenDirectInvestment();
            }

            $("#AcqValue").kendoNumericTextBox({
                format: "n4",
                decimals: 4,
                value: setAcqValue()
            });
            function setAcqValue() {
                if (e == null) {
                    return 0;
                } else {
                    return dataItemX.AcqValue;
                }
            }

            $("#AcqSharePercentage").kendoNumericTextBox({
                format: "###.#### \\%",
                decimals: 4,
                value: setAcqSharePercentage()
            });
            function setAcqSharePercentage() {
                if (e == null) {
                    return 0;
                } else {
                    return dataItemX.AcqSharePercentage;
                }
            }

            //$("#Percentage").kendoNumericTextBox({
            //    format: "##.#### \\%",
            //    decimals: 0,
            //    value: setPercentage()
            //});
            //function setPercentage() {
            //    if (e == null) {
            //        return 0;
            //    } else {
            //        return dataItemX.Percentage;
            //    }
            //}

            
            WinDirectInvestment.center();
            WinDirectInvestment.open();
        }

        function onPopUpClose() {
            clearData()
            showButton();
            $("#gridEquityDirectInvestment").empty();
            $("#gridDividenDirectInvestment").empty();
            refresh();
        }

    

        function clearData() {
            $("#DirectInvestmentPK").val("");
            $("#HistoryPK").val("");
            $("#Notes").val("");

            $("#ProjectName").val("");
            $("#ClientName").val("");
            $("#AcqValue").val("");
            $("#AcqSharePercentage").val("");
            $("#StatusProject").val("");
            $("#Remarks").val("");

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
                                DirectInvestmentPK: { type: "number" },
                                HistoryPK: { type: "number" },
                                Status: { type: "number" },
                                StatusDesc: { type: "string" },
                                Notes: { type: "string" },

                                ProjectName: { type: "string" },
                                ClientName: { type: "string" },
                                AcqValue: { type: "number" },
                                AcqSharePercentage: { type: "number" },
                                StatusProject: { type: "number" },
                                Remarks: { type: "string" },

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
                var gridApproved = $("#gridDirectInvestmentApproved").data("kendoGrid");
                gridApproved.dataSource.read();
            }
            if (tabindex == 1) {
                var gridPending = $("#gridDirectInvestmentPending").data("kendoGrid");
                gridPending.dataSource.read();
            }
            if (tabindex == 2) {
                var gridHistory = $("#gridDirectInvestmentHistory").data("kendoGrid");
                gridHistory.dataSource.read();
            }
        }
        function gridHistoryDataBound() {
            var grid = $("#gridDirectInvestmentHistory").data("kendoGrid");
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
        function initGrid() {
            var DirectInvestmentApprovedURL = window.location.origin + "/Radsoft/DirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
                dataSourceApproved = getDataSource(DirectInvestmentApprovedURL);

            $("#gridDirectInvestmentApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DirectInvestment"
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
                    { field: "DirectInvestmentPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                    //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "ProjectName", title: "ProjectName", width: 200 },
                    { field: "ClientName", title: "ClientName", width: 200 },
                    { field: "AcqValue", title: "AcqValue", width: 200 },
                    { field: "AcqSharePercentage", title: "AcqSharePercentage", width: 300 },
                    { field: "StatusProject", title: "StatusProject", width: 200 },
                    { field: "Remarks", title: "Remarks", width: 300 },

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
            $("#TabDirectInvestment").kendoTabStrip({
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
                            var DirectInvestmentPendingURL = window.location.origin + "/Radsoft/DirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                                dataSourcePending = getDataSource(DirectInvestmentPendingURL);
                            $("#gridDirectInvestmentPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form DirectInvestment"
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
                                    { field: "DirectInvestmentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                    //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "ProjectName", title: "ProjectName", width: 200 },
                                    { field: "ClientName", title: "ClientName", width: 200 },
                                    { field: "AcqValue", title: "AcqValue", width: 200 },
                                    { field: "AcqSharePercentage", title: "AcqSharePercentage", width: 300 },
                                    { field: "StatusProject", title: "StatusProject", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 300 },

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

                            var DirectInvestmentHistoryURL = window.location.origin + "/Radsoft/DirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                                dataSourceHistory = getDataSource(DirectInvestmentHistoryURL);

                            $("#gridDirectInvestmentHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form DirectInvestment"
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
                                    { field: "DirectInvestmentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                    //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "ProjectName", title: "ProjectName", width: 200 },
                                    { field: "ClientName", title: "ClientName", width: 200 },
                                    { field: "AcqValue", title: "AcqValue", width: 200 },
                                    { field: "AcqSharePercentage", title: "AcqSharePercentage", width: 300 },
                                    { field: "StatusProject", title: "StatusProject", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 300 },

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

        var GlobValidatorDirectInvestment = $("#WinDirectInvestment").kendoValidator().data("kendoValidator");
        function validateDataDirectInvestment() {

            if (GlobValidatorDirectInvestment.validate()) {
                //alert("Validation sucess");
                return 1;
            }
            else {
                alertify.alert("Validation not Pass");
                return 0;
            }
        }

        var GlobValidatorEquityDirectInvestment = $("#WinEquityDirectInvestment").kendoValidator().data("kendoValidator");
        function validateDataEquityDirectInvestment() {

            if (GlobValidatorEquityDirectInvestment.validate()) {
                //alert("Validation sucess");
                return 1;
            }
            else {
                alertify.alert("Validation not Pass");
                return 0;
            }
        }

        var GlobValidatorDividenDirectInvestment = $("#WinDividenDirectInvestment").kendoValidator().data("kendoValidator");
        function validateDataDividenDirectInvestment() {

            if (GlobValidatorDividenDirectInvestment.validate()) {
                //alert("Validation sucess");
                return 1;
            }
            else {
                alertify.alert("Validation not Pass");
                return 0;
            }
        }

      
        function DeleteEquityDirectInvestment(e) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;


            var dataItemX;
            var grid = $("#gridEquityDirectInvestment").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $.ajax({
                url: window.location.origin + "/Radsoft/EquityDirectInvestment/CheckEquityDirectInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DirectInvestmentPK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to DELETE Equity Direct Investment ?", function (e) {
                            if (e) {

                                var EquityDirectInvestment = {
                                    DirectInvestmentPK: dataItemX.DirectInvestmentPK,
                                    LastUsersID: sessionStorage.getItem("user"),
                                    EquityDirectInvestmentPK: dataItemX.EquityDirectInvestmentPK
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EquityDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EquityDirectInvestment_D",
                                    type: 'POST',
                                    data: JSON.stringify(EquityDirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refreshEquityDirectInvestmentGrid();
                                        alertify.alert(data);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else {
                        alertify.alert("Direct Investment Has Posted");
                    }
                }
            });
        }
        function refreshEquityDirectInvestmentGrid() {
            var gridEquityDirectInvestment = $("#gridEquityDirectInvestment").data("kendoGrid");
            gridEquityDirectInvestment.dataSource.read();
        }
        function getDataSourceEquityDirectInvestment(_url) {
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
                     pageSize: 500,                    
                     schema: {
                         model: {
                             fields: {
                                 DirectInvestment: { type: "number" },
                                 HistoryPK: { type: "number" },
                                 Status: { type: "number" },
                                 AutoNo: { type: "number" },                                 
                                 Profit: { type: "number" },
                                 TotalAmount: { type: "number" },
                                 BookingDate: { type: "date" },
                                 LastUsersID: { type: "string" },
                                 LastUpdate: { type: "date" }
                             }
                         }
                     }
                 });
        }
        function showEquityDirectInvestment(e) {
            var dataItemX;
            if (e == null) {
                upOradd = 0;
                $("#EquityDirectInvestmentPK").val("");
            } else {
                if (e.handled == true) // This will prevent event triggering more then once
                {
                    return;
                }
                e.handled = true;

                upOradd = 1;
                var grid = $("#gridEquityDirectInvestment").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                $("#EquityDirectInvestmentPK").val(dataItemX.AutoNo);
                $("#Profit").val(dataItemX.Profit);
                $("#TotalAmount").val(dataItemX.TotalAmount);
                $("#BookingDate").val(kendo.toString(kendo.parseDate(dataItemX.BookingDate), 'MM/dd/yyyy '));
                $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
            }



            $("#Profit").kendoNumericTextBox({
                format: "###.#### \\%",
                decimals: 4,
                value: setProfit()

            });
            function setProfit() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.Profit;
                }
            }

            $("#TotalAmount").kendoNumericTextBox({
                format: "n6",
                decimals: 6,
                value: setTotalAmount()
            });
            function setTotalAmount() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.TotalAmount;
                }
            }



            //function OnChangeDate() {
            //    var _date = Date.parse($("#DBookingDate").data("kendoDatePicker").value());

            //    //Check if Date parse is successful
            //    if (!_date) {
            //        alertify.alert("Wrong Format Date MM/DD/YYYY");
            //    }
            //}

            WinEquityDirectInvestment.center();
            WinEquityDirectInvestment.open();

        }
        function initGridEquityDirectInvestment() {
            var EquityDirectInvestmentURL = window.location.origin + "/Radsoft/EquityDirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#DirectInvestmentPK").val(),
              dataSourceEquityDirectInvestment = getDataSourceEquityDirectInvestment(EquityDirectInvestmentURL);

            $("#gridEquityDirectInvestment").kendoGrid({
                dataSource: dataSourceEquityDirectInvestment,
                height: 600,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Equity Direct Investment"
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
                   {
                       command: { text: "show", click: showEquityDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       command: { text: "Delete", click: DeleteEquityDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       field: "EquityDirectInvestmentPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
                   },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 }, 
                   { field: "Profit", title: "Profit", width: 200 },
                   { field: "TotalAmount", title: "Total Amount", width: 200 },
                   { field: "BookingDate", title: "Booking Date", format: "{0:dd/MMM/yyyy }", width: 200 },
                   { field: "LastUsersID", title: "LastUsersID", width: 180 },
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            });

        }
        function EquityDirectInvestmentGrid(e) {
            var EquityDirectInvestmentURL = window.location.origin + "/Radsoft/EquityDirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.DirectInvestmentPK,
             dataSourceEquityDirectInvestment = getDataSourceEquityDirectInvestment(EquityDirectInvestmentURL);

            $("<div/>").appendTo(e.detailCell).kendoGrid({
                dataSource: dataSourceEquityDirectInvestment,
                height: 250,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Equity Direct Investment"
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
                   {
                       command: { text: "show", click: showEquityDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       command: { text: "Delete", click: DeleteEquityDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       field: "EquityDirectInvestmentPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
                   },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "Profit", title: "Profit", width: 200 },
                   { field: "TotalAmount", title: "Total Amount", width: 200 },
                   { field: "BookingDate", title: "Booking Date", width: 200 },
                   { field: "LastUsersID", title: "LastUsersID", width: 180 },
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            });
        }
        function onWinEquityDirectInvestmentClose() {
            GlobValidatorEquityDirectInvestment.hideMessages();
            $("#Profit").val("");
            $("#TotalAmount").val("");
            $("#BookingDate").val("");
            $("#LastUsersID").val("");
            $("#LastUpdate").val("");
        }
        
        function DeleteDividenDirectInvestment(e) {
            if (e.handled == true) {
                return;
            }
            e.handled = true;


            var dataItemX;
            var grid = $("#gridDividenDirectInvestment").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $.ajax({
                url: window.location.origin + "/Radsoft/DividenDirectInvestment/CheckDividenDirectInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DirectInvestmentPK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to DELETE Dividen Direct Investment ?", function (e) {
                            if (e) {

                                var DividenDirectInvestment = {
                                    DirectInvestmentPK: dataItemX.DirectInvestmentPK,
                                    LastUsersID: sessionStorage.getItem("user"),
                                    DividenDirectInvestmentPK: dataItemX.DividenDirectInvestmentPK
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DividenDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DividenDirectInvestment_D",
                                    type: 'POST',
                                    data: JSON.stringify(DividenDirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refreshDividenDirectInvestmentGrid();
                                        alertify.alert(data);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else {
                        alertify.alert("Direct Investment Has Posted");
                    }
                }
            });
        }
        function refreshDividenDirectInvestmentGrid() {
            var gridDividenDirectInvestment = $("#gridDividenDirectInvestment").data("kendoGrid");
            gridDividenDirectInvestment.dataSource.read();
        }
        function getDataSourceDividenDirectInvestment(_url) {
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
                     pageSize: 500,
                     schema: {
                         model: {
                             fields: {
                                 DirectInvestment: { type: "number" },
                                 HistoryPK: { type: "number" },
                                 Status: { type: "number" },
                                 AutoNo: { type: "number" },
                                 DividenRatio: { type: "number" },
                                 Profit: { type: "number" },
                                 Dividen: { type: "number" },
                                 DateOfDeclaration: { type: "date" },
                                 DateOfBooking: { type: "date" },
                                 LastUsersID: { type: "string" },
                                 LastUpdate: { type: "date" }
                             }
                         }
                     }
                 });
        }
        function showDividenDirectInvestment(e) {
            var dataItemX;
            if (e == null) {
                upOradd = 0;
                $("#DividenDirectInvestmentPK").val("");
            } else {
                if (e.handled == true) // This will prevent event triggering more then once
                {
                    return;
                }
                e.handled = true;

                upOradd = 1;
                var grid = $("#gridDividenDirectInvestment").data("kendoGrid");
                dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                $("#DividenDirectInvestmentPK").val(dataItemX.AutoNo);
                $("#DividenRatio").val(dataItemX.DividenRatio);
                $("#DividenProfit").val(dataItemX.Profit);
                $("#Dividen").val(dataItemX.Dividen);
                $("#DateOfDeclaration").val(kendo.toString(kendo.parseDate(dataItemX.DateOfDeclaration), 'MM/dd/yyyy '));
                $("#DateOfBooking").val(kendo.toString(kendo.parseDate(dataItemX.DateOfBooking), 'MM/dd/yyyy '));
                
                $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
            }



            $("#DividenProfit").kendoNumericTextBox({
                format: "###.#### \\%",
                decimals: 4,
                value: setProfit()

            });
            function setProfit() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.Profit;
                }
            }

            $("#Dividen").kendoNumericTextBox({
                format: "###.#### \\%",
                decimals: 4,
                value: setDividen()

            });
            function setDividen() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.Dividen;
                }
            }

            $("#DividenRatio").kendoNumericTextBox({
                format: "n8",
                decimals: 8,
                value: setDividenRatio()
            });
            function setDividenRatio() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.DividenRatio;
                }
            }

            $("#DividenRatio").kendoNumericTextBox({
                format: "n8",
                decimals: 8,
                value: setDividenRatio()
            });
            function setDividenRatio() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.DividenRatio;
                }
            }

           

            //function OnChangeDate() {
            //    var _date = Date.parse($("#DBookingDate").data("kendoDatePicker").value());

            //    //Check if Date parse is successful
            //    if (!_date) {
            //        alertify.alert("Wrong Format Date MM/DD/YYYY");
            //    }
            //}

            WinDividenDirectInvestment.center();
            WinDividenDirectInvestment.open();

        }
        function initGridDividenDirectInvestment() {
            var DividenDirectInvestmentURL = window.location.origin + "/Radsoft/DividenDirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#DirectInvestmentPK").val(),
              dataSourceDividenDirectInvestment = getDataSourceDividenDirectInvestment(DividenDirectInvestmentURL);

            $("#gridDividenDirectInvestment").kendoGrid({
                dataSource: dataSourceDividenDirectInvestment,
                height: 600,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Equity Direct Investment"
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
                   {
                       command: { text: "show", click: showDividenDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       command: { text: "Delete", click: DeleteDividenDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       field: "DividenDirectInvestmentPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
                   },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                   { field: "DividenRatio", title: "Profit", width: 200 },
                   { field: "DividenProfit", title: "Profit", width: 200 },
                   { field: "Dividen", title: "Dividen", width: 200 },
                   { field: "DateOfDeclaration", title: "Date Of Declaration", format: "{0:dd/MMM/yyyy}", width: 200 },
                   { field: "DateOfBooking", title: "Date Of Booking", format: "{0:dd/MMM/yyyy}", width: 200 },
                   { field: "LastUsersID", title: "LastUsersID", width: 180 },
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            });

        }
        function DividenDirectInvestmentGrid(e) {
            var DividenDirectInvestmentURL = window.location.origin + "/Radsoft/DividenDirectInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.DirectInvestmentPK,
             dataSourceDividenDirectInvestment = getDataSourceDividenDirectInvestment(DividenDirectInvestmentURL);

            $("<div/>").appendTo(e.detailCell).kendoGrid({
                dataSource: dataSourceDividenDirectInvestment,
                height: 250,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Equity Direct Investment"
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
                   {
                       command: { text: "show", click: showDividenDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       command: { text: "Delete", click: DeleteDividenDirectInvestment }, title: " ", width: 80, locked: true, lockable: false
                   },
                   {
                       field: "DividenDirectInvestmentPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
                   },
                   { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                  { field: "DividenRatio", title: "Profit", width: 200 },
                   { field: "DividenProfit", title: "Profit", width: 200 },
                   { field: "Dividen", title: "Dividen", width: 200 },
                   { field: "DateOfDeclaration", title: "Date Of Declaration", width: 200 },
                    { field: "DateOfBooking", title: "Date Of Booking", width: 200 },
                   { field: "LastUsersID", title: "LastUsersID", width: 180 },
                   { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                ]
            });
        }
        function onWinDividenDirectInvestmentClose() {
            GlobValidatorDividenDirectInvestment.hideMessages();
            $("#DividenRatio").val("");
            $("#DividenProfit").val("");
            $("#Dividen").val("");
            $("#DateOfDeclaration").val("");
            $("#DateOfBooking").val("");
            $("#LastUsersID").val("");
            $("#LastUpdate").val("");
        }


        $("#BtnRefreshEquityDirectInvestment").click(function () {
            refreshEquityDirectInvestmentGrid();
        });

        $("#BtnCloseEquityDirectInvestment").click(function () {

            alertify.confirm("Are you sure want to Close and Clear Equity Direct Investment?", function (e) {
                if (e) {
                    WinEquityDirectInvestment.close();
                    alertify.alert("Close Equity Direct Investment");
                }
            });
        });

        $("#BtnRefreshDividenDirectInvestment").click(function () {
            refreshDividenDirectInvestmentGrid();
        });

        $("#BtnCloseDividenDirectInvestment").click(function () {

            alertify.confirm("Are you sure want to Close and Clear Equity Direct Investment?", function (e) {
                if (e) {
                    WinDividenDirectInvestment.close();
                    alertify.alert("Close Equity Direct Investment");
                }
            });
        });

        $("#BtnRefresh").click(function () {
            refresh();
        });

        $("#BtnCancel").click(function () {

            alertify.confirm("Are you sure want to close detail?", function (e) {
                if (e) {
                    WinDirectInvestment.close();
                    alertify.alert("Close Detail");
                }
            });
        });

        $("#BtnNew").click(function () {
            //$("#ID").attr('readonly', false);
            showDetails(null);
        });
    
        $("#BtnAdd").click(function () {
            //if ($("#DirectInvestmentPK").val() > 0) {
            //    alertify.alert("DIRECT INVESTMENT HEADER ALREADY EXIST, Cancel and click add new to add more Header");
            //    return
            //}

            var val = validateDataDirectInvestment();
            if (val == 1) {

                alertify.confirm("Are you sure want to Add DIRECT INVESTMENT HEADER ?", function (e) {
                    if (e) {
                        $.ajax({
                            //url: window.location.origin,
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                //$("#ProjectName").val(data);
                                //alertify.alert("Your new reference is " + data);
                                var DirectInvestment = {
                                    ProjectName: $('#ProjectName').val(),
                                    ClientName: $('#ClientName').val(),
                                    AcqValue: $('#AcqValue').val(),
                                    AcqSharePercentage: $('#AcqSharePercentage').val(),
                                    StatusProject: $('#StatusProject').val(),
                                    Remarks: $('#Remarks').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DirectInvestment_I",
                                    type: 'POST',
                                    data: JSON.stringify(DirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data.Message);
                                        $("#DirectInvestmentPK").val(data.DirectInvestmentPK);
                                        $("#HistoryPK").val(data.HistoryPK);
                                        refresh();
                                    },

                                    //error: function (data) {
                                    //    alertify.alert(data.responseText);
                                    //}
                                });

                            },
                            //error: function (data) {
                            //    alertify.alert(data.responseText);
                            //}

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
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DirectInvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "DirectInvestment",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                    var DirectInvestment = {
                                        DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        ProjectName: $('#ProjectName').val(),
                                        ClientName: $('#ClientName').val(),
                                        AcqValue: $('#AcqValue').val(),
                                        AcqSharePercentage: $('#AcqSharePercentage').val(),
                                        StatusProject: $('#StatusProject').val(),
                                        Remarks: $('#Remarks').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/DirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DirectInvestment_U",
                                        type: 'POST',
                                        data: JSON.stringify(DirectInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            WinDirectInvestment.close();
                                            refresh();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });

                                } else {
                                    alertify.alert("Data has been Changed by other user, Please check it first!");
                                    WinDirectInvestment.close();
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
                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DirectInvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "DirectInvestment",
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
                                        url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "DirectInvestment" + "/" + $("#DirectInvestmentPK").val(),
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
                        WinDirectInvestment.close();
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DirectInvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "DirectInvestment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var DirectInvestment = {
                                    DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DirectInvestment_A",
                                    type: 'POST',
                                    data: JSON.stringify(DirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        WinDirectInvestment.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                WinDirectInvestment.close();
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DirectInvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "DirectInvestment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var DirectInvestment = {
                                    DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    VoidUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DirectInvestment_V",
                                    type: 'POST',
                                    data: JSON.stringify(DirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        WinDirectInvestment.close();
                                        refresh();

                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                WinDirectInvestment.close();
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DirectInvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "DirectInvestment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var DirectInvestment = {
                                    DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    VoidUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DirectInvestment_R",
                                    type: 'POST',
                                    data: JSON.stringify(DirectInvestment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        WinDirectInvestment.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                WinDirectInvestment.close();
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


    // Bagian Detail 
        $("#BtnAddEquityDirectInvestment").click(function () {

            if ($("#DirectInvestmentPK").val() == 0 || $("#DirectInvestmentPK").val() == null) {
                alertify.alert("There's no Direct Investment Header");
            } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
                alertify.alert("Direct Investment Already History");
            } else {
                showEquityDirectInvestment();
            }
        });
    
        $("#BtnSaveEquityDirectInvestment").click(function () {
            var val = validateDataEquityDirectInvestment();
            if (val == 1) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/EquityDirectInvestment/CheckEquityDirectInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DirectInvestmentPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            alertify.confirm("Are you sure want to Add Equity Direct Investment ?", function (e) {
                                if (e) {

                                    var EquityDirectInvestment = {
                                        DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                        EquityDirectInvestmentPK: $('#EquityDirectInvestmentPK').val(),
                                        Status: 2,
                                        Profit: $('#Profit').val(),
                                        TotalAmount: $('#TotalAmount').val(),
                                        BookingDate: $('#BookingDate').val(),
                                        LastUsersID: sessionStorage.getItem("user")

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/EquityDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EquityDirectInvestment_I",
                                        type: 'POST',
                                        data: JSON.stringify(EquityDirectInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridEquityDirectInvestment").empty();
                                            initGridEquityDirectInvestment();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        }
                        else {
                            alertify.confirm("Are you sure want to UPDATE Equity Direct Investment ?", function (e) {
                                if (e) {

                                    var EquityDirectInvestment = {
                                        DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                        EquityDirectInvestmentPK: $('#EquityDirectInvestmentPK').val(),
                                        Status: 2,
                                        Profit: $('#Profit').val(),
                                        TotalAmount: $('#TotalAmount').val(),
                                        BookingDate: $('#BookingDate').val(),
                                        LastUsersID: sessionStorage.getItem("user")
                                                       
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/EquityDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EquityDirectInvestment_U",
                                        type: 'POST',
                                        data: JSON.stringify(EquityDirectInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridEquityDirectInvestment").empty();
                                            initGridEquityDirectInvestment();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        }
                    }
                });

            }
        });

        $("#BtnAddDividenDirectInvestment").click(function () {

            if ($("#DirectInvestmentPK").val() == 0 || $("#DirectInvestmentPK").val() == null) {
                alertify.alert("There's no Direct Investment Header");
            } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
                alertify.alert("Direct Investment Already History");
            } else {
                showDividenDirectInvestment();
            }
        });

        $("#BtnSaveDividenDirectInvestment").click(function () {
            var val = validateDataDividenDirectInvestment();
            if (val == 1) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/DividenDirectInvestment/CheckDividenDirectInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#DirectInvestmentPK').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            alertify.confirm("Are you sure want to Add Dividen Direct Investment ?", function (e) {
                                if (e) {

                                    var DividenDirectInvestment = {
                                        DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                        DividenDirectInvestmentPK: $('#DividenDirectInvestmentPK').val(),
                                        Status: 2,
                                        DividenRatio: $('#DividenRatio').val(),
                                        Profit: $('#DividenProfit').val(),
                                        Dividen: $('#Dividen').val(),
                                        DateOfDeclaration: $('#DateOfDeclaration').val(),
                                        DateOfBooking: $('#DateOfBooking').val(),
                                        LastUsersID: sessionStorage.getItem("user")

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/DividenDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DividenDirectInvestment_I",
                                        type: 'POST',
                                        data: JSON.stringify(DividenDirectInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridDividenDirectInvestment").empty();
                                            initGridDividenDirectInvestment();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        }
                        else {
                            alertify.confirm("Are you sure want to UPDATE Dividen Direct Investment ?", function (e) {
                                if (e) {

                                    var DividenDirectInvestment = {
                                        DirectInvestmentPK: $('#DirectInvestmentPK').val(),
                                        DividenDirectInvestmentPK: $('#DividenDirectInvestmentPK').val(),
                                        Status: 2,
                                        DividenRatio: $('#DividenRatio').val(),
                                        Profit: $('#Profit').val(),
                                        Dividen: $('#Dividen').val(),
                                        DateOfDeclaration: $('#DateOfDeclaration').val(),
                                        DateOfBooking: $('#DateOfBooking').val(),
                                        LastUsersID: sessionStorage.getItem("user")

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/DividenDirectInvestment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DividenDirectInvestment_U",
                                        type: 'POST',
                                        data: JSON.stringify(DividenDirectInvestment),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridDividenDirectInvestment").empty();
                                            initGridDividenDirectInvestment();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        }
                    }
                });

            }
        });

       

        







});