$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinGenerateUnitFeeSummary").kendoValidator().data("kendoValidator");
    var checkedIds = {};
    var checkedName = {};
    var button = $("#ShowGrid").data("kendoButton");

    var gridHeight = screen.height - 300;



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

    //3
    initGrid();

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
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
            //define template column with checkbox and attach click event handler
            { template: "<input type='checkbox' class='checkbox' />" }, {
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

            checkedIds[dataItemZ.FundClientPK] = checked;
            checkedName[dataItemZ.Name] = checked;
            if (checked) {
                //-select the row
                rowA.addClass("k-state-selected");



            } else {
                //-remove selection
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
                if (checkedName[view[i].Name]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                    .addClass("k-state-selected")
                    .find(".checkbox")
                    .attr("checked", "checked");
                }
            }
        }
    }

    function HideParameter() {
        $("#LblDateFrom").hide();
    }
    function initWindow() {


        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnGenerateNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnGenerate.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#ShowGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });


        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
            }
        }


        win = $("#WinGenerateUnitFeeSummary").kendoWindow({
            height: 650,
            title: "Generate Unit Fee Summary",
            visible: false,
            width: 1400,
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

        if (_GlobClientCode == "24")
        {
            $("#GenerateBy").kendoComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "For Client", value: 1 },
                    { text: "For Agent CSR", value: 2 },
                    { text: "For Unit Summary Standard", value: 3 },
                ],
                filter: "contains",
                suggest: true,
                change: OnChangeGenerateBy,
                value: setCmbGenerateBy()
            });
        }
        else
        {
            $("#GenerateBy").kendoComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "For Client", value: 1 },
                    { text: "For Agent CSR", value: 2 },
                ],
                filter: "contains",
                suggest: true,
                change: OnChangeGenerateBy,
                value: setCmbGenerateBy()
            });
        }
   

        function OnChangeGenerateBy() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#GenerateBy").val() == 1 || $("#GenerateBy").val() == 3) {
                $("#LblClientFrom").show();

            }
            else {
                $("#LblClientFrom").hide();

            }
        }

        function setCmbGenerateBy() {
            return 1;
        }


        $("#BitAllClient").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGrid").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGrid").data("kendoButton").enable(true);
            }
        });

        $("#ShowGrid").click(function () {
            WinFundClient.center();
            WinFundClient.open();
            LoadData();
        });
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
                    dataSource: data,
                    //change: OnChangeFundFrom,

                    //suggest: true,
                    //index: 0
                });
                refresh();


                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            //$("#FundTo").data("kendoComboBox").value($("#FundFrom").data("kendoComboBox").value());
        }

        //FundClient
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetFundClientDetailComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundClientFrom").kendoMultiSelect({
                    dataValueField: "FundClientPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                    //change: OnChangeFundClientFrom,

                    //suggest: true,
                    //index: 0
                });
                refresh();


                $("#FundClientFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundClientFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            //$("#FundClientTo").data("kendoComboBox").value($("#FundClientFrom").data("kendoComboBox").value());
        }

        win.center();
        win.open();
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
                             DailyDataForCommissionRptNewLogPK: { type: "number" },
                             DateFrom: { type: "date" },
                             DateTo: { type: "date" },
                             UsersID: { type: "string" },
                             GenerateTime: { type: "date" },
                             Fund: { type: "string" },
                             Client: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        initGrid()
    }

    function initGrid() {

        $("#gridDailyDataForCommissionRptNewLogApproved").empty();


        var DailyDataForCommissionRptNewLogURL = window.location.origin + "/Radsoft/DailyDataForCommissionRptNewLog/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
        dataSourceApproved = getDataSource(DailyDataForCommissionRptNewLogURL);



        var grid = $("#gridDailyDataForCommissionRptNewLogApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Daily Data For Commission Rpt New Log"
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
                { field: "DailyDataForCommissionRptNewLogPK", title: "SysNo.", width: 95 },
                { field: "DateFrom", title: "Date From", width: 150, template: "#= kendo.toString(kendo.parseDate(DateFrom), 'dd/MMM/yyyy')#" },
                { field: "DateTo", title: "Date To", width: 150, template: "#= kendo.toString(kendo.parseDate(DateTo), 'dd/MMM/yyyy')#" },
                { field: "UsersID", title: "Users ID", width: 150 },
                { field: "GenerateTime", title: "Generate Time", width: 200, format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "Fund", title: "Fund", width: 500 },
                { field: "Client", title: "Client", width: 500 },
                { field: "LastUpdate", title: "Last Update", width: 200, format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");




    }




    $("#BtnGenerate").click(function () {
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
        var val = validatedata(All);
        if (val == 1) {
        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)


        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Generate data ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var GenerateUnitFeeSummary = {

                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        FundFrom: stringFundFrom,
                        FundClientFrom: stringFundClientFrom
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrails/GenerateUnitFeeSummary/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateUnitFeeSummary),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            initGrid()
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
    }
    else {
            alertify.alert("Please Choose Client!")
}

    });


    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Report");
            }
        });
    });


    $("#CloseGrid").click(function () {

        WinFundClient.close();
    });

    function ClearAttribute() {
        $("#LblValueDate").hide();
        $("#LblValueDateFrom").hide();
        $("#LblDateTo").hide();
    }


    function validatedata(All) {

        if ($("#GenerateBy").val() == 2)
            return 1;

        if ($('#BitAllClient').is(":checked") == false && All == 0) {
            return 0;
        }
        else {
            return 1;
        }
    }



    $("#BtnGenerateNew").click(function (e) {
        var All = 0;

        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
            AllName = 0;
        }
        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }

            AllName = [];
            for (var i in checkedName) {
                if (checkedName[i]) {
                    AllName.push(i);
                }
            }


        }
        var val = validatedata(All);
        if (val == 1) {
            var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
            var stringFundFrom = '';
            for (var i in ArrayFundFrom) {
                stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

            }
            stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

            var ArrayFundClientFrom = All;
            var stringFundClientFrom = '';
            for (var i in ArrayFundClientFrom) {
                stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

            }
            stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

            var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
            var stringFundText = '';
            for (var i in FundText) {
                stringFundText = stringFundText + FundText[i].ID + ',';

            }


            var ArrayFundClientText = AllName;
            var stringFundClientText = '';
            for (var i in ArrayFundClientText) {
                stringFundClientText = stringFundClientText + ArrayFundClientText[i] + ',';

            }


            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Generate data ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var GenerateUnitFeeSummary = {

                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        FundFrom: stringFundFrom,
                        FundClientFrom: stringFundClientFrom,
                        FundText: stringFundText,
                        FundClientText: stringFundClientText,
                        EntryUsersID: sessionStorage.getItem("user")
                    };

                    if ($("#GenerateBy").val() == 1) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/EndDayTrails/GenerateUnitFeeSummaryNew/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateUnitFeeSummary),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                alertify.alert(data);
                                refresh();
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText);
                            }
                        });

                    }
                    else if ($("#GenerateBy").val() == 3) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/EndDayTrails/GenerateUnitFeeSummaryStandard/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateUnitFeeSummary),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                alertify.alert(data);
                                refresh();
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText);
                            }
                        });

                    }
                    else {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/EndDayTrails/GenerateCSRFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateUnitFeeSummary),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                alertify.alert(data);
                                $("#gridDailyDataForCommissionRptNewLogApproved").empty();
                                //refresh();
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText);
                            }
                        });
                    }



                }
            });
        }
        else {
            alertify.alert("Please Choose Client!")
        }

    });



  

});