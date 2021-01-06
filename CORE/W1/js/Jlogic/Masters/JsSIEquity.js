$(document).ready(function () {
    document.title = 'FORM SIEquity';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //1
    initButton();
    //2
    initWindow();

    


    function initWindow() {

        win = $("#WinSIEquity").kendoWindow({
            height: 450,
            title: "SIEquity Detail",
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



    }

    function initButton() {
        $("#BtnImportSIEquity").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportSIDeposito").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
      


    }


    var GlobValidator = $("#WinSIEquity").kendoValidator().data("kendoValidator");

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


    $("#BtnImportSIEquity").click(function () {
        document.getElementById("ImportSIEquity").click();
    });


    $("#ImportSIEquity").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportSIEquity").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("SIEquity", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SIEquity_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportSIEquity").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportSIEquity").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportSIEquity").val("");
        }
    });

    $("#BtnImportSIDeposito").click(function () {
        document.getElementById("ImportSIDeposito").click();
    });


    $("#ImportSIDeposito").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportSIDeposito").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("SIDeposito", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SIDeposito_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportSIDeposito").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportSIDeposito").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportSIDeposito").val("");
        }
    });

   

});
