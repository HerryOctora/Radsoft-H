$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinCustodianRpt").kendoValidator().data("kendoValidator");
    var checkedIds = {};
    function validateData() {

        var currentDateFrom = Date.parse($("#ValueDateFrom").data("kendoDatePicker").value());
        var currentDateTo = Date.parse($("#ValueDateTo").data("kendoDatePicker").value());
        //Check if Date parse is successful
        if (!currentDateFrom) {
            alertify.alert("Wrong Format Date");
            return 0;
        }
        if (!currentDateTo) {
            alertify.alert("Wrong Format Date");
            return 0;
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

                    }
                }
            }
        });



        //Grid definition
        var gridFundClient = $("#gridFundClient").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 250,
            //define dataBound event handler
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
            //define template column with checkbox and attach click event handler
            { template: "<input type='checkbox' class='checkbox' />" }, {
                field: "ID",
                title: "Client ID",
                width: "150px"
            }, {
                field: "Name",
                title: "Client Name",
                width: "300px"
            }, {
                field: "IFUA",
                title: "IFUA Code",
                width: "200px"
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



        //Signature 1
        $.ajax({
            url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Signature1").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature1,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature1() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#Signature1").val() == 0 || $("#Signature1").val() == "") {
                $("#Position1").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position1").val(data.Position);
                    }
                });
            }

        }


        //Signature 2
        $.ajax({
            url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Signature2").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature2,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature2() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#Signature2").val() == 0 || $("#Signature2").val() == "") {
                $("#Position2").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position2").val(data.Position);
                    }
                });
            }
        }

        //Signature 3
        $.ajax({
            url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Signature3").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature3,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature3() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#Signature3").val() == 0 || $("#Signature3").val() == "") {
                $("#Position3").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position3").val(data.Position);
                    }
                });
            }
        }

        //Signature 4
        $.ajax({
            url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Signature4").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature4,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature4() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#Signature4").val() == 0 || $("#Signature4").val() == "") {
                $("#Position4").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position4").val(data.Position);
                    }
                });
            }
        }

        WinFundClient = $("#WinFundClient").kendoWindow({
            height: 500,
            title: "Fund Client List Detail",
            visible: false,
            width: 750,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

        ClearAttribute();
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
            value: new Date(),
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            value: new Date(),
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                if (_GlobClientCode != "01") {
                    $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
                }
            }

        }


        $("#DownloadMode").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "Excel" },
               { text: "PDF" },
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
                       { text: "Aktivitas Bank Custodian Fund" },
                       { text: "Aktivitas Bank Custodian Konvensional" },
                       { text: "Aktivitas Bank Custodian Summary" },
                       { text: "Report NAV per Periode Rekapitulasi_NAB" },


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
                    ClearAttribute();

                    if (this.text() == 'Aktivitas Bank Custodian Fund') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Aktivitas Bank Custodian Konvensional') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Aktivitas Bank Custodian Summary') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Report NAV per Periode Rekapitulasi_NAB') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();                    
                        $("#trFundFrom").show();
                    }
                                        
                }

        }


        $("#Status").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "POSTED ONLY", value: 1 },
               { text: "REVERSED ONLY", value: 2 },
               { text: "APPROVED ONLY", value: 3 },
               { text: "PENDING ONLY", value: 4 },
               { text: "POSTED & APPROVED", value: 5 },
               { text: "POSTED & APPROVED & PENDING", value: 6 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatus,
            value: setCmbStatus()
        });

        function setCmbStatus() {
            return 6;
        }

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

        $("#ShowNullData").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "TRUE", value: true },
               { text: "FALSE", value: false },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeShowNullData,
            index: 0
        });

        function OnChangeShowNullData() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //ACCOUNT

        //
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnlyAll/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountFrom").kendoMultiSelect({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //change: OnChangeAccountFrom,
                    filter: "contains",
                    //suggest: true,
                    //index: 0
                });


                $("#AccountFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function OnChangeAccountFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#AccountTo").data("kendoComboBox").value($("#AccountFrom").data("kendoComboBox").value());
        }

        function OnChangeAccountTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
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
                    dataSource: data
                });
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
            $("#FundClientTo").data("kendoComboBox").value($("#FundClientFrom").data("kendoComboBox").value());
        }

        function OnChangeFundClientTo() {
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
                    dataSource: data,
                    //change: OnChangeFundFrom,

                    //suggest: true,
                    //index: 0
                });


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
            $("#FundTo").data("kendoComboBox").value($("#FundFrom").data("kendoComboBox").value());
        }

        function OnChangeFundTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        //Counterpart
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Counterpart").kendoMultiSelect({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                    //change: OnChangeFundFrom,

                    //suggest: true,
                    //index: 0
                });


                $("#Counterpart").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCounterpart() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            //$("#FundTo").data("kendoComboBox").value($("#Counterpart").data("kendoComboBox").value());
        }

        function OnChangeFundTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        //Instrument
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentFrom").kendoMultiSelect({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    //change: OnChangeInstrumentFrom,
                    filter: "contains",
                    //suggest: true,
                    //index: 0
                });

                //$("#InstrumentTo").kendoComboBox({
                //    dataValueField: "InstrumentPK",
                //    dataTextField: "ID",
                //    dataSource: data,
                //    change: OnChangeyo,
                //    filter: "contains",
                //    suggest: true,
                //    index: data.length - 1
                //});
                $("#InstrumentFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#InstrumentTo").data("kendoComboBox").value($("#InstrumentFrom").data("kendoComboBox").value());
        }

        function OnChangeInstrumentTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }




        //1.combo box ParamData Rpt FundAccounting//
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ParamDataForRptAccounting",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamData").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeParamData,
                    index: 0

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeParamData() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#InstrumentType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "ALL", value: 0 },
               { text: "EQUITY", value: 1 },
               { text: "BOND", value: 2 },
               { text: "DEPOSITO", value: 3 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeInstrumentType,
            index: 0
        });

        function OnChangeInstrumentType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        //1.combo box ParamData Rpt FundAccounting//
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                    index: 0

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }







        win = $("#WinCustodianRpt").kendoWindow({
            height: 500,
            title: "* Custodian Report",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

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

        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var ArrayAccountFrom = $("#AccountFrom").data("kendoMultiSelect").value();
        var stringAccountFrom = '';
        for (var i in ArrayAccountFrom) {
            stringAccountFrom = stringAccountFrom + ArrayAccountFrom[i] + ',';

        }
        stringAccountFrom = stringAccountFrom.substring(0, stringAccountFrom.length - 1)

        var ArrayCounterpart = $("#Counterpart").data("kendoMultiSelect").value();
        var stringCounterpart = '';
        for (var i in ArrayCounterpart) {
            stringCounterpart = stringCounterpart + ArrayCounterpart[i] + ',';

        }
        stringCounterpart = stringCounterpart.substring(0, stringCounterpart.length - 1)


        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        var ArrayInstrumentFrom = $("#InstrumentFrom").data("kendoMultiSelect").value();
        var stringInstrumentFrom = '';
        for (var i in ArrayInstrumentFrom) {
            stringInstrumentFrom = stringInstrumentFrom + ArrayInstrumentFrom[i] + ',';

        }
        stringInstrumentFrom = stringInstrumentFrom.substring(0, stringInstrumentFrom.length - 1)

        //var ArrayInstrumentType = $("#InstrumentType").data("kendoMultiSelect").value();
        //var stringInstrumentType = '';
        //for (var i in ArrayInstrumentType) {
        //    stringInstrumentType = stringInstrumentType + ArrayInstrumentType[i] + ',';

        //}
        //stringInstrumentType = stringInstrumentType.substring(0, stringInstrumentType.length - 1)



        if ($('#Name').val() == 'Aktivitas Bank Custodian Fund'
            || $('#Name').val() == 'Aktivitas Bank Custodian Konvensional'
            || $('#Name').val() == 'Aktivitas Bank Custodian Summary'
            || $('#Name').val() == 'Report NAV per Periode Rekapitulasi_NAB')
        {
            if (ArrayFundFrom.length > 1 || ArrayFundFrom[0] == "0") {
                alertify.alert("Report can only have one parameter Fund and cannot use ALL");
                return;
            }
        }


        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "CustodianReport_O";
                var CustodianRpt = {
                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    AccountFrom: stringAccountFrom,
                    //AccountTo: $("#AccountTo").data("kendoComboBox").text(),
                    FundClientFrom: stringFundClientFrom,
                    //FundClientTo: $("#FundClientTo").data("kendoComboBox").text(),
                    FundFrom: stringFundFrom,
                    CounterpartFrom: stringCounterpart,
                    InstrumentTypeFrom: $("#InstrumentType").data("kendoComboBox").value(),
                    //FundTo: $("#FundTo").data("kendoComboBox").text(),
                    InstrumentFrom: stringInstrumentFrom,
                    //InstrumentTo: $("#InstrumentTo").data("kendoComboBox").text(),
                    PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                    ShowNullData: $("#ShowNullData").data("kendoComboBox").value(),
                    Status: $("#Status").data("kendoComboBox").value(),
                    ParamData: $("#ParamData").data("kendoComboBox").value(),
                    //Profile: $("#Profile").data("kendoComboBox").text(),
                    //Groups: $("#Groups").data("kendoComboBox").text(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    FundPK: $('#FundPK').val(),
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                };
                var _url;
                if ($('#Name').val() == "Fund Fact Sheet") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateFFS/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")
                }
                else {
                    _url = window.location.origin + "/Radsoft/Reports/CustodianReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission
                }
                $.ajax({
                    url: _url,
                    type: 'POST',
                    data: JSON.stringify(CustodianRpt),
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

        checkedIds = [];
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
        $("#trValueDateFrom").hide();
        $("#trValueDateTo").hide();
        $("#trFundFrom").hide();
        $("#trFundTo").hide();
        $("#trInstrumentFrom").hide();
        $("#trInstrumentTo").hide();
        $("#trFundClientFrom").hide();
        $("#trFundClientTo").hide();
        $("#trAccountFrom").hide();
        $("#trAccountTo").hide();
        $("#trCounterpartFrom").hide()
        $("#tdProfile").hide();
        $("#tdGroups").hide();
        $("#trParamData").hide();
        $("#trInstrumentTypeFrom").hide()
        $("#trFund").hide();
        $("#lblSignature").hide();
        $("#lblPosition").hide();
    }

    function onWinFundClientClose() {
        WinFundClient.close();
    }
});