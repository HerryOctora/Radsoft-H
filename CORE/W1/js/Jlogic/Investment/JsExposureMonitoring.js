$(document).ready(function () {
    document.title = 'FORM EXPOSURE MONITORING';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var _valuedate;
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

        $("#BtnGenerateExposureMonitoring").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnGenerateAll").kendoButton({
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



        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            refresh();
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboByMaturityDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    index: 0,
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
            } else {
                refresh();
            }
        }


        WinInformationExposure = $("#WinInformationExposure").kendoWindow({
            height: 500,
            title: "* Fund Exposure",
            visible: false,
            width: 1400,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinInformationExposureClose
        }).data("kendoWindow");


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
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Exposure: { type: "number" },
                             ExposureID: { type: "string" },
                             Parameter: { type: "number" },
                             ParameterDesc: { type: "string" },
                             ExPosurePercent: { type: "number" },
                             WarningMaxExposure: { type: "number" },
                             MaxExposurePercent: { type: "number" },
                             WarningMinExposure: { type: "number" },
                             MinExposurePercent: { type: "number" },
                             MarketValue: { type: "number" },
                             WarningMaxValue: { type: "number" },
                             MaxValue: { type: "number" },
                             WarningMinValue: { type: "number" },
                             MinValue: { type: "number" },
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
        $("#gridExposureMonitoringApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        if ($("#FilterFundID").val() == "") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }



        var ExposureMonitoringApprovedURL = window.location.origin + "/Radsoft/ExposureMonitoring/GetDataExposureMonitoringByDateByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID,
        dataSourceApproved = getDataSource(ExposureMonitoringApprovedURL);

        var grid = $("#gridExposureMonitoringApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Exposure Monitoring"
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
            dataBound: gridInformationExposureOnDataBound,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "FundPK", title: "FundPK", hidden: true },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "Exposure", title: "Exposure", width: 0 },
                { field: "ExposureID", title: "ExposureID", width: 200 },
                { field: "Parameter", title: "Parameter", width: 0 },
                { field: "ParameterDesc", title: "Parameter", width: 200 },
                {
                    field: "ExposurePercent", title: "Exp %", width: 100, format: "{0:n4}",
                    template: "#: ExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMaxExposure", title: "Warn Max %", width: 100, format: "{0:n4}",
                    template: "#: WarningMaxExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MaxExposurePercent", title: "Max %", width: 100, format: "{0:n4}",
                    template: "#: MaxExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "WarningMinExposure", title: "Warn Min %", width: 100, format: "{0:n4}",
                    template: "#: WarningMinExposure  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "MinExposurePercent", title: "Min %", width: 100, format: "{0:n4}",
                    template: "#: MinExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "MarketValue", title: "Value", width: 150, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMaxValue", title: "Warn Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MaxValue", title: "Max", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "WarningMinValue", title: "Warn Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "MinValue", title: "Min", width: 100, format: "{0:n2}", attributes: { style: "text-align:right;" } },

                { field: "AlertWarningMaxExposure", title: "AlertWarningMaxExposure", hidden: true },
                { field: "AlertMaxExposure", title: "AlertMaxExposure", hidden: true },
                { field: "AlertWarningMinExposure", title: "AlertWarningMinExposure", hidden: true },
                { field: "AlertMinExposure", title: "AlertMinExposure", hidden: true },
                { field: "AlertWarningMaxValue", title: "AlertWarningMaxValue", hidden: true },
                { field: "AlertMaxValue", title: "AlertMaxValue", hidden: true },
                { field: "AlertWarningMinValue", title: "AlertWarningMinValue", hidden: true },
                { field: "AlertMinValue", title: "AlertMinValue", hidden: true },
            ]
        }).data("kendoGrid");


    }

    function gridInformationExposureOnDataBound(e) {
        var grid = $("#gridExposureMonitoringApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertMaxExposure == true || row.AlertMinExposure == true || row.AlertMaxValue == true || row.AlertMinValue == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureMaxAlert");
            }
            else if (row.AlertWarningMaxExposure == true || row.AlertWarningMinExposure == true || row.AlertWarningMaxValue == true || row.AlertWarningMinValue == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("OMSExposureWarningMaxAlert");
            }  
        });
    }

    function getDataSourceInformationExposure(_url) {
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
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Exposure: { type: "number" },
                             ExposureID: { type: "string" },
                             Parameter: { type: "number" },
                             ParameterDesc: { type: "string" },
                             MarketValue: { type: "number" },
                             AUM: { type: "number" },
                             ExposurePercent: { type: "number" },


                         }
                     }
                 }
             });
    }


    function showDetails(e) {
        var grid = $("#gridExposureMonitoringApproved").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        if (dataItemX.Exposure == 0) {
            _type = 0;
        }
        else {
            _type = dataItemX.Exposure;
        }
        if (dataItemX.Parameter == 0) {
            _param = 0;
        }
        else {
            _param = dataItemX.Parameter;
        }
        if (dataItemX.FundPK == 0) {
            _fundID = 0;
        }
        else {
            _fundID = dataItemX.FundPK;
        }

        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }



        $("#gridInformationExposure").empty();

        var Info = window.location.origin + "/Radsoft/ExposureMonitoring/GetDataDetailExposureMonitoringByDateByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID + "/" + _type + "/" + _param,
            dataSourceInformationExposure = getDataSourceInformationExposure(Info);


        gridInformationExposure = $("#gridInformationExposure").kendoGrid({
            dataSource: dataSourceInformationExposure,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "300px",
            excel: {
                fileName: "Information_FundExposure.xlsx"
            },
            columns: [
                { field: "FundPK", title: "FundPK", hidden: true, width: 200 },
                { field: "FundID", title: "FundID", width: 150 },
                { field: "Exposure", title: "Exposure", hidden: true, width: 0 },
                { field: "ExposureID", title: "Name", width: 150 },
                { field: "Parameter", title: "Parameter", hidden: true, width: 0 },
                { field: "ParameterDesc", title: "Parameter", width: 150 },
                { field: "InstrumentPK", title: "InstrumentPK", hidden: true, width: 200 },
                { field: "InstrumentID", title: "InstrumentID", width: 150 },
                { field: "MarketValue", title: "Value", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "AUM", title: "AUM", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                {
                    field: "ExposurePercent", title: "Exp %", width: 75, format: "{0:n4}",
                    template: "#: ExposurePercent  # %",
                    attributes: { style: "text-align:right;" }
                },

            ]
        }).data("kendoGrid");


        WinInformationExposure.center();
        WinInformationExposure.open();

    }



    function onWinInformationExposureClose() {

        $("#gridInformationExposure").empty();

    }

    $("#BtnRefresh").click(function () {
        refresh();
    });
    
    $("#BtnGenerateExposureMonitoring").click(function (e) {
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate Data ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/ExposureMonitoring/GenerateExposureMonitoringByDateByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
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

    $("#BtnGenerateAll").click(function (e) {
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want to Generate Data ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/ExposureMonitoring/GenerateExposureMonitoringByDateByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/0",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
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




});
