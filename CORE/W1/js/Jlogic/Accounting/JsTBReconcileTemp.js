$(document).ready(function () {
    var win;
    var winTB;

    var GlobValidator = $("#WinTBReconcileTemp").kendoValidator().data("kendoValidator");

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
        $("#BtnImportTBReconcileTemp").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnGetTB").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnInsertJournal").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnTBRpt").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnReleaseTime").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnTBStatus").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });


        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    index: 0,
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

        WinEndDayTrailsMatching = $("#WinEndDayTrailsMatching").kendoWindow({
            height: 600,
            title: "End Day Trails Matching List",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinEndDayTrailsMatchingClose
        }).data("kendoWindow");

        winTB = $("#WinTB").kendoWindow({
            height: 6000,
            title: "TRIAL BALANCE",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onwinTBClose
        }).data("kendoWindow");



        win = $("#WinTBReconcileTemp").kendoWindow({
            height: 400,
            title: "* TB Reconcile",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");


        windetailMovement = $("#WinDetailMovement").kendoWindow({
            height: 600,
            title: "* TB Reconcile",
            visible: false,
            width: 1000,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");


        winTBStatus = $("#WinTBStatus").kendoWindow({
            height: 600,
            title: "TB Status",
            visible: false,
            width: 1000,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        win.center();
        win.open();
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

    $("#BtnInsertJournal").click(function () {
        var _tb = [];
        var totalBKBalance = 0;
        var gridDataArray = $('#gridTB').data('kendoGrid')._data;
        var flagChange = 0;
        for (var index = 0; index < gridDataArray.length; index++) {
            if (gridDataArray[index]["BKBalance"] != null) {

                var _m = {
                    ID: gridDataArray[index]["ID"],
                    BKBalance: gridDataArray[index]["BKBalance"],
                    CurrentBaseBalance: gridDataArray[index]["CurrentBaseBalance"]
                }
                totalBKBalance = totalBKBalance + (gridDataArray[index]["CurrentBaseBalance"] - gridDataArray[index]["BKBalance"]);
                _tb.push(_m);
                flagChange = 1
            }

        };
        if (flagChange == 0) {
            alertify.alert('No Data BK Balance');
            return;
        }
        var _str = "";
        if (Math.abs(totalBKBalance) > 1) {
            alertify.alert("Total BK Movement is not Balance :" + formatNumber(Math.floor(Math.abs(totalBKBalance))).toString());
        } else {
            $.ajax({
                url: window.location.origin + "/Radsoft/TBReconcileTemp/ValidateSaveToJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {

                    if (data != "") {
                        alertify.alert(data);
                        //winTB.close();
                        win.center();
                        win.open();
                    }
                    else {
                        alertify.prompt("Are you sure want to Save this end Balance ?", "", function (e, str) {
                            if (str.trim() == null || str.trim() == "") {
                                _str = "ADJUSTMENT";
                            }
                            else {
                                _str = str;
                            }
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TBReconcileTemp/TrialBalance_SaveToJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _str,
                                type: 'POST',
                                data: JSON.stringify(_tb),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    RefreshTB();
                                    RefreshHeaderInformation();
                                    UpdateReleaseTime();
                                    alertify.alert(data);
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }, function () {
                            alertify.alert('Cancel');
                        });

                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });
        }
    });

    function RefreshTB()
    {
        var newDS = getDataTrialBalance();
        $("#gridTB").data("kendoGrid").setDataSource(newDS);
    }

    function RefreshHeaderInformation() {
        var _asset = 0;
        var _liabilities = 0;
        var _nav = 0;
        var _navYesterday = 0;
        var _changeNav = 0;
        var _changeNavPercent = 0;

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAccountBalanceByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/1" + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#lblTotalAsset").text(": " + kendo.toString(data, 'n2'));

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAccountBalanceByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/63" + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#lblTotalLiablities").text(": " + kendo.toString(data, 'n2'));
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAUMByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/1/63" + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#lblTotalAUM").text(": " + kendo.toString(data, 'n2'));

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetTotalUnitByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#lblTotalUnit").text(": " + kendo.toString(data, 'n4'));
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetNAVProjectionByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#lblNAVProjection").text(": " + kendo.toString(data, 'n4'));
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetNAVProjectionByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        _nav = data;
        //        $.ajax({
        //            url: window.location.origin + "/Radsoft/Fund/GetNAVDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val(),
        //            type: 'GET',
        //            contentType: "application/json;charset=utf-8",
        //            success: function (data) {
        //                if (data == "0") {
        //                    $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n0'));
        //                }
        //                else if (data == "2") {
        //                    $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n2'));
        //                }
        //                else if (data == "4") {
        //                    $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n4'));
        //                }
        //                else if (data == "6") {
        //                    $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n6'));
        //                }
        //                else {
        //                    $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n8'));
        //                }

        //            },
        //            error: function (data) {
        //                alertify.alert(data.responseText);
        //            }
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetCloseNavYesterdayByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        _navYesterday = data;
        //        $.ajax({
        //            url: window.location.origin + "/Radsoft/Fund/GetNAVDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val(),
        //            type: 'GET',
        //            contentType: "application/json;charset=utf-8",
        //            success: function (data) {
        //                if (data == "0") {
        //                    $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n0'));
        //                }
        //                else if (data == "2") {
        //                    $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n2'));
        //                }
        //                else if (data == "4") {
        //                    $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n4'));
        //                }
        //                else if (data == "6") {
        //                    $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n6'));
        //                }
        //                else {
        //                    $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n8'));
        //                }

        //            },
        //            error: function (data) {
        //                alertify.alert(data.responseText);
        //            }
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //$.ajax({
        //    url: window.location.origin + "/Radsoft/CloseNAV/GetCompareCloseNavByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        _changeNav = data.ChangeNav;
        //        _changeNavPercent = data.ChangeNavPercent;
        //        $.ajax({
        //            url: window.location.origin + "/Radsoft/Fund/GetNAVDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val(),
        //            type: 'GET',
        //            contentType: "application/json;charset=utf-8",
        //            success: function (data) {
        //                if (data == "0") {
        //                    $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n0'));
        //                    $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p0"));
        //                }
        //                else if (data == "2") {
        //                    $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n2'));
        //                    $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p2"));
        //                }
        //                else if (data == "4") {
        //                    $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n4'));
        //                    $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p4"));
        //                }
        //                else if (data == "6") {
        //                    $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n6'));
        //                    $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p6"));
        //                }
        //                else {
        //                    $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n8'));
        //                    $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p8"));
        //                }

        //            },
        //            error: function (data) {
        //                alertify.alert(data.responseText);
        //            }
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        //new
        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetTotalAccountBalanceByFundPKNew/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/1" + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/63",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#lblTotalAsset").text(": " + kendo.toString(data.TotalAccount1, 'n2'));
                $("#lblTotalLiablities").text(": " + kendo.toString(data.TotalAccount63, 'n2'));
                $("#lblTotalUnit").text(": " + kendo.toString(data.Unit, 'n4'));
                $("#lblTotalAUM").text(": " + kendo.toString(data.AUM, 'n2'));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/TBReconcileTemp/GetEDTInformation/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (_GlobClientCode != "05")
                    $("#lblEDTInformation").text(kendo.toString(data));
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        $.ajax({
            url: window.location.origin + "/Radsoft/CloseNAV/GetNAVProjectionByTBRecon/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                _nav = data.NAV;
                _navYesterday = data.NAVYesterday;
                _changeNav = data.ChangeNAV;
                _changeNavPercent = data.ChangeNAVPercent;
                _lastNavDate = data.LastNavDate;
                $.ajax({
                    url: window.location.origin + "/Radsoft/Fund/GetNAVDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "0") {
                            $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n0'));
                            $("#lblLastNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n0'));
                            $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n0'));
                            $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p0"));
                            $("#lblLastNavDate").text(": " + _lastNavDate);
                        }
                        else if (data == "2") {
                            $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n2'));
                            $("#lblLastNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n2'));
                            $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n2'));
                            $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p2"));
                            $("#lblLastNavDate").text(": " + _lastNavDate);
                        }
                        else if (data == "4") {
                            $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n4'));
                            $("#lblLastNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n4'));
                            $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n4'));
                            $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p4"));
                            $("#lblLastNavDate").text(": " + _lastNavDate);
                        }
                        else if (data == "6") {
                            $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n6'));
                            $("#lblLastNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n6'));
                            $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n6'));
                            $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p6"));
                            $("#lblLastNavDate").text(": " + _lastNavDate);
                        }
                        else {
                            $("#lblNAVProjection").text(": " + kendo.toString(_nav, 'n8'));
                            $("#lblNAVYesterday").text(": " + kendo.toString(_navYesterday, 'n8'));
                            $("#lblChangeNAV").text(kendo.toString(_changeNav, 'n8'));
                            $('#lblChangeNAVPercent').text(kendo.toString(_changeNavPercent / 100, "p8"));
                            $("#lblLastNavDate").text(": " + _lastNavDate);
                        }

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
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

        $(grid.tbody).on("click", "td", function (e) {
            var row = $(this).closest("tr");
            var rowIdx = $("tr", grid.tbody).index(row);
            var colIdx = $("td", row).index(this);
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (colIdx == '7') {
                GridDetailMovement(dataItemX.ID);
                windetailMovement.center();
                windetailMovement.open();
            }

        });
    }
    
    function gridTBOnSave(e)
    {
        if (!e.model.BitIsChange || e.model.BitIsGroups)
        {
            e.preventDefault();
        }
    }

    $("#BtnGetTB").click(function () {

        //RefreshHeaderInformation();
        $("#lblFund").text(": " + $("#FilterFundID").data("kendoComboBox").text());
        $("#lblDate").text(kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "dd-MMM-yy"));

        $.ajax({
            url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateToday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    alertify.alert("Plase Generate End Day Trails Fund Journal Today First !");
                    return;
                }
                else {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TBReconcileTemp/ValidateAlreadyTbReconcile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FilterFundID").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                alertify.confirm("This Trial Balance has been reconciled, Are you sure want to Tb Recon data ?", function (e) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/TBReconcileTemp/InsertTBReconileLog/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {

                                            if (data != "")
                                                alertify.alert(data);
                                            else {

                                                RefreshHeaderInformation();
                                                var dsTrialBalance = getDataTrialBalance();
                                                $("#gridTB").empty();
                                                var grid = $("#gridTB").kendoGrid({
                                                    dataSource: dsTrialBalance,
                                                    height: "95%",
                                                    scrollable: {
                                                        virtual: true
                                                    },
                                                    reorderable: true,
                                                    sortable: true,
                                                    resizable: true,
                                                    editable: "incell",
                                                    dataBound: gridTBOndataBound,
                                                    save: gridTBOnSave,
                                                    groupable: true,
                                                    toolbar: kendo.template($("#gridTB").html()),
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
                                                    pageable: true,
                                                    pageable: {
                                                        input: true,
                                                        numeric: false
                                                    },
                                                    toolbar: ["excel"],
                                                    columns: [
                                                        { field: "BitIsGroups", title: "IsGroup", hidden: true, width: 50 },
                                                        { field: "BitIsChange", title: "IsChange", hidden: true, width: 50 },
                                                        { field: "Header", title: "Header", width: 200 },
                                                        { field: "ID", title: "ID", hidden: true, width: 100 },
                                                        { field: "Name", title: "Name", width: 300 },
                                                        { field: "ParentName", title: "Parent Name", hidden: true, width: 350 },
                                                        {
                                                            field: "PreviousBaseBalance", title: "Prev Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , headerAttributes: {
                                                                style: "text-align: center"
                                                            }, attributes: { style: "text-align:right;" }
                                                        },
                                                        {
                                                            field: "Movement", title: "Movement", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , headerAttributes: {
                                                                style: "text-align: center"
                                                            }, attributes: { style: "text-align:right;" }
                                                        },
                                                        {
                                                            field: "CurrentBaseBalance", title: "End Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                            , headerAttributes: {
                                                                style: "text-align: center"
                                                            }, attributes: { style: "text-align:right;" }
                                                        },
                                                        {
                                                            field: "BKBalance", title: "BK Balance", width: 150,
                                                            format: "{0:n2}",

                                                            //editor: function(container, options) {
                                                            //    // create an input element
                                                            //    $("<input name='" + options.field + "'/>")
                                                            //    .appendTo(container)
                                                            //    .kendoNumericTextBox({
                                                            //        decimals: 4
                                                            //    });
                                                            //},
                                                            attributes: { style: "text-align:right;" }
                                                        }
                                                    ]
                                                }).data("kendoGrid");


                                                winTB.center();
                                                winTB.open();

                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });

                                }, function () {
                                    alertify.alert('Cancel');
                                });
                            }
                            else {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/TBReconcileTemp/InsertTBReconileLog/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {

                                        if (data != "")
                                            alertify.alert(data);
                                        else {

                                            RefreshHeaderInformation();
                                            var dsTrialBalance = getDataTrialBalance();
                                            $("#gridTB").empty();
                                            var grid = $("#gridTB").kendoGrid({
                                                dataSource: dsTrialBalance,
                                                height: "95%",
                                                scrollable: {
                                                    virtual: true
                                                },
                                                reorderable: true,
                                                sortable: true,
                                                resizable: true,
                                                editable: "incell",
                                                dataBound: gridTBOndataBound,
                                                save: gridTBOnSave,
                                                groupable: true,
                                                toolbar: kendo.template($("#gridTB").html()),
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
                                                pageable: true,
                                                pageable: {
                                                    input: true,
                                                    numeric: false
                                                },
                                                toolbar: ["excel"],
                                                columns: [
                                                    { field: "BitIsGroups", title: "IsGroup", hidden: true, width: 50 },
                                                    { field: "BitIsChange", title: "IsChange", hidden: true, width: 50 },
                                                    { field: "Header", title: "Header", width: 200 },
                                                    { field: "ID", title: "ID", hidden: true, width: 100 },
                                                    { field: "Name", title: "Name", width: 300 },
                                                    { field: "ParentName", title: "Parent Name", hidden: true, width: 350 },
                                                    {
                                                        field: "PreviousBaseBalance", title: "Prev Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , headerAttributes: {
                                                            style: "text-align: center"
                                                        }, attributes: { style: "text-align:right;" }
                                                    },
                                                    {
                                                        field: "Movement", title: "Movement", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , headerAttributes: {
                                                            style: "text-align: center"
                                                        }, attributes: { style: "text-align:right;" }
                                                    },
                                                    {
                                                        field: "CurrentBaseBalance", title: "End Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                                                        , headerAttributes: {
                                                            style: "text-align: center"
                                                        }, attributes: { style: "text-align:right;" }
                                                    },
                                                    {
                                                        field: "BKBalance", title: "BK Balance", width: 150,
                                                        format: "{0:n2}",

                                                        //editor: function(container, options) {
                                                        //    // create an input element
                                                        //    $("<input name='" + options.field + "'/>")
                                                        //    .appendTo(container)
                                                        //    .kendoNumericTextBox({
                                                        //        decimals: 4
                                                        //    });
                                                        //},
                                                        attributes: { style: "text-align:right;" }
                                                    }
                                                ]
                                            }).data("kendoGrid");


                                            winTB.center();
                                            winTB.open();

                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });
                            }

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });


  
    function getDataTrialBalance() {
        return new kendo.data.DataSource(
                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/TBReconcileTemp/GetTrialBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
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
                     pageSize: 10000,
                     schema: {
                         model: {
                             fields: {
                                 Header: { type: "string", editable: false },
                                 ID: { type: "string", editable: false },
                                 Name: { type: "string", editable: false },
                                 ParentName: { type: "string", editable: false },
                                 PreviousBaseBalance: { editable: false },
                                 Movement: { editable: false },
                                 CurrentBaseBalance: { editable: false },
                                 BitIsChange: { type: "boolean" },
                                 BKBalance: { type: "number" },
                                 ZeroAmount: { type: "boolean" }
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
                     aggregate: [{ field: "PreviousBaseBalance", aggregate: "sum" },
                             { field: "Movement", aggregate: "sum" },
                             { field: "CurrentBaseBalance", aggregate: "sum" },
                            { field: "BKBalance", aggregate: "sum" }
                     ]
                 });
    }

    $("#BtnImportTBReconcileTemp").click(function () {
        document.getElementById("FileImportTBReconcileTemp").click();
    });

    $("#FileImportTBReconcileTemp").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportTBReconcileTemp").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        var _d = files[0].name.substring(0,8);
        if (files.length > 0) {
            data.append("TBReconcileTemp", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TBReconcileTemp_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportTBReconcileTemp").val("");
                    if ((data) != "")
                    {
                        alertify.alert(data);
                    }
                    else
                    {
                        initEndDayTrailsMatching(_d);
                        WinEndDayTrailsMatching.center();
                        WinEndDayTrailsMatching.open();
                    }
        
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportTBReconcileTemp").val("");
                }
            });             
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportTBReconcileTemp").val("");
        }
    });

    $("#BtnCancelListing").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinInvestmentListing.close();
                alertify.alert("Cancel Listing");
            }
        });
    });

    function getDataSourceEndDayTrailsMatching(_d) {
        return new kendo.data.DataSource(
                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/TBReconcileTemp/GetEndDayTrailsMatching/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _d,
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 10000,
                     schema: {
                         model: {
                             fields: {
                                 Date: { type: "string" },
                                 FundID: { type: "string" },
                                 //AccountID: { type: "string" },
                                 AccountName: { type: "string" },
                                 AmountSystem: { type: "number" },
                                 AmountImportData: { type: "number" },
                                 Difference: { type: "number" }
                             }
                         }
                     }
                 });
    }

    function initEndDayTrailsMatching(_d) {
        var dsEndDayTrailsMatching = getDataSourceEndDayTrailsMatching(_d);
        $("#gridEndDayTrailsMatching").kendoGrid({
            dataSource: dsEndDayTrailsMatching,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
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
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            toolbar: ["excel"],
            columns: [
               { command: { text: "Insert", click: showInsert }, title: " ", width: 80 },
               { field: "Date", title: "Date", width: 150 },
               { field: "FundID", title: "Fund ID", width: 150 },
               //{ field: "AccountID", title: "Account ID", width: 250 },
               { field: "AccountName", title: "Account Name", width: 450 },
               { field: "AmountSystem", title: "Amount System", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AmountImportData", title: "Amount Import Data", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "Difference", title: "Difference", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
            ]
        });
    }

    function onWinEndDayTrailsMatchingClose() {
        $("#gridEndDayTrailsMatching").empty();
    }

    function showInsert(e) {

        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Insert ?", function (a) {
                if (a) {
                    var grid = $("#gridEndDayTrailsMatching").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                    var _date = _dataItemX.Date;
                    var TBReconcile = {
                        Date: _date,
                        UsersID: sessionStorage.getItem("user")
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/TBReconcileTemp/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TBReconcileTemp_InsertDifference",
                        type: 'POST',
                        data: JSON.stringify(TBReconcile),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            alertify.alert(data);
                            WinEndDayTrailsMatching.close();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }

            });
            e.handled = true;
        }
    }


    $("#BtnTBRpt").click(function () {
        //alertify.alert(kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "--" + $("#FilterFundID").val())

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                var TBRpt = {
                    ValueDate: kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    FundPK: $("#FilterFundID").val(),
                    FundID: $("#FilterFundID").data("kendoComboBox").text(),
                    DownloadMode: $("#DownloadMode").data("kendoComboBox").value()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/TBReconcileTemp/TBRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(TBRpt),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data;
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });

            }
        });


    });

    function getDataSourceDetailMovement(_url) {
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
                 aggregate: [{ field: "BaseDebit", aggregate: "sum" },
                            { field: "BaseCredit", aggregate: "sum" }],

                 schema: {
                     model: {
                         fields: {
                             Name: { type: "string" },
                             Description: { type: "string" },
                             BaseDebit: { type: "number" },
                             BaseCredit: { type: "number" },
                         },


                     }
                 }
             });
    }

    function GridDetailMovement(_accountID) {
        $("#gridDetailMovement").empty();
        var DetailMovementURL = window.location.origin + "/Radsoft/TBReconcileTemp/GetDetailMovement/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").data("kendoComboBox").value() + "/" + _accountID,
          dataSourceApproved = getDataSourceDetailMovement(DetailMovementURL);

        var gridDetail = $("#gridDetailMovement").kendoGrid({
            dataSource: dataSourceApproved,
            height: 1000,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TB Reconcile"
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
            toolbar: ["excel"],
            columns: [
                { field: "Name", title: "Name", width: 250 },
                { field: "Description", title: "Description", width: 300 },
                {
                    field: "BaseDebit", title: "Base Debit", width: 150,
                    headerAttributes: { style: "text-align: center" },
                    //footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    footerTemplate: "#=kendo.toString(sum,'n2')#",
                    format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "BaseCredit", title: "Base Credit", width: 150,
                    headerAttributes: { style: "text-align: center" },
                    //footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>",
                    footerTemplate: "#=kendo.toString(sum,'n2')#",
                    format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
            ]
        }).data("kendoGrid");


    }

    function formatNumber(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
    }

    function onwinTBClose() {
        UpdateReleaseTime();
    }

    function UpdateReleaseTime() {
        $.ajax({
            url: window.location.origin + "/Radsoft/TBReconcileTemp/UpdateReleaseTime/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
            type: 'GET',
            data: JSON.stringify(),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                //winTB.close();
            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
    }

    $("#BtnReleaseTime").click(function () {

        alertify.confirm("Are You Sure Want To Release LockTime ?", function (e) {
            $.ajax({
                url: window.location.origin + "/Radsoft/TBReconcileTemp/ReleaseLock/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FilterFundID").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alertify.alert("Time Has Been Released");

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }, function () {
            alertify.alert('Cancel');
        });
    });


    function getDataSourceTBStatus(_url) {
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
                            FundID: { type: "string" },
                            StatusReconcile: { type: "string" }
                        },


                    }
                }
            });
    }

    $("#BtnTBStatus").click(function () {

        $("#gridTBStatus").empty();
        var TBStatusURL = window.location.origin + "/Radsoft/TBReconcileTemp/GetDataByTBStatus/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSourceTBStatus(TBStatusURL);

        $("#gridTBStatus").kendoGrid({
            dataSource: dataSourceApproved,
            height: 500,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form TB Reconcile"
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
            toolbar: ["excel"],
            columns: [
                { field: "FundID", title: "Fund", width: 100 },
                { field: "StatusReconcile", title: "Status Reconcile", width: 150 },
                { field: "StatusOpen", title: "Status Open", width: 150 },
                { field: "UsersID", title: "User", width: 150 },

            ]
        }).data("kendoGrid");

        winTBStatus.center();
        winTBStatus.open();

    });



});