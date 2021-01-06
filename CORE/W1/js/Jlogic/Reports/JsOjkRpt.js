$(document).ready(function () {
    var win;

    var GlobValidator = $("#WinOjkRpt").kendoValidator().data("kendoValidator");
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



    function initWindow() {
        //$("#ValueDate").kendoDatePicker({
        //    value: new Date(),
        //});

        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnGenerateSipinaResult").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnGenerateFFSForCloseNav").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnGenerateFFSForIndex").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#ParamDateNKPD").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#ParamDateARIA").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });


        InitName();
        function InitName() {

            if (_GlobClientCode == '01') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "Daily Compliance Report", value: 9 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },


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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        //$("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 7) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 9) {
                        $("#trValueDateTo").show();
                        $("#LblFundFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }

                }
            }
            else if (_GlobClientCode == '02') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }
            else if (_GlobClientCode == '03') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        //{ text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

                        //custom
                        { text: "Fund Fact Sheet", value: 20 },
                        { text: "Exposure All Fund", value: 21 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 20) {
                        $("#trValueDateFrom").show();
                        $("#LblFundCombo").show();
                        $("#LblGenerateFFSForCloseNav").show();
                        $("#LblGenerateFFSForIndex").show();
                    }
                    else if ($("#Name").val() == 21) {
                        $("#trValueDateFrom").show();

                    }
                }
            }
            else if (_GlobClientCode == '06') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "Distributed SID Report", value: 7 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 7) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }
            else if (_GlobClientCode == '07') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "Distributed SID Report", value: 7 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 7) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }
            else if (_GlobClientCode == '09') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }

            else if (_GlobClientCode == '10') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "Transaction Monitoring", value: 10 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "ClientVsBlackListName", value: 15 },
                        { text: "Summary Transaction Monitoring", value: 17 },
                        { text: "Scoring Nasabah", value: 23 },





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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 10) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblStatus").show();
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 17) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 15) {
                        $("#trValueDateFrom").show();

                    }
                    else if ($("#Name").val() == 23) {
                        $("#trValueDateFrom").show();
                        $("#LblClientFrom").show();

                    }

                }
            }

            else if (_GlobClientCode == '11') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "OJK Report", value: 8 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 8) {
                        $("#LblFundFrom").show();
                        $("#trValueDateFrom").show();
                        $("#trPeriod").show();

                        $("#FundFrom").data("kendoMultiSelect").value(1)
                        $("#FundFrom").data("kendoMultiSelect").readonly(true);
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }
            else if (_GlobClientCode == '13') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },

                        //Custom
                        { text: "Risk Analyze Report", value: 19 },
                        { text: "Tata Kelola MI Report", value: 21 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }

                    else if ($("#Name").val() == 19) {

                    }
                    else if ($("#Name").val() == 21) {

                    }
                }
            }
            else if (_GlobClientCode == '14') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "Rebalancing Daily", value: 11 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }
                    else if ($("#Name").val() == 11) {
                        $("#trValueDateFrom").show();
                        $("#LblFundFrom").show();
                        $("#LblIndex").show();

                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }
            else if (_GlobClientCode == '17') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "KYC Risk Profile", value: 18 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 18) {
                        $("#trValueDateFrom").show();
                    }
                }
            }

            else if (_GlobClientCode == '20') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        //{ text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        //{ text: "SIPINA", value: 13 },
                        //{ text: "APU PPT", value: 14 },
                        { text: "Laporan EDD", value: 15 },
                        { text: "Laporan CDD", value: 16 },

                        { text: "Report KYC risk profile", value: 24 },
                        { text: "Investor Risk Profile", value: 25 },
                        { text: "Blacklist Report", value: 26 },

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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 15) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 16) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 24) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 25) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }

                    else if ($("#Name").val() == 26) {
                        $("#trValueDateFrom").show();

                    }

                }
            }

            else if (_GlobClientCode == '21') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Daily Compliance Report", value: 9 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "Broker Commission Report", value: 22 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());

                    if ($("#Name").val() == 26) {
                        $("#trValueDateTo").show();
                        $("#trValueDateFrom").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#LblFundFrom").show();
                    }
                    //if ($("#Name").val() == 1) {
                    //    $("#LblClientFrom").show();
                    //}
                    else if ($("#Name").val() == 2) {
                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    //else if ($("#Name").val() == 6) {
                    //    $("#trValueDateFrom").show();
                    //}
                    else if ($("#Name").val() == 9) {
                        $("#trValueDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }

                }
            }
            else if (_GlobClientCode == '22') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        //{ text: "Hutang Valas", value: 6 },
                        { text: "Daily Compliance Report", value: 9 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "ClientVsBlackListName", value: 15 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    //if ($("#Name").val() == 1) {
                    //    $("#LblClientFrom").show();
                    //}
                    if ($("#Name").val() == 2) {
                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    //else if ($("#Name").val() == 6) {
                    //    $("#trValueDateFrom").show();
                    //}
                    else if ($("#Name").val() == 9) {
                        $("#trValueDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 15) {
                        $("#trValueDateFrom").show();
                    }

                }
            }
            else if (_GlobClientCode == '25') {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        { text: "Hutang Valas", value: 6 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "Laporan EDD", value: 15 },
                        { text: "Laporan CDD", value: 16 },
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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    if ($("#Name").val() == 1) {
                        $("#LblClientFrom").show();
                    }
                    else if ($("#Name").val() == 2) {

                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate1").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 6) {
                        $("#trValueDateFrom").show();
                    }

                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 15) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 16) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                }
            }




            else {

                $("#Name").kendoComboBox({
                    dataValueField: "value",
                    dataTextField: "text",
                    dataSource: [
                        // { text: "OJK", value: 1 },
                        { text: "ARIA", value: 2 },
                        { text: "NKPD", value: 3 },
                        { text: "SiPesat", value: 4 },
                        { text: "KPD", value: 5 },
                        //{ text: "Hutang Valas", value: 6 },
                        { text: "Daily Compliance Report", value: 9 },
                        { text: "EOI Report", value: 12 },
                        { text: "SIPINA", value: 13 },
                        { text: "APU PPT", value: 14 },
                        { text: "ClientVsBlackListName", value: 15 },



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
                    $("#DownloadMode").data("kendoComboBox").value("");
                    HideParameter();
                    DownloadMode($("#Name").val());
                    //if ($("#Name").val() == 1) {
                    //    $("#LblClientFrom").show();
                    //}
                    if ($("#Name").val() == 2) {
                        $("#LblClientFromByAll").show();
                        $("#LblParamValueDate3").show();
                    }
                    else if ($("#Name").val() == 3) {
                        $("#LblParamValueDate3").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 4) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trPeriod").show();
                    }
                    else if ($("#Name").val() == 5) {
                        $("#LblFundFrom").show();
                        $("#LblParamValueDate3").show();
                    }
                    //else if ($("#Name").val() == 6) {
                    //    $("#trValueDateFrom").show();
                    //}
                    else if ($("#Name").val() == 9) {
                        $("#trValueDateTo").show();
                        $("#LblFundFrom").show();
                    }
                    else if ($("#Name").val() == 12) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#LblFundCombo").show();
                    }
                    else if ($("#Name").val() == 13) {
                        $("#trValueDateTo").show();
                        $("#LblGenerateSipina").show();
                    }
                    else if ($("#Name").val() == 14) {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();

                    }
                    else if ($("#Name").val() == 15) {
                        $("#trValueDateFrom").show();
                    }

                }
            }

        }

            function HideParameter() {
                $("#LblClientFromByAll").hide();
                $("#LblParamValueDate1").hide();
                $("#LblParamValueDate2").hide();
                $("#LblParamValueDate3").hide();
                $("#LblParamAPERD").hide();
                $("#LblFundFrom").hide();
                $("#trValueDateFrom").hide();
                $("#trValueDateTo").hide();
                $("#trPeriod").hide();
                $("#LblClientFrom").hide();
                $("#trPeriod").hide();
                $("#FundFrom").data("kendoMultiSelect").readonly(false);
                $("#LblStatus").hide();
                $("#LblFundCombo").hide();
                $("#LblGenerateSipina").hide();
                $("#LblGenerateFFSForCloseNav").hide();
                $("#LblGenerateFFSForIndex").hide();
            }



        win = $("#WinOjkRpt").kendoWindow({
            height: 500,
            title: "* OJK Report",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        $("#ParamInvestorType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Individual", value: 1 },
                { text: "Institution", value: 2 }
            ],
            filter: "contains",
            suggest: true,


        });


        $("#ParamAPERD").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "All", value: 1 },
                { text: "Direct Sales", value: 2 },
                { text: "APERD", value: 3 },
            ],
            filter: "contains",
            suggest: true,


        });

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

        //Param Data
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


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundCombo").kendoComboBox({
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
            }
        }


        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



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
            value: setStatus()
            //index: setStatus,
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setStatus() {
            if (_GlobClientCode == "10") {
                return 1;
            }
            else {
                return 7;
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Index/GetIndexCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Index").kendoComboBox({
                    dataValueField: "IndexPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeIndex,
                    filter: "contains",
                    //suggest: true,
                    //index: 0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeIndex() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        win.center();
        win.open();
    }

    $("#BtnDownload").click(function () {
        //resetNotification();

        var ArrayFundClientFrom = $("#FundClientFrom").data("kendoMultiSelect").value();
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var ArrayCounterpart = $("#Counterpart").data("kendoMultiSelect").value();
        var stringCounterpart = '';
        for (var i in ArrayCounterpart) {
            stringCounterpart = stringCounterpart + ArrayCounterpart[i] + ',';

        }
        stringCounterpart = stringCounterpart.substring(0, stringCounterpart.length - 1)

        if ($('#Name').val() == 11) {
            if (ArrayFundFrom.length > 1 || ArrayFundFrom[0] == "0") {
                alertify.alert("Report can only have one parameter Fund and cannot use ALL");
                return;
            }
        }

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {

                $.blockUI({});

                if ($("#Name").val() == 1) {
                    var OjkRpt = {
                        ReportName: $('#Name').val(),
                        FundClientFrom: stringFundClientFrom,
                        ValueDate: $('#ValueDate').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(OjkRpt),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }
                else if ($("#Name").val() == 2) {
                    if ($("#DownloadMode").data("kendoComboBox").value() == "Txt") {
                        var GenerateAria = {

                            ParamInvestorType: $("#ParamInvestorType").data("kendoComboBox").text(),
                            ReportName: $('#Name').val(),
                            Date: $("#ParamDateARIA").val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value()
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateAria),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data != null) {
                                    $.unblockUI();
                                    $("#downloadFileRadsoft").attr("href", data);
                                    $("#downloadFileRadsoft").attr("download", "Radsoft_ARIA.txt");
                                    document.getElementById("downloadFileRadsoft").click();
                                }
                                else {
                                    alertify.alert("Create Report Failed Or No Data");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert("Create Report Failed Or No Data");
                            }

                        });
                    }
                    else {
                        var GenerateAria = {
                            ParamInvestorType: $("#ParamInvestorType").data("kendoComboBox").text(),
                            ReportName: $('#Name').val(),
                            Date: $("#ParamDateARIA").val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value()
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateAria),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                var newwindow = window.open(data, '_blank');
                                $.unblockUI();
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert("Create Report Failed Or No Data");
                            }

                        });
                    }
                }

                //mulai lagi
                else if ($("#Name").val() == 3) {

                    if ($("#DownloadMode").data("kendoComboBox").value() == "Txt") {
                        var SInvestRpt = {
                            ReportName: $('#Name').val(),
                            Date: $("#Date").val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(SInvestRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data != null) {
                                    $.unblockUI();
                                    $("#downloadFileRadsoft").attr("href", data);
                                    $("#downloadFileRadsoft").attr("download", "Radsoft_NKPD.txt");
                                    document.getElementById("downloadFileRadsoft").click();
                                }
                                else {
                                    alertify.alert("Create Report Failed Or No Data");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }
                    else {
                        var SInvestRpt = {

                            Date: $("#Date").val(),
                            ReportName: $('#Name').val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(SInvestRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                var newwindow = window.open(data, '_blank');
                                //window.location = data
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }

                }
                else if ($("#Name").val() == 4) {
                    if ($("#DownloadMode").data("kendoComboBox").value() == "Txt") {
                        var SiPesatRpt = {
                            ReportName: $('#Name').val(),
                            PeriodPK: $("#PeriodPK").val(),
                            ValueDateFrom: $('#ValueDateFrom').val(),
                            ValueDateTo: $('#ValueDateTo').val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(SiPesatRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data != null) {
                                    $.unblockUI();
                                    $("#downloadFileRadsoft").attr("href", data);
                                    $("#downloadFileRadsoft").attr("download", "Radsoft_Sipesat.txt");
                                    document.getElementById("downloadFileRadsoft").click();
                                }
                                else {
                                    alertify.alert("Create Report Failed Or No Data");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }
                    else {
                        var SiPesatRpt = {
                            ReportName: $('#Name').val(),
                            PeriodPK: $("#PeriodPK").val(),
                            ValueDateFrom: $('#ValueDateFrom').val(),
                            ValueDateTo: $('#ValueDateTo').val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(SiPesatRpt),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                var newwindow = window.open(data, '_blank');
                                //$.unblockUI();
                                //$("#downloadFileRadsoft").attr("href", data);
                                //$("#downloadFileRadsoft").attr("download", "Radsoft_NKPD.txt");
                                //document.getElementById("downloadFileRadsoft").click();
                            },
                            error: function (data) {
                                $.unblockUI();
                                alertify.alert("Create Report Failed Or No Data");
                            }
                        });
                    }
                }
                //KPD
                else if ($("#Name").val() == 5) {
                    if ($("#DownloadMode").data("kendoComboBox").value() == "Txt") {
                        var GenerateAria = {
                            Fund: stringFundFrom,
                            Date: $("#Date").val(),
                            ReportName: $('#Name').val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateAria),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data != null) {
                                    $.unblockUI();
                                    $("#downloadFileRadsoft").attr("href", data);
                                    $("#downloadFileRadsoft").attr("download", "Radsoft_KPD.txt");
                                    document.getElementById("downloadFileRadsoft").click();

                                }
                                else {
                                    alertify.alert("Create Report Failed Or No Data");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }
                    else {
                        var GenerateAria = {
                            Fund: stringFundFrom,
                            Date: $("#Date").val(),
                            ReportName: $('#Name').val(),
                            DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(GenerateAria),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                var newwindow = window.open(data, '_blank');
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }

                }

                //Hutang Valas
                else if ($("#Name").val() == 6) {
                    if ($("#DownloadMode").data("kendoComboBox").value() == "Txt") {

                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundClient/GenerateHutangValas/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data != null) {
                                    $.unblockUI();
                                    $("#downloadFileRadsoft").attr("href", data);
                                    $("#downloadFileRadsoft").attr("download", "Radsoft_HutangValas.txt");
                                    document.getElementById("downloadFileRadsoft").click();
                                }
                                else {
                                    alertify.alert("Create Report Failed Or No Data");
                                    $.unblockUI();
                                }

                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }
                    else {
                        var GenerateAria = {
                            FundFrom: stringFundFrom,
                            Date: $("#Date").val()
                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundClient/GenerateHutangValasToExcel/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + stringFundFrom,
                            type: 'POST',
                            data: JSON.stringify(GenerateAria),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                $.unblockUI();
                                var newwindow = window.open(data, '_blank');
                            },
                            error: function (data) {
                                alertify.alert("Create Report Failed Or No Data");
                                $.unblockUI();
                            }

                        });
                    }
                }

                //Distributed SID
                else if ($("#Name").val() == 7) {
                    var GenerateSID = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value()
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateSID),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }

                    });
                }

                //OJK Report
                else if ($("#Name").val() == 8) {
                    var GenerateSID = {
                        ReportName: $('#Name').val(),
                        FundFrom: stringFundFrom,
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        PeriodPK: $('#PeriodPK').val(),
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateSID),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }

                    });
                }


                //Daily Compliance Ascend
                else if ($("#Name").val() == 9) {
                    var GenerateDailyCompliance = {
                        FundFrom: stringFundFrom,
                        ReportName: $('#Name').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }

                    });
                }

                //Transaction Monitoring Mandiri
                else if ($("#Name").val() == 10) {
                    var GenerateDailyCompliance = {
                        FundFrom: stringFundFrom,
                        FundClientFrom: stringFundClientFrom,
                        ReportName: $("#Name").data("kendoComboBox").text(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        Status: $("#Status").data("kendoComboBox").value(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/UnitRegistryReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundAccountingReport_O",
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }

                    });
                }

                //Rebalancing Daily
                else if ($("#Name").val() == 11) {
                    var RebalancingDaily = {
                        FundFrom: stringFundFrom,
                        ReportName: $("#Name").data("kendoComboBox").text(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        IndexPK: $("#Index").data("kendoComboBox").value(),

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/InvestmentReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundAccountingReport_O",
                        type: 'POST',
                        data: JSON.stringify(RebalancingDaily),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }

                    });
                }
                // EOI
                else if ($("#Name").val() == 12) {
                    var EOI = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        //ParamAPERD: $("#ParamAPERD").data("kendoComboBox").text(),
                        //FundClientFrom: stringFundClientFrom,
                        //ValueDate: $('#ValueDate').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        FundCombo: $("#FundCombo").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EOI),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 13) {
                    var EOI = {
                        ReportName: $('#Name').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EOI),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 14) {
                    var EOI = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EOI),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 15) {
                    var EOI = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EOI),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 16) {
                    var EOI = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(EOI),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 17) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 18) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 19) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 20) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        FundPK: $('#FundCombo').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/GenerateFFS/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 21) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        FundPK: $('#FundCombo').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            window.location = data;
                            //  var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 22) {


                    var FundAccountingRpt = {
                        ReportName: "Broker Commission Report",
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        //AccountFrom: stringAccountFrom,
                        //AccountTo: $("#AccountTo").data("kendoComboBox").text(),
                        //FundClientFrom: stringFundClientFrom,
                        //FundClientTo: $("#FundClientTo").data("kendoComboBox").text(),
                        FundFrom: stringFundFrom,
                        //CurrencyFrom: stringCurrencyFrom,
                        CounterpartFrom: stringCounterpart,
                        //BondRatingFrom: stringBondRatingFrom,
                        //InstrumentTypeFrom: $("#InstrumentType").data("kendoComboBox").value(),
                        //FundTo: $("#FundTo").data("kendoComboBox").text(),
                        //InstrumentFrom: stringInstrumentFrom,
                        //InstrumentTo: $("#InstrumentTo").data("kendoComboBox").text(),
                        //PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                        //ShowNullData: $("#ShowNullData").data("kendoComboBox").value(),
                        //Status: $("#Status").data("kendoComboBox").value(),
                        ParamData: $("#ParamData").data("kendoComboBox").value(),
                        //Profile: $("#Profile").data("kendoComboBox").text(),
                        //Groups: $("#Groups").data("kendoComboBox").text(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //FundPK: $('#FundPK').val(),
                        //Signature1: $("#Signature1").data("kendoComboBox").value(),
                        //Signature2: $("#Signature2").data("kendoComboBox").value(),
                        //Signature3: $("#Signature3").data("kendoComboBox").value(),
                        //Signature4: $("#Signature4").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/FundAccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingReport_O",
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


                else if ($("#Name").val() == 23) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        //ValueDateTo: $('#ValueDateTo').val(),
                        FundClientFrom: stringFundClientFrom,
                        //ValueDate: $('#ValueDate').val(),
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        //FundCombo: $("#FundCombo").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 24) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        FundClientFrom: stringFundClientFrom,
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }

                else if ($("#Name").val() == 25) {
                    var GenerateDailyCompliance = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        FundClientFrom: stringFundClientFrom,
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/ComplianceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateDailyCompliance),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert("Create Report Failed Or No Data");
                        }
                    });
                }




            }
        });
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
        $("#LblParamInvestorType").hide();
        $("#LblParamValueDate").hide();
        $("#LblIndex").hide();
        $("#LblParamAPERD").hide();
    }


    function DownloadMode(_name) {
        if (_name == 7 || _name == 9) {
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
        }
        else {
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



    $("#BtnGenerateSipinaResult").click(function () {
       
        alertify.confirm("Are you sure want to Generate data ?", function (e) {
            if (e) {
                $.blockUI({});

                var GenerateUnitFeeSummary = {                   
                    ValueDateTo: $('#ValueDateTo').val(),                   
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/GenerateSipinaResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
        });


    });

    $("#BtnGenerateFFSForCloseNav").click(function () {

        alertify.confirm("Are you sure want to Generate data ?", function (e) {
            if (e) {
                $.blockUI({});

                var GenerateFFSForCloseNav = {
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    FundPK: $("#FundCombo").data("kendoComboBox").value(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FFSSetup/GenerateFFSForCloseNav/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(GenerateFFSForCloseNav),
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
        });


    });




    $("#BtnGenerateFFSForIndex").click(function () {

        alertify.confirm("Are you sure want to Generate data ?", function (e) {
            if (e) {
                $.blockUI({});

                var GenerateFFSForIndex = {
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    FundPK: $("#FundCombo").data("kendoComboBox").value(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FFSSetup/GenerateFFSForIndex/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(GenerateFFSForIndex),
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
        });


    });




});


