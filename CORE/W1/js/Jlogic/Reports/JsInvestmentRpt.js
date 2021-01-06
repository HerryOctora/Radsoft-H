$(document).ready(function () {
    var win;


    var GlobValidator = $("#WinInvestmentRpt").kendoValidator().data("kendoValidator");
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
        //ClearAttribute();
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
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {

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

        // FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    //suggest: true,
                    //change: onChangeFundPK
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

        $("#ParamInsType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            change: OnChangeParamInsType,
            dataSource: [
                    { text: "EQUITY", value: "1" },
                    { text: "BOND", value: "2" },
                    { text: "DEPOSITO", value: "3" },
                    { text: "REKSADANA", value: "4" },
            ],

        });


        function OnChangeParamInsType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            //if ($("#ParamInsType").val() == 3) {
            //    $("#LblParamListingBitIsMature").show();
            //}
            //else {
            //    $("#LblParamListingBitIsMature").hide();
            //}
        }




        InitName();

        function InitName() {
            //Ascend
            if (_GlobClientCode == '03') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Daily Security Transaction Instruction" },

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

                    if (this.text() == 'Daily Security Transaction Instruction') {

                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }

                }
            }
            else if (_GlobClientCode == '11') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Settlement Instruction" },
                       //{ text: "LPTI" },
                       { text: "LPTI Saham" },
                       { text: "LPTI Bond" },
                       { text: "LPTI Deposito" },
                       { text: "LPTI Reksadana" },
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

                    if (this.text() == 'Settlement Instruction') {
                        $("#LblDate").show();
                        $("#LblParamFund").show();
                        $("#LblParamInsType").show();
                        $("#LblParamListingBitIsMature").show();
                        $("#LblSignature1").show();
                        $("#LblSignature2").show();
                        $("#LblSignature3").show();
                        $("#LblSignature4").show();

                    }
                    else if (this.text() == 'LPTI') {
                        $("#LblParamFund").show();
                        $("#LblDate").show();
                        $("#LblParamInsType").show();

                    }

                    else if (this.text() == 'LPTI Saham') {
                        $("#LblParamFund").show();
                        $("#LblDate").show();

                    }

                    else if (this.text() == 'LPTI Bond') {
                        $("#LblParamFund").show();
                        $("#LblDate").show();

                    }

                    else if (this.text() == 'LPTI Deposito') {
                        $("#LblParamFund").show();
                        $("#LblDate").show();

                    }
                    else if (this.text() == 'LPTI Reksadana') {
                        $("#LblParamFund").show();
                        $("#LblDate").show();

                    }

                }
            }
            else if (_GlobClientCode == '14') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Rebalancing Daily" }
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

                    if (this.text() == 'Rebalancing Daily') {
                        $("#LblDate").show();
                        $("#LblParamFund").show();
                        $("#LblIndex").show();
                    }

                }
            }
            else if (_GlobClientCode == '18') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                          { text: "Investment Listing" },
                          { text: "Dealing Listing" },
                          { text: "Settlement Listing" },
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

                    if (this.text() == 'Investment Listing' || this.text() == 'Dealing Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }
                    else if (this.text() == 'Settlement Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                        $("#LblParamInsType").show();
                    }

                }
            }
            else if (_GlobClientCode == '21') {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Broker Commission Report" },
                        { text: "Report Instrument By Sector And Index" },
                        { text: "Investment Listing" },
                        { text: "Dealing Listing" },
                        { text: "Settlement Listing" },
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



                    if (this.text() == 'Investment Rpt' || this.text() == 'Dealing Rpt' || this.text() == 'Settlement Rpt') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }

                    if (this.text() == 'Investment Listing' || this.text() == 'Dealing Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }
                    else if (this.text() == 'Settlement Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                        $("#LblParamInsType").show();
                    }

                    if (this.text() == 'Report Instrument By Sector And Index') {

                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }

                    else if (this.text() == 'Broker Commission Report') {
                        HideParameter();
                        $("#LblDateTo").show();
                        $("#LblDate").show();
                        $("#trParamData").show();
                        $("#trCounterpartFrom").show();
                        $("#LblParamFund").show();
                    }
                }
            }
            else {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                        { text: "Report Instrument By Sector And Index" },
                        { text: "Investment Listing" },
                        { text: "Dealing Listing" },
                        { text: "Settlement Listing" },

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

                    if (this.text() == 'Investment Rpt' || this.text() == 'Dealing Rpt' || this.text() == 'Settlement Rpt') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }

                    if (this.text() == 'Investment Listing' || this.text() == 'Dealing Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                    }
                    else if (this.text() == 'Settlement Listing') {
                        $("#LblDate").show();
                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
                        $("#LblParamInsType").show();
                    }

                    if (this.text() == 'Report Instrument By Sector And Index') {

                        $("#LblDateTo").show();
                        $("#LblParamFund").show();
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
               { text: "Yes", value: true },
               { text: "No", value: false },
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

        win = $("#WinInvestmentRpt").kendoWindow({
            height: 550,
            title: "* Investment Report",
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

        if ($('#Name').val() == 'Settlement Instruction' || $('#Name').val() == 'LPTI'
            || $('#Name').val() == 'Rebalancing Daily'
        ) {
            if (ArrayFundFrom.length > 1 || ArrayFundFrom[0] == "0") {
                alertify.alert("Report can only have one parameter Fund and cannot use ALL");
                return;
            }
        }


        if ($('#Name').val() == 'Settlement Listing') {
            if ($('#ParamInsType').val() == '') {
                alertify.alert("Ins.Type cannot Blank");
                return;
            }
        }




        //if (validateData() == 1) {
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "InvestmentRpt_O";
                var _permission1 = "FundAccountingReport_O";
                var InvestmentRpt = {

                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    ValueDateTo: $('#ValueDateTo').val(),
                    //FundPK: $('#FundPK').val(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    Message: $('#Message').val(),
                    NoSurat: $('#NoSurat').val(),
                    ParamInstType: $('#ParamInsType').val(),
                    BitIsMature: $('#ParamListingBitIsMature').is(":checked"),
                    FundFrom: stringFundFrom,
                    Signature1: $("#Signature1").data("kendoComboBox").value(),
                    Signature2: $("#Signature2").data("kendoComboBox").value(),
                    Signature3: $("#Signature3").data("kendoComboBox").value(),
                    Signature4: $("#Signature4").data("kendoComboBox").value(),
                    IndexPK: $("#Index").data("kendoComboBox").value(),
                    PageBreak: $("#PageBreak").data("kendoComboBox").value(),
                    CounterpartFrom: stringCounterpart,
                    ParamData: $("#ParamData").data("kendoComboBox").value(),
                };
                if ($('#Name').val() == "Broker Commission Report") {
                    _url = window.location.origin + "/Radsoft/Reports/FundAccountingReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission1;
                }
                else {
                    _url = window.location.origin + "/Radsoft/Reports/InvestmentReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _permission;
                }

                $.ajax({
                    url: _url,
                    type: 'POST',
                    data: JSON.stringify(InvestmentRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        console.log(data);
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
    });

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Report");
            }
        });
    });

    function HideParameter() {
        $("#LblDate").hide();
        $("#LblDateTo").hide();
        $("#LblParamFund").hide();
        $("#LblParamInsType").hide();
        $("#LblParamListingBitIsMature").hide();
        $("#LblSignature1").hide();
        $("#LblSignature2").hide();
        $("#LblSignature3").hide();
        $("#LblSignature4").hide();
        $("#LblNoSurat").hide();
        $("#LblIndex").hide();

    }


});