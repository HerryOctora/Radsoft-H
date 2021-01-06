$(document).ready(function () {
    document.title = 'FORM FFS For OJK';
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

    if (_GlobClientCode == "29") {
        $("#BtnImportPortfolioForFFS").show();
    }
    else {
        $("#BtnImportPortfolioForFFS").hide();
    }


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

        $("#BtnFFSForOJK").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnCopyFFSForOJK").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportPortfolioForFFS").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function initWindow() {
        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });


        win = $("#WinFFSForOJK").kendoWindow({
            height: 800,
            title: "FFS For OJK Detail",
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

        WinFFSForOJKRpt = $("#WinFFSForOJKRpt").kendoWindow({
            height: 250,
            title: "FFS For OJK",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpCloseWinFFSForOJKRpt
        }).data("kendoWindow");

        WinCopyFFSForOJK = $("#WinCopyFFSForOJK").kendoWindow({
            height: 250,
            title: "* Copy FFS For OJK",
            visible: false,
            width: 650,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");


        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
        });

        function OnChangeDate() {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {

                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }

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
            refresh();
        }
        function OnChangeDateTo() {

            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
    }

    var GlobValidator = $("#WinFFSForOJK").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    var dataItemX;

    function showDetails(e) {
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridFFSForOJKApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridFFSForOJKPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridFFSForOJKHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();

            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
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

            $("#FFSForOJKPK").val(dataItemX.FFSForOJKPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(kendo.parseDate(dataItemX.Date), 'dd/MMM/yyyy');
            $("#PeriodePenilaian").val(dataItemX.PeriodePenilaian);
            $("#DanaKegiatanSosial").val(dataItemX.DanaKegiatanSosial);
            $("#AkumulasiDanaCSR").val(dataItemX.AkumulasiDanaCSR);
            $("#CSR").val(dataItemX.CSR);
            $("#Resiko1").val(dataItemX.Resiko1);
            $("#Resiko2").val(dataItemX.Resiko2);
            $("#Resiko3").val(dataItemX.Resiko3);
            $("#Resiko4").val(dataItemX.Resiko4);
            $("#Resiko5").val(dataItemX.Resiko5);
            $("#Resiko6").val(dataItemX.Resiko6);
            $("#Resiko7").val(dataItemX.Resiko7);
            $("#ManajerInvestasi").val(dataItemX.ManajerInvestasi);
            $("#TujuanInvestasi").val(dataItemX.TujuanInvestasi);
            $("#KebijakanInvestasi1").val(dataItemX.KebijakanInvestasi1);
            $("#KebijakanInvestasi2").val(dataItemX.KebijakanInvestasi2);
            $("#KebijakanInvestasi3").val(dataItemX.KebijakanInvestasi3);
            $("#ProfilBankCustodian").val(dataItemX.ProfilBankCustodian);
            $("#AksesProspektus").val(dataItemX.AksesProspektus);
            $("#TemplateType").val(dataItemX.TemplateType);
            $("#KlasifikasiResiko").val(dataItemX.KlasifikasiResiko);

            $("#Resiko8").val(dataItemX.Resiko8);
            $("#Resiko9").val(dataItemX.Resiko9);
            $("#BitDistributedIncome").prop('checked', dataItemX.BitDistributedIncome);

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

        $("#TemplateType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "With Benchmark", value: 1 },
                { text: "Without Benchmark", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeTemplateType,
            value: setCmbTemplateType()
        });
        function OnChangeTemplateType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbTemplateType() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.TemplateType;
            }
        }

        $("#KlasifikasiResiko").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Rendah", value: 1 },
                { text: "Cukup Rendah", value: 2 },
                { text: "Sedang", value: 3 },
                { text: "Cukup Tinggi", value: 4 },
                { text: "Tinggi", value: 5 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeKlasifikasiResiko,
            value: setCmbKlasifikasiResiko()
        });
        function OnChangeKlasifikasiResiko() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbKlasifikasiResiko() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.KlasifikasiResiko;
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
                    dataSource: data,
                    change: onChangeFundPK,
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
        $("#FFSForOJKPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");

        $("#Date").val("");
        $("#FundPK").val("");
        $("#TemplateType").val("");
        $("#PeriodePenilaian").val("");
        $("#DanaKegiatanSosial").val("");
        $("#AkumulasiDanaCSR").val("");
        $("#CSR").val("");
        $("#Resiko1").val("");
        $("#Resiko2").val("");
        $("#Resiko3").val("");
        $("#Resiko4").val("");
        $("#Resiko5").val("");
        $("#Resiko6").val("");
        $("#Resiko7").val("");
        $("#KlasifikasiResiko").val("");
        $("#ManajerInvestasi").val("");
        $("#TujuanInvestasi").val("");
        $("#KebijakanInvestasi1").val("");
        $("#KebijakanInvestasi2").val("");
        $("#KebijakanInvestasi3").val("");
        $("#ProfilBankCustodian").val("");
        $("#AksesProspektus").val("");
        $("#Resiko8").val("");
        $("#Resiko9").val("");
        $("#BitDistributedIncome").prop('checked', false);

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
                            FFSForOJKPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },

                            Date: { type: "date" },
                            FundPK: { type: "number" },
                            FundName: { type: "string" },
                            TemplateType: { type: "number" },
                            TemplateTypeDesc: { type: "string" },
                            PeriodePenilaian: { type: "string" },
                            DanaKegiatanSosial: { type: "string" },
                            AkumulasiDanaCSR: { type: "string" },
                            CSR: { type: "string" },
                            Resiko1: { type: "string" },
                            Resiko2: { type: "string" },
                            Resiko3: { type: "string" },
                            Resiko4: { type: "string" },
                            Resiko5: { type: "string" },
                            Resiko6: { type: "string" },
                            Resiko7: { type: "string" },
                            KlasifikasiResiko: { type: "number" },
                            KlasifikasiResikoDesc: { type: "string" },
                            ManajerInvestasi: { type: "string" },
                            TujuanInvestasi: { type: "string" },
                            KebijakanInvestasi1: { type: "string" },
                            KebijakanInvestasi2: { type: "string" },
                            KebijakanInvestasi3: { type: "string" },
                            ProfilBankCustodian: { type: "string" },
                            AksesProspektus: { type: "string" },
                            Resiko8: { type: "string" },
                            Resiko9: { type: "string" },
                            BitDistributedIncome: { type: "bool" },

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
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            //grid filter
            var gridTesFilter = $("#gridFFSForOJKApproved").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            initGrid()
        }
        if (tabindex == 1) {

            var gridPending = $("#gridFFSForOJKPending").data("kendoGrid");
            //grid filter
            var gridTesFilter = $("#gridFFSForOJKPending").data("kendoGrid");

            if (gridTesFilter.dataSource.filter() != undefined || gridTesFilter.dataSource.filter() != null) {
                filterField = gridTesFilter.dataSource.filter().filters[0].field;
                filterOperator = gridTesFilter.dataSource.filter().filters[0].operator;
                filtervalue = gridTesFilter.dataSource.filter().filters[0].value;
                filterlogic = gridTesFilter.dataSource.filter().logic;
            }
            else {
                filterField = undefined;
                filterOperator = undefined;
                filtervalue = undefined;
                filterlogic = undefined;
            }
            //end grid filter
            RecalGridPending();
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {

            var gridHistory = $("#gridFFSForOJKHistory").data("kendoGrid");
            RecalGridHistory();
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {

        $("#gridFFSForOJKApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var FFSForOJKApprovedURL = window.location.origin + "/Radsoft/FFSForOJK/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(FFSForOJKApprovedURL);

        }


        $("#gridFFSForOJKApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FFS For OJK"
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
                { field: "FFSForOJKPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                { field: "Date", title: "Date", width: 200, format: "{0:dd/MMM/yyyy}" },
                { field: "FundName", title: "Fund", width: 200 },
                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                { field: "PeriodePenilaian", title: "Periode Penilaian ", width: 200 },
                { field: "DanaKegiatanSosial", title: "Dana Kegiatan Sosial", width: 200 },
                { field: "AkumulasiDanaCSR", title: "Akumulasi Dana CSR", width: 200 },
                { field: "CSR", title: "CSR", width: 200 },
                { field: "Resiko1", title: "Resiko 1", width: 200 },
                { field: "Resiko2", title: "Resiko 2", width: 200 },
                { field: "Resiko3", title: "Resiko 3", width: 200 },

                { field: "Resiko4", title: "Resiko 4", width: 200 },
                { field: "Resiko5", title: "Resiko 5", width: 200 },
                { field: "Resiko6", title: "Resiko 6", width: 200 },
                { field: "Resiko7", title: "Resiko 7", width: 200 },
                { field: "Resiko8", title: "Resiko 8", width: 200 },
                { field: "Resiko9", title: "Resiko 9", width: 200 },
                { field: "BitDistributedIncome", title: "Resiko 9", width: 200, hidden: true },
                { field: "KlasifikasiResikoDesc", title: "Klasifikasi Resiko", width: 200 },
                { field: "ManajerInvestasi", title: "Manajer Investasi", width: 1000 },
                { field: "TujuanInvestasi", title: "Tujuan Investasi", width: 1000 },
                { field: "KebijakanInvestasi1", title: "Kebijakan Investasi 1", width: 200 },
                { field: "KebijakanInvestasi2", title: "Kebijakan Investasi 2", width: 200 },
                { field: "KebijakanInvestasi3", title: "Kebijakan Investasi 3", width: 200 },

                { field: "ProfilBankCustodian", title: "Profil Bank Custodian", width: 1000 },
                { field: "AksesProspektus", title: "Akses Prospektus", width: 1000 },

                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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
        $("#TabFFSForOJK").kendoTabStrip({
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

                        $("#gridFFSForOJKPending").empty();
                        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                            alertify.alert("Please Fill Date");
                        }
                        else {

                            var FFSForOJKPendingURL = window.location.origin + "/Radsoft/FFSForOJK/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                dataSourcePending = getDataSource(FFSForOJKPendingURL);

                        }
                        $("#gridFFSForOJKPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFS For OJK"
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
                                { field: "FFSForOJKPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                { field: "Date", title: "Date", width: 200, format: "{0:dd/MMM/yyyy}" },
                                { field: "FundName", title: "Fund", width: 200 },
                                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                                { field: "PeriodePenilaian", title: "Periode Penilaian ", width: 200 },
                                { field: "DanaKegiatanSosial", title: "Dana Kegiatan Sosial", width: 200 },
                                { field: "AkumulasiDanaCSR", title: "Akumulasi Dana CSR", width: 200 },
                                { field: "CSR", title: "CSR", width: 200 },
                                { field: "Resiko1", title: "Resiko 1", width: 200 },
                                { field: "Resiko2", title: "Resiko 2", width: 200 },
                                { field: "Resiko3", title: "Resiko 3", width: 200 },

                                { field: "Resiko4", title: "Resiko 4", width: 200 },
                                { field: "Resiko5", title: "Resiko 5", width: 200 },
                                { field: "Resiko6", title: "Resiko 6", width: 200 },
                                { field: "Resiko7", title: "Resiko 7", width: 200 },
                                { field: "Resiko8", title: "Resiko 8", width: 200 },
                                { field: "Resiko9", title: "Resiko 9", width: 200 },
                                { field: "BitDistributedIncome", title: "Resiko 9", width: 200, hidden: true },
                                { field: "KlasifikasiResikoDesc", title: "Klasifikasi Resiko", width: 200 },
                                { field: "ManajerInvestasi", title: "Manajer Investasi", width: 1000 },
                                { field: "TujuanInvestasi", title: "Tujuan Investasi", width: 1000 },
                                { field: "KebijakanInvestasi1", title: "Kebijakan Investasi 1", width: 200 },
                                { field: "KebijakanInvestasi2", title: "Kebijakan Investasi 2", width: 200 },
                                { field: "KebijakanInvestasi3", title: "Kebijakan Investasi 3", width: 200 },

                                { field: "ProfilBankCustodian", title: "Profil Bank Custodian", width: 1000 },
                                { field: "AksesProspektus", title: "Akses Prospektus", width: 1000 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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
                        $("#gridFFSForOJKHistory").empty();
                        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
                            alertify.alert("Please Fill Date");
                        }
                        else {

                            var FFSForOJKHistoryURL = window.location.origin + "/Radsoft/FFSForOJK/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                dataSourceHistory = getDataSource(FFSForOJKHistoryURL);

                        }

                        $("#gridFFSForOJKHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form FFS For OJK"
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
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "FFSForOJKPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                { field: "Date", title: "Date", width: 200, format: "{0:dd/MMM/yyyy}" },
                                { field: "FundName", title: "Fund", width: 200 },
                                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                                { field: "PeriodePenilaian", title: "Periode Penilaian ", width: 200 },
                                { field: "DanaKegiatanSosial", title: "Dana Kegiatan Sosial", width: 200 },
                                { field: "AkumulasiDanaCSR", title: "Akumulasi Dana CSR", width: 200 },
                                { field: "CSR", title: "CSR", width: 200 },
                                { field: "Resiko1", title: "Resiko 1", width: 200 },
                                { field: "Resiko2", title: "Resiko 2", width: 200 },
                                { field: "Resiko3", title: "Resiko 3", width: 200 },

                                { field: "Resiko4", title: "Resiko 4", width: 200 },
                                { field: "Resiko5", title: "Resiko 5", width: 200 },
                                { field: "Resiko6", title: "Resiko 6", width: 200 },
                                { field: "Resiko7", title: "Resiko 7", width: 200 },
                                { field: "Resiko8", title: "Resiko 8", width: 200 },
                                { field: "Resiko9", title: "Resiko 9", width: 200 },
                                { field: "BitDistributedIncome", title: "Resiko 9", width: 200, hidden: true },
                                { field: "KlasifikasiResikoDesc ", title: "Klasifikasi Resiko", width: 200 },
                                { field: "ManajerInvestasi ", title: "Manajer Investasi", width: 200 },
                                { field: "TujuanInvestasi ", title: "Tujuan Investasi", width: 200 },
                                { field: "KebijakanInvestasi1", title: "Kebijakan Investasi 1", width: 200 },
                                { field: "KebijakanInvestasi2", title: "Kebijakan Investasi 2", width: 200 },
                                { field: "KebijakanInvestasi3", title: "Kebijakan Investasi 3", width: 200 },

                                { field: "ProfilBankCustodian", title: "Profil Bank Custodian", width: 200 },
                                { field: "AksesProspektus", title: "Akses Prospektus", width: 200 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

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
        var grid = $("#gridFFSForOJKHistory").data("kendoGrid");
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
                alertify.alert("Close Detail");
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
                    var FFSForOJK = {
                        Date: $('#Date').val(),
                        FundPK: $('#FundPK').val(),
                        TemplateType: $('#TemplateType').val(),
                        PeriodePenilaian: $('#PeriodePenilaian').val(),
                        DanaKegiatanSosial: $('#DanaKegiatanSosial').val(),
                        AkumulasiDanaCSR: $('#AkumulasiDanaCSR').val(),
                        CSR: $('#CSR').val(),
                        Resiko1: $('#Resiko1').val(),
                        Resiko2: $('#Resiko2').val(),
                        Resiko3: $('#Resiko3').val(),
                        Resiko4: $('#Resiko4').val(),
                        Resiko5: $('#Resiko5').val(),
                        Resiko6: $('#Resiko6').val(),
                        Resiko7: $('#Resiko7').val(),
                        KlasifikasiResiko: $('#KlasifikasiResiko').val(),
                        ManajerInvestasi: $('#ManajerInvestasi').val(),
                        TujuanInvestasi: $('#TujuanInvestasi').val(),
                        KebijakanInvestasi1: $('#KebijakanInvestasi1').val(),
                        KebijakanInvestasi2: $('#KebijakanInvestasi2').val(),
                        KebijakanInvestasi3: $('#KebijakanInvestasi3').val(),
                        ProfilBankCustodian: $('#ProfilBankCustodian').val(),
                        AksesProspektus: $('#AksesProspektus').val(),
                        Resiko8: $('#Resiko8').val(),
                        Resiko9: $('#Resiko9').val(),
                        BitDistributedIncome: $('#BitDistributedIncome').is(":checked"),

                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/FFSForOJK/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSForOJK_I",
                        type: 'POST',
                        data: JSON.stringify(FFSForOJK),
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
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSForOJKPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSForOJK",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FFSForOJK = {
                                    FFSForOJKPK: $('#FFSForOJKPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Date: $('#Date').val(),
                                    FundPK: $('#FundPK').val(),
                                    TemplateType: $('#TemplateType').val(),
                                    PeriodePenilaian: $('#PeriodePenilaian').val(),
                                    DanaKegiatanSosial: $('#DanaKegiatanSosial').val(),
                                    AkumulasiDanaCSR: $('#AkumulasiDanaCSR').val(),
                                    CSR: $('#CSR').val(),
                                    Resiko1: $('#Resiko1').val(),
                                    Resiko2: $('#Resiko2').val(),
                                    Resiko3: $('#Resiko3').val(),
                                    Resiko4: $('#Resiko4').val(),
                                    Resiko5: $('#Resiko5').val(),
                                    Resiko6: $('#Resiko6').val(),
                                    Resiko7: $('#Resiko7').val(),
                                    KlasifikasiResiko: $('#KlasifikasiResiko').val(),
                                    ManajerInvestasi: $('#ManajerInvestasi').val(),
                                    TujuanInvestasi: $('#TujuanInvestasi').val(),
                                    KebijakanInvestasi1: $('#KebijakanInvestasi1').val(),
                                    KebijakanInvestasi2: $('#KebijakanInvestasi2').val(),
                                    KebijakanInvestasi3: $('#KebijakanInvestasi3').val(),
                                    ProfilBankCustodian: $('#ProfilBankCustodian').val(),
                                    AksesProspektus: $('#AksesProspektus').val(),
                                    Resiko8: $('#Resiko8').val(),
                                    Resiko9: $('#Resiko9').val(),
                                    BitDistributedIncome: $('#BitDistributedIncome').is(":checked"),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FFSForOJK/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSForOJK_U",
                                    type: 'POST',
                                    data: JSON.stringify(FFSForOJK),
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
                }
            });
        }
    });

    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSForOJKPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSForOJK",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FFSForOJK" + "/" + $("#FFSForOJKPK").val(),
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
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSForOJKPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSForOJK",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FFSForOJK = {
                                FFSForOJKPK: $('#FFSForOJKPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSForOJK/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSForOJK_A",
                                type: 'POST',
                                data: JSON.stringify(FFSForOJK),
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
            }
        });
    });

    $("#BtnVoid").click(function () {

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSForOJKPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSForOJK",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FFSForOJK = {
                                FFSForOJKPK: $('#FFSForOJKPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSForOJK/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSForOJK_V",
                                type: 'POST',
                                data: JSON.stringify(FFSForOJK),
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
            }
        });
    });

    $("#BtnReject").click(function () {

        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FFSForOJKPK").val() + "/" + $("#HistoryPK").val() + "/" + "FFSForOJK",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FFSForOJK = {
                                FFSForOJKPK: $('#FFSForOJKPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSForOJK/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FFSForOJK_R",
                                type: 'POST',
                                data: JSON.stringify(FFSForOJK),
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
            }
        });

    });

    $("#BtnFFSForOJK").click(function () {
        showFFSForOJKRpt();
    });

    // Untuk Form Listing
    function showFFSForOJKRpt(e) {

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


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamFundPK,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeParamFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        WinFFSForOJKRpt.center();
        WinFFSForOJKRpt.open();

    }

    $("#BtnOkFFSForOJKRpt").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == false) {
                    alertify.confirm("Are you sure want to Download FFS For OJK ?", function (e) {
                        $.blockUI({});
                        if (e) {
                            var FFSForOJK = {
                                DownloadMode: $("#DownloadMode").data("kendoComboBox").value(),
                                ParamFundPK: $("#ParamFundPK").data("kendoComboBox").value(),
                                ParamListDate: $('#ParamDate').val(),

                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/FFSForOJK/FFSForOJKRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(FFSForOJK),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    $.unblockUI({});
                                    var newwindow = window.open(data, '_blank');

                                },
                                error: function (data) {
                                    $.unblockUI({});
                                    alertify.error(data.responseText);
                                }

                            });
                        }
                    });
                }
                else {
                    alertify.alert("Date is Holiday, Please Check Date Correctly!")
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


    });


    $("#BtnCopyFFSForOJK").click(function () {

        showWinCopyFFSForOJK();
    });

    function showWinCopyFFSForOJK(e) {


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFund").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
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



        WinCopyFFSForOJK.center();
        WinCopyFFSForOJK.open();

    }

    $("#BtnOkCopyFFSForOJK").click(function () {



        alertify.confirm("Are you sure want to Copy Data FFS For OJK ?", function (e) {


            $.ajax({
                url: window.location.origin + "/Radsoft/FFSForOJK/ValidateCheckCopyFFSForOJK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamFund").val() + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy") ,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == "") {

                        var FFSForOJK = {
                            ParamFund: $('#ParamFund').val(),
                            ParamDateFrom: $('#ParamDateFrom').val(),
                            ParamDateTo: $('#ParamDateTo').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FFSForOJK/CopyFFSForOJK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(FFSForOJK),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                refresh();
                                $.unblockUI();
                                WinCopyFFSForOJK.close();
                                alertify.alert(data);

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }
                        });

                    } else {
                        alertify.alert(data);
                        $.unblockUI();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });

        }, function () {
                WinCopyFFSForOJK.close();
            $.unblockUI();
        });
    });

    $("#BtnCancelCopyFFSForOJK").click(function () {

        alertify.confirm("Are you sure want to Copy FFS For OJK ?", function (e) {
            if (e) {
                WinCopyFFSForOJK.close();
                alertify.alert("Cancel Copy FFS For OJK ");
            }
        });
    });

    $("#BtnCancelFFSForOJKRpt").click(function () {

        alertify.confirm("Are you sure want to cancel ?", function (e) {
            if (e) {
                WinFFSForOJKRpt.close();
                alertify.success("Cancel Listing");
            }
        });
    });

    function onPopUpCloseWinFFSForOJKRpt() {
    }

    function RecalGridPending() {
        $("#gridFFSForOJKPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var FFSForOJKPendingURL = window.location.origin + "/Radsoft/FFSForOJK/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(FFSForOJKPendingURL);

        }
        $("#gridFFSForOJKPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FFS For OJK"
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
                { field: "FFSForOJKPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                { field: "Date", title: "Date", width: 200, format: "{0:dd/MMM/yyyy}" },
                { field: "FundName", title: "Fund", width: 200 },
                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                { field: "PeriodePenilaian", title: "Periode Penilaian ", width: 200 },
                { field: "DanaKegiatanSosial", title: "Dana Kegiatan Sosial", width: 200 },
                { field: "AkumulasiDanaCSR", title: "Akumulasi Dana CSR", width: 200 },
                { field: "CSR", title: "CSR", width: 200 },
                { field: "Resiko1", title: "Resiko 1", width: 200 },
                { field: "Resiko2", title: "Resiko 2", width: 200 },
                { field: "Resiko3", title: "Resiko 3", width: 200 },

                { field: "Resiko4", title: "Resiko 4", width: 200 },
                { field: "Resiko5", title: "Resiko 5", width: 200 },
                { field: "Resiko6", title: "Resiko 6", width: 200 },
                { field: "Resiko7", title: "Resiko 7", width: 200 },
                { field: "Resiko8", title: "Resiko 8", width: 200 },
                { field: "Resiko9", title: "Resiko 9", width: 200 },
                { field: "BitDistributedIncome", title: "Resiko 9", width: 200, hidden: true },
                { field: "KlasifikasiResikoDesc", title: "Klasifikasi Resiko", width: 200 },
                { field: "ManajerInvestasi", title: "Manajer Investasi", width: 1000 },
                { field: "TujuanInvestasi", title: "Tujuan Investasi", width: 1000 },
                { field: "KebijakanInvestasi1", title: "Kebijakan Investasi 1", width: 200 },
                { field: "KebijakanInvestasi2", title: "Kebijakan Investasi 2", width: 200 },
                { field: "KebijakanInvestasi3", title: "Kebijakan Investasi 3", width: 200 },

                { field: "ProfilBankCustodian", title: "Profil Bank Custodian", width: 1000 },
                { field: "AksesProspektus", title: "Akses Prospektus", width: 1000 },

                { field: "EntryUsersID", title: "Entry ID", width: 200 },
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

    function RecalGridHistory() {
        $("#gridFFSForOJKHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var FFSForOJKHistoryURL = window.location.origin + "/Radsoft/FFSForOJK/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(FFSForOJKHistoryURL);

        }

        $("#gridFFSForOJKHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form FFS For OJK"
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
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "FFSForOJKPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "StatusDesc", title: "Status", width: 200 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                { field: "Date", title: "Date", width: 200, format: "{0:dd/MMM/yyyy}" },
                { field: "FundName", title: "Fund", width: 200 },
                { field: "TemplateTypeDesc", title: "Template Type", width: 200 },
                { field: "PeriodePenilaian", title: "Periode Penilaian ", width: 200 },
                { field: "DanaKegiatanSosial", title: "Dana Kegiatan Sosial", width: 200 },
                { field: "AkumulasiDanaCSR", title: "Akumulasi Dana CSR", width: 200 },
                { field: "CSR", title: "CSR", width: 200 },
                { field: "Resiko1", title: "Resiko 1", width: 200 },
                { field: "Resiko2", title: "Resiko 2", width: 200 },
                { field: "Resiko3", title: "Resiko 3", width: 200 },

                { field: "Resiko4", title: "Resiko 4", width: 200 },
                { field: "Resiko5", title: "Resiko 5", width: 200 },
                { field: "Resiko6", title: "Resiko 6", width: 200 },
                { field: "Resiko7", title: "Resiko 7", width: 200 },
                { field: "Resiko8", title: "Resiko 8", width: 200 },
                { field: "Resiko9", title: "Resiko 9", width: 200 },
                { field: "BitDistributedIncome", title: "Resiko 9", width: 200, hidden: true },
                { field: "KlasifikasiResikoDesc ", title: "Klasifikasi Resiko", width: 200 },
                { field: "ManajerInvestasi ", title: "Manajer Investasi", width: 200 },
                { field: "TujuanInvestasi ", title: "Tujuan Investasi", width: 200 },
                { field: "KebijakanInvestasi1", title: "Kebijakan Investasi 1", width: 200 },
                { field: "KebijakanInvestasi2", title: "Kebijakan Investasi 2", width: 200 },
                { field: "KebijakanInvestasi3", title: "Kebijakan Investasi 3", width: 200 },

                { field: "ProfilBankCustodian", title: "Profil Bank Custodian", width: 200 },
                { field: "AksesProspektus", title: "Akses Prospektus", width: 200 },

                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

            ]
        });
    }

    $("#BtnImportPortfolioForFFS").click(function () {
        document.getElementById("FileImportPortfolioForFFS").click();
    });

    $("#FileImportPortfolioForFFS").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportPortfolioForFFS").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }
        if (files.length > 0) {
            data.append("PortfolioForFFS", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PortfolioForFFS_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportPortfolioForFFS").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportPortfolioForFFS").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportPortfolioForFFS").val("");
        }
    });


});