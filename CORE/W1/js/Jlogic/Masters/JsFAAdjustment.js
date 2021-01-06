$(document).ready(function () {
    document.title = 'FORM FA Adjustment';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 330;
    var winFAAdjustmentDetail;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobStatus;

    var _defaultPeriodPK;

    $.ajax({
        url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            _defaultPeriodPK = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


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

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRevised").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRevise.png"
        });

        $("#BtnVoucher").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnRefreshDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAddDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnClose").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnImportFAAdjustment").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnNavForecast").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnTBForecast").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
    }
    

    function resetNotification() {
        $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
        alertify.set({
            labels: {
                ok: "OK",
                cancel: "Cancel"
            },
            delay: 4000,
            buttonReverse: false,
            buttonFocus: "ok"
        });
    }
    function initWindow() {
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo
        });

        function OnChangeValueDate() {


            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
        }
        function OnChangeDateFrom() {
        
                var _date = Date.parse($("#DateFrom").data("kendoDatePicker").value());
                if (!_date) {
                    
                    alertify.alert("Wrong Format Date DD/MM/YYYY");
                    return;
                }
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
         
            refresh();
        }
        function OnChangeDateTo() {
            var _date = Date.parse($("#DateTo").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            refresh();
        }

        // disini
        winFAAdjustmentDetail = $("#WinFAAdjustmentDetail").kendoWindow({
            height: 250,
            title: "* FAAdjustment DETAIL",
            visible: false,
            width: 650,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinFAAdjustmentDetailClose
        }).data("kendoWindow");

        win = $("#WinFAAdjustment").kendoWindow({
            height: 1500,
            title: "* FAAdjustment HEADER",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            }
        }).data("kendoWindow");

        WinNavForecast = $("#WinNavForecast").kendoWindow({
            height: 550,
            title: "* Nav Forecast",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinTBForecastGrid = $("#WinTBForecastGrid").kendoWindow({
            height: 550,
            title: "* TB Forecast Grid",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinTBForecast = $("#WinTBForecast").kendoWindow({
            height: 200,
            title: "* TB Forecast",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

    }
    var GlobValidatorFAAdjustment = $("#WinFAAdjustment").kendoValidator().data("kendoValidator");
    function validateDataFAAdjustment() {
        
        if (GlobValidatorFAAdjustment.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    var GlobValidatorFAAdjustmentDetail = $("#WinFAAdjustmentDetail").kendoValidator().data("kendoValidator");
    function validateDataFAAdjustmentDetail() {
        
        if (GlobValidatorFAAdjustmentDetail.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    function showDetails(e) {
        var dataItemX;
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#TrxInformation").hide();
            $("#BtnPosting").hide();
            $("#BtnRevised").hide();
            $("#ValueDate").data("kendoDatePicker").value(_d);
            GlobStatus = 0;
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            if (tabindex == 0 || tabindex == undefined) {
                var grid = $("#gridFAAdjustmentApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {
                var grid = $("#gridFAAdjustmentPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {
                var grid = $("#gridFAAdjustmentHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevised").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnVoid").hide();
                $("#BtnRevised").show();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnRevised").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnPosting").show();
                $("#BtnRevised").hide();
                $("#TrxInformation").show();
                $("#BtnUpdate").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevised").hide();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevised").hide();
                $("#BtnOldData").hide();
            }
            dirty = null;

            $("#FAAdjustmentPK").val(dataItemX.FAAdjustmentPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Status").val(dataItemX.Status);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            $("#Reference").val(dataItemX.Reference);
            $("#Description").val(dataItemX.Description);
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Revised == false) {
                $("#State").removeClass("Posted").removeClass("Revised").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");

            }
            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Revised").addClass("Posted");
                $("#State").text("POSTED");
            }
            if (dataItemX.Revised == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Revised");
                $("#State").text("Revised");
            }
            $("#PostedBy").text(dataItemX.PostedBy);
            $("#RevisedBy").text(dataItemX.RevisedBy);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            if (dataItemX.RevisedTime == null) {
                $("#RevisedTime").text("");
            } else {
                $("#RevisedTime").text(kendo.toString(kendo.parseDate(dataItemX.RevisedTime), 'MM/dd/yyyy HH:mm:ss'));
            }
            if (dataItemX.PostedTime == null) {
                $("#PostedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
            $("#gridFAAdjustmentDetail").empty();

            initGridFAAdjustmentDetail();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: false,
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
        }

        function setCmbPeriodPK() {
            if (e == null) {
                return _defaultPeriodPK;
            } else {
                return dataItemX.PeriodPK;
            }
        }

      
        win.center();
        win.open();

    }
    function onPopUpClose() {
            clearData()
            showButton();
            $("#gridFAAdjustmentDetail").empty();
            refresh();
    }
    function clearData() {
        GlobValidatorFAAdjustment.hideMessages();
        $("#FAAdjustmentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#Reference").val("");
        $("#Description").val("");
        $("#Revised").prop('checked', false);
        $("#RevisedBy").val("");
        $("#RevisedTime").val("");
        $("#Posted").prop('checked', false);
        $("#PostedBy").val("");
        $("#PostedTime").val("");
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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             FAAdjustmentPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             Reference: { type: "string" },
                             Description: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Revised: { type: "boolean" },
                             RevisedBy: { type: "string" },
                             RevisedTime: { type: "date" },
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
            initGrid()
        }
        if (tabindex == 1) {
            RecalGridPending();
        }
        if (tabindex == 2) {
            RecalGridHistory();
        }

    }
  
    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };
    function gridApprovedOnDataBound() {
        var grid = $("#gridFAAdjustmentApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            } else if (row.Revised == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }
    function gridHistoryDataBound() {
        var grid = $("#gridFAAdjustmentHistory").data("kendoGrid");
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
        
        $("#gridFAAdjustmentApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FAAdjustmentApprovedURL = window.location.origin + "/Radsoft/FAAdjustment/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(FAAdjustmentApprovedURL);
        }

        //$("#gridFAAdjustmentDetail").empty();
        var grid = $("#gridFAAdjustmentApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FAAdjustment"
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
            detailInit: FAAdjustmentDetailGrid,
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "FAAdjustmentPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "RefNo", title: "RefNo", width: 200 },
               { field: "Reference", title: "Reference", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "PeriodID", title: "Period ID", width: 200 },
               { field: "PostedBy ", title: "Posted By", width: 200 },
               { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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
            

            var grid = $("#gridFAAdjustmentApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _FAAdjustmentPK = dataItemX.FAAdjustmentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _FAAdjustmentPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFAAdjustment").kendoTabStrip({
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

    function ResetButtonBySelectedData()
    {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnVoidBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FAAdjustment/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FAAdjustment/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridFAAdjustmentPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FAAdjustmentPendingURL = window.location.origin + "/Radsoft/FAAdjustment/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(FAAdjustmentPendingURL);
        }
        var grid = $("#gridFAAdjustmentPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FAAdjustment"
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
            detailInit: FAAdjustmentDetailGrid,
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
              { field: "FAAdjustmentPK", title: "SysNo.", width: 95 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "PostedBy ", title: "Posted By", width: 200 },
              { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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
            

            var grid = $("#gridFAAdjustmentPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _FAAdjustmentPK = dataItemX.FAAdjustmentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _FAAdjustmentPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }
    function RecalGridHistory() {
        $("#gridFAAdjustmentHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FAAdjustmentHistoryURL = window.location.origin + "/Radsoft/FAAdjustment/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(FAAdjustmentHistoryURL);
        }
        $("#gridFAAdjustmentHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FAAdjustment"
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
            dataBound: gridHistoryDataBound,
            resizable: true,
            toolbar: ["excel"],
            detailInit: FAAdjustmentDetailGrid,
            columns: [
              { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
              { field: "FAAdjustmentPK", title: "SysNo.", width: 95 },
              { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "StatusDesc", title: "StatusDesc", width: 200 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "PostedBy ", title: "Posted By", width: 200 },
              { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }
    // bagian FAAdjustmentDetail
    function refreshFAAdjustmentDetailGrid() {
        var gridFAAdjustmentDetail = $("#gridFAAdjustmentDetail").data("kendoGrid");
        gridFAAdjustmentDetail.dataSource.read();
    }
    function getDataSourceFAAdjustmentDetail(_url) {
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
                 aggregate: [{ field: "Amount", aggregate: "sum" },
                     { field: "Debit", aggregate: "sum" },
                     { field: "Credit", aggregate: "sum" },
                     { field: "BaseDebit", aggregate: "sum" },
                     { field: "BaseCredit", aggregate: "sum" }],

                 schema: {
                     model: {
                         fields: {
                             FAAdjustmentPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             FACOAAdjustmentPK: { type: "number" },
                             FACOAAdjustmentID: { type: "string" },
                             FACOAAdjustmentName: { type: "string" },
                             FundJournalAccountPK: { type: "number" },
                             FundJournalAccountID: { type: "string" },
                             FundJournalAccountName: { type: "string" },                        
                             DebitCredit: { type: "string" },
                             Amount: { type: "number" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showFAAdjustmentDetail(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#DAutoNo").val("");
            $("#TRAccount").hide();
            $("#TrDebitCredit").hide();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridFAAdjustmentDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#DAutoNo").val(dataItemX.AutoNo);
            $("#DLastUsersID").val(dataItemX.LastUsersID);
            $("#DDebitCredit").val(dataItemX.DebitCredit);
            $("#DAmount").val(dataItemX.Amount);
            $("#DLastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));

            $("#TRAccount").show();
            $("#TrDebitCredit").show();

            $.ajax({
                url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#DFundJournalAccountPK").kendoComboBox({
                        dataValueField: "FundJournalAccountPK",
                        dataTextField: "ID",
                        filter: "contains",
                        suggest: true,
                        enabled: false,
                        dataSource: data,
                        change: onChangeDFundJournalAccountPK,
                        value: setDFundJournalAccountPK()
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            function setDFundJournalAccountPK() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.FundJournalAccountPK;
                }
            }

            function onChangeDFundJournalAccountPK() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                    return;
                }


            }
        }

        //Combo box Fund
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
                    change: onChangeDFundPK,
                    value: setDFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setDFundPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FundPK;
            }
        }

        function onChangeDFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
                return;
            }


        }

        //Combo box FACOAAdjustmentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FACOAAdjustment/GetFACOAAdjustmentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFACOAAdjustmentPK").kendoComboBox({
                    dataValueField: "FACOAAdjustmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeDFACOAAdjustmentPK,
                    value: setDFACOAAdjustmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setDFACOAAdjustmentPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FACOAAdjustmentPK;
            }
        }

        function onChangeDFACOAAdjustmentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
                return;
            }


        }

   

        $("#DAmount").kendoNumericTextBox({
            format: "n0",
            change: onChangeDAmount,
        });

        winFAAdjustmentDetail.center();
        winFAAdjustmentDetail.open();

    }
    function DeleteFAAdjustmentDetail(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        
    
            var dataItemX;
            var grid = $("#gridFAAdjustmentDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            alertify.confirm("Are you sure want to DELETE detail ?", function (e) {
                if (e) {

                    var FAAdjustmentDetail = {
                        FAAdjustmentPK: dataItemX.FAAdjustmentPK,
                        FACOAAdjustmentPK: dataItemX.FACOAAdjustmentPK,
                        LastUsersID: sessionStorage.getItem("user"),
                        AutoNo: dataItemX.AutoNo
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FAAdjustmentDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustmentDetail_D",
                        type: 'POST',
                        data: JSON.stringify(FAAdjustmentDetail),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            refreshFAAdjustmentDetailGrid();
                            alertify.alert(data.Message);
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        
    }
    function initGridFAAdjustmentDetail() {
        var FAAdjustmentDetailURL = window.location.origin + "/Radsoft/FAAdjustmentDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#FAAdjustmentPK").val(),
          dataSourceFAAdjustmentDetail = getDataSourceFAAdjustmentDetail(FAAdjustmentDetailURL);

        $("#gridFAAdjustmentDetail").kendoGrid({
            dataSource: dataSourceFAAdjustmentDetail,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FAAdjustment Detail"
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
                   command: { text: "show", click: showFAAdjustmentDetail }, title: " ", width: 80, locked: true, lockable: false
                },
                {
                   command: { text: "Delete", click: DeleteFAAdjustmentDetail }, title: " ", width: 80, locked: true, lockable: false
                },
                {
                   field: "AutoNo", title: "No.", filterable: false, width: 70, locked: true, lockable: false
                },
                     { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                {
                    field: "FundName", title: "Fund Name", width: 200, locked: true, lockable: false
                },
                {
                    field: "FACOAAdjustmentPK", title: "FA COA Adjustment", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FACOAAdjustmentID", title: "FA COA Adjustment ID", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FACOAAdjustmentName", title: "COA Adj.Name", width: 240, locked: true, lockable: false
                },
                {
                    field: "FundJournalAccountID", title: "Fund Journal Account ID", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
                },
                { field: "DebitCredit", title: "D/C", width: 85 },
                { field: "Amount", title: "Amount", width: 150, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "LastUsersID", title: "LastUsersID", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    function FAAdjustmentDetailGrid(e) {
        var FAAdjustmentDetailURL = window.location.origin + "/Radsoft/FAAdjustmentDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.FAAdjustmentPK,
         dataSourceFAAdjustmentDetail = getDataSourceFAAdjustmentDetail(FAAdjustmentDetailURL);

        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: dataSourceFAAdjustmentDetail,
            height: 250,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FAAdjustment Detail"
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
                   field: "AutoNo", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
                     { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "FundID", title: "Fund ID", width: 250, locked: true, lockable: false, hidden: true,
               },
                {
                    field: "FundName", title: "Fund Name", width: 200, locked: true, lockable: false
                },
                {
                    field: "FACOAAdjustmentPK", title: "FA COA Adjustment", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FACOAAdjustmentID", title: "FA COA Adjustment ID", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FACOAAdjustmentName", title: "COA Adj.Name", width: 240, locked: true, lockable: false
                },
                {
                    field: "FundJournalAccountID", title: "Fund Journal Account ID", width: 250, locked: true, lockable: false, hidden: true,
                },
                {
                    field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
                },
                { field: "DebitCredit", title: "D/C", width: 85 },
                { field: "Amount", title: "Amount", width: 150, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "LastUsersID", title: "LastUsersID", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }
    function onChangeDAmount() {
        //recalAmount(this.value());
    }
    function onChangeDDebitCredit() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        //recalAmount($("#DAmount").data("kendoNumericTextBox").value());
    }
    //function recalAmount(_amount) {
    //    if ($("#DDebitCredit").data("kendoComboBox").value() == 'D') {
    //        $("#DDebit").data("kendoNumericTextBox").value(_amount);
    //        $("#DCredit").data("kendoNumericTextBox").value(0);
    //        $("#DBaseDebit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
    //        $("#DBaseCredit").data("kendoNumericTextBox").value(0);
    //    } else if ($("#DDebitCredit").data("kendoComboBox").value() == 'C') {
    //        $("#DDebit").data("kendoNumericTextBox").value(0);
    //        $("#DCredit").data("kendoNumericTextBox").value(_amount);
    //        $("#DBaseDebit").data("kendoNumericTextBox").value(0);
    //        $("#DBaseCredit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
    //    } else if (_amount > 0) {
    //        alertify.alert("Please Choose Debit or Credit");
    //    }
    //}
    function onWinFAAdjustmentDetailClose() {
        GlobValidatorFAAdjustmentDetail.hideMessages();
        $("#DFundPK").val("");
        $("#DFACOAAdjustmentPK").val("");
        $("#DFundJournalAccountPK").val("");  
        $("#DDebitCredit").val("");
        $("#DAmount").val("");
        $("#DLastUsersID").val("");
        $("#DLastUpdate").val("");
    }
    //function onWinFAAdjustmentPostingClose() {

    //    $("#Posting").val("");
    //    $("#PostPeriodPK").val("");
    //    $("#PostDateFrom").val("");
    //    $("#PostDateTo").val("");
    //    $("#PostFAAdjustmentFrom").val("");
    //    $("#PostFAAdjustmentTo").val("");
    //    $("#LblPostDateFrom").hide();
    //    $("#CmbPostDateFrom").hide();
    //    $("#LblPostDateTo").hide();
    //    $("#LblPostFAAdjustmentFrom").show();
    //    $("#LblPostFAAdjustmentTo").show();
    //    refresh();
    //}

    $("#BtnRefreshDetail").click(function () {
        refreshFAAdjustmentDetailGrid();
    });
    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FAAdjustmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "FAAdjustment",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FAAdjustment" + "/" + $("#FAAdjustmentPK").val(),
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
    $("#BtnRefresh").click(function () {
        refresh();

    });
    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnClose").click(function () {
        
        alertify.confirm("Are you sure want to Close and Clear Detail?", function (e) {
            if (e) {
                winFAAdjustmentDetail.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnNew").click(function () {
        showDetails(null);
    });

    $("#BtnAddDetail").click(function () {
        
        if ($("#FAAdjustmentPK").val() == 0 || $("#FAAdjustmentPK").val() == null) {
            alertify.alert("There's no FA Adjustment Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("FAAdjustment Already History");
        } else {
            showFAAdjustmentDetail();
        }
    });
    $("#BtnSave").click(function () {
        var val = validateDataFAAdjustmentDetail();
        if (val == 1) {
            
            if ($("#DAutoNo").val() == "") {
                alertify.confirm("Are you sure want to Add FA Adjustment DETAIL ?", function (e) {
                    if (e) {
                        var FAAdjustmentDetail = {
                            FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                            AutoNo: $('#DAutoNo').val(),
                            Status: 2,
                            FundPK: $('#DFundPK').val(),
                            FACOAAdjustmentPK: $('#DFACOAAdjustmentPK').val(),
                            FundJournalAccountPK: $('#DFundJournalAccountPK').val(),  
                            DebitCredit: $('#DDebitCredit').val(),
                            Amount: $('#DAmount').val(),
                            LastUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FAAdjustmentDetail/InsertFAAdjustmentDealingMapping/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustmentDetail_I",
                            type: 'POST',
                            data: JSON.stringify(FAAdjustmentDetail),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                $("#gridFAAdjustmentDetail").empty();
                                initGridFAAdjustmentDetail();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                });
            } else {
                alertify.confirm("Are you sure want to UPDATE FAAdjustment DETAIL ?", function (e) {
                    if (e) {

                        var FAAdjustmentDetail = {
                            FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                            AutoNo: $('#DAutoNo').val(),
                            Status: 2,
                            FundPK: $('#DFundPK').val(),
                            FACOAAdjustmentPK: $('#DFACOAAdjustmentPK').val(),
                            FundJournalAccountPK: $('#DFundJournalAccountPK').val(),
                            DebitCredit: $('#DDebitCredit').val(),
                            Amount: $('#DAmount').val(),
                            LastUsersID: sessionStorage.getItem("user")
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FAAdjustmentDetail/UpdateFAAdjustmentDealingMapping/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustmentDetail_U",
                            type: 'POST',
                            data: JSON.stringify(FAAdjustmentDetail),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                $("#gridFAAdjustmentDetail").empty();
                                initGridFAAdjustmentDetail();
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
    $("#BtnResetFilter").click(function () {
        $("#DateFrom").data("kendoDatePicker").value(null);
        $("#DateTo").data("kendoDatePicker").value(null);
        refresh();
    });
    $("#BtnAdd").click(function () {
        if ($("#FAAdjustmentPK").val() > 0) {
            alertify.alert("FAAdjustment HEADER ALREADY EXIST, Cancel and click add new to add more Header");
            return
        }
        var val = validateDataFAAdjustment();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add FAAdjustment HEADER ?", function (e) {
                if (e) {
                    if ($('#Reference').val() == null || $('#Reference').val() == "") {
                        setTimeout(function () {
                            alertify.confirm("Are you sure want to Generate New Reference ?", function (e) {
                                if (e) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/FundJournalReference/FundJournalReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ADJ/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "FundJournalReference_GenerateNewReference",
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            $("#Reference").val(data);
                                            //alertify.alert("Your new reference is " + data);
                                            var FAAdjustment = {
                                                ValueDate: $('#ValueDate').val(),
                                                PeriodPK: $("#PeriodPK").val(),
                                                Reference: data,
                                                Description: $('#Description').val(),
                                                EntryUsersID: sessionStorage.getItem("user")

                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_I",
                                                type: 'POST',
                                                data: JSON.stringify(FAAdjustment),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    alertify.alert(data.Message);
                                                    $("#FAAdjustmentPK").val(data.FAAdjustmentPK);
                                                    $("#HistoryPK").val(data.HistoryPK);
                                                    refresh();
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
                            })
                        }, 1000);;
                    }
                    else {
                        var FAAdjustment = {
                            ValueDate: $('#ValueDate').val(),
                            PeriodPK: $("#PeriodPK").val(),
                            Reference: $("#Reference").val(),
                            Description: $('#Description').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_I",
                            type: 'POST',
                            data: JSON.stringify(FAAdjustment),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data.Message);
                                $("#FAAdjustmentPK").val(data.FAAdjustmentPK);
                                $("#HistoryPK").val(data.HistoryPK);
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateDataFAAdjustment();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FAAdjustmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "FAAdjustment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FAAdjustment = {
                                    FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    PeriodPK: $("#PeriodPK").val(),
                                    Reference: $('#Reference').val(),
                                    Description: $('#Description').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_U",
                                    type: 'POST',
                                    data: JSON.stringify(FAAdjustment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
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
    $("#BtnApproved").click(function () {
        
        if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
            alertify.alert("FAAdjustment Not Balance");
            return;
        }
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FAAdjustmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "FAAdjustment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FAAdjustment = {
                                FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_A",
                                type: 'POST',
                                data: JSON.stringify(FAAdjustment),
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
            if ($("#State").text() == "POSTED" || $("#State").text() == "Revised") {
                alertify.alert("FAAdjustment Already Posted / Revised, Void Cancel");
            } else {
                if (e) {
                    var FAAdjustment = {
                        FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_V",
                        type: 'POST',
                        data: JSON.stringify(FAAdjustment),
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
            }
        });
    });
    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var FAAdjustment = {
                    FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_R",
                    type: 'POST',
                    data: JSON.stringify(FAAdjustment),
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

    $("#BtnDownloadMaster").click(function () {
        
        alertify.confirm("Are you sure want to Download data Master ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/MasterDataReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.location = data
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnPosting").click(function () {
        
        if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
            alertify.alert("FAAdjustment Not Balance");
            return;
        }
        alertify.confirm("Are you sure want to Posting FAAdjustment?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "Revised") {
                alertify.alert("FAAdjustment Already Posted / Revised, Posting Cancel");
            } else {
                if (e) {
                    if (dirty == true) {
                        alertify.alert("Change must be Update First, Posting Cancel");
                        return;
                    }

                    if ($("#Posted").is(":checked")) {
                        alert("FAAdjustment already Posted");
                        return;
                    }

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FAAdjustmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "FAAdjustment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var FAAdjustment = {
                                    FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PostedBy: sessionStorage.getItem("user"),
                                    PeriodPK: $('#PeriodPK').val()
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_Posting",
                                    type: 'POST',
                                    data: JSON.stringify(FAAdjustment),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("Reserved").addClass("Posted");
                                        $("#State").text("POSTED");
                                        $("#PostedBy").text(sessionStorage.getItem("user"));
                                        $("#Posted").prop('checked', true);
                                        $("#Revised").prop('checked', false);
                                        alertify.alert(data);
                                        win.close();
                                        refresh()
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
            }
        });
    });

    //function showPosting(e) {
    //    if (e.handled !== true) // This will prevent event triggering more then once
    //    {
    //        
    //        alertify.confirm("Are you sure want to Posting ?", function (a) {
    //            if (a) {
    //                var grid = $("#gridFAAdjustmentApproved").data("kendoGrid");
    //                var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
    //                var pos = _dataItemX.Posted;

    //                if (pos == true) {
    //                    alertify.alert("Data Already Posted");
    //                }
    //                else {

    //                    var _FAAdjustmentPK = _dataItemX.FAAdjustmentPK;
    //                    var _periodPK = _dataItemX.PeriodPK;
    //                    var _historyPK = _dataItemX.HistoryPK;
    //                    var Posting = {
    //                        FAAdjustmentPK: _FAAdjustmentPK,
    //                        PeriodPK: _periodPK,
    //                        HistoryPK: _historyPK,
    //                        PostedBy: sessionStorage.getItem("user")
    //                    };



    //                    $.ajax({
    //                        url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_Posting",
    //                        type: 'POST',
    //                        data: JSON.stringify(Posting),
    //                        contentType: "application/json;charset=utf-8",
    //                        success: function (data) {
    //                            alertify.alert(data);
    //                            refresh();
    //                        },
    //                        error: function (data) {
    //                            alertify.alert(data.responseText);
    //                        }
    //                    });

    //                }



    //            } else {
    //                alertify.alert("You've clicked Cancel");
    //            }
    //        });
    //        e.handled = true;
    //    }
    //}
    $("#BtnRevised").click(function () {
        
        alertify.confirm("Are you sure want to Revised FAAdjustment?", function (e) {

            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FAAdjustmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "FAAdjustment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FAAdjustment = {
                                FAAdjustmentPK: $('#FAAdjustmentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                RevisedBy: sessionStorage.getItem("user"),
                                PeriodPK: $('#PeriodPK').val()
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_Revised",
                                type: 'POST',
                                data: JSON.stringify(FAAdjustment),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                                    $("#State").text("Revised");
                                    $("#RevisedBy").text(sessionStorage.getItem("user"));
                                    $("#Revised").prop('checked', true);
                                    $("#Posted").prop('checked', false);
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
    function showRevised(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Revised ?", function (a) {
                if (a) {
                    var grid = $("#gridFAAdjustmentApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    var _FAAdjustmentPK = _dataItemX.FAAdjustmentPK;
                    var _periodPK = _dataItemX.PeriodPK;
                    var _historyPK = _dataItemX.HistoryPK;
                    var Revised = {
                        FAAdjustmentPK: _FAAdjustmentPK,
                        PeriodPK: _periodPK,
                        HistoryPK: _historyPK,
                        RevisedBy: sessionStorage.getItem("user")

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/FAAdjustment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FAAdjustment_Revised",
                        type: 'POST',
                        data: JSON.stringify(Revised),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });


                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }

    function validateDataPosting() {
        if ($("#PostDateFrom").data("kendoDatePicker").value() > $("#PostDateTo").data("kendoDatePicker").value()) {
            alertify.alert("Date From >  Date To");
            return 0;
        }
        else if ($("#PostFAAdjustmentFrom").data("kendoComboBox").value() > $("#PostFAAdjustmentTo").data("kendoComboBox").value()) {
            alertify.alert("FAAdjustment From >  FAAdjustment To");
            return 0;
        }

        else {
            return 1;
        }
    }
    $("#BtnVoucher").click(function () {
        
        alertify.confirm("Are you sure want to Download FAAdjustment Voucher ?", function (e) {
            if (e) {
                var FAAdjustment = {
                    FAAdjustmentPK: $('#FAAdjustmentPK').val()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FAAdjustment/FAAdjustmentVoucher/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FAAdjustment),
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

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                
                 $.ajax({
                     url: window.location.origin + "/Radsoft/FAAdjustment/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")  + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FAAdjustment/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FAAdjustment/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
    $("#BtnPostingBySelected").click(function () {
        
        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FAAdjustment/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
    
    $("#BtnNavForecast").click(function () {
        
        GenerateNavForecastGrid();
        WinNavForecast.center();
        WinNavForecast.open();
    });

    function getDataSourceNAVForecast(_url) {
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
                         }
                     }
                 }
             });
    }

    var gridData;
    function GenerateNavForecastGrid() {
        $("#gridDataNavForecast").empty();
        var URL = window.location.origin + "/Radsoft/FAAdjustment/GenerateNavForecast/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceNAVForecast(URL);
            gridData = $("#gridDataNavForecast").kendoGrid({
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
            toolbar: kendo.template($("#gridDataNavForecast").html()),
            columns: [
                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "FundName", title: "FundName", width: 200 },
                { field: "Nav", title: "Nav Forecast", format: "{0:n4}", width: 150 },
                { field: "AUM", title: "AUM Forecast", format: "{0:n2}", width: 150 },
            ]
        }).data("kendoGrid");
    }


    $("#BtnTBForecast").click(function () {
        




        // ParamFund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFund").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
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

     

        WinTBForecast.center();
        WinTBForecast.open();
    });

    $("#BtnOkTBForecast").click(function () {
        
        alertify.confirm("Are you sure want to generate TB Forecast ?", function (e) {
            if (e) {
                GenerateTBForecastGrid(kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yy"), $('#ParamFund').val());
                WinTBForecastGrid.center();
                WinTBForecastGrid.open();
            }
        });
    });

    $("#BtnCancelTBForecast").click(function () {
        
        alertify.confirm("Are you sure want to cancel TB Forecast?", function (e) {
            if (e) {
                WinTBForecast.close();
                alertify.alert("Cancel TB Forecast");
            }
        });
    });

    function onWinApproveMKBDTrailsClose() {
        $("#ParamDate").val("");
        $("#ParamFund").val("");

    }

    function getDataSourceTBForecast(_url) {
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
                 aggregate: [{ field: "Amount", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             ValueDate: { type: "date" },
                             FundName: { type: "string" },
                             AccountID: { type: "string" },
                             AccountName: { type: "string" },
                             Amount: { type: "number" },
                         }
                     }
                 }
             });
    }

    var gridData;
    function GenerateTBForecastGrid(_paramDate,_paramFund) {
        $("#gridDataTBForecast").empty();
        var URL = window.location.origin + "/Radsoft/FAAdjustment/GenerateTBForecast/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _paramDate + "/" + _paramFund,
            dataSourceApproved = getDataSourceTBForecast(URL);
            gridData = $("#gridDataTBForecast").kendoGrid({
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
            toolbar: kendo.template($("#gridDataTBForecast").html()),
            columns: [
                { field: "ValueDate", title: "Value Date", width: 150, hidden: true ,template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "FundName", title: "FundName", width: 200 , hidden: true},
                { field: "AccountID", title: "Account ID", width: 150 },
                { field: "AccountName", title: "Account Name", width: 300 },
                { field: "Amount", title: "Amount", format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>", width: 150, attributes: { style: "text-align:right;" } },
            ]
        }).data("kendoGrid");
    }

});