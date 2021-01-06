$(document).ready(function () {
    document.title = 'FORM RETRIEVE FROM BRIDGE';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //1
    initButton();
    //2
    initWindow();


    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });


        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        //$("#BtnRetrieveAUM").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});

        //$("#BtnGenerateRevaluation").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});

        //$("#BtnRetrieveClosePriceReksadana").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});
        //$("#BtnRetrieveMFee").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});
    }



    function initWindow() {
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });
 

        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
        });


        function OnChangeDateFrom() {
            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
 
        }


        WinRetrieveFromBridge = $("#WinRetrieveFromBridge").kendoWindow({
            height: 150,
            title: "* RETRIEVE MANAGEMENT FEE",
            visible: false,
            width: 800,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinRetrieveFromBridge.center();
        WinRetrieveFromBridge.open();

    }


    $("#BtnPostingManagementFee").click(function () {
      
        alertify.confirm("Are you sure want to Posting Management Fee ?", function (e) {
            if (e) {
                $.blockUI({});
                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveFromBridge/ValidateCheckManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") 
                        {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RetrieveFromBridge/PostingManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#BitIncludeTax').is(":checked"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {

                                    alertify.alert(data)
                                    $.unblockUI();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });
                        }
                        else
                        {
                            alertify.alert(data)
                            $.unblockUI();
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });



            }
           
        });

    });


    $("#BtnVoidManagementFee").click(function () {
        alertify.confirm("Are you sure want to Void Management Fee ?", function (e) {
            if (e) {


                $.ajax({
                    url: window.location.origin + "/Radsoft/RetrieveFromBridge/VoidManagementFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

 
});
