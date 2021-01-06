$(document).ready(function () {
    document.title = 'FORM FUND JOURNAL';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 330;
    var winFundJournalDetail;
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

        $("#BtnReversed").kendoButton({
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
        $("#BtnReverseBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReverseAll.png"
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

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
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
            change: OnChangeValueDate
        });

        $("#DateFrom").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            value: new Date(),
            change: OnChangeDateTo
        });

        function OnChangeValueDate() {
            var _date = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
        }
        function OnChangeDateFrom() {
            
            var currentDate = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            
            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }


        // disini
        winFundJournalDetail = $("#WinFundJournalDetail").kendoWindow({
            height: 450,
            title: "* FUND JOURNAL DETAIL",
            visible: false,
            width: 1280,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinFundJournalDetailClose
        }).data("kendoWindow");

        win = $("#WinFundJournal").kendoWindow({
            height: 1500,
            title: "* FUND JOURNAL HEADER",
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

    }
    var GlobValidatorFundJournal = $("#WinFundJournal").kendoValidator().data("kendoValidator");
    function validateDataFundJournal() {
        
        if ($("#ValueDate").val() != "") {
            var _date = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
        }
        if (GlobValidatorFundJournal.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    var GlobValidatorFundJournalDetail = $("#WinFundJournalDetail").kendoValidator().data("kendoValidator");
    function validateDataFundJournalDetail() {
        
        if (GlobValidatorFundJournalDetail.validate()) {
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
            $("#BtnReversed").hide();
            $("#ValueDate").data("kendoDatePicker").value(_d);
            $("#BtnOldData").hide();
            GlobStatus = 0;
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            if (tabindex == 0 || tabindex == undefined) {
                var grid = $("#gridFundJournalApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {
                var grid = $("#gridFundJournalPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {
                var grid = $("#gridFundJournalHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Reversed == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnVoid").hide();
                $("#BtnReversed").show();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Reversed == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Reversed == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnPosting").show();
                $("#BtnReversed").hide();
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
                $("#BtnReversed").hide();
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
                $("#BtnReversed").hide();
                $("#BtnOldData").hide();
            }

            if (_GlobClientCode == "11") {
                $("#BtnUpdate").show();
            }

    

            $("#FundJournalPK").val(dataItemX.FundJournalPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Status").val(dataItemX.Status);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            $("#TrxNo").val(dataItemX.TrxNo);
            $("#TrxName").val(dataItemX.TrxName);
            $("#Reference").val(dataItemX.Reference);
            $("#Type").val(dataItemX.Type);
            $("#Description").val(dataItemX.Description);
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Reversed == false) {
                $("#State").removeClass("Posted").removeClass("Reversed").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");

            }
            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Reversed").addClass("Posted");
                $("#State").text("POSTED");
            }
            if (dataItemX.Reversed == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Reversed");
                $("#State").text("REVERSED");
            }
            $("#PostedBy").text(dataItemX.PostedBy);
            $("#ReversedBy").text(dataItemX.ReversedBy);
            $("#ReverseNo").val(dataItemX.ReverseNo);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            if (dataItemX.ReversedTime == null) {
                $("#ReversedTime").text("");
            } else {
                $("#ReversedTime").text(kendo.toString(kendo.parseDate(dataItemX.ReversedTime), 'MM/dd/yyyy HH:mm:ss'));
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
            $("#gridFundJournalDetail").empty();

            initGridFundJournalDetail();
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

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundJournalType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: OnChangeType,
                    value: setCmbType(),
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
                }
            }
        }

        win.center();
        win.open();

    }
    function onPopUpClose() {
        clearData()
        showButton();
        $("#gridFundJournalDetail").empty();
        if (_GlobClientCode == "11") {
            //refresh();
        }
        else {
            refresh();
        }

    }

    function clearData() {
        GlobValidatorFundJournal.hideMessages();
        $("#FundJournalPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#TrxNo").val("");
        $("#TrxName").val("");
        $("#Reference").val("");
        $("#Type").val("");
        $("#Description").val("");
        $("#Reversed").prop('checked', false);
        $("#ReverseNo").val("");
        $("#ReverseBy").val("");
        $("#ReversedTime").val("");
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
                 pageSize: 100,
                 schema: {
                     model: {
                         fields: {
                             FundJournalPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             TrxNo: { type: "string" },
                             TrxName: { type: "string" },
                             Reference: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             Description: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Reversed: { type: "boolean" },
                             ReverseNo: { type: "number" },
                             ReversedBy: { type: "string" },
                             ReversedTime: { type: "date" },
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
        var grid = $("#gridFundJournalApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            } else if (row.Reversed == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }
    function gridHistoryDataBound() {
        var grid = $("#gridFundJournalHistory").data("kendoGrid");
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
        
        $("#gridFundJournalApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FundJournalApprovedURL = window.location.origin + "/Radsoft/FundJournal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(FundJournalApprovedURL);
        }

        //$("#gridFundJournalDetail").empty();
        var grid = $("#gridFundJournalApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Journal"
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
            detailInit: FundJournalDetailGrid,
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
               //{ command: { text: "Reverse", click: showReverse }, title: " ", width: 90 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "FundJournalPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "RefNo", title: "RefNo", width: 200 },
               { field: "Reference", title: "Reference", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
               { field: "TrxName", title: "Trx Name", width: 200 },
               { field: "Type", title: "Type", hidden: true, width: 120 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "PeriodID", title: "Period ID", width: 200 },
               { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
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
            

            var grid = $("#gridFundJournalApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _FundJournalPK = dataItemX.FundJournalPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _FundJournalPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundJournal").kendoTabStrip({
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

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnVoidBySelected").show();
        $("#BtnReverseBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundJournal/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundJournal/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridFundJournalPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FundJournalPendingURL = window.location.origin + "/Radsoft/FundJournal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(FundJournalPendingURL);
        }
        var grid = $("#gridFundJournalPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Journal"
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
            detailInit: FundJournalDetailGrid,
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
              { field: "FundJournalPK", title: "SysNo.", width: 95 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "TrxName", title: "Trx Name", width: 200 },
              { field: "Type", title: "Type", hidden: true, width: 120 },
              { field: "TypeDesc", title: "Type", width: 200 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
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
            

            var grid = $("#gridFundJournalPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _FundJournalPK = dataItemX.FundJournalPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _FundJournalPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnReverseBySelected").hide();
    }
    function RecalGridHistory() {
        $("#gridFundJournalHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var FundJournalHistoryURL = window.location.origin + "/Radsoft/FundJournal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(FundJournalHistoryURL);
        }
        $("#gridFundJournalHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Journal"
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
            detailInit: FundJournalDetailGrid,
            columns: [
              { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
              { field: "FundJournalPK", title: "SysNo.", width: 95 },
              { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "StatusDesc", title: "StatusDesc", width: 200 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "TrxName", title: "Trx Name", width: 200 },
              { field: "Type", title: "Type", hidden: true, width: 120 },
              { field: "TypeDesc", title: "Type", width: 200 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
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
        $("#BtnReverseBySelected").hide();
    }
    // bagian FundJournalDetail
    function refreshFundJournalDetailGrid() {
        var gridFundJournalDetail = $("#gridFundJournalDetail").data("kendoGrid");
        gridFundJournalDetail.dataSource.read();
    }
    function getDataSourceFundJournalDetail(_url) {
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
                 aggregate: [{ field: "Amount", aggregate: "sum" },
                     { field: "Debit", aggregate: "sum" },
                     { field: "Credit", aggregate: "sum" },
                     { field: "BaseDebit", aggregate: "sum" },
                     { field: "BaseCredit", aggregate: "sum" }],

                 schema: {
                     model: {
                         fields: {
                             FundJournalPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             FundJournalAccountPK: { type: "number" },
                             FundJournalAccountID: { type: "string" },
                             FundJournalAccountName: { type: "string" },
                             CurrencyPK: { type: "number" },
                             CurrencyID: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             DetailDescription: { type: "string" },
                             DebitCredit: { type: "string" },
                             Amount: { type: "number" },
                             Debit: { type: "number" },
                             Credit: { type: "number" },
                             CurrencyRate: { type: "number" },
                             BaseDebit: { type: "number" },
                             BaseCredit: { type: "number" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showFundJournalDetail(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#DAutoNo").val("");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridFundJournalDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#DAutoNo").val(dataItemX.AutoNo);
            $("#DDetailDescription").val(dataItemX.DetailDescription);
            $("#DLastUsersID").val(dataItemX.LastUsersID);
            $("#DDebitCredit").val(dataItemX.DebitCredit);
            $("#DAmount").val(dataItemX.Amount);
            $("#DDebit").val(dataItemX.Debit);
            $("#DCredit").val(dataItemX.Credit);
            $("#DCurrencyRate").val(dataItemX.CurrencyRate);
            $("#DBaseDebit").val(dataItemX.BaseDebit);
            $("#DBaseCredit").val(dataItemX.BaseCredit);
            $("#DLastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }

        //Combo box AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundJournalAccountPK").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: onChangeDFundJournalAccountPK,
                    filter: "contains",
                    suggest: true,
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
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalAccount/FundJournalAccount_LookupByFundJournalAccountPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DFundJournalAccountPK").data("kendoComboBox").value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#DCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                        var LastRate = {
                            Date: $('#ValueDate').val(),
                            CurrencyPK: data.CurrencyPK,
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(LastRate),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $("#DCurrencyRate").data("kendoNumericTextBox").value(data);
                                recalAmount($("#DAmount").data("kendoNumericTextBox").value());
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    },
                    error: function (data) {
                        alertify.alert("Please Choose Account");
                    }
                });
            }

        }

        //Combo box CurrencyPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDCurrencyPK,
                    enabled: false,
                    value: setDCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CurrencyPK;
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
                    change: OnChangeDFundPK,
                    dataSource: data,
                    value: setDFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.FundPK;
                }

            }
        }

        //Combo box FundClientPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundClientPK").kendoComboBox({
                    dataValueField: "FundClientPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDFundClientPK,
                    dataSource: data,
                    value: setDFundClientPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDFundClientPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDFundClientPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundClientPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.FundClientPK;
                }
            }
        }

        //Combo box Instrument
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDInstrumentPK,
                    dataSource: data,
                    value: setDInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.InstrumentPK;
                }

            }
        }

        $("#DDebitCredit").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "DEBIT", value: "D" },
                { text: "CREDIT", value: "C" },
            ],
            filter: "contains",
            suggest: true,
            change: onChangeDDebitCredit,
        });


        $("#DAmount").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: onChangeDAmount,
        });

        $("#DDebit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });

        $("#DDebit").data("kendoNumericTextBox").enable(false);

        $("#DCredit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });

        $("#DCredit").data("kendoNumericTextBox").enable(false);

        $("#DCurrencyRate").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });
        $("#DCurrencyRate").data("kendoNumericTextBox").enable(false);
        $("#DBaseDebit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });

        $("#DBaseDebit").data("kendoNumericTextBox").enable(false);
        $("#DBaseCredit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
        });

        $("#DBaseCredit").data("kendoNumericTextBox").enable(false);

        winFundJournalDetail.center();
        winFundJournalDetail.open();

    }
    function DeleteFundJournalDetail(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        

        var dataItemX;
        var grid = $("#gridFundJournalDetail").data("kendoGrid");
        dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        alertify.confirm("Are you sure want to DELETE detail ?", function (e) {
            if (e) {

                var FundJournalDetail = {
                    FundJournalPK: dataItemX.FundJournalPK,
                    LastUsersID: sessionStorage.getItem("user"),
                    AutoNo: dataItemX.AutoNo
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalDetail_D",
                    type: 'POST',
                    data: JSON.stringify(FundJournalDetail),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        refreshFundJournalDetailGrid();
                        alertify.alert(data.Message);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    }
    function initGridFundJournalDetail() {
        var FundJournalDetailURL = window.location.origin + "/Radsoft/FundJournalDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#FundJournalPK").val(),
          dataSourceFundJournalDetail = getDataSourceFundJournalDetail(FundJournalDetailURL);

        $("#gridFundJournalDetail").kendoGrid({
            dataSource: dataSourceFundJournalDetail,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FundJournal Detail"
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
                   command: { text: "show", click: showFundJournalDetail }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeleteFundJournalDetail }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AutoNo", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
                     { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "FundJournalAccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "Amount", title: "Amount", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Debit", title: "Debit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Credit", title: "Credit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "DetailDescription", title: "Detail Description", width: 300 },
               { field: "FundJournalPK", title: "FundJournal No.", hidden: true, filterable: false, width: 120 },
               { field: "FundJournalAccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "CurrencyID", title: "Currency", width: 120 },
               { field: "CurrencyRate", title: "Rate", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "BaseDebit", title: "BaseDebit", width: 200, format: "{0:n4}", footerTemplate: "<div id='sumBaseDebit' style='text-align: right'>#= kendo.toString(sum, 'n4') #</div>", attributes: { style: "text-align:right;" } },
               { field: "BaseCredit", title: "BaseCredit", width: 200, format: "{0:n4}", footerTemplate: "<div id='sumBaseCredit' style='text-align: right'>#= kendo.toString(sum, 'n4') #</div>", attributes: { style: "text-align:right;" } },
               { field: "FundID", title: "Fund ID", width: 200 },
               { field: "FundName", title: "Name", width: 200 },
               { field: "FundClientID", title: "Fund Client ID", width: 200 },
               { field: "FundClientName", title: "Name", width: 200 },
               { field: "InstrumentID", title: "Instrument ID", width: 200 },
               { field: "InstrumentName", title: "Name", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    function FundJournalDetailGrid(e) {
        var FundJournalDetailURL = window.location.origin + "/Radsoft/FundJournalDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.FundJournalPK,
         dataSourceFundJournalDetail = getDataSourceFundJournalDetail(FundJournalDetailURL);

        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: dataSourceFundJournalDetail,
            height: 250,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FundJournal Detail"
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
                   field: "FundJournalAccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "FundJournalAccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "Amount", title: "Amount", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Debit", title: "Debit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Credit", title: "Credit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n4')#</div>", format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "DetailDescription", title: "Detail Description", width: 300 },
               { field: "FundJournalPK", title: "FundJournal No.", hidden: true, filterable: false, width: 120 },
               { field: "FundJournalAccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "CurrencyID", title: "Currency", width: 120 },
               { field: "CurrencyRate", title: "Rate", width: 170, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "BaseDebit", title: "BaseDebit", width: 200, format: "{0:n4}", footerTemplate: "<div id='sumBaseDebit' style='text-align: right'>#= kendo.toString(sum, 'n4') #</div>", attributes: { style: "text-align:right;" } },
               { field: "BaseCredit", title: "BaseCredit", width: 200, format: "{0:n4}", footerTemplate: "<div id='sumBaseCredit' style='text-align: right'>#= kendo.toString(sum, 'n4') #</div>", attributes: { style: "text-align:right;" } },
               { field: "FundID", title: "Fund ID", width: 200 },
               { field: "FundName", title: "Name", width: 200 },
               { field: "FundClientID", title: "Fund Client ID", width: 200 },
               { field: "FundClientName", title: "Name", width: 200 },
               { field: "InstrumentID", title: "Instrument ID", width: 200 },
               { field: "InstrumentName", title: "Name", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }
    function onChangeDAmount() {
        recalAmount(this.value());
    }
    function onChangeDDebitCredit() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        recalAmount($("#DAmount").data("kendoNumericTextBox").value());
    }
    function recalAmount(_amount) {
        if ($("#DDebitCredit").data("kendoComboBox").value() == 'D') {
            $("#DDebit").data("kendoNumericTextBox").value(_amount);
            $("#DCredit").data("kendoNumericTextBox").value(0);
            $("#DBaseDebit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
            $("#DBaseCredit").data("kendoNumericTextBox").value(0);
        } else if ($("#DDebitCredit").data("kendoComboBox").value() == 'C') {
            $("#DDebit").data("kendoNumericTextBox").value(0);
            $("#DCredit").data("kendoNumericTextBox").value(_amount);
            $("#DBaseDebit").data("kendoNumericTextBox").value(0);
            $("#DBaseCredit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
        } else if (_amount > 0) {
            alertify.alert("Please Choose Debit or Credit");
        }
    }
    function onWinFundJournalDetailClose() {
        GlobValidatorFundJournalDetail.hideMessages();
        $("#DFundJournalAccountPK").val("");
        $("#DCurrencyPK").val("");
        $("#DFundPK").val("");
        $("#DFundClientPK").val("");
        $("#DInstrumentPK").val("");
        $("#DDetailDescription").val("");
        $("#DDebitCredit").val("");
        $("#DAmount").val("");
        $("#DDebit").val("");
        $("#DCredit").val("");
        $("#DCurrencyRate").val("");
        $("#DBaseDebit").val("");
        $("#DBaseCredit").val("");
        $("#DLastUsersID").val("");
        $("#DLastUpdate").val("");
    }
    function onWinFundJournalPostingClose() {

        $("#Posting").val("");
        $("#PostPeriodPK").val("");
        $("#PostDateFrom").val("");
        $("#PostDateTo").val("");
        $("#PostFundJournalFrom").val("");
        $("#PostFundJournalTo").val("");
        $("#LblPostDateFrom").hide();
        $("#CmbPostDateFrom").hide();
        $("#LblPostDateTo").hide();
        $("#LblPostFundJournalFrom").show();
        $("#LblPostFundJournalTo").show();
        refresh();
    }

    $("#BtnRefreshDetail").click(function () {
        refreshFundJournalDetailGrid();
    });
    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournal",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundJournal" + "/" + $("#FundJournalPK").val(),
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
                winFundJournalDetail.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnNew").click(function () {
        showDetails(null);
    });

    $("#BtnAddDetail").click(function () {
        
        if ($("#FundJournalPK").val() == 0 || $("#FundJournalPK").val() == null) {
            alertify.alert("There's no FundJournal Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("FundJournal Already History");
        } else {
            showFundJournalDetail();
        }
    });
    $("#BtnSave").click(function () {
        var val = validateDataFundJournalDetail();
        if (val == 1) {
            
            if ($("#DAutoNo").val() == "") {
                alertify.confirm("Are you sure want to Add FUND JOURNAL DETAIL ?", function (e) {
                    if (e) {

                        var FundJournalDetail = {
                            FundJournalPK: $('#FundJournalPK').val(),
                            AutoNo: $('#DAutoNo').val(),
                            Status: 2,
                            FundJournalAccountPK: $('#DFundJournalAccountPK').val(),
                            CurrencyPK: $('#DCurrencyPK').val(),
                            FundPK: $('#DFundPK').val(),
                            FundClientPK: $('#DFundClientPK').val(),
                            InstrumentPK: $('#DInstrumentPK').val(),
                            DetailDescription: $('#DDetailDescription').val(),
                            DebitCredit: $('#DDebitCredit').val(),
                            Amount: $('#DAmount').val(),
                            Debit: $('#DDebit').val(),
                            Credit: $('#DCredit').val(),
                            CurrencyRate: $('#DCurrencyRate').val(),
                            BaseDebit: $('#DBaseDebit').val(),
                            BaseCredit: $('#DBaseCredit').val(),
                            LastUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundJournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalDetail_I",
                            type: 'POST',
                            data: JSON.stringify(FundJournalDetail),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                $("#gridFundJournalDetail").empty();
                                initGridFundJournalDetail();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                });
            } else {
                alertify.confirm("Are you sure want to UPDATE FUND JOURNAL DETAIL ?", function (e) {
                    if (e) {

                        var FundJournalDetail = {
                            FundJournalPK: $('#FundJournalPK').val(),
                            AutoNo: $('#DAutoNo').val(),
                            Status: 2,
                            FundJournalAccountPK: $('#DFundJournalAccountPK').val(),
                            CurrencyPK: $('#DCurrencyPK').val(),
                            FundPK: $('#DFundPK').val(),
                            FundClientPK: $('#DFundClientPK').val(),
                            InstrumentPK: $('#DInstrumentPK').val(),
                            DetailDescription: $('#DDetailDescription').val(),
                            DebitCredit: $('#DDebitCredit').val(),
                            Amount: $('#DAmount').val(),
                            Debit: $('#DDebit').val(),
                            Credit: $('#DCredit').val(),
                            CurrencyRate: $('#DCurrencyRate').val(),
                            BaseDebit: $('#DBaseDebit').val(),
                            BaseCredit: $('#DBaseCredit').val(),
                            LastUsersID: sessionStorage.getItem("user")
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundJournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournalDetail_U",
                            type: 'POST',
                            data: JSON.stringify(FundJournalDetail),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                $("#gridFundJournalDetail").empty();
                                initGridFundJournalDetail();
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
        if ($("#FundJournalPK").val() > 0) {
            alertify.alert("FUND JOURNAL HEADER ALREADY EXIST, Cancel and click add new to add more Header");
            return
        }
        var val = validateDataFundJournal();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add FUND JOURNAL HEADER ?", function () {       
                    if ($('#Reference').val() == null || $('#Reference').val() == "") {
                        setTimeout(function () {
                            alertify.confirm("Are you sure want to Generate New Reference ?", function () {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ADJ/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#Reference").val(data);
                                        //alertify.alert("Your new reference is " + data);
                                        var FundJournal = {
                                            ValueDate: $('#ValueDate').val(),
                                            PeriodPK: $("#PeriodPK").val(),
                                            Reference: data,
                                            Type: $('#Type').val(),
                                            Description: $('#Description').val(),
                                            EntryUsersID: sessionStorage.getItem("user")

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_I",
                                            type: 'POST',
                                            data: JSON.stringify(FundJournal),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert(data.Message);
                                                $("#FundJournalPK").val(data.FundJournalPK);
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
                            })
                        }, 1000);
                    }
                    else {
                        var FundJournal = {
                            ValueDate: $('#ValueDate').val(),
                            PeriodPK: $("#PeriodPK").val(),
                            Reference: $("#Reference").val(),
                            Type: $('#Type').val(),
                            Description: $('#Description').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_I",
                            type: 'POST',
                            data: JSON.stringify(FundJournal),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data.Message);
                                $("#FundJournalPK").val(data.FundJournalPK);
                                $("#HistoryPK").val(data.HistoryPK);
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
        var val = validateDataFundJournal();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundJournal = {
                                    FundJournalPK: $('#FundJournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    PeriodPK: $("#PeriodPK").val(),
                                    Reference: $('#Reference').val(),
                                    TrxNo: $('#TrxNo').val(),
                                    TrxName: $('#TrxName').val(),
                                    Type: $('#Type').val(),
                                    Description: $('#Description').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournal),
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
        
        //alert(Number($("#sumBaseDebit").text()) - Number($("#sumBaseCredit").text()));
        //if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
        //    alertify.alert("Fund Journal Not Balance");
        //    return;
        //}
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournal/FundJournalCheckBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundJournalPK").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //if (data == true) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournal",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                        var FundJournal = {
                                            FundJournalPK: $('#FundJournalPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_A",
                                            type: 'POST',
                                            data: JSON.stringify(FundJournal),
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
                        //}
                        //else {
                        //    alertify.alert("Fund Journal Not Balance");
                        //}
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
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVERSED") {
                alertify.alert("Fund Journal Already Posted / Reversed, Void Cancel");
            } else {
                if (e) {
                    var FundJournal = {
                        FundJournalPK: $('#FundJournalPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_V",
                        type: 'POST',
                        data: JSON.stringify(FundJournal),
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
                var FundJournal = {
                    FundJournalPK: $('#FundJournalPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_R",
                    type: 'POST',
                    data: JSON.stringify(FundJournal),
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

    $("#BtnPosting").click(function () {
        
        //if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
        //    alertify.alert("Fund Journal Not Balance");
        //    return;
        //}
        alertify.confirm("Are you sure want to Posting FundJournal?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVERSED") {
                alertify.alert("Fund Journal Already Posted / Reversed, Posting Cancel");
            } else {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var FundJournal = {
                                    FundJournalPK: $('#FundJournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PostedBy: sessionStorage.getItem("user"),
                                    PeriodPK: $('#PeriodPK').val()
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_Posting",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournal),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("Reserved").addClass("Posted");
                                        $("#State").text("POSTED");
                                        $("#PostedBy").text(sessionStorage.getItem("user"));
                                        $("#Posted").prop('checked', true);
                                        $("#Reserved").prop('checked', false);
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
    function showPosting(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Posting ?", function (a) {
                if (a) {
                    var grid = $("#gridFundJournalApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                    var pos = _dataItemX.Posted;

                    if (pos == true) {
                        alertify.alert("Data Already Posted");
                    }
                    else {

                        var _FundJournalPK = _dataItemX.FundJournalPK;
                        var _periodPK = _dataItemX.PeriodPK;
                        var _historyPK = _dataItemX.HistoryPK;
                        var Posting = {
                            FundJournalPK: _FundJournalPK,
                            PeriodPK: _periodPK,
                            HistoryPK: _historyPK,
                            PostedBy: sessionStorage.getItem("user")
                        };



                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_Posting",
                            type: 'POST',
                            data: JSON.stringify(Posting),
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



                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }
    $("#BtnReversed").click(function () {
        
        alertify.confirm("Are you sure want to Reversed Fund Journal?", function (e) {


            if ($("#State").text() == "REVERSED") {
                alertify.alert("Fund Journal Already Reversed !");
            } else {
                if (e) {


                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundJournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundJournal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var FundJournal = {
                                    FundJournalPK: $('#FundJournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ReversedBy: sessionStorage.getItem("user"),
                                    PeriodPK: $('#PeriodPK').val()
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_Reversed",
                                    type: 'POST',
                                    data: JSON.stringify(FundJournal),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                                        $("#State").text("REVERSED");
                                        $("#ReversedBy").text(sessionStorage.getItem("user"));
                                        $("#Reversed").prop('checked', true);
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
            }
        });
    });

    function showReverse(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Reversed ?", function (a) {
                if (a) {
                    var grid = $("#gridFundJournalApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    if (_dataItemX.Type >= 3) {
                        alertify.alert("Transaction Or Cashier Can't Be Reverse From Here.");
                        return;
                    }

                    if (_dataItemX.Reversed == true) {
                        alertify.alert("Data Already Reversed");
                    }
                    else if (_dataItemX.Posted == false) {
                        alertify.alert("Data Not Posting Yet");
                    }
                    else {
                        var _FundJournalPK = _dataItemX.FundJournalPK;
                        var _periodPK = _dataItemX.PeriodPK;
                        var _historyPK = _dataItemX.HistoryPK;
                        var Reversed = {
                            FundJournalPK: _FundJournalPK,
                            PeriodPK: _periodPK,
                            HistoryPK: _historyPK,
                            ReversedBy: sessionStorage.getItem("user")

                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundJournal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundJournal_Reversed",
                            type: 'POST',
                            data: JSON.stringify(Reversed),
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
        else if ($("#PostFundJournalFrom").data("kendoComboBox").value() > $("#PostFundJournalTo").data("kendoComboBox").value()) {
            alertify.alert("Fund Journal From >  Fund Journal To");
            return 0;
        }

        else {
            return 1;
        }
    }
    $("#BtnVoucher").click(function () {
        
        alertify.confirm("Are you sure want to Download FundJournal Voucher ?", function (e) {
            if (e) {
                var FundJournal = {
                    FundJournalPK: $('#FundJournalPK').val()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournal/FundJournalVoucher/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(FundJournal),
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
                    url: window.location.origin + "/Radsoft/FundJournal/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FundJournal/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FundJournal/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                    url: window.location.origin + "/Radsoft/FundJournal/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
    $("#BtnReverseBySelected").click(function () {
        
        alertify.confirm("Are you sure want to Reverse by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundJournal/ReverseBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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


});