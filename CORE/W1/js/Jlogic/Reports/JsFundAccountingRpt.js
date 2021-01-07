$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinFundAccountingRpt").kendoValidator().data("kendoValidator");
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

        $("#ZeroBalance").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "NO", value: false },
                { text: "YES", value: true }

            ],
            filter: "contains",
            suggest: true,
            change: OnChangeZeroBalance,
            index: 0
        });
        function OnChangeZeroBalance() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

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

        //Signature 1
        $.ajax({
            url: window.location.origin + "/Radsoft/Signature/GetDefaultSignature1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Signature11").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature11,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature11() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#Signature11").val() == 0 || $("#Signature11").val() == "") {
                $("#Position11").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position11").val(data.Position);
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
                $("#Signature22").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature22,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature22() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if ($("#Signature22").val() == 0 || $("#Signature22").val() == "") {
                $("#Position22").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position22").val(data.Position);
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
                $("#Signature33").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature33,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature33() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#Signature33").val() == 0 || $("#Signature33").val() == "") {
                $("#Position33").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position33").val(data.Position);
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
                $("#Signature44").kendoComboBox({
                    dataValueField: "SignaturePK",
                    dataTextField: "Name",
                    dataSource: data,
                    change: OnChangeSignature44,
                    filter: "contains",
                    suggest: true
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeSignature44() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if ($("#Signature44").val() == 0 || $("#Signature44").val() == "") {
                $("#Position44").val("");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Signature/GetPosition4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + this.value(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Position44").val(data.Position);
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
            //Ascend
            if (_GlobClientCode == '01') {
                {
                    $("#Name").kendoComboBox({
                        dataValueField: "text",
                        dataTextField: "text",
                        dataSource: [
                            { text: "Nav Report" },
                            { text: "Fund Trial Balance Plain" },
                            { text: "Fund Journal Voucher" },
                            { text: "Fund Account Activity Plain" },
                            //{ text: "Fund Balance Sheet Plain" },
                            //{ text: "Fund Income Statement Plain" },
                            { text: "Fund Portfolio" },
                            { text: "Manajemen Fee Harian" },
                            { text: "Account Activity By Instrument" },
                            { text: "Account Activity By Fund" },
                            { text: "Account Activity By FundClient" },
                            //{ text: "NAV dan AUM Report" },


                            //custom
                            { text: "Trial Balance" },
                            { text: "Income Statement" },
                            { text: "Cash Movement" },
                            { text: "Counterpart Transaction" },
                            { text: "Perhitungan Fee MI dan Fee BK" },
                            { text: "Transaction Summary" },
                            { text: "Counterpart Transaction Grouping By Broker Code" },


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

                        if (this.text() == 'Nav Report') {
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();

                        }
                        else if (this.text() == 'Account Activity By Instrument') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trInstrumentFrom").show();
                        }

                        else if (this.text() == 'Account Activity By Fund') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                        }

                        else if (this.text() == 'Account Activity By FundClient') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundClientFrom").show();
                        }

                        else if (this.text() == 'Fund Trial Balance Plain') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                            $("#trParamData").show();

                        }

                        else if (this.text() == 'Fund Journal Voucher') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show()
                            $("#trFundFrom").show();
                            $("#trInstrumentFrom").show();
                            $("#trFundClientFrom").show();

                        }

                        else if (this.text() == 'Fund Account Activity Plain') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                            $("#trInstrumentFrom").show();
                            $("#trFundClientFrom").show();
                        }

                        else if (this.text() == 'Fund Balance Sheet Plain') {
                            $("#trValueDateFrom").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                            $("#trParamData").show();
                        }

                        else if (this.text() == 'Fund Income Statement Plain') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                            $("#trParamData").show();
                        }

                        else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                            $("#trValueDateFrom").show();
                            $("#trFundFrom").show();
                            $("#trParamData").show();
                            $("#trInstrumentTypeFrom").show();

                        }

                        else if (this.text() == 'Trial Balance') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();

                        }

                        else if (this.text() == 'Income Statement') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trAccountFrom").show();
                            $("#trFundFrom").show();
                        }

                        else if (this.text() == 'Cash Movement') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();
                        }

                        else if (this.text() == 'Counterpart Transaction') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();
                            $("#trCounterpartFrom").show()
                        }
                        else if (this.text() == 'Perhitungan Fee MI dan Fee BK') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();
                        }
                        else if (this.text() == 'Transaction Summary') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();
                            $("#trInstrumentTypeFrom").show();
                        }
                        else if (this.text() == 'Manajemen Fee Harian') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();

                        }

                        else if (this.text() == 'NAV dan AUM Report') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trFundFrom").show();
                        }

                        else if (this.text() == 'Counterpart Transaction Grouping By Broker Code') {
                            $("#trValueDateFrom").show();
                            $("#trValueDateTo").show();
                            $("#trInstrumentTypeFrom").show();
                            $("#trCounterpartFrom").show()
                        }
                    }
                }
            }

            else if (_GlobClientCode == '02') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Account Activity By Fund" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },

                        //customs

                        //custom
                        { text: "Pencairan Deposito" },
                        { text: "Placement Deposito" },
                        { text: "Perpanjangan Deposito" },
                        { text: "Laporan Laba Rugi Saham" },
                        { text: "Portfolio Valuation Report" },
                        { text: "Broker Stock" },
                        { text: "Fund Fact Sheet" },
                        { text: "Daily Compliance Report" },
                        { text: "History Settlement" },

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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'Pencairan Deposito') {
                        $("#trValueDateFrom").show();

                    }
                    else if (this.text() == 'Placement Deposito') {
                        $("#trValueDateFrom").show();
                    }
                    else if (this.text() == 'Perpanjangan Deposito') {
                        $("#trValueDateFrom").show();

                    }

                    else if (this.text() == 'Laporan Laba Rugi Saham') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Portfolio Valuation Report') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Broker Stock') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Fund Fact Sheet') {
                        $("#trValueDateFrom").show();
                        $("#trFund").show();
                    }

                    else if (this.text() == 'Daily Compliance Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'History Settlement') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                }
            }
            //Insight
            else if (_GlobClientCode == '03') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //{ text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Account Activity By Fund" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        //{ text: "NAV dan AUM Report" },

                        //customs
                        { text: "Report Daily NAV dan AUM" },
                        { text: "Bond Summary" },
                        { text: "Equity Summary" },
                        { text: "Historical Instrument Transaction" },
                        //{ text: "Cash Projection" },
                        //{ text: "Kebijakan Investasi" },
                        //{ text: "Bond Portofolio" },
                        //{ text: "List of Equity Fund" },
                        //{ text: "Fund Fact Sheet" },
                        { text: "Portfolio Valuation Report" },
                        { text: "Portfolio Summary Cash" },
                        //{ text: "NAV Projection Pricing" },
                        { text: "Fifo Bond Position" },
                        //{ text: "CSR Report" },
                        { text: "Daily Deal Board" },
                        { text: "New Bonds" },
                        { text: "SA Management Fee Report" },
                        { text: "Dividen And Coupon Fund" },
                        { text: "Total Bond And Equity Transaction" },
                        { text: "Master Deposito" },
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
                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Report Daily NAV dan AUM') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Bond Summary') {
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                    }

                    else if (this.text() == 'Equity Summary') {
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                    }

                    else if (this.text() == 'Historical Instrument Transaction') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentTypeFrom").show();
                        //$("#trInstrumentFrom").show();
                    }

                    else if (this.text() == 'Cash Projection') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Kebijakan Investasi') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Bond Portofolio') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                    }

                    else if (this.text() == 'List of Equity Fund') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    //else if (this.text() == 'Fund Fact Sheet') {
                    //    $("#trValueDateFrom").show();
                    //    $("#trFund").show();
                    //}

                    else if (this.text() == 'Portfolio Valuation Report') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Portfolio Summary Cash') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                    }

                    else if (this.text() == 'NAV Projection Pricing') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Fifo Bond Position') {
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'SA Management Fee Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAgentFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Daily Deal Board') {
                        $("#trValueDateFrom").show();
                        $("#trBankFrom").show();

                    }

                    else if (this.text() == 'New Bonds') {
                        $("#trValueDateFrom").show();


                    }
                    else if (this.text() == 'Dividen And Coupon Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Total Bond And Equity Transaction') {
                        $("#paramPeriod").show();
                    }

                    else if (this.text() == 'Master Deposito') {
                        $("#trValueDateFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '04') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Account Activity By Fund" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },


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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '05') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Trial Balance Plain" },
                        //{ text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fee MI_Bank Custody" },
                        { text: "Historical Bond" },
                        { text: "Historical Stocks" },
                        { text: "Nav Report" },


                        // New Add(Check Parameter)
                        //{ text: "Cash Flow - Historical Tracker - Bond" },
                        //{ text: "Cash Flow - Historical Tracker Bond - Composite" },
                        //{ text: "Cash Flow - Historical Tracker Bond - Summary" },
                        { text: "Summary Deposito" },
                        { text: "Accrued Interest Report" },
                        { text: "Cash Flow Summary" },
                        { text: "Portfolio Report" },
                        { text: "Daily Compliance Report" },
                        { text: "WIN LOSS Report" },
                        { text: "Report Dealing Rekap Transaksi Per Broker" },

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

                    if (this.text() == 'Fee MI_Bank Custody') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }
                    else if (this.text() == 'Historical Bond') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Historical Stocks') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    // New Add(Check Parameter)
                    if (this.text() == 'Accrued Interest Report') {

                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    if (this.text() == 'Cash Flow - Historical Tracker - Bond') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    if (this.text() == 'Cash Flow - Historical Tracker Bond - Composite') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    if (this.text() == 'Cash Flow - Historical Tracker Bond - Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    if (this.text() == 'Summary Deposito') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }


                    if (this.text() == 'Cash Flow Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#trBankFrom").show()

                    }

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }
                    if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    if (this.text() == 'Portfolio Report') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                    }


                    if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    if (this.text() == 'Daily Compliance Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    if (this.text() == 'WIN LOSS Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    if (this.text() == 'Report Dealing Rekap Transaksi Per Broker') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trCounterpartFrom").show();

                    }

                }
            }

            else if (_GlobClientCode == '06') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Account Activity By Fund" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Fund Exposure" },

                        { text: "MI Fee Detail Monthly" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'MI Fee Detail Monthly') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '09') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Fund Exposure" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
                        //Custom
                        { text: "Transactions By Counterpart" },
                        { text: "Counterpart Transactions By Fund" },
                        { text: "Transaction Listing Profit" },
                        { text: "Counterpart Percentage" },
                        //{ text: "Turnover By Fund" },
                        { text: "Report Daily NAV dan AUM" },
                        { text: "Laporan DtD MtD Ytd YoY" },
                        { text: "Laporan NAV Projection" },
                        { text: "Counterpart Percentage Monthly" },
                        { text: "Perhitungan MI Fee" },
                        { text: "Report NAV AUM" },
                        { text: "PVR Sector" }
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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transactions By Counterpart') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }



                    else if (this.text() == 'Transaction Listing Profit') {
                        ClearAttribute();
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trInstrumentFrom").show();
                        $("#trParamData").show();
                        $("#trFundFrom").show();
                        $("#trCounterpartFrom").show();

                    }

                    else if (this.text() == 'Counterpart Transactions By Fund') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Counterpart Percentage') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }

                    //else if (this.text() == 'Turnover By Fund') {
                    //    ClearAttribute();
                    //    $("#trPeriod").show();
                    //    $("#trFundByAll").show();

                    //}

                    else if (this.text() == 'Report Daily NAV dan AUM') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();


                    }

                    else if (this.text() == 'Laporan DtD MtD Ytd YoY') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();


                    }

                    else if (this.text() == 'Laporan NAV Projection') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                    }

                    else if (this.text() == 'Counterpart Percentage Monthly') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Perhitungan MI Fee') {
                        ClearAttribute();
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Report NAV AUM') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'PVR Sector') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }


                }
            }

            else if (_GlobClientCode == '11') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Fund Exposure" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
                        //Custom
                        { text: "Accrued Interest Report" },
                        { text: "Accrued Interest Deposito" },
                        { text: "Accrued Interest Bond" },
                        { text: "Posisi Reksadana" },
                        { text: "Transaksi Pencairan Deposito" },
                        { text: "Report Rekap Investasi" },
                        { text: "Laporan OJK" }

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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }


                    else if (this.text() == 'Accrued Interest Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Accrued Interest Deposito') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Accrued Interest Bond') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Posisi Reksadana') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Transaksi Pencairan Deposito') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Report Rekap Investasi') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Laporan OJK') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '13') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "Account Activity By Fund" },
                        { text: "NAV dan AUM Report" },

                        //Custom
                        { text: "Coupon Bonds" },
                        { text: "Daily Transaction Report" },
                        { text: "Daily Transaction Form Equity" },
                        { text: "Daily Transaction Form Bonds" },
                        { text: "Daily Transaction Form Deposito" },

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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                        $("#LblShownZeroBalance").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Coupon Bonds') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Daily Transaction Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }

                    else if (this.text() == 'Daily Transaction Form Equity') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trCounterpartFrom").show();
                        $("#lblParamTrxType").show();
                    }

                    else if (this.text() == 'Daily Transaction Form Bonds') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Daily Transaction Form Deposito') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#lblParamTrxTypeDeposito").show();
                    }

                }
            }

            else if (_GlobClientCode == '12') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Fund Exposure" },
                        { text: "Accrued Interest Report" },
                        { text: "Accrued Interest Deposito" },
                        { text: "Fund Fact Sheet" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
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

                    if (this.text() == 'Fund Fact Sheet') {
                        $("#trValueDateFrom").show();
                        $("#trFund").show();

                    }
                    else if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }


                    else if (this.text() == 'Accrued Interest Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Accrued Interest Deposito') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Accrued Interest Bond') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '14') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Compliance Report" },
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
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


                    if (this.text() == 'Daily Compliance Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '17') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //{ text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV Harian" },
                        { text: "Report Portfolio Summary" },
                        { text: "Broker Commission Summary" },
                        { text: "Report Cash Forecast" },
                        { text: "AUM Harian" },
                        { text: "Report Accued Interest Deposito" },
                        { text: "Account Activity By Fund" },
                        { text: "Report Settlement Listing Summary" },
                        { text: "Portfolio Balance Report" },
                        { text: "NAV AUM Unit Report" },
                        { text: "NAV AUM Unit Report Per Fund" },
                        { text: "Daily Deal Board" },
                        //{ text: "NAV dan AUM Report" },

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
                    getDateFrom(this.text());
                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'NAV Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Report Portfolio Summary') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Broker Commission Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trCounterpartFrom").show();

                    }

                    else if (this.text() == 'Report Cash Forecast') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'AUM Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Report Accued Interest Deposito') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Report Settlement Listing Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Portfolio Balance Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trCounterpartFrom").show();

                    }

                    else if (this.text() == 'NAV AUM Unit Report Per Fund') {
                        $("#trFundFrom").show();
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }

                    else if (this.text() == 'NAV AUM Unit Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }

                    else if (this.text() == 'Daily Deal Board') {
                        $("#trValueDateTo").show();
                        $("#trBankFrom").show();
                    }


                }

            }

            else if (_GlobClientCode == '19') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },


                        //customs

                        //custom
                        { text: "Pencairan Deposito" },
                        { text: "Placement Deposito" },
                        { text: "Perpanjangan Deposito" },
                        //{ text: "Laporan Laba Rugi Saham" },
                        { text: "Portfolio Valuation Report" },
                        { text: "Broker Commission Summary" },
                        { text: "Counterpart Transaction" },
                        //{ text: "Broker Stock" },
                        //{ text: "Fund Fact Sheet" },
                        //{ text: "Daily Compliance Report" },

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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'Pencairan Deposito') {
                        $("#trValueDateFrom").show();
                        $("#lblSignature").show();
                        $("#lblPosition").show();

                    }
                    else if (this.text() == 'Placement Deposito') {
                        $("#trValueDateFrom").show();
                        $("#lblSignature").show();
                        $("#lblPosition").show();
                    }
                    else if (this.text() == 'Perpanjangan Deposito') {
                        $("#trValueDateFrom").show();
                        $("#lblSignature").show();
                        $("#lblPosition").show();

                    }
                    else if (this.text() == 'Broker Commission Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trCounterpartFrom").show();

                    }

                    else if (this.text() == 'Counterpart Transaction') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                        $("#trCounterpartFrom").show()
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    //else if (this.text() == 'Laporan Laba Rugi Saham') {
                    //    $("#trValueDateTo").show();
                    //    $("#trValueDateFrom").show();
                    //    $("#trInstrumentFrom").show();
                    //    $("#trFundFrom").show();
                    //}

                    //else if (this.text() == 'Portfolio Valuation Report') {
                    //    $("#trValueDateTo").show();
                    //    $("#trValueDateFrom").show();
                    //    $("#trInstrumentFrom").show();
                    //    $("#trFundFrom").show();
                    //}

                    //else if (this.text() == 'Broker Stock') {
                    //    $("#trValueDateTo").show();
                    //    $("#trValueDateFrom").show();
                    //    $("#trFundFrom").show();
                    //}

                    //else if (this.text() == 'Fund Fact Sheet') {
                    //    $("#trValueDateFrom").show();
                    //    $("#trFund").show();
                    //}

                    //else if (this.text() == 'Daily Compliance Report') {
                    //    $("#trValueDateTo").show();
                    //    $("#trFundFrom").show();

                    //}
                }
            }

            else if (_GlobClientCode == '20') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
                        { text: "Report Bloomberg" },
                        //{ text: "Portfolio Valuation Report" },
                        //{ text: "Portfolio Summary Cash" },
                        { text: "SA Management Fee Report" },
                        { text: "Trading Portfolio Bloomberg" },
                        { text: "Daily Deal Board" },

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
                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }
                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }
                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }
                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }
                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }
                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Report Bloomberg') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Portfolio Valuation Report') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Portfolio Summary Cash') {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                    }
                    else if (this.text() == 'SA Management Fee Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAgentFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Trading Portfolio Bloomberg') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }
                    else if (this.text() == 'Daily Deal Board') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#lblSignature1").show();
                        //$("#lblPosition").show();
                    }

                }
            }

            else if (_GlobClientCode == '21') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Fund Portfolio Detail Acq" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Transactions By Counterpart" },
                        { text: "Account Activity By Fund" },

                        //customs

                        //custom
                        { text: "DAILY DEAL BOARD" },
                        { text: "Fund Fact Sheet" },
                        { text: "Broker Commission Report" },

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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Fund Portfolio Detail Acq') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'DAILY DEAL BOARD') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Fund Fact Sheet') {
                        $("#trValueDateFrom").show();
                        $("#trFund").show();
                    }

                    else if (this.text() == 'Transactions By Counterpart') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Broker Commission Report') {
                        ClearAttribute();
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#trFundFrom").show();

                    }

                }
            }

            else if (_GlobClientCode == '22') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //{ text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        //{ text: "Fund Balance Sheet Plain" },
                        //{ text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        //{ text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
                        //customs

                        //custom
                        //{ text: "TOP 10" },
                        //{ text: "Pie Chart" },
                        { text: "Report Yield Portfolio" },
                        { text: "Cash Projection" },
                        //{ text: "Subs VS Redempt" }, //tahan dlu, query belom


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

                    //if (this.text() == 'Nav Report') {
                    //    $("#trValueDateTo").show();
                    //    $("#trFundFrom").show();

                    //}
                    if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'TOP 10') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Pie Chart') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if (this.text() == 'Report Yield Portfolio') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Cash Projection') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //else if (this.text() == 'Subs VS Redempt') {
                    //    $("#trValueDateFrom").show();
                    //    //$("#trFundFrom").show();
                    //}

                }
            }

            else if (_GlobClientCode == '23') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },


                        //customs

                        //custom
                        { text: "Fund Portfolio BPKH" },


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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'Fund Portfolio BPKH') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '25') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },



                        //custom
                        { text: "History Transaction Equity" },
                        { text: "History Transaction Bond" },
                        { text: "History Transaction Deposito" },


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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom
                    else if (this.text() == 'History Transaction Equity') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'History Transaction Bond') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'History Transaction Deposito') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                }
            }

            else if (_GlobClientCode == '27') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Transaction Summary" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },


                        //custom
                        { text: "Fund Portfolio Counterpart" },
                        { text: "Fund Portfolio By CurrencyPK" },
                        { text: "PVR Bond Rating" },
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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Transaction Summary') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    //custom

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Fund Portfolio Counterpart') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trCounterpartFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio By CurrencyPK') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                        $("#trCurrencyFrom").show();


                    }

                    else if (this.text() == 'PVR Bond Rating') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                        $("#trBondRatingFrom").show();

                    }

                }
            }

            else {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Nav Report" },
                        { text: "Fund Trial Balance Plain" },
                        { text: "Fund Journal Voucher" },
                        { text: "Fund Account Activity Plain" },
                        { text: "Fund Balance Sheet Plain" },
                        { text: "Fund Income Statement Plain" },
                        { text: "Fund Portfolio" },
                        { text: "Manajemen Fee Harian" },
                        { text: "NAV dan AUM Report" },
                        { text: "Account Activity By Fund" },
                        { text: "SA Management Fee Report" },


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

                    if (this.text() == 'Nav Report') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }
                    else if (this.text() == 'Fund Trial Balance Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }

                    else if (this.text() == 'Fund Journal Voucher') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show()
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();

                    }

                    else if (this.text() == 'Fund Account Activity Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trInstrumentFrom").show();
                        $("#trFundClientFrom").show();
                    }

                    else if (this.text() == 'Fund Balance Sheet Plain') {
                        $("#trValueDateFrom").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Income Statement Plain') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();
                    }

                    else if (this.text() == 'Fund Portfolio' || this.text() == 'Fund Exposure') {
                        $("#trValueDateFrom").show();
                        $("#trFundFrom").show();
                        $("#trParamData").show();

                    }
                    else if (this.text() == 'Manajemen Fee Harian') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();

                    }

                    else if (this.text() == 'NAV dan AUM Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Account Activity By Fund') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAccountFrom").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'SA Management Fee Report') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trAgentFrom").show();
                        $("#trFundFrom").show();
                    }

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
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/FundClient/GetFundClientDetailComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#FundClientFrom").kendoMultiSelect({
        //            dataValueField: "FundClientPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data
        //        });
        //        $("#FundClientFrom").data("kendoMultiSelect").value("0");
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeFundClientFrom() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //    $("#FundClientTo").data("kendoComboBox").value($("#FundClientFrom").data("kendoComboBox").value());
        //}

        //function OnChangeFundClientTo() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}



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

        // ComboBox Period
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: true,
                    change: OnChangePeriodPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePeriodPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //Currency
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyFrom").kendoMultiSelect({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                    //change: OnChangeCurrencyFrom,

                    //suggest: true,
                    //index: 0
                });


                $("#CurrencyFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        ////BondRating
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentBondRatingComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BondRatingFrom").kendoMultiSelect({
                    dataValueField: "ID",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,

                    //suggest: true,
                    //index: 0
                });


                $("#BondRatingFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


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


        $("#ParamTrxType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamTrxType,
            dataSource: [
                { text: "ALL", value: "0" },
                { text: "BUY", value: "1" },
                { text: "SELL", value: "2" },
            ],

        });
        function OnChangeParamTrxType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        $("#ParamTrxTypeDeposito").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamTrxTypeDeposito,
            dataSource: [
                { text: "All", value: "0" },
                { text: "Deposito", value: "1" },
                { text: "Withdrawal", value: "2" },
                { text: "Rollover", value: "3" },


            ],

        });
        function OnChangeParamTrxTypeDeposito() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }


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

        //Bank
        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankFrom").kendoMultiSelect({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                });


                $("#BankFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        win = $("#WinFundAccountingRpt").kendoWindow({
            height: 500,
            title: "* Fund Admin Report",
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
        var BegDate;
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
        if ($('#BegDate').is(":checked") == true)
            BegDate = 1;
        else
            BegDate = 0;

        var ArrayBankFrom = $("#BankFrom").data("kendoMultiSelect").value();
        var stringBankFrom = '';
        for (var i in ArrayBankFrom) {
            stringBankFrom = stringBankFrom + ArrayBankFrom[i] + ',';

        }
        stringBankFrom = stringBankFrom.substring(0, stringBankFrom.length - 1)

        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var ArrayCurrencyFrom = $("#CurrencyFrom").data("kendoMultiSelect").value();
        var stringCurrencyFrom = '';
        for (var i in ArrayCurrencyFrom) {
            stringCurrencyFrom = stringCurrencyFrom + ArrayCurrencyFrom[i] + ',';

        }
        stringCurrencyFrom = stringCurrencyFrom.substring(0, stringCurrencyFrom.length - 1)

        var ArrayBondRatingFrom = $("#BondRatingFrom").data("kendoMultiSelect").value();
        var stringBondRatingFrom = '\'';
        for (var i in ArrayBondRatingFrom) {
            stringBondRatingFrom = stringBondRatingFrom + ArrayBondRatingFrom[i] + '\',\'';

        }
        stringBondRatingFrom = stringBondRatingFrom.substring(0, stringBondRatingFrom.length - 2)

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

        var ArrayAgentFrom = $("#AgentFrom").data("kendoMultiSelect").value();
        var stringAgentFrom = '';
        for (var i in ArrayAgentFrom) {
            stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

        }
        stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

        //var ArrayInstrumentType = $("#InstrumentType").data("kendoMultiSelect").value();
        //var stringInstrumentType = '';
        //for (var i in ArrayInstrumentType) {
        //    stringInstrumentType = stringInstrumentType + ArrayInstrumentType[i] + ',';

        //}
        //stringInstrumentType = stringInstrumentType.substring(0, stringInstrumentType.length - 1)


        if ($('#Name').val() == 'Nav Report' || $('#Name').val() == 'Fund Trial Balance Plain' || $('#Name').val() == 'Fund Balance Sheet Plain' ||
            $('#Name').val() == 'Fund Income Statement Plain' || $('#Name').val() == 'Trial Balance' || $('#Name').val() == 'Income Statement'
            || $('#Name').val() == 'Cash Movement' || $('#Name').val() == 'Portfolio Valuation Report' || $('#Name').val() == 'Report Rekap Investasi') {
            if (ArrayFundFrom.length > 1 || ArrayFundFrom[0] == "0") {
                alertify.alert("Report can only have one parameter Fund and cannot use ALL");
                return;
            }
        }


        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "FundAccountingReport_O";
                var FundAccountingRpt = {
                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    AccountFrom: stringAccountFrom,
                    //AccountTo: $("#AccountTo").data("kendoComboBox").text(),
                    FundClientFrom: stringFundClientFrom,
                    //FundClientTo: $("#FundClientTo").data("kendoComboBox").text(),
                    FundFrom: stringFundFrom,
                    CurrencyFrom: stringCurrencyFrom,
                    CounterpartFrom: stringCounterpart,
                    BondRatingFrom: stringBondRatingFrom,
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
                    Signature11: $("#Signature11").data("kendoComboBox").value(),
                    Signature22: $("#Signature22").data("kendoComboBox").value(),
                    Signature33: $("#Signature33").data("kendoComboBox").value(),
                    Signature44: $("#Signature44").data("kendoComboBox").value(),
                    ZeroBalance: $("#ZeroBalance").data("kendoComboBox").value(),
                    TrxType: $('#ParamTrxType').val(),
                    TrxTypeDeposito: $('#ParamTrxTypeDeposito').val(),
                    AgentFrom: stringAgentFrom,
                    BankFrom: stringBankFrom,
                    PeriodPK: $('#PeriodPK').val(),
                };
                var _url;
                if ($('#Name').val() == "Fund Fact Sheet") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateFFS/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")
                }
                else {
                    _url = window.location.origin + "/Radsoft/Reports/FundAccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission
                }
                $.ajax({
                    url: _url,
                    type: 'POST',
                    data: JSON.stringify(FundAccountingRpt),
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
        $("#trAgentFrom").hide();
        $("#trValueDateFrom").hide();
        $("#trValueDateTo").hide();
        $("#trFundFrom").hide();
        $("#trCurrencyFrom").hide();
        $("#trBondRatingFrom").hide();
        $("#trFundTo").hide();
        $("#trInstrumentFrom").hide();
        $("#trInstrumentTo").hide();
        $("#trFundClientFrom").hide();
        $("#trFundClientTo").hide();
        $("#trAccountFrom").hide();
        $("#trAccountTo").hide();
        $("#trCounterpartFrom").hide();
        $("#tdProfile").hide();
        $("#tdGroups").hide();
        $("#trParamData").hide();
        $("#trInstrumentTypeFrom").hide()
        $("#trFund").hide();
        $("#lblSignature").hide();
        $("#lblPosition").hide();
        $("#lblSignature1").hide();
        $("#lblPosition1").hide();
        $("#lblSignature2").hide();
        $("#lblPosition2").hide();
        $("#lblSignature3").hide();
        $("#lblPosition3").hide();
        $("#lblSignature4").hide();
        $("#lblPosition4").hide();
        $("#LblShownZeroBalance").hide();
        $("#lblParamTrxType").hide();
        $("#lblParamTrxTypeDeposito").hide();
        $("#trBankFrom").hide();
        $("#paramPeriod").hide();
        $("#LblBegDate").hide();
        $("#BegDate").prop('checked', false);
    }

    function getDateFrom(_reportName) {
        if (_reportName == 'Portfolio Balance Report') {
            $("#LblBegDate").show();
        }
        else {
            $("#LblBegDate").hide();
            $("#ValueDateFrom").data("kendoDatePicker").value(new Date())
            $("#ValueDateFrom").attr('readonly', false);
            $("#ValueDateFrom").data("kendoDatePicker").enable(true);
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