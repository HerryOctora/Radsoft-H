$(document).ready(function () {
    document.title = 'FORM END DAY TRAILS';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinRetrieveFromBridge;
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

        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
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

        $("#BtnListEndDayTrailsMatching").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnOpenCloseNav").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnReviseUnit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnRevise").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnCancelReviseUnit").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnRetrieveFromBridge").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRetrieve").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnGenerateFundJournalOnly").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnGenerateUnitOnly").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnReviseByFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnGenerateReviseByFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkParamGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnGenerateFundJournalOnlyWithParamFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkGenerateFundJournalOnlyWithParamFund").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkRetrieve").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });

        // SETTING BUTTON EDT
        if (_GlobClientCode == '05') {
            resetBtnGenerate();

            $("#BtnGenerateFundJournalOnlyWithParamFund").show();
            $("#BtnRetrieveFromBridge").show();
            $("#BtnRetrieve").show();
        }
        else if (_GlobClientCode == '29') {
            resetBtnGenerate();

            $("#BtnGenerateUnitOnly").show();
        }
        else if (_ParamFundScheme == 'TRUE') {
            resetBtnGenerate();

            $("#BtnGenerateFundJournalOnlyWithParamFund").show();
            $("#BtnGenerateUnitOnly").show();
        }
        else if (_GlobClientCode == '16' || _GlobClientCode == '24') {
            resetBtnGenerate();

            $("#BtnGenerateUnitOnly").show();
        }
        else if (_GlobClientCode == '02' || _GlobClientCode == '25') {
            resetBtnGenerate();

            $("#BtnParamGenerate").show();
        }
        else {
            resetBtnGenerate();

            $("#BtnGenerate").show();

        }
    }

    function resetBtnGenerate()
    {
        $("#BtnGenerate").hide();
        $("#BtnParamGenerate").hide();
        $("#BtnRetrieveFromBridge").hide();
        $("#BtnRetrieve").hide();
        $("#BtnGenerateFundJournalOnly").hide();
        $("#BtnGenerateUnitOnly").hide();
        $("#BtnGenerateFundJournalOnlyWithParamFund").hide();
    }

    
    function initWindow() {
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });
        $("#ValueDate").data("kendoDatePicker").enable(false);
        $("#FilterDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date(),
            change: OnChangeFilterDate
        });
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
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


        function OnChangeValueDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
        }


        win = $("#WinEndDayTrails").kendoWindow({
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


        WinReviseUnit = $("#WinReviseUnit").kendoWindow({
            height: 350,
            title: "Revise Unit",
            visible: false,
            width: 750,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinReviseUnitClose
        }).data("kendoWindow");


        WinRetrieveFromBridge = $("#WinRetrieveFromBridge").kendoWindow({
            height: 450,
            title: "Retrieve From Bridge",
            visible: false,
            width: 950,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListRetrieveFromBridgeClose

        }).data("kendoWindow");

        WinReviseByFund = $("#WinReviseByFund").kendoWindow({
            height: 200,
            title: "Revise By Fund",
            visible: false,
            width: 500,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinReviseByFundClose
        }).data("kendoWindow");


        WinRetrieveFund = $("#WinRetrieveFund").kendoWindow({
            height: 250,
            title: "Generate",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinRetrieveFundClose
        }).data("kendoWindow");

        //WinParamGenerate = $("#WinParamGenerate").kendoWindow({
        //    height: 250,
        //    title: "Generate",
        //    visible: false,
        //    width: 500,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    modal: true,
        //    close: onWinParamGenerateClose
        //}).data("kendoWindow");

        //WinGenerateFundJournalOnlyWithParamFund = $("#WinGenerateFundJournalOnlyWithParamFund").kendoWindow({
        //    height: 250,
        //    title: "Generate",
        //    visible: false,
        //    width: 500,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    modal: true,
        //    close: onWinGenerateFundJournalOnlyWithParamFundClose
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


        WinFundJournalOnlyWithParamFund = $("#WinFundJournalOnlyWithParamFund").kendoWindow({
            height: 500,
            title: "Generate",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinFundJournalOnlyWithParamFundClose
        }).data("kendoWindow");

        WinCheckAccountFromFundAccountingSetup = $("#WinCheckAccountFromFundAccountingSetup").kendoWindow({
            height: 450,
            title: "* Check Account",
            visible: false,
            width: 1200,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");


    }

    var GlobValidator = $("#WinEndDayTrails").kendoValidator().data("kendoValidator");
    var GlobValidatorReviseUnit = $("#WinReviseUnit").kendoValidator().data("kendoValidator");

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

    function validateDataReviseUnit() {


        if (GlobValidatorReviseUnit.validate()) {
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
                grid = $("#gridEndDayTrailsApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridEndDayTrailsPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridEndDayTrailsHistory").data("kendoGrid");
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

            $("#EndDayTrailsPK").val(dataItemX.EndDayTrailsPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            if (dataItemX.BitValidate == true) { $("#BitValidate").val("Yes"); } else if (dataItemX.BitValidate == false) { $("#BitValidate").val("No"); }
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#LogMessages").val(' ' + dataItemX.LogMessages.split('<br/>').join('\n'));
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#LastUsersID").val(dataItemX.LastUsersID);

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

       
        win.center().open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#EndDayTrailsPK").val("");
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
                             EndDayTrailsPK: { type: "number" },
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
        $("#gridEndDayTrailsApproved").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsApprovedURL = window.location.origin + "/Radsoft/EndDayTrails/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(EndDayTrailsApprovedURL);
        }

        var grid = $("#gridEndDayTrailsApproved").kendoGrid({
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
                { field: "EndDayTrailsPK", title: "SysNo.", filterable: false, width: 100 },
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
                grid = $("#gridEndDayTrailsApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.EndDayTrailsPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].EndDayTrailsPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }
        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabEndDayTrails").kendoTabStrip({
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
        $("#gridEndDayTrailsPending").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsPendingURL = window.location.origin + "/Radsoft/EndDayTrails/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(EndDayTrailsPendingURL);
        }

        $("#gridEndDayTrailsPending").kendoGrid({
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
                { field: "EndDayTrailsPK", title: "SysNo.", filterable: false, width: 100 },
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
        $("#gridEndDayTrailsHistory").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var EndDayTrailsHistoryURL = window.location.origin + "/Radsoft/EndDayTrails/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(EndDayTrailsHistoryURL);
        }

        $("#gridEndDayTrailsHistory").kendoGrid({
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
                { field: "EndDayTrailsPK", title: "SysNo.", filterable: false, width: 100 },
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
                if ($("#BitValidate").val() == "Yes") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrails",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var EndDayTrails = {
                                    EndDayTrailsPK: $('#EndDayTrailsPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrails_A",
                                    type: 'POST',
                                    data: JSON.stringify(EndDayTrails),
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
                else {
                    alertify.alert("Please Check Validate End Day Trails First!");
                }
               
            }
        });
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var EndDayTrails = {
                                EndDayTrailsPK: $('#EndDayTrailsPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ValueDate: $('#ValueDate').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrails_R",
                                type: 'POST',
                                data: JSON.stringify(EndDayTrails),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EndDayTrailsPK").val() + "/" + $("#HistoryPK").val() + "/" + "EndDayTrails",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            //$.ajax({
                            //    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateVoid/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                            //    type: 'GET',
                            //    contentType: "application/json;charset=utf-8",
                            //    success: function (data) {
                            //        if (data == false) {
                                        var EndDayTrails = {
                                            EndDayTrailsPK: $('#EndDayTrailsPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ValueDate: $('#ValueDate').val(),
                                            VoidUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/EndDayTrails/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "EndDayTrails_V",
                                            type: 'POST',
                                            data: JSON.stringify(EndDayTrails),
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
                            //            alertify.alert("Please Check Subs/ Redemp , Already Posting data");
                            //            $.unblockUI();
                            //        }
                            //    },
                            //    error: function (data) {
                            //        alertify.alert(data.responseText);
                            //        $.unblockUI();
                            //    }
                            //});


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
        ////Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#ParamFundFrom").kendoMultiSelect({
        //            dataValueField: "FundPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,
        //        });
        //        $("#ParamFundFrom").data("kendoMultiSelect").value("0");


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
        //$('#ParamFundFrom').data('kendoMultiSelect').destroy();
        //$('#ParamFundFrom').unwrap('.k-multiselect').show().empty();
        //$(".k-multiselect-wrap").remove();

    }

    $("#BtnGenerate").click(function () {
        
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
            if (e) {
                if (_GlobClientCode == '08' || _GlobClientCode == '10' || _GlobClientCode == '16' || _GlobClientCode == '24') {
                    $.blockUI({});
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
                                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == false) {
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/EndDayTrails/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                alertify.alert("Generate End Day Trails Success");
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
                                                    else {
                                                        alertify.alert("Date is Holiday");
                                                        $.unblockUI();
                                                    }

                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        } else {
                                            alertify.alert("Please check today/yesterday EndDayTrails!");
                                            $.unblockUI();
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });
                            } else {
                                alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
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
                    
                    $.blockUI({});
                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
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
                                                url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == false) {
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                if (data == false) {
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/EndDayTrails/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                        type: 'GET',
                                                                        contentType: "application/json;charset=utf-8",
                                                                        success: function (data) {
                                                                            alertify.alert("Generate End Day Trails Success");
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
                                                                else {
                                                                    alertify.alert("Date is Holiday");
                                                                    $.unblockUI();
                                                                }

                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                                $.unblockUI();
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Please check today/yesterday EndDayTrails!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        } else {
                                            alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                                            $.unblockUI();
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });
                            } else {
                                alertify.alert("Please Check End Day Trails Fund Portfolio First!");
                                $.unblockUI();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    })
                }
                
            }
        });
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
        
        //clearDataMulti();
        //var ArrayFundFrom = $("#ParamFundFrom").data("kendoMultiSelect").value();
        //var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        //console.log(stringFundFrom)

        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
            if (e) {
                $.blockUI({});


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
                                url: window.location.origin + "/Radsoft/CloseNav/ValidateGetCloseNavYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'POST',
                                contentType: "application/json;charset=utf-8",
                                data: JSON.stringify(CloseNav),
                                success: function (data) {
                                    if (data.Result == 1) {
                                        alertify.alert(data.Notes);
                                        $.unblockUI();
                                    }
                                    else if (data.Result == 2)
                                    {
                                        alertify.confirm(data.Notes + ", Are you sure still want to Generate ?", function (e) {
                                            if (e) {

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
                                                            var EDT = {
                                                                FundFrom: stringFundFrom,
                                                            };
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/EndDayTrails/GenerateWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                type: 'POST',
                                                                data: JSON.stringify(EDT),
                                                                contentType: "application/json;charset=utf-8",
                                                                success: function (data) {
                                                                    alertify.alert("Generate End Day Trails Success");
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
                                        });
                                    }
                                    else
                                    {
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
                                                    var EDT = {
                                                        FundFrom: stringFundFrom,
                                                    };
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/EndDayTrails/GenerateWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                        type: 'POST',
                                                        data: JSON.stringify(EDT),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            alertify.alert("Generate End Day Trails Success");
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

                                      
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });


                        }
                        else {
                            alertify.alert("Date is Holiday");
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
        $("#ParamFundFrom").val("");

    }

    $("#BtnApproveInGrid").click(function (e) {
        
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        alertify.confirm("Are you sure want to Approve data End Day Trails for " + _date, function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrails/ApproveByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EndDayTrails_ApproveByDate" + "/" + _date,
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
                    url: window.location.origin + "/Radsoft/EndDayTrails/DownloadTBReconcile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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


    function InsertIntoCloseNav() {
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNav/InsertIntoCloseNavByEndDayTrails/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $.unblockUI();
            },
            error: function (data) {
                alertify.alert(data.responseText);

            }
        });

    }

    $("#BtnRevise").click(function () {
        showReviseUnit();
    });

    function showReviseUnit(e) {
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#FundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        WinReviseUnit.center();
        WinReviseUnit.open();

    }

    function onWinReviseUnitClose() {
        GlobValidatorReviseUnit.hideMessages();
    }

    function clearDataReviseUnit() {
        $("#FundFrom").data("kendoMultiSelect").value("");
        $("#DateFrom").val("");
        $("#DateTo").val("");

    }

    $("#BtnReviseUnit").click(function () {

        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var val = validateDataReviseUnit();

        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    var EndDayTrails = {
                        FundFrom: stringFundFrom,
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrails/EndDayTrails_UpdateAUMbyDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(EndDayTrails),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            WinReviseUnit.close();
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


    $("#BtnCancelReviseUnit").click(function () {

        alertify.confirm("Are you sure want to close Revise Unit ?",
            function (e) {
                if (e) {
                    WinReviseUnit.close();
                    alertify.alert("Close Revise Unit");
                }
            });
    });


    $("#BtnRetrieveFromBridge").click(function () {
        _showRetrieveFromBridge();
        WinRetrieveFromBridge.center().open();
    });


    function InitDataSourceRetrieveFromBridge() {
        return new kendo.data.DataSource(

            {

                transport:
                {

                    read:
                    {

                        url: window.location.origin + "/Radsoft/RetrieveFromBridge/RetrieveUnitRegistry/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        dataType: "json"
                    },

                },
                batch: true,
                cache: false,
                error: function (e) {
                    alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 50,
                schema: {
                    model: {
                        fields: {
                            RetrieveFromBridgePK: { type: "number" },
                            FundID: { type: "string" },
                            FundName: { type: "string" },
                            SUBAmount: { type: "number" },
                            SUBFeeAmount: { type: "number" },
                            SWIInAmount: { type: "number" },
                            SWIInFeeAmount: { type: "number" },
                            REDAmount: { type: "number" },
                            REDFeeAmount: { type: "number" },
                            REDPaymentDate: { type: "date" },
                            SWIOutAmount: { type: "number" },
                            SWIOutPaymentDate: { type: "date" },
                        }
                    }
                }
            });
    }


    function _showRetrieveFromBridge() {

        var dsListRetrieveFromBridge = InitDataSourceRetrieveFromBridge();
        var gridListRetrieveFromBridge = $("#gridListRetrieveFromBridge").kendoGrid({
            dataSource: dsListRetrieveFromBridge,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            editable: true,
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
                { command: { text: "Edit", click: _update }, title: " ", width: 80 },
                { field: "RetrieveFromBridgePK", title: "RetrieveFromBridgePK", width: 150, hidden: true },
                { field: "FundID", title: "Fund ID", width: 150, hidden: true },
                { field: "FundName", title: "Fund Name", width: 150 },
                { field: "SUBAmount", title: "SUB Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "SUBFeeAmount", title: "SUB Fee", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "SWIInAmount", title: "SWI In", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "SWIInFeeAmount", title: "SWIIn Fee", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "REDAmount", title: "RED Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "REDFeeAmount", title: "RED Fee", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                //{ field: "REDUnit", title: "RED Unit", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                //{ field: "REDFeeUnit", title: "RED Fee", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },     
                { field: "REDPaymentDate", title: "RED PaymentDate", width: 150, template: "#= (REDPaymentDate == null) ? ' ' : kendo.toString(kendo.parseDate(REDPaymentDate), 'dd/MMM/yyyy')#" },
                //{ field: "SWIOutFeeAmount", title: "SWI Out Fee Amount", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                //{ field: "SWIOutUnit", title: "SWI Out Unit", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                //{ field: "SWIOutFeeUnit", title: "SWI Out Fee Unit", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "SWIOutAmount", title: "SWI Out Amount", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "SWIOutPaymentDate", title: "SWIOut PaymentDate", width: 150, template: "#= (SWIOutPaymentDate == null) ? ' ' : kendo.toString(kendo.parseDate(SWIOutPaymentDate), 'dd/MMM/yyyy')#" },


            ]
        }).data("kendoGrid");

    }


    function _update(e) {
        if (e) {
            var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
            var grid;
            grid = $("#gridListRetrieveFromBridge").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var Retrieve = {
                Date: _date,
                RetrieveFromBridgePK: dataItemX.RetrieveFromBridgePK,
                FundName: dataItemX.FundID,
                SUBAmount: dataItemX.SUBAmount,
                SUBFeeAmount: dataItemX.SUBFeeAmount,
                SWIInAmount: dataItemX.SWIInAmount,
                SWIInFeeAmount: dataItemX.SWIInFeeAmount,
                REDAmount: dataItemX.REDAmount,
                REDFeeAmount: dataItemX.REDFeeAmount,
                REDPaymentDate: kendo.toString(kendo.parseDate(dataItemX.REDPaymentDate), 'dd/MMM/yyyy'),
                SWIOutAmount: dataItemX.SWIOutAmount,
                SWIOutPaymentDate: kendo.toString(kendo.parseDate(dataItemX.SWIOutPaymentDate), 'dd/MMM/yyyy'),

            };


            $.ajax({
                url: window.location.origin + "/Radsoft/RetrieveFromBridge/UpdateUnitRegistry/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Retrieve),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }


    function onWinListRetrieveFromBridgeClose() {
        $("#gridListRetrieveFromBridge").empty();
    }


    $("#BtnRetrieve").click(function () {
        showRetrieveFund();
    });



    function showRetrieveFund(e) {

        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#RetrieveFundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                });
                $("#RetrieveFundFrom").data("kendoMultiSelect").value("0");

                $("#RetrieveFundFrom").data("kendoMultiSelect").options.maxSelectedItems = 20;
            },

            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        WinRetrieveFund.center();
        WinRetrieveFund.open();

    }


    $("#BtnOkRetrieve").click(function () {
        $.blockUI({});
        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        clearDataMultiRetrieve();
        var ArrayFundFrom = $("#RetrieveFundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)
        alertify.confirm("Are you sure want to Retrieve data for " + _date, function (e) {
            if (e) {
                var Retrieve = {
                    RetrieveFundFrom: stringFundFrom,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveFromBridge/Generate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'POST',
                    data: JSON.stringify(Retrieve),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Retrieve Success");
                        win.close();
                        $("#gridListRetrieveFromBridge").empty();
                        _showRetrieveFromBridge();
                        $.unblockUI();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });
        $.unblockUI();
    });


    function onWinRetrieveFundClose() {
        $('#RetrieveFundFrom').data('kendoMultiSelect').destroy();
        $('#RetrieveFundFrom').unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();

    }


    function clearDataMultiRetrieve() {
        $("#RetrieveFundFrom").val("");

    }


    $("#BtnGenerateFundJournalOnly").click(function () {

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        alertify.confirm("Are you sure want to Generate Fund Journal data for " + _date, function (e) {
            if (e) {

                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            var CloseNav = {
                                FundFrom: stringFundFrom,
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'POST',
                                contentType: "application/json;charset=utf-8",
                                data: JSON.stringify(CloseNav),
                                success: function (data) {
                                    if (data == false) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateYesterday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == false) {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data == false) {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/EndDayTrails/GenerateFundJournalOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        alertify.alert("Generate End Day Trails Success");
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
                                                            else {
                                                                alertify.alert("Date is Holiday");
                                                                $.unblockUI();
                                                            }

                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                            $.unblockUI();
                                                        }
                                                    });
                                                } else {
                                                    alertify.alert("Please check today/yesterday EndDayTrails!");
                                                    $.unblockUI();
                                                }
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });
                                    } else {
                                        alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                                        $.unblockUI();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });
                        } else {
                            alertify.alert("Please Check End Day Trails Fund Portfolio First!");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                })


            }
        });
    });


    $("#BtnGenerateUnitOnly").click(function () {

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        alertify.confirm("Are you sure want to Generate Unit data for " + _date, function (e) {
            if (e) {

                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckNextDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
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
                                            url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateYesterdayUnitOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == false) {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data == false) {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/EndDayTrails/GenerateUnitOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        alertify.alert("Generate End Day Trails Unit Only Success");
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
                                                            else {
                                                                alertify.alert("Date is Holiday");
                                                                $.unblockUI();
                                                            }

                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                            $.unblockUI();
                                                        }
                                                    });
                                                } else {
                                                    alertify.alert("Please check yesterday EndDayTrails!");
                                                    $.unblockUI();
                                                }
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });
                                    } else {
                                        alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                                        $.unblockUI();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });
                        } else {
                            alertify.alert("Can't End Day Trails Unit, Already Posted Subs/Redemp/Switch or Already End Day Trails in Next Day !");
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


    $("#BtnReviseByFund").click(function () {
        showReviseByFund();
    });


    function showReviseByFund(e) {
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#ParamReviseFund").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#ParamReviseFund").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        WinReviseByFund.center();
        WinReviseByFund.open();

    }


    function onWinReviseByFundClose() {
        $('#ParamReviseFund').data('kendoMultiSelect').destroy();
        $('#ParamReviseFund').unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();

    }


    $("#BtnGenerateReviseByFund").click(function () {

        var ArrayParamReviseFund = $("#ParamReviseFund").data("kendoMultiSelect").value();
        var stringParamReviseFund = '';
        for (var i in ArrayParamReviseFund) {
            stringParamReviseFund = stringParamReviseFund + ArrayParamReviseFund[i] + ',';

        }
        stringParamReviseFund = stringParamReviseFund.substring(0, stringParamReviseFund.length - 1)

        var val = 1;

        if (val == 1) {
            alertify.confirm("Are you sure want to Revise by Fund ?", function (e) {
                if (e) {
                    $.blockUI({});
                    var EndDayTrails = {
                        ValueDate: kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        FundFrom: stringParamReviseFund,
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrails/ReviseByFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EndDayTrails),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert("Revise By Fund Success");
                            WinReviseByFund.close();
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
        }

    });


    $("#BtnGenerateFundJournalOnlyWithParamFund").click(function () {
        showGenerateFundJournalOnlyWithParamFund();
    });


    function showGenerateFundJournalOnlyWithParamFund(e) {
        ////Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#ParamFundJournalOnlyFundFrom").kendoMultiSelect({
        //            dataValueField: "FundPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,
        //        });
        //        $("#ParamFundJournalOnlyFundFrom").data("kendoMultiSelect").value("0");


        //    },

        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        LoadDataParamFund();

        WinFundJournalOnlyWithParamFund.center();
        WinFundJournalOnlyWithParamFund.open();

        //WinGenerateFundJournalOnlyWithParamFund.center();
        //WinGenerateFundJournalOnlyWithParamFund.open();

    }


    function onWinFundJournalOnlyWithParamFundClose() {
        //reset grid select
        for (var i in checkedIds) {
            checkedIds[i] = null
        }
    }



    //fund grid
    function LoadDataParamFund() {

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

        $("#gridParamFund").empty();
        var gridParamFund = $("#gridParamFund").kendoGrid({
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

        gridParamFund.table.on("click", ".checkbox", selectRow);

        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSize = gridParamFund.dataSource.pageSize();
            gridParamFund.dataSource.pageSize(gridParamFund.dataSource.data().length);

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

            gridParamFund.dataSource.pageSize(oldPageSize);

        });


        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridParamFund = $("#gridParamFund").data("kendoGrid"),
                dataItemZ = gridParamFund.dataItem(rowA);

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


    $("#BtnOkGenerateFundJournalOnlyWithParamFund").click(function () {

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

        //clearDataMulti();
        //var ArrayFundFrom = $("#ParamFundFrom").data("kendoMultiSelect").value();
        //var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var _date = kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        if (stringFundFrom == "") {
            alertify.alert("There's No Selected Data");
            return;
        }
        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/CheckInstrumentStatusPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            var EDT = {
                                FundFrom: stringFundFrom,
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolioParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                type: 'POST',
                                data: JSON.stringify(EDT),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data.Result == 1) {
                                        alertify.alert(data.Notes);
                                        $.unblockUI();
                                    }
                                    else {
                                        var EDT = {
                                            FundFrom: stringFundFrom,
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedempParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                            type: 'POST',
                                            data: JSON.stringify(EDT),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data.Result == 1) {
                                                    alertify.alert(data.Notes);
                                                    $.unblockUI();
                                                }
                                                else {
                                                    var EDT = {
                                                        FundFrom: stringFundFrom,
                                                    };
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateYesterdayParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                        type: 'POST',
                                                        data: JSON.stringify(EDT),
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {
                                                            if (data.Result == 1 || data.Result == 2) {
                                                                alertify.alert(data.Notes);
                                                                $.unblockUI();
                                                            }
                                                            else {
                                                                $.ajax({
                                                                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                    type: 'GET',
                                                                    contentType: "application/json;charset=utf-8",
                                                                    success: function (data) {
                                                                        if (data == false) {
                                                                            var EDT = {
                                                                                FundFrom: stringFundFrom,
                                                                            };

                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/EndDayTrails/ValidateCheckAccountFromFundAccountingSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                                                                type: 'POST',
                                                                                data: JSON.stringify(EDT),
                                                                                contentType: "application/json;charset=utf-8",
                                                                                success: function (data) {
                                                                                    if (data == false) {

                                                                                        $.ajax({
                                                                                            url: window.location.origin + "/Radsoft/CloseNav/ValidateCheckCloseNavBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                                            type: 'POST',
                                                                                            data: JSON.stringify(EDT),
                                                                                            contentType: "application/json;charset=utf-8",
                                                                                            success: function (data) {
                                                                                                if (data == "") {
                                                                                                    var EDT = {
                                                                                                        FundFrom: stringFundFrom,
                                                                                                    };
                                                                                                    $.ajax({
                                                                                                        url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateUnitYesterdayParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                                                        type: 'POST',
                                                                                                        data: JSON.stringify(EDT),
                                                                                                        contentType: "application/json;charset=utf-8",
                                                                                                        success: function (data) {
                                                                                                            if (data.Result == 1 || data.Result == 2) {
                                                                                                                alertify.alert(data.Notes);
                                                                                                                $.unblockUI();
                                                                                                            }
                                                                                                            else {

                                                                                                                $.ajax({
                                                                                                                    url: window.location.origin + "/Radsoft/EndDayTrails/GenerateFundJournalOnlyWithParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                                                                                                                    type: 'POST',
                                                                                                                    data: JSON.stringify(EDT),
                                                                                                                    contentType: "application/json;charset=utf-8",
                                                                                                                    success: function (data) {
                                                                                                                        alertify.alert("Generate End Day Trails Success");
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
                                                                                                        },
                                                                                                        error: function (data) {
                                                                                                            alertify.alert(data.responseText);
                                                                                                            $.unblockUI();
                                                                                                        }
                                                                                                    });
                                                                                                }
                                                                                                else {
                                                                                                    alertify.alert(data);
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
                                                                                        InitCheckAccountFromFundAccountingSetup();
                                                                                    }

                                                                                },
                                                                                error: function (data) {
                                                                                    alertify.alert(data.responseText);
                                                                                    $.unblockUI();
                                                                                }
                                                                            });

                                                                        }
                                                                        else {
                                                                            alertify.alert("Date is Holiday");
                                                                            $.unblockUI();
                                                                        }

                                                                    },
                                                                    error: function (data) {
                                                                        alertify.alert(data.responseText);
                                                                        $.unblockUI();
                                                                    }
                                                                });
                                                            }
                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                            $.unblockUI();
                                                        }
                                                    });
                                                }
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                                $.unblockUI();
                                            }
                                        });
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

            }
        });
    });



    function clearDataMulti() {
        $("#ParamFundFrom").val("");

    }

    function InitCheckAccountFromFundAccountingSetup() {
        $.unblockUI();
        var dsList = getDataCheckAccountFromFundAccountingSetup();
        $("#gridCheckAccountFromFundAccountingSetup").empty();
        $("#gridCheckAccountFromFundAccountingSetup").kendoGrid({
            dataSource: dsList,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: ""
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

                { field: "FundID", title: "FundID", width: 100 },
                { field: "AccountName", title: "AccountName", width: 500 },

            ]
        }).data("kendoGrid");


        WinCheckAccountFromFundAccountingSetup.center();
        WinCheckAccountFromFundAccountingSetup.open();
    }



    function getDataCheckAccountFromFundAccountingSetup() {

        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringFundFrom = '';

        //clearDataMulti();
        //var ArrayFundFrom = $("#ParamFundFrom").data("kendoMultiSelect").value();
        //var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        return new kendo.data.DataSource(
            {
                transport:
                {
                    read:
                    {
                        type: 'POST',
                        url: window.location.origin + "/Radsoft/EndDayTrails/CheckAccountFromFundAccountingSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    }, parameterMap: function (data, operation) {
                        return {
                            FundFrom: stringFundFrom,

                        }
                    },
                    dataType: "jsonp"
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
                            FundID: { type: "string" },
                            AccountName: { type: "string" },

                        }
                    }
                }
            });
    }

    $("#BtnVoidBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringEndDayTrailsSelected = '';

        for (var i in ArrayFundFrom) {
            stringEndDayTrailsSelected = stringEndDayTrailsSelected + ArrayFundFrom[i] + ',';

        }
        stringEndDayTrailsSelected = stringEndDayTrailsSelected.substring(0, stringEndDayTrailsSelected.length - 1)

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                var EndDayTrails = {
                    ValueDate: $('#FilterDate').val(),
                    VoidUsersID: sessionStorage.getItem("user"),
                    stringEndDayTrailsFrom: stringEndDayTrailsSelected
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/EndDayTrails/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(EndDayTrails),
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