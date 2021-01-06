$(document).ready(function () {
    document.title = 'FORM FUND CLIENT VERIFICATION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var winImage;
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


        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
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
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo,
            value: new Date(),
        });

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

        winImage = $("#winImage").kendoWindow({
            height: 800,
            title: "Image",
            visible: false,
            width: 800,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");


        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {
                
                alertify.success("Wrong Format Date DD/MMM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            refresh();
        }
        function OnChangeDateTo() {
            var _DateTo = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateTo) {
                
                alertify.success("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }



    }

    function showButton() {
        //$("#BtnUpdate").show();
        //$("#BtnAdd").show();
        //$("#BtnVoid").show();
        //$("#BtnReject").show();
        //$("#BtnApproved").show();
        //$("#BtnOldData").show();
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
                             TransactionDate: { type: "date" },
                             NAVDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Selected: { type: "boolean" }, 
                             TrxPK: { type: "number" },
                             FundClientVerificationPK: { type: "number" },
                             Name: { type: "string" },
                             BankName: { type: "string" },
                             AccountNo: { type: "string" },
                             AccountName: { type: "string" },
                             Amount: { type: "number" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             LastUpdate: { type: "date" },

                            
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridFundClientVerificationPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridFundClientVerificationHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        
        $("#gridFundClientVerificationApproved").empty();
        //if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
        //    alertify.alert("Please Fill Date");
        //}
            
        //else {

            var FundClientVerificationApprovedURL = window.location.origin + "/Radsoft/FundClientVerification/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/2", 
            dataSourceApproved = getDataSource(FundClientVerificationApprovedURL);

        //}

        var grid = $("#gridFundClientVerificationApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Verification"
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
                //{
                //    field: "Selected",
                //    width: 50,
                //    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                //    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                //    filterable: true,
                //    sortable: false,
                //    columnMenu: false
                //},
                //{ field: "FundClientPositionPK", title: "SysNo.", width: 95 },
                { field: "FundClientVerificationPK", title: "ID", width: 120 },
                { field: "TrxPK", title: "Client Subs Number", width: 200 },
                { field: "Name", title: "Client Name", width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "NAVDate", title: "NAV Date", width: 200, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MMM/yyyy')#" },
                { field: "TransactionDate", title: "Payment Date", width: 200, template: "#= kendo.toString(kendo.parseDate(TransactionDate), 'dd/MMM/yyyy')#" },
                { field: "BankName", title: "Bank Name", width:200 },
                { field: "AccountNo", title: "Account No", width:200 },
                { field: "AccountName", title: "Account Name", width:200 },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" }},
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180},
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

            alertify.success(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            

            var grid = $("#gridFundClientVerificationApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _id = dataItemX.ID;
            var _checked = this.checked;
            SelectDeselectData(_checked, _id);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundClientVerification").kendoTabStrip({
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
        $("#BtnVoidBySelected").show();
    }

    function RecalGridPending() {
        $("#gridFundClientVerificationPending").empty();
        //if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
        //    alertify.alert("Please Fill Date");
        //}
        
        //else {

            var FundClientVerificationPendingURL = window.location.origin + "/Radsoft/FundClientVerification/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/1",
            dataSourcePending = getDataSource(FundClientVerificationPendingURL);

        //}
        var grid = $("#gridFundClientVerificationPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Verification"
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
                    //{
                    //    field: "Selected",
                    //    width: 50,
                    //    template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    //    headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                    //    filterable: true,
                    //    sortable: false,
                    //    columnMenu: false
                    //},
                    //{ field: "FundClientPositionPK", title: "SysNo.", width: 95 },
                    { field: "TrxPK", title: "Client Subs Number", width: 200 },
                    { field: "FundClientVerificationPK", title: "ID", width: 120 },
                    { field: "Name", title: "Client Name", width: 200 },
                    { field: "FundID", title: "Fund ID", width: 200 },
                    { field: "NAVDate", title: "NAV Date", width: 200, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MMM/yyyy')#" },
                    { field: "TransactionDate", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(TransactionDate), 'dd/MMM/yyyy')#" },
                    { field: "BankName", title: "Bank Name", width: 200 },
                    { field: "AccountNo", title: "Account No", width: 200 },
                    { field: "AccountName", title: "Account Name", width: 200 },
                    { field: "Amount", title: "Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" }},
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180},
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
            alertify.success(_msg);
            SelectDeselectAllData(_checked, "Pending");

        });

        grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        function selectDataPending(e) {
            

            var grid = $("#gridFundClientVerificationPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _id = dataItemX.ID;
            var _checked = this.checked;
            SelectDeselectData(_checked, _id);

        }

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }

    function RecalGridHistory() {

        $("#gridFundClientVerificationHistory").empty();
        //if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
        //    alertify.alert("Please Fill Date");
        //}
            
        //else {

            var FundClientVerificationHistoryURL = window.location.origin + "/Radsoft/FundClientVerification/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/3", 
            dataSourceHistory = getDataSource(FundClientVerificationHistoryURL);

        //}
        $("#gridFundClientVerificationHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Verification"
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
                //{ field: "FundClientPositionPK", title: "SysNo.", width: 95 },
                { field: "TrxPK", title: "Client Subs Number", width: 200 },
                { field: "FundClientVerificationPK", title: "ID", width: 120 },
                { field: "Name", title: "Client Name", width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "NAVDate", title: "NAV Date", width: 200, template: "#= kendo.toString(kendo.parseDate(NAVDate), 'dd/MMM/yyyy')#" },
                { field: "TransactionDate", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(TransactionDate), 'dd/MMM/yyyy')#" },
                { field: "BankName", title: "Bank Name", width: 200 },
                { field: "AccountNo", title: "Account No", width: 200 },
                { field: "AccountName", title: "Account Name", width: 200 },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridFundClientVerificationHistory").data("kendoGrid");
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
                alertify.success("Close Detail");
            }
        });
    });


    function showDetails(e) {
        
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        var grid;

        if (tabindex == 0 || tabindex == undefined) {
            grid = $("#gridFundClientVerificationApproved").data("kendoGrid");
        }
        if (tabindex == 1) {
            grid = $("#gridFundClientVerificationPending").data("kendoGrid");
        }
        if (tabindex == 2) {
            grid = $("#gridFundClientVerificationHistory").data("kendoGrid");
        }
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
  
            url: "http://" + _GlobUrlServerRDOApi + "image/transactionoriginal?id=" + dataItemX.ImgOri,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PdfApi").removeAttr("type");
                $("#PdfApi").attr("src", "");
                $("#ImgApi_1").attr("src", "");
                if (data != null) {
                    $("#PdfApi").attr("src", data.base64);
                    $("#ImgApi_1").attr("src", data.base64);
                } else {
                    alertify.alert('NO IMAGE');
                }
                
                winImage.center();
                winImage.open();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

});
