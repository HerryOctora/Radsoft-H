$(document).ready(function () {
    document.title = 'FORM BANK';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //1
    initButton();

    initWindow();

    function initButton() {
        $("#BtnSearch").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

    }

    function initWindow() {

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
    }

    function getDataSourceHistoricalTransaction() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     type: 'POST',
                                     url: window.location.origin + "/Radsoft/ClientDashBoard/GetDataClientHistoricalTransactionByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                 }, parameterMap: function (data, operation) {
                                     return {
                                         Name: $('#Name').val(),
                                         Email: $('#Email').val(),
                                         PhoneNo: $('#PhoneNo').val(),
                                         KTPNo: $('#KTPNo').val(),
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
                             Status: { type: "string" },
                             Date: { type: "date" },
                             TransactionType: { type: "string" },
                             Fund: { type: "string" },
                             Amount: { type: "decimal" },
                             NAB: { type: "decimal" },
                             Unit: { type: "decimal" },
                             Description: { type: "string" },

                         }
                     }
                 }
             });
    }


    function getDataSourceSummaryTransaction() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     type: 'POST',
                                     url: window.location.origin + "/Radsoft/ClientDashBoard/GetDataClientSummaryTransactionByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                 }, parameterMap: function (data, operation) {
                                     return {
                                         Name: $('#Name').val(),
                                         Email: $('#Email').val(),
                                         PhoneNo: $('#PhoneNo').val(),
                                         KTPNo: $('#KTPNo').val(),
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
                             Fund: { type: "string" },
                             Unit: { type: "decimal" },
                             Avg: { type: "decimal" },
                             TotalBuy: { type: "decimal" },
                             NAB: { type: "decimal" },
                             MarketValue: { type: "decimal" },
                             ProfitLoss: { type: "decimal" },
                             ProfitLossPercent: { type: "decimal" },

                         }
                     }
                 }
             });
    }


    $("#BtnSearch").click(function () {
        $('#gridHistoricalTransaction').show();
        $('#gridSummaryTransaction').show();
        initListHistoricalTransaction();
        initListSummaryTransaction();
    });

    function initListHistoricalTransaction() {
        var dsListCountry = getDataSourceHistoricalTransaction();
        $("#gridHistoricalTransaction").empty();
        $("#gridHistoricalTransaction").kendoGrid({
            dataSource: dsListCountry,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Historical Transaction"
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
                { field: "Status", title: "Status", width: 150 },
                { field: "Date", title: "Date", width: 130, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MM/yyyy')#" },
                { field: "TransactionType", title: "Transaction Type", width: 150 },
                { field: "Fund", title: "Fund", width: 200 },
                { field: "Amount", title: "Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "NAB", title: "NAV", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "Unit", title: "Unit Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "Description", title: "Description", width: 200 }

                ]
        });
    }

    function initListSummaryTransaction() {
        var dsListCountry = getDataSourceSummaryTransaction();
        $("#gridSummaryTransaction").empty();
        $("#gridSummaryTransaction").kendoGrid({
            dataSource: dsListCountry,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Summary Transaction"
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
                { field: "Fund", title: "Fund Name", width: 200 },
                { field: "Unit", title: "Unit", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "Avg", title: "Avg", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "TotalBuy", title: "Total Buy", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "NAB", title: "NAB", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "ProfitLoss", title: "Profit Loss", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },{
                   field: "ProfitLossPercent", title: "<div style='text-align: right'>Profit and Loss</div>", width: 150,
                   template: '#=kendo.format("{0:p}", ProfitLossPercent / 100)#', attributes: { style: "text-align:right;" }
               },
            ]
        });
    }


    function refresh() {
        initListHistoricalTransaction();
        initListSummaryTransaction();
        var gridHistoricalTransaction = $("#gridHistoricalTransaction").data("kendoGrid");
        gridHistoricalTransaction.dataSource.read();

        var gridSummaryTransaction = $("#gridSummaryTransaction").data("kendoGrid");
        gridSummaryTransaction.dataSource.read();
    }

});
