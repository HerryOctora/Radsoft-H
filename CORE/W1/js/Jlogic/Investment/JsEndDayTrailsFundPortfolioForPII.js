$(document).ready(function () {

    document.title = 'FORM END DAY TRAILS FUND PORTFOLIO FOR PII';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //fund grid
    var checkedIds = {};
    //end Fund grid


    //1
    initButton();
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

        $("#BtnApproveInGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });

        //$("#BtnGenerate").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        //});

        //$("#BtnGenerateUnit").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        //});

        //$("#BtnPosting").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnPosting.png"
        //});

        //$("#BtnDownload").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnDownload.png"
        //});

        $("#BtnListEndDayTrailsFundPortfolioMatching").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        //$("#BtnNavProjection").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        //});

        //$("#BtnReportPortfolioValuation").kendoButton({
        //    imageUrl: "../../Images/Icon/IcBtnDownload.png"
        //});

        $("#BtnParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnOkParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        // SETTING BUTTON EDT
        if (_ParamFundScheme == 'TRUE') {
            resetBtnGenerate();

            $("#BtnParamGenerate").show();
        }
        else if (_GlobClientCode == '99') {
            resetBtnGenerate();
            $("#BtnParamGenerate").show();
            $("#BtnGenerate").show();
        }

        else {
            resetBtnGenerate();

            $("#BtnGenerate").show();

        }


    }

    function resetBtnGenerate() {
        //$("#BtnGenerate").hide();
        $("#BtnParamGenerate").hide();
    }


    function initWindow() {
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });
        $("#ValueDate").data("kendoDatePicker").enable(false);
        $("#FilterDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeFilterDate
        });

        function OnChangeFilterDate() {
            var _FilterDate = Date.parse($("#FilterDate").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_FilterDate) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#FilterDate").data("kendoDatePicker").value(new Date());
                return;
            }
            refresh();
        }

        win = $("#WinEndDayTrailsFundPortfolioForPII").kendoWindow({
            height: 450,
            title: "End Day Trails Detail",
            visible: false,
            width: 950,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //WinNavProjection = $("#WinNavProjection").kendoWindow({
        //    height: 550,
        //    title: "* Nav Projection",
        //    visible: false,
        //    width: 1100,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    modal: true,
        //}).data("kendoWindow");

        WinParamGenerate = $("#WinParamGenerate").kendoWindow({
            height: 500,
            title: "Generate",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinParamGenerateClose
        }).data("kendoWindow");






    }

    var GlobValidator = $("#WinEndDayTrailsFundPortfolioForPII").kendoValidator().data("kendoValidator");

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
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridEndDayTrailsFundPortfolioForPIIApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridEndDayTrailsFundPortfolioForPIIPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridEndDayTrailsFundPortfolioForPIIHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnPosting").hide();
                $("#BtnApproved").show();
                $("#BtnReject").show();
                $("#BtnCancel").show();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnVoid").show();
                $("#BtnCancel").show();
                $("#BtnReject").hide();
                $("#BtnPosting").show();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnReject").hide();
                $("#BtnApproved").hide();
                $("#BtnPosting").hide();
                $("#BtnCancel").show();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnReject").hide();
                $("#BtnApproved").hide();
                $("#BtnPosting").hide();
                $("#BtnCancel").show();
            }

            $("#EndDayTrailsFundPortfolioForPIIPK").val(dataItemX.EndDayTrailsFundPortfolioForPIIPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (dataItemX.BitValidate == true) { $("#BitValidate").val("Yes"); } else if (dataItemX.BitValidate == false) { $("#BitValidate").val("No"); }
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#LogMessages").val(' ' + dataItemX.LogMessages.split('<br/>').join('\n'));
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#LastUsersID").val(dataItemX.LastUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }

        win.center().open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#EndDayTrailsFundPortfolioForPIIPK").val("");
        $("#HistoryPK").val("");
        $("#Status").val("");
        $("#Notes").val("");
        $("#ValueDate").val("");
        $("#BitValidate").val("");
        $("#LogMessages").val("");
        $("#EntryUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             EndDayTrailsFundPortfolioForPIIPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             BitValidate: { type: "boolean" },
                             ValueDate: { type: "date" },
                             LogMessages: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             LastUsersID: { type: "string" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
            $("#BtnApproveInGrid").hide();
        }
        else if (tabindex == 1) {
            RecalGridPending();
            $("#BtnApproveInGrid").show();
        }
        else if (tabindex == 2) {
            RecalGridHistory();
            $("#BtnApproveInGrid").hide();
        }
    }

    function initGrid() {
        $("#gridEndDayTrailsFundPortfolioForPIIApproved").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioForPIIApprovedURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(EndDayTrailsFundPortfolioForPIIApprovedURL);
        }

        $("#gridEndDayTrailsFundPortfolioForPIIApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Day Trails"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "EndDayTrailsFundPortfolioForPIIPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
                { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUsersID", title: "Last User ID", width: 200 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabEndDayTrailsFundPortfolioForPII").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                    refresh();
                } else {
                    refresh();
                }
            }
        });
    }

    function RecalGridPending() {
        $("#gridEndDayTrailsFundPortfolioForPIIPending").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioForPIIPendingURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(EndDayTrailsFundPortfolioForPIIPendingURL);
        }

        $("#gridEndDayTrailsFundPortfolioForPIIPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Day Trails"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "EndDayTrailsFundPortfolioForPIIPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
                { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUsersID", title: "Last User ID", width: 200 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }

    function RecalGridHistory() {
        $("#gridEndDayTrailsFundPortfolioForPIIHistory").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioForPIIHistoryURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(EndDayTrailsFundPortfolioForPIIHistoryURL);
        }

        $("#gridEndDayTrailsFundPortfolioForPIIHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form End Day Trails"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "EndDayTrailsFundPortfolioForPIIPK", title: "SysNo.", filterable: false, width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                { field: "BitValidate", title: "Validate", width: 150, template: "#= BitValidate ? 'Yes' : 'No' #" },
                { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                { field: "LogMessages", title: "Log Messages", width: 350 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUsersID", title: "Last User ID", width: 200 }
            ]
        });
    }


    $("#BtnApproved").click(function () {

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                var EndDayTrailsFundPortfolioForPII = {
                    EndDayTrailsFundPortfolioForPIIPK: $('#EndDayTrailsFundPortfolioForPIIPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolioForPII_A",
                    type: 'POST',
                    data: JSON.stringify(EndDayTrailsFundPortfolioForPII),
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsFundPortfolioForPIIPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrailsFundPortfolioForPII",
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                var EndDayTrailsFundPortfolioForPII = {
                    EndDayTrailsFundPortfolioForPIIPK: $('#EndDayTrailsFundPortfolioForPIIPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ValueDate: $('#ValueDate').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolioForPII_R",
                    type: 'POST',
                    data: JSON.stringify(EndDayTrailsFundPortfolioForPII),
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
                //        } else {
                //            alertify.alert("Data has been Changed by other user, Please check it first!");
                //            win.close();
                //            refresh();
                //        }
                //    },
                //    error: function (data) {
                //        alertify.alert(data.responseText);
                //    }
                //});
            }
        });
    });

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsFundPortfolioForPIIPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrailsFundPortfolioForPII",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var EndDayTrailsFundPortfolioForPII = {
                                EndDayTrailsFundPortfolioForPIIPK: $('#EndDayTrailsFundPortfolioForPIIPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ValueDate: $('#ValueDate').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolioForPII_V",
                                type: 'POST',
                                data: JSON.stringify(EndDayTrailsFundPortfolioForPII),
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

    $("#BtnParamGenerate").click(function () {
        showParamGenerate();
    });

    function showParamGenerate(e) {
        //fund grid disable dibawah ini
        //Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#FundFrom").kendoMultiSelect({
        //            dataValueField: "FundPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,
        //        });
        //        $("#FundFrom").data("kendoMultiSelect").value("0");


        //    },

        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        LoadData();
        //end fund grid

        WinParamGenerate.center();
        WinParamGenerate.open();

    }



    //fund grid 
    function LoadData() {
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Fund/GetFundComboBitNeedRecon/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },

                    }
                }
            }
        });


        var gridFundClient = $("#gridFund").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    template: "<input type='checkbox' class='checkbox' />"
                }

                , {
                    field: "ID",
                    title: "Fund ID",
                    width: "300px"
                }, {
                    field: "Name",
                    title: "Fund Name",
                    width: "300px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");

        gridFundClient.table.on("click", ".checkbox", selectRow);

        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSize = gridFundClient.dataSource.pageSize();
            gridFundClient.dataSource.pageSize(gridFundClient.dataSource.data().length);

            $('.checkbox').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            gridFundClient.dataSource.pageSize(oldPageSize);

        });


        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridFundClient = $("#gridFund").data("kendoGrid"),
                dataItemZ = gridFundClient.dataItem(rowA);

            checkedIds[dataItemZ.FundPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkbox")
                        .attr("checked", "checked");
                }
            }
        }
    }
    // end fund grid


    function onWinParamGenerateClose() {
        //fund grid disabled yg dibawah ini
        //$('#FundFrom').data('kendoMultiSelect').destroy();
        //$('#FundFrom').unwrap('.k-multiselect').show().empty();
        //$(".k-multiselect-wrap").remove();

    }



    $("#BtnOkParamGenerate").click(function () {

        //fund grid
        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)
        // end fund grid

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            var CloseNav = {
                                FundFrom: stringFundFrom,
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/ValidateGetCloseNav/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'POST',
                                contentType: "application/json;charset=utf-8",
                                data: JSON.stringify(CloseNav),
                                success: function (data) {
                                    if (data == false) {
                                        $.blockUI({});
                                        var EDT = {
                                            FundFrom: stringFundFrom,
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/GenerateWithParamFundForPII/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                            type: 'POST',
                                            data: JSON.stringify(EDT),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data);
                                                win.close();
                                                refresh();
                                                $.unblockUI();
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });
                                    } else {
                                        alertify.alert("Already Get NAV For this Day, Void / Reject First!");
                                        $.unblockUI();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });

                        } else {
                            alertify.alert("Date is Holiday, Please Generate Date Correctly!")
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

    function clearDataMulti() {
        $("#FundFrom").val("");

    }


    $("#BtnApproveInGrid").click(function (e) {

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        alertify.confirm("Are you sure want to Approve data End Day Trails for " + _date, function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolioForPII/ApproveByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EndDayTrailsFundPortfolioForPII_ApproveByDate" + "/" + _date,
                    type: 'POST',
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

});