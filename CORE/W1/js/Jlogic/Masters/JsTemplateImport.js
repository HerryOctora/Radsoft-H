$(document).ready(function () {
    var win;
    var GlobDecimalPlaces;
    
   
    var GlobValidator = $("#WinTemplateImport").kendoValidator().data("kendoValidator");
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

    function HideParameter() {
        $("#paramBank").hide();
        $("#paramAccount").hide();
        $("#paramCashierType").hide();
        $("#paramReference").hide();
        $("#paramCurrency").hide();
        $("#paramStatus").hide();
        $("#paramData").hide();
    }

    function initWindow() {

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

  
        InitName();

        function InitName() {
            if (_GlobClientCode == '01') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "01_TemplateLKReportAscend" },

                    ],
                    filter: "contains",
                    suggest: true,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '02') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        { text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "02_FFSTemplate_Bond" },
                        { text: "02_FFSTemplate_Equity" },
                        { text: "02_FFSTemplate_MixedAsset" },
                        { text: "02_FFSTemplate_MoneyMarket" },
                        { text: "02_LapKeu" },

                    ],
                    filter: "contains",
                    suggest: true,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '03') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "03_FinancialStatement" },
                        { text: "03_LaporanBalanceSheet" },
                        { text: "03_LaporanLabaRugi" },
                        { text: "03_LaporanLabaRugiComparison" }

                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '05') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "05_TemplateLK_MAM" },

                    ],
                    filter: "contains",
                    suggest: true,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '08') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        { text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "08_OskIsByCostCenterTemplate" },
                        { text: "08_OSKMonthlyReportTemplate" },
                        { text: "08_OSKMonthlyReportTemplateNewTB" },
                        { text: "08_PillarRHBAMTemplate" },

                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '11') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        { text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "11_OJK_Taspen" },


                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }

            else if (_GlobClientCode == '12') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "12_FFSTemplate" },
                        { text: "12_LapKeu" },

                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }
            else if (_GlobClientCode == '14') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TaxAmnestyDirjenTemplate" },
                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },

                        { text: "14_CompareIncomeStatement" },

                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }

            }
            else {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        // standart 
                        //{ text: "DailyTransactionTemplate" },
                        { text: "DTTOT_v1" },
                        //{ text: "DTTOT_v2" },
                        //{ text: "KPK" },
                        //{ text: "OJK_Rpt" },
                        //{ text: "PillarAMTemplate" },
                        //{ text: "PPATK" },
                        //{ text: "SID" },
                        { text: "TaxAmnestyApollo" },
                        { text: "TemplateBenchmarkIndex" },
                        { text: "TaxAmnestyDirjenTemplate" },

                        //{ text: "Template PnL" },
                        { text: "TemplateEOI2019" }, // xlsm
                        //{ text: "TemplateReports" },
                    ],
                    filter: "contains",
                    suggest: true,

                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
            }
        }
                     


        win = $("#WinTemplateImport").kendoWindow({
            height: 300,
            title: "* Template Import",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }
    $("#BtnDownload").click(function () {
        
     
            alertify.confirm("Are you sure want to Download Template Import ?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TemplateImport/GenerateTemplateImport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#Name').val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            //window.location = data
                            var newwindow = window.open(data, '_blank');
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
       
    });


    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close Template Import?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Template");
            }
        });
    });



});