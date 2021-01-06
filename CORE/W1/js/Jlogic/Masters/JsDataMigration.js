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

    if (_GlobClientCode == "20") {
        $("#LblBtnAllKycAperd").show();
        $("#LblDownloadKYCAperd").show();

    }
    else {
        $("#LblBtnAllKycAperd").hide();
        $("#LblDownloadKYCAperd").hide();
    }

    function initButton() {
        $("#BtnImportTransaction").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportFundClient").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBankFundClient").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportFund").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportClientInd").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportClientIns").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportClientBank").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportDI").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportSubsRedemp").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportSwitch").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBalance").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        //$("#ScheduleOfCashDividend").kendoButton({
        //    imageUrl: "../../Images/Icon/IcImport.png"
        //});

        //APERD

        $("#BtnAPERDImportClientInd").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportClientIns").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportDI").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportSubsRedemp").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportSwitch").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportBalance").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnAPERDImportSubsRedempExcel").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnScheduleOfCashDividend").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnAllKYCAperd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnOkAllKYCAperd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelAllKYCAperd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
    }


    function initWindow() {

        win = $("#WinDataMigration").kendoWindow({
            height: 450,
            title: "DataMigration Detail",
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


        WinAllKYCAperd = $("#WinAllKYCAperd").kendoWindow({
            height: 300,
            title: "* KYC APERD By",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinAllKYCAperdClose
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinDataMigration").kendoValidator().data("kendoValidator");

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

    function getDataSource(_url) {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: _url,
                                     dataType: "json"
                                 }
                         },
                 batch: true,
                 cache: false,
                 error: function (e) {
                     alert(e.errorThrown + " - " + e.xhr.responseText);
                     this.cancelChanges();
                 },
                 pageSize: 100,
                 schema: {
                     model: {
                         fields: {
                             DataMigrationPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

  

    $("#BtnImportClientInd").click(function () {
        document.getElementById("ImportClientInd").click();
    });


    $("#ImportClientInd").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportClientInd").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDClientInd", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportClientInd").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportClientInd").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportClientInd").val("");
        }
    });


    $("#BtnImportClientIns").click(function () {
        document.getElementById("ImportClientIns").click();
    });


    $("#ImportClientIns").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportClientIns").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDClientIns", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportClientIns").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportClientIns").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportClientIns").val("");
        }
    });

    $("#BtnImportClientBank").click(function () {
        document.getElementById("ImportClientBank").click();
    });
    $("#ImportClientBank").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportClientBank").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDClientBank", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportClientBank").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportClientBank").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportClientBank").val("");
        }
    });



    


    $("#BtnImportSubsRedemp").click(function () {
        document.getElementById("ImportSubsRedemp").click();
    });

    $("#ImportSubsRedemp").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportSubsRedemp").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDTrxSubRed", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportSubsRedemp").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportSubsRedemp").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportSubsRedemp").val("");
        }
    });


    $("#BtnImportSwitch").click(function () {
        document.getElementById("ImportSwitch").click();
    });

    $("#ImportSwitch").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportSwitch").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDTrxSwitching", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportSwitch").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportSwitch").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportSwitch").val("");
        }
    });


    $("#BtnImportBalance").click(function () {
        document.getElementById("ImportBalance").click();
    });


    $("#ImportBalance").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportBalance").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDBalance", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportBalance").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportBalance").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportBalance").val("");
        }
    });



    $("#BtnImportDI").click(function () {
        document.getElementById("ImportDI").click();
    });

    $("#ImportDI").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ImportDI").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("NonAPERDDI", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ImportDI").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ImportDI").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ImportDI").val("");
        }
    });


    // APERD

    $("#BtnAPERDImportClientInd").click(function () {
        document.getElementById("APERDImportClientInd").click();
    });


    $("#APERDImportClientInd").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportClientInd").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDClientInd", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportClientInd").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportClientInd").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportClientInd").val("");
        }
    });


    $("#BtnAPERDImportClientIns").click(function () {
        document.getElementById("APERDImportClientIns").click();
    });


    $("#APERDImportClientIns").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportClientIns").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDClientIns", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportClientIns").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportClientIns").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportClientIns").val("");
        }
    });



    $("#BtnAPERDImportBalance").click(function () {
        document.getElementById("APERDImportBalance").click();
    });


    $("#APERDImportBalance").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportBalance").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDBalance", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportBalance").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportBalance").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportBalance").val("");
        }
    });


    $("#BtnAPERDImportSubsRedemp").click(function () {
        document.getElementById("APERDImportSubsRedemp").click();
    });

    $("#APERDImportSubsRedemp").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportSubsRedemp").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDTrxSubRed", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportSubsRedemp").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportSubsRedemp").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportSubsRedemp").val("");
        }
    });


    $("#BtnAPERDImportSwitch").click(function () {
        document.getElementById("APERDImportSwitch").click();
    });

    $("#APERDImportSwitch").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportSwitch").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDTrxSwitching", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportSwitch").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportSwitch").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportSwitch").val("");
        }
    });

    $("#BtnAPERDImportDI").click(function () {
        document.getElementById("APERDImportDI").click();
    });

    $("#APERDImportDI").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportDI").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDDI", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportDI").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportDI").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportDI").val("");
        }
    });

    $("#BtnScheduleOfCashDividend").click(function () {
        document.getElementById("ScheduleOfCashDividend").click();
    });

    $("#ScheduleOfCashDividend").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#ScheduleOfCashDividend").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("ScheduleOfCashDividend", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#ScheduleOfCashDividend").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#ScheduleOfCashDividend").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#ScheduleOfCashDividend").val("");
        }
    });


    $("#BtnAPERDImportSubsRedempExcel").click(function () {
        document.getElementById("APERDImportSubsRedempExcel").click();
    });

    $("#APERDImportSubsRedempExcel").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#APERDImportSubsRedempExcel").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;
        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("APERDSubsRedempExcel", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DataMigration_O/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#APERDImportSubsRedempExcel").val("");
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#APERDImportSubsRedempExcel").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#APERDImportSubsRedempExcel").val("");
        }
    });


    $("#BtnAllKYCAperd").click(function () {
        showWinAllKYCAperd();
    });

    function showWinAllKYCAperd(e) {
        $("#LblParamKYCAperdBitIsMature").hide();

        $("#KYCAperdBy").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "Individu", value: '1' },
                { text: "Institusi", value: '2' },

            ],
            filter: "contains",
            suggest: true,
            change: OnChangeKYCAperdBy,
            index: 0
        });


        function OnChangeKYCAperdBy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }



        WinAllKYCAperd.center();
        WinAllKYCAperd.open();

    }

    $("#BtnOkAllKYCAperd").click(function () {


        alertify.confirm("Are you sure want to Download KYCAperd ?", function (e) {
            if (e) {
                if ($("#KYCAperdBy").val() == 1) {
                    console.log($("#KYCAperdBy").val())

                    var KYCAperd = {

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/DataMigration/KYCAperdInd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(KYCAperd),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }

                    });



                }
                else if ($("#KYCAperdBy").val() == 2) {


                    var KYCAperd = {

                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/DataMigration/KYCAperdIns/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(KYCAperd),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var newwindow = window.open(data, '_blank');

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }

                    });
                }




            }
        });

    });

    $("#BtnCancelAllKYCAperd").click(function () {

        alertify.confirm("Are you sure want to cancel KYCAperd?", function (e) {
            if (e) {
                WinAllKYCAperd.close();
                alertify.alert("Cancel KYCAperd");
            }
        });
    });

    function onWinAllKYCAperdClose() {
        $("#Message").val("");
        $("#KYCAperdBy").val(1);
    }


});
