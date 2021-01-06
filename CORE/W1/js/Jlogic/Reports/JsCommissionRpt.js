$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinCommissionRpt").kendoValidator().data("kendoValidator");
    var checkedIds = {};

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

    function HideParameter() {
        $("#LblFundFrom").hide();
        $("#LblClientFrom").hide();
        $("#LblAgentFrom").hide();
        $("#DepartmentFrom").hide();
        $("#LblAgentOnly").hide();
        $("#LblDateFrom").hide();
        $("#LblbitIsWithIndex").hide();
        $("#LblFundType").hide();
        $("#LblParamDate").hide();

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


        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
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

            if (_GlobClientCode == '03') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        //{ text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **

                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Revenue Per Fund Type" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report
                        //{ text: "Revenue Client Summary" }, //Agent Commission Report

                        //Custom
                        { text: "CSR Fee Report" },
                        { text: "Revenue Per Client Group By SID" }, //RevenuePerClient Group By SID**
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
                    //HideParameter();
                    ClearAttribute();


                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Per Fund Type') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'CSR Fee Report') {
                        $("#LblFundFrom").show();

                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }
                    if (this.text() == 'Revenue Client Summary') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").show();
                    }


                    if (this.text() == 'Revenue Per Client Group By SID') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblBegDate").hide();
                    }

                    //if (this.text() == 'Client Statement Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //}

                }
            }

            else if (_GlobClientCode == '06') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        { text: "Report Management Fee" },

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
                    //HideParameter();
                    ClearAttribute();

                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Report Management Fee') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }


                }
            }

            else if (_GlobClientCode == '07') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        { text: "Distributed SID Report" },

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
                    //HideParameter();
                    ClearAttribute();

                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Distributed SID Report') {
                    }


                }
            }
            else if (_GlobClientCode == '08') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        { text: "Client Statement Report" },
                        { text: "Radsoft VS BK Report" },
                        { text: "Fund Unit Ledger" },

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
                    //HideParameter();
                    ClearAttribute();

                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblValueDateFrom").hide();
                        $("#LblDateTo").show();
                        $("#LblDateFrom").show();
                        $("#LblbitIsWithIndex").show();
                    }
                    if (this.text() == 'Radsoft VS BK Report') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Fund Unit Ledger') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    //if (this.text() == 'AUM & Revenue Performance') {
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}

                }
            }
            else if (_GlobClientCode == '10') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        { text: "Sharing Fee Accounting" },
                        { text: "Selling Agent Summary" },
                        { text: "Sales Tracking" },
                        { text: "Monitoring Fee" },


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
                    //HideParameter();
                    ClearAttribute();


                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();

                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Sharing Fee Accounting') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }

                    if (this.text() == 'Sales Tracking') {
                        $("#LblFundType").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }

                    if (this.text() == 'Monitoring Fee') {
                        $("#LblFundFrom").show();
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
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        //{ text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom

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
                    //HideParameter();
                    ClearAttribute();

                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblWithTax").hide();
                    }

                }
            }

            else if (_GlobClientCode == '21') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        //{ text: "Agent Sharing Fee Report" },
                        { text: "Agent Tracking Report" },
                        { text: "PPH21 PPH23 Report" },
                        { text: "Komisi Penjualan Valuation" },
                        //{ text: "Summary Total Commission" },
                        { text: "Rekap Pembayaran Komisi Agent" },
                        //{ text: "PPH21 Per Agent Report" },

                        { text: "Agent Commission CSV CIMB APERD" },
                        { text: "Agent Commission CSV CIMB Non APERD" },
                        { text: "Agent Commission CSV Non CIMB APERD" },
                        { text: "Agent Commission CSV Non CIMB Non APERD" },

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
                    //HideParameter();
                    ClearAttribute();

                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Agent Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Tracking Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'PPH21 PPH23 Report') {
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'PPH21 Per Agent Report') {
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Summary Total Commission') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Rekap Pembayaran Komisi Agent') {
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                    if (this.text() == 'Komisi Penjualan Valuation') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblParamDate").show();
                    }

                    if (this.text() == 'Agent Commission CSV CIMB APERD') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblParamDate").show();
                    }

                    if (this.text() == 'Agent Commission CSV CIMB Non APERD') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblParamDate").show();
                    }

                    if (this.text() == 'Agent Commission CSV Non CIMB APERD') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblParamDate").show();
                    }

                    if (this.text() == 'Agent Commission CSV Non CIMB Non APERD') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblParamDate").show();
                    }


                }
            }

            else if (_GlobClientCode == '24') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Revenue Per Fund Type" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report
                        { text: "Sharing Fee Report AIA" },
                        { text: "Sharing Fee Report BNI Life" },
                        { text: "Sharing Fee Report BNI Life IDX 30" },
                        { text: "Rate Mi Fee Report" },
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
                    //HideParameter();
                    ClearAttribute();
                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    //if (this.text() == 'Agent Commission Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}


                    if (this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();

                    }
                    if (this.text() == 'AUM & Revenue Performance') {

                    }
                    if (this.text() == 'Revenue Fund Type') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Sharing Fee Report AIA') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }

                    if (this.text() == 'Sharing Fee Report BNI Life') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Sharing Fee Report BNI Life IDX 30') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Rate Mi Fee Report') {
                        $("#LblPeriodFrom").show();
                    }
                }
            }
            else if (_GlobClientCode == '25') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        //Standard
                        //{ text: "Sharing Fee Report" },
                        //{ text: "Client Tracking Report" },
                        //{ text: "Revenue Per Sales" },
                        //{ text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        //{ text: "Product Summary" },
                        //{ text: "Revenue Per Client" }, //RevenuePerClient **
                        //{ text: "Revenue Per Fund" }, //RevenuePerFund **
                        //{ text: "Agent Commission Report" }, //Agent Commission Report

                        //Custom
                        { text: "Revenue Per Sales Detail" },
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
                    //HideParameter();
                    ClearAttribute();

                    //if (this.text() == 'Sharing Fee Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblDepartmentFrom").hide();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblAgentOnly").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblCurrencyType").show();
                    //}
                    //if (this.text() == 'Revenue Per Sales') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    //if (this.text() == 'Revenue Per Sales Detail By Parent') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentGroupFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //if (this.text() == 'Selling Agent Summary') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //    $("#LblAgentFrom").show();
                    //}
                    //if (this.text() == 'Product Summary') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //if (this.text() == 'Revenue Per Client') {
                    //    //$("#LblFundFrom").show();
                    //    //$("#LblAgentFrom").show();
                    //    $("#LblClientFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //if (this.text() == 'Revenue Per Fund') {
                    //    $("#LblFundFrom").show();
                    //    //$("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}
                    //if (this.text() == 'Agent Commission Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}



                }
            }
            else {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Sharing Fee Report" },
                        { text: "Client Tracking Report" },
                        { text: "Revenue Per Sales" },
                        { text: "Revenue Per Sales Detail" },
                        { text: "Revenue Per Sales Detail By Parent" }, //Revenue Per Sales Detail By Parent
                        //{ text: "Selling Agent Summary" },
                        { text: "Product Summary" },
                        { text: "Revenue Per Client" }, //RevenuePerClient **
                        { text: "Revenue Per Fund" }, //RevenuePerFund **
                        { text: "Revenue Per Fund Type" }, //RevenuePerFund **
                        { text: "Agent Commission Report" }, //Agent Commission Report

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
                    //HideParameter();
                    ClearAttribute();
                    if (this.text() == 'Sharing Fee Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblFundType").show();
                        $("#LblAgentFeeType").show();
                    }
                    if (this.text() == 'Client Tracking Report' || this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblAgentOnly").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblCurrencyType").show();
                    }
                    if (this.text() == 'Revenue Per Sales') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Sales Detail By Parent') {
                        $("#LblFundFrom").show();
                        $("#LblAgentGroupFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Selling Agent Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                        $("#LblAgentFrom").show();
                    }
                    if (this.text() == 'Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Client') {
                        //$("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Per Fund') {
                        $("#LblFundFrom").show();
                        //$("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Agent Commission Report') {
                        $("#LblFundFrom").show();
                        $("#LblAgentFrom").show();
                        $("#LblDepartmentFrom").hide();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    //if (this.text() == 'Agent Commission Report') {
                    //    $("#LblFundFrom").show();
                    //    $("#LblAgentFrom").show();
                    //    $("#LblDateFrom").show();
                    //    $("#LblDateTo").show();
                    //}


                    if (this.text() == 'Client Statement Report') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();

                    }
                    if (this.text() == 'AUM & Revenue Performance') {

                    }
                    if (this.text() == 'Revenue Fund Type') {
                        $("#LblAgentFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }
                    if (this.text() == 'Revenue Product Summary') {
                        $("#LblFundFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

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


        $("#ClientStatementType").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
                { text: "With Index" },
                { text: "No Index" },
                { text: "Investment Income" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeClientStatementType,
            index: 0
        });
        function OnChangeClientStatementType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        $("#CurrencyType").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
                { text: "Original Currency" },
                { text: "Fee Convert to Rupiah" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCurrencyType,
            index: 0
        });
        function OnChangeCurrencyType() {

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

                //$("#FundTo").kendoComboBox({
                //    dataValueField: "FundPK",
                //    dataTextField: "ID",
                //    dataSource: data,
                //    change: OnChangeFundTo,
                //    filter: "contains",
                //    suggest: true,
                //    index: data.length - 1
                //});
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


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

        //AGENT
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboGroupOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentGroupFrom").kendoMultiSelect({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#AgentGroupFrom").data("kendoMultiSelect").value("1");
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


        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundType").kendoMultiSelect({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    dataSource: data,
                    //suggest: true,
                    //index: 0

                });
                $("#FundType").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        win = $("#WinCommissionRpt").kendoWindow({
            height: 550,
            title: "* Sales Report",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        //Agent Fee Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentFeeType").kendoMultiSelect({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    dataSource: data
                });
                $("#AgentFeeType").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        //Fund Fee Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundFeeType").kendoMultiSelect({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    index: 0,
                    dataSource: data
                });
                $("#FundFeeType").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        //Period

        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodFrom").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    filter: "contains",
                    enabled: true,
                    dataSource: data,
                    change: OnChangePeriodFrom
                });

                //$("#PeriodTo").kendoComboBox({
                //    dataValueField: "ID",
                //    dataTextField: "ID",
                //    filter: "contains",
                //    suggest: true,
                //    dataSource: data,
                //    change: OnChangePeriodTo
                //});
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
        //function OnChangePeriodTo() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        win.center();
        win.open();


    }


    $("#BtnDownload").click(function () {


        var BegDate;
        var All = 0;
        var WithTax;

        if ($('#BegDate').is(":checked") == true)
            BegDate = 1;
        else
            BegDate = 0;

        if ($('#WithTax').is(":checked") == true)
            WithTax = 1;
        else
            WithTax = 0;

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


            var ArrayAgentGroupFrom = $("#AgentGroupFrom").data("kendoMultiSelect").value();
            var stringAgentGroupFrom = '';
            for (var i in ArrayAgentFrom) {
                stringAgentGroupFrom = stringAgentGroupFrom + ArrayAgentGroupFrom[i] + ',';

            }
            stringAgentGroupFrom = stringAgentGroupFrom.substring(0, stringAgentGroupFrom.length - 1)


            var ArrayDepartmentFrom = $("#DepartmentFrom").data("kendoMultiSelect").value();
            var stringDepartmentFrom = '';
            for (var i in ArrayDepartmentFrom) {
                stringDepartmentFrom = stringDepartmentFrom + ArrayDepartmentFrom[i] + ',';

            }
            stringDepartmentFrom = stringDepartmentFrom.substring(0, stringDepartmentFrom.length - 1)

            var ArrayFundType = $("#FundType").data("kendoMultiSelect").value();
            var stringFundType = '';
            for (var i in ArrayFundType) {
                stringFundType = stringFundType + ArrayFundType[i] + ',';
            }
            stringFundType = stringFundType.substring(0, stringFundType.length - 1)

            var ArrayAgentFeeType = $("#AgentFeeType").data("kendoMultiSelect").value();
            var stringAgentFeeType = '';
            for (var i in ArrayAgentFeeType) {
                stringAgentFeeType = stringAgentFeeType + ArrayAgentFeeType[i] + ',';
            }
            stringAgentFeeType = stringAgentFeeType.substring(0, stringAgentFeeType.length - 1)

            var ArrayFundFeeType = $("#FundFeeType").data("kendoMultiSelect").value();
            var stringFundFeeType = '';
            for (var i in ArrayFundFeeType) {
                stringFundFeeType = stringFundFeeType + ArrayFundFeeType[i] + ',';
            }
            stringFundFeeType = stringFundFeeType.substring(0, stringFundFeeType.length - 1)

            if ($('#Name').val() == 'Client Statement Report') {
                if (ArrayFundClientFrom.length = 1 && ArrayFundClientFrom[0] == "0") {
                    alertify.alert("Client Statement report can only have one parameter Client and cannot use ALL");
                    return;
                }
                if (ArrayFundFrom.length = 1 && ArrayFundFrom[0] == "0") {
                    alertify.alert("Client Statement report can only have one parameter Fund and cannot use ALL");
                    return;
                }
            }


            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Download data ?", function (e) {
                if (e) {
                    $.blockUI({});
                    var _agentFrom;
                    if ($('#Name').val() == 'Revenue Per Sales Detail By Parent') {
                        _agentFrom = stringAgentGroupFrom;
                    }
                    else {
                        _agentFrom = stringAgentFrom;
                    }

                    var CommissionRpt = {

                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        ParamDate: $('#ParamDate').val(),
                        FundFrom: stringFundFrom,
                        FundClientFrom: stringFundClientFrom,
                        BegDate: BegDate,
                        WithTax: WithTax,
                        AgentFrom: _agentFrom,
                        DepartmentFrom: stringDepartmentFrom,
                        PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                        Status: $("#Status").data("kendoComboBox").value(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        Message: $('#Message').val(),
                        AgentOnly: $('#AgentOnly').is(":checked"),
                        ClientStatementType: $("#ClientStatementType").data("kendoComboBox").value(),
                        CurrencyType: $("#CurrencyType").data("kendoComboBox").value(),
                        FundType: stringFundType,
                        AgentFeeType: stringAgentFeeType,
                        FundFeeType: stringFundFeeType,
                        PeriodFrom: $('#PeriodFrom').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/CommissionReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(CommissionRpt),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
            //}
            checkedIds = [];
        }
        else {
            alertify.alert("Please Choose Client !")
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

    function ClearAttribute() {
        $("#LblValueDate").hide();
        //$("#LblValueDateFrom").hide(); 
        $("#LblDateTo").hide();
        $("#LblFundFrom").hide();
        $("#BegDate").prop('checked', false);
        //$("#LblFundTo").hide();
        $("#LblClientFrom").hide();
        //$("#LblClientTo").hide();
        $("#LblAgentFrom").hide();
        $("#LblAgentGroupFrom").hide();
        $("#LblBegDate").hide();
        //$("#LblAgentTo").hide();
        $("#LblDepartmentFrom").hide();
        $("#LblAgentFromByAll").hide();
        $("#LblbitIsWithIndex").hide();
        $("#LblbitIsInvestmentIncome").hide();
        $("#LblAgentOnly").hide();
        $("#LblDateFrom").hide();
        $("#LblCurrencyType").hide();
        //$("#LblDepartmentTo").hide();
        $("#LblFundType").hide();
        $("#LblParamDate").hide();
        $("#LblFundFeeType").hide();
        $("#LblAgentFeeType").hide();
        $("#LblPeriodFrom").hide();
    }

    function validatedata(All) {
        if ($('#Name').val() == 'Sharing Fee Report' || $('#Name').val() == 'Client Statement Report' || $('#Name').val() == 'Client Tracking Report') {
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

});