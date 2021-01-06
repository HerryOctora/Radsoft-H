$(document).ready(function () {
    var win;
    var GlobDecimalPlaces;
    var GlobValidator = $("#WinAccountingRpt").kendoValidator().data("kendoValidator");
    var _d = new Date();
    var _fy = _d.getFullYear();

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

    function HideParameter() {
        $("#paramAccount").hide();
        $("#paramOffice").hide();
        $("#paramDepartment").hide();
        $("#paramAgent").hide();
        $("#paramConsignee").hide();
        $("#paramInstrument").hide();
        $("#paramCounterpart").hide();
        $("#paramData").hide();
        $("#paramStatus").hide();
        $("#paramDateFrom").hide();
        $("#paramDateTo").hide();
        $("#paramProfile").hide();
        $("#paramGroups").hide();
        $("#paramRowFrom").hide();
        $("#paramRowTo").hide();
        $("#paramAccountBy").hide();
        $("#paramPeriod").hide();
        $("#paramPageBreak").hide();
        $("#paramFund").hide();
    }

    function initWindow() {
        //GetJournalDecimalPlaces
        $.ajax({
            url: window.location.origin + "/Radsoft/AccountingSetup/GetJournalDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                GlobDecimalPlaces = data;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        HideParameter();

        $("#Profile").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "A" },
            ],
        });

        $("#Groups").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "ALL" },
               { text: "Department" },
               { text: "Office" },
               { text: "Direct" },
               //{ text: "Consignee" },
               { text: "Instrument" },
               //{ text: "Counterpart" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeGroups,
            index: 0
        });
        function OnChangeGroups() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            HideParameterGroups();

            if (this.text() == 'Counterpart') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramCounterpart").show();
                $("#paramStatus").show();
            }
            else if (this.text() == 'Instrument') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramInstrument").show();
                $("#paramStatus").show();
            }
            else if (this.text() == 'Department') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramDepartment").show();
                $("#paramStatus").show();
            }
            else if (this.text() == 'Office') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramStatus").show();
                $("#paramOffice").show();
            }
            else if (this.text() == 'Direct') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramStatus").show();
                $("#paramAgent").show();
            }
            else if (this.text() == 'Consignee') {
                $("#paramDateTo").show();
                $("#paramDateFrom").show();
                $("#paramAccount").show();
                $("#paramStatus").show();
                $("#paramConsignee").show();
            }
            else {

            }

        }
        function HideParameterGroups() {
            $("#paramDateTo").hide();
            $("#paramDateFrom").hide();
            $("#paramAccount").hide();
            $("#paramOffice").hide();
            $("#paramDepartment").hide();
            $("#paramAgent").hide();
            $("#paramConsignee").hide();
            $("#paramInstrument").hide();
            $("#paramCounterpart").hide();
            $("#paramStatus").hide();
        }

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
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

                $("#PeriodPK").data("kendoComboBox").text($("#ValueDateFrom").data("kendoDatePicker").value().getFullYear().toString());
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

        InitName();
        function InitName() {
            if (_GlobClientCode == '01') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       
                       { text: "Laporan Keuangan" },

                       //bawah Standart
                       { text: "Journal Voucher" },
                       { text: "Account Activity" },
                       { text: "Account Activity Plain" },
 		               { text: "Account Activity By DEPT" },
                       { text: "Account Activity By INSTRUMENT" },
		               { text: "Account Activity By DIRECT" }, // Blom ada
                       { text: "Trial Balance Plain" },
 		               { text: "Trial Balance Groups" },
                       { text: "Balance Sheet Plain" },
                       { text: "Income Statement Plain" },
                       { text: "Income Statement Groups" },
                       { text: "List Instrument By Account" },
                       { text: "List Department By Account" },
 			           //{ text: "List Agent By Account" }, // Blom ada
                      
                       
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


                    if (this.text() == 'Laporan Keuangan') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                        //standart
                    else if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                   
                }
            }

            else if (_GlobClientCode == '02') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       

                       { text: "Laporan Keuangan" },
           

                        //bawah Standart
                       { text: "Journal Voucher" },
                       { text: "Account Activity" },
                       { text: "Account Activity By Counterpart" },
                       { text: "Account Activity Plain" },
 		               { text: "Account Activity By DEPT" },
                       { text: "Account Activity By INSTRUMENT" },
		               { text: "Account Activity By DIRECT" }, // Blom ada
                       { text: "Trial Balance Plain" },
 		               { text: "Trial Balance Groups" },
                       { text: "Balance Sheet Plain" },
                       { text: "Income Statement Plain" },
                       { text: "Income Statement Groups" },
                       { text: "List Instrument By Account" },
                       { text: "List Department By Account" },
                       { text: "Fixed Asset" },
 			           //{ text: "List Agent By Account" }, // Blom ada

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


                   if (this.text() == 'Laporan Keuangan') {//
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();

                   }
                       //standart
                   else if (this.text() == 'Journal Voucher') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramOffice").show();
                       $("#paramDepartment").show();
                       $("#paramAgent").show();
                       $("#paramConsignee").show();
                       $("#paramInstrument").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramOffice").show();
                       $("#paramDepartment").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity Plain') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By DEPT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramDepartment").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By INSTRUMENT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By DIRECT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramAgent").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Trial Balance Plain') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Trial Balance Groups') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramData").show();
                       $("#paramStatus").show();
                       $("#paramGroups").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Balance Sheet Plain') {
                       $("#paramDateTo").show();
                       $("#paramData").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Income Statement Plain') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Income Statement Groups') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramStatus").show();
                       $("#paramGroups").show();
                       $("#paramPageBreak").show();

                   }

                   else if (this.text() == 'List Instrument By Account') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramInstrument").show();
                       $("#paramData").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'List Department By Account') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramDepartment").show();
                       $("#paramData").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By Counterpart') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramOffice").show();
                       $("#paramDepartment").show();
                       $("#paramAgent").show();
                       $("#paramConsignee").show();
                       $("#paramInstrument").show();
                       $("#paramStatus").show();
                       $("#paramCounterpart").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Fixed Asset') {
                   }

                }
            }


            else if (_GlobClientCode == '03') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //{ text: "Laporan Laba Rugi" },
                        { text: "Laporan Laba Rugi Comparison" },
                        { text: "Neraca Comparison" },


                        //bawah Standart
                        { text: "Journal Voucher" },
                        //{ text: "Account Activity" },
                        //{ text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Account Activity By CONSIGNEE" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "List Consignee By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada
                        //----Custom----//
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "GL Piutang Revenue" },
                        { text: "Financial Statement" },
                        { text: "Financial Statement By Instrument" },
                        { text: "Budget to Actual" },
                        { text: "Balance Sheet" },
                        { text: "CSR Report" },
                        { text: "Monthly Report Accounting" },
                        { text: "Management Fee By Fund Type" },
                        { text: "Report CSR Per Yayasan Per Produk" },
                        { text: "Report CSR Per Yayasan Per Produk Inception" },
                        { text: "CSR Per Yayasan" }

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


                    if (this.text() == 'Laporan Laba Rugi Comparison') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Laporan Laba Rugi') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPageBreak").show();
                    }
                    //standart
                    else if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        //$("#paramAgent").show();
                        //$("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By CONSIGNEE') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramConsignee").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'GL Piutang Revenue') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramAccountBy").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Financial Statement') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                    }

                    else if (this.text() == 'Neraca Comparison') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramData").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Financial Statement By Instrument') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramData").show();
                        $("#paramInstrument").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Budget to Actual') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramData").show();
                        $("#paramInstrument").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Balance Sheet') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Consignee By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramConsignee").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'CSR Report') {
                        $("#paramDateFrom").hide();
                        $("#paramDateTo").show();
                        $("#paramAgent").show();
                        $("#paramFund").show();
                    }

                    else if (this.text() == 'Monthly Report Accounting') {
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramFund").hide();
                    }

                    else if (this.text() == 'Management Fee By Fund Type') {
                        $("#paramPeriod").show();
                        $("#paramFund").hide();
                    }

                    else if (this.text() == 'Report CSR Per Yayasan Per Produk') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramAgent").show();

                    }


                    else if (this.text() == 'Report CSR Per Yayasan Per Produk Inception') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramAgent").show();

                    }

                    else if (this.text() == 'CSR Per Yayasan') {
                        $("#paramPeriod").hide();
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramAgent").show();
                    }
                }
            }


            else if (_GlobClientCode == '04') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

               
                    { text: "CALK" },

                    //bawah Standart
                    { text: "Journal Voucher" },
                    { text: "Account Activity" },
                    { text: "Account Activity Plain" },
                    { text: "Account Activity By DEPT" },
                    { text: "Account Activity By INSTRUMENT" },
                    { text: "Account Activity By DIRECT" }, // Blom ada
                    { text: "Trial Balance Plain" },
                    { text: "Trial Balance Groups" },
                    { text: "Balance Sheet Plain" },
                    { text: "Income Statement Plain" },
                    { text: "Income Statement Groups" },
                    { text: "List Instrument By Account" },
                    { text: "List Department By Account" },
                    //{ text: "List Agent By Account" }, // Blom ada

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


                   if (this.text() == 'CALK') {
                       $("#paramDateTo").show();
                       $("#paramPageBreak").show();
                   }
                       //standart
                   else if (this.text() == 'Journal Voucher') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramOffice").show();
                       $("#paramDepartment").show();
                       $("#paramAgent").show();
                       $("#paramConsignee").show();
                       $("#paramInstrument").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramOffice").show();
                       $("#paramDepartment").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity Plain') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By DEPT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramDepartment").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By INSTRUMENT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Account Activity By DIRECT') {
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramAgent").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Trial Balance Plain') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Trial Balance Groups') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramData").show();
                       $("#paramStatus").show();
                       $("#paramGroups").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Balance Sheet Plain') {
                       $("#paramDateTo").show();
                       $("#paramData").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Income Statement Plain') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'Income Statement Groups') {
                       $("#paramData").show();
                       $("#paramDateTo").show();
                       $("#paramDateFrom").show();
                       $("#paramStatus").show();
                       $("#paramGroups").show();
                       $("#paramPageBreak").show();

                   }
                   else if (this.text() == 'List Instrument By Account') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramInstrument").show();
                       $("#paramData").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                   else if (this.text() == 'List Department By Account') {
                       $("#paramDateFrom").show();
                       $("#paramDateTo").show();
                       $("#paramDepartment").show();
                       $("#paramData").show();
                       $("#paramAccount").show();
                       $("#paramStatus").show();
                       $("#paramPageBreak").show();
                   }
                    
                }
            }

            else if (_GlobClientCode == '05') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                       { text: "Cash Flow" },
                       { text: "Mutasi Fix Asset" },
                       { text: "Cash Advance" },
                       { text: "Kartu Hutang" },
                       { text: "Trial Balance" },
                       { text: "Lap Keu" },
                       //new add(check parameter)
                       { text: "Financial Statement PL Cabang" },
                       { text: "Trial Balance" },
                       { text: "Laporan Jamuan" },
                       { text: "Laporan Laba Rugi dan Komprehensif Lainnya" },
                       { text: "List Fixed Asset" },
                       { text: "Perhitungan Fiskal" },
                       { text: "Updated Performance Report" },

                       //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada
                  
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

                   
                    if (this.text() == 'Cash Flow') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Mutasi Fix Asset') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Cash Advance') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Kartu Hutang') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance') {
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Lap Keu') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Financial Statement PL Cabang') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Laporan Jamuan') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Laporan Laba Rugi dan Komprehensif Lainnya') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Fixed Asset') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Perhitungan Fiskal') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Updated Performance Report') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    //standart
                    else if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                }


            }

            else if (_GlobClientCode == '06') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                       
                        { text: "Report Data Bank" },
                        { text: "Balance Sheet" },
                        { text: "Profit And Loss" },

                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                   if (this.text() == 'Report Data Bank') {//
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet') {//
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Profit And Loss') {//
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    //standart
                    else if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    
                }
            }


            else if (_GlobClientCode == '07') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
               if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                }
                else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                }
                else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                }
                else if (this.text() == 'Income Statement Groups') {
                    $("#paramData").show();
                    $("#paramDateTo").show();
                    $("#paramDateFrom").show();
                    $("#paramStatus").show();
                    $("#paramGroups").show();
                    $("#paramPageBreak").show();

                }
                else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                }
            }
            else if (_GlobClientCode == '08') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        //{ text: "Account Activity By Counterpart" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

                        //custom
                        { text: "RHB OSK Monthly Report TB New" },
                        { text: "RHB OSK IS By CostCenter" },
                        { text: "Trial Balance Plain MTD" },
                        { text: "AUM 4 Pillar" },
                        { text: "TB RECAP DETAIL PER COST CENTER" },
                        { text: "PILLAR MTD" },
                        { text: "PILLAR YTD" },
                        { text: "PILLAR AM" },
                        { text: "Income Statement Period" },
                        { text: "General Ledger" },

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'RHB OSK Monthly Report TB New') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    } 
                    else if (this.text() == 'RHB OSK IS By CostCenter') {
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramDepartment").show();
                        $("#paramPageBreak").show();
                        $("#paramDateFrom").show();
                    }
                    else if (this.text() == 'Trial Balance Plain MTD') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramPageBreak").show();
                    } 
                    else if (this.text() == 'AUM 4 Pillar') {
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramInstrument").show();
                        $("#paramPageBreak").show();
                    } 
                    else if (this.text() == 'TB RECAP DETAIL PER COST CENTER') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    } 
                    else if (this.text() == 'PILLAR MTD') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'PILLAR YTD') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'PILLAR AM') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Period') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                }
            }
            else if (_GlobClientCode == '09') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                }
            }
            else if (_GlobClientCode == '10') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" }, // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                }
            }
            else if (_GlobClientCode == '11') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                }
            }

            else if (_GlobClientCode == '12') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "Laporan Keuangan" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Laporan Keuangan') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramAccount").show();
                        $("#paramData").show();
                        $("#paramPageBreak").show();
                    }
                }
            }


                
            else if (_GlobClientCode == '14') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        { text: "Compare Income Statement" },
                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "Laporan Keuangan" },
                        //{ text: "List Agent By Account" }, // Blom ada

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


                    //standart
                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Laporan Keuangan') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramAccount").show();
                        $("#paramData").show();
                        $("#paramPageBreak").show();
                    }


                    else if (this.text() == 'Compare Income Statement') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                }
            }

            else if (_GlobClientCode == '17') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },  // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "Invoice Management Fee" },
                        { text: "Client Expense Vs Client Revenue" },
                        //{ text: "Report Accued Interest Deposito" },
                        //{ text: "List Agent By Account" }, // Blom ada
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


                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Invoice Management Fee') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                        $("#paramInstrument").show();
                    }
                    else if (this.text() == 'Report Accued Interest Deposito') {
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }
                    else if (this.text() == 'Client Expense Vs Client Revenue') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramConsignee").show();
                    }

                }
            }

            else if (_GlobClientCode == '19') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        { text: "Laporan Keuangan" },
                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },  // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada
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


                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    } else if (this.text() == 'Laporan Keuangan') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                    }

                }
            }


            else if (_GlobClientCode == '21') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [

                        //{ text: "Laporan Keuangan" },
                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "General Ledger" },
                        { text: "General Ledger Plain" },
                        { text: "General Ledger By DEPT" },
                        { text: "General Ledger By INSTRUMENT" },
                        { text: "General Ledger By DIRECT" },  // Blom ada
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        //{ text: "List Agent By Account" }, // Blom ada
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


                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'General Ledger By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    //else if (this.text() == 'Laporan Keuangan') {
                    //    $("#paramDateFrom").show();
                    //    $("#paramDateTo").show();
                    //}

                }
            }

            else if (_GlobClientCode == '33') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [


                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },  // Blom ada
                        { text: "Account Activity By CONSIGNEE" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Detail" },
                        { text: "Income Statement Detail" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "List Consignee By Account" },
                        { text: "Daily Transaction Bonds" },
                        { text: "Laporan Posisi Keuangan" },
                        { text: "Laporan Laba Rugi" },
                        { text: "Laporan Arus Kas" },
                        //{ text: "List Agent By Account" }, // Blom ada
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


                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Account Activity By CONSIGNEE') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramConsignee").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Detail') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Detail') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'List Consignee By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramConsignee").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Daily Transaction Bonds') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
                        $("#trFundFrom").show();
                    }

                    else if (this.text() == 'Laporan Posisi Keuangan') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Laporan Laba Rugi') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Laporan Arus Kas') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramPeriod").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                }
            }


            else {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
            
                      
                        //bawah Standart
                        { text: "Journal Voucher" },
                        { text: "Account Activity" },
                        { text: "Account Activity Plain" },
                        { text: "Account Activity By DEPT" },
                        { text: "Account Activity By INSTRUMENT" },
                        { text: "Account Activity By DIRECT" },  // Blom ada
                        { text: "Account Activity By CONSIGNEE" },
                        { text: "Trial Balance Plain" },
                        { text: "Trial Balance Groups" },
                        { text: "Balance Sheet Plain" },
                        { text: "Income Statement Plain" },
                        { text: "Income Statement Groups" },
                        { text: "List Instrument By Account" },
                        { text: "List Department By Account" },
                        { text: "List Consignee By Account" },
                        { text: "Daily Transaction Bonds" },
                        //{ text: "List Agent By Account" }, // Blom ada
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


                    if (this.text() == 'Journal Voucher') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramAgent").show();
                        $("#paramConsignee").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramOffice").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity Plain') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By DEPT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramDepartment").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Account Activity By INSTRUMENT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramInstrument").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Account Activity By DIRECT') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramAgent").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Account Activity By CONSIGNEE') {
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramConsignee").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Trial Balance Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Trial Balance Groups') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Balance Sheet Plain') {
                        $("#paramDateTo").show();
                        $("#paramData").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Plain') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'Income Statement Groups') {
                        $("#paramData").show();
                        $("#paramDateTo").show();
                        $("#paramDateFrom").show();
                        $("#paramStatus").show();
                        $("#paramGroups").show();
                        $("#paramPageBreak").show();

                    }
                    else if (this.text() == 'List Instrument By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramInstrument").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }
                    else if (this.text() == 'List Department By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramDepartment").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'List Consignee By Account') {
                        $("#paramDateFrom").show();
                        $("#paramDateTo").show();
                        $("#paramConsignee").show();
                        $("#paramData").show();
                        $("#paramAccount").show();
                        $("#paramStatus").show();
                        $("#paramPageBreak").show();
                    }

                    else if (this.text() == 'Daily Transaction Bonds') {
                        $("#trValueDateFrom").show();
                        $("#trValueDateTo").show();
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



        // ComboBox Period
        var _defaultPeriodPK;

        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                _defaultPeriodPK = data;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    template: "<table><tr><td width='70px'>${ID}</td></tr></table>",
                    dataSource: data,
                    change: OnChangePeriodPK,
                    enabled: false,
                    value: _defaultPeriodPK
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
        

        //Row
        $.ajax({
            url: window.location.origin + "/Radsoft/Reports/GetRowOSKMonthlyMappingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#RowFrom").kendoComboBox({
                    dataValueField: "Row",
                    dataTextField: "Row",
                    dataSource: data,
                    change: OnChangeRowFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#RowTo").kendoComboBox({
                    dataValueField: "Row",
                    dataTextField: "Row",
                    dataSource: data,
                    change: OnChangeRowTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeRowFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#RowTo").data("kendoComboBox").value($("#RowFrom").data("kendoComboBox").value());
        }

        function OnChangeRowTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //ACCOUNT BY
        $("#AccountBy").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Piutang", value: 1 },
                { text: "Revenue", value: 2 },

            ],
            filter: "contains",
            suggest: true
        });

        //ACCOUNT
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnlyRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountFrom").kendoMultiSelect({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    autoClose : false,
                    dataSource: data
                });
                $("#AccountFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
         

        //OFFICE
        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#OfficeFrom").kendoMultiSelect({
                    dataValueField: "OfficePK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#OfficeFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //DEPARTMENT
        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentFrom").kendoMultiSelect({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#DepartmentFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //DIRECT
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

        //CONSIGNEE
        $.ajax({
            url: window.location.origin + "/Radsoft/Consignee/GetConsigneeComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ConsigneeFrom").kendoMultiSelect({
                    dataValueField: "ConsigneePK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#ConsigneeFrom").data("kendoMultiSelect").value("0");
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
                    filter: "contains",
                    dataSource: data
                });
                $("#InstrumentFrom").data("kendoMultiSelect").value("0");
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
                $("#CounterpartFrom").kendoMultiSelect({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#CounterpartFrom").data("kendoMultiSelect").value("0");
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


        //1.combo box ParamData Rpt Accounting//
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



        win = $("#WinAccountingRpt").kendoWindow({
            height: 700,
            title: "* Accounting Report",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }

    $("#BtnDownload").click(function () {

        var ArrayAccountFrom = $("#AccountFrom").data("kendoMultiSelect").value();
        var stringAccountFrom = '';
        for (var i in ArrayAccountFrom) {
            stringAccountFrom = stringAccountFrom + ArrayAccountFrom[i] + ',';

        }
        stringAccountFrom = stringAccountFrom.substring(0, stringAccountFrom.length - 1)

        var ArrayOfficeFrom = $("#OfficeFrom").data("kendoMultiSelect").value();
        var stringOfficeFrom = '';
        for (var i in ArrayOfficeFrom) {
            stringOfficeFrom = stringOfficeFrom + ArrayOfficeFrom[i] + ',';

        }
        stringOfficeFrom = stringOfficeFrom.substring(0, stringOfficeFrom.length - 1)

        var ArrayDepartmentFrom = $("#DepartmentFrom").data("kendoMultiSelect").value();
        var stringDepartmentFrom = '';

        if ($('#Name').val() == 'RHB OSK IS By CostCenter') {
            if (ArrayDepartmentFrom.length > 1) {
                alertify.alert('Can Only Choose 1 Cost Center for this report');
                return;
            }

            if (ArrayDepartmentFrom[0] == '0') {
                alertify.alert('Cannot choose all for this report');
                return;
            }
        }

        for (var i in ArrayDepartmentFrom) {
            stringDepartmentFrom = stringDepartmentFrom + ArrayDepartmentFrom[i] + ',';

        }
        stringDepartmentFrom = stringDepartmentFrom.substring(0, stringDepartmentFrom.length - 1)



        var ArrayAgentFrom = $("#AgentFrom").data("kendoMultiSelect").value();
        var stringAgentFrom = '';
        for (var i in ArrayAgentFrom) {
            stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

        }
        stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

        var ArrayConsigneeFrom = $("#ConsigneeFrom").data("kendoMultiSelect").value();
        var stringConsigneeFrom = '';
        for (var i in ArrayConsigneeFrom) {
            stringConsigneeFrom = stringConsigneeFrom + ArrayConsigneeFrom[i] + ',';

        }
        stringConsigneeFrom = stringConsigneeFrom.substring(0, stringConsigneeFrom.length - 1)

        var ArrayInstrumentFrom = $("#InstrumentFrom").data("kendoMultiSelect").value();
        var stringInstrumentFrom = '';
        for (var i in ArrayInstrumentFrom) {
            stringInstrumentFrom = stringInstrumentFrom + ArrayInstrumentFrom[i] + ',';

        }
        stringInstrumentFrom = stringInstrumentFrom.substring(0, stringInstrumentFrom.length - 1)

        var ArrayCounterpartFrom = $("#CounterpartFrom").data("kendoMultiSelect").value();
        var stringCounterpartFrom = '';
        for (var i in ArrayCounterpartFrom) {
            stringCounterpartFrom = stringCounterpartFrom + ArrayCounterpartFrom[i] + ',';

        }
        stringCounterpartFrom = stringCounterpartFrom.substring(0, stringCounterpartFrom.length - 1)


        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "AccountingReport_O";
                //if ($('#Name').val() == "Journal Voucher") {
                //    _permission = "JournalVoucher_O";
                //} else if ($('#Name').val() == "Account Activity") {
                //    _permission = "AccountActivity_O";
                //}
                var AccountingRpt = {
                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    AccountFrom: stringAccountFrom,
                    OfficeFrom: stringOfficeFrom,
                    DepartmentFrom: stringDepartmentFrom,
                    AgentFrom: stringAgentFrom,
                    ConsigneeFrom: stringConsigneeFrom,
                    InstrumentFrom: stringInstrumentFrom,
                    CounterpartFrom: stringCounterpartFrom,
                    FundFrom: stringFundFrom,
                    PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                    Status: $("#Status").data("kendoComboBox").value(),
                    ParamData: $("#ParamData").data("kendoComboBox").value(),
                    Profile: $("#Profile").data("kendoComboBox").text(),
                    Groups: $("#Groups").data("kendoComboBox").text(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    RowFrom: $("#RowFrom").data("kendoComboBox").text(),
                    RowTo: $("#RowTo").data("kendoComboBox").text(),
                    Period: $("#PeriodPK").data("kendoComboBox").text(),
                    PeriodPK: $("#PeriodPK").data("kendoComboBox").value(),
                    AccountBy: $('#AccountBy').val(),
                    DecimalPlaces: GlobDecimalPlaces,

                };


                var _url;
                if ($('#Name').val() == "Laporan Laba Rugi") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanLabaRugi/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                }
                else if ($('#Name').val() == "Laporan Laba Rugi Comparison") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanLabaRugiComparison/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                }
                else if ($('#Name').val() == "Balance Sheet") {
                    if (_GlobClientCode == "06") {
                        _url = window.location.origin + "/Radsoft/Reports/AccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission
                    }
                    else {
                        _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanBalanceSheet/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                    }

                }
                else if ($('#Name').val() == "Laporan Arus Kas") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanArusKas/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                }
                else if ($('#Name').val() == "Laporan Keuangan" || $('#Name').val() == "Laporan Posisi Keuangan") {
                    if (_GlobClientCode == "01" || _GlobClientCode == "19") {
                        _url = window.location.origin + "/Radsoft/Reports/AccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission
                    }
                    else {
                        _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanKeuangan/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                    }
                }

                else if ($('#Name').val() == "Compare Income Statement") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateIncomeStatement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                }
                else if ($('#Name').val() == "Financial Statement") {
                    _url = window.location.origin + "/Radsoft/Reports/GenerateLaporanFinancialStatement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
                }
                else {
                    _url = window.location.origin + "/Radsoft/Reports/AccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission
                }

                if ($('#Name').val() == "Income Statement Groups" && $("#Groups").data("kendoComboBox").text() == "ALL") {
                    $.unblockUI();
                    alertify.alert("Please Use Income Statement Plain");
                }
                else {
                    $.ajax({
                        url: _url,
                        type: 'POST',
                        data: JSON.stringify(AccountingRpt),
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
});