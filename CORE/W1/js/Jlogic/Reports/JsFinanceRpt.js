$(document).ready(function () {
    var win;
    var GlobDecimalPlaces;
    
   
    var GlobValidator = $("#WinFinanceRpt").kendoValidator().data("kendoValidator");
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


        $("#paramBank").hide();
        $("#paramAccount").hide();
        $("#paramCashierType").hide();
        $("#paramReference").hide();
        $("#paramCurrency").hide();
        $("#paramStatus").hide();
        $("#paramData").hide();
        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
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
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateTo
        });
        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                if (_GlobClientCode != "01") {
                    $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
                }
                
            }
            $("#ReferenceFrom").data("kendoMultiSelect").value("0");
            GetCashierReference();
        }

        function OnChangeValueDateTo() {
            $("#ReferenceFrom").data("kendoMultiSelect").value("0");
      
            GetCashierReference();
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
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '02') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '03') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '04') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }
                //MNC
            else if (_GlobClientCode == '05') {
                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Voucher Pembayaran" },
                       { text: "Voucher Penerimaan" },
                       { text: "Accounting Journal Listing" },
                       { text: "Budget Summary" },
                       { text: "Profit And Loss" },

                       //custom
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" },
                       { text: "Annual Budget Report" },
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Voucher Pembayaran') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Voucher Penerimaan') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Accounting Journal Listing') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                    else if (this.text() == 'Annual Budget Report') {


                    }

                    else if (this.text() == 'Budget Summary') {


                    }

                    else if (this.text() == 'Ledger Budget') {


                    }

                    else if (this.text() == 'Profit And Loss') {


                    }

                    else if (this.text() == 'Trial Balance By COA by Cost Center') {


                    }

                    else if (this.text() == 'Trial Balance By COA') {


                    }
                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }
                }
            }

            else if (_GlobClientCode == '06') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

            else if (_GlobClientCode == '07') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }
            //RHB
            else if (_GlobClientCode == '08') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" }, 
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

                //EMCO
            else if (_GlobClientCode == '09') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" },
                       //custom
                       { text: "LAPORAN BUKU KAS ATAU BANK HARIAN" },
                       { text: "BANK VOUCHER EMCO" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }
                    else if (this.text() == 'LAPORAN BUKU KAS ATAU BANK HARIAN') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }
                    else if (this.text() == 'BANK VOUCHER EMCO') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }


                }
            }

            else if (_GlobClientCode == '17') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }

            else {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Cashier Voucher" },
                       { text: "Bank Reconcile" },
                       { text: "Cashier and Journal Activity" }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangeName,
                    value: setCmbName()
                });
                function setCmbName() {
                    return "";
                }
                function OnChangeName() {
                    HideParameter();
                    if (this.value() && this.selectedIndex == -1) {
                        var dt = this.dataSource._data[0];
                        this.text('');
                    }

                    if (this.text() == 'Cashier Voucher') {
                        $("#paramStatus").show();
                        $("#paramReference").show();
                        $("#paramCashierType").show();
                    }


                    else if (this.text() == 'Cashier and Journal Activity') {
                        $("#paramStatus").show();
                        $("#paramAccount").show();
                    }

                    else if (this.text() == 'Bank Reconcile') {
                        $("#paramBank").show();
                        $("#paramStatus").show();
                    }

                }
            }
        }


        $("#CashierType").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "IN" },
               { text: "OUT" },
               { text: "ALL" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCashierType,
            value: setCmbCashierType()
        });
        function OnChangeCashierType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            GetCashierReference();
        }
        function setCmbCashierType() {
                return "ALL";        
        }


        $("#Status").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "POSTED ONLY", value: 1 },
               { text: "REVISED ONLY", value: 2 },
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


        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankFrom").kendoMultiSelect({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data
                });
                $("#BankFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $("#PageBreak").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text:"TRUE",value: true },
               { text:"FALSE",value: false },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangePageBreak,
            value: setCmbPageBreak()
        });

        function setCmbPageBreak() {
            return "TRUE";
        }

        function OnChangePageBreak() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

     

        //ACCOUNT
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnlyExcludeCashRefRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccountFrom").kendoMultiSelect({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    autoClose: false,
                    dataSource: data
                });
                $("#AccountFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        //Reference
        $.ajax({
            url: window.location.origin + "/Radsoft/Cashier/GetReferenceComboByCashierTypeRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashierType").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReferenceFrom").kendoMultiSelect({
                    dataValueField: "Reference",
                    dataTextField: "Reference",
                    filter: "contains",
                    dataSource: data
                });
                $("#ReferenceFrom").data("kendoMultiSelect").value("0");

             
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function GetCashierReference() {
            //Reference
          
            $.ajax({
                url: window.location.origin + "/Radsoft/Cashier/GetReferenceComboByCashierTypeRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#CashierType").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#ReferenceFrom").data("kendoMultiSelect").setDataSource(data);
                    $("#ReferenceFrom").data("kendoMultiSelect").value("0");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
          
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
                    dataSource: data
                });
                $("#CurrencyFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


     
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
                    index: 2

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


        win = $("#WinFinanceRpt").kendoWindow({
            height: 600,
            title: "* Finance Report",
            visible: false,
            width: 1000,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }
    $("#BtnDownload").click(function () {
        
        if (validateData() == 1) {
            var ArrayBankFrom = $("#BankFrom").data("kendoMultiSelect").value();
            var stringBankFrom = '';
            for (var i in ArrayBankFrom) {
                stringBankFrom = stringBankFrom + ArrayBankFrom[i] + ',';
            }
            stringBankFrom = stringBankFrom.substring(0, stringBankFrom.length - 1)

            var ArrayAccountFrom = $("#AccountFrom").data("kendoMultiSelect").value();
            var stringAccountFrom = '';
            for (var i in ArrayAccountFrom) {
                stringAccountFrom = stringAccountFrom + ArrayAccountFrom[i] + ',';

            }
            stringAccountFrom = stringAccountFrom.substring(0, stringAccountFrom.length - 1)

            var ArrayCurrencyFrom = $("#CurrencyFrom").data("kendoMultiSelect").value();
            var stringCurrencyFrom = '';
            for (var i in ArrayCurrencyFrom) {
                stringCurrencyFrom = stringCurrencyFrom + ArrayCurrencyFrom[i] + ',';

            }
            stringCurrencyFrom = stringCurrencyFrom.substring(0, stringCurrencyFrom.length - 1)

            var ArrayReferenceFrom = $("#ReferenceFrom").data("kendoMultiSelect").value();
            var stringReferenceFrom = '\'';
            for (var i in ArrayReferenceFrom) {
                stringReferenceFrom = stringReferenceFrom + ArrayReferenceFrom[i] + '\',\'';
            }
            stringReferenceFrom = stringReferenceFrom.substring(0, stringReferenceFrom.length - 2)

            alertify.confirm("Are you sure want to Download data ?", function (e) {
                if (e) {
                    $.blockUI({});
                    var FinanceRpt = {
                        ReportName: $('#Name').val(),
                        ValueDateFrom: $('#ValueDateFrom').val(),
                        ValueDateTo: $('#ValueDateTo').val(),
                        BankFrom: stringBankFrom,
                        AccountFrom: stringAccountFrom,
                        CashierType: $("#CashierType").data("kendoComboBox").text(),
                        ReferenceFrom: stringReferenceFrom,
                        CurrencyFrom: stringCurrencyFrom,
                        DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                        Status: $("#Status").data("kendoComboBox").value(),
                        ParamData: $("#ParamData").data("kendoComboBox").value(),
                        PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                        DecimalPlaces: GlobDecimalPlaces,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Reports/FinanceReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PaymentVoucher_O",
                        type: 'POST',
                        data: JSON.stringify(FinanceRpt),
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
});