$(document).ready(function () {
    document.title = 'FORM FFS Setup';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();



    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOldData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnOldData.png"
        });

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
        });

        $("#BtnApproved").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });

        $("#BtnReject").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });

        $("#BtnNew").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnUploadProfilResiko").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        }); 
    }



    function initWindow() {

        $("#TanggalPerdana").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        }); 

        win = $("#WinFFSSetup_02").kendoWindow({
            height: 600,
            title: "FFSSetup_02 Detail",
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

        WinUploadImage = $("#WinUploadImage").kendoWindow({
            height: 250,
            title: "Upload Image",
            visible: false,
            width: 350,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            //close: onWinAddAgentFeeClose
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinFFSSetup_02").kendoValidator().data("kendoValidator");

    function validateData() {

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.error("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#btnUploadImage").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#btnUploadImage").show();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#btnUploadImage").show();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            $("#FFSSetup_02PK").val(dataItemX.FFSSetup_02PK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#TujuanStrategiInvestasi").val(dataItemX.TujuanStrategiInvestasi);
            $("#InformasiProduk").val(dataItemX.InformasiProduk);
            $("#TanggalPerdana").data("kendoDatePicker").value(dataItemX.TanggalPerdana);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#MarketReview").val(dataItemX.MarketReview);
            $("#UngkapanSanggahan").val(dataItemX.UngkapanSanggahan);
            $("#FaktorResikoYangUtama").val(dataItemX.FaktorResikoYangUtama);
            $("#ManfaatInvestasi").val(dataItemX.ManfaatInvestasi);
            $("#KebijakanInvestasi").val(dataItemX.KebijakanInvestasi);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

        $("#NilaiAktivaBersih").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setNilaiAktivaBersih()
        });
        function setNilaiAktivaBersih() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NilaiAktivaBersih;
            }
        }

        $("#TotalUnitPenyertaan").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setTotalUnitPenyertaan()
        });
        function setTotalUnitPenyertaan() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TotalUnitPenyertaan;
            }
        }

        $("#NilaiAktivaBersihUnit").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setNilaiAktivaBersihUnit()
        });
        function setNilaiAktivaBersihUnit() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NilaiAktivaBersihUnit;
            }
        }

        $("#ImbalJasaManajerInvestasi").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setImbalJasaManajerInvestasi()
        });
        function setImbalJasaManajerInvestasi() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ImbalJasaManajerInvestasi;
            }
        }

        $("#ImbalJasaBankKustodian").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setImbalJasaBankKustodian()
        });
        function setImbalJasaBankKustodian() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ImbalJasaBankKustodian;
            }
        }

        $("#BiayaPembelian").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setBiayaPembelian()
        });
        function setBiayaPembelian() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BiayaPembelian;
            }
        }

        $("#BiayaPenjualan").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setBiayaPenjualan()
        });
        function setBiayaPenjualan() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BiayaPenjualan;
            }
        }

        $("#BiayaPengalihan").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setBiayaPengalihan()
        });
        function setBiayaPengalihan() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BiayaPengalihan;
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundPK,
                    dataSource: data,
                    value: setCmbFundPK()
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



        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankKustodianPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankKustodianPK,
                    dataSource: data,
                    value: setCmbBankKustodianPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeBankKustodianPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#BankAccountPK").data("kendoComboBox").value(this.value())
        }
        function setCmbBankKustodianPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankKustodianPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankKustodianPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankAccountPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    value: setCmbBankKustodianPK(),
                    dataSource: data
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        

        win.center();
        win.open();
    }


    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundPK").val("");
        $("#TujuanStrategiInvestasi").val("");
        $("#ValueDate").val("");
        $("#MarketReview").val("");
        $("#UngkapanSanggahan").val("");
        $("#TanggalPerdana").val("");
        $("#NilaiAktivaBersih").val("");
        $("#TotalUnitPenyertaan").val("");
        $("#NilaiAktivaBersihUnit").val("");
        $("#FaktorResikoYangUtama").val("");
        $("#ManfaatInvestasi").val("");
        $("#ImbalJasaManajerInvestasi").val("");
        $("#ImbalJasaBankKustodian").val("");
        $("#BiayaPembelian").val("");
        $("#BiayaPenjualan").val("");
        $("#BiayaPengalihan").val("");
        $("#BankKustodianPK").val("");
        $("#BankAccount").val("");
        $("#KebijakanInvestasi").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
    }

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnOldData").show();
        $("#btnUploadImage").show();
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
                             FFSSetup_02PK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             TujuanStrategiInvestasi: { type: "string" },
                             ValueDate: { type: "date" },
                             ManajerInvestasi: { type: "string" },
                             TanggalPerdana: { type: "date" },
                             NilaiAktivaBersih: { type: "number" },
                             TotalUnitPenyertaan: { type: "number" },
                             NilaiAktivaBersihUnit: { type: "number" },
                             FaktorResikoYangUtama: { type: "string" },
                             ManfaatInvestasi: { type: "string" },
                             ImbalJasaManajerInvestasi: { type: "number" },
                             ImbalJasaBankKustodian: { type: "number" },
                             BiayaPembelian: { type: "number" },
                             BiayaPenjualan: { type: "number" },
                             BiayaPengalihan: { type: "number" },
                             BankKustodianPK: { type: "number" },
                             BankKustodianID: { type: "string" },
                             BankAccount: { type: "string" },
                             MarketReview: { type: "string" },
                             KebijakanInvestasi: { type: "string" },
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

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridFFSSetup_02Approved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFFSSetup_02Pending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFFSSetup_02History").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FFSSetup_02ApprovedURL = window.location.origin + "/Radsoft/FFSSetup_02/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(FFSSetup_02ApprovedURL);

        $("#gridFFSSetup_02Approved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FFSSetup_02"
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
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "FFSSetup_02PK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "TujuanStrategiInvestasi", title: "Tujuan Strategi Investasi", width: 200,  attributes: { style: "text-align:right;" } },
                { field: "MarketReview", title: "Market Review", width: 200, attributes: { style: "text-align:right;" } },
                { field: "UngkapanSanggahan", title: "Ungkapan Sanggahan", width: 200,  attributes: { style: "text-align:right;" } },
                { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200, attributes: { style: "text-align:right;" } },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFFSSetup_02").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);

                    if (tabindex == 1) {
                        var FFSSetup_02PendingURL = window.location.origin + "/Radsoft/FFSSetup_02/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(FFSSetup_02PendingURL);
                        $("#gridFFSSetup_02Pending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFSSetup_02"
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
                                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                { field: "FFSSetup_02PK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "TujuanStrategiInvestasi", title: "Tujuan Strategi Investasi", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "MarketReview", title: "Market Review", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "UngkapanSanggahan", title: "Ungkapan Sanggahan", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var FFSSetup_02HistoryURL = window.location.origin + "/Radsoft/FFSSetup_02/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(FFSSetup_02HistoryURL);

                        $("#gridFFSSetup_02History").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFSSetup_02"
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
                            dataBound: gridHistoryDataBound,
                            toolbar: ["excel"],
                            columns: [
                                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                                { field: "FFSSetup_02PK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "TujuanStrategiInvestasi", title: "Tujuan Strategi Investasi", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "MarketReview", title: "Market Review", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "UngkapanSanggahan", title: "Ungkapan Sanggahan", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

                            ]
                        });
                    }
                } else {
                    refresh();
                }
            }
        });
    }


    function gridHistoryDataBound() {
        var grid = $("#gridFFSSetup_02History").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Detail");
            }
        });
    });

    $("#BtnNew").click(function () {
        showDetails(null);

    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    var FFSSetup_02 = {
                        FundPK: $('#FundPK').val(),
                        TujuanStrategiInvestasi: $('#TujuanStrategiInvestasi').val(),
                        ValueDate: $('#ValueDate').val(),
                        MarketReview: $('#MarketReview').val(),
                        UngkapanSanggahan: $('#UngkapanSanggahan').val(),
                        TanggalPerdana: $('#TanggalPerdana').val(),
                        NilaiAktivaBersih: $('#NilaiAktivaBersih').val(),
                        TotalUnitPenyertaan: $('#TotalUnitPenyertaan').val(),
                        NilaiAktivaBersihUnit: $('#NilaiAktivaBersihUnit').val(),
                        FaktorResikoYangUtama: $('#FaktorResikoYangUtama').val(),
                        ManfaatInvestasi: $('#ManfaatInvestasi').val(),
                        ImbalJasaManajerInvestasi: $('#ImbalJasaManajerInvestasi').val(),
                        ImbalJasaBankKustodian: $('#ImbalJasaBankKustodian').val(),
                        BiayaPembelian: $('#BiayaPembelian').val(),
                        BiayaPenjualan: $('#BiayaPenjualan').val(),
                        BiayaPengalihan: $('#BiayaPengalihan').val(),
                        BankKustodianPK: $('#BankKustodianPK').val(),
                        BankAccount: $('#BankAccount').val(),
                        KebijakanInvestasi: $('#KebijakanInvestasi').val(),
                        //Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FFSSetup_02/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_02_I",
                        type: 'POST',
                        data: JSON.stringify(FFSSetup_02),
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

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    var FFSSetup_02 = {
                        FFSSetup_02PK: $('#FFSSetup_02PK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        FundPK: $('#FundPK').val(),
                        TujuanStrategiInvestasi: $('#TujuanStrategiInvestasi').val(),
                        ValueDate: $('#ValueDate').val(),
                        MarketReview: $('#MarketReview').val(),
                        UngkapanSanggahan: $('#UngkapanSanggahan').val(),
                        TanggalPerdana: $('#TanggalPerdana').val(),
                        NilaiAktivaBersih: $('#NilaiAktivaBersih').val(),
                        TotalUnitPenyertaan: $('#TotalUnitPenyertaan').val(),
                        NilaiAktivaBersihUnit: $('#NilaiAktivaBersihUnit').val(),
                        FaktorResikoYangUtama: $('#FaktorResikoYangUtama').val(),
                        ManfaatInvestasi: $('#ManfaatInvestasi').val(),
                        ImbalJasaManajerInvestasi: $('#ImbalJasaManajerInvestasi').val(),
                        ImbalJasaBankKustodian: $('#ImbalJasaBankKustodian').val(),
                        BiayaPembelian: $('#BiayaPembelian').val(),
                        BiayaPenjualan: $('#BiayaPenjualan').val(),
                        BiayaPengalihan: $('#BiayaPengalihan').val(),
                        BankKustodianPK: $('#BankKustodianPK').val(),
                        BankAccount: $('#BankAccount').val(),
                        KebijakanInvestasi: $('#KebijakanInvestasi').val(),
                        Notes: str,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FFSSetup_02/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_02_U",
                        type: 'POST',
                        data: JSON.stringify(FFSSetup_02),
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

    $("#BtnOldData").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSSetup_02PK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSSetup_02",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FFSSetup_02" + "/" + $("#FFSSetup_02PK").val(),
                                                dataType: "json"
                                            }
                                    },
                            batch: true,
                            cache: false,
                            error: function (e) {
                                alert(e.errorThrown + " - " + e.xhr.responseText);
                                this.cancelChanges();
                            },
                            pageSize: 10,
                            schema: {
                                model: {
                                    fields: {
                                        FieldName: { type: "string" },
                                        OldValue: { type: "string" },
                                        NewValue: { type: "string" },
                                        Similarity: { type: "number" },
                                    }
                                }
                            }
                        },
                        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                        columnMenu: false,
                        pageable: {
                            input: true,
                            numeric: false
                        },
                        height: 470,
                        reorderable: true,
                        sortable: true,
                        resizable: true,
                        dataBound: gridOldDataDataBound,
                        columns: [
                            { field: "FieldName", title: "FieldName", width: 300 },
                            { field: "OldValue", title: "OldValue", width: 300 },
                            { field: "NewValue", title: "NewValue", width: 300 },
                            { field: "Similarity", title: "Similarity", width: 120 }
                        ]
                    });
                    winOldData.center();
                    winOldData.open();
                } else {
                    alertify.alert("Data has been Changed by other user, Please check it first!");
                    win.close();
                    refresh();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };

    $("#BtnApproved").click(function () {
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                var FFSSetup_02 = {
                    FFSSetup_02PK: $('#FFSSetup_02PK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FFSSetup_02/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_02_A",
                    type: 'POST',
                    data: JSON.stringify(FFSSetup_02),
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
    });

    $("#BtnVoid").click(function () {
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                var FFSSetup_02 = {
                    FFSSetup_02PK: $('#FFSSetup_02PK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FFSSetup_02/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_02_V",
                    type: 'POST',
                    data: JSON.stringify(FFSSetup_02),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.success(data);
                        win.close();
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnReject").click(function () {
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var FFSSetup_02 = {
                    FFSSetup_02PK: $('#FFSSetup_02PK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FFSSetup_02/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSSetup_02_R",
                    type: 'POST',
                    data: JSON.stringify(FFSSetup_02),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.success(data);
                        win.close();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });


    $("#BtnUploadProfilResiko").click(function () {
        document.getElementById("ProfilResiko").click();
    });

    $("#ProfilResiko").change(function () {
        $.blockUI({});
        $.ajax({
            url: window.location.origin + "/Radsoft/FFSSetup_02/GetFundID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                //alertify.alert(data.FundID);
                var datas = new FormData();
                var files = $("#ProfilResiko").get(0).files;

                if (files.length > 0) {
                    datas.append("Image_Import", files[0]);
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FFSSetup_02/UploadImageData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Image_Import/" + "ProfilResiko" + "/" + $('#FundPK').val() + "/" + data.FundID,
                        type: 'POST',
                        data: datas,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            alertify.alert(data);
                            $.unblockUI();
                            $("#ProfilResiko").val("");
                            win.close();
                            refresh();

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                            $("#ProfilResiko").val("");
                        }
                    });
                } else {
                    alertify.alert("Please Choose Correct File");
                    $.unblockUI();
                    $("#ProfilResiko").val("");
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
                $.unblockUI();
            }
        });
        


    });
});
