$(document).ready(function () {
    document.title = 'FORM CORPORATE AR AP PAYMENT';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 300;
    var dirty;
    var upOradd;
    var _d = new Date();
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

        $("#BtnRevise").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRevise.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnUnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnGetNavBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnBatchFormBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnRecalculate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

    }

    
     

    function initWindow() {

        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }
        }


        
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateTo
        });


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


        win = $("#WinCorporateArApPayment").kendoWindow({
            height: 1250,
            title: " CorporateArApPayment Detail",
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
            }
        }).data("kendoWindow");


       
    }


    var GlobValidator = $("#WinCorporateArApPayment").kendoValidator().data("kendoValidator");

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

        $("#btnListFundClientPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")


        var dataItemX;
        if (e == null) {

            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#BtnRecalculate").show();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            $("#ValueDate").data("kendoDatePicker").value(_d);
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridCorporateArApPaymentApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridCorporateArApPaymentPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridCorporateArApPaymentHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnOldData").show();
                $("#BtnApproved").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").show();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnPosting").show();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnRecalculate").hide();
                $("#BtnOldData").hide();
            }

            dirty = null;

            $("#CorporateArApPaymentPK").val(dataItemX.CorporateArApPaymentPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
           
        }


     

        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        //refresh();
    }

   

    function clearData() {

        $("#CorporateArApPaymentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
       

    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnUnApproved").show();
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
                 aggregate: [{ field: "CashAmount", aggregate: "sum" },
                    { field: "UnitAmount", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             CorporateArApPaymentPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             
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
            var gridPending = $("#gridCorporateArApPaymentPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridCorporateArApPaymentHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridCorporateArApPaymentApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            } else if (row.Reversed == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            }
            else if (row.TypeDesc == "Regular") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowRegular");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function gridPendingOnDataBound() {
        var grid = $("#gridCorporateArApPaymentPending").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.TypeDesc == "Ordinary") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            }
            else if (row.TypeDesc == "Regular") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowRegular");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function initGrid() {
        
        $("#gridCorporateArApPaymentApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateArApPaymentApprovedURL = window.location.origin + "/Radsoft/CorporateArApPayment/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(CorporateArApPaymentApprovedURL);

        }

        var grid = $("#gridCorporateArApPaymentApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Corporate Ar Ap Payment"
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
               { field: "CorporateArApPaymentPK", title: "SysNo.", width: 95 },
               
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               
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
            

            var grid = $("#gridCorporateArApPaymentApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _CorporateArApPaymentPK = dataItemX.CorporateArApPaymentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _CorporateArApPaymentPK);

        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCorporateArApPayment").kendoTabStrip({
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

    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CorporateArApPayment/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CorporateArApPayment/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
        $("#gridCorporateArApPaymentPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateArApPaymentPendingURL = window.location.origin + "/Radsoft/CorporateArApPayment/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(CorporateArApPaymentPendingURL);

        }
        var grid = $("#gridCorporateArApPaymentPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Coporate Ar AP Payment"
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
               { field: "CorporateArApPaymentPK", title: "SysNo.", width: 95 },
             
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               
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
            

            var grid = $("#gridCorporateArApPaymentPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _CorporateArApPaymentPK = dataItemX.CorporateArApPaymentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _CorporateArApPaymentPK);

        }

        ResetButtonBySelectedData();
  


    }

    function RecalGridHistory() {

        $("#gridCorporateArApPaymentHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CorporateArApPaymentHistoryURL = window.location.origin + "/Radsoft/CorporateArApPayment/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(CorporateArApPaymentHistoryURL);

        }
        $("#gridCorporateArApPaymentHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Coporate Ar Ap Payment"
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
               { field: "CorporateArApPaymentPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
             
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();

    }

    function gridHistoryDataBound() {
        var grid = $("#gridCorporateArApPaymentHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            }
            else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    //add,update,old data,approve,reject 
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#TotalCashAmount').val() != 0) {
                
                alertify.confirm("Are you sure want to Add data ?", function (e) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/HighRiskMonitoring/CheckInvestorAndFundRiskProfile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data.length != 0) {
                                                alertify.confirm(data + "  Are you sure want to Add data ?", function (e) {
                                                    if (e) {
                                                        var CorporateArApPayment = {
                                                            CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                                            Type: 1,
                                                            NAVDate: $('#NAVDate').val(),
                                                            ValueDate: $('#ValueDate').val(),
                                                            // PaymentDate: $('#PaymentDate').val(),
                                                            NAV: $('#NAV').val(),
                                                            FundPK: $('#FundPK').val(),
                                                            FundClientPK: $('#FundClientPK').val(),
                                                            CashRefPK: $('#CashRefPK').val(),
                                                            CurrencyPK: $('#CurrencyPK').val(),
                                                            Description: $('#Description').val(),
                                                            CashAmount: $('#CashAmount').val(),
                                                            UnitAmount: $('#UnitAmount').val(),
                                                            TotalCashAmount: $('#TotalCashAmount').val(),
                                                            TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                            SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                            SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                            AgentPK: $('#AgentPK').val(),
                                                            AgentFeePercent: $('#AgentFeePercent').val(),
                                                            AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                            BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                            EntryUsersID: sessionStorage.getItem("user")

                                                        };
                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/CorporateArApPayment/CorporateArApPaymentValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                if (data != 0) {
                                                                    alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                        if (e) {
                                                                            $.ajax({
                                                                                url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_I",
                                                                                type: 'POST',
                                                                                data: JSON.stringify(CorporateArApPayment),
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
                                                                } else {
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_I",
                                                                        type: 'POST',
                                                                        data: JSON.stringify(CorporateArApPayment),
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
                                                    }
                                                })
                                            } else {
                                                var CorporateArApPayment = {
                                                    CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                                    Type: 1,
                                                    NAVDate: $('#NAVDate').val(),
                                                    ValueDate: $('#ValueDate').val(),
                                                    // PaymentDate: $('#PaymentDate').val(),
                                                    NAV: $('#NAV').val(),
                                                    FundPK: $('#FundPK').val(),
                                                    FundClientPK: $('#FundClientPK').val(),
                                                    CashRefPK: $('#CashRefPK').val(),
                                                    CurrencyPK: $('#CurrencyPK').val(),
                                                    Description: $('#Description').val(),
                                                    CashAmount: $('#CashAmount').val(),
                                                    UnitAmount: $('#UnitAmount').val(),
                                                    TotalCashAmount: $('#TotalCashAmount').val(),
                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                    AgentPK: $('#AgentPK').val(),
                                                    AgentFeePercent: $('#AgentFeePercent').val(),
                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                    EntryUsersID: sessionStorage.getItem("user")

                                                };
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/CorporateArApPayment/CorporateArApPaymentValidation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val() + "/" + $("#FundPK").val() + "/" + $('#CashAmount').val(),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        if (data != 0) {
                                                            alertify.confirm("You already input same data for this day.  Are you sure want to Add this data again?", function (e) {
                                                                if (e) {
                                                                    $.ajax({
                                                                        url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_I",
                                                                        type: 'POST',
                                                                        data: JSON.stringify(CorporateArApPayment),
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
                                                        } else {
                                                            $.ajax({
                                                                url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_I",
                                                                type: 'POST',
                                                                data: JSON.stringify(CorporateArApPayment),
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

                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                                else {
                                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });

                    }
                });
            }
            else {
                alertify.alert("Please Recalculate First!")
            }
        }

    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#TotalCashAmount').val() != 0) {
                
                alertify.prompt("Are you sure want to Update , please give notes:","", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundClient/CheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundClientPK").val(),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateArApPaymentPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateArApPayment",
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                                var CorporateArApPayment = {
                                                    CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                                    Type: $('#Type').val(),
                                                    HistoryPK: $('#HistoryPK').val(),
                                                    NAVDate: $('#NAVDate').val(),
                                                    ValueDate: $('#ValueDate').val(),
                                                    //PaymentDate: $('#PaymentDate').val(),
                                                    NAV: $('#NAV').val(),
                                                    FundPK: $('#FundPK').val(),
                                                    FundClientPK: $('#FundClientPK').val(),
                                                    CashRefPK: $('#CashRefPK').val(),
                                                    CurrencyPK: $('#CurrencyPK').val(),
                                                    Description: $('#Description').val(),
                                                    CashAmount: $('#CashAmount').val(),
                                                    UnitAmount: $('#UnitAmount').val(),
                                                    TotalCashAmount: $('#TotalCashAmount').val(),
                                                    TotalUnitAmount: $('#TotalUnitAmount').val(),
                                                    SubscriptionFeePercent: $('#SubscriptionFeePercent').val(),
                                                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                                                    AgentPK: $('#AgentPK').val(),
                                                    AgentFeePercent: $('#AgentFeePercent').val(),
                                                    AgentFeeAmount: $('#AgentFeeAmount').val(),
                                                    BitImmediateTransaction: $('#BitImmediateTransaction').is(":checked"),
                                                    Notes: str,
                                                    EntryUsersID: sessionStorage.getItem("user")
                                                };
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_U",
                                                    type: 'POST',
                                                    data: JSON.stringify(CorporateArApPayment),
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
                                    alertify.alert("Fund Client is Pending, Please Check Fund Client First!");
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                });
            }
            else {
                alertify.alert("Please Recalculate First!")
            }
        }

    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateArApPaymentPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateArApPayment",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CorporateArApPayment" + "/" + $("#CorporateArApPaymentPK").val(),
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
        
        if ($("#NAV").val() == 0 || $("#NAV").val() == null || $("#NAV").val() == "") {
            alertify.alert("NAV Must Ready First, Please Click Get NAV");
            return;
        }

        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateArApPaymentPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateArApPayment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateArApPayment = {
                                CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_A",
                                type: 'POST',
                                data: JSON.stringify(CorporateArApPayment),
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

    $("#BtnUnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to UnApprove by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CorporateArApPayment/ValidateUnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/UnApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                        } else {
                            alertify.alert("Client Subscription Already Posted");
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

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateArApPaymentPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateArApPayment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateArApPayment = {
                                CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_V",
                                type: 'POST',
                                data: JSON.stringify(CorporateArApPayment),
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
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CorporateArApPaymentPK").val() + "/" + $("#HistoryPK").val() + "/" + "CorporateArApPayment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CorporateArApPayment = {
                                CorporateArApPaymentPK: $('#CorporateArApPaymentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CorporateArApPayment_R",
                                type: 'POST',
                                data: JSON.stringify(CorporateArApPayment),
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

    function formatNumber(x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }

    $("input[type='text']").change(function () {
        dirty = true;
    });

    $("input[type='number']").change(function () {
        dirty = true;
    });

  

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CorporateArApPayment/ValidateCheckFundClientPending/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CorporateArApPayment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/ValidateApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/CorporateArApPayment/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                                    } else {
                                        alertify.alert("NAV Must Ready First, Please Click Get NAV");
                                        $.unblockUI();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });
                        } else {
                            alertify.alert(data);
                        }
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
                    url: window.location.origin + "/Radsoft/CorporateArApPayment/ValidateCheckDescription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/CorporateArApPayment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CorporateArApPayment/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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
                        } else {
                            alertify.alert(data);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
  
});
