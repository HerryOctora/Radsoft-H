$(document).ready(function () {
    document.title = 'FORM Data Migration';
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
        $("#BtnAddFundClient").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAddTrx").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnRegistrationStatus").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnTransactionStatus").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnUpdateNav").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        
        
    }


    function initWindow() {

        win = $("#WinCAMInterface").kendoWindow({
            height: 450,
            title: "CAM Interface Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

        }).data("kendoWindow");


        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateTo
        });




        function OnChangeDateFrom() {

            var currentDate = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }

        }
        function OnChangeDateTo() {

            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }


        }


    }



    var GlobValidator = $("#WinCAMInterface").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
    }

    $("#BtnAddFundClient").click(function () {

        alertify.confirm("Are you sure want to Add Fund Client?", function (e) {
            if (e) {

                window.open("http://192.168.150.35:89/Radsoft/RDO/AddProfile_21/RDOAPI", '', '');
               
            }
        });
    });

    $("#BtnAddTrx").click(function () {

        alertify.confirm("Are you sure want to Add Transaction?", function (e) {
            if (e) {
                window.open("http://192.168.150.35:89/Radsoft/RDO/AddTransaction_21/RDOAPI/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "yyyyMMdd"), '', '');
            
            }
        });
    });
    


    $("#BtnRegistrationStatus").click(function () {

        alertify.confirm("Are you sure want to Update KYC Status?", function (e) {
            if (e) {
                window.open("http://192.168.150.35:89/Radsoft/RDO/RegistrationStatus_21/RDOAPI", '', '');
            }
        });
    });

    $("#BtnTransactionStatus").click(function () {

        alertify.confirm("Are you sure want to Check Transaction Status?", function (e) {
            if (e) {
                window.open("http://192.168.150.35:89/Radsoft/RDO/TransactionStatus_21/RDOAPI/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yyyy"), '', '');
            }
        });
    });
    


    $("#BtnUpdateNav").click(function () {

        alertify.confirm("Are you sure want to Update Nav Date From To?", function (e) {
            if (e) {
                if (e) {

                    window.open("http://192.168.150.35:89/Radsoft/RDO/UpdateNAV_21/RDOAPI/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "yyyyMMdd") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "yyyyMMdd"), '', '');

                }
            }
        });
    });

});
