$(document).ready(function () {
    var win;

    var GlobValidator = $("#WinUnitReconcile").kendoValidator().data("kendoValidator");
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
        $("#filterDate").show();
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateTo
        });


        function OnChangeDateFrom() {
            
            var currentDate = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            
            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }

        $("#BtnImportUnitReconcile").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportUnitReconcileTxt").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnAdjustmentUnitBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });
        $("#BtnImportUnitReconcileTxtAPERD").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });


        win = $("#WinUnitReconcile").kendoWindow({
            height: 300,
            title: "Unit Reconcile",
            visible: false,
            width: 450,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");
   
    }

    if (_GlobClientCode == "24") {
        $("#ImportTxtAPERD").show();
    }
    else {
        $("#ImportTxtAPERD").hide();
    }

    $("#BtnPostingBySelected").click(function (e) {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/UnitReconcile/ValidatePostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    alertify.alert("Difference <> 0, Posting Cancel");
                    return;
                }
                else {
                    alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
                        if (e) {
                            $.blockUI();
                            $.ajax({
                                url: window.location.origin + "/Radsoft/UnitReconcile/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $.unblockUI();
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }

                    });

                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    });

    $("#BtnAdjustmentUnitBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Adjustment Unit by Selected Data ?", function (e) {
            if (e) {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/UnitReconcile/AdjustmentUnitBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        alertify.alert(data);
                        refreshAfterAdjustment();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#UnitReconcilePK").val("");  
        $("#Description").val(""); 
        $("#AdjustmentUnit").val("");
       
    }

    $("#BtnImportUnitReconcile").click(function () {
        document.getElementById("FileImportUnitReconcile").click();
    });

    $("#FileImportUnitReconcile").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportUnitReconcile").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("UnitReconcile", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UnitReconcile_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportUnitReconcile").val("");
                    alertify.alert("Import Success");
                    GenerateGrid();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportUnitReconcile").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportUnitReconcile").val("");
        }
    });

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
                 pageSize: 1000,
                 schema: {
                     model: {
                         fields: {
                             UnitReconcilePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             Selected: { type: "boolean" },
                             ValueDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             UnitSystem: { type: "number" },
                             UnitSInvest: { type: "number" },
                             Description: { type: "string" },
                             Difference: { type: "number" },
                             AdjustmentUnit: { type: "number" },
                         }
                     }
                 }
             });
    }

    function refresh() {
        var newDS = getDataSource(window.location.origin + "/Radsoft/UnitReconcile/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridData").data("kendoGrid").setDataSource(newDS);

    }

    function refreshAfterAdjustment() {
        var newDS = getDataSource(window.location.origin + "/Radsoft/UnitReconcile/GetDataAfterAdjustment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridData").data("kendoGrid").setDataSource(newDS);

    }

    var gridData;
    function GenerateGrid() {
        $("#filterDate").show();
        $("#LblButton").show();

        $("#gridData").empty();
        var URL = window.location.origin + "/Radsoft/UnitReconcile/GetDataGenerateByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
          dataSourceApproved = getDataSource(URL);
        gridData = $("#gridData").kendoGrid({
            dataSource: dataSourceApproved,
            height: 650,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "FORM UNIT RECONCILE"
                }
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            dataBound: gridDataBound,
            toolbar: ["excel"],
            columns: [
                //{ command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "HistoryPK", title: "HistoryPK.", hidden: true ,width: 95 },
                { field: "Status", title: "Status.", hidden: true, width: 95 },
                { field: "UnitReconcilePK", title: "SysNo.", width: 95 },
                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "FundID", width: 100 },
                { field: "FundClientName", title: "FundClientName", width: 200 },
                { field: "UnitSystem", title: "Unit System", format: "{0:n8}", width: 200 },
                { field: "UnitSInvest", title: "Unit SInvest", format: "{0:n8}", width: 200 },
                { field: "Difference", title: "Difference", format: "{0:n8}", width: 200 },
                { field: "AdjustmentUnit", title: "AdjustmentUnit", format: "{0:n8}", width: 200 },
                { field: "Description", title: "Description", width: 200 },
            ]
        }).data("kendoGrid");
        $("#SelectedAllApproved").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Approved");

        });

        gridData.table.on("click", ".cSelectedDetailApproved", selectDataApproved);
        function selectDataApproved(e) {
            

            var grid = $("#gridData").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _unitreconcilePK = dataItemX.UnitReconcilePK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _unitreconcilePK);
        }


    }

    function gridDataBound() {
        var grid = $("#gridData").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Status == "2") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowApproveBuy");
            } else if (row.Status == "1") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("pendingRowVoid");
            }
        });
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/UnitReconcile/" + _a + "/" + _b,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/UnitReconcile/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
                refresh();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    var UnitReconcile = {
                        UnitReconcilePK: $('#UnitReconcilePK').val(),
                        Description: $('#Description').val(),
                        AdjustmentUnit: $('#AdjustmentUnit').val(),
                        Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/UnitReconcile/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UnitReconcile_U",
                        type: 'POST',
                        data: JSON.stringify(UnitReconcile),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            win.close();
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }

            });
        }
    });

    $("#BtnImportUnitReconcileTxt").click(function () {
        document.getElementById("FileImportUnitReconcileTxt").click();
    });

    $("#FileImportUnitReconcileTxt").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportUnitReconcileTxt").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("UnitReconcileTxt", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UnitReconcile_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportUnitReconcileTxt").val("");
                    alertify.alert("Import Success");
                    GenerateGrid();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportUnitReconcileTxt").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportUnitReconcileTxt").val("");
        }
    });

    $("#BtnImportUnitReconcileTxtAPERD").click(function () {
        document.getElementById("FileImportUnitReconcileTxtAPERD").click();
    });

    $("#FileImportUnitReconcileTxtAPERD").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportUnitReconcileTxtAPERD").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("UnitReconcileTxtAPERD", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "UnitReconcile_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportUnitReconcileTxtAPERD").val("");
                    alertify.alert("Import Success");
                    GenerateGrid();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportUnitReconcileTxtAPERD").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportUnitReconcileTxtAPERD").val("");
        }
    });


});