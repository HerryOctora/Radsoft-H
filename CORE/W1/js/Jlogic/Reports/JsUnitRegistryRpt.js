$(document).ready(function () {
    var win;
    var button = $("#ShowGrid").data("kendoButton");
    var button = $("#ShowGridSID").data("kendoButton");
    var button = $("#ShowGridInternalCategory").data("kendoButton");
    var checkedIds = {};
    var checkedInternalCategory = {};

    var GlobValidator = $("#WinUnitRegistryRpt").kendoValidator().data("kendoValidator");
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
            selectable: "multiple",
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
            //define template column with checkbox and attach click event handler
                {
                    //select all on grid
                    headerTemplate: "<input type='checkbox' id='header-chb' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='header-chb'></label>",
                    //end select all on grid
                    template: "<input type='checkbox' class='checkbox'/>"
                }
                
            , {
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

        //select all on grid
        var oldPageSize = 0;

        $('#header-chb').change(function (ev) {

            

            //var checked = ev.target.checked;

            oldPageSize = gridFundClient.dataSource.pageSize();
            gridFundClient.dataSource.pageSize(gridFundClient.dataSource.data().length);

            $('.checkbox').each(function (idx, item) {
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

            //$('.checkbox').click();

            gridFundClient.dataSource.pageSize(oldPageSize);

        });



        //end select all on grid


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

    function LoadDataSID() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/FundClient/GetFundClientSIDComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

                        SID: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridFundClientSID = $("#gridFundClientSID").kendoGrid({
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
                field: "SID",
                title: "SID",
                width: "200px"
            }
            ],
            editable: "inline"
        }).data("kendoGrid");

        //bind click event to the checkbox
        gridFundClientSID.table.on("click", ".checkbox", selectRow);


        //on click of the checkbox:
        function selectRow() {
            var checked = this.checked,
            rowA = $(this).closest("tr"),
            gridFundClientSID = $("#gridFundClientSID").data("kendoGrid"),
            dataItemZ = gridFundClientSID.dataItem(rowA);

            checkedIds[dataItemZ.SID] = checked;
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
                if (checkedIds[view[i].SID]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                    .addClass("k-state-selected")
                    .find(".checkbox")
                    .attr("checked", "checked");
                }
            }
        }
    }

    function LoadDataInternalCategory() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/FundClient/GetFundClientInternalCategoryComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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

                        InternalCategory: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridFundClientInternalCategory = $("#gridFundClientInternalCategory").kendoGrid({
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
                    field: "InternalCategory",
                    title: "InternalCategory",
                    width: "200px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");

        //bind click event to the checkbox
        gridFundClientInternalCategory.table.on("click", ".checkbox", selectRow);


        //on click of the checkbox:
        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridFundClientInternalCategory = $("#gridFundClientInternalCategory").data("kendoGrid"),
                dataItemZ = gridFundClientInternalCategory.dataItem(rowA);

            checkedInternalCategory[dataItemZ.InternalCategoryPK] = checked;
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
                if (checkedInternalCategory[view[i].InternalCategoryPK]) {
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


        WinFundClientSID = $("#WinFundClientSID").kendoWindow({
            height: 500,
            title: "Fund Client SID Detail",
            visible: false,
            width: 400,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

        WinFundClientInternalCategory = $("#WinFundClientInternalCategory").kendoWindow({
            height: 500,
            title: "Fund Client InternalCategory Detail",
            visible: false,
            width: 400,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");


        $("#ShowGridSID").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGridSID").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#ShowGridInternalCategory").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGridInternalCategory").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

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
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "MM/dd/yyyy", "dd/MMM/yyyy"],
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy", "MM/dd/yyyy", "dd/MMM/yyyy"]
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

        $("#BitAllSID").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGridSID").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGridSID").data("kendoButton").enable(true);
            }
        });

        $("#BitAllInternalCategory").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGridInternalCategory").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGridInternalCategory").data("kendoButton").enable(true);
            }
        });


        $("#ZeroBalance").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "YES", value: true },
                { text: "NO", value: false }
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

        $("#RoundingNav").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "YES", value: 1 },
                { text: "NO", value: 0 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeRoundingNav,
            index: 0
        });
        function OnChangeRoundingNav() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $("#WithAdjustment").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "YES", value: 1 },
                { text: "NO", value: 0 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeAdjustment,
            index: 0
        });
        function OnChangeAdjustment() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodFrom").kendoComboBox({
                    dataValueField: "ID",
                    dataTextField: "ID",
                    filter: "contains",
                    enabled: true,
                    dataSource: data,
                    change: OnChangePeriodFrom
                });

                $("#PeriodTo").kendoComboBox({
                    dataValueField: "ID",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePeriodTo
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }

        });
        function OnChangePeriodFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }
        function OnChangePeriodTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    enabled: true,
                    dataSource: data,
                    change: OnChangeBank
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }

        });
        function OnChangeBank() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {

                $('#FundFrom').data('kendoMultiSelect').destroy();
                $('#divFundFrom').empty();
                $('#divFundFrom').append('<select id="FundFrom" />')
                FundCombo($("#BankPK").val());

            }


        }

        $("#ShowGrid").click(function () {
            WinFundClient.center();
            WinFundClient.open();
            LoadData();
        });

        $("#ShowGridSID").click(function () {
            WinFundClientSID.center();
            WinFundClientSID.open();
            LoadDataSID();
        });

        $("#ShowGridInternalCategory").click(function () {
            WinFundClientInternalCategory.center();
            WinFundClientInternalCategory.open();
            LoadDataInternalCategory();
        });


        
        InitName();

        function FundCombo(_BankPK) {

            var _url = "";
            if (_BankPK == 0)
                _url = window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id")
            else
                _url = window.location.origin + "/Radsoft/Fund/GetFundComboByBankPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _BankPK

            //Fund
            $.ajax({
                url: _url,
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
        }

        function InitName() {
            //Ascend
            if (_GlobClientCode == '01') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //{ text: "Batch Form SUB Instruction" },
                        //{ text: "Batch Form SUB Regular Instruction" },
                        //{ text: "Batch Form RED Amount Instruction" },
                        //{ text: "Batch Form RED Unit Instruction" },
                        //{ text: "Daily Transaction Report For All" },
                        //{ text: "Historical Transaction" },
                        //{ text: "Client Unit Position" },
                        //{ text: "Fund Unit Position" },
                        //{ text: "Unit Activity For Accounting" },
                        //{ text: "Client Unit Position Customized" },
                        //{ text: "Newton Report" },
                        //{ text: "Agent Commission Report" },
                        //{ text: "Monthly Transaction by Fund and InvestorType" },
                        //{ text: "Daily Transaction Report For Management" },

                        //custom
                        { text: "Historical Transaction Per Client" },
                        { text: "Client Report" },
                        { text: "Summary Client" },
                        { text: "Management Fee Per Client" },
                        { text: "Daily Transaction Report" }


                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });


                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }


                    else if (this.text() == 'Historical Transaction Per Client') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Report') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Summary Client') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Management Fee Per Client') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentFrom").show();
                    }
                    else if (this.text() == 'Daily Transaction Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }


                    //else if (this.text() == 'Batch Form SUB Instruction') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Batch Form SUB Regular Instruction') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Batch Form RED Amount Instruction') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblStatus").show();
                    //    $("#LblDateFrom").show();
                    //}
                    //else if (this.text() == 'Batch Form RED Unit Instruction') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblStatus").show();
                    //    $("#LblDateFrom").show();
                    //}
                    //else if (this.text() == 'Management Fee Per Client') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblAgentFrom").show();
                    //}
                    //else if (this.text() == 'Daily Transaction Report For All') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblDepartmentFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Daily Transaction Report For Management') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDepartmentFrom").show();
                    //    $("#LblStatus").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //else if (this.text() == 'Historical Transaction') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblShownZeroBalance").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Client Unit Position') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Fund Unit Position') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblStatus").show();
                    //    $("#LblDateFrom").show();
                    //}
                    //else if (this.text() == 'Unit Activity For Accounting') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Client Unit Position Customized') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Newton Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblStatus").show();
                    //}


                }
            }
            else if (_GlobClientCode == '02') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }
            //Insight
            else if (_GlobClientCode == '03') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        //{ text: "Historical NAV" },

                        //custom
                        { text: "Customer Portfolio All Fund Client" },
                        { text: "Fund Balance Detail" },
                        { text: "Fund Balance Detail All" },
                        { text: "Fund Balance By Categories" },
                        { text: "Daily Subscription Instruction" },
                        { text: "Daily Redemption Instruction" },
                        { text: "Daily Switching Instruction" },
                        //{ text: "Client Income Report" },
                        { text: "Report Daily Transaction" },
                        //{ text: "Report SID Client" },
                        { text: "Laporan Akun Bulanan Client" },
                        { text: "Laporan AUM per Tipe Fund" },
                        { text: "Laporan Pengumuman Pembagian Dividen" },
                        { text: "Weekly Transaction" },
                        { text: "Report SID Client Summary" },
                        { text: "Report Subscription Redemption APERD" },
                        { text: "Report Revenue Grouping by Fund Client" },
                        { text: "Report Fund Unit Position Investment" },
                        { text: "Open Fund Distribution By Client Category" },
                        { text: "AUM Detail Customer" },
                        { text: "NOA Existing And Non Existing" },


                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });


                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                    //custom
                    else if (this.text() == 'Customer Portfolio All Fund Client') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblRoundingNav").show();
                        $("#LblWithAdjustment").show();

                    }
                    else if (this.text() == 'Fund Balance Detail') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Fund Balance Detail All') {
                        $("#LblAgentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Fund Balance By Categories') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Laporan Akun Bulanan Client') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Daily Subscription Instruction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Daily Redemption Instruction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Daily Switching Instruction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Client Income Report') {

                        $("#LblDateFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Report Daily Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Report SID Client') {

                        $("#LblDateFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Laporan AUM per Tipe Fund') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblClientType").show();
                    }
                    else if (this.text() == 'Laporan AUM per Tipe Perusahaan') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                        $("#LblCompanyType").show();
                    }
                    else if (this.text() == 'Laporan Pengumuman Pembagian Dividen') {
                        $("#LblDateFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Weekly Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }

                    else if (this.text() == 'Report SID Client Summary') {

                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblInternalCategoryFrom").show();
                    }
                    else if (this.text() == 'Report Subscription Redemption APERD') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Report Revenue Grouping by Fund Client') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Report Fund Unit Position Investment') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Open Fund Distribution By Client Category') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'AUM Detail Customer') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'NOA Existing And Non Existing') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientType").show();
                    }

                }
            }

            else if (_GlobClientCode == '04') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }
            else if (_GlobClientCode == '05') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }
            else if (_GlobClientCode == '06') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '07') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '08') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //{ text: "Batch Form SUB Instruction" },
                        //{ text: "Batch Form SUB Regular Instruction" },
                        //{ text: "Batch Form RED Amount Transaction" },
                        //{ text: "Bacth Form RED Unit Instruction" },
                        //{ text: "Monthly Transaction by Fund and InvestorType" },
                        //{ text: "Daily Transaction Report For All" },
                        //{ text: "Daily Transaction Report For Management" },
                        //{ text: "Historical Transaction" },
                        //{ text: "Client Unit Position" },
                        //{ text: "Fund Unit Position" },
                        //{ text: "Client Unit Position Customized" },
                        //{ text: "Historical Transaction Agent" },
                        //{ text: "Newton Report" },
                        //{ text: "Agent Commision Report" },
                        //{ text: "Monthly Transaction by Fund and InvestorType" },

                        //custom
                        { text: "Monthly Transaction By Fund And InvestorType And FundClient" },
                        { text: "Historical Transaction Agent All" },
                        { text: "Unit Activity For Accounting" },
                        { text: "Net Sales Report" },
                        { text: "Cash Projection" },
                        { text: "Summary Transaction" },
                        { text: "Unit Allocation" },
                        { text: "Daily Sales Report" },
                        //{ text: "RHB Dashboard" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Summary Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblClientTypeRHB").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                    //custom
                    else if (this.text() == 'Monthly Transaction By Fund And InvestorType And FundClient') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction Agent All') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    else if (this.text() == 'Unit Activity For Accounting') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();

                    }

                    else if (this.text() == 'Net Sales Report') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Cash Projection') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();

                    }
                    else if (this.text() == 'Unit Allocation') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();

                    }

                    else if (this.text() == 'Daily Sales Report') {
                        $("#LblDateFrom").show();

                    }
                }
            }


            else if (_GlobClientCode == '09') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //{ text: "Monthly Transaction by Fund and InvestorType" },
                        //{ text: "Daily Transaction Report For Management" },
                        //{ text: "Historical Transaction Agent All" },

                        //custom
                        { text: "Monthly Transaction By Fund And InvestorType And FundClient" },
                        { text: "Total Transaction Report Client" },
                        { text: "Total Transaction Report Fund" },
                        { text: "Laporan Rekap Unit Penyertaan" },
                        { text: "Laporan Risk Profile Tahunan" },
                        { text: "Monthly Report 100Mil" },
                        { text: "Report Laporan Subs dan Redem Total 100 Juta" },
                        { text: "Rebate Generali Fee Report" },
                        { text: "Perhitungan Agent Fee Report" },
                        { text: "Subs With Interest" },

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }


                    //----------------------------------
                    else if (this.text() == 'Monthly Transaction By Fund And InvestorType And FundClient') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Total Transaction Report Client') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Total Transaction Report Fund') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Laporan Rekap Unit Penyertaan') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Laporan Risk Profile Tahunan') {

                    }

                    else if (this.text() == 'Monthly Report 100Mil') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                    }

                    else if (this.text() == 'Report Laporan Subs dan Redem Total 100 Juta') {
                        $("#LblFundFrom").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Rebate Generali Fee Report') {
                        $("#LblClientFrom").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Perhitungan Agent Fee Report') {
                        $("#LblClientFrom").show();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Subs With Interest') {
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }


                }
            }
            else if (_GlobClientCode == '10') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //{ text: "Batch Form SUB Instruction" },
                        //{ text: "Batch Form SUB Regular Instruction" },
                        //{ text: "Batch Form RED Amount Instruction" },
                        //{ text: "Batch Form RED Unit Instruction" },
                        { text: "Daily Transaction Report For All" },
                        { text: "Historical Transaction" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical NAV" },
                        //{ text: "Unit Activity For Accounting" },
                        //{ text: "Client Unit Position Customized" },
                        //{ text: "Newton Report" },
                        //{ text: "Agent Commission Report" },
                        //{ text: "Monthly Transaction By Fund And InvestorType And FundClient" },
                        //{ text: "Monthly Transaction by Fund and InvestorType" },
                        //{ text: "Daily Transaction Report For Management" },

                        //custom
                        { text: "Historical Transaction Agent All" },
                        { text: "Indication By Agent" },
                        { text: "Indication By Product" },
                        { text: "Indication Summary" },
                        { text: "Detail Transaction" },
                        { text: "AUM Comparison" },
                        { text: "Unit Comparison" },
                        { text: "NAV Harian" },
                        { text: "Window Redemption" },
                        { text: "Account Statement" },
                        { text: "Product & Agency Summary" },
                        { text: "Internal Proprietary" },
                        { text: "Window Redemption And Dividend" },
                        { text: "Moinvest Sales Report" },
                        //{ text: "Daily Transaction" },
                        //{ text: "Summary Transaction Monitoring" },
                        { text: "Transfer Dana Over Booking" },
                        { text: "Summary Daily AUM" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());
                    $('#FundFrom').data('kendoMultiSelect').destroy();
                    $('#divFundFrom').empty();
                    $('#divFundFrom').append('<select id="FundFrom" />')
                    FundCombo(0);



                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Unit Comparison') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblBank").hide();

                    }
                    else if (this.text() == 'NAV Harian') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblBank").hide();

                    }

                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Monthly Transaction By Fund And InvestorType And FundClient') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Historical Transaction Agent All') {
                        $("#LblAgentFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Indication By Agent') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Indication By Product') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Indication Summary') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();

                    }

                    else if (this.text() == 'Detail Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'AUM Comparison') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblBank").hide();

                    }

                    //else if (this.text() == 'Daily Transaction') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblStatus").show();
                    //}


                    else if (this.text() == 'Window Redemption') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Account Statement') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();

                    }
                    else if (this.text() == 'Product & Agency Summary') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        //LblAgentFrom
                        //$("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Internal Proprietary') {
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Window Redemption And Dividend') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblBank").hide();
                    }

                    else if (this.text() == 'Moinvest Sales Report') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                    }
                    else if (this.text() == 'Summary Transaction Monitoring') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();
                        //$("#LblClientFrom").show();

                    }

                    else if (this.text() == 'Transfer Dana Over Booking') {
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblBank").show();

                    }
                    else if (this.text() == 'Summary Daily AUM') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBank").hide();

                    }
                }
            }
            else if (_GlobClientCode == '11') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom
                        { text: "LPTI Subscription" },
                        { text: "LPTI Redemption" },
                        { text: "Batch Subscription Instruction" },
                        { text: "Batch Redemption Instruction" },

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();


                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'LPTI Subscription') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                    }

                    else if (this.text() == 'LPTI Redemption') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                    }

                    else if (this.text() == 'Batch Subscription Instruction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                        $("#LblSignature1").show();
                        $("#LblSignature2").show();
                        $("#LblSignature3").show();
                        $("#LblSignature4").show();
                        $("#LblNoSurat").show()
                    }

                    else if (this.text() == 'Batch Redemption Instruction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                        $("#LblSignature1").show();
                        $("#LblSignature2").show();
                        $("#LblSignature3").show();
                        $("#LblSignature4").show();
                        $("#LblNoSurat").show()
                    }

                }
            }

            else if (_GlobClientCode == '12') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                        { text: "Customer Portfolio All Fund Client" },

                        //{ text: "Batch Form SUB Instruction" },
                        //{ text: "Batch Form SUB Regular Instruction" },
                        //{ text: "Batch Form RED Amount Instruction" },
                        //{ text: "Batch Form RED Unit Instruction" },
                        //{ text: "Daily Transaction Report For All" },
                        //{ text: "Historical Transaction" },
                        //{ text: "Client Unit Position" },
                        //{ text: "Fund Unit Position" },
                        //{ text: "Unit Activity For Accounting" },
                        //{ text: "Client Unit Position Customized" },
                        //{ text: "Newton Report" },
                        //{ text: "Agent Commission Report" },
                        //{ text: "Monthly Transaction by Fund and InvestorType" },
                        //{ text: "Daily Transaction Report For Management" },

                        //custom
                        { text: "Unit Trust Report" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    //custom
                    else if (this.text() == 'Unit Trust Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    else if (this.text() == 'Customer Portfolio All Fund Client') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }

                }

            }
            // JsUnitRegistryRpt

            else if (_GlobClientCode == '13') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom
                        { text: "Client Performence" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                    //custom
                    else if (this.text() == 'Client Performence') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                }

            }


            else if (_GlobClientCode == '14') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },
                        { text: "Weekly Sales Report" },

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    ClearData();
                    getDateFrom(this.text());
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Weekly Sales Report') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }


                }
            }


            else if (_GlobClientCode == '16') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom
                        //{ text: "Monthly APERD Penjualan Per Kantor Pemasaran" },
                        //{ text: "Monthly APERD Penjualan Per Tenaga Pemasaran" },
                        //{ text: "Monthly APERD Profil Investor Perorangan" },
                        //{ text: "Lap Bulanan APERD Table 3" },
                        { text: "Subscription Report" },
                        { text: "Redemption Report" },
                        { text: "Client Tracking" },
                        { text: "Customer Portfolio" },
                        { text: "Rekap Management Fee Individu & Institusi" },
                        { text: "Rekap Management Fee Marketing" },
                        { text: "Rekap Management Fee Fund" },
                        { text: "Rekap Transaction Fee" },
                        //{ text: "High Risk Checking Subs Redemp" },
                        //{ text: "Monthly Report 100Mil" },
                        { text: "Daily Transaction Blotter Subscription" },
                        { text: "Daily Transaction Blotter Redemption" },
                        { text: "Daily Transaction Blotter Switching" },
                        { text: "Laporan Portofolio" },
                        { text: "Laporan Rincian Transaksi" },
                        { text: "Laporan Rekap Total NAB" },
                        { text: "Laporan Rekap Unit Penyertaan" },
                        //{ text: "Daily Report Total AUM Reksadana" },
                        { text: "Laporan Rekap Penjualan Harian" },
                        { text: "Laporan Rekap Penjualan MTD" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    index: 0
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Client Statement') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Tracking') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Redemption Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Subs Batch Form') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();

                    } else if (this.text() == 'Subscription Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Rekap Management Fee Individu & Institusi') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Rekap Management Fee Marketing') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Rekap Management Fee Fund') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Rekap Transaction Fee') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'High Risk Checking Subs Redemp') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Customer Portfolio') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Monthly Report 100Mil') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Daily Transaction Blotter Subscription') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Daily Transaction Blotter Redemption') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Daily Transaction Blotter Switching') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Laporan Portofolio') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Laporan Rincian Transaksi') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Daily Report Total AUM Reksa Dana') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Laporan Rekap Unit Penyertaan') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Laporan Rekap Total NAB') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                    }

                    else if (this.text() == 'Laporan Rekap Penjualan Harian') {
                        $("#LblDateFrom").show();
                    }

                    else if (this.text() == 'Laporan Rekap Penjualan MTD') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                }
            }

            else if (_GlobClientCode == '17') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom                      
                        { text: "Performance Report" },
                        { text: "Draft Performance Report" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });


                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                    else if (this.text() == 'Subs VS Redempt') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Performance Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").hide();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Draft Performance Report') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").hide();
                        $("#LblDateTo").show();
                    }

                }
            }

            else if (_GlobClientCode == '21') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom                      
                        { text: "Batch All Subs and Redemp" },
                        { text: "List Unit Per Client Code" },
                        { text: "Consolidation Report" },//AMBIl Yang INI

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });


                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                    else if (this.text() == 'Subs VS Redempt') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Batch All Subs and Redemp') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'List Unit Per Client Code') { //Yang Ini Juga
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Consolidation Report') { //Yang Ini Juga
                        $("#LblDateFrom").show();
                        $("#LblClientFrom").show();
                    }
                }
            }

            else if (_GlobClientCode == '22') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom                      
                        { text: "Subs VS Redempt" },
                        { text: "Transaction Data KPI Soft" },
                        { text: "Customer Summary KPI" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Subs VS Redempt') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Transaction Data KPI Soft') {
                        $("#LblFundFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Customer Summary KPI') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                }
            }

            else if (_GlobClientCode == '24') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom                      
                        { text: "Laporan Manager Investasi" },
                        { text: "Data Radisi" },
                        { text: "Laporan NAV" },
                        { text: "Rekap Transaksi Harian" },
                        { text: "Summary AUM" },
                        { text: "Transaksi Harian All" },
                        { text: "Data Transaksi Tahunan" },
                        { text: "Detail Informasi Bagi Hasil" },
                        { text: "Tracking Redemption Payment" },
                        { text: "Rekap Revenue Investor Institusi" },
                        { text: "Rekap Transaksi Bulanan Dan Tahunan" },
                        { text: "Rekap Management Fee" },

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Laporan Manager Investasi') {

                        $("#LblDateFrom").show();

                    }
                    else if (this.text() == 'Data Radisi') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();

                    }
                    else if (this.text() == 'Laporan NAV') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();

                    }
                    else if (this.text() == 'Rekap Transaksi Harian') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();

                    }
                    else if (this.text() == 'Summary AUM') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Transaksi Harian All') {
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Data Transaksi Tahunan') {
                        $("#LblPeriodFrom").show();
                        $("#LblPeriodTo").show();
                    }

                    else if (this.text() == 'Detail Informasi Bagi Hasil') {
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Tracking Redemption Payment') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Rekap Revenue Investor Institusi') {
                        $("#LblPeriodTo").show();
                    }
                    else if (this.text() == 'Rekap Transaksi Bulanan Dan Tahunan') {
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Rekap Management Fee') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                }
            }
            else if (_GlobClientCode == '25') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //{ text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        //{ text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        //{ text: "Historical NAV" },

                        //custom                      

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });



                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    //if (this.text() == 'Daily Transaction Report For All') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblClientFrom").show();
                    //    //$("#LblDepartmentFrom").show();
                    //    $("#LblStatus").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    //else if (this.text() == 'Fund Unit Position') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblFundFrom").show();
                    //    //$("#LblStatus").show();
                    //}
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    //else if (this.text() == 'Historical NAV') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblStatus").show();
                    //}
                    //else if (this.text() == 'Client Unit Position') {

                    //    $("#LblDateFrom").show();
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();

                    //}
                    //else if (this.text() == 'Historical Transaction') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblShownZeroBalance").show();
                    //    $("#LblStatus").show();
                    //}

                }
            }

            
            else if (_GlobClientCode == '29') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

                        //custom                      
                        { text: "Investor Activity" },
                        { text: "Net MGT Fee By Product" },
                        { text: "Report NetMgtFee by Product Detail" },
                        { text: "Report Weekly Reports SubsRedeem" },
                        { text: "Report Birthday Investor" },
                        { text: "AUM Balance Breakdown" },
                        { text: "Net Add" },
                        { text: "Redemption List" },
                        { text: "Subscription List" },
                        { text: "Switching List" },
                        { text: "Inquiry Data Profile Individual" },
                        { text: "Profile Nasabah" },
                        { text: "Customer Saldo" },
                        { text: "Transaction History" },
                        { text: "Report NetSubs by Product" },
                        { text: "Report Customer Detail" },
                        { text: "Monthly Statement Periodik" },
                        { text: "Report Summary AUM" },
                        { text: "NAV List" },
                        { text: "Daily NCF Report" },
                        { text: "Daily NCF Per Asset Class" },
                        { text: "Report Net subs monthly JWD BOD Range" },
                        { text: "Report Net Subs Monthly by distributor JWD Simple Range" },
                        { text: "Report Net subs monthly by distributor jwd range" },
                        { text: "Report Summary AUM by Distributor JWD Simple" },
                        { text: "Report Summary AUM by Distributor JWD NOC" },
                        { text: "Report Nett Subs by Product" },
                        { text: "Report Summary AUM by Distributor JWD" },

                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName
                });

                function OnChangeName() {
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }
                    HideParameter();
                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Investor Activity') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                    }

                    else if (this.text() == 'Net MGT Fee By Product') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Report NetMgtFee by Product Detail') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    else if (this.text() == 'Report Weekly Reports SubsRedeem') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    else if (this.text() == 'Report Birthday Investor') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'AUM Balance Breakdown') {
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Net Add') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Redemption List') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Subscription List') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Switching List') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Inquiry Data Profile Individual') {
                        $("#LblDateFrom").show();
                    }
                    else if (this.text() == 'Profile Nasabah') {
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Customer Saldo') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                    }
                    else if (this.text() == 'Transaction History') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Report NetSubs by Product') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Report Customer Detail') {
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Monthly Statement Periodik') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblClientFrom").show();
                    }
                    else if (this.text() == 'Report Summary AUM') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblAgentLevelOneFrom").show();
                        $("#LblBitIsAgent").show();
                        $("#LblBitIsAgentLevelOne").show();
                    }
                    else if (this.text() == 'NAV List') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if (this.text() == 'Daily NCF Report') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Daily NCF Per Asset Class') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Report Net Subs Monthly by distributor JWD Simple Range') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    else if (this.text() == 'Report Net subs monthly JWD BOD Range') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                    }
                    else if (this.text() == 'Report Summary AUM by Distributor JWD Simple') {
                        $("#LblDateFrom").show();
                        //$("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    else if (this.text() == 'Report Summary AUM by Distributor JWD NOC') {
                        $("#LblDateFrom").show();
                        $("#LblAgentFrom").show();

                    }
                    else if (this.text() == 'Report Summary AUM by Distributor JWD') {
                        $("#LblDateFrom").show();
                        $("#LblAgentFrom").show();

                    }
                    else if (this.text() == 'Report Nett Subs by Product') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblFundType").show();
                    }
                    else if (this.text() == 'Report Net subs monthly by distributor jwd range') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblAgentFrom").show();
                    }




                }

            }

            else {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Transaction Report For All" },
                        { text: "Client Unit Position" },
                        { text: "Fund Unit Position" },
                        { text: "Historical Transaction" },
                        { text: "Historical NAV" },

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
                    ClearData();
                    getDateFrom(this.text());

                    if (this.text() == 'Daily Transaction Report For All') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDepartmentFrom").show();
                        $("#LblStatus").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    else if (this.text() == 'Client Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        //$("#LblDateFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Fund Unit Position') {
                        $("#LblDateFrom").show();
                        $("#LblFundFrom").show();
                        //$("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical Transaction') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblShownZeroBalance").show();
                        $("#LblStatus").show();
                    }
                    else if (this.text() == 'Historical NAV') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblStatus").show();
                    }

                }
            }



            if (_GlobClientCode == "24") {
                $("#Status").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "POSTED ONLY", value: 1 },
                        { text: "APPROVED ONLY", value: 3 },
                        { text: "PENDING ONLY", value: 4 },
                        { text: "POSTED & APPROVED & PENDING", value: 7 },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeStatus,
                    value: setStatus(),
                    index: 1,
                });
            }
            else {
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
                    value: setStatus(),
                    index: 1,
                });
            }



            function OnChangeStatus() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }

            function setStatus() {
                return 1;
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

            $("#BitIsAgent").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "TRUE", value: true },
                    { text: "FALSE", value: false },
                ],
                filter: "contains",
                suggest: true,
                change: OnChangeBitIsAgent,
                index: 0
            });

            function OnChangeBitIsAgent() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }

            $("#BitIsAgentLevelOne").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                dataSource: [
                    { text: "TRUE", value: true },
                    { text: "FALSE", value: false },
                ],
                filter: "contains",
                suggest: true,
                change: OnChangeBitIsAgentLevelOne,
                index: 0
            });

            function OnChangeBitIsAgentLevelOne() {
                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
            }

            


            ////FundClient
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

            ////FundClientSID
            //$.ajax({
            //    url: window.location.origin + "/Radsoft/FundClient/GetFundClientSIDDetailComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            //    type: 'GET',
            //    contentType: "application/json;charset=utf-8",
            //    success: function (data) {
            //        $("#FundClientSIDFrom").kendoMultiSelect({
            //            dataValueField: "SID",
            //            dataTextField: "SID",
            //            filter: "contains",
            //            dataSource: data
            //        });
            //        $("#FundClientSIDFrom").data("kendoMultiSelect").value("0");
            //    },
            //    error: function (data) {
            //        alertify.alert(data.responseText);
            //    }
            //});

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

            //AGENT Level One
            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/GetAgentLevelOneComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#AgentLevelOneFrom").kendoMultiSelect({
                        dataValueField: "AgentPK",
                        dataTextField: "ID",
                        filter: "contains",
                        dataSource: data
                    });
                    $("#AgentLevelOneFrom").data("kendoMultiSelect").value("0");
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

            //Department
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundType",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#FundTypeFrom").kendoMultiSelect({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0

                    });

                    $("#FundTypeFrom").data("kendoMultiSelect").value("0");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            //Department
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CompanyType",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#CompanyTypeFrom").kendoMultiSelect({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0

                    });

                    $("#CompanyTypeFrom").data("kendoMultiSelect").value("0");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

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
                        //change: OnChangeSignature1,
                        filter: "contains",
                        //suggest: true,
                        //index: 0
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Signature/GetPosition1Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        $("#Position1").val(data.Position);
                //    }
                //});
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
                        //change: OnChangeSignature2,
                        filter: "contains",
                        //suggest: true,
                        //index: 0
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Signature/GetPosition2Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        $("#Position2").val(data.Position);
                //    }
                //});
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
                        //change: OnChangeSignature3,
                        filter: "contains",
                        //suggest: true,
                        //index: 0
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Signature/GetPosition3Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        $("#Position3").val(data.Position);
                //    }
                //});
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
                        //change: OnChangeSignature4,
                        filter: "contains",
                        //suggest: true,
                        //index: 0
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
                //$.ajax({
                //    url: window.location.origin + "/Radsoft/Signature/GetPosition4Combo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
                //    type: 'GET',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        $("#Position4").val(data.Position);
                //    }
                //});
            }

            //FundTypePK 
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundType",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#FundTypePK").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        filter: "contains",
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            //CompanyTypePK 
            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CompanyType",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#CompanyTypePK").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        dataSource: data,
                        filter: "contains",
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            win = $("#WinUnitRegistryRpt").kendoWindow({
                height: 550,
                title: "* Unit Registry Report",
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
            $("#ClientType").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                filter: "contains",
                suggest: true,
                change: OnChangeParamClientType,
                dataSource: [
                    { text: "Individu", value: "1" },
                    { text: "Institusi", value: "2" },
                    { text: "APERD", value: "3" },
                ],

            });
            function OnChangeParamClientType() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

            }

            $("#ClientTypeRHB").kendoComboBox({
                dataValueField: "value",
                dataTextField: "text",
                filter: "contains",
                suggest: true,
                change: OnChangeParamClientType,
                dataSource: [
                    { text: "RETAIL", value: "1" },
                    { text: "INSTITUTION", value: "2" },
                ],

            });
            function OnChangeParamClientType() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }

            }

            function ClearData() {
                $("#ValueDateFrom").val("");
                $("#ValueDateTo").val("");
                $("#AgentFrom").val("");
                $("#AgentLevelOneFrom").val("");
                $("#BitIsAgent").val("");
                $("#BitIsAgentLevelOne").val("");
                $("#DepartmentFrom").val("");
                $("#PeriodFrom").val("");
                $("#FundFrom").val("");
                $("#LblShownZeroBalance").val("");
                $("#BegDate").prop('checked', false);
                $("#ClientTypeRHB").val("");
                $("#ClientType").val("");
            }

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


            if ($('#BitAllSID').is(":checked") == true) {
                AllSID = 0;
            }
            else {
                AllSID = [];
                for (var i in checkedIds) {
                    if (checkedIds[i]) {
                        AllSID.push(i);
                    }
                }
            }

            if ($('#BitAllInternalCategory').is(":checked") == true) {
                AllInternalCategory = 0;
            }
            else {
                AllInternalCategory = [];
                for (var i in checkedInternalCategory) {
                    if (checkedInternalCategory[i]) {
                        AllInternalCategory.push(i);
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


                var ArrayFundClientSIDFrom = AllSID;
                var stringFundClientSIDFrom = "'";
                for (var i in ArrayFundClientSIDFrom) {
                    stringFundClientSIDFrom = stringFundClientSIDFrom + ArrayFundClientSIDFrom[i] + "','";

                }
                stringFundClientSIDFrom = stringFundClientSIDFrom.substring(0, stringFundClientSIDFrom.length - 2)

                var ArrayFundClientInternalCategoryFrom = AllInternalCategory;
                var stringFundClientInternalCategoryFrom = "'";
                for (var i in ArrayFundClientInternalCategoryFrom) {
                    stringFundClientInternalCategoryFrom = stringFundClientInternalCategoryFrom + ArrayFundClientInternalCategoryFrom[i] + "','";

                }
                stringFundClientInternalCategoryFrom = stringFundClientInternalCategoryFrom.substring(0, stringFundClientInternalCategoryFrom.length - 2)


                var ArrayAgentFrom = $("#AgentFrom").data("kendoMultiSelect").value();
                var stringAgentFrom = '';
                for (var i in ArrayAgentFrom) {
                    stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

                }
                stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

                var ArrayAgentLevelOneFrom = $("#AgentLevelOneFrom").data("kendoMultiSelect").value();
                var stringAgentLevelOneFrom = '';
                for (var i in ArrayAgentLevelOneFrom) {
                    stringAgentLevelOneFrom = stringAgentLevelOneFrom + ArrayAgentLevelOneFrom[i] + ',';

                }
                stringAgentLevelOneFrom = stringAgentLevelOneFrom.substring(0, stringAgentLevelOneFrom.length - 1)

                var ArrayDepartmentFrom = $("#DepartmentFrom").data("kendoMultiSelect").value();
                var stringDepartmentFrom = '';
                for (var i in ArrayDepartmentFrom) {
                    stringDepartmentFrom = stringDepartmentFrom + ArrayDepartmentFrom[i] + ',';

                }
                stringDepartmentFrom = stringDepartmentFrom.substring(0, stringDepartmentFrom.length - 1)

                var ArrayFundTypeFrom = $("#FundTypeFrom").data("kendoMultiSelect").value();
                var stringFundTypeFrom = '';
                for (var i in ArrayFundTypeFrom) {
                    stringFundTypeFrom = stringFundTypeFrom + ArrayFundTypeFrom[i] + ',';

                }
                stringFundTypeFrom = stringFundTypeFrom.substring(0, stringFundTypeFrom.length - 1)

                var ArrayCompanyTypeFrom = $("#CompanyTypeFrom").data("kendoMultiSelect").value();
                var stringCompanyTypeFrom = '';
                for (var i in ArrayCompanyTypeFrom) {
                    stringCompanyTypeFrom = stringCompanyTypeFrom + ArrayCompanyTypeFrom[i] + ',';

                }
                stringCompanyTypeFrom = stringCompanyTypeFrom.substring(0, stringCompanyTypeFrom.length - 1)

                var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
                var stringFundText = '';
                for (var i in FundText) {
                    stringFundText = stringFundText + FundText[i].ID + ',';

                }



                //if (validateData() == 1) {
                alertify.confirm("Are you sure want to Download data ?", function (e) {
                    if (e) {
                        $.blockUI({});
                        if ($('#Name').val() == "Daily Transaction") {
                            var UnitRegistryRpt = {

                                ReportName: $('#Name').val(),
                                ValueDateFrom: $('#ValueDateFrom').val(),
                                ValueDateTo: $('#ValueDateTo').val(),
                                FundFrom: stringFundFrom,
                                FundClientFrom: stringFundClientFrom,
                                FundClientSIDFrom: stringFundClientSIDFrom,
                                FundClientInternalCategoryFrom: stringFundClientInternalCategoryFrom,
                                PeriodFrom: $('#PeriodFrom').val(),
                                PeriodTo: $('#PeriodTo').val(),
                                AgentFrom: stringAgentFrom,
                                AgentLevelOneFrom: stringAgentLevelOneFrom,
                                BegDate: BegDate,
                                DepartmentFrom: stringDepartmentFrom,
                                PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                                BitIsAgent: $("#BitIsAgent").data("kendoComboBox").value(),
                                BitIsAgentLevelOne: $("#BitIsAgentLevelOne").data("kendoComboBox").value(),
                                Status: $("#Status").data("kendoComboBox").value(),
                                DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                                Message: $('#Message').val(),
                                NoSurat: $('#NoSurat').val(),
                                Signature1: $("#Signature1").data("kendoComboBox").value(),
                                Signature2: $("#Signature2").data("kendoComboBox").value(),
                                Signature3: $("#Signature3").data("kendoComboBox").value(),
                                Signature4: $("#Signature4").data("kendoComboBox").value(),
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Reports/GenerateDailyTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(UnitRegistryRpt),
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

                            var UnitRegistryRpt = {

                                ReportName: $('#Name').val(),
                                ValueDateFrom: $('#ValueDateFrom').val(),
                                ValueDateTo: $('#ValueDateTo').val(),
                                FundFrom: stringFundFrom,
                                FundClientFrom: stringFundClientFrom,
                                FundClientSIDFrom: stringFundClientSIDFrom,
                                FundClientInternalCategoryFrom: stringFundClientInternalCategoryFrom,
                                AgentFrom: stringAgentFrom,
                                AgentLevelOneFrom: stringAgentLevelOneFrom,
                                DepartmentFrom: stringDepartmentFrom,
                                PeriodFrom: $('#PeriodFrom').val(),
                                PeriodTo: $('#PeriodTo').val(),
                                BegDate: BegDate,
                                PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                                BitIsAgent: $("#BitIsAgent").data("kendoComboBox").value(),
                                BitIsAgentLevelOne: $("#BitIsAgentLevelOne").data("kendoComboBox").value(),
                                Status: $("#Status").data("kendoComboBox").value(),
                                DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                                Message: $('#Message').val(),
                                NoSurat: $('#NoSurat').val(),
                                Signature1: $("#Signature1").data("kendoComboBox").value(),
                                Signature2: $("#Signature2").data("kendoComboBox").value(),
                                Signature3: $("#Signature3").data("kendoComboBox").value(),
                                Signature4: $("#Signature4").data("kendoComboBox").value(),
                                ZeroBalance: $("#ZeroBalance").data("kendoComboBox").value(),
                                FundTypeFrom: stringFundTypeFrom,
                                CompanyTypeFrom: stringCompanyTypeFrom,
                                FundText: stringFundText,
                                ClientTypeRHB: $("#ClientTypeRHB").data("kendoComboBox").value(),
                                RoundingNav: $("#RoundingNav").data("kendoComboBox").value(),
                                WithAdjustment: $("#WithAdjustment").data("kendoComboBox").value(),
                                ClientType: $("#ClientType").data("kendoComboBox").value(),
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Reports/UnitRegistryReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(UnitRegistryRpt),
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
            if ($('#Name').val() == 'Client Unit Position' || $('#Name').val() == 'Unit Activity For Accounting' || $('#Name').val() == 'Client Unit Position Customized' ||
                $('#Name').val() == 'Newton Report' || $('#Name').val() == 'Monthly Transaction By Fund And InvestorType And FundClient' ||
                $('#Name').val() == 'Monthly Transaction by Fund and InvestorType' || $('#Name').val() == 'Transaction Monitoring' || $('#Name').val() == 'Window Redemption' ||
                $('#Name').val() == 'Account Statement' || $('#Name').val() == 'Unit Trust Report' ||
                $('#Name').val() == 'Historical Transaction Per Client' || $('#Name').val() == 'Client Report' || $('#Name').val() == 'Summary Client' ||
                $('#Name').val() == 'Management Fee Per Client' || $('#Name').val() == 'Daily Transaction Report' || $('#Name').val() == 'Customer Portfolio All Fund Client' ||
                $('#Name').val() == 'Fund Balance Detail' || $('#Name').val() == 'Fund Balance By Categories' || $('#Name').val() == 'Laporan Akun Bulanan Client' ||
                $('#Name').val() == 'Daily Subscription Instruction' || $('#Name').val() == 'Daily Redemption Instruction' || $('#Name').val() == 'Daily Switching Instruction' ||
                $('#Name').val() == 'Laporan AUM Institusi SID' || $('#Name').val() == 'Total Transaction Report Client' || $('#Name').val() == 'Total Transaction Report Fund' ||
                $('#Name').val() == 'Laporan Rekap Unit Penyertaan' || $('#Name').val() == 'Monthly Report 100Mil' || $('#Name').val() == 'Rebate Generali Fee Report' ||
                $('#Name').val() == 'Perhitungan Agent Fee Report' || $('#Name').val() == 'Client Performence' || $('#Name').val() == 'Internal Proprietary' || $('#Name').val() == 'Daily Transaction Report For All' ||

                $('#Name').val() == 'Client Statement' || $('#Name').val() == 'Client Tracking' || $('#Name').val() == 'Redemption Report' || $('#Name').val() == 'Subs Batch Form' ||
                $('#Name').val() == 'Subscription Report' || $('#Name').val() == 'Rekap Management Fee Individu & Institusi' || $('#Name').val() == 'Rekap Management Fee Marketing' || $('#Name').val() == 'Rekap Management Fee Fund' ||
                $('#Name').val() == 'Rekap Transaction Fee' || $('#Name').val() == 'Customer Portfolio' || $('#Name').val() == 'Monthly Report 100Mil' || $('#Name').val() == 'Daily Transaction Blotter Subscription' ||
                $('#Name').val() == 'Daily Transaction Blotter Redemption' || $('#Name').val() == 'Daily Transaction Blotter Switching' || $('#Name').val() == 'Laporan Portofolio' || $('#Name').val() == 'Laporan Rincian Transaksi' ||
                $('#Name').val() == 'Laporan Rekap Unit Penyertaan' || $('#Name').val() == 'Laporan Rekap Total NAB') {
                if ($('#BitAllClient').is(":checked") == false && All == 0) {
                    return 0;
                }
                else {
                    return 1;
                }
                if ($('#BitAllSID').is(":checked") == false && All == 0) {
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

        $("#CloseGridSID").click(function () {
            var All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }
            WinFundClientSID.close();
        });

        $("#CloseGridInternalCategory").click(function () {
            var All = [];
            for (var i in checkedInternalCategory) {
                if (checkedInternalCategory[i]) {
                    All.push(i);
                }
            }
            WinFundClientInternalCategory.close();
        });

        function HideParameter() {
            $("#LblDateFrom").hide();
            $("#LblDateTo").hide();
            $("#LblFundFrom").hide();
            $("#LblFundTo").hide();
            $("#LblClientFrom").hide();
            $("#LblClientTo").hide();
            $("#LblAgentFrom").hide();
            $("#LblAgentLevelOneFrom").hide();
            $("#LblBitIsAgent").hide();
            $("#LblBitIsAgentLevelOne").hide();
            $("#LblAgentTo").hide();
            $("#LblDepartmentFrom").hide();
            $("#LblDepartmentTo").hide();
            $("#LblBegDate").hide();
            $("#LblSignature1").hide();
            $("#LblSignature2").hide();
            $("#LblSignature3").hide();
            $("#LblSignature4").hide();
            $("#LblShownZeroBalance").hide();
            $("#LblMessage").hide();
            $("#LblNoSurat").hide();
            $("#LblPeriodFrom").hide();
            $("#LblPeriodTo").hide();
            $("#LblFundType").hide();
            $("#LblCompanyType").hide();
            $("#LblSIDFrom").hide();
            $("#LblClientTypeRHB").hide();
            $("#LblInternalCategoryFrom").hide();
            $("#LblRoundingNav").hide();
            $("#LblWithAdjustment").hide();
            $("#LblClientType").hide();
        }


        function onWinFundClientClose() {
            WinFundClient.close();
        }

        function getDateFrom(_reportName) {
            if (_reportName == 'Historical Transaction' || _reportName == 'Customer Portfolio All Fund Client') {
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

        FundCombo(0);
    }

});