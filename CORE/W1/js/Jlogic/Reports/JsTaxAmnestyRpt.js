$(document).ready(function () {
    var win;
    var button = $("#ShowGrid").data("kendoButton");
    var checkedIds = {};

    var GlobValidator = $("#WinTaxAmnestyRpt").kendoValidator().data("kendoValidator");
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
            }
        }
    }



    function initWindow() {

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

        //ClearAttribute();
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
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

        $("#DownloadMode").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "Excel" },
               { text: "Txt" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeDownloadMode,
            index: 0
        });
        function OnChangeDownloadMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
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


        InitName();

        function InitName() {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Apollo" },
                       { text: "Dirjen" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();

                    if (this.text() == 'Apollo') {
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Dirjen') {
                        $("#LblFundFrom").show();

                    }

                }
            
        }

        $("#Status").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "POSTED ONLY", value: 1 },
               { text: "REVISED ONLY", value: 2 },
               { text: "APPROVED ONLY", value: 3 },
               { text: "PENDING ONLY", value: 4 },
               { text: "HISTORY ONLY", value: 5 },
               { text: "POSTED & APPROVED", value: 6 },
               { text: "POSTED & APPROVED & PENDING", value: 7 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatus,
            index: 6
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#PageBreak").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "TRUE", value: true },
               { text: "FALSE", value: false },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangePageBreak,
            index: 0
        });

        function OnChangePageBreak() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

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
                    dataSource: data
                });
                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });




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
                    dataSource: data
                });
                $("#FundClientFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //AGENT
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentFrom").kendoMultiSelect({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#AgentFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //Department
        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentFrom").kendoMultiSelect({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    index: 0

                });

                $("#DepartmentFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        win = $("#WinTaxAmnestyRpt").kendoWindow({
            height: 550,
            title: "* Tax Amnesty Report",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        //function ClearAttribute() {
        //    $("#LblDateFrom").hide();
        //    $("#LblDateTo").hide();
        //    $("#LblFundFrom").hide();
        //    $("#LblFundTo").hide();
        //    $("#LblClientFrom").hide();
        //    $("#LblClientTo").hide();
        //    $("#LblAgentFrom").hide();
        //    $("#LblAgentTo").hide();
        //    $("#LblDepartmentFrom").hide();
        //    $("#LblDepartmentTo").hide();
        //}

        function ClearData() {
            $("#ValueDateFrom").val("");
            $("#ValueDateTo").val("");
            $("#AgentFrom").val("");
            $("#DepartmentFrom").val("");

        }

        win.center();
        win.open();
    }

    $("#BtnDownload").click(function () {
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

            var ArrayAgentFrom = $("#AgentFrom").data("kendoMultiSelect").value();
            var stringAgentFrom = '';
            for (var i in ArrayAgentFrom) {
                stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

            }
            stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

            var ArrayDepartmentFrom = $("#DepartmentFrom").data("kendoMultiSelect").value();
            var stringDepartmentFrom = '';
            for (var i in ArrayDepartmentFrom) {
                stringDepartmentFrom = stringDepartmentFrom + ArrayDepartmentFrom[i] + ',';

            }
            stringDepartmentFrom = stringDepartmentFrom.substring(0, stringDepartmentFrom.length - 1)

            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Download data ?", function (e) {
                if (e) {
                    $.blockUI({});
                    if ($('#Name').val() == "Apollo") {
                        var TaxAmnestyRpt = {

                            ReportName: $('#Name').val(),
                            ValueDateFrom: $('#ValueDateFrom').val(),
                            ValueDateTo: $('#ValueDateTo').val(),
                            FundFrom: stringFundFrom,
                            FundClientFrom: stringFundClientFrom,
                            AgentFrom: stringAgentFrom,
                            DepartmentFrom: stringDepartmentFrom,
                            PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                            Status: $("#Status").data("kendoComboBox").value(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                            Message: $('#Message').val(),
                        };
                        var _url;
                        if ($("#DownloadMode").val() == "Excel")
                        {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Reports/GenerateApollo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(TaxAmnestyRpt),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $.unblockUI();
                                    //alertify.alert(data);
                                    window.location = data;

                                    $("#downloadSInvest").attr("href", data);
                                    $("#downloadSInvest").attr("download", "Radsoft_NKPD.txt");
                                    document.getElementById("downloadSInvest").click();

                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
                        else
                        {
                            _url = 
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Reports/GenerateApolloToTxt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(TaxAmnestyRpt),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $.unblockUI();

                                    $("#downloadTaxAmnesty").attr("href", data);
                                    $("#downloadTaxAmnesty").attr("download", "ApolloTxt.txt");
                                    document.getElementById("downloadTaxAmnesty").click();

                                },
                                error: function (data) {
                                    $.unblockUI();
                                    alertify.alert("Create Report Failed Or No Data");
                                }
                            });
                        }
                        
                    }
                    else if ($('#Name').val() == "Dirjen") {
                        var TaxAmnestyRpt = {

                            ReportName: $('#Name').val(),
                            ValueDateFrom: $('#ValueDateFrom').val(),
                            ValueDateTo: $('#ValueDateTo').val(),
                            FundFrom: stringFundFrom,
                            FundClientFrom: stringFundClientFrom,
                            AgentFrom: stringAgentFrom,
                            DepartmentFrom: stringDepartmentFrom,
                            PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                            Status: $("#Status").data("kendoComboBox").value(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                            Message: $('#Message').val(),
                        };
                        var _url;
                        if ($("#DownloadMode").val() == "Excel") {
                            _url = window.location.origin + "/Radsoft/Reports/TaxAmnestyReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")
                        }
                        else {
                            _url = window.location.origin + "/Radsoft/Reports/TaxAmnestyReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")
                        }
                        $.ajax({
                            url: _url,
                            type: 'POST',
                            data: JSON.stringify(TaxAmnestyRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                //alertify.alert(data);
                                window.location = data;
                                //var newwindow = window.open(data, '_blank');
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                    else {

                        var TaxAmnestyRpt = {
                            ReportName: $('#Name').val(),
                            ValueDateFrom: $('#ValueDateFrom').val(),
                            ValueDateTo: $('#ValueDateTo').val(),
                            FundFrom: stringFundFrom,
                            FundClientFrom: stringFundClientFrom,
                            AgentFrom: stringAgentFrom,
                            DepartmentFrom: stringDepartmentFrom,
                            PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                            Status: $("#Status").data("kendoComboBox").value(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                            Message: $('#Message').val(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/TaxAmnestyReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(TaxAmnestyRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                var newwindow = window.open(data, '_blank');
                                //var multiSelect = $('#gridFundClient').data("kendoMultiSelect");
                                //checkedIds = {};
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert(data.responseText);
                            }
                        });
                    }

                }
                //}
                checkedIds = [];
            })
        }
        else {
            alertify.alert("Please Choose Client!")
        }


    });

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Report");
            }
        });
    });



    function validatedata(All) {
        if ($('#Name').val() == 'Client Unit Position Customized' || $('#Name').val() == 'Newton Report' || $('#Name').val() == 'Unit Activity For Accounting' ||
            $('#Name').val() == 'Client Unit Position' || $('#Name').val() == 'Historical Transaction' || $('#Name').val() == 'Daily Transaction Report For All' ||
            $('#Name').val() == 'Monthly Transaction By Fund And InvestorType And FundClient' || $('#Name').val() == 'Perhitungan Agent Fee Report' || $('#Name').val() == 'Rebate Generali Fee Report' ||
            $('#Name').val() == 'Monthly Report 100Mil' || $('#Name').val() == 'Laporan Rekap Unit Penyertaan' || $('#Name').val() == 'Total Transaction Report Fund' ||
            $('#Name').val() == 'Total Transaction Report Client' || $('#Name').val() == 'Daily Switching Instruction' || $('#Name').val() == 'Daily Redemption Instruction' ||
            $('#Name').val() == 'Daily Subscription Instruction' || $('#Name').val() == 'Fund Balance Detail' || $('#Name').val() == 'Customer Portfolio All Fund Client' ||
            $('#Name').val() == 'Laporan Akun Bulanan Client' || $('#Name').val() == 'Fund Balance By Categories' || $('#Name').val() == 'Daily Transaction Report' ||
            $('#Name').val() == 'Management Fee Per Client' || $('#Name').val() == 'Summary Client' || $('#Name').val() == 'Client Report' ||
            $('#Name').val() == 'Historical Transaction Per Client' || $('#Name').val() == 'Transaction Monitoring') {
            if ($('#BitAllClient').is(":checked") == false && All == 0) {
                return 0;
            }
            else {
                return 1;
            }
        }
        else {
            return 1;
        }
    }


    $("#CloseGrid").click(function () {
        var All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }
        WinFundClient.close();
    });

    function HideParameter() {
        $("#LblDateFrom").hide();
        $("#LblDateTo").hide();
        $("#LblFundFrom").hide();
        $("#LblFundTo").hide();
        $("#LblClientFrom").hide();
        $("#LblClientTo").hide();
        $("#LblAgentFrom").hide();
        $("#LblAgentTo").hide();
        $("#LblDepartmentFrom").hide();
        $("#LblDepartmentTo").hide();
        $("#LblBegDate").hide();
    }


    function onWinFundClientClose() {
        WinFundClient.close();
    }

    function getDateFrom(_reportName) {
        if (_reportName == 'Historical Transaction') {
            $("#LblBegDate").show();
        }
        else {
            $("#LblBegDate").hide();
        }

    }

    $("#BegDate").change(function () {
        if (this.checked == true) {
            // disable button
            $("#ValueDateFrom").data("kendoDatePicker").value(new Date('01/01/2000'))
            //$("#ValueDateFrom").attr('readonly', true);
            $("#ValueDateFrom").attr('readonly', true);
            $("#ValueDateFrom").data("kendoDatePicker").enable(false);
        }
        else {
            $("#ValueDateFrom").data("kendoDatePicker").value(new Date())
            $("#ValueDateFrom").attr('readonly', false);
            $("#ValueDateFrom").data("kendoDatePicker").enable(true);
        }
    });
});