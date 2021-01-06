$(document).ready(function () {
    document.title = 'ADVISORY FEE';
    //Global Variabel
    var WinAdvisoryFee;
    var winOldData;
    var WinAdvisoryFeeClient;
    var WinAdvisoryFeeTOP;
    var WinAdvisoryFeeExpense;
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

        $("#BtnAddAdvisoryFeeClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnSaveAdvisoryFeeClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCloseAdvisoryFeeClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnRefreshAdvisoryFeeClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnAddAdvisoryFeeTOP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnSaveAdvisoryFeeTOP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCloseAdvisoryFeeTOP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnRefreshAdvisoryFeeTOP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnAddAdvisoryFeeExpense").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnSaveAdvisoryFeeExpense").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCloseAdvisoryFeeExpense").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnRefreshAdvisoryFeeExpense").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
    }

    function initWindow() {
        $("#StartDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeStartDate,
        });

        $("#EndDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeEndDate,
        });

         $("#Status1ProjectDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeProjectDate1,
         });

         $("#Status2ProjectDate").kendoDatePicker({
             format: "dd/MMM/yyyy",
             parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
             change: OnChangeProjectDate2,
         });

         $("#Status3ProjectDate").kendoDatePicker({
             format: "dd/MMM/yyyy",
             parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
             change: OnChangeProjectDate3,
         });

         $("#Status4ProjectDate").kendoDatePicker({
             format: "dd/MMM/yyyy",
             parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
             change: OnChangeProjectDate4,
         });

         $("#Status5ProjectDate").kendoDatePicker({
             format: "dd/MMM/yyyy",
             parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
             change: OnChangeProjectDate5,
         });



        //$("#DBookingDate").kendoDatePicker({
        //    format: "MM/dd/yyyy",
        //    change: OnChangeDate,
        //});

        WinAdvisoryFee = $("#WinAdvisoryFee").kendoWindow({
            height: 700,
            title: "Advisory Fee Header",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Detail
        WinAdvisoryFeeClient = $("#WinAdvisoryFeeClient").kendoWindow({
            height: 450,
            title: "* ADVISORY FEE CLIENT",
            visible: false,
            width: 1280,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAdvisoryFeeClientClose
        }).data("kendoWindow");

        //window Detail
        WinAdvisoryFeeTOP = $("#WinAdvisoryFeeTOP").kendoWindow({
            height: 450,
            title: "* ADVISORY FEE TOP",
            visible: false,
            width: 1280,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAdvisoryFeeTOPClose
        }).data("kendoWindow");

        //window Detail
        WinAdvisoryFeeExpense = $("#WinAdvisoryFeeExpense").kendoWindow({
            height: 450,
            title: "* ADVISORY FEE Expense",
            visible: false,
            width: 1280,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAdvisoryFeeExpenseClose
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

        function OnChangeStartDate() {
            var _date = Date.parse($("#StartDate").data("kendoDatePicker").value());

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
        function OnChangeEndDate() {
            var _date = Date.parse($("#EndDate").data("kendoDatePicker").value());

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
        function OnChangeProjectDate1() {
            var _date = Date.parse($("#Status1ProjectDate").data("kendoDatePicker").value());

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
        function OnChangeProjectDate2() {
            var _date = Date.parse($("#Status2ProjectDate").data("kendoDatePicker").value());

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
        function OnChangeProjectDate3() {
            var _date = Date.parse($("#Status3ProjectDate").data("kendoDatePicker").value());

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
        function OnChangeProjectDate4() {
            var _date = Date.parse($("#Status4ProjectDate").data("kendoDatePicker").value());

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
        function OnChangeProjectDate5() {
            var _date = Date.parse($("#Status5ProjectDate").data("kendoDatePicker").value());

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


    }

    //var GlobValidator = $("#WinAdvisoryFee").kendoValidator().data("kendoValidator");

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
                var grid = $("#gridAdvisoryFeeApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {
                var grid = $("#gridAdvisoryFeePending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {
                var grid = $("#gridAdvisoryFeeHistory").data("kendoGrid");
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

            $("#AdvisoryFeePK").val(dataItemX.AdvisoryFeePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);

            //$("#Date").val(dataItemX.Date);
            $("#ProjectName").val(dataItemX.ProjectName);
            $("#StartDate").val(kendo.toString(kendo.parseDate(dataItemX.StartDate), 'dd/MMM/yyyy'));
            $("#EndDate").val(kendo.toString(kendo.parseDate(dataItemX.EndDate), 'dd/MMM/yyyy'));
            $("#Remarks").val(dataItemX.Remarks);
            $("#ProjectValue").val(dataItemX.ProjectValue);
            $("#Status1Project").val(dataItemX.Status1Project);
            //ganti
            $("#Status1ProjectDate").val(kendo.toString(kendo.parseDate(dataItemX.Status1ProjectDate), 'dd/MMM/yyyy'));
            $("#Status2Project").val(dataItemX.Status2Project);
            $("#Status2ProjectDate").val(kendo.toString(kendo.parseDate(dataItemX.Status2ProjectDate), 'dd/MMM/yyyy'));
            $("#Status3Project").val(dataItemX.Status3Project);
            $("#Status3ProjectDate").val(kendo.toString(kendo.parseDate(dataItemX.Status3ProjectDate), 'dd/MMM/yyyy'));
            $("#Status4Project").val(dataItemX.Status4Project);
            $("#Status4ProjectDate").val(kendo.toString(kendo.parseDate(dataItemX.Status4ProjectDate), 'dd/MMM/yyyy'));
            $("#Status5Project").val(dataItemX.Status5Project);
            $("#Status5ProjectDate").val(kendo.toString(kendo.parseDate(dataItemX.Status5ProjectDate), 'dd/MMM/yyyy'));

            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            initGridAdvisoryFeeClient();
            initGridAdvisoryFeeTOP();
            initGridAdvisoryFeeExpense();
        }

        //$("#AcqValue").kendoNumericTextBox({
        //    format: "n4",
        //    decimals: 4,
        //    value: setAcqValue()
        //});
        //function setAcqValue() {
        //    if (e == null) {
        //        return 0;
        //    } else {
        //        return dataItemX.AcqValue;
        //    }
        //}

        //$("#AcqSharePercentage").kendoNumericTextBox({
        //    format: "n8",
        //    decimals: 8,
        //    value: setAcqSharePercentage()
        //});
        //function setAcqSharePercentage() {
        //    if (e == null) {
        //        return 0;
        //    } else {
        //        return dataItemX.AcqSharePercentage;
        //    }
        //}

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


        WinAdvisoryFee.center();
        WinAdvisoryFee.open();
    }

    function onPopUpClose() {
        clearData()
        showButton();
        $("#gridAdvisoryFeeClient").empty();
        $("#gridAdvisoryFeeTOP").empty();
        $("#gridAdvisoryFeeExpense").empty();
        refresh();
    }



    function clearData() {
        $("#AdvisoryFeePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");

        $("#ProjectName").val("");
        $("#StartDate").val("");
        $("#EndDate").val("");
        $("#Remarks").val("");
        $("#ProjectDate").val("");
        $("#Status1Project").val("");
        $("#Status1ProjectDate").val("");
        $("#Status2Project").val("");
        $("#Status2ProjectDate").val("");
        $("#Status3Project").val("");
        $("#Status3ProjectDate").val("");
        $("#Status4Project").val("");
        $("#Status4ProjectDate").val("");
        $("#Status5Project").val("");
        $("#Status5ProjectDate").val("");

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
                            AdvisoryFeePK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },

                            ProjectName: { type: "string" },
                            StartDate: { type: "date" },
                            EndDate: { type: "date" },
                            Remarks: { type: "string" },
                            ProjectValue: { type: "number" },
                            Status1Project: { type: "string" },
                            Status1ProjectDate: { type: "date" },
                            Status2Project: { type: "string" },
                            Status2ProjectDate: { type: "date" },
                            Status3Project: { type: "string" },
                            Status3ProjectDate: { type: "date" },
                            Status4Project: { type: "string" },
                            Status4ProjectDate: { type: "date" },
                            Status5Project: { type: "string" },
                            Status5ProjectDate: { type: "date" },

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
            var gridApproved = $("#gridAdvisoryFeeApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAdvisoryFeePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAdvisoryFeeHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }
    function gridHistoryDataBound() {
        var grid = $("#gridAdvisoryFeeHistory").data("kendoGrid");
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
        var AdvisoryFeeApprovedURL = window.location.origin + "/Radsoft/AdvisoryFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(AdvisoryFeeApprovedURL);

        $("#gridAdvisoryFeeApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form AdvisoryFee"
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
                { field: "AdvisoryFeePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                { field: "ProjectName", title: "Project Name", width: 200 },
                { field: "StartDate", title: "Start Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                { field: "EndDate", title: "End Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                { field: "Remarks", title: "Remarks", width: 300 },
                { field: "Status1Project", title: "Status Project 1", width: 200 },
                { field: "Status1ProjectDate", title: "Status Project 1 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                { field: "Status2Project", title: "Status Project 2", width: 200 },
                { field: "Status2ProjectDate", title: "Status Project 2 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                { field: "Status3Project", title: "Status Project 3", width: 200 },
                { field: "Status3ProjectDate", title: "Status Project 3 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                { field: "Status4Project", title: "Status Project 4", width: 200 },
                { field: "Status4ProjectDate", title: "Status Project 4 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                { field: "Status5Project", title: "Status Project 5", width: 200 },
                { field: "Status5ProjectDate", title: "Status Project 5 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
              
               

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
        $("#TabAdvisoryFee").kendoTabStrip({
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
                        var AdvisoryFeePendingURL = window.location.origin + "/Radsoft/AdvisoryFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(AdvisoryFeePendingURL);
                        $("#gridAdvisoryFeePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form AdvisoryFee"
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
                                { field: "AdvisoryFeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "ProjectName", title: "Project Name", width: 200 },
                            { field: "StartDate", title: "Start Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "EndDate", title: "End Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "Remarks", title: "Remarks", width: 300 },
                            { field: "Status1Project", title: "Status Project 1", width: 200 },
                            { field: "Status1ProjectDate", title: "Status Project 1 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status2Project", title: "Status Project 2", width: 200 },
                            { field: "Status2ProjectDate", title: "Status Project 2 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status3Project", title: "Status Project 3", width: 200 },
                            { field: "Status3ProjectDate", title: "Status Project 3 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status4Project", title: "Status Project 4", width: 200 },
                            { field: "Status4ProjectDate", title: "Status Project 4 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status5Project", title: "Status Project 5", width: 200 },
                            { field: "Status5ProjectDate", title: "Status Project 5 Date", format: "{0:dd/MMM/yyyy}", width: 300 },

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

                        var AdvisoryFeeHistoryURL = window.location.origin + "/Radsoft/AdvisoryFee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(AdvisoryFeeHistoryURL);

                        $("#gridAdvisoryFeeHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form AdvisoryFee"
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
                                { field: "AdvisoryFeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                //{ field: "Date", title: "Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "ProjectName", title: "Project Name", width: 200 },
                            { field: "StartDate", title: "Start Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "EndDate", title: "End Date", format: "{0:dd/MMM/yyyy}", width: 200 },
                            { field: "Remarks", title: "Remarks", width: 300 },
                            { field: "Status1Project", title: "Status Project 1", width: 200 },
                            { field: "Status1ProjectDate", title: "Status Project 1 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status2Project", title: "Status Project 2", width: 200 },
                            { field: "Status2ProjectDate", title: "Status Project 2 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status3Project", title: "Status Project 3", width: 200 },
                            { field: "Status3ProjectDate", title: "Status Project 3 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status4Project", title: "Status Project 4", width: 200 },
                            { field: "Status4ProjectDate", title: "Status Project 4 Date", format: "{0:dd/MMM/yyyy}", width: 300 },
                            { field: "Status5Project", title: "Status Project 5", width: 200 },
                            { field: "Status5ProjectDate", title: "Status Project 5 Date", format: "{0:dd/MMM/yyyy}", width: 300 },

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

    var GlobValidatorAdvisoryFee = $("#WinAdvisoryFee").kendoValidator().data("kendoValidator");
    function validateDataAdvisoryFee() {

        if (GlobValidatorAdvisoryFee.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    var GlobValidatorAdvisoryFeeClient = $("#WinAdvisoryFeeClient").kendoValidator().data("kendoValidator");
    function validateDataAdvisoryFeeClient() {

        if (GlobValidatorAdvisoryFeeClient.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    var GlobValidatorAdvisoryFeeTOP = $("#WinAdvisoryFeeTOP").kendoValidator().data("kendoValidator");
    function validateDataAdvisoryFeeTOP() {

        if (GlobValidatorAdvisoryFeeTOP.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    var GlobValidatorAdvisoryFeeExpense = $("#WinAdvisoryFeeExpense").kendoValidator().data("kendoValidator");
    function validateDataAdvisoryFeeExpense() {

        if (GlobValidatorAdvisoryFeeExpense.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }


    function DeleteAdvisoryFeeClient(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;


        var dataItemX;
        var grid = $("#gridAdvisoryFeeClient").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
            url: window.location.origin + "/Radsoft/AdvisoryFeeClient/CheckAdvisoryFeeClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    alertify.confirm("Are you sure want to DELETE Advisory Fee Client?", function (e) {
                        if (e) {

                            var AdvisoryFeeClient = {
                                AdvisoryFeePK: dataItemX.AdvisoryFeePK,
                                LastUsersID: sessionStorage.getItem("user"),
                                AdvisoryFeeClientPK: dataItemX.AdvisoryFeeClientPK
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFeeClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeClient_D",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFeeClient),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    refreshAdvisoryFeeClientGrid();
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
                    alertify.alert("Advisory Fee Has Posted");
                }
            }
        });
    }

    function refreshAdvisoryFeeClientGrid() {
        var gridAdvisoryFeeClient = $("#gridAdvisoryFeeClient").data("kendoGrid");
        gridAdvisoryFeeClient.dataSource.read();
    }
    function getDataSourceAdvisoryFeeClient(_url) {
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
                             AdvisoryFee: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             ClientName: { type: "string" },
                             ClientDescription: { type: "string" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showAdvisoryFeeClient(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#AdvisoryFeeClientPK").val("");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridAdvisoryFeeClient").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#AdvisoryFeeClientPK").val(dataItemX.AutoNo);
            $("#ClientName").val(dataItemX.ClientName);
            $("#ClientDescription").val(dataItemX.ClientDescription);
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }



        //$("#DProfit").kendoNumericTextBox({
        //    format: "###.#### \\%",
        //    decimals: 4,
        //    value: setDProfit()

        //});
        //function setDProfit() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.DProfit;
        //    }
        //}

        //$("#DTotalAmount").kendoNumericTextBox({
        //    format: "n6",
        //    decimals: 6,
        //    value: setDTotalAmount()
        //});
        //function setDTotalAmount() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.DTotalAmount;
        //    }
        //}



        //function OnChangeDate() {
        //    var _date = Date.parse($("#DBookingDate").data("kendoDatePicker").value());

        //    //Check if Date parse is successful
        //    if (!_date) {
        //        alertify.alert("Wrong Format Date MM/DD/YYYY");
        //    }
        //}

        WinAdvisoryFeeClient.center();
        WinAdvisoryFeeClient.open();

    }
    function initGridAdvisoryFeeClient() {
        var AdvisoryFeeClientURL = window.location.origin + "/Radsoft/AdvisoryFeeClient/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#AdvisoryFeePK").val(),
          dataSourceAdvisoryFeeClient = getDataSourceAdvisoryFeeClient(AdvisoryFeeClientURL);

        $("#gridAdvisoryFeeClient").kendoGrid({
            dataSource: dataSourceAdvisoryFeeClient,
            height: 300,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Advisory Fee Client"
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
                   command: { text: "show", click: showAdvisoryFeeClient }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeleteAdvisoryFeeClient }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AdvisoryFeeClientPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "ClientName", title: "Client Name", width: 200 },
               { field: "ClientDescription", title: "Client Description", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }

    function onWinAdvisoryFeeClientClose() {
        GlobValidatorAdvisoryFeeClient.hideMessages();
        $("#ClientName").val("");
        $("#ClientDescription").val("");
        $("#LastUsersID").val("");
        $("#LastUpdate").val("");
    }

    function DeleteAdvisoryFeeTOP(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;


        var dataItemX;
        var grid = $("#gridAdvisoryFeeTOP").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
            url: window.location.origin + "/Radsoft/AdvisoryFeeTOP/CheckAdvisoryFeeTOP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    alertify.confirm("Are you sure want to DELETE Advisory Fee TOP ?", function (e) {
                        if (e) {

                            var AdvisoryFeeTOP = {
                                AdvisoryFeePK: dataItemX.AdvisoryFeePK,
                                LastUsersID: sessionStorage.getItem("user"),
                                AdvisoryFeeTOPPK: dataItemX.AdvisoryFeeTOPPK
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFeeTOP/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeTOP_D",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFeeTOP),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    refreshAdvisoryFeeTOPGrid();
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
                    alertify.alert("Advisory Fee Has Posted");
                }
            }
        });
    }
    function refreshAdvisoryFeeTOPGrid() {
        var gridAdvisoryFeeTOP = $("#gridAdvisoryFeeTOP").data("kendoGrid");
        gridAdvisoryFeeTOP.dataSource.read();
    }
    function getDataSourceAdvisoryFeeTOP(_url) {
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
                             AdvisoryFee: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             TOPPercent: { type: "number" },
                             TOPDate: { type: "date" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showAdvisoryFeeTOP(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#AdvisoryFeeTOPPK").val("");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridAdvisoryFeeTOP").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#AdvisoryFeeTOPPK").val(dataItemX.AutoNo);
            $("#TOPPercent").val(dataItemX.TOPPercent);
            $("#TOPDate").val(kendo.toString(kendo.parseDate(dataItemX.TOPDate), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }



        $("#TOPPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setTOPPercent()

        });
        function setTOPPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TOPPercent;
            }
        }

        $("#TOPDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeTOPDate,
        });

        //$("#DTotalAmount").kendoNumericTextBox({
        //    format: "n6",
        //    decimals: 6,
        //    value: setDTotalAmount()
        //});
        //function setDTotalAmount() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.DTotalAmount;
        //    }
        //}



        function OnChangeTOPDate() {
            var _date = Date.parse($("#TOPDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                alertify.alert("Wrong Format Date MM/DD/YYYY");
            }
        }

        WinAdvisoryFeeTOP.center();
        WinAdvisoryFeeTOP.open();

    }
    function initGridAdvisoryFeeTOP() {
        var AdvisoryFeeTOPURL = window.location.origin + "/Radsoft/AdvisoryFeeTOP/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#AdvisoryFeePK").val(),
          dataSourceAdvisoryFeeTOP = getDataSourceAdvisoryFeeTOP(AdvisoryFeeTOPURL);

        $("#gridAdvisoryFeeTOP").kendoGrid({
            dataSource: dataSourceAdvisoryFeeTOP,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Advisory Fee Client"
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
                   command: { text: "show", click: showAdvisoryFeeTOP }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeleteAdvisoryFeeTOP }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AdvisoryFeeTOPPK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "TOPPercent", title: "TOP Percent", width: 200 },
               { field: "TOPDate", title: "TOP Date", format: "{0:dd/MMM/yyyy}", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    

    function onWinAdvisoryFeeTOPClose() {
        GlobValidatorAdvisoryFeeTOP.hideMessages();
        $("#TOPPercent").val("");
        $("#TOPDate").val("");
        $("#LastUsersID").val("");
        $("#LastUpdate").val("");
    }

    function DeleteAdvisoryFeeExpense(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;


        var dataItemX;
        var grid = $("#gridAdvisoryFeeExpense").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
            url: window.location.origin + "/Radsoft/AdvisoryFeeExpense/CheckAdvisoryFeeExpense/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    alertify.confirm("Are you sure want to DELETE Advisory Fee Expense ?", function (e) {
                        if (e) {

                            var AdvisoryFeeExpense = {
                                AdvisoryFeePK: dataItemX.AdvisoryFeePK,
                                LastUsersID: sessionStorage.getItem("user"),
                                AdvisoryFeeExpensePK: dataItemX.AdvisoryFeeExpensePK
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFeeExpense/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeExpense_D",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFeeExpense),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    refreshAdvisoryFeeExpenseGrid();
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
                    alertify.alert("Advisory Fee Has Posted");
                }
            }
        });
    }
    function refreshAdvisoryFeeExpenseGrid() {
        var gridAdvisoryFeeExpense = $("#gridAdvisoryFeeExpense").data("kendoGrid");
        gridAdvisoryFeeExpense.dataSource.read();
    }
    function getDataSourceAdvisoryFeeExpense(_url) {
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
                             AdvisoryFee: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             DirectExpense: { type: "string" },
                             DirectExpValue: { type: "number" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showAdvisoryFeeExpense(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#AdvisoryFeeExpensePK").val("");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridAdvisoryFeeExpense").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#AdvisoryFeeExpensePK").val(dataItemX.AutoNo);
            $("#DirectExpense").val(dataItemX.DirectExpense);
            $("#DirectExpValue").val(dataItemX.DirectExpValue);
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }



        //$("#DProfit").kendoNumericTextBox({
        //    format: "###.#### \\%",
        //    decimals: 4,
        //    value: setDProfit()

        //});
        //function setDProfit() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.DProfit;
        //    }
        //}

        $("#DirectExpValue").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setDirectExpValue()
        });
        function setDirectExpValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.DirectExpValue;
            }
        }



        //function OnChangeDate() {
        //    var _date = Date.parse($("#DBookingDate").data("kendoDatePicker").value());

        //    //Check if Date parse is successful
        //    if (!_date) {
        //        alertify.alert("Wrong Format Date MM/DD/YYYY");
        //    }
        //}

        WinAdvisoryFeeExpense.center();
        WinAdvisoryFeeExpense.open();

    }
    function initGridAdvisoryFeeExpense() {
        var AdvisoryFeeExpenseURL = window.location.origin + "/Radsoft/AdvisoryFeeExpense/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#AdvisoryFeePK").val(),
          dataSourceAdvisoryFeeExpense = getDataSourceAdvisoryFeeExpense(AdvisoryFeeExpenseURL);

        $("#gridAdvisoryFeeExpense").kendoGrid({
            dataSource: dataSourceAdvisoryFeeExpense,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Advisory Fee Client"
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
                   command: { text: "show", click: showAdvisoryFeeExpense }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeleteAdvisoryFeeExpense }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AdvisoryFeeExpensePK", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "DirectExpense", title: "Direct Expense", width: 200 },
               { field: "DirectExpValue", title: "Direct Exp Value", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    

    function onWinAdvisoryFeeExpenseClose() {
        GlobValidatorAdvisoryFeeExpense.hideMessages();
        $("#DirectExpense").val("");
        $("#DirectExpValue").val("");
        $("#LastUsersID").val("");
        $("#LastUpdate").val("");
    }


    $("#BtnRefreshAdvisoryFeeClient").click(function () {
        refreshAdvisoryFeeClientGrid();
    });

    $("#BtnCloseAdvisoryFeeClient").click(function () {

        alertify.confirm("Are you sure want to Close and Clear Advisory Fee Client?", function (e) {
            if (e) {
                WinAdvisoryFeeClient.close();
                alertify.alert("Close Advisory Fee Client");
            }
        });
    });

    $("#BtnRefreshAdvisoryFeeTOP").click(function () {
        refreshAdvisoryFeeTOPGrid();
    });

    $("#BtnCloseAdvisoryFeeTOP").click(function () {

        alertify.confirm("Are you sure want to Close and Clear Advisory Fee Client?", function (e) {
            if (e) {
                WinAdvisoryFeeTOP.close();
                alertify.alert("Close Advisory Fee Client");
            }
        });
    });

    $("#BtnRefreshAdvisoryFeeExpense").click(function () {
        refreshAdvisoryFeeExpenseGrid();
    });

    $("#BtnCloseAdvisoryFeeExpense").click(function () {

        alertify.confirm("Are you sure want to Close and Clear Advisory Fee Client?", function (e) {
            if (e) {
                WinAdvisoryFeeExpense.close();
                alertify.alert("Close Advisory Fee Client");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                WinAdvisoryFee.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        //$("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        //if ($("#AdvisoryFeePK").val() > 0) {
        //    alertify.alert("Advisory Fee HEADER ALREADY EXIST, Cancel and click add new to add more Header");
        //    return
        //}

        var val = validateDataAdvisoryFee();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add Advisory Fee HEADER ?", function (e) {
                if (e) {
                    $.ajax({
                        //url: window.location.origin,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            //$("#ProjectName").val(data);
                            //alertify.alert("Your new reference is " + data);
                            var AdvisoryFee = {
                                ProjectName: $('#ProjectName').val(),
                                StartDate: $('#StartDate').val(),
                                EndDate: $('#EndDate').val(),
                                Remarks: $('#Remarks').val(),
                                Status1Project: $('#Status1Project').val(),
                                Status1ProjectDate: $('#Status1ProjectDate').val(),
                                Status2Project: $('#Status2Project').val(),
                                Status2ProjectDate: $('#Status2ProjectDate').val(),
                                Status3Project: $('#Status3Project').val(),
                                Status3ProjectDate: $('#Status3ProjectDate').val(),
                                Status4Project: $('#Status4Project').val(),
                                Status4ProjectDate: $('#Status4ProjectDate').val(),
                                Status5Project: $('#Status5Project').val(),
                                Status5ProjectDate: $('#Status5ProjectDate').val(),
                                
                                EntryUsersID: sessionStorage.getItem("user")

                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFee_I",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFee),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data.Message);
                                    $("#AdvisoryFeePK").val(data.AdvisoryFeePK);
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AdvisoryFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "AdvisoryFee",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var AdvisoryFee = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ProjectName: $('#ProjectName').val(),
                                    StartDate: $('#StartDate').val(),
                                    EndDate: $('#EndDate').val(),
                                    Remarks: $('#Remarks').val(),
                                    ProjectValue: $('#StatusProject').val(),
                                    Status1Project: $('#Status1Project').val(),
                                    Status1ProjectDate: $('#Status1ProjectDate').val(),
                                    Status2Project: $('#Status2Project').val(),
                                    Status2ProjectDate: $('#Status2ProjectDate').val(),
                                    Status3Project: $('#Status3Project').val(),
                                    Status3ProjectDate: $('#Status3ProjectDate').val(),
                                    Status4Project: $('#Status4Project').val(),
                                    Status4ProjectDate: $('#Status4ProjectDate').val(),
                                    Status5Project: $('#Status5Project').val(),
                                    Status5ProjectDate: $('#Status5ProjectDate').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFee_U",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFee),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        WinAdvisoryFee.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });

                            } else {
                                alertify.alert("Data has been Changed by other user, Please check it first!");
                                WinAdvisoryFee.close();
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AdvisoryFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "AdvisoryFee",
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
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "AdvisoryFee" + "/" + $("#AdvisoryFeePK").val(),
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
                    WinAdvisoryFee.close();
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AdvisoryFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "AdvisoryFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AdvisoryFee = {
                                AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFee_A",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFee),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    WinAdvisoryFee.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            WinAdvisoryFee.close();
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AdvisoryFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "AdvisoryFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AdvisoryFee = {
                                AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFee_V",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFee),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    WinAdvisoryFee.close();
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            WinAdvisoryFee.close();
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AdvisoryFeePK").val() + "/" + $("#HistoryPK").val() + "/" + "AdvisoryFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AdvisoryFee = {
                                AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AdvisoryFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFee_R",
                                type: 'POST',
                                data: JSON.stringify(AdvisoryFee),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    WinAdvisoryFee.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            WinAdvisoryFee.close();
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
    $("#BtnAddAdvisoryFeeClient").click(function () {

        if ($("#AdvisoryFeePK").val() == 0 || $("#AdvisoryFeePK").val() == null) {
            alertify.alert("There's no Advisory Fee Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("Advisory Fee Already History");
        } else {
            showAdvisoryFeeClient();
        }
    });

    $("#BtnSaveAdvisoryFeeClient").click(function () {
        var val = validateDataAdvisoryFeeClient();
        if (val == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/AdvisoryFeeClient/CheckAdvisoryFeeClient/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Add Advisory Fee Client ?", function (e) {
                            if (e) {

                                var AdvisoryFeeClient = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeClientPK: $('#AdvisoryFeeClientPK').val(),
                                    Status: 2,
                                    ClientName: $('#ClientName').val(),
                                    ClientDescription: $('#ClientDescription').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeClient_I",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeClient),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeClient").empty();
                                        initGridAdvisoryFeeClient();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else {
                        alertify.confirm("Are you sure want to Advisory Fee Client ?", function (e) {
                            if (e) {

                                var AdvisoryFeeClient = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeClientPK: $('#DAdvisoryFeeClientPK').val(),
                                    Status: 2,
                                    ClientName: $('#ClientName').val(),
                                    ClientDescription: $('#ClientDescription').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeClient/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeClient_U",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeClient),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeClient").empty();
                                        initGridAdvisoryFeeClient();
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

    $("#BtnAddAdvisoryFeeTOP").click(function () {

        if ($("#AdvisoryFeePK").val() == 0 || $("#AdvisoryFeePK").val() == null) {
            alertify.alert("There's no Advisory Fee Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("Advisory Fee Already History");
        } else {
            showAdvisoryFeeTOP();
        }
    });

    $("#BtnSaveAdvisoryFeeTOP").click(function () {
        var val = validateDataAdvisoryFeeTOP();
        if (val == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/AdvisoryFeeTOP/CheckAdvisoryFeeTOP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Add Advisory Fee TOP ?", function (e) {
                            if (e) {

                                var AdvisoryFeeTOP = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeTOPPK: $('#AdvisoryFeeTOPPK').val(),
                                    Status: 2,
                                    TOPPercent: $('#TOPPercent').val(),
                                    TOPDate: $('#TOPDate').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeTOP/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeTOP_I",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeTOP),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeTOP").empty();
                                        initGridAdvisoryFeeTOP();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else {
                        alertify.confirm("Are you sure want to UPDATE Advisory Fee TOP ?", function (e) {
                            if (e) {

                                var AdvisoryFeeTOP = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeTOPPK: $('#DAdvisoryFeeTOPPK').val(),
                                    Status: 2,
                                    TOPPercent: $('#TOPPercent').val(),
                                    TOPDate: $('#TOPDate').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeTOP/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeTOP_U",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeTOP),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeTOP").empty();
                                        initGridAdvisoryFeeTOP();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $("#gridAdvisoryFeeTOP").empty();
                                        initGridAdvisoryFeeTOP();
                                    }
                                });
                            }
                        });
                    }
                }
            });

        }
    });

    $("#BtnAddAdvisoryFeeExpense").click(function () {

        if ($("#AdvisoryFeePK").val() == 0 || $("#AdvisoryFeePK").val() == null) {
            alertify.alert("There's no Advisory Fee Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("Advisory Fee Already History");
        } else {
            showAdvisoryFeeExpense();
        }
    });

    $("#BtnSaveAdvisoryFeeExpense").click(function () {
        var val = validateDataAdvisoryFeeExpense();
        if (val == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/AdvisoryFeeExpense/CheckAdvisoryFeeExpense/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AdvisoryFeePK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Add Advisory Fee Expense ?", function (e) {
                            if (e) {

                                var AdvisoryFeeExpense = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeExpensePK: $('#AdvisoryFeeExpensePK').val(),
                                    Status: 2,
                                    DirectExpense: $('#DirectExpense').val(),
                                    DirectExpValue: $('#DirectExpValue').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeExpense/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeExpense_I",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeExpense),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeExpense").empty();
                                        initGridAdvisoryFeeExpense();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else {
                        alertify.confirm("Are you sure want to UPDATE Advisory Fee Expense ?", function (e) {
                            if (e) {

                                var AdvisoryFeeExpense = {
                                    AdvisoryFeePK: $('#AdvisoryFeePK').val(),
                                    AdvisoryFeeExpensePK: $('#DAdvisoryFeeExpensePK').val(),
                                    Status: 2,
                                    DirectExpense: $('#DirectExpense').val(),
                                    DirectExpValue: $('#DirectExpValue').val(),
                                    LastUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AdvisoryFeeExpense/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AdvisoryFeeExpense_U",
                                    type: 'POST',
                                    data: JSON.stringify(AdvisoryFeeExpense),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        $("#gridAdvisoryFeeExpense").empty();
                                        initGridAdvisoryFeeExpense();
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