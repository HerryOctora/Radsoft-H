$(document).ready(function () {
    document.title = 'FORM UnitAllocation';
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


        WinUploadUnitAllocation = $("#WinUploadUnitAllocation").kendoWindow({
            height: 300,
            title: "* UPLOAD UNIT ALLOCATION",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinUploadUnitAllocation.center();
        WinUploadUnitAllocation.open();

    }

    function initButton() {
        $("#BtnImportUnitAllocation").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/UploadUnitAllocation/GetLastUploadInformation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblSpan").text(kendo.toString(data));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }


    var GlobValidator = $("#WinUnitAllocation").kendoValidator().data("kendoValidator");

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


    $("#BtnImportUnitAllocation").click(function () {
        document.getElementById("ImportUnitAllocation").click();
    });


    $("#ImportUnitAllocation").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportUnitAllocation").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ImportUnitAllocation", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UploadUnitAllocation_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportUnitAllocation").val("");

                    $.ajax({
                        url: window.location.origin + "/Radsoft/UploadUnitAllocation/GetLastUploadInformation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#lblSpan").text(kendo.toString(data));
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportUnitAllocation").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportUnitAllocation").val("");
        }
    });



});
