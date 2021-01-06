$(document).ready(function () {
    document.title = 'FORM FUND CLIENT POSITION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinGetNav;
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
     
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnGenerateAvg").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
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
  

        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
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
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
      
     

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
                             FundClientPositionPK: { type: "number" },
                             Date: { type: "date" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             UnitAmount: { type: "number" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
    }

    function initGrid() {

        $("#gridFundClientPositionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var FundClientPositionApprovedURL = window.location.origin + "/Radsoft/FundClientPosition/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(FundClientPositionApprovedURL);

        }

        if (_GlobClientCode == "05") {
            var grid = $("#gridFundClientPositionApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund Client Position"
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
                editable: true,
                resizable: true,
                toolbar: ["excel"],
                columns: [
                    //{ field: "FundClientPositionPK", title: "SysNo.", width: 95 },
                     { command: { text: "Update", click: _updateValue }, title: " ", width: 80 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "FundClientName", title: "Client Name", editable: false, width: 500 },
                    { field: "FundID", title: "Fund ID", editable: false, width: 200 },
                    { field: "UnitAmount", title: "Unit", width: 300, format: "{0:n4}", editor: numericTextBox, attributes: { style: "text-align:right;" } },
                ]
            }).data("kendoGrid");


        }
        else {
            var grid = $("#gridFundClientPositionApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Fund Client Position"
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
                    //{ field: "FundClientPositionPK", title: "SysNo.", width: 95 },
                    { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "SID", title: "SID", width: 150 },
                    { field: "IFUA", title: "IFUA", width: 150 },
                    { field: "FundClientName", title: "Client Name", width: 500 },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "AvgNAV", title: "Avg NAV", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "AUM", title: "AUM", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CostValue", title: "Cost Value", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "Unrealized", title: "Unrealized", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                ]
            }).data("kendoGrid");
        }

    }


    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnGenerateAvg").click(function () {
        var _date = kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate Average for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundClientPosition/GenerateAverageFCP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
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


    function numericTextBox(container, options) {
        $('<input name="' + options.field + '"/>')
            .appendTo(container)
            .kendoNumericTextBox({
                format: "{0:n4}",
                decimals: 4
            });
    }


    function _updateValue(e) {
        if (e) {
            var grid;
            grid = $("#gridFundClientPositionApproved").data("kendoGrid");

            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var FundClientPosition = {
                UnitAmount: dataItemX.UnitAmount,
                FundClientPositionPK: dataItemX.FundClientPositionPK,

            };


            $.ajax({
                url: window.location.origin + "/Radsoft/FundClientPosition/UpdateUnitFundClientPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(FundClientPosition),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Update Success");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }


});
