$(document).ready(function () {
    var WinHoldingPeriod;
    var _Unit, _FundPK, _FundClientPK;
    var checkedIds = {};

    initWindow();
    InitHoldingPeriod();

    function initWindow() {
        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: onChangeDate
        });

        function onChangeDate() {
            if ($("#Unit").val() == "")
                _Unit = 0;
            else
                _Unit = $("#Unit").val();
            //refresh();
        }

        getFundClientCashRefByFundClientPK(0, 0);

        _Unit = 0

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#ParamHoldingFund").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                    change: onchangeFund
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onchangeFund() {
            if ($("#Unit").val() == "")
                _Unit = 0;
            else
                _Unit = $("#Unit").val();
            var All = 0;
            if ($('#BitAllClient').is(":checked") == true) {
                All = 0;
            }

            else {
                All = [];
                for (var i in checkedIds) {
                    if (checkedIds[i]) {
                        All.push(i);
                    }
                }
            }



            var stringFundFrom = '';
            if ($('#ParamHoldingFund').val() != undefined)
                stringFundFrom = $('#ParamHoldingFund').val();

            var ArrayFundClientFrom = All;
            var stringFundClientFrom = '';
            for (var i in ArrayFundClientFrom) {
                stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

            }
            stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

            if (stringFundFrom == "") {
                stringFundFrom = "0";
            }

            if (stringFundClientFrom == "") {
                stringFundClientFrom = "0";
            }

            if ($("#Unit").val() == "") {
                _Unit = 0;
            }

            else {
                _Unit = $("#Unit").val();
                //refresh();
            }
          
       

            getFundClientCashRefByFundClientPK(stringFundFrom, stringFundClientFrom);
            //refresh();
        }

        $("#ParamHoldingFundClient").click(function () {
            for (var i in checkedIds) {
                checkedIds[i] = null
            }
            WinFundClient.center();
            WinFundClient.open();
            LoadData();
        });



        $("#CloseGrid").click(function () {
            var All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }
            WinFundClient.close();



            var All = 0;
            if ($('#BitAllClient').is(":checked") == true) {
                All = 0;
            }

            else {
                All = [];
                for (var i in checkedIds) {
                    if (checkedIds[i]) {
                        All.push(i);
                    }
                }
            }



            var stringFundFrom = '';
            if ($('#ParamHoldingFund').val() != undefined)
                stringFundFrom = $('#ParamHoldingFund').val();

            var ArrayFundClientFrom = All;
            var stringFundClientFrom = '';
            for (var i in ArrayFundClientFrom) {
                stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

            }
            stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

            if (stringFundFrom == "") {
                stringFundFrom = "0";
            }

            if (stringFundClientFrom == "") {
                stringFundClientFrom = "0";
            }

            if ($("#Unit").val() == "") {
                _Unit = 0;
            }

            else {
                _Unit = $("#Unit").val();
                //refresh();
            }
         
          

            getFundClientCashRefByFundClientPK(stringFundFrom, stringFundClientFrom);
        });


        $("#Unit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            change: onchangeUnitAmount
        });
        function onchangeUnitAmount() {
            _Unit = $("#Unit").val();
            //refresh();
        }

        $("#ParamHoldingFundClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#CloseGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        WinHoldingPeriod = $("#WinHoldingPeriod").kendoWindow({
            height: 800,
            title: "HOLDING PERIOD",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        WinFundClient = $("#WinFundClient").kendoWindow({
            height: 500,
            title: "Fund Client List Detail",
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

    }

    function LoadData() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/FundClient/GetFundClientComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            //cache: false,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundClientPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },
                        IFUA: { type: "string" },
                        SID: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridFundClient = $("#gridFundClient").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            //define dataBound event handler
            dataBound: onDataBound,
            selectable: "multiple",
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                //define template column with checkbox and attach click event handler
                {
                    //select all on grid
                    //headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    //end select all on grid
                    template: "<input type='checkbox' class='checkbox'/>"
                }

                ,
                {
                    field: "ID",
                    title: "Client ID",
                    width: "120px"
                }, {
                    field: "Name",
                    title: "Client Name",
                    width: "300px"
                }, {
                    field: "IFUA",
                    title: "IFUA Code",
                    width: "185px"
                }, {
                    field: "SID",
                    title: "SID",
                    width: "150px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");



        //bind click event to the checkbox
        gridFundClient.table.on("click", ".checkbox", selectRow);

        //on click of the checkbox:
        function selectRow() {

            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridFundClient = $("#gridFundClient").data("kendoGrid"),
                dataItemZ = gridFundClient.dataItem(rowA);

            for (var i in checkedIds) {
                checkedIds[i] = null
            }


            checkedIds[dataItemZ.FundClientPK] = checked;
            if (checked) {
                //-select the row
                gridFundClient.clearSelection();
                rowA.addClass("k-state-selected");



            } else {
                //-remove selection
                gridFundClient.clearSelection();
                rowA.removeClass("k-state-selected");
            }


        }

        //on dataBound event restore previous selected rows:
        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundClientPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkbox")
                        .attr("checked", "checked");
                }
            }
        }
    }


    var RedempFeepercent;
    var TotalFeeAmount;
    var RedempUnit;
    var rowIdx;
    var colIdx;
    var row;
    var uid;
    var UnitDecimalPlaces;

    function InitDataSourceHoldingPeriod() {

        var All = 0;
        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
        }

        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }
        }


        
        var stringFundFrom = '';
        if ($('#ParamHoldingFund').val() != undefined)
            stringFundFrom = $('#ParamHoldingFund').val();

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        if (stringFundFrom == "") {
            stringFundFrom = "0";
        }

        if (stringFundClientFrom == "") {
            stringFundClientFrom = "0";
        }

        return new kendo.data.DataSource(

            {

                transport:
                {

                    read:
                    {

                        url: window.location.origin + "/Radsoft/HoldingPeriod/InitDataHoldingPeriod/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + stringFundFrom + "/" + stringFundClientFrom + "/" + kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _Unit,
                        dataType: "json"
                    }
                },
                batch: true,
                cache: false,
                error: function (e) {
                    alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                    this.cancelChanges();
                },
                pageSize: 25,
                schema: {
                    model: {
                        fields: {

                            FundID: { type: "string", editable: false},
                            FundClientName: { type: "string", editable: false },
                            ValueDate: { type: "date", editable: false },
                            HoldingPeriod: { type: "number", editable: false },
                            TotalSubs: { type: "number", editable: false },
                            TakenOut: { type: "number", editable: false },
                            Remaining: { type: "number", editable: false },
                            RedempUnit: { type: "number", editable: false },
                            TotalFeeAmount: { type: "number" },
                            RedempFeePercent: { type: "number" },
                            UnitDecimalPlaces: { type: "number", editable: false  }

                        }
                    }
                }
            });
    }

    function InitHoldingPeriod() {
        $("#gridHoldingPeriod").empty();
        var dsListHoldingPeriod = InitDataSourceHoldingPeriod();
        var gridHoldingPeriod = $("#gridHoldingPeriod").kendoGrid({
            dataSource: dsListHoldingPeriod,
            height: 500,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            resizable: true,
            editable: "incell",
            selectable: "cell",
            dataBound: databoundHoldingPeriod,
            columns: [
                { field: "FundID", title: "Fund", width: 120 },
                { field: "FundClientName", title: "Fund Client", width: 150 },
                { field: "ValueDate", title: "Subs Date", width: 100, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "HoldingPeriod", title: "Holding Period (Month)", format: "{0:n0}", attributes: { style: "text-align:right;" }, width: 100 },
                { field: "TotalSubs", title: "Total Subs", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "TakenOut", title: "Taken Out", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "Remaining", title: "Remaining Balance", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "RedempUnit", title: "Redemp Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120},
                { field: "RedempFeePercent", title: "Red Fee Percent", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 100 },
                { field: "TotalFeeAmount", title: "Total Fee Unit", format: "{0:n6}", attributes: { style: "text-align:right;" }, width: 120 },
                { field: "UnitDecimalPlaces", title: "UnitDecimalPlaces", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120, hidden: true},

            ]
        }).data("kendoGrid");


        WinHoldingPeriod.center();
        WinHoldingPeriod.open();

    }


    function databoundHoldingPeriod() {
        RedempFeepercent = 0;
        TotalFeeAmount = 0;
        RedempUnit = 0;
        var grid = $("#gridHoldingPeriod").data("kendoGrid");
        $(grid.tbody).on()
        $(grid.tbody).on("focusout", "td", function (e) {
            row = $(this).closest("tr");
            rowIdx = $("tr", grid.tbody).index(row);
            colIdx = $("td", row).index(this);
            RedempFeepercent = grid.dataItem(row).RedempFeePercent;
            TotalFeeAmount = grid.dataItem(row).TotalFeeAmount;
            RedempUnit = grid.dataItem(row).RedempUnit;
            uid = grid.dataItem(row).uid;
            UnitDecimalPlaces = grid.dataItem(row).UnitDecimalPlaces;

            if (colIdx == 8)
                $("#gridHoldingPeriod").find("tr[data-uid='" + uid + "'] td:eq(9)").text((RedempUnit * RedempFeepercent / 100).toFixed(UnitDecimalPlaces));
            else
                $("#gridHoldingPeriod").find("tr[data-uid='" + uid + "'] td:eq(8)").text((TotalFeeAmount / RedempUnit * 100).toFixed(UnitDecimalPlaces));

        });

        
        
    }
    
    function refresh() {
        InitHoldingPeriod();
    }

    var GlobValidator = $("#WinHoldingPeriod").kendoValidator().data("kendoValidator");

    function validateData() {

        if ($("#ParamDate").val() != "") {
            var _date = Date.parse($("#ParamDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date");
                return 0;
            }
        }
        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }

        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function getFundClientCashRefByFundClientPK(_fundPK, _fundClientPK) {
        $("#CashRefPK").val("");
        //check data fundclietcashref
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClientCashRef/CheckDataFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 1) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefByFundClientCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + _fundClientPK + "/RED",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#CashRefPK").kendoComboBox({
                                dataValueField: "FundCashRefPK",
                                dataTextField: "ID",
                                dataSource: data,
                                filter: "contains",
                                suggest: true,
                                index: 0,
                                change: onchangeCashRef

                            });
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/RED",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#CashRefPK").kendoComboBox({
                                dataValueField: "FundCashRefPK",
                                dataTextField: "ID",
                                dataSource: data,
                                filter: "contains",
                                suggest: true,
                                change: onchangeCashRef
                            });
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onchangeCashRef() {
            if ($("#Unit").val() == "") {
                _Unit = 0;
            }

            else {
                _Unit = $("#Unit").val();
                //refresh();
            }
           
        }

    }

    $("#GenerateClientRedemption").click(function () {
        var _RedempDate;
        var _CashRefPK;

        var All = 0;
        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
        }

        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }
        }

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)


        if (stringFundClientFrom == "") {
            stringFundClientFrom = "0";
        }

        var flagChange = 0;
        var _ClientRedemption = [];
        var gridDataArray = $('#gridHoldingPeriod').data('kendoGrid')._data;
        for (var index = 0; index < gridDataArray.length; index++) {

            var _m = {
                FundPK: $('#ParamHoldingFund').val(),
                FundClientPK: stringFundClientFrom,
                RedempDate: $('#ParamDate').val(),
                CashRefPK: $('#CashRefPK').val(),
                RedempUnit: gridDataArray[index]["RedempUnit"],
                RedempFeePercent: gridDataArray[index]["RedempFeePercent"],
                TotalFeeAmount: gridDataArray[index]["TotalFeeAmount"]
            }
            _ClientRedemption.push(_m);
            flagChange = 1;
        };

        var val = validateData();
        if (val == 1) {
            if (flagChange == 0) {
                alertify.alert('No Data To Generate');
                return;
            }
            $.ajax({
                url: window.location.origin + "/Radsoft/HoldingPeriod/AddClientRed/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(_ClientRedemption),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ParamHoldingFund").data("kendoComboBox").value("");
                    $("#CashRefPK").data("kendoComboBox").value("");
                    $("#Unit").data("kendoNumericTextBox").value("");
                    refresh();
                    alertify.alert(data);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }



    });


    $("#BtnCalculateTrx").click(function () {
        InitHoldingPeriod();
    });

});