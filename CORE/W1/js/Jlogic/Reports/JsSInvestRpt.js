$(document).ready(function () {
    var win;
    
      
    var GlobValidator = $("#WinSInvestRpt").kendoValidator().data("kendoValidator");
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
        $("#ValueDate").kendoDatePicker({
            value: new Date(),
        });

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        //$("#ValueDateFrom").kendoDatePicker({
        //    value: new Date(),
        //    change: OnChangeValueDateFrom
        //});

        //$("#ValueDateTo").kendoDatePicker({
        //    value: new Date()
        //});

        //function OnChangeValueDateFrom() {
        //    if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
        //        $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
        //    }
        //        $("#UnitRegistryPKFrom").data("kendoComboBox").text("");
        //        $("#UnitRegistryPKTo").data("kendoComboBox").text("");
        //        getPKFromUnitRegistry();
        //}

        $("#Name").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "S-Invest KYC" },
               { text: "S-Invest Unit Registry" },
               { text: "Old KYC" },
               { text: "Selling Agent Fund List" },
               
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
            if (this.text() == 'S-Invest KYC') {
                ClearAttribute();
                $("#LblParamCategory").show();
                $("#LblParamDownload").show();
                $("#LblParamDownloadMode").show();

            }
            if (this.text() == 'Old KYC') {
                ClearAttribute();
                $("#LblParamCategory").show();
                $("#LblParamDownload").show();
                $("#LblParamDownloadMode").show();

            }
            if (this.text() == 'Selling Agent Fund List') {
                ClearAttribute();
                $("#LblParamDownload").show();
                $("#LblParamDownloadMode").show();
            }


            if (this.text() == 'S-Invest Unit Registry') {
                ClearAttribute();
                $("#LblParamValueDate").show();
                $("#LblParamUnitRegistry").show();
            }   

        }

        $("#ParamUnitRegistry").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "3.1.1.1-01.SUBS_REDM_SWTC Order Upload_SUBS REDM Order" },
               { text: "3.1.1.1-02.SUBS_REDM_SWTC Order Fee Upload_SUBS REDM Order Fee" },
               { text: "3.1.1.1-03.SUBS_REDM_SWTC Order Inquiry_SUBS REDM Order_IM" },
               { text: "3.1.1.1-04.SUBS_REDM_SWTC Order Inquiry(Summary)_SUBS REDM Order (Summary by fund)" },
               { text: "3.1.1.1-04.SUBS_REDM_SWTC Order Inquiry(Summary)_SUBS REDM Order (Summary by SA_fund)" },
               { text: "3.1.5.1-01.Unit Allocation Upload_Fund Unit Allocation" },
               
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeParamUnitRegistry,
            index: 0
        });

        function OnChangeParamUnitRegistry() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if (this.text() == '3.1.1.1-01.SUBS_REDM_SWTC Order Upload_SUBS REDM Order' ||
               this.text() == '3.1.1.1-02.SUBS_REDM_SWTC Order Fee Upload_SUBS REDM Order Fee' ||
               this.text() == '3.1.1.1-03.SUBS_REDM_SWTC Order Inquiry_SUBS REDM Order_IM' ||
               this.text() == '3.1.1.1-04.SUBS_REDM_SWTC Order Inquiry(Summary)_SUBS REDM Order (Summary by fund)' ||
               this.text() == '3.1.1.1-04.SUBS_REDM_SWTC Order Inquiry(Summary)_SUBS REDM Order (Summary by SA_fund)' ||
               this.text() == '3.1.5.1-01.Unit Allocation Upload_Fund Unit Allocation'
               ) {
                ClearAttribute();
                $("#LblParamValueDate").show();
                $("#LblParamUnitRegistry").show();

            }

        }

        $("#ParamCategory").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "Individual", value: 1 },
               { text: "Institutional", value: 2 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeParamCategory,
            index: 0
        });

        function OnChangeParamCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        win = $("#WinSInvestRpt").kendoWindow({
            height: 200,
            title: "* S-Invest Report",
            visible: false,
            width: 700,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            }
        }).data("kendoWindow");

        win.center();
        win.open();
    }

    $("#BtnDownload").click(function () {
        
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {

                var SInvestRpt = {
                    ReportName: $('#Name').val(),
                    ParamUnitRegistry: $("#ParamUnitRegistry").data("kendoComboBox").text(),
                    ValueDate: $('#ValueDate').val(),
                    ParamCategory: $("#ParamCategory").data("kendoComboBox").text(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/SInvestReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
                    type: 'POST',
                    data: JSON.stringify(SInvestRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Report");
            }
        });
    });


    function ClearAttribute() {
        $("#LblParamCategory").hide();
        $("#LblParamValueDate").hide();
        $("#LblParamDownload").hide();
        $("#LblParamDownloadMode").hide();
        $("#LblParamUnitRegistry").hide();
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
});