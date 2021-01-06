$(document).ready(function () {
    document.title = 'FORM PTP Upload';
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
        $("#BtnPTPImportEquitySI").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnPTPImportTDCB").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnPTPImportTDACB").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportFITax").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportFICb").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

    }

    function initWindow() {

        win = $("#WinPTPUpload").kendoWindow({
            height: 450,
            title: "PTPUpload Detail",
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

    var GlobValidator = $("#WinPTPUpload").kendoValidator().data("kendoValidator");

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

    //function getDataSource(_url) {
    //    return new kendo.data.DataSource(
    //         {
    //             transport:
    //                     {
    //                         read:
    //                             {
    //                                 url: _url,
    //                                 dataType: "json"
    //                             }
    //                     },
    //             batch: true,
    //             cache: false,
    //             error: function (e) {
    //                 alert(e.errorThrown + " - " + e.xhr.responseText);
    //                 this.cancelChanges();
    //             },
    //             pageSize: 100,
    //             schema: {
    //                 model: {
    //                     fields: {
    //                         PTPUploadPK: { type: "number" },
    //                         HistoryPK: { type: "number" },
    //                         Status: { type: "number" },
    //                         StatusDesc: { type: "string" },
    //                         Notes: { type: "string" },
    //                         ID: { type: "string" },
    //                         Name: { type: "string" },
    //                         Type: { type: "string" },
    //                         TypeDesc: { type: "string" },
    //                         EntryUsersID: { type: "string" },
    //                         EntryTime: { type: "date" },
    //                         UpdateUsersID: { type: "string" },
    //                         UpdateTime: { type: "date" },
    //                         ApprovedUsersID: { type: "string" },
    //                         ApprovedTime: { type: "date" },
    //                         VoidUsersID: { type: "string" },
    //                         VoidTime: { type: "date" },
    //                         LastUpdate: { type: "date" },
    //                         Timestamp: { type: "string" }
    //                     }
    //                 }
    //             }
    //         });
    //}


    $("#BtnPTPImportEquitySI").click(function () {
        document.getElementById("PTPImportEquitySI").click();
    });

    $("#PTPImportEquitySI").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#PTPImportEquitySI").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PTPUploadEquitySI", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PTPUpload_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#PTPImportEquitySI").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#PTPImportEquitySI").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#PTPImportEquitySI").val("");
        }
    });


    $("#BtnPTPImportTDCB").click(function () {
        document.getElementById("PTPImportTDCB").click();
    });

    $("#PTPImportTDCB").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#PTPImportTDCB").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PTPUploadTDCB", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PTPUpload_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#PTPImportTDCB").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#PTPImportTDCB").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#PTPImportTDCB").val("");
        }
    });


    $("#BtnPTPImportTDACB").click(function () {
        document.getElementById("PTPImportTDACB").click();
    });

    $("#PTPImportTDACB").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#PTPImportTDACB").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PTPUploadTDACB", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PTPUpload_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#PTPImportTDACB").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#PTPImportTDACB").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#PTPImportTDACB").val("");
        }
    });


    $("#BtnImportFITax").click(function () {
        document.getElementById("ImportFITax").click();
    });

    $("#ImportFITax").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportFITax").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PTPUploadFITax", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PTPUpload_O",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportFITax").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportFITax").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportFITax").val("");
        }
    });


    $("#BtnImportFICb").click(function () {
        document.getElementById("ImportFICb").click();
    });

    $("#ImportFICb").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportFICb").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("PTPUploadFICb", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PTPUpload_O",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportFICb").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportFICb").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportFICb").val("");
        }
    });


});