$(document).ready(function () {
    var win;
    
     
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
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK
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
            ],

        });


        function OnChangeParamInsType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            if ($("#ParamInsType").val() == 3) {
                $("#LblParamListingBitIsMature").show();
            }
            else {
                $("#LblParamListingBitIsMature").hide();
            }
        }

        InitName();

        function InitName() {
            //Ascend
             if (_GlobClientCode == '11')
                {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       { text: "Settlement Instruction" },
                       { text: "LPTI" },
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
                    }
                    else if (this.text() == 'LPTI') {
                        $("#LblFundFrom").show();
                        $("#LblClientFrom").show();
                        $("#LblDateFrom").show();
                        $("#LblDateTo").show();
                    }

                }
            }
            else  {

                $("#Name").kendoComboBox({
                    dataValueField: "text",
                    dataTextField: "text",
                    dataSource: [
                       
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


       
        win = $("#WinUnitRegistryRpt").kendoWindow({
            height: 550,
            title: "* Settlement Instructions Report",
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
        
        //if (validateData() == 1) {
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var SettlementInstructionRpt = {

                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#ValueDateFrom').val(),
                    FundPK: $('#FundPK').val(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                    Message: $('#Message').val(),
                    ParamInstType: $('#ParamInsType').val(),
                    BitIsMature: $('#ParamListingBitIsMature').is(":checked"),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/SettlementInstructionReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(SettlementInstructionRpt),
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
        $("#LblParamFund").hide();
        $("#LblParamInsType").hide();
        $("#LblParamListingBitIsMature").hide();
    }

   

});