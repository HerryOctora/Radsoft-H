$(document).ready(function () {
    document.title = 'FORM Import FXD';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridTB;
    var gridHeight = screen.height - 300;
    //1
    initButton();
    //2
    initWindow();

    function initButton() {
        $("#BtnImportFxd").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportFxd14").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnRecon").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnShow").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnInsertJournal").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function initWindow() {
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        win = $("#WinImportFxd").kendoWindow({
            height: 450,
            title: "ImportFxd Detail",
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

        WinRecon = $("#WinRecon").kendoWindow({
            height: 250,
            title: "SHOW RECONCILE DATA",
            visible: false,
            width: 1000,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        winTB = $("#WinTB").kendoWindow({
            height: 1000,
            title: "TRIAL BALANCE",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");

        winFundPositionIndex0 = $("#WinDetailFundPositionIndex0").kendoWindow({
            height: 400,
            title: "Detail",
            visible: false,
            width: 800,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 320, left: 300 })
            },
        }).data("kendoWindow");

    }
    if ($("#FilterFundID").val() == "" || $("#FilterFundID").val() == "0") {
        _fundID = "0";
        _type = "0";
    }
    else {
        _fundID = $("#FilterFundID").val();
        _type = $("#FilterExposureType").val();
    }

    var GlobValidator = $("#WinImportFxd").kendoValidator().data("kendoValidator"); 
    var GlobValidatorRecon = $("#WinRecon").kendoValidator().data("kendoValidator");

    

    function validateData() {


        if (GlobValidatorRecon.validate()) {
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

    function clearData() {
        $("#FormatImport").val("");
    }

    function showButton() {
        $("#BtnImportTransaction").show();
        $("#BtnImportFundClient").show();
        $("#BtnImportFund").show();
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
                             ImportFxdPK: { type: "number" },
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

    $("#BtnImportFxd").click(function () {
        document.getElementById("FileImportFxd").click();
    });


    $("#FileImportFxd").change(function () {


        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportFxd").get(0).files;
        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("FxdImport", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fxd_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportFxd").val("");


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportFxd").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportFxd").val("");
        }


    });


    $("#BtnImportFxd14").click(function () {
        document.getElementById("FileImportFxd14").click();
    });


    $("#FileImportFxd14").change(function () {


        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportFxd14").get(0).files;
        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("FxdImport14", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fxd_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportFxd14").val("");


                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportFxd14").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportFxd14").val("");
        }


    });


    $("#BtnRecon").click(function () {
        showRecon()

    });

    function showRecon(e) {

        //combo box FundFeePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }
        
        WinRecon.center();
        WinRecon.open();

    }

    function onWinReconClose() {
        GlobValidatorRecon.hideMessages();
        clearDataRecon();
        //refresh();
    }

    function clearDataRecon()
    {
        $("#Date").val("");
        $("#FundPK").val("");

    }


    function RefreshHeaderInformation() {
        var _asset = 0;
        var _liabilities = 0;
        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAccountBalanceByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/1" + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblTotalAsset").text(": " + kendo.toString(data, 'n2'));

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAccountBalanceByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/63" + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblTotalLiablities").text(": " + kendo.toString(data, 'n2'));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAUMByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/1/63" + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblTotalAUM").text(": " + kendo.toString(data, 'n2'));

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetTotalUnitByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblTotalUnit").text(": " + kendo.toString(data, 'n4'));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetNAVProjectionByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblNAVProjection").text(": " + kendo.toString(data, 'n4'));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    $("#BtnShow").click(function () {
        var val = validateData();
        if (val == 1) {
            
            $.ajax({
                url: window.location.origin + "/Radsoft/ImportFxd/CheckData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == true) {
                        FxdRecon();
                        winTB.center();
                        winTB.open();
                    }
                    else {
                        alertify.alert("Bang Bang Tuuuuttt!!");
                    }
                }

            })
            
        }
    });

    function FxdRecon() {
        var dsTrialBalance = getDataTrialBalance();

        gridTB = $("#gridTB").kendoGrid({
            dataSource: dsTrialBalance,
            height: "750px",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            toolbar: kendo.template($("#gridTBTemplate").html()),
            selectable: "cell",
            dataBound: gridTBOndataBound,
            save: gridTBOnSave,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            columns: [
            { field: "Fxd11AccountPK", title: "SysNo.", width: 95 },
            { field: "ID", title: "ID", width: 100 },
            {
                field: "SystemBalance", title: "System Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
            },
            {
                field: "FxdBalance", title: "Fxd Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
            },
            {
                field: "Diff", title: "Diff", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                    , headerAttributes: {
                        style: "text-align: center"
                    }, attributes: { style: "text-align:right;" }
            }
            ]
        }).data("kendoGrid");

        RefreshHeaderInformation();
        $("#lblFund").text(":" + $("#FilterFundID").data("kendoComboBox").text());
        $("#lblDate").text(kendo.toString($("#Date").data("kendoDatePicker").value(), "dd-MMM-yy"));

        $(gridTB.tbody).on("click", "td", function (e) {
            //FxdReconDetail();
            var row = $(this).closest("tr");
            var rowIdx = $("tr", gridTB.tbody).index(row);
            var colIdx = $("td", row).index(this);
            var colName = $('#gridTB').find('th').eq(colIdx).text();
            var item = gridTB.dataItem(row);
            var FxdReconDetailURL = window.location.origin + "/Radsoft/ImportFxd/GetDataSourceFxdReconDetail/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + item.Fxd11AccountPK + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSource = getDataSourceDetail(FxdReconDetailURL);
            gridDetailFundPositionIndex0 = $("#gridDetailFundPositionIndex0").kendoGrid({
                dataSource: dataSource,
                height: "750px",
                scrollable: {
                    virtual: true
                },
                reorderable: true,
                sortable: true,
                //resizable: true,
                //selectable: "cell",
                filterable: {
                    extra: false,
                    operators: {
                        string: {
                            contains: "Contain",
                            eq: "Is equal to",
                            neq: "Is not equal to"
                        }
                    }
                },
                columns: [
                { field: "ID", title: "ID", width: 100 },
                { field: "Name", title: "Name", width: 100 },
                {
                    field: "CurrentBaseBalance", title: "Current Base Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
                }
                ]
            }).data("kendoGrid");


            winFundPositionIndex0.open();
        });
    }



    function FxdReconDetail() {
        

    }


    function gridTBOndataBound(e) {
        var grid = $("#gridTB").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsGroups == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("TBReconcileHeader");
            } else if (row.BitIsChange == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("TBReconcileHeader");
            }
        });
    }

    function gridTBOnSave(e) {
        if (!e.model.BitIsChange || e.model.BitIsGroups) {
            e.preventDefault();
        }
    }

    function getDataTrialBalance() {
        return new kendo.data.DataSource(
                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/ImportFxd/GetTrialBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     batch: true,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 100,
                     schema: {
                         model: {
                             fields: {
                                 Fxd11AccountPK: { type: "number" },
                                 ID: { type: "string"},
                                 SystemBalance: { type: "number" },
                                 Diff: { type: "number" },
                                 FxdBalance: { type: "number" },
                             }
                         }
                     },
                     //group:
                     //    [
                     //    {
                     //        field: "ParentName", aggregates: [
                     //        { field: "PreviousBaseBalance", aggregate: "sum" },
                     //        { field: "Movement", aggregate: "sum" },
                     //        { field: "CurrentBaseBalance", aggregate: "sum" },
                     //        { field: "BKBalance", aggregate: "sum" }
                     //        ]
                     //    }
                     //    ],
                     aggregate: [{ field: "SystemBalance", aggregate: "sum" },
                             { field: "Diff", aggregate: "sum" },
                             { field: "FxdBalance", aggregate: "sum" }
                     ]
                 });
    }

    function getDataSourceDetail(_url) {
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
                             ID: { type: "string" },
                             Name: { type: "string" },
                             CurrentBaseBalance: { type: "number" },
                         }
                     }
                 }
             });
    }

});
