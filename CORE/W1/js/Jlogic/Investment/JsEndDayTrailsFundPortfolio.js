$(document).ready(function () {
    document.title = 'FORM END DAY TRAILS FUND PORTFOLIO';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //fund grid
    var checkedIds = {};
    var checkedApproved = {};
    //end Fund grid


    //1
    initButton();
    //2
    initWindow();
    //3
    refresh();

    function initButton() {
        $("#BtnGenerateAvg").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
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

        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnGenerateUnit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnListEndDayTrailsFundPortfolioMatching").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnNavProjection").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnReportPortfolioValuation").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnOkParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnGenerate05").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
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

        $("#BtnUpdateFundPosition").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        //if (_GlobClientCode == '21') {
        $("#BtnUpdateFundPosition").show();
        //}
        //else {
        //    $("#BtnUpdateFundPosition").hide();
        //}


        if (_GlobClientCode == '05') {
            $("#BtnGenerate05").show();
        }
        else {
            $("#BtnGenerate05").hide();
        }


    }

    function resetBtnGenerate() {
        $("#BtnGenerate").hide();
        $("#BtnParamGenerate").hide();
        $("#BtnUpdateFundPosition").hide();
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

        win = $("#WinEndDayTrailsFundPortfolio").kendoWindow({
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

        WinNavProjection = $("#WinNavProjection").kendoWindow({
            height: 550,
            title: "* Nav Projection",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

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


        WinDetailFundPosition = $("#WinDetailFundPosition").kendoWindow({
            height: 600,
            title: "Fund Position",
            visible: false,
            width: 1500,
            open: function (e) {
                this.wrapper.css({ top: 70 })
            },
            //close: onWinNewSatuClose
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinEndDayTrailsFundPortfolio").kendoValidator().data("kendoValidator");

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
                grid = $("#gridEndDayTrailsFundPortfolioApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridEndDayTrailsFundPortfolioPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridEndDayTrailsFundPortfolioHistory").data("kendoGrid");
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

            $("#EndDayTrailsFundPortfolioPK").val(dataItemX.EndDayTrailsFundPortfolioPK);
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
        $("#EndDayTrailsFundPortfolioPK").val("");
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
                            EndDayTrailsFundPortfolioPK: { type: "number" },
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
        $("#gridEndDayTrailsFundPortfolioApproved").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioApprovedURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(EndDayTrailsFundPortfolioApprovedURL);
        }

        var grid = $("#gridEndDayTrailsFundPortfolioApproved").kendoGrid({
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
            dataBound: onDataBoundApproved,
            toolbar: ["excel"],
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                    template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                },
                { command: { text: "Show", click: showDetails }, title: " ", width: 100 },
                { field: "EndDayTrailsFundPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
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
        }).data("kendoGrid");

        grid.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;


        $('#chbB').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxApproved').each(function (idx, item) {
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

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridEndDayTrailsFundPortfolioApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.EndDayTrailsFundPortfolioPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].EndDayTrailsFundPortfolioPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }
        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabEndDayTrailsFundPortfolio").kendoTabStrip({
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
        $("#gridEndDayTrailsFundPortfolioPending").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioPendingURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(EndDayTrailsFundPortfolioPendingURL);
        }

        $("#gridEndDayTrailsFundPortfolioPending").kendoGrid({
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
                { field: "EndDayTrailsFundPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
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
        $("#gridEndDayTrailsFundPortfolioHistory").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsFundPortfolioHistoryURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(EndDayTrailsFundPortfolioHistoryURL);
        }

        $("#gridEndDayTrailsFundPortfolioHistory").kendoGrid({
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
                { field: "EndDayTrailsFundPortfolioPK", title: "SysNo.", filterable: false, width: 100 },
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
                var EndDayTrailsFundPortfolio = {
                    EndDayTrailsFundPortfolioPK: $('#EndDayTrailsFundPortfolioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolio_A",
                    type: 'POST',
                    data: JSON.stringify(EndDayTrailsFundPortfolio),
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
                //    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsFundPortfolioPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrailsFundPortfolio",
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                var EndDayTrailsFundPortfolio = {
                    EndDayTrailsFundPortfolioPK: $('#EndDayTrailsFundPortfolioPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ValueDate: $('#ValueDate').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolio_R",
                    type: 'POST',
                    data: JSON.stringify(EndDayTrailsFundPortfolio),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsFundPortfolioPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrailsFundPortfolio",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var EndDayTrailsFundPortfolio = {
                                EndDayTrailsFundPortfolioPK: $('#EndDayTrailsFundPortfolioPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ValueDate: $('#ValueDate').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrailsFundPortfolio_V",
                                type: 'POST',
                                data: JSON.stringify(EndDayTrailsFundPortfolio),
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

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");

        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Fund/GetFundComboByMaturityDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
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

        $("#gridFund").empty();
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
        //reset grid select
        for (var i in checkedIds) {
            checkedIds[i] = null
        }

    }

    $("#BtnGenerate").click(function () {
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        if (_GlobClientCode == "11") {
            alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateCheckYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    data = false;
                                                    if (data == false) {
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                if (data == false) {
                                                                    $.blockUI({});
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                        type: 'GET',
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
                                                                    alertify.alert("Already Generate data For this Day, / Reject First!");
                                                                    $.unblockUI();
                                                                }
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                                $.unblockUI();
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Please Check End Day Trails Fund Trx Portfolio Yesterday First!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        } else {
                                            alertify.alert("Investment Must Be Approved in this day, Please Check Investment First!");
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
        }
        else {
            alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateCheckYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    data = false;
                                                    if (data == false) {
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                if (data == false) {
                                                                    $.blockUI({});
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                        type: 'GET',
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
                                                                    alertify.alert("Already Generate data For this Day, / Reject First!");
                                                                    $.unblockUI();
                                                                }
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                                $.unblockUI();
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Please Check End Day Trails Fund Trx Portfolio Yesterday First!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        } else {
                                            alertify.alert("Investment Must Be Approved in this day, Please Check Investment First!");
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
        }



    });

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
        if (stringFundFrom == "") {
            alertify.alert("There's No Selected Data");
            return;
        }

        var SettlementStatus = {
            FundFrom: stringFundFrom,
        };

        $.ajax({
            url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateStatusSettlement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(SettlementStatus),
            success: function (data) {
                if (_GlobClientCode == "05") { // skip validasi
                    alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
                        if (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == "") {

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Instrument/CheckInstrumentStatusPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GenerateWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
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

                                                }
                                                else {
                                                    alertify.alert("Please Check Instrument Status Pending !");
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
                }
                else {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == "") {

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Instrument/CheckInstrumentStatusPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                                                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GenerateWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
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

                                                    }
                                                    else {
                                                        alertify.alert("Please Check Instrument Status Pending !");
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

                    } else {
                        alertify.alert("Cannot Generate " + data + ", Please Check Settlement!");
                        $.unblockUI();
                    }
                }


            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    });

    function clearDataMulti() {
        $("#FundFrom").val("");

    }

    $("#BtnNavProjection").click(function () {

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        alertify.confirm("Are you sure want to get data Nav Projection for " + _date, function (e) {
            if (e) {
                var CloseNav = {
                    FundFrom: 0,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(CloseNav),
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {
                                        GenerateNavProjectionGrid();
                                        WinNavProjection.center();
                                        WinNavProjection.open();
                                    } else {
                                        alertify.alert("Please Check End Day Trails Fund Portfolio First!");
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnApproveInGrid").click(function (e) {

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        alertify.confirm("Are you sure want to Approve data End Day Trails for " + _date, function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ApproveByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EndDayTrailsFundPortfolio_ApproveByDate" + "/" + _date,
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

    $("#BtnDownload").click(function () {

        alertify.confirm("Are you sure want to Download TB Reconcile ?", function (e) {
            if (e) {
                var TBReconcile = {
                    ValueDate: $('#FilterDate').val()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/DownloadTBReconcile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(TBReconcile),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.open(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    function getDataSourceProjection(_url) {
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
                            ValueDate: { type: "date" },
                            FundName: { type: "string" },
                            Nav: { type: "number" },
                            AUM: { type: "number" },
                            Compare: { type: "number" },
                        }
                    }
                }
            });
    }

    var gridData;
    function GenerateNavProjectionGrid() {
        $("#gridData").empty();
        var URL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GenerateNavProjection/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceProjection(URL);
        gridData = $("#gridData").kendoGrid({
            dataSource: dataSourceApproved,
            height: 500,
            scrollable: {
                virtual: true
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
            toolbar: kendo.template($("#gridData").html()),
            columns: [
                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "FundName", title: "FundName", width: 200 },
                { field: "Nav", title: "Nav Projection", format: "{0:n4}", width: 150 },
                { field: "AUM", title: "AUM Projection", format: "{0:n2}", width: 150 },
                {
                    field: "Compare", title: "Compare NAV Yesterday", width: 150,
                    template: "#: Compare  # %", attributes: { style: "text-align:right;" }
                },
            ]
        }).data("kendoGrid");
    }

    $("#BtnReportPortfolioValuation").click(function () {

        // Validasi untuk ngejaga FilterDate tetap diisi
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to get data Report Portfolio Valuation for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ReportPortfolioValuation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        window.location = data
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    // Update Maturity Deposito
    $("#BtnShow").click(function () {
        GridUpdateFundPosition();
        $('#gridDetailFundPosition').show();
    });

    $("#BtnUpdateFundPosition").click(function () {
        $('#gridDetailFundPosition').hide();
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeUFundPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeUFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        WinDetailFundPosition.center();
        WinDetailFundPosition.open();
    });

    function getDataSourceUpdateFundPosition(_url) {
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
                            InstrumentID: { type: "string" , editable: false },
                            InstrumentName: { type: "string", editable: false  },
                            MaturityDate: { type: "date" },
                            Balance: { type: "number", editable: false  },
                            AcqDate: { type: "date", editable: false  },
                            InterestPercent: { type: "number" },
                            Category: { type: "string", editable: false  },
                            InterestDaysTypeDesc: { type: "string", editable: false  },
                            InterestPaymentTypeDesc: { type: "string", editable: false  },
                            PaymentModeOnMaturityDesc: { type: "string", editable: false  },
                        }
                    }
                },

            });
    }

    function GridUpdateFundPosition() {
        $("#gridUpdateFundPosition").empty();
        var UpdateFundPositionURL = window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GetDataForUpdateFundPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UFundPK').val() + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceUpdateFundPosition(UpdateFundPositionURL);

        var gridUpdateFundPosition = $("#gridUpdateFundPosition").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Detail Fund Position"
                }
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            editable: "incell",
            reorderable: true,
            sortable: true,
            resizable: true,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Update", click: _update }, title: " ", width: 95 },
                { field: "FundPK", title: "FundPK", hidden: true },
                { field: "InstrumentPK", title: "InstrumentPK", hidden: true },
                { field: "InstrumentID", title: "Instrument ID" },
                { field: "InstrumentName", title: "Instrument Name" },
                {
                    field: "InterestPercent", title: "Interest Percent", width: 150,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "MaturityDate", title: "Maturity Date", width: 110, template: "#= (MaturityDate == null) ? ' ' : kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                { field: "Balance", title: "Nominal", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "AcqDate", title: "Acq Date", width: 110, template: "#= (AcqDate == null) ? ' ' : kendo.toString(kendo.parseDate(AcqDate), 'dd/MMM/yyyy')#" },
                { field: "InterestDaysTypeDesc", title: "Interest Days Type" },
                { field: "InterestPaymentTypeDesc", title: "Interest Payment Type" },
                { field: "Category", title: "Category" },
                { field: "PaymentModeOnMaturityDesc", title: "Payment Mode On Maturity" },
            ]
        }).data("kendoGrid");

    }

    function _update(e) {

        if (e) {
            var grid;
            grid = $("#gridUpdateFundPosition").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var UpdateFundPosition = {
                ValueDate: kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                FundPK: dataItemX.FundPK,
                InstrumentPK: dataItemX.InstrumentPK,
                MaturityDate: kendo.toString(dataItemX.MaturityDate, "MM-dd-yy"),
                InterestPercent: dataItemX.InterestPercent,
                UpdateUsersID: sessionStorage.getItem("user")
            };

            alertify.confirm("Are you sure want to Update Data ?", function (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/UpdateDataFundPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(UpdateFundPosition),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Update Success");
                        refreshUpdateFundPosition();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            });
        }

    }

    function refreshUpdateFundPosition() {
        var newDS = getDataSourceUpdateFundPosition(window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GetDataForUpdateFundPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#UFundPK').val() + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridUpdateFundPosition").data("kendoGrid").setDataSource(newDS);
    }

    $("#BtnGenerate05").click(function () {

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
                                            url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GenerateWithParamFund_05/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
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

    $("#BtnGenerateAvg").click(function () {
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate Average for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/GenerateAverageFP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
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

    $("#BtnVoidBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringEndDayTrailsFundPortfolioSelected = '';

        for (var i in ArrayFundFrom) {
            stringEndDayTrailsFundPortfolioSelected = stringEndDayTrailsFundPortfolioSelected + ArrayFundFrom[i] + ',';

        }
        stringEndDayTrailsFundPortfolioSelected = stringEndDayTrailsFundPortfolioSelected.substring(0, stringEndDayTrailsFundPortfolioSelected.length - 1)

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                var EndDayTrailsFundPortfolio = {
                    ValueDate: $('#FilterDate').val(),
                    VoidUsersID: sessionStorage.getItem("user"),
                    stringEndDayTrailsFundPortfolioFrom: stringEndDayTrailsFundPortfolioSelected
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(EndDayTrailsFundPortfolio),
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