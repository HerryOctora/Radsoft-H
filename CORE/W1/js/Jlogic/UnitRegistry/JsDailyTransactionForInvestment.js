$(document).ready(function () {
    var win;
    var winBySelected;
    var winBySAandFund;

    //fund grid
    var checkedIds = {};
    var checkedApproved = {};
    var checkedPending = {};

    //end Fund grid

    if (_GlobClientCode == "24") {
        $("#LblImportAPERDSummary").show();
    }
    else {
        $("#LblImportAPERDSummary").hide();
    }

    var GlobValidator = $("#WinDailyTransactionForInvestment").kendoValidator().data("kendoValidator");
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

    initWindow();

    function initWindow() {
        $("#FilterDate").kendoDatePicker({
            format: "MM/dd/yyyy",
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

        $("#BtnImportDailyTransactionForInvestment").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnUpdateBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnUpdateBySAandFund").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnSubmitBySAandFund").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnSubmitBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#PaymentDateBySelected").kendoDatePicker({
            format: "MM/dd/yyyy",
        });

        $("#PaymentDateBySAandFund").kendoDatePicker({
            format: "MM/dd/yyyy",
        });

        $("#BtnMoveToSubsRedemp").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnGenerateToSInvestBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportAPERD").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportAPERDSummary").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        winBySelected = $("#WinUpdateBySelected").kendoWindow({
            height: 200,
            title: "* Update Payment Date By Selected",
            visible: false,
            width: 700,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

        winBySAandFund = $("#WinUpdateBySAandFund").kendoWindow({
            height: 300,
            title: "* Update Payment Date By SA and Fund",
            visible: false,
            width: 700,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

        initGrid();

        //win = $("#WinDailyTransactionForInvestment").kendoWindow({
        //    height: 200,
        //    title: "* Update Payment Sinvest",
        //    visible: false,
        //    width: 400,
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //}).data("kendoWindow");

        winAPERD = $("#WinImportAPERDTemp").kendoWindow({
            height: 200,
            title: "* Update Payment Sinvest",
            visible: false,
            width: 400,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");


        //Type//
        $("#Transaction").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Subscription/Redemption", value: "sub" },
                { text: "Switching", value: "swi" },

            ],
            filter: "contains",
            change: onChangeTransaction,
            suggest: true
        });

        function onChangeTransaction() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //win.center();
        //win.open();
    }

    $("#BtnUpdateBySelected").click(function () {
        winBySelected.center();
        winBySelected.open();
    });

    $("#BtnUpdateBySAandFund").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/GetData_SA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SA").kendoComboBox({
                    dataValueField: "SA",
                    dataTextField: "SA",
                    change: OnChangeSA,
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeSA() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/GetData_Fund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Fund").kendoComboBox({
                    dataValueField: "Fund",
                    dataTextField: "Fund",
                    change: OnChangeFund,
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function OnChangeFund() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        winBySAandFund.center();
        winBySAandFund.open();
    });

    $("#BtnSubmitBySelected").click(function () {
        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringDailyTransactionForInvestmentSelected = '';

        for (var i in ArrayFundFrom) {
            stringDailyTransactionForInvestmentSelected = stringDailyTransactionForInvestmentSelected + ArrayFundFrom[i] + ',';

        }
        stringDailyTransactionForInvestmentSelected = stringDailyTransactionForInvestmentSelected.substring(0, stringDailyTransactionForInvestmentSelected.length - 1)

        alert(stringDailyTransactionForInvestmentSelected);
        if ($("#PaymentDateBySelected").val() == null || $("#PaymentDateBySelected").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        } else {
            alertify.confirm("Are you sure want to Change Payment Date ?", function (e) {
                if (e) {

                    var DailyTransactionForInvestment = {
                        DailyTransactionForInvestmentSelected: stringDailyTransactionForInvestmentSelected,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/DailyTransactionForInvestment_ChangePaymentDate_BySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#PaymentDateBySelected").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(DailyTransactionForInvestment),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            winBySelected.close();
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

    $("#BtnSubmitBySAandFund").click(function () {

        if ($("#PaymentDateBySAandFund").val() == null || $("#PaymentDateBySAandFund").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        } else {
            alertify.confirm("Are you sure want to Change Payment Date ?", function (e) {
                if (e) {
                    var DailyTransactionForInvestment_SAandFund = {
                        SA: $('#SA').val(),
                        Fund: $('#Fund').val()
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/DailyTransactionForInvestment_ChangePaymentDate_BySAandFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#PaymentDateBySAandFund").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        data: JSON.stringify(DailyTransactionForInvestment_SAandFund),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            winBySAandFund.close();
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

    $("#BtnImportAPERD").click(function () {
        winAPERD.center();
        winAPERD.open();
    });

    $("#BtnImportDailyTransactionForInvestment").click(function () {
        document.getElementById("FileImportDailyTransactionForInvestment").click();
    });

    $("#FileImportDailyTransactionForInvestment").change(function () {
        if ($("#Transaction").val() == "sub") {
            $.blockUI({});

            var data = new FormData();
            var files = $("#FileImportDailyTransactionForInvestment").get(0).files;

            var fileSize = this.files[0].size / 1024 / 1024;

            if (fileSize > _GlobMaxFileSizeInMB) {
                $.unblockUI();
                alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
                return;
            }

            //var _d = files[0].name.substring(0, 50);
            if (files.length > 0) {
                data.append("DailyTransactionForInvestmentSubRedTemp", files[0]);
                $.ajax({
                    url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdatePaymentSInvestTemp_Import/01-01-1900/0",
                    type: 'POST',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $.unblockUI();
                        $("#FileImportDailyTransactionForInvestment").val("");
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        $("#FileImportDailyTransactionForInvestment").val("");
                    }
                });
            } else {
                alertify.alert("Please Choose Correct File");
                $("#FileImportDailyTransactionForInvestment").val("");
            }
        }
        else if ($("#Transaction").val() == "swi") {
            $.blockUI({});

            var data = new FormData();
            var files = $("#FileImportDailyTransactionForInvestment").get(0).files;
            //var _d = files[0].name.substring(0, 50);
            if (files.length > 0) {
                data.append("DailyTransactionForInvestment", files[0]);
                $.ajax({
                    url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UpdatePaymentSInvestTemp_Import/01-01-1900/0",
                    type: 'POST',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $.unblockUI();
                        $("#FileImportDailyTransactionForInvestment").val("");
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        $("#FileImportDailyTransactionForInvestment").val("");
                    }
                });
            } else {
                alertify.alert("Please Choose Correct File");
                $("#FileImportDailyTransactionForInvestment").val("");
            }
        }
        else {
            $.unblockUI();
            alertify.alert("Please Choose Transaction Type");
        }

    });

    $("#BtnCancelListing").click(function () {

        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinInvestmentListing.close();
                alertify.alert("Cancel Listing");
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
                pageSize: 1000,
                schema: {
                    model: {
                        fields: {
                            DailyTransactionForInvestmentPK: { type: "number" },
                            Selected: { type: "boolean" },
                            TransactionType: { type: "string" },
                            TransactionDate: { type: "date" },
                            RefNumber: { type: "string" },
                            SellingAgentCode: { type: "string" },
                            SellingAgentName: { type: "string" },
                            IFUA: { type: "string" },
                            FundCode: { type: "string" },
                            FundName: { type: "string" },
                            AmountCash: { type: "number" },
                            AmountUnit: { type: "number" },
                            FeePercent: { type: "number" },
                            BICCode: { type: "string" },
                            BankAcc: { type: "string" },
                            PaymentDate: { type: "string" },
                            TransferType: { type: "string" },
                            TransferTypeDesc: { type: "string" },
                            ReferenceNumber: { type: "string" },
                        }
                    }
                }
            });
    }
    var gridData;

    function initGrid() {
        $("#gridData").empty();
        if ($("#FilterDate").val() == null || $("#FilterDate").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var URL = window.location.origin + "/Radsoft/DailyTransactionForInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(URL);
        }
        gridData = $("#gridData").kendoGrid({
            dataSource: dataSourceApproved,
            height: 650,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FORM S-INVEST TRANSACTION"
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
            dataBound: onDataBoundApproved,
            resizable: true,
            toolbar: ["excel"],
            columns: [
                //{ command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "PaymentDate", title: "PaymentDate", width: 120, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },

                { field: "DailyTransactionForInvestmentPK", title: "SysNo.", width: 95 },
                { field: "TransactionType", title: "Transaction Type", width: 120 },
                { field: "TransactionDate", title: "Trx. Date", width: 150, template: "#= kendo.toString(kendo.parseDate(TransactionDate), 'dd/MM/yyyy')#" },
                { field: "RefNumber", title: "Ref Number", width: 100 },
                { field: "SellingAgentCode", title: "Selling Agent Code", width: 150 },
                { field: "SellingAgentName", title: "Selling Agent Name", width: 200 },

                { field: "FundName", title: "Fund Name", width: 200 },
                { field: "AmountCash", title: "Amount Nominal", format: "{0:n2}", width: 200 },
                { field: "AmountUnit", title: "Amount Unit", format: "{0:n4}", width: 200 },
                { field: "FeePercent", title: "Fee Percent", template: "#: FeePercent  # %", width: 200 },
                { field: "FundCode", title: "Fund Code", width: 100 },
                { field: "IFUA", title: "IFUA", width: 200 },
                { field: "BICCode", title: "BIC Code", width: 200 },
                { field: "BankAcc", title: "Bank Acc", width: 200 },
                { field: "TransferType", hidden: true, title: "Transfer Type", width: 200 },
                { field: "TransferTypeDesc", title: "Transfer Type", width: 200 },
                { field: "ReferenceNumber", title: "Reference Number", width: 200 },
            ]
        }).data("kendoGrid");
        gridData.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;
    }

    function refresh() {
        var newDS = getDataSource(window.location.origin + "/Radsoft/DailyTransactionForInvestment/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridData").data("kendoGrid").setDataSource(newDS);

    }
    $('#chbB').change(function (ev) {

        var checked = ev.target.checked;

        oldPageSizeApproved = gridData.dataSource.pageSize();
        gridData.dataSource.pageSize(gridData.dataSource.data().length);

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

        gridData.dataSource.pageSize(oldPageSizeApproved);

    });


    //$("#showSelectedApproved").bind("click", function () {
    //    var checked = [];
    //    for (var i in checkedApproved) {
    //        if (checkedApproved[i]) {
    //            checked.push(i);
    //        }
    //    }
    //    console.log(checked + ' ' + checked.length);
    //});

    function selectRowApproved() {
        var checked = this.checked,
            rowA = $(this).closest("tr"),
            gridData = $("#gridData").data("kendoGrid"),
            dataItemZ = gridData.dataItem(rowA);

        checkedIds[dataItemZ.DailyTransactionForInvestmentPK] = checked;
        if (checked) {
            rowA.addClass("k-state-selected");
        } else {
            rowA.removeClass("k-state-selected");
        }
    }

    function onDataBoundApproved(e) {

        var view = this.dataSource.view();
        for (var i = 0; i < view.length; i++) {
            if (checkedIds[view[i].DailyTransactionForInvestmentPK]) {
                this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                    .addClass("k-state-selected")
                    .find(".checkboxApproved")
                    .attr("checked", "checked");
            }
        }
    }


    $("#BtnMoveToSubsRedemp").click(function (e) {
        alertify.confirm("Are you sure want to Move To SubsRedemp ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/DailyTransactionForInvestment_Validate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data.Result == 1) {
                            alertify.alert("Validation Not Pass, Please Check Fund : " + data.Fund + " and Selling Agent : " + data.FundClient + " and Transaction Type : " + data.TrxType);
                            $.unblockUI();
                        } else {
                            var DailyTransactionForInvestment = {
                                EntryUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/MoveToSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(DailyTransactionForInvestment),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    $.unblockUI();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });
                        }
                    }
                });
            }
        });
    });

    $("#BtnImportAPERDSummary").click(function () {
        document.getElementById("FileImportAPERDSummary").click();
    });

    $("#FileImportAPERDSummary").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportAPERDSummary").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ImportAPERDSummary", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "APERDSummary_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportAPERDSummary").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportAPERDSummary").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportAPERDSummary").val("");
        }
    });

    $("#BtnReport").click(function () {

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/DailyTransactionForInvestment/Report/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#FilterDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        var newwindow = window.open(data, '_blank'); // ini untuk tarik report PDF tambah newtab

                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });
});